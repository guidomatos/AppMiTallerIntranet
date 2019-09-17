<%@ WebHandler Language="C#" Class="FileTransferHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

public class FileTransferHandler : IHttpHandler
{
    private readonly JavaScriptSerializer js = new JavaScriptSerializer();
   
    public string StorageRoot
    {
        get { return ConfigurationManager.AppSettings["FileServerPath"] + FilesStatus.pathDoc; }
    }
    public bool IsReusable { get { return false; } }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context">paramJson</param>
    /// pathDoc = Ruta que se concatena al FileServerPath
    /// flCarpetaAnioMes => Valor booleano para crear nueva carpeta con fecha y mes actual en caso no exista
    /// flGeneraNombre_ConFecha => Valor booleano para guardar el archivo concatenando el nombre con la fecha y hora actual
    /// flOpenNewWindow => Valor booleando para indicar que el archivo se abrirá en una ventana aparte y no se descargará directo
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            FilesStatus.pathDoc = context.Request.Form["pathDoc"]; //Solo se envia cuando se guarda un nuevo archivo
            if (context.Request.Form["flCarpetaAnioMes"] != null)
            {
                Boolean flCarpetaAnioMes = false;
                Boolean.TryParse(context.Request.Form["flCarpetaAnioMes"], out flCarpetaAnioMes);
                if (flCarpetaAnioMes)
                {
                    FilesStatus.pathDoc = FilesStatus.pathDoc + Utility.GenerarRutaCarpeta() + @"\";
                }
            }

            FilesStatus.flOpenNewWindow = false;
            if (context.Request.Form["flOpenNewWindow"] != null)
            {
                Boolean flOpenNewWindow = false;
                Boolean.TryParse(context.Request.Form["flOpenNewWindow"], out flOpenNewWindow);
                if (flOpenNewWindow)
                {
                    FilesStatus.flOpenNewWindow = true;
                }
            }

            if (!System.IO.Directory.Exists(StorageRoot))
            {
                System.IO.Directory.CreateDirectory(StorageRoot);
            }

            context.Response.AddHeader("Pragma", "no-cache");
            context.Response.AddHeader("Cache-Control", "private, no-cache");

            HandleMethod(context);
        }
        catch (Exception ex)
        {
            var statuses = new List<FilesStatus>();
            statuses.Add(new FilesStatus(string.Empty, 0, ex.Message, string.Empty));
            WriteJsonIframeSafe(context, statuses);
        }
    }

    // Handle request based on method
    private void HandleMethod(HttpContext context)
    {
        string dataType = context.Request.Form["data-type"];
        if (!String.IsNullOrEmpty(dataType) && dataType == "DELETE")
        {
            DeleteFile(context);
        }
        else
        {
            switch (context.Request.HttpMethod)
            {
                case "HEAD":
                case "GET":
                    if (GivenFilename(context)) DeliverFile(context);
                    //else ListCurrentFiles(context);
                    break;
                case "POST":
                case "PUT":
                    UploadFile(context);
                    break;
                case "DELETE":
                    DeleteFile(context);
                    break;
                case "OPTIONS":
                    ReturnOptions(context);
                    break;
                default:
                    context.Response.ClearHeaders();
                    context.Response.StatusCode = 405;
                    break;
            }
        }
    }

    private static void ReturnOptions(HttpContext context)
    {
        context.Response.AddHeader("Allow", "DELETE,GET,HEAD,POST,PUT,OPTIONS");
        context.Response.StatusCode = 200;
    }

    // Delete file from the server
    private void DeleteFile(HttpContext context)
    {
        var filePath = StorageRoot + context.Request["f"];
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    // Upload file to the server
    private void UploadFile(HttpContext context)
    {
        var statuses = new List<FilesStatus>();
        var headers = context.Request.Headers;

        if (string.IsNullOrEmpty(headers["X-File-Name"]))
        {
            UploadWholeFile(context, statuses);
        }
        else
        {
            UploadPartialFile(headers["X-File-Name"], context, statuses);
        }

        WriteJsonIframeSafe(context, statuses);
    }

    // Upload partial file
    private void UploadPartialFile(string fileName, HttpContext context, List<FilesStatus> statuses)
    {
        if (context.Request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
        var inputStream = context.Request.Files[0].InputStream;
        var fullName = StorageRoot + Path.GetFileName(fileName);

        using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
        {
            var buffer = new byte[1024];

            var l = inputStream.Read(buffer, 0, 1024);
            while (l > 0)
            {
                fs.Write(buffer, 0, l);
                l = inputStream.Read(buffer, 0, 1024);
            }
            fs.Flush();
            fs.Close();
        }
        statuses.Add(new FilesStatus(new FileInfo(fullName)));
    }

    // Upload entire file
    private void UploadWholeFile(HttpContext context, List<FilesStatus> statuses)
    {
        Boolean flGeneraNombre_ConFecha = false;
        if (context.Request.Form["flGeneraNombre_ConFecha"] != null)
        {
            Boolean.TryParse(context.Request.Form["flGeneraNombre_ConFecha"], out flGeneraNombre_ConFecha);
        }
        
        for (int i = 0; i < context.Request.Files.Count; i++)
        {
            var file = context.Request.Files[i];
            string fullName = Path.GetFileName(file.FileName);
            fullName = getTextoNormalizado(fullName);
            string fullName_Original = fullName;

            if(fc_ValidarArchivo(context, file, statuses)){
                String filePath = StorageRoot + fullName;
                if (File.Exists(filePath) || flGeneraNombre_ConFecha)
                {
                    fullName = ObtenerNombre_ConFecha(fullName);
                    filePath = StorageRoot + fullName;
                }
                file.SaveAs(filePath);
                statuses.Add(new FilesStatus(fullName, file.ContentLength, string.Empty, fullName_Original));
            }
        }
    }

    private String ObtenerNombre_ConFecha(String NomArchivo)
    {
        String nom = Path.GetFileNameWithoutExtension(NomArchivo);
        String ext = Path.GetExtension(NomArchivo);
        return String.Format("{0}_{1}{2}", nom, DateTime.Now.ToString("yyyyMMddhhmmss"), ext);
    }

    /// <summary>
    /// Quita todos los acentos y caracteres no alfanuméricos excepto los puntos (.) y los guiones (-)
    /// </summary>
    /// <param name="textoOriginal"></param>
    /// <returns></returns>
    private String getTextoNormalizado(String textoOriginal)
    {
        string textoNormalizado = textoOriginal;
        textoNormalizado = textoNormalizado.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
        textoNormalizado = textoNormalizado.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");
        textoNormalizado = textoNormalizado.Replace(" ", "_");
        textoNormalizado = Regex.Replace(textoNormalizado, @"[^\w\.-]", "");
        return textoNormalizado;
    }

    private Boolean fc_ValidarArchivo(HttpContext context, HttpPostedFile file, List<FilesStatus> statuses)
    {
        string fullName = Path.GetFileName(file.FileName);
        fullName = getTextoNormalizado(fullName);
        
        Boolean fl_procesar = false;

        Int64 maxFileSize; Int64.TryParse(context.Request.Form["maxFileSize"], out maxFileSize);
        String acceptFileTypes = context.Request.Form["acceptFileTypes"];

        String msg_retorno = String.Empty;
        if (file.ContentLength > maxFileSize)
        {
            msg_retorno = "El archivo es demasiado grande. (Peso máximo: " + fc_formatFileSize(maxFileSize) + ")";
            statuses.Add(new FilesStatus(fullName, file.ContentLength, msg_retorno, fullName));
        }
        else if (file.ContentLength > 5242880) //5MB como máximo
        {
            msg_retorno = "El archivo es demasiado grande. (Peso máximo: 5 MB)";
            statuses.Add(new FilesStatus(fullName, file.ContentLength, msg_retorno, fullName));
        }
        else if (acceptFileTypes != null && fc_ValidarExtension(fullName, acceptFileTypes) == false)
        {
            msg_retorno = "Tipo de archivo no permitido (Permitidos: [" + acceptFileTypes + "]).";
            statuses.Add(new FilesStatus(fullName, file.ContentLength, msg_retorno, fullName));
        }
        else if (fc_ValidarExtension(fullName, ext_validos) == false)
        {
            msg_retorno = "Tipo de archivo no permitido por el sistema.";
            statuses.Add(new FilesStatus(fullName, file.ContentLength, msg_retorno, fullName));
        }
        else if (Regex.IsMatch(fullName.Trim(), "^[A-Za-z0-9-_.]+$") == false)
        {
            msg_retorno = "Nombre de archivo con caracteres no permitidos, modificar con letras y/o números.";
            statuses.Add(new FilesStatus(fullName, file.ContentLength, msg_retorno, fullName));
        }
        else
        {
            fl_procesar = true;
        }
        return fl_procesar;
    }

    private string ext_validos = "jpg|jpeg|png|gif|pdf|doc|docx|xls|xlsx|ppt|pptx|txt|rtf|msg|xml";
    private Boolean fc_ValidarExtension(String NomArchivo, String acceptFileTypes)
    {
        acceptFileTypes = acceptFileTypes.ToLower();
        
        String ext = Path.GetExtension(NomArchivo);
        ext = ext.ToLower().Replace(".", "");
        
        if (ext == "exe") { return false; }
        else if (acceptFileTypes.IndexOf("*") >= 0) return true;
        else if (acceptFileTypes.IndexOf(ext, 0) >= 0) { return true; }
        else { return false; }
    }
    private string fc_formatFileSize(Int64 bytes) {
        if (bytes >= 1073741824) {
            return (bytes / 1073741824).ToString("N2") + " GB";
        }
        if (bytes >= 1048576) {
            return (bytes / 1048576).ToString("N2") + " MB";
        }
        return (bytes / 1024).ToString("N2") + " KB";
    }

    private void WriteJsonIframeSafe(HttpContext context, List<FilesStatus> statuses)
    {
        context.Response.AddHeader("Vary", "Accept");
        try
        {
            if (context.Request["HTTP_ACCEPT"].Contains("application/json"))
                context.Response.ContentType = "application/json";
            else
                context.Response.ContentType = "text/plain";
        }
        catch
        {
            context.Response.ContentType = "text/plain";
        }

        var jsonObj = js.Serialize(statuses.ToArray());
        context.Response.Write(jsonObj);
    }

    private static bool GivenFilename(HttpContext context)
    {
        return !string.IsNullOrEmpty(context.Request["f"]);
    }

    private void DeliverFile(HttpContext context)
    {
        var filename = context.Request["f"];
        var filePath = StorageRoot + filename;

        int index = filename.IndexOf("\\");
        string[] arr_filename;
        if (index > 0) arr_filename = filename.Split('\\');
        else arr_filename = filename.Split('/');
        if (arr_filename.Length > 0) { filename = arr_filename[arr_filename.Length - 1]; }

        if (File.Exists(filePath))
        {
            context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            context.Response.ContentType = "application/octet-stream";
            context.Response.ClearContent();
            context.Response.WriteFile(filePath);
        }
        else
            context.Response.StatusCode = 404;
    }

    //private void ListCurrentFiles(HttpContext context)
    //{
    //    var files =
    //        new DirectoryInfo(StorageRoot)
    //            .GetFiles("*", SearchOption.TopDirectoryOnly)
    //            .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))
    //            .Select(f => new FilesStatus(f))
    //            .ToArray();

    //    string jsonObj = js.Serialize(files);
    //    context.Response.AddHeader("Content-Disposition", "inline; filename=\"files.json\"");
    //    context.Response.Write(jsonObj);
    //    context.Response.ContentType = "application/json";
    //}
}
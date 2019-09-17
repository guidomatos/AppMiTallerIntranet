<%@ WebHandler Language="C#" Class="nicUpload" %>

using System;
using System.Web;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Web.Script.Serialization;

public class nicUpload : IHttpHandler 
{
    private readonly JavaScriptSerializer js = new JavaScriptSerializer();
    private Int64 maxFileSize_Byte = 1048576; //1MB 
    private String fileServerPath = ConfigurationManager.AppSettings["FileServerPath"] + "Documentos\\Mantenimiento\\SRC\\TipoServicio\\";

    private class FileStatus
    {
        public string type { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string error { get; set; }
        public FileStatus() { }
        public FileStatus(string msg_error) { SetValues("", 0, 0, 0, msg_error); }
        public void SetValues(string msg_error)
        {
            SetValues("", 0, 0, 0, msg_error);
        }
        public FileStatus(string fileName, int fileLength, int ancho, int alto, string msg_error) { SetValues(fileName, fileLength, ancho, alto, msg_error); }
        public void SetValues(string fileName, int fileLength, int ancho, int alto, string msg_error)
        {
            String lnk = String.Empty;
            if (String.IsNullOrEmpty(msg_error))
            {
                String virtualServerPathPublic = ConfigurationManager.AppSettings["VirtualPathPublic"] + "TipoServicio/";
                lnk = virtualServerPathPublic + fileName;
            }
            
            name = fileName;
            type = "image/png";
            width = ancho;
            height = alto;
            size = fileLength;
            link = lnk;
            error = msg_error;
        }
    }
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            if (!System.IO.Directory.Exists(fileServerPath))
            {
                System.IO.Directory.CreateDirectory(fileServerPath);
            }

            context.Response.AddHeader("Pragma", "no-cache");
            context.Response.AddHeader("Cache-Control", "private, no-cache");
            
            UploadFile(context);
        }
        catch (Exception ex)
        {
            var statuses = new FileStatus(ex.Message);
            WriteJsonIframeSafe(context, statuses);
        }
    }

    // Upload file to the server
    private void UploadFile(HttpContext context)
    {
        FileStatus statuses = new FileStatus();

        for (int i = 0; i < context.Request.Files.Count; i++)
        {
            var file = context.Request.Files[i];
            string fullName = System.IO.Path.GetFileName(file.FileName);

            if (fc_ValidarArchivo(context, file, statuses))
            {
                String filePath = fileServerPath + fullName;
                if (System.IO.File.Exists(filePath))
                {
                    fullName = DateTime.Now.ToString("yyyymmddHHmmss_") + fullName;
                    filePath = fileServerPath + fullName;
                }
                file.SaveAs(filePath);
                
                System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);
                statuses.SetValues(fullName, file.ContentLength, img.Width, img.Height, string.Empty);
            }
        }

        WriteJsonIframeSafe(context, statuses);
    }

    private Boolean fc_ValidarArchivo(HttpContext context, HttpPostedFile file, FileStatus statuses)
    {
        string fullName = System.IO.Path.GetFileName(file.FileName);
        Boolean fl_procesar = false;

        String msg_retorno = String.Empty;
        if (file.ContentLength > maxFileSize_Byte)
        {
            msg_retorno = "El archivo es demasiado grande. (Peso máximo: " + fc_formatFileSize(maxFileSize_Byte) + ")";
            statuses.SetValues(msg_retorno);
        }
        else if (file.ContentLength > 1048576) //1MB como máximo
        {
            msg_retorno = "El archivo es demasiado grande. (Peso máximo: 1 MB)";
            statuses.SetValues(msg_retorno);
        }
        else if (fc_ValidarExtension(fullName, ext_validos) == false)
        {
            msg_retorno = "Tipo de archivo no permitido por el sistema.";
            statuses.SetValues(msg_retorno);
        }
        else
        {
            fl_procesar = true;
        }
        return fl_procesar;
    }
    private string ext_validos = "jpg|jpeg|png|gif";
    private Boolean fc_ValidarExtension(String NomArchivo, String acceptFileTypes)
    {
        acceptFileTypes = acceptFileTypes.ToLower();

        String ext = System.IO.Path.GetExtension(NomArchivo);
        ext = ext.ToLower().Replace(".", "");

        if (ext == "exe") { return false; }
        else if (acceptFileTypes.IndexOf("*") >= 0) return true;
        else if (acceptFileTypes.IndexOf(ext, 0) >= 0) { return true; }
        else { return false; }
    }
    private string fc_formatFileSize(Int64 bytes)
    {
        if (bytes >= 1073741824)
        {
            return (bytes / 1073741824).ToString("N2") + " GB";
        }
        if (bytes >= 1048576)
        {
            return (bytes / 1048576).ToString("N2") + " MB";
        }
        return (bytes / 1024).ToString("N2") + " KB";
    }
    private void WriteJsonIframeSafe(HttpContext context, FileStatus statuses)
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

        object res = new { data = statuses };
        var jsonObj = js.Serialize(res);
        context.Response.Write(jsonObj);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
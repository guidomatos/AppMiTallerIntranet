<%@ WebHandler Language="C#" Class="DownloadFile" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Web.Script.Serialization;


public class DownloadFile : IHttpHandler
{   
    public string StorageRoot
    {
        get { return ConfigurationManager.AppSettings["FileServerPath"]; }
    }
    public bool IsReusable { get { return false; } }

    /// <summary>
    /// Permite descargar un archivo que se encuentre en el FileServer (el nombre del archivo debe ir en la URL)
    /// Ejemplode llamada: window.open => ../SGS_Comunes/DownloadFile.ashx?nomCarpeta/nomArchivo.txt
    /// </summary>
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            String nomFile = HttpUtility.UrlDecode(context.Request.QueryString.ToString());
            
            context.Response.AddHeader("Pragma", "no-cache");
            context.Response.AddHeader("Cache-Control", "private, no-cache");

            DeliverFile(context, nomFile);
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;
        }
    }

    private void DeliverFile(HttpContext context, String filename)
    {
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

}
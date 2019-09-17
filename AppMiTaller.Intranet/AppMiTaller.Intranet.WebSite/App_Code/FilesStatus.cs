using System;
using System.IO;

public class FilesStatus
{
    public static string pathDoc { get; set; }
    public static Boolean flOpenNewWindow { get; set; }

    
    public string HandlerPath
    {        
        get { return System.Web.VirtualPathUtility.ToAbsolute("~/HandlerFileUpload/"); }
    }

    public string group { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public int size { get; set; }
    public string progress { get; set; }
    public string url { get; set; }
    public string thumbnail_url { get; set; }
    public string delete_url { get; set; }
    public string delete_type { get; set; }
    public string error { get; set; }
    public string name_ori { get; set; }

    public FilesStatus() { }

    public FilesStatus(FileInfo fileInfo) { SetValues(fileInfo.Name, (int)fileInfo.Length, string.Empty, fileInfo.Name); }

    public FilesStatus(string fileName, int fileLength, string msg_error, string fileName_Original) { SetValues(fileName, fileLength, msg_error, fileName_Original); }

    private void SetValues(string fileName, int fileLength, string msg_error, string fileName_Original)
    {
        string URLRutaArchivo = HandlerPath + "FileTransferHandler.ashx?f=";
        if (flOpenNewWindow)
        {
            URLRutaArchivo = System.Configuration.ConfigurationManager.AppSettings["VirtualPath"].ToString();
        }
        name = fileName;
        type = "image/png";
        size = fileLength;
        progress = "1.0";
        url = URLRutaArchivo + pathDoc + fileName;
        thumbnail_url = HandlerPath + "Thumbnail.ashx?f=" + pathDoc + fileName;
        delete_url = HandlerPath + "FileTransferHandler.ashx?f=" + pathDoc + fileName;
        delete_type = "DELETE";
        error = msg_error;
        name_ori = fileName_Original;
    }
}
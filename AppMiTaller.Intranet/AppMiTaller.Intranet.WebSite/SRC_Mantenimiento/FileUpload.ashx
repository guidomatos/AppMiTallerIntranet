<%@ WebHandler Language="C#" Class="FileUpload" %>

using System;
using System.Web;
using System.IO;
using AppMiTaller.Intranet.BE;

public class FileUpload : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    int mFileSize = 0;
    public void ProcessRequest(HttpContext context)
    {
        try
        {

            if (context.Request.QueryString["path"] != null && context.Request.QueryString["file"] != null)
            {
                //eliminando la imagen
                string Serverpath = context.Request.QueryString["path"].ToString();
                string filename = context.Request.QueryString["file"].ToString();
                Serverpath = String.Concat(System.Configuration.ConfigurationManager.AppSettings["FileServerPath"].ToString(), ConstanteBE.RUTA_IMAGENES_CONTENIDO, Serverpath, "\\", filename);
                Serverpath = String.Concat(System.Configuration.ConfigurationManager.AppSettings["FileServerPath"].ToString(), ConstanteBE.RUTA_IMAGENES_CONTENIDO, Serverpath, "\\", filename);
                
                if (File.Exists(Serverpath))
                {
                    File.Delete(Serverpath);
                }
            }
            else if (context.Request.QueryString["filepath"] != null && context.Request.QueryString["file"] != null)
            {
                //bajando la imagen
                string filepath = context.Request.QueryString["filepath"].ToString();
                string file = context.Request.QueryString["file"].ToString();
                string Serverpath = String.Concat(System.Configuration.ConfigurationManager.AppSettings["FileServerPath"].ToString(), ConstanteBE.RUTA_IMAGENES_CONTENIDO, filepath, "\\", file);

                if (File.Exists(Serverpath))
                {
                    context.Response.Clear();
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", file));
                    context.Response.WriteFile(Serverpath);
                    context.Response.Flush();
                }
            }
            else
            {
                //subiendo la imagen
                string Serverpath = String.Concat(System.Configuration.ConfigurationManager.AppSettings["FileServerPath"].ToString(), ConstanteBE.RUTA_IMAGENES_CONTENIDO);

                var postedFile = context.Request.Files[0];
                string filesize = "1";
                mFileSize = postedFile.ContentLength / 1048576;

                if (mFileSize <= Convert.ToInt32(filesize))
                {
                    //Get Folder de la imagen
                    string foldername = context.Request.QueryString["id"].ToString();
                    string Savepath = Serverpath + foldername;
                    string file;

                    //Si es IE
                    if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" ||
                        HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] files = postedFile.FileName.Split(new char[] { '\\' });
                        file = files[files.Length - 1];
                    }
                    //Otros Browsers
                    else
                    {
                        file = postedFile.FileName;
                    }

                    if (!Directory.Exists(Savepath))
                        Directory.CreateDirectory(Savepath);

                    string fileDirectory = Savepath + "\\" + file;
                    postedFile.SaveAs(fileDirectory);

                    //Establecer mensaje
                    string msg = "{";
                    msg += string.Format("error:'{0}',\n", string.Empty);
                    msg += string.Format("upfile:'{0}'\n", file);
                    msg += "}";
                    context.Response.Write(msg);
                }
            }
        }
        catch //(Exception ex)
        {
            //context.Response.Write("Error: " + ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
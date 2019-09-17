﻿<%@ WebHandler Language="C#" Class="Thumbnail" %>

using System;
using System.Web;

public class Thumbnail : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "image/jpg";
        context.Response.WriteFile(context.Server.MapPath("/images/default_thumb.jpg"));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
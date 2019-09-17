<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
    }
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
    }
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        Global.LastError = Server.GetLastError();
    }
    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
    }
    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
    void Application_OnBeginRequest(object sender, EventArgs e)
    {
        HttpApplication app = (HttpApplication)sender;
        HttpRequest request = app.Request;
        HttpResponse response = app.Response;

        if (request.ContentType.ToLowerInvariant().StartsWith("application/json"))
        {
            //User may be using an older version of IE which does not support compression, so skip those
            if (!((request.Browser.IsBrowser("IE")) && (request.Browser.MajorVersion <= 6)))
            {
                string acceptEncoding = request.Headers["Accept-Encoding"];

                if (!string.IsNullOrEmpty(acceptEncoding))
                {
                    acceptEncoding = acceptEncoding.ToLowerInvariant();

                    if (acceptEncoding.Contains("gzip"))
                    {
                        response.Filter = new System.IO.Compression.GZipStream(response.Filter, System.IO.Compression.CompressionMode.Compress);
                        response.AddHeader("Content-encoding", "gzip");
                    }
                    else if (acceptEncoding.Contains("deflate"))
                    {
                        response.Filter = new System.IO.Compression.DeflateStream(response.Filter, System.IO.Compression.CompressionMode.Compress);
                        response.AddHeader("Content-encoding", "deflate");
                    }
                }
            }
        }
    }
</script>
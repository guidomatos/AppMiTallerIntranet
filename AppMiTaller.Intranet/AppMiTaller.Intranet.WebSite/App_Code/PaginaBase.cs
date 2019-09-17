using System;
using System.Configuration;
using System.Web.Configuration;

public class PaginaBase : System.Web.UI.Page
{
    public PaginaBase()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
        this.PreInit += new EventHandler(Page_PreInit);
    }

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        GlobalizationSection oGlobalization = (GlobalizationSection)ConfigurationManager.GetSection("system.web/globalization");
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(oGlobalization.Culture);
    }

}
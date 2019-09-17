using System;
using System.Web.Security;
using System.Web.UI;

public partial class PaginaError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.MensajeError.Text = Global.getMensajeError();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Response.Redirect(FormsAuthentication.DefaultUrl, false);
    }

    protected void btnCerrarSesion_OnClick(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Redirect(FormsAuthentication.LoginUrl, false);
        Response.End();
    }
}

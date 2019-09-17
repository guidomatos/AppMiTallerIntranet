using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using AppMiTaller.Intranet.BE;


/// <summary>
/// Summary description for PageBase
/// </summary>
public class PageBase : System.Web.UI.Page
{
    public PageBase()
    {
        System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        System.Web.HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        System.Web.HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        System.Web.HttpContext.Current.Response.Cache.SetNoStore();
    }

    protected void MostrarMensaje(string mensaje, eTipoMensaje Tipo)
    {
        string ImagePath = String.Empty;
        switch (Convert.ToInt16(Tipo))
        {

            case 1:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenOK);
                break; // TODO: might not be correct. Was : Exit Select

            case 2:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenWaring);
                break; // TODO: might not be correct. Was : Exit Select

            case 3:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenError);
                break; // TODO: might not be correct. Was : Exit Select
            case 5:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenInfoReport);
                break; // TODO: might not be correct. Was : Exit Select

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LanzarMensaje", string.Concat("fShowMessageInfo('", mensaje, "','", ImagePath, "');"), true);

    }

    protected void MostrarMensajePopup(Page page, string mensaje, eTipoMensaje Tipo)
    {
        string ImagePath = String.Empty;
        switch (Convert.ToInt16(Tipo))
        {

            case 1:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenOK);
                break; // TODO: might not be correct. Was : Exit Select

            case 2:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenWaring);
                break; // TODO: might not be correct. Was : Exit Select

            case 3:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenError);
                break; // TODO: might not be correct. Was : Exit Select
            case 5:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenInfoReport);
                break; // TODO: might not be correct. Was : Exit Select

        }

        ScriptManager.RegisterStartupScript(page, this.GetType(), "LanzarMensaje", string.Concat("fShowMessageInfoPopup('", mensaje, "','", ImagePath, "');"), true);

    }

    protected void MostrarMensajeCorregir(string mensaje, eTipoMensaje Tipo)
    {
        string ImagePath = String.Empty;
        switch (Convert.ToInt16(Tipo))
        {

            case 1:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenOK);
                break; // TODO: might not be correct. Was : Exit Select

            case 2:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenWaring);
                break; // TODO: might not be correct. Was : Exit Select

            case 3:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenError);
                break; // TODO: might not be correct. Was : Exit Select

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LanzarMensaje", string.Concat("fShowMessageInfoError('", mensaje, "','", ImagePath, "');"), true);

    }

    protected void MostrarMensajeConfirmar(string mensaje, eTipoMensaje tipo, string parametro)
    {
        string ImagePath = String.Empty;
        switch (Convert.ToInt16(tipo))
        {

            case 4:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenQuestion);
                break; // TODO: might not be correct. Was : Exit Select

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LanzarMensaje", string.Concat("fShowMessageConfirm('", mensaje, "','", ImagePath, "','", parametro, "');"), true);

    }

    protected void MostrarMensajeConfirmarEliminar(string mensaje, eTipoMensaje tipo, string parametro)
    {
        string ImagePath = String.Empty;
        switch (Convert.ToInt16(tipo))
        {

            case 4:
                ImagePath = Page.ResolveUrl(ConstantesUI._ImagenQuestion);
                break; // TODO: might not be correct. Was : Exit Select

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LanzarMensaje", string.Concat("fShowMessageConfirmElimina('", mensaje, "','", ImagePath, "','", parametro, "');"), true);

    }

 

   
}
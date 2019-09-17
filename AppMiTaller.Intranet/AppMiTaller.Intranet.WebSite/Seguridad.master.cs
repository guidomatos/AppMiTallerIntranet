using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class Seguridad : System.Web.UI.MasterPage
{
    public bool onError { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        CargaMenu();
    }

    private void CargaMenu()
    {
        PerfilBL oPerfilBL = new PerfilBL();
        OpcionSeguridadBE oOpcionSeguridadBE = new OpcionSeguridadBE();
        OpcionSeguridadBEList oOpcionSeguridadBEList = new OpcionSeguridadBEList();
        OpcionSeguridadBEList oOpcionSeguridadBEListHijos;
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate(Transaction_ErrorEvent);
        Panel oMenuItem;
        Panel oMenuItemHijos;

        //String cadenaID;
        try
        {
            oOpcionSeguridadBE.CCOAPL = Profile.Aplicacion;
            oOpcionSeguridadBE.NIVEL = 0;
            oOpcionSeguridadBE.CSTRUCT = CONSTANTE_SEGURIDAD.Seguridad;
            oOpcionSeguridadBEList = oPerfilBL.GetAllOpciones(oOpcionSeguridadBE, Profile.Usuario.NID_PERFIL, Profile.Usuario.NID_USUARIO);

            OpcionSeguridadBE oOpSeguridad;
            for (int i = 0; i < oOpcionSeguridadBEList.Count; i++)
            {
                oOpSeguridad = oOpcionSeguridadBEList[i];
                if (oOpSeguridad.CSTRUCT.Length == 6 &&//Solo los item de la raiz
                    oOpSeguridad.fl_ind_visible.Trim().Equals("1") &&
                    oOpcionSeguridadBEList.DebeVerOpcion(oOpSeguridad.CSTRUCT))
                {
                    (Master as Principal).CreaOpcionVertical(out oMenuItem, oOpSeguridad, 1);
                    oMenuItemHijos = null;
                    if (oOpSeguridad.fl_ind_ver_hijos.Trim().Equals("1"))
                    {
                        oOpcionSeguridadBEListHijos = new OpcionSeguridadBEList();
                        for (int j = i; j < oOpcionSeguridadBEList.Count; j++)
                        {
                            if (oOpcionSeguridadBEList[j].CSTRUCT.IndexOf(oOpSeguridad.CSTRUCT) == 0)
                                oOpcionSeguridadBEListHijos.Add(oOpcionSeguridadBEList[j]);
                        }
                        //(Master as Principal).GetHijosMenuVertical(out oMenuItemHijos, 4, 2, oOpcionSeguridadBEListHijos, out cadenaID);
                        (Master as Principal).GetHijosMenuVertical(out oMenuItemHijos, 4, 2, oOpcionSeguridadBEListHijos);
                    }

                    divOpciones.Controls.Add(oMenuItem);
                    if (oMenuItemHijos != null)
                    {
                        divOpciones.Controls.Add(oMenuItemHijos);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Web_ErrorEvent(this, ex);
        }
    }
    public String ValidaTipoAccesoPagina(Page sender, String codEstructPagina)
    {
        return (Master as Principal).ValidaTipoAccesoPagina(sender, codEstructPagina);
    }
    public String ValidaAccesoOpcion(String codEstructPagina)
    {
        return (Master as Principal).ValidaAccesoOpcion(codEstructPagina);
    }
    public void Transaction_ErrorEvent(object sender, Exception ex)
    {
        (Master as Principal).Transaction_ErrorEvent(sender, ex);
    }
    public void Web_ErrorEvent(object sender, Exception ex)
    {
        (Master as Principal).Web_ErrorEvent(sender, ex);
    }
}
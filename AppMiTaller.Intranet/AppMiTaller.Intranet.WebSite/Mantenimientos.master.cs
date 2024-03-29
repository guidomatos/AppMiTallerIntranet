using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class Mantenimientos : System.Web.UI.MasterPage
{
    private bool _onError;
    public bool onError
    {
        get
        {
            return this._onError;
        }
        set
        {
            this._onError = value;
        }
    }
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
        String codEstructuraPaginaAbierta = String.Empty;

        txhSubOpciones.Value = String.Empty;
        if (codEstructuraPaginaAbierta.Trim().Equals(String.Empty) && ViewState["codEstructuraPaginaAbierta"] != null)
            codEstructuraPaginaAbierta = (String)ViewState["codEstructuraPaginaAbierta"];

        try
        {
            oOpcionSeguridadBE.CCOAPL = Profile.Aplicacion;
            oOpcionSeguridadBE.NIVEL = 0;
            oOpcionSeguridadBE.CSTRUCT = CONSTANTE_SEGURIDAD.Mantenimiento_Tablas;
            oOpcionSeguridadBEList = oPerfilBL.GetAllOpciones(oOpcionSeguridadBE, Profile.Usuario.NID_PERFIL, Profile.Usuario.Nid_usuario);
            oOpcionSeguridadBEList = oOpcionSeguridadBEList.Ordenar();

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
                        oMenuItem.Attributes["onClick"] = String.Format("javascript: return fc_SelOpcionMenuVertical('{0}', '{1}');"
                                                            , oMenuItem.ClientID, this.txhSubOpciones.ClientID);
                        txhSubOpciones.Value += oMenuItem.ClientID + "|";
                        oMenuItemHijos.ID = oMenuItem.ID + "_SUBOP";
                        oMenuItemHijos.Style["display"] = "none";

                        if (codEstructuraPaginaAbierta.IndexOf(oMenuItem.ID) == 0)
                            oMenuItemHijos.Style["display"] = "inline";

                        divOpciones.Controls.Add(oMenuItemHijos);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Web_ErrorEvent(this, ex);
        }

        ViewState["codEstructuraPaginaAbierta"] = codEstructuraPaginaAbierta;
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
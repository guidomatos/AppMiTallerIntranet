using System;
using System.Web.UI;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PerfilBL oPerfilBL = new PerfilBL();
            OpcionSeguridadBE oOpcionSeguridadBE = new OpcionSeguridadBE();

            oOpcionSeguridadBE.CCOAPL = Profile.Aplicacion;
            oOpcionSeguridadBE.NIVEL = 0;
            oOpcionSeguridadBE.CSTRUCT = String.Empty;
            Profile.Opciones = oPerfilBL.GetAllOpciones(oOpcionSeguridadBE, Profile.Usuario.NID_PERFIL, Profile.Usuario.Nid_usuario);
        }
    }
}
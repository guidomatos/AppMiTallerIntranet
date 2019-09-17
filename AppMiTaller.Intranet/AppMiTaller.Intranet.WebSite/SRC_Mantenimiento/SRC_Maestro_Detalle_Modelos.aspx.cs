using System;
using System.Web.UI;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SRC_Mantenimiento_SRC_Maestro_Detalle_Modelos : System.Web.UI.Page
{
    #region "GLOBALES"
    ModeloBE objEnt = new ModeloBE();
    ModeloBL objNeg = new ModeloBL();
    #endregion

    #region "METODOS PROPIOS"
    private void CargarMarcas()
    {
        ddl_marca.DataSource = objNeg.GETListarMarcas(Profile.Usuario.NID_USUARIO);
        ddl_marca.DataTextField = "DES";
        ddl_marca.DataValueField = "ID";
        ddl_marca.DataBind();
    }
    private void CargarNegocios()
    {
        ddl_negocio.DataSource = objNeg.GETListarNegocios();
        ddl_negocio.DataTextField = "DES";
        ddl_negocio.DataValueField = "ID";
        ddl_negocio.DataBind();
    }
    private void CargarFamiliaByNegocio(ModeloBE ent)
    {
        ddl_familia.DataSource = objNeg.GETListarFamiliasByNegocio(ent);
        ddl_familia.DataTextField = "DES";
        ddl_familia.DataValueField = "ID";
        ddl_familia.DataBind();
    }
    private void CargarEntidad()
    {
        ModeloBEList objBEList = new ModeloBEList();
        objBEList = (ModeloBEList)(Session["ModeloBEList"]);
        if (objBEList != null)
        {
            for (int i = 0; i < objBEList.Count; i++)
            {
                if (objBEList[i].nid_modelo.ToString().Trim().Equals(Session["txh_nid_modelo"].ToString().Trim()))
                    objEnt = objBEList[i];
            }
        }
    }

    private void Inicializa()
    {
        btnEditar.Visible = false;
        if (Request.QueryString["nid_modelo"] != null)
            Session["txh_nid_modelo"] = Request.QueryString["nid_modelo"];
        txt_cod.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_nommod.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_kildefecto.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_kilservicio.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_minreservacita.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_perservicio.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        CargarEntidad();
        try
        {
            string ind = Session["verdet_objEnt"].ToString();
            btnGrabar.Visible = false;
            btnEditar.Visible = true;
            txt_cod.Text = objEnt.co_modelo.ToString().Trim();
            CargarMarcas();
            ddl_marca.SelectedItem.Text = objEnt.no_marca.ToString().Trim();
            txt_nommod.Text = objEnt.no_modelo.ToString().Trim();
            CargarNegocios();
            ddl_negocio.SelectedValue = objEnt.co_negocio.ToString().Trim();
            objEnt.co_negocio = ddl_negocio.SelectedValue.ToString().Trim();
            CargarFamiliaByNegocio(objEnt);
            ddl_familia.SelectedValue = objEnt.co_familia.ToString().Trim();

            string[] lista = new string[4];
            lista = objNeg.GETListarParamByModelo(objEnt);
            txt_kildefecto.Text = (lista.GetValue(0) == null ? "" : lista.GetValue(0).ToString());
            txt_kilservicio.Text = (lista.GetValue(1) == null ? "" : lista.GetValue(1).ToString());
            txt_perservicio.Text = (lista.GetValue(2) == null ? "" : lista.GetValue(2).ToString());
            txt_minreservacita.Text = (lista.GetValue(3) == null ? "" : lista.GetValue(3).ToString());
            hid_nid_parametro.Value = (lista.GetValue(4) == null ? "" : lista.GetValue(4).ToString());
            p_DG.Enabled = false;
            PP.Enabled = false;
        }
        catch (Exception)
        {
            string ind = Session["edidet_objEnt"].ToString();
            btnGrabar.Visible = true;
            txt_cod.Text = objEnt.co_modelo.ToString().Trim();
            CargarMarcas();
            ddl_marca.SelectedItem.Text = objEnt.no_marca.ToString().Trim();
            txt_nommod.Text = objEnt.no_modelo.ToString().Trim();
            CargarNegocios();
            ddl_negocio.SelectedValue = objEnt.co_negocio.ToString().Trim();
            objEnt.co_negocio = ddl_negocio.SelectedValue.ToString().Trim();
            CargarFamiliaByNegocio(objEnt);
            ddl_familia.SelectedValue = objEnt.co_familia.ToString().Trim();

            string[] lista = new string[5];
            lista = objNeg.GETListarParamByModelo(objEnt);
            txt_kildefecto.Text = (lista.GetValue(0) == null ? "" : lista.GetValue(0).ToString());
            txt_kilservicio.Text = (lista.GetValue(1) == null ? "" : lista.GetValue(1).ToString());
            txt_perservicio.Text = (lista.GetValue(2) == null ? "" : lista.GetValue(2).ToString());
            txt_minreservacita.Text = (lista.GetValue(3) == null ? "" : lista.GetValue(3).ToString());
            hid_nid_parametro.Value = (lista.GetValue(4) == null ? "" : lista.GetValue(4).ToString());
            p_DG.Enabled = false;
            PP.Enabled = true;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOffForm('1');setTabCabeceraOffForm('0');setTabCabeceraOnForm('" + tabMantMaesModelo.ActiveTabIndex.ToString() + "');</script>", false);

        if (!Page.IsPostBack)
            Inicializa();
    }
    protected void tabMantMaesModelo_ActiveTabChanged(object sender, EventArgs e)
    {
        if (tabMantMaesModelo.ActiveTabIndex == 0)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOnForm('0'); setTabCabeceraOffForm('1');</script>", false);
        else if (tabMantMaesModelo.ActiveTabIndex == 1)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOffForm('0'); setTabCabeceraOnForm('1');</script>", false);
    }

    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        int r = 0;
        //objEnt = (ModeloBE)(Session["edidet_objEnt"]);
        CargarEntidad();

        string[] lista = new string[11];
        lista.SetValue((hid_nid_parametro.Value.Length > 0 ? hid_nid_parametro.Value.ToString() : "0"), 0);
        lista.SetValue(objEnt.nid_modelo.ToString(), 1);
        lista.SetValue((txt_kildefecto.Text.Length > 0 ? txt_kildefecto.Text.Trim() : "0"), 2);
        lista.SetValue((txt_kilservicio.Text.Length > 0 ? txt_kilservicio.Text.Trim() : "0"), 3);
        lista.SetValue((txt_perservicio.Text.Length > 0 ? txt_perservicio.Text.Trim() : "0"), 4);
        lista.SetValue((txt_minreservacita.Text.Length > 0 ? txt_minreservacita.Text.Trim() : "0"), 5);
        lista.SetValue(Profile.UserName, 6);
        lista.SetValue(Profile.UsuarioRed, 7);
        lista.SetValue(Profile.Estacion, 8);
        lista.SetValue("0", 9);
        lista.SetValue((hid_nid_parametro.Value.Length > 0 ? "2" : "1"), 10);

        r = objNeg.GETInserActuParamByModelo(lista);
        if (r > 0)
        {
            lbl_mensajebox.Text = "El registro se actualizo con exito.";
            popup_msgbox_confirm.Show();
            //Response.Redirect("SRC_Maestro_Modelos.aspx");
        }
    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SRC_Maestro_Modelos.aspx");
    }
    protected void btn_msgboxconfir_no_Click(object sender, EventArgs e)
    {
        popup_msgbox_confirm.Hide();
        Response.Redirect("SRC_Maestro_Modelos.aspx");
    }
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        btnEditar.Visible = false;
        PP.Enabled = true;
        btnGrabar.Visible = true;
    }
}
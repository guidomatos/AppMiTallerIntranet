using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SRC_Mantenimiento_SRC_Maestro_Detalle_Servicio : System.Web.UI.Page
{
    #region " Variables "
    ServicioBL objServBL = new ServicioBL();
    ServicioBE objServBE = new ServicioBE();

    TipoServicioBL objTServBL = new TipoServicioBL();
    TipoServicioBE objTServBE = new TipoServicioBE();
    private String op = "", id_serv = "", co_serv = "", no_serv = "", id_tserv = "", q_time = "", fl_quick_service = "", tipo_serv = "", no_dias_val = "", fl_activo = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //string sreTR = (String)Session["TR"];

        if (Session["op"] != null)
        {
            op = Session["op"].ToString().Trim();
        }

        if (!Page.IsPostBack)
        {
            CargaInicial();
        }
    }

    void CargarEntidad()
    {
        ServicioBEList objBEList = new ServicioBEList();
        objBEList = (ServicioBEList)(Session["ServicioBEList"]);

        for (int i = 0; i < objBEList.Count; i++)
        {
            if (objBEList[i].Id_Servicio.ToString().Trim().Equals(Session["txh_Id_Servicio"].ToString().Trim()))
            {
                id_serv = objBEList[i].Id_Servicio.ToString().Trim();
                ViewState.Add("id_serv", id_serv);
                co_serv = objBEList[i].Co_Servicio.ToString().Trim();
                no_serv = objBEList[i].No_Servicio.ToString().Trim();
                id_tserv = objBEList[i].Id_TipoServicio.ToString().Trim();
                tipo_serv = objBEList[i].No_tipo_servicio.ToString().Trim();
                q_time = objBEList[i].Qt_tiempo_prom.ToString().Trim();
                fl_quick_service = objBEList[i].Fl_quick_service.ToString().Trim();
                no_dias_val = objBEList[i].no_dias_validos.ToString().Trim();
                fl_activo = objBEList[i].Fl_activo.ToString().Trim();
            }
        }
    }
    void CargaInicial()
    {
        btnEditar.Visible = false;
        if (Request.QueryString["Id_Servicio"] != null)
        {
            Session["txh_Id_Servicio"] = Request.QueryString["Id_Servicio"];
        }

        txtCodigo.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
        txtNom.Attributes.Add("onkeypress", "return Valida_Nombre(event)");
        //txtTprom.Attributes.Add("onkeypress", "return SoloNumeros(event)");

        cboEstado.Items.Insert(0, new ListItem());
        cboEstado.Items[0].Text = "Activo";
        cboEstado.Items[0].Value = "A";
        cboEstado.Items.Insert(1, new ListItem());
        cboEstado.Items[1].Text = "Inactivo";
        cboEstado.Items[1].Value = "I";

        objTServBE.Co_tipo_servicio = "";
        objTServBE.No_tipo_servicio = "";
        objTServBE.Fl_activo = "A";
        cboTServicio.DataSource = objTServBL.BusqTServicioList(objTServBE);
        cboTServicio.DataTextField = "no_tipo_servicio";
        cboTServicio.DataValueField = "Id_TipoServicio";
        cboTServicio.DataBind();
        if (op != "0") //nuevo
            CargarEntidad();

        if (op == "1")
        {
            btnEditar.Visible = false;
            btnGrabar.Visible = true;

            p_items.Enabled = true;
            txtCodigo.Enabled = false;
            txtCodigo.Text = co_serv;
            txtNom.Text = no_serv;
            cboTServicio.SelectedValue = id_tserv;
            txtTprom.Text = q_time;

            if (fl_quick_service == "0")
                chkquickservice.Checked = false;
            else
                chkquickservice.Checked = true;

            if (fl_activo == "Activo")
            {
                cboEstado.SelectedIndex = 0;
            }
            else
            {
                cboEstado.SelectedIndex = 1;
            }
        }
        else if (op == "2")
        {
            p_items.Enabled = false;
            btnEditar.Visible = true;
            btnGrabar.Visible = false;
            txtCodigo.Enabled = false;
            txtCodigo.Text = co_serv;
            txtNom.Text = no_serv;
            cboTServicio.SelectedValue = id_tserv;
            txtTprom.Text = q_time;

            if (fl_quick_service == "0")
                chkquickservice.Checked = false;
            else
                chkquickservice.Checked = true;

            if (fl_activo == "Activo")
            {
                cboEstado.SelectedIndex = 0;
            }
            else
            {
                cboEstado.SelectedIndex = 1;
            }

            //--
            string[] _nom_dias = no_dias_val.Split('|');
            string nom_dia = string.Empty;
            foreach (ListItem dia in chkDias.Items)
            {
                dia.Selected = false;
            }
            foreach (string dias in _nom_dias)
            {
                if (String.IsNullOrEmpty(dias.Trim())) continue;
                chkDias.Items[Int32.Parse(dias) - 1].Selected = true;
            }
        }
        else
        {
            p_items.Enabled = true;
            txtCodigo.Text = "";
            txtNom.Text = "";
        }
    }

    protected void _mesg(string sms)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('" + sms + "')</script>", false);
    }
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (op == "0")
        {
            if (chkquickservice.Checked == true)
                objServBE.Fl_quick_service = "1";
            else
                objServBE.Fl_quick_service = "0";

            string nom_dia = string.Empty;
            foreach (ListItem dia in chkDias.Items)
            {
                if (dia.Selected) nom_dia += dia.Value + "|";
            }
            if (!string.IsNullOrEmpty(nom_dia)) nom_dia = nom_dia.Substring(0, nom_dia.Length - 1);

            objServBE.Co_Servicio = txtCodigo.Text.Trim();
            objServBE.No_Servicio = txtNom.Text.Trim();
            objServBE.Id_TipoServicio = Int32.Parse(cboTServicio.SelectedValue.ToString());
            objServBE.Qt_tiempo_prom = Int32.Parse(txtTprom.Text.Trim());
            objServBE.no_dias_validos = nom_dia;
            //----------------------------------------------
            objServBE.co_usuario_crea = Profile.UserName;
            objServBE.no_usuario_red = Profile.UsuarioRed;
            objServBE.no_estacion_red = Profile.Estacion;

            objServBE.Fl_activo = cboEstado.SelectedValue.ToString();

            Int32 res = 0;
            res = objServBL.InsertServicio(objServBE);
            if (res == 0)
            {
                Parametros.SRC_Mensaje_Redireccionar(this, "El registro se guardo con exito.", "SRC_Maestro_Servicio.aspx");
            }
            else if (res == 3)
            {
                //ya existe
                _mesg("Codigo ya existe.");
            }
        }
        else if (op == "1" || op == "2")
        {
            objServBE.Id_Servicio = Int32.Parse(ViewState["id_serv"].ToString());
            objServBE.Co_Servicio = txtCodigo.Text.Trim();
            objServBE.No_Servicio = txtNom.Text.Trim();
            objServBE.Id_TipoServicio = Int32.Parse(cboTServicio.SelectedValue.ToString());
            objServBE.Qt_tiempo_prom = Int32.Parse(txtTprom.Text.Trim());
            if (chkquickservice.Checked == true)
                objServBE.Fl_quick_service = "1";
            else
                objServBE.Fl_quick_service = "0";
            string nom_dia = string.Empty;
            foreach (ListItem dia in chkDias.Items)
            {
                if (dia.Selected) nom_dia += dia.Value + "|";
            }
            if (!string.IsNullOrEmpty(nom_dia)) nom_dia = nom_dia.Substring(0, nom_dia.Length - 1);
            objServBE.no_dias_validos = nom_dia;
            objServBE.co_usuario_cambio = Profile.UserName;
            objServBE.no_usuario_red = Profile.UsuarioRed;
            objServBE.no_estacion_red = Profile.Estacion;
            objServBE.Fl_activo = cboEstado.SelectedValue.ToString();

            Int32 res = 0;
            res = objServBL.ActualizarServicio(objServBE);
            if (res == 0)
            {
                Parametros.SRC_Mensaje_Redireccionar(this, "El registro se actualizo con exito.", "SRC_Maestro_Servicio.aspx");
            }
            else if (res == 3)
            {
                _mesg("Codigo ya existe.");
            }
        }
    }
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        btnEditar.Visible = false;
        btnGrabar.Visible = true;
        p_items.Enabled = true;
        txtCodigo.Enabled = false;
        txtNom.Enabled = true;
        cboTServicio.Enabled = true;
        txtTprom.Enabled = true;
        chkquickservice.Enabled = true;
        cboEstado.Enabled = true;
    }


    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SRC_Maestro_Servicio.aspx");
    }

}
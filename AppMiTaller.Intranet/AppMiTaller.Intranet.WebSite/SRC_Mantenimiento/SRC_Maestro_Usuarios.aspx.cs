using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using AppMiTaller.Intranet.BL.Exportacion;

public partial class SRC_Mantenimiento_SRC_Maestro_Usuarios : System.Web.UI.Page
{

    private TallerBEList oMaestroTallerBEList;
    TallerBE objEnt = new TallerBE();
    TallerBL objNeg = new TallerBL();

    UsuarioBEList oMaestroUsuariosBEList;
    UsuarioBE oMaestroUsuariosBE;

    private string _strDatosUsuario = string.Empty;
    private string strHorario = string.Empty;


    #region METODOS PROPIOS

    private void CargarEstado()
    {
        ddl_Estado.Items.Clear();
        ddl_Estado.Items.Add("");
        ddl_Estado.Items[0].Text = "--Todos--";
        ddl_Estado.Items[0].Value = "";
        ddl_Estado.Items.Add("");
        ddl_Estado.Items[1].Text = "Activo";
        ddl_Estado.Items[1].Value = "0";
        ddl_Estado.Items.Add("");
        ddl_Estado.Items[2].Text = "Inactivo";
        ddl_Estado.Items[2].Value = "1";
    }

    private void CargarPerfil()
    {
        UsuarioTallerBL objNegUsu = new UsuarioTallerBL();
        UsuarioBE ent = new UsuarioBE();
        ent.CUSR_ID = Profile.UserName;
        List<UsuarioBE> List = objNegUsu.GETListarPerfiles(ent);
        ddl_Perfil.Items.Clear();
        for (Int32 i = 0; i < List.Count; i++)
        {
            ddl_Perfil.Items.Add("");
            ddl_Perfil.Items[i].Value = List[i].Cod_perfil;
            ddl_Perfil.Items[i].Text = List[i].Perfil;
        }
        ddl_Perfil.Items.Insert(0, new ListItem("--Todos--", ""));
        ddl_Perfil.SelectedIndex = 0;
        if (ddl_Perfil.Items.Count > 1)
            ddl_Perfil.Enabled = true;
        else
            ddl_Perfil.Enabled = false;
        objNegUsu = null;
        List = null;
    }

    private void CargarUbigeo()
    {
        ddl_departamento.Items.Clear();
        ddl_departamento.Enabled = false;
        UsuarioTallerBL objNegUsu = new UsuarioTallerBL();
        List<UsuarioBE> ListUbigeo = objNegUsu.GETListarUbigeo(Profile.Usuario.NID_USUARIO);
        DataTable dtUbigeo = new DataTable();
        dtUbigeo.Columns.Add("coddpto", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("codprov", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("coddist", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("nombre", System.Type.GetType("System.String"));
        for (Int32 i = 0; i < ListUbigeo.Count; i++)
            dtUbigeo.Rows.Add(ListUbigeo[i].Coddpto, ListUbigeo[i].Codprov, ListUbigeo[i].Coddist, ListUbigeo[i].Ubigeo);
        ViewState.Add("dtubigeo", dtUbigeo);
        DataRow[] oRow = dtUbigeo.Select("codprov='00' AND coddist='00'", "nombre", DataViewRowState.CurrentRows);
        for (Int32 i = 0; i < oRow.Length; i++)
        {
            ddl_departamento.Items.Add("");
            ddl_departamento.Items[i].Value = oRow[i]["coddpto"].ToString();
            ddl_departamento.Items[i].Text = oRow[i]["nombre"].ToString();
        }
        if (ddl_departamento.Items.Count > 0)
            ddl_departamento.Enabled = true;
        ddl_departamento.Items.Insert(0, new ListItem("--Todos--", ""));
        ddl_departamento.SelectedIndex = 0;
        ddl_provincia.Items.Clear();
        ddl_provincia.Items.Insert(0, new ListItem("--Todos--", ""));
        ddl_provincia.SelectedIndex = 0;
        ddl_provincia.Enabled = false;
        ddl_distrito.Items.Clear();
        ddl_distrito.Items.Insert(0, new ListItem("--Todos--", ""));
        ddl_distrito.SelectedIndex = 0;
        ddl_distrito.Enabled = false;
        ddl_taller.Items.Clear();
        ddl_taller.Items.Insert(0, new ListItem("--Todos--", "0"));
        ddl_taller.SelectedIndex = 0;
        ddl_taller.Enabled = false;
        objNegUsu = null; ListUbigeo = null; dtUbigeo = null;
    }

    private void CargarTalleresDistrito()
    {
        UsuarioTallerBL objNegUsu = new UsuarioTallerBL();
        UsuarioBE objEntUsu = new UsuarioBE();
        objEntUsu.Co_Perfil_Login = Profile.Usuario.co_perfil_usuario;
        objEntUsu.Nid_Usuario_Login = Profile.Usuario.NID_USUARIO;
        List<UsuarioBE> ListTallerDist = objNegUsu.GETListarTalleresDistrito(objEntUsu);
        DataTable dtTallerDist = new DataTable();
        dtTallerDist.Columns.Add("nid_taller", System.Type.GetType("System.Int32"));
        dtTallerDist.Columns.Add("no_taller", System.Type.GetType("System.String"));
        dtTallerDist.Columns.Add("coddpto", System.Type.GetType("System.String"));
        dtTallerDist.Columns.Add("codprov", System.Type.GetType("System.String"));
        dtTallerDist.Columns.Add("coddist", System.Type.GetType("System.String"));
        for (Int32 i = 0; i < ListTallerDist.Count; i++)
            dtTallerDist.Rows.Add(ListTallerDist[i].nid_taller, ListTallerDist[i].No_taller, ListTallerDist[i].Coddpto, ListTallerDist[i].Codprov, ListTallerDist[i].Coddist);
        ViewState.Add("dttallerdist", dtTallerDist);
        ddl_taller.Items.Insert(0, new ListItem("--Todos--", "0"));
        ddl_taller.SelectedIndex = 0;
        ddl_taller.Enabled = false;
        objNegUsu = null;
        objEntUsu = null;
    }


    private void CargarPuntoRedTaller()
    {
        UsuarioTallerBL objneg = new UsuarioTallerBL();
        UsuarioBE objent = new UsuarioBE();

        objent.Co_Perfil_Login = Profile.Usuario.co_perfil_usuario;
        objent.Nid_Usuario_Login = Profile.Usuario.NID_USUARIO;

        List<UsuarioBE> List = objneg.GETListarPtoRedTaller_PorDistrito(objent);

        string strPR_Taller = string.Empty;

        foreach (UsuarioBE oEntidad in List)
        {
            strPR_Taller += oEntidad.Nid_ubica.ToString() + "|" + oEntidad.nid_taller.ToString() + "=";
        }

        strPR_Taller = (!string.IsNullOrEmpty(strPR_Taller) ? strPR_Taller.Substring(0, strPR_Taller.Trim().Length - 1) : string.Empty);

        ViewState["PuntoRed_Taller"] = strPR_Taller;

    }

    private void Label_X_Pais()
    {
        Parametros oParm = new Parametros();

        lbl_dep_taller.Text = oParm.N_Departamento.ToString();
        lbl_prov_taller.Text = oParm.N_Provincia.ToString();
        lbl_dist_taller.Text = oParm.N_Distrito.ToString();
        lbl_nom_taller.Text = oParm.N_Taller.ToString();

        gdUsuarios.Columns[8].HeaderText = oParm.N_Departamento.ToString();
        gdUsuarios.Columns[9].HeaderText = oParm.N_Provincia.ToString();
        gdUsuarios.Columns[10].HeaderText = oParm.N_Distrito.ToString();
        gdUsuarios.Columns[11].HeaderText = oParm.N_Taller.ToString();
    }

    private void MensajeScript(string SMS)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('" + SMS + "');</script>", false);

    }
    private void SRC_MsgInformacion(string strError)
    {
        MensajeScript(strError);
    }

    private TallerBEList CargarServicios()
    {
        return objNeg.GETListarServicios();
    }

    private TallerBEList CargarMarcasModelos()
    {
        TallerBE ent = new TallerBE();
        ent.Co_perfil_usuario = Profile.Usuario.co_perfil_usuario;
        ent.Nid_usuario = Profile.Usuario.NID_USUARIO;
        return objNeg.GETListarMarcasModelos(ent);
    }


    #endregion

    private void CargarTipoDocumento()
    {
        CitasBL objBLT = new CitasBL();
        CitasBEList list = objBLT.GETListarTipoDocumentos();
        string strTiposDoc = string.Empty;
        foreach (CitasBE oEntidad in list)
        {
            strTiposDoc += oEntidad.cod_tipo_documento.ToString() + '=' + oEntidad.des_tipo_documento.ToString() + '|';
        }
        if (strTiposDoc.Length > 0) strTiposDoc = strTiposDoc.Substring(0, strTiposDoc.Length - 1);
        ViewState["CargarTipoDocumento"] = strTiposDoc;
    }

    private void CargarIDUbica()
    {
        string strID = string.Empty;

        TallerBL objNegTal = new TallerBL();
        TallerBE objEntTal = new TallerBE();
        objEntTal.Co_perfil_usuario = Profile.Usuario.co_perfil_usuario;
        objEntTal.Nid_usuario = Profile.Usuario.NID_USUARIO;
        List<TallerBE> List = objNegTal.GETListarUbicacion(objEntTal);
        if (List.Count > 0)
        {
            foreach (TallerBE oEntidad in List)
            {
                strID += oEntidad.nid_ubica.ToString() + '|';
            }
            if (strID.Length > 0) strID = strID.Substring(0, strID.Length - 1);
        }
        ViewState["CargarIDUbica"] = strID;
    }

    #region Seguridad_Botones_Accion

    public bool NuevoBoton()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantUsuario_AccionNuevo).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool VerBoton()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantUsuario_AccionVerDetalle).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool EditarBoton()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantUsuario_AccionEditar).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }

    #endregion

    protected void InicializaPagina()
    {
        ViewState.Add("ordenGridUsuarios", SortDirection.Descending);

        Label_X_Pais();
        CargarEstado();
        CargarPerfil();
        CargarUbigeo();
        CargarTalleresDistrito();
        CargarPuntoRedTaller();
        //--------------------------------
        CargarTipoDocumento();
        CargarIDUbica();
        CargarServicios();
        CargarMarcasModelos();

        //INICIALIZANDO EL GRIDVIEW
        this.oMaestroUsuariosBEList = new UsuarioBEList();
        this.oMaestroUsuariosBEList.Add(new UsuarioBE());
        Session["ListUsu"] = this.oMaestroUsuariosBEList;
        this.gdUsuarios.DataSource = this.oMaestroUsuariosBEList;
        this.gdUsuarios.DataBind();


        if (Session["filtros_usu"] != null)
        {
            txt_NroDNI.Text = Session["filtros_usu"].ToString().Split('|')[0];
            txt_Nombres.Text = Session["filtros_usu"].ToString().Split('|')[1];
            ddl_departamento.SelectedValue = Session["filtros_usu"].ToString().Split('|')[2];
            txt_Usuario.Text = Session["filtros_usu"].ToString().Split('|')[3];
            txt_ApPaterno.Text = Session["filtros_usu"].ToString().Split('|')[4];
            CargarProvinciaPorDepartamento();
            ddl_provincia.SelectedValue = Session["filtros_usu"].ToString().Split('|')[5];
            ddl_Estado.SelectedValue = Session["filtros_usu"].ToString().Split('|')[6];
            txt_ApMaterno.Text = Session["filtros_usu"].ToString().Split('|')[7];
            CargarDistritoPorProvincia();
            ddl_distrito.SelectedValue = Session["filtros_usu"].ToString().Split('|')[8];
            ddl_Perfil.SelectedValue = Session["filtros_usu"].ToString().Split('|')[9];
            CargarTallerPorDistrito();
            ddl_taller.SelectedValue = Session["filtros_usu"].ToString().Split('|')[10];
            btnBuscar_Click(null, null);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        string AccesoPagina = (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantUsuario_AccionVerForm);
        if (string.IsNullOrEmpty(AccesoPagina))
            AccesoPagina = (Master as Principal).ValidaTipoAccesoPagina(Page, "SinAcceso");

        btnNuevo.Visible = NuevoBoton();
        btnVerDet.Visible = VerBoton();
        BtnEditar.Visible = EditarBoton();

        txt_NroDNI.Attributes.Add("onKeypress", "return Valida_DNI(event)");
        txt_Usuario.Attributes.Add("onKeypress", "return Valida_Usuario(event)");
        txt_Nombres.Attributes.Add("onKeypress", "return Valida_Nombre(event)");
        txt_ApPaterno.Attributes.Add("onKeypress", "return Valida_ApePaterno(event)");
        txt_ApMaterno.Attributes.Add("onKeypress", "return Valida_ApeMaterno(event)");

        if (!Page.IsPostBack)
        {
            InicializaPagina();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Session["ListUsu"] != null &&
            this.gdUsuarios != null &&
            this.gdUsuarios.Rows.Count > 0 &&
            this.gdUsuarios.PageCount > 1)
        {
            GridViewRow oRow = this.gdUsuarios.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", ((List<UsuarioBE>)Session["ListUsu"]).Count);
                oRow.Cells[0].Controls.AddAt(0, oTotalReg);
                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }



    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        hf_exportaexcel.Value = "";

        UsuarioTallerBL objNegUsu = new UsuarioTallerBL();
        UsuarioBE objEntUsu = new UsuarioBE();

        objEntUsu.Co_Perfil_Login = Profile.Usuario.co_perfil_usuario;
        objEntUsu.Nid_Usuario_Login = Profile.Usuario.NID_USUARIO;
        objEntUsu.CUSR_ID = txt_Usuario.Text.Trim();
        objEntUsu.Nu_tipo_documento = txt_NroDNI.Text.Trim();
        objEntUsu.VNOMUSR = txt_Nombres.Text.Trim();
        objEntUsu.Fl_activo = ddl_Estado.SelectedValue;
        objEntUsu.No_ape_paterno = txt_ApPaterno.Text.Trim();
        objEntUsu.No_ape_materno = txt_ApMaterno.Text.Trim();
        objEntUsu.Cod_perfil = ddl_Perfil.SelectedValue;
        objEntUsu.Coddpto = ddl_departamento.SelectedValue;
        objEntUsu.Codprov = ddl_provincia.SelectedValue;
        objEntUsu.Coddist = ddl_distrito.SelectedValue;
        objEntUsu.nid_taller = Convert.ToInt32(ddl_taller.SelectedValue);


        this.oMaestroUsuariosBEList = new UsuarioBEList();
        this.oMaestroUsuariosBEList = objNegUsu.GETListarUsuarios(objEntUsu);
        //List<UsuarioBE> ListUsu = objNegUsu.GETListarUsuarios(objEntUsu);


        if (oMaestroUsuariosBEList == null || oMaestroUsuariosBEList.Count == 0)
        {
            Session["filtros_usu"] = null;

            //INICIALIZANDO EL GRIDVIEW
            this.oMaestroUsuariosBE = null;
            this.oMaestroUsuariosBE = new UsuarioBE();
            this.oMaestroUsuariosBEList.Add(new UsuarioBE());
        }
        else
        {
            hf_exportaexcel.Value = "1";
            EnviarFiltros();
        }

        Session["ListUsu"] = oMaestroUsuariosBEList;

        gdUsuarios.DataSource = oMaestroUsuariosBEList;
        gdUsuarios.DataBind();

        objNegUsu = null; objEntUsu = null;

    }

    protected void btnVerDet_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_nid_usuario.Value.ToString() != "")
        {
            Session["nid_usuario_detalle"] = txh_nid_usuario.Value.ToString();
            Session["nid_usuario_editar"] = null;
            Session["nid_usuario_nuevo"] = null;
            EnviarFiltros();
            Response.Redirect("SRC_Maestro_Detalle_Usuarios.aspx");
        }
    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session["nid_usuario_detalle"] = null;
        Session["nid_usuario_editar"] = null;
        Session["nid_usuario_nuevo"] = "n";
        EnviarFiltros();
        Response.Redirect("SRC_Maestro_Detalle_Usuarios.aspx");
    }

    private void EnviarFiltros()
    {
        Session["filtros_usu"] = txt_NroDNI.Text.Trim() + "|" + txt_Nombres.Text.Trim() + "|" +
                ddl_departamento.SelectedValue + "|" + txt_Usuario.Text.Trim() + "|" +
                txt_ApPaterno.Text.Trim() + "|" + ddl_provincia.SelectedValue + "|" +
                ddl_Estado.SelectedValue + "|" + txt_ApMaterno.Text.Trim() + "|" +
                ddl_distrito.SelectedValue + "|" + ddl_Perfil.SelectedValue + "|" + ddl_taller.SelectedValue;
    }

    protected void BtnEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_nid_usuario.Value.ToString() != "")
        {
            Session["nid_usuario_editar"] = txh_nid_usuario.Value.ToString();
            Session["nid_usuario_detalle"] = null;
            Session["nid_usuario_nuevo"] = null;
            EnviarFiltros();
            Response.Redirect("SRC_Maestro_Detalle_Usuarios.aspx");
        }
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            UsuarioTallerBL oMaestroUsuariosBL = new UsuarioTallerBL();
            oMaestroUsuariosBL.ErrorEvent += new UsuarioTallerBL.ErrorDelegate(Master.Transaction_ErrorEvent);
            UsuarioBE objEntUsu = new UsuarioBE();

            objEntUsu.Co_Perfil_Login = Profile.Usuario.co_perfil_usuario;
            objEntUsu.Nid_Usuario_Login = Profile.Usuario.NID_USUARIO;
            objEntUsu.CUSR_ID = txt_Usuario.Text.Trim();
            objEntUsu.Nu_tipo_documento = txt_NroDNI.Text.Trim();
            objEntUsu.VNOMUSR = txt_Nombres.Text.Trim();
            objEntUsu.Fl_activo = ddl_Estado.SelectedValue;
            objEntUsu.No_ape_paterno = txt_ApPaterno.Text.Trim();
            objEntUsu.No_ape_materno = txt_ApMaterno.Text.Trim();
            objEntUsu.Cod_perfil = ddl_Perfil.SelectedValue;
            objEntUsu.Coddpto = ddl_departamento.SelectedValue;
            objEntUsu.Codprov = ddl_provincia.SelectedValue;
            objEntUsu.Coddist = ddl_distrito.SelectedValue;
            objEntUsu.nid_taller = Convert.ToInt32(ddl_taller.SelectedValue);

            this.oMaestroUsuariosBEList = new UsuarioBEList();
            this.oMaestroUsuariosBEList = oMaestroUsuariosBL.GETListarUsuarios(objEntUsu);


            const string RUTA_DOCUMENTOS = ConstanteBE.RUTA_MANTENIMIENTO_SRC;
            String carpeta = String.Empty, nombre = String.Empty, RutaFinal = String.Empty;
            String ruta = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + RUTA_DOCUMENTOS;
            ruta = Utility.CrearCarpetaFileServer(ruta);

            String fl_Ruta = ConstanteBE.FLAT_EXCEL_SRC;
            ExportarExcelXml oExportarExcelXml = new ExportarExcelXml();
            String archivo = oExportarExcelXml.GenerarExcelExportableUsuario(this.oMaestroUsuariosBEList, ruta);

            if (!archivo.Equals("-1"))
            {
                nombre = archivo;
                carpeta = Utility.ObtenerCarpetaFileServer(nombre);
                archivo = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + RUTA_DOCUMENTOS + carpeta + @"\" + Convert.ToString(nombre.Trim());
                if (File.Exists(archivo))
                {
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), String.Empty, "<script> window.open('../SGS_Comunes/SGS_VerExcelExpotable.aspx?nombreArchivo=" + nombre + "&flatRuta=" + fl_Ruta + "')</script>");
                }
            }
            else
            {
                JavaScriptHelper.Alert(this, "'Problemas al generar el excel, consulte con el administrador.'", String.Empty);
            }
        }
        catch (Exception ex)
        {
            Master.Web_ErrorEvent(this, ex);
            JavaScriptHelper.Alert(this, ex.Message.ToString(), String.Empty);
        }
    }

    protected void ddl_departamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarProvinciaPorDepartamento();
    }

    private void CargarProvinciaPorDepartamento()
    {
        ddl_provincia.Items.Clear();
        ddl_provincia.Enabled = false;
        if (ddl_departamento.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtubigeo"]).Select("codprov <> '00' AND coddist='00' AND coddpto='" + ddl_departamento.SelectedValue + "'", "nombre", DataViewRowState.CurrentRows);
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_provincia.Items.Add("");
                ddl_provincia.Items[i].Value = oRow[i]["codprov"].ToString();
                ddl_provincia.Items[i].Text = oRow[i]["nombre"].ToString();
            }
            if (ddl_provincia.Items.Count > 0)
                ddl_provincia.Enabled = true;
        }
        ddl_provincia.Items.Insert(0, new ListItem("--Todos--", ""));
        ddl_provincia.SelectedIndex = 0;

        ddl_distrito.Items.Clear();
        ddl_distrito.Items.Insert(0, new ListItem("--Todos--", ""));
        ddl_distrito.SelectedIndex = 0;
        ddl_distrito.Enabled = false;

        ddl_taller.Items.Clear();
        ddl_taller.Items.Insert(0, new ListItem("--Todos--", "0"));
        ddl_taller.SelectedIndex = 0;
        ddl_taller.Enabled = false;
    }

    protected void ddl_provincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDistritoPorProvincia();
    }

    private void CargarDistritoPorProvincia()
    {
        ddl_distrito.Items.Clear();
        ddl_distrito.Enabled = false;
        if (ddl_provincia.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtubigeo"]).Select("codprov <> '00' AND coddist <> '00' AND coddpto='" + ddl_departamento.SelectedValue + "' AND codprov='" + ddl_provincia.SelectedValue + "'", "nombre", DataViewRowState.CurrentRows);
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_distrito.Items.Add("");
                ddl_distrito.Items[i].Value = oRow[i]["coddist"].ToString();
                ddl_distrito.Items[i].Text = oRow[i]["nombre"].ToString();
            }
            if (ddl_distrito.Items.Count > 0)
                ddl_distrito.Enabled = true;
        }
        ddl_distrito.Items.Insert(0, new ListItem("--Todos--", ""));
        ddl_distrito.SelectedIndex = 0;
        ddl_taller.Items.Clear();
        ddl_taller.Items.Insert(0, new ListItem("--Todos--", "0"));
        ddl_taller.SelectedIndex = 0;
        ddl_taller.Enabled = false;
    }

    private void CargarTallerPorDistrito()
    {
        ddl_taller.Items.Clear();
        ddl_taller.Enabled = false;
        if (ddl_distrito.SelectedValue != "")
        {
            DataRow[] oRow = ((DataTable)ViewState["dttallerdist"]).Select("coddpto='" + ddl_departamento.SelectedValue + "' AND codprov='" + ddl_provincia.SelectedValue + "' AND coddist='" + ddl_distrito.SelectedValue + "'", "no_taller", DataViewRowState.CurrentRows);
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_taller.Items.Add("");
                ddl_taller.Items[i].Value = oRow[i]["nid_taller"].ToString();
                ddl_taller.Items[i].Text = oRow[i]["no_taller"].ToString();
            }
            if (ddl_taller.Items.Count > 0) ddl_taller.Enabled = true;
        }
        ddl_taller.Items.Insert(0, new ListItem("--Todos--", "0"));
        ddl_taller.SelectedIndex = 0;
    }

    protected void ddl_distrito_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarTallerPorDistrito();
    }

    #region Metodos Grid usuarios

    protected void gdUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdUsuarios.DataSource = (List<UsuarioBE>)Session["ListUsu"];
        gdUsuarios.PageIndex = e.NewPageIndex;
        gdUsuarios.DataBind();
        txh_nid_usuario.Value = String.Empty;
    }
    protected void gdUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = gdUsuarios.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["nid_usuario"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }
            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                            , txh_nid_usuario.ClientID, dataKey.Values["nid_usuario"].ToString());
            if (VerBoton())
                e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SRC_Maestro_Detalle_Usuarios.aspx?nid_usu={0}'", dataKey.Values["nid_usuario"].ToString());
        }
    }
    protected void gdUsuarios_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            UsuarioBEList oMaestroUsuariosBEList = (UsuarioBEList)(List<UsuarioBE>)Session["ListUsu"];
            SortDirection indOrden = (SortDirection)ViewState["ordenGridUsuarios"];

            txh_nid_usuario.Value = String.Empty;

            if (oMaestroUsuariosBEList != null)
            {
                if (indOrden == SortDirection.Ascending)
                {
                    oMaestroUsuariosBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                    ViewState["ordenGridUsuarios"] = SortDirection.Descending;
                }
                else
                {
                    oMaestroUsuariosBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                    ViewState["ordenGridUsuarios"] = SortDirection.Ascending;
                }
            }
            gdUsuarios.DataSource = oMaestroUsuariosBEList;
            gdUsuarios.DataBind();
            Session["ListUsu"] = oMaestroUsuariosBEList;
        }
        catch { }
    }
    #endregion

    protected void ddl_taller_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    private Excel.Application applExcel;
    private Excel._Workbook workBook;
    private Excel._Worksheet workSheet;

    private string Salto()
    {
        return "\\n";
    }

    public string ImportarDatosUsuarioFromExcel(String path)
    {

        string strResp = "1";
        string ID_PuntoRed = string.Empty;


        try
        {
            Inicia();

            workBook = applExcel.Workbooks.Open(path, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            _strDatosUsuario = string.Empty;
            strHorario = string.Empty;

            //----------------------------------------------------
            Int32 rowIndex = 3;
            bool fl_ok = true;
            string strMensaje = string.Empty;
            DataRow row;

            string strID = string.Empty;
            Int32 reg = 0;
            string strHoja = string.Empty;
            string strError = string.Empty;
            bool existeID = false;

            strHorario = string.Empty;
            string strIDTaller = string.Empty;
            string strIDModelo = string.Empty;
            string strIDServicio = string.Empty;

            DataTable dtUsuario = null;
            DataTable dtUsuarioTaller = null;
            DataTable dtUsuarioModelo = null;
            DataTable dtUsuarioServicio = null;
            DataTable dtUsuarioHorario = null;
            DataTable dtUsuarioCapacidad = null;
            DataTable dtUsuarioFechaExep = null;

            // 0 -> Asesor de Servicio
            // 1 -> Administrador de Taller
            string idTipoUsu = rbTipo.Items[rbTipo.SelectedIndex].Value;
            //string idTipoUsu = "0";

            string strTipoUsuario = rbTipo.Items[rbTipo.SelectedIndex].Text;
            //string strTipoUsuario = "Asesor de Servicio";

            //--> 6
            string[] nomHoja = Convert.ToString("" +
                                "DATOS|" +
                                "TALLERES|" +
                                "MODELOS|" +
                                "SERVICIOS|" +
                                "HORARIO|" +
                                "CAPACIDAD_ATENCION|" +
                                "FECHAS_EXCEPTUADAS" +
                                "").Split('|');
            /****************************************************************************************************************/

            bool fl_NoHoja = false;
            foreach (Excel._Worksheet _oHoja in workBook.Sheets)
            {
                fl_NoHoja = false;

                foreach (string _strHoja in nomHoja)
                {
                    if (idTipoUsu.Equals("1"))
                    {
                        if (_strHoja.Equals(nomHoja[3]) || _strHoja.Equals(nomHoja[4]) || _strHoja.Equals(nomHoja[5]) || _strHoja.Equals(nomHoja[6]))
                        {
                            continue;
                        }
                    }

                    if (_strHoja.Equals(_oHoja.Name.ToUpper()))
                    {
                        fl_NoHoja = true;
                        break;
                    }
                }
                if (!fl_NoHoja)
                {
                    SRC_MsgInformacion("El archivo excel no posee el formato correcto.");
                    return "-1";
                }
            }

            /*********************  MAE_USUARIO   *************************************************************************************/


            #region MAE_USUARIO
            workSheet = (Excel.Worksheet)workBook.Sheets[nomHoja[0]]; //--> DATOS USUARIO
            strHoja = nomHoja[0];

            string[] nomColumnas = Convert.ToString("ID|" +
                                    "TIPO_DOC|" +
                                    "NRO_DOCUMENTO|" +
                                    "NOMBRES|" +
                                    "APE_PATERNO|" +
                                    "APE_MATERNO|" +
                                    "LOGIN|" +
                                    "PASSWORD|" +
                                    "CORREO|" +
                                    "TELF_FIJO|" +
                                    "TELF_MOVIL|" +
                                    "NID_UBICA|" +
                                    "ESTADO|" +
                                    "FECHA_INICIO|" +
                                    "FECHA_FIN|" +
                                    "HORA_INICIO|" +
                                    "HORA_FIN|" +
                                    "MENSAJE|" +
                                    "CONSULTA_VIN").Split('|');

            if (workSheet == null)
            {
                SRC_MsgInformacion("No existe la Hoja [" + strHoja + "] en el Excel.");
                return "-1";
            }

            dtUsuario = new DataTable();
            foreach (string nomColumna in nomColumnas)
            {
                dtUsuario.Columns.Add(nomColumna);
            }

            reg = 0;
            row = null;
            rowIndex = 3;
            fl_ok = true;
            strMensaje = string.Empty;

            while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                reg++;
                row = dtUsuario.NewRow();

                for (Int32 i = 0; i < dtUsuario.Columns.Count; i++)
                {
                    row[i] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, i + 2]).Text).Trim();
                }

                //---------------------------------------------------------------------------------------
                strID += row[0] + "|";
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validacio 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (EsVacio(row[0])) { strMensaje += "- No contiene el código del " + strTipoUsuario + ". \\n"; }
                if (EsVacio(row[1])) { strMensaje += "- No contiene el código del tipo de documento. \\n"; }
                if (EsVacio(row[2])) { strMensaje += "- No contiene su número de documento. \\n"; }
                if (EsVacio(row[3])) { strMensaje += "- No contiene su nombre. \\n"; }
                if (EsVacio(row[4])) { strMensaje += "- No contiene su apellido paterno. \\n"; }
                if (EsVacio(row[6])) { strMensaje += "- No contiene su login. \\n"; }
                //if (EsVacio(row[7])) { strMensaje += "- No contiene su password. \\n"; }
                if (EsVacio(row[11])) { strMensaje += "- No contiene su ID de Ubicación. \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del " + strTipoUsuario + " debe ser entero. \\n"; }
                if (!EsEntero(row[1].ToString())) { strMensaje += "- El código de intervalo del taller debe ser entero. \\n"; }
                if (row[6].ToString().Trim().Length < 4) { strMensaje += "- El login del " + strTipoUsuario + " debe tener al menos 4 caracteres. \\n"; }
                if (!EsVacio(row[7])) { if (row[7].ToString().Length < 4) { strMensaje += "- El password del " + strTipoUsuario + " debe tener al menos 4 caracteres. \\n"; } }
                if (!EsVacio(row[8])) { if (!EsEmail(row[8])) { strMensaje += "- El email del " + strTipoUsuario + " es inválido. \\n"; } }
                if (!EsVacio(row[10])) { if (row[10].ToString().Length < 9) { strMensaje += "- El teléfono nóvil debe tener al menos 9 dígitos. \\n"; } }
                if (!EsVacio(row[11])) { if (!EsEntero(row[11].ToString())) { strMensaje += "- El ID de ubicación debe ser entero. \\n"; } }

                if (!EsVacio(row[12]) || !EsVacio(row[13]))
                {
                    if (!EsVacio(row[12])) { if (!EsTipoFecha(row[12])) { strMensaje += "- La fecha de inicio de acceso debe estar en el formato " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + "  \\n"; } }
                    if (!EsVacio(row[13])) { if (!EsTipoFecha(row[13])) { strMensaje += "- La fecha de fin de acceso debe estar en el formato " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + "  \\n"; } }

                    if (!EsVacio(row[12]) && EsVacio(row[13])) { strMensaje += "- Falta ingresar la fecha de fin de acceso. \\n"; }
                    if (!EsVacio(row[13]) && EsVacio(row[12])) { strMensaje += "- Falta ingresar la fecha de inicio de acceso. \\n"; }

                    if (!EsVacio(row[12]) && !EsVacio(row[13]))
                    {
                        if (EsTipoFecha(row[12]) && EsTipoFecha(row[13]))
                        {
                            if (Convert.ToDateTime(row[12]) >= Convert.ToDateTime(row[13])) { strMensaje += "- La fecha final de acceso debe ser mayor que la fecha de inicio. \\n"; }
                        }
                    }
                }

                if (!EsVacio(row[14]) || !EsVacio(row[15]))
                {
                    if (!EsVacio(row[14])) { if (!EsTipoHora(row[14])) { strMensaje += "- La hora de inicio de acceso debe estar en el formato [HH:mm]  \\n"; } }
                    if (!EsVacio(row[15])) { if (!EsTipoHora(row[15])) { strMensaje += "- La hora de fin de acceso debe estar en el formato [HH:mm]  \\n"; } }

                    if (!EsVacio(row[14]) && EsVacio(row[15])) { strMensaje += "- Falta ingresar la hora final de acceso. \\n"; }
                    if (!EsVacio(row[15]) && EsVacio(row[14])) { strMensaje += "- Falta ingresar la hora de inicio de acceso. \\n"; }

                    if (!EsVacio(row[14]) && !EsVacio(row[15]))
                    {
                        if (EsTipoHora(row[14]) && EsTipoHora(row[15]))
                        {
                            if (Convert.ToDateTime(row[14]) >= Convert.ToDateTime(row[15])) { strMensaje += "- La hora final de acceso debe ser mayor que la fecha de inicio. \\n"; }
                        }
                    }
                }


                if (!row[16].ToString().Equals("0") && !row[16].ToString().Equals("1")) { strMensaje += "- Consulta de vin debe ser 1 ó 0.  \\n"; }


                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 3 - Existe dicho dato en BD
                //--------------------------------------------------------------------------------------------

                string strTD = ViewState["CargarTipoDocumento"].ToString();
                existeID = false;

                foreach (string strDOC in strTD.Split('|'))
                {
                    if (row[1].ToString().Equals(strDOC.Split('=').GetValue(0)))
                    {

                        string oDNI = ConfigurationManager.AppSettings["TIPODOCDNI"].ToString();
                        string oRUC = ConfigurationManager.AppSettings["TIPODOCRUC"].ToString();

                        Parametros oParm = new Parametros();

                        string numDoc = row[2].ToString();

                        if (row[1].ToString().Equals(oDNI))
                        {
                            //DNI - RUT
                            if (oParm.SRC_Pais.Equals(1))
                            {
                                if (!EsDNI(row[2].ToString())) { strMensaje += "- El número de DNI debe tener 8 dígitos enteros. \\n"; }
                            }
                            else
                            {
                                if (!EsRUT(row[2].ToString())) { strMensaje += "- El número de RUT es inválido. \\n"; }
                            }
                        }
                        else if (row[1].ToString().Equals(oRUC))
                        {
                            //RUC
                            if (!EsRUC(row[2].ToString())) { strMensaje += "- El número de RUC debe tener 11 dígitos enteros. \\n"; }

                        }
                        else
                        {
                            //OTRO
                            //txt_nro_documento.MaxLength = 20;
                            //txt_nro_documento.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
                        }

                        existeID = true;
                        break;
                    }
                }
                if (!existeID)
                {
                    strMensaje = "- El Tipo de documento [" + row[1].ToString() + "] no existe en la BD.";
                    fl_ok = false;
                    break;
                }

                //CargarIDUbica
                //------------------------------------------------------------
                existeID = false;
                string strPuntoRedes = ViewState["PuntoRed_Taller"].ToString();
                foreach (string strPuntoRed in strPuntoRedes.Split('='))
                {
                    if (row[11].ToString().Equals(strPuntoRed.Split('|').GetValue(0).ToString()))
                    {
                        existeID = true;
                        break;
                    }
                }
                if (!existeID)
                {
                    strMensaje += "- El id de ubicación no existe en la BD. \\n";
                    fl_ok = false;
                }


                //-------------------->>>

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }


                //---------------------------------------------------------------------------------------------
                rowIndex++;
                dtUsuario.Rows.Add(row);

                //_strDatosUsuario += row[0].ToString() + '|' + row["CO_INTERVALO_ATENC"].ToString() + '|' + strIntervalo + "=";
                ID_PuntoRed += row[0].ToString() + "|" + row[11].ToString() + "=";
            }

            if (reg == 0)
            {
                SRC_MsgInformacion("- No hay registro en la Hoja " + strHoja + ".");
                fl_ok = false;
            }

            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }
            //--------------------------------------------------------------------------------------------------------------

            if (!String.IsNullOrEmpty(strID.Trim())) strID = strID.Substring(0, strID.Length - 1);
            if (!String.IsNullOrEmpty(ID_PuntoRed.Trim())) ID_PuntoRed = ID_PuntoRed.Substring(0, ID_PuntoRed.Length - 1);


            #endregion

            /*********************  MAE_USUARIO_TALLER  *******************************************************************************/

            #region MAE_USUARIO_TALLER
            workSheet = (Excel.Worksheet)workBook.Sheets[nomHoja[1]];
            strHoja = nomHoja[1];

            dtUsuarioTaller = new DataTable();
            dtUsuarioTaller.Columns.Add("ID");
            dtUsuarioTaller.Columns.Add("NID_TALLER");

            reg = 0;
            row = null;
            rowIndex = 3;
            fl_ok = true;
            existeID = false;
            strMensaje = string.Empty;

            while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                reg++;
                row = dtUsuarioTaller.NewRow();
                //---------------------------------------------------------------------------------------
                row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                //---------------------------------------------------------------------------------------
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validar relacion con el ID_USUARIO de las otras hojas
                //------------------------------------------------------------------------------
                existeID = false;
                foreach (string strI in strID.Split('|'))
                {
                    if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                }
                if (!existeID)
                {
                    strMensaje = "- El ID del " + strTipoUsuario + " [" + row[0].ToString().Trim() + "] " +
                        "en la Hoja [" + strHoja + "] no tiene relacion con el ID del " + strTipoUsuario + " de la hoja [" + nomHoja[0] + "]." + strError;
                    fl_ok = false;
                    break;
                }


                //Validacio 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (EsVacio(row[0])) { strMensaje += "- No contiene el código del " + strTipoUsuario + ". \\n"; }
                if (EsVacio(row[1])) { strMensaje += "- No contiene el ID del taller. \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacion 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del " + strTipoUsuario + " debe ser entero. \\n"; }
                if (!EsEntero(row[1].ToString())) { strMensaje += "- El ID de taller debe ser entero"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacion 3 - Existe dicho dato en BD
                //--------------------------------------------------------------------------------------------
                //DataTable dtTaller = (DataTable)ViewState["dttallerdist"];
                string strPuntoRedes = ViewState["PuntoRed_Taller"].ToString();

                if (string.IsNullOrEmpty(strPuntoRedes.Trim())) { strMensaje += "- No existe talleres en la BD."; fl_ok = false; }
                else
                {
                    existeID = false;
                    string strIDUbica = string.Empty;

                    foreach (string strPR in ID_PuntoRed.Split('='))
                    {
                        if (row[0].ToString().Trim().Equals(strPR.Split('|').GetValue(0).ToString()))
                        {
                            strIDUbica = strPR.Split('|').GetValue(1).ToString();

                            foreach (string strPuntoRed in strPuntoRedes.Split('='))
                            {
                                if (strPR.Split('|').GetValue(1).ToString().Equals(strPuntoRed.Split('|').GetValue(0).ToString()))
                                {
                                    if (strPuntoRed.Split('|').GetValue(1).ToString().Equals(row[1].ToString()))
                                    {
                                        existeID = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (!existeID)
                    {
                        strMensaje += "- El ID de Taller [" + row[1].ToString().Trim() + "] no pertenece al ID Ubicacion [" + strIDUbica + "]. \\n";
                        fl_ok = false;
                    }
                    else
                    {
                        existeID = false;

                        foreach (string strT in strIDTaller.Split('|'))
                        {
                            if (strT.Split('=').GetValue(0).Equals(row[0].ToString()))
                            {
                                if (strT.Split('=').GetValue(1).Equals(row[1].ToString()))
                                {
                                    strMensaje += "- Existe duplicidad del ID del " + strTipoUsuario + " [" + row[0].ToString() + "] y del ID Taller [" + row[1].ToString() + "]. \\n";
                                    existeID = true;
                                    fl_ok = false;
                                    break;
                                }
                            }
                        }
                        if (!existeID)
                        {
                            strIDTaller += row[0].ToString() + "=" + row[1].ToString().Trim() + "|";
                        }
                    }
                }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //-------------------------------------------------------------------------------------------
                rowIndex++;
                dtUsuarioTaller.Rows.Add(row);
            }
            /*
            if (reg == 0)
            {
                SRC_MsgInformacion("No hay registro en la Hoja ["+ strHoja +"]");
                return "0";
            }
            */
            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }

            if (strIDTaller.Length > 0) strIDTaller = strIDTaller.Substring(0, strIDTaller.Length - 1);

            #endregion

            /*********************  MAE_USUARIO_MODELO  *******************************************************************************/

            #region MAE_USUARIO_MODELO

            workSheet = (Excel.Worksheet)workBook.Sheets[nomHoja[2]];
            strHoja = nomHoja[2];

            dtUsuarioModelo = new DataTable();
            dtUsuarioModelo.Columns.Add("ID");
            dtUsuarioModelo.Columns.Add("NID_MODELO");

            reg = 0;
            row = null;
            rowIndex = 3;
            fl_ok = true;
            existeID = false;
            strMensaje = string.Empty;

            while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                reg++;
                row = dtUsuarioModelo.NewRow();
                //---------------------------------------------------------------------------------------
                row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                //---------------------------------------------------------------------------------------
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validar relacion con el ID_TALLER de las otras hojas
                //------------------------------------------------------------------------------
                existeID = false;
                foreach (string strI in strID.Split('|'))
                {
                    if (row[0].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                }
                if (!existeID)
                {
                    strMensaje = "- El ID del " + strTipoUsuario + " [" + row[0].ToString().Trim() + "] " +
                        "en la Hoja [" + strHoja + "] no tiene relacion con el ID en la hoja [" + nomHoja[0] + "]." + strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (EsVacio(row[0])) { strMensaje += "- No contiene el código del " + strTipoUsuario + ". \\n"; }
                if (EsVacio(row[1])) { strMensaje += "- No contiene el ID del modelo. \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacion 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del " + strTipoUsuario + " debe ser entero. \\n"; }
                if (!EsEntero(row[1].ToString())) { strMensaje += "- El ID de modelo debe ser entero"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacion 3 - Existe dicho dato en BD
                //--------------------------------------------------------------------------------------------
                existeID = false;

                this.oMaestroTallerBEList = new TallerBEList();
                this.oMaestroTallerBEList = CargarMarcasModelos();

                if (oMaestroTallerBEList.Count == 0) { strMensaje += "- No existe modelos en la BD."; fl_ok = false; }
                else
                {
                    foreach (TallerBE oEntidad in oMaestroTallerBEList)
                    {
                        if (row[1].ToString().Trim().Equals(oEntidad.Nid_modelo.ToString().Trim())) { existeID = true; break; }
                    }
                    if (!existeID)
                    {
                        strMensaje += "- El ID Modelo [" + row[1].ToString().Trim() + "] no existe en la BD.";
                        fl_ok = false;
                    }
                    else
                    {
                        existeID = false;
                        foreach (string strT in strIDModelo.Split('|'))
                        {
                            if (strT.Split('=').GetValue(0).Equals(row[0].ToString()))
                            {
                                if (strT.Split('=').GetValue(1).Equals(row[1].ToString()))
                                {
                                    strMensaje += "- Existe duplicidad del ID del " + strTipoUsuario + " y del ID Modelo. \\n";
                                    existeID = true;
                                    fl_ok = false;
                                    break;
                                }
                            }
                        }
                        if (!existeID)
                        {
                            strIDModelo += row[0].ToString() + "=" + row[1].ToString().Trim() + "|";
                        }
                    }
                }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //-------------------------------------------------------------------------------------------
                rowIndex++;
                dtUsuarioModelo.Rows.Add(row);
            }
            /*
            if (reg == 0)
            {
                SRC_MsgInformacion("No hay registro en la Hoja [MODELOS]");
                return "0";
            }
            */
            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }

            if (strIDModelo.Length > 0) strIDModelo = strIDModelo.Substring(0, strIDModelo.Length - 1);

            #endregion

            if (idTipoUsu.Equals("0")) // Si es de tipo: 'Asesor de Servicio'
            {

                /*********************  MAE_USUARIO_SERVICIO  *******************************************************************************/

                #region MAE_USUARIO_SERVICIO

                workSheet = (Excel.Worksheet)workBook.Sheets[nomHoja[3]];
                strHoja = nomHoja[3];

                dtUsuarioServicio = new DataTable();
                dtUsuarioServicio.Columns.Add("ID");
                dtUsuarioServicio.Columns.Add("NID_SERVICIO");

                reg = 0;
                row = null;
                rowIndex = 3;
                fl_ok = true;
                existeID = false;
                strMensaje = string.Empty;

                while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
                {
                    reg++;
                    row = dtUsuarioServicio.NewRow();
                    //---------------------------------------------------------------------------------------
                    row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                    row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                    //---------------------------------------------------------------------------------------
                    strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                    //---------------------------------------------------------------------------------------

                    //Validar relacion con el ID_TALLER de las otras hojas
                    //------------------------------------------------------------------------------
                    existeID = false;
                    foreach (string strI in strID.Split('|'))
                    {
                        if (row[0].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                    }
                    if (!existeID)
                    {
                        strMensaje = "- El ID del " + strTipoUsuario + " [" + row[0].ToString().Trim() + "] " +
                            "en la Hoja [" + strHoja + "] no tiene relacion con el ID en la hoja [" + nomHoja[0] + "]." + strError;
                        fl_ok = false;
                        break;
                    }

                    //Validacio 1 - Campos Obligatorios
                    //------------------------------------------------------------------------------
                    if (EsVacio(row[0])) { strMensaje += "- No contiene el código del " + strTipoUsuario + ". \\n"; }
                    if (EsVacio(row[1])) { strMensaje += "- No contiene el ID del servicio. \\n"; }

                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }

                    //Validacion 2 - Tipo de Dato Correcto
                    //-------------------------------------------------------------------------------------------
                    if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del " + strTipoUsuario + " debe ser entero. \\n"; }
                    if (!EsEntero(row[1].ToString())) { strMensaje += "- El ID de servicio debe ser entero"; }

                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }


                    //Validacion 3 - Existe dicho dato en BD
                    //--------------------------------------------------------------------------------------------
                    existeID = false;

                    this.oMaestroTallerBEList = new TallerBEList();
                    this.oMaestroTallerBEList = CargarServicios();

                    if (oMaestroTallerBEList.Count == 0) { strMensaje += "- No existe servicios en la BD."; fl_ok = false; }
                    else
                    {
                        existeID = false;
                        foreach (TallerBE oEntidad in oMaestroTallerBEList)
                        {
                            if (row[1].ToString().Trim().Equals(oEntidad.Nid_serv.ToString().Trim())) { existeID = true; break; }
                        }
                        if (!existeID)
                        {
                            strMensaje += "- El ID de Servicio [" + row[1].ToString().Trim() + "] no existe en la BD.";
                            fl_ok = false;
                        }
                        else
                        {
                            existeID = false;
                            foreach (string strT in strIDServicio.Split('|'))
                            {
                                if (strT.Split('=').GetValue(0).Equals(row[0].ToString()))
                                {
                                    if (strT.Split('=').GetValue(1).Equals(row[1].ToString()))
                                    {
                                        strMensaje += "- Existe duplicidad del ID del " + strTipoUsuario + " y del ID Servicio. \\n";
                                        existeID = true;
                                        fl_ok = false;
                                        break;
                                    }
                                }
                            }
                            if (!existeID)
                            {
                                strIDServicio += row[0].ToString() + "=" + row[1].ToString().Trim() + "|";
                            }
                        }
                    }

                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }

                    //-------------------------------------------------------------------------------------------
                    rowIndex++;
                    dtUsuarioServicio.Rows.Add(row);
                }

                if (!fl_ok)
                {
                    SRC_MsgInformacion(strMensaje);
                    return "0";
                }

                if (strIDServicio.Length > 0) strIDServicio = strIDServicio.Substring(0, strIDServicio.Length - 1);

                #endregion

                /*********************  MAE_USUARIO_HORARIO  *******************************************************************************/

                #region MAE_USUARIO_HORARIO

                workSheet = (Excel.Worksheet)workBook.Sheets[nomHoja[4]];
                strHoja = nomHoja[4];

                dtUsuarioHorario = new DataTable();
                dtUsuarioHorario.Columns.Add("ID");
                dtUsuarioHorario.Columns.Add("DIA_ATENCION");
                dtUsuarioHorario.Columns.Add("HORA_INICIO");
                dtUsuarioHorario.Columns.Add("HORA_FIN");

                reg = 0;
                row = null;
                rowIndex = 3;
                fl_ok = true;
                existeID = false;
                strMensaje = string.Empty;
                strHorario = string.Empty;

                while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
                {
                    reg++;
                    row = dtUsuarioHorario.NewRow();
                    //---------------------------------------------------------------------------------------
                    row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                    row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                    row[2] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 4]).Text);
                    row[3] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 5]).Text);
                    //---------------------------------------------------------------------------------------
                    strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                    //---------------------------------------------------------------------------------------

                    //Validar relacion con el ID_USUARIO de las otras hojas
                    //------------------------------------------------------------------------------
                    existeID = false;
                    foreach (string strI in strID.Split('|'))
                    {
                        if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                    }
                    if (!existeID)
                    {
                        strMensaje = "- El ID del " + strTipoUsuario + " [" + row[0].ToString().Trim() + "] " +
                            "en la Hoja [" + strHoja + "] no tiene relacion con el ID en la hoja [" + nomHoja[0] + "]." + strError;
                        fl_ok = false;
                        break;
                    }


                    //Validacion 1 - Campos Obligatorios
                    //------------------------------------------------------------------------------
                    if (EsVacio(row[0])) { strMensaje += "- No contiene el código del " + strTipoUsuario + ". \\n"; }
                    if (EsVacio(row[1])) { strMensaje += "- No contiene el dia de Atención. \\n"; }
                    if (EsVacio(row[2])) { strMensaje += "- No contiene la hora inicial de atención. \\n"; }
                    if (EsVacio(row[3])) { strMensaje += "- No contiene la hora final de atención. \\n"; }

                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }

                    //Validacion 2 - Tipo de Dato Correcto
                    //-------------------------------------------------------------------------------------------

                    if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del " + strTipoUsuario + " debe ser entero. \\n"; }
                    if (!EsEntero(row[1].ToString())) { strMensaje += "- El dia de atención debe ser entero. \\n"; }
                    if (!EsTipoHora(row[2].ToString())) { strMensaje += "- La hora inicial debe tene el formato HH:mm. \\n"; }
                    if (!EsTipoHora(row[3].ToString())) { strMensaje += "- La hora final de atención debe tene el formato HH:mm. \\n"; }

                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }

                    row[2] = Convert.ToDateTime(row[2]).ToString("HH:mm");
                    row[3] = Convert.ToDateTime(row[3]).ToString("HH:mm");

                    //Validacion 3 - Existe dicho dato en BD
                    //--------------------------------------------------------------------------------------------
                    Int32 idTaller = 0;
                    foreach (string strT in strIDTaller.Split('|'))
                    {
                        if (strT.Split('=').GetValue(0).Equals(row[0].ToString()))
                        { idTaller = Int32.Parse(strT.Split('=').GetValue(1).ToString()); break; }
                    }

                    existeID = true;

                    if (!EsRangoDia(Int32.Parse(row[1].ToString()), idTaller)) { strMensaje += "- El dia de atención " + row[1].ToString() + " no esta disponible para el Taller con ID [" + idTaller.ToString() + "] relacionado. \\n"; existeID = false; }

                    if (existeID)
                    {
                        string strRes = EsRangoHora(idTaller, Int32.Parse(row[1].ToString()), row[2].ToString(), row[3].ToString());
                        if (strRes.Length > 0) { strMensaje += strRes; }
                    }


                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }

                    strHorario += row[0].ToString() + "|" + row[1].ToString() + "|" + row[2].ToString() + "|" + row[3].ToString() + "=";

                    //-------------------------------------------------------------------------------------------
                    rowIndex++;
                    dtUsuarioHorario.Rows.Add(row);

                }

                if (!fl_ok)
                {
                    SRC_MsgInformacion(strMensaje);
                    return "0";
                }

                if (!string.IsNullOrEmpty(strHorario)) strHorario = strHorario.Substring(0, strHorario.Length - 1);

                #endregion

                /*********************  MAE_USUARIO_CAPACIDAD_ATENCION  *******************************************************************************/

                #region MAE_USUARIO_CAPACIDAD_ATENCION

                workSheet = (Excel.Worksheet)workBook.Sheets[nomHoja[5]];
                strHoja = nomHoja[5];

                dtUsuarioCapacidad = new DataTable();
                dtUsuarioCapacidad.Columns.Add("ID");
                dtUsuarioCapacidad.Columns.Add("DIA_ATENCION");
                dtUsuarioCapacidad.Columns.Add("CAPACIDAD_FO");
                dtUsuarioCapacidad.Columns.Add("CAPACIDAD_BO");
                //dtUsuarioCapacidad.Columns.Add("CAPACIDAD_TOTAL");
                //dtUsuarioCapacidad.Columns.Add("FL_CONTROL");

                reg = 0;
                row = null;
                rowIndex = 3;
                fl_ok = true;
                existeID = false;
                strMensaje = string.Empty;

                while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
                {
                    reg++;
                    row = dtUsuarioCapacidad.NewRow();
                    //---------------------------------------------------------------------------------------
                    row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                    row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                    row[2] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 4]).Text);
                    row[3] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 5]).Text);
                    //---------------------------------------------------------------------------------------
                    strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                    //---------------------------------------------------------------------------------------

                    //Validar relacion con el ID_USUARIO de las otras hojas
                    //------------------------------------------------------------------------------
                    existeID = false;
                    foreach (string strI in strID.Split('|'))
                    {
                        if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                    }
                    if (!existeID)
                    {
                        strMensaje = "- El ID del " + strTipoUsuario + " [" + row[0].ToString().Trim() + "] " +
                            "en la Hoja [" + strHoja + "] no tiene relacion con el ID en la hoja [" + nomHoja[0] + "]." + strError;
                        fl_ok = false;
                        break;
                    }


                    //Validacion 1 - Campos Obligatorios
                    //------------------------------------------------------------------------------
                    if (EsVacio(row[0])) { strMensaje += "- No contiene el código del " + strTipoUsuario + ". \\n"; }
                    if (EsVacio(row[1])) { strMensaje += "- No contiene el dia de Atención. \\n"; }

                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }

                    //Validacion 2 - Tipo de Datos Correctos
                    //-------------------------------------------------------------------------------------------
                    if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del " + strTipoUsuario + " debe ser entero. \\n"; }
                    if (!EsEntero(row[1].ToString())) { strMensaje += "- El dia de atención debe ser entero. \\n"; }
                    if (!EsVacio(row[2])) { if (!EsEntero(row[2].ToString())) { strMensaje += "- La capacidad de atención del FO debe ser entera. \\n"; } }
                    if (!EsVacio(row[3])) { if (!EsEntero(row[3].ToString())) { strMensaje += "- La capacidad de atención del BO debe ser entera. \\n"; } }

                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }


                    //Validacion 3 - Existe dicho dato en BD
                    //--------------------------------------------------------------------------------------------
                    Int32 idTaller = 0;
                    foreach (string strT in strIDTaller.Split('|'))
                    {
                        if (strT.Split('=').GetValue(0).Equals(row[0].ToString()))
                        { idTaller = Int32.Parse(strT.Split('=').GetValue(1).ToString()); break; }
                    }

                    existeID = true;

                    //if (!EsRangoDia(Int32.Parse(row[1].ToString()), idTaller)) { strMensaje += "- El dia de atención " + row[1].ToString() + " no esta disponible para el taller relacionado. \\n"; existeID = false; }
                    if (!EsRangoDia(Int32.Parse(row[1].ToString()), idTaller)) { strMensaje += "- El dia de atención " + row[1].ToString() + " no esta disponible para el Taller con ID [" + idTaller.ToString() + "] relacionado. \\n"; existeID = false; }
                    else
                    {
                        bool existeHU = false;
                        foreach (string strHoraUsu in strHorario.Split('='))
                        {
                            if (row[0].ToString().Equals(strHoraUsu.Split('|').GetValue(0)) && row[1].ToString().Equals(strHoraUsu.Split('|').GetValue(1)))
                            {
                                existeHU = true;
                                break;
                            }
                        }
                        if (!existeHU)
                        {
                            strMensaje += "- El dia de atención " + row[1].ToString() + " no esta disponible en el Horario del Asesor de Servicio [" + row[0].ToString() + "]. \\n";
                        }
                    }

                    //-----------------------------------------------------------------------------------------------

                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }

                    //row[4] = String.Empty;
                    //row[5] = "I";

                    //-------------------------------------------------------------------------------------------
                    rowIndex++;
                    dtUsuarioCapacidad.Rows.Add(row);
                }

                if (!fl_ok)
                {
                    SRC_MsgInformacion(strMensaje);
                    return "0";
                }

                #endregion

                /*********************  MAE_USUARIO_FECHA  *******************************************************************************/

                #region MAE_USUARIO_FECHA

                workSheet = (Excel.Worksheet)workBook.Sheets[nomHoja[6]];
                strHoja = nomHoja[6];

                dtUsuarioFechaExep = new DataTable();
                dtUsuarioFechaExep.Columns.Add("ID");
                dtUsuarioFechaExep.Columns.Add("FECHA_EXCEPTUADA");

                reg = 0;
                row = null;
                rowIndex = 3;
                fl_ok = true;
                existeID = false;
                strMensaje = string.Empty;


                while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
                {
                    reg++;
                    row = dtUsuarioFechaExep.NewRow();
                    //---------------------------------------------------------------------------------------
                    row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                    row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                    //---------------------------------------------------------------------------------------
                    strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                    //---------------------------------------------------------------------------------------

                    //Validar relacion con el ID_TALLER de las otras hojas
                    //------------------------------------------------------------------------------
                    existeID = false;
                    foreach (string strI in strID.Split('|'))
                    {
                        if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                    }
                    if (!existeID)
                    {
                        strMensaje = "- El ID del " + strTipoUsuario + " [" + row[0].ToString().Trim() + "] " +
                            "en la Hoja [" + strHoja + "] no tiene relacion con el ID en la hoja [" + nomHoja[0] + "]." + strError;
                        fl_ok = false;
                        break;
                    }


                    //Validacio 1 - Campos Obligatorios
                    //------------------------------------------------------------------------------
                    if (EsVacio(row[0])) { strMensaje += "- No contiene el código del " + strTipoUsuario + ". \\n"; }
                    if (EsVacio(row[1])) { strMensaje += "- No contiene una fecha exceptuada. \\n"; }

                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }

                    //Validacio 2 - Tipo de Dato Correcto
                    //-------------------------------------------------------------------------------------------
                    if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del " + strTipoUsuario + " debe ser entero. \\n"; }
                    if (!EsTipoFecha(row[1])) { strMensaje += "- La fecha exceptuada debe estar en el formato " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + "  \\n"; }

                    if (strMensaje.Trim().Length > 0)
                    {
                        strMensaje += strError;
                        fl_ok = false;
                        break;
                    }


                    //-------------------------------------------------------------------------------------------
                    rowIndex++;
                    dtUsuarioFechaExep.Rows.Add(row);
                }

                if (!fl_ok)
                {
                    SRC_MsgInformacion(strMensaje);
                    return "0";
                }



                #endregion
            }

            /*************************************************************************************************************
            * GRABAR DATA A BD
            ****************************/

            UsuarioBE oMaestroUsuariosBE = new UsuarioBE();
            UsuarioTallerBL objneg = new UsuarioTallerBL();


            int idUsuario = 0;
            //Int32 resp = 0;
            Int32 intReg = 0;


            string co_usuario_crea = ((string.IsNullOrEmpty(Profile.UserName)) ? "" : Profile.UserName);
            string co_usuario_modi = ((string.IsNullOrEmpty(Profile.UserName)) ? "" : Profile.UserName);
            string no_usuario_red = ((string.IsNullOrEmpty(Profile.UsuarioRed)) ? "" : Profile.UsuarioRed);
            string no_estacion_red = ((string.IsNullOrEmpty(Profile.Estacion)) ? "" : Profile.Estacion);

            foreach (string strIDUsuario in strID.Split('|'))
            {
                if (string.IsNullOrEmpty(strIDUsuario.Trim())) continue;


                #region GRABAR_USUARIO

                oMaestroUsuariosBE.Nu_tipo_documento = dtUsuario.Rows[intReg][2].ToString();
                oMaestroUsuariosBE.VNOMUSR = dtUsuario.Rows[intReg][3].ToString();
                oMaestroUsuariosBE.No_ape_paterno = dtUsuario.Rows[intReg][4].ToString();
                oMaestroUsuariosBE.No_ape_materno = dtUsuario.Rows[intReg][5].ToString();
                oMaestroUsuariosBE.CUSR_ID = dtUsuario.Rows[intReg][6].ToString();
                oMaestroUsuariosBE.VPASSMD5 = EsVacio(dtUsuario.Rows[intReg][7]) ? "" : oMaestroUsuariosBE.GetMD5(dtUsuario.Rows[intReg][7].ToString());
                oMaestroUsuariosBE.VCORREO = dtUsuario.Rows[intReg][8].ToString();
                oMaestroUsuariosBE.VTELEFONO = dtUsuario.Rows[intReg][9].ToString() + "|" + dtUsuario.Rows[intReg][10].ToString();
                oMaestroUsuariosBE.Nid_ubica = EsVacio(dtUsuario.Rows[intReg][11]) ? 0 : Int32.Parse(dtUsuario.Rows[intReg][11].ToString());
                oMaestroUsuariosBE.Fe_inicio_acceso = (dtUsuario.Rows[intReg][12].ToString().Trim() != "" ? Convert.ToDateTime(dtUsuario.Rows[intReg][12].ToString()) : DateTime.MinValue);
                oMaestroUsuariosBE.Fe_fin_acceso = (dtUsuario.Rows[intReg][13].ToString().Trim() != "" ? Convert.ToDateTime(dtUsuario.Rows[intReg][13].ToString()) : DateTime.MinValue);
                oMaestroUsuariosBE.Hr_inicio_acceso = dtUsuario.Rows[intReg][14].ToString();
                oMaestroUsuariosBE.Hr_fin_acceso = dtUsuario.Rows[intReg][15].ToString();
                //--------------------------------------------------------------------
                oMaestroUsuariosBE.VUSR_PASS = "";
                oMaestroUsuariosBE.Nid_cod_tipo_usuario = 1;//interno - externo
                oMaestroUsuariosBE.VMSGBLQ = ""; //Mensaje
                oMaestroUsuariosBE.CESTBLQ = "0";//Bloqueado [0-> NO]
                oMaestroUsuariosBE.Fl_inactivo = "0";
                //--------------------------------------------------------------------
                oMaestroUsuariosBE.Co_usuario_crea = co_usuario_crea;
                oMaestroUsuariosBE.No_estacion_red = no_estacion_red;
                oMaestroUsuariosBE.No_usuario_red = no_usuario_red;

                idUsuario = objneg.InsertarUsuario(oMaestroUsuariosBE);//Obtener el nid_usuario de la BD

                if (idUsuario <= 0)
                {
                    SRC_MsgInformacion("Error al grabar el Usuario.");
                    return "-1";
                }


                #endregion

                #region GRABAR USUARIO PERFIL

                oMaestroUsuariosBE = new UsuarioBE();
                oMaestroUsuariosBE.CCOAPL = Profile.Aplicacion;
                oMaestroUsuariosBE.Nid_usuario = idUsuario;
                oMaestroUsuariosBE.Cod_perfil = idTipoUsu.Equals("0") ? "ASRV" : "ATAL";
                //------------------------------------------------
                oMaestroUsuariosBE.Fl_inactivo = "0";
                oMaestroUsuariosBE.Co_usuario_crea = co_usuario_crea;
                oMaestroUsuariosBE.No_usuario = no_usuario_red;
                oMaestroUsuariosBE.No_estacion_red = no_estacion_red;

                objneg.InsertarUsuarioPerfil(oMaestroUsuariosBE);

                #endregion

                #region GRABAR USUARIO_MODELO - ASESOR
                oMaestroUsuariosBE = new UsuarioBE();

                oMaestroUsuariosBE.Nid_usuario = idUsuario;
                oMaestroUsuariosBE.Co_usuario_crea = co_usuario_crea;
                oMaestroUsuariosBE.No_usuario = no_usuario_red;
                oMaestroUsuariosBE.No_estacion_red = no_estacion_red;
                oMaestroUsuariosBE.Fl_activo = "A";

                foreach (DataRow oRow in dtUsuarioModelo.Rows)
                {
                    if (!oRow[0].ToString().Equals(strIDUsuario)) continue;
                    if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                    oMaestroUsuariosBE.Nid_modelo = Convert.ToInt32(oRow[1].ToString());
                    objneg.InsertarUsuarioModelo(oMaestroUsuariosBE);
                }

                #endregion

                #region GRABAR USUARIO TALLER - ASESOR
                oMaestroUsuariosBE = new UsuarioBE();
                oMaestroUsuariosBE.Nid_usuario = idUsuario;
                oMaestroUsuariosBE.Co_usuario_crea = co_usuario_crea;
                oMaestroUsuariosBE.Co_usuario_red = no_usuario_red;
                oMaestroUsuariosBE.No_estacion_red = no_estacion_red;
                oMaestroUsuariosBE.Fl_activo = "A";
                foreach (DataRow oRow in dtUsuarioTaller.Rows)
                {
                    if (!oRow[0].ToString().Equals(strIDUsuario)) continue;
                    if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                    oMaestroUsuariosBE.nid_taller = Convert.ToInt32(oRow[1].ToString());
                    objneg.InsertarUsuarioTaller(oMaestroUsuariosBE);
                }
                #endregion

                //---------------------------------------------------------------
                if (idTipoUsu.Equals("0")) // Si es de tipo: 'Asesor de Servicio'
                {

                    #region GRABAR MAE_USR_SERVICIO

                    oMaestroUsuariosBE = new UsuarioBE();
                    oMaestroUsuariosBE.Nid_usuario = idUsuario;
                    oMaestroUsuariosBE.Co_usuario_crea = co_usuario_crea;
                    oMaestroUsuariosBE.Co_usuario_red = no_usuario_red;
                    oMaestroUsuariosBE.No_estacion_red = no_estacion_red;
                    oMaestroUsuariosBE.Fl_activo = "A";

                    foreach (DataRow oRow in dtUsuarioServicio.Rows)
                    {
                        if (!oRow[0].ToString().Equals(strIDUsuario)) continue;
                        if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                        oMaestroUsuariosBE.Nid_servicio = Convert.ToInt32(oRow[1].ToString());
                        objneg.InsertarUsuarioServicio(oMaestroUsuariosBE);
                    }

                    #endregion

                    #region GRABAR MAE_HORARIO - ASESOR

                    oMaestroUsuariosBE = new UsuarioBE();
                    oMaestroUsuariosBE.Nid_usuario = idUsuario;
                    oMaestroUsuariosBE.Co_usuario_crea = co_usuario_crea;
                    oMaestroUsuariosBE.Co_usuario_red = no_usuario_red;
                    oMaestroUsuariosBE.No_estacion_red = no_estacion_red;
                    oMaestroUsuariosBE.Fl_activo = "A";
                    oMaestroUsuariosBE.Fl_tipo = "A";

                    foreach (DataRow oRow in dtUsuarioHorario.Rows)
                    {
                        if (!oRow[0].ToString().Equals(strIDUsuario)) continue;
                        if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                        oMaestroUsuariosBE.Dd_atencion = Convert.ToInt32(oRow[1].ToString());
                        oMaestroUsuariosBE.Ho_inicio = oRow[2].ToString();
                        oMaestroUsuariosBE.Ho_fin = oRow[3].ToString();
                        objneg.InsertarUsuarioHorario(oMaestroUsuariosBE);
                    }

                    #endregion

                    #region GRABAR MAE_CAPACIDAD_ATENCION

                    oMaestroUsuariosBE = new UsuarioBE();
                    oMaestroUsuariosBE.Nid_usuario = idUsuario;
                    oMaestroUsuariosBE.Co_usuario_crea = co_usuario_crea;
                    oMaestroUsuariosBE.Co_usuario_red = no_usuario_red;
                    oMaestroUsuariosBE.No_estacion_red = no_estacion_red;

                    foreach (DataRow oRow in dtUsuarioCapacidad.Rows)
                    {
                        if (!oRow[0].ToString().Equals(strIDUsuario)) continue;
                        if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                        oMaestroUsuariosBE.Dd_atencion = Convert.ToInt32(oRow[1].ToString());
                        oMaestroUsuariosBE.qt_capacidad_fo = -1;
                        oMaestroUsuariosBE.qt_capacidad_bo = -1;
                        if (!EsVacio(oRow[2])) { oMaestroUsuariosBE.qt_capacidad_fo = Convert.ToInt32(oRow[2].ToString()); }
                        if (!EsVacio(oRow[3])) { oMaestroUsuariosBE.qt_capacidad_bo = Convert.ToInt32(oRow[3].ToString()); }

                        Int32 oResp = objneg.MantenimientoCapacidadAtencion_Usuario(oMaestroUsuariosBE);
                    }

                    #endregion

                    #region GRABAR MAE_DIAS_EXCEPTUADOS

                    oMaestroUsuariosBE = new UsuarioBE();
                    oMaestroUsuariosBE.Nid_usuario = idUsuario;
                    oMaestroUsuariosBE.Co_usuario_crea = co_usuario_crea;
                    oMaestroUsuariosBE.Co_usuario_red = no_usuario_red;
                    oMaestroUsuariosBE.No_estacion_red = no_estacion_red;
                    oMaestroUsuariosBE.Fl_tipo = "A";

                    oMaestroUsuariosBE.Fl_activo = "A";

                    foreach (DataRow oRow in dtUsuarioFechaExep.Rows)
                    {
                        if (!oRow[0].ToString().Equals(strIDUsuario)) continue;
                        if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                        oMaestroUsuariosBE.Fe_exceptuada = Convert.ToDateTime(oRow[1].ToString());
                        Int32 oResp = objneg.InsertarUsuarioDiaExceptuado(oMaestroUsuariosBE);
                    }

                    #endregion
                }
                //--------------------------------------------------------------------------------------------------------        

                intReg++;
            }

            SRC_MsgInformacion("Todos los registros [" + intReg.ToString() + "] fueron insertados correctamente.");

            /*************************************************************************************************************/
        }
        catch (Exception ex)
        {
            strResp = "-1";
            SRC_MsgInformacion("ERROR:" + ex.Message);
        }
        finally
        {
            CloseExcel(ref workBook);
        }

        return strResp;
    }

    private void releaseComObject(object theObject, bool makeNull)
    {
        try
        {
            while (System.Runtime.InteropServices.Marshal.ReleaseComObject(theObject) > 0)
            {
            }
        }
        finally
        {
            if (makeNull)
            {
                theObject = null;
            }
        }
    }

    private void CloseExcel(ref Excel._Workbook book)
    {
        //if (workBook != null)
        //    workBook.Close(false, Type.Missing, Type.Missing);
        //if (app != null)
        //    app.Quit();

        if (book != null)
            book.Close(false, Type.Missing, Type.Missing);

        releaseComObject(book, true);

        GC.Collect();
        GC.WaitForPendingFinalizers();

        applExcel.Caption = System.Guid.NewGuid().ToString().ToUpper(); // NewGuid.ToString.ToUpper;
        String sVer = applExcel.Version;
        IntPtr iHandle = IntPtr.Zero;
        if (Convert.ToDecimal(sVer) >= 10) iHandle = new IntPtr(Convert.ToInt32(applExcel.Parent.Hwnd));

        EnsureProcessKilled(iHandle, applExcel.Caption);


    }

    #region "Abrir Excel"
    private void Inicia()
    {
        try
        {
            this.applExcel = OpenExcel(false);
        }
        catch (Exception ex)
        {
            SRC_MsgInformacion("Error al iniciar Excel: " + ex.Message);
            this.applExcel = null;
        }
    }
    private Int32 iGetIDProcces(String nameProcces)
    {

        try
        {
            System.Diagnostics.Process[] asProccess = System.Diagnostics.Process.GetProcessesByName(nameProcces);

            foreach (System.Diagnostics.Process pProccess in asProccess)
            {
                if (pProccess.MainWindowTitle == "")
                {
                    return pProccess.Id;
                }
            }

            return -1;
        }
        catch //(Exception ex)
        {
            return -1;
        }
    }
    private Excel.ApplicationClass OpenExcel(Boolean interact)
    {
        try
        {
            Excel.ApplicationClass app = new Excel.ApplicationClass();
            app.DisplayAlerts = false;
            app.Interactive = false;
            app.Visible = false;
            app.WindowState = Excel.XlWindowState.xlMaximized;
            app.Caption = System.Guid.NewGuid().ToString().ToUpper();
            return app;
        }
        catch { throw; }
        //return null;
    }
    #endregion

    #region "Cerrar Excel"
    public void EnsureProcessKilled(IntPtr MainWindowHandle, String Caption)
    {
        if (IntPtr.Equals(MainWindowHandle, IntPtr.Zero)) MainWindowHandle = FindWindow(null, Caption);
        if (IntPtr.Equals(MainWindowHandle, IntPtr.Zero)) return; //at this point, presume the window has been closed.

        int iRes = 0, iProcID = 0;
        iRes = GetWindowThreadProcessId(MainWindowHandle, ref iProcID);

        // for Excel versions <10, this won't be set yet
        if (iProcID == 0)// can’t get Process ID
        {
            if (EndTask(MainWindowHandle) != 0) return;// success
            throw new ApplicationException("Failed to close.");
        }

        System.Diagnostics.Process proc;
        proc = System.Diagnostics.Process.GetProcessById(iProcID);
        proc.CloseMainWindow();
        proc.Refresh();
        if (proc.HasExited) return;
        proc.Kill();
    }

    [DllImport("user32.dll", SetLastError = true)]
    static extern int EndTask(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    static extern int GetWindowThreadProcessId(IntPtr hWnd, ref int lpdwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr SetLastError(int dwErrCode);

    #endregion

    #region "Validacion Excel"

    private bool EsEmail(object email)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (System.Text.RegularExpressions.Regex.IsMatch(email.ToString().Trim(), expresion))
        {
            return (Regex.Replace(email.ToString().Trim(), expresion, String.Empty).Length == 0);
        }
        else
        {
            return false;
        }
    }

    private bool EsRUC(object str)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(str.ToString().Trim(), "[0-9]"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool EsRUT(object str)
    {

        try
        {
            string rut = str.ToString();
            // No valida si es vacío

            if (rut.Equals(String.Empty))
                return false;

            int largo = rut.Length;
            if (largo > 10 || largo < 2)
                return false;

            int parteRut = 0;

            // La parte del RUT debe ser un número entero
            if (!int.TryParse(rut.Substring(0, largo - 1), out parteRut))
                return false;

            // El dígito verificador debe ser un número o la K
            if ((rut[largo - 1].CompareTo('0') < 0 || rut[largo - 1].CompareTo('9') > 0) && rut[largo - 1] != 'K')
                return false;

            // Realiza la operación módulo
            int suma = 0;
            int mul = 2;

            // -2 porque rut contiene el dígito verificador, el cual no hay que considerar
            for (int i = rut.Length - 2; i >= 0; i--)
            {
                suma += int.Parse(rut[i].ToString()) * mul;
                if (mul == 7) mul = 2; else mul++;
            }

            int residuo = suma % 11;
            char dvr = '0';

            if (residuo == 1) dvr = 'K';
            else if (residuo == 0) dvr = 'O';
            else dvr = (11 - residuo).ToString()[0];

            return dvr.Equals(rut[rut.Length - 1]);

        }
        catch (Exception ex)
        {
            string str1 = ex.Message;
        }
        finally
        {

        }

        return true;

    }

    private bool EsDNI(object str)
    {
        Int32 intEnt;
        bool fl = true;
        try
        {
            intEnt = Int32.Parse(str.ToString());
            fl = (str.ToString().Length == 8);
        }
        catch
        {
            fl = false;
        }
        return fl;
    }

    private bool EsVacio(object str)
    {
        return string.IsNullOrEmpty(str.ToString().Trim());
    }

    private bool EsEntero(string str)
    {
        Int32 intEnt;
        bool fl = true;
        try
        {
            intEnt = Int32.Parse(str);
            fl = (intEnt > 0);
        }
        catch
        {
            fl = false;
        }

        return fl;

    }

    private bool EsTipoHora(object str)
    {
        bool fl = false;
        //DateTime oDate;
        try
        {
            fl = System.Text.RegularExpressions.Regex.IsMatch(str.ToString(), "^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$");
        }
        catch
        {
            fl = false;
        }

        return fl;

    }

    private string EsRangoHora(Int32 idTaller, Int32 ddAtencion, string strHoraI, string strHoraF)
    {

        string strError = "";
        try
        {
            if (DateTime.Parse(strHoraI) >= DateTime.Parse(strHoraF))
            {
                strError = "La hora final debe ser mayor que la hora inicial de atención.";
            }
            else
            {
                UsuarioBE objEU = new UsuarioBE();
                UsuarioTallerBL objLU = new UsuarioTallerBL();

                objEU.nid_taller = idTaller;
                objEU.Dd_atencion = ddAtencion;

                bool flhoraI = false;
                bool flhoraF = false;

                List<UsuarioBE> lst = objLU.GETListarHorario_Por_Taller_Dia(objEU);
                if (lst.Count > 0)
                {
                    DateTime dtHoraIni = Convert.ToDateTime(lst[0].Ho_inicio.ToString());
                    DateTime dtHoraFin = Convert.ToDateTime(lst[0].Ho_fin.ToString());
                    Int32 intIntervalo = Convert.ToInt32(lst[0].No_valor1.ToString());

                    while (dtHoraIni <= dtHoraFin)
                    {
                        if (!flhoraI) if (dtHoraIni.ToString("HH:mm").Equals(Convert.ToDateTime(strHoraI).ToString("HH:mm"))) { flhoraI = true; }
                        if (!flhoraF) if (dtHoraIni.ToString("HH:mm").Equals(Convert.ToDateTime(strHoraF).ToString("HH:mm"))) { flhoraF = true; }

                        dtHoraIni = dtHoraIni.AddMinutes(intIntervalo);
                        if (flhoraI == true && flhoraF == true)
                            break;
                    }

                    if (flhoraI == false) { strError += "La hora de inicio de atención no se encuentra en el rango. \\n"; }
                    if (flhoraF == false) { strError += "La hora de fin de atención no se encuentra en el rango. \\n"; }

                }
                else
                {
                    strError = "No existe horario para este dia. ";
                }
            }


        }
        catch
        {
            strError = "";
        }

        return strError;

    }

    private bool EsRangoDia(Int32 intDia, Int32 idTaller)
    {
        //Int32 intEnt;
        bool fl = false;
        try
        {

            TallerBL neg = new TallerBL();
            TallerBE ent = new TallerBE();
            ent.nid_taller = idTaller;
            List<TallerBE> list = neg.ListarDias_Taller(ent);
            foreach (TallerBE oEntidad in list)
            {
                if (oEntidad.Dd_atencion == intDia)
                {
                    fl = true;
                    break;
                }
            }
        }
        catch
        {
            fl = false;
        }

        return fl;

    }

    private bool EsTipoFecha(object str)
    {
        DateTime oDate;
        bool fl = false;
        try
        {
            //System.Globalization .CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
            oDate = DateTime.Parse(str.ToString());
            fl = oDate.ToShortDateString().Equals(str.ToString());
            //fl = (oDate.ToString("dd/MM/yyyy").Equals(str));         
        }
        catch
        {
            fl = false;
        }

        return fl;

    }

    #endregion

    protected void btnGargaMasiva_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnAceptarCargaMasiva_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string pathExcel = Server.MapPath("CargaExcel");
            string nomArchivo = string.Empty;//  
            string rutaArchivo = string.Empty;//    

            if (this.txtAdjuntoCargaMasiva.PostedFile != null && this.txtAdjuntoCargaMasiva.PostedFile.ContentLength > 0)
            {
                if (!Directory.Exists(pathExcel))
                {
                    Directory.CreateDirectory(pathExcel);
                }

                string strHora = DateTime.Today.ToString("HHmmss");

                rutaArchivo = Path.Combine(pathExcel, Path.GetFileName(txtAdjuntoCargaMasiva.PostedFile.FileName) + "_" + strHora);
                if (File.Exists(rutaArchivo))
                {
                    try
                    {
                        File.Delete(rutaArchivo);
                    }
                    catch
                    { }
                }

                this.txtAdjuntoCargaMasiva.SaveAs(rutaArchivo);
                string strResp = ImportarDatosUsuarioFromExcel(rutaArchivo);

                if (File.Exists(rutaArchivo))
                {
                    try
                    {
                        File.Delete(rutaArchivo);
                    }
                    catch
                    { }
                }
            }
            else
            {
                SRC_MsgInformacion("Problemas al cargar el archivo Excel.");
            }
        }
        catch (Exception ex)
        {
            SRC_MsgInformacion("ERROR 0: " + ex.Message);
        }
    }
}
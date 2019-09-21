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
using AppMiTaller.Intranet.BL.Exportacion;

public partial class SRC_Mantenimiento_SRC_Maestro_Talleres : System.Web.UI.Page
{

    #region "GLOBALES"

    TallerBE objEnt = new TallerBE();
    TallerBL objNeg = new TallerBL();
    private TallerBEList oMaestroTallerBEList;

    private string _strDatosTaller = string.Empty;
    private string strHorario = string.Empty;

    #endregion

    #region "METODOS PROPIOS"

    private void CargarUbigeo()
    {       
        TallerBL objneg = new TallerBL();
        List<TallerBE> ListUbigeo = objneg.GETListarUbigeo(Profile.Usuario.Nid_usuario);
        DataTable dtUbigeo = new DataTable();
        dtUbigeo.Columns.Add("coddpto", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("codprov", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("coddist", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("nombre", System.Type.GetType("System.String"));
        for (Int32 i = 0; i < ListUbigeo.Count; i++)        
            dtUbigeo.Rows.Add(ListUbigeo[i].coddpto, ListUbigeo[i].codprov, ListUbigeo[i].coddist, ListUbigeo[i].Ubigeo);        
        ViewState.Add("dtubigeo", dtUbigeo);
        ddl_provincia.Items.Insert(0, new ListItem("--Todos--", ""));
        ddl_provincia.Enabled = false;
        ddl_distrito.Items.Insert(0, new ListItem("--Todos--",""));
        ddl_distrito.Enabled = false;
        DataRow[] oRow = dtUbigeo.Select("codprov='00' AND coddist='00'", "nombre", DataViewRowState.CurrentRows);
        ddl_departamento.Items.Clear();
        for (Int32 i = 0; i < oRow.Length; i++)
        {
            ddl_departamento.Items.Add("");
            ddl_departamento.Items[i].Value = oRow[i]["coddpto"].ToString();
            ddl_departamento.Items[i].Text = oRow[i]["nombre"].ToString();
        }
        ddl_departamento.Items.Insert(0, new ListItem("--Todos--",""));        
        ddl_departamento.AutoPostBack = true;
        objneg = null; ListUbigeo = null; dtUbigeo = null;
    }

    private void CargarUbicacion()
    {
        TallerBL objNegTal = new TallerBL();
        TallerBE objEntTal = new TallerBE();
        objEntTal.Co_perfil_usuario = Profile.Usuario.co_perfil_usuario;
        objEntTal.Nid_usuario = Profile.Usuario.Nid_usuario;
        List<TallerBE> List = objNegTal.GETListarUbicacion(objEntTal);
        if (List.Count > 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nid_ubica", System.Type.GetType("System.Int32"));
            dt.Columns.Add("no_ubica", System.Type.GetType("System.String"));
            dt.Columns.Add("coddpto", System.Type.GetType("System.String"));
            dt.Columns.Add("codprov", System.Type.GetType("System.String"));
            dt.Columns.Add("coddist", System.Type.GetType("System.String"));
            for (Int32 i = 0; i < List.Count; i++)            
                dt.Rows.Add(List[i].nid_ubica, List[i].no_ubica, List[i].coddpto, List[i].codprov, List[i].coddist);            
            ViewState.Add("dtubicacion", dt);
            dt = null;
        }
        objNegTal = null; objEntTal = null;
        ddl_puntored.Items.Insert(0, new ListItem("--Todos--","0"));
        ddl_puntored.SelectedIndex = 0;
        ddl_puntored.Enabled = false;
    }

    private void CargarEstado()
    {
        ddl_estado.Items.Clear();
        ddl_estado.Items.Add("");
        ddl_estado.Items[0].Text = "--Todos--";
        ddl_estado.Items[0].Value = "";
        ddl_estado.Items.Add("");
        ddl_estado.Items[1].Text = "Activo";
        ddl_estado.Items[1].Value = "A";
        ddl_estado.Items.Add("");
        ddl_estado.Items[2].Text = "Inactivo";
        ddl_estado.Items[2].Value = "I";
    }
    
    private void CargarHorasAtencion()
    {
        ParametrosBackOffieBL objNegHorDef = new ParametrosBackOffieBL();

        ParametrosBackOffieBEList oHoras = objNegHorDef.GetHorasSRC();
        ViewState["HorasTaller"] = oHoras;
    
    }


    private void Label_X_Pais()
    {
        Parametros oParm = new Parametros();

        lbl_dep_taller.Text = oParm.N_Departamento.ToString();
        lbl_prov_taller.Text = oParm.N_Provincia.ToString();
        lbl_dist_taller.Text = oParm.N_Distrito.ToString();

        gdTalleres.Columns[2].HeaderText = oParm.N_Departamento.ToString();
        gdTalleres.Columns[3].HeaderText = oParm.N_Provincia.ToString();
        gdTalleres.Columns[4].HeaderText = oParm.N_Distrito.ToString();
        gdTalleres.Columns[5].HeaderText = oParm.N_Local.ToString();


    }

    private void Inicializa()
    {
        Label_X_Pais();
        Session["nuevo"] = null;
        Session["editar"] = null;
        Session["detalle"] = null;
        CargarUbigeo();
        CargarUbicacion();
        CargarEstado();
        CargarHorasAtencion();
        hf_exportaexcel.Value = "";
    }

    private void MensajeScript(string SMS)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('" + SMS + "');</script>", false);

    }
    private void SRC_MsgInformacion(string strError)
    {
        MensajeScript(strError);
    }

    #endregion

    #region Seguridad_Botones_Accion

    public bool VerBoton() {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantTaller_AccionVerDetalle).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool EditarBoton()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantTaller_AccionEditar).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool NuevoBoton()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantTaller_AccionNuevo).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        string AccesoPagina = (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantTaller_AccionVerForm);
        if (string.IsNullOrEmpty(AccesoPagina))
            AccesoPagina = (Master as Principal).ValidaTipoAccesoPagina(Page, "SinAcceso");
        btnVerDet.Visible = VerBoton();
        BtnEditar.Visible = EditarBoton();
        btnNuevo.Visible = NuevoBoton();

        txt_codtaller.Attributes.Add("onKeypress", "return Valida_Codigo_Taller(event)");
        txt_nomtaller.Attributes.Add("onKeypress", "return Valida_Nombre_Taller(event)");        
        if (!Page.IsPostBack)
        {
            ViewState.Add("ordenGridTalleres", SortDirection.Descending);            
            Inicializa();

            //INICIALIZANDO EL GRIDVIEW
            this.oMaestroTallerBEList = new TallerBEList();
            this.oMaestroTallerBEList.Add(new TallerBE());
            //Session["List_Taller"] = this.oMaestroTallerBEList;
            this.gdTalleres.DataSource = this.oMaestroTallerBEList;
            this.gdTalleres.DataBind();

            if (Session["List_Taller"] != null) 
            {
                txt_codtaller.Text = Session["filtros"].ToString().Split('|')[0];
                txt_nomtaller.Text = Session["filtros"].ToString().Split('|')[1];
                ddl_estado.SelectedValue = Session["filtros"].ToString().Split('|')[2];
                ddl_departamento.SelectedValue = Session["filtros"].ToString().Split('|')[3];
                CargarProvinciaPorDepartamento();
                ddl_provincia.SelectedValue = Session["filtros"].ToString().Split('|')[4];
                CargarDistritoPorProvincia();
                ddl_distrito.SelectedValue = Session["filtros"].ToString().Split('|')[5];
                CargarPuntoRedPorDistrito();
                ddl_puntored.SelectedValue = Session["filtros"].ToString().Split('|')[6];
                gdTalleres.DataSource = (List<TallerBE>)Session["List_Taller"];
                gdTalleres.DataBind();
                if (gdTalleres.Rows.Count > 0)
                    hf_exportaexcel.Value = "1";
            }
            //
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Session["List_Taller"] != null &&
            this.gdTalleres != null &&
            this.gdTalleres.Rows.Count > 0 &&
            this.gdTalleres.PageCount > 1)
        {
            GridViewRow oRow = this.gdTalleres.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                //oTotalReg.Text = String.Format("Total Reg. {0}", ((List<TallerBE>)ViewState["List_Taller"]).Count);
                oTotalReg.Text = String.Format("Total Reg. {0}", ((List<TallerBE>)Session["List_Taller"]).Count);

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }

    protected void gdTalleres_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
        gdTalleres.PageIndex = e.NewPageIndex;        
        gdTalleres.DataSource = (List<TallerBE>)Session["List_Taller"];
        gdTalleres.DataBind();
        txh_nid_taller.Value = String.Empty;
    }

    protected void btnVerDet_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_nid_taller.Value.ToString() != "")
        {
            Session["verHorExcep"] = "0";
            Session["detalle"] = txh_nid_taller.Value.ToString();
            Session["editar"] = null;
            Session["nuevo"] = null;
            Enviar_Filtros();
            Response.Redirect("SRC_Maestro_Detalle_Talleres.aspx");
        }
    }

    private void Enviar_Filtros()     
    {
        Session["filtros"] = txt_codtaller.Text.Trim() + "|" + txt_nomtaller.Text.Trim() + "|" + ddl_estado.SelectedValue + "|" + ddl_departamento.SelectedValue + "|" + ddl_provincia.SelectedValue + "|" + ddl_distrito.SelectedValue + "|" + ddl_puntored.SelectedValue;        
    }

    protected void BtnEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_nid_taller.Value.ToString() != "")
        {
            Session["verHorExcep"] = "0";
            Session["editar"] = txh_nid_taller.Value.ToString();
            Session["detalle"] = null;
            Session["nuevo"] = null;
            Enviar_Filtros();
            Response.Redirect("SRC_Maestro_Detalle_Talleres.aspx");
        }        
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session["verHorExcep"] = "0";
        Session["nuevo"] = "0";
        Session["editar"] = null;
        Session["detalle"] = null;
        Enviar_Filtros();
        Response.Redirect("SRC_Maestro_Detalle_Talleres.aspx");
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            TallerBL oMaestroTallerBL = new TallerBL();
            oMaestroTallerBL.ErrorEvent += new TallerBL.ErrorDelegate(Master.Transaction_ErrorEvent);

            objEnt.Nid_usuario = Profile.Usuario.Nid_usuario;
            objEnt.Co_perfil_usuario = Profile.Usuario.co_perfil_usuario;
            objEnt.co_taller = txt_codtaller.Text.Trim();
            objEnt.no_taller = txt_nomtaller.Text.Trim();
            objEnt.coddpto = ddl_departamento.SelectedValue;
            objEnt.codprov = ddl_provincia.SelectedValue;
            objEnt.coddist = ddl_distrito.SelectedValue;
            objEnt.nid_ubica = Convert.ToInt32(ddl_puntored.SelectedValue);
            objEnt.fl_activo = ddl_estado.SelectedValue;

            this.oMaestroTallerBEList = oMaestroTallerBL.GETListarTalleres(objEnt);


            const string RUTA_DOCUMENTOS = ConstanteBE.RUTA_MANTENIMIENTO_SRC;
            String carpeta = String.Empty, nombre = String.Empty, RutaFinal = String.Empty;
            String ruta = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + RUTA_DOCUMENTOS;
            ruta = Utility.CrearCarpetaFileServer(ruta);

            String fl_Ruta = ConstanteBE.FLAT_EXCEL_SRC;
            ExportarExcelXml oExportarExcelXml = new ExportarExcelXml();
            String archivo = oExportarExcelXml.GenerarExcelExportarTaller(this.oMaestroTallerBEList, ruta);

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
            ddl_provincia.Items.Insert(0, new ListItem("--Todos--",""));
            ddl_provincia.SelectedIndex = 0;
            if (ddl_provincia.Items.Count > 1) 
            {
                ddl_provincia.AutoPostBack = true;
                ddl_provincia.Enabled = true;
            }
            ddl_distrito.Items.Insert(0, new ListItem("--Todos--",""));
            ddl_distrito.SelectedIndex = 0;
            ddl_distrito.Enabled = false;            
        }
        else
        {
            ddl_provincia.Items.Insert(0, new ListItem("--Todos--",""));
            ddl_provincia.SelectedIndex = 0;
            ddl_provincia.Enabled = false;                      
        }
        ddl_distrito.Items.Insert(0, new ListItem("--Todos--",""));
        ddl_distrito.SelectedIndex = 0;
        ddl_distrito.Enabled = false;

        ddl_puntored.Items.Insert(0, new ListItem("--Todos--","0"));
        ddl_puntored.SelectedIndex = 0;
        ddl_puntored.Enabled = false;
    }

    protected void ddl_departamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarProvinciaPorDepartamento();
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
            ddl_distrito.Items.Insert(0, new ListItem("--Todos--",""));
            ddl_distrito.SelectedIndex = 0;
            if (ddl_distrito.Items.Count > 1)
            {
                ddl_distrito.Enabled = true;
                ddl_distrito.AutoPostBack = true;
            }
        }
        else
        {
            ddl_distrito.Items.Insert(0, new ListItem("--Todos--", ""));
            ddl_distrito.SelectedIndex = 0;
            ddl_distrito.Enabled = false;
        }
        ddl_puntored.Items.Clear();
        ddl_puntored.Items.Insert(0, new ListItem("--Todos--", "0"));
        ddl_puntored.Enabled = false;
    }

    protected void ddl_provincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDistritoPorProvincia();
    }

    private void CargarPuntoRedPorDistrito()
    {
        ddl_puntored.Items.Clear();
        ddl_puntored.Enabled = false;
        if (ddl_distrito.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtubicacion"]).Select("coddpto='" + ddl_departamento.SelectedValue + "' AND codprov='" + ddl_provincia.SelectedValue + "' AND coddist='" + ddl_distrito.SelectedValue + "'", "no_ubica", DataViewRowState.CurrentRows);
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_puntored.Items.Add("");
                ddl_puntored.Items[i].Value = oRow[i]["nid_ubica"].ToString();
                ddl_puntored.Items[i].Text = oRow[i]["no_ubica"].ToString();
            }
            ddl_puntored.Items.Insert(0, new ListItem("--Todos--", "0"));
            ddl_puntored.SelectedIndex = 0;
            if (ddl_puntored.Items.Count > 1)
                ddl_puntored.Enabled = true;
            else
                ddl_puntored.Enabled = false;
        }
        else 
        {
            ddl_puntored.Items.Insert(0, new ListItem("--Todos--", "0"));
            ddl_puntored.SelectedIndex = 0;
            ddl_puntored.Enabled = false;
        }
    }

    protected void ddl_distrito_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarPuntoRedPorDistrito();
    }
    private void src_Mensaje(String msg) 
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "alert('" + msg + "');", true);
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        hf_exportaexcel.Value = String.Empty;

        objEnt.Nid_usuario = Profile.Usuario.Nid_usuario;
        objEnt.Co_perfil_usuario = Profile.Usuario.co_perfil_usuario;
        objEnt.co_taller = txt_codtaller.Text.Trim();
        objEnt.no_taller = txt_nomtaller.Text.Trim();        
        objEnt.coddpto = ddl_departamento.SelectedValue;
        objEnt.codprov = ddl_provincia.SelectedValue;
        objEnt.coddist = ddl_distrito.SelectedValue;
        objEnt.nid_ubica = Convert.ToInt32(ddl_puntored.SelectedValue);
        objEnt.fl_activo = ddl_estado.SelectedValue;


        List<TallerBE> List = objNeg.GETListarTalleres(objEnt);

        this.oMaestroTallerBEList = objNeg.GETListarTalleres(objEnt);

        if (oMaestroTallerBEList == null || oMaestroTallerBEList.Count == 0)
        {
            Session["List_Taller"] = null;
            objEnt = null;
            objEnt = new TallerBE();
            oMaestroTallerBEList.Add(objEnt);
        }
        else
        {
            Enviar_Filtros();
            hf_exportaexcel.Value = "1";
        }

        this.gdTalleres.DataSource = oMaestroTallerBEList;
        this.gdTalleres.DataBind();

        Session["List_Taller"] = oMaestroTallerBEList;
        ViewState.Add("List_Taller", oMaestroTallerBEList);
    }
    protected void gdTalleres_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = gdTalleres.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["nid_taller"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }
            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                                , txh_nid_taller.ClientID, dataKey.Values["nid_taller"].ToString());
            if (VerBoton())
            {
                Session["verHorExcep"] = "0";
                e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SRC_Maestro_Detalle_Talleres.aspx?nid_taller={0}'", dataKey.Values["nid_taller"].ToString());
            }
        }
    }
    protected void gdTalleres_Sorting(object sender, GridViewSortEventArgs e)
    {

        TallerBEList oMaestroTallerBEList = (TallerBEList)(List<TallerBE>)Session["List_Taller"];
        SortDirection indOrden = (SortDirection)ViewState["ordenGridTalleres"];

        txh_nid_taller.Value = String.Empty;

        if (oMaestroTallerBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oMaestroTallerBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                ViewState["ordenGridTalleres"] = SortDirection.Descending;
            }
            else
            {
                oMaestroTallerBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                ViewState["ordenGridTalleres"] = SortDirection.Ascending;
            }
        }
        gdTalleres.DataSource = oMaestroTallerBEList;
        gdTalleres.DataBind();
        Session["List_Taller"] = oMaestroTallerBEList;
    }

    private TallerBEList CargarServicios()
    {
        return objNeg.GETListarServicios();     
    }

    private TallerBEList CargarMarcasModelos()
    {
        TallerBE ent = new TallerBE();
        ent.Co_perfil_usuario = Profile.Usuario.co_perfil_usuario;
        ent.Nid_usuario = Profile.Usuario.Nid_usuario;
        return objNeg.GETListarMarcasModelos(ent);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    private Excel.Application applExcel;
    private Excel._Workbook workBook;
    private Excel._Worksheet workSheet;

    private string Salto()
    {
        return "\\n";
    }

    public string ImportarDatosTallerFromExcel(String path)
    {
        string strResp = "1";

        try
        {
            Inicia();

            workBook = applExcel.Workbooks.Open(path, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            _strDatosTaller = string.Empty;
            strHorario = string.Empty;

            Int32 rowIndex = 3;
            bool fl_ok = true;
            string strMensaje = string.Empty;
            DataRow row;

            string strID = string.Empty;
            Int32 reg = 0;
            string strHoja = string.Empty;
            string strError = string.Empty;
            bool existeID = false;

            string[] nomHoja = { "TALLER", "HORARIO", "CAPACIDAD_ATENCION", "MODELOS", "SERVICIOS", "FECHAS_EXCEPTUADAS", "HORARIO_EXCEPCIONAL", "HORARIO_EXCEPCIONAL_DET" };

            /*********************    ***************************************************************************************/

            foreach (Excel.Worksheet oWorksheet in workBook.Worksheets)
            {
                if (Array.IndexOf(nomHoja, oWorksheet.Name.ToUpper().Trim()) == -1)
                {
                    SRC_MsgInformacion("El Formato de archivo no es correcto.");
                    return "-1";
                }
            }

            /*********************   MAE_TALLER   ***************************************************************************************/

            #region [TALLER]

            workSheet = (Excel.Worksheet)workBook.Sheets[nomHoja[0]]; //--> TALLER
            string[] nomColumna = { "ID", "CO_TALLER", "NO_TALLER", "CO_INTERVALO_ATENC", "NID_UBICA", "NO_DIRECCION", "NU_TELEFONO", "TX_URL_TALLER" };
            strHoja = "TALLER";


            if (workSheet == null)
            {
                SRC_MsgInformacion("No existe la Hoja [" + nomHoja[0] + "] en el Excel.");
                return "-1";
            }


            DataTable dtTaller = new DataTable();

            dtTaller.Columns.Add("ID");
            dtTaller.Columns.Add("CO_TALLER");
            dtTaller.Columns.Add("NO_TALLER");
            dtTaller.Columns.Add("CO_INTERVALO_ATENC");
            dtTaller.Columns.Add("NID_UBICA");
            dtTaller.Columns.Add("NO_DIRECCION");
            dtTaller.Columns.Add("NU_TELEFONO");
            dtTaller.Columns.Add("TX_URL_TALLER");

            reg = 0;
            row = null;
            rowIndex = 3;
            fl_ok = true;
            strMensaje = string.Empty;

            while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                reg++;
                row = dtTaller.NewRow();
                row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text).Trim();
                row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text).Trim();
                row[2] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 4]).Text).Trim();
                row[3] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 5]).Text).Trim();
                row[4] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 6]).Text).Trim();
                row[5] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 7]).Text).Trim();
                row[6] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 8]).Text).Trim();
                row[7] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 9]).Text).Trim();
                //---------------------------------------------------------------------------------------
                strID += row[0] + "|";
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validacio 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (row[0].Equals("")) { strMensaje += "- No contiene el código de Taller. \\n"; }
                if (row[1].Equals("")) { strMensaje += "- No contiene el nombre  de Taller. \\n"; }
                if (row[2].Equals("")) { strMensaje += "- No contiene el código de intervalo de Taller. \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row["ID"].ToString())) { strMensaje += "- El ID del Taller debe ser entero. \\n"; }
                if (!EsEntero(row["CO_INTERVALO_ATENC"].ToString())) { strMensaje += "- El Cod. de Intervalo del Taller debe ser entero. \\n"; }
                if (!EsEntero(row["NID_UBICA"].ToString())) { strMensaje += "- El ID de la Ubicación debe ser entero. \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 3 - Existe dicho dato en BD
                //--------------------------------------------------------------------------------------------
                this.oMaestroTallerBEList = new TallerBEList();
                this.oMaestroTallerBEList = objNeg.GETListarIntervalosAtencion();

                existeID = false;
                string strIntervalo = "";
                if (oMaestroTallerBEList.Count == 0) { strMensaje += "- No existe intervalos de atencion en la BD. \\n"; fl_ok = false; }
                else
                {
                    foreach (TallerBE oEntidad in oMaestroTallerBEList)
                    {
                        if (row[3].ToString().Trim().Equals(oEntidad.Cod_intervalo.ToString().Trim())) { existeID = true; strIntervalo = oEntidad.Num_intervalo.ToString(); break; }
                    }
                    if (!existeID)
                    {
                        strMensaje += "- El código de intervalo no existe en la BD. \\n";
                        fl_ok = false;
                        //break;
                    }
                }

                existeID = false;
                DataTable dtUbica = ((DataTable)ViewState["dtubicacion"]);
                foreach (DataRow oRow in dtUbica.Rows)
                {
                    if (row[4].ToString().Trim().Equals(oRow["nid_ubica"].ToString().Trim()))
                    {
                        existeID = true; break;
                    }
                }
                if (!existeID)
                {
                    strMensaje += "- El nid_ubicacion no existe en la BD. \\n";
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
                dtTaller.Rows.Add(row);

                _strDatosTaller += row["ID"].ToString() + '|' + row["CO_INTERVALO_ATENC"].ToString() + '|' + strIntervalo + "=";
            }

            if (reg == 0)
            {
                SRC_MsgInformacion("- No hay registros en la Hoja [TALLER].");
                fl_ok = false;
            }

            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }


            if (!String.IsNullOrEmpty(_strDatosTaller.Trim())) _strDatosTaller = _strDatosTaller.Substring(0, _strDatosTaller.Length - 1);
            if (!String.IsNullOrEmpty(strID.Trim())) strID = strID.Substring(0, strID.Length - 1);

            #endregion

            /*********************  MAE_TALLER_MODELO  *******************************************************************************/

            #region [MODELOS]

            workSheet = (Excel.Worksheet)workBook.Sheets["MODELOS"];
            strHoja = "MODELOS";

            DataTable dtTallerModelo = new DataTable();
            dtTallerModelo.Columns.Add("ID");
            dtTallerModelo.Columns.Add("NID_MODELO");

            reg = 0;
            row = null;
            rowIndex = 3;
            fl_ok = true;
            existeID = false;
            strMensaje = string.Empty;

            while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                reg++;
                row = dtTallerModelo.NewRow();
                //---------------------------------------------------------------------------------------
                row["ID"] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                row["NID_MODELO"] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                //---------------------------------------------------------------------------------------
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validar relacion con el ID_TALLER de las otras hojas
                //------------------------------------------------------------------------------
                foreach (string strI in strID.Split('|'))
                {
                    if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                }
                if (!existeID)
                {
                    strMensaje = "- El ID Taller [" + row["ID"].ToString().Trim() + "] " +
                        "en la Hoja [MODELOS] no tiene relacion con el ID del Taller de la hoja [TALLER]." + strError;
                    fl_ok = false;
                    break;
                }


                //Validacio 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (row[0].Equals("")) { strMensaje += "- No contiene el id Taller."; }
                if (row[1].Equals("")) { strMensaje += "- No contiene el id Modelo."; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacion 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row["ID"].ToString())) { strMensaje += "- El id del Taller debe ser entero."; }
                if (!EsEntero(row["NID_MODELO"].ToString())) { strMensaje += "- El id de Modelo debe ser entero"; }

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
                        if (row["NID_MODELO"].ToString().Trim().Equals(oEntidad.Nid_modelo.ToString().Trim())) { existeID = true; break; }
                    }
                    if (!existeID)
                    {
                        strMensaje += "- El ID Modelo [" + row["NID_MODELO"].ToString().Trim() + "] no existe en la BD.";
                        fl_ok = false;
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
                dtTallerModelo.Rows.Add(row);
            }
            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }

            #endregion

            /*********************  MAE_TALLER_SERVICIO  *******************************************************************************/

            #region [SERVICIOS]
            workSheet = (Excel.Worksheet)workBook.Sheets["SERVICIOS"];
            strHoja = "SERVICIOS";

            DataTable dtTallerServicio = new DataTable();
            dtTallerServicio.Columns.Add("ID");
            dtTallerServicio.Columns.Add("NID_SERVICIO");

            reg = 0;
            row = null;
            rowIndex = 3;
            fl_ok = true;
            existeID = false;
            strMensaje = string.Empty;

            while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                reg++;
                row = dtTallerServicio.NewRow();
                //---------------------------------------------------------------------------------------
                row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                //---------------------------------------------------------------------------------------
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validar relacion con el ID_TALLER de las otras hojas
                //------------------------------------------------------------------------------
                foreach (string strI in strID.Split('|'))
                {
                    if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                }
                if (!existeID)
                {
                    strMensaje = "- El ID Taller [" + row["ID"].ToString().Trim() + "] " +
                        "en la Hoja [SERVICIOS] no tiene relacion con el ID del Taller de la hoja [TALLER]." + strError;
                    fl_ok = false;
                    break;
                }


                //Validacion 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (row[0].Equals("")) { strMensaje += "- No contiene el id Taller."; }
                if (row[1].Equals("")) { strMensaje += "- No contiene el id Servicio."; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacion 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row["ID"].ToString())) { strMensaje += "- El id del Taller debe ser entero."; }
                if (!EsEntero(row["NID_SERVICIO"].ToString())) { strMensaje += "El id de Servicio debe ser entero"; }

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
                    foreach (TallerBE oEntidad in oMaestroTallerBEList)
                    {
                        if (row["NID_SERVICIO"].ToString().Trim().Equals(oEntidad.Nid_serv.ToString().Trim())) { existeID = true; break; }
                    }
                    if (!existeID)
                    {
                        strMensaje += "- El ID Servicio [" + row["NID_SERVICIO"].ToString().Trim() + "] no existe en la BD.";
                        fl_ok = false;
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
                dtTallerServicio.Rows.Add(row);
            }

            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }

            #endregion

            /*********************  MAE_TALLER_HORARIO  *******************************************************************************/

            #region [HORARIO]

            workSheet = (Excel.Worksheet)workBook.Sheets["HORARIO"];
            strHoja = "HORARIO";

            DataTable dtTallerHorario = new DataTable();
            dtTallerHorario.Columns.Add("ID");
            dtTallerHorario.Columns.Add("DIA_ATENCION");
            dtTallerHorario.Columns.Add("HORA_INICIO");
            dtTallerHorario.Columns.Add("HORA_FIN");

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
                row = dtTallerHorario.NewRow();
                //---------------------------------------------------------------------------------------
                row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                row[2] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 4]).Text);
                row[3] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 5]).Text);
                //---------------------------------------------------------------------------------------
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validar relacion con el ID_TALLER de las otras hojas
                //------------------------------------------------------------------------------
                foreach (string strI in strID.Split('|'))
                {
                    if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                }
                if (!existeID)
                {
                    strMensaje = "- El ID Taller [" + row["ID"].ToString().Trim() + "] " +
                        "en la Hoja [HORARIO] no tiene relacion con el ID del Taller de la hoja [TALLER]." + strError;
                    fl_ok = false;
                    break;
                }


                //Validacio 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (row[0].Equals("")) { strMensaje += "- No contiene el id Taller."; }
                if (row[1].Equals("")) { strMensaje += "- No contiene el dia de Atención. \\n"; }
                if (row[2].Equals("")) { strMensaje += "- No contiene la hora inicial de atención. \\n"; }
                if (row[3].Equals("")) { strMensaje += "- No contiene la hora final de atención. \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del Taller debe ser entero. \\n"; }
                if (!EsRangoDia(row[1].ToString())) { strMensaje += "- El dia de atención debe ser entero. \\n"; }
                if (!EsTipoHora(row[2].ToString())) { strMensaje += "- La hora inicial debe tene el formato 'HH:mm'. \\n"; }
                if (!EsTipoHora(row[3].ToString())) { strMensaje += "- La hora final de atención debe tene el formato 'HH:mm'. \\n"; }
                if (!EsRangoHora(row[2].ToString())) { strMensaje += "- La hora inicial no esta en el rango de las horas válidas. \\n"; }
                if (!EsRangoHora(row[3].ToString())) { strMensaje += "- La hora final no esta en el rango de las horas válidas. \\n"; }
                if (EsTipoHora(row[2].ToString()) && EsTipoHora(row[3].ToString()))
                {
                    if (Convert.ToDateTime(row[2].ToString()) >= Convert.ToDateTime(row[3].ToString()))
                    {
                        strMensaje += "- La hora final debe ser mayor que la hora inicial de atención del taller. \\n";
                    }
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
                dtTallerHorario.Rows.Add(row);
            }

            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }

            if (!string.IsNullOrEmpty(strHorario)) strHorario = strHorario.Substring(0, strHorario.Length - 1);

            #endregion

            /*********************  MAE_TALLER_CAPACIDAD_ATENCION  *******************************************************************************/

            #region [CAPACIDAD_ATENCION]

            workSheet = (Excel.Worksheet)workBook.Sheets["CAPACIDAD_ATENCION"];
            strHoja = "CAPACIDAD_ATENCION";

            DataTable dtTallerCapacidad = new DataTable();
            dtTallerCapacidad.Columns.Add("ID");
            dtTallerCapacidad.Columns.Add("DIA_ATENCION");
            dtTallerCapacidad.Columns.Add("CAPACIDAD_FO");
            dtTallerCapacidad.Columns.Add("CAPACIDAD_BO");
            dtTallerCapacidad.Columns.Add("CAPACIDAD_TOTAL");
            dtTallerCapacidad.Columns.Add("FL_CONTROL");

            reg = 0;
            row = null;
            rowIndex = 3;
            fl_ok = true;
            existeID = false;
            strMensaje = string.Empty;

            while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                reg++;
                row = dtTallerCapacidad.NewRow();
                //---------------------------------------------------------------------------------------
                row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                row[2] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 4]).Text);
                row[3] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 5]).Text);
                row[4] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 6]).Text);
                //---------------------------------------------------------------------------------------
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validar relacion con el ID_TALLER de las otras hojas
                //------------------------------------------------------------------------------
                foreach (string strI in strID.Split('|'))
                {
                    if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                }
                if (!existeID)
                {
                    strMensaje = "- El ID Taller [" + row["ID"].ToString().Trim() + "] " +
                        "en la Hoja [CAPACIDAD_ATENCION] no tiene relacion con el ID del Taller de la hoja [TALLER]." + strError;
                    fl_ok = false;
                    break;
                }


                //Validacio 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (row[0].Equals("")) { strMensaje += "- No contiene el id Taller."; }
                if (row[1].Equals("")) { strMensaje += "- No contiene el dia de Atención. \\n"; }
                if (row[2].Equals("") && row[3].Equals("") && row[4].Equals("")) { strMensaje += "- Debe indicar la capacidad de atención. \\n"; }
                else if (!string.IsNullOrEmpty(row[4].ToString()) && !string.IsNullOrEmpty(string.Concat(row[2].ToString(), row[3].ToString())))
                {
                    strMensaje += "- Debe indicar la capacidad de atención en la Capacidad_FO y Capacidad_BO ó en Capacidad_Total. \\n";
                }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del Taller debe ser entero. \\n"; }
                if (!EsRangoDia(row[1].ToString())) { strMensaje += "- El dia de atención debe ser entero. \\n"; }


                if (!string.IsNullOrEmpty(row[2].ToString().Trim()))
                {
                    if (!EsEntero(row[2].ToString())) { strMensaje += "- La capacidad de atención del FO debe ser entera. \\n"; }
                    row[5] = "I";
                }
                if (!string.IsNullOrEmpty(row[3].ToString().Trim()))
                {
                    if (!EsEntero(row[3].ToString())) { strMensaje += "- La capacidad de atención del BO debe ser entera. \\n"; }
                    row[5] = "I";
                }
                if (!string.IsNullOrEmpty(row[4].ToString().Trim()))
                {
                    if (!EsEntero(row[4].ToString())) { strMensaje += "- La capacidad de atención Total debe ser entera. \\n"; }
                    row[5] = "T";
                }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //-------------------------------------------------------------------------------------------
                rowIndex++;
                dtTallerCapacidad.Rows.Add(row);
            }

            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }

            #endregion

            /*********************  MAE_TALLER_FECHA  *******************************************************************************/

            #region [FECHAS_EXCEPTUADAS]

            workSheet = (Excel.Worksheet)workBook.Sheets["FECHAS_EXCEPTUADAS"];
            strHoja = "FECHAS_EXCEPTUADAS";

            DataTable dtTallerFechaExep = new DataTable();
            dtTallerFechaExep.Columns.Add("ID");
            dtTallerFechaExep.Columns.Add("FECHA_EXCEPTUADA");

            reg = 0;
            row = null;
            rowIndex = 3;
            fl_ok = true;
            existeID = false;
            strMensaje = string.Empty;


            while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                reg++;
                row = dtTallerFechaExep.NewRow();
                //---------------------------------------------------------------------------------------
                row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                //---------------------------------------------------------------------------------------
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validar relacion con el ID_TALLER de las otras hojas
                //------------------------------------------------------------------------------
                foreach (string strI in strID.Split('|'))
                {
                    if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                }
                if (!existeID)
                {
                    strMensaje = "- El ID Taller [" + row["ID"].ToString().Trim() + "] " +
                        "en la Hoja [FECHAS_EXCEPTUADAS] no tiene relacion con el ID del Taller de la hoja [TALLER]." + strError;
                    fl_ok = false;
                    break;
                }


                //Validacio 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (row[0].Equals("")) { strMensaje += "- No contiene el id Taller."; }
                if (row[1].Equals("")) { strMensaje += "- No contiene la fecha exceptuada. \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del Taller debe ser entero. \\n"; }
                if (!EsTipoFecha(row[1].ToString())) { strMensaje += "- La fecha exceptuada debe estar en el formato " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + "  \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }


                //-------------------------------------------------------------------------------------------
                rowIndex++;
                dtTallerFechaExep.Rows.Add(row);
            }

            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }

            #endregion

            /*********************  MAE_TALLER_HORARIO_EXCEPCIONAL  *******************************************************************************/

            #region [HORARIO_EXCEPCIONAL]

            workSheet = (Excel.Worksheet)workBook.Sheets["HORARIO_EXCEPCIONAL"];
            strHoja = "HORARIO_EXCEPCIONAL";

            DataTable dtTallerHorarioExcepional = new DataTable();
            dtTallerHorarioExcepional.Columns.Add("ID");
            dtTallerHorarioExcepional.Columns.Add("NID_HORARIO");
            dtTallerHorarioExcepional.Columns.Add("NO_DESCRIPCION");
            dtTallerHorarioExcepional.Columns.Add("FECHA_INICIO");
            dtTallerHorarioExcepional.Columns.Add("FECHA_FIN");

            reg = 0;
            row = null;
            rowIndex = 3;
            fl_ok = true;
            existeID = false;
            strMensaje = string.Empty;

            string strIdHorario = string.Empty;//nid de horarios_excepcionales

            while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                reg++;
                row = dtTallerHorarioExcepional.NewRow();
                //---------------------------------------------------------------------------------------
                row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                row[2] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 4]).Text);
                row[3] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 5]).Text);
                row[4] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 6]).Text);
                //---------------------------------------------------------------------------------------
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validar relacion con el ID_TALLER de las otras hojas
                //------------------------------------------------------------------------------
                foreach (string strI in strID.Split('|'))
                {
                    if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                }
                if (!existeID)
                {
                    strMensaje = "- El ID Taller [" + row["ID"].ToString().Trim() + "] " +
                        "en la Hoja [HORARIO_EXCEPCIONAL] no tiene relacion con el ID del Taller de la hoja [TALLER]." + strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (row[0].Equals("")) { strMensaje += "- No contiene el id Taller."; }
                if (row[1].Equals("")) { strMensaje += "- No contiene el id del Horario Excepcional. \\n"; }
                if (row[2].Equals("")) { strMensaje += "- No contiene la descripcion del horario excepcional. \\n"; }
                if (row[3].Equals("")) { strMensaje += "- No contiene la fecha de inicio del horario excepcional. \\n"; }
                if (row[4].Equals("")) { strMensaje += "- No contiene la fecha de fin del horario excepcional. \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del Taller debe ser entero. \\n"; }
                if (!EsEntero(row[1].ToString())) { strMensaje += "- El id del Horario Excepcional debe ser entero. \\n"; }
                if (!EsTipoFecha(row[3].ToString())) { strMensaje += "- La fecha de inicio del horario excepcional debe estar en el formato " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + "  \\n"; }
                if (!EsTipoFecha(row[4].ToString())) { strMensaje += "- La fecha de fin del horario excepcional debe estar en el formato " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + "  \\n"; }

                if (EsTipoFecha(row[3].ToString()) && EsTipoFecha(row[4].ToString()))
                {
                    if (Convert.ToDateTime(row[3].ToString()) >= Convert.ToDateTime(row[4].ToString()))
                    {
                        strMensaje += "- La fecha final debe ser mayor que la fecha inicial del horario excepcional. \\n";
                    }
                }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }
                //-------------------------------------------------------------------------------------------

                strIdHorario += row["NID_HORARIO"].ToString().Trim() + "|";

                //-------------------------------------------------------------------------------------------
                rowIndex++;
                dtTallerHorarioExcepional.Rows.Add(row);
            }

            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }

            //Agrupando los NID_HORARIO
            if (!string.IsNullOrEmpty(strIdHorario)) strIdHorario = strIdHorario.Substring(0, strIdHorario.Length - 1);

            #endregion

            /*********************  MAE_TALLER_HORARIO_EXCEPCIONAL_DET *************************************************************/

            #region [HORARIO_EXCEPCIONAL_DET]

            workSheet = (Excel.Worksheet)workBook.Sheets["HORARIO_EXCEPCIONAL_DET"];
            strHoja = "HORARIO_EXCEPCIONAL_DET";


            DataTable dtTallerHorarioExc = new DataTable();
            dtTallerHorarioExc.Columns.Add("ID");
            dtTallerHorarioExc.Columns.Add("NID_HORARIO");
            dtTallerHorarioExc.Columns.Add("DIA_ATENCION");
            dtTallerHorarioExc.Columns.Add("HORA_INICIO_1");
            dtTallerHorarioExc.Columns.Add("HORA_FIN_1");
            dtTallerHorarioExc.Columns.Add("HORA_INICIO_2");
            dtTallerHorarioExc.Columns.Add("HORA_FIN_2");
            dtTallerHorarioExc.Columns.Add("HORA_INICIO_3");
            dtTallerHorarioExc.Columns.Add("HORA_FIN_4");

            reg = 0;
            row = null;
            rowIndex = 3;
            fl_ok = true;
            existeID = false;
            strMensaje = string.Empty;

            while (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
            {
                reg++;
                row = dtTallerHorarioExc.NewRow();
                //---------------------------------------------------------------------------------------
                row[0] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 2]).Text);
                row[1] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 3]).Text);
                row[2] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 4]).Text);
                row[3] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 5]).Text);
                row[4] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 6]).Text);
                row[5] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 7]).Text);
                row[6] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 8]).Text);
                row[7] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 9]).Text);
                row[8] = Convert.ToString(((Excel.Range)workSheet.Cells[rowIndex, 10]).Text);
                //---------------------------------------------------------------------------------------
                strError = "\\n" + "Registro: " + reg.ToString() + " - Hoja: " + strHoja;
                //---------------------------------------------------------------------------------------

                //Validar relacion con el ID_TALLER de las otras hojas
                //------------------------------------------------------------------------------
                foreach (string strI in strID.Split('|'))
                {
                    if (row["ID"].ToString().Trim().Equals(strI.Trim())) { existeID = true; break; }
                }
                if (!existeID)
                {
                    strMensaje = "- El ID Taller [" + row["ID"].ToString().Trim() + "] " +
                        "en la Hoja [HORARIO_EXCEPCIONAL_DET] no tiene relacion con el ID del Taller de la hoja [TALLER]." + strError;
                    fl_ok = false;
                    break;
                }



                //Validar relacion con el 'NID_HORARIO' con la hoja anterior
                //------------------------------------------------------------------------------
                foreach (string strH in strIdHorario.Split('|'))
                {
                    if (row["NID_HORARIO"].ToString().Trim().Equals(strH.Trim())) { existeID = true; break; }
                }
                if (!existeID)
                {
                    strMensaje = "- El ID Horario [" + row["NID_HORARIO"].ToString().Trim() + "] " +
                        "en la Hoja [HORARIO_EXCEPCIONAL_DET] no tiene relacion con el ID Horario de la hoja [HORARIO_EXCEPCIONAL]." + strError;
                    fl_ok = false;
                    break;
                }


                //Validacio 1 - Campos Obligatorios
                //------------------------------------------------------------------------------
                if (row[0].Equals("")) { strMensaje += "- No contiene el id Taller."; }
                if (row[1].Equals("")) { strMensaje += "- No contiene el id del Horario Excepcional. \\n"; }
                if (row[2].Equals("")) { strMensaje += "- No contiene el dia de atención. \\n"; }
                if ((row[3].Equals("") && row[4].Equals("")) && (row[5].Equals("") && row[6].Equals("")) && (row[7].Equals("") && row[8].Equals("")))
                { strMensaje += "- No contiene al menos un rango de horas excepcional. \\n"; }

                //--------
                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 2 - Tipo de Dato Correcto
                //-------------------------------------------------------------------------------------------
                if (!EsEntero(row[0].ToString())) { strMensaje += "- El ID del Taller debe ser entero. \\n"; }
                if (!EsEntero(row[1].ToString())) { strMensaje += "- El id del Horario Excepcional debe ser entero. \\n"; }
                if (!EsRangoDia(row[2].ToString())) { strMensaje += "- El dia de atención debe ser entero. \\n"; }

                if (!EsTipoHora(row[3].ToString())) { strMensaje += "- La hora excepcional inicial  del Rango 1 debe tene el formato 'HH:mm'. \\n"; }
                if (!EsTipoHora(row[4].ToString())) { strMensaje += "- La hora excepcional final  del Rango 1 debe tene el formato 'HH:mm'. \\n"; }
                if (!EsTipoHora(row[5].ToString())) { strMensaje += "- La hora excepcional inicial  del Rango 2 debe tene el formato 'HH:mm'. \\n"; }
                if (!EsTipoHora(row[6].ToString())) { strMensaje += "- La hora excepcional final  del Rango 2 debe tene el formato 'HH:mm'. \\n"; }
                if (!EsTipoHora(row[7].ToString())) { strMensaje += "- La hora excepcional inicial  del Rango 3 debe tene el formato 'HH:mm'. \\n"; }
                if (!EsTipoHora(row[8].ToString())) { strMensaje += "- La hora excepcional final  del Rango 3 debe tene el formato 'HH:mm'. \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 3 - Horas en el Rango del Taller
                //-------------------------------------------------------------------------------------------

                if (!EsRangoHoraExcepcional(row[0].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString())) { strMensaje += "- El Rango 1 del Horario Excepcioanl no pertenece al Horario del Taller " + row[0].ToString() + ". \\n"; }
                if (!EsRangoHoraExcepcional(row[0].ToString(), row[2].ToString(), row[5].ToString(), row[6].ToString())) { strMensaje += "- El Rango 2 del Horario Excepcioanl no pertenece al Horario del Taller " + row[0].ToString() + ". \\n"; }
                if (!EsRangoHoraExcepcional(row[0].ToString(), row[2].ToString(), row[7].ToString(), row[8].ToString())) { strMensaje += "- El Rango 3 del Horario Excepcioanl no pertenece al Horario del Taller " + row[0].ToString() + ". \\n"; }

                if (strMensaje.Trim().Length > 0)
                {
                    strMensaje += strError;
                    fl_ok = false;
                    break;
                }

                //Validacio 4 - ValidarRangos [Inicial - Final]
                //-------------------------------------------------------------------------------------------

                if (!string.IsNullOrEmpty(row[3].ToString()) && !string.IsNullOrEmpty(row[4].ToString()))
                {
                    if (Convert.ToDateTime(row[3].ToString()) >= Convert.ToDateTime(row[4].ToString()))
                    {
                        strMensaje += "- La hora final del Rango 1 del Horario Excepcional debe ser mayor que la hora inicial. \\n";
                    }
                }
                if (!string.IsNullOrEmpty(row[5].ToString()) && !string.IsNullOrEmpty(row[6].ToString()))
                {
                    if (Convert.ToDateTime(row[5].ToString()) >= Convert.ToDateTime(row[6].ToString()))
                    {
                        strMensaje += "- La hora final del Rango 2 del Horario Excepcional debe ser mayor que la hora inicial. \\n";
                    }
                }
                if (!string.IsNullOrEmpty(row[7].ToString()) && !string.IsNullOrEmpty(row[8].ToString()))
                {
                    if (Convert.ToDateTime(row[7].ToString()) >= Convert.ToDateTime(row[8].ToString()))
                    {
                        strMensaje += "- La hora final del Rango 3 del Horario Excepcional debe ser mayor que la hora inicial. \\n";
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
                dtTallerHorarioExc.Rows.Add(row);
            }

            if (!fl_ok)
            {
                SRC_MsgInformacion(strMensaje);
                return "0";
            }

            #endregion

            /*************************************************************************************************************/

            //applExcel.Workbooks.Close();

            TallerBE oMaestroTallerBE = new TallerBE();
            TallerBL oMaestroTallerBL = new TallerBL();

            string strIDTaller = string.Empty;
            int resp = 0;
            int intReg = 0;

            foreach (string idTaller in strID.Split('|'))
            {
                if (string.IsNullOrEmpty(idTaller.Trim())) continue;

                //--------------------------------------------------------------------------------------------------------        

                #region GRABAR DATOS TALLER


                oMaestroTallerBE.co_taller = dtTaller.Rows[intReg][1].ToString();
                oMaestroTallerBE.no_taller = dtTaller.Rows[intReg][2].ToString();
                oMaestroTallerBE.Cod_intervalo = int.Parse(dtTaller.Rows[intReg][3].ToString());
                oMaestroTallerBE.nid_ubica = int.Parse(dtTaller.Rows[intReg][4].ToString());
                oMaestroTallerBE.no_direccion = dtTaller.Rows[intReg][5].ToString();
                oMaestroTallerBE.nu_telefono = dtTaller.Rows[intReg][6].ToString();
                oMaestroTallerBE.tx_url_taller = dtTaller.Rows[intReg][7].ToString();
                oMaestroTallerBE.fl_activo = "A";
                //-------------------------------------------------------------------------------
                oMaestroTallerBE.co_usuario = Profile.UserName;
                oMaestroTallerBE.co_usuario_red = Profile.UsuarioRed;
                oMaestroTallerBE.no_estacion_red = Profile.Estacion;

                strIDTaller = oMaestroTallerBL.InsertTaller(oMaestroTallerBE).ToString();

                #endregion

                #region GRABAR HORARIOS



                foreach (DataRow oRow in dtTallerHorario.Rows)
                {
                    if (!oRow[0].ToString().Equals(idTaller)) continue;
                    if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                    oMaestroTallerBE = new TallerBE();

                    oMaestroTallerBE.nid_taller = int.Parse(strIDTaller);
                    oMaestroTallerBE.Dd_atencion = Convert.ToInt32(oRow[1].ToString());
                    oMaestroTallerBE.HoraInicio = oRow[2].ToString();
                    oMaestroTallerBE.HoraFin = oRow[3].ToString();
                    oMaestroTallerBE.Fl_tipo = "T";
                    oMaestroTallerBE.co_usuario = Profile.UserName;
                    oMaestroTallerBE.co_usuario_red = Profile.UsuarioRed;
                    oMaestroTallerBE.no_estacion_red = Profile.Estacion;
                    oMaestroTallerBE.fl_activo = "A";

                    resp = oMaestroTallerBL.InsertTallerHorario(oMaestroTallerBE);

                }

                #endregion

                #region GRABAR CAPACIDAD DE ATENCION

                TallerHorariosBE objECapacidad = new TallerHorariosBE();
                TallerBL objLCapacidad = new TallerBL();

                foreach (DataRow oRow in dtTallerCapacidad.Rows)
                {
                    if (!oRow[0].ToString().Equals(idTaller)) continue;
                    if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                    objECapacidad.nid_propietario = int.Parse(strIDTaller);
                    objECapacidad.fl_tipo = "T";
                    objECapacidad.dd_atencion = Convert.ToInt32(oRow[1].ToString());

                    if (!string.IsNullOrEmpty(oRow[2].ToString())) objECapacidad.qt_capacidad_fo = Convert.ToInt32(oRow[2].ToString());
                    if (!string.IsNullOrEmpty(oRow[3].ToString())) objECapacidad.qt_capacidad_bo = Convert.ToInt32(oRow[3].ToString());
                    if (!string.IsNullOrEmpty(oRow[4].ToString())) objECapacidad.qt_capacidad = Convert.ToInt32(oRow[4].ToString());

                    objECapacidad.fl_control = oRow[5].ToString();
                    objECapacidad.co_usuario = Profile.UserName;
                    objECapacidad.co_usuario_red = Profile.UsuarioRed;
                    objECapacidad.no_estacion_red = Profile.Estacion;

                    resp = objLCapacidad.MantenimientoCapacidadAtencion_Taller(objECapacidad);
                }

                #endregion

                #region GRABAR DIAS EXCEPTUADOS

                foreach (DataRow oRow in dtTallerFechaExep.Rows)
                {
                    if (!oRow[0].ToString().Equals(idTaller)) continue;
                    if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                    oMaestroTallerBE = new TallerBE();

                    oMaestroTallerBE.nid_taller = int.Parse(strIDTaller);
                    oMaestroTallerBE.Fe_exceptuada = Convert.ToDateTime(oRow[1].ToString());
                    oMaestroTallerBE.co_usuario = Profile.UserName;
                    oMaestroTallerBE.co_usuario_red = Profile.UsuarioRed;
                    oMaestroTallerBE.no_estacion_red = Profile.Estacion;
                    oMaestroTallerBE.fl_activo = "A";

                    resp = oMaestroTallerBL.InsertTallerDiaExceptuado(oMaestroTallerBE);
                }

                #endregion

                #region GRABAR SERVICIOS

                foreach (DataRow oRow in dtTallerServicio.Rows)
                {
                    if (!oRow[0].ToString().Equals(idTaller)) continue;
                    if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                    oMaestroTallerBE = new TallerBE();

                    oMaestroTallerBE.nid_taller = int.Parse(strIDTaller);
                    oMaestroTallerBE.Nid_serv = Convert.ToInt32(oRow[1].ToString());
                    oMaestroTallerBE.co_usuario = Profile.UserName;
                    oMaestroTallerBE.co_usuario_red = Profile.UsuarioRed;
                    oMaestroTallerBE.no_estacion_red = Profile.Estacion;
                    oMaestroTallerBE.fl_activo = "A";

                    resp = oMaestroTallerBL.InsertTallerServicio(oMaestroTallerBE);

                }

                #endregion

                #region GRABAR MODELOS

                foreach (DataRow oRow in dtTallerModelo.Rows)
                {
                    if (!oRow[0].ToString().Equals(idTaller)) continue;
                    if (string.IsNullOrEmpty(oRow[1].ToString())) continue;

                    oMaestroTallerBE = new TallerBE();

                    oMaestroTallerBE.nid_taller = int.Parse(strIDTaller);
                    oMaestroTallerBE.Nid_modelo = Convert.ToInt32(oRow[1].ToString());
                    oMaestroTallerBE.co_usuario = Profile.UserName;
                    oMaestroTallerBE.co_usuario_red = Profile.UsuarioRed;
                    oMaestroTallerBE.no_estacion_red = Profile.Estacion;
                    oMaestroTallerBE.fl_activo = "A";
                    resp = oMaestroTallerBL.InsertTallerModelo(oMaestroTallerBE);

                }

                #endregion

                //--------------------------------------------------------------------------------------------------------        

                intReg++;
            }

            SRC_MsgInformacion("Todos los registros [" + intReg.ToString() + "] fueron insertados correctamente.");

            /*************************************************************************************************************/
        }
        catch (Exception ex)
        {
            strResp = "-1";
            SRC_MsgInformacion(ex.Message);
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

    private bool EsTipoHora(string str)
    {        
        DateTime oDate;
        bool fl = false;
        try
        {
            if (!string.IsNullOrEmpty(str))
                oDate = DateTime.Parse(str);
            fl = true;
        }
        catch
        {
            fl = false;
        }

        return fl;

    }

    private bool EsRangoHora(string str)
    {
        DateTime oDate;
        DateTime oDateF;
        bool fl = false;
        try
        {
            oDate = DateTime.Parse(str);
            ParametrosBackOffieBEList Horas = (ParametrosBackOffieBEList)ViewState["HorasTaller"];
            
            foreach (ParametrosBackOfficeBE oEntidad in Horas)
            {
                oDateF = DateTime.Parse(oEntidad.ID);
                //. .ToShortTimeString().Equals(oDateF.ToShortTimeString()))
                if (oDate.ToString("HH:mm").Equals(oDateF.ToString("HH:mm")))
                {
                    fl = true; break;
                }
            }
        }
        catch
        {
            fl = false;
        }

        return fl;
    }

    private bool EsRangoHoraExcepcional(string IdTaller, string Dia, string HoraIni, string HoraFin)
    {
        //DateTime oDate;
        bool fl = true;
        try
        {
            if (string.IsNullOrEmpty(HoraIni) || string.IsNullOrEmpty(HoraFin))
            {
                fl = true;
            }
            else
            {
                DateTime dHoraIni = Convert.ToDateTime(HoraIni);
                DateTime dHoraFin = Convert.ToDateTime(HoraFin);

                foreach (string _sHoras in strHorario.Split('='))
                {
                    if (_sHoras.Split('|').GetValue(0).ToString().Equals(IdTaller))
                    {
                        DateTime _HI = Convert.ToDateTime(_sHoras.Split('|').GetValue(2).ToString());
                        DateTime _HF = Convert.ToDateTime(_sHoras.Split('|').GetValue(3).ToString());

                        if (dHoraIni >= _HI && dHoraFin <= _HF)
                        {
                            fl = true;
                            break;
                        }
                        else
                        {
                            //fl = false;
                            //break;
                        }
                    }
                }
            }
        }
        catch
        {
            fl = false;
        }

        return fl;

    }

    
    
    private bool EsRangoDia(string str)
    {
        Int32 intEnt;
        bool fl = false;
        try
        {
            intEnt = Int32.Parse(str);
            fl = (intEnt >= 1 && intEnt <= 7);
        }
        catch
        {
            fl = false;
        }

        return fl;

    }

    private bool EsTipoFecha(string str)
    {
        DateTime oDate;
        bool fl = false;
        try
        {
            fl = true;            
            oDate = DateTime.Parse(str);
            //fl = oDate.ToShortDateString().Equals(str);
            //fl = (oDate.ToString("dd/MM/yyyy").Equals(str));
        }
        catch
        {
            fl = false;
        }

        return fl;
    }

    #endregion

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

                string strResp = ImportarDatosTallerFromExcel(rutaArchivo);

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
            SRC_MsgInformacion(ex.Message);
        }

    }

    protected void btnGargaMasiva_Click(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void Button1_Click1(object sender, EventArgs e)
    {

    }


}
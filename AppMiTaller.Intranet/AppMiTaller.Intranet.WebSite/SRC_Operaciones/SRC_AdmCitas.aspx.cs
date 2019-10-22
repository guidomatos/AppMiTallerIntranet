using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.Text;
using System.Globalization;

public partial class SRC_Operaciones_SRC_AdmCitas : System.Web.UI.Page
{
    private const string imgURL_HORA_LIBRE = "~/Images/SRC/SI.PNG";
    private const string imgURL_HORA_RESERVADA = "~/Images/SRC/NO.PNG";
    private const string imgURL_HORA_VACIA = "~/Images/SRC/vacio.PNG";
    private const string imgURL_HORA_EXCEPCIONAL = "~/Images/SRC/vacio.PNG";
    private const string imgURL_HORA_SEPARADA = "~/Images/SRC/SEPARA.PNG";

    private const string _SELECCIONAR = "--Seleccionar--";
    private const string _TODOS = "---- Todos ----";

    private const Int32 _WIDTH_COL_TALLER = 100;
    private const Int32 _WIDTH_COL_ASESOR = 200;
    private const Int32 _WIDTH_COL_TCA = 20;
    private const Int32 _WIDTH_COL_TCE = 20;
    private const Int32 _WIDTH_COL_HORAS = 45;

    private const string _DATA_TALLER = "TALLER";
    private const string _DATA_ASESOR = "ASESOR_SERVICIO";

    private const string _HEADER_TALLER = "Taller";
    private const string _HEADER_ASESOR = "Asesor de Servicio";

    private const string _MSG_PREGUNTA = "¿ Esta seguro de {estado} la Cita ?.";
    private const string _MSG_RESPUESTA = "La Cita quedó {estado}.";


    private TallerBE oMaestroTallerBE = new TallerBE();
    private TallerBL oMaestroTallerBL = new TallerBL();

    private VehiculoBE oMaestroVehiculoBE = new VehiculoBE();
    private MaestroVehiculoBL oMaestroVehiculoBL = new MaestroVehiculoBL();

    private AdminCitaBE oAdminCitaBE = new AdminCitaBE();
    private AdminCitasBL oAdminCitasBL = new AdminCitasBL();
    private AdminCitaBEList oAdminCitaBEList;

    private ModeloBL oMaestroModeloBL = new ModeloBL();

    private CombosBEList oCombosBEList;

    private CitasBE oCitasBE;
    private CitasBEList oCitasBEList;

    string strEstadoCita = string.Empty;
    Parametros oParametros = new Parametros();

    protected void Page_PreRender(object sender, EventArgs e)
    {
        AdminCitaBEList oAdminCitaBEList = (AdminCitaBEList)(ViewState["oAdminCitaBEList"]);
        if (oAdminCitaBEList != null &&
            this.gv_admcitas != null &&
            this.gv_admcitas.Rows.Count > 0 &&
            this.gv_admcitas.PageCount > 1)
        {
            GridViewRow oRow = this.gv_admcitas.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", (oAdminCitaBEList.Count));

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Acceso a Pagina
        string AccesoPagina = (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_AccionVerForm);
        if (string.IsNullOrEmpty(AccesoPagina))
            AccesoPagina = (Master as Principal).ValidaTipoAccesoPagina(Page, "SinAcceso");


        if (!Page.IsPostBack)
        {
            ViewState["ordenGridTalleres"] = SortDirection.Descending;

            InicializaPagina();
        }
    }

    private void Limpiar()
    {
        txt_bus_fecreg1.Text = string.Empty;
        txt_bus_fecreg2.Text = string.Empty;
        txt_bus_feccita1.Text = string.Empty;
        txt_bus_feccita2.Text = string.Empty;
        txt_bus_hr1.Text = string.Empty;
        txt_bus_hr2.Text = string.Empty;
    }

    #region Seguridad Botones

    public bool OpcionVerDet()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_VerDet).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionActEstVerif()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_ActEstVerif).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionAnuCita()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_AnuCita).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionConfCita()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_ConfCita).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionReprogCita()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_ReprogCita).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionReasigCita()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_ReasigCita).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionActDatosVehPropie()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_ActDatosVehPropie).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    #endregion

    #region Metodos

    private void InicializaPagina()
    {

        ClienteBL oMaestroClienteBL = new ClienteBL();

        //Etiquetas
        gv_admcitas.Columns[1].HeaderText = oParametros.N_Taller.ToString();
        gv_admcitas.Columns[6].HeaderText = oParametros.N_Placa.ToString();
        //gv_admcitas.Columns[15].HeaderText = oParametros.N_Departamento.ToString();
        //gv_admcitas.Columns[16].HeaderText = oParametros.N_Provincia.ToString();
        //gv_admcitas.Columns[17].HeaderText = oParametros.N_Distrito.ToString();
        //gv_admcitas.Columns[18].HeaderText = oParametros.N_Local.ToString();

        lblTextoLocal.Text = oParametros.N_Local.ToString();
        lblTextoTaller.Text = oParametros.N_Taller.ToString();
        lblTextoPlaca.Text = oParametros.N_Placa.ToString();
        lblTextoLocalR.Text = oParametros.N_Local.ToString();
        lblTextoTallerR.Text = oParametros.N_Taller.ToString();

        Label5.Text = oParametros.N_Departamento.ToString();
        Label9.Text = oParametros.N_Provincia.ToString();
        Label13.Text = oParametros.N_Distrito.ToString();

        btnVerDetalle.Visible = OpcionVerDet();
        //btnActualizarEstado.Visible = OpcionActEstVerif();
        btnActualizarEstado.Visible = false;
        btnAnularCita.Visible = OpcionAnuCita();
        //btnConfirmarCita.Visible = OpcionConfCita();
        btnConfirmarCita.Visible = false;
        //btnReasignarCita.Visible = OpcionReasigCita();
        btnReasignarCita.Visible = false;
        btnReprogramarCita.Visible = OpcionReprogCita();
        //btn_bus_ActDatosVehPropie.Visible = OpcionActDatosVehPropie();
        btn_bus_ActDatosVehPropie.Visible = false;


        hf_DETALLE.Value = String.Empty;

        txt_bus_nrodoc.Attributes.Add("onkeypress", "return SoloNumeros(event)");

        //Indicador Pendientes
        cboIndPendiente.Items.Add(new ListItem(_TODOS, "2"));
        cboIndPendiente.Items.Add(new ListItem("Datos Incompletos", "1"));
        cboIndPendiente.Items.Add(new ListItem("Datos Completos", "0"));

        //Tipos de Documentos
        ddl_bus_tipodoc.DataSource = oMaestroClienteBL.GETListarTipoDocumento("0");
        ddl_bus_tipodoc.DataTextField = "DES";
        ddl_bus_tipodoc.DataValueField = "ID";
        ddl_bus_tipodoc.DataBind();
        ddl_bus_tipodoc.Items.Insert(0, new ListItem(_TODOS, "0"));
        ddl_bus_tipodoc.SelectedIndex = (ddl_bus_tipodoc.Items.Count == 2) ? 1 : 0;

        //Departamentos
        ddl_bus_departamento.DataSource = oMaestroTallerBL.GETListarDepartamento(Profile.Usuario.Nid_usuario);
        ddl_bus_departamento.DataTextField = "DES";
        ddl_bus_departamento.DataValueField = "ID";
        ddl_bus_departamento.DataBind();
        ddl_bus_departamento.Items.Insert(0, new ListItem(_TODOS, "0"));
        ddl_bus_departamento.SelectedIndex = 0;

        //Default
        ddl_bus_provincia.Items.Insert(0, new ListItem(_TODOS, "0"));
        ddl_bus_distrito.Items.Insert(0, new ListItem(_TODOS, "0"));
        ddl_bus_puntored.Items.Insert(0, new ListItem(_TODOS, "0"));
        ddl_bus_taller.Items.Insert(0, new ListItem(_TODOS, "0"));

        //Estado Reserva
        ddl_bus_estreserva.DataSource = oAdminCitasBL.GETListarEstCitas();
        ddl_bus_estreserva.DataTextField = "DES";
        ddl_bus_estreserva.DataValueField = "ID";
        ddl_bus_estreserva.DataBind();
        ddl_bus_estreserva.Items.Insert(0, new ListItem(_TODOS, "0"));
        ddl_bus_estreserva.SelectedIndex = 0;

        //Marcas
        ddl_bus_marca.DataSource = oMaestroModeloBL.GETListarMarcas(Profile.Usuario.Nid_usuario);
        ddl_bus_marca.DataTextField = "DES";
        ddl_bus_marca.DataValueField = "ID";
        ddl_bus_marca.DataBind();
        ddl_bus_marca.Items.Insert(0, new ListItem(_TODOS, "0"));
        ddl_bus_marca.SelectedIndex = (ddl_bus_marca.Items.Count == 2) ? 1 : 0;

        ddl_bus_modvehiculo.Items.Insert(0, new ListItem(_TODOS, "0"));

        //----------------------------------------------------       

        CalendarExtender1.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        CalendarExtender2.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        CalendarExtender3.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        CalendarExtender4.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        ceFecha.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        ceFechaIni.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        ceFechaFin.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        this.btnVerDetalle.Attributes.Add("onclick", "javascript:return validarSelGridView('1','" + gv_admcitas.ClientID + "');");
        this.btnConfirmarCita.Attributes.Add("onclick", "javascript:return validarSelGridView('2','" + gv_admcitas.ClientID + "');");
        this.btnReprogramarCita.Attributes.Add("onclick", "javascript:return validarSelGridView('3','" + gv_admcitas.ClientID + "');");
        this.btnAnularCita.Attributes.Add("onclick", "javascript:return validarSelGridView('4','" + gv_admcitas.ClientID + "');");
        this.btnReasignarCita.Attributes.Add("onclick", "javascript:return validarSelGridView('5','" + gv_admcitas.ClientID + "');");
        this.btnActualizarEstado.Attributes.Add("onclick", "javascript:return validarSelGridView('6','" + gv_admcitas.ClientID + "');");
        this.btnImprimir.Attributes.Add("onclick", "javascript:return validarSelGridView('7','" + gv_admcitas.ClientID + "');");

        //-------------------------------------------------------------

        //INICIALIZANDO EL GRIDVIEW
        this.oAdminCitaBEList = new AdminCitaBEList();
        this.oAdminCitaBEList.Add(new AdminCitaBE());
        ViewState["oAdminCitaBEList"] = oAdminCitaBEList;
        this.gv_admcitas.DataSource = this.oAdminCitaBEList;
        this.gv_admcitas.DataBind();

        Cargar_DLL_Taller();

        //-------------------------------------------------------------

        if (Session["AdminCitaBE_FILTROS"] != null)
        {
            oMaestroVehiculoBE = new VehiculoBE();
            oMaestroTallerBE = new TallerBE();

            AdminCitaBE oEntidad = new AdminCitaBE();
            oCombosBEList = new CombosBEList();

            oEntidad = (AdminCitaBE)(Session["AdminCitaBE_FILTROS"]);

            txt_bus_codreserva.Text = oEntidad.cod_reserva_cita.Trim();

            if (!oEntidad.coddpto.ToString().Equals("0"))
            {
                ddl_bus_departamento.SelectedValue = oEntidad.coddpto.ToString().Trim();
                oMaestroTallerBE.coddpto = oEntidad.coddpto;
                oCombosBEList = new CombosBEList();
                oCombosBEList = oMaestroTallerBL.GETListarProvincia(oMaestroTallerBE, Profile.Usuario.Nid_usuario);
                cargarComboBox(ddl_bus_provincia, oCombosBEList, "DES", "ID", _TODOS);

                if (!oEntidad.codprov.ToString().Equals("0"))
                {
                    ddl_bus_provincia.SelectedValue = oEntidad.codprov.ToString().Trim();
                    oMaestroTallerBE.codprov = oEntidad.codprov;
                    oCombosBEList = new CombosBEList();
                    oCombosBEList = oMaestroTallerBL.GETListarDistrito(oMaestroTallerBE, Profile.Usuario.Nid_usuario);
                    cargarComboBox(ddl_bus_distrito, oCombosBEList, "DES", "ID", _TODOS);

                    if (!oEntidad.coddist.ToString().Equals("0"))
                    {
                        ddl_bus_distrito.SelectedValue = oEntidad.coddist.ToString().Trim();
                        oMaestroTallerBE.coddist = oEntidad.coddist;
                        oCombosBEList = new CombosBEList();
                        oCombosBEList = oMaestroTallerBL.GETListarPuntoRed(oMaestroTallerBE, Profile.Usuario.Nid_usuario);
                        cargarComboBox(ddl_bus_puntored, oCombosBEList, "DES", "IntID", _TODOS);
                    }
                }
            }

            ddl_bus_puntored.SelectedValue = oEntidad.nid_ubica.ToString().Trim();
            oCombosBEList = new CombosBEList();
            oCombosBEList = oAdminCitasBL.GETListarTalleres(oEntidad.nid_ubica, Profile.Usuario.Nid_usuario);
            cargarComboBox(ddl_bus_taller, oCombosBEList, "DES", "ID", _TODOS);

            ddl_bus_taller.SelectedValue = oEntidad.nid_taller.ToString().Trim();
            txt_bus_asesorservicio.Text = oEntidad.AsesorServicio.Trim();
            ddl_bus_estreserva.SelectedValue = oEntidad.Estadoreserva.ToString();
            cboIndPendiente.SelectedValue = oEntidad.IndPendiente.ToString().Trim();
            txt_bus_fecreg1.Text = oEntidad.fecreg1.Trim();
            txt_bus_fecreg2.Text = oEntidad.fecreg2.Trim();
            txt_bus_feccita1.Text = oEntidad.feccita1.Trim();
            txt_bus_feccita2.Text = oEntidad.feccita2.Trim();
            txt_bus_hr1.Text = oEntidad.horacita1.Trim();
            txt_bus_hr2.Text = oEntidad.horacita2.Trim();
            txt_bus_placapatente.Text = oEntidad.nu_placa.Trim();

            if (!oEntidad.nid_marca.Equals(0))
            {
                ddl_bus_marca.SelectedValue = oEntidad.nid_marca.ToString().Trim();
                oMaestroVehiculoBE.nid_marca = oEntidad.nid_marca;
                oCombosBEList = new CombosBEList();
                oCombosBEList = oMaestroVehiculoBL.GETListarModelosXMarca(oMaestroVehiculoBE, Profile.Usuario.Nid_usuario);

                ddl_bus_modvehiculo.Items.Clear();
                foreach (ComboBE oModelo in oCombosBEList) { ddl_bus_modvehiculo.Items.Add(new ListItem(oModelo.DES, oModelo.ID)); }
                ddl_bus_modvehiculo.Items.Insert(0, new ListItem(_TODOS, "0"));
            }

            ddl_bus_modvehiculo.SelectedValue = oEntidad.nid_modelo.ToString().Trim();
            ddl_bus_tipodoc.SelectedValue = oEntidad.co_tipo_documento.ToString().Trim();
            txt_bus_nrodoc.Text = oEntidad.nu_documento.Trim();
            txt_bus_nombres.Text = oEntidad.no_cliente.Trim();
            txt_bus_apellidos.Text = oEntidad.no_apellidos.Trim();

            BuscarCitas(oEntidad);


        }
    }

    private void Cargar_DLL_Taller()
    {
        oCombosBEList = new CombosBEList();
        oCombosBEList = oAdminCitasBL.GETListarTalleres(Int32.Parse(ddl_bus_puntored.SelectedValue.ToString().Trim()), Profile.Usuario.Nid_usuario);
        cargarComboBox(ddl_bus_taller, oCombosBEList, "DES", "ID", _TODOS);

    }

    private void BuscarCitas(AdminCitaBE ent)
    {
        try
        {
            this.oAdminCitaBEList = oAdminCitasBL.GETListaAdminCitasP(ent, Profile.Usuario.Nid_usuario);

            if (oAdminCitaBEList == null || oAdminCitaBEList.Count == 0)
            {
                Session["AdminCitaBE_FILTROS"] = null;
                oAdminCitaBE = null;
                oAdminCitaBE = new AdminCitaBE();
                oAdminCitaBEList.Add(oAdminCitaBE);
            }
            this.gv_admcitas.DataSource = oAdminCitaBEList;

            this.gv_admcitas.DataBind();
            this.gv_admcitas.PageIndex = (gv_admcitas.PageCount >= (ent.paginacion + 1)) ? ent.paginacion : 0;
            this.gv_admcitas.DataBind();

            hf_DETALLE.Value = string.Empty;
            ViewState["oAdminCitaBEList"] = oAdminCitaBEList;
        }
        catch (Exception ex)
        {
            SRC_MsgExclamacion(ex.Message);
        }

    }

    private void vaciarGrillaHorarioDisponible()
    {
        GridView gvControl = gvHorarioDisponible;
        for (Int32 f = 0; f <= gvControl.Rows.Count - 1; f++)
        {
            for (Int32 c = 2; c <= gvControl.Columns.Count - 1; c++)
            {
                DataControlFieldCell CTR1 = (DataControlFieldCell)gvControl.Rows[f].Controls[c];
                foreach (Control _ctr in CTR1.Controls)
                {
                    if (_ctr.GetType().ToString() == "System.Web.UI.WebControls.DataControlImageButton")
                    {
                        ImageButton imgB1 = (ImageButton)_ctr;
                        if (imgB1.ImageUrl.ToUpper() == imgURL_HORA_SEPARADA.ToUpper())
                        {
                            imgB1.ImageUrl = imgURL_HORA_LIBRE;
                            break;
                        }
                    }
                }
            }
        }
        hf_DATOS_CITA.Text = "";
        lblSeleccion2.Text = String.Empty;
    }
    #endregion

    /***************************************************************************************/

    #region "----------- MENSAJES --------------"

    private void MensajeScript(string msg)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('" + msg + "');</script>", false);
    }
    private void SRC_MsgInformacion(string strError)
    {
        MensajeScript(strError);
    }
    private void SRC_MsgExclamacion(string strError)
    {
        MensajeScript(strError);
    }
    private void SRC_MsgError(string strError)
    {
        MensajeScript(strError);
    }

    #endregion

    /***************************************************************************************/

    #region "---------- BUSQUEDA DE CITA: EVENTOS DE CONTROLES  -----------------"

    protected void cargarComboBox(DropDownList ddl, Object lista, string Text, string Value, string header)
    {
        ddl.Items.Clear();
        ddl.DataSource = lista;
        ddl.DataTextField = Text;
        ddl.DataValueField = Value;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(header, "0"));
        ddl.SelectedIndex = (ddl.Items.Count == 2) ? 1 : 0;
    }

    protected void ddl_bus_departamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_bus_departamento.SelectedValue.ToString().Equals("0"))
            {
                ddl_bus_puntored.Items.Clear(); ddl_bus_puntored.Items.Insert(0, new ListItem(_TODOS, "0"));
                ddl_bus_distrito.Items.Clear(); ddl_bus_distrito.Items.Insert(0, new ListItem(_TODOS, "0"));
                ddl_bus_provincia.Items.Clear(); ddl_bus_provincia.Items.Insert(0, new ListItem(_TODOS, "0"));
                Cargar_DLL_Taller();
            }
            else
            {
                oMaestroTallerBE.coddpto = ddl_bus_departamento.SelectedValue.ToString().Trim();

                oCombosBEList = new CombosBEList();
                oCombosBEList = oMaestroTallerBL.GETListarProvincia(oMaestroTallerBE, Profile.Usuario.Nid_usuario);

                cargarComboBox(ddl_bus_provincia, oCombosBEList, "DES", "ID", _TODOS);
                if (oCombosBEList.Count == 1) ddl_bus_provincia_SelectedIndexChanged(this, null);
            }
        }
        catch (Exception)
        {
            //-->
        }
    }
    protected void ddl_bus_provincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_bus_provincia.SelectedValue.ToString().Equals("0"))
            {
                ddl_bus_puntored.Items.Clear(); ddl_bus_puntored.Items.Insert(0, new ListItem(_TODOS, "0"));
                ddl_bus_distrito.Items.Clear(); ddl_bus_distrito.Items.Insert(0, new ListItem(_TODOS, "0"));
                Cargar_DLL_Taller();
            }
            else
            {
                oMaestroTallerBE.coddpto = ddl_bus_departamento.SelectedValue.ToString().Trim();
                oMaestroTallerBE.codprov = ddl_bus_provincia.SelectedValue.ToString().Trim();

                oCombosBEList = new CombosBEList();
                oCombosBEList = oMaestroTallerBL.GETListarDistrito(oMaestroTallerBE, Profile.Usuario.Nid_usuario);

                cargarComboBox(ddl_bus_distrito, oCombosBEList, "DES", "ID", _TODOS);
                if (oCombosBEList.Count == 1) ddl_bus_distrito_SelectedIndexChanged(this, null);
            }
        }
        catch (Exception)
        {

        }
    }
    protected void ddl_bus_distrito_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_bus_distrito.SelectedValue.ToString().Equals("0"))
            {
                ddl_bus_puntored.Items.Clear(); ddl_bus_puntored.Items.Insert(0, new ListItem(_TODOS, "0"));

                Cargar_DLL_Taller();
            }
            else
            {
                oMaestroTallerBE.coddpto = ddl_bus_departamento.SelectedValue.ToString().Trim();
                oMaestroTallerBE.codprov = ddl_bus_provincia.SelectedValue.ToString().Trim();
                oMaestroTallerBE.coddist = ddl_bus_distrito.SelectedValue.ToString().Trim();

                oCombosBEList = new CombosBEList();
                oCombosBEList = oMaestroTallerBL.GETListarPuntoRed(oMaestroTallerBE, Profile.Usuario.Nid_usuario);

                cargarComboBox(ddl_bus_puntored, oCombosBEList, "DES", "IntID", _TODOS);
                if (oCombosBEList.Count == 1) ddl_bus_puntored_SelectedIndexChanged(this, null);
            }
        }
        catch (Exception)
        {
        }
    }
    protected void ddl_bus_puntored_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Cargar_DLL_Taller();
        }
        catch (Exception)
        {
        }
    }
    protected void ddl_bus_marca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_bus_marca.SelectedValue.ToString().Equals("0"))
            {
                ddl_bus_modvehiculo.Items.Clear();
                ddl_bus_modvehiculo.Items.Insert(0, new ListItem(_TODOS, "0"));
            }
            else
            {
                oMaestroVehiculoBE.nid_marca = int.Parse(ddl_bus_marca.SelectedValue.ToString().Trim());

                oCombosBEList = new CombosBEList();
                oCombosBEList = oMaestroVehiculoBL.GETListarModelosXMarca(oMaestroVehiculoBE, Profile.Usuario.Nid_usuario);

                ddl_bus_modvehiculo.Items.Clear();
                foreach (ComboBE oEntidad in oCombosBEList) { ddl_bus_modvehiculo.Items.Add(new ListItem(oEntidad.DES, oEntidad.ID)); }
                ddl_bus_modvehiculo.Items.Insert(0, new ListItem(_TODOS, "0"));

            }
        }
        catch (Exception)
        {
        }
    }
    protected void ddl_bus_tipodoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        string oDNI = ConfigurationManager.AppSettings["TIPODOCDNI"].ToString();
        string oRUC = ConfigurationManager.AppSettings["TIPODOCRUC"].ToString();

        Int32 intMAX_DNI = oParametros.SRC_CodPais.Equals(1) ? 8 : 9;
        Int32 intMAX_RUC = oParametros.SRC_CodPais.Equals(1) ? 11 : 11;

        string strFN_DNI = oParametros.SRC_CodPais.Equals(1) ? "return SoloNumeros(event)" : "return SoloLetrasNumeros(event)";
        string strFN_RUC = oParametros.SRC_CodPais.Equals(1) ? "return SoloNumeros(event)" : "return SoloNumeros(event)";

        txt_bus_nrodoc.Text = string.Empty;
        txt_bus_nrodoc.Enabled = !ddl_bus_tipodoc.SelectedIndex.Equals(0);
        txt_bus_nrodoc.MaxLength = ddl_bus_tipodoc.SelectedValue.ToString().Equals(oDNI) ? intMAX_DNI : (ddl_bus_tipodoc.SelectedValue.ToString().Equals(oRUC) ? intMAX_RUC : intMAX_DNI);
        txt_bus_nrodoc.Attributes.Add("onkeypress", ddl_bus_tipodoc.SelectedValue.ToString().Equals(oDNI) ? strFN_DNI : (ddl_bus_tipodoc.SelectedValue.ToString().Equals(oRUC) ? strFN_RUC : strFN_DNI));
    }

    protected void gv_admcitas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        oAdminCitaBE = new AdminCitaBE();
        oAdminCitaBE = (AdminCitaBE)Session["AdminCitaBE_FILTROS"];
        if (oAdminCitaBE != null)
        {
            oAdminCitaBE.paginacion = e.NewPageIndex;
            Session["AdminCitaBE_FILTROS"] = oAdminCitaBE;
        }

        gv_admcitas.PageIndex = e.NewPageIndex;
        gv_admcitas.DataSource = (AdminCitaBEList)(ViewState["oAdminCitaBEList"]);
        gv_admcitas.DataBind();
    }
    protected void gv_admcitas_Sorting(object sender, GridViewSortEventArgs e)
    {
        AdminCitaBEList oAdminCitaBEList = (AdminCitaBEList)(ViewState["oAdminCitaBEList"]);
        SortDirection indOrden = (SortDirection)(ViewState["ordenGridTalleres"]);

        if (oAdminCitaBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oAdminCitaBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                ViewState["ordenGridTalleres"] = SortDirection.Descending;
            }
            else
            {
                oAdminCitaBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                ViewState["ordenGridTalleres"] = SortDirection.Ascending;
            }
        }
        gv_admcitas.DataSource = oAdminCitaBEList;
        gv_admcitas.DataBind();
        ViewState["oAdminCitaBEList"] = oAdminCitaBEList;
    }
    protected void gv_admcitas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux = 1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = gv_admcitas.DataKeys[e.Row.RowIndex];
            if (dataKey.Values["grid_nid_cita"] == null) aux = 0;
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }

            //------------------------------------------------------------------
            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["ondblclick"] = String.Format("javascript: Fc_verDetalleCita();");
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                       , hf_DETALLE.ClientID,
                                       dataKey.Values["grid_nid_cita"].ToString() + "=" +
                                       dataKey.Values["grid_nid_estado"].ToString() + "=" +
                                       dataKey.Values["grid_IndPendiente"].ToString() + "=" +
                                       dataKey.Values["grid_no_dias_validos"].ToString() + "=" +
                                       dataKey.Values["grid_nid_servicioCita"].ToString() + "=" +
                                       dataKey.Values["grid_nid_modelo"].ToString());
        }
    }

    #endregion

    //********************************************************************************************************************************************************************************************

    #region "---------- EMAILS: CLIENTE-ASESOR // SMS: CLIENTE ------------------"

    private void EnviarCorreo_Cliente(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        CorreoElectronico oCorreoElectronico = new CorreoElectronico(Server.MapPath("~/"));

        bool flEnvio = oCorreoElectronico.EnviarMensajeCorreo(oDatos, oTipoCita, Parametros.PERSONA.CLIENTE);
        if (!flEnvio)
        {
            //SRC_MsgInformacion("Error al enviar Correo-Cliente");
        }
    }
    private void EnviarCorreo_Asesor(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        CorreoElectronico oCorreoElectronico = new CorreoElectronico(Server.MapPath("~/"));

        bool flEnvio = oCorreoElectronico.EnviarMensajeCorreo(oDatos, oTipoCita, Parametros.PERSONA.ASESOR);
        if (!flEnvio)
        {
            //SRC_MsgInformacion("Error al enviar Correo-Asesor"); ;
        }
    }
    private void EnviarCorreo_CallCenter(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        CorreoElectronico oCorreoElectronico = new CorreoElectronico(Server.MapPath("~/"));

        bool flEnvio = oCorreoElectronico.EnviarMensajeCorreo(oDatos, oTipoCita, Parametros.PERSONA.CALL_CENTER);
        if (!flEnvio)
        {
            //SRC_MsgInformacion("Error al enviar Correo-CallCenter");
        }
    }

    #endregion

    //********************************************************************************************************************************************************************************************

    #region "------------- REPROGRAMAR CITA -------------------"

    private void setIconoGrilla(Int32 intFila, DateTime dtHora, string strIcon, Int32 intCE)
    {
        Int32 intCol = 0;

        foreach (DataControlField oColumn in gvHorarioReserva.Columns)
        {
            if ((oColumn.GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField")))
            {
                DateTime dtHoraGrilla = Convert.ToDateTime((((ButtonField)oColumn).DataTextField.Substring(((ButtonField)oColumn).DataTextField.IndexOf("_") + 1).Insert(2, ":")));
                DateTime dtHoraSig = dtHoraGrilla.AddMinutes(Convert.ToInt32(hf_INTERVALO_TALLER.Value.ToString()));

                if ((dtHora >= dtHoraGrilla & dtHora < dtHoraSig))
                {
                    DataControlFieldCell CTR = (DataControlFieldCell)gvHorarioReserva.Rows[intFila].Controls[intCol + (dtHora == dtHoraGrilla ? 0 : 1)];

                    foreach (Control _ctr in CTR.Controls)
                    {
                        if (_ctr.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                        {
                            ((ImageButton)_ctr).ImageUrl = strIcon;
                            if (intCE > 0) ((ImageButton)_ctr).ToolTip = intCE.ToString();
                            return;
                        }
                    }
                }
            }

            intCol += 1;

        }
    }

    private void cargarComboHorarioTaller(DropDownList ddlHora, DateTime dtHoraIni, DateTime dtHoraFin, Int32 intInterv, string strInd)
    {
        ddlHora.AutoPostBack = false;
        ddlHora.Items.Clear();

        while (dtHoraIni <= dtHoraFin)
        {
            ddlHora.Items.Add(new ListItem(FormatoHora(dtHoraIni.ToString("HH:mm")), dtHoraIni.ToString("HH:mm")));
            dtHoraIni = dtHoraIni.AddMinutes(intInterv);
        }

        ddlHora.SelectedIndex = (strInd.Equals("HI") ? 0 : ddlHora.Items.Count - 1);
        ddlHora.AutoPostBack = true;
    }

    private string FormatoHora(string strHora)
    {
        string strHF = strHora;
        try
        {
            string strHoraF = Convert.ToDateTime(strHora).ToString("hh:mm");
            int strHoraF1 = Convert.ToInt32(strHora.Replace(":", ""));
            strHF = strHoraF + ((strHoraF1 < 1200) ? " A.M." : " P.M.");
        }
        catch
        {
            strHF = strHora;
        }
        return strHF;

    }
    private string GetFechaLarga(DateTime dtFecha)
    {
        return dtFecha.ToString("D", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
    }
    private DateTime GetFecha(string strFecha)
    {
        DateTime strFechaR = DateTime.MinValue;

        char strSepara = Convert.ToChar(CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);

        if (!string.IsNullOrEmpty(strFecha))
        {
            strFechaR = new DateTime(Convert.ToInt32(strFecha.Split(strSepara)[2]), Convert.ToInt16(strFecha.Split(strSepara)[1]), Convert.ToInt16(strFecha.Split(strSepara)[0]));
        }
        return strFechaR;
    }
    private DateTime SRC_FECHA_HABIL()
    {
        CitasBL oCitasBL = new CitasBL();

        oCitasBE = new CitasBE();

        Int32 _ID_TALLER = Convert.ToInt32(hf_ID_TALLER.Value.ToString());
        Int32 _INTERVALO = 0;
        Int32 _ID_SERVICIO = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[4].ToString());
        Int32 _ID_MODELO = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[5].ToString());


        string _TALLER = string.Empty;

        DateTime dFechaIni = Convert.ToDateTime(GetFechaMinReserva());
        DateTime dFechaFin = Convert.ToDateTime(GetFechaMaxReserva());

        DateTime dHoraIni_T = DateTime.MinValue;
        DateTime dHoraFin_T = DateTime.MinValue;

        DateTime _dHoraIni_T = DateTime.MinValue;
        DateTime _dHoraFin_T = DateTime.MinValue;

        DateTime dHoraIni_A = DateTime.MinValue;
        DateTime dHoraFin_A = DateTime.MinValue;

        //CitasBEList _lstTalleres;
        CitasBEList _lstAsesores;
        CitasBEList _lstCitas;
        CitasBEList _lstTalleresHE;

        CitasBEList lstTalleres;
        CitasBEList lstAsesores;
        CitasBEList lstCitas;
        CitasBEList lstTalleresHE;

        string strTotalHE = string.Empty;

        DateTime dtHEITaller;
        DateTime dtHEFTaller;

        DateTime dtHoraI_ddl = DateTime.MaxValue;
        DateTime dtHoraF_ddl = DateTime.MinValue;

        Int32 hay1 = 0;
        Int32 hay2 = 0;


        ddlHoraInicio2.Items.Clear();
        ddlHoraFin2.Items.Clear();
        ddlHoraInicio2.Enabled = false;
        ddlHoraFin2.Enabled = false;

        //bool swHorasT = false;


        try
        {
        //Recorremos Fecha por Fecha 
        //------------------------------------------------------

        Continue_While_1:
            while (dFechaIni <= dFechaFin)
            {
                //================================

                oCitasBE.nid_Servicio = _ID_SERVICIO;
                oCitasBE.nid_modelo = _ID_MODELO;
                oCitasBE.coddpto = "0";
                oCitasBE.codprov = "0";
                oCitasBE.coddis = "0";
                oCitasBE.nid_ubica = Convert.ToInt32((ddlPuntoRed.SelectedIndex <= 0 ? "0" : ddlPuntoRed.SelectedValue.ToString()));
                oCitasBE.nid_taller = _ID_TALLER;
                oCitasBE.fe_atencion = dFechaIni;
                oCitasBE.dd_atencion = getDiaSemana(dFechaIni);
                oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

                lstTalleres = oCitasBL.ListarTalleresDisponibles_PorFecha(oCitasBE);//   1-Listado todos Talleres
                ViewState["lstTalleres"] = lstTalleres;

                if (lstTalleres.Count == 0)
                {
                    dFechaIni = dFechaIni.AddDays(+1);
                    goto Continue_While_1;
                }

                _lstTalleresHE = oCitasBL.ListarHorarioExcepcional_Talleres(oCitasBE);// 2-Listado Horario Excepcionales
                ViewState["lstTalleresHE"] = _lstTalleresHE;

                _lstAsesores = oCitasBL.ListarAsesoresDisponibles_PorFecha(oCitasBE);//  3-Listado Asesores Talleres
                ViewState["lstAsesores"] = _lstAsesores;

                _lstCitas = oCitasBL.ListarCitasAsesores(oCitasBE);//                    4-Listado CitasAsesores Talleres
                ViewState["lstCitas"] = _lstCitas;

                //===========================================

                foreach (CitasBE oTaller in lstTalleres)
                {
                    _ID_TALLER = Convert.ToInt32(oTaller.nid_taller);
                    _INTERVALO = Convert.ToInt32(oTaller.qt_intervalo_atenc);

                    _dHoraIni_T = Convert.ToDateTime(oTaller.ho_inicio_t);
                    _dHoraFin_T = Convert.ToDateTime(oTaller.ho_fin_t);


                    //=================================================================================
                    //> Validaciones
                    //=================================================================================
                    if (oTaller.qt_cantidad_t <= 0) continue;//Capacidad Atencion Taller
                    if (oParametros.SRC_Pais.Equals(1))//07092012 -> SOLO PERU
                    {
                        if (oTaller.qt_cantidad_m <= 0) continue;//Capacidad Atencion Taller y Modelo
                    }
                    if (oTaller.nid_dia_exceptuado_t == 1) continue;//Dia Exceptuado


                    //=================================================================================
                    //> Horas Excepcional del Taller
                    //=================================================================================
                    // FILTRAR HORARIO EXCEPCIONAL
                    //-----------------------------

                    lstTalleresHE = new CitasBEList();

                    hay1 = 0;
                    hay2 = 0;
                    foreach (CitasBE oEntidad in _lstTalleresHE)
                    {
                        hay1 = 0;
                        if (oTaller.nid_taller.Equals(oEntidad.nid_taller))
                        {
                            hay1 = 1; hay2 = 1;
                            lstTalleresHE.Add(oEntidad);
                        }
                        if ((hay1 == 0) && (hay2 == 1))
                            break;
                    }

                    //-----------------------------------------------------------------------
                    strTotalHE = string.Empty;
                    foreach (CitasBE oHET in lstTalleresHE)
                    {
                        if (!string.IsNullOrEmpty(oHET.ho_rango1)) strTotalHE += oHET.ho_rango1 + "-";
                        if (!string.IsNullOrEmpty(oHET.ho_rango2)) strTotalHE += oHET.ho_rango2 + "-";
                        if (!string.IsNullOrEmpty(oHET.ho_rango3)) strTotalHE += oHET.ho_rango3 + "-";
                    }
                    if (!string.IsNullOrEmpty(strTotalHE))
                    {
                        strTotalHE = strTotalHE.Substring(0, strTotalHE.Length - 1);
                    }

                    //=================================================================================
                    //> Listado de Asesores 
                    //=================================================================================
                    // FILTRAR ASESORES - TALLER
                    //-----------------------------------

                    lstAsesores = new CitasBEList();// 2

                    hay1 = 0;
                    hay2 = 0;
                    foreach (CitasBE oEntidad in _lstAsesores)
                    {
                        hay1 = 0;
                        if (oTaller.nid_taller.Equals(oEntidad.nid_taller))
                        {
                            hay1 = 1; hay2 = 1;
                            //lstAsesores.Add(oEntidad);
                            if (oEntidad.qt_cantidad_a > 0) lstAsesores.Add(oEntidad);
                        }
                        if ((hay1 == 0) && (hay2 == 1))
                            break;
                    }

                    //--------------------------------------------------------------

                    DateTime odHIA = _dHoraIni_T;
                    DateTime odHFA = _dHoraFin_T;

                    foreach (CitasBE oAsesor in lstAsesores)//==========================>>>>
                    {
                        //=================================================================================
                        //> Listar Citas Asesores 
                        //=================================================================================
                        // FILTRAR CITAS - TALLER - ASESOR
                        //-----------------------------------

                        lstCitas = new CitasBEList();

                        hay1 = 0;
                        hay2 = 0;
                        foreach (CitasBE oEntidad in _lstCitas)
                        {
                            hay1 = 0;
                            if (oTaller.nid_taller.Equals(oEntidad.nid_taller) && oAsesor.nid_asesor.Equals(oEntidad.nid_asesor))
                            {
                                hay1 = 1; hay2 = 1;
                                lstCitas.Add(oEntidad);
                            }
                            if ((hay1 == 0) && (hay2 == 1))
                                break;
                        }

                        //-------------------------------------------------------------
                        //Recorrer cada horario del Asesor
                        foreach (string strHorario in oAsesor.horario_asesor.Split('|'))
                        {
                            dHoraIni_A = Convert.ToDateTime(strHorario.Split('-').GetValue(0).ToString());
                            dHoraFin_A = Convert.ToDateTime(strHorario.Split('-').GetValue(1).ToString());

                            if (dHoraIni_A < odHIA) dHoraIni_A = odHIA;
                            if (dHoraFin_A > odHFA) dHoraFin_A = odHFA;

                            //----------------------------------------------------
                            //> Get menor HI y mayor HF de Horarios disponibles
                            //---------------------------------------------------- 
                            if (dHoraFin_A > dtHoraF_ddl) dtHoraF_ddl = dHoraFin_A;
                            if (dHoraIni_A < dtHoraI_ddl) dtHoraI_ddl = dHoraIni_A;

                            //swHorasT = true;

                            Continue_While_2:
                            while (dHoraIni_A < dHoraFin_A)
                            {
                                foreach (CitasBE oCita in lstCitas)
                                {
                                    DateTime dtHActual = dHoraIni_A;

                                    DateTime dtHICita = Convert.ToDateTime(oCita.ho_inicio_c);
                                    DateTime dtHFCita = Convert.ToDateTime(oCita.ho_fin_c);

                                    if (dtHActual >= dtHICita & dtHActual < dtHFCita)
                                    {
                                        dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                                        goto Continue_While_2; //> Es una Cita ya reservada
                                    }
                                }

                                // > Se verifica que la hora del Asesor no sea una hora excepcional

                                if (!string.IsNullOrEmpty(strTotalHE))
                                {
                                    foreach (string _strRangoHE in strTotalHE.Split('-'))
                                    {
                                        dtHEITaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(0));// Hora Inicial HET
                                        dtHEFTaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(1));// Hora Final   HET

                                        //> Si es una hora excepcionl
                                        if (dHoraIni_A >= dtHEITaller & dHoraIni_A < dtHEFTaller)
                                        {
                                            dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                                            goto Continue_While_2; //> Es Horario Excepcional
                                        }
                                    }
                                }

                                //> FECHA ENCONTADA
                                //-------------------------
                                return dFechaIni;
                            }
                        }
                    }
                }

                dFechaIni = dFechaIni.AddDays(+1);
            }
        }
        catch (Exception ex)
        {
            string strError = ex.Message;
        }

        return dFechaFin;
    }

    protected void btn_Hidden_1_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsDate(txtFecha.Text))
            {
                DateTime dtFecha = Convert.ToDateTime(txtFecha.Text);
                DateTime dtFechaMin = Convert.ToDateTime(GetFechaMinReserva());
                DateTime dtFechaMax = Convert.ToDateTime(GetFechaMaxReserva());

                lblSeleccion1.Text = "";
                lblSeleccion2.Text = "";

                if (DateDiff(DateInterval.Day, dtFechaMin, dtFecha) < 0)
                {
                    txtFecha.Text = Convert.ToString(hfFecha.Value);
                    SRC_MsgInformacion("La fecha mínima para una reservaciòn es el " + GetFechaMinReserva());
                }
                else if (DateDiff(DateInterval.Day, dtFecha, dtFechaMax) < 0)
                {
                    txtFecha.Text = Convert.ToString(hfFecha.Value);
                    SRC_MsgInformacion("La fecha máxima para una reservaciòn es el " + GetFechaMaxReserva());
                }
                else
                {
                    hfFecha.Value = txtFecha.Text;
                    if (ddlHoraInicio1.Items.Count > 0)
                    {
                        ddlHoraInicio1.SelectedIndex = 0;
                        ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.Count - 1;
                    }

                    ConsultarHorarioReserva(); //--> GRID 1
                }

            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }

    }
    protected void btn_Hidden_2_Click(object sender, EventArgs e)
    {
        DateTime dtFechaMin = Convert.ToDateTime(GetFechaMinReserva());
        DateTime dtFechaMax = Convert.ToDateTime(GetFechaMaxReserva());

        DateTime dtFechaIni;
        DateTime dtFechaFin;

        if (IsDate(txtFechaIni.Text))
        {
            dtFechaIni = Convert.ToDateTime(txtFechaIni.Text);

            lblSeleccion1.Text = "";
            lblSeleccion2.Text = "";

            if (DateDiff(DateInterval.Day, dtFechaMin, dtFechaIni) < 0)
            {
                txtFechaIni.Text = Convert.ToString(hfFechaIni.Value);
                txtFechaFin.Text = Convert.ToString(hfFechaFin.Value);
                //SRC_MsgInformacion("La fecha mínima para una reservaciòn es el " + GetFechaMinReserva());//@004
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La fecha mínima para una reservaciòn es el " + GetFechaMinReserva()), false);//@004
            }
            else if (IsDate(txtFechaFin.Text))
            {
                dtFechaFin = Convert.ToDateTime(txtFechaFin.Text);

                if (DateDiff(DateInterval.Day, dtFechaMin, dtFechaFin) < 0)
                {
                    txtFechaIni.Text = Convert.ToString(hfFechaIni.Value);
                    txtFechaFin.Text = Convert.ToString(hfFechaFin.Value);
                    //SRC_MsgInformacion("La fecha mínima para una reservaciòn es el " + GetFechaMinReserva());//@004
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La fecha mínima para una reservaciòn es el " + GetFechaMinReserva()), false);//@004
                }
                else if (DateDiff(DateInterval.Day, dtFechaIni, dtFechaFin) < 0)
                {
                    txtFechaIni.Text = Convert.ToString(hfFechaIni.Value);
                    txtFechaFin.Text = Convert.ToString(hfFechaFin.Value);
                    //SRC_MsgInformacion("La fecha inicial no debe ser mayor que la fecha final");//@004
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La fecha inicial no debe ser mayor que la fecha final"), false);//@004
                }
                else if (DateDiff(DateInterval.Day, dtFechaFin, dtFechaMax) < 0)
                {
                    txtFechaFin.Text = Convert.ToString(hfFechaFin.Value);
                    //SRC_MsgInformacion("La fecha máxima para una reservaciòn es el " + GetFechaMaxReserva());//@004
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La fecha máxima para una reservaciòn es el " + GetFechaMaxReserva()), false);//@004
                }
                else
                {
                    hfFechaIni.Value = txtFechaIni.Text;
                    hfFechaFin.Value = txtFechaFin.Text;
                    if (ddlHoraInicio2.Items.Count > 0)
                    {
                        ddlHoraInicio2.SelectedIndex = 0;
                        ddlHoraFin2.SelectedIndex = ddlHoraFin2.Items.Count - 1;
                    }
                    MostrarHorarioDisponible();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>fc_mostrarFiltro();fc_filtrarGrilla()</script>", false);//@004
                }
            }
            else if (DateDiff(DateInterval.Day, dtFechaIni, dtFechaMax) < 0)
            {
                txtFechaIni.Text = Convert.ToString(hfFechaIni.Value);
                //SRC_MsgInformacion("La fecha máxima para una reservaciòn es el " + GetFechaMaxReserva());//004
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La fecha máxima para una reservaciòn es el " + GetFechaMaxReserva()), false);//@004
            }
            else
            {
                hfFechaIni.Value = txtFechaIni.Text;

                if (ddlHoraInicio2.Items.Count > 0)
                {
                    ddlHoraInicio2.SelectedIndex = 0;
                    ddlHoraFin2.SelectedIndex = ddlHoraFin2.Items.Count - 1;
                }
            }
        }
    }

    protected void imbFecAnt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (IsDate(txtFecha.Text))
            {
                lblSeleccion1.Text = "";
                lblSeleccion2.Text = "";

                if (!GetFechaMinReserva().Equals(txtFecha.Text))
                {
                    txtFecha.Text = GetFecha(txtFecha.Text).AddDays(-1).ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                    hfFecha.Value = txtFecha.Text;

                    if (ddlHoraInicio1.Items.Count > 0)
                    {
                        ddlHoraInicio1.SelectedIndex = 0;
                        ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.Count - 1;
                    }

                    ConsultarHorarioReserva();  //---> GRID 1
                }
                else
                {
                    SRC_MsgInformacion("La fecha mínima para una reservaciòn es el " + txtFecha.Text);
                }
            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }
    protected void imbFecSgte_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (IsDate(txtFecha.Text))
            {
                lblSeleccion1.Text = "";
                lblSeleccion2.Text = "";

                DateTime oFechaMax = Convert.ToDateTime(GetFechaMaxReserva());
                DateTime oFechaSig = Convert.ToDateTime(txtFecha.Text);

                if (DateDiff(DateInterval.Day, oFechaSig, oFechaMax) > 0)
                {
                    txtFecha.Text = GetFecha(txtFecha.Text).AddDays(+1).ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                    hfFecha.Value = txtFecha.Text;

                    if (ddlHoraInicio1.Items.Count > 0)
                    {
                        ddlHoraInicio1.SelectedIndex = 0;
                        ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.Count - 1;
                    }

                    ConsultarHorarioReserva();  //---> GRID 1
                }
                else
                {
                    SRC_MsgInformacion("La fecha máxima para una reservaciòn es el " + txtFechaFin.Text);
                }
            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }

    protected void ddlHoraInicio1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtHoraIni = Convert.ToDateTime(ddlHoraInicio1.SelectedValue);
        DateTime dtHoraFin = Convert.ToDateTime(ddlHoraFin1.SelectedValue);

        if (dtHoraIni >= dtHoraFin)
        {
            SRC_MsgInformacion("La hora inicial debe ser menor que la hora final.");
            ddlHoraInicio1.SelectedIndex = ddlHoraInicio1.Items.IndexOf(ddlHoraInicio1.Items.FindByValue(hfHoraIni1.Value.ToString()));
        }
        else
        {
            hfHoraIni1.Value = ddlHoraInicio1.Text;

            //CAMBIAR LA GRILLA
            ActualizarRangoHorario();
        }
    }
    protected void ddlHoraFin1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtHoraIni = Convert.ToDateTime(ddlHoraInicio1.SelectedValue.ToString());
        DateTime dtHoraFin = Convert.ToDateTime(ddlHoraFin1.SelectedValue.ToString());

        if (dtHoraIni >= dtHoraFin)
        {

            ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.IndexOf(ddlHoraFin1.Items.FindByValue(hfHoraFin1.Value.ToString()));
        }
        else
        {
            hfHoraFin1.Value = ddlHoraFin1.Text;

            //CAMBIAR LA GRILLA
            ActualizarRangoHorario();

        }
    }
    protected void ddlHoraInicio2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtHoraIni = Convert.ToDateTime(ddlHoraInicio2.SelectedValue);
        DateTime dtHoraFin = Convert.ToDateTime(ddlHoraFin2.SelectedValue);

        if (dtHoraIni > dtHoraFin)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La hora inicial debe ser menor que la hora final."), false);//@004
            ddlHoraInicio2.SelectedIndex = ddlHoraInicio2.Items.IndexOf(ddlHoraInicio2.Items.FindByValue(hfHoraIni2.Value.ToString()));
        }
        else
        {
            hfHoraIni2.Value = ddlHoraInicio2.Text;
            ActualizarRangoHorarioDisponible();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>fc_mostrarFiltro();fc_filtrarGrilla()</script>", false);//@004
        }
    }
    protected void ddlHoraFin2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtHoraIni = Convert.ToDateTime(ddlHoraInicio2.SelectedValue);
        DateTime dtHoraFin = Convert.ToDateTime(ddlHoraFin2.SelectedValue);

        if (dtHoraIni > dtHoraFin)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La hora final debe ser menor que la hora final."), false);//@004
            ddlHoraFin2.SelectedIndex = ddlHoraFin2.Items.IndexOf(ddlHoraFin2.Items.FindByValue(hfHoraFin2.Value.ToString()));
        }
        else
        {
            hfHoraFin2.Value = ddlHoraFin2.Text;

            //CAMBIAR LA GRILLA
            ActualizarRangoHorarioDisponible();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>fc_mostrarFiltro();fc_filtrarGrilla()</script>", false);//@004
        }
    }

    protected void lnkVerHorariosProximos_Click(object sender, EventArgs e)
    {
        if (txtFecha.Text.Trim().Equals("")) return;

        lblSeleccion2.Text = lblSeleccion1.Text;

        if (!txtFechaIni.Text.Trim().Equals(txtFecha.Text.Trim()))
        {
            hfFechaIni.Value = txtFecha.Text;// hfFecha.Value.ToString();
            hfFechaFin.Value = txtFecha.Text; //hfFecha.Value.ToString();

            txtFechaIni.Text = hfFechaIni.Value.ToString();
            txtFechaFin.Text = hfFechaIni.Value.ToString();

            MostrarHorarioDisponible();
        }

        //------------
        //Horas
        //--------

        if (ddlHoraInicio1.Items.Count > 0)
        {
            DateTime dtHoraIni1 = Convert.ToDateTime(ddlHoraInicio1.SelectedValue);
            DateTime dtHoraFin1 = Convert.ToDateTime(ddlHoraFin1.SelectedValue);

            DateTime dtHoraIni2 = Convert.ToDateTime(ddlHoraInicio2.SelectedValue);
            DateTime dtHoraFin2 = Convert.ToDateTime(ddlHoraFin2.SelectedValue);

            DateTime dtHI1 = Convert.ToDateTime(ddlHoraInicio1.Items[0].Value);
            DateTime dtHF1 = Convert.ToDateTime(ddlHoraFin1.Items[ddlHoraFin1.Items.Count - 1].Value);

            DateTime dtHI2 = Convert.ToDateTime(ddlHoraInicio2.Items[0].Value);
            DateTime dtHF2 = Convert.ToDateTime(ddlHoraFin2.Items[ddlHoraFin2.Items.Count - 1].Value);

            //----------

            if (dtHI2 > dtHoraIni1)
            {
                ddlHoraInicio2.SelectedIndex = 0;
            }
            else
            {
                ddlHoraInicio2.SelectedIndex = ddlHoraInicio2.Items.IndexOf(ddlHoraInicio2.Items.FindByValue(ddlHoraInicio1.SelectedItem.Value));
            }
            if (dtHoraFin1 > dtHF2)
            {
                ddlHoraFin2.SelectedIndex = ddlHoraFin2.Items.Count - 1;
            }
            else
            {
                ddlHoraFin2.SelectedIndex = ddlHoraFin2.Items.IndexOf(ddlHoraFin2.Items.FindByValue(ddlHoraFin1.SelectedItem.Value));
            }

            ActualizarRangoHorarioDisponible();
        }
        PanelUno.Visible = false;
        Paneldos.Visible = true;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>fc_mostrarFiltro()</script>", false);//@004


    }
    protected void lnkVerHorariosActuales_Click(object sender, EventArgs e)
    {
        if (txtFecha.Text.Trim().Equals(""))
            return;

        lblSeleccion1.Text = lblSeleccion2.Text;

        if (!txtFechaIni.Text.Trim().Equals(txtFecha.Text.Trim()))
        {
            hfFecha.Value = Convert.ToString(hfFechaIni.Value);

            txtFecha.Text = Convert.ToString(hfFecha.Value);

            ConsultarHorarioReserva();
        }

        pnlLeyenda.Visible = (!(gvHorarioReserva.Rows.Count == 0));

        //Horas
        //----------
        if (ddlHoraInicio2.Items.Count > 0)
        {
            if (ddlHoraInicio1.Items.Count > 0)
            {

                //Horas
                //----------
                DateTime dtHoraIni1 = Convert.ToDateTime(ddlHoraInicio1.SelectedValue);
                DateTime dtHoraFin1 = Convert.ToDateTime(ddlHoraFin1.SelectedValue);
                DateTime dtHoraIni2 = Convert.ToDateTime(ddlHoraInicio2.SelectedValue);
                DateTime dtHoraFin2 = Convert.ToDateTime(ddlHoraFin2.SelectedValue);

                DateTime dtHI1 = Convert.ToDateTime(ddlHoraInicio1.Items[0].Value);
                DateTime dtHF1 = Convert.ToDateTime(ddlHoraFin1.Items[ddlHoraFin1.Items.Count - 1].Value);

                //Horas
                //----------
                if (dtHI1 > dtHoraIni2)
                {
                    ddlHoraInicio1.SelectedIndex = 0;
                }
                else
                {
                    ddlHoraInicio1.SelectedIndex = ddlHoraInicio1.Items.IndexOf(ddlHoraInicio1.Items.FindByValue(ddlHoraInicio2.SelectedItem.Value));
                }
                if (dtHoraFin2 > dtHF1)
                {
                    ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.Count - 1;
                }
                else
                {
                    ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.IndexOf(ddlHoraFin1.Items.FindByValue(ddlHoraFin2.SelectedItem.Value));
                }

                ActualizarRangoHorario();

            }
        }

        PanelUno.Visible = true;
        Paneldos.Visible = false;
    }

    protected void btnCerrarReprogramar_Click(object sender, ImageClickEventArgs e)
    {
        MpeReprogramacion.Hide();
    }

    protected void gvHorarioReserva_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 intFila = Convert.ToInt32(e.CommandArgument);
        Int32 intCol = 4 + Convert.ToInt32(e.CommandName.ToString().Substring(e.CommandName.ToString().IndexOf("_") + 1, (e.CommandName.LastIndexOf("_") - e.CommandName.IndexOf("_") - 1)));


        if (gvHorarioReserva.Rows[intFila].BackColor == System.Drawing.Color.FromArgb(178, 213, 247))
            return;

        string strIMG = string.Empty;
        string strHora = string.Empty;

        //----------------------------
        ImageButton imbCelda = null;
        DataControlFieldCell CTR = ((DataControlFieldCell)gvHorarioReserva.Rows[intFila].Controls[intCol]);
        foreach (Control _ctr in CTR.Controls)
        {
            if (_ctr.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
            {
                imbCelda = (ImageButton)_ctr;
                break;
            }
        }
        //----------------------------

        DataKey oDatos = gvHorarioReserva.DataKeys[intFila];

        strIMG = imbCelda.ImageUrl;

        strHora = e.CommandName.Substring(e.CommandName.LastIndexOf("_") + 1);

        if (strIMG == imgURL_HORA_VACIA)
        {
            //-->
        }
        else if (strIMG == imgURL_HORA_RESERVADA)
        {
            //-->

        }
        else if (strIMG == imgURL_HORA_LIBRE)
        {

            ImageButton imbTemp;

            for (Int32 f = 0; f <= gvHorarioDisponible.Rows.Count - 1; f++)
            {
                DataControlFieldCell CTR1 = (DataControlFieldCell)gvHorarioDisponible.Rows[f].Controls[6];
                foreach (Control _ctr in CTR1.Controls)
                {
                    if (_ctr.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                    {
                        imbTemp = (ImageButton)_ctr;
                        if (imbTemp.ImageUrl.ToUpper() == imgURL_HORA_SEPARADA.ToUpper())
                        {
                            imbTemp.ImageUrl = imgURL_HORA_LIBRE;
                            break;
                        }
                    }
                }
            }

            //----------------

            strHora = e.CommandName.Substring(e.CommandName.LastIndexOf("_") + 1);


            for (Int32 f = 0; f <= gvHorarioReserva.Rows.Count - 1; f++)
            {
                for (Int32 c = 4; c <= gvHorarioReserva.Columns.Count - 1; c++)
                {
                    ImageButton imgB1 = getControl(f, c);
                    if (imgB1.ImageUrl == imgURL_HORA_SEPARADA)
                    {
                        imgB1.ImageUrl = imgURL_HORA_LIBRE;
                        break;
                    }
                }
            }

            imbCelda.ImageUrl = imgURL_HORA_SEPARADA;

            hf_DATOS_CITA.Text = oDatos.Values["ID_ASESOR"] + "|" + oDatos.Values["NOM_ASESOR"] + "|" + gvHorarioReserva.Rows[intFila].Cells[0].Text + "|" + txtFecha.Text + "|" + strHora.Insert(2, ":") + "|" + oDatos.Values["TELEFONO"].ToString().Replace('|', '/') + "|" + oDatos.Values["EMAIL"].ToString();
            lblSeleccion1.Text = "Selección de Reserva -  " + (oParametros.GetValor(Parametros.PARM._10).Equals("0") ? "Asesor de Servicio " + oDatos.Values["ID_ASESOR"].ToString() : oDatos.Values["NOM_ASESOR"].ToString()) + " - " + GetFechaLarga(GetFecha(txtFecha.Text)) + " - " + FormatoHora(strHora.Insert(2, ":"));

            lblSeleccion2.Text = lblSeleccion1.Text;
        }
        else
        {
            lblSeleccion1.Text = "";
            imbCelda.ImageUrl = imgURL_HORA_LIBRE;
        }
    }
    protected void gvHorarioDisponible_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 fila = Convert.ToInt32(e.CommandArgument);
        Int32 Col = 6;


        if (gvHorarioDisponible.Rows[fila].BackColor == System.Drawing.Color.FromArgb(178, 213, 247))
        {
            return;
        }

        string strIMG = string.Empty;
        string strHora = string.Empty;

        ImageButton imbCelda = null;
        ImageButton imbTemp = null;


        DataControlFieldCell CTR = (DataControlFieldCell)gvHorarioDisponible.Rows[fila].Controls[Col];
        foreach (Control _ctr in CTR.Controls)
        {
            if (_ctr.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
            {
                imbCelda = (ImageButton)_ctr;
                strIMG = imbCelda.ImageUrl.ToUpper();
                break;
            }
        }


        if (strIMG == imgURL_HORA_VACIA.ToUpper())
        {
            //-->
        }
        else if (strIMG == imgURL_HORA_RESERVADA.ToUpper())
        {
            //-->
        }
        else if (strIMG == imgURL_HORA_LIBRE.ToUpper())
        {
            //'> Quitar la seleccion de horario de Reserva

            for (Int32 f = 0; f <= gvHorarioReserva.Rows.Count - 1; f++)
            {
                for (Int32 c = 4; c <= gvHorarioReserva.Columns.Count - 1; c++)
                {
                    ImageButton imgB1 = getControl(f, c);
                    if (imgB1.ImageUrl == imgURL_HORA_SEPARADA)
                    {
                        imgB1.ImageUrl = imgURL_HORA_LIBRE;
                        break;
                    }
                }
            }

            //-->
            strHora = gvHorarioDisponible.Rows[fila].Cells[3].Text;

            for (Int32 f = 0; f <= gvHorarioDisponible.Rows.Count - 1; f++)
            {
                DataControlFieldCell CTR1 = (DataControlFieldCell)gvHorarioDisponible.Rows[f].Controls[Col];
                foreach (Control _ctr in CTR1.Controls)
                {
                    if (_ctr.GetType().ToString() == "System.Web.UI.WebControls.DataControlImageButton")
                    {
                        imbTemp = (ImageButton)_ctr;
                        if (imbTemp.ImageUrl.ToUpper() == imgURL_HORA_SEPARADA.ToUpper())
                        {
                            imbTemp.ImageUrl = imgURL_HORA_LIBRE;
                            break;
                        }
                    }
                }
            }

            imbCelda.ImageUrl = imgURL_HORA_SEPARADA;

            DataKey oDatos = gvHorarioDisponible.DataKeys[fila];

            hf_DATOS_CITA.Text = oDatos.Values["ID_ASESOR"] + "|" + oDatos.Values["NOM_ASESOR"] + "|" + gvHorarioDisponible.Rows[fila].Cells[1].Text + "|" + gvHorarioDisponible.Rows[fila].Cells[2].Text + "|" + oDatos.Values["HORA_CITA"].ToString() + "|" + oDatos.Values["TELEFONO"].ToString().Replace('|', '/') + "|" + oDatos.Values["EMAIL"].ToString();
            lblSeleccion2.Text = "Selección de Reserva -  " + (oParametros.GetValor(Parametros.PARM._10).Equals("0") ? "Asesor de Servicio " + oDatos.Values["ID_ASESOR"].ToString() : oDatos.Values["NOM_ASESOR"].ToString()) + " - " + GetFechaLarga(Convert.ToDateTime(gvHorarioDisponible.Rows[fila].Cells[2].Text)) + " - " + FormatoHora(strHora);
            lblSeleccion1.Text = lblSeleccion2.Text;


        }
        else
        {
            lblSeleccion2.Text = "";
            imbCelda.ImageUrl = imgURL_HORA_LIBRE;
            hf_DATOS_CITA.Text = "";
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>fc_filtrarGrilla()</script>", false);//@004
    }

    protected void btnReprogramarCita_1_2_Click(object sender, EventArgs e)
    {
        if (txtFecha.Text.Trim().Equals("")) return;
        if (ddlHoraInicio1.Items.Count == 0) return;


        try
        {

            if (String.IsNullOrEmpty(lblSeleccion1.Text.Trim()) || String.IsNullOrEmpty(lblSeleccion1.Text.Trim()))
            {
                SRC_MsgInformacion(oParametros.msgSelFec);
            }
            else
            {
                //Validando
                //--------------------
                if (PanelUno.Visible)
                {
                    //GRILLA 1
                    if (lblFlgHorario1.Visible == true)
                    {
                        return;
                    }
                    else if (!escogioReservacion())
                    {
                        SRC_MsgInformacion("Seleccione una fecha para Reprogramar su Cita.");
                        return;
                    }
                }
                else
                {
                    //GRILLA 2
                    if (lblFlgHorario2.Visible == true)
                    {
                        return;
                    }
                    else if (!escogioReservacionHD())
                    {
                        return;
                    }
                }

                //---------------------------------------
                // VERIFICAR CLIENTES EN COLA DE ESPERA
                //---------------------------------------

                oCitasBE = new CitasBE();
                oCitasBEList = new CitasBEList();
                CitasBL oCitasBL = new CitasBL();

                Int32 int_ID_CITA = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[0].ToString());

                hf_ID_CITA.Value = int_ID_CITA.ToString();

                oCitasBE.nid_cita = int_ID_CITA;

                CitasBEList oDatos = oCitasBL.GETListarDatosCita(oCitasBE);
                ViewState["id_asesor"] = oDatos[0].Nid_usuario.ToString();

                gv_Cola_Espera.DataSource = oCitasBL.GETListarClientesEnColaEspera(oCitasBE);
                gv_Cola_Espera.DataBind();

                //-----------------------------
                // REPROGRAMAMOS LA CITA
                //-----------------------------

                string strDatosCita = hf_DATOS_CITA.Text;

                oCitasBE.nid_cita = int_ID_CITA;
                oCitasBE.fe_prog = GetFecha(strDatosCita.Split('|').GetValue(3).ToString());
                oCitasBE.ho_inicio = strDatosCita.Split('|').GetValue(4).ToString();
                oCitasBE.ho_fin = Convert.ToDateTime(strDatosCita.Split('|').GetValue(4).ToString()).AddMinutes(Convert.ToInt32(hf_INTERVALO_TALLER.Value)).ToString("HH:mm");
                oCitasBE.nid_taller = (ConfigurationManager.AppSettings["CambiarTaller"].ToString().Equals("1")) ? Convert.ToInt32(hf_ID_TALLER.Value.ToString()) : 0;
                oCitasBE.nid_Estado = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[1].ToString());
                oCitasBE.tx_observacion = "";
                oCitasBE.Nid_usuario = Convert.ToInt32(strDatosCita.Split('|').GetValue(0).ToString());
                oCitasBE.dd_atencion = getDiaSemana(oCitasBE.fe_prog);
                //-------------------------------
                oCitasBE.co_usuario_crea = ((string.IsNullOrEmpty(Profile.UserName)) ? "" : Profile.UserName);
                oCitasBE.co_usuario_red = ((string.IsNullOrEmpty(Profile.UsuarioRed)) ? "" : Profile.UsuarioRed);
                oCitasBE.no_estacion_red = ((string.IsNullOrEmpty(Profile.Estacion)) ? "" : Profile.Estacion);

                //bool saveQRPNG = true;
                if (oDatos[0].no_nombreqr == null)
                    oCitasBE.no_nombreqr = new CitasBL().GetNombreImagenQR(oDatos[0].nu_placa);
                else if (oDatos[0].no_nombreqr == "")
                    oCitasBE.no_nombreqr = new CitasBL().GetNombreImagenQR(oDatos[0].nu_placa);
                else
                {
                    oCitasBE.no_nombreqr = oDatos[0].no_nombreqr;
                    //saveQRPNG = false;
                }
                //string resultado = new CitasBL().SaveImageQRText(oParametros.SRC_GuardaQR, oCitasBE.no_nombreqr, oDatos[0].nu_placa, oCitasBE.fe_prog, oCitasBE.ho_inicio, oDatos[0].no_marca, oDatos[0].no_modelo, oDatos[0].no_color_exterior, saveQRPNG);

                Int32 resCita = oCitasBL.Reprogramar(oCitasBE);

                if (resCita == 11)
                {
                    return;
                }
                else if (resCita == 22)
                {
                    SRC_MsgInformacion("Ya se ha reservado una Cita en este mismo horario.");
                    return;
                }
                else if (resCita == 33)
                {
                    SRC_MsgInformacion("Este vehículo ya tiene cita separada para esta fecha y hora programada.");
                    return;
                }
                else if (resCita == 44)
                {
                    SRC_MsgInformacion("Ya se ha alcanzado el limite de atenciones por día del Taller.");
                    return;
                }
                else if (resCita == 55)
                {
                    SRC_MsgInformacion("Ya se ha alcanzado el limite de atenciones por día del Asesor.");
                    return;
                }
                else if (resCita == 66)
                {
                    SRC_MsgInformacion("Ya se ha alcanzado el limite de atenciones por día del Taller y Modelo");
                    return;
                }
                else if (resCita == 1)
                {

                    MpeReprogramacion.Hide();

                    EstadoBotones(false, false, true);
                    lbl_mensajebox.Text = _MSG_RESPUESTA.Replace("{estado}", "Reprogramada");
                    popup_msgbox_confirm.Show();


                    oCitasBE.nid_cita = int_ID_CITA;

                    oCitasBEList = oCitasBL.GETListarDatosCita(oCitasBE);

                    if (oCitasBEList.Count == 0) return;

                    //>>------ REPROGRAMACION ----- >>

                    //EnviarCorreo_Cliente(oCitasBEList[0], Parametros.EstadoCita.REPROGRAMADA);
                    //EnviarCorreo_Asesor(oCitasBEList[0], Parametros.EstadoCita.REPROGRAMADA);

                }
                else if (resCita == 0)
                {
                    //REPROGRAMADA POR OTRO USUARIO
                }
                else if (resCita > 10)
                {
                }
                else
                {   //< 0 -> ERROR DE BD
                }

            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }

    protected void ddlPuntoRed_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LimpiarFiltros(6);

            string strRutaMapa1 = string.Empty;

            if (ddlPuntoRed.SelectedIndex == 0)
            {
                ddlTaller.Items.Insert(0, _TODOS);
            }
            else
            {
                oCitasBE = new CitasBE();
                oCitasBEList = new CitasBEList();

                CitasBL oCitasBL = new CitasBL();

                oCitasBE.nid_Servicio = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[4].ToString());
                oCitasBE.nid_modelo = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[5].ToString());
                oCitasBE.nid_ubica = Convert.ToInt32(ddlPuntoRed.SelectedValue.ToString());
                oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

                oCitasBEList = oCitasBL.Listar_Talleres(oCitasBE);

                if (oCitasBEList.Count == 0)
                {
                    ddlTaller.Text = "No hay Talleres";
                    ddlTaller.Enabled = false;
                    btnMapaTaller.Visible = false;
                }
                else if (oCitasBEList.Count == 1)
                {
                    lblTaller.Text = oCitasBEList[0].no_taller;
                    ddlTaller.Visible = false;
                    ddlTaller.Enabled = false;
                    lblTaller.Visible = true;
                    btnMapaTaller.Visible = true;

                    imbFecSgte.Enabled = true;
                    imbFecAnt.Enabled = true;
                    imbFecha.Enabled = true;
                    imbFecha1.Enabled = true;
                    imbFecha2.Enabled = true;

                    string strMapaTaller = oCitasBEList[0].tx_mapa_taller;
                    string strNombTaller = oCitasBEList[0].no_taller;

                    if (!strMapaTaller.Trim().Equals(""))
                    {
                        string strRutaMapa = ConfigurationManager.AppSettings["RutaMapasBO"] + strMapaTaller;
                        btnMapaTaller.Attributes.Add("onclick", "javascript:foto('" + strRutaMapa + "','" + strNombTaller + "');");
                    }
                    else
                    {
                        btnMapaTaller.Attributes.Add("onclick", "javascript:alert('" + oParametros.msgNoMapa + "');");
                    }

                    //------------------------------------------------------

                    hf_ID_TALLER.Value = oCitasBEList[0].nid_taller.ToString();
                    string strFechaHabil = SRC_FECHA_HABIL().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);

                    //------------------------------------------------------------------

                    oCitasBE.nid_taller = oCitasBEList[0].nid_taller;
                    oCitasBE.dd_atencion = getDiaSemana(GetFecha(strFechaHabil));

                    oCitasBEList = new CitasBEList();
                    oCitasBEList = oCitasBL.GETListarDatosTaller(oCitasBE);

                    //------------------------------------------------------------------
                    hf_FECHA_HABIL.Value = strFechaHabil;

                    txtFecha.Text = strFechaHabil;
                    hfFecha.Value = strFechaHabil;

                    hf_DATOS_TALLER.Value = oCitasBEList[0].no_distrito + "|" + oCitasBEList[0].di_ubica + "|" + oCitasBEList[0].nu_telefono + "|" + oCitasBEList[0].no_taller;
                    hf_INTERVALO_TALLER.Value = oCitasBEList[0].qt_intervalo_atenc;

                    ConsultarHorarioReserva(); //----> GRID 1
                    //MostrarHorarioDisponible() '---> GRID 2
                }
                else
                {
                    ddlTaller.AutoPostBack = false;

                    ddlTaller.DataSource = oCitasBEList;
                    ddlTaller.DataTextField = "NO_TALLER";
                    ddlTaller.DataValueField = "NID_TALLER";
                    ddlTaller.DataBind();

                    ddlTaller.Items.Insert(0, _TODOS);
                    ddlTaller.SelectedIndex = 1;
                    ddlTaller.Enabled = true;

                    btnMapaTaller.Visible = true;

                    ddlTaller.AutoPostBack = true;
                    ddlTaller_SelectedIndexChanged(this, null);
                }
            }
        }
        catch (Exception ex)
        {
            SRC_MsgInformacion(ex.Message);
        }
    }

    protected void ddlTaller_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LimpiarFiltros(7);

            if (ddlTaller.SelectedIndex == 0)
            {
                btnMapaTaller.Attributes.Add("onclick", "javascript:alert('Seleccione un Taller.');");
            }
            else
            {
                oCitasBE = new CitasBE();
                oCitasBEList = new CitasBEList();

                CitasBL oCitasBL = new CitasBL();

                oCitasBE.nid_Servicio = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[4].ToString());
                oCitasBE.nid_modelo = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[5].ToString());
                oCitasBE.nid_ubica = Convert.ToInt32(ddlPuntoRed.SelectedValue.ToString());
                oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

                oCitasBEList = oCitasBL.Listar_Talleres(oCitasBE);

                hf_ID_TALLER.Value = ddlTaller.SelectedValue.ToString();
                string strFechaHabil = SRC_FECHA_HABIL().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);

                //------------------------------------------------------------------

                oCitasBE.nid_taller = Convert.ToInt32(ddlTaller.SelectedValue.ToString());
                oCitasBE.dd_atencion = getDiaSemana(GetFecha(strFechaHabil));

                oCitasBEList = new CitasBEList();
                oCitasBEList = oCitasBL.GETListarDatosTaller(oCitasBE);

                string strMapaTaller = oCitasBEList[0].tx_mapa_taller;
                string strNombTaller = oCitasBEList[0].no_taller;


                if (!(strMapaTaller.Trim().Equals("")))
                {
                    string strRutaMapa = ConfigurationManager.AppSettings["RutaMapasBO"] + strMapaTaller;
                    btnMapaTaller.Attributes.Add("onclick", "javascript:foto('" + strRutaMapa + "','" + strNombTaller + "');");
                }
                else
                {
                    btnMapaTaller.Attributes.Add("onclick", "javascript:alert('" + oParametros.msgNoMapa + "');");
                }

                hf_ID_TALLER.Value = ddlTaller.SelectedValue.ToString();
                hf_INTERVALO_TALLER.Value = oCitasBEList[0].qt_intervalo_atenc;
                hf_FECHA_HABIL.Value = strFechaHabil;
                hf_DATOS_TALLER.Value = oCitasBEList[0].no_distrito.Trim() + '|' + oCitasBEList[0].di_ubica.Trim() + '|' + oCitasBEList[0].nu_telefono.Trim();

                txtFecha.Text = strFechaHabil;
                hfFecha.Value = strFechaHabil;

                imbFecSgte.Enabled = true;
                imbFecAnt.Enabled = true;
                imbFecha.Enabled = true;
                imbFecha2.Enabled = true;
                imbFecha1.Enabled = true;

                ConsultarHorarioReserva();  //---> GRID 1
            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }

    private void LimpiarFiltros(Int32 cantCBO)
    {
        if (cantCBO <= 5) { ddlPuntoRed.Items.Clear(); ddlPuntoRed.Items.Insert(0, (new ListItem(_SELECCIONAR, "0"))); }
        if (cantCBO <= 6)
        {
            ddlTaller.Items.Clear();
            //ddlTaller.Enabled = false;
            ddlTaller.Items.Insert(0, (new ListItem(_SELECCIONAR, "0")));
            ddlTaller.Visible = true;

            btnMapaTaller.Visible = false;
            lblTaller.Visible = false;
        }
        if (cantCBO <= 7)
        {
            LimpiarReservaCita();
        }
    }
    private void LimpiarReservaCita()
    {
        ddlHoraInicio1.Items.Clear();
        ddlHoraFin1.Items.Clear();
        ddlHoraInicio2.Items.Clear();
        ddlHoraFin2.Items.Clear();

        ddlHoraInicio1.Enabled = false;
        ddlHoraFin1.Enabled = false;
        ddlHoraInicio2.Enabled = false;
        ddlHoraFin2.Enabled = false;


        gvHorarioReserva.DataSource = null;
        gvHorarioReserva.DataBind();
        gvHorarioReserva.Columns.Clear();
        gvHorarioDisponible.DataSource = null;
        gvHorarioDisponible.DataBind();

        imbFecSgte.Enabled = false;
        imbFecAnt.Enabled = false;
        imbFecha.Enabled = false;
        imbFecha1.Enabled = false;
        imbFecha2.Enabled = false;

        lblFlgHorario1.Text = ConfigurationManager.AppSettings["msgNoHorario1"].ToString();
        lblFlgHorario2.Text = ConfigurationManager.AppSettings["msgNoHorario1"].ToString();

        pnlHorarioReserva.Visible = false;
        pnlHorarioDisponible.Visible = false;
        pnlLeyenda.Visible = false;
        pnlLeyenda2.Visible = false;
        lblFlgHorario1.Visible = true;
        lblFlgHorario2.Visible = true;

        lblSeleccion1.Text = "";
        lblSeleccion2.Text = "";
        txtFecha.Text = "";
        txtFechaIni.Text = "";
        txtFechaFin.Text = "";
    }

    private string GetFechaMaxReserva()
    {
        CitasBL objL = new CitasBL();
        string strFecha = String.Format("{0:d}", Convert.ToDateTime(objL.GETFechaActual()).AddDays(Convert.ToInt32(oParametros.GetValor(Parametros.PARM._06))));
        objL = null;
        return strFecha;

    }
    private string GetFechaMinReserva()
    {
        CitasBL objL = new CitasBL();
        string strFecha = String.Format("{0:d}", Convert.ToDateTime(objL.GETFechaActual()).AddDays(Convert.ToInt32(oParametros.GetValor(Parametros.PARM._05))));
        objL = null;
        return strFecha;
    }

    public bool IsDate(object Expression)
    {
        if (Expression != null)
        {
            if (Expression is DateTime)
            {
                return true;
            }
            if (Expression is string)
            {
                try
                {
                    DateTime time1 = Convert.ToDateTime(Expression);
                    return true;
                }
                catch //(Exception ex)
                {

                }
            }
        }
        return false;
    }
    public long DateDiff(DateInterval interval, DateTime date1, DateTime date2)
    {
        long rs = 0;
        TimeSpan diff = date2.Subtract(date1);
        switch (interval)
        {
            case DateInterval.Day:
            case DateInterval.DayOfYear:
                rs = (long)diff.TotalDays;
                break;
            case DateInterval.Hour:
                rs = (long)diff.TotalHours;
                break;
            case DateInterval.Minute:
                rs = (long)diff.TotalMinutes;
                break;
            case DateInterval.Second:
                rs = (long)diff.TotalSeconds;
                break;
            case DateInterval.Weekday:
            case DateInterval.WeekOfYear:
                rs = (long)(diff.TotalDays / 7);
                break;
            case DateInterval.Year:
                rs = date2.Year - date1.Year;
                break;
        }
        return rs;
    }
    public enum DateInterval
    {
        Day,
        DayOfYear,
        Hour,
        Minute,
        Month,
        Quarter,
        Second,
        Weekday,
        WeekOfYear,
        Year
    }
    private string returnnameday(int numberday)
    {
        string nameday = string.Empty;
        switch (numberday)
        {
            case 1: nameday = "Lunes"; break;
            case 2: nameday = "Martes"; break;
            case 3: nameday = "Mi&eacute;rcoles"; break;
            case 4: nameday = "Jueves"; break;
            case 5: nameday = "Viernes"; break;
            case 6: nameday = "S&aacute;bado"; break;
            case 7: nameday = "Domingo"; break;
        }
        return nameday;
    }
    private int getDiaSemana(DateTime dtFecha)
    {
        Int32 intDia = 0;
        switch (dtFecha.DayOfWeek)
        {
            case DayOfWeek.Monday: intDia = 1; break;
            case DayOfWeek.Tuesday: intDia = 2; break;
            case DayOfWeek.Wednesday: intDia = 3; break;
            case DayOfWeek.Thursday: intDia = 4; break;
            case DayOfWeek.Friday: intDia = 5; break;
            case DayOfWeek.Saturday: intDia = 6; break;
            case DayOfWeek.Sunday: intDia = 7; break;
        }
        return intDia;
    }

    private void ConsultarHorarioReserva()
    {
        try
        {
            Int32 intTCA = 0; // TCA -> Total Citas x Asesor
            Int32 intTCE = 0; // TCE -> Total Citas en Cola espera
            Int32 intTCT = 0; // TCT -> Total Citas x Taller

            Int32 intSW = 0;//SW 
            Int32 intCantCE = 0;

            hf_HORAS_VACIAS.Value = "";

            DateTime dHoraIni_T;//  Horario Inicial del Taller (08:00)
            DateTime dHoraFin_T;//  Horario Final del   Taller (18:00)

            DateTime _dHoraIni_T;//HI Taller Temp
            DateTime _dHoraFin_T;//HF Taller Temp

            DateTime dHoraIni_E;// Horario Excepcional Inicial del Taller (12:00)
            DateTime dHoraFin_E;// Horario Excepcional Final   del Taller (15:00)

            DateTime dHoraIni_C;// Horario Inicial Cita
            DateTime dHoraFin_C;// Horario Final   Cita

            DateTime dHoraIni_A = DateTime.MinValue;
            DateTime dHoraFin_A = DateTime.MinValue;

            Int32 _ID_TALLER = Convert.ToInt32(hf_ID_TALLER.Value.ToString());
            Int32 _INTERVALO = Convert.ToInt32(hf_INTERVALO_TALLER.Value.ToString());
            string _TALLER = string.Empty;


            //string[] strRangosHE;// Rangos del Horario Excepcional
            //string[] strRangosHA = null;// Rangos del Horario de Asesor

            string strTotalHE;// Acumulacion de Rangos de HE
            //string strCodAsesor ;//Codigos de los Asesores

            //CitasBEList _lstTalleres;
            //CitasBEList _lstAsesores;
            CitasBEList _lstCitas;
            //CitasBEList _lstTalleresHE;

            CitasBEList lstTalleres;
            CitasBEList lstAsesores;
            CitasBEList lstCitas;
            CitasBEList lstTalleresHE;

            Int32 hay1 = 0;
            Int32 hay2 = 0;

            DataTable dtReserva = new DataTable();//Datatable para rellenar el Gridview

            var blNoDisponible = false; // -> 28.08.2012
            //----------------

            ddlHoraInicio1.Items.Clear();
            ddlHoraFin1.Items.Clear();
            //ddlHoraInicio2.Items.Clear();
            //ddlHoraFin2.Items.Clear();
            ddlHoraInicio1.Enabled = false;
            ddlHoraFin1.Enabled = false;
            ddlHoraInicio2.Enabled = false;
            ddlHoraFin2.Enabled = false;

            pnlHorarioReserva.Visible = false;
            pnlLeyenda.Visible = false;
            lblFlgHorario1.Visible = true;
            gvHorarioReserva.DataSource = null;
            gvHorarioReserva.DataBind();

            //------------------------------------------------------------------------>>>> OPCION 1
            oCitasBE = new CitasBE();

            oCitasBE.nid_Servicio = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[4].ToString());
            oCitasBE.nid_modelo = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[5].ToString());
            oCitasBE.coddpto = "0";
            oCitasBE.codprov = "0";
            oCitasBE.coddis = "0";
            oCitasBE.nid_ubica = Convert.ToInt32((ddlPuntoRed.SelectedIndex <= 0 ? "0" : ddlPuntoRed.SelectedValue.ToString()));
            oCitasBE.nid_taller = _ID_TALLER;
            oCitasBE.fe_atencion = GetFecha(txtFecha.Text);
            oCitasBE.dd_atencion = getDiaSemana(oCitasBE.fe_atencion);
            oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

            //-------------------------------------------------
            lstTalleres = new CitasBEList();
            lstTalleresHE = new CitasBEList();
            lstAsesores = new CitasBEList();
            _lstCitas = new CitasBEList();

            CitasBL oCitasBL = new CitasBL();

            lstTalleres = oCitasBL.ListarTalleresDisponibles_PorFecha(oCitasBE);//  1-Listado el Taller
            lstTalleresHE = oCitasBL.ListarHorarioExcepcional_Talleres(oCitasBE);// 2-Listado Horario Excepcionales
            lstAsesores = oCitasBL.ListarAsesoresDisponibles_PorFecha(oCitasBE);//  3-Listado Asesores - Taller
            _lstCitas = oCitasBL.ListarCitasAsesores(oCitasBE);//                   4-Listado CitasAsesores - Taller

            //-------------------------------------------------------------------------------------

            CitasBE oTaller = new CitasBE();

            if (lstTalleres.Count > 0)
            {
                oTaller = lstTalleres[0];
                dHoraIni_T = Convert.ToDateTime(oTaller.ho_inicio_t);
                dHoraFin_T = Convert.ToDateTime(oTaller.ho_fin_t);
                _TALLER = oTaller.no_taller;
            }
            else
            {
                lblFlgHorario1.Text = oParametros.msgNoHorario2 + " " + txtFecha.Text;
                return;
            }

            //-------------------------------------------------            

            if (lstAsesores.Count == 0)
            {
                //lblFlgPanel1.Text = "No hay Asesores disponibles para el " + txtFecha.Text;
                return;
            }

            //=================================================================================
            //> Horas Excepcional del Taller
            //=================================================================================

            strTotalHE = string.Empty;
            foreach (CitasBE oHET in lstTalleresHE)
            {
                if (!string.IsNullOrEmpty(oHET.ho_rango1)) strTotalHE += oHET.ho_rango1 + "-";
                if (!string.IsNullOrEmpty(oHET.ho_rango2)) strTotalHE += oHET.ho_rango2 + "-";
                if (!string.IsNullOrEmpty(oHET.ho_rango3)) strTotalHE += oHET.ho_rango3 + "-";
            }
            if (!string.IsNullOrEmpty(strTotalHE))
            {
                strTotalHE = strTotalHE.Substring(0, strTotalHE.Length - 1);
            }


            //**********************************************************************
            //  > Creando el GridView dinamicamente
            //----------------------------------------------------------------------

            gvHorarioReserva.DataSource = null;
            gvHorarioReserva.DataBind();
            gvHorarioReserva.Columns.Clear();

            int intWIDTH_GRID = 0;

            BoundField oColumna;
            ButtonField oColumnaB;

            //-----------------------------------
            //Columna: TALLER
            //-----------------------------------
            oColumna = new BoundField();
            oColumna.HeaderText = _HEADER_TALLER;
            oColumna.DataField = _DATA_TALLER;
            oColumna.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.HeaderStyle.Width = _WIDTH_COL_TALLER;
            oColumna.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.ItemStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.ItemStyle.Width = _WIDTH_COL_TALLER;
            gvHorarioReserva.Columns.Add(oColumna);

            //-----------------------------------
            //Columna: ASESOR
            //-----------------------------------
            oColumna = new BoundField();
            oColumna.HeaderText = _HEADER_ASESOR;
            oColumna.DataField = _DATA_ASESOR;
            oColumna.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.HeaderStyle.Width = _WIDTH_COL_ASESOR;
            oColumna.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.ItemStyle.Width = _WIDTH_COL_ASESOR;
            gvHorarioReserva.Columns.Add(oColumna);

            //-----------------------------------
            //Columna: TCA
            //-----------------------------------
            oColumna = new BoundField();
            oColumna.HeaderText = "TCA";
            oColumna.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.HeaderStyle.Width = _WIDTH_COL_TCA;
            oColumna.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.ItemStyle.Width = _WIDTH_COL_TCA;
            gvHorarioReserva.Columns.Add(oColumna);

            //-----------------------------------
            //Columna: TCE
            //-----------------------------------
            oColumna = new BoundField();
            oColumna.HeaderText = "TCE";
            oColumna.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.HeaderStyle.Width = _WIDTH_COL_TCE;
            oColumna.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.ItemStyle.Width = _WIDTH_COL_TCE;
            gvHorarioReserva.Columns.Add(oColumna);

            intWIDTH_GRID = _WIDTH_COL_TALLER + _WIDTH_COL_ASESOR + _WIDTH_COL_TCE + _WIDTH_COL_TCA;

            oColumna = null;

            //--------------------------------------------------------------------------------
            // Armnado el Datatable para el Cronograma de Citas
            //----------------------------------------------------
            dtReserva = new DataTable();
            dtReserva.Columns.Add("TALLER", System.Type.GetType("System.String"));
            dtReserva.Columns.Add("ASESOR_SERVICIO", System.Type.GetType("System.String"));
            //---
            dtReserva.Columns.Add("ID_ASESOR", System.Type.GetType("System.String"));
            dtReserva.Columns.Add("ID_TALLER", System.Type.GetType("System.String"));
            dtReserva.Columns.Add("NOM_ASESOR", System.Type.GetType("System.String"));
            dtReserva.Columns.Add("TELEFONO", System.Type.GetType("System.String"));
            dtReserva.Columns.Add("EMAIL", System.Type.GetType("System.String"));

            //------

            gvHorarioReserva.DataKeyNames = new string[] { "ID_ASESOR", "ID_TALLER", "NOM_ASESOR", "TELEFONO", "EMAIL" };


            _dHoraIni_T = dHoraIni_T;
            _dHoraFin_T = dHoraFin_T;

            Int32 intCant = 0;


            while ((_dHoraIni_T < _dHoraFin_T))
            {
                string _strHT = _dHoraIni_T.ToString("HH:mm").ToUpper().Replace(".", "").Replace(":", "");

                oColumnaB = new ButtonField();
                oColumnaB.ButtonType = ButtonType.Image;
                oColumnaB.HeaderText = _dHoraIni_T.ToString("hh:mm").ToUpper().Replace(".", "");
                oColumnaB.DataTextField = String.Concat("HORA_", _strHT);
                oColumnaB.CommandName = String.Concat("HORA_", intCant.ToString(), '_', _strHT);
                oColumnaB.ImageUrl = imgURL_HORA_VACIA;
                oColumnaB.HeaderStyle.Width = _WIDTH_COL_HORAS;
                oColumnaB.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                gvHorarioReserva.Columns.Add(oColumnaB);

                dtReserva.Columns.Add(string.Concat("HORA_", _strHT), System.Type.GetType("System.String"));

                intCant = intCant + 1;
                intWIDTH_GRID = intWIDTH_GRID + _WIDTH_COL_HORAS;

                _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
            }

            //***************************************************************************************

            string strPARM_10 = oParametros.GetValor(Parametros.PARM._10).ToString();
            DataRow oRow = null;


            foreach (CitasBE oAsesor in lstAsesores)
            {
                oRow = dtReserva.NewRow();
                oRow["TALLER"] = _TALLER.Trim();
                oRow["ASESOR_SERVICIO"] = (strPARM_10.Equals("0") ? "Asesor de Servicio - " + oAsesor.nid_asesor.ToString() : oAsesor.no_asesor);
                //--
                oRow["ID_ASESOR"] = oAsesor.nid_asesor.ToString().Trim();
                oRow["ID_TALLER"] = _ID_TALLER;
                oRow["NOM_ASESOR"] = oAsesor.no_asesor.ToString().Trim();
                oRow["TELEFONO"] = oAsesor.nu_telefono_a.ToString().Trim();
                oRow["EMAIL"] = oAsesor.no_correo_a.ToString().Trim();
                //--
                dtReserva.Rows.Add(oRow);
            }

            gvHorarioReserva.DataSource = dtReserva;
            gvHorarioReserva.DataBind();
            gvHorarioReserva.Width = intWIDTH_GRID;

            //----------------------------------------------------------
            bool blNoDisponibleT = false;

            //Validaciones Fecha Exceptuada - Taller
            if (oTaller.nid_dia_exceptuado_t == 1)
            {
                blNoDisponibleT = true;
            }
            //Validaciones Capacidad Atencion - Taller
            else if (oTaller.qt_cantidad_t <= 0)
            {
                blNoDisponibleT = true;
            }
            //Validacion Capacidad Atencion - Taller y Modelo
            else if (oParametros.SRC_Pais.Equals(1))//07092012 -> SOLO PERU
            {
                if (oTaller.qt_cantidad_m <= 0) blNoDisponibleT = true;
            }

            //----------------------------------------------------------
            //------------------------------------------------------------------------
            // Colocamos los iconos de Horario Disponible, Reservado y Excepcional
            //------------------------------------------------------------------------

            Int32 intFila = 0;
            Int32 intCol = 0;

            foreach (CitasBE oAsesor in lstAsesores)
            {
                if (blNoDisponibleT)// Por Taller
                {
                    gvHorarioReserva.Rows[intFila].BackColor = System.Drawing.Color.FromArgb(178, 213, 247);
                }
                else// Por Asesor
                {
                    blNoDisponible = false;

                    //Validacion Fecha Exceptuada - Asesor
                    if (oAsesor.nid_dia_exceptuado_a == 1)
                    {
                        blNoDisponible = true; // Continue For' -> 28.08.2012
                        gvHorarioReserva.Rows[intFila].BackColor = System.Drawing.Color.FromArgb(178, 213, 247);
                    }
                    if (!blNoDisponible)
                    {
                        //Validacion Capacidad Atencion - Asesor
                        if (oAsesor.qt_cantidad_a <= 0)
                        {
                            blNoDisponible = true; // Continue For' -> 28.08.2012
                            gvHorarioReserva.Rows[intFila].BackColor = System.Drawing.Color.FromArgb(178, 213, 247);
                        }
                    }
                }
                //-------------------------
                // SET HORARIO DEL ASESOR 
                //------------------------------------------------------ 

                intCol = 0;

                foreach (string strHorario in oAsesor.horario_asesor.Split('|'))
                {
                    _dHoraIni_T = dHoraIni_T;
                    _dHoraFin_T = dHoraFin_T;

                    intCol = 0;

                    dHoraIni_A = Convert.ToDateTime(strHorario.Split('-').GetValue(0).ToString());
                    dHoraFin_A = Convert.ToDateTime(strHorario.Split('-').GetValue(1).ToString());

                    while (_dHoraIni_T <= _dHoraFin_T)
                    {
                        if (_dHoraIni_T >= dHoraIni_A && _dHoraIni_T < dHoraFin_A)
                        {
                            setIconoGrilla(intFila, _dHoraIni_T, imgURL_HORA_LIBRE, -1);
                        }

                        intCol += 1;
                        _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
                    }
                }

                //-------------------------
                // SET CITAS 
                //------------------------------------------------------                   
                // Filtrar Citas por Asesor
                //------------------------------------------------------   

                lstCitas = new CitasBEList();

                hay1 = 0;
                hay2 = 0;
                foreach (CitasBE oEntidad in _lstCitas)
                {
                    hay1 = 0;
                    if (oAsesor.nid_asesor.Equals(oEntidad.nid_asesor))
                    {
                        hay1 = 1; hay2 = 1;
                        lstCitas.Add(oEntidad);
                    }
                    if ((hay1 == 0) && (hay2 == 1))
                        break;
                }

                //-------------------------------------------------------------------
                intTCA = 0;
                intTCE = 0;

                intSW = 0;
                intCantCE = 0;

                foreach (CitasBE oCita in lstCitas)
                {
                    dHoraIni_C = DateTime.Parse(oCita.ho_inicio_c.ToString());
                    dHoraFin_C = DateTime.Parse(oCita.ho_fin_c.ToString());

                    intCantCE = (Int32)oCita.qt_cola_espera;// Cantidad - Cola Espera

                    intSW = 0;

                    while (dHoraIni_C < dHoraFin_C)
                    {
                        _dHoraIni_T = dHoraIni_T;
                        _dHoraFin_T = dHoraFin_T;

                        intCol = 0;

                        while (_dHoraIni_T <= _dHoraFin_T)
                        {

                            if (_dHoraIni_T >= dHoraIni_C && dHoraIni_C < _dHoraFin_T)
                            {
                                //>> Si hay cita reservada (ICONO DE RESERVADO)
                                setIconoGrilla(intFila, _dHoraIni_T, imgURL_HORA_RESERVADA, intCantCE);
                                intSW = 1;
                                break;
                            }

                            intCol += 1;
                            _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
                        }

                        dHoraIni_C = dHoraIni_C.AddMinutes(_INTERVALO);
                    }

                    if (intSW == 1)
                    {
                        //Contabilizamos el TCE y TCA
                        //------------------------------
                        intTCA++;
                        intTCE += intCantCE;
                    }
                }

                intTCT += intTCA;

                gvHorarioReserva.Rows[intFila].Cells[2].Text = intTCA.ToString(); // TCA
                gvHorarioReserva.Rows[intFila].Cells[3].Text = intTCE.ToString(); // TCE 


                //--------------------------
                // SET HORARIO EXCEPCIONAL  
                //------------------------------------------------

                if (!string.IsNullOrEmpty(strTotalHE))
                {
                    foreach (string _strRangoHE in strTotalHE.Split('-'))
                    {
                        dHoraIni_E = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(0));// Hora Inicial HET
                        dHoraFin_E = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(1));// Hora Final   HET

                        _dHoraIni_T = dHoraIni_T;
                        _dHoraFin_T = dHoraFin_T;

                        intCol = 0;

                        while (_dHoraIni_T <= _dHoraFin_T)
                        {
                            // Si es una hora excepcionl
                            if (_dHoraIni_T >= dHoraIni_E && _dHoraIni_T < dHoraFin_E)
                            {
                                // Icon: Horario Excepcional
                                setIconoGrilla(intFila, _dHoraIni_T, imgURL_HORA_EXCEPCIONAL, -1);
                            }

                            intCol += 1;
                            _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
                        }
                    }
                }

                intFila += 1;
            }

            Lbltct.Text = intTCT.ToString();

            cargarComboHorarioTaller(ddlHoraInicio1, dHoraIni_T, dHoraFin_T, _INTERVALO, "HI");
            cargarComboHorarioTaller(ddlHoraFin1, dHoraIni_T, dHoraFin_T, _INTERVALO, "HF");

            hfHoraIni1.Value = ddlHoraInicio1.SelectedValue.ToString();
            hfHoraFin1.Value = ddlHoraFin1.SelectedValue.ToString();


            ddlHoraInicio1.Enabled = true;
            ddlHoraFin1.Enabled = true;
            pnlLeyenda.Visible = true;
            lblFlgHorario1.Visible = false;


            //imbFecSgte.Enabled = true;
            //imbFecAnt.Enabled = true;
            //imbFecha.Enabled = true;

            //---------------------------------------------> REMOVE: HORARIO BLANCO

            Int32 inrFilas = gvHorarioReserva.Rows.Count;
            Int32 intCount = 0;


            string strHorasV = string.Empty;

            for (Int32 intC = 0; intC <= gvHorarioReserva.Columns.Count - 1; intC++)
            {
                intCount = 0;
                for (Int32 intF = 0; intF <= gvHorarioReserva.Rows.Count - 1; intF++)
                {
                    DataControlFieldCell CTR_I = (DataControlFieldCell)gvHorarioReserva.Rows[intF].Controls[intC];
                    foreach (Control _ctrI in CTR_I.Controls)
                    {
                        if (_ctrI.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                        {
                            if (((ImageButton)_ctrI).ImageUrl == imgURL_HORA_VACIA)
                                intCount += 1;
                        }
                    }
                }

                if ((gvHorarioReserva.Columns[intC].GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField")))
                {
                    if (intCount != inrFilas)
                        break;
                    strHorasV += ((ButtonField)gvHorarioReserva.Columns[intC]).DataTextField + "|";
                }
            }

            for (Int32 intC = gvHorarioReserva.Columns.Count - 1; intC >= 0; intC += -1)
            {
                intCount = 0;
                for (Int32 intF = 0; intF <= gvHorarioReserva.Rows.Count - 1; intF++)
                {
                    DataControlFieldCell CTR_F = (DataControlFieldCell)gvHorarioReserva.Rows[intF].Controls[intC];
                    foreach (Control _ctrF in CTR_F.Controls)
                    {
                        if (_ctrF.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                        {
                            if (((ImageButton)_ctrF).ImageUrl == imgURL_HORA_VACIA)
                                intCount += 1;
                        }
                    }
                }

                if ((gvHorarioReserva.Columns[intC].GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField")))
                {
                    if (intCount != inrFilas)
                        break;
                    strHorasV += ((ButtonField)gvHorarioReserva.Columns[intC]).DataTextField + '|';
                }
            }

            hf_HORAS_VACIAS.Value = strHorasV;

            if (!string.IsNullOrEmpty(strHorasV))
            {
                foreach (DataControlField oColumn in gvHorarioReserva.Columns)
                {
                    if ((oColumn.GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField")))
                    {
                        if (strHorasV.Contains(((ButtonField)oColumn).DataTextField))
                        {
                            oColumn.Visible = false;
                        }
                    }
                }

                gvHorarioReserva.Width = intWIDTH_GRID - (_WIDTH_COL_HORAS * strHorasV.Split('|').Length);
            }

            pnlHorarioReserva.Visible = true;

            //----------------------------------------------------------------
            // Deshabilitando celdas en blanco y Celda coloreadas
            //----------------------------------------------------------------

            for (int intF = 0; intF <= gvHorarioReserva.Rows.Count - 1; intF++)
            {
                for (int intC = 0; intC <= gvHorarioReserva.Columns.Count - 1; intC++)
                {
                    if ((gvHorarioReserva.Columns[intC].GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField")))
                    {

                        if (gvHorarioReserva.Rows[intF].BackColor == System.Drawing.Color.FromArgb(178, 213, 247))
                        {
                            gvHorarioReserva.Rows[intF].Cells[intC].Enabled = false;
                        }
                        else
                        {
                            DataControlFieldCell CTR_I = (DataControlFieldCell)gvHorarioReserva.Rows[intF].Controls[intC];
                            foreach (Control _ctrI in CTR_I.Controls)
                            {
                                if (_ctrI.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                                    if (((ImageButton)_ctrI).ImageUrl == imgURL_HORA_VACIA || ((ImageButton)_ctrI).ImageUrl == imgURL_HORA_EXCEPCIONAL)
                                        gvHorarioReserva.Rows[intF].Cells[intC].Enabled = false;
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }

    private void MostrarHorarioDisponible()
    {

        if (!IsDate(txtFechaFin.Text))
        {
            return;
        }


        CitasBL oCitasBL = new CitasBL();

        Int32 _ID_TALLER = Convert.ToInt32(hf_ID_TALLER.Value.ToString());
        Int32 _INTERVALO = Convert.ToInt32(hf_INTERVALO_TALLER.Value.ToString());
        Int32 _ID_SERVICIO = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[4].ToString());
        Int32 _ID_MODELO = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[5].ToString());


        string _TALLER = string.Empty;

        DateTime dFechaIni = Convert.ToDateTime(GetFechaMinReserva());
        DateTime dFechaFin = Convert.ToDateTime(GetFechaMaxReserva());

        DateTime dHoraIni_T = DateTime.MinValue;
        DateTime dHoraFin_T = DateTime.MinValue;

        DateTime _dHoraIni_T = DateTime.MinValue;
        DateTime _dHoraFin_T = DateTime.MinValue;

        DateTime dHoraIni_A = DateTime.MinValue;
        DateTime dHoraFin_A = DateTime.MinValue;

        //CitasBEList _lstTalleres;
        CitasBEList _lstAsesores;
        CitasBEList _lstCitas;
        CitasBEList _lstTalleresHE;

        CitasBEList lstTalleres;
        CitasBEList lstAsesores;
        CitasBEList lstCitas;
        CitasBEList lstTalleresHE;

        string strTotalHE;// Acumulacion de Rangos de HE

        string strAsesorCapac = string.Empty;

        DateTime dtHEITaller;
        DateTime dtHEFTaller;

        DateTime dtHoraI_ddl = DateTime.MaxValue;
        DateTime dtHoraF_ddl = DateTime.MinValue;

        Int32 hay1 = 0;
        Int32 hay2 = 0;

        ddlHoraInicio2.Items.Clear();
        ddlHoraFin2.Items.Clear();
        ddlHoraInicio2.Enabled = false;
        ddlHoraFin2.Enabled = false;

        bool swHorasT = false;
        DataRow oRow = null;

        try
        {
            oCitasBE = new CitasBE();

            Int32 intPRM_10 = Convert.ToInt32(oParametros.GetValor(Parametros.PARM._10));

            DataTable dtHorarioDisp = new DataTable();
            //---------------------------------
            dtHorarioDisp.Columns.Add("PUNTO_RED", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("TALLER", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("FECHA", System.Type.GetType("System.DateTime"));
            dtHorarioDisp.Columns.Add("HORA", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("ASESOR_SERVICIO", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("QUICK_SERVICE", System.Type.GetType("System.String"));
            //------
            dtHorarioDisp.Columns.Add("ID_ASESOR", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("ID_TALLER", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("NOM_ASESOR", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("HORA_CITA", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("TELEFONO", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("EMAIL", System.Type.GetType("System.String"));

            //------

            gvHorarioDisponible.DataKeyNames = new String[] { "ID_ASESOR", "ID_TALLER", "HORA_CITA", "NOM_ASESOR", "TELEFONO", "EMAIL" };

        //Recorremos Fecha por Fecha 
        //------------------------------------------------------

        Continue_While_1:
            while (dFechaIni <= dFechaFin)
            {
                //================================

                oCitasBE.nid_Servicio = _ID_SERVICIO;
                oCitasBE.nid_modelo = _ID_MODELO;
                oCitasBE.coddpto = "0";
                oCitasBE.codprov = "0";
                oCitasBE.coddis = "0";
                oCitasBE.nid_ubica = Convert.ToInt32((ddlPuntoRed.SelectedIndex <= 0 ? "0" : ddlPuntoRed.SelectedValue.ToString()));
                oCitasBE.nid_taller = _ID_TALLER;
                oCitasBE.fe_atencion = dFechaIni;
                oCitasBE.dd_atencion = getDiaSemana(dFechaIni);
                oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

                //---------------------------------------------------------------------------------------------------

                lstTalleres = oCitasBL.ListarTalleresDisponibles_PorFecha(oCitasBE);//   1-Listado todos Talleres
                ViewState["lstTalleres"] = lstTalleres;

                if (lstTalleres.Count == 0)
                {
                    dFechaIni = dFechaIni.AddDays(+1);
                    goto Continue_While_1;
                }

                _lstTalleresHE = oCitasBL.ListarHorarioExcepcional_Talleres(oCitasBE);// 2-Listado Horario Excepcionales
                ViewState["lstTalleresHE"] = _lstTalleresHE;

                _lstAsesores = oCitasBL.ListarAsesoresDisponibles_PorFecha(oCitasBE);//  3-Listado Asesores Talleres
                ViewState["lstAsesores"] = _lstAsesores;

                _lstCitas = oCitasBL.ListarCitasAsesores(oCitasBE);//                    4-Listado CitasAsesores Talleres
                ViewState["lstCitas"] = _lstCitas;

                //===========================================

                foreach (CitasBE oTaller in lstTalleres)
                {
                    _ID_TALLER = Convert.ToInt32(oTaller.nid_taller);
                    _INTERVALO = Convert.ToInt32(oTaller.qt_intervalo_atenc);

                    _dHoraIni_T = Convert.ToDateTime(oTaller.ho_inicio_t);
                    _dHoraFin_T = Convert.ToDateTime(oTaller.ho_fin_t);

                    //=================================================================================
                    //> Validaciones
                    //=================================================================================
                    if (oTaller.qt_cantidad_t <= 0) continue;//Capacidad Atencion Taller
                    if (oTaller.qt_cantidad_m <= 0) continue;//Capacidad Atencion Taller y Modelo
                    if (oTaller.nid_dia_exceptuado_t == 1) continue;//Dia Exceptuado


                    //=================================================================================
                    //> Horas Excepcional del Taller
                    //=================================================================================
                    // FILTRAR HORARIO EXCEPCIONAL
                    //-----------------------------

                    lstTalleresHE = new CitasBEList();

                    hay1 = 0;
                    hay2 = 0;
                    foreach (CitasBE oEntidad in _lstTalleresHE)
                    {
                        hay1 = 0;
                        if (oTaller.nid_taller.Equals(oEntidad.nid_taller))
                        {
                            hay1 = 1; hay2 = 1;
                            lstTalleresHE.Add(oEntidad);
                        }
                        if ((hay1 == 0) && (hay2 == 1))
                            break;
                    }

                    //-----------------------------------------------------------------------
                    strTotalHE = string.Empty;
                    foreach (CitasBE oHET in lstTalleresHE)
                    {
                        if (!string.IsNullOrEmpty(oHET.ho_rango1)) strTotalHE += oHET.ho_rango1 + "-";
                        if (!string.IsNullOrEmpty(oHET.ho_rango2)) strTotalHE += oHET.ho_rango2 + "-";
                        if (!string.IsNullOrEmpty(oHET.ho_rango3)) strTotalHE += oHET.ho_rango3 + "-";
                    }
                    if (!string.IsNullOrEmpty(strTotalHE))
                    {
                        strTotalHE = strTotalHE.Substring(0, strTotalHE.Length - 1);
                    }

                    //=================================================================================
                    //> Listado de Asesores 
                    //=================================================================================
                    // FILTRAR ASESORES - TALLER
                    //-----------------------------------

                    lstAsesores = new CitasBEList();// 2

                    hay1 = 0;
                    hay2 = 0;
                    foreach (CitasBE oEntidad in _lstAsesores)
                    {
                        hay1 = 0;
                        if (oTaller.nid_taller.Equals(oEntidad.nid_taller))
                        {
                            hay1 = 1; hay2 = 1;
                            //lstAsesores.Add(oEntidad);

                            //Validar FechaExceptuada
                            if (oEntidad.nid_dia_exceptuado_a == 0)
                            {
                                //Validar Capacidad de Atencion
                                if (oEntidad.qt_cantidad_a > 0) lstAsesores.Add(oEntidad);
                            }
                        }
                        if ((hay1 == 0) && (hay2 == 1))
                            break;
                    }

                    //--------------------------------------------------------------

                    DateTime odHIA = _dHoraIni_T;
                    DateTime odHFA = _dHoraFin_T;

                    foreach (CitasBE oAsesor in lstAsesores)//==========================>>>>
                    {
                        //=================================================================================
                        //> Listar Citas Asesores 
                        //=================================================================================
                        // FILTRAR CITAS - TALLER - ASESOR
                        //-----------------------------------

                        lstCitas = new CitasBEList();

                        hay1 = 0;
                        hay2 = 0;
                        foreach (CitasBE oEntidad in _lstCitas)
                        {
                            hay1 = 0;
                            if (oTaller.nid_taller.Equals(oEntidad.nid_taller) && oAsesor.nid_asesor.Equals(oEntidad.nid_asesor))
                            {
                                hay1 = 1; hay2 = 1;
                                lstCitas.Add(oEntidad);
                            }
                            if ((hay1 == 0) && (hay2 == 1))
                                break;
                        }

                        //-------------------------------------------------------------
                        //Recorrer cada horario del Asesor
                        foreach (string strHorario in oAsesor.horario_asesor.Split('|'))
                        {
                            dHoraIni_A = Convert.ToDateTime(strHorario.Split('-').GetValue(0).ToString());
                            dHoraFin_A = Convert.ToDateTime(strHorario.Split('-').GetValue(1).ToString());

                            if (dHoraIni_A < odHIA) dHoraIni_A = odHIA;
                            if (dHoraFin_A > odHFA) dHoraFin_A = odHFA;

                            //----------------------------------------------------
                            //> Get menor HI y mayor HF de Horarios disponibles
                            //---------------------------------------------------- 
                            if (dHoraFin_A > dtHoraF_ddl) dtHoraF_ddl = dHoraFin_A;
                            if (dHoraIni_A < dtHoraI_ddl) dtHoraI_ddl = dHoraIni_A;

                            swHorasT = true;

                        Continue_While_2:
                            while (dHoraIni_A < dHoraFin_A)
                            {
                                string strHoraAct = dHoraIni_A.ToString("HH:mm");

                                foreach (CitasBE oCita in lstCitas)
                                {
                                    DateTime dtHActual = dHoraIni_A;

                                    DateTime dtHICita = Convert.ToDateTime(oCita.ho_inicio_c);
                                    DateTime dtHFCita = Convert.ToDateTime(oCita.ho_fin_c);

                                    if (dtHActual >= dtHICita & dtHActual < dtHFCita)
                                    {
                                        dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                                        goto Continue_While_2; //> Es una Cita ya reservada
                                    }
                                }

                                // > Se verifica que la hora del Asesor no sea una hora excepcional

                                if (!String.IsNullOrEmpty(strTotalHE))
                                {
                                    foreach (string _strRangoHE in strTotalHE.Split('-'))
                                    {
                                        dtHEITaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(0));// Hora Inicial HET
                                        dtHEFTaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(1));// Hora Final   HET

                                        //> Si es una hora excepcionl
                                        if (dHoraIni_A >= dtHEITaller & dHoraIni_A < dtHEFTaller)
                                        {
                                            dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                                            goto Continue_While_2; //> Es Horario Excepcional
                                        }
                                    }
                                }

                                //> FECHA ENCONTADA
                                //-------------------------

                                oRow = dtHorarioDisp.NewRow();

                                oRow["PUNTO_RED"] = oTaller.no_ubica;
                                oRow["TALLER"] = oTaller.no_taller;
                                oRow["FECHA"] = dFechaIni.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                                oRow["HORA"] = strHoraAct;
                                oRow["ASESOR_SERVICIO"] = (intPRM_10.Equals(0) ? "Asesor de Servicio - " + oAsesor.nid_asesor.ToString() : oAsesor.no_asesor.ToString());
                                oRow["QUICK_SERVICE"] = (hf_QUICK_SERVICE.Value).Equals("1") ? "SI" : "NO";
                                //---
                                oRow["ID_ASESOR"] = oAsesor.nid_asesor.ToString();
                                oRow["ID_TALLER"] = _ID_TALLER.ToString();
                                oRow["NOM_ASESOR"] = oAsesor.no_asesor;
                                oRow["HORA_CITA"] = strHoraAct;
                                oRow["TELEFONO"] = oAsesor.nu_telefono_a.Trim();
                                oRow["EMAIL"] = oAsesor.no_correo_a.Trim();
                                //

                                dtHorarioDisp.Rows.Add(oRow);

                                dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                            }
                        }
                    }
                }

                dFechaIni = dFechaIni.AddDays(+1);
            }


            //-------------------
            //Ordenar  GridView
            //-------------------

            DataTable dt = dtHorarioDisp.Clone();
            DataRow[] drs = dtHorarioDisp.Select("", "FECHA Asc,HORA Asc,ASESOR_SERVICIO Asc");
            foreach (DataRow dr in drs)
            {
                dt.ImportRow(dr);
            }

            dtHorarioDisp = dt;


            //> Si hay registrio se envia al Grdview
            if (dtHorarioDisp.Rows.Count > 0)
            {
                gvHorarioDisponible.DataSource = dtHorarioDisp;
                gvHorarioDisponible.DataBind();

                //------------------------------------------------------
                //ADD_2
                //-------
                // capacidad max de asesores
                //---------------------
                if (!string.IsNullOrEmpty(strAsesorCapac.Trim()))
                {
                    strAsesorCapac = strAsesorCapac.Trim().Substring(0, strAsesorCapac.Trim().Length - 1);
                    foreach (string strIndex in strAsesorCapac.Split('|'))
                    {
                        string strIdAsesor = strIndex.Replace("==", "");

                        foreach (GridViewRow _oRow in gvHorarioDisponible.Rows)
                        {
                            DataKey oDatos = gvHorarioDisponible.DataKeys[_oRow.RowIndex];

                            if (oDatos.Values["ID_ASESOR"].ToString().Equals(strIdAsesor))
                            {
                                _oRow.BackColor = System.Drawing.Color.FromArgb(178, 213, 247);
                            }
                        }
                    }
                }
                //------------------------

                lblFlgHorario2.Visible = false;
                pnlLeyenda.Visible = true;
                pnlHorarioDisponible.Visible = true;

                ddlHoraInicio2.Enabled = true;
                ddlHoraFin2.Enabled = true;
            }
            else
            {
                gvHorarioDisponible.DataSource = null;
                gvHorarioDisponible.DataBind();

                lblFlgHorario2.Visible = true;
                lblFlgHorario2.Text = "No hay Horario disponible para el rango de fecha seleccionado.";
                pnlLeyenda.Visible = false;
                pnlHorarioDisponible.Visible = false;

                ddlHoraInicio2.Enabled = false;
                ddlHoraFin2.Enabled = false;
            }

            dtHorarioDisp = null;

            if (swHorasT)
            {
                cargarComboHorarioTaller(ddlHoraInicio2, dtHoraI_ddl, dtHoraF_ddl, _INTERVALO, "HI");
                cargarComboHorarioTaller(ddlHoraFin2, dtHoraI_ddl, dtHoraF_ddl, _INTERVALO, "HF");

                hfHoraIni2.Value = ddlHoraInicio2.SelectedValue.ToString();
                hfHoraFin2.Value = ddlHoraFin2.SelectedValue.ToString();

                ddlHoraInicio2.Enabled = (gvHorarioDisponible.Rows.Count > 0) ? true : false;
                ddlHoraFin2.Enabled = (gvHorarioDisponible.Rows.Count > 0) ? true : false;

                imbFecha2.Enabled = true;
                imbFecha1.Enabled = true;
            }
            else
            {
                ddlHoraInicio2.Enabled = false;
                ddlHoraFin2.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }

    private void ActualizarRangoHorario()
    {
        DateTime dtHIRango = Convert.ToDateTime(ddlHoraInicio1.SelectedValue.ToString());
        DateTime dtHFRango = Convert.ToDateTime(ddlHoraFin1.SelectedValue.ToString());

        Int32 intCol = 0;
        Int32 intWIDTH = 0;
        string strHoraV = hf_HORAS_VACIAS.Value.ToString();

        foreach (DataControlField oColumn in gvHorarioReserva.Columns)
        {
            string strTipo1 = oColumn.GetType().ToString();

            if (oColumn.GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField"))
            {
                DateTime dtHoraC = Convert.ToDateTime(((ButtonField)oColumn).DataTextField.Substring(((ButtonField)oColumn).DataTextField.IndexOf('_') + 1).Insert(2, ":"));

                gvHorarioReserva.Columns[intCol].Visible = !(dtHoraC < dtHIRango || dtHoraC >= dtHFRango);
                gvHorarioReserva.Columns[intCol].Visible = (strHoraV.Contains(((ButtonField)oColumn).DataTextField)) ? false : gvHorarioReserva.Columns[intCol].Visible;
                intWIDTH += (dtHoraC < dtHIRango || dtHoraC >= dtHFRango) ? 0 : ((strHoraV.Contains(((ButtonField)oColumn).DataTextField)) ? 0 : _WIDTH_COL_HORAS);
            }

            intCol += 1;
        }

        gvHorarioReserva.Width = _WIDTH_COL_TALLER + _WIDTH_COL_ASESOR + _WIDTH_COL_TCA + _WIDTH_COL_TCE + intWIDTH;
    }
    private void ActualizarRangoHorarioDisponible()
    {
        DateTime dtHoraI2R = Convert.ToDateTime(ddlHoraInicio2.SelectedValue.ToString());
        DateTime dtHoraF2R = Convert.ToDateTime(ddlHoraFin2.SelectedValue.ToString());


        for (Int32 i = 0; i <= gvHorarioDisponible.Rows.Count - 1; i++)
        {
            DateTime dtHora = Convert.ToDateTime(gvHorarioDisponible.Rows[i].Cells[3].Text.ToString());
            //if (dtHoraI2R <= dtHora & dtHora <= dtHoraF2R)//@005
            if (dtHoraI2R <= dtHora & dtHora < dtHoraF2R)//@005
            {
                gvHorarioDisponible.Rows[i].Visible = true;
            }
            else
            {
                gvHorarioDisponible.Rows[i].Visible = false;
            }
        }
    }
    private ImageButton getControl(Int32 fila, Int32 col)
    {

        ImageButton imbCelda = null;
        DataControlFieldCell CTR = ((DataControlFieldCell)gvHorarioReserva.Rows[fila].Controls[col]);
        foreach (Control _ctr in CTR.Controls)
        {
            if (_ctr.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
            {
                imbCelda = (ImageButton)_ctr;
            }
        }
        return imbCelda;
    }



    private bool escogioReservacion()
    {

        for (Int32 f = 0; f <= gvHorarioReserva.Rows.Count - 1; f++)
        {
            for (Int32 c = 4; c <= gvHorarioReserva.Columns.Count - 1; c++)
            {
                ImageButton imgB1 = getControl(f, c);
                if (imgB1.ImageUrl == imgURL_HORA_SEPARADA)
                {
                    return true;
                    //break;
                }
            }
        }

        return false;

    }
    private bool escogioReservacionHD()
    {


        for (Int32 f = 0; f <= gvHorarioDisponible.Rows.Count - 1; f++)
        {
            DataControlFieldCell CTR1 = (DataControlFieldCell)(gvHorarioDisponible.Rows[f].Controls[6]);
            foreach (Control _ctr in CTR1.Controls)
            {
                if (_ctr.GetType().ToString() == "System.Web.UI.WebControls.DataControlImageButton")
                {
                    ImageButton imbCelda = (ImageButton)_ctr;
                    if (imbCelda.ImageUrl == imgURL_HORA_SEPARADA)
                    {
                        return true;
                        //break;
                    }
                }
            }

        }
        return false;
    }

    protected void btnRegresarPanelreprog_Click(object sender, ImageClickEventArgs e)
    {
        MpeReprogramacion.Hide();
    }

    #endregion

    //********************************************************************************************************************************************************************************************

    #region "---------- COLA DE ESPERA ------------------"

    protected void gv_Cola_Espera_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRegresarColaEspera.Enabled = false;
        mpColaEspera.Hide();
        int intCita = Convert.ToInt32(gv_Cola_Espera.DataKeys[gv_Cola_Espera.SelectedRow.RowIndex].Value);

        //Registrar Cola Espera
        string noPais = (oParametros.SRC_Pais == 1) ? "PE" : "CH";

        oCitasBE = new CitasBE();
        CitasBL oCitasBL = new CitasBL();

        oCitasBE.nid_cita = intCita;
        oCitasBE.No_pais = noPais;
        oCitasBE.nid_asesor = Convert.ToInt32(ViewState["id_asesor"].ToString());
        oCitasBE.co_usuario_crea = Profile.UserName;
        oCitasBE.co_usuario_red = Profile.UsuarioRed;
        oCitasBE.no_estacion_red = Profile.Estacion;

        string oResp = oCitasBL.AsignarClienteColaEspera(oCitasBE);

        if (oResp.Trim().Equals("OK"))
        {
            return;
        }
        else //if (oResp.StartsWith(noPais))
        {
            // [PERU] - [CHILE]
            oCitasBEList = new CitasBEList();
            oCitasBEList = oCitasBL.GETListarDatosCita(oCitasBE);

            if (oCitasBEList.Count == 0) return;

            //>>------ ASIGNACION  ----- >>

            //EnviarCorreo_Cliente(oCitasBEList[0], Parametros.EstadoCita.REASIGNADA);
            //EnviarCorreo_Asesor(oCitasBEList[0], Parametros.EstadoCita.REASIGNADA);
        }


        EstadoBotones(false, false, true);
        lbl_mensajebox.Text = _MSG_PREGUNTA.Replace("{estado}", "Anular");
        lbl_mensajebox.Text = "La Cita ha sido Asignada al Cliente.";
        hf_ESTADO_CITA.Value = "ColaEspera";
        popup_msgbox_confirm.Show();

        btnRegresarColaEspera.Enabled = true;

    }
    protected void gv_Cola_Espera_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        CitasBE objBE = new CitasBE();
        CitasBL objBL = new CitasBL();

        try
        {
            objBE.nid_cita = Convert.ToInt32(hf_ID_CITA.Value.ToString());

            gv_Cola_Espera.DataSource = objBL.GETListarClientesEnColaEspera(objBE);
            gv_Cola_Espera.DataBind();
        }
        catch //(Exception ex)
        {
        }
    }

    protected void btnRegresarColaEspera_Click(object sender, ImageClickEventArgs e)
    {
        int intCita = Convert.ToInt32(gv_Cola_Espera.DataKeys[0].Value);
        mpColaEspera.Hide();
    }


    #endregion

    //********************************************************************************************************************************************************************************************

    #region "------------- BOTONES DE ACCION ---------------"

    //1 - Busqueda de Citas
    protected void btnBuscarCita_Click(object sender, ImageClickEventArgs e)
    {
        //return;
        if (!IsDate(txt_bus_fecreg1.Text))
        {
            if (txt_bus_fecreg1.Text.Length.ToString() != "0")
            {
                SRC_MsgInformacion("Ingrese Fecha Valida");
                Limpiar();
                return;
            }
        }
        if (!IsDate(txt_bus_fecreg2.Text))
        {
            if (txt_bus_fecreg2.Text.Length.ToString() != "0")
            {
                SRC_MsgInformacion("Ingrese Fecha Registro Valida");
                Limpiar();
                return;
            }
        }
        if (!IsDate(txt_bus_feccita1.Text))
        {
            if (txt_bus_feccita1.Text.Length.ToString() != "0")
            {
                SRC_MsgInformacion("Ingrese Fecha Cita Valida");
                Limpiar();
                return;
            }
        }
        if (!IsDate(txt_bus_feccita2.Text))
        {
            if (txt_bus_feccita2.Text.Length.ToString() != "0")
            {
                SRC_MsgInformacion("Ingrese Fecha Cita Valida");
                Limpiar();
                return;
            }
        }
        if (!IsDate(txt_bus_hr1.Text))
        {
            if (txt_bus_hr1.Text.Length.ToString() != "0")
            {
                SRC_MsgInformacion("Ingrese Hora Cita Valida");
                Limpiar();
                return;
            }
        }
        if (!IsDate(txt_bus_hr2.Text))
        {
            if (txt_bus_hr2.Text.Length.ToString() != "0")
            {
                SRC_MsgInformacion("Ingrese Hora Cita Valida");
                Limpiar();
                return;
            }
        }

        if (hid_indvalidador.Value.ToString().Trim() == "0")
        {
            oAdminCitaBE.cod_reserva_cita = txt_bus_codreserva.Text.Trim();
            oAdminCitaBE.coddpto = ddl_bus_departamento.SelectedValue.ToString().Trim();
            oAdminCitaBE.codprov = ddl_bus_provincia.SelectedValue.ToString().Trim();
            oAdminCitaBE.coddist = ddl_bus_distrito.SelectedValue.ToString().Trim();
            oAdminCitaBE.nid_ubica = int.Parse(ddl_bus_puntored.SelectedValue.ToString().Trim());
            oAdminCitaBE.nid_taller = int.Parse(ddl_bus_taller.SelectedValue.ToString().Trim());
            oAdminCitaBE.AsesorServicio = txt_bus_asesorservicio.Text.Trim();
            oAdminCitaBE.Estadoreserva = ddl_bus_estreserva.SelectedValue.ToString();
            oAdminCitaBE.IndPendiente = cboIndPendiente.SelectedValue.ToString().Trim();
            oAdminCitaBE.fecreg1 = txt_bus_fecreg1.Text.Trim();
            oAdminCitaBE.fecreg2 = txt_bus_fecreg2.Text.Trim();
            oAdminCitaBE.feccita1 = txt_bus_feccita1.Text.Trim();
            oAdminCitaBE.feccita2 = txt_bus_feccita2.Text.Trim();
            oAdminCitaBE.horacita1 = txt_bus_hr1.Text.Trim();
            oAdminCitaBE.horacita2 = txt_bus_hr2.Text.Trim();
            string[] arr_placa = new string[2];
            arr_placa = txt_bus_placapatente.Text.Trim().Split('-');
            if (arr_placa.Length == 2) { oAdminCitaBE.nu_placa = arr_placa.GetValue(0).ToString() + arr_placa.GetValue(1).ToString(); }
            else
            {
                if (arr_placa.Length == 1) { oAdminCitaBE.nu_placa = arr_placa.GetValue(0).ToString(); }
            }
            oAdminCitaBE.nid_marca = int.Parse(ddl_bus_marca.SelectedValue.ToString().Trim());
            oAdminCitaBE.nid_modelo = int.Parse(ddl_bus_modvehiculo.SelectedValue.ToString().Trim());
            oAdminCitaBE.co_tipo_documento = ddl_bus_tipodoc.SelectedValue.ToString().Trim();
            oAdminCitaBE.nu_documento = txt_bus_nrodoc.Text.Trim();
            oAdminCitaBE.no_cliente = txt_bus_nombres.Text.Trim();
            oAdminCitaBE.no_apellidos = txt_bus_apellidos.Text.Trim();
            oAdminCitaBE.paginacion = 0;

            Session["AdminCitaBE_FILTROS"] = oAdminCitaBE;
            BuscarCitas(oAdminCitaBE);
        }
    }
    //2 - ver el detalle de la cita
    protected void btnVerDetalle_Click(object sender, ImageClickEventArgs e)
    {
        oAdminCitaBE.grid_nid_cita = hf_DETALLE.Value.Split('=').GetValue(0).ToString();
        oAdminCitaBE.grid_nid_estado = hf_DETALLE.Value.Split('=').GetValue(1).ToString();
        oAdminCitaBE.grid_no_dias_validos = hf_DETALLE.Value.Split('=').GetValue(3).ToString();
        oAdminCitaBE.grid_nid_servicioCita = hf_DETALLE.Value.Split('=').GetValue(4).ToString();
        oAdminCitaBE.grid_nid_modelo = hf_DETALLE.Value.Split('=').GetValue(5).ToString();
        Session["oAdminCitaBE"] = oAdminCitaBE;

        Response.Redirect("SRC_AdmCitas_VerDetalle.aspx");
    }
    //3 - Actualizar el estado de la Cita 
    protected void btnActualizarEstado_Click(object sender, ImageClickEventArgs e)
    {
        AdminCitaBEList AdminCitaBELista = (AdminCitaBEList)(ViewState["oAdminCitaBEList"]);
        oAdminCitaBEList = new AdminCitaBEList();

        oAdminCitaBE = new AdminCitaBE();
        AdminCitaBE oEntidad;

        string EstVeri = "";

        foreach (GridViewRow row in gv_admcitas.Rows)
        {
            if (((CheckBox)(row.FindControl("chk_opcion"))).Checked == true)
            {
                oEntidad = new AdminCitaBE();
                EstVeri = gv_admcitas.DataKeys[row.RowIndex].Values[2].ToString().Trim();

                if (EstVeri.Trim().Equals("SI"))
                {
                    if (AdminCitaBELista[row.RowIndex].grid_nid_cita.ToString().Trim().Equals(gv_admcitas.DataKeys[row.RowIndex].Values[0].ToString().Trim()))
                    {
                        oEntidad.grid_nid_cita = AdminCitaBELista[row.RowIndex].grid_nid_cita.ToString().Trim();
                        oEntidad.grid_nid_estado = AdminCitaBELista[row.RowIndex].grid_nid_estado.ToString().Trim();
                        oEntidad.grid_cod_reserva_cita = AdminCitaBELista[row.RowIndex].grid_cod_reserva_cita.ToString().Trim();
                        oEntidad.grid_FE_HORA_REG = AdminCitaBELista[row.RowIndex].grid_FE_HORA_REG.ToString().Trim();
                        oEntidad.grid_FECHA_CITA = AdminCitaBELista[row.RowIndex].grid_FECHA_CITA.ToString().Trim();
                        oEntidad.grid_HORA_CITA = AdminCitaBELista[row.RowIndex].grid_HORA_CITA.ToString().Trim();
                        oEntidad.grid_ESTADO_CITA = AdminCitaBELista[row.RowIndex].grid_ESTADO_CITA.ToString().Trim();
                        oEntidad.grid_Departamento = AdminCitaBELista[row.RowIndex].grid_Departamento.ToString().Trim();
                        oEntidad.grid_Provincia = AdminCitaBELista[row.RowIndex].grid_Provincia.ToString().Trim();
                        oEntidad.grid_Distrito = AdminCitaBELista[row.RowIndex].grid_Distrito.ToString().Trim();
                        oEntidad.grid_Punto_RED = AdminCitaBELista[row.RowIndex].grid_Punto_RED.ToString().Trim();
                        oEntidad.grid_Taller = AdminCitaBELista[row.RowIndex].grid_Taller.ToString().Trim();
                        oEntidad.grid_AsesorServicio = AdminCitaBELista[row.RowIndex].grid_AsesorServicio.ToString().Trim();
                        oEntidad.grid_PlacaPatente = AdminCitaBELista[row.RowIndex].grid_PlacaPatente.ToString().Trim();
                        oEntidad.grid_NomCliente = AdminCitaBELista[row.RowIndex].grid_NomCliente.ToString().Trim();
                        oEntidad.grid_ApeCliente = AdminCitaBELista[row.RowIndex].grid_ApeCliente.ToString().Trim();
                        oEntidad.grid_IndPendiente = AdminCitaBELista[row.RowIndex].grid_IndPendiente.ToString().Trim();
                        oEntidad.grid_TelefonoCliente = AdminCitaBELista[row.RowIndex].grid_TelefonoCliente.ToString().Trim();
                        oEntidad.grid_EmailCliente = AdminCitaBELista[row.RowIndex].grid_EmailCliente.ToString().Trim();

                        oAdminCitaBEList.Add(oEntidad);
                    }
                }
                else
                {
                    SRC_MsgInformacion("Seleccione citas donde el flag Ind. Pendiente Datos sea Si.");
                    return;
                }
            }
        }

        if (oAdminCitaBEList.Count > 0)
        {
            Session["objEntActuLista"] = oAdminCitaBEList;
            Response.Redirect("SRC_AdminCitas_ActuEst.aspx");
        }
    }
    //4 - Anular la Cita
    protected void btnAnularCita_Click(object sender, ImageClickEventArgs e)
    {
        EstadoBotones(true, true, false);
        lbl_mensajebox.Text = _MSG_PREGUNTA.Replace("{estado}", "Anular");
        hf_ESTADO_CITA.Value = "Anular";
        popup_msgbox_confirm.Show();
    }
    //5 - Confirmar la Cita
    protected void btnConfirmarCita_Click(object sender, ImageClickEventArgs e)
    {
        int cont = 0;
        foreach (GridViewRow row in gv_admcitas.Rows)
        {
            if (((CheckBox)(row.FindControl("chk_opcion"))).Checked == true)
            {
                cont += 1;
                hf_ROW_INDEX.Value = row.RowIndex.ToString();
            }
        }
        if (cont >= 1)
        {
            EstadoBotones(true, true, false);
            lbl_mensajebox.Text = "¿ Esta usted seguro de Confirmar " + ((cont == 1) ? "la" : "las") + " Cita ?.";
            hf_ESTADO_CITA.Value = "Confirmar";
            popup_msgbox_confirm.Show();
        }
        else
        {
            SRC_MsgInformacion("Debe seleccionar por lo menos un Registro.");
        }
    }
    //6 - Reprogramar la Cita
    protected void btnReprogramarCita_Click(object sender, ImageClickEventArgs e)
    {
        int cont = 0;

        oCitasBE = new CitasBE();
        oCitasBEList = new CitasBEList();

        oAdminCitaBE = new AdminCitaBE();
        oAdminCitaBEList = new AdminCitaBEList();

        CitasBL oCitasBL = new CitasBL();


        foreach (GridViewRow row in gv_admcitas.Rows)
        {
            if (((CheckBox)(row.FindControl("chk_opcion"))).Checked == true)
            {
                cont += 1;
            }
        }

        if (cont == 1)
        {
            foreach (GridViewRow row in gv_admcitas.Rows)
            {
                if (((CheckBox)(row.FindControl("chk_opcion"))).Checked == true)
                {
                    string _Estado_ = "";


                    oCitasBE.nid_cita = Convert.ToInt32(gv_admcitas.DataKeys[row.RowIndex].Values[0].ToString());

                    _Estado_ = oCitasBL.VerificarEstadoCita(oCitasBE);

                    if (_Estado_.Equals("EC07") || _Estado_.Equals("EC03") || _Estado_.Equals("EC06") || _Estado_.Equals("EC08"))
                    {
                        SRC_MsgInformacion("No se Puede Reprogramar. Debido a que se encuentra en Estado " + gv_admcitas.Rows[row.RowIndex].Cells[5].Text);
                        return;
                    }

                    hf_ROW_INDEX.Value = row.RowIndex.ToString();

                    //CHECK THIS
                    oAdminCitaBE.grid_nid_cita = gv_admcitas.DataKeys[row.RowIndex].Values[0].ToString();
                    oAdminCitaBE.grid_nid_estado = gv_admcitas.DataKeys[row.RowIndex].Values[1].ToString();
                    oAdminCitaBE.grid_no_dias_validos = gv_admcitas.DataKeys[row.RowIndex].Values[3].ToString();
                    Session["oAdminCitaBE"] = oAdminCitaBE;

                    //-------------------------------------------------------
                    oAdminCitaBEList = oAdminCitasBL.GETListaAdminCitasDetalle(oAdminCitaBE);

                    lblGridTipoServicio.Text = oAdminCitaBEList[0].DET_TipoServicio.ToString().Trim();
                    lblGridServicio.Text = oAdminCitaBEList[0].DET_Servicio.ToString().Trim();
                    lblTextoGridPlaca.Text = oParametros.N_Placa;
                    lblGridPlaca.Text = oAdminCitaBEList[0].DET_Placa.ToString().Trim();
                    lblGridMarca.Text = oAdminCitaBEList[0].DET_Marca.ToString().Trim();
                    lblGridModelo.Text = oAdminCitaBEList[0].DET_Modelo.ToString().Trim();

                    //-------------------------------------------------------
                    hf_ESTADO_CITA.Value = "Reprogramar";

                    lblSeleccion1.Text = string.Empty;
                    lblSeleccion2.Text = string.Empty;

                    PanelUno.Visible = true;
                    Paneldos.Visible = false;

                    //---ReprogramarCita();

                    if (ConfigurationManager.AppSettings["CambiarTaller"].ToString().Equals("1"))
                    {
                        LimpiarFiltros(5);

                        oCitasBE = new CitasBE();

                        //Get nid_ubica
                        oCitasBE.nid_cita = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[0].ToString());
                        CitasBEList lstCita = oCitasBL.GETListarDatosCita(oCitasBE);

                        //Carga: Punto de Red
                        //---------------------
                        oCitasBE = new CitasBE();
                        oCitasBEList = new CitasBEList();

                        oCitasBE.nid_Servicio = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[4].ToString());
                        oCitasBE.nid_modelo = Convert.ToInt32(gv_admcitas.DataKeys[Convert.ToInt32(hf_ROW_INDEX.Value.ToString())].Values[5].ToString());
                        oCitasBE.coddpto = "0";
                        oCitasBE.codprov = "0";
                        oCitasBE.coddis = "0";
                        oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

                        oCitasBEList = oCitasBL.Listar_PuntosRed(oCitasBE);

                        ddlPuntoRed.Items.Clear();
                        if (oCitasBEList.Count > 0)
                        {
                            ddlPuntoRed.DataSource = oCitasBEList;
                            ddlPuntoRed.DataTextField = "no_ubica";
                            ddlPuntoRed.DataValueField = "nid_ubica";
                            ddlPuntoRed.DataBind();
                            ddlPuntoRed.Items.Insert(0, new ListItem(_SELECCIONAR, "0"));
                            //----
                            ddlPuntoRed.SelectedValue = lstCita[0].nid_ubica.ToString();
                            ddlPuntoRed_SelectedIndexChanged(this, null);
                        }
                        else
                        {
                            ddlPuntoRed.Items.Insert(0, new ListItem(_SELECCIONAR, "0"));
                        }


                    }
                    else
                    {
                        string strFechaHabil = SRC_FECHA_HABIL().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);

                        hf_FECHA_HABIL.Value = strFechaHabil;
                        txtFecha.Text = strFechaHabil;
                        hfFecha.Value = strFechaHabil;

                        ConsultarHorarioReserva();
                    }

                    MpeReprogramacion.Show();
                }
            }
        }
        else
        {
            SRC_MsgInformacion("Debe seleccionar solo un registro.");
        }
    }
    //7 - Reasignar la Cita
    protected void btnReasignarCita_Click(object sender, ImageClickEventArgs e)
    {
        AdminCitaBEList AdminCitaBELista = (AdminCitaBEList)(ViewState["oAdminCitaBEList"]);
        AdminCitaBE oEntidad;

        oAdminCitaBEList = new AdminCitaBEList();

        int cont_taller = 0;
        string v_taller = "";
        foreach (GridViewRow row in gv_admcitas.Rows)
        {
            if (((CheckBox)(row.FindControl("chk_opcion"))).Checked == true)
            {
                Int32 IndiceGrid = (gv_admcitas.PageIndex * gv_admcitas.PageSize) + row.RowIndex;
                if (cont_taller == 0)
                {
                    v_taller = AdminCitaBELista[IndiceGrid].grid_Taller.ToString().Trim();
                }
                else
                {
                    if (!v_taller.Trim().Equals(AdminCitaBELista[IndiceGrid].grid_Taller.ToString().Trim()))
                    {
                        SRC_MsgInformacion("No se puede reasignar citas de diferentes talleres");
                        return;
                    }
                }
                cont_taller += 1;
            }
        }

        foreach (GridViewRow row in gv_admcitas.Rows)
        {
            if (((CheckBox)(row.FindControl("chk_opcion"))).Checked == true)
            {
                Int32 IndiceGrid = (gv_admcitas.PageIndex * gv_admcitas.PageSize) + row.RowIndex;
                string _Estado_ = gv_admcitas.Rows[row.RowIndex].Cells[5].Text;
                if (_Estado_.Equals("Anulada") || _Estado_.Equals("Atendida") || _Estado_.Equals("Cola de Espera") || _Estado_.Equals("Reasignada"))
                {
                    SRC_MsgInformacion("No se Puede Reasignar. Debido a que se encuentra en Estado " + _Estado_);
                    return;
                }
                oEntidad = new AdminCitaBE();

                if (AdminCitaBELista[IndiceGrid].grid_nid_cita.ToString().Trim().Equals(gv_admcitas.DataKeys[row.RowIndex].Values[0].ToString().Trim()))
                {
                    oEntidad.grid_nid_cita = AdminCitaBELista[IndiceGrid].grid_nid_cita.ToString().Trim();
                    oEntidad.grid_nid_estado = AdminCitaBELista[IndiceGrid].grid_nid_estado.ToString().Trim();
                    oEntidad.grid_cod_reserva_cita = AdminCitaBELista[IndiceGrid].grid_cod_reserva_cita.ToString().Trim();
                    oEntidad.grid_FE_HORA_REG = AdminCitaBELista[IndiceGrid].grid_FE_HORA_REG.ToString().Trim();
                    oEntidad.grid_FECHA_CITA = AdminCitaBELista[IndiceGrid].grid_FECHA_CITA.ToString().Trim();
                    oEntidad.grid_HORA_CITA = AdminCitaBELista[IndiceGrid].grid_HORA_CITA.ToString().Trim();
                    oEntidad.grid_ESTADO_CITA = AdminCitaBELista[IndiceGrid].grid_ESTADO_CITA.ToString().Trim();
                    oEntidad.grid_Departamento = AdminCitaBELista[IndiceGrid].grid_Departamento.ToString().Trim();
                    oEntidad.grid_Provincia = AdminCitaBELista[IndiceGrid].grid_Provincia.ToString().Trim();
                    oEntidad.grid_Distrito = AdminCitaBELista[IndiceGrid].grid_Distrito.ToString().Trim();
                    oEntidad.grid_Punto_RED = AdminCitaBELista[IndiceGrid].grid_Punto_RED.ToString().Trim();
                    oEntidad.grid_Taller = AdminCitaBELista[IndiceGrid].grid_Taller.ToString().Trim();
                    oEntidad.grid_AsesorServicio = AdminCitaBELista[IndiceGrid].grid_AsesorServicio.ToString().Trim();
                    oEntidad.grid_PlacaPatente = AdminCitaBELista[IndiceGrid].grid_PlacaPatente.ToString().Trim();
                    oEntidad.grid_NomCliente = AdminCitaBELista[IndiceGrid].grid_NomCliente.ToString().Trim();
                    oEntidad.grid_ApeCliente = AdminCitaBELista[IndiceGrid].grid_ApeCliente.ToString().Trim();
                    oEntidad.grid_IndPendiente = AdminCitaBELista[IndiceGrid].grid_IndPendiente.ToString().Trim();
                    oEntidad.grid_TelefonoCliente = AdminCitaBELista[IndiceGrid].grid_TelefonoCliente.ToString().Trim();
                    oEntidad.grid_EmailCliente = AdminCitaBELista[IndiceGrid].grid_EmailCliente.ToString().Trim();
                    oEntidad.grid_nid_tallerCita = AdminCitaBELista[IndiceGrid].grid_nid_tallerCita.ToString().Trim();
                    oEntidad.grid_nid_servicioCita = AdminCitaBELista[IndiceGrid].grid_nid_servicioCita.ToString().Trim();
                    oEntidad.grid_IntervaloTaller = AdminCitaBELista[IndiceGrid].grid_IntervaloTaller.ToString().Trim();
                    oEntidad.grid_Id_Asesor = AdminCitaBELista[IndiceGrid].grid_Id_Asesor.ToString().Trim();
                    oEntidad.grid_HORA_CITA_FIN = AdminCitaBELista[IndiceGrid].grid_HORA_CITA_FIN.ToString().Trim();
                    oEntidad.grid_nid_modelo = AdminCitaBELista[IndiceGrid].grid_nid_modelo.ToString().Trim();
                    oEntidad.grid_no_dias_validos = AdminCitaBELista[IndiceGrid].grid_no_dias_validos.ToString().Trim();
                    oEntidad.grid_NumDocumento = AdminCitaBELista[IndiceGrid].grid_NumDocumento.ToString().Trim();

                    oAdminCitaBEList.Add(oEntidad);
                }
            }
        }

        if (oAdminCitaBEList.Count > 0)
        {
            Session["objEntActuLista"] = oAdminCitaBEList;
            Response.Redirect("SRC_AdminCitas_ReasignarCita.aspx");
        }
    }
    //8 - Actualizar los datos del vehiculo
    protected void btnActualizarVehiculo(object sender, ImageClickEventArgs e)
    {
        int cont = 0;
        foreach (GridViewRow row in gv_admcitas.Rows)
        {
            if (((CheckBox)(row.FindControl("chk_opcion"))).Checked == true)
            {
                cont += 1;
            }
        }

        if (cont == 1)
        {
            foreach (GridViewRow row in gv_admcitas.Rows)
            {
                if (((CheckBox)(row.FindControl("chk_opcion"))).Checked == true)
                {
                    oAdminCitaBE.grid_nid_cita = gv_admcitas.DataKeys[row.RowIndex].Values[0].ToString();
                    oAdminCitaBE.grid_nid_estado = gv_admcitas.DataKeys[row.RowIndex].Values[1].ToString();

                    Session["oAdminCitaBE"] = oAdminCitaBE;

                    Response.Redirect("SRC_AdminCitas_Vehi_Propietario.aspx");
                }
            }
        }
        else
        {
            SRC_MsgInformacion("Debe seleccionar solo un registro.");
        }
    }
    //9 - Limpiar los filtros de busqueda
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txt_bus_codreserva.Text = "";
        ddl_bus_departamento.SelectedIndex = 0;
        ddl_bus_provincia.Items.Clear();
        ddl_bus_provincia.Items.Insert(0, (new ListItem(_TODOS, "0")));
        ddl_bus_distrito.Items.Clear();
        ddl_bus_distrito.Items.Insert(0, (new ListItem(_TODOS, "0")));
        ddl_bus_puntored.Items.Clear();
        ddl_bus_puntored.Items.Insert(0, (new ListItem(_TODOS, "0")));
        ddl_bus_taller.Items.Clear();
        ddl_bus_taller.Items.Insert(0, (new ListItem(_TODOS, "0")));

        txt_bus_asesorservicio.Text = "";
        ddl_bus_estreserva.SelectedIndex = 0;
        cboIndPendiente.SelectedIndex = 0;
        txt_bus_feccita1.Text = "";
        txt_bus_feccita2.Text = "";
        txt_bus_fecreg1.Text = "";
        txt_bus_fecreg2.Text = "";

        txt_bus_placapatente.Text = "";
        ddl_bus_marca.SelectedIndex = 0;
        ddl_bus_modvehiculo.Items.Clear();
        ddl_bus_modvehiculo.Items.Insert(0, (new ListItem(_TODOS, "0")));
        txt_bus_hr1.Text = "";
        txt_bus_hr2.Text = "";

        ddl_bus_tipodoc.SelectedIndex = 0;
        txt_bus_nrodoc.Text = "";
        txt_bus_nombres.Text = "";
        txt_bus_apellidos.Text = "";


        oAdminCitaBEList = new AdminCitaBEList();
        oAdminCitaBEList.Add(new AdminCitaBE());
        ViewState["oAdminCitaBEList"] = oAdminCitaBEList;
        Session["AdminCitaBE_FILTROS"] = null;
        gv_admcitas.DataSource = null;
        gv_admcitas.DataSource = oAdminCitaBEList;
        gv_admcitas.DataBind();
    }
    //10 - Imprimir el listado de citas
    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["oAdminCitaBEList"] != null)
        {
            oAdminCitaBEList = new AdminCitaBEList();
            oAdminCitaBEList = (AdminCitaBEList)ViewState["oAdminCitaBEList"];
            DataTable dtCitas = new DataTable();

            Int32 _size = 40;
            Int32 count = 0;
            System.Text.StringBuilder strHTML = new StringBuilder();
            string strHeader = "";
            string strBody = "";
            string strBreak = "";

            string strStyleTH = "";
            string strStyleTD = "";

            if (oAdminCitaBEList.Count > 0)
            {
                strStyleTH = "border:#000000 1px solid;background-color:#ccc;font-family:Arial;font-size:11px;height:30px;";

                if (oParametros.SRC_Pais.Equals(1))
                {
                    strHeader += "<th style='width: 12%;" + strStyleTH + "' scope=col>Codigo Reserva</th>";
                    strHeader += "<th style='width: 9%;" + strStyleTH + "' scope=col>Fecha Cita</th>";
                    strHeader += "<th style='width: 9%;" + strStyleTH + "' scope=col>Hora Cita</th>";
                    strHeader += "<th style='width: 12%;" + strStyleTH + "' scope=col>Taller</th>";
                    strHeader += "<th style='width: 19%;" + strStyleTH + "' scope=col>Asesor de Servicio</th>";
                    strHeader += "<th style='width: 8%;" + strStyleTH + "' scope=col>Nro. Vin</th>"; //@001
                    strHeader += "<th style='width: 8%;" + strStyleTH + "' scope=col>" + oParametros.N_Placa + "</th>";
                    strHeader += "<th style='width: 23%;" + strStyleTH + "' scope=col>Cliente</th>";
                }
                else
                {
                    strHeader += "<th style='width: 8%;" + strStyleTH + "' scope=col>Codigo Reserva</th>";
                    strHeader += "<th style='width: 8%;" + strStyleTH + "' scope=col>Fecha Cita</th>";
                    strHeader += "<th style='width: 8%;" + strStyleTH + "' scope=col>Hora Cita</th>";
                    strHeader += "<th style='width: 12%;" + strStyleTH + "' scope=col>Taller</th>";
                    strHeader += "<th style='width: 17%;" + strStyleTH + "' scope=col>Asesor de Servicio</th>"; //@001
                    strHeader += "<th style='width: 8%;" + strStyleTH + "' scope=col>Nro. Vin</th>";
                    strHeader += "<th style='width: 7%;" + strStyleTH + "' scope=col>" + oParametros.N_Placa + "</th>";
                    strHeader += "<th style='width: 22%;" + strStyleTH + "' scope=col>Cliente</th>";
                    strHeader += "<th style='width: 9%;" + strStyleTH + "' scope=col>Motivo</th>";// Nombre del Servicio
                    //strHeader += "<th style='width: 11%;" + strStyleTH + "' scope=col>Marca</th>";
                }
                //------------------------------------------------------
                strBreak = "<br/><div style='page-break-after: always;'></div><br/>";
                //------------------------------------------------------

                strHTML.Append("<div>");

                foreach (AdminCitaBE oEntidad in oAdminCitaBEList)
                {
                    if (count == 0)
                    {
                        strHTML.Append("<table id='gvSample' style='border:#000000 1px solid; width: 100%; border-collapse: collapse;' border='1' rules='all' cellSpacing='0'>");
                        strHTML.Append("<tbody>");
                        strHTML.Append(strHeader);
                    }

                    strStyleTD = "border:#000000 1px solid;font-family:Arial;font-size:9px;";

                    strBody = "<tr>";
                    strBody += "<td style='" + strStyleTD + "' align='left' >" + oEntidad.grid_cod_reserva_cita.ToString().Trim() + "</td>";
                    strBody += "<td style='" + strStyleTD + "' align='center' >" + oEntidad.grid_FECHA_CITA.ToString().Trim() + "</td>";
                    strBody += "<td style='" + strStyleTD + "' align='center' >" + FormatoHora(oEntidad.grid_HORA_CITA.ToString().Trim()) + "</td>";
                    strBody += "<td style='" + strStyleTD + "' align='left' >" + oEntidad.grid_Taller.ToString().Trim() + "</td>";
                    strBody += "<td style='" + strStyleTD + "' align='left' >" + oEntidad.grid_AsesorServicio.ToString().Trim() + "</td>";
                    strBody += "<td style='" + strStyleTD + "' align='left' >" + oEntidad.grid_nu_vin.ToString().Trim() + "</td>"; //@001
                    strBody += "<td style='" + strStyleTD + "' align='center' >" + oEntidad.grid_PlacaPatente.ToString().Trim() + "</td>";
                    strBody += "<td style='" + strStyleTD + "' align='left' >" + oEntidad.grid_NomCliente.ToString().Trim() + " " + oEntidad.grid_ApeCliente.ToString().Trim() + "</td>";
                    if (oParametros.SRC_Pais.Equals(2))
                    {
                        strBody += "<td style='" + strStyleTD + "' align='center' >" + oEntidad.grid_no_servicio.ToString().Trim() + "</td>";
                        //strBody += "<td style='" + strStyleTD + "' align='center' >" + oEntidad.grid_no_marca.ToString().Trim() + "</td>";
                    }
                    strBody += "</tr>";

                    count += 1;

                    if (count == _size)
                    {
                        strBody += "</tbody>";
                        strBody += "</table>";
                        strBody += strBreak;
                        count = 0;
                    }
                    strHTML.Append(strBody);
                }

                strHTML.Append("</div>");
                strHTML.Replace("'", "\"");//Formato pra visualizar

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script type=\"text/javascript\">" + "FC_PopupImprimir('{0}');</" + "script>", strHTML), false);
            }
        }
    }

    #endregion

    //********************************************************************************************************************************************************************************************

    #region "----------- MENSAJE SI-NO-ACEPTAR --------------"

    protected void EstadoBotones(bool blnSI, bool blnNO, bool blnAceptar)
    {
        btn_msgboxconfir_si.Visible = blnSI;
        btn_msgboxconfir_no.Visible = blnNO;
        btn_msgboxconfir_aceptar.Visible = blnAceptar;
    }
    protected void btn_msgboxconfir_si_Click(object sender, EventArgs e)
    {

        oCitasBE = new CitasBE();
        oCitasBEList = new CitasBEList();
        CitasBL oCitasBL = new CitasBL();

        try
        {
            string strAccion = string.Empty;
            int oRep = 0;
            int cont = 0;

            strEstadoCita = hf_ESTADO_CITA.Value.ToString();



            if (strEstadoCita.Equals("Confirmar"))//-------------------------------------------------------> CONFIRMAR
            {
                foreach (GridViewRow row in gv_admcitas.Rows)
                {
                    if (((CheckBox)(row.FindControl("chk_opcion"))).Checked == true)
                    {

                        //CHECK THIS
                        oAdminCitaBE.grid_nid_cita = gv_admcitas.DataKeys[row.RowIndex].Values[0].ToString();
                        oAdminCitaBE.grid_nid_estado = gv_admcitas.DataKeys[row.RowIndex].Values[1].ToString();
                        Session["oAdminCitaBE"] = oAdminCitaBE;

                        oCitasBE.nid_Estado = Convert.ToInt32(gv_admcitas.DataKeys[row.RowIndex].Values[1].ToString());
                        oCitasBE.tex_verifica = "EC04";


                        string strEstado = oCitasBL.VerificarCitasCambiaEstado(oCitasBE);
                        int Valor = (string.IsNullOrEmpty(strEstado)) ? 0 : Convert.ToInt32(strEstado);

                        if (Valor == 0)
                        {
                            oCitasBE.nid_cita = Convert.ToInt32(gv_admcitas.DataKeys[row.RowIndex].Values[0].ToString());
                            oCitasBE.nid_Estado = Convert.ToInt32(gv_admcitas.DataKeys[row.RowIndex].Values[1].ToString());
                            //-----------------------------------------
                            oCitasBE.co_usuario_crea = Profile.UserName;
                            oCitasBE.co_usuario_red = Profile.UsuarioRed;
                            oCitasBE.no_estacion_red = Profile.Estacion;

                            oRep = oCitasBL.ConfirmarCita(oCitasBE);

                            cont++;
                        }
                    }
                }

                strAccion = "Confirmado";
                hf_ESTADO_CITA.Value = "Confirmar";
            }

            else if (strEstadoCita.Equals("Anular"))//-------------------------------------------------------> ANULAR
            {

                int sw = 0;
                foreach (GridViewRow row in gv_admcitas.Rows)
                {
                    if (sw == 1) break;

                    if (((CheckBox)(row.FindControl("chk_opcion"))).Checked == true)
                    {
                        //CHECK THIS
                        oAdminCitaBE.grid_nid_cita = gv_admcitas.DataKeys[row.RowIndex].Values[0].ToString();
                        oAdminCitaBE.grid_nid_estado = gv_admcitas.DataKeys[row.RowIndex].Values[1].ToString();
                        Session["oAdminCitaBE"] = oAdminCitaBE;

                        oCitasBE.nid_cita = Convert.ToInt32(gv_admcitas.DataKeys[row.RowIndex].Values[0].ToString());

                        CitasBEList oDatos = oCitasBL.GETListarDatosCita(oCitasBE);
                        ViewState["id_asesor"] = oDatos[0].Nid_usuario.ToString();

                        string strEstado = oCitasBL.VerificarEstadoCita(oCitasBE);



                        if (!strEstado.Equals("EC07"))
                        {
                            //------------------- VERIFICAR CLIENTES EN COLA DE ESPERA ---------------------------

                            hf_ID_CITA.Value = oCitasBE.nid_cita.ToString();

                            gv_Cola_Espera.DataSource = oCitasBL.GETListarClientesEnColaEspera(oCitasBE);
                            gv_Cola_Espera.DataBind();
                        }

                        oCitasBE.nid_Estado = Convert.ToInt32(gv_admcitas.DataKeys[row.RowIndex].Values[1].ToString());
                        oCitasBE.tex_verifica = "EC03";

                        string strCambio = oCitasBL.VerificarCitasCambiaEstado(oCitasBE);
                        int Valor = (string.IsNullOrEmpty(strCambio)) ? 0 : Convert.ToInt32(strCambio);



                        if (Valor == 0)
                        {
                            oCitasBE.nid_cita = Convert.ToInt32(gv_admcitas.DataKeys[row.RowIndex].Values[0].ToString());
                            oCitasBE.nid_Estado = Convert.ToInt32(gv_admcitas.DataKeys[row.RowIndex].Values[1].ToString());

                            //-----------------------------------------
                            oCitasBE.co_usuario_crea = Profile.UserName;
                            oCitasBE.co_usuario_red = Profile.UsuarioRed;
                            oCitasBE.no_estacion_red = Profile.Estacion;

                            oRep = oCitasBL.AnularCita(oCitasBE);

                            oCitasBEList = new CitasBEList();
                            oCitasBEList = oCitasBL.GETListarDatosCita(oCitasBE);
                            if (oCitasBEList.Count == 0) return;


                            //>>------ ANULACION ----- >>

                            //if (!strEstado.Equals("EC07")) EnviarCorreo_Cliente(oCitasBEList[0], Parametros.EstadoCita.ANULADA);
                            //if (oParametros.SRC_Pais.Equals(2)) EnviarCorreo_Asesor(oCitasBEList[0], Parametros.EstadoCita.ANULADA);

                            sw = 1;
                            cont = 1;
                        }

                        break;
                    }
                }

                strAccion = "Anulado";
                hf_ESTADO_CITA.Value = "Anular";

            }
            else
            {
                return;
            }

            //>>> RESULTADO 
            //----------------------
            popup_msgbox_confirm.Hide();

            lbl_mensajebox.Text = _MSG_RESPUESTA;
            EstadoBotones(false, false, true);
            popup_msgbox_confirm.Show();


            if (cont == 1)
            {
                //lblMensaje.Text = "Se ha " + strAccion + " la cita seleccionada.";
                lbl_mensajebox.Text = _MSG_RESPUESTA.Replace("{estado}", strAccion);
            }
            else if (cont > 1)
            {
                //lblMensaje.Text = "Se han " + strAccion + " " + cont.ToString() + " citas.";
                lbl_mensajebox.Text = "Se han " + strAccion + " " + cont.ToString() + " citas.";
            }
            else
            {
                lbl_mensajebox.Text = "No se han " + strAccion + " ninguna Cita.";
            }

            EstadoBotones(false, false, true);
            lbl_mensajebox.Text = lbl_mensajebox.Text.Replace("{estado}", strEstadoCita);
            popup_msgbox_confirm.Show();

        }

        catch (Exception ex)
        {
            SRC_MsgInformacion(ex.Message.ToString());
        }
    }
    protected void btn_msgboxconfir_no_Click(object sender, EventArgs e)
    {
        popup_msgbox_confirm.Hide();
        lbl_mensajebox.Text = _MSG_PREGUNTA;
    }
    protected void btn_msgboxconfir_aceptar_Click(object sender, EventArgs e)
    {
        popup_msgbox_confirm.Hide();

        string strEstadoCita = hf_ESTADO_CITA.Value.ToString().Trim();

        if (strEstadoCita.Equals("Reprogramar"))
        {
            if (gv_Cola_Espera.Rows.Count > 0)
            {
                if (lbl_mensajebox.Text.Contains("Asignada"))
                {
                    Response.Redirect("SRC_AdmCitas.aspx");
                }
                else
                {
                    mpColaEspera.Show();
                }
            }
            else
            {
                Response.Redirect("SRC_AdmCitas.aspx");
            }
        }
        else if (strEstadoCita.Equals("Anular"))
        {
            if (gv_Cola_Espera.Rows.Count > 0)
            {
                if (lbl_mensajebox.Text.Contains("Asignada"))
                {
                    Response.Redirect("SRC_AdmCitas.aspx");
                }
                else
                {
                    mpColaEspera.Show();
                }
            }
            else
            {
                Response.Redirect("SRC_AdmCitas.aspx");
            }
        }
        else
        {
            Response.Redirect("SRC_AdmCitas.aspx");
        }
    }

    #endregion

}
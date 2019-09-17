using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SGS_Mantenimiento_SGS_Destino_Mantenimiento : PaginaBase
{
    private DestinoBE oDestinoBE;
    public Int32 destinoID;
    public Int32 idZona;
    private String TipoAccesoPagina;
    public Int32 indiceTabOn;
    public String tipoAccion = ConstanteBE.TIPO_AGREGAR;
    public SGS_UserControl_ComboEstado ComboEstado1;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        base.Page_PreInit(sender, e);
        ComboEstado1 = (SGS_UserControl_ComboEstado)Page.LoadControl("../SGS_UserControl/ComboEstado.ascx");
        ComboEstado1.ID = "ComboEstado1";
        ComboEstado1.SkinID = "cboob";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.spanCboEstado1.Controls.Add(ComboEstado1);

        if (!Page.IsPostBack)
        {
            /*CONTROL DE ACCESO A PÁGINA*/
            TipoAccesoPagina = Master.ValidaTipoAccesoPagina(this, CONSTANTE_SEGURIDAD.Destinos);
            ViewState["TipoAccesoPagina"] = TipoAccesoPagina;
            /*FIN CONTROL DE ACCESO A PÁGINA*/
            ViewState["ordenListaZona"] = SortDirection.Descending;
            ViewState["ordenListaVitrina"] = SortDirection.Descending;
            InicializaPagina();
            Int32.TryParse(Request.QueryString["txh1"], out destinoID);
            txhIdDestino.Value = destinoID.ToString();
            if (destinoID > 0)
            {
                InicializaData();
            }
        }
        /*CONTROL DE ACCESO A PÁGINA*/
        TipoAccesoPagina = (String)ViewState["TipoAccesoPagina"];
        if (!TipoAccesoPagina.Equals(CONSTANTE_SEGURIDAD.AccesoEdicion))
        {
            this.btnGrabar.Visible = false;
        }
        /*FIN CONTROL DE ACCESO A PÁGINA*/

        if (ViewState["indiceTabOn"] != null)
        {
            this.tabContMarca.ActiveTabIndex = (Int32)ViewState["indiceTabOn"];
            this.indiceTabOn = (Int32)ViewState["indiceTabOn"];
        }
    }

    #region "Carga de página"
    private void InicializaPagina()
    {
        ComboTipoDestino1.Condicion = ConstanteBE.OBJECTO_TIPO_SELECCIONE;
        this.ComboEstado1.cargarCombo(ConstanteBE.OBJECTO_TIPO_SELECCIONE);
        ComboEstado1.Enabled = false;
        ComboTipoDestino1.CssClass = "cboob";
        ComboEstado1.SelectedValue = "0";
        ComboTipoDestino1.CargarCombo();
        DestinoBL oDestinoBL = new DestinoBL();

        //combo Departamento
        this.ComboDepartamento1.cargarCombo(ConstanteBE.OBJECTO_TIPO_SELECCIONE);
        ComboDepartamento1.CssClass = "cboob";
        //combo Provincia
        this.ComboProvincia1.Condicion = ConstanteBE.OBJECTO_TIPO_SELECCIONE;
        ComboProvincia1.CssClass = "cboob";
        //combo Distrito
        this.ComboDistrito1.Condicion = ConstanteBE.OBJECTO_TIPO_SELECCIONE;
        ComboDistrito1.CssClass = "cboob";

        tdAduana1.Style["display"] = "none";
        tdAduana2.Style["display"] = "none";

        tipoAccion = ConstanteBE.TIPO_AGREGAR;
    }
    private void InicializaData()
    {
        DestinoBL oDestinoBL = new DestinoBL();

        Int32.TryParse(txhIdDestino.Value, out destinoID);
        oDestinoBE = oDestinoBL.ListarById(destinoID);

        ComboTipoDestino1.SelectedValue = oDestinoBE.Tipo_ubicacion;
        txtRuc.Text = oDestinoBE.Nro_ruc;
        txtDescripcion.Text = oDestinoBE.Nom_ubicacion;
        txtNombreCorto.Text = oDestinoBE.Nom_corto_ubicacion;
        txtDireccion.Text = oDestinoBE.Direccion;
        ComboEstado1.SelectedValue = oDestinoBE.Cod_estado;

        ComboDepartamento1.SelectedValue = oDestinoBE.Cod_dpto;
        CargarComboProvincia();
        ComboProvincia1.SelectedValue = oDestinoBE.Cod_provincia;
        CargarComboDistrito();
        ComboDistrito1.SelectedValue = oDestinoBE.Cod_distrito;

        String coCom = oDestinoBE.Tipo_ubicacion;

        tipoAccion = ConstanteBE.TIPO_MODIFICAR;
        ComboEstado1.Enabled = true;

    }

    #endregion

    #region "Metodos Botones"
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        DestinoBL oDestinoBL = new DestinoBL();
        oDestinoBL.ErrorEvent += new DestinoBL.ErrorDelegate(Master.Transaction_ErrorEvent);
        Int32 indicador = 0;
        Int32.TryParse(txhIdDestino.Value, out destinoID);
        if (destinoID > 0)
        {
            CargarEntidadDesdeForm();
            indicador = oDestinoBL.Modificar(oDestinoBE);

            if (indicador == -3)
            {
                JavaScriptHelper.Alert(this, Message.keyErrorGrabarNulo, "");
            }
            else
                if (indicador == -2)
            {
                JavaScriptHelper.Alert(this, Message.KeyRUCExiste, "");
            }
            else
                    if (indicador == -1)
            {
                JavaScriptHelper.Alert(this, Message.keyErrorGrabar, "");
            }
            else if (indicador > 0)
            {
                tipoAccion = ConstanteBE.TIPO_MODIFICAR;
                InicializaData();
                JavaScriptHelper.Alert(this, Message.keyActualizar, "");
            }
        }
        else
        {
            CargarEntidadDesdeForm();
            indicador = oDestinoBL.Insertar(oDestinoBE);

            if (indicador == -3)
            {
                JavaScriptHelper.Alert(this, Message.keyErrorGrabarNulo, "");
            }
            else
                if (indicador == -2)
            {
                JavaScriptHelper.Alert(this, Message.KeyRUCExiste, "");
            }
            else
                    if (indicador == -1)
            {
                JavaScriptHelper.Alert(this, Message.keyErrorGrabar, "");
            }
            else if (indicador > 0)
            {
                tipoAccion = ConstanteBE.TIPO_MODIFICAR;
                txhIdDestino.Value = indicador.ToString();
                InicializaData();
                JavaScriptHelper.Alert(this, Message.keyGrabar, "");
            }
        }
    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        chkAlmacenCampo.Checked.Equals("False");
        Response.Redirect(String.Format("SGS_Destino_Bandeja.aspx"), false);
    }
    private void CargarEntidadDesdeForm()
    {
        if (ViewState["oDestinoBE"] != null)
            oDestinoBE = (DestinoBE)ViewState["oDestinoBE"];
        else
            oDestinoBE = new DestinoBE();
        Int32.TryParse(txhIdDestino.Value, out destinoID);
        oDestinoBE.Id_ubicacion = (Int32)destinoID;
        oDestinoBE.Nom_ubicacion = txtDescripcion.Text;
        oDestinoBE.Nom_corto_ubicacion = this.txtNombreCorto.Text;
        oDestinoBE.Tipo_ubicacion = ComboTipoDestino1.SelectedValue.ToString();
        oDestinoBE.Nro_ruc = txtRuc.Text;
        oDestinoBE.Direccion = txtDireccion.Text;
        oDestinoBE.Cod_dpto = ComboDepartamento1.SelectedValue.ToString();
        oDestinoBE.Cod_provincia = ComboProvincia1.SelectedValue.ToString();
        oDestinoBE.Cod_distrito = ComboDistrito1.SelectedValue.ToString();
        oDestinoBE.Cod_estado = this.ComboEstado1.SelectedValue.ToString();
        oDestinoBE.Cod_usu_crea = Profile.Usuario.CUSR_ID;
        oDestinoBE.Nom_estacion = Profile.Estacion;
        oDestinoBE.Nom_usuario_red = Profile.UsuarioRed;
    }

    protected void ComboDepartamento1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        CargarComboProvincia();
        CargarComboDistrito();
    }
    protected void ComboProvincia1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        CargarComboDistrito();
    }
    private void CargarComboProvincia()
    {
        this.ComboProvincia1.id_Departamento = this.ComboDepartamento1.SelectedValue;
        this.ComboProvincia1.cargarCombo(ConstanteBE.OBJECTO_TIPO_SELECCIONE);
    }
    private void CargarComboDistrito()
    {
        this.ComboDistrito1.id_departamento = this.ComboDepartamento1.SelectedValue;
        this.ComboDistrito1.id_provincia = this.ComboProvincia1.SelectedValue;
        this.ComboDistrito1.cargarCombo(ConstanteBE.OBJECTO_TIPO_SELECCIONE);
    }
    protected void ComboTipo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region "Evento Tab"
    protected void tabContMarca_ActiveTabChanged(object sender, EventArgs e)
    {
        indiceTabOn = this.tabContMarca.ActiveTabIndex;

        ActualizaBotoneria();

        TipoAccesoPagina = (String)ViewState["TipoAccesoPagina"];
        if (!TipoAccesoPagina.Equals(CONSTANTE_SEGURIDAD.AccesoEdicion))
        {
            this.btnGrabar.Visible = false;
        }
        ViewState["indiceTabOn"] = indiceTabOn;
    }

    private void ActualizaBotoneria()
    {
        if (this.indiceTabOn == 0)
        {

        }
    }

    #endregion

}
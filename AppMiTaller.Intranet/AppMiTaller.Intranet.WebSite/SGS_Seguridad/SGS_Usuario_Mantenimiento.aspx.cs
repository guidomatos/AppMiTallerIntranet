using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SGS_Seguridad_SGS_Usuario_Mantenimiento : PaginaBase
{
    #region Atributo y propiedades
    UsuarioBE oUsuarioBE;
    public String tipoAccion = ConstanteBE.TIPO_AGREGAR;
    public Int32 indiceTabOn;
    public Int32 usuarioID;
    private String TipoAccesoPagina;
    public String indExisteClave = "0";//'0' - Ho existe; '1' - Si existe;

    public String ConstTipo = String.Empty;
    public String indRefreshGrilla = String.Empty;
    public String idFilaGrilla = String.Empty;

    public Int32 perfilID;
    /*Perfil*/
    OpcionSeguridadBEList oOpcionSeguridadBEList = new OpcionSeguridadBEList();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        
        SetPaginacion();

        if (!Page.IsPostBack)
        {
            /*CONTROL DE ACCESO A PGINA*/
            TipoAccesoPagina = (Master as Seguridad).ValidaTipoAccesoPagina(this, CONSTANTE_SEGURIDAD.Usuarios);
            ViewState["TipoAccesoPagina"] = TipoAccesoPagina;
            /*FIN CONTROL DE ACCESO A PGINA*/

            ViewState["ordenLista"] = SortDirection.Descending;

            btnRefreshOpciones.Style["display"] = "none";
            Int32.TryParse(Request.QueryString["usuarioID"], out usuarioID);
            this.txhNidUsuario.Value = usuarioID.ToString();
            InicializaPagina();
            CargaUsuario();
        }

        /*CONTROL DE ACCESO A PGINA*/
        TipoAccesoPagina = (String)ViewState["TipoAccesoPagina"];
        /*
         * Si el acceso es diferente a Edicion se tiene que ocualtar todos los botones de Agregar, eliminar y 
         * las acciones de grabar.
         */
        if (!TipoAccesoPagina.Equals(CONSTANTE_SEGURIDAD.AccesoEdicion))
        {
            this.btnGrabar.Visible = false;
        }
        /*FIN CONTROL DE ACCESO A PGINA*/


        usuarioID = (Int32)ViewState["usuarioID"];

        if (usuarioID == 0)
        {
            tipoAccion = ConstanteBE.TIPO_AGREGAR;
        }
        else
        {
            tipoAccion = ConstanteBE.TIPO_MODIFICAR;
        }

        oUsuarioBE = (UsuarioBE)ViewState["oUsuarioBE"];
        indExisteClave = (String)ViewState["indExisteClave"];

    }

    #region "Carga de pagina"

    private void SetPaginacion()
    {
        
    }

    private void InicializaPagina()
    {
        UsuarioBEList oUsuarioListBE = new UsuarioBEList();

        try
        {
            ActualizaTabs(0);
            this.chkUpdateCombos.Style["display"] = "none";

            /*Combo Tipo*/
            this.cboTipoUsuario.Items.Clear();
            TipoTablaDetalleBL oTipoTablaDetalleBL = new TipoTablaDetalleBL();
            oTipoTablaDetalleBL.ErrorEvent += new TipoTablaDetalleBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
            TipoTablaDetalleBEList oTipoTablaDetalleBEList = oTipoTablaDetalleBL.ListarTipoTablaDetalle(ConstanteBE.NID_TABLA_TIPO_USUARIO.ToString(), String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            this.cboTipoUsuario.DataSource = oTipoTablaDetalleBEList;
            this.cboTipoUsuario.DataTextField = "Valor1";
            this.cboTipoUsuario.DataValueField = "Valor2";
            this.cboTipoUsuario.DataBind();
            this.cboTipoUsuario.Items.Insert(0, new ListItem());
            this.cboTipoUsuario.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboTipoUsuario.Items[0].Value = String.Empty;

            /*Combo Perfil*/
            this.cboPerfil.Items.Clear();
            PerfilBL oPerfilBL = new PerfilBL();
            oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
            PerfilBEList oPerfilBEList = oPerfilBL.GetPerfilesBandeja(Profile.Aplicacion, String.Empty, ConstanteBE.FL_ESTADO_ACTIVO, "");

            this.cboPerfil.DataSource = oPerfilBEList;
            this.cboPerfil.DataTextField = "VDEPRF";
            this.cboPerfil.DataValueField = "NID_PERFIL";
            this.cboPerfil.DataBind();
            this.cboPerfil.Items.Insert(0, new ListItem());
            this.cboPerfil.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboPerfil.Items[0].Value = String.Empty;

            /*Combo Punto Venta*/
            this.cboPuntoVenta.Items.Clear();
            this.cboPuntoVenta.Items.Insert(0, new ListItem());
            this.cboPuntoVenta.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboPuntoVenta.Items[0].Value = String.Empty;

            this.cboUbicacion.Items.Clear();
            
            DestinoBL oDestinoBL = new DestinoBL();
            oDestinoBL.ErrorEvent += new DestinoBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
            
            DestinoBE oDestinoBE = new DestinoBE();
            oDestinoBE.Tipo_ubicacion = String.Empty;
            oDestinoBE.Nro_ruc = String.Empty;
            oDestinoBE.Nom_ubicacion = String.Empty;
            oDestinoBE.Cod_estado = ConstanteBE.FL_ESTADO_ACTIVO;
            DestinoBEList oDestinoBEList = oDestinoBL.Listar(oDestinoBE);
            //@019 F
            this.cboUbicacion.DataSource = oDestinoBEList;
            this.cboUbicacion.DataTextField = "Nom_corto_ubicacion";
            this.cboUbicacion.DataValueField = "Id_ubicacion";
            this.cboUbicacion.DataBind();
            this.cboUbicacion.Items.Insert(0, new ListItem());
            this.cboUbicacion.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboUbicacion.Items[0].Value = String.Empty;

            /*combo Ubicacion Dependiente*/
            this.cboUbicacionDependiente.Items.Clear();
            this.cboUbicacionDependiente.Items.Insert(0, new ListItem());
            this.cboUbicacionDependiente.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboUbicacionDependiente.Items[0].Value = String.Empty;

            /*Combo Almacenera*/
            this.cboAlmacenera.Items.Clear();
            this.cboAlmacenera.Items.Insert(0, new ListItem());
            this.cboAlmacenera.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboAlmacenera.Items[0].Value = String.Empty;

            /*Combo Jefe Venta*/
            this.cboJefeVenta.Items.Clear();
            this.cboJefeVenta.Items.Insert(0, new ListItem());
            this.cboJefeVenta.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboJefeVenta.Items[0].Value = String.Empty;

            /*Combo Gerente*/
            this.cboGerente.Items.Clear();
            this.cboGerente.Items.Insert(0, new ListItem());
            this.cboGerente.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboGerente.Items[0].Value = String.Empty;

            /*Combo Punto Venta*/
            this.cboPuntoVentaConc.Items.Clear();
            this.cboPuntoVentaConc.Items.Insert(0, new ListItem());
            this.cboPuntoVentaConc.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboPuntoVentaConc.Items[0].Value = String.Empty;

            /*Combo Gerente*/
            this.cboGerenteConc.Items.Clear();
            this.cboGerenteConc.Items.Insert(0, new ListItem());
            this.cboGerenteConc.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboGerenteConc.Items[0].Value = String.Empty;

            /*Combo Agente Aduana*/
            this.cboAgente.Items.Clear();
            this.cboAgente.Items.Insert(0, new ListItem());
            this.cboAgente.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboAgente.Items[0].Value = String.Empty;

            /*Paginado*/
            SetPaginacion();

            //Combo Aprobador Venta Liberada
            UsuarioBL oUsuarioBL = new UsuarioBL();
            oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
            UsuarioBEList oUsuarioList01;

            UsuarioBE oUsuarioBE = new UsuarioBE();
            oUsuarioBE.NO_APE_MATERNO = String.Empty;
            oUsuarioBE.NO_APE_PATERNO = String.Empty;
            oUsuarioBE.VNOMUSR = String.Empty;
            oUsuarioBE.NID_PERFIL = 0;
            oUsuarioBE.FL_INACTIVO = ConstanteBE.FL_ESTADO_ACTIVO;
            oUsuarioBE.NID_UBICA = 0;
            oUsuarioBE.NID_ROL = 0;

            oUsuarioList01 = oUsuarioBL.GetAllUsuarioBandeja(oUsuarioBE, Profile.Aplicacion);
            
            this.cboAprobadorVentaLiberada.Items.Clear();
            this.cboAprobadorVentaLiberada.DataSource = oUsuarioList01;
            this.cboAprobadorVentaLiberada.DataTextField = "VNOMUSR";
            this.cboAprobadorVentaLiberada.DataValueField = "NID_USUARIO";
            this.cboAprobadorVentaLiberada.DataBind();
            this.cboAprobadorVentaLiberada.Items.Insert(0, new ListItem());
            this.cboAprobadorVentaLiberada.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboAprobadorVentaLiberada.Items[0].Value = String.Empty;


            oUsuarioBE = new UsuarioBE();
            oUsuarioBE.VNOMUSR = String.Empty;
            oUsuarioBE.NO_APE_PATERNO = String.Empty;
            oUsuarioBE.NO_APE_MATERNO = String.Empty;
            oUsuarioBE.NID_UBICA = 0;
            oUsuarioBE.NID_ROL = 0;
            oUsuarioBE.NID_PERFIL = 0;
            oUsuarioBE.FL_INACTIVO = ConstanteBE.FL_ESTADO_ACTIVO;
            
            oUsuarioListBE = oUsuarioBL.GetAllUsuarioBandeja(oUsuarioBE, Profile.Aplicacion);

            txtDNI.Attributes.Add("onKeyPress", "javascript:return fc_ValidaNumero();");
            
            this.lblReqNBrevete.Style["display"] = "none";
            this.lblReqFBrevete.Style["display"] = "none";
            
        }
        catch (Exception ex)
        {
            (Master as Seguridad).Web_ErrorEvent(this, ex);
        }
    }

    private void CargaUsuario()
    {
        UsuarioBL oUsuarioBL = new UsuarioBL();
        oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        this.trHrJefeVenta.Style["display"] = "none";
        this.trUbicacion.Style["display"] = "none";
        this.trCanalVenta.Style["display"] = "none";
        this.trUbicacionDependiente.Style["display"] = "none";
        this.trAlmacenera.Style["display"] = "none";
        this.trJefeVenta.Style["display"] = "none";
        this.trAgente.Style["display"] = "none";
        this.trAprobador.Style["display"] = "none";
        this.trCanalVentaAux.Style["display"] = "none";

        
        this.trGerenteConc.Style["display"] = "none";
        this.trUbicacionConc.Style["display"] = "none";
        
        this.tdGerentetexto.Style["display"] = "";
        this.tdGerentecombo.Style["display"] = "";

        if (this.usuarioID != 0)
        {
            ActualizaTabs(0);
            tipoAccion = ConstanteBE.TIPO_MODIFICAR;

            this.oUsuarioBE = oUsuarioBL.GetUsuarioById(this.usuarioID);

            this.txtNombres.Text = this.oUsuarioBE.VNOMUSR.Trim();
            this.txtApePat.Text = this.oUsuarioBE.NO_APE_PATERNO.Trim();
            this.txtApeMat.Text = this.oUsuarioBE.NO_APE_MATERNO.Trim();
            this.txtDNI.Text = this.oUsuarioBE.NU_TIPO_DOCUMENTO.Trim();

            this.chkBloqueado.Checked = false;
            if (this.oUsuarioBE.CESTBLQ.Trim().Equals("1")) this.chkBloqueado.Checked = true;

            this.txtLogin.Text = this.oUsuarioBE.CUSR_ID.Trim();

            if (!this.oUsuarioBE.VPASSMD5.Trim().Equals(String.Empty)) { indExisteClave = "1"; }

            this.txtCorreo.Text = this.oUsuarioBE.VCORREO.Trim();
            this.txtTelefono.Text = this.oUsuarioBE.VTELEFONO.Trim();

            if (this.cboTipoUsuario.Items.FindByValue(this.oUsuarioBE.NID_TIPO.ToString()) != null) this.cboTipoUsuario.SelectedValue = this.oUsuarioBE.NID_TIPO.ToString();
            if (this.cboPerfil.Items.FindByValue(this.oUsuarioBE.NID_PERFIL.ToString()) != null) this.cboPerfil.SelectedValue = this.oUsuarioBE.NID_PERFIL.ToString();

            String rol = String.Empty, rol1;
            String flConcesionario = String.Empty;

            PerfilBL oPerfilBL = new PerfilBL();
            oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
            PerfilBEList list = oPerfilBL.GetPerfilesBandeja(Profile.Aplicacion, String.Empty, ConstanteBE.FL_ESTADO_ACTIVO, "");
            rol1 = this.cboPerfil.SelectedValue;

            if (list != null)
            {
                PerfilBE obj = list.Find(delegate (PerfilBE p) { return p.NID_PERFIL.ToString() == rol1; });
                if (obj != null)
                {
                    rol = obj.co_perfil_usuario.Trim();
                    ConstTipo = obj.co_perfil_usuario.Trim();
                    flConcesionario = obj.fl_concesionario.Trim();
                }
            }

            cboUbicacionDependiente_SelectedIndexChanged(null, null);

            this.txtFecIni.Text = this.oUsuarioBE.SFE_INICIO_ACCESO;
            this.txtFecFin.Text = this.oUsuarioBE.SFE_FIN_ACCESO;
            this.txtHoraIni.Text = this.oUsuarioBE.HR_INICIO_ACCESO.Trim();
            this.txtHoraFin.Text = this.oUsuarioBE.HR_FIN_ACCESO.Trim();
            this.txtMensaje.Text = this.oUsuarioBE.VMSGBLQ.Trim();



            this.chkDisponible.Checked = false;
            this.lbl1erSuplente.Visible = false;
            this.cbo1erSuplente.Visible = false;
            this.lbl2doSuplente.Visible = false;
            this.cbo2doSuplente.Visible = false;
        }
        else
        {
            tipoAccion = ConstanteBE.TIPO_AGREGAR;
            oUsuarioBE = new UsuarioBE();
        }
        
        ViewState["usuarioID"] = usuarioID;
        ViewState["oUsuarioBE"] = oUsuarioBE;
        ViewState["indExisteClave"] = indExisteClave;
    }

    #endregion

    #region "Eventos Botones"
    protected void btnGrabar_OnClick(object sender, EventArgs e)
    {
        Int32 retorno;
        UsuarioBL oUsuarioBL = new UsuarioBL();
        oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        SeguridadBL oSeguridadBL = new SeguridadBL();
        oSeguridadBL.ErrorEvent += new SeguridadBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        
        String str_cod_empresa = "";
        foreach (ListItem item in cbo_empresa.Items)
        {
            if (item.Selected)
            {
                str_cod_empresa += item.Value + ",";
            }
        }
        
        CargaDesdeForm();

        if (!this.txtClave.Text.Trim().Equals(String.Empty))
            this.oUsuarioBE.VUSR_PASS = oSeguridadBL.GetEncripta(this.oUsuarioBE.VUSR_PASS);

        retorno = oUsuarioBL.GrabarUsuario(this.oUsuarioBE, Profile.Aplicacion);
        if (retorno > 0)
        {
            this.txhNidUsuario.Value = retorno.ToString();
            JavaScriptHelper.Alert(this, Message.keyGrabar, "");
            this.usuarioID = retorno;
            this.oUsuarioBE.NID_USUARIO = this.usuarioID;
            CargaUsuario();
        }
        else
        {
            if (retorno == -5) JavaScriptHelper.Alert(this, Message.keyLoginRepetido, "");
            else if (retorno == -6) JavaScriptHelper.Alert(this, Message.keyDNIRepetido, "");
            else JavaScriptHelper.Alert(this, Message.keyErrorGrabar, "");
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("SGS_Usuario_Bandeja.aspx", false);
    }

    protected void btnPassReset_Click(object sender, EventArgs e)
    {
        UsuarioBL oUsuarioBL = new UsuarioBL();
        oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        oUsuarioBE = (UsuarioBE)ViewState["oUsuarioBE"];
        int retorno;
        try
        {
            (Master as Seguridad).onError = false;
            retorno = oUsuarioBL.ModificarPassWord(this.oUsuarioBE.CUSR_ID, txtClave.Text, Profile.Usuario.CUSR_ID, Profile.UsuarioRed, Profile.Estacion, "1", txtClave.Text);//I/F @016
            if (!(Master as Seguridad).onError && retorno > 0)
                JavaScriptHelper.Alert(this, "'La contraseña se reinicio exitosamente.'", String.Empty);
            else if (retorno == -6)
                JavaScriptHelper.Alert(this, "'La contraseña ya fue reinicializada.'", String.Empty);
            else
                JavaScriptHelper.Alert(this, Message.keyErrorGrabar, String.Empty);
        }
        catch (Exception ex)
        {
            (Master as Seguridad).Web_ErrorEvent(this, ex);
        }
    }

    private void CargaDesdeForm()
    {
        Int32 aux;

        this.oUsuarioBE = (UsuarioBE)ViewState["oUsuarioBE"];
        this.usuarioID = (Int32)ViewState["usuarioID"];

        this.oUsuarioBE.VNOMUSR = this.txtNombres.Text;
        this.oUsuarioBE.NO_APE_PATERNO = this.txtApePat.Text;
        this.oUsuarioBE.NO_APE_MATERNO = this.txtApeMat.Text;
        this.oUsuarioBE.NU_TIPO_DOCUMENTO = this.txtDNI.Text;

        this.oUsuarioBE.CESTBLQ = "0";
        if (this.chkBloqueado.Checked) this.oUsuarioBE.CESTBLQ = "1";

        this.oUsuarioBE.CUSR_ID = this.txtLogin.Text;

        if (!this.txtClave.Text.Trim().Equals(String.Empty))
        {
            this.oUsuarioBE.VPASSMD5 = this.oUsuarioBE.GetMD5(this.txtClave.Text);
            this.oUsuarioBE.VUSR_PASS = this.txtClave.Text;
        }

        this.oUsuarioBE.VCORREO = this.txtCorreo.Text;
        this.oUsuarioBE.VTELEFONO = this.txtTelefono.Text;

        /*Combo Perfil*/
        String rol = String.Empty, rol1;
        String fl_concesionario = String.Empty;

        PerfilBL oPerfilBL = new PerfilBL();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        PerfilBEList list = oPerfilBL.GetPerfilesBandeja(Profile.Aplicacion, String.Empty, ConstanteBE.FL_ESTADO_ACTIVO, "");
        rol1 = this.cboPerfil.SelectedValue;

        if (list != null)
        {
            PerfilBE obj = list.Find(delegate(PerfilBE p) { return p.NID_PERFIL.ToString() == rol1; });
            if (obj != null)
            {
                rol = obj.co_perfil_usuario.Trim();
                ConstTipo = obj.co_perfil_usuario.Trim();
                fl_concesionario = obj.fl_concesionario.Trim();
            }
        }

            Int32.TryParse(this.cboUbicacion.SelectedValue, out aux);
            this.oUsuarioBE.NID_UBICA = aux;

        Int32.TryParse(this.cboPerfil.SelectedValue, out aux);
        this.oUsuarioBE.NID_PERFIL = aux;
        Int32.TryParse(this.cboTipoUsuario.SelectedValue, out aux);
        this.oUsuarioBE.NID_TIPO = aux;
        this.oUsuarioBE.NID_ROL = aux;

        this.oUsuarioBE.SFE_INICIO_ACCESO = this.txtFecIni.Text;
        this.oUsuarioBE.SFE_FIN_ACCESO = this.txtFecFin.Text;
        this.oUsuarioBE.HR_INICIO_ACCESO = this.txtHoraIni.Text;
        this.oUsuarioBE.HR_FIN_ACCESO = this.txtHoraFin.Text;
        this.oUsuarioBE.VMSGBLQ = this.txtMensaje.Text;
        this.oUsuarioBE.FL_INACTIVO = ConstanteBE.FL_ESTADO_ACTIVO;
        this.oUsuarioBE.CO_USUARIO_CREA = Profile.Usuario.CUSR_ID;
        this.oUsuarioBE.NO_USUARIO_RED = Profile.UsuarioRed;
        this.oUsuarioBE.NO_ESTACION_RED = Profile.Estacion;
        this.oUsuarioBE.passwordDesEnc = this.txtClave.Text.Trim();
    }
    #endregion

    #region "Eventos Combos"

    protected void cboUbicacion_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void combos_SelectedIndexChanged(object sender, EventArgs e)
    {
        String rol = String.Empty, rol1;
        UsuarioBL oUsuarioBL = new UsuarioBL();
        oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        String codPerfil = String.Empty;
        String flConcesionario = String.Empty;
        PerfilBL oPerfilBL = new PerfilBL();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        try
        {
            this.trHrJefeVenta.Style["display"] = "none";
            this.trJefeVenta.Style["display"] = "none";
            this.trAlmacenera.Style["display"] = "none";
            this.trAgente.Style["display"] = "none";
            this.trUbicacion.Style["display"] = "none";
            this.trCanalVenta.Style["display"] = "none";
            this.trUbicacionDependiente.Style["display"] = "none";
            this.trAprobador.Style["display"] = "none";
            this.trCanalVentaAux.Style["display"] = "none";
            this.trUbicacionConc.Style["display"] = "none";
            this.trGerenteConc.Style["display"] = "none";
            this.tdGerentetexto.Style["display"] = "";
            this.tdGerentecombo.Style["display"] = "";
            this.lblReqNBrevete.Style["display"] = "none";
            this.lblReqFBrevete.Style["display"] = "none";

            this.cboCanalVentaConc.Enabled = true;
            this.cboJefeVenta.Enabled = true;

            codPerfil = this.cboPerfil.SelectedValue;

            PerfilBEList list = oPerfilBL.GetPerfilesBandeja(Profile.Aplicacion, String.Empty, ConstanteBE.FL_ESTADO_ACTIVO, "");
            rol1 = this.cboPerfil.SelectedValue;

            if (list != null)
            {
                PerfilBE obj = list.Find(delegate (PerfilBE p) { return p.NID_PERFIL.ToString() == rol1; });
                if (obj != null)
                {
                    rol = obj.co_perfil_usuario.Trim();
                    ConstTipo = obj.co_perfil_usuario.Trim();
                    flConcesionario = obj.fl_concesionario.Trim();
                }
            }

            this.txtDNI.CssClass = "";
            this.cboTipoUsuario.Enabled = true;


            this.trHrJefeVenta.Style["display"] = "";

            this.trCanalVenta.Style["display"] = "";

            this.cboUbicacion.Items.Clear();
            DestinoBL oDestinoBL = new DestinoBL();
            oDestinoBL.ErrorEvent += new DestinoBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
            DestinoBE oDestinoBE = new DestinoBE();
            oDestinoBE.Tipo_ubicacion = String.Empty;
            oDestinoBE.Nro_ruc = String.Empty;
            oDestinoBE.Nom_ubicacion = String.Empty;
            oDestinoBE.Cod_estado = ConstanteBE.FL_ESTADO_ACTIVO;
            DestinoBEList oDestinoBEList = oDestinoBL.Listar(oDestinoBE);

            this.cboUbicacion.DataSource = oDestinoBEList;
            this.cboUbicacion.DataTextField = "Nom_corto_ubicacion";
            this.cboUbicacion.DataValueField = "Id_ubicacion";
            this.cboUbicacion.DataBind();
            this.cboUbicacion.Items.Insert(0, new ListItem());
            this.cboUbicacion.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboUbicacion.Items[0].Value = String.Empty;

            this.cboUbicacionDependiente.Items.Clear();
            this.cboUbicacionDependiente.Items.Insert(0, new ListItem());
            this.cboUbicacionDependiente.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboUbicacionDependiente.Items[0].Value = String.Empty;

            /*Combo Jefe Venta*/
            this.cboJefeVenta.Items.Clear();
            this.cboJefeVenta.Items.Insert(0, new ListItem());
            this.cboJefeVenta.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboJefeVenta.Items[0].Value = String.Empty;

            /*Combo Gerente*/
            this.cboGerente.Items.Clear();
            this.cboGerente.Items.Insert(0, new ListItem());
            this.cboGerente.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboGerente.Items[0].Value = String.Empty;

        }
        catch { }
    }

    protected void cboCanalVenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void cboCanalVentaConc_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void cboPuntoVentaConc_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void cboPuntoVenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void cboUbicacionDependiente_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int32 ubicacion;
        UsuarioBEList oUsuarioList;
        UsuarioBL oUsuarioBL = new UsuarioBL();
        UsuarioBE oUsuarioBE;
        oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        Int32.TryParse(this.cboUbicacionDependiente.SelectedValue, out ubicacion);
        Int32 idperfil = 0;

        oUsuarioBE = new UsuarioBE();
        oUsuarioBE.NO_APE_MATERNO = String.Empty;
        oUsuarioBE.NO_APE_PATERNO = String.Empty;
        oUsuarioBE.VNOMUSR = String.Empty;
        oUsuarioBE.NID_PERFIL = idperfil;
        oUsuarioBE.FL_INACTIVO = ConstanteBE.FL_ESTADO_ACTIVO;
        oUsuarioBE.NID_UBICA = 0;// ubicacion;
        oUsuarioBE.NID_ROL = 1;

        this.cboJefeVenta.Items.Clear();

        oUsuarioList = oUsuarioBL.GetAllUsuarioBandeja(oUsuarioBE, Profile.Aplicacion);
        this.cboJefeVenta.DataSource = oUsuarioList;
        this.cboJefeVenta.DataTextField = "VNOMUSR";
        this.cboJefeVenta.DataValueField = "NID_USUARIO";
        this.cboJefeVenta.DataBind();
        this.cboJefeVenta.Items.Insert(0, new ListItem());
        this.cboJefeVenta.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
        this.cboJefeVenta.Items[0].Value = String.Empty;

        oUsuarioBE = new UsuarioBE();
        oUsuarioBE.NO_APE_MATERNO = String.Empty;
        oUsuarioBE.NO_APE_PATERNO = String.Empty;
        oUsuarioBE.VNOMUSR = String.Empty;
        oUsuarioBE.NID_PERFIL = idperfil;
        oUsuarioBE.FL_INACTIVO = ConstanteBE.FL_ESTADO_ACTIVO;
        oUsuarioBE.NID_UBICA = 0;// ubicacion;
        oUsuarioBE.NID_ROL = 2;

        this.cboGerente.Items.Clear();

        oUsuarioList = oUsuarioBL.GetAllUsuarioBandeja(oUsuarioBE, Profile.Aplicacion);
        this.cboGerente.DataSource = oUsuarioList;
        this.cboGerente.DataTextField = "VNOMUSR";
        this.cboGerente.DataValueField = "NID_USUARIO";
        this.cboGerente.DataBind();
        this.cboGerente.Items.Insert(0, new ListItem());
        this.cboGerente.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
        this.cboGerente.Items[0].Value = String.Empty;
    }

    protected void chkDisponible_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chkDisponible.Checked)
        {
            this.lbl1erSuplente.Visible = true;
            this.cbo1erSuplente.Visible = true;
            this.lbl2doSuplente.Visible = true;
            this.cbo2doSuplente.Visible = true;
        }
        else
        {
            this.lbl1erSuplente.Visible = false;
            this.cbo1erSuplente.Visible = false;
            this.lbl2doSuplente.Visible = false;
            this.cbo2doSuplente.Visible = false;
        }

    }

    #endregion

    #region "Eventos de TAB"
    protected void tabTabIndexChange_ActiveTabChanged(object sender, EventArgs e)
    {
        Int32.TryParse(this.txhIndiceTab.Value.Trim(), out indiceTabOn);
        //indiceTabOn = this.tabContUsuario.ActiveTabIndex;

        if (indiceTabOn == 1)
        {
            
        }
        else if (indiceTabOn == 2)
        {
            this.txhModuloSelected.Value = String.Empty;
            CargaOpcionesModulo();
        }

        ActualizaTabs(indiceTabOn);
    }

    private void ActualizaTabs(int indiceTab)
    {
        this.btnGrabar.Visible = false;
        this.btnGrabarPerfil.Visible = false;
        this.tblNuevoUsuario.Visible = false;
        this.tblPerfilAsociado.Visible = false;

        if (this.indiceTabOn == 0)
        {
            this.btnGrabar.Visible = true;
            this.tblNuevoUsuario.Visible = true;
        }
        else if (this.indiceTabOn == 2)
        {
            this.tblPerfilAsociado.Visible = true;
            this.btnGrabarPerfil.Visible = true;
        }
    }
    #endregion

    
    #region "Tab de Perfil"

    private void CargaOpcionesModulo()
    {
        PerfilBL oPerfilBL = new PerfilBL();
        OpcionSeguridadBE oOpcionSeguridadBE = new OpcionSeguridadBE();
        OpcionSeguridadBEList oOpcionSeguridadBEList = new OpcionSeguridadBEList();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        oOpcionSeguridadBE.CCOAPL = Profile.Aplicacion;
        oOpcionSeguridadBE.NIVEL = 1;
        oOpcionSeguridadBE.CSTRUCT = String.Empty;//this.oUsuarioBE.NID_PERFIL
        oOpcionSeguridadBEList = oPerfilBL.GetAllOpciones(oOpcionSeguridadBE, 0, this.usuarioID/*Profile.Usuario.NID_USUARIO*/);

        this.txhCodOpciones.Value = String.Empty;
        this.txhIndOpciones.Value = String.Empty;
        
        foreach (OpcionSeguridadBE oOpcion in oOpcionSeguridadBEList)
        {
            oOpcionSeguridadBE = new OpcionSeguridadBE();
            oOpcionSeguridadBE.CCOAPL = Profile.Aplicacion;
            oOpcionSeguridadBE.NIVEL = 0;
            oOpcionSeguridadBE.CSTRUCT = oOpcion.CSTRUCT.Trim();

            if (this.txhModuloSelected.Value.Trim().Equals(String.Empty)) this.txhModuloSelected.Value = oOpcion.CSTRUCT.Trim();

            foreach (OpcionSeguridadBE oOpcionDet in oPerfilBL.GetAllOpciones(oOpcionSeguridadBE, 0, this.usuarioID/*Profile.Usuario.NID_USUARIO*/))
            {
                if (this.txhCodOpciones.Value.Trim().IndexOf("|" + oOpcionDet.NID_OPCION.ToString().Trim() + "|") < 0)
                {
                    this.txhCodOpciones.Value += txhCodOpciones.Value.Trim().Equals(String.Empty) ? "|" + oOpcionDet.NID_OPCION.ToString() + "|" : oOpcionDet.NID_OPCION.ToString() + "|";
                    this.txhIndOpciones.Value += txhIndOpciones.Value.Trim().Equals(String.Empty) ? "|" + oOpcionDet.IND_REL + "|" : oOpcionDet.IND_REL + "|";
                }
            }
        }

        if (oOpcionSeguridadBEList != null && oOpcionSeguridadBEList.Count == 0) oOpcionSeguridadBEList.Add(new OpcionSeguridadBE());
        indRefreshGrilla = "1";
        this.gvModulos.DataSource = oOpcionSeguridadBEList;
        this.gvModulos.DataBind();

        btnRefreshOpciones_OnClick(null, null);

    }

    #region "Metodos Grilla gvModulos"
    protected void gvModulos_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = this.gvModulos.DataKeys[e.Row.RowIndex];
            if (((Int32)dataKey.Values["NID_OPCION"]) == 0)
            {
                e.Row.Visible = false;
                return;
            }

            e.Row.Style["cursor"] = "pointer";
            //e.Row.Attributes["onclick"] = "javascript: fc_SeleccionaFilaSimple(this);";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'; document.getElementById('{2}').click();"
                                            , this.txhModuloSelected.ClientID
                                            , dataKey.Values["CSTRUCT"].ToString()
                                            , this.btnRefreshOpciones.ClientID);

            if (indRefreshGrilla == "1" && dataKey.Values["CSTRUCT"].ToString().Trim().Equals(this.txhModuloSelected.Value.Trim()))
            {
                idFilaGrilla = e.Row.ClientID;
            }

            /*e.Row.Attributes["ondblclick"] = String.Format("javascript: document.getElementById('{0}').value = '{1}'; document.getElementById('{2}').click(); "
                        , this.txhModuloSelected.ClientID
                        , dataKey.Values["CSTRUCT"].ToString()
                        , this.btnRefreshOpciones.ClientID);*/
        }
    }

    protected void btnRefreshOpciones_OnClick(object sender, EventArgs e)
    {
        PerfilBL oPerfilBL = new PerfilBL();
        OpcionSeguridadBE oOpcionSeguridadBE = new OpcionSeguridadBE();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        if (!this.txhModuloSelected.Value.Equals(String.Empty))
        {
            oOpcionSeguridadBE.CCOAPL = Profile.Aplicacion;
            oOpcionSeguridadBE.NIVEL = 0;
            oOpcionSeguridadBE.CSTRUCT = this.txhModuloSelected.Value.Trim();//this.oUsuarioBE.NID_PERFIL
            oOpcionSeguridadBEList = oPerfilBL.GetAllOpciones(oOpcionSeguridadBE, 0, this.usuarioID/*Profile.Usuario.NID_USUARIO*/);

            if (oOpcionSeguridadBEList != null && oOpcionSeguridadBEList.Count == 0) oOpcionSeguridadBEList.Add(new OpcionSeguridadBE());

            this.gvOpciones.DataSource = oOpcionSeguridadBEList;
            this.gvOpciones.DataBind();
        }
    }

    #endregion

    #region "Metodos Grilla gvOpciones"
    protected void gvOpciones_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        CheckBox chkMant, chkConst;
        String indRelacion;
        bool flagIndVerChecks;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = this.gvOpciones.DataKeys[e.Row.RowIndex];
            if (((Int32)dataKey.Values["NID_OPCION"]) == 0)
            {
                e.Row.Visible = false;
                return;
            }

            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = "javascript: fc_SeleccionaFilaSimple(this);";
            //e.Row.BackColor = System.Drawing.Color.White;
            /*if (dataKey.Values["CSTRUCT"].ToString().Trim().Length != 4) e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#e3e7f2");*/
            e.Row.Cells[0].Style["padding-left"] = String.Format("{0}px", (dataKey.Values["CSTRUCT"].ToString().Trim().Length - 4) / 2 * 20);

            chkMant = (CheckBox)e.Row.FindControl("chkMant");
            chkConst = (CheckBox)e.Row.FindControl("chkConst");

            flagIndVerChecks = true;
            int i = e.Row.RowIndex + 1;
            while (i < this.oOpcionSeguridadBEList.Count && flagIndVerChecks)
            {
                if (this.oOpcionSeguridadBEList[i].CSTRUCT.IndexOf(dataKey.Values["CSTRUCT"].ToString()) == 0)
                    flagIndVerChecks = false;
                i++;
            }

            if (!flagIndVerChecks)
            {
                chkMant.Visible = false;
                chkConst.Visible = false;
                return;
            }

            chkMant.Attributes["onClick"] = String.Format("javascript: fc_SelOpcion('{0}', '{1}', '{2}', 'MANT', '{3}');"
                                            , chkMant.ClientID, chkConst.ClientID
                                            , dataKey.Values["NID_OPCION"].ToString()
                                            , dataKey.Values["fl_icono"].ToString());
            chkConst.Attributes["onClick"] = String.Format("javascript: fc_SelOpcion('{0}', '{1}', '{2}', 'CONS', '{3}');"
                                            , chkMant.ClientID, chkConst.ClientID
                                            , dataKey.Values["NID_OPCION"].ToString()
                                            , dataKey.Values["fl_icono"].ToString());

            chkMant.Checked = false;
            chkConst.Checked = false;

            indRelacion = GetIndRelacion(dataKey.Values["NID_OPCION"].ToString().Trim());

            if (indRelacion.Equals("A"))
            {
                chkMant.Checked = true;
                chkConst.Checked = true;
                chkConst.Enabled = false;
            }
            else if (indRelacion.Equals("C"))
            {
                chkMant.Checked = false;
                chkConst.Checked = true;
                chkConst.Enabled = true;
                if (this.chkMantAll.Checked) this.chkMantAll.Checked = false;
            }
            else
            {
                if (this.chkMantAll.Checked) this.chkMantAll.Checked = false;
                if (this.chkConsAll.Checked) this.chkConsAll.Checked = false;
            }

            if (dataKey.Values["fl_icono"].ToString().Equals("1"))
            {
                chkConst.Checked = false;
                //chkConst.Attributes["disabled"] = "false";
                chkConst.Enabled = false;
            }
        }
    }

    private String GetIndRelacion(String opcionId)
    {
        String[] arrOpciones = this.txhCodOpciones.Value.Split('|');
        String[] arrIndRelacion = this.txhIndOpciones.Value.Split('|');

        for (int i = 0; i < arrOpciones.Length; i++)
        {
            if (arrOpciones[i].Trim().Equals(opcionId.Trim())) return arrIndRelacion[i];
        }

        return String.Empty;
    }

    #endregion

    protected void btnGrabarPerfil_OnClick(object sender, EventArgs e)
    {
        Int32 retorno;
        SeguridadBL oSeguridadBL = new SeguridadBL();
        oSeguridadBL.ErrorEvent += new SeguridadBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        try
        {

            String cadena = String.Empty;
            String[] arrCodigos;
            
            arrCodigos = this.txhCodOpciones.Value.Trim().Split('|');

            /*Insertamos detalle de opciones por perfil*/

            String XML = Global.ObtenerXml(this.txhCodOpciones.Value.Trim(), this.txhIndOpciones.Value.Trim());
            retorno = oSeguridadBL.InsertUsuarioOpcion(this.usuarioID, XML, Profile.Usuario.CUSR_ID, Profile.Estacion, Profile.UsuarioRed);

            if (retorno > 0)
            {
                CargaOpcionesModulo();
                JavaScriptHelper.Alert(this, Message.keyGrabar, String.Empty);
            }
            else JavaScriptHelper.Alert(this, Message.keyErrorGrabar, String.Empty);

        }
        catch { }
    }
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        
    }
}
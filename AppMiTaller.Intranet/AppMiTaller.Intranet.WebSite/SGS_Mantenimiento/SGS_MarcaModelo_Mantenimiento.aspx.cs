using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.IO;

public partial class SGS_Mantenimiento_SGS_MarcaModelo_Mantenimiento : PaginaBase
{
    #region "Atributos"

    public String tipoAccion = ConstanteBE.TIPO_AGREGAR;
    public String tipoAgregar = ConstanteBE.TIPO_AGREGAR;
    private MarcaBE oMarcaBE;
    public Int32 indiceTabOn;
    public Int32 idMarca;
    public static int idMarcaSeleccionada;
    private String TipoAccesoPagina;

    ModeloBE oModeloBE;

    public SGS_UserControl_ComboEstado cboEstadoMarca;
    public SGS_UserControl_ComboEstado cboEstadoModeloPoput;

    //AGP.SGS.BE.Mantenimiento.ModelosBEList oModelosBEList = null;
    ModeloBEList oModelosBEList = null;
    #endregion

    #region "Inicio"

    protected void Page_PreInit(object sender, EventArgs e)
    {
        base.Page_PreInit(sender, e);

        cboEstadoMarca = (SGS_UserControl_ComboEstado)Page.LoadControl("../SGS_UserControl/ComboEstado.ascx");
        cboEstadoMarca.ID = "cboEstadoMarca";
        cboEstadoMarca.SkinID = "cboob";

        cboEstadoModeloPoput = (SGS_UserControl_ComboEstado)Page.LoadControl("../SGS_UserControl/ComboEstado.ascx");
        cboEstadoModeloPoput.ID = "cboEstadoModeloPoput";
        cboEstadoModeloPoput.SkinID = "cboob";
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja Modelo
        if (/*ViewState["oModeloBEList"]*/ViewState["oModelosBEList"] != null &&
            this.gvModelo != null &&
            this.gvModelo.Rows.Count > 0 &&
            this.gvModelo.PageCount > 1)
        {
            GridViewRow oRow = this.gvModelo.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", ((ModeloBEList)ViewState["oModelosBEList"]).Count);
                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.spanCboEstadoMarca.Controls.Add(cboEstadoMarca);
        this.spanCboEstadoModeloPoput.Controls.Add(cboEstadoModeloPoput);

        if (!Page.IsPostBack)
        {
            /*CONTROL DE ACCESO A PGINA*/
            TipoAccesoPagina = Master.ValidaTipoAccesoPagina(this, CONSTANTE_SEGURIDAD.Marca_Modelo);
            ViewState["TipoAccesoPagina"] = TipoAccesoPagina;
            /*FIN CONTROL DE ACCESO A PGINA*/

            ViewState["ordenLista"] = SortDirection.Descending;

            Int32.TryParse(Request.QueryString["idSelMarca"], out idMarca);
            Int32.TryParse(Request.QueryString["idSelMarca"], out idMarcaSeleccionada);
            this.InicializaPagina();
            this.CargaMarca();
        }
        /*CONTROL DE ACCESO A PGINA*/
        TipoAccesoPagina = (String)ViewState["TipoAccesoPagina"];
        if (!TipoAccesoPagina.Equals(CONSTANTE_SEGURIDAD.AccesoEdicion))
        {
            this.btnGrabarMarca.Visible = false;
            this.btnAgregarModelo.Visible = false;
            this.btnQuitarModelo.Visible = false;
        }
        /*FIN CONTROL DE ACCESO A PGINA*/

        idMarca = (Int32)ViewState["idMarca"];
        idMarcaSeleccionada = (Int32)ViewState["idMarca"];

        if (idMarca != 0) tipoAccion = "Mod.";// ConstanteBE.TIPO_MODIFICAR;
        oMarcaBE = (MarcaBE)ViewState["oMarcaBE"];

        if (ViewState["indiceTabOn"] != null)
        {
            this.tabContMarca.ActiveTabIndex = (Int32)ViewState["indiceTabOn"];
            this.indiceTabOn = (Int32)ViewState["indiceTabOn"];
        }
    }

    #endregion

    #region "Carga de pagina"

    private void InicializaPagina()
    {
        TipoTablaDetalleBL oTipoTablaDetalleBL = new TipoTablaDetalleBL();
        oTipoTablaDetalleBL.ErrorEvent += new TipoTablaDetalleBL.ErrorDelegate(Master.Transaction_ErrorEvent);

        //Marca
        this.cboEmpMarca.CargarCombo(ConstanteBE.OBJECTO_TIPO_SELECCIONE);
        this.cboEstadoMarca.cargarCombo(ConstanteBE.OBJECTO_TIPO_SELECCIONE);
        this.cboEstadoMarca.SelectedValue = ConstanteBE.FL_ESTADO_ACTIVO;
        this.cboEstadoMarca.Enabled = false;

        //Marca Modelo
        this.cboEstadoModelo.cargarCombo(ConstanteBE.OBJECTO_TIPO_TODOS);
        this.cboEstadoModelo.SelectedValue = ConstanteBE.FL_ESTADO_ACTIVO;

        //Modelo Poput
        this.cboNegocioModeloPoput.cargarCombo(ConstanteBE.OBJECTO_TIPO_SELECCIONE);
        this.cboLineaModeloPoput.Condicion = ConstanteBE.OBJECTO_TIPO_SELECCIONE;

        //COMBO LINEA COMERCIAL
        this.cboLineaComercialModeloPoput.Items.Clear();
        this.cboLineaComercialModeloPoput.Items.Insert(0, new ListItem());
        this.cboLineaComercialModeloPoput.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
        this.cboLineaComercialModeloPoput.Items[0].Value = String.Empty;

        //COMBO ESTADO        
        this.cboEstadoModeloPoput.cargarCombo(ConstanteBE.OBJECTO_TIPO_SELECCIONE);
        this.cboEstadoModeloPoput.SelectedValue = ConstanteBE.FL_ESTADO_ACTIVO;
        //Paginado
        this.gvModelo.PageSize = Profile.PageSize;

    }
    private void CargaMarca()
    {
        MarcaBL oMarcaBL = new MarcaBL();
        oMarcaBL.ErrorEvent += new MarcaBL.ErrorDelegate(Master.Transaction_ErrorEvent);

        if (this.idMarca != 0)
        {
            tipoAccion = "Mod.";//ConstanteBE.TIPO_MODIFICAR;
            //*******************
            try
            {
                oMarcaBE = oMarcaBL.GetById(this.idMarca);
                if (oMarcaBE != null)
                {
                    this.txtCodMarca.Text = oMarcaBE.co_marca;
                    this.txtNomMarca.Text = oMarcaBE.no_marca;
                    this.cboEmpMarca.SelectedValue = oMarcaBE.nid_empresa.ToString();

                    /*Inicio:Para mostrar la imagen ya guardada en el fileserver*/
                    string RutaPathFile1 = "", RutaPathFile2 = "";
                    this.txhNombreArchivoBD.Value = Convert.ToString(oMarcaBE.logo).Trim();
                    RutaPathFile1 = Convert.ToString(ConfigurationManager.AppSettings["VirtualPath"]) + ConstanteBE.RUTA_MARCAS.Replace("\\", "/") + Convert.ToString(this.txhNombreArchivoBD.Value).Trim();
                    RutaPathFile2 = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + ConstanteBE.RUTA_MARCAS + Convert.ToString(this.txhNombreArchivoBD.Value).Trim();
                    if (File.Exists(RutaPathFile2))
                    {
                        try { System.Drawing.Image oImg = System.Drawing.Image.FromFile(RutaPathFile2); this.imgLogo.Visible = true; }
                        catch //(Exception ex) 
                        { this.imgLogo.Visible = false; }
                        this.imgLogo.Src = RutaPathFile1;
                    }
                    else { this.imgLogo.Visible = false; }

                    this.cboEstadoMarca.SelectedValue = oMarcaBE.fl_inactivo;
                    this.cboEstadoMarca.Enabled = true;
                    this.txtNomMarModelo.Text = oMarcaBE.no_marca;
                    this.txtNomMarModeloPoput.Text = oMarcaBE.no_marca;

                    this.tabModelo.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                Master.Web_ErrorEvent(this, ex);
            }
        }
        else
        {
            oMarcaBE = new MarcaBE();
            this.tabModelo.Enabled = false;
        }

        this.ActualizaBotoneria();

        ViewState["idMarca"] = idMarca;
        ViewState["oMarcaBE"] = oMarcaBE;
    }

    #endregion

    #region "Mantenimiento Marca"

    #region "Eventos Botones"

    protected void btnGrabarMarca_Click(object sender, ImageClickEventArgs e)
    {
        Int32 retorno, indicador2;
        indicador2 = 0;
        MarcaBL oMarcaBL = new MarcaBL();
        oMarcaBL.ErrorEvent += new MarcaBL.ErrorDelegate(Master.Transaction_ErrorEvent);
        try
        {
            if (CargarEntidadMarcaDesdeForm())
            {
                MarcaBE oMarcaBEMad = new MarcaBE();
                oMarcaBEMad = oMarcaBL.GetById(oMarcaBE.nid_marca);
                
                retorno = oMarcaBL.Grabar(this.oMarcaBE);
                if (retorno > 0)
                {
                    if (indicador2 > 0 || retorno > 0)
                    {
                        if (this.oMarcaBE.nid_marca == 0) JavaScriptHelper.Alert(this, Message.keyGrabar, "");
                        else JavaScriptHelper.Alert(this, Message.keyActualizar, "");

                        this.idMarca = retorno;
                        this.oMarcaBE.nid_marca = this.idMarca;
                        CargaMarca();
                    }
                }
                else
                {
                    if (retorno == -4) JavaScriptHelper.Alert(this, Message.keyErrorGrabarLlave, "");
                    else if (retorno == -3) JavaScriptHelper.Alert(this, Message.keyErrorGrabarNulo, "");
                    else if (retorno == -2) JavaScriptHelper.Alert(this, Message.keyErrorGrabarCodDuplicado, "");
                    else if (retorno == -1) JavaScriptHelper.Alert(this, Message.keyErrorGrabar, "");
                    else JavaScriptHelper.Alert(this, Message.keyErrorGrabar, "");
                }
            }
        }
        catch { }
    }

    protected void btnRegresarMarca_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/SGS_Mantenimiento/SGS_MarcaModelo_Bandeja.aspx", false);
    }

    private bool CargarEntidadMarcaDesdeForm()
    {
        this.oMarcaBE = (MarcaBE)ViewState["oMarcaBE"];
        this.idMarca = (Int32)ViewState["idMarca"];

        oMarcaBE.co_marca = this.txtCodMarca.Text;
        oMarcaBE.no_marca = this.txtNomMarca.Text;
        oMarcaBE.nid_empresa = Int32.Parse(this.cboEmpMarca.SelectedValue);
        oMarcaBE.fl_inactivo = this.cboEstadoMarca.SelectedValue;
        oMarcaBE.co_usuario_creacion = Profile.Usuario.CUSR_ID;
        oMarcaBE.no_estacion = Profile.Estacion;
        oMarcaBE.no_usuario_red = Profile.UsuarioRed;
        
        return true;
    }

    #endregion

    #endregion

    #region "Mantenimiento Modelo"

    private void InicializaModelo()
    {
        this.oModelosBEList = new ModeloBEList();
        this.oModelosBEList.Add(new ModeloBE());
        ViewState["oModelosBEList"] = this.oModelosBEList;
        this.gvModelo.DataSource = this.oModelosBEList;

        this.gvModelo.DataBind();
        this.cboEstadoModelo.SelectedValue = ConstanteBE.FL_ESTADO_ACTIVO;
    }

    #region "Eventos Grilla gvModelo"
    protected void gvModelo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataKey dataKey;
        Int32 idModelo;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            dataKey = this.gvModelo.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["nid_modelo"].ToString(), out idModelo);

            if (idModelo == 0)
            {
                e.Row.Visible = false;
                return;
            }

            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                            , txhIdModelo.ClientID
                                            , dataKey.Values["fl_inactivo"].ToString().Trim().Equals("0") ? dataKey.Values["nid_modelo"].ToString() : "-1");

            e.Row.Attributes["ondblclick"] = String.Format("javascript: document.getElementById('{0}').value = '{1}'; document.getElementById('{2}').click();"
                                            , txhIdModelo.ClientID, dataKey.Values["nid_modelo"].ToString(), this.btnModificarModelo.ClientID);
        }
    }
    protected void gvModelo_Sorting(object sender, GridViewSortEventArgs e)
    {
        this.oModelosBEList = (ModeloBEList)ViewState["oModelosBEList"];
        SortDirection indOrden = (SortDirection)ViewState["ordenLista"];

        if (oModelosBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oModelosBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                ViewState["ordenLista"] = SortDirection.Descending;
            }
            else
            {
                oModelosBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                ViewState["ordenLista"] = SortDirection.Ascending;
            }
        }
        this.gvModelo.DataSource = oModelosBEList;
        this.gvModelo.DataBind();
        ViewState["oModelosBEList"] = oModelosBEList;

        this.txhIdModelo.Value = String.Empty;
    }
    protected void gvModelo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        oModelosBEList = (ModeloBEList)ViewState["oModelosBEList"];
        this.gvModelo.DataSource = oModelosBEList;
        this.gvModelo.PageIndex = e.NewPageIndex;
        this.gvModelo.DataBind();

        this.txhIdModelo.Value = String.Empty;
    }
    #endregion

    #region "Eventos Botones"

    protected void btnAgregarModelo_Click(object sender, ImageClickEventArgs e)
    {
        this.lblModelo.Text = ConstanteBE.TIPO_AGREGAR + " Modelo";
        this.cboEstadoModeloPoput.SelectedValue = ConstanteBE.FL_ESTADO_ACTIVO;

        cboNegocioModeloPoput_OnSelectedIndexChanged(null, null);
        this.cboEstadoModeloPoput.Enabled = false;
        ViewState["oModeloBE"] = null;
        this.txhIdModelo.Value = String.Empty;
    }

    protected void btnModificarModelo_Click(object sender, ImageClickEventArgs e)
    {
        Int32 idModelo;
        Int32.TryParse(this.txhIdModelo.Value, out idModelo);

        try
        {
            if (idModelo > 0)
            {
                this.lblModelo.Text = ConstanteBE.TIPO_MODIFICAR + " Modelo";

                cboNegocioModeloPoput_OnSelectedIndexChanged(null, null);

                ModeloBL oModeloBL = new ModeloBL();

                ModeloBE oModeloBE = new ModeloBE();
                oModeloBE = oModeloBL.GetById(idModelo);

                if (oModeloBE != null)
                {
                    this.txtCodModeloPoput.Text = oModeloBE.co_modelo;
                    this.txtNomModeloPoput.Text = oModeloBE.no_modelo;
                    this.cboNegocioModeloPoput.SelectedValue = oModeloBE.co_negocio;
                    cboNegocioModeloPoput_OnSelectedIndexChanged(null, null);
                    this.cboLineaModeloPoput.SelectedValue = oModeloBE.nid_linea_importacion.ToString();
                    cboLineaModeloPoput_OnSelectedIndexChanged(null, null);
                    if (oModeloBE.nid_linea_comercial > 0)
                    {
                        if (this.cboLineaComercialModeloPoput.Items.FindByValue(oModeloBE.nid_linea_comercial.ToString()) != null)
                            this.cboLineaComercialModeloPoput.SelectedValue = oModeloBE.nid_linea_comercial.ToString();
                    }
                    else this.cboLineaComercialModeloPoput.SelectedValue = String.Empty;

                    this.cboEstadoModeloPoput.SelectedValue = oModeloBE.fl_inactivo;
                    this.cboEstadoModeloPoput.Enabled = true;
                }
                ViewState["oModeloBE"] = oModeloBE;
            }
        }
        catch { }

        this.txhIdModelo.Value = String.Empty;
    }

    protected void btnBuscarModelo_Click(object sender, ImageClickEventArgs e)
    {
        ModeloBL oModelosBL = new ModeloBL();
        oModelosBL.ErrorEvent += new ModeloBL.ErrorDelegate(Master.Transaction_ErrorEvent);

        String codModelo = this.txtCodModelo.Text;
        String nomModelo = this.txtNomModelo.Text;
        String codEstado = this.cboEstadoModelo.SelectedValue;

        this.oModelosBEList = oModelosBL.GetAllBandeja(this.idMarca, codModelo, nomModelo, codEstado, Profile.Usuario.Nid_usuario);

        if (this.oModelosBEList == null || this.oModelosBEList.Count == 0)
        {
            JavaScriptHelper.Alert(this, Message.keyNoRegistros, "");
            this.oModelosBEList.Add(new ModeloBE());
        }

        this.gvModelo.DataSource = this.oModelosBEList;
        this.gvModelo.DataBind();
        ViewState["oModelosBEList"] = this.oModelosBEList;

        this.txhIdModelo.Value = String.Empty;
    }

    protected void btnQuitarModelo_Click(object sender, ImageClickEventArgs e)
    {
        ModeloBL oModeloBL = new ModeloBL();
        ModeloBE oModeloBE = new ModeloBE();
        oModeloBL.ErrorEvent += new ModeloBL.ErrorDelegate(Master.Transaction_ErrorEvent);
        Int32 retorno;

        try
        {
            Master.onError = false;

            oModeloBE.nid_marca = idMarca;
            oModeloBE.nid_modelo = Int32.Parse(this.txhIdModelo.Value.Trim());
            oModeloBE.co_usuario_cambio = Profile.Usuario.CUSR_ID;
            oModeloBE.no_estacion = Profile.Estacion;
            oModeloBE.no_usuario_red = Profile.UsuarioRed;

            ModeloBE oModeloBEMad = new ModeloBE();
            oModeloBEMad = oModeloBL.GetById(oModeloBE.nid_modelo);
            oModeloBE.sfe_cambio = "";
            oModeloBE.fl_inactivo = "1";

            retorno = oModeloBL.Eliminar(oModeloBE);

            if (!Master.onError && retorno > 0)
            {
                //Si todo es exito recien mostrar mensaje de eliminacin con exito
                btnBuscarModelo_Click(null, null);
                JavaScriptHelper.Alert(this, Message.keyElimino, "");
                this.txhIdModelo.Value = String.Empty;
            }
            else
            {
                if (retorno == -5) JavaScriptHelper.Alert(this, Message.keyNoEliminoRelacionado, "");
                else JavaScriptHelper.Alert(this, Message.keyNoElimino, "");
                this.txhIdModelo.Value = String.Empty;
            }
        }
        catch (Exception ex)
        {
            Master.Web_ErrorEvent(this, ex);
            btnBuscarModelo_Click(null, null);
            JavaScriptHelper.Alert(this, Message.keyNoElimino, "");
        }
    }

    protected void btnGrabarModelo_Click(object sender, EventArgs e)
    {
        Int32 retorno;
        ModeloBL oModeloBL = new ModeloBL();
        oModeloBL.ErrorEvent += new ModeloBL.ErrorDelegate(Master.Transaction_ErrorEvent);

        try
        {
            CargarEntidadModeloDesdeForm();
            ModeloBE oModeloBEMad = new ModeloBE();

            oModeloBEMad = oModeloBL.GetById(oModeloBE.nid_modelo);
            if (this.oModeloBE.nid_modelo == 0) { this.oModeloBE.sfe_cambio = String.Empty; }

            //retorno = oModelosBL.grabarModelo(this.oModeloBE);
            retorno = oModeloBL.Grabar(this.oModeloBE);

            if (retorno > 0)
            {
                if (this.oModeloBE.nid_modelo == 0) JavaScriptHelper.Alert(this, Message.keyGrabar, String.Empty);
                else JavaScriptHelper.Alert(this, Message.keyActualizar, String.Empty);

            }
            else
            {
                if (retorno == -41) JavaScriptHelper.Alert(this, Message.KeyModeloExistSGSNET, String.Empty);
                else if (retorno == -4) JavaScriptHelper.Alert(this, Message.keyErrorGrabarLlave, String.Empty);
                else if (retorno == -3) JavaScriptHelper.Alert(this, Message.keyErrorGrabarNulo, String.Empty);
                else if (retorno == -2) JavaScriptHelper.Alert(this, Message.keyErrorGrabarCodDuplicado, String.Empty);
                else if (retorno == -1) JavaScriptHelper.Alert(this, Message.keyErrorGrabar, String.Empty);
                else JavaScriptHelper.Alert(this, Message.keyErrorGrabar, String.Empty);
            }

            this.txtCodModeloPoput.Text = String.Empty;
            this.txtNomModeloPoput.Text = String.Empty;
            this.cboNegocioModeloPoput.SelectedValue = String.Empty;
            cboNegocioModeloPoput_OnSelectedIndexChanged(null, null);
            this.cboLineaModeloPoput.SelectedValue = String.Empty;
            this.cboLineaComercialModeloPoput.SelectedValue = String.Empty;
            this.cboEstadoModeloPoput.SelectedValue = String.Empty;

            this.btnBuscarModelo_Click(null, null);
        }
        catch (Exception ex)
        {
            Master.Web_ErrorEvent(this, ex);
            JavaScriptHelper.Alert(this, Message.keyErrorGrabar, String.Empty);
        }
    }

    private void CargarEntidadModeloDesdeForm()
    {
        if (ViewState["oModeloBE"] != null)
        {
            oModeloBE = (ModeloBE)ViewState["oModeloBE"];
        }
        else
        {
            oModeloBE = new ModeloBE();
        }
        oModeloBE.nid_marca = this.idMarca;
        oModeloBE.co_modelo = this.txtCodModeloPoput.Text;
        oModeloBE.no_modelo = this.txtNomModeloPoput.Text;
        oModeloBE.co_negocio = this.cboNegocioModeloPoput.SelectedValue;
        oModeloBE.nid_linea_importacion = Int32.Parse(this.cboLineaModeloPoput.SelectedValue);
        oModeloBE.nid_linea_comercial = Int32.Parse(this.cboLineaComercialModeloPoput.SelectedValue);
        oModeloBE.fl_inactivo = this.cboEstadoModeloPoput.SelectedValue;
        oModeloBE.co_usuario_creacion = Profile.Usuario.CUSR_ID;
        oModeloBE.no_estacion = Profile.Estacion;
        oModeloBE.no_usuario_red = Profile.UsuarioRed;
    }

    protected void btnRegresarModelo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/SGS_Mantenimiento/SGS_MarcaModelo_Bandeja.aspx", false);
    }

    #endregion

    #region "Eventos Combos"

    protected void cboNegocioModeloPoput_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        String codNegocio = this.cboNegocioModeloPoput.SelectedValue;
        this.cboLineaModeloPoput.cod_negocio = codNegocio;
        this.cboLineaModeloPoput.cargarCombo(ConstanteBE.OBJECTO_TIPO_SELECCIONE);
        //lIMPIO EL COMBO DE LINEA COMERCIAL
        this.cboLineaComercialModeloPoput.Items.Clear();
        this.cboLineaComercialModeloPoput.Items.Insert(0, new ListItem());
        this.cboLineaComercialModeloPoput.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
        this.cboLineaComercialModeloPoput.Items[0].Value = String.Empty;

        this.cboLineaModeloPoput.Attributes.Add("Enabled", "true");
        this.cboLineaComercialModeloPoput.Attributes.Add("Enabled", "true");

    }

    protected void cboLineaModeloPoput_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        NegocioLineaBL oNegocioLineaBL = new NegocioLineaBL();
        oNegocioLineaBL.ErrorEvent += new NegocioLineaBL.ErrorDelegate(Master.Transaction_ErrorEvent);

        Int32 idNegocioLinea;
        Int32.TryParse(this.cboLineaModeloPoput.SelectedValue.Trim(), out idNegocioLinea);

        try
        {
            this.cboLineaComercialModeloPoput.Items.Clear();
            this.cboLineaComercialModeloPoput.DataSource = oNegocioLineaBL.GetListaLineaComercial(idNegocioLinea);
            this.cboLineaComercialModeloPoput.DataTextField = "Nom_linea";
            this.cboLineaComercialModeloPoput.DataValueField = "nid_cod_tipo_linea";
            this.cboLineaComercialModeloPoput.DataBind();
            this.cboLineaComercialModeloPoput.Items.Insert(0, new ListItem());
            this.cboLineaComercialModeloPoput.Items[0].Text = ConstanteBE.OBJECTO_SELECCIONE;
            this.cboLineaComercialModeloPoput.Items[0].Value = String.Empty;

            this.cboLineaModeloPoput.Attributes.Add("Enabled", "true");
            this.cboLineaComercialModeloPoput.Attributes.Add("Enabled", "true");

        }
        catch (Exception ex)
        {
            Master.Web_ErrorEvent(this, ex);
        }
    }

    #endregion

    #endregion

    #region "Evento Tab"

    protected void tabContMarca_ActiveTabChanged(object sender, EventArgs e)
    {
        indiceTabOn = this.tabContMarca.ActiveTabIndex;

        if (this.tabContMarca.ActiveTabIndex == 1)
        {
            this.InicializaModelo();
        }

        this.ActualizaBotoneria();

        if (!TipoAccesoPagina.Equals(CONSTANTE_SEGURIDAD.AccesoEdicion))
        {
            btnGrabarMarca.Visible = false;
            btnAgregarModelo.Visible = false;
            btnQuitarModelo.Visible = false;
            btnGrabarModelo.Visible = false;
        }
        ViewState["indiceTabOn"] = indiceTabOn;
    }

    private void ActualizaBotoneria()
    {
        //Marca
        this.btnGrabarMarca.Visible = false;
        this.btnRegresarMarca.Visible = false;
        //Modelo
        this.btnAgregarModelo.Visible = false;
        this.btnBuscarModelo.Visible = false;
        this.btnQuitarModelo.Visible = false;
        this.btnLimpiarModelo.Visible = false;
        this.btnRegresarModelo.Visible = false;
        this.btnRegresar.Visible = false;

        //Tabla
        this.tblMarca.Visible = false;
        this.tblModelo.Visible = false;

        if (this.indiceTabOn == 0)
        {
            this.btnGrabarMarca.Visible = true;
            this.btnRegresarMarca.Visible = true;
            this.tblMarca.Visible = true;
            this.btnRegresarMarca.Visible = true;
        }
        else if (this.indiceTabOn == 1)
        {
            this.btnAgregarModelo.Visible = true;
            this.btnBuscarModelo.Visible = true;
            this.btnQuitarModelo.Visible = true;
            this.btnLimpiarModelo.Visible = true;
            this.tblModelo.Visible = true;
            this.btnRegresarModelo.Visible = true;
        }
    }

    #endregion


}
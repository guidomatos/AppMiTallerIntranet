using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SGS_Seguridad_SGS_Perfil_Mantenimiento : PaginaBase
{
    PerfilBE oPerfil;
    public Int32 strIndiceTabOn;
    public Int32 perfilID;
    OpcionSeguridadBEList oOpcionSeguridadBEList = new OpcionSeguridadBEList();
    private String TipoAccesoPagina = String.Empty;
    public String indRefreshGrilla = String.Empty;
    public String idFilaGrilla = String.Empty;
    UsuarioBEList oPerfilUsuarioList, oPerfilUsuarioListAsig;

    protected void Page_Load(object sender, EventArgs e)
    {
        TipoAccesoPagina = (Master as Seguridad).ValidaTipoAccesoPagina(this, CONSTANTE_SEGURIDAD.Perfiles);

        /*Asignacin de registros por pgina;*/
        SetPaginacion();

        if (!Page.IsPostBack)
        {
            Int32.TryParse(Request.QueryString["perfilID"], out perfilID);
            InicializaPagina();
            CargaPerfil();
        }

        perfilID = (Int32)ViewState["perfilID"];
        oPerfil = (PerfilBE)ViewState["oPerfil"];

        if (ViewState["strIndiceTabOn"] != null)
        {
            this.tabContPerfil.ActiveTabIndex = (Int32)ViewState["strIndiceTabOn"];
            this.strIndiceTabOn = (Int32)ViewState["strIndiceTabOn"];
        }
    }

    #region "Carga de pagina"

    private void SetPaginacion()
    {
        this.gvPerfilUsuario.PageSize = this.Profile.PageSize;
        this.gvAsigPerfilUsuario.PageSize = this.Profile.PageSizePopUp;
    }

    private void InicializaPagina()
    {
        PerfilBL oPerfilBL = new PerfilBL();
        OpcionSeguridadBE oOpcionSeguridadBE = new OpcionSeguridadBE();
        OpcionSeguridadBEList oOpcionSeguridadBEList = new OpcionSeguridadBEList();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        this.cboEstado.cargarCombo(ConstanteBE.OBJECTO_TIPO_SELECCIONE);
        this.cboEstado.SelectedValue = "0";

        oOpcionSeguridadBE.CCOAPL = Profile.Aplicacion;
        oOpcionSeguridadBE.NIVEL = 1;
        oOpcionSeguridadBE.CSTRUCT = String.Empty;
        oOpcionSeguridadBEList = oPerfilBL.GetAllOpciones(oOpcionSeguridadBE, perfilID, 0/*Profile.Usuario.NID_USUARIO*/);

        this.txhModuloSelected.Value = String.Empty;

        foreach (OpcionSeguridadBE oOpcion in oOpcionSeguridadBEList)
        {
            oOpcionSeguridadBE = new OpcionSeguridadBE();
            oOpcionSeguridadBE.CCOAPL = Profile.Aplicacion;
            oOpcionSeguridadBE.NIVEL = 0;
            oOpcionSeguridadBE.CSTRUCT = oOpcion.CSTRUCT.Trim();

            if (this.txhModuloSelected.Value.Equals(String.Empty)) this.txhModuloSelected.Value = oOpcion.CSTRUCT.Trim();

            foreach (OpcionSeguridadBE oOpcionDet in oPerfilBL.GetAllOpciones(oOpcionSeguridadBE, perfilID, 0/*Profile.Usuario.NID_USUARIO*/))
            {
                //@001 - DAC - Inicio
                //if (this.txhCodOpciones.Value.Trim().IndexOf("|" + oOpcionDet.NID_OPCION.ToString() + "|") < 0)
                //{
                //    this.txhCodOpciones.Value += this.txhCodOpciones.Value.Trim().Equals(String.Empty) ? "|" + oOpcionDet.NID_OPCION.ToString() + "|" : oOpcionDet.NID_OPCION.ToString() + "|";
                //    this.txhIndOpciones.Value += this.txhIndOpciones.Value.Trim().Equals(String.Empty) ? "|" + oOpcionDet.IND_REL + "|" : oOpcionDet.IND_REL + "|";
                //}
                if (this.txhCodOpciones.Value.Trim().IndexOf("|" + oOpcionDet.NID_OPCION.ToString().Trim() + "|") < 0)
                {
                    this.txhCodOpciones.Value += this.txhCodOpciones.Value.Trim().Equals(String.Empty) ? "|" + oOpcionDet.NID_OPCION.ToString().Trim() + "|" : oOpcionDet.NID_OPCION.ToString().Trim() + "|";
                    this.txhIndOpciones.Value += this.txhIndOpciones.Value.Trim().Equals(String.Empty) ? "|" + oOpcionDet.IND_REL.Trim() + "|" : oOpcionDet.IND_REL.Trim() + "|";
                    //I @002
                    this.txhNidOpcionPerfil.Value += this.txhNidOpcionPerfil.Value.Trim().Equals(String.Empty) ? "|" + oOpcionDet.nid_opcion_perfil.ToString() + "|" : oOpcionDet.nid_opcion_perfil.ToString() + "|";
                    //F @002
                }
                //@001 - DAC - Fin
            }
        }

        if (oOpcionSeguridadBEList != null && oOpcionSeguridadBEList.Count == 0) oOpcionSeguridadBEList.Add(new OpcionSeguridadBE());
        indRefreshGrilla = "1";
        this.gvModulos.DataSource = oOpcionSeguridadBEList;
        this.gvModulos.DataBind();
        /*oOpcionSeguridadBEList = new OpcionSeguridadBEList();
        oOpcionSeguridadBEList.Add(new OpcionSeguridadBE());
        this.gvOpciones.DataSource = oOpcionSeguridadBEList;
        this.gvOpciones.DataBind();*/
        btnRefreshOpciones_OnClick(null, null);

        this.btnRefreshOpciones.Style["display"] = "none";
        ViewState["RowIndexSelected"] = -1;

        this.btnGrabar.Visible = false;
        this.btnAgregar.Visible = false;
        this.btnEliminar.Visible = false;
        if (this.strIndiceTabOn == 0)
        {
            this.btnGrabar.Visible = true;
        }
        else
        {
            this.btnAgregar.Visible = true;
            this.btnEliminar.Visible = true;
        }
    }

    private void CargaPerfil()
    {
        PerfilBL oPerfilBL = new PerfilBL();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        if (perfilID != 0)
        {
            this.tabUsuariosAsignados.Enabled = true;
            oPerfil = oPerfilBL.GetPerfilById(perfilID);
            this.txtDscPerfil.Text = oPerfil.VDEPRF.Trim();
            this.txtFecIni.Text = oPerfil.SFEINIVIG;
            this.txtFecFin.Text = oPerfil.SFEFINVIG;
            this.txtHoraIni.Text = oPerfil.CHORINI.Trim();
            this.txtHoraFin.Text = oPerfil.CHORFIN.Trim();
            this.cboEstado.SelectedValue = oPerfil.FL_INACTIVO;

            //DAC - 21/12/2010 - Inicio
            String flConcs = "0";
            flConcs = oPerfil.fl_concesionario.Trim();
            if (flConcs.Equals("1"))
            {
                this.chkConcesionario.Checked = true;
            }
            else
            {
                this.chkConcesionario.Checked = false;
            }
            //DAC - 21/12/2010 - Fin

            this.lblTipoPerfil.Text = ConstanteBE.TIPO_MODIFICAR;
            if (!this.txhModuloSelected.Value.Equals(String.Empty))
            {
                indRefreshGrilla = "1";
                btnRefreshOpciones_OnClick(null, null);
            }
        }
        else
        {
            this.lblTipoPerfil.Text = ConstanteBE.TIPO_AGREGAR;
            this.tabUsuariosAsignados.Enabled = false;
            oPerfil = new PerfilBE();
        }

        ViewState["perfilID"] = perfilID;
        ViewState["oPerfil"] = oPerfil;
    }

    #endregion

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
            if (indRefreshGrilla == "1" &&
                dataKey.Values["CSTRUCT"].ToString().Trim().Equals(this.txhModuloSelected.Value.Trim()))
            {
                idFilaGrilla = e.Row.ClientID;
            }
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
            oOpcionSeguridadBE.CSTRUCT = this.txhModuloSelected.Value.Trim();
            this.oOpcionSeguridadBEList = oPerfilBL.GetAllOpciones(oOpcionSeguridadBE, perfilID, 0/*Profile.Usuario.NID_USUARIO*/);

            if (oOpcionSeguridadBEList != null && oOpcionSeguridadBEList.Count == 0) oOpcionSeguridadBEList.Add(new OpcionSeguridadBE());

            this.chkMantAll.Checked = true;
            this.chkConsAll.Checked = true;

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

        bool flagIndVerChecks = true;
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
                //chkConst.Attributes["disabled"] = "true";
                chkConst.Enabled = false;
            }
            else if (indRelacion.Equals("C"))
            {
                chkMant.Checked = false;
                chkConst.Checked = true;
                //chkConst.Attributes["disabled"] = "false";
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

    #region "Eventos Botones"

    protected void btnGrabar_OnClick(object sender, EventArgs e)
    {
        try
        {
            Int32 retorno;
            PerfilBL oPerfilBL = new PerfilBL();
            oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

            this.CargaDesdeForm();

            retorno = oPerfilBL.GrabarPerfil(this.oPerfil);
            if (retorno > 0)
            {
                this.perfilID = retorno;
                this.oPerfil.NID_PERFIL = this.perfilID;
                /*Insertamos detalle de opciones por perfil*/

                Int32 intRetornoP = 0;

                String XML = Global.ObtenerXmlPerfil(this.txhCodOpciones.Value, this.txhIndOpciones.Value, this.txhNidOpcionPerfil.Value.Trim());

                intRetornoP = oPerfilBL.InsertarOpcionByPerfil(this.oPerfil, XML);

                if (intRetornoP > 0)
                {
                    JavaScriptHelper.Alert(this, Message.keyGrabar, "");
                }
                else
                {
                    JavaScriptHelper.Alert(this, Message.keyErrorGrabar, "");
                }

                this.CargaPerfil();
            }
            else
            {
                JavaScriptHelper.Alert(this, Message.keyErrorGrabar, "");
            }
        }
        catch
        {
            JavaScriptHelper.Alert(this, Message.keyErrorGrabar, "");
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("SGS_Perfil_Bandeja.aspx", false);
    }

    private void CargaDesdeForm()
    {
        oPerfil = (PerfilBE)ViewState["oPerfil"];
        perfilID = (Int32)ViewState["perfilID"];

        oPerfil.NID_PERFIL = perfilID;
        oPerfil.VDEPRF = this.txtDscPerfil.Text.Trim();
        oPerfil.SFEINIVIG = this.txtFecIni.Text.Trim();
        oPerfil.SFEFINVIG = this.txtFecFin.Text.Trim();
        oPerfil.CHORINI = this.txtHoraIni.Text;
        oPerfil.CHORFIN = this.txtHoraFin.Text;

        String fl_Concs = "0";
        if (this.chkConcesionario.Checked)
        {
            fl_Concs = "1";
        }
        oPerfil.fl_concesionario = fl_Concs;

        oPerfil.CCOAPL = Profile.Aplicacion;
        oPerfil.CO_USUARIO_CREA = Profile.Usuario.CUSR_ID;
        oPerfil.CO_USUARIO_CAMBIO = Profile.Usuario.CUSR_ID;
        oPerfil.NO_ESTACION_RED = Profile.Estacion;
        oPerfil.NO_USUARIO_RED = Profile.UsuarioRed;

        oPerfil.FL_INACTIVO = this.cboEstado.SelectedValue;
    }

    #endregion

    #region "Eventos de TAB"
    protected void tabContPerfil_ActiveTabChanged(object sender, EventArgs e)
    {
        strIndiceTabOn = this.tabContPerfil.ActiveTabIndex;

        if (strIndiceTabOn == 1)
        {
            InicializaPerfilUsuario();
        }
        ActualizaTabs();
        ViewState["strIndiceTabOn"] = strIndiceTabOn;
    }

    private void ActualizaTabs()
    {
        this.btnGrabar.Visible = false;
        this.btnAgregar.Visible = false;
        this.btnEliminar.Visible = false;

        this.tblNuevoPerfil.Visible = false;
        this.tblUsuariosAsignados.Visible = false;
        this.pnlAsigPerfilUsuario.Visible = false;

        if (this.strIndiceTabOn == 0)
        {
            this.btnGrabar.Visible = true;
            this.tblNuevoPerfil.Visible = true;
        }
        else
        {
            this.btnAgregar.Visible = true;
            this.btnEliminar.Visible = true;
            this.tblUsuariosAsignados.Visible = true;
            this.pnlAsigPerfilUsuario.Visible = true;
        }
    }

    #endregion

    #region "Perfil Usuario"

    private void InicializaPerfilUsuario()
    {
        PerfilBL oPerfilBL = new PerfilBL();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        TipoTablaDetalleBL oTipoTablaDetalleBL = new TipoTablaDetalleBL();
        oTipoTablaDetalleBL.ErrorEvent += new TipoTablaDetalleBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        TipoTablaDetalleBEList oTipoTablaDetalleBEList;
        try
        {
            this.txhPerfilUsuarioID.Value = String.Empty;

            //Cargando bandeja de usuarios relacionados
            (Master as Seguridad).onError = false;
            oPerfilUsuarioList = oPerfilBL.GetBandejaUsuariosRelacionados(this.perfilID, Profile.Aplicacion);
            if ((Master as Seguridad).onError) oPerfilUsuarioList = new UsuarioBEList();
            if (oPerfilUsuarioList.Count == 0) oPerfilUsuarioList.Add(new UsuarioBE());
            this.gvPerfilUsuario.DataSource = oPerfilUsuarioList;
            this.gvPerfilUsuario.DataBind();

            /*Combo Tipo*/
            oTipoTablaDetalleBEList = oTipoTablaDetalleBL.ListarTipoTablaDetalle(ConstanteBE.NID_TABLA_TIPO_USUARIO.ToString(), String.Empty, String.Empty
                                                    , String.Empty, String.Empty, String.Empty, String.Empty);
            this.cboTipo.DataSource = oTipoTablaDetalleBEList;
            this.cboTipo.DataTextField = "Valor1";
            this.cboTipo.DataValueField = "Id_tabla_detalle";
            this.cboTipo.DataBind();
            this.cboTipo.Items.Insert(0, new ListItem());
            this.cboTipo.Items[0].Text = ConstanteBE.OBJECTO_TODOS;
            this.cboTipo.Items[0].Value = String.Empty;

            //Cargando Bandeja de Asigancion de usuarios
            oPerfilUsuarioListAsig = new UsuarioBEList();
            oPerfilUsuarioListAsig.Add(new UsuarioBE());
            this.gvAsigPerfilUsuario.DataSource = oPerfilUsuarioListAsig;
            this.gvAsigPerfilUsuario.DataBind();

        }
        catch (Exception ex)
        {
            (Master as Seguridad).Web_ErrorEvent(this, ex);
        }

        ViewState["oPerfilUsuarioList"] = oPerfilUsuarioList;
        ViewState["ordenBandejaPerfilUsuario"] = SortDirection.Descending;

        ViewState["oPerfilUsuarioListAsig"] = oPerfilUsuarioListAsig;
        ViewState["ordenAsignacionPerfilUsuario"] = SortDirection.Descending;
    }

    protected void gvPerfilUsuario_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = this.gvPerfilUsuario.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["NID_PERFIL"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }

            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                            , this.txhPerfilUsuarioID.ClientID, dataKey.Values["NID_PERFIL"].ToString());

        }
    }

    protected void gvPerfilUsuario_Sorting(Object sender, GridViewSortEventArgs e)
    {
        oPerfilUsuarioList = (UsuarioBEList)ViewState["oPerfilUsuarioList"];
        SortDirection indOrden = (SortDirection)ViewState["ordenBandejaPerfilUsuario"];

        if (oPerfilUsuarioList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oPerfilUsuarioList.Ordenar(e.SortExpression, direccionOrden.Descending);
                ViewState["ordenBandejaPerfilUsuario"] = SortDirection.Descending;
            }
            else
            {
                oPerfilUsuarioList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                ViewState["ordenBandejaPerfilUsuario"] = SortDirection.Ascending;
            }
        }
        this.gvPerfilUsuario.DataSource = oPerfilUsuarioList;
        this.gvPerfilUsuario.DataBind();
        this.txhPerfilUsuarioID.Value = String.Empty;
        ViewState["oPerfilUsuarioList"] = oPerfilUsuarioList;
    }

    protected void gvPerfilUsuario_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        oPerfilUsuarioList = (UsuarioBEList)ViewState["oPerfilUsuarioList"];
        this.gvPerfilUsuario.DataSource = oPerfilUsuarioList;
        this.gvPerfilUsuario.PageIndex = e.NewPageIndex;
        this.gvPerfilUsuario.DataBind();
        this.txhPerfilUsuarioID.Value = String.Empty;
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        UsuarioBE oUsuario = new UsuarioBE();
        PerfilBL oPerfilBL = new PerfilBL();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        try
        {
            oUsuario.NID_PERFIL = Int32.Parse(this.txhPerfilUsuarioID.Value);
            oUsuario.CO_USUARIO_CAMBIO = Profile.Usuario.CUSR_ID;
            oUsuario.NO_ESTACION_RED = Profile.Estacion;
            oUsuario.NO_USUARIO_RED = Profile.UsuarioRed;

            (Master as Seguridad).onError = false;
            if (oPerfilBL.EliminarUsuariosRelacionados(oUsuario) > 0 && !(Master as Seguridad).onError)
            {
                InicializaPerfilUsuario();
                JavaScriptHelper.Alert(this, Message.keyElimino, String.Empty);
            }
            else
            {
                JavaScriptHelper.Alert(this, Message.keyNoElimino, String.Empty);
            }
        }
        catch (Exception ex)
        {
            JavaScriptHelper.Alert(this, Message.keyNoElimino, String.Empty);
            (Master as Seguridad).Web_ErrorEvent(this, ex);
        }
        this.txhPerfilUsuarioID.Value = String.Empty;
    }

    #region "Asignacion de Usuario"
    protected void btnBuscarPerfilUsuario_Click(object sender, EventArgs e)
    {
        UsuarioBE oUsuarioBE = new UsuarioBE();
        PerfilBL oPerfilBL = new PerfilBL();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        try
        {
            oUsuarioBE.NID_PERFIL = this.perfilID;
            oUsuarioBE.VNOMUSR = this.txtNomUsuario.Text;
            oUsuarioBE.NO_APE_PATERNO = this.txtApePat.Text;
            oUsuarioBE.NO_APE_MATERNO = this.txtApeMat.Text;

            int tipoUsuario;
            Int32.TryParse(this.cboTipo.SelectedValue, out tipoUsuario);
            oUsuarioBE.NID_TIPO = tipoUsuario;

            (Master as Seguridad).onError = false;
            oPerfilUsuarioListAsig = oPerfilBL.GetAsignacionUsuariosRelacionados(oUsuarioBE, Profile.Aplicacion);
            if ((Master as Seguridad).onError) oPerfilUsuarioListAsig = new UsuarioBEList();

            this.txhCadenaTotalPU.Value = "|";
            for (int i = 0; i < this.oPerfilUsuarioListAsig.Count; i++)
            {
                this.txhCadenaTotalPU.Value = this.txhCadenaTotalPU.Value + this.oPerfilUsuarioListAsig[i].NID_USUARIO.ToString() + "|";
            }

            if (oPerfilUsuarioListAsig.Count == 0)
            {
                oPerfilUsuarioListAsig.Add(new UsuarioBE());
                JavaScriptHelper.Alert(this, Message.keyNoRegistros, String.Empty);
            }
            this.gvAsigPerfilUsuario.DataSource = oPerfilUsuarioListAsig;
            this.gvAsigPerfilUsuario.DataBind();

        }
        catch (Exception ex)
        {
            (Master as Seguridad).Web_ErrorEvent(this, ex);
        }

        this.txhFlagChekTodosPU.Value = "0";
        this.txhCadenaSelPU.Value = String.Empty;
        this.mpAsigPerfilUsuario.Show();

        ViewState["oPerfilUsuarioListAsig"] = oPerfilUsuarioListAsig;
    }

    protected void btnNuevoPerfilUsuario1_Click(object sender, EventArgs e)
    {

        try
        {
            txhNroFilasPU.Value = "";
            txhFlagChekTodosPU.Value = "";
            txhCadenaSelPU.Value = "";
            txhCadenaTotalPU.Value = "|";

            oPerfilUsuarioListAsig = new UsuarioBEList();
            oPerfilUsuarioListAsig.Add(new UsuarioBE());
            this.gvAsigPerfilUsuario.DataSource = oPerfilUsuarioListAsig;
            this.gvAsigPerfilUsuario.DataBind();

            ViewState["oPerfilUsuarioListAsig"] = oPerfilUsuarioListAsig;
        }
        catch (Exception ex)
        {
            (Master as Seguridad).Web_ErrorEvent(this, ex);
        }
    }

    protected void btnAsignarPerfilUsuario_Click(object sender, EventArgs e)
    {
        PerfilBL oPerfilBL = new PerfilBL();
        UsuarioBE oUsuarioBE = new UsuarioBE();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        Int32 retorno;
        try
        {

            oUsuarioBE.NID_PERFIL = this.perfilID;
            oUsuarioBE.CO_USUARIO_CREA = Profile.Usuario.CUSR_ID;
            oUsuarioBE.NO_USUARIO_RED = Profile.UsuarioRed;
            oUsuarioBE.NO_ESTACION_RED = Profile.Estacion;

            (Master as Seguridad).onError = false;

            retorno = oPerfilBL.InsertarUsuariosRelacionado(oUsuarioBE, Profile.Aplicacion, this.txhCadenaSelPU.Value);

            if (!(Master as Seguridad).onError && retorno > 0)
            {
                JavaScriptHelper.Alert(this, Message.keyGrabar, String.Empty);
                InicializaPerfilUsuario();
            }
            else if (retorno == -2)
            {
                JavaScriptHelper.Alert(this, Message.keyUsuariosNoVigentes, String.Empty);
            }
            else
            {
                JavaScriptHelper.Alert(this, Message.keyErrorGrabar, String.Empty);
            }
        }
        catch (Exception ex)
        {
            Master.Web_ErrorEvent(this, ex);
            JavaScriptHelper.Alert(this, Message.keyErrorGrabar, String.Empty);
        }

        this.mpAsigPerfilUsuario.Hide();
    }

    protected void gvAsigPerfilUsuario_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        CheckBox chkSel, chkSelCabecera;

        if (e.Row.RowType == DataControlRowType.Header)
        {
            this.txhNroFilasPU.Value = "0";
            chkSelCabecera = (CheckBox)e.Row.FindControl("chkSelCabecera");
            chkSelCabecera.Attributes["onclick"] = String.Format("javascript: Fc_SelecDeselecTodos('{0}','{1}','{2}','{3}','{4}')"
                    , this.txhNroFilasPU.ClientID, this.txhFlagChekTodosPU.ClientID, this.txhCadenaSelPU.ClientID
                    , this.gvAsigPerfilUsuario.ClientID, this.txhCadenaTotalPU.ClientID);

            if (this.txhFlagChekTodosPU.Value.Equals("1")) { chkSelCabecera.Checked = true; }
            else { chkSelCabecera.Checked = false; }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = this.gvAsigPerfilUsuario.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["NID_USUARIO"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }

            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this);");
            /*e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SGS_Perfil_Mantenimiento.aspx?perfilID={0}'", dataKey.Values["NID_PERFIL"].ToString());*/

            this.txhNroFilasPU.Value = Convert.ToString(Convert.ToInt32(this.txhNroFilasPU.Value) + 1);
            chkSel = (CheckBox)e.Row.FindControl("chkSel");
            chkSel.Attributes["onclick"] = String.Format("javascript:Fc_SeleccionaItem('{0}','{1}')", this.txhCadenaSelPU.ClientID, dataKey.Values["NID_USUARIO"].ToString());

            if (this.txhCadenaSelPU.Value.Contains("|" + dataKey.Values["NID_USUARIO"].ToString() + "|")) { chkSel.Checked = true; }
            else { chkSel.Checked = false; }
        }

    }

    protected void gvAsigPerfilUsuario_Sorting(Object sender, GridViewSortEventArgs e)
    {
        oPerfilUsuarioListAsig = (UsuarioBEList)ViewState["oPerfilUsuarioListAsig"];
        SortDirection indOrden = (SortDirection)ViewState["ordenAsignacionPerfilUsuario"];

        if (oPerfilUsuarioListAsig != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oPerfilUsuarioListAsig.Ordenar(e.SortExpression, direccionOrden.Descending);
                ViewState["ordenAsignacionPerfilUsuario"] = SortDirection.Descending;
            }
            else
            {
                oPerfilUsuarioListAsig.Ordenar(e.SortExpression, direccionOrden.Ascending);
                ViewState["ordenAsignacionPerfilUsuario"] = SortDirection.Ascending;
            }
        }
        this.gvAsigPerfilUsuario.DataSource = oPerfilUsuarioListAsig;
        this.gvAsigPerfilUsuario.DataBind();

        ViewState["oPerfilUsuarioListAsig"] = oPerfilUsuarioListAsig;
    }

    protected void gvAsigPerfilUsuario_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        oPerfilUsuarioListAsig = (UsuarioBEList)ViewState["oPerfilUsuarioListAsig"];
        this.gvAsigPerfilUsuario.DataSource = oPerfilUsuarioListAsig;
        this.gvAsigPerfilUsuario.PageIndex = e.NewPageIndex;
        this.gvAsigPerfilUsuario.DataBind();
    }

    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Grilla de usuarios asignados al perfil
        if (ViewState["oPerfilUsuarioList"] != null &&
            this.gvPerfilUsuario != null &&
            this.gvPerfilUsuario.Rows.Count > 0)
        {
            GridViewRow oRow = this.gvPerfilUsuario.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", ((UsuarioBEList)ViewState["oPerfilUsuarioList"]).Count);

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }

        //Grilla de usuarios para asignar
        if (ViewState["oPerfilUsuarioListAsig"] != null &&
            this.gvAsigPerfilUsuario != null &&
            this.gvAsigPerfilUsuario.Rows.Count > 0)
        {
            GridViewRow oRow = this.gvAsigPerfilUsuario.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", ((UsuarioBEList)ViewState["oPerfilUsuarioListAsig"]).Count);

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }

    #endregion
}

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SGS_Seguridad_SGS_Usuario_Bandeja : PaginaBase
{
    UsuarioBEList oUsuarioListBE;
    private String TipoAccesoPagina;

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
            InicializaPagina();
            Regresar();
        }

        /*CONTROL DE ACCESO A PGINA*/
        TipoAccesoPagina = (String)ViewState["TipoAccesoPagina"];
        /*
         * Si el acceso es diferente a Edicion se tiene que ocualtar todos los botones de Agregar, eliminar y 
         * las acciones de grabar.
         */
        if (!TipoAccesoPagina.Equals(CONSTANTE_SEGURIDAD.AccesoEdicion))
        {
            this.btnAgregar.Visible = false;
            this.btnEliminar.Visible = false;
        }
    }

    #region "Carga de pgina"

    private void SetPaginacion()
    {
        this.gvUsuarios.PageSize = this.Profile.PageSize;
    }

    private void InicializaPagina()
    {
        oUsuarioListBE = new UsuarioBEList();
        oUsuarioListBE.Add(new UsuarioBE());

        this.gvUsuarios.DataSource = oUsuarioListBE;
        this.gvUsuarios.DataBind();

        this.cboEstado.cargarCombo(ConstanteBE.OBJECTO_TIPO_TODOS);
        this.cboEstado.SelectedValue = "0";

        PerfilBL oPerfilBL = new PerfilBL();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        PerfilBEList oPerfilBEList = oPerfilBL.GetPerfilesBandeja(Profile.Aplicacion, String.Empty, ConstanteBE.FL_ESTADO_ACTIVO, "");

        this.cboPerfil.DataSource = oPerfilBEList;
        this.cboPerfil.DataTextField = "VDEPRF";
        this.cboPerfil.DataValueField = "NID_PERFIL";
        this.cboPerfil.DataBind();

        this.cboPerfil.Items.Insert(0, new ListItem());
        this.cboPerfil.Items[0].Text = ConstanteBE.OBJECTO_TODOS;
        this.cboPerfil.Items[0].Value = String.Empty;

        ViewState["oUsuarioListBE"] = oUsuarioListBE;
    }
    #endregion

    #region "Metodos Grilla gvUsuarios"
    protected void gvUsuarios_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        CheckBox chkSelNum, chkSelCabeceraNum;
        String valorIdvin;

        if (e.Row.RowType == DataControlRowType.Header)
        {
            this.txhNroFilasNum.Value = "0";
            chkSelCabeceraNum = (CheckBox)e.Row.FindControl("chkSelCabeceraNum");
            chkSelCabeceraNum.Attributes.Add("onclick", "javascript:Fc_SelecDeselecTodos()");
            if (this.txhFlagChekTodosNum.Value.Equals("1")) { chkSelCabeceraNum.Checked = true; }
            else { chkSelCabeceraNum.Checked = false; }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = this.gvUsuarios.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["NID_USUARIO"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }

            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                            , txhUsuarioID.ClientID
                                            , dataKey.Values["fl_inactivo"].ToString().Trim().Equals("0") ? dataKey.Values["NID_USUARIO"].ToString() : "-1");
            e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SGS_Usuario_Mantenimiento.aspx?usuarioID={0}'", dataKey.Values["NID_USUARIO"].ToString());
            if (e.Row.Visible == true)
            {
                this.txhNroFilasNum.Value = Convert.ToString(Convert.ToInt32(this.txhNroFilasNum.Value) + 1);
                dataKey = this.gvUsuarios.DataKeys[e.Row.RowIndex];
                valorIdvin = dataKey.Values["NID_USUARIO"].ToString();
                chkSelNum = (CheckBox)e.Row.FindControl("chkSelNum");
                chkSelNum.Attributes.Add("onclick", "javascript:Fc_SeleccionaItemAsigNum('" + valorIdvin + "')");
                if (this.txhCadenaSelNum.Value.Contains("|" + valorIdvin + "|").Equals(true) && chkSelNum.Enabled == true)
                { chkSelNum.Checked = true; }
                else { chkSelNum.Checked = false; }
            }
        }

    }

    protected void gvUsuarios_Sorting(Object sender, GridViewSortEventArgs e)
    {
        oUsuarioListBE = (UsuarioBEList)ViewState["oUsuarioListBE"];
        SortDirection indOrden = (SortDirection)ViewState["ordenLista"];

        if (oUsuarioListBE != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oUsuarioListBE.Ordenar(e.SortExpression, direccionOrden.Descending);
                ViewState["ordenLista"] = SortDirection.Descending;
            }
            else
            {
                oUsuarioListBE.Ordenar(e.SortExpression, direccionOrden.Ascending);
                ViewState["ordenLista"] = SortDirection.Ascending;
            }
        }
        this.gvUsuarios.DataSource = oUsuarioListBE;
        this.gvUsuarios.DataBind();
        ViewState["oUsuarioListBE"] = oUsuarioListBE;
    }

    protected void gvUsuarios_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        oUsuarioListBE = (UsuarioBEList)ViewState["oUsuarioListBE"];
        this.gvUsuarios.DataSource = oUsuarioListBE;
        this.gvUsuarios.PageIndex = e.NewPageIndex;
        this.gvUsuarios.DataBind();
    }
    #endregion

    #region "Metodos Botones"
    protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SGS_Usuario_Mantenimiento.aspx", false);
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        UsuarioBL oUsuarioBL = new UsuarioBL();
        UsuarioBE oUsuarioBE = new UsuarioBE();
        int perfilID;
        oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        oUsuarioBE.VNOMUSR = this.txtNombres.Text;
        oUsuarioBE.NO_APE_PATERNO = this.txtApeMat.Text;
        oUsuarioBE.NO_APE_MATERNO = this.txtApePat.Text;
        oUsuarioBE.NID_UBICA = 0;
        oUsuarioBE.NID_ROL = 0;

        Int32.TryParse(this.cboPerfil.SelectedValue, out perfilID);
        oUsuarioBE.NID_PERFIL = perfilID;
        oUsuarioBE.FL_INACTIVO = this.cboEstado.SelectedValue;
        oUsuarioBE.NU_TIPO_DOCUMENTO = this.txtNumDoc.Text;

        Int32 nidPuntoVenta = 0;
        Int32.TryParse(this.cboPuntoVenta.SelectedValue, out nidPuntoVenta);
        oUsuarioBE.VNOMUSR_CUSR_ID = this.txtLogin.Text.Trim();

        oUsuarioListBE = oUsuarioBL.GetAllUsuarioBandeja(oUsuarioBE, Profile.Aplicacion);

        if (oUsuarioListBE == null || oUsuarioListBE.Count == 0)
        {
            JavaScriptHelper.Alert(this, Message.keyNoRegistros, String.Empty);
            oUsuarioListBE.Add(new UsuarioBE());
            GuardaParametros(0);
        }
        else
        {
            this.txhCadenaTotalNum.Value = "|";
            this.txhCadenaSelNum.Value = "";
            txhFlagChekTodosNum.Value = "";
            if (oUsuarioListBE != null || oUsuarioListBE.Count > 0)
            {
                for (int i = 0; i < oUsuarioListBE.Count; i++)
                {
                    this.txhCadenaTotalNum.Value = this.txhCadenaTotalNum.Value +
                        oUsuarioListBE[i].NID_USUARIO.ToString() + "|";

                }
                GuardaParametros(0);
            }
            GuardaParametros(1);
        }

        this.gvUsuarios.DataSource = oUsuarioListBE;
        this.gvUsuarios.DataBind();
        ViewState["oUsuarioListBE"] = oUsuarioListBE;
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        UsuarioBL oUsuarioBL = new UsuarioBL();
        UsuarioBE oUsuarioBE = new UsuarioBE();
        oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        
        Int32 retorno = 0, contador;

        String cadNidPuntoVenta = this.txhCadenaSelNum.Value.Trim();
        String[] arrCodigos = this.txhCadenaSelNum.Value.Trim().Split('|');
        contador = arrCodigos.Length - 2;
        
        //oUsuarioBE.NID_USUARIO = Int32.Parse(this.txhUsuarioID.Value.Trim());
        oUsuarioBE.CO_USUARIO_CREA = Profile.Usuario.CUSR_ID;
        oUsuarioBE.NO_ESTACION_RED = Profile.Estacion;
        oUsuarioBE.NO_USUARIO_RED = Profile.UsuarioRed;

        
        retorno = oUsuarioBL.EliminarUsuarioMasivo(oUsuarioBE, cadNidPuntoVenta, contador);
        

        if (retorno > 0)
        {
            JavaScriptHelper.Alert(this, Message.keyElimino, String.Empty);
            btnBuscar_Click(null, null);
            this.txhUsuarioID.Value = String.Empty;
        }
        else
        {
            JavaScriptHelper.Alert(this, Message.keyNoElimino, String.Empty);
            this.txhUsuarioID.Value = String.Empty;
        }
    }
    
    protected void btnActivar_Click(object sender, ImageClickEventArgs e)
    {
        UsuarioBL oUsuarioBL = new UsuarioBL();
        UsuarioBE oUsuarioBE = new UsuarioBE();
        oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);
        Int32 retorno = 0, contador;

        String cadNidPuntoVenta = this.txhCadenaSelNum.Value.Trim();
        String[] arrCodigos = this.txhCadenaSelNum.Value.Trim().Split('|');
        contador = arrCodigos.Length - 2;

        //oUsuarioBE.NID_USUARIO = Int32.Parse(this.txhUsuarioID.Value.Trim());
        oUsuarioBE.CO_USUARIO_CREA = Profile.Usuario.CUSR_ID;
        oUsuarioBE.NO_ESTACION_RED = Profile.Estacion;
        oUsuarioBE.NO_USUARIO_RED = Profile.UsuarioRed;

        retorno = oUsuarioBL.ActivarUsuarioMasivo(oUsuarioBE, cadNidPuntoVenta, contador);
        if (retorno > 0)
        {
            JavaScriptHelper.Alert(this, Message.keyActivo, String.Empty);
            btnBuscar_Click(null, null);
            this.txhUsuarioID.Value = String.Empty;
        }
        else
        {
            JavaScriptHelper.Alert(this, Message.keyNoElimino, String.Empty);
            this.txhUsuarioID.Value = String.Empty;
        }
    }
    
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja de perfiles
        if (ViewState["oUsuarioListBE"] != null &&
            this.gvUsuarios != null &&
            this.gvUsuarios.Rows.Count > 0 &&
            this.gvUsuarios.PageCount > 1)
        {
            GridViewRow oRow = this.gvUsuarios.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", ((UsuarioBEList)ViewState["oUsuarioListBE"]).Count);

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }

    #region "PARAMETROS DE BANDEJA"
    private void GuardaParametros(Int32 flatBusqueda)
    {
        String nombres = this.txtNombres.Text.Trim();
        String apePaterno = this.txtApePat.Text.Trim();
        String apeMaterno = this.txtApeMat.Text.Trim();
        String combo_perfil = this.cboPerfil.SelectedValue.ToString();

        String estado = this.cboEstado.SelectedValue.ToString();

        String idUsuario = Profile.Usuario.NID_USUARIO.ToString();


        String coPaginaActual = CONSTANTE_SEGURIDAD.Usuarios;

        //ProfileCommon profile = Profile.GetProfile(Profile.Usuario.CUSR_ID);
        ParametrosPaginaBEList oParametrosPaginaBEListSave = Profile.Paginas;
        if (oParametrosPaginaBEListSave == null || oParametrosPaginaBEListSave.Count == 0)
        {
            ParametrosPaginaBE oParametrosPaginaBE = new ParametrosPaginaBE();
            oParametrosPaginaBE.co_pagina = coPaginaActual;
            oParametrosPaginaBE.nu_tab = 0; //indiceTabOn;

            oParametrosPaginaBE.filtros.Add(nombres);//0
            oParametrosPaginaBE.filtros.Add(apePaterno);
            oParametrosPaginaBE.filtros.Add(apeMaterno);//
            oParametrosPaginaBE.filtros.Add(combo_perfil);
            oParametrosPaginaBE.filtros.Add(estado);

            oParametrosPaginaBE.filtros.Add(idUsuario);
            oParametrosPaginaBE.filtros.Add(Convert.ToString(flatBusqueda));


            if (oParametrosPaginaBEListSave == null)
                oParametrosPaginaBEListSave = new ParametrosPaginaBEList();

            oParametrosPaginaBEListSave.Add(oParametrosPaginaBE);
            Profile.Paginas = oParametrosPaginaBEListSave;
            //Profile.Save();
        }
        else
        {

            ParametrosPaginaBE oParametrosPaginaBEBusqueda = oParametrosPaginaBEListSave.Find(delegate(ParametrosPaginaBE p) { return p.co_pagina == coPaginaActual && p.nu_tab == 0; });

            if (oParametrosPaginaBEBusqueda != null)
            {
                oParametrosPaginaBEListSave.Remove(oParametrosPaginaBEBusqueda);
            }

            ParametrosPaginaBE oParametrosPaginaBE = new ParametrosPaginaBE();
            oParametrosPaginaBE.co_pagina = coPaginaActual;
            oParametrosPaginaBE.nu_tab = 0;// indiceTabOn;

            oParametrosPaginaBE.filtros.Add(nombres);//0
            oParametrosPaginaBE.filtros.Add(apePaterno);
            oParametrosPaginaBE.filtros.Add(apeMaterno);//
            oParametrosPaginaBE.filtros.Add(combo_perfil);
            oParametrosPaginaBE.filtros.Add(estado);

            oParametrosPaginaBE.filtros.Add(idUsuario);
            oParametrosPaginaBE.filtros.Add(Convert.ToString(flatBusqueda));

            oParametrosPaginaBEListSave.Add(oParametrosPaginaBE);
            Profile.Paginas = oParametrosPaginaBEListSave;
            //Profile.Save();
        }
    }

    private void Regresar()
    {
        String coPaginaActual = CONSTANTE_SEGURIDAD.Usuarios;
        ParametrosPaginaBEList oParametrosPaginaBEListSave = Profile.Paginas;

        //Verificamos si es llamado desde el menu o si es llamado desde una pgina(regresar)
        String indFirstCall = Request.QueryString["fc"];
        if (indFirstCall != null && indFirstCall.Equals("1"))
        {
            if (oParametrosPaginaBEListSave != null && oParametrosPaginaBEListSave.Count > 0)
            {
                ParametrosPaginaBE oParametrosPaginaBEBusqueda = oParametrosPaginaBEListSave.Find(delegate(ParametrosPaginaBE p) { return p.co_pagina == coPaginaActual && p.nu_tab == 0; });
                if (oParametrosPaginaBEBusqueda != null) oParametrosPaginaBEListSave.Remove(oParametrosPaginaBEBusqueda);
                Profile.Paginas = oParametrosPaginaBEListSave;
            }
        }
        else
        {

            if (oParametrosPaginaBEListSave != null || oParametrosPaginaBEListSave.Count > 0)
            {
                ParametrosPaginaBE oParametrosPaginaBEBusqueda = oParametrosPaginaBEListSave.Find(delegate(ParametrosPaginaBE p) { return p.co_pagina == coPaginaActual && p.nu_tab == 0; });

                if (oParametrosPaginaBEBusqueda != null)
                {

                    this.txtNombres.Text = oParametrosPaginaBEBusqueda.filtros[0];
                    this.txtApePat.Text = oParametrosPaginaBEBusqueda.filtros[1];
                    this.txtApeMat.Text = oParametrosPaginaBEBusqueda.filtros[2];
                    this.cboPerfil.SelectedValue = oParametrosPaginaBEBusqueda.filtros[3];
                    this.cboEstado.SelectedValue = oParametrosPaginaBEBusqueda.filtros[4];

                    if (oParametrosPaginaBEBusqueda.filtros[6].Equals("1"))
                        btnBuscar_Click(null, null);

                    //ActualizaBotoneria(oParametrosPaginaBEBusqueda.nu_tab);
                }
            }
        }
    }
    #endregion
}

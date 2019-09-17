using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SGS_Mantenimiento_SGS_MarcaModelo_Bandeja : PaginaBase
{
    MarcaBEList oMarcaBEList;
    private String TipoAccesoPagina;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            /*CONTROL DE ACCESO A PÁGINA*/
            TipoAccesoPagina = Master.ValidaTipoAccesoPagina(this, CONSTANTE_SEGURIDAD.Marca_Modelo);
            ViewState["TipoAccesoPagina"] = TipoAccesoPagina;
            /*FIN CONTROL DE ACCESO A PÁGINA*/
            ViewState["ordenLista"] = SortDirection.Descending;
            InicializaPagina();
            Regresar();
        }
        /*CONTROL DE ACCESO A PÁGINA*/
        TipoAccesoPagina = (String)ViewState["TipoAccesoPagina"];
        if (!TipoAccesoPagina.Equals(CONSTANTE_SEGURIDAD.AccesoEdicion))
        {
            btnEliminarMarca.Visible = false;
            btnAgregarMarca.Visible = false;
        }
        /*FIN CONTROL DE ACCESO A PÁGINA*/
    }

    #region "Carga Pagina"
    private void InicializaPagina()
    {
        oMarcaBEList = new MarcaBEList();
        oMarcaBEList.Add(new MarcaBE());
        ViewState["oMarcaBEList"] = oMarcaBEList;

        this.gvMarca.DataSource = oMarcaBEList;
        this.gvMarca.DataBind();
        //Combo Estado
        this.cboEstadoMarca.cargarCombo(ConstanteBE.OBJECTO_TIPO_TODOS);
        this.cboEstadoMarca.SelectedValue = ConstanteBE.FL_ESTADO_ACTIVO;
        //Valor Paginado
        this.gvMarca.PageSize = Profile.PageSize;

    }
    #endregion

    #region "Metodos Grilla gvMarca"
    protected void gvMarca_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataKey dataKey;
        Int32 idMarca;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            dataKey = this.gvMarca.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["nid_marca"].ToString(), out idMarca);

            if (idMarca == 0)
            {
                e.Row.Visible = false;
                return;
            }

            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}';"
                                            , txhIdMarca.ClientID
                                            , dataKey.Values["fl_inactivo"].ToString().Trim().Equals("0") ? dataKey.Values["nid_marca"].ToString() : "-1"
                                            );
            e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SGS_MarcaModelo_Mantenimiento.aspx?idSelMarca={0}'", dataKey.Values["nid_marca"].ToString());
        }
    }
    protected void gvMarca_Sorting(object sender, GridViewSortEventArgs e)
    {
        oMarcaBEList = (MarcaBEList)ViewState["oMarcaBEList"];
        SortDirection indOrden = (SortDirection)ViewState["ordenLista"];

        if (oMarcaBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oMarcaBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                ViewState["ordenLista"] = SortDirection.Descending;
            }
            else
            {
                oMarcaBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                ViewState["ordenLista"] = SortDirection.Ascending;
            }
        }
        this.gvMarca.DataSource = oMarcaBEList;
        this.gvMarca.DataBind();
        ViewState["oMarcaBEList"] = oMarcaBEList;

        this.txhIdMarca.Value = String.Empty;
    }
    protected void gvMarca_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        oMarcaBEList = (MarcaBEList)ViewState["oMarcaBEList"];
        this.gvMarca.DataSource = oMarcaBEList;
        this.gvMarca.PageIndex = e.NewPageIndex;
        this.gvMarca.DataBind();

        this.txhIdMarca.Value = String.Empty;
    }
    #endregion

    #region "Metodos Botones"
    protected void btnAgregarMarca_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SGS_MarcaModelo_Mantenimiento.aspx", false);
    }
    protected void btnBuscarMarca_Click(object sender, ImageClickEventArgs e)
    {
        MarcaBL oMarcaBL = new MarcaBL();
        oMarcaBL.ErrorEvent += new MarcaBL.ErrorDelegate(Master.Transaction_ErrorEvent);

        String nomMarca = this.txtNomMarca.Text;
        String codEstado = this.cboEstadoMarca.SelectedValue;

        oMarcaBEList = oMarcaBL.GetAll(nomMarca, codEstado);

        if (oMarcaBEList == null || oMarcaBEList.Count == 0)
        {
            JavaScriptHelper.Alert(this, Message.keyNoRegistros, "");
            if (oMarcaBEList == null) oMarcaBEList = new MarcaBEList();
            oMarcaBEList.Add(new MarcaBE());
            GuardaParametros(0);
        }
        else GuardaParametros(1);

        this.gvMarca.DataSource = oMarcaBEList;
        this.gvMarca.DataBind();
        ViewState["oMarcaBEList"] = oMarcaBEList;

        this.txhIdMarca.Value = String.Empty;
    }
    protected void btnEliminarMarca_Click(object sender, ImageClickEventArgs e)
    {
        MarcaBL oMarcaBL = new MarcaBL();
        MarcaBE oMarcaBE = new MarcaBE();
        oMarcaBL.ErrorEvent += new MarcaBL.ErrorDelegate(Master.Transaction_ErrorEvent);
        Int32 retorno;

        try
        {
            Master.onError = false;

            oMarcaBE.nid_marca = Int32.Parse(this.txhIdMarca.Value.Trim());
            oMarcaBE.co_usuario_cambio = Profile.Usuario.CUSR_ID;
            oMarcaBE.no_estacion = Profile.Estacion;
            oMarcaBE.no_usuario_red = Profile.UsuarioRed;

            MarcaBE oMarcaBEMad = new MarcaBE();
            oMarcaBEMad = oMarcaBL.GetById(oMarcaBE.nid_marca);


            oMarcaBE.sfe_cambio = "";
            oMarcaBE.fl_inactivo = "1";
            retorno = oMarcaBL.Eliminar(oMarcaBE);

            if (!Master.onError && retorno > 0)
            {
                //Si todo es exito recien mostrar mensaje de eliminación con exito
                JavaScriptHelper.Alert(this, Message.keyElimino, "");
                btnBuscarMarca_Click(null, null);
                this.txhIdMarca.Value = String.Empty;
            }
            else
            {
                if (retorno == -5) JavaScriptHelper.Alert(this, Message.keyNoEliminoRelacionado, "");
                else JavaScriptHelper.Alert(this, Message.keyNoElimino, "");
                this.txhIdMarca.Value = String.Empty;
            }
        }
        catch (Exception ex)
        {
            Master.Web_ErrorEvent(this, ex);
            btnBuscarMarca_Click(null, null);
            JavaScriptHelper.Alert(this, Message.keyNoElimino, "");
        }
    }
    #endregion

    #region "Paginado"
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja Marca
        if (ViewState["oMarcaBEList"] != null &&
            this.gvMarca != null &&
            this.gvMarca.Rows.Count > 0 &&
            this.gvMarca.PageCount > 1)
        {
            GridViewRow oRow = this.gvMarca.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", ((MarcaBEList)ViewState["oMarcaBEList"]).Count);

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }
    #endregion

    #region "Parametros Bandeja"
    private void GuardaParametros(Int32 flatBusqueda)
    {
        String marca = this.txtNomMarca.Text.Trim();
        String estado = this.cboEstadoMarca.SelectedValue.Trim();

        String coPaginaActual = CONSTANTE_SEGURIDAD.Marca_Modelo;

        ParametrosPaginaBEList oParametrosPaginaBEListSave = Profile.Paginas;
        if (oParametrosPaginaBEListSave == null || oParametrosPaginaBEListSave.Count == 0)
        {
            ParametrosPaginaBE oParametrosPaginaBE = new ParametrosPaginaBE();
            oParametrosPaginaBE.co_pagina = coPaginaActual;
            oParametrosPaginaBE.nu_tab = 0;

            oParametrosPaginaBE.filtros.Add(marca);
            oParametrosPaginaBE.filtros.Add(estado);
            oParametrosPaginaBE.filtros.Add(Convert.ToString(flatBusqueda));

            if (oParametrosPaginaBEListSave == null)
                oParametrosPaginaBEListSave = new ParametrosPaginaBEList();

            oParametrosPaginaBEListSave.Add(oParametrosPaginaBE);
            Profile.Paginas = oParametrosPaginaBEListSave;
        }
        else
        {
            ParametrosPaginaBE oParametrosPaginaBEBusqueda = oParametrosPaginaBEListSave.Find(delegate (ParametrosPaginaBE p) { return p.co_pagina == coPaginaActual && p.nu_tab == 0; });

            if (oParametrosPaginaBEBusqueda != null)
            {
                oParametrosPaginaBEListSave.Remove(oParametrosPaginaBEBusqueda);
            }

            ParametrosPaginaBE oParametrosPaginaBE = new ParametrosPaginaBE();
            oParametrosPaginaBE.co_pagina = coPaginaActual;
            oParametrosPaginaBE.nu_tab = 0;

            oParametrosPaginaBE.filtros.Add(marca);
            oParametrosPaginaBE.filtros.Add(estado);
            oParametrosPaginaBE.filtros.Add(Convert.ToString(flatBusqueda));

            oParametrosPaginaBEListSave.Add(oParametrosPaginaBE);
            Profile.Paginas = oParametrosPaginaBEListSave;
        }
    }
    private void Regresar()
    {
        String coPaginaActual = CONSTANTE_SEGURIDAD.Marca_Modelo;
        ParametrosPaginaBEList oParametrosPaginaBEListSave = Profile.Paginas;

        //Verificamos si es llamado desde el menu o si es llamado desde una página(regresar)
        String indFirstCall = Request.QueryString["fc"];
        if (indFirstCall != null && indFirstCall.Equals("1"))
        {
            if (oParametrosPaginaBEListSave != null && oParametrosPaginaBEListSave.Count > 0)
            {
                ParametrosPaginaBE oParametrosPaginaBEBusqueda = oParametrosPaginaBEListSave.Find(delegate (ParametrosPaginaBE p) { return p.co_pagina == coPaginaActual && p.nu_tab == 0; });
                if (oParametrosPaginaBEBusqueda != null) oParametrosPaginaBEListSave.Remove(oParametrosPaginaBEBusqueda);
                Profile.Paginas = oParametrosPaginaBEListSave;
            }
        }
        else
        {
            if (oParametrosPaginaBEListSave != null || oParametrosPaginaBEListSave.Count > 0)
            {
                ParametrosPaginaBE oParametrosPaginaBEBusqueda = oParametrosPaginaBEListSave.Find(delegate (ParametrosPaginaBE p) { return p.co_pagina == coPaginaActual && p.nu_tab == 0; });

                if (oParametrosPaginaBEBusqueda != null)
                {
                    this.txtNomMarca.Text = oParametrosPaginaBEBusqueda.filtros[0];
                    this.cboEstadoMarca.SelectedValue = oParametrosPaginaBEBusqueda.filtros[1];
                    if (oParametrosPaginaBEBusqueda.filtros[2].Equals("1"))
                        btnBuscarMarca_Click(null, null);
                }
            }
        }
    }
    #endregion
}
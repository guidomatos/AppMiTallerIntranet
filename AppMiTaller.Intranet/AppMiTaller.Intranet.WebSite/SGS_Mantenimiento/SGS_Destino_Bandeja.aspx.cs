using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;


public partial class SGS_Mantenimiento_SGS_Destino_Bandeja : PaginaBase
{
    private DestinoBE oDestinoBE;
    DestinoBEList oDestinoBEList;
    private String TipoAccesoPagina;

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja de perfiles
        if (ViewState["oDestinoBEList"] != null &&
            this.grwDestino != null &&
            this.grwDestino.Rows.Count > 0 &&
            this.grwDestino.PageCount > 1)
        {
            GridViewRow oRow = this.grwDestino.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", ((DestinoBEList)ViewState["oDestinoBEList"]).Count);
                oRow.Cells[0].Controls.AddAt(0, oTotalReg);
                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            /*CONTROL DE ACCESO A PÁGINA*/
            TipoAccesoPagina = Master.ValidaTipoAccesoPagina(this, CONSTANTE_SEGURIDAD.Destinos);
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
            this.btnEliminar.Visible = false;
            btnNuevo.Visible = false;
        }
        /*FIN CONTROL DE ACCESO A PÁGINA*/
    }

    #region "Carga de página"
    private void InicializaPagina()
    {
        DestinoBL oDestinoBL = new DestinoBL();
        oDestinoBL.ErrorEvent += new DestinoBL.ErrorDelegate(Master.Transaction_ErrorEvent);

        DestinoBEList oDestinoBEList = new DestinoBEList();
        oDestinoBEList.Add(new DestinoBE());

        this.grwDestino.DataSource = oDestinoBEList;
        this.grwDestino.DataBind();

        this.grwDestino.PageSize = 5;
        this.ComboEstado1.cargarCombo(ConstanteBE.OBJECTO_TIPO_TODOS);
        ComboEstado1.SelectedValue = "0";

        cboTipoDestino.Condicion = ConstanteBE.OBJECTO_TIPO_TODOS;
        cboTipoDestino.CargarCombo();
        ViewState["oDestinoBEList"] = oDestinoBEList;
    }
    #endregion

    #region "Metodos Grilla grwDestino"
    protected void grwDestino_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = this.grwDestino.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["Id_ubicacion"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }

            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                            , txhIdDestinos.ClientID
                                             , dataKey.Values["Cod_estado"].ToString().Trim().Equals("0") ? dataKey.Values["Id_ubicacion"].ToString() : "-1");
            //, dataKey.Values["Id_ubicacion"].ToString());
            e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SGS_Destino_Mantenimiento.aspx?txh1={0}'", dataKey.Values["Id_ubicacion"].ToString());
        }
    }
    protected void grwDestino_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        oDestinoBEList = (DestinoBEList)ViewState["oDestinoBEList"];
        this.grwDestino.DataSource = oDestinoBEList;
        this.grwDestino.PageIndex = e.NewPageIndex;
        this.grwDestino.DataBind();
        txhIdDestinos.Value = String.Empty;
    }
    protected void grwDestino_Sorting(object sender, GridViewSortEventArgs e)
    {
        oDestinoBEList = (DestinoBEList)ViewState["oDestinoBEList"];
        SortDirection indOrden = (SortDirection)ViewState["ordenLista"];
        txhIdDestinos.Value = String.Empty;
        if (oDestinoBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oDestinoBEList.ordenar(e.SortExpression, direccionOrden.Descending);
                ViewState["ordenLista"] = SortDirection.Descending;
            }
            else
            {
                oDestinoBEList.ordenar(e.SortExpression, direccionOrden.Ascending);
                ViewState["ordenLista"] = SortDirection.Ascending;
            }
        }
        this.grwDestino.DataSource = oDestinoBEList;
        this.grwDestino.DataBind();
        ViewState["oDestinoBEList"] = oDestinoBEList;
    }
    #endregion

    #region "Metodos Botones"
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(
           String.Format("SGS_Destino_Mantenimiento.aspx?txh1={0}&thx2={1}", "", "")
           , false);
    }
    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        DestinoBL oDestinoBL = new DestinoBL();

        oDestinoBL.ErrorEvent += new DestinoBL.ErrorDelegate(Master.Transaction_ErrorEvent);

        Int32 indicador = 0;
        String resultado = String.Empty;
        if (!(txhIdDestinos.Value.Equals("")))
        {
            oDestinoBE = new DestinoBE();
            oDestinoBE.Id_ubicacion = Int32.Parse(txhIdDestinos.Value.ToString());
            oDestinoBE.Cod_usu_crea = Profile.Usuario.CUSR_ID;
            oDestinoBE.Nom_estacion = Profile.Estacion;
            oDestinoBE.Nom_usuario_red = Profile.UsuarioRed;

            indicador = oDestinoBL.Eliminar(oDestinoBE);

            if (indicador == -1)
            {
                JavaScriptHelper.Alert(this, Message.keyNoElimino, "");
            }
            else if (indicador == -5)
            {
                JavaScriptHelper.Alert(this, Message.keyNoEliminoRelacionado, "");
            }
            else if (indicador > 0)
            {
                JavaScriptHelper.Alert(this, Message.keyElimino, "");
            }

            btnBuscar_Click(null, null);
        }
        else
        {
            JavaScriptHelper.Alert(this, Message.keySeleccioneUno, "");
        }
        txhIdDestinos.Value = "";
    }
    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {

        DestinoBL oDestinoBL = new DestinoBL();
        oDestinoBL.ErrorEvent += new DestinoBL.ErrorDelegate(Master.Transaction_ErrorEvent);
        txhIdDestinos.Value = String.Empty;
        String codTipoUbicacion = cboTipoDestino.SelectedValue.ToString();
        String ruc = this.txtRuc.Text;
        String ubicacion = this.txtDescripcion.Text;
        String codEstado = this.ComboEstado1.SelectedValue.ToString();

        //METODO DE CARGA DE PARAMETROS
        DestinoBE oDestinoBE = new DestinoBE();
        oDestinoBE.Tipo_ubicacion = codTipoUbicacion;
        oDestinoBE.Nro_ruc = ruc;
        oDestinoBE.Nom_ubicacion = ubicacion;
        oDestinoBE.Cod_estado = codEstado;
        oDestinoBEList = oDestinoBL.Listar(oDestinoBE);

        if (oDestinoBEList == null || oDestinoBEList.Count == 0)
        {
            JavaScriptHelper.Alert(this, Message.keyNoRegistros, "");
            oDestinoBEList.Add(new DestinoBE());
            GuardaParametros("0");
        }
        else GuardaParametros("1");
        this.grwDestino.DataSource = oDestinoBEList;
        this.grwDestino.DataBind();
        ViewState["oDestinoBEList"] = oDestinoBEList;
    }
    protected void ComboTipo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region "PARAMETROS DE BANDEJA"
    private void GuardaParametros(String tot)
    {
        String codTipoUbicacion = cboTipoDestino.SelectedValue.ToString();
        String ruc = this.txtRuc.Text;
        String ubicacion = this.txtDescripcion.Text;
        String codEstado = this.ComboEstado1.SelectedValue.ToString();

        String coPaginaActual = CONSTANTE_SEGURIDAD.Destinos;
        ParametrosPaginaBEList oParametrosPaginaBEListSave = Profile.Paginas;
        if (oParametrosPaginaBEListSave == null || oParametrosPaginaBEListSave.Count == 0)
        {
            ParametrosPaginaBE oParametrosPaginaBE = new ParametrosPaginaBE();
            oParametrosPaginaBE.co_pagina = coPaginaActual;
            oParametrosPaginaBE.nu_tab = 0;

            oParametrosPaginaBE.filtros.Add(codTipoUbicacion);
            oParametrosPaginaBE.filtros.Add(ruc);
            oParametrosPaginaBE.filtros.Add(ubicacion);
            oParametrosPaginaBE.filtros.Add(codEstado);
            oParametrosPaginaBE.filtros.Add(tot);

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

            oParametrosPaginaBE.filtros.Add(codTipoUbicacion);
            oParametrosPaginaBE.filtros.Add(ruc);
            oParametrosPaginaBE.filtros.Add(ubicacion);
            oParametrosPaginaBE.filtros.Add(codEstado);
            oParametrosPaginaBE.filtros.Add(tot);

            oParametrosPaginaBEListSave.Add(oParametrosPaginaBE);
            Profile.Paginas = oParametrosPaginaBEListSave;

        }
    }

    private void Regresar()
    {
        String coPaginaActual = CONSTANTE_SEGURIDAD.Destinos;
        ParametrosPaginaBEList oParametrosPaginaBEListSave = Profile.Paginas;
        String tot = String.Empty;
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

                    this.cboTipoDestino.SelectedValue = oParametrosPaginaBEBusqueda.filtros[0];
                    this.txtRuc.Text = oParametrosPaginaBEBusqueda.filtros[1];
                    this.txtDescripcion.Text = oParametrosPaginaBEBusqueda.filtros[2];
                    this.ComboEstado1.SelectedValue = oParametrosPaginaBEBusqueda.filtros[3];
                    tot = oParametrosPaginaBEBusqueda.filtros[4];
                    if (tot == "1")
                    {
                        btnBuscar_Click(null, null);
                    }

                }
            }
        }
    }
    #endregion

}
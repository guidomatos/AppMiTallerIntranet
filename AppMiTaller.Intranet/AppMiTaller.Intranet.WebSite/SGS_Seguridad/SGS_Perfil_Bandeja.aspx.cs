using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SGS_Seguridad_SGS_Perfil_Bandeja : PaginaBase
{
    PerfilBEList oPerfilBEList;
    private String TipoAccesoPagina = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        TipoAccesoPagina = (Master as Seguridad).ValidaTipoAccesoPagina(this, CONSTANTE_SEGURIDAD.Perfiles);
        SetPaginacion();
        if (!Page.IsPostBack)
        {
            ViewState["ordenLista"] = SortDirection.Descending;
            InicializaPagina();
            Regresar();
        }

        if (!TipoAccesoPagina.Equals(CONSTANTE_SEGURIDAD.AccesoEdicion))
        {
            this.btnAgregar.Visible = false;
            this.btnEliminar.Visible = false;
        }
    }

    #region "Carga de pgina"

    private void SetPaginacion()
    {
        this.gvPerfiles.PageSize = this.Profile.PageSize;
    }

    private void InicializaPagina()
    {

        try
        {

            PerfilBL oPerfilBL = new PerfilBL();
            oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

            oPerfilBEList = new PerfilBEList();
            oPerfilBEList.Add(new PerfilBE());

            this.cboEstado.cargarCombo(ConstanteBE.OBJECTO_TIPO_TODOS);
            this.cboEstado.SelectedValue = "0";

            this.gvPerfiles.DataSource = oPerfilBEList;
            this.gvPerfiles.DataBind();

            this.gvPerfiles.PageSize = Profile.PageSize;

            ViewState["oPerfilBEList"] = oPerfilBEList;
        }
        catch (Exception ex)
        {
            (Master as Seguridad).Web_ErrorEvent(this, ex);
        }
    }
    #endregion

    #region "Metodos Grilla gvPerfiles"
    protected void gvPerfiles_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = this.gvPerfiles.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["NID_PERFIL"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }

            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}';document.getElementById('{2}').value = '{3}'"
                                            , txhPerfilID.ClientID, dataKey.Values["NID_PERFIL"].ToString(), txhFlObligatorio.ClientID, dataKey.Values["fl_obligatorio"].ToString());
            e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SGS_Perfil_Mantenimiento.aspx?perfilID={0}'", dataKey.Values["NID_PERFIL"].ToString());

        }
    }

    protected void gvPerfiles_Sorting(Object sender, GridViewSortEventArgs e)
    {
        oPerfilBEList = (PerfilBEList)ViewState["oPerfilBEList"];
        SortDirection indOrden = (SortDirection)ViewState["ordenLista"];

        if (oPerfilBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oPerfilBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                ViewState["ordenLista"] = SortDirection.Descending;
            }
            else
            {
                oPerfilBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                ViewState["ordenLista"] = SortDirection.Ascending;
            }
        }
        this.gvPerfiles.DataSource = oPerfilBEList;
        this.gvPerfiles.DataBind();
        ViewState["oPerfilBEList"] = oPerfilBEList;
    }

    protected void gvPerfiles_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        oPerfilBEList = (PerfilBEList)ViewState["oPerfilBEList"];
        this.gvPerfiles.DataSource = oPerfilBEList;
        this.gvPerfiles.PageIndex = e.NewPageIndex;
        this.gvPerfiles.DataBind();
    }
    #endregion

    #region "Metodos Botonos"
    protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SGS_Perfil_Mantenimiento.aspx", false);
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        PerfilBL oPerfilBL = new PerfilBL();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate((Master as Seguridad).Transaction_ErrorEvent);

        oPerfilBEList = oPerfilBL.GetPerfilesBandeja(Profile.Aplicacion, this.txtPerfil.Text.Trim(), this.cboEstado.SelectedValue, "");

        if (oPerfilBEList == null || oPerfilBEList.Count == 0)
        {
            oPerfilBEList = new PerfilBEList();
            JavaScriptHelper.Alert(this, Message.keyNoRegistros, "");
            oPerfilBEList.Add(new PerfilBE());
            GuardaParametros(0);
        }
        else
            GuardaParametros(1);
        this.gvPerfiles.DataSource = oPerfilBEList;
        this.gvPerfiles.DataBind();
        ViewState["oPerfilBEList"] = oPerfilBEList;
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        PerfilBL oPerfilBL = new PerfilBL();
        PerfilBE oPerfilBE = new PerfilBE();
        oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate(Master.Transaction_ErrorEvent);

        oPerfilBE.NID_PERFIL = Int32.Parse(this.txhPerfilID.Value.Trim());
        oPerfilBE.CCOAPL = Profile.Aplicacion;
        oPerfilBE.CO_USUARIO_CAMBIO = Profile.Usuario.CUSR_ID;
        oPerfilBE.NO_ESTACION_RED = Profile.Estacion;
        oPerfilBE.NO_USUARIO_RED = Profile.UsuarioRed;

        Int32 retorno = oPerfilBL.EliminarPerfil(oPerfilBE);

        if (retorno == 1)
        {
            JavaScriptHelper.Alert(this, Message.keyElimino, "");
            btnBuscar_Click(null, null);
            this.txhPerfilID.Value = String.Empty;
        }
        else if (retorno == 0)
        {
            JavaScriptHelper.Alert(this, Message.keyPefilEnUso, "");
            this.txhPerfilID.Value = String.Empty;
        }
        else
        {
            JavaScriptHelper.Alert(this, Message.keyNoElimino, "");
            this.txhPerfilID.Value = String.Empty;
        }
    }
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja de perfiles
        if (ViewState["oPerfilBEList"] != null &&
            this.gvPerfiles != null &&
            this.gvPerfiles.Rows.Count > 0 &&
            this.gvPerfiles.PageCount > 1)
        {
            GridViewRow oRow = this.gvPerfiles.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", ((PerfilBEList)ViewState["oPerfilBEList"]).Count);

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
        String perfil = this.txtPerfil.Text.Trim();
        String estado = this.cboEstado.SelectedValue.ToString();

        String idUsuario = Profile.Usuario.Nid_usuario.ToString();


        String coPaginaActual = CONSTANTE_SEGURIDAD.Perfiles;

        ParametrosPaginaBEList oParametrosPaginaBEListSave = Profile.Paginas;
        if (oParametrosPaginaBEListSave == null || oParametrosPaginaBEListSave.Count == 0)
        {
            ParametrosPaginaBE oParametrosPaginaBE = new ParametrosPaginaBE();
            oParametrosPaginaBE.co_pagina = coPaginaActual;
            oParametrosPaginaBE.nu_tab = 0; //indiceTabOn;

            oParametrosPaginaBE.filtros.Add(perfil);//0
            oParametrosPaginaBE.filtros.Add(estado);

            oParametrosPaginaBE.filtros.Add(idUsuario);
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
            oParametrosPaginaBE.nu_tab = 0;// indiceTabOn;

            oParametrosPaginaBE.filtros.Add(perfil);//0            
            oParametrosPaginaBE.filtros.Add(estado);

            oParametrosPaginaBE.filtros.Add(idUsuario);
            oParametrosPaginaBE.filtros.Add(Convert.ToString(flatBusqueda));

            oParametrosPaginaBEListSave.Add(oParametrosPaginaBE);
            Profile.Paginas = oParametrosPaginaBEListSave;
        }
    }

    private void Regresar()
    {
        String coPaginaActual = CONSTANTE_SEGURIDAD.Perfiles;
        ParametrosPaginaBEList oParametrosPaginaBEListSave = Profile.Paginas;

        //Verificamos si es llamado desde el menu o si es llamado desde una pgina(regresar)
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
                    this.txtPerfil.Text = oParametrosPaginaBEBusqueda.filtros[0];

                    this.cboEstado.SelectedValue = oParametrosPaginaBEBusqueda.filtros[1];

                    if (oParametrosPaginaBEBusqueda.filtros[3].Equals("1"))
                        btnBuscar_Click(null, null);

                    //ActualizaBotoneria(oParametrosPaginaBEBusqueda.nu_tab);
                }
            }
        }
    }
    #endregion
}
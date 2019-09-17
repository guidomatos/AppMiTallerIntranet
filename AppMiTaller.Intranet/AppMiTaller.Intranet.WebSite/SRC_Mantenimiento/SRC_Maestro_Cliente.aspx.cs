using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.IO;
using AppMiTaller.Intranet.BL.Exportacion;


public partial class SRC_Mantenimiento_SRC_Maestro_Cliente : System.Web.UI.Page
{

    #region "GLOBALES"
    ClienteBE objEnt = new ClienteBE();
    ClienteBL objNeg = new ClienteBL();
    ClienteBEList oMestroClienteBEList;
    #endregion

    #region "METODOS PROPIOS"
    private void CargarEstados()
    {
        cboEstado.Items.Insert(0, new ListItem());
        cboEstado.Items[0].Text = "--TODOS--";
        cboEstado.Items[0].Value = "";
        cboEstado.Items.Insert(1, new ListItem());
        cboEstado.Items[1].Text = "Activo";
        cboEstado.Items[1].Value = "0";
        cboEstado.Items.Insert(2, new ListItem());
        cboEstado.Items[2].Text = "Inactivo";
        cboEstado.Items[2].Value = "1";
    }
    private void CargarTipoDocumento()
    {
        ddl_bustipodocumento.DataSource = objNeg.GETListarTipoDocumento("0");
        ddl_bustipodocumento.DataTextField = "DES";
        ddl_bustipodocumento.DataValueField = "ID";
        ddl_bustipodocumento.DataBind();

        ddl_bustipodocumento.Items.Insert(0, new ListItem("--TODOS--", "0"));
        ddl_bustipodocumento.SelectedIndex = 0;
    }

    private void buscar(ClienteBE ent)
    {
        Session["ClienteBEList"] = objNeg.GETListarClientes(ent);

        gdClientes.DataSource = (ClienteBEList)(Session["ClienteBEList"]);
        gdClientes.DataBind();
    }

    private void Inicializa()
    {
        txt_busnombres.Attributes.Add("onkeypress", "return Valida_Nombre(event)");
        txt_busapepaterno.Attributes.Add("onkeypress", "return Valida_Paterno(event)");
        txt_busapematerno.Attributes.Add("onkeypress", "return Valida_Materno(event)");
        txt_busnrodoc.Attributes.Add("onkeypress", "return SoloNumeros(event)");

        CargarEstados();
        CargarTipoDocumento();
    }
    #endregion

    #region
    public bool OpcionNuevo()
    {
        return Master.ValidaAccesoOpcion(ConstanteBE.SRC_MantCliente_AccionNuevo).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionEditar()
    {
        return Master.ValidaAccesoOpcion(ConstanteBE.SRC_MantCliente_AccionEditar).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Acceso a Pagina
        string AccesoPagina = Master.ValidaAccesoOpcion(ConstanteBE.SRC_MantCliente_AccionVerForm);
        if (string.IsNullOrEmpty(AccesoPagina))
            AccesoPagina = Master.ValidaTipoAccesoPagina(Page, "SinAcceso");

        btnNuevo.Visible = OpcionNuevo();
        BtnEditar.Visible = OpcionEditar();

        if (!Page.IsPostBack)
        {
            Session["ordenGridTalleres"] = SortDirection.Descending;
            Inicializa();
            //-------------------------------------------------------------

            //INICIALIZANDO EL GRIDVIEW
            this.oMestroClienteBEList = new ClienteBEList();
            this.oMestroClienteBEList.Add(new ClienteBE());
            Session["ClienteBEList"] = oMestroClienteBEList;
            this.gdClientes.DataSource = this.oMestroClienteBEList;
            this.gdClientes.DataBind();

            if (Session["MestroClienteBE_FILTRO"] != null)
            {
                ClienteBE objFiltro = new ClienteBE();
                objFiltro = (ClienteBE)(Session["MestroClienteBE_FILTRO"]);
                txt_busnombres.Text = objFiltro.no_cliente.Trim();
                txt_busapepaterno.Text = objFiltro.no_ape_pat.Trim();
                txt_busapematerno.Text = objFiltro.no_ape_mat.Trim();
                ddl_bustipodocumento.SelectedValue = objFiltro.co_tipo_documento.ToString().Trim();
                txt_busnrodoc.Text = objFiltro.nu_documento.Trim();
                cboEstado.SelectedValue = objFiltro.fl_inactivo.ToString().Trim();
                btnBuscarWarrant_Click(null, null);
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja de perfiles
        ClienteBEList oMestroClienteBEList = (ClienteBEList)(Session["ClienteBEList"]);
        if (oMestroClienteBEList != null &&
            this.gdClientes != null &&
            this.gdClientes.Rows.Count > 0 &&
            this.gdClientes.PageCount > 1)
        {
            GridViewRow oRow = this.gdClientes.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", (oMestroClienteBEList.Count));

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }
    protected void gdClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdClientes.PageIndex = e.NewPageIndex;

        gdClientes.DataSource = (ClienteBEList)(Session["ClienteBEList"]);
        gdClientes.DataBind();
    }
    protected void btnBuscarWarrant_Click(object sender, ImageClickEventArgs e)
    {
        hf_exportaexcel.Value = "";
        objEnt.no_cliente = txt_busnombres.Text.Trim();
        objEnt.no_ape_pat = txt_busapepaterno.Text.Trim();
        objEnt.no_ape_mat = txt_busapematerno.Text.Trim();
        objEnt.co_tipo_documento = ddl_bustipodocumento.SelectedValue.ToString().Trim();
        objEnt.nu_documento = txt_busnrodoc.Text.Trim();
        objEnt.fl_inactivo = cboEstado.SelectedValue.ToString().Trim();

        Session["MestroClienteBE_FILTRO"] = objEnt;


        this.oMestroClienteBEList = objNeg.GETListarClientes(objEnt);

        if (oMestroClienteBEList == null || oMestroClienteBEList.Count == 0)
        {
            Session["MestroClienteBE_FILTRO"] = null;
            objEnt = null;
            objEnt = new ClienteBE();
            oMestroClienteBEList.Add(objEnt);
        }
        else
        {
            hf_exportaexcel.Value = "1";
        }


        this.gdClientes.DataSource = oMestroClienteBEList;
        this.gdClientes.DataBind();

        this.txh_nid_cliente.Value = string.Empty;
        Session["ClienteBEList"] = oMestroClienteBEList;

    }
    protected void BtnEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_nid_cliente.Value.ToString() != "")
        {
            Response.Redirect("SRC_Maestro_Detalle_Cliente.aspx?nid_cliente=" + txh_nid_cliente.Value.ToString().Trim());
        }
    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session["txh_nid_cliente"] = "";
        Session["edidet_objEnt"] = null;
        Session["NUEVO"] = "1";
        Response.Redirect("SRC_Maestro_Detalle_Cliente.aspx");
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClienteBL oMaestroClienteBL = new ClienteBL();
            oMaestroClienteBL.ErrorEvent += new ClienteBL.ErrorDelegate(Master.Transaction_ErrorEvent);

            objEnt.no_cliente = txt_busnombres.Text.Trim();
            objEnt.no_ape_pat = txt_busapepaterno.Text.Trim();
            objEnt.no_ape_mat = txt_busapematerno.Text.Trim();
            objEnt.co_tipo_documento = ddl_bustipodocumento.SelectedValue.ToString().Trim();
            objEnt.nu_documento = txt_busnrodoc.Text.Trim();
            objEnt.fl_inactivo = cboEstado.SelectedValue.ToString().Trim();

            this.oMestroClienteBEList = oMaestroClienteBL.GETListarClientes(objEnt);


            const string RUTA_DOCUMENTOS = ConstanteBE.RUTA_MANTENIMIENTO_SRC;
            String carpeta = String.Empty, nombre = String.Empty, RutaFinal = String.Empty;
            String ruta = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + RUTA_DOCUMENTOS;
            ruta = Utility.CrearCarpetaFileServer(ruta);

            String fl_Ruta = ConstanteBE.FLAT_EXCEL_SRC;
            ExportarExcelXml oExportarExcelXml = new ExportarExcelXml();
            String archivo = oExportarExcelXml.GenerarExcelExportarCliente(this.oMestroClienteBEList, ruta);

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

    protected void gdClientes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = gdClientes.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["nid_cliente"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }
            e.Row.Style["cursor"] = "pointer";
            //if (VerBoton())
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                            , txh_nid_cliente.ClientID, dataKey.Values["nid_cliente"].ToString());
            Session["edidet_objEnt"] = "2";
            Session["NUEVO"] = null;
            e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SRC_Maestro_Detalle_Cliente.aspx?nid_cliente={0}'", dataKey.Values["nid_cliente"].ToString());
        }
    }
    protected void gdClientes_Sorting(object sender, GridViewSortEventArgs e)
    {
        ClienteBEList oMaestroClienteBEList = (ClienteBEList)(Session["ClienteBEList"]);
        SortDirection indOrden = (SortDirection)(Session["ordenGridTalleres"]);

        txh_nid_cliente.Value = String.Empty;

        if (oMaestroClienteBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oMaestroClienteBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                Session["ordenGridTalleres"] = SortDirection.Descending;
            }
            else
            {
                oMaestroClienteBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                Session["ordenGridTalleres"] = SortDirection.Ascending;
            }
        }
        gdClientes.DataSource = oMaestroClienteBEList;
        gdClientes.DataBind();
        Session["ClienteBEList"] = oMaestroClienteBEList;
    }

}
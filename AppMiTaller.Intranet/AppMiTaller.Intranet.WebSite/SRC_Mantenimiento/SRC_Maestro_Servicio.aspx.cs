using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.IO;
using AppMiTaller.Intranet.BL.Exportacion;

public partial class SRC_Mantenimiento_SRC_Maestro_Servicio : System.Web.UI.Page
{
    #region " Variables "
    ServicioBL objServBL = new ServicioBL();
    ServicioBE objServBE = new ServicioBE();

    TipoServicioBL objTServBL = new TipoServicioBL();
    TipoServicioBE objTServBE = new TipoServicioBE();

    ServicioBEList oMaestroServicioBEList;

    #endregion

    #region
    public bool OpcionVer()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantServicio_AccionVer).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionNuevo()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantServicio_AccionNuevo).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionEditar()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantServicio_AccionEditar).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Acceso a Pagina
        string AccesoPagina = (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantServicio_AccionVerForm);
        if (string.IsNullOrEmpty(AccesoPagina))
            AccesoPagina = (Master as Principal).ValidaTipoAccesoPagina(Page, "SinAcceso");

        btnVerDet.Visible = OpcionVer();
        btnNuevo.Visible = OpcionNuevo();
        BtnEditar.Visible = OpcionEditar();

        if (!Page.IsPostBack)
        {
            Session["ordenGridTalleres"] = SortDirection.Descending;
            CargaInicial();
            if (Session["MaestroServicioBE_FILTRO"] != null)
            {
                ServicioBE objFiltro = new ServicioBE();
                objFiltro = (ServicioBE)(Session["MaestroServicioBE_FILTRO"]);
                txtCodigo.Text = objFiltro.Co_Servicio.Trim();
                txtNom.Text = objFiltro.No_Servicio.Trim();
                cboTServicio.SelectedValue = objFiltro.Id_TipoServicio.ToString();
                CboEstado.SelectedValue = objFiltro.Fl_activo;
                buscar();
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja de perfiles
        ServicioBEList oMaestroServicioBEList = (ServicioBEList)(Session["ServicioBEList"]);
        if (oMaestroServicioBEList != null &&
            this.gdServicios != null &&
            this.gdServicios.Rows.Count > 0 &&
            this.gdServicios.PageCount > 1)
        {
            GridViewRow oRow = this.gdServicios.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", (oMaestroServicioBEList.Count));
                oRow.Cells[0].Controls.AddAt(0, oTotalReg);
                System.Web.UI.WebControls.Table oTablaPaginacion = (System.Web.UI.WebControls.Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }

        }
    }


    void CargaInicial()
    {
        txtCodigo.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
        txtNom.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");

        CboEstado.Items.Insert(0, new ListItem());
        CboEstado.Items[0].Text = "--TODOS--";
        CboEstado.Items[0].Value = "";
        CboEstado.Items.Insert(1, new ListItem());
        CboEstado.Items[1].Text = "Activo";
        CboEstado.Items[1].Value = "A";
        CboEstado.Items.Insert(2, new ListItem());
        CboEstado.Items[2].Text = "Inactivo";
        CboEstado.Items[2].Value = "I";

        objTServBE.Co_tipo_servicio = "";
        objTServBE.No_tipo_servicio = "";
        objTServBE.Fl_activo = "A";
        cboTServicio.DataSource = objTServBL.BusqTServicioList(objTServBE);
        cboTServicio.DataTextField = "no_tipo_servicio";
        cboTServicio.DataValueField = "Id_TipoServicio";
        cboTServicio.DataBind();
        cboTServicio.Items.Insert(0, new ListItem());
        cboTServicio.Items[0].Text = "--TODOS--";
        cboTServicio.Items[0].Value = "0";

        //-------------------------------------------------------------

        //INICIALIZANDO EL GRIDVIEW
        this.oMaestroServicioBEList = new ServicioBEList();
        this.oMaestroServicioBEList.Add(new ServicioBE());
        Session["ServicioBEList"] = oMaestroServicioBEList;
        this.gdServicios.DataSource = this.oMaestroServicioBEList;
        this.gdServicios.DataBind();

    }
    void buscar()
    {
        hf_exportaexcel.Value = "";

        objServBE.Co_Servicio = txtCodigo.Text.Trim();
        objServBE.No_Servicio = txtNom.Text.Trim(); ;
        objServBE.Id_TipoServicio = Int32.Parse(cboTServicio.SelectedValue.ToString());
        objServBE.Fl_activo = CboEstado.SelectedValue.ToString();

        Session["MaestroServicioBE_FILTRO"] = objServBE;

        this.oMaestroServicioBEList = objServBL.BusqServicioList(objServBE);

        if (oMaestroServicioBEList == null || oMaestroServicioBEList.Count == 0)
        {
            Session["MaestroServicioBE_FILTRO"] = null;
            objServBE = null;
            objServBE = new ServicioBE();
            oMaestroServicioBEList.Add(objServBE);
        }
        else
        {
            hf_exportaexcel.Value = "1";
        }

        this.gdServicios.DataSource = oMaestroServicioBEList;
        this.gdServicios.DataBind();

        Session["ServicioBEList"] = oMaestroServicioBEList;
    }
    protected void gdServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdServicios.PageIndex = e.NewPageIndex;
        buscar();
    }
    protected void gdServicios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = gdServicios.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["Id_Servicio"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }
            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                            , txh_Id_Servicio.ClientID, dataKey.Values["Id_Servicio"].ToString());
            Session["op"] = "2";
            e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SRC_Maestro_Detalle_Servicio.aspx?Id_Servicio={0}'", dataKey.Values["Id_Servicio"].ToString());
        }
    }
    protected void btnBuscarWarrant_Click(object sender, ImageClickEventArgs e)
    {
        buscar();
    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session["txh_Id_Servicio"] = "";
        Session["op"] = "0";
        Response.Redirect("SRC_Maestro_Detalle_Servicio.aspx");
    }
    protected void BtnEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_Id_Servicio.Value.ToString() != "")
        {
            Session["txh_Id_Servicio"] = txh_Id_Servicio.Value.ToString();
            Session["op"] = "1";
            Response.Redirect("SRC_Maestro_Detalle_Servicio.aspx");
        }
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ServicioBL oMaestroServicioBL = new ServicioBL();

            objServBE.Co_Servicio = txtCodigo.Text.Trim();
            objServBE.No_Servicio = txtNom.Text.Trim(); ;
            objServBE.Id_TipoServicio = Int32.Parse(cboTServicio.SelectedValue.ToString());
            objServBE.Fl_activo = CboEstado.SelectedValue.ToString();

            this.oMaestroServicioBEList = oMaestroServicioBL.BusqServicioList(objServBE);


            const string RUTA_DOCUMENTOS = ConstanteBE.RUTA_MANTENIMIENTO_SRC;
            String carpeta = String.Empty, nombre = String.Empty, RutaFinal = String.Empty;
            String ruta = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + RUTA_DOCUMENTOS;
            ruta = Utility.CrearCarpetaFileServer(ruta);

            String fl_Ruta = ConstanteBE.FLAT_EXCEL_SRC;
            ExportarExcelXml oExportarExcelXml = new ExportarExcelXml();
            String archivo = oExportarExcelXml.GenerarExcelExportarServicio(this.oMaestroServicioBEList, ruta);

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

    protected void btnVerDet_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_Id_Servicio.Value.ToString() != "")
        {
            Session["txh_Id_Servicio"] = txh_Id_Servicio.Value.ToString();
            Session["op"] = "2";
            Response.Redirect("SRC_Maestro_Detalle_Servicio.aspx");
        }
    }
    protected void gdServicios_Sorting(object sender, GridViewSortEventArgs e)
    {
        ServicioBEList oMaestroServicioBEList = (ServicioBEList)(Session["ServicioBEList"]);
        SortDirection indOrden = (SortDirection)(Session["ordenGridTalleres"]);

        txh_Id_Servicio.Value = String.Empty;

        if (oMaestroServicioBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oMaestroServicioBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                Session["ordenGridTalleres"] = SortDirection.Descending;
            }
            else
            {
                oMaestroServicioBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                Session["ordenGridTalleres"] = SortDirection.Ascending;
            }
        }
        gdServicios.DataSource = oMaestroServicioBEList;
        gdServicios.DataBind();
        Session["ServicioBEList"] = oMaestroServicioBEList;
    }

}
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.IO;
using AppMiTaller.Intranet.BL.Exportacion;


public partial class SRC_Mantenimiento_SRC_Maestro_TServicio : System.Web.UI.Page
{
    #region " Variables "
    TipoServicioBL objTServBL = new TipoServicioBL();
    TipoServicioBE objTServBE = new TipoServicioBE();

    TipoServicioBEList oMaestroTipoServicioBEList;
    #endregion

    #region
    public bool OpcionVer()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantTipoServicio_AccionVer).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionNuevo()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantTipoServicio_AccionNuevo).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionEditar()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantTipoServicio_AccionEditar).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Acceso a Pagina
        string AccesoPagina = (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantTipoServicio_AccionVerForm);
        if (string.IsNullOrEmpty(AccesoPagina))
            AccesoPagina = (Master as Principal).ValidaTipoAccesoPagina(Page, "SinAcceso");

        btnVerDet.Visible = OpcionVer();
        btnNuevo.Visible = OpcionNuevo();
        BtnEditar.Visible = OpcionEditar();

        if (!Page.IsPostBack)
        {
            Session["ordenGridTalleres"] = SortDirection.Descending;
            CargaInicial();
            Session["fila"] = 0;

            if (Session["MaestroTipoServicioBE_FILTRO"] != null)
            {
                TipoServicioBE objFiltro = new TipoServicioBE();
                objFiltro = (TipoServicioBE)(Session["MaestroTipoServicioBE_FILTRO"]);
                txtCodigo.Text = objFiltro.Co_tipo_servicio.Trim();
                txtNom.Text = objFiltro.No_tipo_servicio.Trim();
                cboEstado.SelectedValue = objFiltro.Fl_activo.ToString().Trim();
                Buscar();
            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja de perfiles
        TipoServicioBEList oMaestroTipoServicioBEList = (TipoServicioBEList)(Session["TipoServicioBEList"]);
        if (oMaestroTipoServicioBEList != null &&
            this.gdTServicio != null &&
            this.gdTServicio.Rows.Count > 0 &&
            this.gdTServicio.PageCount > 1)
        {
            GridViewRow oRow = this.gdTServicio.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", (oMaestroTipoServicioBEList.Count));

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

        cboEstado.Items.Insert(0, new ListItem());
        cboEstado.Items[0].Text = "--TODOS--";
        cboEstado.Items[0].Value = "";
        cboEstado.Items.Insert(1, new ListItem());
        cboEstado.Items[1].Text = "Activo";
        cboEstado.Items[1].Value = "A";
        cboEstado.Items.Insert(2, new ListItem());
        cboEstado.Items[2].Text = "Inactivo";
        cboEstado.Items[2].Value = "I";

        //----------------------------------------------------

        //INICIALIZANDO EL GRIDVIEW
        this.oMaestroTipoServicioBEList = new TipoServicioBEList();
        this.oMaestroTipoServicioBEList.Add(new TipoServicioBE());
        Session["TipoServicioBEList"] = this.oMaestroTipoServicioBEList;
        this.gdTServicio.DataSource = this.oMaestroTipoServicioBEList;
        this.gdTServicio.DataBind();



    }
    void Buscar()
    {
        hf_exportaexcel.Value = "";


        objTServBE.Co_tipo_servicio = txtCodigo.Text.Trim();
        objTServBE.No_tipo_servicio = txtNom.Text.Trim();
        objTServBE.Fl_activo = cboEstado.SelectedValue.ToString();

        Session["MaestroTipoServicioBE_FILTRO"] = objTServBE;

        this.oMaestroTipoServicioBEList = objTServBL.BusqTServicioList(objTServBE);

        if (oMaestroTipoServicioBEList == null || oMaestroTipoServicioBEList.Count == 0)
        {
            Session["MaestroTipoServicioBE_FILTRO"] = null;
            objTServBE = null;
            objTServBE = new TipoServicioBE();
            oMaestroTipoServicioBEList.Add(objTServBE);
        }
        else
        {
            hf_exportaexcel.Value = "1";
        }

        this.gdTServicio.DataSource = oMaestroTipoServicioBEList;
        this.gdTServicio.DataBind();

        Session["TipoServicioBEList"] = oMaestroTipoServicioBEList;
    }

    protected void gdTServicio_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdTServicio.PageIndex = e.NewPageIndex;
        Buscar();
    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session["txh_id_TipoServicio"] = "";
        Session["op"] = "0";
        Response.Redirect("SRC_Maestro_Detalle_TServicio.aspx");
    }
    protected void gdTServicio_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = gdTServicio.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["id_TipoServicio"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }
            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                            , txh_id_TipoServicio.ClientID, dataKey.Values["id_TipoServicio"].ToString());
            Session["op"] = "2";
            e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SRC_Maestro_Detalle_TServicio.aspx?id_TipoServicio={0}'", dataKey.Values["id_TipoServicio"].ToString());
        }
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void gdTServicio_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnBuscarWarrant_Click(object sender, ImageClickEventArgs e)
    {
        Buscar();
    }
    protected void BtnEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_id_TipoServicio.Value.ToString() != "")
        {
            Session["txh_id_TipoServicio"] = txh_id_TipoServicio.Value.ToString();
            Session["op"] = "1";
            Response.Redirect("SRC_Maestro_Detalle_TServicio.aspx");
        }
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            TipoServicioBL oMaestroTipoServicioBL = new TipoServicioBL();
            oMaestroTipoServicioBL.ErrorEvent += new TipoServicioBL.ErrorDelegate(Master.Transaction_ErrorEvent);


            objTServBE.Co_tipo_servicio = txtCodigo.Text.Trim();
            objTServBE.No_tipo_servicio = txtNom.Text.Trim();
            objTServBE.Fl_activo = cboEstado.SelectedValue.ToString();

            this.oMaestroTipoServicioBEList = oMaestroTipoServicioBL.BusqTServicioList(objTServBE);


            const string RUTA_DOCUMENTOS = ConstanteBE.RUTA_MANTENIMIENTO_SRC;
            String carpeta = String.Empty, nombre = String.Empty, RutaFinal = String.Empty;
            String ruta = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + RUTA_DOCUMENTOS;
            ruta = Utility.CrearCarpetaFileServer(ruta);

            String fl_Ruta = ConstanteBE.FLAT_EXCEL_SRC;
            ExportarExcelXml oExportarExcelXml = new ExportarExcelXml();
            String archivo = oExportarExcelXml.GenerarExcelExportarTipoServicio(this.oMaestroTipoServicioBEList, ruta);

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
        if (txh_id_TipoServicio.Value.ToString() != "")
        {
            Session["txh_id_TipoServicio"] = txh_id_TipoServicio.Value.ToString();
            Session["op"] = "2";
            Response.Redirect("SRC_Maestro_Detalle_TServicio.aspx");
        }
    }
    protected void gdTServicio_Sorting(object sender, GridViewSortEventArgs e)
    {
        TipoServicioBEList oMaestroTServicioBEList = (TipoServicioBEList)(Session["TipoServicioBEList"]);
        SortDirection indOrden = (SortDirection)(Session["ordenGridTalleres"]);

        txh_id_TipoServicio.Value = String.Empty;

        if (oMaestroTServicioBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oMaestroTServicioBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                Session["ordenGridTalleres"] = SortDirection.Descending;
            }
            else
            {
                oMaestroTServicioBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                Session["ordenGridTalleres"] = SortDirection.Ascending;
            }
        }
        gdTServicio.DataSource = oMaestroTServicioBEList;
        gdTServicio.DataBind();
        Session["TipoServicioBEList"] = oMaestroTServicioBEList;
    }
}
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.IO;
using AppMiTaller.Intranet.BL.Exportacion;


public partial class SRC_Mantenimiento_SRC_Maestro_Modelos : System.Web.UI.Page
{
    #region "GLOBALES"
    ModeloBE objEnt = new ModeloBE();
    ModeloBL objNeg = new ModeloBL();
    ModeloBEList oMaestroModeloBEList;
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

    private void CargarModelos(ModeloBE ent)
    {
        Session["ModeloBEList"] = objNeg.GETListarModelos(ent);
        gdModelos.DataSource = Session["ModeloBEList"];
        gdModelos.DataBind();

        if (gdModelos.Rows.Count > 0)
            hf_exportaexcel.Value = "1";
    }
    private void CargarMarcas()
    {
        ddl_marca.DataSource = objNeg.GETListarMarcas(Profile.Usuario.Nid_usuario);
        ddl_marca.DataTextField = "DES";
        ddl_marca.DataValueField = "ID";
        ddl_marca.DataBind();
        ddl_marca.Items.Insert(0, new ListItem("--TODOS--", "0"));
        ddl_marca.SelectedIndex = 0;
    }
    private void CargarNegocios(string s_nid_marca)
    {
        ddl_negocio.DataSource = objNeg.GETListarNegocios_X_Marca(s_nid_marca);
        ddl_negocio.DataTextField = "DES";
        ddl_negocio.DataValueField = "ID";
        ddl_negocio.DataBind();
        ddl_negocio.Items.Insert(0, new ListItem("--TODOS--", "0"));
        ddl_negocio.SelectedIndex = 0;
    }
    private void CargarFamiliaByNegocio(ModeloBE ent)
    {
        ddl_familia.Items.Clear();
        ddl_familia.DataSource = objNeg.GETListarFamiliasByNegocio(ent);
        ddl_familia.DataTextField = "DES";
        ddl_familia.DataValueField = "ID";
        ddl_familia.DataBind();
        ddl_familia.Items.Insert(0, new ListItem("--TODOS--", "0"));
        ddl_familia.SelectedIndex = 0;
    }

    private void Inicializa()
    {
        CargarEstados();
        CargarMarcas();
        CargarNegocios(ddl_marca.SelectedValue.ToString().Trim());
        objEnt.co_negocio = ddl_negocio.SelectedValue.ToString().Trim();
        CargarFamiliaByNegocio(objEnt);

        //-------------------------------------------------------------

        //INICIALIZANDO EL GRIDVIEW
        this.oMaestroModeloBEList = new ModeloBEList();
        this.oMaestroModeloBEList.Add(new ModeloBE());
        Session["ModeloBEList"] = oMaestroModeloBEList;
        this.gdModelos.DataSource = this.oMaestroModeloBEList;
        this.gdModelos.DataBind();

    }
    #endregion

    #region
    public bool OpcionVer()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantModelo_AccionVer).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionEditar()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantModelo_AccionEditar).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Acceso a Pagina
        string AccesoPagina = (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantModelo_AccionVerForm);
        if (string.IsNullOrEmpty(AccesoPagina))
            AccesoPagina = (Master as Principal).ValidaTipoAccesoPagina(Page, "SinAcceso");

        btnVerDet.Visible = OpcionVer();
        BtnEditar.Visible = OpcionEditar();

        if (!Page.IsPostBack)
        {
            Session["ordenGridTalleres"] = SortDirection.Descending;
            Inicializa();
            if (Session["MaestroModeloBE_FILTRO"] != null)
            {
                objEnt = new ModeloBE();
                ModeloBE objFiltro = new ModeloBE();
                objFiltro = (ModeloBE)(Session["MaestroModeloBE_FILTRO"]);
                txt_codmodelo.Text = objFiltro.co_modelo.Trim();
                txt_nommodelo.Text = objFiltro.no_modelo.Trim();
                ddl_marca.SelectedValue = objFiltro.nid_marca.ToString().Trim();
                CargarNegocios(ddl_marca.SelectedValue.ToString().Trim());
                ddl_negocio.SelectedValue = objFiltro.co_negocio.ToString().Trim();
                objEnt.co_negocio = ddl_negocio.SelectedValue.ToString().Trim();
                CargarFamiliaByNegocio(objEnt);
                ddl_familia.SelectedValue = objFiltro.co_familia.ToString();
                cboEstado.SelectedValue = objFiltro.estado.ToString().Trim();
                btnBuscarWarrant_Click(null, null);
            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja de perfiles
        ModeloBEList oMaestroModeloBEList = (ModeloBEList)(Session["ModeloBEList"]);
        if (oMaestroModeloBEList != null &&
            this.gdModelos != null &&
            this.gdModelos.Rows.Count > 0 &&
            this.gdModelos.PageCount > 1)
        {
            GridViewRow oRow = this.gdModelos.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", (oMaestroModeloBEList.Count));

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }

    protected void btnBuscarWarrant_Click(object sender, ImageClickEventArgs e)
    {
        hf_exportaexcel.Value = "";

        objEnt.co_modelo = txt_codmodelo.Text.Trim();
        objEnt.no_modelo = txt_nommodelo.Text.Trim();
        objEnt.nid_marca = int.Parse(ddl_marca.SelectedValue.ToString().Trim());
        objEnt.co_negocio = ddl_negocio.SelectedValue.ToString().Trim();
        objEnt.co_familia = ddl_familia.SelectedValue.ToString();
        objEnt.estado = cboEstado.SelectedValue.ToString().Trim();

        Session["MaestroModeloBE_FILTRO"] = objEnt;
        Session["objEnt_bus"] = objEnt;

        //---------
        this.oMaestroModeloBEList = objNeg.GETListarModelos(objEnt);

        if (oMaestroModeloBEList == null || oMaestroModeloBEList.Count == 0)
        {
            Session["MaestroModeloBE_FILTRO"] = null;
            objEnt = null;
            objEnt = new ModeloBE();
            oMaestroModeloBEList.Add(objEnt);
        }
        else
        {
            hf_exportaexcel.Value = "1";
        }

        this.gdModelos.DataSource = oMaestroModeloBEList;
        this.gdModelos.DataBind();

        Session["ModeloBEList"] = oMaestroModeloBEList;


    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void BtnEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_nid_modelo.Value.ToString() != "")
        {
            Session["txh_nid_modelo"] = txh_nid_modelo.Value.ToString();
            Session["verdet_objEnt"] = null;
            Session["edidet_objEnt"] = "2";
            Response.Redirect("SRC_Maestro_Detalle_Modelos.aspx");
        }
    }
    protected void gdModelos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdModelos.PageIndex = e.NewPageIndex;
        CargarModelos((ModeloBE)(Session["objEnt_bus"]));
    }
    protected void ddl_negocio_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objEnt.co_negocio = ddl_negocio.SelectedValue.ToString().Trim();
            CargarFamiliaByNegocio(objEnt);
        }
        catch (Exception)
        {

        }
    }
    protected void btnVerDet_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_nid_modelo.Value.ToString() != "")
        {
            Session["txh_nid_modelo"] = txh_nid_modelo.Value.ToString();
            Session["verdet_objEnt"] = "1";
            Session["edidet_objEnt"] = null;
            Response.Redirect("SRC_Maestro_Detalle_Modelos.aspx");
        }
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ModeloBL oMaestroModeloBL = new ModeloBL();
            oMaestroModeloBL.ErrorEvent += new ModeloBL.ErrorDelegate(Master.Transaction_ErrorEvent);



            objEnt.co_modelo = txt_codmodelo.Text.Trim();
            objEnt.no_modelo = txt_nommodelo.Text.Trim();
            objEnt.nid_marca = int.Parse(ddl_marca.SelectedValue.ToString().Trim());
            objEnt.co_negocio = ddl_negocio.SelectedValue.ToString().Trim();
            objEnt.co_familia = ddl_familia.SelectedValue.ToString();
            objEnt.estado = cboEstado.SelectedValue.ToString().Trim();

            this.oMaestroModeloBEList = oMaestroModeloBL.GETListarModelos(objEnt);


            const string RUTA_DOCUMENTOS = ConstanteBE.RUTA_MANTENIMIENTO_SRC;
            String carpeta = String.Empty, nombre = String.Empty, RutaFinal = String.Empty;
            String ruta = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + RUTA_DOCUMENTOS;
            ruta = Utility.CrearCarpetaFileServer(ruta);

            String fl_Ruta = ConstanteBE.FLAT_EXCEL_SRC;
            ExportarExcelXml oExportarExcelXml = new ExportarExcelXml();
            String archivo = oExportarExcelXml.GenerarExcelExportarModelo(this.oMaestroModeloBEList, ruta);

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


    protected void ddl_marca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CargarNegocios(ddl_marca.SelectedValue.ToString().Trim());
        }
        catch (Exception)
        {

        }
    }
    protected void gdModelos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = gdModelos.DataKeys[e.Row.RowIndex];
            if (dataKey.Values["nid_modelo"] == null) aux = 1;
            //Int32.TryParse(dataKey.Values["nid_modelo"].ToString(), out aux);
            if (aux == 1)
            {
                e.Row.Visible = false;
                return;
            }
            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                            , txh_nid_modelo.ClientID, dataKey.Values["nid_modelo"].ToString());
            Session["verdet_objEnt"] = "1";
            Session["edidet_objEnt"] = null;
            e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SRC_Maestro_Detalle_Modelos.aspx?nid_modelo={0}'", dataKey.Values["nid_modelo"].ToString());
        }
    }
    protected void gdModelos_Sorting(object sender, GridViewSortEventArgs e)
    {
        ModeloBEList oMaestroModeloBEList = (ModeloBEList)(Session["ModeloBEList"]);
        SortDirection indOrden = (SortDirection)(Session["ordenGridTalleres"]);

        txh_nid_modelo.Value = String.Empty;

        if (oMaestroModeloBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oMaestroModeloBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                Session["ordenGridTalleres"] = SortDirection.Descending;
            }
            else
            {
                oMaestroModeloBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                Session["ordenGridTalleres"] = SortDirection.Ascending;
            }
        }
        gdModelos.DataSource = oMaestroModeloBEList;
        gdModelos.DataBind();
        Session["ModeloBEList"] = oMaestroModeloBEList;
    }
}
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.IO;
using AppMiTaller.Intranet.BL.Exportacion;

public partial class SRC_Mantenimiento_SRC_Maestro_Vehiculo : System.Web.UI.Page
{
    #region "GLOBALES"
    ModeloBL objNeg1 = new ModeloBL();
    VehiculoBE objEnt = new VehiculoBE();
    MaestroVehiculoBL objNeg = new MaestroVehiculoBL();

    VehiculoBEList oMaestroVehiculoBEList;

    Parametros oParm = new Parametros();

    #endregion

    #region "METODOS PROPIOS"
    private void CargarEstados()
    {
        cboEstado.Items.Insert(0, new ListItem());
        cboEstado.Items[0].Text = "--TODOS--";
        cboEstado.Items[0].Value = "";
        cboEstado.Items.Insert(1, new ListItem());
        cboEstado.Items[1].Text = "Activo";
        cboEstado.Items[1].Value = "A";
        cboEstado.Items.Insert(2, new ListItem());
        cboEstado.Items[2].Text = "Inactivo";
        cboEstado.Items[2].Value = "I";
    }
    private void CargarMarcas()
    {
        ddl_busmarca.DataSource = objNeg1.GETListarMarcas(Profile.Usuario.NID_USUARIO);
        ddl_busmarca.DataTextField = "DES";
        ddl_busmarca.DataValueField = "ID";
        ddl_busmarca.DataBind();
        ddl_busmarca.Items.Insert(0, new ListItem("--TODOS--", "0"));
        ddl_busmarca.SelectedIndex = 0;
    }

    private void CargarModelos(VehiculoBE objEnt)
    {
        ddl_busmodelo.DataSource = objNeg.GETListarModelosXMarca(objEnt, Profile.Usuario.NID_USUARIO);
        ddl_busmodelo.DataTextField = "DES";
        ddl_busmodelo.DataValueField = "ID";
        ddl_busmodelo.DataBind();
        ddl_busmodelo.Items.Insert(0, new ListItem("--TODOS--", "0"));
        ddl_busmodelo.SelectedIndex = 0;
    }

    private void Buscar(VehiculoBE objEnt)
    {
        Session["VehiculoBEList"] = objNeg.GETListarVehiculos(objEnt, Profile.Usuario.NID_USUARIO);
        gdVehiculos.DataSource = Session["VehiculoBEList"];
        gdVehiculos.DataBind();
    }

    private void Inicializa()
    {
        txt_buskilometraje.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        CargarEstados();
        CargarMarcas();
        objEnt.nid_marca = int.Parse(ddl_busmarca.SelectedValue.ToString().Trim());
        CargarModelos(objEnt);
        if (ddl_busmodelo.Items.Count == 0)
            ddl_busmodelo.Items.Insert(0, new ListItem("--No Tiene--", "--No Tiene--"));

    }
    #endregion

    #region
    public bool OpcionVer()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantVehiculo_AccionVer).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionNuevo()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantVehiculo_AccionNuevo).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionEditar()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantVehiculo_AccionEditar).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Acceso a Pagina
        string AccesoPagina = (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantVehiculo_AccionVerForm);
        if (string.IsNullOrEmpty(AccesoPagina))
            AccesoPagina = (Master as Principal).ValidaTipoAccesoPagina(Page, "SinAcceso");

        btnVerDet.Visible = OpcionVer();
        btnNuevo.Visible = OpcionNuevo();
        BtnEditar.Visible = OpcionEditar();


        lblTextoPlaca.Text = oParm.N_Placa.ToString();
        gdVehiculos.Columns[0].HeaderText = oParm.N_Placa.ToString();

        if (!Page.IsPostBack)
        {
            Session["ordenGridTalleres"] = SortDirection.Descending;
            Inicializa();


            //-------------------------------------------------------------

            //INICIALIZANDO EL GRIDVIEW
            this.oMaestroVehiculoBEList = new VehiculoBEList();
            this.oMaestroVehiculoBEList.Add(new VehiculoBE());
            Session["VehiculoBEList"] = oMaestroVehiculoBEList;
            this.gdVehiculos.DataSource = this.oMaestroVehiculoBEList;
            this.gdVehiculos.DataBind();

            //Solo mostrar en Chile [Anio-Tipo]
            this.gdVehiculos.Columns[5].Visible = oParm.SRC_CodPais.Equals("2");
            this.gdVehiculos.Columns[6].Visible = oParm.SRC_CodPais.Equals("2");


            if (Session["MaestroVehiculoBE_FILTRO"] != null)
            {
                objEnt = new VehiculoBE();
                VehiculoBE objFiltro = new VehiculoBE();
                objFiltro = (VehiculoBE)(Session["MaestroVehiculoBE_FILTRO"]);
                txt_busplacapatente.Text = objFiltro.nu_placa.Trim();
                txt_busnrovin.Text = objFiltro.nu_vin.Trim();
                ddl_busmarca.SelectedValue = objFiltro.nid_marca.ToString();
                objEnt.nid_marca = int.Parse(ddl_busmarca.SelectedValue.ToString().Trim());
                CargarModelos(objEnt);
                if (ddl_busmodelo.Items.Count == 0)
                {
                    ddl_busmodelo.Items.Insert(0, new ListItem("--No Tiene--", "--No Tiene--"));
                }
                ddl_busmodelo.SelectedValue = objFiltro.nid_modelo.ToString();
                if (objFiltro.qt_km_actual.ToString().Trim().Length == 0)
                {
                    txt_buskilometraje.Text = "";
                }
                else
                {
                    if (objFiltro.qt_km_actual.ToString().Trim() == "0")
                    {
                        txt_buskilometraje.Text = "";
                    }
                    else if (objFiltro.qt_km_actual.ToString().Trim() == "-1")
                    {
                        txt_buskilometraje.Text = "";
                    }
                    else
                    {
                        txt_buskilometraje.Text = objFiltro.qt_km_actual.ToString().Trim();
                    }
                }
                cboEstado.SelectedValue = objFiltro.fl_activo.ToString();
                btnBuscarWarrant_Click(null, null);
            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja de perfiles
        VehiculoBEList oMaestroVehiculoBEList = (VehiculoBEList)(Session["VehiculoBEList"]);
        if (oMaestroVehiculoBEList != null &&
            this.gdVehiculos != null &&
            this.gdVehiculos.Rows.Count > 0 &&
            this.gdVehiculos.PageCount > 1)
        {
            GridViewRow oRow = this.gdVehiculos.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", (oMaestroVehiculoBEList.Count));

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }

    protected void ddl_busmarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objEnt.nid_marca = int.Parse(ddl_busmarca.SelectedValue.ToString().Trim());
            CargarModelos(objEnt);
            if (ddl_busmodelo.Items.Count == 0)
            {
                ddl_busmodelo.Items.Insert(0, new ListItem("--No Tiene--", "--No Tiene--"));
            }
        }
        catch (Exception)
        {

        }
    }
    protected void btnBuscarWarrant_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            hf_exportaexcel.Value = "";
            txh_nid_vehiculo.Value = String.Empty;
            objEnt.nu_placa = txt_busplacapatente.Text.Trim();
            objEnt.nu_vin = txt_busnrovin.Text.Trim();
            objEnt.nid_marca = int.Parse(ddl_busmarca.SelectedValue.ToString());
            objEnt.nid_modelo = int.Parse(ddl_busmodelo.SelectedValue.ToString());
            objEnt.qt_km_actual = Int64.Parse((txt_buskilometraje.Text.Length == 0 ? "-1" : txt_buskilometraje.Text.Trim()));
            objEnt.fl_activo = cboEstado.SelectedValue.ToString();

            Session["MaestroVehiculoBE_FILTRO"] = objEnt;
            Session["bus_objEnt"] = objEnt;

            //---------
            this.oMaestroVehiculoBEList = objNeg.GETListarVehiculos(objEnt, Profile.Usuario.NID_USUARIO);

            if (oMaestroVehiculoBEList == null || oMaestroVehiculoBEList.Count == 0)
            {
                Session["MaestroVehiculoBE_FILTRO"] = null;
                objEnt = null;
                objEnt = new VehiculoBE();
                oMaestroVehiculoBEList.Add(objEnt);
            }
            else
            {
                hf_exportaexcel.Value = "1";
            }

            this.gdVehiculos.DataSource = oMaestroVehiculoBEList;
            this.gdVehiculos.DataBind();

            Session["VehiculoBEList"] = oMaestroVehiculoBEList;
        }
        catch (Exception)
        {

        }
    }
    protected void gdModelos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdVehiculos.PageIndex = e.NewPageIndex;
            objEnt = (VehiculoBE)(Session["bus_objEnt"]);
            Buscar(objEnt);
        }
        catch (Exception)
        {

        }
    }
    protected void btnVerDet_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_nid_vehiculo.Value.ToString() != "")
        {
            Session["txh_nid_vehiculo"] = txh_nid_vehiculo.Value.ToString();
            Session["verdet_objEnt"] = "2";
            Session["edidet_objEnt"] = null;
            Session["NUEVO"] = null;
            Response.Redirect("SRC_Maestro_Detalle_Vehiculo.aspx");
        }
    }
    protected void BtnEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (txh_nid_vehiculo.Value.ToString() != "")
        {
            Session["txh_nid_vehiculo"] = txh_nid_vehiculo.Value.ToString();
            Session["verdet_objEnt"] = null;
            Session["edidet_objEnt"] = "1";
            Session["NUEVO"] = null;
            Response.Redirect("SRC_Maestro_Detalle_Vehiculo.aspx");
        }
    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session["txh_nid_vehiculo"] = "";
        Session["verdet_objEnt"] = null;
        Session["edidet_objEnt"] = null;
        Session["NUEVO"] = "1";
        Response.Redirect("SRC_Maestro_Detalle_Vehiculo.aspx");
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            MaestroVehiculoBL oMaestroVehiculoBL = new MaestroVehiculoBL();
            oMaestroVehiculoBL.ErrorEvent += new MaestroVehiculoBL.ErrorDelegate(Master.Transaction_ErrorEvent);

            objEnt.nu_placa = txt_busplacapatente.Text.Trim();
            objEnt.nu_vin = txt_busnrovin.Text.Trim();
            objEnt.nid_marca = int.Parse(ddl_busmarca.SelectedValue.ToString());
            objEnt.nid_modelo = int.Parse(ddl_busmodelo.SelectedValue.ToString());
            objEnt.qt_km_actual = Int64.Parse((txt_buskilometraje.Text.Length == 0 ? "-1" : txt_buskilometraje.Text.Trim()));
            objEnt.fl_activo = cboEstado.SelectedValue.ToString();

            this.oMaestroVehiculoBEList = oMaestroVehiculoBL.GETListarVehiculos(objEnt, Profile.Usuario.NID_USUARIO);

            const string RUTA_DOCUMENTOS = ConstanteBE.RUTA_MANTENIMIENTO_SRC;
            String carpeta = String.Empty, nombre = String.Empty, RutaFinal = String.Empty;
            String ruta = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + RUTA_DOCUMENTOS;
            ruta = Utility.CrearCarpetaFileServer(ruta);

            String fl_Ruta = ConstanteBE.FLAT_EXCEL_SRC;
            ExportarExcelXml oExportarExcelXml = new ExportarExcelXml();
            String archivo = oExportarExcelXml.GenerarExcelExportarVehiculo(this.oMaestroVehiculoBEList, ruta);

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

    protected void gdVehiculos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = gdVehiculos.DataKeys[e.Row.RowIndex];
            Int32.TryParse(dataKey.Values["nid_vehiculo"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }

            e.Row.Cells[5].Visible = oParm.SRC_CodPais.Equals("2");
            e.Row.Cells[6].Visible = oParm.SRC_CodPais.Equals("2");
            if (oParm.SRC_CodPais.Equals("2"))
            {
                if (string.IsNullOrEmpty(dataKey.Values["co_tipo"].ToString()))
                {
                    e.Row.Cells[5].Text = "";
                    e.Row.Cells[6].Text = "";
                }
                else
                {
                    string strTpos = oParm.N_TiposVehiculo;
                    foreach (string strTipo in strTpos.Split('|'))
                    {
                        if (strTipo.Split('-').GetValue(0).ToString().Trim().Equals(dataKey.Values["co_tipo"].ToString()))
                        {
                            e.Row.Cells[6].Text = strTipo.Split('-').GetValue(1).ToString().Trim();
                            break;
                        }
                    }
                }
            }
            //---------------------

            e.Row.Style["cursor"] = "pointer";
            //if (VerBoton())
            e.Row.Attributes["onclick"] = String.Format("javascript: fc_SeleccionaFilaSimple(this); document.getElementById('{0}').value = '{1}'"
                                            , txh_nid_vehiculo.ClientID, dataKey.Values["nid_vehiculo"].ToString());
            Session["verdet_objEnt"] = "2";
            Session["edidet_objEnt"] = null;
            Session["NUEVO"] = null;
            e.Row.Attributes["ondblclick"] = String.Format("javascript: location.href='SRC_Maestro_Detalle_Vehiculo.aspx?nid_vehiculo={0}'", dataKey.Values["nid_vehiculo"].ToString());
        }
    }
    protected void gdVehiculos_Sorting(object sender, GridViewSortEventArgs e)
    {
        VehiculoBEList oMaestroVehiculoBEList = (VehiculoBEList)(Session["VehiculoBEList"]);
        SortDirection indOrden = (SortDirection)(Session["ordenGridTalleres"]);

        txh_nid_vehiculo.Value = String.Empty;

        if (oMaestroVehiculoBEList != null)
        {
            if (indOrden == SortDirection.Ascending)
            {
                oMaestroVehiculoBEList.Ordenar(e.SortExpression, direccionOrden.Descending);
                Session["ordenGridTalleres"] = SortDirection.Descending;
            }
            else
            {
                oMaestroVehiculoBEList.Ordenar(e.SortExpression, direccionOrden.Ascending);
                Session["ordenGridTalleres"] = SortDirection.Ascending;
            }
        }
        gdVehiculos.DataSource = oMaestroVehiculoBEList;
        gdVehiculos.DataBind();
        Session["VehiculoBEList"] = oMaestroVehiculoBEList;
    }

}
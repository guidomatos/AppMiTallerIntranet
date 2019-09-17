using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using AppMiTaller.Intranet.BL.Exportacion;
using System.IO;

public partial class SRC_Mantenimiento_Parametros : System.Web.UI.Page
{
    ParametrosBackOfficeBE objEnt;
    ParametrosBackOffieBL objNeg;

    #region "Metodo Propios"
    private void CargarParametros()
    {
        objNeg = new ParametrosBackOffieBL();
        Session["GETListarParametros"] = objNeg.GETListarParametros();
        gvListado.DataSource = (ParametrosBackOffieBEList)(Session["GETListarParametros"]);
        gvListado.DataBind();

        if (gvListado.Rows.Count > 0)
            hf_exportaexcel.Value = "1";

    }

    private void AgregarDias(string Nom_SE_DT, string Nom_QUITAR_DT, string id_dia)
    {
        DataTable DT_DIAS = (DataTable)(Session[Nom_SE_DT]);
        DataTable DT_DIAS2 = (DataTable)(Session[Nom_QUITAR_DT]);
        DataRow dr;
        if (id_dia.Trim().Equals("1"))
        {
            dr = DT_DIAS.NewRow();
            dr["id"] = "1";
            dr["des"] = "Lunes";
            DT_DIAS.Rows.Add(dr);
            foreach (DataRow dr2 in DT_DIAS2.Rows)
            {
                if (dr2["id"].ToString().Equals(id_dia.Trim()))
                {
                    DT_DIAS2.Rows.Remove(dr2);
                    break;
                }
            }
        }

        if (id_dia.Trim().Equals("2"))
        {
            dr = DT_DIAS.NewRow();
            dr["id"] = "2";
            dr["des"] = "Martes";
            DT_DIAS.Rows.Add(dr);
            foreach (DataRow dr2 in DT_DIAS2.Rows)
            {
                if (dr2["id"].ToString().Equals(id_dia.Trim()))
                {
                    DT_DIAS2.Rows.Remove(dr2);
                    break;
                }
            }
        }

        if (id_dia.Trim().Equals("3"))
        {
            dr = DT_DIAS.NewRow();
            dr["id"] = "3";
            dr["des"] = "Miercoles";
            DT_DIAS.Rows.Add(dr);
            foreach (DataRow dr2 in DT_DIAS2.Rows)
            {
                if (dr2["id"].ToString().Equals(id_dia.Trim()))
                {
                    DT_DIAS2.Rows.Remove(dr2);
                    break;
                }
            }
        }

        if (id_dia.Trim().Equals("4"))
        {
            dr = DT_DIAS.NewRow();
            dr["id"] = "4";
            dr["des"] = "Jueves";
            DT_DIAS.Rows.Add(dr);
            foreach (DataRow dr2 in DT_DIAS2.Rows)
            {
                if (dr2["id"].ToString().Equals(id_dia.Trim()))
                {
                    DT_DIAS2.Rows.Remove(dr2);
                    break;
                }
            }
        }

        if (id_dia.Trim().Equals("5"))
        {
            dr = DT_DIAS.NewRow();
            dr["id"] = "5";
            dr["des"] = "Viernes";
            DT_DIAS.Rows.Add(dr);
            foreach (DataRow dr2 in DT_DIAS2.Rows)
            {
                if (dr2["id"].ToString().Equals(id_dia.Trim()))
                {
                    DT_DIAS2.Rows.Remove(dr2);
                    break;
                }
            }
        }

        if (id_dia.Trim().Equals("6"))
        {
            dr = DT_DIAS.NewRow();
            dr["id"] = "6";
            dr["des"] = "Sabado";
            DT_DIAS.Rows.Add(dr);
            foreach (DataRow dr2 in DT_DIAS2.Rows)
            {
                if (dr2["id"].ToString().Equals(id_dia.Trim()))
                {
                    DT_DIAS2.Rows.Remove(dr2);
                    break;
                }
            }
        }

        if (id_dia.Trim().Equals("7"))
        {
            dr = DT_DIAS.NewRow();
            dr["id"] = "7";
            dr["des"] = "Domingo";
            DT_DIAS.Rows.Add(dr);
            foreach (DataRow dr2 in DT_DIAS2.Rows)
            {
                if (dr2["id"].ToString().Equals(id_dia.Trim()))
                {
                    DT_DIAS2.Rows.Remove(dr2);
                    break;
                }
            }
        }

        Session[Nom_SE_DT] = DT_DIAS;
    }

    private void TablaDias_off()
    {
        DataTable DT_Dias_off = new DataTable();
        DT_Dias_off.Columns.Add("id");
        DT_Dias_off.Columns.Add("des");

        Session["DT_Dias_off"] = DT_Dias_off;
    }

    private void TablaDias_on()
    {
        DataTable DT_Dias_on = new DataTable();
        DT_Dias_on.Columns.Add("id");
        DT_Dias_on.Columns.Add("des");

        Session["DT_Dias_on"] = DT_Dias_on;
    }

    private DataTable TablaHoras()
    {
        DataTable DT_HORAS = new DataTable();

        DT_HORAS.Columns.Add("id");
        DT_HORAS.Columns.Add("des");

        DataRow dr;
        for (int i = 1; i < 25; i++)
        {
            dr = DT_HORAS.NewRow();
            if (i < 13)
            {
                if (i.ToString().Length > 1)
                {
                    dr["id"] = i.ToString() + ":00";
                    dr["des"] = i.ToString() + ":00am";
                }
                else
                {
                    dr["id"] = "0" + i.ToString() + ":00";
                    dr["des"] = "0" + i.ToString() + ":00am";
                }
            }
            else
            {
                dr["id"] = i.ToString() + ":00";
                dr["des"] = i.ToString() + ":00pm";
            }
            DT_HORAS.Rows.Add(dr);
        }

        return DT_HORAS;
    }

    private void llenar_ddl_horas(DropDownList ddl)
    {
        DataTable DT_HORAS = TablaHoras();

        ddl.DataSource = DT_HORAS;
        ddl.DataTextField = "des";
        ddl.DataValueField = "id";
        ddl.DataBind();
    }

    private void cargar_listas(DataTable dt, ListBox lst)
    {
        lst.DataSource = dt;
        lst.DataTextField = "des";
        lst.DataValueField = "id";
        lst.DataBind();
    }

    private void Llenar_listas(string Valor)
    {
        DataTable DT_Dias_off = (DataTable)(Session["DT_Dias_off"]);
        DT_Dias_off.Rows.Clear();
        Session["DT_Dias_off"] = DT_Dias_off;
        DataTable DT_Dias_on = (DataTable)(Session["DT_Dias_on"]);
        DT_Dias_on.Rows.Clear();
        Session["DT_Dias_on"] = DT_Dias_on;
        if (Valor.Length == 0)
        {
            for (int i = 1; i < 8; i++)
            {
                AgregarDias("DT_Dias_off", "DT_Dias_on", i.ToString());
            }
            cargar_listas((DataTable)(Session["DT_Dias_off"]), lst_dias_off);
        }
        else
        {
            string[] arr1 = new string[3];
            arr1 = Valor.Split('|');
            if (arr1.Length == 3)
            {
                string dias = arr1[0].ToString().Trim();
                if (dias.Split(',').Length > 0)
                {
                    string[] arr_dias = new string[dias.Split(',').Length];
                    arr_dias = dias.Split(',');

                    int ind_exis = 0;
                    if (arr_dias.Length > 0)
                    {
                        for (int j = 1; j < 8; j++)
                        {
                            ind_exis = 0;
                            for (int i = 0; i < arr_dias.Length; i++)
                            {
                                if (j.ToString().Equals(arr_dias.GetValue(i).ToString().Trim()))
                                {
                                    ind_exis = 1;
                                    break;
                                }
                            }
                            if (ind_exis == 1)
                            {
                                AgregarDias("DT_Dias_on", "DT_Dias_off", j.ToString());
                            }
                            else
                            {
                                AgregarDias("DT_Dias_off", "DT_Dias_on", j.ToString());
                            }
                        }
                        cargar_listas((DataTable)(Session["DT_Dias_off"]), lst_dias_off);
                        cargar_listas((DataTable)(Session["DT_Dias_on"]), lst_dias_on);
                    }
                }
                ddl_hora_de.SelectedValue = arr1.GetValue(1).ToString().Trim();
                ddl_hora_a.SelectedValue = arr1.GetValue(2).ToString().Trim();
            }
        }
    }

    private void Llenar_TipoConsolHora(DropDownList ddl)
    {
        string v_Cod_Pais = ConfigurationManager.AppSettings["CodPais"].ToString().Trim();
        if (v_Cod_Pais.Equals("1"))
        {
            ddl.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["nDepartamento_1"].ToString().Trim(), "1"));
            ddl.Items.Insert(1, new ListItem(ConfigurationManager.AppSettings["nProvincia_1"].ToString().Trim(), "2"));
            ddl.Items.Insert(2, new ListItem(ConfigurationManager.AppSettings["nDistrito_1"].ToString().Trim(), "3"));
        }
        else
        {
            if (v_Cod_Pais.Equals("2"))
            {
                ddl.Items.Insert(0, new ListItem(ConfigurationManager.AppSettings["nDepartamento_2"].ToString().Trim(), "1"));
                ddl.Items.Insert(1, new ListItem(ConfigurationManager.AppSettings["nProvincia_2"].ToString().Trim(), "2"));
                ddl.Items.Insert(2, new ListItem(ConfigurationManager.AppSettings["nDistrito_2"].ToString().Trim(), "3"));
            }
        }
    }
    #endregion

    #region
    public bool OpcionEditar()
    {
        return Master.ValidaAccesoOpcion(ConstanteBE.SRC_MantParametros_AccionEditar).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    #endregion

    protected void InicializaPagina()
    {
        try
        {
            TablaDias_off();
            TablaDias_on();
            CargarParametros();
            llenar_ddl_horas(ddl_hora_de);
            llenar_ddl_horas(ddl_hora_a);
            Llenar_listas("");
            ddl_hora_a.SelectedValue = "13:00";

            this.btnNuevo.Style.Add("display", "none");
            this.btnBuscar.Style.Add("display", "none");
        }
        catch (Exception)
        {
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InicializaPagina();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja de perfiles
        ParametrosBackOffieBEList oParametrosBackOffieBEList = (ParametrosBackOffieBEList)(Session["GETListarParametros"]);
        if (oParametrosBackOffieBEList != null &&
            this.gvListado != null &&
            this.gvListado.Rows.Count > 0 &&
            this.gvListado.PageCount >= 0)
        {
            GridViewRow oRow = this.gvListado.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", (oParametrosBackOffieBEList.Count));

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
            else
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", (oParametrosBackOffieBEList.Count));

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }
    protected void gvListado_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (OpcionEditar())
        {
            string horario_pordefecto = "";
            string tipo = "";
            string val = "";
            //Set the edit index.
            gvListado.EditIndex = e.NewEditIndex;
            horario_pordefecto = gvListado.DataKeys[gvListado.EditIndex].Values[2].ToString();
            if (!horario_pordefecto.Trim().Equals("1"))
            {
                //Bind data to the GridView control.
                CargarParametros();
                tipo = gvListado.DataKeys[gvListado.EditIndex].Values[0].ToString();
                val = gvListado.DataKeys[gvListado.EditIndex].Values[1].ToString();
                if (tipo.Trim().Equals("STRING") || tipo.Trim().Equals("INTEGER"))
                {
                    GridViewRow dr = gvListado.Rows[gvListado.EditIndex];
                    dr.FindControl("txt_valorparam").Visible = true;
                    dr.FindControl("chk_valorparam").Visible = false;
                    dr.FindControl("ddl_conshoraspor").Visible = false;
                }
                else
                {
                    if (tipo.Trim().Equals("BOOLEAN"))
                    {
                        GridViewRow dr = gvListado.Rows[gvListado.EditIndex];
                        dr.FindControl("txt_valorparam").Visible = false;
                        dr.FindControl("ddl_conshoraspor").Visible = false;
                        dr.FindControl("chk_valorparam").Visible = true;
                        if (val.Trim().Equals("0"))
                        {
                            ((CheckBox)(dr.FindControl("chk_valorparam"))).Checked = false;
                        }
                        else
                        {
                            ((CheckBox)(dr.FindControl("chk_valorparam"))).Checked = true;
                        }
                    }
                    else
                    {
                        if (tipo.Trim().Equals("STRING_DISPLAY"))
                        {
                            GridViewRow dr = gvListado.Rows[gvListado.EditIndex];
                            dr.FindControl("txt_valorparam").Visible = false;
                            dr.FindControl("chk_valorparam").Visible = false;
                            dr.FindControl("ddl_conshoraspor").Visible = true;

                            Llenar_TipoConsolHora((DropDownList)(dr.FindControl("ddl_conshoraspor")));
                            ((DropDownList)(dr.FindControl("ddl_conshoraspor"))).SelectedValue = val.Trim();
                        }
                    }
                }
            }
            else
            {
                val = gvListado.DataKeys[gvListado.EditIndex].Values[1].ToString();
                Session["val_defecto"] = val;
                Session["nid_parametro"] = gvListado.DataKeys[gvListado.EditIndex].Values[2].ToString();
                Session["no_tipo_valor"] = gvListado.DataKeys[gvListado.EditIndex].Values[0].ToString();
                gvListado.EditIndex = -1;
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>changeTab(1)</script>", false);
                Response.Redirect("SRC_QNET_Detalle_CitasParametros.aspx");
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOnForm('0');</script>", false);
    }
    protected void gvListado_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        objEnt = new ParametrosBackOfficeBE();
        objNeg = new ParametrosBackOffieBL();
        int rpta = 0;
        string tipo = "";
        string val = "";
        tipo = gvListado.DataKeys[gvListado.EditIndex].Values[0].ToString();
        if (tipo.Trim().Equals("STRING") || tipo.Trim().Equals("INTEGER"))
        {
            TextBox txtval = gvListado.Rows[e.RowIndex].FindControl("txt_valorparam") as TextBox;
            val = txtval.Text;
        }
        else
        {
            if (tipo.Trim().Equals("BOOLEAN"))
            {
                GridViewRow dr = gvListado.Rows[gvListado.EditIndex];
                if (((CheckBox)(dr.FindControl("chk_valorparam"))).Checked == false)
                {
                    val = "0";
                }
                else
                {
                    val = "1";
                }
            }
            else
            {
                if (tipo.Trim().Equals("STRING_DISPLAY"))
                {
                    GridViewRow dr = gvListado.Rows[gvListado.EditIndex];
                    val = ((DropDownList)(dr.FindControl("ddl_conshoraspor"))).SelectedValue;
                }
            }
        }

        objEnt.no_tipo_valor = gvListado.DataKeys[gvListado.EditIndex].Values[0].ToString();
        objEnt.nid_parametro = int.Parse(gvListado.DataKeys[gvListado.EditIndex].Values[2].ToString());
        objEnt.valor = val;
        objEnt.co_usuario = Profile.UserName;
        objEnt.co_usuario_red = Profile.UsuarioRed;
        objEnt.no_estacion_red = Profile.Estacion;
        rpta = objNeg.GETActualizarParametro(objEnt);
        gvListado.EditIndex = -1;
        CargarParametros();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOnForm('0');</script>", false);
    }
    protected void gvListado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvListado.EditIndex = -1;
        CargarParametros();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOnForm('0');</script>", false);
    }
    protected void tabContMantParametros_ActiveTabChanged(object sender, EventArgs e)
    {
        if (hid_tab1.Value.ToString().Trim().Equals("1"))
        {
            try
            {
                Llenar_listas(Session["val_defecto"].ToString());
            }
            catch (Exception)
            {

            }
        }
    }
    protected void btn_on_Click(object sender, EventArgs e)
    {
        for (int index = 0; index < lst_dias_off.Items.Count; index++)
        {
            if (lst_dias_off.Items[index].Selected == true)
            {
                AgregarDias("DT_Dias_on", "DT_Dias_off", lst_dias_off.Items[index].Value.ToString().Trim());
            }
        }
        cargar_listas((DataTable)(Session["DT_Dias_off"]), lst_dias_off);
        cargar_listas((DataTable)(Session["DT_Dias_on"]), lst_dias_on);
    }
    protected void btn_off_Click(object sender, EventArgs e)
    {
        for (int index = 0; index < lst_dias_on.Items.Count; index++)
        {
            if (lst_dias_on.Items[index].Selected == true)
            {
                AgregarDias("DT_Dias_off", "DT_Dias_on", lst_dias_on.Items[index].Value.ToString().Trim());
            }
        }
        cargar_listas((DataTable)(Session["DT_Dias_off"]), lst_dias_off);
        cargar_listas((DataTable)(Session["DT_Dias_on"]), lst_dias_on);
    }
    protected void tabContMantParametros_Unload(object sender, EventArgs e)
    {
        tabContMantParametros_ActiveTabChanged(null, null);
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ParametrosBackOffieBL oParametrosBackOffieBL = new ParametrosBackOffieBL();
            oParametrosBackOffieBL.ErrorEvent += new ParametrosBackOffieBL.ErrorDelegate(Master.Transaction_ErrorEvent);
            ParametrosBackOffieBEList oParametrosBackOffieBEList = new ParametrosBackOffieBEList();

            oParametrosBackOffieBEList = oParametrosBackOffieBL.GETListarParametros();

            const string RUTA_DOCUMENTOS = ConstanteBE.RUTA_MANTENIMIENTO_SRC;
            String carpeta = String.Empty, nombre = String.Empty, RutaFinal = String.Empty;
            String ruta = Convert.ToString(ConfigurationManager.AppSettings["FileServerPath"]) + RUTA_DOCUMENTOS;
            ruta = Utility.CrearCarpetaFileServer(ruta);

            String fl_Ruta = ConstanteBE.FLAT_EXCEL_SRC;
            ExportarExcelXml oExportarExcelXml = new ExportarExcelXml();
            String archivo = oExportarExcelXml.GenerarExcelExportableParametroSistema(oParametrosBackOffieBEList, ruta);

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

    protected void gvListado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvListado.PageIndex = e.NewPageIndex;
        CargarParametros();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string GetDynamicContent(string contextKey)
    {
        return default(string);
    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void gvLista_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvLista_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gvListaConfig_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvListaConfig_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvListaConfig_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvListaConfig_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
}
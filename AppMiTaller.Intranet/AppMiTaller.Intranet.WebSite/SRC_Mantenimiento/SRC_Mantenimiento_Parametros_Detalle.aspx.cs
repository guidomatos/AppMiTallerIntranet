using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SRC_Mantenimiento_SRC_Mantenimiento_Parametros_Detalle : System.Web.UI.Page
{
    ParametrosBackOfficeBE objEnt;
    ParametrosBackOffieBL objNeg;

    #region "Metodo Propios"
    private void AgregarDias(string Nom_SE_DT, string Nom_QUITAR_DT, string id_dia)
    {
        DataTable DT_DIAS = (DataTable)(Session[Nom_SE_DT]);
        DataTable DT_DIAS2 = (DataTable)(Session[Nom_QUITAR_DT]);
        DataRow dr;
        int existe = 0;
        if (id_dia.Trim().Equals("1"))
        {
            foreach (DataRow fila in DT_DIAS.Rows)
            {
                if (fila["id"].ToString().Trim().Equals(id_dia.Trim()))
                {
                    existe = 1;
                    break;
                }
            }
            if (existe == 0)
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
        }

        existe = 0;
        if (id_dia.Trim().Equals("2"))
        {
            foreach (DataRow fila in DT_DIAS.Rows)
            {
                if (fila["id"].ToString().Trim().Equals(id_dia.Trim()))
                {
                    existe = 1;
                    break;
                }
            }
            if (existe == 0)
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
        }

        existe = 0;
        if (id_dia.Trim().Equals("3"))
        {
            foreach (DataRow fila in DT_DIAS.Rows)
            {
                if (fila["id"].ToString().Trim().Equals(id_dia.Trim()))
                {
                    existe = 1;
                    break;
                }
            }
            if (existe == 0)
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
        }

        existe = 0;
        if (id_dia.Trim().Equals("4"))
        {
            foreach (DataRow fila in DT_DIAS.Rows)
            {
                if (fila["id"].ToString().Trim().Equals(id_dia.Trim()))
                {
                    existe = 1;
                    break;
                }
            }
            if (existe == 0)
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
        }

        existe = 0;
        if (id_dia.Trim().Equals("5"))
        {
            foreach (DataRow fila in DT_DIAS.Rows)
            {
                if (fila["id"].ToString().Trim().Equals(id_dia.Trim()))
                {
                    existe = 1;
                    break;
                }
            }
            if (existe == 0)
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
        }

        existe = 0;
        if (id_dia.Trim().Equals("6"))
        {
            foreach (DataRow fila in DT_DIAS.Rows)
            {
                if (fila["id"].ToString().Trim().Equals(id_dia.Trim()))
                {
                    existe = 1;
                    break;
                }
            }
            if (existe == 0)
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
        }

        existe = 0;
        if (id_dia.Trim().Equals("7"))
        {
            foreach (DataRow fila in DT_DIAS.Rows)
            {
                if (fila["id"].ToString().Trim().Equals(id_dia.Trim()))
                {
                    existe = 1;
                    break;
                }
            }
            if (existe == 0)
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
        ParametrosBackOfficeBE objEntHorDef = new ParametrosBackOfficeBE();
        objNeg = new ParametrosBackOffieBL();
        int minutos = 0;
        string min_real = "";
        int h1 = 0;
        objEntHorDef = objNeg.GetHorarioXDefecto();
        h1 = int.Parse(objEntHorDef.HoraInicio.Split(':')[0].ToString().Trim());

        DT_HORAS.Columns.Add("id");
        DT_HORAS.Columns.Add("des");

        DataRow dr;
        for (int i = 0; i < 24; i++)
        {
            if (i >= int.Parse(objEntHorDef.HoraInicio.Split(':')[0].ToString().Trim()) && i <= int.Parse(objEntHorDef.HoraFinal.Split(':')[0].ToString().Trim()))
            {
                dr = DT_HORAS.NewRow();
                if (i > int.Parse(objEntHorDef.HoraInicio.Split(':')[0].ToString().Trim()))
                    minutos = i * 60 - (i - 1) * 60;
                else
                    minutos = 0;

                if (minutos.ToString().Trim().Length > 1)
                {
                    min_real = minutos.ToString().Trim();
                }
                else
                {
                    min_real = "0" + minutos.ToString().Trim();
                }

                if (i < 13)
                {
                    if (i.ToString().Length > 1)
                    {
                        dr["id"] = i.ToString() + ":" + min_real;
                        dr["des"] = i.ToString() + ":" + min_real + "am";
                    }
                    else
                    {
                        dr["id"] = "0" + i.ToString() + ":00";
                        dr["des"] = "0" + i.ToString() + ":00" + "am";
                    }
                }
                else
                {
                    dr["id"] = i.ToString() + ":" + min_real;
                    dr["des"] = i.ToString() + ":" + min_real + "pm";
                }
                DT_HORAS.Rows.Add(dr);
            }
        }

        return DT_HORAS;
    }

    private void llenar_ddl_horas(DropDownList ddl)
    {
        ParametrosBackOfficeBE objEntHorDef = new ParametrosBackOfficeBE();
        ParametrosBackOffieBL objNegHorDef = new ParametrosBackOffieBL();
        objEntHorDef = objNegHorDef.GetHorarioXDefecto();

        DateTime dtHoraIni = Convert.ToDateTime(objEntHorDef.HoraInicio.ToString().Trim());
        DateTime dtHoraFin = Convert.ToDateTime(objEntHorDef.HoraFinal.ToString().Trim());
        int v_intminutos = int.Parse(objEntHorDef.IntervaloTime.ToString().Trim());

        ddl.DataSource = objNegHorDef.GetHorasSRC();
        ddl.DataTextField = "DES";
        ddl.DataValueField = "ID";
        ddl.DataBind();

        if (objNegHorDef.GetHorasSRC().Count > 0)
        {
            ddl.SelectedValue = objEntHorDef.HoraInicio;
        }
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
    #endregion

    protected void InicializaPagina()
    {
        try
        {
            TablaDias_off();
            TablaDias_on();
            llenar_ddl_horas(ddl_hora_de);
            llenar_ddl_horas(ddl_hora_a);
            Llenar_listas(Session["val_defecto"].ToString());
            Session["ind_msg"] = "0";
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
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        DateTime strf1 = Convert.ToDateTime(ddl_hora_de.SelectedValue.ToString().Trim());
        DateTime strf2 = Convert.ToDateTime(ddl_hora_a.SelectedValue.ToString().Trim());

        if (strf1 < strf2)
        {
            objEnt = new ParametrosBackOfficeBE();
            objNeg = new ParametrosBackOffieBL();
            string valor = "";
            string dias = "";
            int rpta = 0;
            for (int index = 0; index < lst_dias_on.Items.Count; index++)
            {
                if (lst_dias_on.Items.Count - index != 1)
                {
                    dias += lst_dias_on.Items[index].Value.ToString() + ",";
                }
                else
                {
                    dias += lst_dias_on.Items[index].Value.ToString();
                }
            }
            valor = dias + "|" + ddl_hora_de.SelectedValue.ToString() + "|" + ddl_hora_a.SelectedValue.ToString();
            objEnt.no_tipo_valor = Session["no_tipo_valor"].ToString();
            objEnt.nid_parametro = int.Parse(Session["nid_parametro"].ToString());
            objEnt.valor = valor;
            objEnt.co_usuario = Profile.UserName;
            objEnt.co_usuario_red = Profile.UsuarioRed;
            objEnt.no_estacion_red = Profile.Estacion;
            rpta = objNeg.GETActualizarParametro(objEnt);
            if (rpta > 0)
            {
                Session["ind_msg"] = "1";
                lbl_mensajebox.Text = "El registro se actualizo con exito.";
                popup_msgbox_confirm.Show();
            }
            else
            {
                Session["ind_msg"] = "1";
                lbl_mensajebox.Text = "Error al actualizar.";
                popup_msgbox_confirm.Show();
            }
        }
        else
        {
            Session["ind_msg"] = "0";
            lbl_mensajebox.Text = "Hora final debe ser mayor a hora de inicio.";
            popup_msgbox_confirm.Show();
        }
        hid_tab1.Value = "0";
    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SGS_QNET_Citas_Parametros.aspx");
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
    protected void btn_msgboxconfir_no_Click(object sender, EventArgs e)
    {
        if (Session["ind_msg"].ToString().Trim().Equals("1"))
        {
            popup_msgbox_confirm.Hide();
            Response.Redirect("SGS_QNET_Citas_Parametros.aspx");
        }
        else if (Session["ind_msg"].ToString().Trim().Equals("0"))
        {
            popup_msgbox_confirm.Hide();
        }
    }
}
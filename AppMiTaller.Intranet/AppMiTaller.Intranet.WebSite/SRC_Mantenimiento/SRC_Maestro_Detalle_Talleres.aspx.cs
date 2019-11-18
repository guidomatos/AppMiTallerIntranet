using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.Web.Services;

public partial class SRC_Mantenimiento_SRC_Maestro_Detalle_Talleres : System.Web.UI.Page
{
    #region "GLOBALES"
    TallerBE objEnt = new TallerBE();
    TallerBL objNeg = new TallerBL();

    CitasBEList oCitasBEList;
    TallerHorariosExcepcionalBE objEntHorexcep = new TallerHorariosExcepcionalBE();
    TallerHorariosExcepcionalBEList objListHorexcep = new TallerHorariosExcepcionalBEList();

    System.Data.DataTable DT_HorExcep;
    System.Data.DataTable DT_HorExcepDet;


    #endregion

    #region "METODOS PROPIOS"

    public enum DateInterval
    {
        Day,
        DayOfYear,
        Hour,
        Minute,
        Month,
        Quarter,
        Second,
        Weekday,
        WeekOfYear,
        Year
    }

    //DATOS GENERALES 

    private void CargarEstado()
    {
        ddl_estado.Items.Clear();
        ddl_estado.Items.Add("");
        ddl_estado.Items[0].Text = "--Seleccione--";
        ddl_estado.Items[0].Value = "";
        ddl_estado.Items.Add("");
        ddl_estado.Items[1].Text = "Activo";
        ddl_estado.Items[1].Value = "A";
        ddl_estado.Items.Add("");
        ddl_estado.Items[2].Text = "Inactivo";
        ddl_estado.Items[2].Value = "I";


    }

    private void CargarUbigeo()
    {
        TallerBL objneg = new TallerBL();
        List<TallerBE> ListUbigeo = objneg.GETListarUbigeo(Profile.Usuario.Nid_usuario);
        System.Data.DataTable dtUbigeo = new System.Data.DataTable();
        dtUbigeo.Columns.Add("coddpto", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("codprov", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("coddist", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("nombre", System.Type.GetType("System.String"));

        for (Int32 i = 0; i < ListUbigeo.Count; i++)
            dtUbigeo.Rows.Add(ListUbigeo[i].coddpto, ListUbigeo[i].codprov, ListUbigeo[i].coddist, ListUbigeo[i].Ubigeo);

        ViewState.Add("dtubigeo", dtUbigeo);
        ddl_prov.Items.Insert(0, new ListItem("--Seleccione--", ""));

        ddl_prov.Enabled = false;
        ddl_dist.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddl_dist.Enabled = false;
        DataRow[] oRow = dtUbigeo.Select("codprov='00' AND coddist='00'", "nombre", DataViewRowState.CurrentRows);
        ddl_dpto.Items.Clear();
        for (Int32 i = 0; i < oRow.Length; i++)
        {
            ddl_dpto.Items.Add("");
            ddl_dpto.Items[i].Value = oRow[i]["coddpto"].ToString();
            ddl_dpto.Items[i].Text = oRow[i]["nombre"].ToString();
        }
        ddl_dpto.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddl_dpto.SelectedIndex = 0;
        ddl_dpto.AutoPostBack = true;
        objneg = null; ListUbigeo = null; dtUbigeo = null;
    }

    //HORARIOS    

    private void CargarFeriados()
    {
        TallerBL neg = new TallerBL();
        List<TallerBE> list = neg.GETListarFeriados();
        System.Data.DataTable dt = new System.Data.DataTable();
        dt.Columns.Add("ID", System.Type.GetType("System.String"));
        dt.Columns.Add("DES", System.Type.GetType("System.String"));
        for (Int32 i = 0; i < list.Count; i++)
        {
            if (dt.Select("DES = '" + list[i].DES + "'").Length == 0)
                dt.Rows.Add(list[i].ID, list[i].DES);
        }
        lst_DiasExcep.Items.Clear();
        for (Int32 i = 0; i < dt.Rows.Count; i++)
        {
            lst_DiasExcep.Items.Add("");
            lst_DiasExcep.Items[i].Value = dt.Rows[i]["ID"].ToString();
            lst_DiasExcep.Items[i].Text = dt.Rows[i]["DES"].ToString();
        }
        dt = null;
        neg = null;
    }

    private void CargarHora()
    {
        List<TallerBE> List = objNeg.GETListarDiasDisp();
        String horaIni = List[0].No_valor1.Split('|')[1];
        String horaFin = List[0].No_valor1.Split('|')[2];
        ViewState.Add("horaIni", horaIni);
        ViewState.Add("horaFin", horaFin);
        //dtHoraIni.ToString("HH:mm", ci)

        cargarRangoHorasDefecto(ref ddl_horainicio, horaIni, horaFin);
        cargarRangoHorasDefecto(ref ddl_horafin, horaIni, horaFin);
    }

    private String DevolverDia(Int32 dia)
    {
        String[] nombre_dia = { "", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
        return nombre_dia[dia];
    }

    private void CargarDiasDisponibles()
    {
        lst_DiasDisp.Items.Clear();
        lst_DiasHab.Items.Clear();

        hf_Capacidad.Value = "";
        //txt_Capacidad.Text = "";
        string strCapacidadT = string.Empty;

        TallerBL neg = new TallerBL();
        List<TallerBE> list = neg.GETListarDiasDisp();
        String dias = list[0].No_valor1.ToString().Split('|')[0];

        System.Data.DataTable dt = new System.Data.DataTable();
        dt.Columns.Add("dia", System.Type.GetType("System.String"));
        for (Int32 i = 0; i < dias.Length; i++)
        {
            if (!dias.Substring(i, 1).Equals(","))
                dt.Rows.Add(dias.Substring(i, 1));
        }
        for (Int32 i = 0; i < dt.Rows.Count; i++)
        {
            lst_DiasHab.Items.Add("");
            lst_DiasHab.Items[i].Value = dt.Rows[i]["dia"].ToString();
            lst_DiasHab.Items[i].Text = DevolverDia(Convert.ToInt32(dt.Rows[i]["dia"].ToString()));

            strCapacidadT += dt.Rows[i]["dia"].ToString() + "----|";
        }

        if (strCapacidadT.Length > 0) strCapacidadT = strCapacidadT.Substring(0, strCapacidadT.Length - 1);

        hf_Capacidad.Value = strCapacidadT;

        txt_capacidad_fo.Text = "";
        txt_capacidad_bo.Text = "";
        txt_capacidad.Text = "";

        System.Data.DataTable dt1 = new System.Data.DataTable();
        dt1.Columns.Add("dia", System.Type.GetType("System.String"));
        Boolean existe = false;
        for (Int32 i = 0; i < 7; i++)
        {
            for (Int32 j = 0; j < lst_DiasHab.Items.Count; j++)
            {
                if ((i + 1) == Convert.ToInt32(lst_DiasHab.Items[j].Value))
                    existe = true;
            }
            if (!existe)
                dt1.Rows.Add(Convert.ToString(i + 1));
            existe = false;
        }
        for (Int32 i = 0; i < dt1.Rows.Count; i++)
        {
            lst_DiasDisp.Items.Add("");
            lst_DiasDisp.Items[i].Value = dt1.Rows[i]["dia"].ToString();
            lst_DiasDisp.Items[i].Text = DevolverDia(Convert.ToInt32(dt1.Rows[i]["dia"].ToString()));
        }

        System.Data.DataTable dt3 = (System.Data.DataTable)ViewState["dtHorario"];
        dt3.Rows.Clear();
        for (Int32 i = 0; i < lst_DiasHab.Items.Count; i++)
            dt3.Rows.Add(lst_DiasHab.Items[i].Value, ddl_horainicio.Text, ddl_horafin.Text, "1");
        for (Int32 i = 0; i < lst_DiasDisp.Items.Count; i++)
            dt3.Rows.Add(lst_DiasDisp.Items[i].Value, ddl_horainicio.Text, ddl_horafin.Text, "0");
        ViewState["dtHorario"] = dt3;
        dt = null; dt1 = null; neg = null; dt3 = null;
    }

    private void CargarIntervalosAtencion()
    {
        List<TallerBE> List = objNeg.GETListarIntervalosAtencion();

        System.Data.DataTable dtIntervAtenc = new System.Data.DataTable();
        dtIntervAtenc.Columns.Add("Cod_intervalo", System.Type.GetType("System.Int32"));
        dtIntervAtenc.Columns.Add("NUM_INTERVALO", System.Type.GetType("System.String"));
        for (int i = 0; i < List.Count; i++)
            dtIntervAtenc.Rows.Add(List[i].Cod_intervalo, List[i].Num_intervalo);

        for (int i = 0; i < dtIntervAtenc.Rows.Count; i++)
        {
            ddl_intervAtenc.Items.Add("");
            ddl_intervAtenc.Items[i].Value = dtIntervAtenc.Rows[i]["Cod_intervalo"].ToString();
            ddl_intervAtenc.Items[i].Text = dtIntervAtenc.Rows[i]["NUM_INTERVALO"].ToString() + " minutos";
        }

        if (ddl_intervAtenc.Items.Count > 0)
        {
            ddl_intervAtenc.Items.Insert(0, "--Seleccionar--");
            ddl_intervAtenc.Enabled = true;
        }
        else
            ddl_intervAtenc.Enabled = false;

        dtIntervAtenc = null;
    }

    //SERVICIOS

    private void CargarServicios()
    {
        List<TallerBE> List = objNeg.GETListarServicios();
        System.Data.DataTable dtTipoServ = new System.Data.DataTable();
        System.Data.DataTable dtServ = new System.Data.DataTable();
        System.Data.DataTable dtServsel = (System.Data.DataTable)ViewState["dtServiciosSel"];

        dtTipoServ.Columns.Add("nid_tipo_servicio", System.Type.GetType("System.Int32"));
        dtTipoServ.Columns.Add("no_tipo_servicio", System.Type.GetType("System.String"));

        dtServ.Columns.Add("nid_tipo_servicio", System.Type.GetType("System.Int32"));
        dtServ.Columns.Add("nid_servicio", System.Type.GetType("System.Int32"));
        dtServ.Columns.Add("no_servicio", System.Type.GetType("System.String"));
        //
        dtServ.Columns.Add("existe", System.Type.GetType("System.String"));
        //

        for (int i = 0; i < List.Count; i++)
        {
            if (dtTipoServ.Select("nid_tipo_servicio=" + Convert.ToInt32(List[i].Nid_tserv)).Length == 0)
                dtTipoServ.Rows.Add(List[i].Nid_tserv, List[i].No_tserv);
            dtServ.Rows.Add(List[i].Nid_tserv, List[i].Nid_serv, List[i].No_serv, "0");
        }

        if (dtServsel.Rows.Count > 0)
        {
            for (Int32 i = 0; i < dtServsel.Rows.Count; i++)
            {
                for (Int32 j = 0; j < dtServ.Rows.Count; j++)
                {
                    if ((Convert.ToInt32(dtServsel.Rows[i]["nid_servicio"].ToString())) == Convert.ToInt32(dtServ.Rows[j]["nid_servicio"].ToString()))
                    {
                        DataRow[] fila = dtServ.Select("nid_servicio = " + Convert.ToInt32(dtServsel.Rows[i]["nid_servicio"].ToString()));
                        fila[0]["existe"] = "1";
                        break;
                    }
                }
            }
        }
        ViewState.Add("Tipo_Serv", dtTipoServ);
        ViewState.Add("Serv", dtServ);

        DataRow[] oRow = dtTipoServ.Select("", "no_tipo_servicio", DataViewRowState.CurrentRows);
        for (int i = 0; i < oRow.Length; i++)
        {
            ddl_tiposerv.Items.Add("");
            ddl_tiposerv.Items[i].Value = oRow[i]["nid_tipo_servicio"].ToString();
            ddl_tiposerv.Items[i].Text = oRow[i]["no_tipo_servicio"].ToString();
        }

        if (ddl_tiposerv.Items.Count > 0)
        {
            ddl_tiposerv.Items.Insert(0, "--Seleccionar--");
            ddl_tiposerv.SelectedIndex = 0;
            ddl_tiposerv.Enabled = true;
        }
        else
        {
            ddl_tiposerv.Items.Insert(0, "--Seleccionar--");
            ddl_tiposerv.SelectedIndex = 0;
            ddl_tiposerv.Enabled = false;
        }

        dtTipoServ = null;
        dtServ = null;
    }

    //MARCAS Y MODELOS

    private void CargarMarcasModelos()
    {
        TallerBE ent = new TallerBE();
        ent.Co_perfil_usuario = Profile.Usuario.co_perfil_usuario;
        ent.Nid_usuario = Profile.Usuario.Nid_usuario;

        List<TallerBE> List = objNeg.GETListarMarcasModelos(ent);

        System.Data.DataTable dtMarcas = new System.Data.DataTable();
        System.Data.DataTable dtModelos = new System.Data.DataTable();
        System.Data.DataTable dtModelosSel = (System.Data.DataTable)ViewState["dtModelosSel"];

        dtMarcas.Columns.Add("nid_marca", System.Type.GetType("System.Int32"));
        dtMarcas.Columns.Add("no_marca", System.Type.GetType("System.String"));

        dtModelos.Columns.Add("nid_marca", System.Type.GetType("System.Int32"));
        dtModelos.Columns.Add("nid_modelo", System.Type.GetType("System.Int32"));
        dtModelos.Columns.Add("no_modelo", System.Type.GetType("System.String"));
        //
        dtModelos.Columns.Add("existe", System.Type.GetType("System.String"));
        //
        for (int i = 0; i < List.Count; i++)
        {
            if (dtMarcas.Select("nid_marca=" + Convert.ToInt32(List[i].Nid_marca)).Length == 0)
                dtMarcas.Rows.Add(List[i].Nid_marca, List[i].No_marca);
            dtModelos.Rows.Add(List[i].Nid_marca, List[i].Nid_modelo, List[i].No_modelo, "0");
        }

        if (dtModelosSel.Rows.Count > 0)
        {
            for (Int32 i = 0; i < dtModelosSel.Rows.Count; i++)
            {
                for (Int32 j = 0; j < dtModelos.Rows.Count; j++)
                {
                    if ((Convert.ToInt32(dtModelosSel.Rows[i]["nid_modelo"].ToString())) == Convert.ToInt32(dtModelos.Rows[j]["nid_modelo"].ToString()))
                    {
                        DataRow[] fila = dtModelos.Select("nid_modelo = " + Convert.ToInt32(dtModelosSel.Rows[i]["nid_modelo"].ToString()));
                        fila[0]["existe"] = "1";
                        break;
                    }
                }
            }
        }

        ViewState.Add("Marca", dtMarcas);
        ViewState.Add("Modelo", dtModelos);

        DataRow[] oRow = dtMarcas.Select("", "", DataViewRowState.CurrentRows);
        for (int i = 0; i < oRow.Length; i++)
        {
            ddl_marca.Items.Add("");
            ddl_marca.Items[i].Value = oRow[i]["nid_marca"].ToString();
            ddl_marca.Items[i].Text = oRow[i]["no_marca"].ToString();
        }

        if (ddl_marca.Items.Count > 0)
        {
            ddl_marca.Items.Insert(0, "--Seleccionar--");
            ddl_marca.SelectedIndex = 0;
            ddl_marca.Enabled = true;
        }
        else
            ddl_marca.Enabled = false;

        dtMarcas = null;
        dtModelos = null;
        ent = null;
    }

    private void Nuevo()
    {
        txt_codtall.Text = "";
        txt_nomtall.Text = "";
        ddl_dpto.SelectedIndex = -1;
        ddl_prov.Enabled = false;
        ddl_dist.SelectedIndex = -1; ddl_dist.Enabled = false;
        ddl_ptored.SelectedIndex = -1; ddl_ptored.Enabled = false;

        txt_direccion.Text = "";
    }

    //PARA UPDATE

    private void MostrarDetallePorPuntoRed(TallerBE ent)
    {
        txt_direccion.Text = String.Empty;
        List<TallerBE> List = objNeg.GETDetallePorPuntoRed(objEnt);
        txt_direccion.Text = List[0].Di_ubica.ToString();
    }

    private void MostrarDiasExceptuadosPorTaller()
    {
      
        if (Session["editar"] != null)
            objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
        else if (Session["detalle"] != null)
            objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());
        List<TallerBE> List = objNeg.GETDiasExcepPorTaller(objEnt);
        ViewState.Add("listdiasexcep", List);
        if (List.Count > 0)
        {
            lst_DiasExcep.Items.Clear();
            for (int i = 0; i < List.Count; i++)
            {
                lst_DiasExcep.Items.Add("");
                lst_DiasExcep.Items[i].Text = List[i].Fe_exceptuada1.Split('|')[0];
                lst_DiasExcep.Items[i].Value = List[i].Fe_exceptuada1.Split('|')[1];
            }
        }
        else
        {
            CargarFeriados();
        }
    }

    private void CargarGridServicios()
    {
        if (Session["editar"] != null)
            objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
        else if (Session["detalle"] != null)
            objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());

        List<TallerBE> List = objNeg.GETServiciosPorTaller(objEnt);
        System.Data.DataTable dt1 = new System.Data.DataTable();

        dt1.Columns.Add("nid_tipo_servicio", System.Type.GetType("System.Int32"));
        dt1.Columns.Add("no_tipo_servicio", System.Type.GetType("System.String"));
        dt1.Columns.Add("nid_servicio", System.Type.GetType("System.Int32"));
        dt1.Columns.Add("no_servicio", System.Type.GetType("System.String"));
        dt1.Columns.Add("no_dias", System.Type.GetType("System.String"));

        for (int i = 0; i < List.Count; i++)
        {
            string strDias = List[i].no_dias.Trim ();
            string sDias = string.Empty;
            if (!String.IsNullOrEmpty(strDias))
            {
                //2|3|4|5|6
                sDias += (strDias.IndexOf('1') != -1) ? "1" : "0";
                sDias += (strDias.IndexOf('2') != -1) ? "1" : "0";
                sDias += (strDias.IndexOf('3') != -1) ? "1" : "0";
                sDias += (strDias.IndexOf('4') != -1) ? "1" : "0";
                sDias += (strDias.IndexOf('5') != -1) ? "1" : "0";
                sDias += (strDias.IndexOf('6') != -1) ? "1" : "0";
            }
            else
                sDias = "1|2|3|4|5|6";


            dt1.Rows.Add(List[i].Nid_tserv, List[i].No_tserv, List[i].Nid_serv, List[i].No_serv, sDias);
        }


            

        
        ViewState.Add("dtServiciosSel", dt1);
        ViewState.Add("dtServiciosSel_Edit", dt1);

        if (dt1.Rows.Count == 0)
        {
            LlenarVacio();
        }
        else
        {
            gd_servsel.DataSource = dt1;
            gd_servsel.DataBind();
            EstablecerValoresDias();
        }

        dt1 = null;
    }

    private void CargarGridModelos()
    {
        if (Session["editar"] != null)
            objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
        else if (Session["detalle"] != null)
            objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());

        List<TallerBE> List = objNeg.GETModelosPorTaller(objEnt);

        List<TallerBE> lstCapacidad = objNeg.GETListarCapacidadTallerModelo(objEnt);
        
        System.Data.DataTable dt1 = new System.Data.DataTable();
        dt1.Columns.Add("nid_marca", System.Type.GetType("System.Int32"));
        dt1.Columns.Add("no_marca", System.Type.GetType("System.String"));
        dt1.Columns.Add("nid_modelo", System.Type.GetType("System.Int32"));
        dt1.Columns.Add("no_modelo", System.Type.GetType("System.String"));
        dt1.Columns.Add("qt_capacidad", System.Type.GetType("System.String"));

        for (int i = 0; i < List.Count; i++)
        {
            string strCapacidad = "1-|2-|3-|4-|5-|6-|7-";

            foreach (TallerBE oModelo in lstCapacidad)
            {
                if (oModelo.Nid_modelo == List[i].Nid_modelo)
                {
                    switch (oModelo.Dd_atencion)
                    {
                        case 1: strCapacidad = strCapacidad.Replace("1-|", "1-" + oModelo.qt_capacidad.ToString() + "|"); break;
                        case 2: strCapacidad = strCapacidad.Replace("2-|", "2-" + oModelo.qt_capacidad.ToString() + "|"); break;
                        case 3: strCapacidad = strCapacidad.Replace("3-|", "3-" + oModelo.qt_capacidad.ToString() + "|"); break;
                        case 4: strCapacidad = strCapacidad.Replace("4-|", "4-" + oModelo.qt_capacidad.ToString() + "|"); break;
                        case 5: strCapacidad = strCapacidad.Replace("5-|", "5-" + oModelo.qt_capacidad.ToString() + "|"); break;
                        case 6: strCapacidad = strCapacidad.Replace("6-|", "6-" + oModelo.qt_capacidad.ToString() + "|"); break;
                        case 7: strCapacidad = strCapacidad.Replace("7-", "7-" + oModelo.qt_capacidad.ToString() + ""); break;
                    }
                }
            }

            dt1.Rows.Add(List[i].Nid_marca, List[i].No_marca, List[i].Nid_modelo, List[i].No_modelo, strCapacidad);
        }
                            
        ViewState["dtModelosSel"] = dt1;
        ViewState.Add("dtModelosSel_Edit", dt1);

        if (dt1.Rows.Count == 0)
        {
            LlenarVacio_2();
        }
        else
        {
            gd_modsel.DataSource = dt1;
            gd_modsel.DataBind();

            EstablecerValoresCapacidad_TallerModelo();
        }

        dt1 = null;

    }

    private void CargarUbicacion()
    {
        TallerBL objNeg = new TallerBL();
        TallerBE objEnt = new TallerBE();
        objEnt.Co_perfil_usuario = Profile.Usuario.co_perfil_usuario;
        objEnt.Nid_usuario = Profile.Usuario.Nid_usuario;
        List<TallerBE> List = objNeg.GETListarUbicacion(objEnt);
        if (List.Count > 0)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("nid_ubica", System.Type.GetType("System.Int32"));
            dt.Columns.Add("no_ubica", System.Type.GetType("System.String"));
            dt.Columns.Add("coddpto", System.Type.GetType("System.String"));
            dt.Columns.Add("codprov", System.Type.GetType("System.String"));
            dt.Columns.Add("coddist", System.Type.GetType("System.String"));
            for (Int32 i = 0; i < List.Count; i++)
                dt.Rows.Add(List[i].nid_ubica, List[i].no_ubica, List[i].coddpto, List[i].codprov, List[i].coddist);
            ViewState.Add("dtubicacion", dt);
            dt = null;
        }
        objNeg = null; objEnt = null;
        ddl_ptored.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddl_ptored.SelectedIndex = 0;
        ddl_ptored.Enabled = false;
    }
    private void CargarDiasHabiles_Horas_PorTaller()
    {
        if (Session["editar"] != null)
            objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
        else if (Session["detalle"] != null)
            objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());
        if (Session["editar"] != null || Session["detalle"] != null)
        {
            System.Data.DataTable dtHora = new System.Data.DataTable();
            dtHora.Columns.Add("dd_atencion", System.Type.GetType("System.Int32"));
            dtHora.Columns.Add("ho_inicio", System.Type.GetType("System.String"));
            dtHora.Columns.Add("ho_fin", System.Type.GetType("System.String"));
            dtHora.Columns.Add("habil", System.Type.GetType("System.String"));
            for (Int32 i = 1; i <= 7; i++)
                dtHora.Rows.Add(i, ViewState["horaIni"].ToString(), ViewState["horaFin"].ToString(), "0");
            List<TallerBE> List = objNeg.GETDiasHabiles_Hora_PorTaller(objEnt);
            if (List.Count > 0)
            {
                for (Int32 j = 0; j < List.Count; j++)
                {
                    DataRow[] fila = dtHora.Select("dd_atencion = " + List[j].Dd_atencion);
                    if (fila.Length > 0)
                    {
                        fila[0]["ho_inicio"] = List[j].HoraInicio;
                        fila[0]["ho_fin"] = List[j].HoraFin;
                        fila[0]["habil"] = "1";
                    }
                }
                Boolean hora_diferente = false;
                for (Int32 i = 0; i < 1; i++)
                {
                    for (Int32 j = 1; j < List.Count; j++)
                    {
                        if ((List[i].HoraInicio != List[j].HoraInicio) && (List[i].HoraFin != List[j].HoraFin))
                        { hora_diferente = true; break; }
                    }
                }
                if (!hora_diferente)
                {
                    ddl_horainicio.Text = List[0].HoraInicio;
                    ddl_horafin.Text = List[0].HoraFin;
                }
                lst_DiasHab.Items.Clear();
                for (Int32 i = 0; i < List.Count; i++)
                {
                    lst_DiasHab.Items.Add("");
                    lst_DiasHab.Items[i].Value = List[i].Dd_atencion.ToString();
                    lst_DiasHab.Items[i].Text = DevolverDia(List[i].Dd_atencion);
                }
                System.Data.DataTable dt1 = new System.Data.DataTable();
                dt1.Columns.Add("dia", System.Type.GetType("System.String"));
                Boolean existe = false;
                for (Int32 i = 0; i < 7; i++)
                {
                    for (Int32 j = 0; j < lst_DiasHab.Items.Count; j++)
                    {
                        if ((i + 1) == Convert.ToInt32(lst_DiasHab.Items[j].Value))
                            existe = true;
                    }
                    if (!existe)
                        dt1.Rows.Add(Convert.ToString(i + 1));
                    existe = false;
                }
                lst_DiasDisp.Items.Clear();
                for (Int32 i = 0; i < dt1.Rows.Count; i++)
                {
                    lst_DiasDisp.Items.Add("");
                    lst_DiasDisp.Items[i].Value = dt1.Rows[i]["dia"].ToString();
                    lst_DiasDisp.Items[i].Text = DevolverDia(Convert.ToInt32(dt1.Rows[i]["dia"].ToString()));
                }
                dt1 = null;
                ViewState.Add("dtHorario_sel", dtHora);
            }
            else
            {
                CargarDiasDisponibles();
                ViewState.Add("dtHorario_sel", (System.Data.DataTable)ViewState["dtHorario"]);
            }
            ViewState.Add("dtHorario_editar", dtHora);

            dtHora = null; List = null;
        }
    }

    private void CargarCantidadAtencionDia_PorTaller()
    {

        TallerHorariosBE objECapacidad = new TallerHorariosBE();
        TallerBL objLCapacidad = new TallerBL();


        objECapacidad.nid_propietario = (Session["editar"] != null) ? Convert.ToInt32(Session["editar"].ToString()) : Convert.ToInt32(Session["detalle"].ToString());

        List<TallerHorariosBE> lstCapacidad = objLCapacidad.GETListarCapacidadAtencion_PorTaller(objECapacidad);

        string strTmpCapacidad = string.Empty;
        bool blnExiste = false;

        for (int i = 0; i < lst_DiasHab.Items.Count; i++)
        {
            string strDiaValue = lst_DiasHab.Items[i].Value.ToString();

            blnExiste = false;

            foreach (TallerHorariosBE oCapacidad in lstCapacidad)
            {
                if (strDiaValue.Equals(oCapacidad.dd_atencion.ToString()))
                {
                    string strCControl = oCapacidad.fl_control.ToString();
                    string strCFO = oCapacidad.qt_capacidad_fo.Equals(-1) ? "" : oCapacidad.qt_capacidad_fo.ToString();
                    string strCBO = oCapacidad.qt_capacidad_bo.Equals(-1) ? "" : oCapacidad.qt_capacidad_bo.ToString();
                    string strCTotal = oCapacidad.qt_capacidad.Equals(-1) ? "" : oCapacidad.qt_capacidad.ToString();

                    strTmpCapacidad +=  oCapacidad.dd_atencion.ToString()  + "-"+strCControl + "-" + strCFO + "-" + strCBO + "-" + strCTotal + "|";
                    blnExiste = true;

                    break;
                }
            }

            if (!blnExiste) strTmpCapacidad += strDiaValue + "----|";
        }

        if (strTmpCapacidad.Length > 0) strTmpCapacidad = strTmpCapacidad.Substring(0, strTmpCapacidad.Length - 1);

        hf_Capacidad.Value = strTmpCapacidad;

    }

    private void LlenarVacio()
    {
        //INICIALIZANDO EL GRIDVIEW
        this.oCitasBEList = new CitasBEList();
        this.oCitasBEList.Add(new CitasBE());
        this.gd_servsel.DataSource = this.oCitasBEList;
        this.gd_servsel.DataBind();
    }

    private void LlenarVacio_2()
    {
        //INICIALIZANDO EL GRIDVIEW
        this.oCitasBEList = new CitasBEList();
        this.oCitasBEList.Add(new CitasBE());
        this.gd_modsel.DataSource = this.oCitasBEList;
        this.gd_modsel.DataBind();
    }

    private void Inicializa()
    {
        
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOffForm('0');setTabCabeceraOffForm('1');setTabCabeceraOffForm('2');setTabCabeceraOffForm('3');setTabCabeceraOffForm('4');setTabCabeceraOnForm('0');</script>", false);
        tabMantMaesTaller.ActiveTabIndex = 0;

        //tabMantMaesTaller.Tabs[6].Enabled = false;
        hidPerfil.Value = Profile.Usuario.co_perfil_usuario;
         

        Label_X_Pais();
        this.btnEditar.Style.Add("display", "none");
        tabMantMaesTaller.ActiveTabIndex = 0;

        ViewState.Add("existe_codigo", "0");
        ViewState.Add("mapa", "");
        this.btnBuscar.Style.Add("display", "none");
        this.btnNuevo.Style.Add("display", "none");
        this.chkI.Checked = true;
        this.txt_capacidad.Enabled = false;

        if (Request.QueryString["nid_taller"] != null)
        {
            Session["detalle"] = Request.QueryString["nid_taller"];
            hid_id_tllr.Value = Request.QueryString["nid_taller"];
        }
        if (Session["detalle"] != null)
        {
            //btnEditar.Visible = true;
            //btnGrabar.Visible = false;
            this.btnEditar.Style.Add("display", "inline");
            this.btnGrabar.Style.Add("display", "none");
        }
        //
        try
        {
            System.Data.DataTable dtModelosSel = new System.Data.DataTable();
            dtModelosSel.Columns.Add("nid_marca", System.Type.GetType("System.Int32"));
            dtModelosSel.Columns.Add("no_marca", System.Type.GetType("System.String"));
            dtModelosSel.Columns.Add("nid_modelo", System.Type.GetType("System.Int32"));
            dtModelosSel.Columns.Add("no_modelo", System.Type.GetType("System.String"));
            dtModelosSel.Columns.Add("qt_capacidad", System.Type.GetType("System.String"));//-->

            System.Data.DataTable dtServiciosSel = new System.Data.DataTable();
            dtServiciosSel.Columns.Add("nid_tipo_servicio", System.Type.GetType("System.Int32"));
            dtServiciosSel.Columns.Add("no_tipo_servicio", System.Type.GetType("System.String"));
            dtServiciosSel.Columns.Add("nid_servicio", System.Type.GetType("System.Int32"));
            dtServiciosSel.Columns.Add("no_servicio", System.Type.GetType("System.String"));
            dtServiciosSel.Columns.Add("no_dias", System.Type.GetType("System.String"));

            //para horario
            System.Data.DataTable dtHorario = new System.Data.DataTable();
            dtHorario.Columns.Add("dd_atencion", System.Type.GetType("System.Int32"));
            dtHorario.Columns.Add("ho_inicio", System.Type.GetType("System.String"));
            dtHorario.Columns.Add("ho_fin", System.Type.GetType("System.String"));
            dtHorario.Columns.Add("habil", System.Type.GetType("System.String"));
            ViewState.Add("dtHorario", dtHorario);
            //
            ViewState.Add("dtModelosSel", dtModelosSel);
            ViewState.Add("dtServiciosSel", dtServiciosSel);
            CargarEstado();
            CargarUbigeo();
            CargarUbicacion();
            CargarHora();
            CargarIntervalosAtencion();

            hidUsuarioRed.Value = Profile.UsuarioRed;
            hidEstacionRed.Value = Profile.Estacion;

            //-------------------------------------------------------------

            LlenarVacio();
            LlenarVacio_2();


            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOffForm('0');setTabCabeceraOffForm('1');setTabCabeceraOffForm('2');setTabCabeceraOffForm('3');setTabCabeceraOffForm('4');setTabCabeceraOnForm('0');</script>", false);
            
            if (Session["nuevo"] != null)
            {
                ViewState.Add("hora_defecto_nuevo", "1");
                hid_id_tllr.Value = "0";
                ddl_prov.Items.Insert(0, new ListItem("--Seleccione--", ""));
                ddl_dist.Items.Insert(0, new ListItem("--Seleccione--", ""));
                ddl_ptored.Items.Insert(0, new ListItem("--Seleccione--", ""));

                ddl_intervAtenc.SelectedIndex = 0;
                Nuevo();
                CargarDiasDisponibles();

                // --> 18.05
                //tabMantMaesTaller.ActiveTabIndex = 0;
                tabMantMaesTaller.Tabs[4].Enabled = false;
                tabMantMaesTaller.Tabs[5].Enabled = false;//@002
                tabMantMaesTaller.Tabs[6].Enabled = false;
                
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>CargarInicial();</script>", false);
            }
            else if (Session["editar"] != null || Session["detalle"] != null)
            {
                //DATOS GENERALES

                ViewState.Add("hora_defecto_editar", "1");
                if (Session["editar"] != null)
                    objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
                else if (Session["detalle"] != null)
                    objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());

                hid_id_tllr.Value = objEnt.nid_taller.ToString();

                //18.05.2011
                //tabMantMaesTaller.ActiveTabIndex = 0;
                //tabMantMaesTaller.Tabs[5].Enabled = true;

                List<TallerBE> List = objNeg.GETDetalleTaller(objEnt);
                ViewState.Add("co_taller", List[0].co_taller);
                txt_codtall.Text = List[0].co_taller;
                txt_codtall.Enabled = false;
                txt_nomtall.Text = List[0].no_taller;

                if (List[0].co_intervalo_atenc != 0)
                    ddl_intervAtenc.SelectedValue = List[0].co_intervalo_atenc.ToString();
                else
                    ddl_intervAtenc.SelectedIndex = 0;

                if (List[0].coddpto != "")
                {
                    ddl_dpto.SelectedValue = List[0].coddpto;
                    CargarProvinciaPorDepartamento();
                }
                else
                    ddl_dpto.SelectedIndex = 0;

                if (List[0].codprov != "")
                {
                    ddl_prov.SelectedValue = List[0].codprov;
                    CargarDistritoPorProvincia();
                }
                else
                    ddl_prov.SelectedIndex = 0;

                if (List[0].coddist != "")
                {
                    ddl_dist.SelectedValue = List[0].coddist;
                    CargarPuntoRedPorDistrito();
                }
                else
                    ddl_dist.SelectedIndex = 0;

                if (List[0].nid_ubica != 0)
                {
                    ddl_ptored.SelectedValue = List[0].nid_ubica.ToString();
                    objEnt.nid_ubica = Convert.ToInt32(ddl_ptored.SelectedValue);
                    MostrarDetallePorPuntoRed(objEnt);
                }
                else
                {
                    ddl_ptored.SelectedIndex = 0;
                    ddl_ptored.Enabled = false;
                }
                txt_direccion.Text = List[0].no_direccion;
                if (List[0].fl_activo != "")
                    ddl_estado.SelectedValue = List[0].fl_activo;
                else
                    ddl_estado.SelectedIndex = 0;

                //MAPA
                ViewState["mapa"] = List[0].tx_mapa_taller;
                imgMapa.ImageUrl = @"Mapas/" + ViewState["mapa"].ToString();

                //HORARIOS
                CargarDiasHabiles_Horas_PorTaller();
                MostrarDiasExceptuadosPorTaller();
                CargarCantidadAtencionDia_PorTaller();//CAPACIDAD DE ATENCION

                //SERVICIOS
                CargarGridServicios();

                //MODELOS
                CargarGridModelos();

                // HORARIO EXCEPCIONAL
                // 18.05.2011
                //---para ordenamiento
                CargarEstado(ddl_bus_excepestado, "1");
                CargarEstado(ddl_horexcep_estado, "2");
                cargarGrid_HorExcep_Inicio();
                //OcultarPestanasHorExcep();

                //tabMantMaesTaller.Tabs[6].Enabled = true;//@002
                //CreateFolder();//@002
            }
            //
            CargarServicios();
            CargarMarcasModelos();
            //
            if (Session["detalle"] != null)
                Habilitar_Controles(false);

            //---para ordenamiento
            DateTime[] array_exc = new DateTime[0];
            Int32 cant = 0;
            cant = lst_DiasExcep.Items.Count;
            if (cant > 0)
            {
                Array.Resize(ref array_exc, cant);
                //array_exc[0] = DateTime.Now;
                for (Int32 i = 0; i < cant; i++)
                    array_exc[i] = Convert.ToDateTime(lst_DiasExcep.Items[i].Value);
            }
            else
                Array.Resize(ref array_exc, cant);
            ViewState.Add("array_exc", array_exc);
            ViewState.Add("array_long", cant);

            tabMantMaesTaller.ActiveTabIndex = 0;
            hidFechaHoy.Value = DateTime.Now.ToString("dd/MM/yyyy");
            

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');</script>", ex.Message), false);        
        }
    }

    private void Habilitar_Controles(Boolean flag)
    {
        btnGrabar.Enabled = flag;

        //DATOS GENERALES
        txt_codtall.Enabled = flag;
        txt_nomtall.Enabled = flag;
        ddl_dpto.Enabled = flag;
        ddl_prov.Enabled = flag;
        ddl_dist.Enabled = flag;
        ddl_ptored.Enabled = flag;
        ddl_estado.Enabled = flag;
        txt_direccion.Enabled = flag;
        //MAPA
        FileUpload1.Enabled = flag;
        btn_SubirMapa.Enabled = flag;
        //HORARIOS
        lst_DiasDisp.Enabled = flag;
        lst_DiasHab.Enabled = true;
        lst_DiasExcep.Enabled = true;
        txt_dia.Enabled = flag;


        chkI.Enabled = flag;
        chkT.Enabled = flag;

        txt_capacidad_fo.Enabled = (chkI.Enabled && chkI.Checked);
        txt_capacidad_bo.Enabled = (chkI.Enabled && chkI.Checked);
        txt_capacidad.Enabled = (chkT.Enabled && chkT.Checked);


        ddl_horainicio.Enabled = flag;
        ddl_horafin.Enabled = flag;
        ddl_intervAtenc.Enabled = flag;
        cal_DiasExcep.Enabled = flag;

        btn_adddhab.Enabled = flag;
        btn_delhab.Enabled = flag;

        btn_adddiasexc.Enabled = flag;
        btn_deldiasexc.Enabled = flag;

        //SERVICIOS
        ddl_tiposerv.Enabled = flag;
        lst_servdisp.Enabled = flag;
        gd_servsel.Enabled = flag;

        btn_addserv.Enabled = flag;
        btn_delserv.Enabled = flag;

        //MARCA
        ddl_marca.Enabled = flag;
        lst_moddisp.Enabled = flag;
        gd_modsel.Enabled = flag;

        btn_addmod.Enabled = flag;
        btn_delmod.Enabled = flag;

        //HORARIO EXCEPCIONAL - 18.05
        txt_bus_excepdes.Enabled = flag;
        txt_bus_excepfecini.Enabled = flag;
        txt_bus_excepfecfin.Enabled = flag;
        btn_bus_ExcepCalFecIni.Enabled = flag;
        btn_bus_ExcepCalFecFin.Enabled = flag;
        ddl_bus_excepestado.Enabled = flag;

    }

    private void Label_X_Pais()
    {

        Parametros oParm = new Parametros();
        lbl_dep_taller.Text = oParm.N_Departamento.ToString();
        lbl_prov_taller.Text = oParm.N_Provincia.ToString();
        lbl_dist_taller.Text = oParm.N_Distrito.ToString();
        lblTextoLocal.Text = oParm.N_Local.ToString();

        excepCalendarIni.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        excepCalendarFin.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        CalendarExtender1.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        CalendarExtender2.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        oParm = null;

    }
    #endregion

    #region "CODIGO MASIVO"
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_codtall.Attributes.Add("onKeypress", "return Valida_Codigo_Taller(event)");
        txt_nomtall.Attributes.Add("onKeypress", "return Valida_Nombre_Taller(event)");
        txt_capacidad_fo.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_capacidad_bo.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_capacidad.Attributes.Add("onkeypress", "return SoloNumeros(event)");

        if (!Page.IsPostBack)
        {
            Inicializa();            
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bandeja de perfiles
        DataTable dtLista = (System.Data.DataTable)ViewState["dtServiciosSel"];
        //VehiculoBEList oMaestroVehiculoBEList = (VehiculoBEList)(Session["VehiculoBEList"]);
        if (dtLista != null &&
            this.gd_servsel != null &&
            this.gd_servsel.Rows.Count > 0 &&
            this.gd_servsel.PageCount > 1)
        {
            GridViewRow oRow = this.gd_servsel.BottomPagerRow;
            if (oRow != null)
            {
                Label oTotalReg = new Label();
                oTotalReg.Text = String.Format("Total Reg. {0}", (dtLista.Rows.Count));

                oRow.Cells[0].Controls.AddAt(0, oTotalReg);

                Table oTablaPaginacion = (Table)oRow.Cells[0].Controls[1];
                oTablaPaginacion.CellPadding = 0;
                oTablaPaginacion.CellSpacing = 0;
            }
        }
    }

    private Boolean HayError()
    {
        if (Session["editar"] != null || Session["detalle"] != null)
        {
            if (ViewState["co_taller"].ToString() != txt_codtall.Text.Trim())
            {
                if (Existe_Codigo(txt_codtall.Text.Trim()) == "1")
                {
                    lbl_mensajebox.Text = "Codigo de Taller Existe. Ingrese Otro Por Favor.";
                    popup_msgbox_confirm.Show();
                    ViewState["existe_codigo"] = "1";
                    return true;
                }
            }
        }
        else if (Session["nuevo"] != null)
        {
            if (Existe_Codigo(txt_codtall.Text.Trim()) == "1")
            {
                lbl_mensajebox.Text = "Codigo de Taller Existe. Ingrese Otro Por Favor.";
                popup_msgbox_confirm.Show();
                ViewState["existe_codigo"] = "1";
                return true;
            }
        }
        ViewState["existe_codigo"] = "0";

        if (lst_DiasHab.SelectedIndex != -1) lst_DiasHab_SelectedIndexChanged(this, null);
        return false;
    }

    private String Existe_Codigo(String codigo)
    {
        TallerBL neg = new TallerBL();
        TallerBE ent = new TallerBE();
        ent.co_taller = codigo;
        return neg.ExisteCodigo(ent);
    }

    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (HayError() == false)
        {
            GuardarValoresDias();
            GuardarValoresCapacidad_TallerModelo();

            #region GRABAR
            if (Session["nuevo"] != null)
            {
                #region GRABAR DATOS GENERALES

                objEnt.co_taller = txt_codtall.Text.Trim();
                objEnt.no_taller = txt_nomtall.Text.Trim();
                objEnt.co_valoracion = "";
                objEnt.Cod_intervalo = ddl_intervAtenc.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddl_intervAtenc.SelectedValue);
                objEnt.nid_ubica = ddl_ptored.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddl_ptored.SelectedValue);
                objEnt.no_direccion = txt_direccion.Text.Trim();
                objEnt.tx_url_taller = "";
                objEnt.descripcion = txt_descripcion.Text.Trim();

                objEnt.co_usuario = Profile.UserName;
                objEnt.co_usuario_red = Profile.UsuarioRed;
                objEnt.no_estacion_red = Profile.Estacion;
                objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                
                //MAPA
                if (ViewState["mapa"] == null)
                    objEnt.tx_mapa_taller = "";
                else
                    objEnt.tx_mapa_taller = ViewState["mapa"].ToString();

                Int32 res1 = 0;
                res1 = objNeg.InsertTaller(objEnt);
                hid_id_tllr.Value = res1.ToString();
                #endregion

                if (res1 > 0)
                {
                    #region GRABAR HORARIOS

                    if (lst_DiasHab.Items.Count > 0)
                    {
                        objEnt.nid_taller = res1;
                        System.Data.DataTable dth = (System.Data.DataTable)ViewState["dtHorario"];
                        for (Int32 i = 0; i < dth.Rows.Count; i++)
                        {
                            if (dth.Rows[i]["habil"].ToString() == "1")
                            {
                                objEnt.Dd_atencion = Convert.ToInt32(dth.Rows[i]["dd_atencion"].ToString());
                                objEnt.HoraInicio = dth.Rows[i]["ho_inicio"].ToString();
                                objEnt.HoraFin = dth.Rows[i]["ho_fin"].ToString();
                                objEnt.Fl_tipo = "T";
                                objEnt.co_usuario = Profile.UserName;
                                objEnt.co_usuario_red = Profile.UsuarioRed;
                                objEnt.no_estacion_red = Profile.Estacion;
                                objEnt.fl_activo = "A";
                                //objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                objNeg.InsertTallerHorario(objEnt);
                            }

                        }
                    }
                    #endregion

                    #region GRABAR CAPACIDAD DE ATENCION

                    if (hf_Capacidad.Value.ToString().Trim().Length > 0)
                    {
                        TallerHorariosBE objECapacidad = new TallerHorariosBE();
                        TallerBL objLCapacidad = new TallerBL();

                        objECapacidad.nid_propietario = res1;
                        objECapacidad.fl_tipo = "T";
                        objECapacidad.co_usuario = Profile.UserName;
                        objECapacidad.co_usuario_red = Profile.UsuarioRed;
                        objECapacidad.no_estacion_red = Profile.Estacion;


                        string strTmpCapacidad = string.Empty;
                        string[] strCapacidaDias;

                        strCapacidaDias = hf_Capacidad.Value.ToString().Split('|');
                        foreach (string strCapacidadDia in strCapacidaDias)
                        {
                            if (strCapacidadDia.Trim().Length == 0) continue;

                            string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                            string strC_Tipo = (lst_DiasHab.SelectedIndex == -1) ? ((chkI.Checked) ? "I" : "T") : strCapacidadDia.Split('-').GetValue(1).ToString();
                            string strC_FO = (lst_DiasHab.SelectedIndex == -1) ? ((chkI.Checked) ? txt_capacidad_fo.Text : txt_capacidad.Text) : strCapacidadDia.Split('-').GetValue(2).ToString();
                            string strC_BO = (lst_DiasHab.SelectedIndex == -1) ? ((chkI.Checked) ? txt_capacidad_bo.Text : txt_capacidad.Text) : strCapacidadDia.Split('-').GetValue(3).ToString();
                            string strC_Total = (lst_DiasHab.SelectedIndex == -1) ? ((chkT.Checked) ? txt_capacidad.Text : "") : strCapacidadDia.Split('-').GetValue(4).ToString();

                            if (!String.IsNullOrEmpty(strC_FO) || !String.IsNullOrEmpty(strC_BO) || !String.IsNullOrEmpty(strC_Total))
                            {
                                objECapacidad.dd_atencion = Convert.ToInt32(strC_Dia);
                                objECapacidad.fl_control = strC_Tipo;
                                objECapacidad.qt_capacidad_fo = String.IsNullOrEmpty(strC_FO) ? -1 : Convert.ToInt32(strC_FO);
                                objECapacidad.qt_capacidad_bo = String.IsNullOrEmpty(strC_BO) ? -1 : Convert.ToInt32(strC_BO);
                                objECapacidad.qt_capacidad = String.IsNullOrEmpty(strC_Total) ? -1 : Convert.ToInt32(strC_Total);
                                Int32 oResp = objLCapacidad.MantenimientoCapacidadAtencion_Taller(objECapacidad);
                            }
                        }
                    }
                    #endregion

                    #region GRABAR DIAS EXCEPTUADOS

                    for (int i = 0; i < lst_DiasExcep.Items.Count; i++)
                    {
                        objEnt.nid_taller = res1;
                        objEnt.Fe_exceptuada = Convert.ToDateTime(lst_DiasExcep.Items[i].Value);
                        objEnt.co_usuario = Profile.UserName;
                        objEnt.co_usuario_red = Profile.UsuarioRed;
                        objEnt.no_estacion_red = Profile.Estacion;
                        objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                        objNeg.InsertTallerDiaExceptuado(objEnt);
                    }

                    #endregion

                    #region GRABAR SERVICIOS

                    System.Data.DataTable dt1 = (System.Data.DataTable)ViewState["dtServiciosSel"];
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string _strDias = dt1.Rows[i]["no_dias"].ToString();
                        string strDias = string.Empty;
                        strDias += _strDias[0].ToString().Equals("1") ? "1|" : "";
                        strDias += _strDias[1].ToString().Equals("1") ? "2|" : "";
                        strDias += _strDias[2].ToString().Equals("1") ? "3|" : "";
                        strDias += _strDias[3].ToString().Equals("1") ? "4|" : "";
                        strDias += _strDias[4].ToString().Equals("1") ? "5|" : "";
                        strDias += _strDias[5].ToString().Equals("1") ? "6|" : "";

                        strDias = (!string.IsNullOrEmpty(strDias)) ? strDias.Substring(0, strDias.Length - 1) : strDias;

                        objEnt.nid_taller = res1;
                        objEnt.Nid_serv = Convert.ToInt32(dt1.Rows[i]["nid_servicio"].ToString());
                        objEnt.no_dias = strDias;

                        objEnt.co_usuario = Profile.UserName;
                        objEnt.co_usuario_red = Profile.UsuarioRed;
                        objEnt.no_estacion_red = Profile.Estacion;
                        objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");

                        objNeg.InsertTallerServicio(objEnt);
                    }
                    dt1 = null;

                    #endregion

                    #region GRABAR MODELOS

                    System.Data.DataTable dt2 = ((System.Data.DataTable)ViewState["dtModelosSel"]);
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        objEnt.nid_taller = res1;
                        objEnt.Nid_modelo = Convert.ToInt32(dt2.Rows[i]["nid_modelo"].ToString());
                        objEnt.co_usuario = Profile.UserName;
                        objEnt.co_usuario_red = Profile.UsuarioRed;
                        objEnt.no_estacion_red = Profile.Estacion;
                        objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");

                        objNeg.InsertTallerModelo(objEnt);

                        //Capacidad taller-modelo
                        string strCapacidad = dt2.Rows[i]["qt_capacidad"].ToString();
                        foreach (string sCapacidad in strCapacidad.Split('|'))
                        {
                            if (String.IsNullOrEmpty(sCapacidad)) continue;

                            if (!String.IsNullOrEmpty(sCapacidad.Split('-').GetValue(1).ToString().Trim().Replace("0", "")))
                            {
                                objEnt.Dd_atencion = Int32.Parse(sCapacidad.Split('-').GetValue(0).ToString());
                                objEnt.qt_capacidad = Int32.Parse(sCapacidad.Split('-').GetValue(1).ToString());

                                Int32 oResp = objNeg.InsertarTallerModeloCapacidad(objEnt);
                            }
                        }
                    }

                    dt2 = null;

                    #endregion

                    Session["nuevo"] = null; Session["editar"] = null; Session["detalle"] = null;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('El registro se guardo con exito.');</script>", false);                    
                    lbl_mensajebox.Text = "El registro se guardo con exito.";
                    popup_msgbox_confirm.Show();
                }
            }
            #endregion

            #region ACTUALIZAR

            else if (Session["editar"] != null || Session["detalle"] != null)
            {
                #region ACTUALIZAR DATOS GENERALES

                if (Session["editar"] != null)
                    objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
                else if (Session["detalle"] != null)
                    objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());

                hid_id_tllr.Value = objEnt.nid_taller.ToString();

                objEnt.co_taller = txt_codtall.Text.Trim();
                objEnt.no_taller = txt_nomtall.Text.Trim();
                objEnt.co_valoracion = "";
                objEnt.Cod_intervalo = ddl_intervAtenc.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddl_intervAtenc.SelectedValue);
                objEnt.nid_ubica = ddl_ptored.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddl_ptored.SelectedValue);
                objEnt.no_direccion = txt_direccion.Text.Trim();
                objEnt.descripcion = txt_descripcion.Text.Trim();

                objEnt.Co_usuario_modi = Profile.UserName;
                objEnt.co_usuario_red = Profile.UsuarioRed;
                objEnt.no_estacion_red = Profile.Estacion;

                objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");

                //MAPA
                if (ViewState["mapa"] == null)
                    objEnt.tx_mapa_taller = "";
                else
                    objEnt.tx_mapa_taller = ViewState["mapa"].ToString();
                Int32 res1 = 0;
                res1 = objNeg.ActualizarTaller(objEnt);

                #endregion

                if (res1 > 0)
                {
                    #region ACTUALIZAR HORARIOS
                    //HORARIOS
                    System.Data.DataTable dtHorario_editar = (System.Data.DataTable)ViewState["dtHorario_editar"];
                    System.Data.DataTable dtHorario_sel = (System.Data.DataTable)ViewState["dtHorario_sel"];

                    if (dtHorario_sel != null)
                    {
                        for (Int32 i = 0; i < dtHorario_sel.Rows.Count; i++)
                        {
                            if (dtHorario_sel.Rows[i]["habil"].ToString() == dtHorario_editar.Rows[i]["habil"].ToString())  //update
                            {
                                // update
                                // 0 = 0 - Sin Accion
                                if (dtHorario_sel.Rows[i]["habil"].ToString() == "1")
                                {
                                    if ((dtHorario_sel.Rows[i]["ho_inicio"].ToString() == dtHorario_editar.Rows[i]["ho_inicio"].ToString()) && (dtHorario_editar.Rows[i]["ho_fin"].ToString() == dtHorario_sel.Rows[i]["ho_fin"].ToString()))
                                    {
                                        //no actualizo nada
                                    }
                                    else
                                    {
                                        //actualizo - U                                    
                                        if (Session["editar"] != null)
                                            objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
                                        else if (Session["detalle"] != null)
                                            objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());
                                        objEnt.Dd_atencion = Convert.ToInt32(dtHorario_sel.Rows[i]["dd_atencion"].ToString());
                                        objEnt.HoraInicio = dtHorario_sel.Rows[i]["ho_inicio"].ToString();
                                        objEnt.HoraFin = dtHorario_sel.Rows[i]["ho_fin"].ToString();
                                        objEnt.Co_usuario_modi = "";
                                        objEnt.co_usuario_red = Profile.UsuarioRed;
                                        objEnt.no_estacion_red = Profile.Estacion;
                                        //objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                        objEnt.fl_activo = "A";
                                        objNeg.ActualizarHorario(objEnt);
                                    }
                                }
                            }
                            else
                            {
                                //ha pasado de 1 a 0
                                if (dtHorario_sel.Rows[i]["habil"].ToString() == "0")
                                {
                                    //actualizo - U                          
                                    if (Session["editar"] != null)
                                        objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
                                    else if (Session["detalle"] != null)
                                        objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());
                                    objEnt.Dd_atencion = Convert.ToInt32(dtHorario_sel.Rows[i]["dd_atencion"].ToString());
                                    objEnt.HoraInicio = dtHorario_sel.Rows[i]["ho_inicio"].ToString();
                                    objEnt.HoraFin = dtHorario_sel.Rows[i]["ho_fin"].ToString();
                                    objEnt.Co_usuario_modi = "";
                                    objEnt.co_usuario_red = Profile.UsuarioRed;
                                    objEnt.no_estacion_red = Profile.Estacion;
                                    //objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    objEnt.fl_activo = "I";
                                    objNeg.ActualizarHorario(objEnt);
                                }
                                //ha pasado de 0 a 1
                                else if (dtHorario_sel.Rows[i]["habil"].ToString() == "1")
                                {
                                    //inserto - I
                                    objEnt.Dd_atencion = Convert.ToInt32(dtHorario_sel.Rows[i]["dd_atencion"].ToString());
                                    objEnt.HoraInicio = dtHorario_sel.Rows[i]["ho_inicio"].ToString();
                                    objEnt.HoraFin = dtHorario_sel.Rows[i]["ho_fin"].ToString();
                                    objEnt.Fl_tipo = "T";
                                    objEnt.co_usuario = Profile.UserName;
                                    objEnt.co_usuario_red = Profile.UsuarioRed;
                                    objEnt.no_estacion_red = Profile.Estacion;
                                    objEnt.fl_activo = "A";
                                    //objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    objNeg.InsertTallerHorario(objEnt);
                                }
                                //insert
                            }
                        }
                    }
                    dtHorario_editar = null;
                    dtHorario_sel = null;

                    #endregion

                    #region ACTUALIZAR CAPACIDAD DE ATENCION

                    if (hf_Capacidad.Value.ToString().Trim().Length > 0)
                    {
                        TallerHorariosBE objECapacidad = new TallerHorariosBE();
                        TallerBL objLCapacidad = new TallerBL();

                        if (Session["editar"] != null)
                            objECapacidad.nid_propietario = Convert.ToInt32(Session["editar"].ToString());
                        else if (Session["detalle"] != null)
                            objECapacidad.nid_propietario = Convert.ToInt32(Session["detalle"].ToString());
                        objECapacidad.fl_tipo = "T";
                        objECapacidad.co_usuario = Profile.UserName;
                        objECapacidad.co_usuario_red = Profile.UsuarioRed;
                        objECapacidad.no_estacion_red = Profile.Estacion;


                        Int32 intResp = objLCapacidad.InhabilitarCapacidadAtencion_Taller(objECapacidad);

                        string strTmpCapacidad = string.Empty;
                        string[] strCapacidaDias;

                        strCapacidaDias = hf_Capacidad.Value.ToString().Split('|');

                        foreach (string strCapacidadDia in strCapacidaDias)
                        {
                            if (strCapacidadDia.Trim().Length == 0) continue;

                            string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                            string strC_Tipo =  strCapacidadDia.Split('-').GetValue(1).ToString();
                            string strC_FO = strCapacidadDia.Split('-').GetValue(2).ToString();
                            string strC_BO = strCapacidadDia.Split('-').GetValue(3).ToString();
                            string strC_Total = strCapacidadDia.Split('-').GetValue(4).ToString();
                            
                            if (!String.IsNullOrEmpty(strC_FO) || !String.IsNullOrEmpty(strC_BO) || !String.IsNullOrEmpty(strC_Total))
                            {
                                objECapacidad.dd_atencion = Convert.ToInt32(strC_Dia);
                                objECapacidad.fl_control = strC_Tipo;
                                objECapacidad.qt_capacidad_fo = String.IsNullOrEmpty(strC_FO) ? -1 : Convert.ToInt32(strC_FO);
                                objECapacidad.qt_capacidad_bo = String.IsNullOrEmpty(strC_BO) ? -1 : Convert.ToInt32(strC_BO);
                                objECapacidad.qt_capacidad = String.IsNullOrEmpty(strC_Total) ? -1 : Convert.ToInt32(strC_Total);
                                Int32 oResp = objLCapacidad.MantenimientoCapacidadAtencion_Taller(objECapacidad);
                            }
                        }

                    }
                    #endregion

                    #region ACTUALIZAR DIAS EXCEPTUADOS

                    List<TallerBE> List = (List<TallerBE>)ViewState["listdiasexcep"];

                    Boolean flag4 = true;

                    if (Session["editar"] != null)
                        objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
                    else if (Session["detalle"] != null)
                        objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());

                    if (List != null && lst_DiasExcep != null)
                    {

                        if (List.Count > lst_DiasExcep.Items.Count)
                        {
                            for (Int32 i = 0; i < List.Count; i++)
                            {
                                if (lst_DiasExcep.Items.Count == 0)
                                {
                                    objEnt.Op = "D";
                                    objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].Fe_exceptuada1.Split('|')[1]);
                                    objNeg.MantenimientoTallerDiasExceptuados(objEnt);
                                }
                                else
                                {
                                    for (Int32 j = 0; j < lst_DiasExcep.Items.Count; j++)
                                    {
                                        if (List[i].Fe_exceptuada1.Split('|')[0] == lst_DiasExcep.Items[j].Text)
                                        {
                                            flag4 = true;
                                            break;
                                        }
                                        else
                                            flag4 = false;
                                    }
                                    if (flag4)
                                    {
                                        //UPDATE
                                    }
                                    else
                                    {
                                        //DELETE
                                        objEnt.Op = "D";
                                        objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].Fe_exceptuada1.Split('|')[1]);
                                        objNeg.MantenimientoTallerDiasExceptuados(objEnt);
                                    }
                                }
                            }
                        }
                        else if (List.Count < lst_DiasExcep.Items.Count)
                        {
                            for (Int32 i = 0; i < lst_DiasExcep.Items.Count; i++)
                            {
                                if (List.Count == 0)
                                {
                                    objEnt.Fe_exceptuada = Convert.ToDateTime(lst_DiasExcep.Items[i].Value);
                                    objEnt.co_usuario = Profile.UserName;
                                    objEnt.co_usuario_red = Profile.UsuarioRed;
                                    objEnt.no_estacion_red = Profile.Estacion;
                                    objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    objEnt.Op = "I";
                                    objNeg.MantenimientoTallerDiasExceptuados(objEnt);
                                    //insert
                                }
                                else
                                {
                                    for (Int32 j = 0; j < List.Count; j++)
                                    {
                                        if (lst_DiasExcep.Items[i].Text == List[j].Fe_exceptuada1.Split('|')[0])
                                        {
                                            flag4 = true;
                                            break;
                                        }
                                        else
                                            flag4 = false;
                                    }
                                    if (flag4)
                                    {
                                        //update
                                    }
                                    else
                                    {
                                        objEnt.Fe_exceptuada = Convert.ToDateTime(lst_DiasExcep.Items[i].Value);
                                        objEnt.co_usuario = Profile.UserName;
                                        objEnt.co_usuario_red = Profile.UsuarioRed;
                                        objEnt.no_estacion_red = Profile.Estacion;
                                        objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                        objEnt.Op = "I";
                                        objNeg.MantenimientoTallerDiasExceptuados(objEnt);
                                        //insert
                                    }
                                }
                            }
                        }
                        else if (List.Count == lst_DiasExcep.Items.Count)
                        {
                            for (Int32 i = 0; i < List.Count; i++)
                            {
                                for (Int32 j = 0; j < lst_DiasExcep.Items.Count; j++)
                                {
                                    if (List[i].Fe_exceptuada1.Split('|')[0] == lst_DiasExcep.Items[j].Text)
                                    {
                                        flag4 = true;
                                        break;
                                    }
                                    else
                                        flag4 = false;
                                }
                                if (flag4)
                                {
                                    //UPDATE
                                }
                                else
                                {
                                    //DELETE
                                    objEnt.Op = "D";
                                    objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].Fe_exceptuada1.Split('|')[1]);
                                    objNeg.MantenimientoTallerDiasExceptuados(objEnt);
                                }
                            }


                            for (Int32 i = 0; i < lst_DiasExcep.Items.Count; i++)
                            {
                                for (Int32 j = 0; i < List.Count; j++)
                                {
                                    if (lst_DiasExcep.Items[i].Text == List[j].Fe_exceptuada1.Split('|')[0])
                                    {
                                        flag4 = true;
                                        break;
                                    }
                                    else
                                        flag4 = false;
                                }
                                if (flag4)
                                {
                                    //update
                                }
                                else
                                {
                                    //insert
                                    objEnt.Fe_exceptuada = Convert.ToDateTime(lst_DiasExcep.Items[i].Value);
                                    objEnt.co_usuario = Profile.UserName;
                                    objEnt.co_usuario_red = Profile.UsuarioRed;
                                    objEnt.no_estacion_red = Profile.Estacion;
                                    objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    objEnt.Op = "I";
                                    objNeg.MantenimientoTallerDiasExceptuados(objEnt);
                                }
                            }
                        }
                    }

                    #endregion

                    #region ACTUALIZAR SERVICIOS

                    System.Data.DataTable dtServSelec = ((System.Data.DataTable)ViewState["dtServiciosSel"]);
                    System.Data.DataTable dtServActual = ((System.Data.DataTable)ViewState["dtServiciosSel_Edit"]);
                    
                    Boolean flag = true;

                    if (Session["editar"] != null)
                        objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
                    else if (Session["detalle"] != null)
                        objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());

                    if (dtServActual != null && dtServSelec != null)
                    {
                        if (dtServActual.Rows.Count > dtServSelec.Rows.Count)
                        {
                            foreach (DataRow f1 in dtServActual.Rows)
                            {
                                if (dtServSelec.Rows.Count == 0)
                                {
                                    objEnt.Op = "D";
                                    objEnt.Nid_serv = Convert.ToInt32(f1["nid_servicio"].ToString());
                                    objNeg.MantenimientoTallerServicios(objEnt);
                                    //delete
                                }
                                else
                                {
                                    foreach (DataRow f2 in dtServSelec.Rows)
                                    {
                                        if (f1["nid_servicio"].ToString() == f2["nid_servicio"].ToString())
                                        {
                                            flag = true;
                                            break;
                                        }
                                        else
                                            flag = false;
                                    }
                                    if (flag)
                                    {
                                        //objEnt.Nid_serv = Convert.ToInt32(f1["nid_servicio"].ToString());
                                        //objEnt.Co_usuario_modi = "";
                                        //objEnt.co_usuario_red = Profile.UsuarioRed;
                                        //objEnt.no_estacion_red = Profile.Estacion; 
                                        //objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                        //objEnt.Op = "U";
                                        //objNeg.MantenimientoTallerServicios(objEnt);                                    
                                        //update
                                    }
                                    else
                                    {
                                        objEnt.Op = "D";
                                        objEnt.Nid_serv = Convert.ToInt32(f1["nid_servicio"].ToString());
                                        objEnt.no_dias = string.Empty;
                                        objNeg.MantenimientoTallerServicios(objEnt);
                                        //delete
                                    }
                                }
                            }
                        }
                        else if (dtServActual.Rows.Count < dtServSelec.Rows.Count)
                        {
                            foreach (DataRow f1 in dtServSelec.Rows)
                            {
                                if (dtServActual.Rows.Count == 0)
                                {
                                    string _strDias = f1["no_dias"].ToString();
                                    string strDias = string.Empty;
                                    strDias += _strDias[0].ToString().Equals("1") ? "1|" : "";
                                    strDias += _strDias[1].ToString().Equals("1") ? "2|" : "";
                                    strDias += _strDias[2].ToString().Equals("1") ? "3|" : "";
                                    strDias += _strDias[3].ToString().Equals("1") ? "4|" : "";
                                    strDias += _strDias[4].ToString().Equals("1") ? "5|" : "";
                                    strDias += _strDias[5].ToString().Equals("1") ? "6|" : "";
                                    strDias = (!string.IsNullOrEmpty(strDias)) ? strDias.Substring(0, strDias.Length - 1) : strDias;

                                    //------------------------------

                                    objEnt.Nid_serv = Convert.ToInt32(f1["nid_servicio"].ToString());
                                    objEnt.no_dias = strDias;
                                    objEnt.co_usuario = Profile.UserName;
                                    objEnt.co_usuario_red = Profile.UsuarioRed;
                                    objEnt.no_estacion_red = Profile.Estacion;
                                    objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    objEnt.Op = "I";
                                    objNeg.MantenimientoTallerServicios(objEnt);
                                    //insert
                                }
                                else
                                {
                                    foreach (DataRow f2 in dtServActual.Rows)
                                    {
                                        if (f1["nid_servicio"].ToString() == f2["nid_servicio"].ToString())
                                        {
                                            flag = true;
                                            break;
                                        }
                                        else
                                            flag = false;
                                    }
                                    if (flag)
                                    {
                                        //objEnt.Nid_serv = Convert.ToInt32(f1["nid_servicio"].ToString());

                                        //objEnt.Co_usuario_modi = "";
                                        //objEnt.co_usuario_red = Profile.UsuarioRed;
                                        //objEnt.no_estacion_red = Profile.Estacion; 
                                        //objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                        //objEnt.Op = "U";
                                        //objNeg.MantenimientoTallerServicios(objEnt);

                                        //update
                                    }
                                    else
                                    {
                                        string _strDias = f1["no_dias"].ToString();
                                        string strDias = string.Empty;
                                        strDias += _strDias[0].ToString().Equals("1") ? "1|" : "";
                                        strDias += _strDias[1].ToString().Equals("1") ? "2|" : "";
                                        strDias += _strDias[2].ToString().Equals("1") ? "3|" : "";
                                        strDias += _strDias[3].ToString().Equals("1") ? "4|" : "";
                                        strDias += _strDias[4].ToString().Equals("1") ? "5|" : "";
                                        strDias += _strDias[5].ToString().Equals("1") ? "6|" : "";
                                        strDias = (!string.IsNullOrEmpty(strDias)) ? strDias.Substring(0, strDias.Length - 1) : strDias;

                                        //---------------------------------------
                                        objEnt.Nid_serv = Convert.ToInt32(f1["nid_servicio"].ToString());
                                        objEnt.no_dias = strDias;
                                        objEnt.co_usuario = Profile.UserName;
                                        objEnt.co_usuario_red = Profile.UsuarioRed;
                                        objEnt.no_estacion_red = Profile.Estacion;
                                        objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                        objEnt.Op = "I";
                                        objNeg.MantenimientoTallerServicios(objEnt);
                                        //insert
                                    }
                                }
                            }
                        }
                        else if (dtServActual.Rows.Count == dtServSelec.Rows.Count)
                        {
                            Int32 index = 0;

                            foreach (DataRow f1 in dtServActual.Rows)
                            {
                                index = 0;
                                foreach (DataRow f2 in dtServSelec.Rows)
                                {
                                    if (f1["nid_servicio"].ToString() == f2["nid_servicio"].ToString())
                                    {
                                        flag = true;
                                        break;
                                    }
                                    else
                                    {
                                        flag = false;
                                    }
                                    index += 1;
                                }
                                if (flag)
                                {
                                    string _strDias = dtServSelec.Rows[index]["no_dias"].ToString();
                                    string strDias = string.Empty;
                                    strDias += _strDias[0].ToString().Equals("1") ? "1|" : "";
                                    strDias += _strDias[1].ToString().Equals("1") ? "2|" : "";
                                    strDias += _strDias[2].ToString().Equals("1") ? "3|" : "";
                                    strDias += _strDias[3].ToString().Equals("1") ? "4|" : "";
                                    strDias += _strDias[4].ToString().Equals("1") ? "5|" : "";
                                    strDias += _strDias[5].ToString().Equals("1") ? "6|" : "";
                                    strDias = (!string.IsNullOrEmpty(strDias)) ? strDias.Substring(0, strDias.Length - 1) : strDias;

                                    //---------------------------------------

                                    objEnt.Nid_serv = Convert.ToInt32(f1["nid_servicio"].ToString());
                                    objEnt.no_dias = strDias;

                                    objEnt.Co_usuario_modi = "";
                                    objEnt.co_usuario_red = Profile.UsuarioRed;
                                    objEnt.no_estacion_red = Profile.Estacion;
                                    objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    objEnt.Op = "U";
                                    objNeg.MantenimientoTallerServicios(objEnt);

                                    //update
                                }
                                else
                                {
                                    objEnt.Nid_serv = Convert.ToInt32(f1["nid_servicio"].ToString());
                                    objEnt.Op = "D";
                                    objNeg.MantenimientoTallerServicios(objEnt);
                                    //delete
                                }
                            }
                            foreach (DataRow f1 in dtServSelec.Rows)
                            {
                                foreach (DataRow f2 in dtServActual.Rows)
                                {
                                    if (f1["nid_servicio"].ToString() == f2["nid_servicio"].ToString())
                                    {
                                        flag = true;
                                        break;
                                    }
                                    else
                                        flag = false;
                                }
                                if (flag)
                                {
                                    //objEnt.Nid_serv = Convert.ToInt32(f1["nid_servicio"].ToString());
                                    //objEnt.Co_usuario_modi = "";
                                    //objEnt.co_usuario_red = Profile.UsuarioRed;
                                    //objEnt.no_estacion_red = Profile.Estacion; 
                                    //objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    //objEnt.Op = "U";
                                    //objNeg.MantenimientoTallerServicios(objEnt);

                                    //update
                                }
                                else
                                {
                                    string _strDias = f1["no_dias"].ToString();
                                    string strDias = string.Empty;
                                    strDias += _strDias[0].ToString().Equals("1") ? "1|" : "";
                                    strDias += _strDias[1].ToString().Equals("1") ? "2|" : "";
                                    strDias += _strDias[2].ToString().Equals("1") ? "3|" : "";
                                    strDias += _strDias[3].ToString().Equals("1") ? "4|" : "";
                                    strDias += _strDias[4].ToString().Equals("1") ? "5|" : "";
                                    strDias += _strDias[5].ToString().Equals("1") ? "6|" : "";
                                    strDias = (!string.IsNullOrEmpty(strDias)) ? strDias.Substring(0, strDias.Length - 1) : strDias;

                                    //---------------------------------------

                                    objEnt.Nid_serv = Convert.ToInt32(f1["nid_servicio"].ToString());
                                    objEnt.no_dias = strDias;
                                    objEnt.co_usuario = Profile.UserName;
                                    objEnt.co_usuario_red = Profile.UsuarioRed;
                                    objEnt.no_estacion_red = Profile.Estacion;
                                    objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    objEnt.Op = "I";
                                    objNeg.MantenimientoTallerServicios(objEnt);
                                    //insert
                                }
                            }
                        }
                    }
                    #endregion

                    #region ACTUALIZAR MODELOS

                    System.Data.DataTable dtModeloSelec = ((System.Data.DataTable)ViewState["dtModelosSel"]);
                    System.Data.DataTable dtModeloActual = ((System.Data.DataTable)ViewState["dtModelosSel_Edit"]);
                    Boolean flag2 = true;

                    if (Session["editar"] != null)
                        objEnt.nid_taller = Convert.ToInt32(Session["editar"].ToString());
                    else if (Session["detalle"] != null)
                        objEnt.nid_taller = Convert.ToInt32(Session["detalle"].ToString());

                    if (dtModeloActual != null && dtModeloSelec != null)
                    {
                        if (dtModeloActual.Rows.Count > dtModeloSelec.Rows.Count)
                        {
                            foreach (DataRow f1 in dtModeloActual.Rows)
                            {
                                if (dtModeloSelec.Rows.Count == 0)
                                {
                                    objEnt.Op = "D";
                                    objEnt.Nid_modelo = Convert.ToInt32(f1["nid_modelo"].ToString());
                                    objNeg.MantenimientoTallerModelos(objEnt);

                                    //delete taller-modelo-capacidad
                                }
                                else
                                {
                                    foreach (DataRow f2 in dtModeloSelec.Rows)
                                    {
                                        if (f1["nid_modelo"].ToString() == f2["nid_modelo"].ToString())
                                        {
                                            flag2 = true;
                                            break;
                                        }
                                        else
                                            flag2 = false;
                                    }
                                    if (flag2)
                                    {
                                        //objEnt.Nid_modelo = Convert.ToInt32(f1["nid_modelo"].ToString());
                                        //objEnt.Co_usuario_modi = "";
                                        //objEnt.co_usuario_red = Profile.UsuarioRed;
                                        //objEnt.no_estacion_red = Profile.Estacion; 
                                        //objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                        //objEnt.Op = "U";
                                        //objNeg.MantenimientoTallerModelos(objEnt);

                                        //update
                                    }
                                    else
                                    {
                                        objEnt.Op = "D";
                                        objEnt.Nid_modelo = Convert.ToInt32(f1["nid_modelo"].ToString());
                                        objNeg.MantenimientoTallerModelos(objEnt);
                                        //delete
                                    }
                                }
                            }
                        }
                        else if (dtModeloActual.Rows.Count < dtModeloSelec.Rows.Count)
                        {
                            foreach (DataRow f1 in dtModeloSelec.Rows)
                            {
                                if (dtModeloActual.Rows.Count == 0)
                                {
                                    objEnt.Nid_modelo = Convert.ToInt32(f1["nid_modelo"].ToString());
                                    objEnt.co_usuario = Profile.UserName;
                                    objEnt.co_usuario_red = Profile.UsuarioRed;
                                    objEnt.no_estacion_red = Profile.Estacion;
                                    objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    objEnt.Op = "I";
                                    objNeg.MantenimientoTallerModelos(objEnt);

                                    //insert-> Capacidad taller-modelo //06092012
                                    string strCapacidad = f1["qt_capacidad"].ToString();
                                    foreach (string sCapacidad in strCapacidad.Split('|'))
                                    {
                                        if (!String.IsNullOrEmpty(sCapacidad.Split('-').GetValue(1).ToString().Trim().Replace("0", "")))
                                        {
                                            objEnt.Dd_atencion = Int32.Parse(sCapacidad.Split('-').GetValue(0).ToString());
                                            objEnt.qt_capacidad = Int32.Parse(sCapacidad.Split('-').GetValue(1).ToString());

                                            Int32 oResp = objNeg.InsertarTallerModeloCapacidad(objEnt);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (DataRow f2 in dtModeloActual.Rows)
                                    {
                                        if (f1["nid_modelo"].ToString() == f2["nid_modelo"].ToString())
                                        {
                                            flag2 = true;
                                            break;
                                        }
                                        else
                                            flag2 = false;
                                    }
                                    if (flag2)
                                    {
                                        //objEnt.Nid_modelo = Convert.ToInt32(f1["nid_modelo"].ToString());
                                        //objEnt.Co_usuario_modi = "";
                                        //objEnt.co_usuario_red = Profile.UsuarioRed;
                                        //objEnt.no_estacion_red = Profile.Estacion; 
                                        //objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                        //objEnt.Op = "U";
                                        //objNeg.MantenimientoTallerModelos(objEnt);
                                        //update
                                    }
                                    else
                                    {
                                        objEnt.Nid_modelo = Convert.ToInt32(f1["nid_modelo"].ToString());
                                        objEnt.co_usuario = Profile.UserName;
                                        objEnt.co_usuario_red = Profile.UsuarioRed;
                                        objEnt.no_estacion_red = Profile.Estacion;
                                        objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                        objEnt.Op = "I";
                                        objNeg.MantenimientoTallerModelos(objEnt);

                                        //insert-> Capacidad taller-modelo //06092012
                                        string strCapacidad = f1["qt_capacidad"].ToString();
                                        foreach (string sCapacidad in strCapacidad.Split('|'))
                                        {
                                            if (String.IsNullOrEmpty(sCapacidad)) continue;

                                            if (!String.IsNullOrEmpty(sCapacidad.Split('-').GetValue(1).ToString().Trim().Replace("0", "")))
                                            {
                                                objEnt.Dd_atencion = Int32.Parse(sCapacidad.Split('-').GetValue(0).ToString());
                                                objEnt.qt_capacidad = Int32.Parse(sCapacidad.Split('-').GetValue(1).ToString());

                                                Int32 oResp = objNeg.InsertarTallerModeloCapacidad(objEnt);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (dtModeloActual.Rows.Count == dtModeloSelec.Rows.Count)
                        {
                            foreach (DataRow f1 in dtModeloActual.Rows)
                            {
                                Int32 index = 0;
                                foreach (DataRow f2 in dtModeloSelec.Rows)
                                {
                                    if (f1["nid_modelo"].ToString() == f2["nid_modelo"].ToString())
                                    {
                                        flag2 = true;
                                        break;
                                    }
                                    else
                                        flag2 = false;

                                    index += 1;
                                }
                                if (flag2)
                                {
                                    /* 06092012*/
                                    objEnt.Nid_modelo = Convert.ToInt32(f1["nid_modelo"].ToString());
                                    objEnt.co_usuario = Profile.UserName;
                                    objEnt.co_usuario_red = Profile.UsuarioRed;
                                    objEnt.no_estacion_red = Profile.Estacion;
                                    objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    objEnt.Op = "U";
                                    objNeg.MantenimientoTallerModelos(objEnt);

                                    //update
                                    //update-> Capacidad taller-modelo //06092012
                                    string strCapacidad = dtModeloSelec.Rows[index]["qt_capacidad"].ToString();
                                    foreach (string sCapacidad in strCapacidad.Split('|'))
                                    {
                                        if (!String.IsNullOrEmpty(sCapacidad.Split('-').GetValue(1).ToString().Trim().Replace("0", "")))
                                        {
                                            objEnt.Dd_atencion = Int32.Parse(sCapacidad.Split('-').GetValue(0).ToString());
                                            objEnt.qt_capacidad = Int32.Parse(sCapacidad.Split('-').GetValue(1).ToString());

                                            Int32 oResp = objNeg.MantenimientoTallerModelosCapacidad(objEnt);
                                        }
                                    }
                                }
                                else
                                {
                                    objEnt.Nid_modelo = Convert.ToInt32(f1["nid_modelo"].ToString());
                                    objEnt.Op = "D";
                                    objNeg.MantenimientoTallerModelos(objEnt);
                                    //delete
                                }
                            }
                            foreach (DataRow f1 in dtModeloSelec.Rows)
                            {
                                foreach (DataRow f2 in dtModeloActual.Rows)
                                {
                                    if (f1["nid_modelo"].ToString() == f2["nid_modelo"].ToString())
                                    {
                                        flag2 = true;
                                        break;
                                    }
                                    else
                                        flag2 = false;
                                }
                                if (flag2)
                                {
                                    //objEnt.Nid_modelo = Convert.ToInt32(f1["nid_modelo"].ToString());
                                    //objEnt.Co_usuario_modi = "";
                                    //objEnt.co_usuario_red = Profile.UsuarioRed;
                                    //objEnt.no_estacion_red = Profile.Estacion; 
                                    //objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    //objEnt.Op = "U";
                                    //objNeg.MantenimientoTallerModelos(objEnt);

                                    //update
                                }
                                else
                                {
                                    objEnt.Nid_modelo = Convert.ToInt32(f1["nid_modelo"].ToString());
                                    objEnt.co_usuario = Profile.UserName;
                                    objEnt.co_usuario_red = Profile.UsuarioRed;
                                    objEnt.no_estacion_red = Profile.Estacion;
                                    objEnt.fl_activo = (ddl_estado.SelectedIndex != 0 ? ddl_estado.SelectedValue : "");
                                    objEnt.Op = "I";
                                    objNeg.MantenimientoTallerModelos(objEnt);

                                    //insert-> Capacidad taller-modelo //06092012
                                    string strCapacidad = f1["qt_capacidad"].ToString();
                                    foreach (string sCapacidad in strCapacidad.Split('|'))
                                    {
                                        if (!String.IsNullOrEmpty(sCapacidad.Split('-').GetValue(1).ToString().Trim().Replace("0", "")))
                                        {
                                            objEnt.Dd_atencion = Int32.Parse(sCapacidad.Split('-').GetValue(0).ToString());
                                            objEnt.qt_capacidad = Int32.Parse(sCapacidad.Split('-').GetValue(1).ToString());

                                            Int32 oResp = objNeg.InsertarTallerModeloCapacidad(objEnt);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Session["nuevo"] = null; Session["editar"] = null; Session["detalle"] = null;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('El registro se actualizó con exito.');</script>", false);
                    lbl_mensajebox.Text = "El registro se actualizo con exito.";
                    popup_msgbox_confirm.Show();
                    #endregion
                }
            }
            #endregion
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Session["nuevo"] = null;
        Session["editar"] = null;
        Session["detalle"] = null;
        Response.Redirect("SRC_Maestro_Talleres.aspx");
    }

    private void cargarRangoHorasDefecto(ref DropDownList ddlHoras, String strHoraInicio, String strHoraFin)
    {
        DateTime dtHoraIni = Convert.ToDateTime(strHoraInicio);
        DateTime dtHoraFin = Convert.ToDateTime(strHoraFin);

        ParametrosBackOfficeBE objEntHorDef = new ParametrosBackOfficeBE();
        ParametrosBackOffieBL objNegHorDef = new ParametrosBackOffieBL();
        objEntHorDef = objNegHorDef.GetHorarioXDefecto();

        int v_intminutos = int.Parse(objEntHorDef.IntervaloTime.ToString().Trim());

        ddlHoras.DataSource = objNegHorDef.GetHorasSRC();
        ddlHoras.DataTextField = "DES";
        ddlHoras.DataValueField = "ID";
        ddlHoras.DataBind();

        if (ddlHoras.ID.ToUpper().Contains("DDL_HORAINICIO"))
        {
            ddlHoras.SelectedIndex = 0;
            if (objNegHorDef.GetHorasSRC().Count > 0)
            {
                ddlHoras.SelectedValue = objEntHorDef.HoraInicio;
            }
        }
        if (ddlHoras.ID.ToUpper().Contains("DDL_HORAFIN"))
        {
            ddlHoras.SelectedIndex = ddlHoras.Items.Count - 1;
            if (objNegHorDef.GetHorasSRC().Count > 0)
            {
                ddlHoras.SelectedValue = objEntHorDef.HoraFinal;
            }
        }
    }

    private void CargarProvinciaPorDepartamento()
    {
        txt_direccion.Text = String.Empty;
        if (ddl_dpto.SelectedIndex != 0)
        {
            DataRow[] oRow = ((System.Data.DataTable)ViewState["dtubigeo"]).Select("codprov <> '00' AND coddist='00' AND coddpto='" + ddl_dpto.SelectedValue + "'", "nombre", DataViewRowState.CurrentRows);
            ddl_prov.Items.Clear();
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_prov.Items.Add("");
                ddl_prov.Items[i].Value = oRow[i]["codprov"].ToString();
                ddl_prov.Items[i].Text = oRow[i]["nombre"].ToString();
            }
            ddl_prov.AutoPostBack = true;
            ddl_prov.Enabled = true;
        }
        else
            ddl_prov.Enabled = false;

        ddl_prov.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddl_prov.SelectedIndex = 0;

        ddl_dist.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddl_dist.SelectedIndex = 0;
        ddl_dist.Enabled = false;

        ddl_ptored.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddl_ptored.SelectedIndex = 0;
        ddl_ptored.Enabled = false;
    }

    protected void ddl_dpto_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarProvinciaPorDepartamento();
    }

    private void CargarDistritoPorProvincia()
    {
        if (ddl_prov.SelectedIndex != 0)
        {
            DataRow[] oRow = ((System.Data.DataTable)ViewState["dtubigeo"]).Select("codprov <> '00' AND coddist <> '00' AND coddpto='" + ddl_dpto.SelectedValue + "' AND codprov='" + ddl_prov.SelectedValue + "'", "nombre", DataViewRowState.CurrentRows);
            ddl_dist.Items.Clear();
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_dist.Items.Add("");
                ddl_dist.Items[i].Value = oRow[i]["coddist"].ToString();
                ddl_dist.Items[i].Text = oRow[i]["nombre"].ToString();
            }
            ddl_dist.Enabled = true;
        }
        else
            ddl_dist.Enabled = false;
        ddl_dist.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddl_dist.SelectedIndex = 0;

        ddl_ptored.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddl_ptored.SelectedIndex = 0;
        ddl_ptored.Enabled = false;

        txt_direccion.Text = String.Empty;
    }

    protected void ddl_prov_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDistritoPorProvincia();
    }

    private void CargarPuntoRedPorDistrito()
    {
        if (ddl_dist.SelectedIndex != 0)
        {
            DataRow[] oRow = ((System.Data.DataTable)ViewState["dtubicacion"]).Select("coddpto='" + ddl_dpto.SelectedValue + "' AND codprov='" + ddl_prov.SelectedValue + "' AND coddist='" + ddl_dist.SelectedValue + "'", "no_ubica", DataViewRowState.CurrentRows);
            ddl_ptored.Items.Clear();
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_ptored.Items.Add("");
                ddl_ptored.Items[i].Value = oRow[i]["nid_ubica"].ToString();
                ddl_ptored.Items[i].Text = oRow[i]["no_ubica"].ToString();
            }

        }
        ddl_ptored.Items.Insert(0, "--Seleccione--");
        ddl_ptored.SelectedIndex = 0;
        if (ddl_ptored.Items.Count > 1)
            ddl_ptored.Enabled = true;
        else
            ddl_ptored.Enabled = false;

        txt_direccion.Text = String.Empty;

    }

    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarPuntoRedPorDistrito();
    }

    protected void ddl_tiposerv_SelectedIndexChanged(object sender, EventArgs e)
    {
        lst_servdisp.Items.Clear();
        if (ddl_tiposerv.SelectedIndex != 0)
        {
            Int32 nid_tiposerv = Convert.ToInt32(ddl_tiposerv.SelectedValue);
            DataRow[] oRow = ((System.Data.DataTable)ViewState["Serv"]).Select("nid_tipo_servicio=" + nid_tiposerv + " AND existe='0'", "", DataViewRowState.CurrentRows);
            for (int i = 0; i < oRow.Length; i++)
            {
                lst_servdisp.Items.Add("");
                lst_servdisp.Items[i].Value = oRow[i]["nid_servicio"].ToString();
                lst_servdisp.Items[i].Text = oRow[i]["no_servicio"].ToString();
            }
        }
    }
    protected void ddl_marca_SelectedIndexChanged(object sender, EventArgs e)
    {
        lst_moddisp.Items.Clear();
        if (ddl_marca.SelectedIndex != 0)
        {
            Int32 nid_marca = Convert.ToInt32(ddl_marca.SelectedValue);
            DataRow[] oRow = ((System.Data.DataTable)ViewState["Modelo"]).Select("nid_marca=" + nid_marca + " AND existe = '0'", "no_modelo", DataViewRowState.CurrentRows);
            for (int i = 0; i < oRow.Length; i++)
            {
                lst_moddisp.Items.Add("");
                lst_moddisp.Items[i].Value = oRow[i]["nid_modelo"].ToString();
                lst_moddisp.Items[i].Text = oRow[i]["no_modelo"].ToString();
            }
        }
    }

    protected void btn_addmod_Click(object sender, ImageClickEventArgs e)
    {
        try
        {  
            if (ddl_marca.SelectedIndex == 0)
                return;
            if (lst_moddisp.SelectedIndex == -1)
                return;

            GuardarValoresCapacidad_TallerModelo();

            System.Data.DataTable dt1 = (System.Data.DataTable)ViewState["dtModelosSel"];
            System.Data.DataTable dtModelos = (System.Data.DataTable)ViewState["Modelo"];

            for (Int32 i = 0; i < dt1.Rows.Count; i++)
            {
                if ((Convert.ToInt32(dt1.Rows[i]["nid_marca"].ToString())) == Convert.ToInt32(ddl_marca.SelectedValue) && (Convert.ToInt32(dt1.Rows[i]["nid_modelo"].ToString())) == Convert.ToInt32(lst_moddisp.SelectedValue))
                    return;
            }
            String value = String.Empty;
            for (Int32 i = 0; i < lst_moddisp.Items.Count; i++)
            {
                if (lst_moddisp.Items[i].Selected == true)
                {
                    dt1.Rows.Add(
                        Convert.ToInt32(ddl_marca.SelectedValue),
                        ddl_marca.Items[ddl_marca.SelectedIndex].Text,
                        Convert.ToInt32(lst_moddisp.Items[i].Value),
                        lst_moddisp.Items[i].Text,
                        "1-|2-|3-|4-|5-|6-|7-|"
                                );
                    DataRow[] fila = dtModelos.Select("nid_modelo = " + Convert.ToInt32(lst_moddisp.Items[i].Value));
                    fila[0]["existe"] = "1";
                    value = value + lst_moddisp.Items[i].Value + "|";
                }
            }
            if (value.Length > 0)
            {
                value = value.Substring(0, value.Length - 1);
                for (Int32 i = 0; i < value.Split('|').Length; i++)
                {
                    for (Int32 j = 0; j < lst_moddisp.Items.Count; j++)
                    {
                        if (lst_moddisp.Items[j].Value == value.Split('|')[i].ToString())
                        {
                            lst_moddisp.Items.RemoveAt(j);
                            break;
                        }
                    }
                }
            }

            ViewState["Modelo"] = dtModelos;
            ViewState["dtModelosSel"] = dt1;

            if (dt1.Rows.Count == 0)
            {
                LlenarVacio_2();
            }
            else
            {
                gd_modsel.DataSource = dt1;
                gd_modsel.DataBind();

                EstablecerValoresCapacidad_TallerModelo();

            }

            dt1 = null;
        }
        catch
        {
            
        }
    }
    protected void btn_delmod_Click(object sender, ImageClickEventArgs e)
    {
        if (gd_modsel.Rows.Count == 0)
            return;

        GuardarValoresCapacidad_TallerModelo();

        System.Data.DataTable dt = (System.Data.DataTable)ViewState["dtModelosSel"];
        System.Data.DataTable dtModelos = (System.Data.DataTable)ViewState["Modelo"];
        Int32 indice = 0;

        Int32 intIdMarca = 0;
        string strMarca = string.Empty;
        Int32 intId = 0;

        String value = String.Empty;
        for (Int32 i = 0; i < gd_modsel.Rows.Count; i++)
        {
            indice = (gd_modsel.PageIndex * gd_modsel.PageSize) + i;
            CheckBox chk = (CheckBox)gd_modsel.Rows[i].FindControl("CheckBox2");
            if (chk.Checked == true)
            {
                lst_moddisp.Items.Add("");
                lst_moddisp.Items[lst_moddisp.Items.Count - 1].Value = dt.Rows[indice]["nid_modelo"].ToString();
                lst_moddisp.Items[lst_moddisp.Items.Count - 1].Text = dt.Rows[indice]["no_modelo"].ToString();

                intId = Convert.ToInt32(dt.Rows[indice]["nid_marca"].ToString());
                if (intIdMarca == 0)
                {
                    intIdMarca = intId;
                    strMarca = dt.Rows[indice]["no_marca"].ToString();
                }
                else if (intIdMarca != intId)
                {
                    intIdMarca = 0;
                    strMarca = "";
                }
                else
                {
                    intIdMarca = intId;
                }



                DataRow[] fila = dtModelos.Select("nid_modelo = " + Convert.ToInt32(dt.Rows[indice]["nid_modelo"].ToString()));
                fila[0]["existe"] = "0";
                value = value + dt.Rows[indice]["nid_modelo"].ToString() + "|";
            }
        }
        if (value.Length > 0)
        {
            value = value.Substring(0, value.Length - 1);
            for (Int32 i = 0; i < value.Split('|').Length; i++)
            {
                for (Int32 j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["nid_modelo"].ToString() == value.Split('|')[i].ToString())
                    {
                        dt.Rows.RemoveAt(j);
                        break;
                    }
                }
            }
        }

        ViewState["Modelo"] = dtModelos;
        ViewState["dtModelosSel"] = dt;

        if (dt.Rows.Count == 0)
        {
            LlenarVacio_2();
        }
        else
        {
            gd_modsel.DataSource = dt;
            gd_modsel.DataBind();

            EstablecerValoresCapacidad_TallerModelo();
        }

        dt = null;

		
        if (intIdMarca != 0)
        {

            // la marca seleccioanda del Modelo

            for (int i = 0; i < ddl_marca.Items.Count; i++)
            {
                if (ddl_marca.Items[i].Text.ToUpper().Equals(strMarca.Trim().ToUpper()))
                {
                    ddl_marca.SelectedIndex = i;
                    ddl_marca_SelectedIndexChanged(this, null);
                }
            }
        }
        else
        {
            //Todas las Marcas

            lst_moddisp.Items.Clear();
            ddl_marca.SelectedIndex = 0;
            ddl_marca_SelectedIndexChanged(this, null);
        }


    }
    
    protected void GuardarValoresCapacidad_TallerModelo()
    {
        //Guardar valores actuales del CheckBoxList [Antes de Paginar]

        DataTable dtMod = (DataTable)ViewState["dtModelosSel"];

        if (dtMod.Rows.Count == 0) return;

        Int32 indexDT = gd_modsel.PageIndex * 5;

        string strCapacidad = string.Empty;

        foreach (GridViewRow row in gd_modsel.Rows)
        {
            foreach (ListItem item in lst_DiasDisp.Items)
            {
                ((TextBox)row.Cells[3].FindControl("txtDia" + item.Value + "")).Text = "";
                
                row.Cells[3].FindControl("TDT" + item.Value + "").Visible = false;
                row.Cells[3].FindControl("TDL" + item.Value + "").Visible = false;
            }

            strCapacidad = string.Empty;

            strCapacidad += "1-" + ((row.Cells[3].FindControl("TDT1").Visible) ? ((TextBox)row.Cells[3].FindControl("txtDia1")).Text : "") + "|";
            strCapacidad += "2-" + ((row.Cells[3].FindControl("TDT2").Visible) ? ((TextBox)row.Cells[3].FindControl("txtDia2")).Text : "") + "|";
            strCapacidad += "3-" + ((row.Cells[3].FindControl("TDT3").Visible) ? ((TextBox)row.Cells[3].FindControl("txtDia3")).Text : "") + "|";
            strCapacidad += "4-" + ((row.Cells[3].FindControl("TDT4").Visible) ? ((TextBox)row.Cells[3].FindControl("txtDia4")).Text : "") + "|";
            strCapacidad += "5-" + ((row.Cells[3].FindControl("TDT5").Visible) ? ((TextBox)row.Cells[3].FindControl("txtDia5")).Text : "") + "|";
            strCapacidad += "6-" + ((row.Cells[3].FindControl("TDT6").Visible) ? ((TextBox)row.Cells[3].FindControl("txtDia6")).Text : "") + "|";
            strCapacidad += "7-" + ((row.Cells[3].FindControl("TDT7").Visible) ? ((TextBox)row.Cells[3].FindControl("txtDia7")).Text : "") + "";

            dtMod.Rows[indexDT]["qt_capacidad"] = strCapacidad;

            indexDT += 1;
        }

        ViewState["dtModelosSel"] = dtMod;
    }    
    protected void EstablecerValoresCapacidad_TallerModelo()
    {
        //Establecer valores guardados del CheckBoxList [Despues de Paginar]
        //--------------------------------------------------------------------

        DataTable dtMod = (DataTable)ViewState["dtModelosSel"];

        if (dtMod.Rows.Count == 0) return;

        Int32 indexDT = gd_modsel.PageIndex * 5;

        foreach (GridViewRow row in gd_modsel.Rows)
        {
            string strCapacidad = dtMod.Rows[indexDT]["qt_capacidad"].ToString();

            foreach (ListItem item in lst_DiasDisp.Items)
            {
                //((TextBox)row.Cells[3].FindControl("txtDia" + item.Value + "")).Text = "";

                row.Cells[3].FindControl("TDT" + item.Value + "").Visible = false;
                row.Cells[3].FindControl("TDL" + item.Value + "").Visible = false;
            }

            if (row.Cells[3].FindControl("TDT1").Visible) ((TextBox)row.Cells[3].FindControl("txtDia1")).Text = strCapacidad.Split('|').GetValue(0).ToString().Split('-').GetValue(1).ToString();
            if (row.Cells[3].FindControl("TDT2").Visible) ((TextBox)row.Cells[3].FindControl("txtDia2")).Text = strCapacidad.Split('|').GetValue(1).ToString().Split('-').GetValue(1).ToString();
            if (row.Cells[3].FindControl("TDT3").Visible) ((TextBox)row.Cells[3].FindControl("txtDia3")).Text = strCapacidad.Split('|').GetValue(2).ToString().Split('-').GetValue(1).ToString();
            if (row.Cells[3].FindControl("TDT4").Visible) ((TextBox)row.Cells[3].FindControl("txtDia4")).Text = strCapacidad.Split('|').GetValue(3).ToString().Split('-').GetValue(1).ToString();
            if (row.Cells[3].FindControl("TDT5").Visible) ((TextBox)row.Cells[3].FindControl("txtDia5")).Text = strCapacidad.Split('|').GetValue(4).ToString().Split('-').GetValue(1).ToString();
            if (row.Cells[3].FindControl("TDT6").Visible) ((TextBox)row.Cells[3].FindControl("txtDia6")).Text = strCapacidad.Split('|').GetValue(5).ToString().Split('-').GetValue(1).ToString();
            if (row.Cells[3].FindControl("TDT7").Visible) ((TextBox)row.Cells[3].FindControl("txtDia7")).Text = strCapacidad.Split('|').GetValue(6).ToString().Split('-').GetValue(1).ToString();

            indexDT += 1;

        }
    }
    
    protected void btn_addserv_Click(object sender, ImageClickEventArgs e)
    {
        if (ddl_tiposerv.SelectedIndex == 0)
        {
            return;
        }
        if (lst_servdisp.SelectedIndex == -1)
        {
            return;
        }

        GuardarValoresDias();

        System.Data.DataTable dt1 = (System.Data.DataTable)ViewState["dtServiciosSel"];
        System.Data.DataTable dtServ = (System.Data.DataTable)ViewState["Serv"];

        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            if ((Convert.ToInt32(dt1.Rows[i]["nid_tipo_servicio"].ToString())) == Convert.ToInt32(ddl_tiposerv.SelectedValue) && (Convert.ToInt32(dt1.Rows[i]["nid_servicio"].ToString())) == Convert.ToInt32(lst_servdisp.SelectedValue))
                return;
        }
        String value = String.Empty;
        for (Int32 i = 0; i < lst_servdisp.Items.Count; i++)
        {
            if (lst_servdisp.Items[i].Selected == true)
            {
                dt1.Rows.Add(
                    Convert.ToInt32(ddl_tiposerv.SelectedValue),
                    ddl_tiposerv.Items[ddl_tiposerv.SelectedIndex].Text,
                    Convert.ToInt32(lst_servdisp.Items[i].Value),
                    lst_servdisp.Items[i].Text,
                    "111111"
                             );
                DataRow[] fila = dtServ.Select("nid_servicio = " + Convert.ToInt32(lst_servdisp.Items[i].Value));
                fila[0]["existe"] = "1";
                value = value + lst_servdisp.Items[i].Value + "|";
            }
        }
        if (value.Length > 0)
        {
            value = value.Substring(0, value.Length - 1);
            for (Int32 i = 0; i < value.Split('|').Length; i++)
            {
                for (Int32 j = 0; j < lst_servdisp.Items.Count; j++)
                {
                    if (lst_servdisp.Items[j].Value == value.Split('|')[i].ToString())
                    {
                        lst_servdisp.Items.RemoveAt(j);
                        break;
                    }
                }
            }
        }

        ViewState["Serv"] = dtServ;
        ViewState["dtServiciosSel"] = dt1;

        if (dt1.Rows.Count == 0)
        {
            LlenarVacio();
        }
        else
        {
            gd_servsel.DataSource = dt1;
            gd_servsel.DataBind();
            EstablecerValoresDias();
        }
    }
    protected void btn_delserv_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresDias();

        System.Data.DataTable dt = (System.Data.DataTable)ViewState["dtServiciosSel"];
        System.Data.DataTable dtServ = (System.Data.DataTable)ViewState["Serv"];
        String value = String.Empty;
        Int32 indice = 0;

        for (Int32 i = 0; i < gd_servsel.Rows.Count; i++)
        {
            indice = (gd_servsel.PageIndex * gd_servsel.PageSize) + i;
            CheckBox chk = (CheckBox)gd_servsel.Rows[i].FindControl("CheckBox1");
            if (chk.Checked == true)
            {
                lst_servdisp.Items.Add("");
                lst_servdisp.Items[lst_servdisp.Items.Count - 1].Value = dt.Rows[indice]["nid_servicio"].ToString();
                lst_servdisp.Items[lst_servdisp.Items.Count - 1].Text = dt.Rows[indice]["no_servicio"].ToString();

                value = value + dt.Rows[indice]["nid_servicio"].ToString() + "|";
                DataRow[] fila = dtServ.Select("nid_servicio = " + Convert.ToInt32(dt.Rows[indice]["nid_servicio"].ToString()));
                fila[0]["existe"] = "0";
            }
        }

        if (value.Length > 0)
        {
            value = value.Substring(0, value.Length - 1);
            for (Int32 i = 0; i < value.Split('|').Length; i++)
            {
                for (Int32 j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["nid_servicio"].ToString() == value.Split('|')[i].ToString())
                    {
                        dt.Rows.RemoveAt(j);
                        break;
                    }
                }
            }
        }

        ViewState["Serv"] = dtServ;
        ViewState["dtServiciosSel"] = dt;
        
        if (dt.Rows.Count == 0)
        {
            LlenarVacio();
        }
        else
        {
            gd_servsel.DataSource = dt;
            gd_servsel.DataBind();
            EstablecerValoresDias();
        }

        dt = null;
    }

    protected void GuardarValoresDias()
    {
        //Guardar valores actuales del CheckBoxList [Antes de Paginar]

        DataTable dtServ = (DataTable)ViewState["dtServiciosSel"];

        if (dtServ.Rows.Count == 0) return;

        Int32 indexDT = gd_servsel.PageIndex * 5;

        string strDias = string.Empty;

        foreach (GridViewRow row in gd_servsel.Rows)
        {
            CheckBoxList chkDias = (CheckBoxList)row.Cells[3].FindControl("chkDias");

            strDias = string.Empty;

            strDias += chkDias.Items[0].Selected ? "1" : "0";
            strDias += chkDias.Items[1].Selected ? "1" : "0";
            strDias += chkDias.Items[2].Selected ? "1" : "0";
            strDias += chkDias.Items[3].Selected ? "1" : "0";
            strDias += chkDias.Items[4].Selected ? "1" : "0";
            strDias += chkDias.Items[5].Selected ? "1" : "0";

            dtServ.Rows[indexDT]["no_dias"] = strDias;

            indexDT += 1;
        }

        ViewState["dtServiciosSel"] = dtServ;
    }
    protected void EstablecerValoresDias()
    {
        //Establecer valores guardados del CheckBoxList [Despues de Paginar]

        DataTable dtServ = (DataTable)ViewState["dtServiciosSel"];

        Int32 indexDT = gd_servsel.PageIndex * 5;

        foreach (GridViewRow row in gd_servsel.Rows)
        {
            string strDias = dtServ.Rows[indexDT]["no_dias"].ToString();

            CheckBoxList chkDias = (CheckBoxList)row.Cells[3].FindControl("chkDias");

            chkDias.Items[0].Selected = strDias[0].ToString().Equals("1");
            chkDias.Items[1].Selected = strDias[1].ToString().Equals("1");
            chkDias.Items[2].Selected = strDias[2].ToString().Equals("1");
            chkDias.Items[3].Selected = strDias[3].ToString().Equals("1");
            chkDias.Items[4].Selected = strDias[4].ToString().Equals("1");
            chkDias.Items[5].Selected = strDias[5].ToString().Equals("1");

            indexDT += 1;
        }
    }

    protected void gd_servsel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GuardarValoresDias();

        //----------------------------------------------------------------
        gd_servsel.PageIndex = e.NewPageIndex;
        gd_servsel.DataSource = (System.Data.DataTable)ViewState["dtServiciosSel"];
        gd_servsel.DataBind();
        //----------------------------------------------------------------

        EstablecerValoresDias();       
    }
    protected void gd_modsel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GuardarValoresCapacidad_TallerModelo();

        gd_modsel.PageIndex = e.NewPageIndex;
        gd_modsel.DataSource = (System.Data.DataTable)ViewState["dtModelosSel"];
        gd_modsel.DataBind();

        EstablecerValoresCapacidad_TallerModelo();
    }

    protected void btn_SubirMapa_Click(object sender, ImageClickEventArgs e)
    {        
        imgMapa.ImageUrl = null;
        string strFile = string.Empty;

        if (this.FileUpload1.PostedFile != null && this.FileUpload1.PostedFile.ContentLength > 0)
        {
            if (System.IO.Directory.Exists(Server.MapPath("./Mapas")) == false)
                System.IO.Directory.CreateDirectory(Server.MapPath("./Mapas"));

            String fecha = String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now);
            fecha = fecha.Split(' ')[0].Replace("-", "_") + "_" + fecha.Split(' ')[1].Replace(":", "_");

            strFile = FileUpload1.PostedFile.FileName;


            this.FileUpload1.SaveAs(Server.MapPath("./Mapas/" + txt_codtall.Text.Trim() + "_" + fecha + System.IO.Path.GetExtension(strFile)));
            if (Session["editar"] != null || Session["detalle"] != null)
            {
                if (ViewState["mapa"].ToString() != "")
                {
                    if (ViewState["mapa"].ToString() != System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName))
                        System.IO.File.Delete(Server.MapPath("./Mapas/" + ViewState["mapa"]));
                }
            }
            //lblMensaje.Visible = true;
            lblMensaje.Text = "Mapa Subido con Exito" +
            "<br />" + "Tamaño del Mapa: " + this.FileUpload1.PostedFile.ContentLength.ToString() + " KB" +
            "<br />" + "Nombre del Mapa: " + txt_codtall.Text.Trim() + "_" + fecha + System.IO.Path.GetExtension(strFile) +
            "<br />" + "Tipo MIME: " + FileUpload1.PostedFile.ContentType;

            imgMapa.ImageUrl = @"Mapas/" + txt_codtall.Text.Trim() + "_" + fecha + System.IO.Path.GetExtension(strFile);
            ViewState["mapa"] = txt_codtall.Text.Trim() + "_" + fecha + System.IO.Path.GetExtension(strFile);
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOnForm('1');</script>", false);
    }

    protected void btn_adddhab_Click(object sender, ImageClickEventArgs e)
    {
        if (lst_DiasDisp.Items.Count == 0 || lst_DiasDisp.SelectedIndex == -1)
            return;

        //lst_DiasHab_SelectedIndexChanged(this, null);

        GuardarValoresCapacidad_TallerModelo();

        lst_DiasHab.Items.Add("");
        lst_DiasHab.Items[lst_DiasHab.Items.Count - 1].Text = lst_DiasDisp.Items[lst_DiasDisp.SelectedIndex].Text;
        lst_DiasHab.Items[lst_DiasHab.Items.Count - 1].Value = lst_DiasDisp.Items[lst_DiasDisp.SelectedIndex].Value;
        
        if (Session["nuevo"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["dtHorario"];
            DataRow[] fila = dt.Select("dd_atencion = " + Convert.ToInt32(lst_DiasDisp.Items[lst_DiasDisp.SelectedIndex].Value));
            fila[0]["habil"] = "1";
            ViewState["dtHorario"] = dt;
            dt = null;
        }
        else if (Session["editar"] != null || Session["detalle"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["dtHorario_sel"];
            DataRow[] fila = dt.Select("dd_atencion = " + Convert.ToInt32(lst_DiasDisp.Items[lst_DiasDisp.SelectedIndex].Value));
            fila[0]["habil"] = "1";
            ViewState["dtHorario_sel"] = dt;
            dt = null;
        }
        lst_DiasDisp.Items.RemoveAt(lst_DiasDisp.SelectedIndex);

        string strTmpCapacidad = string.Empty;
        string[] strCapacidaDias;
        bool blnExiste = false;

        strCapacidaDias = hf_Capacidad.Value.ToString().Split('|');

        for (int i = 0; i < lst_DiasHab.Items.Count; i++)
        {
            blnExiste = false;

            foreach (string strCapacidadDia in strCapacidaDias)
            {
                if (strCapacidadDia.Trim().Length == 0) continue;

                string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                string strC_Tipo = strCapacidadDia.Split('-').GetValue(1).ToString();
                string strC_FO = strCapacidadDia.Split('-').GetValue(2).ToString();
                string strC_BO = strCapacidadDia.Split('-').GetValue(3).ToString();
                string strC_Total = strCapacidadDia.Split('-').GetValue(4).ToString();

                if (lst_DiasHab.Items[i].Value.Equals(strC_Dia))
                {
                    strTmpCapacidad += strCapacidadDia + "|";
                    blnExiste = true;
                    break;
                }
            }

            // --> Si no existe el dia se le incluye
            if (!blnExiste)
                strTmpCapacidad += lst_DiasHab.Items[i].Value.ToString() + "----|";   // ADD DIA
        }

        //------------------------------------------------------------------------

        if (strTmpCapacidad.Trim().Length > 0) strTmpCapacidad = strTmpCapacidad.Substring(0, strTmpCapacidad.Length - 1);

        hf_Capacidad.Value = strTmpCapacidad;

        EstablecerValoresCapacidad_TallerModelo();
    }
    protected void btn_delhab_Click(object sender, ImageClickEventArgs e)
    {
        if (lst_DiasHab.Items.Count == 0 || lst_DiasHab.SelectedIndex == -1)
            return;

        GuardarValoresCapacidad_TallerModelo();

        lst_DiasDisp.Items.Add("");
        lst_DiasDisp.Items[lst_DiasDisp.Items.Count - 1].Text = lst_DiasHab.Items[lst_DiasHab.SelectedIndex].Text;
        lst_DiasDisp.Items[lst_DiasDisp.Items.Count - 1].Value = lst_DiasHab.Items[lst_DiasHab.SelectedIndex].Value;
        if (Session["nuevo"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["dtHorario"];
            DataRow[] fila = dt.Select("dd_atencion = " + Convert.ToInt32(lst_DiasHab.Items[lst_DiasHab.SelectedIndex].Value));
            fila[0]["habil"] = "0";
            ViewState["dtHorario"] = dt;
            dt = null;
        }
        else if (Session["editar"] != null || Session["detalle"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["dtHorario_sel"];
            DataRow[] fila = dt.Select("dd_atencion = " + Convert.ToInt32(lst_DiasHab.Items[lst_DiasHab.SelectedIndex].Value));
            fila[0]["habil"] = "0";
            ViewState["dtHorario_sel"] = dt;
            dt = null;
        }
        lst_DiasHab.Items.RemoveAt(lst_DiasHab.SelectedIndex);
        
        // --- CT -->> REMOVE

        string strTmpCapacidad = string.Empty;
        string[] strCapacidaDias;

        strCapacidaDias = hf_Capacidad.Value.ToString().Split('|');

        foreach (string strCapacidadDia in strCapacidaDias)
        {
            if (strCapacidadDia.Trim().Length == 0) continue;

            string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
            string strC_Tipo = strCapacidadDia.Split('-').GetValue(1).ToString();
            string strC_FO = strCapacidadDia.Split('-').GetValue(2).ToString();
            string strC_BO = strCapacidadDia.Split('-').GetValue(3).ToString();
            string strC_Total = strCapacidadDia.Split('-').GetValue(4).ToString();

            if (lst_DiasHab.Items.IndexOf(lst_DiasHab.Items.FindByValue(strC_Dia)) != -1)
            {
                strTmpCapacidad += strCapacidadDia + "|";
            }
        }

        if (strTmpCapacidad.Trim().Length > 0) strTmpCapacidad = strTmpCapacidad.Substring(0, strTmpCapacidad.Length - 1);

        hf_Capacidad.Value = strTmpCapacidad;

        if (lst_DiasHab.Items.Count > 0)
        {
            lst_DiasHab.SelectedIndex = 0;
            lst_DiasHab_SelectedIndexChanged(this, null);
        }
        else
        {
            txt_dia.Text = "";
            txt_capacidad_fo.Text = "";
            txt_capacidad_bo.Text = "";
            txt_capacidad.Text = "";
            txt_capacidad_fo.Enabled = false;
            txt_capacidad_bo.Enabled = false;
            txt_capacidad.Enabled = false;
        }

        // --- CT --<<

        EstablecerValoresCapacidad_TallerModelo();

    }

    protected void btn_adddiasexc_Click(object sender, ImageClickEventArgs e)
    {
        if (cal_DiasExcep.SelectedDate == DateTime.MinValue)
            return;
        for (Int32 i = 0; i < lst_DiasExcep.Items.Count; i++)
        {
            if ((String.Format("{0:MMM}", cal_DiasExcep.SelectedDate) + " " + String.Format("{0:dd}", cal_DiasExcep.SelectedDate)) == (lst_DiasExcep.Items[i].Text))
                return;
        }

        lst_DiasExcep.Items.Add("");
        lst_DiasExcep.Items[lst_DiasExcep.Items.Count - 1].Text = String.Format("{0:MMM}", cal_DiasExcep.SelectedDate) + " " + String.Format("{0:dd}", cal_DiasExcep.SelectedDate);
        lst_DiasExcep.Items[lst_DiasExcep.Items.Count - 1].Value = cal_DiasExcep.SelectedDate.ToString();
    }
    protected void btn_deldiasexc_Click(object sender, ImageClickEventArgs e)
    {
        if (lst_DiasExcep.Items.Count == 0)
            return;
        else if (lst_DiasExcep.SelectedIndex == -1)
            return;
        String value = String.Empty;
        for (Int32 i = 0; i < lst_DiasExcep.Items.Count; i++)
        {
            if (lst_DiasExcep.Items[i].Selected == true)
                value = value + lst_DiasExcep.Items[i].Value + ",";
        }
        //
        //---para ordenamiento
        //Int32 cant = (Int32)ViewState["array_long"];
        //DateTime[] array_exc = new DateTime[0];
        //array_exc = (DateTime[])ViewState["array_exc"];
        //---para ordenamiento
        //
        if (value.Length > 0)
        {
            value = value.Substring(0, value.Length - 1);
            for (Int32 i = 0; i < value.Split(',').Length; i++)
            {
                for (Int32 j = 0; j < lst_DiasExcep.Items.Count; j++)
                {
                    if (lst_DiasExcep.Items[j].Value == value.Split(',')[i].ToString())
                    {
                        //---para ordenamiento
                        //cant--;                        
                        //Array.Resize(ref array_exc, cant);
                        //---para ordenamiento
                        lst_DiasExcep.Items.RemoveAt(j);
                        break;
                    }
                }
            }
        }
        //---para ordenamiento
        //ViewState["array_long"] = cant;
        //ViewState["array_exc"] = array_exc;
        //---para ordenamiento
    }

    protected void ddl_ptored_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_ptored.SelectedIndex != 0)
        {
            objEnt.nid_ubica = Convert.ToInt32(ddl_ptored.SelectedValue.ToString());
            MostrarDetallePorPuntoRed(objEnt);
        }
    }

    protected void ddl_horafin_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["nuevo"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["dtHorario"];
            if (lst_DiasHab.SelectedIndex != -1)
            {
                ViewState["hora_defecto_nuevo"] = "0";
                DataRow[] fila = dt.Select("dd_atencion = " + Convert.ToInt32(lst_DiasHab.SelectedValue));
                fila[0]["ho_inicio"] = ddl_horainicio.Text;
                fila[0]["ho_fin"] = ddl_horafin.Text;
                ViewState["dtHorario"] = dt;
            }
            else
            {
                if (ViewState["hora_defecto_nuevo"].ToString() == "1")
                {
                    foreach (DataRow fila in dt.Rows)
                    {
                        fila["ho_inicio"] = ddl_horainicio.Text;
                        fila["ho_fin"] = ddl_horafin.Text;
                    }
                    ViewState["dtHorario"] = dt;
                }
            }
            dt = null;
        }
        else if (Session["editar"] != null || Session["detalle"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["dtHorario_sel"];
            if (lst_DiasHab.SelectedIndex != -1)
            {
                ViewState["hora_defecto_editar"] = "0";
                DataRow[] fila = dt.Select("dd_atencion = " + Convert.ToInt32(lst_DiasHab.SelectedValue));
                fila[0]["ho_inicio"] = ddl_horainicio.Text;
                fila[0]["ho_fin"] = ddl_horafin.Text;
                ViewState["dtHorario_sel"] = dt;
            }
            else
            {
                if (ViewState["hora_defecto_editar"].ToString() == "1")
                {
                    foreach (DataRow fila in dt.Rows)
                    {
                        fila["ho_inicio"] = ddl_horainicio.Text;
                        fila["ho_fin"] = ddl_horafin.Text;
                    }
                    ViewState["dtHorario_sel"] = dt;
                }
            }
            dt = null;
        }
    }

    protected void ddl_horainicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["nuevo"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["dtHorario"];
            if (lst_DiasHab.SelectedIndex != -1)
            {
                ViewState["hora_defecto_nuevo"] = "0";
                DataRow[] fila = dt.Select("dd_atencion = " + Convert.ToInt32(lst_DiasHab.SelectedValue));
                fila[0]["ho_inicio"] = ddl_horainicio.Text;
                fila[0]["ho_fin"] = ddl_horafin.Text;
                ViewState["dtHorario"] = dt;
            }
            else
            {
                if (ViewState["hora_defecto_nuevo"].ToString() == "1")
                {
                    foreach (DataRow fila in dt.Rows)
                    {
                        fila["ho_inicio"] = ddl_horainicio.Text;
                        fila["ho_fin"] = ddl_horafin.Text;
                    }
                    ViewState["dtHorario"] = dt;
                }
            }
            dt = null;
        }
        else if (Session["editar"] != null || Session["detalle"] != null)
        {
            System.Data.DataTable dt = (System.Data.DataTable)ViewState["dtHorario_sel"];
            if (lst_DiasHab.SelectedIndex != -1)
            {
                ViewState["hora_defecto_editar"] = "0";
                DataRow[] fila = dt.Select("dd_atencion = " + Convert.ToInt32(lst_DiasHab.SelectedValue));
                fila[0]["ho_inicio"] = ddl_horainicio.Text;
                fila[0]["ho_fin"] = ddl_horafin.Text;
                ViewState["dtHorario_sel"] = dt;
                dt = null;
            }
            else
            {
                if (ViewState["hora_defecto_editar"].ToString() == "1")
                {
                    foreach (DataRow fila in dt.Rows)
                    {
                        fila["ho_inicio"] = ddl_horainicio.Text;
                        fila["ho_fin"] = ddl_horafin.Text;
                    }
                    ViewState["dtHorario_sel"] = dt;
                }
            }
            dt = null;
        }
    }
    protected void btn_msgboxconfir_no_Click(object sender, EventArgs e)
    {
        if (ViewState["existe_codigo"].ToString() == "0")
            Response.Redirect("SRC_Maestro_Talleres.aspx");
        popup_msgbox_confirm.Hide();
    }
    protected void lst_DiasHab_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["nuevo"] != null)
        {
            if (lst_DiasHab.SelectedIndex == -1) return;
            txt_dia.Text = lst_DiasHab.Items[lst_DiasHab.SelectedIndex].Text;
            System.Data.DataTable dtHoraNuevo = (System.Data.DataTable)ViewState["dtHorario"];
            DataRow[] fila = dtHoraNuevo.Select("dd_atencion = " + Convert.ToInt32(lst_DiasHab.Items[lst_DiasHab.SelectedIndex].Value));
            ddl_horainicio.Text = fila[0]["ho_inicio"].ToString();
            ddl_horafin.Text = fila[0]["ho_fin"].ToString();
            dtHoraNuevo = null;

        }
        else if (Session["editar"] != null || Session["detalle"] != null)
        {
            txt_dia.Text = lst_DiasHab.Items[lst_DiasHab.SelectedIndex].Text;
            System.Data.DataTable dtHoraEditar = (System.Data.DataTable)ViewState["dtHorario_sel"];
            DataRow[] fila = dtHoraEditar.Select("dd_atencion = " + Convert.ToInt32(lst_DiasHab.Items[lst_DiasHab.SelectedIndex].Value));
            try
            {
                ddl_horainicio.Text = fila[0]["ho_inicio"].ToString();
            }
            catch (Exception)
            {
                ddl_horainicio.SelectedIndex = 0;
                lbl_alerta_msj.Text = "- Hora Desde no esta disponible en el maestro de horas SRC.(" + fila[0]["ho_inicio"].ToString() + ")";
                popup_alerta_msj.Show();
            }
            try
            {
                ddl_horafin.Text = fila[0]["ho_fin"].ToString();
            }
            catch (Exception)
            {
                ddl_horafin.SelectedIndex = 0;
                lbl_alerta_msj.Text = "- Hora Hasta no esta disponible en el maestro de horas SRC.(" + fila[0]["ho_fin"].ToString() + ")";
                popup_alerta_msj.Show();
            }
            dtHoraEditar = null;
        }

        //CT -- 09.05.2011  -->>

        string strDiaValue = string.Empty;
        string strTmpCapacidad = string.Empty;
        string[] strCapacidaDias;


        if (lst_DiasHab.SelectedIndex >= 0)
        {
            strDiaValue = hf_DiaValue.Value.ToString();
            strCapacidaDias = hf_Capacidad.Value.ToString().Split('|');

            //--- SAVE

            foreach (string strCapacidadDia in strCapacidaDias)
            {
                if (strCapacidadDia.Trim().Length == 0) continue;

                string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                string strC_Tipo = strCapacidadDia.Split('-').GetValue(1).ToString();
                string strC_FO = strCapacidadDia.Split('-').GetValue(2).ToString();
                string strC_BO = strCapacidadDia.Split('-').GetValue(3).ToString();
                string strC_Total = strCapacidadDia.Split('-').GetValue(4).ToString();
                
                if (strC_Dia.Equals(strDiaValue))
                {
                    strTmpCapacidad += strC_Dia + "-" + ( (this.chkI.Checked) ? "I": "T" ) + "-" + txt_capacidad_fo.Text.Trim() + "-" + txt_capacidad_bo.Text.Trim() + "-"+ txt_capacidad.Text.Trim() + "|";
                }
                else
                {
                    strTmpCapacidad += strCapacidadDia + "|";
                }
            }

            if (strTmpCapacidad.Trim().Length > 0) strTmpCapacidad = strTmpCapacidad.Substring(0, strTmpCapacidad.Length - 1);

            hf_Capacidad.Value = strTmpCapacidad;

            //--- SET

            strCapacidaDias = hf_Capacidad.Value.ToString().Split('|');

            foreach (string strCapacidadDia in strCapacidaDias)
            {
                if (strCapacidadDia.Trim().Length == 0) continue;

                string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                string strC_Tipo = strCapacidadDia.Split('-').GetValue(1).ToString();
                string strC_FO = strCapacidadDia.Split('-').GetValue(2).ToString();
                string strC_BO = strCapacidadDia.Split('-').GetValue(3).ToString();
                string strC_Total = strCapacidadDia.Split('-').GetValue(4).ToString();
                
                if (strC_Dia.Equals(lst_DiasHab.SelectedValue.ToString()))
                {
                    txt_capacidad_fo.Text = strC_FO;
                    txt_capacidad_bo.Text = strC_BO;
                    txt_capacidad.Text = strC_Total;

                    chkI.Checked = strC_Tipo.Equals("T") ? false : true;
                    chkT.Checked = strC_Tipo.Equals("T") ? true : false;

                    txt_capacidad_fo.Enabled = chkI.Checked;
                    txt_capacidad_bo.Enabled = chkI.Checked;
                    txt_capacidad.Enabled = chkT.Checked;
                }
            }

            hf_DiaValue.Value = lst_DiasHab.SelectedValue.ToString();

            txt_capacidad_fo.Enabled = (chkI.Enabled && chkI.Checked);
            txt_capacidad_bo.Enabled = (chkI.Enabled && chkI.Checked);
            txt_capacidad.Enabled = (chkT.Enabled && chkT.Checked);
        }
        else
        {
            txt_capacidad_fo.Enabled = false;
            txt_capacidad_bo.Enabled = false;
            txt_capacidad.Enabled = false;
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        this.btnEditar.Style.Add("display", "none");

        Habilitar_Controles(true);
        this.btnGrabar.Style.Add("display", "inline");
        //btnNuevo.Enabled = true;

        if (this.tabMantMaesTaller.ActiveTabIndex == 5)
        {
            this.btnBuscar.Style.Add("display", "inline");
            this.btnNuevo.Style.Add("display", "inline");
            this.btnGrabar.Style.Add("display", "none");
        }

        if (this.tabMantMaesTaller.ActiveTabIndex == 6)
        {
            this.btnGrabar.Style.Add("display", "none");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>CargarInicial();</script>", false);
        }

      
    }

    protected void btn_alertaconfir_aceptar_Click(object sender, EventArgs e)
    {
        popup_alerta_msj.Hide();
        if (hid_acc_val_hor_excep.Value.ToString().Length > 0)
        {
            popup_horexcepcional.Show();
            //ModalPopupExtender1.Show
        }
    }
    #endregion

    private static string _TODOS = "--Todos--";


    private void Estructura_DT_HorExcep()
    {
        DT_HorExcep = new System.Data.DataTable();
        DT_HorExcep.Columns.Add("grid_cod_hor_excep");
        DT_HorExcep.Columns.Add("grid_codTllr_hor_excep");
        DT_HorExcep.Columns.Add("grid_des_hor_excep");
        DT_HorExcep.Columns.Add("grid_fecini_hor_excep");
        DT_HorExcep.Columns.Add("grid_fecfin_hor_excep");
        DT_HorExcep.Columns.Add("grid_estado_hor_excep");
        DT_HorExcep.Columns.Add("grid_idestado_hor_excep");
    }
    private void Estructura_DT_HorExcepDet()
    {
        DT_HorExcepDet = new System.Data.DataTable();
        DT_HorExcepDet.Columns.Add("grid_cod_hor_excep");
        DT_HorExcepDet.Columns.Add("grid_cod_dia_hor_excep");
        DT_HorExcepDet.Columns.Add("grid_dia_hor_excep");
        DT_HorExcepDet.Columns.Add("grid_hor1ini_hor_excep");
        DT_HorExcepDet.Columns.Add("grid_hor1fin_hor_excep");
        DT_HorExcepDet.Columns.Add("grid_hor2ini_hor_excep");
        DT_HorExcepDet.Columns.Add("grid_hor2fin_hor_excep");
        DT_HorExcepDet.Columns.Add("grid_hor3ini_hor_excep");
        DT_HorExcepDet.Columns.Add("grid_hor3fin_hor_excep");
    }

    private void MaquetearHorExcep()
    {
        TallerHorariosExcepcionalBE ent = new TallerHorariosExcepcionalBE();
        ent.VI_nid_propietario = int.Parse(hid_id_tllr.Value.ToString());
        TallerHorariosExcepcionalBEList oDiasXTllr = new TallerHorariosExcepcionalBEList();
        oDiasXTllr = objNeg.GetListDiasXTllrHorarioExcepcional(ent);

        for (int i = 0; i < oDiasXTllr.Count; i++)
        {
            DataRow dr;
            dr = DT_HorExcepDet.NewRow();
            if (hid_acc_hor_excep.Value.ToString() == "N")
            {
                dr["grid_cod_hor_excep"] = "";
            }
            dr["grid_cod_dia_hor_excep"] = oDiasXTllr[i].VI_nid_propietario.ToString();
            dr["grid_dia_hor_excep"] = DevolverDia(oDiasXTllr[i].VI_nid_propietario);
            dr["grid_hor1ini_hor_excep"] = "";
            dr["grid_hor1fin_hor_excep"] = "";
            dr["grid_hor2ini_hor_excep"] = "";
            dr["grid_hor2fin_hor_excep"] = "";
            dr["grid_hor3ini_hor_excep"] = "";
            dr["grid_hor3fin_hor_excep"] = "";

            DT_HorExcepDet.Rows.Add(dr);
        }

    }

    private void CargarEstado(DropDownList ddlEstado, string IndOpcion)
    {
        ddlEstado.Items.Clear();
        if (IndOpcion.Equals("1"))
        {
            ddlEstado.Items.Add("");
            ddlEstado.Items[0].Text = "--Todos--";
            ddlEstado.Items[0].Value = "";
        }
        else
        {
            ddlEstado.Items.Add("");
            ddlEstado.Items[0].Text = "--Seleccione--";
            ddlEstado.Items[0].Value = "";
        }
        ddlEstado.Items.Add("");
        ddlEstado.Items[1].Text = "Activo";
        ddlEstado.Items[1].Value = "A";
        ddlEstado.Items.Add("");
        ddlEstado.Items[2].Text = "Inactivo";
        ddlEstado.Items[2].Value = "I";

        hid_acc_val_hor_excep.Value = "";
    }

    private void cargarGrid_HorExcep_Inicio()
    {
        cargarGrid_HorExcep();
        MostrarCabGrid(gv_HorExcep);
    }
    private void cargarGrid_HorExcep()
    {
        Estructura_DT_HorExcep();
        gv_HorExcep.DataSource = DT_HorExcep;
        gv_HorExcep.DataBind();
    }
    private void cargarGrid_HorExcepFiltro(TallerHorariosExcepcionalBEList olist)
    {
        gv_HorExcep.DataSource = olist;
        gv_HorExcep.DataBind();
    }
    private void cargarGrid_HorExcepMant(System.Data.DataTable DT)
    {
        gv_horexcep_mant.DataSource = DT;
        gv_horexcep_mant.DataBind();
    }

    private void llenarHorasHorExcep()
    {
        TallerHorariosExcepcionalBE ent = new TallerHorariosExcepcionalBE();
        ent.VI_nid_propietario = int.Parse(hid_id_tllr.Value.ToString());
        TallerHorariosExcepcionalBEList oLista = new TallerHorariosExcepcionalBEList();

        int dia = 0;

        string oTllrHorIniAten = "";
        string oTllrHorfinAten = "";
        string oTllrIntervaloAten = "";

        foreach (GridViewRow dr in gv_horexcep_mant.Rows)
        {
            dia = int.Parse(gv_horexcep_mant.DataKeys[dr.RowIndex].Values["grid_cod_dia_hor_excep"].ToString());

            ent.VI_dd_atencion = dia;
            oLista = objNeg.GetListHorXDiaXTllrHorarioExcepcional(ent);
            oTllrHorIniAten = oLista[0].TllrHorIniAten.ToString();
            oTllrHorfinAten = oLista[0].TllrHorfinAten.ToString();
            oTllrIntervaloAten = oLista[0].TllrIntervaloAten.ToString();

            DateTime oHorI = DateTime.Parse(oTllrHorIniAten);
            DateTime oHorF = DateTime.Parse(oTllrHorfinAten);
            int intIndex = 0;
            while (oHorI <= oHorF)
            {
                DropDownList dpHora;

                dpHora = new DropDownList();
                dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini")));

                dpHora.Items.Add("");
                dpHora.Items[intIndex].Text = oHorI.ToString("hh:mm");
                dpHora.Items[intIndex].Value = oHorI.ToString("HH:mm");

                dpHora = new DropDownList();
                dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin")));

                dpHora.Items.Add("");
                dpHora.Items[intIndex].Text = oHorI.ToString("hh:mm");
                dpHora.Items[intIndex].Value = oHorI.ToString("HH:mm");

                dpHora = new DropDownList();
                dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini")));

                dpHora.Items.Add("");
                dpHora.Items[intIndex].Text = oHorI.ToString("hh:mm");
                dpHora.Items[intIndex].Value = oHorI.ToString("HH:mm");

                dpHora = new DropDownList();
                dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin")));

                dpHora.Items.Add("");
                dpHora.Items[intIndex].Text = oHorI.ToString("hh:mm");
                dpHora.Items[intIndex].Value = oHorI.ToString("HH:mm");

                dpHora = new DropDownList();
                dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini")));

                dpHora.Items.Add("");
                dpHora.Items[intIndex].Text = oHorI.ToString("hh:mm");
                dpHora.Items[intIndex].Value = oHorI.ToString("HH:mm");

                dpHora = new DropDownList();
                dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin")));

                dpHora.Items.Add("");
                dpHora.Items[intIndex].Text = oHorI.ToString("hh:mm");
                dpHora.Items[intIndex].Value = oHorI.ToString("HH:mm");

                oHorI = oHorI.AddMinutes(double.Parse(oTllrIntervaloAten));

                intIndex++;
            }
        }

        foreach (GridViewRow dr in gv_horexcep_mant.Rows)
        {
            DropDownList dpHora;

            dpHora = new DropDownList();
            dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini")));
            dpHora.Items.Insert(0, new ListItem("LIBRE", ""));

            dpHora = new DropDownList();
            dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin")));
            dpHora.Items.Insert(0, new ListItem("LIBRE", ""));

            dpHora = new DropDownList();
            dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini")));
            dpHora.Items.Insert(0, new ListItem("LIBRE", ""));

            dpHora = new DropDownList();
            dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin")));
            dpHora.Items.Insert(0, new ListItem("LIBRE", ""));

            dpHora = new DropDownList();
            dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini")));
            dpHora.Items.Insert(0, new ListItem("LIBRE", ""));

            dpHora = new DropDownList();
            dpHora = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin")));
            dpHora.Items.Insert(0, new ListItem("LIBRE", ""));
        }
    }

    protected void MostrarCabGrid(GridView grdView)
    {
        if (grdView.Rows.Count == 0 &&
            grdView.DataSource != null)
        {
            System.Data.DataTable dt = null;

            if (grdView.DataSource is DataSet)
            {
                dt = ((DataSet)grdView.DataSource).Tables[0].Clone();
            }
            else if (grdView.DataSource is System.Data.DataTable)
            {
                dt = ((System.Data.DataTable)grdView.DataSource).Clone();
            }

            if (dt == null)
            {
                return;
            }

            dt.Rows.Add(dt.NewRow());
            grdView.DataSource = dt;
            grdView.DataBind();

            grdView.Rows[0].Visible = false;
            grdView.Rows[0].Controls.Clear();
        }

        if (grdView.Rows.Count == 1 &&
            grdView.DataSource == null)
        {
            bool bIsGridEmpty = true;

            for (int i = 0; i < grdView.Rows[0].Cells.Count; i++)
            {
                if (grdView.Rows[0].Cells[i].Text != string.Empty)
                {
                    bIsGridEmpty = false;
                }
            }
            if (bIsGridEmpty)
            {
                grdView.Rows[0].Visible = false;
                grdView.Rows[0].Controls.Clear();
            }
        }
    }

    protected void LimpiarCabeHorExcep()
    {
        txt_horexcep_des.Text = "";
        txt_horexcep_fini.Text = "";
        txt_horexcep_ffin.Text = "";
        ddl_horexcep_estado.SelectedIndex = 0;
    }

    protected string ValidarHorasGvMant()
    {
        string cad = "", cadTemp = "";
        int indVal = 0;
        foreach (GridViewRow dr in gv_horexcep_mant.Rows)
        {

            Int32 intDia = int.Parse(gv_horexcep_mant.DataKeys[dr.RowIndex].Values["grid_cod_dia_hor_excep"].ToString());

            if (!diaEstaEnRangoHE(txt_horexcep_des.Text.Trim(), txt_horexcep_ffin.Text.Trim(), intDia))
                continue;


            indVal = 0;
            cadTemp += "- Dia: " + intDia.ToString();

            //RANGO 1
            if ((((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString().Length == 0) || (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString().Length == 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString().Length > 0))
            {
                cadTemp += "<li>Horario 1: Rango incorrecto</li>";
                indVal += 1;
            }
            else
            {
                if ((((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString().Length > 0))
                {
                    if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) >= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()))
                    {
                        cadTemp += "<li>Horario 1: Hr. Inicio menor a Hr. Final</li>";
                        indVal += 1;
                    }
                    else
                    {
                        //Rango 1 Vs Rango 2
                        if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString().Length > 0)
                        {
                            if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()))
                            {
                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) >= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 1: Hr. Inicio incluido en Horario 2</li>";
                                    indVal += 1;
                                }

                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()) > DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()) <= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 1: Hr. Final incluido en Horario 2</li>";
                                    indVal += 1;
                                }
                            }
                        }

                        //Rango 1 Vs Rango 3
                        if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString().Length > 0)
                        {
                            if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()))
                            {
                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) >= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 1: Hr. Inicio incluido en Horario 3</li>";
                                    indVal += 1;
                                }

                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()) > DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()) <= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 1: Hr. Final incluido en Horario 3</li>";
                                    indVal += 1;
                                }
                            }
                        }
                    }
                }
            }

            //RANGO 2
            if ((((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString().Length == 0) || (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString().Length == 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString().Length > 0))
            {
                cadTemp += "<li>Horario 2: Rango incorrecto</li>";
                indVal += 1;
            }
            else
            {
                if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString().Length > 0)
                {
                    if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) >= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()))
                    {
                        cadTemp += "<li>Horario 2: Hr. Inicio menor a Hr. Final</li>";
                        indVal += 1;
                    }
                    else
                    {
                        //Rango 2 Vs Rango 1
                        if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString().Length > 0)
                        {
                            if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()))
                            {
                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) >= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 2: Hr. Inicio incluido en Horario 1</li>";
                                    indVal += 1;
                                }

                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()) > DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()) <= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 2: Hr. Final incluido en Horario 1</li>";
                                    indVal += 1;
                                }
                            }
                        }

                        //Rango 2 Vs Rango 3
                        if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString().Length > 0)
                        {
                            if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()))
                            {
                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) >= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 2: Hr. Inicio incluido en Horario 3</li>";
                                    indVal += 1;
                                }

                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()) > DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()) <= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 2: Hr. Final incluido en Horario 3</li>";
                                    indVal += 1;
                                }
                            }
                        }
                    }
                }
            }

            //RANGO 3
            if ((((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString().Length == 0) || (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString().Length == 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString().Length > 0))
            {
                cadTemp += "<li>Horario 3: Rango incorrecto</li>";
                indVal += 1;
            }
            else
            {
                if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString().Length > 0)
                {
                    if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) >= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()))
                    {
                        cadTemp += "<li>Horario 3: Hr. Inicio menor a Hr. Final</li>";
                        indVal += 1;
                    }
                    else
                    {
                        //Rango 3 Vs Rango 1
                        if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString().Length > 0)
                        {
                            if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()))
                            {
                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) >= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 3: Hr. Inicio incluido en Horario 1</li>";
                                    indVal += 1;
                                }

                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()) > DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()) <= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 3: Hr. Final incluido en Horario 1</li>";
                                    indVal += 1;
                                }
                            }
                        }

                        //Rango 3 Vs Rango 2
                        if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString().Length > 0)
                        {
                            if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()))
                            {
                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) >= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString()) < DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 3: Hr. Inicio incluido en Horario 2</li>";
                                    indVal += 1;
                                }

                                if (DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()) > DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString()) && DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString()) <= DateTime.Parse(((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString()))
                                {
                                    cadTemp += "<li>Horario 3: Hr. Final incluido en Horario 2</li>";
                                    indVal += 1;
                                }
                            }
                        }
                    }
                }
            }

            if (indVal == 0) { cadTemp = ""; }
            cad += cadTemp;
            cadTemp = "";
        }

        return cad;
    }

    public bool IsDate(object Expression)
    {
        if (Expression != null)
        {
            if (Expression is DateTime)
            {
                return true;
            }
            if (Expression is string)
            {
                try
                {
                    DateTime time1 = Convert.ToDateTime(Expression);
                    return true;
                }
                catch (Exception)
                {
                    // SRC_MsgError(ex.Message);
                }
            }
        }
        return false;
    }
    public long DateDiff(DateInterval interval, DateTime date1, DateTime date2)
    {
        long rs = 0;
        TimeSpan diff = date2.Subtract(date1);
        switch (interval)
        {
            case DateInterval.Day:
            case DateInterval.DayOfYear:
                rs = (long)diff.TotalDays;
                break;
            case DateInterval.Hour:
                rs = (long)diff.TotalHours;
                break;
            case DateInterval.Minute:
                rs = (long)diff.TotalMinutes;
                break;
            case DateInterval.Second:
                rs = (long)diff.TotalSeconds;
                break;
            case DateInterval.Weekday:
            case DateInterval.WeekOfYear:
                rs = (long)(diff.TotalDays / 7);
                break;
            case DateInterval.Year:
                rs = date2.Year - date1.Year;
                break;
        }//switch
        return rs;
    }
    
    protected string ValidarCabeHorExcep()
    {
        ParametrosBackOffieBL objBLT = new ParametrosBackOffieBL();

        Int32 indExit = 1;

        DateTime dtFechaHoy = Convert.ToDateTime(Convert.ToDateTime(objBLT.GetFechaActual()).ToString("dd/MM/yyyy"));

        string val = "";

        if (txt_horexcep_des.Text.Trim().Length == 0)
        {
            val += "- Falta Descripcion.<br />"; indExit = 0;
        }
        if (txt_horexcep_fini.Text.Trim().Length == 0)
        {
            val += "- Falta Fecha Inicio.<br />"; indExit = 0;
        }
        else if (!IsDate(txt_horexcep_fini.Text.Trim()))
        {
            val += "- Fecha de inicio no valida.<br />"; indExit = 0;
        }
        else if (DateDiff(DateInterval.Day, dtFechaHoy, Convert.ToDateTime(txt_horexcep_fini.Text.Trim())) < 1)
        {
            val += "- Fecha de inicio de ser mayor a la actual.<br />"; indExit = 0;
        }


        if (txt_horexcep_ffin.Text.Trim().Length == 0)
        {
            val += "- Falta Fecha Fin.<br />"; indExit = 0;
        }
        else if (!IsDate(txt_horexcep_ffin.Text.Trim()))
        {
            val += "- Fecha final no valida.<br />"; indExit = 0;
        }
        else if (DateDiff(DateInterval.Day, Convert.ToDateTime(txt_horexcep_fini.Text.Trim()), Convert.ToDateTime(txt_horexcep_ffin.Text.Trim())) < 0)
        {
            val += "- Fecha final debe ser mayor a la inicial.<br />"; indExit = 0;
        }


        if (ddl_horexcep_estado.SelectedIndex == 0)
        {
            val += "- Falta Estado.<br />";
        }

        if (indExit == 0) return val;

        int IndExisDet = 0;
        //int inthayDia = 0;
        int indDias = 0;
        string strDiasS = string.Empty;

        foreach (GridViewRow dr in gv_horexcep_mant.Rows)
        {
            Int32 intDia = int.Parse(gv_horexcep_mant.DataKeys[dr.RowIndex].Values["grid_cod_dia_hor_excep"].ToString());

            if (!diaEstaEnRangoHE(txt_horexcep_des.Text.Trim(), txt_horexcep_ffin.Text.Trim(), intDia))
                continue;

            strDiasS += DevolverDia(intDia) + ",";

            indDias += 1;

            //inthayDia += 1;

            if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString().Length > 0)
            {
                IndExisDet += 1;
            }

            if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString().Length > 0)
            {
                IndExisDet += 1;
            }

            if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString().Length > 0)
            {
                IndExisDet += 1;
            }
        }

        if (indDias == gv_horexcep_mant.Rows.Count) //Si el rango de fecha estan todos loa dias de la Semana del taller        
        {
            if (IndExisDet == 0)
            {
                val += "- Debe existir minimo un rango de horario.<br />";
            }
        }
        else  //Validar si se ingreso Rango de horarios olo en los dias que esteen en el rango de fechas
        {
            if (strDiasS.Length > 0) strDiasS = strDiasS.Substring(0, strDiasS.Length - 1);

            if (IndExisDet == 0)
            {
                val += (strDiasS.Length > 0) ? "- Debe ingresar los rangos de:  <br />" + strDiasS + ". <br />" : "";
            }
        }


        return val;
    }

    protected void btnRetornarHorExcep_Click(object sender, ImageClickEventArgs e)
    {
        popup_horexcepcional.Hide();
        hid_acc_hor_excep.Value = "N";

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOffForm('0');setTabCabeceraOffForm('1');setTabCabeceraOffForm('2');setTabCabeceraOffForm('3');setTabCabeceraOffForm('4');setTabCabeceraOnForm('5');</script>", false);


    }

    protected bool diaEstaEnRangoHE(string strFI, string strFF, Int32 intDia)
    {

        DateTime dtFI_HE = DateTime.Parse(txt_horexcep_fini.Text.Trim());
        DateTime dtFF_HE = DateTime.Parse(txt_horexcep_ffin.Text.Trim());

        while (dtFI_HE <= dtFF_HE)
        {
            Int32 intDiaS = 0;
            switch (dtFI_HE.DayOfWeek)
            {
                case DayOfWeek.Monday: intDiaS = 1; break;
                case DayOfWeek.Tuesday: intDiaS = 2; break;
                case DayOfWeek.Wednesday: intDiaS = 3; break;
                case DayOfWeek.Thursday: intDiaS = 4; break;
                case DayOfWeek.Friday: intDiaS = 5; break;
                case DayOfWeek.Saturday: intDiaS = 6; break;
                case DayOfWeek.Sunday: intDiaS = 7; break;
            }

            if (intDiaS.Equals(intDia)) return true;

            dtFI_HE = dtFI_HE.AddDays(1);
        }

        return false;
    }
    
    protected void btnGrabarHorExcep_Click(object sender, ImageClickEventArgs e)
    {
        string MsjCabeVal = ValidarCabeHorExcep();

        hid_acc_val_hor_excep.Value = (MsjCabeVal.ToString().Length > 0) ? "1" : ""; //MsjCabeVal;
        if (MsjCabeVal.Length == 0)
        {
            hid_acc_val_hor_excep.Value = "";
            string MsjVal = ValidarHorasGvMant();
            hid_acc_val_hor_excep.Value = (MsjVal.ToString().Length > 0) ? "1" : "";//MsjVal;
            if (hid_acc_hor_excep.Value.ToString().Equals("N"))
            {
                if (MsjVal.Length == 0)
                {
                    int IdHorExcep = 0;

                    TallerHorariosExcepcionalBE entCabe = new TallerHorariosExcepcionalBE();
                    entCabe.VI_nid_horario_HECabe = 0;
                    entCabe.VI_nid_propietario_HECabe = int.Parse(hid_id_tllr.Value.ToString());
                    entCabe.VI_no_descripcion_HECabe = txt_horexcep_des.Text.Trim();
                    entCabe.VI_fe_inicio_HECabe = txt_horexcep_fini.Text.Trim();
                    entCabe.VI_fe_fin_HECabe = txt_horexcep_ffin.Text.Trim();
                    entCabe.VI_fl_tipo_HECabe = "T";
                    entCabe.VI_co_usuario_crea_HECabe = Profile.UserName;
                    entCabe.VI_co_usuario_modi_HECabe = "";
                    entCabe.VI_co_usuario_red_HECabe = Profile.UsuarioRed;
                    entCabe.VI_no_estacion_red_HECabe = Profile.Estacion;
                    entCabe.VI_fl_activo_HECabe = ddl_horexcep_estado.SelectedValue.ToString();

                    IdHorExcep = objNeg.InsertarCabeHorExcepcional(entCabe);

                    if (IdHorExcep > 0)
                    {
                        int IndInsertDeta = 0;
                        foreach (GridViewRow dr in gv_horexcep_mant.Rows)
                        {
                            Int32 intDia = int.Parse(gv_horexcep_mant.DataKeys[dr.RowIndex].Values["grid_cod_dia_hor_excep"].ToString());

                            if (!diaEstaEnRangoHE(txt_horexcep_des.Text.Trim(), txt_horexcep_ffin.Text.Trim(), intDia))
                                continue;

                            string oho_rango1 = "";
                            if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString().Length > 0)
                            {
                                oho_rango1 = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString() + "|" + ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString();
                            }
                            else
                            {
                                oho_rango1 = "";
                            }

                            string oho_rango2 = "";
                            if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString().Length > 0)
                            {
                                oho_rango2 = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString() + "|" + ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString();
                            }
                            else
                            {
                                oho_rango2 = "";
                            }

                            string oho_rango3 = "";
                            if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString().Length > 0)
                            {
                                oho_rango3 = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString() + "|" + ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString();
                            }
                            else
                            {
                                oho_rango3 = "";
                            }

                            TallerHorariosExcepcionalBE entDeta = new TallerHorariosExcepcionalBE();
                            entDeta.VI_nid_horario_HEDeta = IdHorExcep;
                            entDeta.VI_dd_atencion_HEDeta = intDia; // int.Parse(gv_horexcep_mant.DataKeys[dr.RowIndex].Values["grid_cod_dia_hor_excep"].ToString());
                            entDeta.VI_ho_rango1_HEDeta = oho_rango1;
                            entDeta.VI_ho_rango2_HEDeta = oho_rango2;
                            entDeta.VI_ho_rango3_HEDeta = oho_rango3;
                            entDeta.VI_co_usuario_crea_HEDeta = Profile.UserName;
                            entDeta.VI_co_usuario_modi_HEDeta = "";
                            entDeta.VI_co_usuario_red_HEDeta = Profile.UsuarioRed;
                            entDeta.VI_no_estacion_red_HEDeta = Profile.Estacion;
                            entDeta.VI_fl_activo_HEDeta = ddl_horexcep_estado.SelectedValue.ToString();

                            int IndInsertarDetalle = 0;

                            if (oho_rango1.Length > 0 || oho_rango2.Length > 0 || oho_rango3.Length > 0)
                            {
                                IndInsertarDetalle = 1;
                            }
                            else
                            {
                                IndInsertarDetalle = 0;
                            }

                            if (IndInsertarDetalle > 0)
                            {
                                IndInsertDeta += objNeg.InsertarDetaHorExcepcional(entDeta);
                            }
                        }
                    }

                    if (IdHorExcep > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('Horario Exepcional guardado satisfactoriamente');</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('Error al Guardar');</script>", false);
                    }
                    popup_horexcepcional.Hide();
                    LimpiarCabeHorExcep();
                    hid_acc_hor_excep.Value = "N";
                    //btn_bus_excepBusHorExcep_Click(null, null);
                    btnBuscar_Click(null, null);

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOffForm('0');setTabCabeceraOffForm('1');setTabCabeceraOffForm('2');setTabCabeceraOffForm('3');setTabCabeceraOffForm('4');setTabCabeceraOnForm('5');</script>", false);
                
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('" + MsjVal + "');</script>", false);
                    lbl_alerta_msj.Text = MsjVal;
                    popup_alerta_msj.Show();
                    popup_horexcepcional.Show();
                }
            }
            else if (hid_acc_hor_excep.Value.ToString().Equals("M"))
            {
                if (MsjVal.Length == 0)
                {
                    int IndCabeHorExcep = 0;

                    TallerHorariosExcepcionalBE entCabe = new TallerHorariosExcepcionalBE();
                    entCabe.VI_nid_horario_HECabe = int.Parse(hid_id_HorExcep.Value.ToString());
                    entCabe.VI_nid_propietario_HECabe = int.Parse(hid_id_tllr.Value.ToString());
                    entCabe.VI_no_descripcion_HECabe = txt_horexcep_des.Text.Trim();
                    entCabe.VI_fe_inicio_HECabe = txt_horexcep_fini.Text.Trim();
                    entCabe.VI_fe_fin_HECabe = txt_horexcep_ffin.Text.Trim();
                    entCabe.VI_fl_tipo_HECabe = "T";
                    entCabe.VI_co_usuario_crea_HECabe = Profile.UserName;
                    entCabe.VI_co_usuario_modi_HECabe = "";
                    entCabe.VI_co_usuario_red_HECabe = Profile.UsuarioRed;
                    entCabe.VI_no_estacion_red_HECabe = Profile.Estacion;
                    entCabe.VI_fl_activo_HECabe = ddl_horexcep_estado.SelectedValue.ToString();

                    IndCabeHorExcep = objNeg.ActualizarCabeHorExcepcional(entCabe);

                    if (IndCabeHorExcep > 0)
                    {
                        int IndEliDeta = 0;
                        TallerHorariosExcepcionalBE entEliDetaHorExcep = new TallerHorariosExcepcionalBE();
                        entEliDetaHorExcep.VI_nid_horario_HEDeta = int.Parse(hid_id_HorExcep.Value.ToString());
                        entEliDetaHorExcep.VI_co_usuario_modi_HEDeta = Profile.UserName;
                        entEliDetaHorExcep.VI_co_usuario_red_HEDeta = Profile.UsuarioRed;
                        entEliDetaHorExcep.VI_no_estacion_red_HEDeta = Profile.Estacion;
                        entEliDetaHorExcep.VI_fl_activo_HEDeta = "I";
                        IndEliDeta = objNeg.EliminarDetaHorExcepcional(entEliDetaHorExcep);

                        if (IndEliDeta > 0)
                        {
                            int IndInsertDeta = 0;
                            foreach (GridViewRow dr in gv_horexcep_mant.Rows)
                            {
                                Int32 intDia = int.Parse(gv_horexcep_mant.DataKeys[dr.RowIndex].Values["grid_cod_dia_hor_excep"].ToString());

                                if (!diaEstaEnRangoHE(txt_horexcep_des.Text.Trim(), txt_horexcep_ffin.Text.Trim(), intDia))
                                    continue;


                                string oho_rango1 = "";
                                if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString().Length > 0)
                                {
                                    oho_rango1 = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue.ToString() + "|" + ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue.ToString();
                                }
                                else
                                {
                                    oho_rango1 = "";
                                }

                                string oho_rango2 = "";
                                if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString().Length > 0)
                                {
                                    oho_rango2 = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue.ToString() + "|" + ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue.ToString();
                                }
                                else
                                {
                                    oho_rango2 = "";
                                }

                                string oho_rango3 = "";
                                if (((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString().Length > 0 && ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString().Length > 0)
                                {
                                    oho_rango3 = ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue.ToString() + "|" + ((DropDownList)(gv_horexcep_mant.Rows[dr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue.ToString();
                                }
                                else
                                {
                                    oho_rango3 = "";
                                }

                                TallerHorariosExcepcionalBE entDeta = new TallerHorariosExcepcionalBE();
                                entDeta.VI_nid_horario_HEDeta = int.Parse(hid_id_HorExcep.Value.ToString());
                                entDeta.VI_dd_atencion_HEDeta = intDia; //int.Parse(gv_horexcep_mant.DataKeys[dr.RowIndex].Values["grid_cod_dia_hor_excep"].ToString());
                                entDeta.VI_ho_rango1_HEDeta = oho_rango1;
                                entDeta.VI_ho_rango2_HEDeta = oho_rango2;
                                entDeta.VI_ho_rango3_HEDeta = oho_rango3;
                                entDeta.VI_co_usuario_crea_HEDeta = Profile.UserName;
                                entDeta.VI_co_usuario_modi_HEDeta = "";
                                entDeta.VI_co_usuario_red_HEDeta = Profile.UsuarioRed;
                                entDeta.VI_no_estacion_red_HEDeta = Profile.Estacion;
                                entDeta.VI_fl_activo_HEDeta = ddl_horexcep_estado.SelectedValue.ToString();

                                int IndInsertarDetalle = 0;

                                if (oho_rango1.Length > 0 || oho_rango2.Length > 0 || oho_rango3.Length > 0)
                                {
                                    IndInsertarDetalle = 1;
                                }
                                else
                                {
                                    IndInsertarDetalle = 0;
                                }

                                if (IndInsertarDetalle > 0)
                                {
                                    IndInsertDeta += objNeg.InsertarDetaHorExcepcional(entDeta);
                                }
                            }
                        }
                    }

                    if (IndCabeHorExcep > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('Horario Exepcional actualizado satisfactoriamente');</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('Error al Actualizar');</script>", false);
                    }
                    LimpiarCabeHorExcep();
                    hid_acc_hor_excep.Value = "N";
                    //btn_bus_excepBusHorExcep_Click(null, null);
                    btnBuscar_Click(null, null);

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOffForm('0');setTabCabeceraOffForm('1');setTabCabeceraOffForm('2');setTabCabeceraOffForm('3');setTabCabeceraOffForm('4');setTabCabeceraOnForm('5');</script>", false);

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('" + MsjVal + "');</script>", false);
                    lbl_alerta_msj.Text = MsjVal;
                    popup_alerta_msj.Show();
                    popup_horexcepcional.Show();
                }
            }
        }
        else
        {
            lbl_alerta_msj.Text = MsjCabeVal;
            popup_alerta_msj.Show();
            popup_horexcepcional.Show();
        }
    }

    protected void gv_HorExcep_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string CmdName = e.CommandName.ToString();
        hid_id_HorExcep.Value = e.CommandArgument.ToString();
        if (CmdName.Equals("HorExcepModi"))
        {
            int IndModif = 0;
            hid_acc_hor_excep.Value = "M";

            TallerHorariosExcepcionalBE entModiCabe = new TallerHorariosExcepcionalBE();
            entModiCabe.VI_nid_horario_HECabe = int.Parse(hid_id_HorExcep.Value.ToString());
            TallerHorariosExcepcionalBEList oListEntModiCabe = new TallerHorariosExcepcionalBEList();
            oListEntModiCabe = objNeg.GetListHorarioExcepcionalXHorario(entModiCabe);
            if (oListEntModiCabe.Count > 0)
            {
                txt_horexcep_des.Text = oListEntModiCabe[0].grid_des_hor_excep.ToString().Trim();
                txt_horexcep_fini.Text = Convert.ToDateTime(oListEntModiCabe[0].grid_fecini_hor_excep.ToString().Trim()).ToShortDateString();
                txt_horexcep_ffin.Text = Convert.ToDateTime(oListEntModiCabe[0].grid_fecfin_hor_excep.ToString().Trim()).ToShortDateString();
                ddl_horexcep_estado.SelectedValue = oListEntModiCabe[0].grid_idestado_hor_excep.ToString().Trim();

                if (DateTime.Parse(oListEntModiCabe[0].grid_fecini_hor_excep.ToString().Trim()) > DateTime.Now)
                {
                    IndModif = 0;
                }
                else
                {
                    IndModif = 1;
                }
            }

            if (IndModif == 0)
            {
                Estructura_DT_HorExcepDet();
                MaquetearHorExcep();
                cargarGrid_HorExcepMant(DT_HorExcepDet);
                llenarHorasHorExcep();

                TallerHorariosExcepcionalBEList oListEntModiDeta = new TallerHorariosExcepcionalBEList();
                oListEntModiDeta = objNeg.GetListDetaHorarioExcepcionalXHorario(entModiCabe);
                if (oListEntModiDeta.Count > 0)
                {
                    for (int i = 0; i < oListEntModiDeta.Count; i++)
                    {
                        foreach (DataRow dr in DT_HorExcepDet.Rows)
                        {
                            dr["grid_cod_hor_excep"] = hid_id_HorExcep.Value.ToString();
                            if (dr["grid_cod_dia_hor_excep"].ToString().Trim().Equals(oListEntModiDeta[i].DetIdDia.ToString().Trim()))
                            {
                                string[] arrRango1 = new string[2];
                                if (oListEntModiDeta[i].DetHo_rango1.ToString().Trim().Length > 0)
                                {
                                    arrRango1 = oListEntModiDeta[i].DetHo_rango1.ToString().Trim().Split('|');
                                    dr["grid_hor1ini_hor_excep"] = arrRango1.GetValue(0).ToString().Trim();
                                    dr["grid_hor1fin_hor_excep"] = arrRango1.GetValue(1).ToString().Trim();
                                }
                                else
                                {
                                    dr["grid_hor1ini_hor_excep"] = "";
                                    dr["grid_hor1fin_hor_excep"] = "";
                                }

                                string[] arrRango2 = new string[2];
                                if (oListEntModiDeta[i].DetHo_rango2.ToString().Trim().Length > 0)
                                {
                                    arrRango2 = oListEntModiDeta[i].DetHo_rango2.ToString().Trim().Split('|');
                                    dr["grid_hor2ini_hor_excep"] = arrRango2.GetValue(0).ToString().Trim();
                                    dr["grid_hor2fin_hor_excep"] = arrRango2.GetValue(1).ToString().Trim();
                                }
                                else
                                {
                                    dr["grid_hor2ini_hor_excep"] = "";
                                    dr["grid_hor2fin_hor_excep"] = "";
                                }

                                string[] arrRango3 = new string[2];
                                if (oListEntModiDeta[i].DetHo_rango3.ToString().Trim().Length > 0)
                                {
                                    arrRango3 = oListEntModiDeta[i].DetHo_rango3.ToString().Trim().Split('|');
                                    dr["grid_hor3ini_hor_excep"] = arrRango3.GetValue(0).ToString().Trim();
                                    dr["grid_hor3fin_hor_excep"] = arrRango3.GetValue(1).ToString().Trim();
                                }
                                else
                                {
                                    dr["grid_hor3ini_hor_excep"] = "";
                                    dr["grid_hor3fin_hor_excep"] = "";
                                }

                                break;
                            }
                        }
                    }
                }

                foreach (GridViewRow gDr in gv_horexcep_mant.Rows)
                {
                    foreach (DataRow Dr in DT_HorExcepDet.Rows)
                    {
                        string dGV = "";
                        dGV = gv_horexcep_mant.DataKeys[gDr.RowIndex].Values["grid_cod_dia_hor_excep"].ToString().Trim();
                        String dTB = "";
                        dTB = Dr["grid_cod_dia_hor_excep"].ToString().Trim();
                        if (gv_horexcep_mant.DataKeys[gDr.RowIndex].Values["grid_cod_dia_hor_excep"].ToString().Trim().Equals(Dr["grid_cod_dia_hor_excep"].ToString().Trim()))
                        {
                            ((DropDownList)(gv_horexcep_mant.Rows[gDr.RowIndex].FindControl("ddl_hor1ini"))).SelectedValue = Dr["grid_hor1ini_hor_excep"].ToString().Trim();
                            ((DropDownList)(gv_horexcep_mant.Rows[gDr.RowIndex].FindControl("ddl_hor1fin"))).SelectedValue = Dr["grid_hor1fin_hor_excep"].ToString().Trim();

                            ((DropDownList)(gv_horexcep_mant.Rows[gDr.RowIndex].FindControl("ddl_hor2ini"))).SelectedValue = Dr["grid_hor2ini_hor_excep"].ToString().Trim();
                            ((DropDownList)(gv_horexcep_mant.Rows[gDr.RowIndex].FindControl("ddl_hor2fin"))).SelectedValue = Dr["grid_hor2fin_hor_excep"].ToString().Trim();

                            ((DropDownList)(gv_horexcep_mant.Rows[gDr.RowIndex].FindControl("ddl_hor3ini"))).SelectedValue = Dr["grid_hor3ini_hor_excep"].ToString().Trim();
                            ((DropDownList)(gv_horexcep_mant.Rows[gDr.RowIndex].FindControl("ddl_hor3fin"))).SelectedValue = Dr["grid_hor3fin_hor_excep"].ToString().Trim();

                            break;
                        }
                    }
                }
                popup_horexcepcional.Show();
            }
            else
            {
                LimpiarCabeHorExcep();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('Horario no se puede editar');</script>", false);
            }
        }
        else if (CmdName.Equals("HorExcepEli"))
        {

        }
    }    
    protected void gd_servsel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux=1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = gd_servsel.DataKeys[e.Row.RowIndex];

            if (dataKey.Values["no_tipo_servicio"] == null) aux = 0;
            //Int32.TryParse(dataKey.Values["no_tipo_servicio"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }
            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = "javascript: fc_SeleccionaFilaSimple(this);";
        }
    }
    protected void gd_modsel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Int32 aux = 1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataKey dataKey = gd_modsel.DataKeys[e.Row.RowIndex];

            if (dataKey.Values["no_marca"] == null) aux = 0;
            //Int32.TryParse(dataKey.Values["no_tipo_servicio"].ToString(), out aux);
            if (aux == 0)
            {
                e.Row.Visible = false;
                return;
            }
            e.Row.Style["cursor"] = "pointer";
            e.Row.Attributes["onclick"] = "javascript: fc_SeleccionaFilaSimple(this);";
        }
    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        objEntHorexcep.VI_nid_propietario = int.Parse(hid_id_tllr.Value.ToString());
        objEntHorexcep.VI_no_descripcion = txt_bus_excepdes.Text;
        objEntHorexcep.VI_fe_inicio = txt_bus_excepfecini.Text;
        objEntHorexcep.VI_fe_fin = txt_bus_excepfecfin.Text;
        objEntHorexcep.VI_fl_activo = ddl_bus_excepestado.SelectedValue.ToString();

        objListHorexcep = objNeg.GetListHorarioExcepcional(objEntHorexcep);
        if (objListHorexcep.Count > 0)
        {
            cargarGrid_HorExcepFiltro(objListHorexcep);
        }
        else
        {
            cargarGrid_HorExcep_Inicio();
        }
    }
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        popup_horexcepcional.Show();
        hid_acc_hor_excep.Value = "N";
        LimpiarCabeHorExcep();
        Estructura_DT_HorExcepDet();
        MaquetearHorExcep();
        cargarGrid_HorExcepMant(DT_HorExcepDet);
        llenarHorasHorExcep();
    }
    
    protected void chkI_CheckedChanged(object sender, EventArgs e)
    {

    }
    
}
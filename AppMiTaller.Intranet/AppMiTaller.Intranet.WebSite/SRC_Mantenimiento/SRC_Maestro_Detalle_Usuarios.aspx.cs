using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SRC_Mantenimiento_SRC_Maestro_Detalle_Usuarios : System.Web.UI.Page
{

    UsuarioBE oMaestroUsuariosBE;
    Int32 oResp;
    Parametros oParametros;

    protected void Page_Load(object sender, EventArgs e)
    {
        Labels_x_Pais();

        txt_nro_documento.Attributes.Add("onKeypress", "return Valida_DNI(event)");
        txt_nombres.Attributes.Add("onKeypress", "return Valida_Nombre(event)");
        txt_ape_paterno.Attributes.Add("onKeypress", "return Valida_ApePaterno(event)");
        txt_ape_materno.Attributes.Add("onKeypress", "return Valida_ApeMaterno(event)");
        txt_login.Attributes.Add("onKeypress", "return Valida_Usuario(event)");

        txt_capacidad_fo.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_capacidad_bo.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_capacidad.Attributes.Add("onkeypress", "return SoloNumeros(event)");

        if (!Page.IsPostBack)
        {
            Inicializa();

            if (Session["Nid_usuario_nuevo"] != null)
            {
                tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
                tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
                tabMantDetalleUsuarios.Tabs[3].Enabled = false; tabMantDetalleUsuarios.Tabs[3].HeaderText = "";
                tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
            }
            else if (Session["Nid_usuario_editar"] != null || Session["Nid_usuario_detalle"] != null)
            {
                HabilitarTabsPorPerfil();
            }
        }
        else
        {
            txt_contraseña.Attributes.Add("value", hdf_pass_nuevo.Value);
        }
    }

    private void TraspasoItemListBox(ListBox lstInicio, ListBox lstDestino)
    {
        if (lstInicio.SelectedIndex != -1)
        {
            ListItemCollection lista = new ListItemCollection();

            foreach (ListItem lsServicio in lstInicio.Items)
            {
                if (lsServicio.Selected) { lista.Add(lsServicio); }
            }

            foreach (ListItem item in lista)
            {
                if (lstInicio.Items.Contains(item)) { lstInicio.Items.Remove(item); }
                if (!lstDestino.Items.Contains(item)) { lstDestino.Items.Add(item); }
            }

            lstInicio.SelectedIndex = -1;
            lstDestino.SelectedIndex = -1;
        }
    }

    private void TraspasoItemDropDownList(ListBox lstInicio, DropDownList lstDestino)
    {
        lstDestino.Items.Clear();
        foreach (ListItem item in lstInicio.Items) { lstDestino.Items.Add(item); }
        lstDestino.Items.Insert(0, new ListItem("--Seleccione--", "--Seleccione--"));
    }


    protected void Labels_x_Pais()
    {

        Parametros oParm = new Parametros();
        lblTextoDepaASE.Text = oParm.N_Departamento.ToString();
        lblTextoDepaCALL.Text = oParm.N_Departamento.ToString();
        lblTextoDepaDG.Text = oParm.N_Departamento.ToString();
        lblTextoDepaTAL.Text = oParm.N_Departamento.ToString();
        lblTextoDepaTND.Text = oParm.N_Departamento.ToString();

        lblTextoProvASE.Text = oParm.N_Provincia.ToString();
        lblTextoProvCALL.Text = oParm.N_Provincia.ToString();
        lblTextoProvDG.Text = oParm.N_Provincia.ToString();
        lblTextoProvTAL.Text = oParm.N_Provincia.ToString();
        lblTextoProvTND.Text = oParm.N_Provincia.ToString();

        lblTextoDistASE.Text = oParm.N_Distrito.ToString();
        lblTextoDistCALL.Text = oParm.N_Distrito.ToString();
        lblTextoDistDG.Text = oParm.N_Distrito.ToString();
        lblTextoDistTAL.Text = oParm.N_Distrito.ToString();
        lblTextoDistTND.Text = oParm.N_Distrito.ToString();

        lblTextoLocalASE.Text = oParm.N_Local.ToString();
        lblTextoLocalCALL.Text = oParm.N_Local.ToString();
        lblTextoLocalDG.Text = oParm.N_Local.ToString();
        lblTextoLocalTAL.Text = oParm.N_Local.ToString();
        lblTextoLocalTND.Text = oParm.N_Local.ToString();

        lblTextoTallerASE.Text = oParm.N_Taller.ToString();
        lblTextoTallerCALL.Text = oParm.N_Taller.ToString();
        lblTextoTallerTAL.Text = oParm.N_Taller.ToString();

        lblTextoTipoLocalTND.Text = "Tipo de " + oParm.N_Local.ToString();

    }

    private void HabilitarDatosGenerales(Boolean flag)
    {
        ddl_tipo_doc.Enabled = flag;
        txt_nombres.Enabled = flag;
        txt_nro_documento.Enabled = flag;
        txt_ape_paterno.Enabled = flag;
        txt_ape_materno.Enabled = flag;
        txt_contraseña.Enabled = flag;
        txt_login.Enabled = flag;
        txt_correo.Enabled = flag;
        txt_telefono.Enabled = flag;
        txt_movil.Enabled = flag;
        ddl_tipo.Enabled = flag;
        ddl_perfil.Enabled = flag;
        ddl_departamento_dg.Enabled = flag;
        ddl_provincia_dg.Enabled = flag;
        ddl_distrito_dg.Enabled = flag;
        ddl_ubicacion_dg.Enabled = flag;
        ddl_estado.Enabled = flag;
        txt_fec_inicio_acceso.Enabled = flag; btn_Calendario1.Enabled = flag;
        txt_fin_acceso.Enabled = flag; btn_calendario2.Enabled = flag;
        txt_hora_inicio.Enabled = flag;
        txt_hora_fin.Enabled = flag;
        txt_mensaje.Enabled = flag;
        chk_bloqueado.Enabled = flag;
        chk_no_disponible.Enabled = flag;
        chk_consulta_VIN.Enabled = flag;
        ddlModulo.Enabled = flag;
    }
    private void Habilitar_PrmAdmTienda(Boolean flag)
    {
        ddl_departamento_tienda.Enabled = flag;
        ddl_provincia_taller.Enabled = flag;
        ddl_distrito_taller.Enabled = flag;
        ddl_tipoptored_tienda.Enabled = flag;
        btn_add_ptored_t.Enabled = flag; btn_del_ptored_tienda.Enabled = flag;
        ddl_empresa_tienda.Enabled = flag;
        ddl_marca_tienda.Enabled = flag;
        ddl_linea_comercial_tienda.Enabled = flag;
        btn_add_modelo_tienda.Enabled = flag; btn_del_modelo_tienda.Enabled = flag;
    }
    private void Habilitar_PrmAdmTaller(Boolean flag)
    {
        ddl_departamento_taller.Enabled = flag;
        ddl_provincia_taller.Enabled = flag;
        ddl_distrito_taller.Enabled = flag;
        ddl_ptored_taller.Enabled = flag;
        btn_add_taller_t.Enabled = flag; btn_del_taller_t.Enabled = flag;
        ddl_empresa_taller.Enabled = flag;
        ddl_marca_taller.Enabled = flag;
        ddl_lineacomercial_taller.Enabled = flag;
        btn_add_modelo_t.Enabled = flag;
        btn_del_modelo_t.Enabled = flag;
    }
    private void Habilitar_PrmAsesorServicio_Taller(Boolean flag)
    {
        ddl_departamento_ase_serv_t.Enabled = false;
        ddl_provincia_ases_serv_t.Enabled = false;
        ddl_distrito_ase_serv_t.Enabled = false;
        ddl_ptored_ase_serv_t.Enabled = false;
        ddl_taller_ase_serv_t.Enabled = flag;
        ddl_empresa_ase_serv_t.Enabled = flag;
        ddl_marca_ase_serv_t.Enabled = flag;
        ddl_linea_comercial_ase_serv_t.Enabled = flag;
        btn_add_modelo_ase_serv_t.Enabled = flag;
        btn_del_modelo_ase_serv_t.Enabled = flag;
    }
    private void Habilitar_PrmAsesorServicio_Horario(Boolean flag)
    {
        ddl_dias_semana.Enabled = flag;
        btn_add_dia_semana.Enabled = flag;
        btn_del_dia_semana.Enabled = flag;
        ddl_dia.Enabled = flag;
        ddl_hora_inicio.Enabled = flag;
        ddl_hora_fin.Enabled = flag;
        btn_add_hora.Enabled = flag;
        btn_del_hora.Enabled = flag;
        txt_capacidad_fo.Enabled = flag;
        txt_capacidad_bo.Enabled = flag;
        txt_capacidad.Enabled = flag;
        chkTotal.Enabled = flag;
        //lst_horas_sel.Enabled = flag;
        txt_dia_exceptuado.Enabled = flag;
        btn_calendario3.Enabled = flag;
        btn_add_dia_excep.Enabled = flag;
        btn_del_dia_excep.Enabled = flag;
        //lst_dias_excep.Enabled = flag;
    }
    private void Habilitar_PrmAsesorServicio_Serv(Boolean flag)
    {
        btn_add_tipo_serv.Enabled = flag;
        btn_del_tipo_serv.Enabled = flag;
        ddl_tipo_servicio_s.Enabled = flag;
        btn_add_serv.Enabled = flag;
        btn_del_serv.Enabled = flag;
    }
    private void Habilitar_PrmOprCallCenter_Taller(Boolean flag)
    {
        ddl_departamento_call.Enabled = flag;
        ddl_provincia_call.Enabled = flag;
        ddl_distrito_call.Enabled = flag;
        ddl_ptored_call.Enabled = flag;
        btn_add_taller_call.Enabled = flag; btn_del_taller_call.Enabled = flag;
        ddl_empresa_call.Enabled = flag;
        ddl_marca_call.Enabled = flag;
        ddl_linea_comercial_call.Enabled = flag;
        btn_add_modelo_call.Enabled = flag;
        btn_del_modelo_call.Enabled = flag;
    }
    private void Habilitar_PrmOprCallCenter_Servicio(Boolean flag)
    {
        btn_add_tipo_serv_cc.Enabled = flag;
        btn_del_tipo_serv_cc.Enabled = flag;
        ddl_tipo_servicio_cc.Enabled = flag;
        btn_add_serv_cc.Enabled = flag;
        btn_del_serv_cc.Enabled = flag;
    }

    private void Label_X_Pais()
    {

    }

    private void Inicializa()
    {
        Label_X_Pais();
        btnEditar.Visible = false;
        ViewState.Add("existe_login", "0");
        if (Request.QueryString["nid_usu"] != null)
            Session["Nid_usuario_detalle"] = Request.QueryString["nid_usu"].ToString();

        if (Session["Nid_usuario_nuevo"] != null)
            hdf_pass.Value = "nuevo";
        else if (Session["Nid_usuario_editar"] != null || Session["Nid_usuario_detalle"] != null)
            hdf_pass.Value = "editar";

        if (Session["Nid_usuario_detalle"] != null)
        {
            btnEditar.Visible = true;
            btnGrabar.Visible = false;
        }

        CargarLineaComercialMarca();

        #region DATOS GENERALES

        CargarTipoDocumento();
        CargarTipo();
        CargarPerfil();

        CargarUbigeo(ref ddl_departamento_dg, ref ddl_provincia_dg, ref ddl_distrito_dg);

        CargarUbicacion();
        CargarEstado();

        //@001 - I        
        oParametros = new Parametros();

        if (oParametros.SRC_Pais.Equals(2))
        {
            string nu_modulos = ConfigurationManager.AppSettings["nu_modulos"].ToString();
            foreach (string nu_modulo in nu_modulos.Split('|'))
            {
                ddlModulo.Items.Add(new ListItem(nu_modulo, nu_modulo));
            }
            ddlModulo.Items.Insert(0, new ListItem("--Sel--", "0"));
        }
        else
        {
            ddlModulo.Visible = false;
            lblModulo.Visible = false;
        }
        //@001 - F

        if (Session["Nid_usuario_detalle"] != null || Session["Nid_usuario_editar"] != null)
        {
            UsuarioTallerBL objneg = new UsuarioTallerBL();
            UsuarioBE objent = new UsuarioBE();
            if (Session["Nid_usuario_detalle"] != null)
                objent.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
            else if (Session["Nid_usuario_editar"] != null)
                objent.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
            List<UsuarioBE> list = objneg.GETListarDetalleUsuarioPorCodigo(objent);
            ddl_tipo_doc.SelectedIndex = 1;
            ddl_tipo_doc.Enabled = false;
            txt_nro_documento.Text = list[0].Nu_tipo_documento;
            txt_nombres.Text = list[0].VNOMUSR;
            txt_ape_paterno.Text = list[0].No_ape_paterno;
            txt_ape_materno.Text = list[0].No_ape_materno;
            txt_login.Text = list[0].CUSR_ID;
            ViewState["CUSR_ID"] = list[0].CUSR_ID;
            txt_correo.Text = list[0].VCORREO;
            if (list[0].VTELEFONO.Contains("|"))
            {
                txt_telefono.Text = list[0].VTELEFONO.Split('|')[0];
                txt_movil.Text = list[0].VTELEFONO.Split('|')[1];
            }
            if (list[0].Nid_cod_tipo_usuario != 0)
                ddl_tipo.SelectedValue = list[0].Nid_cod_tipo_usuario.ToString();
            else
                ddl_tipo.SelectedIndex = 0;

            if (list[0].Cod_perfil != "")
                ddl_perfil.SelectedValue = list[0].Cod_perfil;
            else
                ddl_perfil.SelectedIndex = 0;

            ViewState.Add("perfil", list[0].Cod_perfil);

            if (list[0].Coddpto != "")
                ddl_departamento_dg.SelectedValue = list[0].Coddpto;
            else
                ddl_departamento_dg.SelectedIndex = 0;

            CargarProvinciaPorDepartamentoSeleccionado(ref ddl_departamento_dg, ref ddl_provincia_dg, ref ddl_distrito_dg);

            if (list[0].Codprov != "")
                ddl_provincia_dg.SelectedValue = list[0].Codprov;
            else
                ddl_provincia_dg.SelectedIndex = 0;

            CargarDistritoPorProvinciaSeleccionada(ref ddl_departamento_dg, ref ddl_provincia_dg, ref ddl_distrito_dg);

            if (list[0].Coddist != "")
                ddl_distrito_dg.SelectedValue = list[0].Coddist;
            else
                ddl_distrito_dg.SelectedIndex = 0;

            //cargar ubicacion            

            if (ddl_distrito_dg.SelectedIndex != 0)
            {
                DataRow[] oRow = ((DataTable)ViewState["dtubicacion"]).Select("coddpto='" + ddl_departamento_dg.SelectedValue + "' AND codprov='" + ddl_provincia_dg.SelectedValue + "' AND coddist='" + ddl_distrito_dg.SelectedValue + "'", "no_ubica", DataViewRowState.CurrentRows);
                ddl_ubicacion_dg.Items.Clear();
                for (Int32 i = 0; i < oRow.Length; i++)
                {
                    ddl_ubicacion_dg.Items.Add("");
                    ddl_ubicacion_dg.Items[i].Value = oRow[i]["nid_ubica"].ToString();
                    ddl_ubicacion_dg.Items[i].Text = oRow[i]["no_ubica"].ToString();
                }
                ddl_ubicacion_dg.Items.Insert(0, "--Seleccione--");
                ddl_ubicacion_dg.SelectedIndex = 0;
            }

            if (list[0].Nid_ubica != 0)
            {
                ddl_ubicacion_dg.Enabled = true;
                ddl_ubicacion_dg.SelectedValue = list[0].Nid_ubica.ToString();
            }
            else
            {
                ddl_ubicacion_dg.Enabled = false;
                ddl_ubicacion_dg.SelectedIndex = 0;
            }

            if (list[0].Fl_inactivo != "")
                ddl_estado.SelectedValue = list[0].Fl_inactivo;
            else
                ddl_estado.SelectedIndex = 0;

            txt_fec_inicio_acceso.Text = list[0].Fe_inicio_acceso1;
            txt_fin_acceso.Text = list[0].Fe_fin_acceso1;
            txt_hora_inicio.Text = list[0].Hr_inicio_acceso;
            txt_hora_fin.Text = list[0].Hr_fin_acceso;
            txt_mensaje.Text = list[0].VMSGBLQ;

            if (list[0].CESTBLQ == "1")
                chk_bloqueado.Checked = true;
            else
                chk_bloqueado.Checked = false;

            if (list[0].nu_modulo != 0)
                ddlModulo.SelectedIndex = ddlModulo.Items.IndexOf(ddlModulo.Items.FindByValue(list[0].nu_modulo.ToString()));
            else
                ddlModulo.SelectedIndex = 0;

            if (Session["Nid_usuario_detalle"] != null)
            {
                HabilitarDatosGenerales(false);
                ddl_tipo_doc.Enabled = false;
            }
            else if (Session["Nid_usuario_editar"] != null)
            {
                HabilitarDatosGenerales(true);
                ddl_tipo_doc.Enabled = false;
            }



        }

        #endregion

        CargarPtoRedTaller_PorDistrito();

        #region PRM ADM TIENDA
        CargarUbigeo(ref ddl_departamento_tienda, ref ddl_provincia_tienda, ref ddl_distrito_tienda);
        CargarMarcaEmpresa(ref ddl_empresa_tienda);
        ddl_tipoptored_tienda.Items.Insert(0, "--Seleccione--");
        ddl_tipoptored_tienda.SelectedIndex = 0;
        ddl_tipoptored_tienda.Enabled = false;
        ddl_marca_tienda.Items.Insert(0, "--Seleccione--");
        ddl_marca_tienda.SelectedIndex = 0;
        ddl_marca_tienda.Enabled = false;
        ddl_linea_comercial_tienda.Items.Insert(0, "--Seleccione--");
        ddl_linea_comercial_tienda.SelectedIndex = 0;
        ddl_linea_comercial_tienda.Enabled = false;
        Habilitar_PrmAdmTienda(false);

        if (Session["Nid_usuario_detalle"] != null || Session["Nid_usuario_editar"] != null)
        {
            if (ViewState["perfil"].ToString() == "ATIE") //adm. tienda
            {
                //cargar listbox lst_ptored_sel_tienda
                Cargar_List_Ubicacion_Por_Perfil_Tienda();
                List<UsuarioBE> list2 = (List<UsuarioBE>)ViewState["listptoredtda"];
                lst_ptored_sel_tienda.Items.Clear();
                for (Int32 i = 0; i < list2.Count; i++)
                {
                    lst_ptored_sel_tienda.Items.Add("");
                    lst_ptored_sel_tienda.Items[i].Value = list2[i].IntID.ToString();
                    lst_ptored_sel_tienda.Items[i].Text = list2[i].DES;
                }

                //cargar listbox lst_modelo_sel_tienda

                Cargar_List_Modelo_Por_Perfil_Tienda();
                List<UsuarioBE> list3 = (List<UsuarioBE>)ViewState["list_mod"];

                lst_modelo_sel_tienda.Items.Clear();
                for (Int32 i = 0; i < list3.Count; i++)
                {
                    lst_modelo_sel_tienda.Items.Add("");
                    lst_modelo_sel_tienda.Items[i].Value = list3[i].IntID.ToString();
                    lst_modelo_sel_tienda.Items[i].Text = list3[i].DES;
                }

                if (Session["Nid_usuario_detalle"] != null)
                    Habilitar_PrmAdmTienda(false);
                else if (Session["Nid_usuario_editar"] != null)
                {
                    Habilitar_PrmAdmTienda(true);
                    ddl_provincia_tienda.Enabled = false;
                    ddl_distrito_tienda.Enabled = false;
                    ddl_tipoptored_tienda.Enabled = false;
                    ddl_marca_tienda.Enabled = false;
                    ddl_linea_comercial_tienda.Enabled = false;
                }
            }
        }
        CargarTiposPuntoRedPorDistrito();
        #endregion

        #region PRM ADM TALLER
        CargarUbigeo(ref ddl_departamento_taller, ref ddl_provincia_taller, ref ddl_distrito_taller);
        CargarMarcaEmpresa(ref ddl_empresa_taller);
        ddl_ptored_taller.Items.Insert(0, "--Seleccione--");
        ddl_ptored_taller.SelectedIndex = 0;
        ddl_ptored_taller.Enabled = false;
        ddl_marca_taller.Items.Insert(0, "--Seleccione--");
        ddl_marca_taller.SelectedIndex = 0;
        ddl_marca_taller.Enabled = false;
        ddl_lineacomercial_taller.Items.Insert(0, "--Seleccione--");
        ddl_lineacomercial_taller.SelectedIndex = 0;
        ddl_lineacomercial_taller.Enabled = false;





        Habilitar_PrmAdmTaller(false);

        if (Session["Nid_usuario_detalle"] != null || Session["Nid_usuario_editar"] != null)
        {
            if (ViewState["perfil"].ToString() == "ATAL") //adm. taller
            {
                //cargar listbox lst_talleres_sel_t
                Cargar_List_Taller_Por_Perfil_Taller();
                List<UsuarioBE> list4 = (List<UsuarioBE>)ViewState["listtaller"];
                lst_talleres_sel_t.Items.Clear();
                for (Int32 i = 0; i < list4.Count; i++)
                {
                    lst_talleres_sel_t.Items.Add("");
                    lst_talleres_sel_t.Items[i].Value = list4[i].IntID.ToString();
                    lst_talleres_sel_t.Items[i].Text = list4[i].DES;
                }
                //cargar listbox lst_modelos_sel_taller
                Cargar_List_Modelo_Por_Perfil_Taller();
                List<UsuarioBE> list5 = (List<UsuarioBE>)ViewState["list_mod"];
                lst_modelos_sel_taller.Items.Clear();
                for (Int32 i = 0; i < list5.Count; i++)
                {
                    lst_modelos_sel_taller.Items.Add("");
                    lst_modelos_sel_taller.Items[i].Value = list5[i].IntID.ToString();
                    lst_modelos_sel_taller.Items[i].Text = list5[i].DES;
                }

                if (Session["Nid_usuario_detalle"] != null)
                {
                    Habilitar_PrmAdmTaller(false);
                }
                else if (Session["Nid_usuario_editar"] != null)
                {
                    Habilitar_PrmAdmTaller(true);

                    ddl_provincia_taller.Enabled = false;
                    ddl_distrito_taller.Enabled = false;
                    ddl_ptored_taller.Enabled = false;
                    ddl_marca_taller.Enabled = false;
                    ddl_lineacomercial_taller.Enabled = false;
                }
            }
        }
        #endregion

        #region PRM ASESOR SERVICIO

        #region TALLER
        
        CargarUbigeo(ref ddl_departamento_ase_serv_t, ref ddl_provincia_ases_serv_t, ref ddl_distrito_ase_serv_t);
        CargarMarcaEmpresa(ref ddl_empresa_ase_serv_t);
        ddl_taller_ase_serv_t.Items.Insert(0, "--Seleccione--");
        ddl_taller_ase_serv_t.SelectedIndex = 0;
        ddl_taller_ase_serv_t.Enabled = false;
        ddl_ptored_ase_serv_t.Items.Insert(0, "--Seleccione--");
        ddl_ptored_ase_serv_t.SelectedIndex = 0;
        ddl_ptored_ase_serv_t.Enabled = false;
        ddl_marca_ase_serv_t.Items.Insert(0, "--Seleccione--");
        ddl_marca_ase_serv_t.SelectedIndex = 0;
        ddl_marca_ase_serv_t.Enabled = false;
        ddl_linea_comercial_ase_serv_t.Items.Insert(0, "--Seleccione--");
        ddl_linea_comercial_ase_serv_t.SelectedIndex = 0;
        ddl_linea_comercial_ase_serv_t.Enabled = false;
        Habilitar_PrmAsesorServicio_Taller(false);
        if (Session["Nid_usuario_detalle"] != null || Session["Nid_usuario_editar"] != null)
        {
            if (ViewState["perfil"].ToString() == "ASRV" || ViewState["perfil"].ToString() == "MECA") //asesor servicio //@004 I/F
            {
                Cargar_List_Taller_Por_Perfil_Asesor();
                List<UsuarioBE> list6_1 = (List<UsuarioBE>)ViewState["listtaller"];
                if (list6_1.Count > 0)
                {
                    ddl_departamento_ase_serv_t.SelectedValue = list6_1[0].Coddpto;
                    CargarProvinciaPorDepartamentoSeleccionado(ref ddl_departamento_ase_serv_t, ref ddl_provincia_ases_serv_t, ref ddl_distrito_ase_serv_t);
                    ddl_provincia_ases_serv_t.SelectedValue = list6_1[0].Codprov;
                    CargarDistritoPorProvinciaSeleccionada(ref ddl_departamento_ase_serv_t, ref ddl_provincia_ases_serv_t, ref ddl_distrito_ase_serv_t);
                    ddl_distrito_ase_serv_t.SelectedValue = list6_1[0].Coddist;
                    CargarPtoRed_PorDistrito(ddl_departamento_ase_serv_t, ddl_provincia_ases_serv_t, ddl_distrito_ase_serv_t, ref ddl_ptored_ase_serv_t);
                    ddl_ptored_ase_serv_t.SelectedValue = list6_1[0].Nid_ubica.ToString();
                    CargarTalleres_PorPtoRed();
                    ddl_taller_ase_serv_t.SelectedValue = list6_1[0].nid_taller.ToString();
                    ViewState.Add("nid_taller_asesserv", list6_1[0].nid_taller.ToString());
                }

                Cargar_List_Modelo_Por_Perfil_Asesor();
                List<UsuarioBE> list6 = (List<UsuarioBE>)ViewState["list_mod"];
                lst_modelos_sel_ase_serv_t.Items.Clear();
                for (Int32 i = 0; i < list6.Count; i++)
                {
                    lst_modelos_sel_ase_serv_t.Items.Add("");
                    lst_modelos_sel_ase_serv_t.Items[i].Value = list6[i].IntID.ToString();
                    lst_modelos_sel_ase_serv_t.Items[i].Text = list6[i].DES;
                }

                if (Session["Nid_usuario_detalle"] != null)
                    Habilitar_PrmAsesorServicio_Taller(false);
                else if (Session["Nid_usuario_editar"] != null)
                {
                    Habilitar_PrmAsesorServicio_Taller(true);
                    ddl_marca_ase_serv_t.Enabled = false;
                    ddl_linea_comercial_ase_serv_t.Enabled = false;
                }
            }
        }
        #endregion

        #region HORARIO

        ddl_dia.Enabled = false;
        ddl_dia.Items.Insert(0, "--Seleccione--");
        ddl_dia.SelectedIndex = 0;
        ddl_hora_inicio.Enabled = false;
        ddl_hora_fin.Enabled = false;

        if (Session["Nid_usuario_nuevo"] != null)
            CargarFeriados();
        Habilitar_PrmAsesorServicio_Horario(false);

        if (Session["Nid_usuario_detalle"] != null || Session["Nid_usuario_editar"] != null)
        {
            if (ViewState["perfil"].ToString() == "ASRV" || ViewState["perfil"].ToString() == "MECA") //asesor servicio //@004 I/F
            {
                //cargar listbox lst_horas_sel
                Cargar_List_Horario_Por_Perfil_Asesor();
                if (ViewState["list_horario"] != null)
                {
                    List<UsuarioBE> list7 = (List<UsuarioBE>)ViewState["list_horario"];

                    lst_horas_sel.Items.Clear();
                    if (list7.Count > 0)
                    {
                        for (Int32 i = 0; i < list7.Count; i++)
                        {
                            lst_horas_sel.Items.Add("");
                            lst_horas_sel.Items[i].Value = list7[i].StrID;
                            lst_horas_sel.Items[i].Text = list7[i].DES;
                        }
                        //comparar dias existentes
                        DataTable dtdia = new DataTable();
                        dtdia.Columns.Add("dia", System.Type.GetType("System.Int32"));
                        Boolean existe = true;
                        for (Int32 i = 0; i < list7.Count; i++)
                        {
                            for (Int32 j = 0; j <= 7; j++)
                            {
                                if (Convert.ToInt32(list7[i].StrID.Split('|')[0]) != j)
                                    existe = false;
                                else { existe = true; break; }
                            }
                            if (!existe)
                                dtdia.Rows.Add(list7[i].StrID.Split('|')[0]);
                        }

                        Boolean existe2 = false;
                        Int32 cont = 0;
                        lst_dias_sel.Items.Clear();
                        ddl_dia.Items.Clear();
                        for (Int32 j = 1; j <= 7; j++)
                        {
                            for (Int32 i = 0; i < dtdia.Rows.Count; i++)
                            {
                                if (j == Convert.ToInt32(dtdia.Rows[i]["dia"].ToString()))
                                {
                                    existe2 = true;
                                    break;
                                }
                            }
                            if (existe2)
                            {
                                lst_dias_sel.Items.Add("");
                                lst_dias_sel.Items[cont].Value = j.ToString();
                                lst_dias_sel.Items[cont].Text = "- " + DevolverDia(j);

                                ddl_dia.Items.Add("");
                                ddl_dia.Items[cont].Value = j.ToString();
                                ddl_dia.Items[cont].Text = DevolverDia(j);
                                cont += 1;
                            }
                            existe2 = false;
                        }
                        ddl_dia.Items.Insert(0, "--Seleccione--");
                        ddl_dia.SelectedIndex = 0;
                        String value = "";
                        for (Int32 i = 0; i < dtdia.Rows.Count; i++)
                        {
                            for (Int32 j = 1; j < ddl_dias_semana.Items.Count; j++)
                            {
                                if (dtdia.Rows[i]["dia"].ToString() == ddl_dias_semana.Items[j].Value)
                                {
                                    value = value + ddl_dias_semana.Items[j].Value + ",";
                                    break;
                                }
                            }
                        }
                        if (value.Length > 0)
                        {
                            value = value.Substring(0, value.Length - 1);
                            for (Int32 i = 0; i < value.Split(',').Length; i++)
                            {
                                for (Int32 j = 1; j < ddl_dias_semana.Items.Count; j++)
                                {
                                    if (value.Split(',')[i] == ddl_dias_semana.Items[j].Value)
                                    {
                                        ddl_dias_semana.Items.RemoveAt(j);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }


                ViewState["dias_sel"] = (List<UsuarioBE>)ViewState["list_horario"];

                CargarDiasSemana(ref ddl_dias_semana);
                //
                Cargar_List_DiasExcep_Por_Perfil_Asesor();
                List<UsuarioBE> list8 = (List<UsuarioBE>)ViewState["listDiasExcep"];
                lst_dias_excep.Items.Clear();
                if (list8.Count > 0)
                {
                    for (Int32 i = 0; i < list8.Count; i++)
                    {
                        lst_dias_excep.Items.Add("");
                        lst_dias_excep.Items[i].Value = list8[i].StrID;
                        lst_dias_excep.Items[i].Text = list8[i].DES;
                    }
                }
                else
                    CargarFeriados();
                if (Session["Nid_usuario_detalle"] != null)
                    Habilitar_PrmAsesorServicio_Horario(false);
                else if (Session["Nid_usuario_editar"] != null)
                {
                    Habilitar_PrmAsesorServicio_Horario(true);
                    ddl_hora_inicio.Enabled = false;
                    ddl_hora_fin.Enabled = false;
                }

                Cargar_List_CapacidadAtencion_Asesor();
            }
        }

        #endregion

        #region SERVICIO
        ddl_tipo_servicio_s.Items.Insert(0, "--Seleccione--");
        ddl_tipo_servicio_s.SelectedIndex = 0;
        ddl_tipo_servicio_s.Enabled = false;

        Habilitar_PrmAsesorServicio_Serv(false);

        if (Session["Nid_usuario_detalle"] != null || Session["Nid_usuario_editar"] != null)
        {
            if (ViewState["perfil"].ToString() == "ASRV") //asesor servicio
            {
                //cargar listbox lst_servicio_espec_sel_s
                Cargar_List_Servicios_Por_Perfil_Asesor();
                List<UsuarioBE> list9 = (List<UsuarioBE>)ViewState["listserv"];
                lst_servicio_espec_sel_s.Items.Clear();
                for (Int32 i = 0; i < list9.Count; i++)
                {
                    lst_servicio_espec_sel_s.Items.Add("");
                    lst_servicio_espec_sel_s.Items[i].Value = list9[i].IntID.ToString();
                    lst_servicio_espec_sel_s.Items[i].Text = list9[i].DES;
                }
                if (Session["Nid_usuario_detalle"] != null)
                    Habilitar_PrmAsesorServicio_Serv(false);
                else if (Session["Nid_usuario_editar"] != null)
                    Habilitar_PrmAsesorServicio_Serv(true);
            }
        }

        CargarTipoServicio_Especifico();

        #endregion

        #endregion

        #region PRM CALL CENTER

        #region TALLER
        CargarUbigeo(ref ddl_departamento_call, ref ddl_provincia_call, ref ddl_distrito_call);
        CargarMarcaEmpresa(ref ddl_empresa_call);
        ddl_ptored_call.Items.Insert(0, "--Seleccione--");
        ddl_ptored_call.SelectedIndex = 0;
        ddl_ptored_call.Enabled = false;
        ddl_marca_call.Items.Insert(0, "--Seleccione--");
        ddl_marca_call.SelectedIndex = 0;
        ddl_marca_call.Enabled = false;
        ddl_linea_comercial_call.Items.Insert(0, "--Seleccione--");
        ddl_linea_comercial_call.SelectedIndex = 0;
        ddl_linea_comercial_call.Enabled = false;
        Habilitar_PrmOprCallCenter_Taller(false);
        if (Session["Nid_usuario_detalle"] != null || Session["Nid_usuario_editar"] != null)
        {
            if (ViewState["perfil"].ToString() == "CALL") //opr call center
            {
                //cargar listbox lst_talleres_sel_call
                Cargar_List_Talleres_Por_Perfil_Call();
                List<UsuarioBE> list10 = (List<UsuarioBE>)ViewState["listtaller"];
                lst_talleres_sel_call.Items.Clear();
                for (Int32 i = 0; i < list10.Count; i++)
                {
                    lst_talleres_sel_call.Items.Add("");
                    lst_talleres_sel_call.Items[i].Value = list10[i].IntID.ToString();
                    lst_talleres_sel_call.Items[i].Text = list10[i].DES;
                }

                // cargar listbox lst_modelos_sel_call

                Cargar_List_Modelos_Por_Perfil_Call();
                List<UsuarioBE> list11 = (List<UsuarioBE>)ViewState["list_mod"];
                lst_modelos_sel_call.Items.Clear();
                for (Int32 i = 0; i < list11.Count; i++)
                {
                    lst_modelos_sel_call.Items.Add("");
                    lst_modelos_sel_call.Items[i].Value = list11[i].IntID.ToString();
                    lst_modelos_sel_call.Items[i].Text = list11[i].DES;
                }

                if (Session["Nid_usuario_detalle"] != null)
                    Habilitar_PrmOprCallCenter_Taller(false);
                else if (Session["Nid_usuario_editar"] != null)
                {
                    Habilitar_PrmOprCallCenter_Taller(true);
                    ddl_provincia_call.Enabled = false;
                    ddl_distrito_call.Enabled = false;
                    ddl_ptored_call.Enabled = false;
                    ddl_marca_call.Enabled = false;
                    ddl_linea_comercial_call.Enabled = false;
                }
            }
        }
        #endregion

        #region SERVICIO
        ddl_tipo_servicio_cc.Items.Insert(0, "--Seleccione--");
        ddl_tipo_servicio_cc.SelectedIndex = 0;
        ddl_tipo_servicio_cc.Enabled = false;

        Habilitar_PrmOprCallCenter_Servicio(false);

        if (Session["Nid_usuario_detalle"] != null || Session["Nid_usuario_editar"] != null)
        {
            if (ViewState["perfil"].ToString() == "CALL") //asesor servicio
            {
                //cargar listbox lst_servicio_espec_sel_s
                Cargar_List_Servicios_Por_Perfil_Call();
                List<UsuarioBE> list9 = (List<UsuarioBE>)ViewState["listserv_call"];
                lst_servicio_espec_sel_cc.Items.Clear();
                for (Int32 i = 0; i < list9.Count; i++)
                {
                    lst_servicio_espec_sel_cc.Items.Add("");
                    lst_servicio_espec_sel_cc.Items[i].Value = list9[i].IntID.ToString();
                    lst_servicio_espec_sel_cc.Items[i].Text = list9[i].DES;
                }
                if (Session["Nid_usuario_detalle"] != null)
                    Habilitar_PrmOprCallCenter_Servicio(false);
                else if (Session["Nid_usuario_editar"] != null)
                    Habilitar_PrmOprCallCenter_Servicio(true);
            }
        }

        CargarTipoServicio_Especifico_Call();

        #endregion

        #endregion

        CargarModelo_LineaMarca();
        CargarPtoRedTaller_PorDistrito_Editar();
    }


    private void Cargar_List_CapacidadAtencion_Asesor()
    {
        UsuarioTallerBL objneg = new UsuarioTallerBL();
        UsuarioBE objent = new UsuarioBE();

        objent.Nid_usuario = (Session["Nid_usuario_editar"] != null) ? Convert.ToInt32(Session["Nid_usuario_editar"].ToString()) : Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());

        AppMiTaller.Intranet.BE.UsuarioBEList lstCapacidad = objneg.GETListarCapacidadAtencion_PorUsuario(objent);

        string strTmpCapacidad = string.Empty;
        bool blnExiste = false;

        for (int i = 1; i < ddl_dia.Items.Count; i++)
        {
            string strDiaValue = ddl_dia.Items[i].Value.ToString();

            blnExiste = false;
            foreach (UsuarioBE oCapacidad in lstCapacidad)
            {
                if (strDiaValue.Equals(oCapacidad.Dd_atencion.ToString()))
                {
                    string strCControl = string.IsNullOrEmpty(oCapacidad.fl_control.ToString()) ? "I" : oCapacidad.fl_control.ToString();
                    string strCFO = oCapacidad.qt_capacidad_fo.Equals(-1) ? "" : oCapacidad.qt_capacidad_fo.ToString();
                    string strCBO = oCapacidad.qt_capacidad_bo.Equals(-1) ? "" : oCapacidad.qt_capacidad_bo.ToString();
                    string strCTotal = oCapacidad.qt_capacidad.Equals(-1) ? "" : oCapacidad.qt_capacidad.ToString();

                    strTmpCapacidad += oCapacidad.Dd_atencion.ToString() + "-" + strCControl + "-" + strCFO + "-" + strCBO + "-" + strCTotal + "|";
                    blnExiste = true;
                    break;
                }
            }

            if (!blnExiste) strTmpCapacidad += strDiaValue + "----|";
        }

        if (strTmpCapacidad.Length > 0) strTmpCapacidad = strTmpCapacidad.Substring(0, strTmpCapacidad.Length - 1);

        hfCapacidad.Value = strTmpCapacidad;
    }

    private void CargarPtoRedTaller_PorDistrito()
    {
        UsuarioTallerBL objneg = new UsuarioTallerBL();
        UsuarioBE objent = new UsuarioBE();
        objent.Co_Perfil_Login = Profile.Usuario.co_perfil_usuario;
        objent.Nid_usuario_Login = Profile.Usuario.Nid_usuario;
        List<UsuarioBE> List = objneg.GETListarPtoRedTaller_PorDistrito(objent);
        DataTable dt = new DataTable();
        dt.Columns.Add("nid_ubica", System.Type.GetType("System.Int32"));
        dt.Columns.Add("no_ubica", System.Type.GetType("System.String"));
        dt.Columns.Add("nid_taller", System.Type.GetType("System.Int32"));
        dt.Columns.Add("no_taller", System.Type.GetType("System.String"));
        dt.Columns.Add("coddpto", System.Type.GetType("System.String"));
        dt.Columns.Add("codprov", System.Type.GetType("System.String"));
        dt.Columns.Add("coddist", System.Type.GetType("System.String"));
        //
        dt.Columns.Add("existe", System.Type.GetType("System.String"));
        //
        for (Int32 i = 0; i < List.Count; i++)
            dt.Rows.Add(List[i].Nid_ubica, List[i].No_ubica, List[i].nid_taller, List[i].No_taller,
                List[i].Coddpto, List[i].Codprov, List[i].Coddist, "0");
        ViewState.Add("dtptoredtaller_dist", dt);
        objneg = null; dt = null;
    }

    private void Cargar_List_Ubicacion_Por_Perfil_Tienda()
    {
        UsuarioBE ent1 = new UsuarioBE();
        UsuarioTallerBL neg1 = new UsuarioTallerBL();
        if (Session["Nid_usuario_detalle"] != null)
            ent1.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent1.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list2 = neg1.GETListarPtoRed_PorUsuario(ent1);
        ViewState.Add("listptoredtda", list2);
        ent1 = null; neg1 = null;
    }
    private void Cargar_List_Modelo_Por_Perfil_Tienda()
    {
        UsuarioTallerBL neg2 = new UsuarioTallerBL();
        UsuarioBE ent2 = new UsuarioBE();
        if (Session["Nid_usuario_detalle"] != null)
            ent2.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent2.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list3 = neg2.GETListarModelos_PorUsuario(ent2);
        ViewState.Add("list_mod", list3);
        ent2 = null; neg2 = null;
    }

    private void Cargar_List_Taller_Por_Perfil_Taller()
    {
        UsuarioTallerBL neg3 = new UsuarioTallerBL();
        UsuarioBE ent3 = new UsuarioBE();
        if (Session["Nid_usuario_detalle"] != null)
            ent3.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent3.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list4 = neg3.GETListarTalleres_PorUsuario(ent3);
        ViewState.Add("listtaller", list4);
        ent3 = null; neg3 = null;
    }
    private void Cargar_List_Modelo_Por_Perfil_Taller()
    {
        UsuarioTallerBL neg4 = new UsuarioTallerBL();
        UsuarioBE ent4 = new UsuarioBE();
        if (Session["Nid_usuario_detalle"] != null)
            ent4.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent4.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list5 = neg4.GETListarModelos_PorUsuario(ent4);
        ViewState.Add("list_mod", list5);
        ent4 = null; neg4 = null;
    }

    private void Cargar_List_Talleres_Por_Perfil_Call()
    {
        UsuarioTallerBL neg9 = new UsuarioTallerBL();
        UsuarioBE ent9 = new UsuarioBE();
        if (Session["Nid_usuario_detalle"] != null)
            ent9.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent9.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list10 = neg9.GETListarTalleres_PorUsuario(ent9);
        ViewState.Add("listtaller", list10);
        ent9 = null; neg9 = null;
    }
    private void Cargar_List_Modelos_Por_Perfil_Call()
    {
        UsuarioTallerBL neg10 = new UsuarioTallerBL();
        UsuarioBE ent10 = new UsuarioBE();
        if (Session["Nid_usuario_detalle"] != null)
            ent10.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent10.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list11 = neg10.GETListarModelos_PorUsuario(ent10);
        ViewState.Add("list_mod", list11);
        ent10 = null; neg10 = null;
    }
    private void Cargar_List_Servicios_Por_Perfil_Call()
    {
        UsuarioBE ent8 = new UsuarioBE();
        UsuarioTallerBL neg8 = new UsuarioTallerBL();
        if (Session["Nid_usuario_detalle"] != null)
            ent8.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent8.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list9 = neg8.GETListarServicios_PorUsuario(ent8);
        ViewState.Add("listserv_call", list9);
        ent8 = null; neg8 = null;
    }

    private void Cargar_List_Taller_Por_Perfil_Asesor()
    {
        UsuarioTallerBL neg5_1 = new UsuarioTallerBL();
        UsuarioBE ent5_1 = new UsuarioBE();
        if (Session["Nid_usuario_detalle"] != null)
            ent5_1.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent5_1.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list6_1 = neg5_1.GETListarTaller_PorUsuario_AsesServ(ent5_1);
        ViewState.Add("listtaller", list6_1);
        ent5_1 = null; neg5_1 = null;
    }
    private void Cargar_List_Modelo_Por_Perfil_Asesor()
    {
        UsuarioTallerBL neg5 = new UsuarioTallerBL();
        UsuarioBE ent5 = new UsuarioBE();
        if (Session["Nid_usuario_detalle"] != null)
            ent5.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent5.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list6 = neg5.GETListarModelos_PorUsuario(ent5);
        ViewState.Add("list_mod", list6);
        ent5 = null; neg5 = null;
    }
    private void Cargar_List_Horario_Por_Perfil_Asesor()
    {
        UsuarioBE ent6 = new UsuarioBE();
        UsuarioTallerBL neg6 = new UsuarioTallerBL();
        if (Session["Nid_usuario_detalle"] != null)
            ent6.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent6.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list7 = neg6.GETListarHorario_PorUsuario(ent6);
        ViewState.Add("list_horario", list7);
        ent6 = null; neg6 = null;
    }
    private void Cargar_List_DiasExcep_Por_Perfil_Asesor()
    {
        UsuarioBE ent7 = new UsuarioBE();
        UsuarioTallerBL neg7 = new UsuarioTallerBL();
        if (Session["Nid_usuario_detalle"] != null)
            ent7.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent7.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list8 = neg7.GETListarDiasExcep_PorUsuario(ent7);
        ViewState.Add("listDiasExcep", list8);
        ent7 = null; neg7 = null;
    }
    private void Cargar_List_Servicios_Por_Perfil_Asesor()
    {
        UsuarioBE ent8 = new UsuarioBE();
        UsuarioTallerBL neg8 = new UsuarioTallerBL();
        if (Session["Nid_usuario_detalle"] != null)
            ent8.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());
        else if (Session["Nid_usuario_editar"] != null)
            ent8.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
        List<UsuarioBE> list9 = neg8.GETListarServicios_PorUsuario(ent8);
        ViewState.Add("listserv", list9);
        ent8 = null; neg8 = null;
    }


    #region Metodos Propios
    
    private void CargarTipoDocumento()
    {
        ddl_tipo_doc.Items.Clear();

        //UsuarioTallerBL objneg = new UsuarioTallerBL();
        //List<UsuarioBE> list = objneg.GETListarTipoDocumento();

        CitasBL objBLT = new CitasBL();
        List<CitasBE> list = objBLT.GETListarTipoDocumentos();

        if (list.Count > 0)
        {
            for (Int32 i = 0; i < list.Count; i++)
            {
                ddl_tipo_doc.Items.Add("");
                ddl_tipo_doc.Items[i].Value = list[i].cod_tipo_documento.ToString();
                ddl_tipo_doc.Items[i].Text = list[i].des_tipo_documento;
            }

            ddl_tipo_doc.Items.Insert(0, "--Seleccione--");
            ddl_tipo_doc.SelectedIndex = 1;
            ddl_tipo_doc_SelectedIndexChanged(this, null);
            //ddl_tipo_doc.Enabled = false;

        }
        else
        {
            ddl_tipo_doc.Enabled = false;
        }
    }

    private void CargarTipo()
    {
        ddl_tipo.Items.Clear();
        UsuarioTallerBL objNeg = new UsuarioTallerBL();
        List<UsuarioBE> List = objNeg.GETListarTipo();
        for (Int32 i = 0; i < List.Count; i++)
        {
            ddl_tipo.Items.Add("");
            ddl_tipo.Items[i].Value = List[i].StrID;
            ddl_tipo.Items[i].Text = List[i].DES;
        }
        if (ddl_tipo.Items.Count > 0)
        {
            ddl_tipo.Items.Insert(0, "--Seleccione--");
            ddl_tipo.Enabled = true;
            ddl_tipo.SelectedIndex = 0;
        }
        else
            ddl_tipo.Enabled = false;
        objNeg = null;
        List = null;
    }

    private void CargarPerfil()
    {
        ddl_perfil.Items.Clear();
        UsuarioTallerBL objNegUsu = new UsuarioTallerBL();
        UsuarioBE ent = new UsuarioBE();
        ent.CUSR_ID = Profile.UserName;
        List<UsuarioBE> List = objNegUsu.GETListarPerfiles(ent);

        for (Int32 i = 0; i < List.Count; i++)
        {
            ddl_perfil.Items.Add("");
            ddl_perfil.Items[i].Value = List[i].Cod_perfil;
            ddl_perfil.Items[i].Text = List[i].Perfil;
        }
        ddl_perfil.Items.Insert(0, "--Seleccione--");
        ddl_perfil.SelectedIndex = 0;
        if (ddl_perfil.Items.Count > 1)
            ddl_perfil.Enabled = true;
        else
            ddl_perfil.Enabled = false;
        objNegUsu = null;
        List = null;
    }

    private void CargarUbigeo(ref DropDownList ddl_dpto, ref DropDownList ddl_prov, ref DropDownList ddl_dist)
    {
        UsuarioTallerBL objNegUsu = new UsuarioTallerBL();
        List<UsuarioBE> ListUbigeo = objNegUsu.GETListarUbigeo(Profile.Usuario.Nid_usuario);
        DataTable dtUbigeo = new DataTable();
        dtUbigeo.Columns.Add("coddpto", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("codprov", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("coddist", System.Type.GetType("System.String"));
        dtUbigeo.Columns.Add("nombre", System.Type.GetType("System.String"));
        for (Int32 i = 0; i < ListUbigeo.Count; i++)
            dtUbigeo.Rows.Add(ListUbigeo[i].Coddpto, ListUbigeo[i].Codprov, ListUbigeo[i].Coddist, ListUbigeo[i].Ubigeo);
        ViewState.Add("dtubigeo", dtUbigeo);
        ddl_prov.Items.Insert(0, "--Seleccione--");
        ddl_prov.Enabled = false;
        ddl_dist.Items.Insert(0, "--Seleccione--");
        ddl_dist.Enabled = false;

        DataRow[] oRow = dtUbigeo.Select("codprov='00' AND coddist='00'", "nombre", DataViewRowState.CurrentRows);
        ddl_dpto.Items.Clear();
        for (Int32 i = 0; i < oRow.Length; i++)
        {
            ddl_dpto.Items.Add("");
            ddl_dpto.Items[i].Value = oRow[i]["coddpto"].ToString();
            ddl_dpto.Items[i].Text = oRow[i]["nombre"].ToString();
        }
        ddl_dpto.Items.Insert(0, "--Seleccione--");
        ddl_dpto.SelectedIndex = 0;
        ddl_dpto.AutoPostBack = true;

        objNegUsu = null; ListUbigeo = null; dtUbigeo = null;
    }

    private void CargarUbicacion()
    {
        UsuarioTallerBL objNeg = new UsuarioTallerBL();
        UsuarioBE objEnt = new UsuarioBE();
        objEnt.Co_Perfil_Login = Profile.Usuario.co_perfil_usuario;
        objEnt.Nid_usuario_Login = Profile.Usuario.Nid_usuario;
        List<UsuarioBE> List = objNeg.GETListarUbicacion(objEnt);
        if (List.Count > 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nid_ubica", System.Type.GetType("System.Int32"));
            dt.Columns.Add("no_ubica", System.Type.GetType("System.String"));
            dt.Columns.Add("coddpto", System.Type.GetType("System.String"));
            dt.Columns.Add("codprov", System.Type.GetType("System.String"));
            dt.Columns.Add("coddist", System.Type.GetType("System.String"));
            for (Int32 i = 0; i < List.Count; i++)
                dt.Rows.Add(List[i].Nid_ubica, List[i].No_ubica, List[i].Coddpto, List[i].Codprov, List[i].Coddist);
            ViewState.Add("dtubicacion", dt);
            dt = null;
        }
        objNeg = null; objEnt = null;
        ddl_ubicacion_dg.Items.Insert(0, "--Seleccione--");
        ddl_ubicacion_dg.SelectedIndex = 0;
        ddl_ubicacion_dg.Enabled = false;
    }

    private void CargarEstado()
    {
        ddl_estado.Items.Clear();
        ddl_estado.Items.Add("");
        ddl_estado.Items[0].Text = "--Seleccione--";
        ddl_estado.Items[0].Value = "";
        ddl_estado.Items.Add("");
        ddl_estado.Items[1].Text = "Activo";
        ddl_estado.Items[1].Value = "0";
        ddl_estado.Items.Add("");
        ddl_estado.Items[2].Text = "Inactivo";
        ddl_estado.Items[2].Value = "1";
    }

    //PRM ADM TIENDA

    private void CargarTiposPuntoRedPorDistrito()
    {
        UsuarioTallerBL objNeg = new UsuarioTallerBL();
        UsuarioBE objEnt = new UsuarioBE();
        //objEnt.Co_Perfil_Login = Profile.Usuario.co_perfil_usuario;
        //objEnt.Nid_usuario_Login = Profile.Usuario.Nid_usuario;
        List<UsuarioBE> List = objNeg.GETListarTipoPuntosRedPorDistrito();
        if (List.Count > 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("tip_ubica", System.Type.GetType("System.String"));
            dt.Columns.Add("no_tip_ubica", System.Type.GetType("System.String"));
            dt.Columns.Add("nid_ubica", System.Type.GetType("System.Int32"));
            dt.Columns.Add("no_ubica", System.Type.GetType("System.String"));
            dt.Columns.Add("coddpto", System.Type.GetType("System.String"));
            dt.Columns.Add("codprov", System.Type.GetType("System.String"));
            dt.Columns.Add("coddist", System.Type.GetType("System.String"));
            //
            dt.Columns.Add("existe", System.Type.GetType("System.String"));
            //
            for (Int32 i = 0; i < List.Count; i++)
            {
                dt.Rows.Add(List[i].Tip_ubica, List[i].No_tip_ubica, List[i].Nid_ubica,
                    List[i].No_ubica, List[i].Coddpto, List[i].Codprov, List[i].Coddist, "0");
            }

            if (ViewState["listptoredtda"] != null)
            {
                List<UsuarioBE> list_ptored = (List<UsuarioBE>)ViewState["listptoredtda"];
                if (list_ptored.Count > 0)
                {
                    for (Int32 i = 0; i < list_ptored.Count; i++)
                    {
                        for (Int32 j = 0; j < dt.Rows.Count; j++)
                        {
                            if (list_ptored[i].IntID == Convert.ToInt32(dt.Rows[j]["nid_ubica"].ToString()))
                            {
                                DataRow[] fila = dt.Select("nid_ubica = " + list_ptored[i].IntID);
                                fila[0]["existe"] = "1";
                                break;
                            }
                        }
                    }
                }
            }
            ViewState.Add("dtptoredpordistrito", dt);
            dt = null;
        }
        objNeg = null;
    }

    private void CargarMarcaEmpresa(ref DropDownList ddl_empresa)
    {
        UsuarioTallerBL objNeg = new UsuarioTallerBL();
        List<UsuarioBE> List = objNeg.GETListarMarcaEmpresa(Profile.Usuario.Nid_usuario);
        if (List.Count > 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nid_marca", System.Type.GetType("System.Int32"));
            dt.Columns.Add("no_marca", System.Type.GetType("System.String"));
            dt.Columns.Add("nid_empresa", System.Type.GetType("System.Int32"));
            dt.Columns.Add("no_empresa", System.Type.GetType("System.String"));

            for (Int32 i = 0; i < List.Count; i++)
                dt.Rows.Add(List[i].Nid_marca, List[i].No_marca, List[i].Nid_empresa, List[i].No_empresa);
            ViewState.Add("dtmarcaempresa", dt);

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("nid_empresa", System.Type.GetType("System.Int32"));
            dt1.Columns.Add("no_empresa", System.Type.GetType("System.String"));
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                if (dt1.Select("nid_empresa = " + Convert.ToInt32(dt.Rows[i]["nid_empresa"])).Length == 0)
                    dt1.Rows.Add(Convert.ToInt32(dt.Rows[i]["nid_empresa"].ToString()), dt.Rows[i]["no_empresa"].ToString());
            }
            dt = null;
            for (Int32 i = 0; i < dt1.Rows.Count; i++)
            {
                ddl_empresa.Items.Add("");
                ddl_empresa.Items[i].Value = dt1.Rows[i]["nid_empresa"].ToString();
                ddl_empresa.Items[i].Text = dt1.Rows[i]["no_empresa"].ToString();
            }
            dt1 = null;
            if (ddl_empresa.Items.Count > 0)
            {
                ddl_empresa.Items.Insert(0, "--Seleccione--");
                ddl_empresa.SelectedIndex = 0;
                ddl_empresa.Enabled = true;
                ddl_empresa.AutoPostBack = true;
            }
            else
            {
                ddl_empresa.Enabled = false;
                ddl_empresa.AutoPostBack = false;
            }
        }
        objNeg = null;
    }

    private void CargarLineaComercialMarca()
    {
        UsuarioTallerBL objNeg = new UsuarioTallerBL();
        List<UsuarioBE> List = objNeg.GETListarLineaComercialMarca(Profile.Usuario.Nid_usuario);
        if (List.Count > 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nid_negocio_linea", System.Type.GetType("System.Int32"));
            dt.Columns.Add("linea_comercial", System.Type.GetType("System.String"));
            dt.Columns.Add("nid_marca", System.Type.GetType("System.Int32"));
            for (Int32 i = 0; i < List.Count; i++)
                dt.Rows.Add(List[i].Nid_negocio_linea, List[i].Linea_comercial, List[i].Nid_marca);
            ViewState.Add("dtlineamarca", dt);
            dt = null;
        }
        objNeg = null;
    }

    private void CargarModelo_LineaMarca()
    {
        UsuarioTallerBL objneg = new UsuarioTallerBL();
        UsuarioBE objent = new UsuarioBE();
        objent.Co_Perfil_Login = Profile.Usuario.co_perfil_usuario;
        objent.Nid_usuario_Login = Profile.Usuario.Nid_usuario;
        List<UsuarioBE> List = objneg.GETListarModelo_LineaMarca(objent);
        if (List.Count > 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nid_modelo", System.Type.GetType("System.Int32"));
            dt.Columns.Add("no_modelo", System.Type.GetType("System.String"));
            dt.Columns.Add("nid_negocio_linea", System.Type.GetType("System.Int32"));
            dt.Columns.Add("nid_marca", System.Type.GetType("System.Int32"));
            //
            dt.Columns.Add("existe", System.Type.GetType("System.String"));
            //
            for (Int32 i = 0; i < List.Count; i++)
                dt.Rows.Add(List[i].Nid_modelo, List[i].No_modelo, List[i].Nid_negocio_linea, List[i].Nid_marca, "0");

            if (ViewState["list_mod"] != null)
            {
                List<UsuarioBE> list_mod = (List<UsuarioBE>)ViewState["list_mod"];
                if (list_mod.Count > 0)
                {
                    for (Int32 i = 0; i < list_mod.Count; i++)
                    {
                        for (Int32 j = 0; j < dt.Rows.Count; j++)
                        {
                            if (list_mod[i].IntID == Convert.ToInt32(dt.Rows[j]["nid_modelo"].ToString()))
                            {
                                DataRow[] fila = dt.Select("nid_modelo = " + list_mod[i].IntID);
                                fila[0]["existe"] = "1";
                                break;
                            }
                        }
                    }
                }
            }
            ViewState.Add("dtmodelo_lineamarca", dt);
            dt = null;
        }
        objent = null; objneg = null;
    }

    //PRM ASESOR SERVICIO

    //TALLER

    private void CargarPtoRedTaller_PorDistrito_Editar()
    {
        DataTable dt = (DataTable)ViewState["dtptoredtaller_dist"];
        if (ViewState["listtaller"] != null)
        {
            List<UsuarioBE> list_taller = (List<UsuarioBE>)ViewState["listtaller"];
            if (list_taller.Count > 0)
            {
                for (Int32 i = 0; i < list_taller.Count; i++)
                {
                    for (Int32 j = 0; j < dt.Rows.Count; j++)
                    {
                        if (list_taller[i].IntID == Convert.ToInt32(dt.Rows[j]["nid_taller"].ToString()))
                        {
                            DataRow[] fila = dt.Select("nid_taller = " + list_taller[i].IntID);
                            fila[0]["existe"] = "1";
                            break;
                        }
                    }
                }
            }
        }
        ViewState["dtptoredtaller_dist"] = dt;
        dt = null;
    }

    //HORARIOS

    private void CargarDiasSemana(ref DropDownList ddl_dia)
    {
        ddl_dia.Items.Clear();

        int ind_exis = 0;
        List<UsuarioBE> list = (List<UsuarioBE>)ViewState["dias_sel"];
        String[] dia = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
        for (Int32 i = 1; i <= dia.Length; i++)
        {
            for (Int32 j = 0; j < list.Count; j++)
            {
                if (i.ToString() == list[j].StrID.Split('|')[0])
                {
                    ind_exis += 1;
                    break;
                }
            }
            if (ind_exis == 0)
            {
                ddl_dia.Items.Add("");
                ddl_dia.Items[ddl_dia.Items.Count - 1].Text = dia[i - 1].ToString();
                ddl_dia.Items[ddl_dia.Items.Count - 1].Value = i.ToString();

            }
            ind_exis = 0;
        }
        ddl_dia.Items.Insert(0, "--Seleccione--");
        ddl_dia.SelectedIndex = 0;
    }

    private void cargarRangoHorasDefecto(ref DropDownList ddlHoras, String strHoraInicio, String strHoraFin, Int32 intIntervalo)
    {
        DateTime dtHoraIni = Convert.ToDateTime(strHoraInicio);
        DateTime dtHoraFin = Convert.ToDateTime(strHoraFin);

        Int32 intIndex = 0;
        ddlHoras.AutoPostBack = false;

        while (dtHoraIni <= dtHoraFin)
        {
            ddlHoras.Items.Add("");
            ddlHoras.Items[intIndex].Text = String.Format("{0:hh:mm}", dtHoraIni) + " " + (dtHoraIni.Hour >= 12 ? "PM" : "AM");
            ddlHoras.Items[intIndex].Value = String.Format("{0:HH:mm}", dtHoraIni).ToUpper().Replace(".", "");
            intIndex += 1;
            dtHoraIni = dtHoraIni.AddMinutes(intIntervalo);
        }

        if (ddlHoras.ID.ToUpper().Contains("DDL_HORA_INICIO"))
            ddlHoras.SelectedIndex = 0;

        if (ddlHoras.ID.ToUpper().Contains("DDL_HORA_FIN"))
            ddlHoras.SelectedIndex = ddlHoras.Items.Count - 1;
    }

    private void CargarFeriados()
    {
        UsuarioTallerBL neg = new UsuarioTallerBL();
        List<UsuarioBE> list = neg.GETListarFeriados();
        lst_dias_excep.Items.Clear();
        for (Int32 i = 0; i < list.Count; i++)
        {
            lst_dias_excep.Items.Add("");
            lst_dias_excep.Items[i].Value = list[i].StrID;
            lst_dias_excep.Items[i].Text = list[i].DES;
        }
        neg = null;
    }

    //PRM OPR CALL CENTER

    #endregion


    private void MantenimientoModelos(int idUsuario, ListBox lstModelos, string fl_tipo)
    {
        oMaestroUsuariosBE = new UsuarioBE();
        UsuarioTallerBL oMaestroUsuariosBL = new UsuarioTallerBL();

        string co_modelos = string.Empty;
        foreach (ListItem lstModelo in lstModelos.Items)
        {
            co_modelos += lstModelo.Value.ToString() + "|";
        }

        oMaestroUsuariosBE.Nid_usuario = idUsuario;
        oMaestroUsuariosBE.No_modelo = co_modelos.Trim();
        oMaestroUsuariosBE.Co_usuario_crea = Profile.UserName;
        oMaestroUsuariosBE.Co_usuario_red = Profile.UsuarioRed;
        oMaestroUsuariosBE.No_estacion_red = Profile.Estacion;
        oMaestroUsuariosBE.Fl_tipo = fl_tipo;

        oResp = oMaestroUsuariosBL.MantenimientoUsuarioModelo(oMaestroUsuariosBE);
    }
    private void MantenimientoServicios(int idUsuario, ListBox lstServicios, string fl_tipo)
    {
        oMaestroUsuariosBE = new UsuarioBE();
        UsuarioTallerBL oMaestroUsuariosBL = new UsuarioTallerBL();

        string co_servicios = string.Empty;
        foreach (ListItem lstServicio in lstServicios.Items)
        {
            co_servicios += lstServicio.Value.ToString() + "|";
        }

        oMaestroUsuariosBE.Nid_usuario = idUsuario;
        oMaestroUsuariosBE.No_servicio = co_servicios.Trim();
        oMaestroUsuariosBE.Co_usuario_crea = Profile.UserName;
        oMaestroUsuariosBE.Co_usuario_red = Profile.UsuarioRed;
        oMaestroUsuariosBE.No_estacion_red = Profile.Estacion;
        oMaestroUsuariosBE.Fl_tipo = fl_tipo;

        oResp = oMaestroUsuariosBL.MantenimientoUsuarioServicio(oMaestroUsuariosBE);
    }
    private void MantenimientoTalleres(int idUsuario, ListBox lstTalleres, string fl_tipo)
    {
        oMaestroUsuariosBE = new UsuarioBE();
        UsuarioTallerBL oMaestroUsuariosBL = new UsuarioTallerBL();

        string co_talleres = string.Empty;
        foreach (ListItem lstTaller in lstTalleres.Items)
        {
            co_talleres += lstTaller.Value.ToString() + "|";
        }

        oMaestroUsuariosBE.Nid_usuario = idUsuario;
        oMaestroUsuariosBE.No_taller = co_talleres.Trim();
        oMaestroUsuariosBE.Co_usuario_crea = Profile.UserName;
        oMaestroUsuariosBE.Co_usuario_red = Profile.UsuarioRed;
        oMaestroUsuariosBE.No_estacion_red = Profile.Estacion;
        oMaestroUsuariosBE.Fl_tipo = fl_tipo;

        oResp = oMaestroUsuariosBL.MantenimientoUsuarioTaller(oMaestroUsuariosBE);
    }


    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (HayErrorEnDatosGenerales() == false)
        {
            UsuarioBE objEnt = new UsuarioBE();
            UsuarioTallerBL objneg = new UsuarioTallerBL();

            string fl_tipo = string.Empty;
            int _ID_USUARIO = 0;
            string co_modelos = string.Empty;
            string co_servicios = string.Empty;
            string co_talleres = string.Empty;


            if (Session["Nid_usuario_nuevo"] != null)
            {
                #region GRABAR
                try
                {
                    fl_tipo = "1";

                    #region GRABAR_USUARIO
                    objEnt.No_ape_paterno = txt_ape_paterno.Text.Trim();
                    objEnt.No_ape_materno = txt_ape_materno.Text.Trim();
                    objEnt.VNOMUSR = txt_nombres.Text.Trim();
                    objEnt.CUSR_ID = txt_login.Text.Trim();
                    objEnt.VUSR_PASS = "";
                    if (txt_telefono.Text.Trim() == "" && txt_movil.Text.Trim() == "")
                        objEnt.VTELEFONO = "";
                    else
                        objEnt.VTELEFONO = txt_telefono.Text.Trim() + "|" + txt_movil.Text.Trim();
                    objEnt.Nid_ubica = (ddl_ubicacion_dg.SelectedValue.ToString() != "--Seleccione--" ? Convert.ToInt32(ddl_ubicacion_dg.SelectedValue) : 0);
                    objEnt.Nid_cod_tipo_usuario = (ddl_tipo.SelectedValue.ToString() != "--Seleccione--" ? Convert.ToInt32(ddl_tipo.SelectedValue) : 0);
                    objEnt.VMSGBLQ = (txt_mensaje.Text.Trim() == "" ? "" : txt_mensaje.Text.Trim());
                    objEnt.CESTBLQ = (chk_bloqueado.Checked == true ? "1" : "0");
                    objEnt.Fe_inicio_acceso = (txt_fec_inicio_acceso.Text.Trim() != "" ? Convert.ToDateTime(txt_fec_inicio_acceso.Text.Trim()) : DateTime.MinValue);
                    objEnt.Fe_fin_acceso = (txt_fin_acceso.Text.Trim() != "" ? Convert.ToDateTime(txt_fin_acceso.Text.Trim()) : DateTime.MinValue);
                    objEnt.Hr_inicio_acceso = (txt_hora_inicio.Text.Trim() == "" ? "" : txt_hora_inicio.Text.Trim());
                    objEnt.Hr_fin_acceso = (txt_hora_fin.Text.Trim() == "" ? "" : txt_hora_fin.Text.Trim());
                    objEnt.Nu_tipo_documento = (txt_nro_documento.Text.Trim() == "" ? "" : txt_nro_documento.Text.Trim());
                    objEnt.VCORREO = (txt_correo.Text.Trim() == "" ? "" : txt_correo.Text.Trim());
                    if (txt_contraseña.Text.Trim() == "")
                        objEnt.VPASSMD5 = "";
                    else
                        objEnt.VPASSMD5 = objEnt.GetMD5(txt_contraseña.Text.Trim());
                    objEnt.Fl_inactivo = (ddl_estado.SelectedValue != "--Seleccione--" ? ddl_estado.SelectedValue : "0");
                    objEnt.Co_usuario_crea = Profile.UserName;
                    objEnt.No_estacion_red = Profile.Estacion; ;
                    objEnt.No_usuario_red = Profile.UsuarioRed;
                    Int32 res = objneg.InsertarUsuario(objEnt);
                    _ID_USUARIO = res;
                    #endregion

                    if (res > 0)
                    {
                        #region GRABAR USUARIO PERFIL
                        objEnt.CCOAPL = Profile.Aplicacion;
                        objEnt.Nid_usuario = res;
                        objEnt.Cod_perfil = ddl_perfil.SelectedValue;
                        objEnt.Fl_inactivo = "0";
                        objEnt.Co_usuario_crea = Profile.UserName;
                        objEnt.No_usuario = "";
                        objEnt.No_estacion_red = Profile.Estacion;

                        objneg.InsertarUsuarioPerfil(objEnt);

                        #endregion

                        if (ddl_perfil.SelectedValue == "AGEN") //general
                        {
                            //ACCESO TOTAL
                        }
                        else if (ddl_perfil.SelectedValue == "ATAL") //taller
                        {
                            if (lst_modelos_sel_taller.Items.Count > 0)
                            {
                                #region GRABAR USUARIO_MODELO - TALLER

                                MantenimientoModelos(_ID_USUARIO, lst_modelos_sel_taller, fl_tipo);

                                #endregion
                            }

                            if (lst_talleres_sel_t.Items.Count > 0)
                            {
                                #region GRABAR USUARIO_TALLER - TALLER

                                MantenimientoTalleres(_ID_USUARIO, lst_talleres_sel_t, fl_tipo);

                                #endregion
                            }

                        }
                        else if (ddl_perfil.SelectedValue == "ATIE") //tienda
                        {
                            if (lst_ptored_sel_tienda.Items.Count > 0)
                            {
                                #region GRABAR USUARIO_UBICACION - TIENDA

                                objEnt.Nid_usuario = res;
                                objEnt.Co_usuario_crea = Profile.UserName;
                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                objEnt.No_estacion_red = Profile.Estacion;
                                if (ddl_estado.SelectedValue != "--Seleccione--")
                                {
                                    if (ddl_estado.SelectedValue == "0")
                                        objEnt.Fl_activo = "A";
                                    else
                                        objEnt.Fl_activo = "I";
                                }
                                else
                                    objEnt.Fl_activo = "";

                                for (Int32 i = 0; i < lst_ptored_sel_tienda.Items.Count; i++)
                                {
                                    objEnt.Nid_ubica = Convert.ToInt32(lst_ptored_sel_tienda.Items[i].Value);
                                    objneg.InsertarUsuarioUbicacion(objEnt);
                                }

                                #endregion
                            }

                            if (lst_modelo_sel_tienda.Items.Count > 0)
                            {
                                #region GRABAR USUARIO_MODELO - TIENDA

                                MantenimientoModelos(_ID_USUARIO, lst_modelo_sel_tienda, fl_tipo);

                                #endregion
                            }
                        }
                        else if (ddl_perfil.SelectedValue == "ASRV") //servicio
                        {
                            if (lst_modelos_sel_ase_serv_t.Items.Count > 0)
                            {
                                #region GRABAR USUARIO_MODELO - ASESOR

                                MantenimientoModelos(_ID_USUARIO, lst_modelos_sel_ase_serv_t, fl_tipo);

                                #endregion
                            }

                            if (ddl_taller_ase_serv_t.SelectedIndex != 0)
                            {
                                #region GRABAR USUARIO TALLER - ASESOR

                                oMaestroUsuariosBE = new UsuarioBE();
                                UsuarioTallerBL oMaestroUsuariosBL = new UsuarioTallerBL();

                                oMaestroUsuariosBE.Nid_usuario = _ID_USUARIO;
                                oMaestroUsuariosBE.No_taller = ddl_taller_ase_serv_t.SelectedValue.ToString() + "|";
                                oMaestroUsuariosBE.Co_usuario_crea = Profile.UserName;
                                oMaestroUsuariosBE.Co_usuario_red = Profile.UsuarioRed;
                                oMaestroUsuariosBE.No_estacion_red = Profile.Estacion;
                                oMaestroUsuariosBE.Fl_tipo = fl_tipo;

                                oResp = oMaestroUsuariosBL.MantenimientoUsuarioTaller(oMaestroUsuariosBE);

                                #endregion
                            }

                            if (lst_horas_sel.Items.Count > 0)
                            {
                                #region GRABAR MAE_HORARIO - ASESOR

                                objEnt.Nid_usuario = res;
                                objEnt.Fl_tipo = "A";
                                objEnt.Co_usuario_crea = Profile.UserName;
                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                objEnt.No_estacion_red = Profile.Estacion;
                                if (ddl_estado.SelectedValue != "--Seleccione--")
                                {
                                    if (ddl_estado.SelectedValue == "0")
                                        objEnt.Fl_activo = "A";
                                    else
                                        objEnt.Fl_activo = "I";
                                }
                                else
                                    objEnt.Fl_activo = "";

                                for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
                                {
                                    objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[i].Value.ToString().Split('|')[0]);
                                    objEnt.Ho_inicio = lst_horas_sel.Items[i].Value.ToString().Split('|')[1];
                                    objEnt.Ho_fin = lst_horas_sel.Items[i].Value.ToString().Split('|')[2];
                                    objneg.InsertarUsuarioHorario(objEnt);
                                }

                                #endregion
                            }

                            if (lst_dias_excep.Items.Count > 0)
                            {
                                #region GRABAR MAE_DIAS_EXCEPTUADOS

                                objEnt.Nid_usuario = res;
                                objEnt.Fl_tipo = "A";
                                objEnt.Co_usuario_crea = Profile.UserName;
                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                objEnt.No_estacion_red = Profile.Estacion;
                                if (ddl_estado.SelectedValue != "--Seleccione--")
                                {
                                    if (ddl_estado.SelectedValue == "0")
                                        objEnt.Fl_activo = "A";
                                    else
                                        objEnt.Fl_activo = "I";
                                }
                                else
                                    objEnt.Fl_activo = "";

                                for (Int32 i = 0; i < lst_dias_excep.Items.Count; i++)
                                {
                                    objEnt.Fe_exceptuada = Convert.ToDateTime(lst_dias_excep.Items[i].Value.ToString());
                                    objneg.InsertarUsuarioDiaExceptuado(objEnt);
                                }

                                #endregion
                            }

                            if (lst_servicio_espec_sel_s.Items.Count > 0)
                            {
                                #region GRABAR MAE_USR_SERVICIO

                                MantenimientoServicios(_ID_USUARIO, lst_servicio_espec_sel_s, fl_tipo);

                                #endregion
                            }

                            if (hfCapacidad.Value.ToString().Length > 0)
                            {
                                #region GRABAR MAE_CAPACIDAD_ATENCION

                                objEnt.Nid_usuario = res;
                                objEnt.Co_usuario_crea = Profile.UserName;
                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                objEnt.No_estacion_red = Profile.Estacion;

                                string strTmpCapacidad = string.Empty;
                                string[] strCapacidaDias;

                                strCapacidaDias = hfCapacidad.Value.ToString().Split('|');
                                foreach (string strCapacidadDia in strCapacidaDias)
                                {
                                    if (strCapacidadDia.Trim().Length == 0) continue;

                                    string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                                    string strC_Tipo = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? "T" : "I") : strCapacidadDia.Split('-').GetValue(1).ToString();
                                    string strC_FO = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : txt_capacidad_fo.Text) : strCapacidadDia.Split('-').GetValue(2).ToString();
                                    string strC_BO = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : txt_capacidad_bo.Text) : strCapacidadDia.Split('-').GetValue(3).ToString();
                                    string strC_Total = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : "") : strCapacidadDia.Split('-').GetValue(4).ToString();

                                    if (!String.IsNullOrEmpty(strC_FO) || !String.IsNullOrEmpty(strC_BO) || !String.IsNullOrEmpty(strC_Total))
                                    {

                                        UsuarioBE oMaestroUsuariosBE = new UsuarioBE();
                                        UsuarioTallerBL oMaestroUsuariosBL = new UsuarioTallerBL();


                                        oMaestroUsuariosBE.Nid_usuario = res;
                                        oMaestroUsuariosBE.Dd_atencion = Convert.ToInt32(strC_Dia);
                                        oMaestroUsuariosBE.fl_control = strC_Tipo;
                                        oMaestroUsuariosBE.qt_capacidad_fo = String.IsNullOrEmpty(strC_FO) ? -1 : Convert.ToInt32(strC_FO);
                                        oMaestroUsuariosBE.qt_capacidad_bo = String.IsNullOrEmpty(strC_BO) ? -1 : Convert.ToInt32(strC_BO);
                                        oMaestroUsuariosBE.qt_capacidad = String.IsNullOrEmpty(strC_Total) ? -1 : Convert.ToInt32(strC_Total);
                                        //--
                                        oMaestroUsuariosBE.Co_usuario_crea = Profile.UserName;
                                        oMaestroUsuariosBE.Co_usuario_red = Profile.UsuarioRed;
                                        oMaestroUsuariosBE.No_estacion_red = Profile.Estacion;
                                        Int32 oResp = oMaestroUsuariosBL.MantenimientoCapacidadAtencion_Usuario(oMaestroUsuariosBE);

                                    }
                                }

                                #endregion
                            }


                            //@001 - I
                            oParametros = new Parametros();
                            if (oParametros.SRC_Pais.Equals(2))
                            {
                                CitasBE oCitas = new CitasBE();
                                oCitas.Nid_usuario = _ID_USUARIO;
                                oCitas.nid_modulo = Convert.ToInt32(ddlModulo.SelectedValue.ToString());
                                oCitas.co_usuario_crea = Profile.UserName;
                                oCitas.co_usuario_red = Profile.UsuarioRed;
                                oCitas.no_estacion_red = Profile.Estacion;

                                Int32 oResp = objneg.InsertarAsesorModulo(oCitas);
                            }
                            //@001 - F

                        }
                        else if (ddl_perfil.SelectedValue == "CALL") //call
                        {
                            if (lst_modelos_sel_call.Items.Count > 0)
                            {
                                #region GRABAR USUARIO_MODELO - CALL

                                MantenimientoModelos(_ID_USUARIO, lst_modelos_sel_call, fl_tipo);

                                #endregion
                            }

                            if (lst_talleres_sel_call.Items.Count > 0)
                            {
                                #region GRABAR USUARIO_TALLER - CALL

                                MantenimientoTalleres(_ID_USUARIO, lst_talleres_sel_call, fl_tipo);

                                #endregion
                            }
                            if (lst_servicio_espec_sel_cc.Items.Count > 0)
                            {
                                #region GRABAR USUARIO_SERVICIO - CALL

                                MantenimientoServicios(_ID_USUARIO, lst_servicio_espec_sel_cc, fl_tipo);

                                #endregion
                            }
                        }
                        //@004 I
                        else if (ddl_perfil.SelectedValue == "MECA") //Mecanico
                        {

                            if (ddl_taller_ase_serv_t.SelectedIndex != 0)
                            {
                                #region GRABAR USUARIO TALLER - ASESOR

                                oMaestroUsuariosBE = new UsuarioBE();
                                UsuarioTallerBL oMaestroUsuariosBL = new UsuarioTallerBL();

                                oMaestroUsuariosBE.Nid_usuario = _ID_USUARIO;
                                oMaestroUsuariosBE.No_taller = ddl_taller_ase_serv_t.SelectedValue.ToString() + "|";
                                oMaestroUsuariosBE.Co_usuario_crea = Profile.UserName;
                                oMaestroUsuariosBE.Co_usuario_red = Profile.UsuarioRed;
                                oMaestroUsuariosBE.No_estacion_red = Profile.Estacion;
                                oMaestroUsuariosBE.Fl_tipo = fl_tipo;

                                oResp = oMaestroUsuariosBL.MantenimientoUsuarioTaller(oMaestroUsuariosBE);

                                #endregion
                            }

                            if (lst_horas_sel.Items.Count > 0)
                            {
                                #region GRABAR MAE_HORARIO - ASESOR

                                objEnt.Nid_usuario = res;
                                objEnt.Fl_tipo = "A";
                                objEnt.Co_usuario_crea = Profile.UserName;
                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                objEnt.No_estacion_red = Profile.Estacion;
                                if (ddl_estado.SelectedValue != "--Seleccione--")
                                {
                                    if (ddl_estado.SelectedValue == "0")
                                        objEnt.Fl_activo = "A";
                                    else
                                        objEnt.Fl_activo = "I";
                                }
                                else
                                    objEnt.Fl_activo = "";

                                for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
                                {
                                    objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[i].Value.ToString().Split('|')[0]);
                                    objEnt.Ho_inicio = lst_horas_sel.Items[i].Value.ToString().Split('|')[1];
                                    objEnt.Ho_fin = lst_horas_sel.Items[i].Value.ToString().Split('|')[2];
                                    objneg.InsertarUsuarioHorario(objEnt);
                                }

                                #endregion
                            }

                            if (lst_dias_excep.Items.Count > 0)
                            {
                                #region GRABAR MAE_DIAS_EXCEPTUADOS

                                objEnt.Nid_usuario = res;
                                objEnt.Fl_tipo = "A";
                                objEnt.Co_usuario_crea = Profile.UserName;
                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                objEnt.No_estacion_red = Profile.Estacion;
                                if (ddl_estado.SelectedValue != "--Seleccione--")
                                {
                                    if (ddl_estado.SelectedValue == "0")
                                        objEnt.Fl_activo = "A";
                                    else
                                        objEnt.Fl_activo = "I";
                                }
                                else
                                    objEnt.Fl_activo = "";

                                for (Int32 i = 0; i < lst_dias_excep.Items.Count; i++)
                                {
                                    objEnt.Fe_exceptuada = Convert.ToDateTime(lst_dias_excep.Items[i].Value.ToString());
                                    objneg.InsertarUsuarioDiaExceptuado(objEnt);
                                }

                                #endregion
                            }

                            if (hfCapacidad.Value.ToString().Length > 0)
                            {
                                #region GRABAR MAE_CAPACIDAD_ATENCION

                                objEnt.Nid_usuario = res;
                                objEnt.Co_usuario_crea = Profile.UserName;
                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                objEnt.No_estacion_red = Profile.Estacion;

                                string strTmpCapacidad = string.Empty;
                                string[] strCapacidaDias;

                                strCapacidaDias = hfCapacidad.Value.ToString().Split('|');
                                foreach (string strCapacidadDia in strCapacidaDias)
                                {
                                    if (strCapacidadDia.Trim().Length == 0) continue;

                                    string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                                    string strC_Tipo = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? "T" : "I") : strCapacidadDia.Split('-').GetValue(1).ToString();
                                    string strC_FO = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : txt_capacidad_fo.Text) : strCapacidadDia.Split('-').GetValue(2).ToString();
                                    string strC_BO = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : txt_capacidad_bo.Text) : strCapacidadDia.Split('-').GetValue(3).ToString();
                                    string strC_Total = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : "") : strCapacidadDia.Split('-').GetValue(4).ToString();

                                    if (!String.IsNullOrEmpty(strC_FO) || !String.IsNullOrEmpty(strC_BO) || !String.IsNullOrEmpty(strC_Total))
                                    {

                                        UsuarioBE oMaestroUsuariosBE = new UsuarioBE();
                                        UsuarioTallerBL oMaestroUsuariosBL = new UsuarioTallerBL();


                                        oMaestroUsuariosBE.Nid_usuario = res;
                                        oMaestroUsuariosBE.Dd_atencion = Convert.ToInt32(strC_Dia);
                                        oMaestroUsuariosBE.fl_control = strC_Tipo;
                                        oMaestroUsuariosBE.qt_capacidad_fo = String.IsNullOrEmpty(strC_FO) ? -1 : Convert.ToInt32(strC_FO);
                                        oMaestroUsuariosBE.qt_capacidad_bo = String.IsNullOrEmpty(strC_BO) ? -1 : Convert.ToInt32(strC_BO);
                                        oMaestroUsuariosBE.qt_capacidad = String.IsNullOrEmpty(strC_Total) ? -1 : Convert.ToInt32(strC_Total);
                                        //--
                                        oMaestroUsuariosBE.Co_usuario_crea = Profile.UserName;
                                        oMaestroUsuariosBE.Co_usuario_red = Profile.UsuarioRed;
                                        oMaestroUsuariosBE.No_estacion_red = Profile.Estacion;
                                        Int32 oResp = oMaestroUsuariosBL.MantenimientoCapacidadAtencion_Usuario(oMaestroUsuariosBE);

                                    }
                                }

                                #endregion
                            }
                        }
                        //@004 F

                        //objEnt = null; objneg = null;
                        Session["Nid_usuario_nuevo"] = null; Session["Nid_usuario_detalle"] = null; Session["Nid_usuario_editar"] = null;
                        lbl_mensajebox.Text = "El registro se guardo con exito.";
                        popup_msgbox_confirm.Show();
                    }
                }
                catch { }
                #endregion
            }

            if (Session["Nid_usuario_editar"] != null || Session["Nid_usuario_detalle"] != null)
            {
                #region ACTUALIZAR

                try
                {
                    fl_tipo = "2";

                    if (Session["Nid_usuario_editar"] != null)
                        _ID_USUARIO = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
                    else if (Session["Nid_usuario_detalle"] != null)
                        _ID_USUARIO = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());


                    #region ACTUALIZAR_USUARIO

                    objEnt.Nid_usuario = _ID_USUARIO;
                    objEnt.No_ape_paterno = txt_ape_paterno.Text.Trim();
                    objEnt.No_ape_materno = txt_ape_materno.Text.Trim();
                    objEnt.VNOMUSR = txt_nombres.Text.Trim();
                    objEnt.CUSR_ID = txt_login.Text.Trim();
                    objEnt.VUSR_PASS = "";
                    objEnt.VTELEFONO = txt_telefono.Text.Trim() + "|" + txt_movil.Text.Trim();
                    objEnt.Nid_ubica = (ddl_ubicacion_dg.SelectedValue.ToString() != "--Seleccione--" ? Convert.ToInt32(ddl_ubicacion_dg.SelectedValue) : 0);
                    objEnt.Nid_cod_tipo_usuario = (ddl_tipo.SelectedValue.ToString() != "--Seleccione--" ? Convert.ToInt32(ddl_tipo.SelectedValue) : 0);
                    objEnt.VMSGBLQ = (txt_mensaje.Text.Trim() == "" ? "" : txt_mensaje.Text.Trim());
                    objEnt.CESTBLQ = (chk_bloqueado.Checked == true ? "1" : "0");
                    objEnt.Fe_inicio_acceso = (txt_fec_inicio_acceso.Text.Trim() != "" ? Convert.ToDateTime(txt_fec_inicio_acceso.Text.Trim()) : DateTime.MinValue);
                    objEnt.Fe_fin_acceso = (txt_fin_acceso.Text.Trim() != "" ? Convert.ToDateTime(txt_fin_acceso.Text.Trim()) : DateTime.MinValue);
                    objEnt.Hr_inicio_acceso = (txt_hora_inicio.Text.Trim() == "" ? "" : txt_hora_inicio.Text.Trim());
                    objEnt.Hr_fin_acceso = (txt_hora_fin.Text.Trim() == "" ? "" : txt_hora_fin.Text.Trim());
                    objEnt.Nu_tipo_documento = (txt_nro_documento.Text.Trim() == "" ? "" : txt_nro_documento.Text.Trim());
                    objEnt.VCORREO = (txt_correo.Text.Trim() == "" ? "" : txt_correo.Text.Trim());
                    objEnt.VPASSMD5 = (txt_contraseña.Text.Trim().Equals("") ? "" : objEnt.GetMD5(txt_contraseña.Text.Trim()));
                    objEnt.Fl_inactivo = (ddl_estado.SelectedValue != "--Seleccione--" ? ddl_estado.SelectedValue : "");
                    objEnt.Co_usuario_crea = Profile.UserName;
                    objEnt.No_estacion_red = Profile.Estacion;
                    objEnt.No_usuario_red = Profile.UsuarioRed;

                    Int32 res = objneg.ActualizarUsuario(objEnt);

                    #endregion

                    if (res > 0)
                    {
                        if (ddl_perfil.SelectedIndex != 0)
                        {
                            #region ACTUALIZAR USUARIO PERFIL

                            objEnt.Nid_usuario = _ID_USUARIO;
                            objEnt.CCOAPL = Profile.Aplicacion;
                            objEnt.Cod_perfil = ddl_perfil.SelectedValue;
                            objEnt.Fl_activo = "0";
                            objEnt.Co_usuario_cambio = Profile.UserName;
                            objEnt.No_usuario = "";
                            objEnt.No_estacion_red = Profile.Estacion;
                            objneg.ActualizarUsuarioPerfil(objEnt);

                            #endregion
                        }
                        if (ddl_perfil.SelectedValue == "ATAL") //taller
                        {

                            #region ACTUALIZAR MAE_USUARIO_TALLER - TALLER

                            MantenimientoTalleres(_ID_USUARIO, lst_talleres_sel_t, fl_tipo);

                            #endregion

                            #region ACTUALIZAR MAE_USR_MODELO - TALLER

                            MantenimientoModelos(_ID_USUARIO, lst_modelos_sel_taller, fl_tipo);

                            #endregion

                        }
                        else if (ddl_perfil.SelectedValue == "ATIE") //tienda
                        {

                            #region ACTUALIZAR_USUARIO_UBICACION - TIENDA
                            List<UsuarioBE> listub = (List<UsuarioBE>)ViewState["listptoredtda"];

                            if (Session["Nid_usuario_editar"] != null)
                                objEnt.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
                            else if (Session["Nid_usuario_detalle"] != null)
                                objEnt.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());


                            objEnt.Co_usuario_cambio = Profile.UserName;
                            objEnt.Co_usuario_red = Profile.UsuarioRed;
                            objEnt.No_estacion_red = Profile.Estacion;

                            //VEN
                            bool blHay = false;

                            //1era vuelta
                            foreach (UsuarioBE oAnterior in listub)
                            {
                                blHay = false;

                                foreach (ListItem oNuevo in lst_ptored_sel_tienda.Items)
                                {
                                    if (oAnterior.IntID.Equals(Int32.Parse(oNuevo.Value)))
                                    {
                                        blHay = true;
                                        break;
                                    }
                                }
                                if (!blHay)
                                {
                                    objEnt.Nid_ubica = oAnterior.IntID;
                                    objEnt.Fl_activo = "I";
                                    objEnt.Op = "D";
                                    objneg.MantenimientoUsuarioUbicacion(objEnt);
                                }

                            }

                            //2da Vuelta

                            foreach (ListItem oNuevo in lst_ptored_sel_tienda.Items)
                            {
                                blHay = false;

                                foreach (UsuarioBE oAnterior in listub)
                                {
                                    if (Int32.Parse(oNuevo.Value).Equals(oAnterior.IntID))
                                    {
                                        blHay = true;
                                        break;
                                    }
                                }

                                if (!blHay)
                                {
                                    objEnt.Nid_ubica = Int32.Parse(oNuevo.Value.ToString());
                                    objEnt.Fl_activo = "A";
                                    objEnt.Op = "I";
                                    objneg.MantenimientoUsuarioUbicacion(objEnt);
                                }

                            }

                            //if (listub.Count > lst_ptored_sel_tienda.Items.Count)
                            //{
                            //    for (Int32 i = 0; i < listub.Count; i++)
                            //    {
                            //        if (lst_ptored_sel_tienda.Items.Count == 0)
                            //        {
                            //            //delete - update actual                                        
                            //            objEnt.Nid_ubica = listub[i].IntID;
                            //            objEnt.Co_usuario_cambio = Profile.UserName;
                            //            objEnt.Co_usuario_red = Profile.UsuarioRed;
                            //            objEnt.No_estacion_red = Profile.Estacion;
                            //            //if (ddl_estado.SelectedValue != "--Seleccione--")
                            //            //{
                            //            //    if (ddl_estado.SelectedValue == "0")
                            //            //        objEnt.Fl_activo = "A";
                            //            //    else
                            //            //        objEnt.Fl_activo = "I";
                            //            //}
                            //            //else
                            //            //    objEnt.Fl_activo = "";
                            //            objEnt.Fl_activo = "I";                                        
                            //            objEnt.Op = "D";                                        
                            //            objneg.MantenimientoUsuarioUbicacion(objEnt);
                            //        }
                            //        else
                            //        {
                            //            for (Int32 j = 0; j < lst_ptored_sel_tienda.Items.Count; j++)
                            //            {
                            //                if (listub[i].IntID.ToString() == lst_ptored_sel_tienda.Items[j].Value)
                            //                {
                            //                    _flag = true;
                            //                    break;
                            //                }
                            //                else
                            //                    _flag = false;
                            //            }
                            //            if (_flag)
                            //            {
                            //                //UPDATE
                            //            }
                            //            else
                            //            {                                            
                            //                //delete - update actual                                        
                            //                objEnt.Nid_ubica = listub[i].IntID;
                            //                objEnt.Co_usuario_cambio = Profile.UserName;
                            //                objEnt.Co_usuario_red = Profile.UsuarioRed;
                            //                objEnt.No_estacion_red = Profile.Estacion;
                            //                //if (ddl_estado.SelectedValue != "--Seleccione--")
                            //                //{
                            //                //    if (ddl_estado.SelectedValue == "0")
                            //                //        objEnt.Fl_activo = "A";
                            //                //    else
                            //                //        objEnt.Fl_activo = "I";
                            //                //}
                            //                //else
                            //                //    objEnt.Fl_activo = "";
                            //                objEnt.Fl_activo = "I";
                            //                objEnt.Op = "D";
                            //                objneg.MantenimientoUsuarioUbicacion(objEnt);
                            //            }
                            //        }
                            //    }
                            //}
                            //else if (listub.Count < lst_ptored_sel_tienda.Items.Count)
                            //{
                            //    for (Int32 i = 0; i < lst_ptored_sel_tienda.Items.Count; i++)
                            //    {
                            //        if (listub.Count == 0)
                            //        {
                            //            //insert
                            //            objEnt.Nid_ubica = Convert.ToInt32(lst_ptored_sel_tienda.Items[i].Value);
                            //            objEnt.Co_usuario_crea = Profile.UserName;
                            //            objEnt.Co_usuario_red = Profile.UsuarioRed;
                            //            //if (ddl_estado.SelectedValue != "--Seleccione--")
                            //            //{
                            //            //    if (ddl_estado.SelectedValue == "0")
                            //            //        objEnt.Fl_activo = "A";
                            //            //    else
                            //            //        objEnt.Fl_activo = "I";
                            //            //}
                            //            //else
                            //            //    objEnt.Fl_activo = "";
                            //            objEnt.Fl_activo = "A";
                            //            objEnt.Op = "I";
                            //            objneg.MantenimientoUsuarioUbicacion(objEnt);                                        
                            //        }
                            //        else
                            //        {
                            //            for (Int32 j = 0; j < listub.Count; j++)
                            //            {
                            //                if (lst_ptored_sel_tienda.Items[i].Value == listub[j].IntID.ToString())
                            //                {
                            //                    _flag = true;
                            //                    break;
                            //                }
                            //                else
                            //                    _flag = false;
                            //            }
                            //            if (_flag)
                            //            {
                            //                //update
                            //            }
                            //            else
                            //            {
                            //                //insert
                            //                objEnt.Nid_ubica = Convert.ToInt32(lst_ptored_sel_tienda.Items[i].Value);
                            //                objEnt.Co_usuario_crea = Profile.UserName;
                            //                objEnt.Co_usuario_red = Profile.UsuarioRed;
                            //                //if (ddl_estado.SelectedValue != "--Seleccione--")
                            //                //{
                            //                //    if (ddl_estado.SelectedValue == "0")
                            //                //        objEnt.Fl_activo = "A";
                            //                //    else
                            //                //        objEnt.Fl_activo = "I";
                            //                //}
                            //                //else
                            //                //    objEnt.Fl_activo = "";
                            //                objEnt.Fl_activo = "A";
                            //                objEnt.Op = "I";
                            //                objneg.MantenimientoUsuarioUbicacion(objEnt); 
                            //            }
                            //        }
                            //    }
                            //}
                            //else if (listub.Count == lst_ptored_sel_tienda.Items.Count)
                            //{
                            //    for (Int32 i = 0; i < listub.Count; i++)
                            //    {
                            //        for (Int32 j = 0; j < lst_ptored_sel_tienda.Items.Count; j++)
                            //        {
                            //            if (listub[i].IntID.ToString() == lst_ptored_sel_tienda.Items[j].Value)
                            //            {
                            //                _flag = true;
                            //                break;
                            //            }
                            //            else
                            //                _flag = false;
                            //        }
                            //        if (_flag)
                            //        {
                            //            //UPDATE
                            //        }
                            //        else
                            //        {                                     
                            //            //delete - update actual                                        
                            //            objEnt.Nid_ubica = listub[i].IntID;
                            //            objEnt.Co_usuario_cambio = Profile.UserName;
                            //            objEnt.Co_usuario_red = Profile.UsuarioRed;
                            //            objEnt.No_estacion_red = Profile.Estacion;
                            //            //if (ddl_estado.SelectedValue != "--Seleccione--")
                            //            //{
                            //            //    if (ddl_estado.SelectedValue == "0")
                            //            //        objEnt.Fl_activo = "A";
                            //            //    else
                            //            //        objEnt.Fl_activo = "I";
                            //            //}
                            //            //else
                            //            //    objEnt.Fl_activo = "";
                            //            objEnt.Fl_activo = "I";
                            //            objEnt.Op = "D";
                            //            objneg.MantenimientoUsuarioUbicacion(objEnt);
                            //        }
                            //    }
                            //    for (Int32 i = 0; i < lst_ptored_sel_tienda.Items.Count; i++)
                            //    {
                            //        for (Int32 j = 0; j < listub.Count; j++)
                            //        {
                            //            if (lst_ptored_sel_tienda.Items[i].Value == listub[j].IntID.ToString())
                            //            {
                            //                _flag = true;
                            //                break;
                            //            }
                            //            else
                            //                _flag = false;
                            //        }
                            //        if (_flag)
                            //        {
                            //            //update
                            //        }
                            //        else
                            //        {
                            //            objEnt.Nid_ubica = Convert.ToInt32(lst_ptored_sel_tienda.Items[i].Value);
                            //            objEnt.Co_usuario_crea = Profile.UserName;
                            //            objEnt.Co_usuario_red = Profile.UsuarioRed;
                            //            //if (ddl_estado.SelectedValue != "--Seleccione--")
                            //            //{
                            //            //    if (ddl_estado.SelectedValue == "0")
                            //            //        objEnt.Fl_activo = "A";
                            //            //    else
                            //            //        objEnt.Fl_activo = "I";
                            //            //}
                            //            //else
                            //            //    objEnt.Fl_activo = "";
                            //            objEnt.Fl_activo = "A";
                            //            objEnt.Op = "I";
                            //            objneg.MantenimientoUsuarioUbicacion(objEnt);
                            //            //insert
                            //        }
                            //    }
                            //}

                            #endregion

                            #region ACTUALIZAR MAE_USR_MODELO - TIENDA

                            MantenimientoModelos(_ID_USUARIO, lst_modelos_sel_call, fl_tipo);

                            #endregion

                        }
                        else if (ddl_perfil.SelectedValue == "AGEN") //general
                        {

                        }
                        else if (ddl_perfil.SelectedValue == "ASRV") //servicio
                        {
                            #region ACTUALIZAR USUARIO_TALLER - SERVICIO

                            oMaestroUsuariosBE = new UsuarioBE();
                            UsuarioTallerBL oMaestroUsuariosBL = new UsuarioTallerBL();

                            oMaestroUsuariosBE.Nid_usuario = _ID_USUARIO;
                            oMaestroUsuariosBE.No_taller = ddl_taller_ase_serv_t.SelectedValue.ToString() + "|";
                            oMaestroUsuariosBE.Co_usuario_crea = Profile.UserName;
                            oMaestroUsuariosBE.Co_usuario_red = Profile.UsuarioRed;
                            oMaestroUsuariosBE.No_estacion_red = Profile.Estacion;
                            oMaestroUsuariosBE.Fl_tipo = fl_tipo;

                            oResp = oMaestroUsuariosBL.MantenimientoUsuarioTaller(oMaestroUsuariosBE);

                            #endregion

                            #region ACTUALIZAR MAE_HORARIO - SERVICIO

                            List<UsuarioBE> list_hora = (List<UsuarioBE>)ViewState["list_horario"];

                            Boolean _flag9 = true;

                            objEnt.Nid_usuario = _ID_USUARIO;

                            if (list_hora.Count > lst_horas_sel.Items.Count)
                            {
                                for (Int32 i = 0; i < list_hora.Count; i++)
                                {
                                    if (lst_horas_sel.Items.Count == 0)
                                    {
                                        //objEnt.Op = "D";
                                        //objEnt.Dd_atencion = Convert.ToInt32(list_hora[i].StrID.Split('|')[0]);
                                        //objEnt.Ho_inicio = list_hora[i].StrID.Split('|')[1];
                                        //objEnt.Ho_fin = list_hora[i].StrID.Split('|')[2];                                        
                                        //objEnt.Fl_tipo = "A";

                                        //DELETE - update actual
                                        objEnt.Op = "D";
                                        objEnt.Dd_atencion = Convert.ToInt32(list_hora[i].StrID.Split('|')[0]);
                                        objEnt.Ho_inicio = list_hora[i].StrID.Split('|')[1];
                                        objEnt.Ho_fin = list_hora[i].StrID.Split('|')[2];
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Fl_activo = "A";
                                        objneg.MantenimientoUsuarioHorario(objEnt);
                                    }
                                    else
                                    {
                                        for (Int32 j = 0; j < lst_horas_sel.Items.Count; j++)
                                        {
                                            if (list_hora[i].StrID == lst_horas_sel.Items[j].Value)
                                            {
                                                _flag9 = true;
                                                break;
                                            }
                                            else
                                                _flag9 = false;
                                        }
                                        if (_flag9)
                                        {
                                            //UPDATE
                                        }
                                        else
                                        {
                                            //DELETE - update actual 
                                            objEnt.Op = "D";
                                            objEnt.Dd_atencion = Convert.ToInt32(list_hora[i].StrID.Split('|')[0]);
                                            objEnt.Ho_inicio = list_hora[i].StrID.Split('|')[1];
                                            objEnt.Ho_fin = list_hora[i].StrID.Split('|')[2];
                                            objEnt.Fl_tipo = "A";
                                            objEnt.Fl_activo = "A";
                                            objneg.MantenimientoUsuarioHorario(objEnt);
                                        }
                                    }
                                }
                                
                                if (list_hora.Count < lst_horas_sel.Items.Count)
                                {
                                    for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
                                    {
                                        if (list_hora.Count == 0)
                                        {
                                            if (lst_horas_sel.Items.Count == 0)
                                            {
                                                //DELETE - update actual
                                                objEnt.Op = "D";
                                                objEnt.Dd_atencion = Convert.ToInt32(list_hora[i].StrID.Split('|')[0]);
                                                objEnt.Ho_inicio = list_hora[i].StrID.Split('|')[1];
                                                objEnt.Ho_fin = list_hora[i].StrID.Split('|')[2];
                                                objEnt.Fl_tipo = "A";
                                                objEnt.Fl_activo = "A";
                                                objneg.MantenimientoUsuarioHorario(objEnt);
                                            }
                                        }
                                        else
                                        {
                                            for (Int32 j = 0; j < list_hora.Count; j++)
                                            {
                                                if (lst_horas_sel.Items[i].Value == list_hora[j].StrID)
                                                {
                                                    _flag9 = true;
                                                    break;
                                                }
                                                else
                                                    _flag9 = false;
                                            }
                                            if (_flag9)
                                            {
                                                //update
                                            }
                                            else
                                            {
                                                //insert
                                                objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[i].Value.Split('|')[0]);
                                                objEnt.Ho_inicio = lst_horas_sel.Items[i].Value.Split('|')[1];
                                                objEnt.Ho_fin = lst_horas_sel.Items[i].Value.Split('|')[2];
                                                objEnt.Fl_tipo = "A";
                                                objEnt.Co_usuario_crea = Profile.UserName;
                                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                                objEnt.No_estacion_red = Profile.Estacion;
                                                objEnt.Fl_activo = "A";
                                                objEnt.Op = "I";
                                                objneg.MantenimientoUsuarioHorario(objEnt); // msg 001
                                            }
                                        }
                                    }
                                }
                                if (lst_horas_sel.Items.Count > 0)
                                {
                                    for (Int32 x = 0; x < lst_horas_sel.Items.Count; x++)
                                    {
                                        objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[x].Value.Split('|')[0]);
                                        objEnt.Ho_inicio = lst_horas_sel.Items[x].Value.Split('|')[1];
                                        objEnt.Ho_fin = lst_horas_sel.Items[x].Value.Split('|')[2];
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Co_usuario_crea = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        objEnt.Fl_activo = "A";
                                        objEnt.Op = "I";
                                        objneg.MantenimientoUsuarioHorario(objEnt); // msg 001
                                    }

                                }
                                
                            }
                            else if (list_hora.Count < lst_horas_sel.Items.Count)
                            {
                                for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
                                {
                                    if (list_hora.Count == 0)
                                    {
                                        //insert
                                        objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[i].Value.Split('|')[0]);
                                        objEnt.Ho_inicio = lst_horas_sel.Items[i].Value.Split('|')[1];
                                        objEnt.Ho_fin = lst_horas_sel.Items[i].Value.Split('|')[2];
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Co_usuario_crea = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        //if (ddl_estado.SelectedValue != "--Seleccione--")
                                        //{
                                        //    if (ddl_estado.SelectedValue == "0")
                                        //        objEnt.Fl_activo = "A";
                                        //    else
                                        //        objEnt.Fl_activo = "I";
                                        //}
                                        //else
                                        //    objEnt.Fl_activo = "";
                                        objEnt.Fl_activo = "A";
                                        objEnt.Op = "I";
                                        objneg.MantenimientoUsuarioHorario(objEnt);
                                    }
                                    else
                                    {
                                        for (Int32 j = 0; j < list_hora.Count; j++)
                                        {
                                            if (lst_horas_sel.Items[i].Value == list_hora[j].StrID)
                                            {
                                                _flag9 = true;
                                                break;
                                            }
                                            else
                                                _flag9 = false;
                                        }
                                        if (_flag9)
                                        {
                                            //update
                                        }
                                        else
                                        {
                                            //insert
                                            objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[i].Value.Split('|')[0]);
                                            objEnt.Ho_inicio = lst_horas_sel.Items[i].Value.Split('|')[1];
                                            objEnt.Ho_fin = lst_horas_sel.Items[i].Value.Split('|')[2];
                                            objEnt.Fl_tipo = "A";
                                            objEnt.Co_usuario_crea = Profile.UserName;
                                            objEnt.Co_usuario_red = Profile.UsuarioRed;
                                            objEnt.No_estacion_red = Profile.Estacion;
                                            //if (ddl_estado.SelectedValue != "--Seleccione--")
                                            //{
                                            //    if (ddl_estado.SelectedValue == "0")
                                            //        objEnt.Fl_activo = "A";
                                            //    else
                                            //        objEnt.Fl_activo = "I";
                                            //}
                                            //else
                                            //    objEnt.Fl_activo = "";
                                            objEnt.Fl_activo = "A";
                                            objEnt.Op = "I";
                                            objneg.MantenimientoUsuarioHorario(objEnt);
                                        }
                                    }
                                }
                            }
                            else if (list_hora.Count == lst_horas_sel.Items.Count)
                            {
                                for (Int32 i = 0; i < list_hora.Count; i++)
                                {
                                    for (Int32 j = 0; j < lst_horas_sel.Items.Count; j++)
                                    {
                                        if (list_hora[i].StrID == lst_horas_sel.Items[j].Value)
                                        {
                                            _flag9 = true;
                                            break;
                                        }
                                        else
                                            _flag9 = false;
                                    }
                                    if (_flag9)
                                    {
                                        //UPDATE
                                    }
                                    else
                                    {
                                        //DELETE - update actual                                            
                                        objEnt.Op = "D";
                                        objEnt.Dd_atencion = Convert.ToInt32(list_hora[i].StrID.Split('|')[0]);
                                        objEnt.Ho_inicio = list_hora[i].StrID.Split('|')[1];
                                        objEnt.Ho_fin = list_hora[i].StrID.Split('|')[2];
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Fl_activo = "A";
                                        objneg.MantenimientoUsuarioHorario(objEnt);
                                    }
                                }
                                for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
                                {
                                    for (Int32 j = 0; j < list_hora.Count; j++)
                                    {
                                        if (lst_horas_sel.Items[i].Value == list_hora[j].StrID)
                                        {
                                            _flag9 = true;
                                            break;
                                        }
                                        else
                                            _flag9 = false;
                                    }
                                    if (_flag9)
                                    {
                                        //update
                                    }
                                    else
                                    {
                                        //insert
                                        objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[i].Value.Split('|')[0]);
                                        objEnt.Ho_inicio = lst_horas_sel.Items[i].Value.Split('|')[1];
                                        objEnt.Ho_fin = lst_horas_sel.Items[i].Value.Split('|')[2];
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Co_usuario_crea = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        //if (ddl_estado.SelectedValue != "--Seleccione--")
                                        //{
                                        //    if (ddl_estado.SelectedValue == "0")
                                        //        objEnt.Fl_activo = "A";
                                        //    else
                                        //        objEnt.Fl_activo = "I";
                                        //}
                                        //else
                                        //    objEnt.Fl_activo = "";
                                        objEnt.Fl_activo = "A";
                                        objEnt.Op = "I";
                                        objneg.MantenimientoUsuarioHorario(objEnt);
                                    }
                                }
                            }

                            #endregion

                            #region ACTUALIZAR MAE_USR_MODELO - SERVICIO

                            MantenimientoModelos(_ID_USUARIO, lst_modelos_sel_ase_serv_t, fl_tipo);

                            #endregion

                            #region ACTUALIZAR MAE_DIAS_EXCEPTUADOS - SERVICIO

                            List<UsuarioBE> List = (List<UsuarioBE>)ViewState["listDiasExcep"];

                            Boolean _flag2 = true;

                            if (Session["Nid_usuario_editar"] != null)
                                objEnt.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
                            else if (Session["Nid_usuario_detalle"] != null)
                                objEnt.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());

                            if (List.Count > lst_dias_excep.Items.Count)
                            {
                                for (Int32 i = 0; i < List.Count; i++)
                                {
                                    if (lst_dias_excep.Items.Count == 0)
                                    {
                                        //delete - update actual
                                        objEnt.Co_usuario_cambio = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        objEnt.Op = "D";
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Fl_activo = "I";
                                        objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].StrID);
                                        objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                    }
                                    else
                                    {
                                        for (Int32 j = 0; j < lst_dias_excep.Items.Count; j++)
                                        {
                                            if (List[i].StrID == lst_dias_excep.Items[j].Value)
                                            {
                                                _flag2 = true;
                                                break;
                                            }
                                            else
                                                _flag2 = false;
                                        }
                                        if (_flag2)
                                        {
                                            //UPDATE
                                        }
                                        else
                                        {
                                            ////DELETE
                                            //objEnt.Op = "D";
                                            //objEnt.Fl_tipo = "A";
                                            //objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].StrID);
                                            //objneg.MantenimientoDiasExceptuados_Usuario(objEnt);

                                            //delete - update actual
                                            objEnt.Co_usuario_cambio = Profile.UserName;
                                            objEnt.Co_usuario_red = Profile.UsuarioRed;
                                            objEnt.No_estacion_red = Profile.Estacion;
                                            objEnt.Op = "D";
                                            objEnt.Fl_tipo = "A";
                                            objEnt.Fl_activo = "I";
                                            objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].StrID);
                                            objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                        }
                                    }
                                }
                            }
                            else if (List.Count < lst_dias_excep.Items.Count)
                            {
                                for (Int32 i = 0; i < lst_dias_excep.Items.Count; i++)
                                {
                                    if (List.Count == 0)
                                    {
                                        //insert
                                        objEnt.Fe_exceptuada = Convert.ToDateTime(lst_dias_excep.Items[i].Value);
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Co_usuario_crea = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        //if (ddl_estado.SelectedValue != "--Seleccione--")
                                        //{
                                        //    if (ddl_estado.SelectedValue == "0")
                                        //        objEnt.Fl_activo = "A";
                                        //    else
                                        //        objEnt.Fl_activo = "I";
                                        //}
                                        //else
                                        //    objEnt.Fl_activo = "";
                                        objEnt.Fl_activo = "A";
                                        objEnt.Op = "I";
                                        objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                    }
                                    else
                                    {
                                        for (Int32 j = 0; j < List.Count; j++)
                                        {
                                            if (lst_dias_excep.Items[i].Value == List[j].StrID)
                                            {
                                                _flag2 = true;
                                                break;
                                            }
                                            else
                                                _flag2 = false;
                                        }
                                        if (_flag2)
                                        {
                                            //update
                                        }
                                        else
                                        {
                                            //insert
                                            objEnt.Fe_exceptuada = Convert.ToDateTime(lst_dias_excep.Items[i].Value);
                                            objEnt.Fl_tipo = "A";
                                            objEnt.Co_usuario_crea = Profile.UserName;
                                            objEnt.Co_usuario_red = Profile.UsuarioRed;
                                            objEnt.No_estacion_red = Profile.Estacion;
                                            //if (ddl_estado.SelectedValue != "--Seleccione--")
                                            //{
                                            //    if (ddl_estado.SelectedValue == "0")
                                            //        objEnt.Fl_activo = "A";
                                            //    else
                                            //        objEnt.Fl_activo = "I";
                                            //}
                                            //else
                                            //    objEnt.Fl_activo = "";
                                            objEnt.Fl_activo = "A";
                                            objEnt.Op = "I";
                                            objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                        }
                                    }
                                }
                            }
                            else if (List.Count == lst_dias_excep.Items.Count)
                            {
                                for (Int32 i = 0; i < List.Count; i++)
                                {
                                    for (Int32 j = 0; j < lst_dias_excep.Items.Count; j++)
                                    {
                                        if (List[i].StrID == lst_dias_excep.Items[j].Value)
                                        {
                                            _flag2 = true;
                                            break;
                                        }
                                        else
                                            _flag2 = false;
                                    }
                                    if (_flag2)
                                    {
                                        //UPDATE
                                    }
                                    else
                                    {
                                        //delete - update actual
                                        objEnt.Co_usuario_cambio = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        objEnt.Op = "D";
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Fl_activo = "I";
                                        objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].StrID);
                                        objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                    }
                                }


                                for (Int32 i = 0; i < lst_dias_excep.Items.Count; i++)
                                {
                                    for (Int32 j = 0; i < List.Count; j++)
                                    {
                                        if (lst_dias_excep.Items[i].Value == List[j].StrID)
                                        {
                                            _flag2 = true;
                                            break;
                                        }
                                        else
                                            _flag2 = false;
                                    }
                                    if (_flag2)
                                    {
                                        //update
                                    }
                                    else
                                    {
                                        //insert
                                        objEnt.Fe_exceptuada = Convert.ToDateTime(lst_dias_excep.Items[i].Value);
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Co_usuario_crea = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        //if (ddl_estado.SelectedValue != "--Seleccione--")
                                        //{
                                        //    if (ddl_estado.SelectedValue == "0")
                                        //        objEnt.Fl_activo = "A";
                                        //    else
                                        //        objEnt.Fl_activo = "I";
                                        //}
                                        //else
                                        //    objEnt.Fl_activo = "";
                                        objEnt.Fl_activo = "A";
                                        objEnt.Op = "I";
                                        objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                    }
                                }
                            }

                            #endregion

                            #region ACTUALIZAR MAE_USR_SERVICIO - SERVICIO

                            MantenimientoServicios(_ID_USUARIO, lst_servicio_espec_sel_s, fl_tipo);

                            #endregion

                            #region ACTUALIZAR MAE_CAPACIDAD_ATENCION


                            if (hfCapacidad.Value.ToString().Length > 0)
                            {
                                objEnt.Nid_usuario = int.Parse((Session["Nid_usuario_editar"] == null) ? Session["Nid_usuario_detalle"].ToString() : Session["Nid_usuario_editar"].ToString());
                                objEnt.Co_usuario_crea = Profile.UserName;
                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                objEnt.No_estacion_red = Profile.Estacion;


                                if (ddl_dia.SelectedIndex > 0)
                                {
                                    Int32 intResp = objneg.InhabilitarCapacidadAtencion_Usuario(objEnt);
                                }

                                string strTmpCapacidad = string.Empty;
                                string[] strCapacidaDias;

                                strCapacidaDias = hfCapacidad.Value.ToString().Split('|');

                                foreach (string strCapacidadDia in strCapacidaDias)
                                {
                                    if (strCapacidadDia.Trim().Length == 0) continue;

                                    string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                                    string strC_Tipo = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? "T" : "I") : strCapacidadDia.Split('-').GetValue(1).ToString();
                                    string strC_FO = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : txt_capacidad_fo.Text) : strCapacidadDia.Split('-').GetValue(2).ToString();
                                    string strC_BO = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : txt_capacidad_bo.Text) : strCapacidadDia.Split('-').GetValue(3).ToString();
                                    string strC_Total = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : "") : strCapacidadDia.Split('-').GetValue(4).ToString();

                                    if (!String.IsNullOrEmpty(strC_FO) || !String.IsNullOrEmpty(strC_BO) || !String.IsNullOrEmpty(strC_Total))
                                    {
                                        oMaestroUsuariosBE.Dd_atencion = Convert.ToInt32(strC_Dia);
                                        oMaestroUsuariosBE.fl_control = strC_Tipo;
                                        oMaestroUsuariosBE.qt_capacidad_fo = String.IsNullOrEmpty(strC_FO) ? -1 : Convert.ToInt32(strC_FO);
                                        oMaestroUsuariosBE.qt_capacidad_bo = String.IsNullOrEmpty(strC_BO) ? -1 : Convert.ToInt32(strC_BO);
                                        oMaestroUsuariosBE.qt_capacidad = String.IsNullOrEmpty(strC_Total) ? -1 : Convert.ToInt32(strC_Total);

                                        oResp = oMaestroUsuariosBL.MantenimientoCapacidadAtencion_Usuario(oMaestroUsuariosBE);
                                    }
                                }
                            }

                            #endregion

                            //---------------------------------------------------------------------------
                            //Enviar Email a Call Center si se Inhabilita al Asesor
                            #region ENVIO EMAIL SI ES INACTIVO EL ASESOR

                            if (ddl_estado.SelectedValue.ToString().ToUpper().Equals("I"))
                            {
                                try
                                {
                                    //objEnt.Fl_inactivo = (ddl_estado.SelectedValue != "--Seleccione--" ? ddl_estado.SelectedValue : "");
                                    string strAsesor = txt_nombres.Text.Trim() + " " + txt_ape_paterno.Text.Trim() + " " + txt_ape_materno.Text.Trim();

                                    System.Text.StringBuilder strBody = null;

                                    string strTexto = string.Empty;

                                    strTexto += "<br>Se notifica que el Asesor de Servicio: <br><h1> " + strAsesor.ToUpper() + " </h1>";
                                    strTexto += "<br>ya no se ecnuentra activo.";
                                    strTexto += "<br>";
                                    strTexto += "<br>Gildemeister.";

                                    strBody.Append(strTexto);

                                    System.Net.Mail.MailMessage oEmail = new System.Net.Mail.MailMessage();
                                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

                                    try
                                    {
                                        oEmail.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["MailAddress"], ConfigurationManager.AppSettings["DisplayName"]);

                                        oEmail.To.Add(ConfigurationManager.AppSettings["EmailCallCenter"]);
                                        oEmail.Subject = "Movimiento de Asesor";
                                        oEmail.Body = strBody.ToString();
                                        oEmail.IsBodyHtml = true;
                                        oEmail.Priority = System.Net.Mail.MailPriority.High;

                                        smtp.Host = ConfigurationManager.AppSettings["Host"];
                                        smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());
                                        smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UsuarioMail"], ConfigurationManager.AppSettings["ClaveMail"]);

                                        smtp.Send(oEmail);
                                    }
                                    catch //(Exception ex)
                                    {

                                    }
                                    finally
                                    {
                                        oEmail = null;
                                        smtp = null;
                                    }
                                }
                                catch //(Exception ex)
                                {
                                    // code ex
                                }

                            }

                            #endregion

                            //---------------------------------------------------------------------------



                            //@001 - I
                            oParametros = new Parametros();
                            if (oParametros.SRC_Pais.Equals(2))
                            {
                                CitasBE oCitas = new CitasBE();
                                oCitas.Nid_usuario = _ID_USUARIO;
                                oCitas.nid_modulo = Convert.ToInt32(ddlModulo.SelectedValue.ToString());
                                oCitas.co_usuario_crea = Profile.UserName;
                                oCitas.co_usuario_red = Profile.UsuarioRed;
                                oCitas.no_estacion_red = Profile.Estacion;

                                Int32 oResp2 = objneg.ActualizarAsesorModulo(oCitas);
                            }
                            //@001 - F
                        }
                        else if (ddl_perfil.SelectedValue == "CALL") //call 
                        {
                            #region ACTUALIZAR MAE_USUARIO MODELO - CALL CENTER

                            MantenimientoModelos(_ID_USUARIO, lst_modelos_sel_call, fl_tipo);

                            #endregion

                            #region ACTUALIZAR MAE_USUARIO_TALLER - CALL CENTER

                            MantenimientoTalleres(_ID_USUARIO, lst_talleres_sel_call, fl_tipo);

                            #endregion

                            #region ACTUALIZAR MAE_USUARIO_SERVICIO - SERVICIO

                            MantenimientoServicios(_ID_USUARIO, lst_servicio_espec_sel_cc, fl_tipo);

                            #endregion
                        }
                        //@004 I
                        else if (ddl_perfil.SelectedValue == "MECA") //mecanico
                        {
                            #region ACTUALIZAR USUARIO_TALLER - SERVICIO

                            oMaestroUsuariosBE = new UsuarioBE();
                            UsuarioTallerBL oMaestroUsuariosBL = new UsuarioTallerBL();

                            oMaestroUsuariosBE.Nid_usuario = _ID_USUARIO;
                            oMaestroUsuariosBE.No_taller = ddl_taller_ase_serv_t.SelectedValue.ToString() + "|";
                            oMaestroUsuariosBE.Co_usuario_crea = Profile.UserName;
                            oMaestroUsuariosBE.Co_usuario_red = Profile.UsuarioRed;
                            oMaestroUsuariosBE.No_estacion_red = Profile.Estacion;
                            oMaestroUsuariosBE.Fl_tipo = fl_tipo;

                            oResp = oMaestroUsuariosBL.MantenimientoUsuarioTaller(oMaestroUsuariosBE);

                            #endregion

                            #region ACTUALIZAR MAE_HORARIO - SERVICIO

                            List<UsuarioBE> list_hora = (List<UsuarioBE>)ViewState["list_horario"];

                            Boolean _flag9 = true;

                            objEnt.Nid_usuario = _ID_USUARIO;

                            if (list_hora.Count > lst_horas_sel.Items.Count)
                            {
                                for (Int32 i = 0; i < list_hora.Count; i++)
                                {
                                    if (lst_horas_sel.Items.Count == 0)
                                    {
                                        //objEnt.Op = "D";
                                        //objEnt.Dd_atencion = Convert.ToInt32(list_hora[i].StrID.Split('|')[0]);
                                        //objEnt.Ho_inicio = list_hora[i].StrID.Split('|')[1];
                                        //objEnt.Ho_fin = list_hora[i].StrID.Split('|')[2];                                        
                                        //objEnt.Fl_tipo = "A";

                                        //DELETE - update actual
                                        objEnt.Op = "D";
                                        objEnt.Dd_atencion = Convert.ToInt32(list_hora[i].StrID.Split('|')[0]);
                                        objEnt.Ho_inicio = list_hora[i].StrID.Split('|')[1];
                                        objEnt.Ho_fin = list_hora[i].StrID.Split('|')[2];
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Fl_activo = "A";
                                        objneg.MantenimientoUsuarioHorario(objEnt);
                                    }
                                    else
                                    {
                                        for (Int32 j = 0; j < lst_horas_sel.Items.Count; j++)
                                        {
                                            if (list_hora[i].StrID == lst_horas_sel.Items[j].Value)
                                            {
                                                _flag9 = true;
                                                break;
                                            }
                                            else
                                                _flag9 = false;
                                        }
                                        if (_flag9)
                                        {
                                            //UPDATE
                                        }
                                        else
                                        {
                                            //DELETE - update actual 
                                            objEnt.Op = "D";
                                            objEnt.Dd_atencion = Convert.ToInt32(list_hora[i].StrID.Split('|')[0]);
                                            objEnt.Ho_inicio = list_hora[i].StrID.Split('|')[1];
                                            objEnt.Ho_fin = list_hora[i].StrID.Split('|')[2];
                                            objEnt.Fl_tipo = "A";
                                            objEnt.Fl_activo = "A";
                                            objneg.MantenimientoUsuarioHorario(objEnt);
                                        }
                                    }
                                }
                                
                                if (list_hora.Count < lst_horas_sel.Items.Count)
                                {
                                    for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
                                    {
                                        if (list_hora.Count == 0)
                                        {
                                            if (lst_horas_sel.Items.Count == 0)
                                            {
                                                //DELETE - update actual
                                                objEnt.Op = "D";
                                                objEnt.Dd_atencion = Convert.ToInt32(list_hora[i].StrID.Split('|')[0]);
                                                objEnt.Ho_inicio = list_hora[i].StrID.Split('|')[1];
                                                objEnt.Ho_fin = list_hora[i].StrID.Split('|')[2];
                                                objEnt.Fl_tipo = "A";
                                                objEnt.Fl_activo = "A";
                                                objneg.MantenimientoUsuarioHorario(objEnt);
                                            }
                                        }
                                        else
                                        {
                                            for (Int32 j = 0; j < list_hora.Count; j++)
                                            {
                                                if (lst_horas_sel.Items[i].Value == list_hora[j].StrID)
                                                {
                                                    _flag9 = true;
                                                    break;
                                                }
                                                else
                                                    _flag9 = false;
                                            }
                                            if (_flag9)
                                            {
                                                //update
                                            }
                                            else
                                            {
                                                //insert
                                                objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[i].Value.Split('|')[0]);
                                                objEnt.Ho_inicio = lst_horas_sel.Items[i].Value.Split('|')[1];
                                                objEnt.Ho_fin = lst_horas_sel.Items[i].Value.Split('|')[2];
                                                objEnt.Fl_tipo = "A";
                                                objEnt.Co_usuario_crea = Profile.UserName;
                                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                                objEnt.No_estacion_red = Profile.Estacion;
                                                objEnt.Fl_activo = "A";
                                                objEnt.Op = "I";
                                                objneg.MantenimientoUsuarioHorario(objEnt); // msg 001
                                            }
                                        }
                                    }
                                }
                                if (lst_horas_sel.Items.Count > 0)
                                {
                                    for (Int32 x = 0; x < lst_horas_sel.Items.Count; x++)
                                    {
                                        objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[x].Value.Split('|')[0]);
                                        objEnt.Ho_inicio = lst_horas_sel.Items[x].Value.Split('|')[1];
                                        objEnt.Ho_fin = lst_horas_sel.Items[x].Value.Split('|')[2];
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Co_usuario_crea = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        objEnt.Fl_activo = "A";
                                        objEnt.Op = "I";
                                        objneg.MantenimientoUsuarioHorario(objEnt); // msg 001
                                    }

                                }
                                
                            }
                            else if (list_hora.Count < lst_horas_sel.Items.Count)
                            {
                                for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
                                {
                                    if (list_hora.Count == 0)
                                    {
                                        //insert
                                        objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[i].Value.Split('|')[0]);
                                        objEnt.Ho_inicio = lst_horas_sel.Items[i].Value.Split('|')[1];
                                        objEnt.Ho_fin = lst_horas_sel.Items[i].Value.Split('|')[2];
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Co_usuario_crea = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        //if (ddl_estado.SelectedValue != "--Seleccione--")
                                        //{
                                        //    if (ddl_estado.SelectedValue == "0")
                                        //        objEnt.Fl_activo = "A";
                                        //    else
                                        //        objEnt.Fl_activo = "I";
                                        //}
                                        //else
                                        //    objEnt.Fl_activo = "";
                                        objEnt.Fl_activo = "A";
                                        objEnt.Op = "I";
                                        objneg.MantenimientoUsuarioHorario(objEnt);
                                    }
                                    else
                                    {
                                        for (Int32 j = 0; j < list_hora.Count; j++)
                                        {
                                            if (lst_horas_sel.Items[i].Value == list_hora[j].StrID)
                                            {
                                                _flag9 = true;
                                                break;
                                            }
                                            else
                                                _flag9 = false;
                                        }
                                        if (_flag9)
                                        {
                                            //update
                                        }
                                        else
                                        {
                                            //insert
                                            objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[i].Value.Split('|')[0]);
                                            objEnt.Ho_inicio = lst_horas_sel.Items[i].Value.Split('|')[1];
                                            objEnt.Ho_fin = lst_horas_sel.Items[i].Value.Split('|')[2];
                                            objEnt.Fl_tipo = "A";
                                            objEnt.Co_usuario_crea = Profile.UserName;
                                            objEnt.Co_usuario_red = Profile.UsuarioRed;
                                            objEnt.No_estacion_red = Profile.Estacion;
                                            //if (ddl_estado.SelectedValue != "--Seleccione--")
                                            //{
                                            //    if (ddl_estado.SelectedValue == "0")
                                            //        objEnt.Fl_activo = "A";
                                            //    else
                                            //        objEnt.Fl_activo = "I";
                                            //}
                                            //else
                                            //    objEnt.Fl_activo = "";
                                            objEnt.Fl_activo = "A";
                                            objEnt.Op = "I";
                                            objneg.MantenimientoUsuarioHorario(objEnt);
                                        }
                                    }
                                }
                            }
                            else if (list_hora.Count == lst_horas_sel.Items.Count)
                            {
                                for (Int32 i = 0; i < list_hora.Count; i++)
                                {
                                    for (Int32 j = 0; j < lst_horas_sel.Items.Count; j++)
                                    {
                                        if (list_hora[i].StrID == lst_horas_sel.Items[j].Value)
                                        {
                                            _flag9 = true;
                                            break;
                                        }
                                        else
                                            _flag9 = false;
                                    }
                                    if (_flag9)
                                    {
                                        //UPDATE
                                    }
                                    else
                                    {
                                        //DELETE - update actual                                            
                                        objEnt.Op = "D";
                                        objEnt.Dd_atencion = Convert.ToInt32(list_hora[i].StrID.Split('|')[0]);
                                        objEnt.Ho_inicio = list_hora[i].StrID.Split('|')[1];
                                        objEnt.Ho_fin = list_hora[i].StrID.Split('|')[2];
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Fl_activo = "A";
                                        objneg.MantenimientoUsuarioHorario(objEnt);
                                    }
                                }
                                for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
                                {
                                    for (Int32 j = 0; j < list_hora.Count; j++)
                                    {
                                        if (lst_horas_sel.Items[i].Value == list_hora[j].StrID)
                                        {
                                            _flag9 = true;
                                            break;
                                        }
                                        else
                                            _flag9 = false;
                                    }
                                    if (_flag9)
                                    {
                                        //update
                                    }
                                    else
                                    {
                                        //insert
                                        objEnt.Dd_atencion = Convert.ToInt32(lst_horas_sel.Items[i].Value.Split('|')[0]);
                                        objEnt.Ho_inicio = lst_horas_sel.Items[i].Value.Split('|')[1];
                                        objEnt.Ho_fin = lst_horas_sel.Items[i].Value.Split('|')[2];
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Co_usuario_crea = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        //if (ddl_estado.SelectedValue != "--Seleccione--")
                                        //{
                                        //    if (ddl_estado.SelectedValue == "0")
                                        //        objEnt.Fl_activo = "A";
                                        //    else
                                        //        objEnt.Fl_activo = "I";
                                        //}
                                        //else
                                        //    objEnt.Fl_activo = "";
                                        objEnt.Fl_activo = "A";
                                        objEnt.Op = "I";
                                        objneg.MantenimientoUsuarioHorario(objEnt);
                                    }
                                }
                            }

                            #endregion

                            #region ACTUALIZAR MAE_DIAS_EXCEPTUADOS - SERVICIO

                            List<UsuarioBE> List = (List<UsuarioBE>)ViewState["listDiasExcep"];

                            Boolean _flag2 = true;

                            if (Session["Nid_usuario_editar"] != null)
                                objEnt.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_editar"].ToString());
                            else if (Session["Nid_usuario_detalle"] != null)
                                objEnt.Nid_usuario = Convert.ToInt32(Session["Nid_usuario_detalle"].ToString());

                            if (List.Count > lst_dias_excep.Items.Count)
                            {
                                for (Int32 i = 0; i < List.Count; i++)
                                {
                                    if (lst_dias_excep.Items.Count == 0)
                                    {
                                        //delete - update actual
                                        objEnt.Co_usuario_cambio = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        objEnt.Op = "D";
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Fl_activo = "I";
                                        objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].StrID);
                                        objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                    }
                                    else
                                    {
                                        for (Int32 j = 0; j < lst_dias_excep.Items.Count; j++)
                                        {
                                            if (List[i].StrID == lst_dias_excep.Items[j].Value)
                                            {
                                                _flag2 = true;
                                                break;
                                            }
                                            else
                                                _flag2 = false;
                                        }
                                        if (_flag2)
                                        {
                                            //UPDATE
                                        }
                                        else
                                        {
                                            ////DELETE
                                            //objEnt.Op = "D";
                                            //objEnt.Fl_tipo = "A";
                                            //objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].StrID);
                                            //objneg.MantenimientoDiasExceptuados_Usuario(objEnt);

                                            //delete - update actual
                                            objEnt.Co_usuario_cambio = Profile.UserName;
                                            objEnt.Co_usuario_red = Profile.UsuarioRed;
                                            objEnt.No_estacion_red = Profile.Estacion;
                                            objEnt.Op = "D";
                                            objEnt.Fl_tipo = "A";
                                            objEnt.Fl_activo = "I";
                                            objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].StrID);
                                            objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                        }
                                    }
                                }
                            }
                            else if (List.Count < lst_dias_excep.Items.Count)
                            {
                                for (Int32 i = 0; i < lst_dias_excep.Items.Count; i++)
                                {
                                    if (List.Count == 0)
                                    {
                                        //insert
                                        objEnt.Fe_exceptuada = Convert.ToDateTime(lst_dias_excep.Items[i].Value);
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Co_usuario_crea = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        //if (ddl_estado.SelectedValue != "--Seleccione--")
                                        //{
                                        //    if (ddl_estado.SelectedValue == "0")
                                        //        objEnt.Fl_activo = "A";
                                        //    else
                                        //        objEnt.Fl_activo = "I";
                                        //}
                                        //else
                                        //    objEnt.Fl_activo = "";
                                        objEnt.Fl_activo = "A";
                                        objEnt.Op = "I";
                                        objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                    }
                                    else
                                    {
                                        for (Int32 j = 0; j < List.Count; j++)
                                        {
                                            if (lst_dias_excep.Items[i].Value == List[j].StrID)
                                            {
                                                _flag2 = true;
                                                break;
                                            }
                                            else
                                                _flag2 = false;
                                        }
                                        if (_flag2)
                                        {
                                            //update
                                        }
                                        else
                                        {
                                            //insert
                                            objEnt.Fe_exceptuada = Convert.ToDateTime(lst_dias_excep.Items[i].Value);
                                            objEnt.Fl_tipo = "A";
                                            objEnt.Co_usuario_crea = Profile.UserName;
                                            objEnt.Co_usuario_red = Profile.UsuarioRed;
                                            objEnt.No_estacion_red = Profile.Estacion;
                                            //if (ddl_estado.SelectedValue != "--Seleccione--")
                                            //{
                                            //    if (ddl_estado.SelectedValue == "0")
                                            //        objEnt.Fl_activo = "A";
                                            //    else
                                            //        objEnt.Fl_activo = "I";
                                            //}
                                            //else
                                            //    objEnt.Fl_activo = "";
                                            objEnt.Fl_activo = "A";
                                            objEnt.Op = "I";
                                            objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                        }
                                    }
                                }
                            }
                            else if (List.Count == lst_dias_excep.Items.Count)
                            {
                                for (Int32 i = 0; i < List.Count; i++)
                                {
                                    for (Int32 j = 0; j < lst_dias_excep.Items.Count; j++)
                                    {
                                        if (List[i].StrID == lst_dias_excep.Items[j].Value)
                                        {
                                            _flag2 = true;
                                            break;
                                        }
                                        else
                                            _flag2 = false;
                                    }
                                    if (_flag2)
                                    {
                                        //UPDATE
                                    }
                                    else
                                    {
                                        //delete - update actual
                                        objEnt.Co_usuario_cambio = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        objEnt.Op = "D";
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Fl_activo = "I";
                                        objEnt.Fe_exceptuada = Convert.ToDateTime(List[i].StrID);
                                        objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                    }
                                }


                                for (Int32 i = 0; i < lst_dias_excep.Items.Count; i++)
                                {
                                    for (Int32 j = 0; i < List.Count; j++)
                                    {
                                        if (lst_dias_excep.Items[i].Value == List[j].StrID)
                                        {
                                            _flag2 = true;
                                            break;
                                        }
                                        else
                                            _flag2 = false;
                                    }
                                    if (_flag2)
                                    {
                                        //update
                                    }
                                    else
                                    {
                                        //insert
                                        objEnt.Fe_exceptuada = Convert.ToDateTime(lst_dias_excep.Items[i].Value);
                                        objEnt.Fl_tipo = "A";
                                        objEnt.Co_usuario_crea = Profile.UserName;
                                        objEnt.Co_usuario_red = Profile.UsuarioRed;
                                        objEnt.No_estacion_red = Profile.Estacion;
                                        //if (ddl_estado.SelectedValue != "--Seleccione--")
                                        //{
                                        //    if (ddl_estado.SelectedValue == "0")
                                        //        objEnt.Fl_activo = "A";
                                        //    else
                                        //        objEnt.Fl_activo = "I";
                                        //}
                                        //else
                                        //    objEnt.Fl_activo = "";
                                        objEnt.Fl_activo = "A";
                                        objEnt.Op = "I";
                                        objneg.MantenimientoDiasExceptuados_Usuario(objEnt);
                                    }
                                }
                            }

                            #endregion

                            #region ACTUALIZAR MAE_CAPACIDAD_ATENCION


                            if (hfCapacidad.Value.ToString().Length > 0)
                            {
                                objEnt.Nid_usuario = int.Parse((Session["Nid_usuario_editar"] == null) ? Session["Nid_usuario_detalle"].ToString() : Session["Nid_usuario_editar"].ToString());
                                objEnt.Co_usuario_crea = Profile.UserName;
                                objEnt.Co_usuario_red = Profile.UsuarioRed;
                                objEnt.No_estacion_red = Profile.Estacion;


                                if (ddl_dia.SelectedIndex > 0)
                                {
                                    Int32 intResp = objneg.InhabilitarCapacidadAtencion_Usuario(objEnt);
                                }

                                string strTmpCapacidad = string.Empty;
                                string[] strCapacidaDias;

                                strCapacidaDias = hfCapacidad.Value.ToString().Split('|');

                                foreach (string strCapacidadDia in strCapacidaDias)
                                {
                                    if (strCapacidadDia.Trim().Length == 0) continue;

                                    string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                                    string strC_Tipo = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? "T" : "I") : strCapacidadDia.Split('-').GetValue(1).ToString();
                                    string strC_FO = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : txt_capacidad_fo.Text) : strCapacidadDia.Split('-').GetValue(2).ToString();
                                    string strC_BO = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : txt_capacidad_bo.Text) : strCapacidadDia.Split('-').GetValue(3).ToString();
                                    string strC_Total = (ddl_dia.SelectedIndex == 0) ? ((chkTotal.Checked) ? txt_capacidad.Text : "") : strCapacidadDia.Split('-').GetValue(4).ToString();

                                    if (!String.IsNullOrEmpty(strC_FO) || !String.IsNullOrEmpty(strC_BO) || !String.IsNullOrEmpty(strC_Total))
                                    {
                                        oMaestroUsuariosBE.Dd_atencion = Convert.ToInt32(strC_Dia);
                                        oMaestroUsuariosBE.fl_control = strC_Tipo;
                                        oMaestroUsuariosBE.qt_capacidad_fo = String.IsNullOrEmpty(strC_FO) ? -1 : Convert.ToInt32(strC_FO);
                                        oMaestroUsuariosBE.qt_capacidad_bo = String.IsNullOrEmpty(strC_BO) ? -1 : Convert.ToInt32(strC_BO);
                                        oMaestroUsuariosBE.qt_capacidad = String.IsNullOrEmpty(strC_Total) ? -1 : Convert.ToInt32(strC_Total);

                                        oResp = oMaestroUsuariosBL.MantenimientoCapacidadAtencion_Usuario(oMaestroUsuariosBE);
                                    }
                                }
                            }

                            #endregion

                        }
                        //@004 F
                        objEnt = null; objneg = null;
                        Session["Nid_usuario_nuevo"] = null; Session["Nid_usuario_detalle"] = null; Session["Nid_usuario_editar"] = null;
                        lbl_mensajebox.Text = "El registro se actualizo con exito.";
                        popup_msgbox_confirm.Show();
                    }
                }
                catch { }
                #endregion
            }
        }
    }

    private String ExisteLogin(String login)
    {
        UsuarioTallerBL neg = new UsuarioTallerBL();
        UsuarioBE ent = new UsuarioBE();
        ent.CUSR_ID = login;
        return neg.ExisteLogin(ent);
    }

    private Boolean HayErrorEnDatosGenerales()
    {
        if (Session["Nid_usuario_editar"] != null || Session["Nid_usuario_detalle"] != null)
        {
            if (ViewState["CUSR_ID"].ToString() != txt_login.Text.Trim())
            {
                if (ExisteLogin(txt_login.Text.Trim()) == "1")
                {
                    lbl_mensajebox.Text = "Login ya Existe. Ingrese Otro por favor.";
                    popup_msgbox_confirm.Show();
                    ViewState["existe_login"] = "1";
                    return true;
                }
            }
        }
        else if (Session["Nid_usuario_nuevo"] != null)
        {
            if (ExisteLogin(txt_login.Text.Trim()) == "1")
            {
                lbl_mensajebox.Text = "Login ya Existe. Ingrese Otro por favor.";
                popup_msgbox_confirm.Show();
                ViewState["existe_login"] = "1";
                return true;
            }
        }
        ViewState["existe_login"] = "0";

        if (ddl_perfil.SelectedValue == "ASRV") { if (ddl_dia.SelectedIndex != 0) ddl_dia_SelectedIndexChanged(this, null); }

        return false;
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["Nid_usuario_nuevo"] = null;
            Session["Nid_usuario_editar"] = null;
            Session["Nid_usuario_detalle"] = null;
            Response.Redirect("SRC_Maestro_Usuarios.aspx");
        }
        catch { }
    }

    protected void ddl_departamento_dg_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarProvinciaPorDepartamentoSeleccionado(ref ddl_departamento_dg, ref ddl_provincia_dg, ref ddl_distrito_dg);
        //----------
        ddl_ubicacion_dg.Items.Clear();
        ddl_ubicacion_dg.Items.Insert(0, "--Seleccione--");
        ddl_ubicacion_dg.Enabled = false;
        //----------


        ddl_departamento_ase_serv_t.SelectedValue = ddl_departamento_dg.SelectedValue;
    }

    private void CargarProvinciaPorDepartamentoSeleccionado(ref DropDownList ddl_dpto, ref DropDownList ddl_prov, ref DropDownList ddl_dist)
    {
        ddl_prov.Items.Clear();
        ddl_prov.Enabled = false;

        ddl_provincia_ases_serv_t.Items.Clear();
        ddl_provincia_ases_serv_t.Enabled = false;


        if (ddl_dpto.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtubigeo"]).Select("codprov <> '00' AND coddist='00' AND coddpto='" + ddl_dpto.SelectedValue + "'", "nombre", DataViewRowState.CurrentRows);
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_prov.Items.Add("");
                ddl_prov.Items[i].Value = oRow[i]["codprov"].ToString();
                ddl_prov.Items[i].Text = oRow[i]["nombre"].ToString();

                ddl_provincia_ases_serv_t.Items.Add("");
                ddl_provincia_ases_serv_t.Items[i].Value = oRow[i]["codprov"].ToString();
                ddl_provincia_ases_serv_t.Items[i].Text = oRow[i]["nombre"].ToString();
            }

            if (ddl_prov.Items.Count > 0)//  antes > 1 
            {
                ddl_prov.Items.Insert(0, "--Seleccione--");
                ddl_prov.SelectedIndex = 0;
                ddl_prov.AutoPostBack = true;
                ddl_prov.Enabled = true;

                ddl_provincia_ases_serv_t.Items.Insert(0, "--Seleccione--");
                ddl_provincia_ases_serv_t.SelectedIndex = 0;
                ddl_provincia_ases_serv_t.AutoPostBack = true;
                ddl_provincia_ases_serv_t.Enabled = false;
            }


            ddl_dist.Items.Insert(0, "--Seleccione--");
            ddl_dist.SelectedIndex = 0;
            ddl_dist.Enabled = false;

            ddl_distrito_ase_serv_t.Items.Insert(0, "--Seleccione--");
            ddl_distrito_ase_serv_t.SelectedIndex = 0;
            ddl_distrito_ase_serv_t.Enabled = false;

            if (ddl_dpto.ID.ToUpper().Contains("DDL_DEPARTAMENTO_TIENDA"))
            {
                ddl_tipoptored_tienda.Items.Insert(0, "--Seleccione--");
                ddl_tipoptored_tienda.SelectedIndex = 0;
                ddl_tipoptored_tienda.Enabled = false;
                lst_ptored_tienda.Items.Clear();
            }

            //if (ddl_dpto.ID.ToUpper().Contains("DDL_DEPARTAMENTO_ASE_SERV_T"))
            //{
            //    ddl_ptored_ase_serv_t.Items.Insert(0, "--Seleccione--");
            //    ddl_ptored_ase_serv_t.SelectedIndex = 0;
            //    ddl_ptored_ase_serv_t.Enabled = false;
            //    ddl_taller_ase_serv_t.Items.Insert(0, "--Seleccione--");
            //    ddl_taller_ase_serv_t.SelectedIndex = 0;
            //    ddl_taller_ase_serv_t.Enabled = false;
            //}

            if (ddl_dpto.ID.ToUpper().Contains("DDL_DEPARTAMENTO_TALLER"))
            {
                ddl_ptored_taller.Items.Insert(0, "--Seleccione--");
                ddl_ptored_taller.SelectedIndex = 0;
                ddl_ptored_taller.Enabled = false;

                lst_talleres_t.Items.Clear();
            }

            if (ddl_dpto.ID.ToUpper().Contains("DDL_DEPARTAMENTO_CALL"))
            {
                ddl_ptored_call.Items.Insert(0, "--Seleccione--");
                ddl_ptored_call.SelectedIndex = 0;
                ddl_ptored_call.Enabled = false;

                lst_talleres_call.Items.Clear();
            }

        }
        else
        {
            ddl_prov.Items.Insert(0, "--Seleccione--");
            ddl_provincia_ases_serv_t.Items.Insert(0, "--Seleccione--");

            ddl_dist.Items.Insert(0, "--Seleccione--");
            ddl_dist.SelectedIndex = 0;
            ddl_dist.Enabled = false;

            ddl_distrito_ase_serv_t.Items.Insert(0, "--Seleccione--");
            ddl_distrito_ase_serv_t.SelectedIndex = 0;
            ddl_distrito_ase_serv_t.Enabled = false;


            if (ddl_dpto.ID.ToUpper().Contains("DDL_DEPARTAMENTO_TIENDA"))
            {
                ddl_tipoptored_tienda.Items.Insert(0, "--Seleccione--");
                ddl_tipoptored_tienda.SelectedIndex = 0;
                ddl_tipoptored_tienda.Enabled = false;

                lst_ptored_tienda.Items.Clear();
            }

            //if (ddl_dpto.ID.ToUpper().Contains("DDL_DEPARTAMENTO_ASE_SERV_T"))
            //{
            //    ddl_ptored_ase_serv_t.Items.Insert(0, "--Seleccione--");
            //    ddl_ptored_ase_serv_t.SelectedIndex = 0;
            //    ddl_ptored_ase_serv_t.Enabled = false;

            //    ddl_taller_ase_serv_t.Items.Insert(0, "--Seleccione--");
            //    ddl_taller_ase_serv_t.SelectedIndex = 0;
            //    ddl_taller_ase_serv_t.Enabled = false;
            //}
            if (ddl_dpto.ID.ToUpper().Contains("DDL_DEPARTAMENTO_TALLER"))
            {
                ddl_ptored_taller.Items.Insert(0, "--Seleccione--");
                ddl_ptored_taller.SelectedIndex = 0;
                ddl_ptored_taller.Enabled = false;

                lst_talleres_t.Items.Clear();
            }

            if (ddl_dpto.ID.ToUpper().Contains("DDL_DEPARTAMENTO_CALL"))
            {
                ddl_ptored_call.Items.Insert(0, "--Seleccione--");
                ddl_ptored_call.SelectedIndex = 0;
                ddl_ptored_call.Enabled = false;

                lst_talleres_call.Items.Clear();
            }

        }
    }

    protected void ddl_provincia_dg_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDistritoPorProvinciaSeleccionada(ref ddl_departamento_dg, ref ddl_provincia_dg, ref ddl_distrito_dg);
        ddl_provincia_ases_serv_t.SelectedValue = ddl_provincia_dg.SelectedValue;

        //----------
        ddl_ubicacion_dg.Items.Clear();
        ddl_ubicacion_dg.Items.Insert(0, "--Seleccione--");
        ddl_ubicacion_dg.Enabled = false;
        //----------


        pnl_Capacidad.Visible = ((ddl_perfil.SelectedValue == "ASRV") ? true : false);
    }

    private void CargarDistritoPorProvinciaSeleccionada(ref DropDownList ddl_dpto, ref DropDownList ddl_prov, ref DropDownList ddl_dist)
    {
        ddl_dist.Items.Clear();
        ddl_dist.AutoPostBack = false;
        ddl_dist.Enabled = false;

        ddl_distrito_ase_serv_t.Items.Clear();
        ddl_distrito_ase_serv_t.AutoPostBack = false;
        ddl_distrito_ase_serv_t.Enabled = false;
        if (ddl_prov.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtubigeo"]).Select("codprov <> '00' AND coddist <> '00' AND coddpto='" + ddl_dpto.SelectedValue + "' AND codprov='" + ddl_prov.SelectedValue + "'", "nombre", DataViewRowState.CurrentRows);

            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_dist.Items.Add("");
                ddl_dist.Items[i].Value = oRow[i]["coddist"].ToString();
                ddl_dist.Items[i].Text = oRow[i]["nombre"].ToString();

                ddl_distrito_ase_serv_t.Items.Add("");
                ddl_distrito_ase_serv_t.Items[i].Value = oRow[i]["coddist"].ToString();
                ddl_distrito_ase_serv_t.Items[i].Text = oRow[i]["nombre"].ToString();

            }

            if (ddl_dist.Items.Count > 0)//  antes > 1 
            {
                ddl_dist.Items.Insert(0, "--Seleccione--");
                ddl_dist.SelectedIndex = 0;
                ddl_dist.AutoPostBack = true;
                ddl_dist.Enabled = true;



                ddl_distrito_ase_serv_t.Items.Insert(0, "--Seleccione--");
                ddl_distrito_ase_serv_t.SelectedIndex = 0;
                ddl_distrito_ase_serv_t.AutoPostBack = true;
                ddl_distrito_ase_serv_t.Enabled = false;

            }

            if (ddl_prov.ID.ToUpper().Contains("DDL_PROVINCIA_TIENDA"))
            {
                ddl_tipoptored_tienda.Items.Insert(0, "--Seleccione--");
                ddl_tipoptored_tienda.SelectedIndex = 0;
                ddl_tipoptored_tienda.Enabled = false;

                lst_ptored_tienda.Items.Clear();
            }

            //if (ddl_prov.ID.ToUpper().Contains("DDL_PROVINCIA_ASES_SERV_T"))
            //{
            //    ddl_ptored_ase_serv_t.Items.Insert(0, "--Seleccione--");
            //    ddl_ptored_ase_serv_t.SelectedIndex = 0;
            //    ddl_ptored_ase_serv_t.Enabled = false;

            //    ddl_taller_ase_serv_t.Items.Insert(0, "--Seleccione--");
            //    ddl_taller_ase_serv_t.SelectedIndex = 0;
            //    ddl_taller_ase_serv_t.Enabled = false;
            //}

            if (ddl_prov.ID.ToUpper().Contains("DDL_PROVINCIA_TALLER"))
            {
                ddl_ptored_taller.Items.Insert(0, "--Seleccione--");
                ddl_ptored_taller.SelectedIndex = 0;
                ddl_ptored_taller.Enabled = false;

                lst_talleres_t.Items.Clear();
            }

            if (ddl_prov.ID.ToUpper().Contains("DDL_PROVINCIA_CALL"))
            {
                ddl_ptored_call.Items.Insert(0, "--Seleccione--");
                ddl_ptored_call.SelectedIndex = 0;
                ddl_ptored_call.Enabled = false;

                lst_talleres_call.Items.Clear();
            }

        }
        else
        {
            ddl_dist.Items.Insert(0, "--Seleccione--");
            ddl_dist.SelectedIndex = 0;
            ddl_dist.Enabled = false;

            ddl_distrito_ase_serv_t.Items.Insert(0, "--Seleccione--");
            ddl_distrito_ase_serv_t.SelectedIndex = 0;
            ddl_distrito_ase_serv_t.Enabled = false;

            if (ddl_prov.ID.ToUpper().Contains("DDL_PROVINCIA_TIENDA"))
            {
                ddl_tipoptored_tienda.Items.Insert(0, "--Seleccione--");
                ddl_tipoptored_tienda.SelectedIndex = 0;
                ddl_tipoptored_tienda.Enabled = false;

                lst_ptored_tienda.Items.Clear();
            }

            //if (ddl_prov.ID.ToUpper().Contains("DDL_PROVINCIA_ASES_SERV_T"))
            //{
            //    ddl_ptored_ase_serv_t.Items.Insert(0, "--Seleccione--");
            //    ddl_ptored_ase_serv_t.SelectedIndex = 0;
            //    ddl_ptored_ase_serv_t.Enabled = false;

            //    ddl_taller_ase_serv_t.Items.Insert(0, "--Seleccione--");
            //    ddl_taller_ase_serv_t.SelectedIndex = 0;
            //    ddl_taller_ase_serv_t.Enabled = false;
            //}
            if (ddl_prov.ID.ToUpper().Contains("DDL_PROVINCIA_TALLER"))
            {
                ddl_ptored_taller.Items.Insert(0, "--Seleccione--");
                ddl_ptored_taller.SelectedIndex = 0;
                ddl_ptored_taller.Enabled = false;

                lst_talleres_t.Items.Clear();
            }

            if (ddl_prov.ID.ToUpper().Contains("DDL_PROVINCIA_CALL"))
            {
                ddl_ptored_call.Items.Insert(0, "--Seleccione--");
                ddl_ptored_call.SelectedIndex = 0;
                ddl_ptored_call.Enabled = false;

                lst_talleres_call.Items.Clear();
            }

        }
    }

    protected void ddl_distrito_dg_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_ubicacion_dg.Items.Clear();
        ddl_ptored_ase_serv_t.Items.Clear();
        if (ddl_distrito_dg.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtubicacion"]).Select("coddpto='" + ddl_departamento_dg.SelectedValue + "' AND codprov='" + ddl_provincia_dg.SelectedValue + "' AND coddist='" + ddl_distrito_dg.SelectedValue + "'", "no_ubica", DataViewRowState.CurrentRows);
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_ubicacion_dg.Items.Add("");
                ddl_ubicacion_dg.Items[i].Value = oRow[i]["nid_ubica"].ToString();
                ddl_ubicacion_dg.Items[i].Text = oRow[i]["no_ubica"].ToString();

                ddl_ptored_ase_serv_t.Items.Add("");
                ddl_ptored_ase_serv_t.Items[i].Value = oRow[i]["nid_ubica"].ToString();
                ddl_ptored_ase_serv_t.Items[i].Text = oRow[i]["no_ubica"].ToString();

            }
            ddl_ubicacion_dg.Enabled = true;
            ddl_ubicacion_dg.Items.Insert(0, "--Seleccione--");
            ddl_ubicacion_dg.SelectedIndex = 0;

            ddl_ptored_ase_serv_t.Enabled = false;
            ddl_ptored_ase_serv_t.Items.Insert(0, "--Seleccione--");
            ddl_ptored_ase_serv_t.SelectedIndex = 0;
        }
        else
        {
            //----------
            ddl_ubicacion_dg.Items.Clear();
            ddl_ubicacion_dg.Items.Insert(0, "--Seleccione--");
            ddl_ubicacion_dg.Enabled = false;
            //----------
        }
        ddl_distrito_ase_serv_t.SelectedValue = ddl_distrito_dg.SelectedValue;
    }
    protected void ddl_departamento_tienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarProvinciaPorDepartamentoSeleccionado(ref ddl_departamento_tienda, ref ddl_provincia_tienda, ref ddl_distrito_tienda);
    }
    protected void ddl_provincia_tienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDistritoPorProvinciaSeleccionada(ref ddl_departamento_tienda, ref ddl_provincia_tienda, ref ddl_distrito_tienda);
    }
    protected void ddl_distrito_tienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_tipoptored_tienda.Items.Clear();
        ddl_tipoptored_tienda.AutoPostBack = false;
        ddl_tipoptored_tienda.Enabled = false;

        lst_ptored_tienda.Items.Clear();
        if (ddl_distrito_tienda.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtptoredpordistrito"]).Select("coddpto = '" + ddl_departamento_tienda.SelectedValue + "' AND codprov = '" + ddl_provincia_tienda.SelectedValue + "' AND coddist = '" + ddl_distrito_tienda.SelectedValue + "'", "no_tip_ubica", DataViewRowState.CurrentRows);
            DataTable dt = new DataTable();
            dt.Columns.Add("tip_ubica", System.Type.GetType("System.String"));
            dt.Columns.Add("no_tip_ubica", System.Type.GetType("System.String"));

            ddl_tipoptored_tienda.Items.Clear();
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                if (dt.Select("tip_ubica='" + oRow[i]["tip_ubica"].ToString() + "'").Length == 0)
                    dt.Rows.Add(oRow[i]["tip_ubica"].ToString(), oRow[i]["no_tip_ubica"].ToString());
            }
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                ddl_tipoptored_tienda.Items.Add("");
                ddl_tipoptored_tienda.Items[i].Value = dt.Rows[i]["tip_ubica"].ToString();
                ddl_tipoptored_tienda.Items[i].Text = dt.Rows[i]["no_tip_ubica"].ToString();
            }

            if (ddl_tipoptored_tienda.Items.Count > 0)
            {
                ddl_tipoptored_tienda.Enabled = true;
                ddl_tipoptored_tienda.Items.Insert(0, "--Seleccione--");
                ddl_tipoptored_tienda.SelectedIndex = 0;
                ddl_tipoptored_tienda.AutoPostBack = true;
            }
            else
            {
                ddl_tipoptored_tienda.Items.Insert(0, "--Seleccione--");
                ddl_tipoptored_tienda.SelectedIndex = 0;
                ddl_tipoptored_tienda.AutoPostBack = false;
                ddl_tipoptored_tienda.Enabled = false;
            }
            dt = null;
        }
        else
        {
            ddl_tipoptored_tienda.Items.Insert(0, "--Seleccione--");
            ddl_tipoptored_tienda.SelectedIndex = 0;
            ddl_tipoptored_tienda.AutoPostBack = false;
            ddl_tipoptored_tienda.Enabled = false;
        }
    }
    protected void ddl_tipoptored_tienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        lst_ptored_tienda.Items.Clear();
        if (ddl_tipoptored_tienda.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtptoredpordistrito"]).Select("tip_ubica = '" + ddl_tipoptored_tienda.SelectedValue + "' AND existe = '0'", "no_ubica", DataViewRowState.CurrentRows);
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("nid_ubica", System.Type.GetType("System.Int32"));
            dt1.Columns.Add("no_ubica", System.Type.GetType("System.String"));

            for (Int32 i = 0; i < oRow.Length; i++)
            {
                if (dt1.Select("nid_ubica = " + Convert.ToInt32(oRow[i]["nid_ubica"].ToString())).Length == 0)
                    dt1.Rows.Add(Convert.ToInt32(oRow[i]["nid_ubica"].ToString()), oRow[i]["no_ubica"].ToString());
            }
            for (Int32 i = 0; i < dt1.Rows.Count; i++)
            {
                lst_ptored_tienda.Items.Add("");
                lst_ptored_tienda.Items[i].Value = dt1.Rows[i]["nid_ubica"].ToString();
                lst_ptored_tienda.Items[i].Text = dt1.Rows[i]["no_ubica"].ToString();
            }
        }
    }
    protected void btn_add_ptored_t_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_ptored_tienda, lst_ptored_sel_tienda);
    }
    protected void btn_del_ptored_tienda_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_ptored_sel_tienda, lst_ptored_tienda);
    }

    protected void ddl_empresa_tienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMarca_PorEmpresa(ddl_empresa_tienda, ref ddl_marca_tienda);
    }

    private void CargarMarca_PorEmpresa(DropDownList ddl_empresa, ref DropDownList ddl_marca)
    {
        ddl_marca.Items.Clear();
        ddl_marca.Enabled = false;
        if (ddl_empresa.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtmarcaempresa"]).Select("nid_empresa = " + Convert.ToInt32(ddl_empresa.SelectedValue), "no_marca", DataViewRowState.CurrentRows);
            ddl_marca.Items.Clear();
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_marca.Items.Add("");
                ddl_marca.Items[i].Value = oRow[i]["nid_marca"].ToString();
                ddl_marca.Items[i].Text = oRow[i]["no_marca"].ToString();
            }
            if (ddl_marca.Items.Count > 0)
            {
                ddl_marca.Items.Insert(0, "--Seleccione--");
                ddl_marca.SelectedIndex = 0;
                ddl_marca.Enabled = true;
                ddl_marca.AutoPostBack = true;
            }
            else
            {
                ddl_marca.Items.Insert(0, "--Seleccione--");
                ddl_marca.SelectedIndex = 0;
                ddl_marca.Enabled = false;
                ddl_marca.AutoPostBack = false;
            }

            if (ddl_empresa.ID.ToUpper().Contains("DDL_EMPRESA_TIENDA"))
            {
                ddl_linea_comercial_tienda.Items.Clear();
                ddl_linea_comercial_tienda.Enabled = false;
                ddl_linea_comercial_tienda.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                ddl_linea_comercial_tienda.SelectedIndex = 0;

                lst_modelos_tienda.Items.Clear();
            }

            if (ddl_empresa.ID.ToUpper().Contains("DDL_EMPRESA_TALLER"))
            {
                ddl_lineacomercial_taller.Items.Clear();
                ddl_lineacomercial_taller.Enabled = false;
                ddl_lineacomercial_taller.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                ddl_lineacomercial_taller.SelectedIndex = 0;

                lst_modelos_taller.Items.Clear();
            }

            if (ddl_empresa.ID.ToUpper().Contains("DDL_EMPRESA_ASE_SERV_T"))
            {
                ddl_linea_comercial_ase_serv_t.Items.Clear();
                ddl_linea_comercial_ase_serv_t.Enabled = false;
                ddl_linea_comercial_ase_serv_t.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                ddl_linea_comercial_ase_serv_t.SelectedIndex = 0;

                lst_modelos_ase_serv_t.Items.Clear();
            }

            if (ddl_empresa.ID.ToUpper().Contains("DDL_EMPRESA_CALL"))
            {
                ddl_linea_comercial_call.Items.Clear();
                ddl_linea_comercial_call.Enabled = false;
                ddl_linea_comercial_call.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                ddl_linea_comercial_call.SelectedIndex = 0;

                lst_modelos_call.Items.Clear();
            }
        }
        else
        {
            ddl_marca.Items.Clear();
            ddl_marca.Enabled = false;
            ddl_marca.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            ddl_marca.SelectedIndex = 0;
            ddl_marca.AutoPostBack = false;

            if (ddl_empresa.ID.ToUpper().Contains("DDL_EMPRESA_TIENDA"))
            {
                ddl_linea_comercial_tienda.Items.Clear();
                ddl_linea_comercial_tienda.Enabled = false;
                ddl_linea_comercial_tienda.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                ddl_linea_comercial_tienda.SelectedIndex = 0;

                lst_modelos_tienda.Items.Clear();
            }

            if (ddl_empresa.ID.ToUpper().Contains("DDL_EMPRESA_TALLER"))
            {
                ddl_lineacomercial_taller.Enabled = false;
                ddl_lineacomercial_taller.Items.Insert(0, "--Seleccione--");
                ddl_lineacomercial_taller.SelectedIndex = 0;

                lst_modelos_taller.Items.Clear();

            }

            if (ddl_empresa.ID.ToUpper().Contains("DDL_EMPRESA_ASE_SERV_T"))
            {
                ddl_linea_comercial_ase_serv_t.Items.Clear();
                ddl_linea_comercial_ase_serv_t.Enabled = false;
                ddl_linea_comercial_ase_serv_t.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                ddl_linea_comercial_ase_serv_t.SelectedIndex = 0;

                lst_modelos_ase_serv_t.Items.Clear();
            }

        }
    }

    protected void ddl_marca_tienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarLineaComercial_PorMarca(ddl_marca_tienda, ref ddl_linea_comercial_tienda);
    }

    private void CargarLineaComercial_PorMarca(DropDownList ddl_marca, ref DropDownList ddl_linea_comercial)
    {
        ddl_linea_comercial.Items.Clear();
        ddl_linea_comercial.Enabled = false;
        if (ddl_marca.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtlineamarca"]).Select("nid_marca = " + Convert.ToInt32(ddl_marca.SelectedValue), "linea_comercial", DataViewRowState.CurrentRows);
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("nid_negocio_linea", System.Type.GetType("System.Int32"));
            dt1.Columns.Add("linea_comercial", System.Type.GetType("System.String"));
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                if (dt1.Select("nid_negocio_linea = " + Convert.ToInt32(oRow[i]["nid_negocio_linea"].ToString())).Length == 0)
                    dt1.Rows.Add(Convert.ToInt32(oRow[i]["nid_negocio_linea"].ToString()), oRow[i]["linea_comercial"].ToString());
            }
            ddl_linea_comercial.Items.Clear();
            for (Int32 i = 0; i < dt1.Rows.Count; i++)
            {
                ddl_linea_comercial.Items.Add("");
                ddl_linea_comercial.Items[i].Value = dt1.Rows[i]["nid_negocio_linea"].ToString();
                ddl_linea_comercial.Items[i].Text = dt1.Rows[i]["linea_comercial"].ToString();
            }
            if (ddl_linea_comercial.Items.Count > 0)
            {
                ddl_linea_comercial.Items.Insert(0, "--Seleccione--");
                ddl_linea_comercial.SelectedIndex = 0;
                ddl_linea_comercial.Enabled = true;
                ddl_linea_comercial.AutoPostBack = true;
            }
            else
            {
                ddl_linea_comercial.Items.Insert(0, "--Seleccione--");
                ddl_linea_comercial.SelectedIndex = 0;
                ddl_linea_comercial.Enabled = false;
                ddl_linea_comercial.AutoPostBack = false;
            }

            if (ddl_marca.ID.ToUpper().Contains("DDL_MARCA_TIENDA"))
                lst_modelos_tienda.Items.Clear();

            if (ddl_marca.ID.ToUpper().Contains("DDL_MARCA_TALLER"))
                lst_modelos_taller.Items.Clear();

            if (ddl_marca.ID.ToUpper().Contains("DDL_MARCA_ASE_SERV_T"))
                lst_modelos_ase_serv_t.Items.Clear();

            if (ddl_marca.ID.ToUpper().Contains("DDL_MARCA_CALL"))
                lst_modelos_call.Items.Clear();
            dt1 = null;
        }
        else
        {
            ddl_linea_comercial.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            ddl_linea_comercial.SelectedIndex = 0;
            ddl_linea_comercial.Enabled = false;

            if (ddl_marca.ID.ToUpper().Contains("DDL_MARCA_TIENDA"))
                lst_modelos_tienda.Items.Clear();

            if (ddl_marca.ID.ToUpper().Contains("DDL_MARCA_TALLER"))
                lst_modelos_taller.Items.Clear();

            if (ddl_marca.ID.ToUpper().Contains("DDL_MARCA_ASE_SERV_T"))
                lst_modelos_ase_serv_t.Items.Clear();
        }
    }

    protected void ddl_linea_comercial_tienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarModelo_PorLineaMarca(ddl_linea_comercial_tienda, ddl_marca_tienda, ref lst_modelos_tienda);
    }

    private void CargarModelo_PorLineaMarca(DropDownList ddl_linea_comercial, DropDownList ddl_marca, ref ListBox lst_modelo)
    {
        lst_modelo.Items.Clear();
        if (ddl_linea_comercial.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtmodelo_lineamarca"]).Select("nid_negocio_linea = " + Convert.ToInt32(ddl_linea_comercial.SelectedValue) + " AND nid_marca = " + Convert.ToInt32(ddl_marca.SelectedValue) + " AND existe = '0'", "no_modelo", DataViewRowState.CurrentRows);
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                lst_modelo.Items.Add("");
                lst_modelo.Items[i].Value = oRow[i]["nid_modelo"].ToString();
                lst_modelo.Items[i].Text = oRow[i]["no_modelo"].ToString();
            }
        }
    }

    protected void btn_add_modelo_tienda_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_modelos_tienda, lst_modelo_sel_tienda);
    }
    protected void btn_del_modelo_tienda_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_modelo_sel_tienda, lst_modelos_tienda);
    }


    #region PRM_ADMIN_TALLER

    protected void btn_add_modelo_t_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_modelos_taller, lst_modelos_sel_taller);
    }
    protected void btn_del_modelo_t_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_modelos_sel_taller, lst_modelos_taller);
    }
    protected void ddl_departamento_taller_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarProvinciaPorDepartamentoSeleccionado(ref ddl_departamento_taller, ref ddl_provincia_taller, ref ddl_distrito_taller);
    }
    protected void ddl_provincia_taller_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDistritoPorProvinciaSeleccionada(ref ddl_departamento_taller, ref ddl_provincia_taller, ref ddl_distrito_taller);
    }

    protected void ddl_distrito_taller_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarPtoRed_PorDistrito(ddl_departamento_taller, ddl_provincia_taller, ddl_distrito_taller, ref ddl_ptored_taller);
    }

    private void CargarPtoRed_PorDistrito(DropDownList ddl_dpto, DropDownList ddl_prov, DropDownList ddl_dist, ref DropDownList ddl_ptored)
    {
        ddl_ptored.Items.Clear();
        ddl_ptored.Enabled = false;

        if (ddl_dist.SelectedIndex != 0)
        {
            DataTable dt = ((DataTable)ViewState["dtptoredtaller_dist"]);
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("nid_ubica", System.Type.GetType("System.Int32"));
            dt1.Columns.Add("no_ubica", System.Type.GetType("System.String"));
            dt1.Columns.Add("coddpto", System.Type.GetType("System.String"));
            dt1.Columns.Add("codprov", System.Type.GetType("System.String"));
            dt1.Columns.Add("coddist", System.Type.GetType("System.String"));

            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                if (dt1.Select("nid_ubica = " + Convert.ToInt32(dt.Rows[i]["nid_ubica"].ToString()) +
                    " AND coddpto = '" + dt.Rows[i]["coddpto"].ToString() + "' AND codprov = '" + dt.Rows[i]["codprov"].ToString() + "' AND coddist = '" + dt.Rows[i]["coddist"].ToString() + "'").Length == 0)
                    dt1.Rows.Add(
                        Convert.ToInt32(dt.Rows[i]["nid_ubica"].ToString()),
                        dt.Rows[i]["no_ubica"].ToString(),
                        dt.Rows[i]["coddpto"].ToString(),
                        dt.Rows[i]["codprov"].ToString(),
                        dt.Rows[i]["coddist"].ToString()
                        );
            }

            DataRow[] oRow = dt1.Select("coddpto = '" + ddl_dpto.SelectedValue +
                "' AND codprov = '" + ddl_prov.SelectedValue +
                "' AND coddist = '" + ddl_dist.SelectedValue + "'");
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_ptored.Items.Add("");
                ddl_ptored.Items[i].Value = oRow[i]["nid_ubica"].ToString();
                ddl_ptored.Items[i].Text = oRow[i]["no_ubica"].ToString();
            }
            ddl_ptored.Items.Insert(0, "--Seleccione--");
            ddl_ptored.SelectedIndex = 0;
            if (ddl_ptored.Items.Count > 1)
            {
                ddl_ptored.Enabled = true;
                ddl_ptored.AutoPostBack = true;
            }
            else
            {
                ddl_ptored.Enabled = false;
                ddl_ptored.Items.Insert(0, "--Seleccione--");
                ddl_ptored.SelectedIndex = 0;
                ddl_ptored.AutoPostBack = false;
            }

            if (ddl_dist.ID.ToUpper().Contains("DDL_DISTRITO_ASE_SERV_T"))
            {
                ddl_taller_ase_serv_t.Enabled = false;
                ddl_taller_ase_serv_t.Items.Insert(0, "--Seleccione--");
                ddl_taller_ase_serv_t.SelectedIndex = 0;
            }

            if (ddl_dist.ID.ToUpper().Contains("DDL_DISTRITO_TALLER"))
                lst_talleres_t.Items.Clear();

            if (ddl_dist.ID.ToUpper().Contains("DDL_DISTRITO_CALL"))
                lst_talleres_call.Items.Clear();

        }
        else
        {
            ddl_ptored.Enabled = false;
            ddl_ptored.Items.Insert(0, "--Seleccione--");
            ddl_ptored.SelectedIndex = 0;

            if (ddl_dist.ID.ToUpper().Contains("DDL_DISTRITO_ASE_SERV_T"))
            {
                ddl_taller_ase_serv_t.Enabled = false;
                ddl_taller_ase_serv_t.Items.Insert(0, "--Seleccione--");
                ddl_taller_ase_serv_t.SelectedIndex = 0;
            }

            if (ddl_dist.ID.ToUpper().Contains("DDL_DISTRITO_TALLER"))
                lst_talleres_t.Items.Clear();

            if (ddl_dist.ID.ToUpper().Contains("DDL_DISTRITO_CALL"))
                lst_talleres_call.Items.Clear();

        }
    }

    protected void ddl_ptored_taller_SelectedIndexChanged(object sender, EventArgs e)
    {
        lst_talleres_t.Items.Clear();
        if (ddl_ptored_taller.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtptoredtaller_dist"]).Select("nid_ubica = " + Convert.ToInt32(ddl_ptored_taller.SelectedValue) + " AND existe = '0'", "no_taller", DataViewRowState.CurrentRows);
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                lst_talleres_t.Items.Add("");
                lst_talleres_t.Items[i].Value = oRow[i]["nid_taller"].ToString();
                lst_talleres_t.Items[i].Text = oRow[i]["no_taller"].ToString();
            }
        }
    }
    protected void btn_add_taller_t_Click(object sender, ImageClickEventArgs e)
    {
        if (lst_talleres_t.SelectedIndex != -1)
        {
            Boolean existe = false;
            String value = String.Empty;
            DataTable dt = (DataTable)ViewState["dtptoredtaller_dist"];
            for (Int32 i = 0; i < lst_talleres_t.Items.Count; i++)
            {
                if (lst_talleres_t.Items[i].Selected == true)
                {
                    for (Int32 j = 0; j < lst_talleres_sel_t.Items.Count; j++)
                    {
                        if (lst_talleres_t.Items[i].Value == lst_talleres_sel_t.Items[j].Value)
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                    {
                        lst_talleres_sel_t.Items.Add("");
                        lst_talleres_sel_t.Items[lst_talleres_sel_t.Items.Count - 1].Value = lst_talleres_t.Items[i].Value;
                        lst_talleres_sel_t.Items[lst_talleres_sel_t.Items.Count - 1].Text = lst_talleres_t.Items[i].Text;
                        //
                        DataRow[] fila = dt.Select("nid_taller = " + Convert.ToInt32(lst_talleres_t.Items[i].Value));
                        fila[0]["existe"] = "1";
                        //
                        value = value + lst_talleres_t.Items[i].Value + "|";
                    }
                }
            }
            if (value.Length > 0)
            {
                value = value.Substring(0, value.Length - 1);
                for (Int32 i = 0; i < value.Split('|').Length; i++)
                {
                    for (Int32 j = 0; j < lst_talleres_t.Items.Count; j++)
                    {
                        if (lst_talleres_t.Items[j].Value == value.Split('|')[i].ToString())
                        {
                            lst_talleres_t.Items.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            ViewState["dtptoredtaller_dist"] = dt;
            dt = null;
        }
    }
    protected void btn_del_taller_t_Click(object sender, ImageClickEventArgs e)
    {
        if (lst_talleres_sel_t.SelectedIndex != -1)
        {
            Boolean existe = false;
            String value = String.Empty;
            DataTable dt = (DataTable)ViewState["dtptoredtaller_dist"];
            for (Int32 i = 0; i < lst_talleres_sel_t.Items.Count; i++)
            {
                if (lst_talleres_sel_t.Items[i].Selected == true)
                {
                    for (Int32 j = 0; j < lst_talleres_t.Items.Count; j++)
                    {
                        if (lst_talleres_sel_t.Items[i].Value == lst_talleres_t.Items[j].Value)
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                    {
                        lst_talleres_t.Items.Add("");
                        lst_talleres_t.Items[lst_talleres_t.Items.Count - 1].Value = lst_talleres_sel_t.Items[i].Value;
                        lst_talleres_t.Items[lst_talleres_t.Items.Count - 1].Text = lst_talleres_sel_t.Items[i].Text;
                        //
                        DataRow[] fila = dt.Select("nid_taller = " + Convert.ToInt32(lst_talleres_sel_t.Items[i].Value));
                        fila[0]["existe"] = "0";
                        //
                        value = value + lst_talleres_sel_t.Items[i].Value + "|";
                    }
                }
            }
            if (value.Length > 0)
            {
                value = value.Substring(0, value.Length - 1);
                for (Int32 i = 0; i < value.Split('|').Length; i++)
                {
                    for (Int32 j = 0; j < lst_talleres_sel_t.Items.Count; j++)
                    {
                        if (lst_talleres_sel_t.Items[j].Value == value.Split('|')[i].ToString())
                        {
                            lst_talleres_sel_t.Items.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            ViewState["dtptoredtaller_dist"] = dt;
            dt = null;
        }
    }


    protected void ddl_empresa_taller_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMarca_PorEmpresa(ddl_empresa_taller, ref ddl_marca_taller);
    }
    protected void ddl_marca_taller_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarLineaComercial_PorMarca(ddl_marca_taller, ref ddl_lineacomercial_taller);
    }
    protected void ddl_lineacomercial_taller_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarModelo_PorLineaMarca(ddl_lineacomercial_taller, ddl_marca_taller, ref lst_modelos_taller);
    }

    #endregion

    #region PRM_ASESOR_SERVICIO_TALLER

    protected void ddl_departamento_ase_serv_t_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarProvinciaPorDepartamentoSeleccionado(ref ddl_departamento_ase_serv_t, ref ddl_provincia_ases_serv_t, ref ddl_distrito_ase_serv_t);

    }
    protected void ddl_provincia_ases_serv_t_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDistritoPorProvinciaSeleccionada(ref ddl_departamento_ase_serv_t, ref ddl_provincia_ases_serv_t, ref ddl_distrito_ase_serv_t);
    }
    protected void ddl_distrito_ase_serv_t_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarPtoRed_PorDistrito(ddl_departamento_ase_serv_t, ddl_provincia_ases_serv_t, ddl_distrito_ase_serv_t, ref ddl_ptored_ase_serv_t);
    }
    protected void ddl_empresa_ase_serv_t_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMarca_PorEmpresa(ddl_empresa_ase_serv_t, ref ddl_marca_ase_serv_t);
    }

    protected void ddl_linea_comercial_ase_serv_t_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarModelo_PorLineaMarca(ddl_linea_comercial_ase_serv_t, ddl_marca_ase_serv_t, ref lst_modelos_ase_serv_t);
    }
    protected void ddl_marca_ase_serv_t_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarLineaComercial_PorMarca(ddl_marca_ase_serv_t, ref ddl_linea_comercial_ase_serv_t);
    }
    protected void ddl_ptored_ase_serv_t_SelectedIndexChanged(object sender, EventArgs e)
    {
        //CargarTalleres_PorPtoRed();              
    }

    private void CargarTalleres_PorPtoRed()
    {
        ddl_taller_ase_serv_t.Items.Clear();
        ddl_taller_ase_serv_t.Enabled = false;

        if (ddl_ptored_ase_serv_t.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtptoredtaller_dist"]).Select("nid_ubica = " + Convert.ToInt32(ddl_ptored_ase_serv_t.SelectedValue), "no_taller", DataViewRowState.CurrentRows);

            for (Int32 i = 0; i < oRow.Length; i++)
            {
                ddl_taller_ase_serv_t.Items.Add("");
                ddl_taller_ase_serv_t.Items[i].Value = oRow[i]["nid_taller"].ToString();
                ddl_taller_ase_serv_t.Items[i].Text = oRow[i]["no_taller"].ToString();
            }

            ddl_taller_ase_serv_t.Items.Insert(0, "--Seleccione--");
            ddl_taller_ase_serv_t.SelectedIndex = 0;
            if (ddl_taller_ase_serv_t.Items.Count > 1)
                ddl_taller_ase_serv_t.Enabled = true;
            else
                ddl_taller_ase_serv_t.Enabled = false;
        }
        else
        {
            ddl_taller_ase_serv_t.Items.Clear();
            ddl_taller_ase_serv_t.Enabled = false;
            ddl_taller_ase_serv_t.Items.Insert(0, "--Seleccione--");
            ddl_taller_ase_serv_t.SelectedIndex = 0;
        }
    }

    protected void btn_add_modelo_ase_serv_t_Click1(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_modelos_ase_serv_t, lst_modelos_sel_ase_serv_t);
    }
    protected void btn_del_modelo_ase_serv_t_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_modelos_sel_ase_serv_t, lst_modelos_ase_serv_t);
    }

    #endregion

    #region PRM_ASESOR_SERVICIO_HORARIO

    protected void btn_add_dia_semana_Click(object sender, ImageClickEventArgs e)
    {
        if (ddl_dias_semana.SelectedIndex != 0)
        {
            lst_dias_sel.Items.Add("");
            lst_dias_sel.Items[lst_dias_sel.Items.Count - 1].Value = ddl_dias_semana.Items[ddl_dias_semana.SelectedIndex].Value;
            lst_dias_sel.Items[lst_dias_sel.Items.Count - 1].Text = "- " + ddl_dias_semana.Items[ddl_dias_semana.SelectedIndex].Text;

            ddl_dia.Items.Add("");
            ddl_dia.Items[ddl_dia.Items.Count - 1].Value = ddl_dias_semana.Items[ddl_dias_semana.SelectedIndex].Value;
            ddl_dia.Items[ddl_dia.Items.Count - 1].Text = ddl_dias_semana.Items[ddl_dias_semana.SelectedIndex].Text;

            ddl_dias_semana.Items.RemoveAt(ddl_dias_semana.SelectedIndex);
            if (ddl_dias_semana.Items.Count == 1)
                ddl_dias_semana.Enabled = false;


            // --- CA --->> ADD

            string strTmpCapacidad = string.Empty;
            string[] strCapacidaDias;
            bool blnExiste = false;

            strCapacidaDias = hfCapacidad.Value.ToString().Split('|');


            for (int i = 0; i < ddl_dia.Items.Count; i++)
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

                    if (ddl_dia.Items[i].Value.Equals(strC_Dia))
                    {
                        strTmpCapacidad += strCapacidadDia + "|";
                        blnExiste = true;
                        break;
                    }
                }

                // --> Si no existe el dia se le incluye
                if (!blnExiste)
                    strTmpCapacidad += ddl_dia.Items[i].Value.ToString() + "----|";   // ADD DIA
            }

            //------------------------------------------------------------------------

            if (strTmpCapacidad.Trim().Length > 0) strTmpCapacidad = strTmpCapacidad.Substring(0, strTmpCapacidad.Length - 1);

            hfCapacidad.Value = strTmpCapacidad;
            
            hfCapacidad.Value = hfCapacidad.Value.Replace("--Seleccione--", "");
            
            //txt_capacidad.Text = strTmpCapacidad;

            //---  CA ---<<


        }
    }

    private String DevolverDia(Int32 dia)
    {
        String[] strDia = { "", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
        return strDia[dia];
    }

    protected void btn_del_dia_semana_Click(object sender, ImageClickEventArgs e)
    {
        if (lst_dias_sel.SelectedIndex != -1)
        {
            String value = String.Empty;
            for (Int32 i = 0; i < lst_dias_sel.Items.Count; i++)
            {
                if (lst_dias_sel.Items[i].Selected == true)
                    value = value + lst_dias_sel.Items[i].Value + ",";
            }
            if (value.Length > 0)
            {
                value = value.Substring(0, value.Length - 1);
                for (Int32 i = 0; i < value.Split(',').Length; i++)
                {
                    for (Int32 j = 0; j < lst_dias_sel.Items.Count; j++)
                    {
                        if (value.Split(',')[i] == lst_dias_sel.Items[j].Value)
                        {
                            lst_dias_sel.Items.RemoveAt(j);
                            break;
                        }
                    }
                }
                for (Int32 i = 0; i < value.Split(',').Length; i++)
                {
                    for (Int32 j = 1; j < ddl_dia.Items.Count; j++)
                    {
                        if (value.Split(',')[i] == ddl_dia.Items[j].Value)
                        {
                            ddl_dia.Items.RemoveAt(j);
                            break;
                        }
                    }
                }


                for (Int32 i = 0; i < value.Split(',').Length; i++)
                {
                    ddl_dias_semana.Items.Add("");
                    ddl_dias_semana.Items[ddl_dias_semana.Items.Count - 1].Value = value.Split(',')[i];
                    ddl_dias_semana.Items[ddl_dias_semana.Items.Count - 1].Text = DevolverDia(Convert.ToInt32(value.Split(',')[i]));
                }
                if (ddl_dias_semana.Items.Count > 1)
                    ddl_dias_semana.Enabled = true;
                if (lst_dias_sel.Items.Count == 0)
                {
                    ddl_hora_inicio.Items.Clear();
                    ddl_hora_inicio.Enabled = false;
                    ddl_hora_fin.Items.Clear();
                    ddl_hora_fin.Enabled = false;
                }


                // ---> CU - REMOVE 

                string strTmpCapacidad = string.Empty;
                string[] strCapacidaDias;

                strCapacidaDias = hfCapacidad.Value.ToString().Split('|');

                foreach (string strCapacidadDia in strCapacidaDias)
                {
                    if (strCapacidadDia.Trim().Length == 0) continue;

                    string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                    string strC_FO = strCapacidadDia.Split('-').GetValue(1).ToString();
                    string strC_BO = strCapacidadDia.Split('-').GetValue(2).ToString();

                    if (ddl_dia.Items.IndexOf(ddl_dia.Items.FindByValue(strC_Dia)) != -1)
                    {
                        strTmpCapacidad += strCapacidadDia + "|";
                    }
                }

                if (strTmpCapacidad.Trim().Length > 0) strTmpCapacidad = strTmpCapacidad.Substring(0, strTmpCapacidad.Length - 1);

                hfCapacidad.Value = strTmpCapacidad;
                //txt_capacidad.Text = strTmpCapacidad;

                if (ddl_dia.Items.Count > 0)
                {
                    ddl_dia.SelectedIndex = 0;
                    ddl_dia_SelectedIndexChanged(this, null);
                }
                else
                {
                    txt_capacidad_fo.Text = "";
                    txt_capacidad_bo.Text = "";
                    txt_capacidad.Text = "";
                    txt_capacidad_fo.Enabled = false;
                    txt_capacidad_bo.Enabled = false;
                    txt_capacidad.Enabled = false;
                }

                //---------------------------------------------------
            }
        }
    }
    protected void btn_add_hora_Click(object sender, ImageClickEventArgs e)
    {
        if (ddl_hora_inicio.SelectedIndex == -1)
            return;
        if (ddl_hora_fin.SelectedIndex == -1)
            return;
        if (ddl_dia.SelectedIndex != 0)
        {
            Int32 hora_inicio = Convert.ToInt32(ddl_hora_inicio.SelectedValue.Replace(":", ""));
            Int32 hora_fin = Convert.ToInt32(ddl_hora_fin.SelectedValue.Replace(":", ""));
            Boolean existe_dia = false;
            Boolean hora_inicio_mayor = false;
            if (hora_inicio < hora_fin)
            {
                if (lst_horas_sel.Items.Count > 0)
                {
                    for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
                    {
                        if (lst_horas_sel.Items[i].Value.Split('|')[0] == ddl_dia.SelectedValue)
                        {
                            existe_dia = true;
                            break;
                        }
                        else
                            existe_dia = false;
                    }
                    if (existe_dia)
                    {
                        for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
                        {
                            if (lst_horas_sel.Items[i].Value.Split('|')[0] == ddl_dia.SelectedValue)
                            {
                                if (hora_inicio >= Convert.ToInt32(lst_horas_sel.Items[i].Value.Split('|')[2].Replace(":", "")))
                                    hora_inicio_mayor = true;
                                else
                                    hora_inicio_mayor = false;
                            }
                        }
                        if (hora_inicio_mayor)
                        {
                            lst_horas_sel.Items.Add("");
                            lst_horas_sel.Items[lst_horas_sel.Items.Count - 1].Value = ddl_dia.SelectedValue + "|" + ddl_hora_inicio.SelectedValue + "|" + ddl_hora_fin.SelectedValue;
                            //lst_horas_sel.Items[lst_horas_sel.Items.Count - 1].Text = "- " + ddl_dia.Items[ddl_dia.SelectedIndex].Text + " de " + ddl_hora_inicio.Text.ToLower() + " a " + ddl_hora_fin.Text.ToLower();
                            lst_horas_sel.Items[lst_horas_sel.Items.Count - 1].Text = "- " + ddl_dia.Items[ddl_dia.SelectedIndex].Text + " de " + ddl_hora_inicio.Items[ddl_hora_inicio.SelectedIndex].Text.ToLower() + " a " + ddl_hora_fin.Items[ddl_hora_fin.SelectedIndex].Text.ToLower();
                        }
                    }
                    else
                    {
                        lst_horas_sel.Items.Add("");
                        lst_horas_sel.Items[lst_horas_sel.Items.Count - 1].Value = ddl_dia.SelectedValue + "|" + ddl_hora_inicio.SelectedValue + "|" + ddl_hora_fin.SelectedValue;
                        //lst_horas_sel.Items[lst_horas_sel.Items.Count - 1].Text = "- " + ddl_dia.Items[ddl_dia.SelectedIndex].Text + " de " + ddl_hora_inicio.Text.ToLower() + " a " + ddl_hora_fin.Text.ToLower();
                        lst_horas_sel.Items[lst_horas_sel.Items.Count - 1].Text = "- " + ddl_dia.Items[ddl_dia.SelectedIndex].Text + " de " + ddl_hora_inicio.Items[ddl_hora_inicio.SelectedIndex].Text.ToLower() + " a " + ddl_hora_fin.Items[ddl_hora_fin.SelectedIndex].Text.ToLower();
                    }
                }
                else
                {
                    lst_horas_sel.Items.Add("");
                    lst_horas_sel.Items[0].Value = ddl_dia.SelectedValue + "|" + ddl_hora_inicio.SelectedValue + "|" + ddl_hora_fin.SelectedValue;
                    //lst_horas_sel.Items[lst_horas_sel.Items.Count - 1].Text = "- " + ddl_dia.Items[ddl_dia.SelectedIndex].Text + " de " + ddl_hora_inicio.Text.ToLower() + " a " + ddl_hora_fin.Text.ToLower();
                    lst_horas_sel.Items[0].Text = "- " + ddl_dia.Items[ddl_dia.SelectedIndex].Text + " de " + ddl_hora_inicio.Items[ddl_hora_inicio.SelectedIndex].Text.ToLower() + " a " + ddl_hora_fin.Items[ddl_hora_fin.SelectedIndex].Text.ToLower();
                }
            }
        }
    }
    protected void btn_del_hora_Click(object sender, ImageClickEventArgs e)
    {
        if (lst_horas_sel.SelectedIndex != -1)
        {
            String value = String.Empty;
            for (Int32 i = 0; i < lst_horas_sel.Items.Count; i++)
            {
                if (lst_horas_sel.Items[i].Selected == true)
                    value = value + lst_horas_sel.Items[i].Value + "-";
            }
            if (value.Length > 0)
            {
                value = value.Substring(0, value.Length - 1);
                for (Int32 i = 0; i < value.Split('-').Length; i++)
                {
                    for (Int32 j = 0; j < lst_horas_sel.Items.Count; j++)
                    {
                        if (lst_horas_sel.Items[j].Value == value.Split('-')[i].ToString())
                        {
                            lst_horas_sel.Items.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region PRM_ASESOR_SERVICIO_SERVICIO

    private void CargarTipoServicio_Especifico()
    {
        UsuarioTallerBL objneg = new UsuarioTallerBL();
        List<UsuarioBE> List = objneg.GETListarTipoServ_Especifico();
        DataTable dt = new DataTable();
        DataTable dtTS = new DataTable();

        dt.Columns.Add("nid_tipo_servicio", System.Type.GetType("System.Int32"));
        dt.Columns.Add("no_tipo_servicio", System.Type.GetType("System.String"));
        dt.Columns.Add("nid_servicio", System.Type.GetType("System.Int32"));
        dt.Columns.Add("no_servicio", System.Type.GetType("System.String"));
        //
        dt.Columns.Add("existe", System.Type.GetType("System.String"));
        //
        for (Int32 i = 0; i < List.Count; i++)
            dt.Rows.Add(List[i].Nid_tipo_servicio, List[i].No_tipo_servicio, List[i].Nid_servicio, List[i].No_servicio, "0");

        if (ViewState["listserv"] != null)
        {
            List<UsuarioBE> list_serv = (List<UsuarioBE>)ViewState["listserv"];

            if (list_serv.Count > 0)
            {
                for (Int32 i = 0; i < list_serv.Count; i++)
                {
                    for (Int32 j = 0; j < dt.Rows.Count; j++)
                    {
                        if ((list_serv[i].IntID) == Convert.ToInt32(dt.Rows[j]["nid_servicio"].ToString()))
                        {
                            DataRow[] fila = dt.Select("nid_servicio = " + list_serv[i].IntID);
                            fila[0]["existe"] = "1";
                            break;
                        }
                    }
                }

                //cargar combo y listbox servicio

                dtTS.Columns.Add("nid_tipo_servicio", System.Type.GetType("System.Int32"));
                dtTS.Columns.Add("no_tipo_servicio", System.Type.GetType("System.String"));
                for (Int32 i = 0; i < list_serv.Count; i++)
                {
                    for (Int32 j = 0; j < dt.Rows.Count; j++)
                    {
                        if (list_serv[i].IntID == Convert.ToInt32(dt.Rows[j]["nid_servicio"].ToString()))
                        {
                            if (dtTS.Select("nid_tipo_servicio=" + Convert.ToInt32(dt.Rows[j]["nid_tipo_servicio"].ToString())).Length == 0)
                            {
                                dtTS.Rows.Add(Convert.ToInt32(dt.Rows[j]["nid_tipo_servicio"].ToString()), dt.Rows[j]["no_tipo_servicio"].ToString());
                            }
                            break;
                        }
                    }
                }
                lst_tipo_serv_sel.Items.Clear();
                ddl_tipo_servicio_s.Items.Clear();
                for (Int32 i = 0; i < dtTS.Rows.Count; i++)
                {
                    lst_tipo_serv_sel.Items.Add("");
                    lst_tipo_serv_sel.Items[i].Value = dtTS.Rows[i]["nid_tipo_servicio"].ToString();
                    lst_tipo_serv_sel.Items[i].Text = dtTS.Rows[i]["no_tipo_servicio"].ToString();

                    ddl_tipo_servicio_s.Items.Add("");
                    ddl_tipo_servicio_s.Items[i].Value = dtTS.Rows[i]["nid_tipo_servicio"].ToString();
                    ddl_tipo_servicio_s.Items[i].Text = dtTS.Rows[i]["no_tipo_servicio"].ToString();
                }
                ddl_tipo_servicio_s.Items.Insert(0, "--Seleccione--");
                ddl_tipo_servicio_s.SelectedIndex = 0;
            }
        }

        ViewState.Add("dttiposerv", dt);

        //

        DataTable dt1 = new DataTable();
        dt1.Columns.Add("nid_tipo_servicio", System.Type.GetType("System.Int32"));
        dt1.Columns.Add("no_tipo_servicio", System.Type.GetType("System.String"));
        for (Int32 i = 0; i < dt.Rows.Count; i++)
        {
            if ((dt1.Select("nid_tipo_servicio = " + Convert.ToInt32(dt.Rows[i]["nid_tipo_servicio"].ToString()))).Length == 0)
                dt1.Rows.Add(Convert.ToInt32(dt.Rows[i]["nid_tipo_servicio"].ToString()), dt.Rows[i]["no_tipo_servicio"].ToString());
        }
        dt = null;

        lst_tipo_serv.Items.Clear();
        Boolean existe = false;
        if (dtTS.Rows.Count > 0)
        {
            Int32 cont1 = 0;
            for (Int32 i = 0; i < dt1.Rows.Count; i++)
            {
                for (Int32 k = 0; k < dtTS.Rows.Count; k++)
                {
                    if (dt1.Rows[i]["nid_tipo_servicio"].ToString() == dtTS.Rows[k]["nid_tipo_servicio"].ToString())
                    {
                        existe = true;
                        break;
                    }
                }
                if (!existe)
                {
                    lst_tipo_serv.Items.Add("");
                    lst_tipo_serv.Items[cont1].Value = dt1.Rows[i]["nid_tipo_servicio"].ToString();
                    lst_tipo_serv.Items[cont1].Text = dt1.Rows[i]["no_tipo_servicio"].ToString();
                    cont1 += 1;
                }
                existe = false;
            }
        }
        else
        {
            for (Int32 i = 0; i < dt1.Rows.Count; i++)
            {
                lst_tipo_serv.Items.Add("");
                lst_tipo_serv.Items[i].Value = dt1.Rows[i]["nid_tipo_servicio"].ToString();
                lst_tipo_serv.Items[i].Text = dt1.Rows[i]["no_tipo_servicio"].ToString();
            }
        }
        dt1 = null; objneg = null;
    }

    protected void btn_add_tipo_serv_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_tipo_serv, lst_tipo_serv_sel);
        TraspasoItemDropDownList(lst_tipo_serv_sel, ddl_tipo_servicio_s);
        lst_servicio_espec_s.Items.Clear();
        /*
        //Add DropDownList
        Int32 index = ddl_tipo_servicio_s.SelectedIndex;
        ddl_tipo_servicio_s.Items.Clear();
        ListItemCollection lista = new ListItemCollection();
        foreach (ListItem lsServicio in lst_tipo_serv_sel.Items)
        {
            ddl_tipo_servicio_s.Items.Add(lsServicio);
        }
        ddl_tipo_servicio_s.Items.Insert(0, new ListItem("--Seleccione--", ""));
        ddl_tipo_servicio_s.SelectedIndex = index;
        lst_servicio_espec_s.Items.Clear();*/

    }
    protected void btn_del_tipo_serv_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_tipo_serv_sel, lst_tipo_serv);
        TraspasoItemDropDownList(lst_tipo_serv_sel, ddl_tipo_servicio_s);
        lst_servicio_espec_s.Items.Clear();
        /*
        //Del DropDownList
        //Int32 index = ddl_tipo_servicio_s.SelectedIndex;
        ddl_tipo_servicio_s.Items.Clear();
        ListItemCollection lista = new ListItemCollection();
        foreach (ListItem lsServicio in lst_tipo_serv_sel.Items)
        {
            ddl_tipo_servicio_s.Items.Add(lsServicio);
        }
        ddl_tipo_servicio_s.Items.Insert(0, new ListItem("--Seleccione--", ""));
        //ddl_tipo_servicio_s.SelectedIndex = 0;
        lst_servicio_espec_s.Items.Clear();
        */

    }
    protected void btn_add_serv_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_servicio_espec_s, lst_servicio_espec_sel_s);
    }
    protected void btn_del_serv_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_servicio_espec_sel_s, lst_servicio_espec_s);
    }

    protected void ddl_tipo_servicio_s_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_tipo_servicio_s.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dttiposerv"]).Select("nid_tipo_servicio = " + Convert.ToInt32(ddl_tipo_servicio_s.SelectedValue) + " AND existe = '0'", "", DataViewRowState.CurrentRows);
            lst_servicio_espec_s.Items.Clear();
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                //@002-I
                lst_servicio_espec_s.Items.Add(new ListItem(oRow[i]["no_servicio"].ToString(), oRow[i]["nid_servicio"].ToString()));
                //lst_servicio_espec_s.Items.Add("");
                //lst_servicio_espec_s.Items[i].Value = oRow[i]["nid_servicio"].ToString();
                //lst_servicio_espec_s.Items[i].Text = oRow[i]["no_servicio"].ToString();
                //@002-F
            }
        }
        else
            lst_servicio_espec_s.Items.Clear();
    }

    #endregion

    #region PRM_CALLCENTER_TALLER

    protected void ddl_departamento_call_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarProvinciaPorDepartamentoSeleccionado(ref ddl_departamento_call, ref ddl_provincia_call, ref ddl_distrito_call);
    }
    protected void ddl_provincia_call_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDistritoPorProvinciaSeleccionada(ref ddl_departamento_call, ref ddl_provincia_call, ref ddl_distrito_call);
    }
    protected void ddl_distrito_call_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarPtoRed_PorDistrito(ddl_departamento_call, ddl_provincia_call, ddl_distrito_call, ref ddl_ptored_call);
    }
    protected void ddl_ptored_call_SelectedIndexChanged(object sender, EventArgs e)
    {
        lst_talleres_call.Items.Clear();
        if (ddl_ptored_call.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dtptoredtaller_dist"]).Select("nid_ubica = " + Convert.ToInt32(ddl_ptored_call.SelectedValue) + " AND existe = '0'", "no_taller", DataViewRowState.CurrentRows);
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                lst_talleres_call.Items.Add("");
                lst_talleres_call.Items[i].Value = oRow[i]["nid_taller"].ToString();
                lst_talleres_call.Items[i].Text = oRow[i]["no_taller"].ToString();
            }
        }
    }

    protected void ddl_empresa_call_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMarca_PorEmpresa(ddl_empresa_call, ref ddl_marca_call);
    }
    protected void ddl_linea_comercial_call_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarModelo_PorLineaMarca(ddl_linea_comercial_call, ddl_marca_call, ref lst_modelos_call);
    }
    protected void ddl_marca_call_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarLineaComercial_PorMarca(ddl_marca_call, ref ddl_linea_comercial_call);
    }
    protected void btn_add_taller_call_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_talleres_call, lst_talleres_sel_call);
    }
    protected void btn_del_taller_call_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_talleres_sel_call, lst_talleres_call);
    }
    protected void btn_add_modelo_call_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_modelos_call, lst_modelos_sel_call);
    }
    protected void btn_del_modelo_call_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_modelos_sel_call, lst_modelos_call);
    }

    #endregion

    #region PRM_CALLCENTER_SERVICIO_SERVICIO

    private void CargarTipoServicio_Especifico_Call()
    {
        UsuarioTallerBL objneg = new UsuarioTallerBL();
        List<UsuarioBE> List = objneg.GETListarTipoServ_Especifico();
        DataTable dt = new DataTable();
        DataTable dtTS = new DataTable();

        dt.Columns.Add("nid_tipo_servicio", System.Type.GetType("System.Int32"));
        dt.Columns.Add("no_tipo_servicio", System.Type.GetType("System.String"));
        dt.Columns.Add("nid_servicio", System.Type.GetType("System.Int32"));
        dt.Columns.Add("no_servicio", System.Type.GetType("System.String"));
        //
        dt.Columns.Add("existe", System.Type.GetType("System.String"));
        //
        for (Int32 i = 0; i < List.Count; i++)
            dt.Rows.Add(List[i].Nid_tipo_servicio, List[i].No_tipo_servicio, List[i].Nid_servicio, List[i].No_servicio, "0");

        if (ViewState["listserv_call"] != null)
        {
            List<UsuarioBE> list_serv = (List<UsuarioBE>)ViewState["listserv_call"];

            if (list_serv.Count > 0)
            {
                for (Int32 i = 0; i < list_serv.Count; i++)
                {
                    for (Int32 j = 0; j < dt.Rows.Count; j++)
                    {
                        if ((list_serv[i].IntID) == Convert.ToInt32(dt.Rows[j]["nid_servicio"].ToString()))
                        {
                            DataRow[] fila = dt.Select("nid_servicio = " + list_serv[i].IntID);
                            fila[0]["existe"] = "1";
                            break;
                        }
                    }
                }

                //cargar combo y listbox servicio

                dtTS.Columns.Add("nid_tipo_servicio", System.Type.GetType("System.Int32"));
                dtTS.Columns.Add("no_tipo_servicio", System.Type.GetType("System.String"));
                for (Int32 i = 0; i < list_serv.Count; i++)
                {
                    for (Int32 j = 0; j < dt.Rows.Count; j++)
                    {
                        if (list_serv[i].IntID == Convert.ToInt32(dt.Rows[j]["nid_servicio"].ToString()))
                        {
                            if (dtTS.Select("nid_tipo_servicio=" + Convert.ToInt32(dt.Rows[j]["nid_tipo_servicio"].ToString())).Length == 0)
                            {
                                dtTS.Rows.Add(Convert.ToInt32(dt.Rows[j]["nid_tipo_servicio"].ToString()), dt.Rows[j]["no_tipo_servicio"].ToString());
                            }
                            break;
                        }
                    }
                }
                lst_tipo_serv_sel_cc.Items.Clear();
                ddl_tipo_servicio_cc.Items.Clear();
                for (Int32 i = 0; i < dtTS.Rows.Count; i++)
                {
                    lst_tipo_serv_sel_cc.Items.Add("");
                    lst_tipo_serv_sel_cc.Items[i].Value = dtTS.Rows[i]["nid_tipo_servicio"].ToString();
                    lst_tipo_serv_sel_cc.Items[i].Text = dtTS.Rows[i]["no_tipo_servicio"].ToString();

                    ddl_tipo_servicio_cc.Items.Add("");
                    ddl_tipo_servicio_cc.Items[i].Value = dtTS.Rows[i]["nid_tipo_servicio"].ToString();
                    ddl_tipo_servicio_cc.Items[i].Text = dtTS.Rows[i]["no_tipo_servicio"].ToString();
                }
                ddl_tipo_servicio_cc.Items.Insert(0, "--Seleccione--");
                ddl_tipo_servicio_cc.SelectedIndex = 0;
            }
        }

        ViewState.Add("dttiposerv_call", dt);

        //

        DataTable dt1 = new DataTable();
        dt1.Columns.Add("nid_tipo_servicio", System.Type.GetType("System.Int32"));
        dt1.Columns.Add("no_tipo_servicio", System.Type.GetType("System.String"));
        for (Int32 i = 0; i < dt.Rows.Count; i++)
        {
            if ((dt1.Select("nid_tipo_servicio = " + Convert.ToInt32(dt.Rows[i]["nid_tipo_servicio"].ToString()))).Length == 0)
                dt1.Rows.Add(Convert.ToInt32(dt.Rows[i]["nid_tipo_servicio"].ToString()), dt.Rows[i]["no_tipo_servicio"].ToString());
        }
        dt = null;

        lst_tipo_serv_cc.Items.Clear();
        Boolean existe = false;
        if (dtTS.Rows.Count > 0)
        {
            Int32 cont1 = 0;
            for (Int32 i = 0; i < dt1.Rows.Count; i++)
            {
                for (Int32 k = 0; k < dtTS.Rows.Count; k++)
                {
                    if (dt1.Rows[i]["nid_tipo_servicio"].ToString() == dtTS.Rows[k]["nid_tipo_servicio"].ToString())
                    {
                        existe = true;
                        break;
                    }
                }
                if (!existe)
                {
                    lst_tipo_serv_cc.Items.Add("");
                    lst_tipo_serv_cc.Items[cont1].Value = dt1.Rows[i]["nid_tipo_servicio"].ToString();
                    lst_tipo_serv_cc.Items[cont1].Text = dt1.Rows[i]["no_tipo_servicio"].ToString();
                    cont1 += 1;
                }
                existe = false;
            }
        }
        else
        {
            for (Int32 i = 0; i < dt1.Rows.Count; i++)
            {
                lst_tipo_serv_cc.Items.Add("");
                lst_tipo_serv_cc.Items[i].Value = dt1.Rows[i]["nid_tipo_servicio"].ToString();
                lst_tipo_serv_cc.Items[i].Text = dt1.Rows[i]["no_tipo_servicio"].ToString();
            }
        }
        dt1 = null; objneg = null;
    }

    protected void btn_add_tipo_serv_cc_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_tipo_serv_cc, lst_tipo_serv_sel_cc);
        TraspasoItemDropDownList(lst_tipo_serv_sel_cc, ddl_tipo_servicio_cc);
        lst_servicio_espec_cc.Items.Clear();

    }
    protected void btn_del_tipo_serv_cc_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_tipo_serv_sel_cc, lst_tipo_serv_cc);
        TraspasoItemDropDownList(lst_tipo_serv_sel_cc, ddl_tipo_servicio_cc);
        lst_servicio_espec_cc.Items.Clear();
    }
    protected void btn_add_serv_cc_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_servicio_espec_cc, lst_servicio_espec_sel_cc);
    }
    protected void btn_del_serv_cc_Click(object sender, ImageClickEventArgs e)
    {
        TraspasoItemListBox(lst_servicio_espec_sel_cc, lst_servicio_espec_cc);
    }

    protected void ddl_tipo_servicio_cc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_tipo_servicio_cc.SelectedIndex != 0)
        {
            DataRow[] oRow = ((DataTable)ViewState["dttiposerv"]).Select("nid_tipo_servicio = " + Convert.ToInt32(ddl_tipo_servicio_cc.SelectedValue) + " AND existe = '0'", "", DataViewRowState.CurrentRows);
            lst_servicio_espec_cc.Items.Clear();
            for (Int32 i = 0; i < oRow.Length; i++)
            {
                if (lst_servicio_espec_sel_cc.Items.IndexOf(lst_servicio_espec_sel_cc.Items.FindByValue(oRow[i]["nid_servicio"].ToString())) == -1)
                {
                    //@002-I
                    lst_servicio_espec_cc.Items.Add(new ListItem(oRow[i]["no_servicio"].ToString(), oRow[i]["nid_servicio"].ToString()));
                    //lst_servicio_espec_cc.Items.Add("");
                    //lst_servicio_espec_cc.Items[i].Value = oRow[i]["nid_servicio"].ToString();
                    //lst_servicio_espec_cc.Items[i].Text = oRow[i]["no_servicio"].ToString();
                    //@002-F
                }
            }
        }
        else
            lst_servicio_espec_cc.Items.Clear();
    }





    #endregion


    protected void btn_add_dia_excep_Click(object sender, ImageClickEventArgs e)
    {
        if (IsDate(txt_dia_exceptuado.Text.Trim()))
        {
            Boolean existe = false;
            for (Int32 i = 0; i < lst_dias_excep.Items.Count; i++)
            {
                if (lst_dias_excep.Items[i].Value == txt_dia_exceptuado.Text.Trim())
                {
                    existe = true;
                    break;
                }
            }
            if (!existe)
            {
                DateTime fecha = Convert.ToDateTime(txt_dia_exceptuado.Text.Trim());
                Int32 dia = (Int32)fecha.DayOfWeek;
                lst_dias_excep.Items.Add("");
                lst_dias_excep.Items[lst_dias_excep.Items.Count - 1].Value = txt_dia_exceptuado.Text.Trim();
                //lst_dias_excep.Items[lst_dias_excep.Items.Count - 1].Text = "- " + fecha.DayOfWeek.ToString() + " " + fecha.Day.ToString() + " de " + DevolverMes(fecha.Month) + " del " + fecha.Year.ToString();
                lst_dias_excep.Items[lst_dias_excep.Items.Count - 1].Text = "- " + fecha.ToLongDateString();
            }
        }
    }

    protected void btn_del_dia_excep_Click(object sender, ImageClickEventArgs e)
    {

        if (lst_dias_excep.SelectedIndex != -1)
        {
            String value = String.Empty;
            for (Int32 i = 0; i < lst_dias_excep.Items.Count; i++)
            {
                if (lst_dias_excep.Items[i].Selected == true)
                    value = value + lst_dias_excep.Items[i].Value + "|";
            }
            if (value.Length > 0)
            {
                value = value.Substring(0, value.Length - 1);
                for (Int32 i = 0; i < value.Split('|').Length; i++)
                {
                    for (Int32 j = 0; j < lst_dias_excep.Items.Count; j++)
                    {
                        if (lst_dias_excep.Items[j].Value == value.Split('|')[i].ToString())
                        {
                            lst_dias_excep.Items.RemoveAt(j);
                            // break; @003 I   /   F
                        }
                    }
                }
            }
        }
    }

    private String DevolverMes(Int32 mes)
    {
        String[] strMes = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octbre", "Noviembre", "Diciembre" };
        return strMes[mes];
    }

    public static Boolean IsDate(String anyString)
    {
        if (anyString == null) anyString = "";
        if (anyString.Length > 0)
        {DateTime dummyDate;
            try { dummyDate = DateTime.Parse(anyString); }
            catch { return false; } return true;
        }else return false;
    }

    protected void ddl_perfil_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_perfil.SelectedIndex != 0)
            {
                //if (ddl_perfil.SelectedIndex == 1 || ddl_perfil.SelectedValue == "62" || ddl_perfil.Items[ddl_perfil.SelectedIndex].Text == "ADMINISTRADOR DE TALLER") //taller
                //{
                if (ddl_perfil.SelectedValue == "ATAL") //taller
                {
                    Cargar_List_Taller_Por_Perfil_Taller(); Cargar_List_Modelo_Por_Perfil_Taller();

                    Habilitar_PrmAdmTaller(true); Habilitar_PrmAdmTienda(false);
                    Habilitar_PrmAsesorServicio_Taller(false); Habilitar_PrmAsesorServicio_Horario(false); Habilitar_PrmAsesorServicio_Serv(false);
                    Habilitar_PrmOprCallCenter_Taller(false); Habilitar_PrmOprCallCenter_Servicio(false);

                    tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[2].Enabled = true; tabMantDetalleUsuarios.Tabs[2].HeaderText = "Prm Adm Taller";
                    tabMantDetalleUsuarios.Tabs[3].Enabled = false; tabMantDetalleUsuarios.Tabs[3].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
                    
                }
                else if (ddl_perfil.SelectedValue == "ATIE") //tienda
                {
                    Cargar_List_Ubicacion_Por_Perfil_Tienda(); Cargar_List_Modelo_Por_Perfil_Tienda();

                    Habilitar_PrmAdmTienda(true); Habilitar_PrmAdmTaller(false);
                    Habilitar_PrmAsesorServicio_Taller(false); Habilitar_PrmAsesorServicio_Horario(false); Habilitar_PrmAsesorServicio_Serv(false);
                    Habilitar_PrmOprCallCenter_Taller(false); Habilitar_PrmOprCallCenter_Servicio(false);

                    tabMantDetalleUsuarios.Tabs[1].Enabled = true; tabMantDetalleUsuarios.Tabs[1].HeaderText = "prm adm tienda";
                    tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[3].Enabled = false; tabMantDetalleUsuarios.Tabs[3].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
                }
                else if (ddl_perfil.SelectedValue == "AGEN") //general
                {
                    Habilitar_PrmAdmTaller(false); Habilitar_PrmAdmTienda(false);
                    Habilitar_PrmAsesorServicio_Taller(false); Habilitar_PrmAsesorServicio_Horario(false); Habilitar_PrmAsesorServicio_Serv(false);
                    Habilitar_PrmOprCallCenter_Taller(false); Habilitar_PrmOprCallCenter_Servicio(false);

                    tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[3].Enabled = false; tabMantDetalleUsuarios.Tabs[3].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
                }
                //else if (ddl_perfil.SelectedIndex == 4 || ddl_perfil.SelectedValue == "65" || ddl_perfil.Items[ddl_perfil.SelectedIndex].Text == "ASESOR DE SERVICIO") //servicio
                //{
                else if (ddl_perfil.SelectedValue == "ASRV") //servicio
                {
                    Cargar_List_Taller_Por_Perfil_Asesor();
                    Cargar_List_Modelo_Por_Perfil_Asesor();
                    Cargar_List_Horario_Por_Perfil_Asesor();
                    Cargar_List_DiasExcep_Por_Perfil_Asesor();
                    Cargar_List_Servicios_Por_Perfil_Asesor();

                    Habilitar_PrmAsesorServicio_Taller(true); Habilitar_PrmAsesorServicio_Horario(true);
                    Habilitar_PrmAsesorServicio_Serv(true); Habilitar_PrmAdmTaller(false); Habilitar_PrmAdmTienda(false);
                    Habilitar_PrmOprCallCenter_Taller(false); Habilitar_PrmOprCallCenter_Servicio(false);

                    tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[3].Enabled = true; tabMantDetalleUsuarios.Tabs[3].HeaderText = "prm asesor servicio";
                    tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
                    
                    td_AsesorServicio.InnerText = "ASESOR DE SERVICIO"; 
                    trDatosEmpresa_Ase_Serv_1.Visible = true;
                    trDatosEmpresa_Ase_Serv_2.Visible = true;
                    trDatosEmpresa_Ase_Serv_3.Visible = true;
                }
                
                else if (ddl_perfil.SelectedValue == "CALL") //call
                {
                    Cargar_List_Talleres_Por_Perfil_Call();
                    Cargar_List_Modelos_Por_Perfil_Call();

                    Habilitar_PrmOprCallCenter_Taller(true); Habilitar_PrmOprCallCenter_Servicio(true);
                    Habilitar_PrmAsesorServicio_Taller(false); Habilitar_PrmAsesorServicio_Horario(false); Habilitar_PrmAsesorServicio_Serv(false);
                    Habilitar_PrmAdmTaller(false);
                    Habilitar_PrmAdmTienda(false);

                    tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[3].Enabled = false; tabMantDetalleUsuarios.Tabs[3].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[4].Enabled = true; tabMantDetalleUsuarios.Tabs[4].HeaderText = "prm opr call center";
                    
                }
                else if (ddl_perfil.SelectedValue == "MECA") //mecanico
                {
                    Cargar_List_Taller_Por_Perfil_Asesor();
                    Cargar_List_Modelo_Por_Perfil_Asesor();
                    Cargar_List_Horario_Por_Perfil_Asesor();
                    Cargar_List_DiasExcep_Por_Perfil_Asesor();
                    Cargar_List_Servicios_Por_Perfil_Asesor();

                    Habilitar_PrmAsesorServicio_Taller(true); Habilitar_PrmAsesorServicio_Horario(true);
                    Habilitar_PrmAsesorServicio_Serv(true); Habilitar_PrmAdmTaller(false); Habilitar_PrmAdmTienda(false);
                    Habilitar_PrmOprCallCenter_Taller(false); Habilitar_PrmOprCallCenter_Servicio(false);

                    tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
                    tabMantDetalleUsuarios.Tabs[3].Enabled = true; tabMantDetalleUsuarios.Tabs[3].HeaderText = "prm mecanico";
                    tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
                    tabPrmAsesServ.Tabs[2].Enabled = false;
                    trDatosEmpresa_Ase_Serv_1.Visible = false;
                    trDatosEmpresa_Ase_Serv_2.Visible = false;
                    trDatosEmpresa_Ase_Serv_3.Visible = false;
                    td_AsesorServicio.InnerText = "MECANICO";
                }
            }
        }
        catch
        {

        }
    }

    protected void ddl_dia_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_hora_inicio.Items.Clear();
        ddl_hora_inicio.Enabled = false;
        ddl_hora_fin.Items.Clear();
        ddl_hora_fin.Enabled = false;

        string strDiaValue = string.Empty;
        string strTmpCapacidad = string.Empty;
        string[] strCapacidaDias;


        if (ddl_perfil.SelectedValue == "ASRV")
        {
            //CU
            if (!hfDiaAtencion.Value.ToString().Equals("0"))
            {
                strDiaValue = hfDiaAtencion.Value.ToString();
                strCapacidaDias = hfCapacidad.Value.ToString().Split('|');

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
                        //strTmpCapacidad += strC_Dia + "-" + txt_capacidad_fo.Text.Trim() + "-" + txt_capacidad_bo.Text.Trim() + "|";
                        strTmpCapacidad += strC_Dia + "-" + ((this.chkTotal.Checked) ? "T" : "I") + "-" + txt_capacidad_fo.Text.Trim() + "-" + txt_capacidad_bo.Text.Trim() + "-" + txt_capacidad.Text.Trim() + "|";
                    }
                    else
                    {
                        strTmpCapacidad += strCapacidadDia + "|";
                    }
                }

                if (strTmpCapacidad.Trim().Length > 0) strTmpCapacidad = strTmpCapacidad.Substring(0, strTmpCapacidad.Length - 1);

                hfCapacidad.Value = strTmpCapacidad;
                //txt_capacidad.Text = strTmpCapacidad;

            }


            txt_capacidad_fo.Text = "";
            txt_capacidad_bo.Text = "";
            txt_capacidad.Text = "";
            //txt_capacidad_fo.Enabled = false;
            //txt_capacidad_bo.Enabled = false;

        }

        if (ddl_dia.SelectedIndex != 0 && ddl_taller_ase_serv_t.SelectedIndex != 0)
        {

            UsuarioBE objEU = new UsuarioBE();
            UsuarioTallerBL objLU = new UsuarioTallerBL();

            objEU.nid_taller = Convert.ToInt32(ddl_taller_ase_serv_t.SelectedValue.ToString());
            objEU.Dd_atencion = Convert.ToInt32(ddl_dia.SelectedValue.ToString());

            List<UsuarioBE> lst = objLU.GETListarHorario_Por_Taller_Dia(objEU);

            if (lst.Count > 0)
            {


                String horaIni = lst[0].Ho_inicio.ToString();
                String horaFin = lst[0].Ho_fin.ToString();
                Int32 intInterv = Convert.ToInt32(lst[0].No_valor1.ToString());

                cargarRangoHorasDefecto(ref ddl_hora_inicio, horaIni, horaFin, intInterv);
                cargarRangoHorasDefecto(ref ddl_hora_fin, horaIni, horaFin, intInterv);

                if (ddl_perfil.SelectedValue == "ASRV" || ddl_perfil.SelectedValue == "MECA")   //@004 I/F
                {
                    ddl_hora_inicio.Enabled = true;
                    ddl_hora_fin.Enabled = true;

                    txt_capacidad_fo.Enabled = true;
                    txt_capacidad_bo.Enabled = true;
                    txt_capacidad.Enabled = true;

                    //CU

                    strDiaValue = ddl_dia.SelectedValue.ToString();
                    strCapacidaDias = hfCapacidad.Value.ToString().Split('|');

                    foreach (string strCapacidadDia in strCapacidaDias)
                    {
                        if (strCapacidadDia.Trim().Length == 0) continue;

                        string strC_Dia = strCapacidadDia.Split('-').GetValue(0).ToString();
                        string strC_Tipo = strCapacidadDia.Split('-').GetValue(1).ToString();
                        string strC_FO = strCapacidadDia.Split('-').GetValue(2).ToString();
                        string strC_BO = strCapacidadDia.Split('-').GetValue(3).ToString();
                        string strC_Total = strCapacidadDia.Split('-').GetValue(4).ToString();

                        if (strC_Dia.Equals(ddl_dia.SelectedValue.ToString()))
                        {
                            chkTotal.Checked = strC_Tipo.Equals("T");
                            txt_capacidad_fo.Enabled = !strC_Tipo.Equals("T");
                            txt_capacidad_bo.Enabled = !strC_Tipo.Equals("T");
                            txt_capacidad.Enabled = strC_Tipo.Equals("T");

                            txt_capacidad_fo.Text = strC_FO;
                            txt_capacidad_bo.Text = strC_BO;
                            txt_capacidad.Text = strC_Total;


                            break;
                        }
                    }
                }


            }

        }

        if (ddl_perfil.SelectedValue == "ASRV")
        {
            hfDiaAtencion.Value = ddl_dia.SelectedValue.ToString();
            //txt_capacidad.Text = hfCapacidad.Value;
        }

    }

    protected void btn_msgboxconfir_no_Click(object sender, EventArgs e)
    {
        if (ViewState["existe_login"].ToString() == "0")
            Response.Redirect("SRC_Maestro_Usuarios.aspx");
        popup_msgbox_confirm.Hide();
    }

    private void HabilitarControlesPorPerfil()
    {
        if (ViewState["perfil"].ToString() == "ATIE")
            Habilitar_PrmAdmTienda(true);
        else if (ViewState["perfil"].ToString() == "ATAL")
            Habilitar_PrmAdmTaller(true);
        else if (ViewState["perfil"].ToString() == "ASRV")
        {
            Habilitar_PrmAsesorServicio_Taller(true);
            Habilitar_PrmAsesorServicio_Horario(true);
            Habilitar_PrmAsesorServicio_Serv(true);
        }
        else if (ViewState["perfil"].ToString() == "CALL")
        {
            Habilitar_PrmOprCallCenter_Taller(true);
            Habilitar_PrmOprCallCenter_Servicio(true);
        }
        else if (ViewState["perfil"].ToString() == "MECA")
        {
            Habilitar_PrmAsesorServicio_Taller(true);
            Habilitar_PrmAsesorServicio_Horario(true);
        }
    }

    private void HabilitarTabsPorPerfil()
    {
        if (ViewState["perfil"].ToString() == "AGEN")
        {
            tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[3].Enabled = false; tabMantDetalleUsuarios.Tabs[3].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
        }
        else if (ViewState["perfil"].ToString() == "ATIE")
        {
            tabMantDetalleUsuarios.Tabs[1].Enabled = true; tabMantDetalleUsuarios.Tabs[1].HeaderText = "prm adm tienda";
            tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[3].Enabled = false; tabMantDetalleUsuarios.Tabs[3].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
        }
        else if (ViewState["perfil"].ToString() == "ATAL")
        {
            tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[2].Enabled = true; tabMantDetalleUsuarios.Tabs[2].HeaderText = "prm adm taller";
            tabMantDetalleUsuarios.Tabs[3].Enabled = false; tabMantDetalleUsuarios.Tabs[3].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
        }
        else if (ViewState["perfil"].ToString() == "ASRV")
        {
            tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[3].Enabled = true; tabMantDetalleUsuarios.Tabs[3].HeaderText = "prm asesor servicio";
            tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
        }
        else if (ViewState["perfil"].ToString() == "CALL")
        {
            tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[3].Enabled = false; tabMantDetalleUsuarios.Tabs[3].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[4].Enabled = true; tabMantDetalleUsuarios.Tabs[4].HeaderText = "prm opr call center";
        }
        else if (ViewState["perfil"].ToString() == "MECA")
        {
            tabMantDetalleUsuarios.Tabs[1].Enabled = false; tabMantDetalleUsuarios.Tabs[1].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[2].Enabled = false; tabMantDetalleUsuarios.Tabs[2].HeaderText = "";
            tabMantDetalleUsuarios.Tabs[3].Enabled = true; tabMantDetalleUsuarios.Tabs[3].HeaderText = "prm mecanico";
            tabMantDetalleUsuarios.Tabs[4].Enabled = false; tabMantDetalleUsuarios.Tabs[4].HeaderText = "";
            tabPrmAsesServ.Tabs[2].Enabled = false;
            trDatosEmpresa_Ase_Serv_1.Visible = false;
            trDatosEmpresa_Ase_Serv_2.Visible = false;
            trDatosEmpresa_Ase_Serv_3.Visible = false;
            td_AsesorServicio.InnerText = "MECANICO";
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        btnEditar.Visible = false;
        btnGrabar.Visible = true;
        HabilitarDatosGenerales(true);
        HabilitarControlesPorPerfil();
    }
    protected void ddl_ubicacion_dg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_perfil.SelectedValue == "ASRV")
        {
            ddl_ptored_ase_serv_t.SelectedValue = ddl_ubicacion_dg.SelectedValue;
            CargarTalleres_PorPtoRed();
            lst_dias_sel.Items.Clear();
            ddl_dia.Items.Clear();
            ddl_dia.Items.Add("");
            ddl_dia_SelectedIndexChanged(null, null);
            lst_horas_sel.Items.Clear();
        }

    }
    protected void ddl_taller_ase_serv_t_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_dias_semana.Items.Clear();
        ddl_dias_semana.Items.Insert(0, "--Seleccione--");
        ddl_dias_semana.SelectedIndex = 0;

        lst_dias_sel.Items.Clear();

        ddl_dia.Items.Clear();
        ddl_dia.Items.Insert(0, "--Seleccione--");
        ddl_dia.SelectedIndex = 0;

        ddl_hora_inicio.Items.Clear();
        ddl_hora_inicio.Enabled = false;
        ddl_hora_fin.Items.Clear();
        ddl_hora_fin.Enabled = false;

        lst_horas_sel.Items.Clear();

        if (ddl_perfil.SelectedValue == "ASRV")
        {
            //hfCapacidad.Value = "";
            // txt_capacidad.Text = "";
        }

        if (ddl_taller_ase_serv_t.SelectedIndex != 0)
        {
            TallerBL neg = new TallerBL();
            TallerBE ent = new TallerBE();
            ent.nid_taller = Convert.ToInt32(ddl_taller_ase_serv_t.SelectedValue);
            List<TallerBE> list = neg.ListarDias_Taller(ent);
            lst_dias_sel.Items.Clear();
            ddl_dias_semana.Items.Clear();
            //hfCapacidad.Value = "";//CU
            ddl_dias_semana.Items.Insert(0, "--Seleccione--");
            ddl_dias_semana.SelectedIndex = 0;
            for (Int32 i = 0; i < list.Count; i++)
            {
                lst_dias_sel.Items.Add("");
                lst_dias_sel.Items[lst_dias_sel.Items.Count - 1].Value = list[i].Dd_atencion.ToString();
                lst_dias_sel.Items[lst_dias_sel.Items.Count - 1].Text = "- " + DevolverDia(list[i].Dd_atencion);

                ddl_dia.Items.Add("");
                ddl_dia.Items[ddl_dia.Items.Count - 1].Value = list[i].Dd_atencion.ToString();
                ddl_dia.Items[ddl_dia.Items.Count - 1].Text = DevolverDia(list[i].Dd_atencion);
                if (ddl_perfil.SelectedValue == "ASRV")
                    hfCapacidad.Value = hfCapacidad.Value + list[i].Dd_atencion.ToString() + "----" + "|";

            }
            if (ddl_dia.Items.Count > 1)
            {
                for (int i = 1; i < ddl_dia.Items.Count; i++)
                {
                    ddl_dia.SelectedIndex = i;
                    ddl_dia_SelectedIndexChanged(null, null);
                    btn_add_hora_Click(null, null);
                }
            }

            ViewState["dias_sel"] = list;
            list = null;
            neg = null; ent = null;


        }
    }

    protected void ddl_dia_PreRender(object sender, EventArgs e)
    {

    }

    protected void ddl_tipo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddl_tipo_doc_SelectedIndexChanged(object sender, EventArgs e)
    {
        string oDNI = ConfigurationManager.AppSettings["TIPODOCDNI"].ToString();
        string oRUC = ConfigurationManager.AppSettings["TIPODOCRUC"].ToString();

        Parametros oParm = new Parametros();

        txt_nro_documento.Text = string.Empty;

        if (oParm.SRC_CodPais.Equals(1))
        {
            if (ddl_tipo_doc.SelectedValue.ToString().Equals(oDNI))
            {
                txt_nro_documento.MaxLength = 8;
                txt_nro_documento.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            }
            else if (ddl_tipo_doc.SelectedValue.ToString().Equals(oRUC))
            {
                txt_nro_documento.MaxLength = 11;
                txt_nro_documento.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            }
            else
            {
                txt_nro_documento.MaxLength = 20;
                txt_nro_documento.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
            }
        }
        else
        {
            if (ddl_tipo_doc.SelectedValue.ToString().Equals(oDNI))
            {
                txt_nro_documento.MaxLength = 8;
                txt_nro_documento.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            }
            else if (ddl_tipo_doc.SelectedValue.ToString().Equals(oRUC))
            {
                txt_nro_documento.MaxLength = 11;
                txt_nro_documento.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            }
            else
            {
                txt_nro_documento.MaxLength = 20;
                txt_nro_documento.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
            }
        }
    }


}
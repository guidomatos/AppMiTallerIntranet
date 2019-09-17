using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SRC_Mantenimiento_SRC_Maestro_Detalle_Vehiculo : System.Web.UI.Page
{

    ModeloBL objNeg1 = new ModeloBL();
    VehiculoBE objEnt = new VehiculoBE();
    MaestroVehiculoBL objNeg = new MaestroVehiculoBL();
    ClienteBL objNegCliente = new ClienteBL();

    Parametros oParm = new Parametros();

    #region " Metodos "

    private void SRC_MsgInformacion(string strError)
    {
        MensajeScript(strError);
    }
    private void MensajeScript(string SMS)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('" + SMS + "');</script>", false);
    }

    private void CargarEstado()
    {
        ddl_estado.Items.Clear();
        ddl_estado.Items.Add(new ListItem(oParm._SELECCIONE, ""));
        ddl_estado.Items.Add(new ListItem("Activo", "A"));
        ddl_estado.Items.Add(new ListItem("Inactivo", "I"));
        ddl_estado.SelectedValue = "A";
        ddl_estado.Enabled = (Session["NUEVO"] == null);
    }
    private void CargarMarcas()
    {
        ddl_marca.DataSource = objNeg1.GETListarMarcas(Profile.Usuario.NID_USUARIO);
        ddl_marca.DataTextField = "DES";
        ddl_marca.DataValueField = "ID";
        ddl_marca.DataBind();
        ddl_marca.Items.Insert(0, new ListItem(oParm._SELECCIONE, "0"));

        ddl_modelo.Items.Clear();
        ddl_modelo.Items.Insert(0, new ListItem(oParm._SELECCIONE, "0"));

    }
    private void CargarTipoPersona(DropDownList ddl, DropDownList ddlTD)
    {
        ddl.DataSource = objNeg.GETListarTipoPersona();
        ddl.DataTextField = "DES";
        ddl.DataValueField = "ID";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(oParm._SELECCIONE, "0"));

        ddlTD.Items.Clear();
        ddlTD.Items.Add(new ListItem(oParm._SELECCIONE, "0"));

        //Contacto solo Persona Natural 
        if (ddl.ID.Equals("ddl_cont_tipopersona"))
        {
            ddl.Items.RemoveAt(2);
        }

    }
    private void CargarTipoDocumento(DropDownList ddl, string oPersona)
    {
        ddl.Items.Clear();
        if (!oPersona.Equals("0"))
        {
            ddl.DataSource = objNegCliente.GETListarTipoDocumento(oPersona);
            ddl.DataTextField = "DES";
            ddl.DataValueField = "ID";
            ddl.DataBind();
        }
        ddl.Items.Insert(0, new ListItem(oParm._SELECCIONE, "0"));
    }
    private void CargarAnioTipos()
    {
        //PERU
        if (oParm.SRC_CodPais.Equals("1"))
        {
            return;
        }

        //CHILE
        //----------------------

        //Cargar Años  
        //--------------
        string strAnios = oParm.N_RangoAniosVehiculo;
        Int32 intInicio = Int32.Parse(strAnios.Split('-').GetValue(0).ToString());
        Int32 intFin = Int32.Parse(strAnios.Split('-').GetValue(1).ToString());
        ddl_Anio.Items.Clear();

        if (intInicio > intFin)
        {
            for (Int32 i = intInicio; i >= intFin; i--)
            {
                ddl_Anio.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        else
        {
            for (Int32 i = intInicio; i <= intFin; i++)
            {
                ddl_Anio.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        ddl_Anio.Items.Insert(0, oParm._SELECCIONE);

        //Cargar Tipos 
        //--------------
        string strTpos = oParm.N_TiposVehiculo;
        ddl_Tipo.Items.Clear();
        foreach (string strTipo in strTpos.Split('|'))
        {
            ddl_Tipo.Items.Add(new ListItem(strTipo.Split('-').GetValue(1).ToString(), strTipo.Split('-').GetValue(0).ToString()));
        }
        ddl_Tipo.Items.Insert(0, oParm._SELECCIONE);
    }

    private void elegirtipodocxpersona(string TIPO)
    {
        string oJuridica = ConfigurationManager.AppSettings["PersonaJuridica"].ToString();
        string oNatural = ConfigurationManager.AppSettings["PersonaNatural"].ToString();
        string oDNI = ConfigurationManager.AppSettings["TIPODOCDNI"].ToString();
        string oRUC = ConfigurationManager.AppSettings["TIPODOCRUC"].ToString();

        string tipoPers = string.Empty;

        if (TIPO == "1")
        {
            tipoPers = ddl_pro_tipopersona.SelectedValue.ToString();

            txt_pro_nom_rz.Text = "";
            txt_prop_apepaterno.Text = "";
            txt_prop_apematerno.Text = "";
            txt_pro_nro_doc.Text = "";
            txt_pro_telefono.Text = "";
            txt_pro_telefono2.Text = "";
            txt_pro_celular.Text = "";
            txt_pro_celular2.Text = "";
            txt_prop_email.Text = "";
            txt_prop_email_trab.Text = "";
            txt_prop_email_alter.Text = "";

            ddl_clie_tipodoc.AutoPostBack = false;

            ddl_pro_tipodoc.SelectedValue = tipoPers.Equals(oJuridica) ? oRUC : (tipoPers.Equals(oNatural) ? oDNI : "0");
            lblPropAP.Visible = !tipoPers.Equals(oJuridica);
            lblPropAM.Visible = !tipoPers.Equals(oJuridica);
            txt_prop_apepaterno.Visible = !tipoPers.Equals(oJuridica);
            txt_prop_apematerno.Visible = !tipoPers.Equals(oJuridica);
            txt_prop_apepaterno.Text = txt_prop_apepaterno.Visible ? txt_prop_apepaterno.Text : "";
            txt_prop_apematerno.Text = txt_prop_apematerno.Visible ? txt_prop_apematerno.Text : "";
            hid_indnuevoclie.Value = (!tipoPers.Equals(oJuridica)) ? "" : hid_indnuevoclie.Value;

            ddl_pro_tipodoc.AutoPostBack = true;
            validarDocumento(ddl_pro_tipodoc, txt_pro_nro_doc);

        }

        if (TIPO == "2")
        {
            tipoPers = ddl_clie_tipopersona.SelectedValue.ToString();

            txt_clie_apepaterno.Text = "";
            txt_clie_apematerno.Text = "";
            txt_clie_nro_doc.Text = "";
            txt_clie_telefono.Text = "";
            txt_clie_telefono2.Text = "";
            txt_clie_celular.Text = "";
            txt_clie_celular2.Text = "";
            txt_clie_nom_rz.Text = "";
            txt_clie_email.Text = "";
            txt_clie_email_trab.Text = "";
            txt_clie_email_alter.Text = "";

            txtTelefonoFijo_Anexo.Text = "";
            hfPaisTelefonoCelular.Value = "";
            hfPaisTelefonoFijo.Value = "";
            ddl_clie_tipodoc.AutoPostBack = false;

            ddl_clie_tipodoc.SelectedValue = tipoPers.Equals(oJuridica) ? oRUC : (tipoPers.Equals(oNatural) ? oDNI : "0");
            lblCliAP.Visible = !tipoPers.Equals(oJuridica);
            lblCliAM.Visible = !tipoPers.Equals(oJuridica);
            txt_clie_apepaterno.Visible = !tipoPers.Equals(oJuridica);
            txt_clie_apematerno.Visible = !tipoPers.Equals(oJuridica);
            txt_clie_apepaterno.Text = txt_clie_apepaterno.Visible ? txt_clie_apepaterno.Text : "";
            txt_clie_apematerno.Text = txt_clie_apematerno.Visible ? txt_clie_apematerno.Text : "";

            ddl_clie_tipodoc.AutoPostBack = true;
            validarDocumento(ddl_clie_tipodoc, txt_clie_nro_doc);
        }

        if (TIPO == "3")
        {
            tipoPers = ddl_cont_tipopersona.SelectedValue.ToString();

            txt_cont_apepaterno.Text = "";
            txt_cont_apematerno.Text = "";
            txt_cont_nro_doc.Text = "";
            txt_cont_telefono.Text = "";
            txt_cont_telefono2.Text = "";
            txt_cont_celular.Text = "";
            txt_cont_celular2.Text = "";
            txt_cont_nom_rz.Text = "";
            txt_cont_email.Text = "";
            txt_cont_email_trab.Text = "";
            txt_cont_email_alter.Text = "";

            ddl_cont_tipodoc.AutoPostBack = false;

            ddl_cont_tipodoc.SelectedValue = tipoPers.Equals(oJuridica) ? oRUC : (tipoPers.Equals(oNatural) ? oDNI : "0");
            lblContAP.Visible = !tipoPers.Equals(oJuridica);
            lblContAM.Visible = !tipoPers.Equals(oJuridica);
            txt_cont_apepaterno.Visible = !tipoPers.Equals(oJuridica);
            txt_cont_apematerno.Visible = !tipoPers.Equals(oJuridica);
            txt_cont_apepaterno.Text = txt_cont_apepaterno.Visible ? txt_cont_apepaterno.Text : "";
            txt_cont_apematerno.Text = txt_cont_apematerno.Visible ? txt_cont_apematerno.Text : "";

            ddl_cont_tipodoc.AutoPostBack = true;
            validarDocumento(ddl_cont_tipodoc, txt_cont_nro_doc);
        }
    }

    private void CargarEntidad()
    {
        VehiculoBEList objBEList = new VehiculoBEList();
        objBEList = (VehiculoBEList)(Session["VehiculoBEList"]);
        if (objBEList != null)
        {
            for (int i = 0; i < objBEList.Count; i++)
            {
                if (objBEList[i].nid_vehiculo.ToString().Trim().Equals(Session["txh_nid_vehiculo"].ToString().Trim()))
                    objEnt = objBEList[i];
            }
        }
        else
        {
            Response.Redirect("SRC_Maestro_Vehiculo.aspx");
        }
    }
    public void Metodo_Buscar_Propietario()
    {
        objEnt.DET_co_tipo_cliente = ddl_pro_tipopersona.SelectedValue.ToString().Trim();
        objEnt.DET_co_tipo_documento = ddl_pro_tipodoc.SelectedValue.ToString().Trim();
        objEnt.DET_nu_documento = txt_pro_nro_doc.Text.Trim();

        VehiculoBEList ObjLista = new VehiculoBEList();

        ObjLista = objNeg.GETListarBuscarCliente(objEnt);

        if (ObjLista.Count > 0)
        {
            string cadena = ObjLista[0].DET_NOMBRES_RZ;
            string[] nombre_rz = new string[3];
            nombre_rz = cadena.Split('*');
            if (nombre_rz.Length == 1)
            {
                txt_pro_nom_rz.Text = nombre_rz.GetValue(0).ToString();
            }
            else
            {
                txt_pro_nom_rz.Text = nombre_rz.GetValue(0).ToString();
                txt_prop_apepaterno.Text = nombre_rz.GetValue(1).ToString();
                txt_prop_apematerno.Text = nombre_rz.GetValue(2).ToString();
            }

            txt_pro_telefono.Text = ObjLista[0].DET_nu_telefono.ToString();
            txt_pro_telefono2.Text = ObjLista[0].DET_nu_telefono2.ToString();
            txt_pro_celular.Text = ObjLista[0].DET_nu_celular.ToString();
            txt_pro_celular2.Text = ObjLista[0].DET_nu_celular2.ToString();
            txt_prop_email.Text = ObjLista[0].DET_no_correo.ToString();
            txt_prop_email_trab.Text = ObjLista[0].DET_no_correo_trab.ToString();
            txt_prop_email_alter.Text = ObjLista[0].DET_no_correo_alter.ToString();

            hid_nid_propietario.Value = ObjLista[0].DET_cod_cliente.ToString();

            if (ObjLista[0].fl_identidad_validada.Equals(1))
            {
                trPropietario_Mensaje.Visible = true;
                txt_pro_nom_rz.Enabled = false;
                txt_prop_apepaterno.Enabled = false;
                txt_prop_apematerno.Enabled = false;
            }
            else
            {
                trPropietario_Mensaje.Visible = false;
                txt_pro_nom_rz.Enabled = true;
                txt_prop_apepaterno.Enabled = true;
                txt_prop_apematerno.Enabled = true;
            }
        }
        else
        {
            trPropietario_Mensaje.Visible = false;
            txt_pro_nom_rz.Enabled = true;
            txt_prop_apepaterno.Enabled = true;
            txt_prop_apematerno.Enabled = true;
            hid_nid_propietario.Value = "0";
            txt_pro_nom_rz.Text = "";
            txt_prop_apepaterno.Text = "";
            txt_prop_apematerno.Text = "";
            txt_pro_telefono.Text = "";
            txt_pro_telefono2.Text = "";
            txt_pro_celular.Text = "";
            txt_pro_celular2.Text = "";
            txt_prop_email.Text = "";
            txt_prop_email_trab.Text = "";
            txt_prop_email_alter.Text = "";
        }
    }
    public void Metodo_Buscar_Cliente()
    {
        objEnt.DET_co_tipo_cliente = ddl_clie_tipopersona.SelectedValue.ToString().Trim();
        objEnt.DET_co_tipo_documento = ddl_clie_tipodoc.SelectedValue.ToString().Trim();
        objEnt.DET_nu_documento = txt_clie_nro_doc.Text.Trim();

        VehiculoBEList ObjLista = new VehiculoBEList();
        ObjLista = objNeg.GETListarBuscarCliente(objEnt);

        if (ObjLista.Count > 0)
        {
            string cadena = ObjLista[0].DET_NOMBRES_RZ;
            string[] nombre_rz = new string[3];
            nombre_rz = cadena.Split('*');
            if (nombre_rz.Length == 1)
            {
                txt_clie_nom_rz.Text = nombre_rz.GetValue(0).ToString();
            }
            else
            {
                txt_clie_nom_rz.Text = nombre_rz.GetValue(0).ToString();
                txt_clie_apepaterno.Text = nombre_rz.GetValue(1).ToString();
                txt_clie_apematerno.Text = nombre_rz.GetValue(2).ToString();
            }

            txt_clie_telefono.Text = ObjLista[0].DET_nu_telefono.ToString();
            txt_clie_telefono2.Text = ObjLista[0].DET_nu_telefono2.ToString();
            txt_clie_celular.Text = ObjLista[0].DET_nu_celular.ToString();
            txt_clie_celular2.Text = ObjLista[0].DET_nu_celular2.ToString();
            txtTelefonoFijo_Anexo.Text = ObjLista[0].nu_anexo_telefono.ToString();
            hfPaisTelefonoCelular.Value = ObjLista[0].nid_pais_celular.ToString();
            hfPaisTelefonoFijo.Value = ObjLista[0].nid_pais_telefono.ToString();
            txt_clie_email.Text = ObjLista[0].DET_no_correo.ToString();
            txt_clie_email_trab.Text = ObjLista[0].DET_no_correo_trab.ToString();
            txt_clie_email_alter.Text = ObjLista[0].DET_no_correo_alter.ToString();
            hid_nid_cliente.Value = ObjLista[0].DET_cod_cliente.ToString();
            tabMantMaesVehiculo.ActiveTabIndex = 1;

            if (ObjLista[0].fl_identidad_validada.Equals(1))
            {
                trCliente_Mensaje.Visible = true;
                txt_clie_nom_rz.Enabled = false;
                txt_clie_apepaterno.Enabled = false;
                txt_clie_apematerno.Enabled = false;
            }
            else
            {
                trCliente_Mensaje.Visible = false;
                txt_clie_nom_rz.Enabled = true;
                txt_clie_apepaterno.Enabled = true;
                txt_clie_apematerno.Enabled = true;
            }
        }
        else
        {
            trCliente_Mensaje.Visible = false;
            txt_clie_nom_rz.Enabled = true;
            txt_clie_apepaterno.Enabled = true;
            txt_clie_apematerno.Enabled = true;
            hid_nid_cliente.Value = "0";
            txt_clie_nom_rz.Text = "";
            txt_clie_apepaterno.Text = "";
            txt_clie_apematerno.Text = "";
            txt_clie_telefono.Text = "";
            txt_clie_telefono2.Text = "";
            txt_clie_celular.Text = "";
            txtTelefonoFijo_Anexo.Text = "";
            hfPaisTelefonoCelular.Value = "";
            hfPaisTelefonoFijo.Value = "";
            txt_clie_celular2.Text = "";
            txt_clie_email.Text = "";
            txt_clie_email_trab.Text = "";
            txt_clie_email_alter.Text = "";
        }
    }
    public void Metodo_Buscar_Contacto()
    {
        objEnt.DET_co_tipo_cliente = ddl_cont_tipopersona.SelectedValue.ToString().Trim();
        objEnt.DET_co_tipo_documento = ddl_cont_tipodoc.SelectedValue.ToString().Trim();
        objEnt.DET_nu_documento = txt_cont_nro_doc.Text.Trim();

        VehiculoBEList ObjLista = new VehiculoBEList();
        ObjLista = objNeg.GETListarBuscarCliente(objEnt);

        if (ObjLista.Count > 0)
        {
            string cadena = ObjLista[0].DET_NOMBRES_RZ;
            string[] nombre_rz = new string[3];
            nombre_rz = cadena.Split('*');
            if (nombre_rz.Length == 1)
            {
                txt_cont_nom_rz.Text = nombre_rz.GetValue(0).ToString();
            }
            else
            {
                txt_cont_nom_rz.Text = nombre_rz.GetValue(0).ToString();
                txt_cont_apepaterno.Text = nombre_rz.GetValue(1).ToString();
                txt_cont_apematerno.Text = nombre_rz.GetValue(2).ToString();
            }

            txt_cont_telefono.Text = ObjLista[0].DET_nu_telefono.ToString();
            txt_cont_telefono2.Text = ObjLista[0].DET_nu_telefono2.ToString();
            txt_cont_celular.Text = ObjLista[0].DET_nu_celular.ToString();
            txt_cont_celular2.Text = ObjLista[0].DET_nu_celular2.ToString();
            txt_cont_email.Text = ObjLista[0].DET_no_correo.ToString();
            txt_cont_email_trab.Text = ObjLista[0].DET_no_correo_trab.ToString();
            txt_cont_email_alter.Text = ObjLista[0].DET_no_correo_alter.ToString();
            hid_nid_contacto.Value = ObjLista[0].DET_cod_cliente.ToString();
            tabMantMaesVehiculo.ActiveTabIndex = 1;

            int nid_contacto = int.Parse(hid_nid_contacto.Value);
            ClienteBE oMestroClienteBE = ClienteBL.ListarDatosClienteDireccion(nid_contacto);
            txhIDDireccion.Value = "0";
            if (!oMestroClienteBE.nid_cliente.Equals(0))
            {
                txhIDDireccion.Value = oMestroClienteBE.nid_cliente_direccion.ToString();
                ddl_cont_dep.SelectedValue = oMestroClienteBE.coddpto;
                ddl_cont_dep_SelectedIndexChanged(this, null);
                ddl_cont_prov.SelectedValue = oMestroClienteBE.codprov;
                ddl_cont_prov_SelectedIndexChanged(this, null);
                ddl_cont_dist.SelectedValue = oMestroClienteBE.coddist;
                txt_cont_dir.Text = oMestroClienteBE.no_direccion.Trim();
                txt_cont_fax.Text = oMestroClienteBE.nu_fax.Trim();
                bool flAcceso = (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantCliente_AccionEditarDireccion).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion) && oParm.SRC_Pais.Equals(1);
                ddl_cont_dep.Enabled = ddl_cont_prov.Enabled = ddl_cont_dist.Enabled = txt_cont_dir.Enabled = txt_cont_fax.Enabled = flAcceso;
            }
            if (ObjLista[0].fl_identidad_validada.Equals(1))
            {
                trContacto_Mensaje.Visible = true;
                txt_cont_nom_rz.Enabled = false;
                txt_cont_apepaterno.Enabled = false;
                txt_cont_apematerno.Enabled = false;
            }
            else
            {
                trContacto_Mensaje.Visible = false;
                txt_cont_nom_rz.Enabled = true;
                txt_cont_apepaterno.Enabled = true;
                txt_cont_apematerno.Enabled = true;
            }

        }
        else
        {
            trContacto_Mensaje.Visible = false;
            txt_cont_nom_rz.Enabled = true;
            txt_cont_apepaterno.Enabled = true;
            txt_cont_apematerno.Enabled = true;
            txt_cont_dir.Text = "";
            txt_cont_fax.Text = "";
            ddl_cont_dep.SelectedValue = "0";
            ddl_cont_prov.Items.Clear();
            ddl_cont_prov.Items.Add(new ListItem("--Seleccione--", "0"));
            ddl_cont_dist.Items.Clear();
            ddl_cont_dist.Items.Add(new ListItem("--Seleccione--", "0"));
            hid_nid_contacto.Value = "0";
            txt_cont_nom_rz.Text = "";
            txt_cont_apepaterno.Text = "";
            txt_cont_apematerno.Text = "";
            txt_cont_telefono.Text = "";
            txt_cont_telefono2.Text = "";
            txt_cont_celular.Text = "";
            txt_cont_celular2.Text = "";
            txt_cont_email.Text = "";
            txt_cont_email_trab.Text = "";
            txt_cont_email_alter.Text = "";
        }
    }

    public void BuscarDatosClientes()
    {
        VehiculoBE objEntBus = new VehiculoBE();

        objEnt.nid_vehiculo = Convert.ToInt32(Session["txh_nid_vehiculo"]);
        objEntBus = objNeg.ListarDatosClientesPorIDVehiculo(objEnt);

        string cod = string.Empty;

        if (objEntBus != null)
        {
            if (objEntBus.per_pro.Trim().Length > 0 && objEntBus.tipodoc_pro.Trim().Length > 0 && objEntBus.nrodoc_pro.Trim().Length > 0)
            {
                ddl_pro_tipopersona.SelectedValue = objEntBus.per_pro.ToString().Trim();
                ddl_pro_tipopersona_SelectedIndexChanged(this, null);
                ddl_pro_tipodoc.SelectedValue = objEntBus.tipodoc_cont.ToString().Trim();
                txt_pro_nro_doc.Text = objEntBus.nrodoc_pro.ToString().Trim();
                Metodo_Buscar_Propietario();
            }
            if (objEntBus.per_clie.Trim().Length > 0 && objEntBus.tipodoc_clie.Trim().Length > 0 && objEntBus.nrodoc_clie.Trim().Length > 0)
            {
                ddl_clie_tipopersona.SelectedValue = objEntBus.per_clie.ToString().Trim();
                ddl_clie_tipopersona_SelectedIndexChanged(this, null);
                ddl_clie_tipodoc.SelectedValue = objEntBus.tipodoc_cont.ToString().Trim();
                txt_clie_nro_doc.Text = objEntBus.nrodoc_clie.ToString().Trim();
                Metodo_Buscar_Cliente();
            }
            if (objEntBus.per_cont.Trim().Length > 0 && objEntBus.tipodoc_cont.Trim().Length > 0 && objEntBus.nrodoc_cont.Trim().Length > 0)
            {
                ddl_cont_tipopersona.SelectedValue = objEntBus.per_cont.ToString().Trim();
                ddl_cont_tipopersona_SelectedIndexChanged(this, null);
                ddl_cont_tipodoc.SelectedValue = objEntBus.tipodoc_cont.ToString().Trim();
                txt_cont_nro_doc.Text = objEntBus.nrodoc_cont.ToString().Trim();
                Metodo_Buscar_Contacto();
            }
        }
    }

    protected void validarDocumento(DropDownList dp, TextBox txt)
    {
        string oDNI = ConfigurationManager.AppSettings["TIPODOCDNI"].ToString();
        string oRUC = ConfigurationManager.AppSettings["TIPODOCRUC"].ToString();

        if (oParm.SRC_CodPais.Equals("1"))
        {
            if (dp.SelectedValue.ToString().Equals(oDNI))
            {
                txt.MaxLength = 8;
                txt.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            }
            else if (dp.SelectedValue.ToString().Equals(oRUC))
            {
                txt.MaxLength = 11;
                txt.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            }
            else
            {
                txt.MaxLength = 20;
                txt.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
            }
        }
        else
        {
            if (dp.SelectedValue.ToString().Equals(oDNI))
            {
                txt.MaxLength = 9;
                txt.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
            }
            else if (dp.SelectedValue.ToString().Equals(oRUC))
            {
                txt.MaxLength = 11;
                txt.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            }
            else
            {
                txt.MaxLength = 20;
                txt.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
            }
        }
    }

    #endregion

    private void Inicializa()
    {
        if (Request.QueryString["nid_vehiculo"] != null)
            Session["txh_nid_vehiculo"] = Request.QueryString["nid_vehiculo"];

        txt_pro_nro_doc.Attributes.Add("onBlur", "return  Fc_BuscarPropietario()");
        txt_clie_nro_doc.Attributes.Add("onBlur", "return Fc_BuscarCliente()");
        txt_cont_nro_doc.Attributes.Add("onBlur", "return Fc_BuscarContacto()");
        txt_pro_nro_doc.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_clie_nro_doc.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_cont_nro_doc.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        txt_kilometraje.Attributes.Add("onkeypress", "return SoloNumeros(event)");

        lblTextoPlaca.Text = oParm.N_Placa;

        CargarMarcas();
        CargarEstado();
        CargarAnioTipos();

        CargarTipoPersona(ddl_pro_tipopersona, ddl_pro_tipodoc);
        CargarTipoPersona(ddl_clie_tipopersona, ddl_clie_tipodoc);
        CargarTipoPersona(ddl_cont_tipopersona, ddl_cont_tipodoc);


        if (oParm.SRC_CodPais.Equals("1"))
        {
            lblEtiquetaTipo.Visible = false;
            ddl_Anio.Visible = false;
            ddl_Tipo.Visible = false;

            lblEtiquetaAnio.Visible = (Session["NUEVO"] == null);
            lblEtiquetaColor.Visible = (Session["NUEVO"] == null);
            lblEtiquetaMotor.Visible = (Session["NUEVO"] == null);
            txtAnio.Visible = (Session["NUEVO"] == null);
            txtColor.Visible = (Session["NUEVO"] == null);
            txtMotor.Visible = (Session["NUEVO"] == null);

        }
        else
        {
            lblEtiquetaColor.Visible = false;
            lblEtiquetaMotor.Visible = false;
            txtAnio.Visible = false;
            txtColor.Visible = false;
            txtMotor.Visible = false;
        }
        
        AppMiTaller.Intranet.BL.UbigeoBL oUbigeoBL = new AppMiTaller.Intranet.BL.UbigeoBL();
        ddl_cont_dep.DataSource = oUbigeoBL.GetListaDepartamento();
        ddl_cont_dep.DataValueField = "coddpto";
        ddl_cont_dep.DataTextField = "nombre";
        ddl_cont_dep.DataBind();
        ddl_cont_dep.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        ddl_cont_prov.Items.Add(new ListItem("--Seleccione--", "0"));
        ddl_cont_dist.Items.Add(new ListItem("--Seleccione--", "0"));
        txt_cont_dir.Text = string.Empty;

        if (Session["NUEVO"] != null)
        {
            btnEditar.Visible = false;
            btnGrabar.Visible = true;
            btn_obtvin.Visible = false;
            txt_nrovin.Enabled = true;

            txt_placapatente.Text = "";
            txt_nrovin.Text = "";
            txt_kilometraje.Text = "0";

            p_DV.Enabled = true;

            p_propietario.Enabled = true;
            p_cliente.Enabled = true;
            p_contacto.Enabled = true;

            hid_nid_propietario.Value = "0";//PROP
            hid_nid_cliente.Value = "0";//    CLI
            hid_nid_contacto.Value = "0";//   CONT

            hid_indnuevo.Value = "0";// ID_VEH
        }
        else
        {
            CargarEntidad();

            btnEditar.Visible = true;
            btnGrabar.Visible = false;
            btn_obtvin.Visible = false;

            txt_placapatente.Text = objEnt.nu_placa.ToString().Trim();
            txt_nrovin.Text = (oParm.SRC_VINObligatorio.Equals("1") ? objEnt.nu_vin.ToString().Trim() : objEnt.nu_placa.ToString().Trim());

            txt_nrovin.Enabled = true;

            txt_kilometraje.Text = objEnt.qt_km_actual.ToString();

            hid_NumPlaca.Value = objEnt.nu_placa.ToString().Trim();
            hid_indnuevo.Value = objEnt.nid_vehiculo.ToString().Trim();

            ddl_marca.SelectedValue = objEnt.nid_marca.ToString();
            ddl_marca_SelectedIndexChanged(this, null);
            ddl_modelo.SelectedValue = objEnt.nid_modelo.ToString();

            if (objEnt.fl_activo.ToString().Trim().Equals("Activo")) { ddl_estado.SelectedValue = "A"; }
            else { ddl_estado.SelectedValue = "I"; }

            if (oParm.SRC_CodPais.Equals("2"))
            {
                if (objEnt.nu_anio != 0) ddl_Anio.SelectedValue = objEnt.nu_anio.ToString();// Anio Vehiculo
                if (!String.IsNullOrEmpty(objEnt.co_tipo)) ddl_Tipo.SelectedValue = objEnt.co_tipo.ToString();// Anio Vehiculo
            }

            BuscarDatosClientes();

            p_DV.Enabled = (Session["edidet_objEnt"] != null);
            p_propietario.Enabled = (Session["edidet_objEnt"] != null);
            p_cliente.Enabled = (Session["edidet_objEnt"] != null);
            p_contacto.Enabled = (Session["edidet_objEnt"] != null);

            btnEditar.Visible = (Session["verdet_objEnt"] != null);
            btnGrabar.Visible = (Session["edidet_objEnt"] != null);

            if (oParm.SRC_CodPais.Equals("1"))
            {
                CitasBE oCitasBE = new CitasBE();
                CitasBEList oCitasBEList = new CitasBEList();
                CitasBL oCitasBL = new CitasBL();

                oCitasBE.Nu_vin = txt_nrovin.Text.Trim();

                oCitasBEList = new CitasBEList();
                oCitasBEList = oCitasBL.ListarDatosSecVehiculo(oCitasBE);

                if (oCitasBEList.Count > 0)
                {
                    txtAnio.Text = oCitasBEList[0].an_fabricacion.ToString();
                    txtColor.Text = oCitasBEList[0].no_color_exterior.ToString();
                    txtMotor.Text = oCitasBEList[0].nu_motor.ToString();
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Inicializa();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>setTabCabeceraOffForm('1');setTabCabeceraOffForm('0');setTabCabeceraOnForm('0');loadComboPaises();</script>", false); 
            tabMantMaesVehiculo.ActiveTabIndex = 0;
        }
    }
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        Int32 resp = 0;
        int ID_VEHICULO = int.Parse(hid_indnuevo.Value.ToString().Trim());
        string regVeh = "1";

        VehiculoBE objEnt2 = new VehiculoBE();
        ClienteBE entClie = new ClienteBE();
        ClienteBL negClie = new ClienteBL();

        try
        {
            objEnt2.nu_placa = txt_placapatente.Text.Trim();

            //Validar si existe la placa
            //----------------------------

            if (ID_VEHICULO == 0)// Si es nuevo vehiculo
            {
                if (!String.IsNullOrEmpty(objNeg.GETVIN_X_PLACA(objEnt2).Trim()))
                {
                    SRC_MsgInformacion("El número de " + lblTextoPlaca.Text + " ingresado ya existe.");
                    return;
                }
            }
            else// Si es vehiculo editable
            {
                if (!txt_placapatente.Text.Trim().ToUpper().Equals(hid_NumPlaca.Value.ToString().Trim().ToUpper()))
                {
                    //Si es otra placa.
                    if (!String.IsNullOrEmpty(objNeg.GETVIN_X_PLACA(objEnt2).Trim()))
                    {
                        SRC_MsgInformacion("El número de " + lblTextoPlaca.Text + " ingresado ya existe.");
                        return;
                    }
                }
                else
                {
                    regVeh = "2";
                }
            }

            //---------------
            // PROPIETARIO 
            //---------------
            if (this.ddl_pro_tipopersona.SelectedIndex != 0)
            {

                entClie.nid_cliente = int.Parse(hid_nid_propietario.Value.ToString().Trim());
                entClie.co_tipo_documento = ddl_pro_tipodoc.SelectedValue.ToString().Trim();
                entClie.nu_documento = txt_pro_nro_doc.Text.Trim();
                entClie.no_cliente = txt_pro_nom_rz.Text.Trim();
                entClie.no_ape_pat = txt_prop_apepaterno.Text.Trim();
                entClie.no_ape_mat = txt_prop_apematerno.Text.Trim();
                entClie.nu_telefono = txt_pro_telefono.Text.Trim();
                entClie.nu_tel_oficina = txt_pro_telefono2.Text.Trim();
                entClie.nu_celular = txt_pro_celular.Text.Trim();
                entClie.nu_celular_alter = txt_pro_celular2.Text.Trim();
                entClie.no_correo = txt_prop_email.Text.Trim();
                entClie.no_correo_trabajo = txt_prop_email_trab.Text.Trim();
                entClie.no_correo_alter = txt_prop_email_alter.Text.Trim();
                entClie.co_usuario_crea = (Profile.UserName == null ? "" : Profile.UserName.ToString());
                entClie.co_usuario_red = (Profile.UsuarioRed == null ? "" : Profile.UsuarioRed.ToString());
                entClie.no_estacion_red = (Profile.Estacion == null ? "" : Profile.Estacion.ToString());
                entClie.fl_inactivo = "0";
                entClie.ind_accion = (negClie.SRC_SPS_VAL_CLIENTE_X_DOC(entClie) == 0) ? "1" : "2";


                resp = negClie.GETInserActuCliente(entClie);
                if (resp > 0)
                {
                    Metodo_Buscar_Propietario();
                }
                if (hid_nid_cliente.Value.ToString().Trim().Equals("11"))
                    hid_nid_cliente.Value = hid_nid_propietario.Value.ToString().Trim();
                if (hid_nid_contacto.Value.ToString().Trim().Equals("11"))
                    hid_nid_contacto.Value = hid_nid_propietario.Value.ToString().Trim();
            }

            //-----------
            // CLIENTE     
            //-----------
            if (this.ddl_clie_tipopersona.SelectedIndex != 0)
            {
                entClie.nid_cliente = int.Parse(hid_nid_cliente.Value.ToString().Trim());
                entClie.co_tipo_documento = ddl_clie_tipodoc.SelectedValue.ToString().Trim();
                entClie.nu_documento = txt_clie_nro_doc.Text.Trim();
                entClie.no_cliente = txt_clie_nom_rz.Text.Trim();
                entClie.no_ape_pat = txt_clie_apepaterno.Text.Trim();
                entClie.no_ape_mat = txt_clie_apematerno.Text.Trim();
                entClie.nu_telefono = txt_clie_telefono.Text.Trim();
                entClie.nu_tel_oficina = txt_clie_telefono2.Text.Trim();
                entClie.nu_celular = txt_clie_celular.Text.Trim();
                entClie.nu_celular_alter = txt_clie_celular2.Text.Trim();
                entClie.no_correo = txt_clie_email.Text.Trim();
                entClie.no_correo_trabajo = txt_clie_email_trab.Text.Trim();
                entClie.no_correo_alter = txt_clie_email_alter.Text.Trim();
                entClie.co_usuario_crea = (Profile.UserName == null ? "" : Profile.UserName.ToString());
                entClie.co_usuario_red = (Profile.UsuarioRed == null ? "" : Profile.UsuarioRed.ToString());
                entClie.no_estacion_red = (Profile.Estacion == null ? "" : Profile.Estacion.ToString());
                entClie.fl_inactivo = "0";
                entClie.ind_accion = (negClie.SRC_SPS_VAL_CLIENTE_X_DOC(entClie) == 0) ? "1" : "2";

                Int32 nid_pais_telefono; Int32.TryParse(hfPaisTelefonoFijo.Value, out nid_pais_telefono);
                Int32 nid_pais_celular; Int32.TryParse(hfPaisTelefonoCelular.Value, out nid_pais_celular);

                entClie.nu_anexo_telefono = txtTelefonoFijo_Anexo.Text;
                entClie.nid_pais_celular = nid_pais_celular;
                entClie.nid_pais_telefono = nid_pais_telefono;

                resp = negClie.GETInserActuCliente(entClie);
                if (resp > 0)
                {
                    Metodo_Buscar_Cliente();
                }

                if (hid_nid_contacto.Value.ToString().Trim().Equals("12"))
                    hid_nid_contacto.Value = hid_nid_cliente.Value.ToString().Trim();
            }

            //-------------
            // CONTACTO   
            //-------------
            if (this.ddl_cont_tipopersona.SelectedIndex != 0)
            {
                entClie.nid_cliente = int.Parse(hid_nid_contacto.Value.ToString().Trim());
                entClie.co_tipo_documento = ddl_cont_tipodoc.SelectedValue.ToString().Trim();
                entClie.nu_documento = txt_cont_nro_doc.Text.Trim();
                entClie.no_cliente = txt_cont_nom_rz.Text.Trim();
                entClie.no_ape_pat = txt_cont_apepaterno.Text.Trim();
                entClie.no_ape_mat = txt_cont_apematerno.Text.Trim();
                entClie.nu_telefono = txt_cont_telefono.Text.Trim();
                entClie.nu_tel_oficina = txt_cont_telefono2.Text.Trim();
                entClie.nu_celular = txt_cont_celular.Text.Trim();
                entClie.nu_celular_alter = txt_cont_celular2.Text.Trim();
                entClie.no_correo = txt_cont_email.Text.Trim();
                entClie.no_correo_trabajo = txt_cont_email_trab.Text.Trim();
                entClie.no_correo_alter = txt_cont_email_alter.Text.Trim();
                entClie.co_usuario_crea = (Profile.UserName == null ? "" : Profile.UserName.ToString());
                entClie.co_usuario_red = (Profile.UsuarioRed == null ? "" : Profile.UsuarioRed.ToString());
                entClie.no_estacion_red = (Profile.Estacion == null ? "" : Profile.Estacion.ToString());
                entClie.fl_inactivo = "0";
                entClie.ind_accion = (negClie.SRC_SPS_VAL_CLIENTE_X_DOC(entClie) == 0) ? "1" : "2";

                resp = negClie.GETInserActuCliente(entClie);
                
                string coddpto = ddl_cont_dep.SelectedValue.ToString();
                string codprov = ddl_cont_prov.SelectedValue.ToString();
                string coddist = ddl_cont_dist.SelectedValue.ToString();
                string no_direccion = txt_cont_dir.Text.Trim();
                string no_correo = txt_cont_email.Text.Trim();
                string nu_fax = txt_cont_fax.Text.Trim();
                
                if (resp > 0)
                {
                    Metodo_Buscar_Contacto();
                    
                    if ((Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_MantCliente_AccionEditarDireccion).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion) && oParm.SRC_Pais.Equals(1))
                    {
                        ClienteBE oMestroClienteBE = new ClienteBE();
                        string[] res = new string[] { "", "" };
                        oMestroClienteBE.nid_cliente_direccion = int.Parse(txhIDDireccion.Value.ToString().Trim());
                        oMestroClienteBE.nid_cliente = int.Parse(hid_nid_contacto.Value.ToString().Trim());
                        oMestroClienteBE.nu_telefono = txt_cont_telefono.Text.Trim();
                        oMestroClienteBE.nu_fax = nu_fax;
                        oMestroClienteBE.nid_pais = 0;
                        oMestroClienteBE.no_direccion = no_direccion;
                        oMestroClienteBE.coddpto = coddpto;
                        oMestroClienteBE.codprov = codprov;
                        oMestroClienteBE.coddist = coddist;
                        oMestroClienteBE.no_correo = no_correo;
                        oMestroClienteBE.co_usuario = (Profile.UserName == null ? "" : Profile.UserName.ToString());
                        oMestroClienteBE.no_usuario_red = (Profile.UsuarioRed == null ? "" : Profile.UsuarioRed.ToString());
                        oMestroClienteBE.no_estacion_red = (Profile.Estacion == null ? "" : Profile.Estacion.ToString());
                        string oRespuesta = ClienteBL.GuardarDireccionTaller(oMestroClienteBE);
                    }
                }
            }

            //--------------
            //  VEHICULO
            //--------------             
            objEnt.nid_vehiculo = ID_VEHICULO;
            objEnt.nid_propietario = int.Parse(hid_nid_propietario.Value.ToString().Length == 0 ? "0" : hid_nid_propietario.Value.ToString());
            objEnt.nid_cliente = int.Parse(hid_nid_cliente.Value.ToString().Length == 0 ? "0" : hid_nid_cliente.Value.ToString());
            objEnt.nid_contacto = int.Parse(hid_nid_contacto.Value.ToString().Length == 0 ? "0" : hid_nid_contacto.Value.ToString());
            objEnt.nu_placa = txt_placapatente.Text.Trim().ToUpper();
            objEnt.nu_vin = (oParm.SRC_VINObligatorio.Equals("1") ? txt_nrovin.Text.Trim().ToUpper() : (string.IsNullOrEmpty(txt_nrovin.Text.Trim().ToUpper()) ? txt_placapatente.Text.Trim().ToUpper() : txt_nrovin.Text.Trim().ToUpper()));
            objEnt.nid_marca = int.Parse(ddl_marca.SelectedValue.ToString());
            objEnt.nid_modelo = int.Parse(ddl_modelo.SelectedValue.ToString());
            objEnt.qt_km_actual = Int64.Parse(txt_kilometraje.Text.Trim());
            objEnt.fl_reg_manual = "1";
            objEnt.co_usuario = (Profile.UserName == null ? "" : Profile.UserName.ToString());
            objEnt.co_usuario_red = (Profile.UsuarioRed == null ? "" : Profile.UsuarioRed.ToString());
            objEnt.no_estacion_red = (Profile.Estacion == null ? "" : Profile.Estacion.ToString());
            objEnt.fl_activo = ddl_estado.SelectedValue.ToString();
            objEnt.ind_accion = regVeh;
            if (oParm.SRC_CodPais.Equals("2"))
            {
                objEnt.nu_anio = Int32.Parse(ddl_Anio.SelectedValue.ToString());
                objEnt.co_tipo = ddl_Tipo.SelectedValue.ToString();
            }


            resp = objNeg.GETInserActuVehiculo(objEnt);
            if (resp == -1) //El número de VIN ya existe
            {
                // ya se valido al inicio
            }
            if (resp == -2) //El número de VIN ya existe //23.08.2012
            {
                SRC_MsgInformacion("El número de VIN ingresado ya existe.");
                return;
            }
            else if (resp > 0)
            {
                if (ID_VEHICULO == 0)
                    SRC_MsgInformacion("El registro de vehículo se registró con exito.");
                else
                    SRC_MsgInformacion("El registro de vehículo se actualizó con exito.");

                Response.Redirect("SRC_Maestro_Vehiculo.aspx", true);
            }
            else
            {
                SRC_MsgInformacion("Error al guardar.");
            }
        }
        catch (Exception ex)
        {
            SRC_MsgInformacion(ex.Message);
        }
    }
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        btnEditar.Visible = false;

        btnGrabar.Visible = true;

        p_DV.Enabled = true;
        p_propietario.Enabled = true;
        p_cliente.Enabled = true;
        p_contacto.Enabled = true;
            
    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SRC_Maestro_Vehiculo.aspx");
    }

    protected void btn_obtvin_Click(object sender, EventArgs e)
    {
        VehiculoBE objEnt2 = new VehiculoBE();

        if (txt_placapatente.Text.Trim().Length > 0)
        {
            string VIN = "";
            objEnt2.nu_placa = txt_placapatente.Text.Trim();

            VIN = objNeg.GETVIN_X_PLACA(objEnt2);

            if (VIN.Length > 0)
            {
                txt_nrovin.Enabled = false;
                txt_nrovin.Text = VIN;

                SRC_MsgInformacion("El número de " + lblTextoPlaca.Text + " ingresado ya existe.");//, favor de ingresar por la opción de Editar desde la bandeja de busqueda.");
                return;
            }
            else
            {
                txt_nrovin.Enabled = true;
                txt_nrovin.Text = "";
            }
        }
        else
        {
            txt_nrovin.Enabled = true;
            txt_nrovin.Text = "";
        }

        hid_NumPlaca.Value = txt_placapatente.Text.Trim();
    }

    protected void btn_valpro_Click(object sender, EventArgs e)
    {
        Metodo_Buscar_Propietario();
    }
    protected void btn_valcliente_Click(object sender, EventArgs e)
    {
        Metodo_Buscar_Cliente();
    }
    protected void btn_val_contacto_Click(object sender, EventArgs e)
    {
        Metodo_Buscar_Contacto();
    }

    protected void ddl_pro_tipopersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarTipoDocumento(ddl_pro_tipodoc, ddl_pro_tipopersona.SelectedValue.ToString().Trim());
        elegirtipodocxpersona("1");
        hid_nid_propietario.Value = (ddl_pro_tipopersona.SelectedIndex == 0) ? "0" : hid_nid_propietario.Value.ToString();
    }
    protected void ddl_clie_tipopersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarTipoDocumento(ddl_clie_tipodoc, ddl_clie_tipopersona.SelectedValue.ToString().Trim());
        elegirtipodocxpersona("2");
        hid_nid_cliente.Value = (ddl_clie_tipopersona.SelectedIndex == 0) ? "0" : hid_nid_propietario.Value.ToString();
    }
    protected void ddl_cont_tipopersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarTipoDocumento(ddl_cont_tipodoc, ddl_cont_tipopersona.SelectedValue.ToString().Trim());
        elegirtipodocxpersona("3");
        hid_nid_contacto.Value = (ddl_cont_tipopersona.SelectedIndex == 0) ? "0" : hid_nid_propietario.Value.ToString();
    }

    protected void ddl_pro_tipodoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ddl_pro_tipodoc.SelectedValue.ToString().Equals("0"))
        {
            validarDocumento(ddl_pro_tipodoc, txt_pro_nro_doc);
        }
        else
        {
            txt_prop_apepaterno.Text = "";
            txt_prop_apematerno.Text = "";
            txt_pro_nro_doc.Text = "";
            txt_pro_telefono.Text = "";
            txt_pro_celular.Text = "";
            txt_pro_nom_rz.Text = "";
        }
    }
    protected void ddl_clie_tipodoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ddl_clie_tipodoc.SelectedValue.ToString().Equals("0"))
        {
            validarDocumento(ddl_clie_tipodoc, txt_clie_nro_doc);
        }
        else
        {
            txt_clie_apepaterno.Text = "";
            txt_clie_apematerno.Text = "";
            txt_clie_nro_doc.Text = "";
            txt_clie_telefono.Text = "";
            txt_clie_celular.Text = "";
            txt_clie_nom_rz.Text = "";
            txt_clie_email.Text = "";
            //@006 I
            txtTelefonoFijo_Anexo.Text = "";
            hfPaisTelefonoCelular.Value = "";
            hfPaisTelefonoFijo.Value = "";
            //@006 F
        }
    }
    protected void ddl_cont_tipodoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ddl_cont_tipodoc.SelectedValue.ToString().Equals("0"))
        {
            validarDocumento(ddl_cont_tipodoc, txt_cont_nro_doc);
        }
        else
        {
            txt_cont_apepaterno.Text = "";
            txt_cont_apematerno.Text = "";
            txt_cont_nro_doc.Text = "";
            txt_cont_telefono.Text = "";
            txt_cont_celular.Text = "";
            txt_cont_nom_rz.Text = "";
            txt_cont_email.Text = "";
        }
    }

    protected void ddl_marca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (!ddl_marca.SelectedValue.Equals("0"))
            {
                objEnt.nid_marca = int.Parse(ddl_marca.SelectedValue.ToString().Trim());

                this.ddl_modelo.DataSource = objNeg.GETListarModelosXMarca(objEnt, Profile.Usuario.NID_USUARIO);
                this.ddl_modelo.DataTextField = "DES";
                this.ddl_modelo.DataValueField = "ID";
                this.ddl_modelo.DataBind();
            }

            this.ddl_modelo.Items.Insert(0, new ListItem(oParm._SELECCIONE, ""));
        }
        catch
        {

        }
    }
    
    protected void ddl_cont_dep_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_cont_prov.Items.Clear();
        ddl_cont_dist.Items.Clear();

        if (ddl_cont_dep.SelectedIndex != 0)
        {
            AppMiTaller.Intranet.BL.UbigeoBL oUbigeoBL = new AppMiTaller.Intranet.BL.UbigeoBL();
            ddl_cont_prov.DataSource = oUbigeoBL.GetListaProvincia(ddl_cont_dep.SelectedValue.ToString());
            ddl_cont_prov.DataValueField = "codprov";
            ddl_cont_prov.DataTextField = "nombre";
            ddl_cont_prov.DataBind();
            ddl_cont_prov.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            ddl_cont_dist.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }
    }
    protected void ddl_cont_prov_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_cont_dist.Items.Clear();

        if (ddl_cont_prov.SelectedIndex != 0)
        {
            AppMiTaller.Intranet.BL.UbigeoBL oUbigeoBL = new AppMiTaller.Intranet.BL.UbigeoBL();
            ddl_cont_dist.DataSource = oUbigeoBL.GetListaDistrito(ddl_cont_dep.SelectedValue.ToString(), ddl_cont_prov.SelectedValue.ToString());
            ddl_cont_dist.DataValueField = "coddist";
            ddl_cont_dist.DataTextField = "nombre";
            ddl_cont_dist.DataBind();
            ddl_cont_dist.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }
    }
}
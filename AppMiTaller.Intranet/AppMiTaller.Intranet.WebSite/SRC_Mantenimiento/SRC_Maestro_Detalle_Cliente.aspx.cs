using System;
using System.Configuration;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public partial class SRC_Mantenimiento_SRC_Maestro_Detalle_Cliente : System.Web.UI.Page
{

    [WebMethod]
    public static string[] GuardarDireccionTaller(string[] filtro)
    {
        ClienteBE oMestroClienteBE = new ClienteBE();
        string[] res = new string[] { "", "" };

        oMestroClienteBE.nid_cliente_direccion = int.Parse(filtro[1].ToString().Trim());
        oMestroClienteBE.nid_cliente = int.Parse(filtro[2].ToString().Trim());
        oMestroClienteBE.nu_telefono = filtro[3].ToString().Trim();
        oMestroClienteBE.nu_fax = filtro[4].ToString().Trim();
        oMestroClienteBE.nid_pais = int.Parse(filtro[5].ToString().Trim());
        oMestroClienteBE.no_direccion = filtro[6].ToString().Trim();
        oMestroClienteBE.coddpto = filtro[7].ToString().Trim();
        oMestroClienteBE.codprov = filtro[8].ToString().Trim();
        oMestroClienteBE.coddist = filtro[9].ToString().Trim();
        oMestroClienteBE.no_correo = filtro[10].ToString().Trim();
        oMestroClienteBE.co_usuario = filtro[11].Trim();
        oMestroClienteBE.no_usuario_red = filtro[12].Trim();
        oMestroClienteBE.no_estacion_red = filtro[13].Trim();

        string oRespuesta = ClienteBL.GuardarDireccionTaller(oMestroClienteBE);
        string sMensaje = string.Empty;

        if (string.IsNullOrEmpty(oRespuesta))
        {
            //Error aplicacion
            sMensaje = "-1|No se pudo realizar la acción.\\nConsulte con el administrador.";
        }
        else if (oRespuesta.Split('|').GetValue(0).ToString().Equals("0"))
        {
            //Error BD al Importar los datos.
            int retorno = int.Parse(oRespuesta.Split('|').GetValue(1).ToString());
            if (retorno == -4) sMensaje = "No se pudo realizar la acción.\nNo existe la llave foránea.";
            else if (retorno == -3) sMensaje = "No se pudo realizar la acción.\nSe ingresó un valor nulo en un campo no permitido.";
            else if (retorno == -2) sMensaje = "No se pudo realizar la acción.\nSe ingresó un valor duplicado.";
            else if (retorno == -1) sMensaje = "No se pudo realizar la acción.\nConsulte con el administrador.";
            else if (retorno == -5) sMensaje = "No se pudo realizar la acción.\nNombre ingresado ya existe.";
            else sMensaje = "No se pudo realizar la acción.\nConsulte con el administrador.";

            sMensaje = "-1|" + sMensaje;
        }
        else if (oRespuesta.Split('|').GetValue(0).ToString().Equals("1"))
        {
            sMensaje = "1|" + oRespuesta.Split('|').GetValue(2).ToString();
        }

        return sMensaje.Split('|');
    }
    [WebMethod]
    public static string[] ListarDatoDireccion(string[] filtro)
    {
        int nid_cliente = int.Parse(filtro[1]);
        ClienteBE Entidad = ClienteBL.ListarDatosClienteDireccion(nid_cliente);

        string[] datos = new string[16] { "0", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        ArrayList oListaJ = new ArrayList();
        if (Entidad.nid_cliente != 0) //<> null
        {
            datos[0] = Entidad.nid_cliente.ToString();
            datos[1] = Entidad.nid_cliente_direccion.ToString();
            datos[2] = Entidad.nu_telefono.ToString();
            datos[3] = Entidad.nu_fax.ToString();
            datos[4] = Entidad.nid_pais.ToString();
            datos[5] = Entidad.no_direccion.ToString();
            datos[6] = Entidad.coddpto.ToString().Trim();
            datos[7] = Entidad.codprov.ToString().Trim();
            datos[8] = Entidad.coddist.ToString().Trim();
            datos[9] = Entidad.no_ubigeo.ToString();
            datos[10] = Entidad.no_correo.ToString();
        }
        return datos;
    }

    [WebMethod]
    public static string ListarDepartamento(string[] filtro)
    {
        UbigeoBL oUbigeoBL = new UbigeoBL();
        List<UbigeoBE> oLista = oUbigeoBL.GetListaDepartamento();

        System.Collections.ArrayList oListaJ = new System.Collections.ArrayList();
        oLista.ForEach(oEntidad => oListaJ.Add(new { id = oEntidad.coddpto.Trim(), des = oEntidad.nombre.Trim() }));

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(oListaJ);
    }
    [WebMethod]
    public static string ListarProvincia(string[] filtro)
    {
        UbigeoBL oUbigeoBL = new UbigeoBL();
        string codDep = filtro[1].Trim();
        List<UbigeoBE> oLista = oUbigeoBL.GetListaProvincia(codDep);

        System.Collections.ArrayList oListaJ = new System.Collections.ArrayList();
        oLista.ForEach(oEntidad => oListaJ.Add(new { id = oEntidad.codprov.Trim(), des = oEntidad.nombre.Trim() }));

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(oListaJ);
    }
    [WebMethod]
    public static string ListarDistrito(string[] filtro)
    {
        UbigeoBL oUbigeoBL = new UbigeoBL();
        string codDep = filtro[1].Trim();
        string codProv = filtro[2].Trim();
        List<UbigeoBE> oLista = oUbigeoBL.GetListaDistrito(codDep, codProv);

        System.Collections.ArrayList oListaJ = new System.Collections.ArrayList();
        oLista.ForEach(oEntidad => oListaJ.Add(new { id = oEntidad.coddist.Trim(), des = oEntidad.nombre.Trim() }));

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(oListaJ);
    }
    //@003-F

    ClienteBE oMestroClienteBE;
    ClienteBL objNeg = new ClienteBL();
    MaestroVehiculoBL objNegVeh = new MaestroVehiculoBL();
    Parametros oParm = new Parametros();


    private void limpiar_objetos()
    {
        txt_grmaterno.Text = "";
        txt_grnombres.Text = "";
        txt_grpaterno.Text = "";
        txt_grtelfijo.Text = "";
        txt_grtelfijo2.Text = "";
        txt_grtelmovil.Text = "";
        txt_grtelmovil2.Text = "";
        txt_grnrodoc.Text = "";
        txt_gremail.Text = "";
        txt_gremail_alter.Text = "";
        txt_gremail_trab.Text = "";
        txtTelefonoFijo_Anexo.Value = "";
        hfPaisTelefonoCelular.Value = "";
        hfPaisTelefonoFijo.Value = "";
        ddl_estado.SelectedIndex = 0;
    }
    private void Inicializa()
    {
        int id_cliente = 0;


        if (Request.QueryString["nid_cliente"] != null)
            id_cliente = int.Parse(Request.QueryString["nid_cliente"]);

        ViewState["nid_cliente"] = id_cliente.ToString();
        hdIDCliente.Value = id_cliente.ToString();
        btnModificarD.Visible = (Master.ValidaAccesoOpcion(ConstanteBE.SRC_MantCliente_AccionEditarDireccion).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion) && oParm.SRC_Pais.Equals(1) && !id_cliente.Equals(0));

        if (oParm.SRC_Pais == 1)
            txt_grnrodoc.Attributes.Add("onkeypress", "return SoloNumeros(event)");
        else
            txt_grnrodoc.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");

        limpiar_objetos();


        ddl_clie_tipopersona.DataSource = objNegVeh.GETListarTipoPersona();
        ddl_clie_tipopersona.DataTextField = "DES";
        ddl_clie_tipopersona.DataValueField = "ID";
        ddl_clie_tipopersona.DataBind();
        ddl_clie_tipopersona.Items.Insert(0, new ListItem("--Seleccione--", ""));

        ddl_grtipodocumento.Items.Clear();
        ddl_grtipodocumento.Items.Insert(0, new ListItem("--Seleccione--", ""));


        ddl_estado.Items.Clear();
        ddl_estado.Items.Add(new ListItem("--Seleccione--", ""));
        ddl_estado.Items.Add(new ListItem("Activo", "0"));
        ddl_estado.Items.Add(new ListItem("Inactivo", "1"));
        ddl_estado.SelectedIndex = 1;
        ddl_estado.Enabled = false;

        try
        {

            ClienteBL oClienteBL = new ClienteBL();

            if (!id_cliente.Equals(0))
            {
                ClienteBL oMaestroClienteBL = new ClienteBL();
                oMestroClienteBE = new ClienteBE();
                oMestroClienteBE = oMaestroClienteBL.ListarDatosCliente(id_cliente);

                if (oMestroClienteBE == null || oMestroClienteBE.nid_cliente.Equals(0))
                {
                    return;
                }
                ddl_estado.Enabled = true;

                ddl_clie_tipopersona.SelectedValue = oMestroClienteBE.co_tipo_cliente.ToString();
                ddl_clie_tipopersona_SelectedIndexChanged(this, null);
                ddl_grtipodocumento.SelectedValue = oMestroClienteBE.co_tipo_documento.ToString().Trim();
                txt_grmaterno.Text = oMestroClienteBE.no_ape_mat.ToString();
                txt_grnombres.Text = oMestroClienteBE.no_cliente.ToString();
                txt_grnrodoc.Text = oMestroClienteBE.nu_documento.ToString();
                txt_grpaterno.Text = oMestroClienteBE.no_ape_pat.ToString();
                txt_grtelfijo.Text = oMestroClienteBE.nu_telefono.ToString();
                txt_grtelfijo2.Text = oMestroClienteBE.nu_tel_oficina.ToString();
                txt_grtelmovil.Text = oMestroClienteBE.nu_celular.ToString();
                txtTelefonoFijo_Anexo.Value = oMestroClienteBE.nu_anexo_telefono.ToString();
                hfPaisTelefonoCelular.Value = oMestroClienteBE.nid_pais_celular.ToString();
                hfPaisTelefonoFijo.Value = oMestroClienteBE.nid_pais_telefono.ToString();
                txt_grtelmovil2.Text = oMestroClienteBE.nu_celular_alter.ToString();
                txt_gremail.Text = oMestroClienteBE.no_correo.ToString();
                txt_gremail_trab.Text = oMestroClienteBE.no_correo_trabajo.ToString();
                txt_gremail_alter.Text = oMestroClienteBE.no_correo_alter.ToString();
                ddl_estado.SelectedValue = oMestroClienteBE.fl_inactivo.ToString().Trim();

                if (oMestroClienteBE.fl_identidad_validada.ToString().Trim().Equals("1"))
                {
                    trPersona_Mensaje.Visible = true;
                    txt_grmaterno.Enabled = false;
                    txt_grpaterno.Enabled = false;
                    txt_grnombres.Enabled = false;
                }

                ViewState["co_tipo_doc"] = oMestroClienteBE.co_tipo_documento.ToString().Trim();
                ViewState["nu_documento"] = oMestroClienteBE.nu_documento.ToString().Trim();

            }
        }
        catch (Exception)
        {

        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Inicializa();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>loadComboPaises();</script>", false); 

        }
    }
    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {

        int IDCliente = int.Parse(ViewState["nid_cliente"].ToString());
        Int32 oResp = 0;
        Int32 ind_exis = 0;
        string strMensaje = string.Empty;

        oMestroClienteBE = new ClienteBE();

        oMestroClienteBE.nid_cliente = IDCliente;

        oMestroClienteBE.co_tipo_documento = ddl_grtipodocumento.SelectedValue.ToString().Trim();
        oMestroClienteBE.nu_documento = txt_grnrodoc.Text.Trim();
        oMestroClienteBE.no_cliente = txt_grnombres.Text.Trim();
        oMestroClienteBE.no_ape_pat = txt_grpaterno.Text.Trim();
        oMestroClienteBE.no_ape_mat = txt_grmaterno.Text.Trim();
        oMestroClienteBE.nu_telefono = txt_grtelfijo.Text.Trim();
        oMestroClienteBE.nu_tel_oficina = txt_grtelfijo2.Text.Trim();
        oMestroClienteBE.nu_celular = txt_grtelmovil.Text.Trim();
        oMestroClienteBE.nu_celular_alter = txt_grtelmovil2.Text.Trim();
        oMestroClienteBE.no_correo = txt_gremail.Text.Trim();
        oMestroClienteBE.no_correo_trabajo = txt_gremail_trab.Text.Trim();
        oMestroClienteBE.no_correo_alter = txt_gremail_alter.Text.Trim();
        oMestroClienteBE.co_usuario_crea = Profile.UserName;
        oMestroClienteBE.co_usuario_red = Profile.UsuarioRed;
        oMestroClienteBE.no_estacion_red = Profile.Estacion;
        oMestroClienteBE.fl_inactivo = ddl_estado.SelectedValue.ToString();

        Int32 nid_pais_telefono; Int32.TryParse(hfPaisTelefonoFijo.Value, out nid_pais_telefono);
        Int32 nid_pais_celular; Int32.TryParse(hfPaisTelefonoCelular.Value, out nid_pais_celular);

        oMestroClienteBE.nu_anexo_telefono = txtTelefonoFijo_Anexo.Value;
        oMestroClienteBE.nid_pais_celular = nid_pais_celular;
        oMestroClienteBE.nid_pais_telefono = nid_pais_telefono;

        if (IDCliente.Equals(0))
        {
            oMestroClienteBE.ind_accion = "1";
            strMensaje = "El registro se guardo con exito.";

            ind_exis = objNeg.SRC_SPS_VAL_CLIENTE_X_DOC(oMestroClienteBE);
        }
        else
        {
            oMestroClienteBE.ind_accion = "2";
            strMensaje = "El registro se actualizo con exito.";

            string tipo_doc = ViewState["co_tipo_doc"].ToString();
            string num_doc = ViewState["nu_documento"].ToString();


            if (oMestroClienteBE.co_tipo_documento.Trim().Equals(tipo_doc) && oMestroClienteBE.nu_documento.Trim().Equals(num_doc))
            {
                ind_exis = 0;
            }
            else
            {
                ind_exis = objNeg.SRC_SPS_VAL_CLIENTE_X_DOC(oMestroClienteBE);
            }
        }



        if (ind_exis == 0)
        {
            oResp = objNeg.GETInserActuCliente(oMestroClienteBE);
            if (oResp > 0)
            {
                Parametros.SRC_Mensaje_Redireccionar(this, strMensaje, "SRC_Maestro_Cliente.aspx");
            }
            else
            {
                Parametros.SRC_Mensaje(this, "Error al guardar.");
            }
        }
        else
        {
            Parametros.SRC_Mensaje(this, "Documento ya existe.");
        }

    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SRC_Maestro_Cliente.aspx");
    }
    protected void btn_msgboxconfir_no_Click(object sender, EventArgs e)
    {
        popup_msgbox_confirm.Hide();
        Response.Redirect("SRC_Maestro_Cliente.aspx");
    }
    protected void btn_alertaconfir_aceptar_Click(object sender, EventArgs e)
    {
        popup_alerta_msj.Hide();
    }
    protected void ddl_clie_tipopersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string oruc = ConfigurationManager.AppSettings["PersonaJuridica"].ToString().Trim();

            ddl_grtipodocumento.DataSource = objNeg.GETListarTipoDocumento(ddl_clie_tipopersona.SelectedValue.ToString().Trim());
            ddl_grtipodocumento.DataTextField = "DES";
            ddl_grtipodocumento.DataValueField = "ID";
            ddl_grtipodocumento.DataBind();
            ddl_grtipodocumento.Items.Insert(0, new ListItem("--Seleccione--", "0"));


            if (oruc.Equals(ddl_clie_tipopersona.SelectedValue.ToString().Trim()))
            {
                Label1.Text = "Razon Social :";
                Label3.Visible = false;
                Label5.Visible = false;
                txt_grpaterno.Visible = false;
                txt_grmaterno.Visible = false;
                txt_grpaterno.Text = "";
                txt_grmaterno.Text = "";
            }
            else
            {
                Label1.Text = "Nombre(s) :";
                Label3.Visible = true;
                Label5.Visible = true;
                txt_grpaterno.Visible = true;
                txt_grmaterno.Visible = true;
                txt_grpaterno.Text = "";
                txt_grmaterno.Text = "";
            }
            ddl_grtipodocumento_SelectedIndexChanged(null, null);
        }
        catch (Exception)
        {

        }
    }
    protected void ddl_grtipodocumento_SelectedIndexChanged(object sender, EventArgs e)
    {

        string oDNI = ConfigurationManager.AppSettings["TIPODOCDNI"].ToString();
        string oRUC = ConfigurationManager.AppSettings["TIPODOCRUC"].ToString();

        txt_grnrodoc.Text = string.Empty;

        if (oParm.SRC_CodPais.Equals(1))
        {
            if (ddl_grtipodocumento.SelectedValue.ToString().Equals(oDNI))
            {
                txt_grnrodoc.MaxLength = 8;
                txt_grnrodoc.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            }
            else if (ddl_grtipodocumento.SelectedValue.ToString().Equals(oRUC))
            {
                txt_grnrodoc.MaxLength = 11;
                txt_grnrodoc.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            }
            else
            {
                txt_grnrodoc.MaxLength = 20;
                txt_grnrodoc.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
            }
        }
        else
        {
            if (ddl_grtipodocumento.SelectedValue.ToString().Equals(oDNI))
            {
                txt_grnrodoc.MaxLength = 9;
                txt_grnrodoc.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
            }
            else if (ddl_grtipodocumento.SelectedValue.ToString().Equals(oRUC))
            {
                txt_grnrodoc.MaxLength = 11;
                txt_grnrodoc.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            }
            else
            {
                txt_grnrodoc.MaxLength = 20;
                txt_grnrodoc.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
            }
        }

    }

}
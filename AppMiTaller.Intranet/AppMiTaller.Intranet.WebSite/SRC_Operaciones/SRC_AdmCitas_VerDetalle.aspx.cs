using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Drawing;
using System.Linq;

public partial class SRC_Operaciones_SRC_AdmCitas_VerDetalle : System.Web.UI.Page
{
    public static string placa_defecto = "";
    public static bool open_contact_center = false;
    private CitasBE oCitasBE;
    private CitasBEList oCitasBEList;

    AdminCitaBE objEntAdminCitas = new AdminCitaBE();
    ClienteBE objEntCliente = new ClienteBE();

    AdminCitasBL objNegAdmCitas = new AdminCitasBL();

    string strEstadoCita = string.Empty;

    private const string imgURL_HORA_LIBRE = "~/Images/SRC/SI.PNG";
    private const string imgURL_HORA_RESERVADA = "~/Images/SRC/NO.PNG";
    private const string imgURL_HORA_VACIA = "~/Images/SRC/vacio.PNG";
    private const string imgURL_HORA_EXCEPCIONAL = "~/Images/SRC/vacio.PNG";
    private const string imgURL_HORA_SEPARADA = "~/Images/SRC/SEPARA.PNG";

    private const string _SELECCIONAR = "--Seleccionar--";
    private const string _TODOS = "---- Todos ----";
    private const string _VACIO = "      ---      ";

    private const Int32 _WIDTH_COL_PUNTORED = 170;
    private const Int32 _WIDTH_COL_TALLER = 100;
    private const Int32 _WIDTH_COL_DIRECCION = 250;
    private const Int32 _WIDTH_COL_ASESOR = 200;
    private const Int32 _WIDTH_COL_TCA = 20;
    private const Int32 _WIDTH_COL_TCE = 20;
    private const Int32 _WIDTH_COL_HORAS = 45;

    private const string _DATA_PUNTORED = "PUNTO_RED";
    private const string _DATA_TALLER = "TALLER";
    private const string _DATA_DIRECCION = "DIRECCION";

    private const string _DATA_ASESOR = "ASESOR_SERVICIO";
    private const string _HEADER_PUNTORED = "Punto de Red";
    private const string _HEADER_TALLER = "Taller";
    private const string _HEADER_DIRECCION = "Dirección";
    private const string _HEADER_ASESOR = "Asesor de Servicio";

    private const string _MSG_PREGUNTA = "¿ Esta seguro de {estado} la Cita ?.";
    private const string _MSG_RESPUESTA = "La Cita quedó {estado}.";

    Parametros oParametros = new Parametros();

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Anular_Cita.Visible = OpcionAnuCita();
        //btn_Confirmar_Cita.Visible = OpcionConfCita();
        btn_Confirmar_Cita.Visible = false;
        btn_Reprog_Cita.Visible = OpcionReprogCita();
        BtnAtenderCita.Visible = OpcionAtenderCita();

        if (!Page.IsPostBack)
        {
            if (Request.Form["placa"] != null && Request.Form["cita"] != null)
            {
                AdminCitaBE oAdminCitaBE = new AdminCitaBE();
                AdminCitasBL oAdminCitasBL = new AdminCitasBL();
                int cita = int.Parse(Request.Form["cita"].ToString());
                placa_defecto = Request.Form["placa"].ToString();
                #region Inicializa datos Secundarios
                oAdminCitaBE.cod_reserva_cita = "";
                oAdminCitaBE.coddpto = "0";
                oAdminCitaBE.codprov = "0";
                oAdminCitaBE.coddist = "0";
                oAdminCitaBE.nid_ubica = 0;
                oAdminCitaBE.nid_taller = 0;
                oAdminCitaBE.AsesorServicio = "";
                oAdminCitaBE.Estadoreserva = "0";
                oAdminCitaBE.IndPendiente = "2";
                oAdminCitaBE.fecreg1 = "";
                oAdminCitaBE.fecreg2 = "";
                oAdminCitaBE.feccita1 = "";
                oAdminCitaBE.feccita2 = "";
                oAdminCitaBE.horacita1 = "";
                oAdminCitaBE.horacita2 = "";
                oAdminCitaBE.nid_marca = 0;
                oAdminCitaBE.nid_modelo = 0;
                oAdminCitaBE.co_tipo_documento = "0";
                oAdminCitaBE.nu_documento = "";
                oAdminCitaBE.no_cliente = "";
                oAdminCitaBE.no_apellidos = "";
                oAdminCitaBE.paginacion = 0;
                #endregion
                oAdminCitaBE.nu_placa = placa_defecto;
                AdminCitaBEList lista = oAdminCitasBL.GETListaAdminCitasP(oAdminCitaBE, Profile.Usuario.Nid_usuario);
                if (lista.Count > 0)
                {
                    oAdminCitaBE = new AdminCitaBE();
                    oAdminCitaBE = lista.Where(x => x.grid_PlacaPatente.Equals(placa_defecto)).FirstOrDefault();

                    if (oAdminCitaBE != null)
                    {
                        Session["oAdminCitaBE"] = oAdminCitaBE;
                        open_contact_center = true;
                    }

                }
                else
                {
                    open_contact_center = false;
                }
            }
            else
            {
                open_contact_center = false;
            }

            lblTextoPlaca.Text = oParametros.N_Placa;
            lblTextoPlacaCA.Text = oParametros.N_Placa;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "__LoadComboPaises__", "<script>loadComboPaises();</script>", false);
            Inicializa();
        }
    }

    #region "METODOS PROPIOS"

    private string getDiaSemanaConfig(Int32 intDia)
    {
        string strDia = string.Empty;
        switch (intDia)
        {
            case 1: strDia = "Lunes"; break;
            case 2: strDia = "Martes"; break;
            case 3: strDia = "Miércoles"; break;
            case 4: strDia = "Jueves"; break;
            case 5: strDia = "Viernes"; break;
            case 6: strDia = "Sábado"; break;
            case 7: strDia = "Domingo"; break;
        }
        return strDia;
    }
    private int getIndiceHoraRecord(string strH)
    {
        if (strH.Equals("0")) return 0;

        strH = Convert.ToDateTime(strH).ToString("HH");
        CitasBL objBLT = new CitasBL();
        List<CitasBE> lstCita = objBLT.Listar_HorarioRecordatorio();

        if (lstCita.Count == 0) return 0;

        string dtHI = lstCita[0].no_valor;
        string dtHF = lstCita[1].no_valor;
        Int32 intINTERV = Convert.ToInt32(lstCita[2].no_valor);

        lstCita = null;
        objBLT = null;

        DateTime dtHoraIni = Convert.ToDateTime(dtHI);
        DateTime dtHoraFin = Convert.ToDateTime(dtHF);

        Int32 intIndex = 1;

        while (dtHoraIni < dtHoraFin)
        {
            if (dtHoraIni.ToString("HH").Equals(strH))
            {
                break;
            }
            intIndex = intIndex + 1;
            dtHoraIni = dtHoraIni.AddMinutes(intINTERV);
        }
        return intIndex;
    }
    private void Combo_Estado()
    {

        cbo_det_citaestado.DataSource = objNegAdmCitas.GETListarEstCitas();
        cbo_det_citaestado.DataValueField = "ID";
        cbo_det_citaestado.DataTextField = "DES";
        cbo_det_citaestado.DataBind();
    }
    private void Cargar_Datos(AdminCitaBE ent)
    {
        AdminCitaBEList objDatos = new AdminCitaBEList();

        objDatos = objNegAdmCitas.GETListaAdminCitasDetalle(ent);

        if (objDatos.Count == 1)
        {
            lblTextoTipoDoc.Text = objDatos[0].co_tipo_documento.Trim().Equals("01") ? (oParametros.SRC_Pais.Equals(1) ? "DNI" : "RUT") : (objDatos[0].co_tipo_documento.Trim().Equals("03") ? "RUC" : "Carnet Extranjería");
            txt_det_citafecha.Text = objDatos[0].DET_Fecha.ToString().Trim();
            txt_det_citahora.Text = FormatoHora(objDatos[0].DET_Hora.ToString().Trim());
            txt_det_citaasesorserv.Text = objDatos[0].DET_AsesorServicio.ToString().Trim();
            txt_det_citaubicacion.Text = objDatos[0].DET_Ubicacion.ToString().Trim();
            txt_det_citaCodigo.Text = objDatos[0].DET_CodigoReserva.ToString().Trim();
            txt_det_citaServicio.Text = objDatos[0].DET_Servicio.ToString().Trim();
            cbo_det_citaestado.SelectedItem.Text = objDatos[0].DET_Estado.ToString().Trim();

            txt_det_clinombre.Text = objDatos[0].DET_Nombre.ToString().Trim();
            txt_det_cliapellidospat.Text = objDatos[0].DET_ApePat.ToString().Trim();
            txt_det_cliapellidosmat.Text = objDatos[0].DET_ApeMat.ToString().Trim();
            txt_det_clidnirut.Text = objDatos[0].DET_DNI.ToString().Trim();
            txt_det_clitelffijo.Text = objDatos[0].DET_TelfFijo.ToString().Trim();
            txt_det_clitelfoficina.Text = objDatos[0].DET_TelfOficina.ToString().Trim();
            txt_det_clitelfmovil.Text = objDatos[0].DET_TelfMovil.ToString().Trim().Contains("-") ? objDatos[0].DET_TelfMovil.ToString().Trim().Split('-').GetValue(1).ToString() : objDatos[0].DET_TelfMovil.ToString().Trim();
            txt_det_clitelfmovil2.Text = objDatos[0].DET_TelfMovil2.ToString().Trim().Contains("-") ? objDatos[0].DET_TelfMovil2.ToString().Trim().Split('-').GetValue(1).ToString() : objDatos[0].DET_TelfMovil2.ToString().Trim();
            txt_det_cliemail.Text = objDatos[0].DET_Email.ToString().Trim();
            txt_det_cliemail_trab.Text = objDatos[0].DET_Email_Trab.ToString().Trim();
            txt_det_cliemail_alter.Text = objDatos[0].DET_Email_Alter.ToString().Trim();

            txt_det_vehplaca.Text = objDatos[0].DET_Placa.ToString().Trim();
            txt_det_vehmarca.Text = objDatos[0].DET_Marca.ToString().Trim();
            txt_det_vehmodelo.Text = objDatos[0].DET_Modelo.ToString().Trim();

            lblGridTipoServicio.Text = objDatos[0].DET_TipoServicio.ToString().Trim();
            lblGridServicio.Text = objDatos[0].DET_Servicio.ToString().Trim();
            lblTextoGridPlaca.Text = oParametros.N_Placa;
            lblGridPlaca.Text = objDatos[0].DET_Placa.ToString().Trim();
            lblGridMarca.Text = objDatos[0].DET_Marca.ToString().Trim();
            lblGridModelo.Text = objDatos[0].DET_Modelo.ToString().Trim();

            txtTelefonoFijo_Anexo.Text = objDatos[0].nu_anexo_telefono.ToString();
            hfPaisTelefonoCelular.Value = objDatos[0].nid_pais_celular.ToString();
            hfPaisTelefonoFijo.Value = objDatos[0].nid_pais_telefono.ToString();
            txt_det_observaciones.Text = objDatos[0].DET_Observaciones.ToString().Trim();

            if (objDatos[0].fl_identidad_validada.Equals(1))
            {
                trContacto_Mensaje.Visible = true;
                txt_det_clinombre.Enabled = false;
                txt_det_cliapellidospat.Enabled = false;
                txt_det_cliapellidosmat.Enabled = false;
            }
            else
            {
                trContacto_Mensaje.Visible = false;
                txt_det_clinombre.Enabled = true;
                txt_det_cliapellidospat.Enabled = true;
                txt_det_cliapellidosmat.Enabled = true;
            }

            //- 1 - REGISTRADA    
            //- 2 - REPROGRAMADA X   
            //- 3 - CANCELADA    
            //- 4 - CONFIRMADA    
            //- 5 - REASIGNADA	X
            //- 6 - ATENDIDA
            //- 7 - COLA ESPERA


            CitasBE objECita = new CitasBE();
            CitasBL objLCita = new CitasBL();

            objECita.nid_cita = Convert.ToInt32(hf_ID_CITA.Value.ToString());
            objECita.nid_asesor = Profile.Usuario.Nid_usuario; //@009 I/F

            string strEstado = objLCita.VerificarEstadoCita(objECita);

            objECita = null; objLCita = null;


            btn_Confirmar_Cita.Enabled = strEstado.Equals("EC01");
            btn_Anular_Cita.Enabled = (strEstado.Equals("EC01") || strEstado.Equals("EC04") || strEstado.Equals("EC07"));
            btn_Reprog_Cita.Enabled = (strEstado.Equals("EC01") || strEstado.Equals("EC04"));
            btn_det_ActCliente.Enabled = (strEstado.Equals("EC01") || strEstado.Equals("EC04") || strEstado.Equals("EC07"));
            btn_det_FormRecor.Enabled = (strEstado.Equals("EC01") || strEstado.Equals("EC04") || strEstado.Equals("EC07"));
            BtnAtenderCita.Enabled = (strEstado.Equals("EC01") || strEstado.Equals("EC04") || strEstado.Equals("EC000"));
            btnEditarObserv.Enabled = (strEstado.Equals("EC01") || strEstado.Equals("EC04"));

            //************************************//
            //Imprimir Cita por Cliente
            //-----------------------------

            CitasBE oCita;
            CitasBL oCitasBL = new CitasBL();

            oCita = new CitasBE();
            oCita.nid_cita = Int32.Parse(((AdminCitaBE)ent).grid_nid_cita);

            oCitasBEList = new CitasBEList();
            oCitasBEList = oCitasBL.GETListarDatosCita(oCita);
            oCita = new CitasBE();
            oCita = oCitasBEList[0];

            oCita.nu_callcenter = (!string.IsNullOrEmpty(oCita.nu_callcenter.Trim()) ? oCita.nu_callcenter.Trim() : ConfigurationManager.AppSettings["TelefCallCenter"].ToString());
            oCita.no_cliente = String.Concat(oCita.no_cliente.Trim(), " ", oCita.no_ape_paterno.Trim(), " ", oCita.no_ape_materno.Trim()).ToUpper();
            oCita.fecha_atencion = GetFechaLarga(oCita.fe_prog) + " - " + FormatoHora(oCita.ho_inicio_c);

            ViewState["id_asesor"] = oCita.Nid_usuario.ToString();
            ViewState["oCitaImprimir"] = oCita;
            ViewState["img_qr"] = objDatos[0].no_nombreqr;
        }
    }
    private void Inabilitar_Sectores(bool vbool)
    {
        tbl_cita.Enabled = vbool;
        tbl_cliente.Enabled = vbool;
        tbl_observacion.Enabled = vbool;
        tbl_recordatorio.Enabled = vbool;
        tbl_vehiculo.Enabled = vbool;
        tbl_observacion.Enabled = vbool;
    }
    private void ActualizarCliente(AdminCitaBE ent, ClienteBE entCliente)
    {
        int rpta = 0;
        rpta = objNegAdmCitas.GETListaAdminCitasDetalleUpdCliente(ent, entCliente);
        if (rpta >= 1) { Inabilitar_Sectores(false); }
        else { Inabilitar_Sectores(false); tbl_cliente.Enabled = true; }
    }
    
    private void Inicializa()
    {
        try
        {
            lblTextoPlaca.Text = oParametros.N_Placa.ToString();
            lblTextoLocalR.Text = oParametros.N_Local.ToString();
            lblTextoTallerR.Text = oParametros.N_Taller.ToString();

            txtkminicial.Attributes.Add("onkeypress", "return SoloNumeros(event)");
            txtkminicial.Attributes.Add("onpaste", "return false");

            objEntAdminCitas = (AdminCitaBE)(Session["oAdminCitaBE"]);

            hf_ID_CITA.Value = objEntAdminCitas.grid_nid_cita.ToString();
            hf_ID_MODELO.Value = objEntAdminCitas.grid_nid_modelo.ToString();
            hf_ID_SERVICIO.Value = objEntAdminCitas.grid_nid_servicioCita.ToString();

            Combo_Estado();
            
            Cargar_Datos(objEntAdminCitas);
            Inabilitar_Sectores(false);

            ceFecha.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            ceFechaIni.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            ceFechaFin.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

            txt_det_clitelfmovil.Style["width"] = oParametros.SRC_Pais.Equals(1) ? "100px" : "80px";
            txt_det_clitelfmovil2.Style["width"] = oParametros.SRC_Pais.Equals(1) ? "100px" : "80px";
            txt_det_clitelfmovilCod.Visible = oParametros.SRC_Pais.Equals(2);
            txt_det_clitelfmovil2Cod.Visible = oParametros.SRC_Pais.Equals(2);

            if (placa_defecto != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script type=\"text/javascript\">" + "FC_ReprogramacionAutomatico();</" + "script>"), false);
            }
            placa_defecto = "";
        }
        catch (Exception ex)
        {
            SRC_MsgInformacion(ex.Message);
        }
    }

    #endregion

    #region METODOS SEGURIDAD
    public bool OpcionAnuCita()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_AnuCita).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionConfCita()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_ConfCita).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionReprogCita()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_ReprogCita).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    public bool OpcionAtenderCita()
    {
        return (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_AdminCitas_AtenderCita).Equals(CONSTANTE_SEGURIDAD.AccesoEdicion);
    }
    #endregion


    #region "----------- MENSAJES --------------"

    private void MensajeScript(string SMS)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('" + SMS + "');</script>", false);
    }
    private void SRC_MsgInformacion(string strError)
    {
        MensajeScript(strError);
    }
    private void SRC_MsgExclamacion(string strError)
    {
        MensajeScript(strError);
    }
    private void SRC_MsgError(string strError)
    {
        MensajeScript(strError);
    }

    #endregion

    //***************************************************************************************************************/

    #region [-------- ENVIO EMAIL ----------------------]

    private void EnviarCorreo_Cliente(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        CorreoElectronico oCorreoElectronico = new CorreoElectronico(Server.MapPath("~/"));

        bool flEnvio = oCorreoElectronico.EnviarMensajeCorreo(oDatos, oTipoCita, Parametros.PERSONA.CLIENTE);
        if (!flEnvio)
        {
            //SRC_MsgInformacion("Error al enviar Correo-Cliente");
        }
    }
    private void EnviarCorreo_Asesor(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        CorreoElectronico oCorreoElectronico = new CorreoElectronico(Server.MapPath("~/"));

        bool flEnvio = oCorreoElectronico.EnviarMensajeCorreo(oDatos, oTipoCita, Parametros.PERSONA.ASESOR);
        if (!flEnvio)
        {
            //SRC_MsgInformacion("Error al enviar Correo-Asesor"); ;
        }
    }
    private void EnviarCorreo_CallCenter(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        CorreoElectronico oCorreoElectronico = new CorreoElectronico(Server.MapPath("~/"));

        bool flEnvio = oCorreoElectronico.EnviarMensajeCorreo(oDatos, oTipoCita, Parametros.PERSONA.CALL_CENTER);
        if (!flEnvio)
        {
            //SRC_MsgInformacion("Error al enviar Correo-CallCenter");
        }
    }

    #endregion

    //***************************************************************************************************************/

    #region "---------- COLA DE ESPERA ------------------"

    protected void gv_Cola_Espera_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRegresarColaEspera.Enabled = false;
        mpColaEspera.Hide();
        int intCita = Convert.ToInt32(gv_Cola_Espera.DataKeys[gv_Cola_Espera.SelectedRow.RowIndex].Value);

        RegistrarColaEspera(intCita);
    }
    protected void btnRegresarColaEspera_Click(object sender, ImageClickEventArgs e)
    {
        btnRegresarColaEspera.Enabled = false;
        mpColaEspera.Hide();
    }
    protected void RegistrarColaEspera(int intCita)
    {
        Parametros oParm = new Parametros();
        string noPais = (oParm.SRC_Pais == 1) ? "PE" : "CH";
        oParm = null;

        CitasBE objE = new CitasBE();
        CitasBL objL = new CitasBL();

        objE.nid_cita = intCita;
        objE.No_pais = noPais;
        objE.nid_asesor = Convert.ToInt32(ViewState["id_asesor"].ToString());
        objE.co_usuario_crea = Profile.UserName;
        objE.co_usuario_red = Profile.UsuarioRed;
        objE.no_estacion_red = Profile.Estacion;

        string oResp = objL.AsignarClienteColaEspera(objE);

        if (oResp.Trim().Equals("OK"))
        {
            //---
        }
        else
        {
            CitasBEList oCitasBEList = objL.GETListarDatosCita(objE);

            if (oCitasBEList.Count == 0) return;

            //EnviarCorreo_Cliente(oCitasBEList[0], Parametros.EstadoCita.REASIGNADA);
            
        }
        EstadoBotones(false, false, true);
        lbl_mensajebox.Text = _MSG_PREGUNTA.Replace("{estado}", "Anular");
        lbl_mensajebox.Text = "La Cita ha sido Asignada al Cliente.";
        hf_ESTADO_CITA.Value = "ColaEspera";
        popup_msgbox_confirm.Show();

        btnRegresarColaEspera.Enabled = true;
    }
    protected void gv_Cola_Espera_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        CitasBE objBE = new CitasBE();
        CitasBL objBL = new CitasBL();

        try
        {
            objBE.nid_cita = Convert.ToInt32(hf_ID_CITA.Value.ToString());

            gv_Cola_Espera.DataSource = objBL.GETListarClientesEnColaEspera(objBE);
            gv_Cola_Espera.DataBind();
        }
        catch //(Exception ex)
        {
            objBE = null;
            objBL = null;
        }
    }

    #endregion

    //***************************************************************************************************************/

    #region "------------- REPROGRAMAR CITA -------------------"

    private void setIconoGrilla(Int32 intFila, DateTime dtHora, string strIcon, Int32 intCE)
    {
        Int32 intCol = 0;

        foreach (DataControlField oColumn in gvHorarioReserva.Columns)
        {
            if ((oColumn.GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField")))
            {
                DateTime dtHoraGrilla = Convert.ToDateTime((((ButtonField)oColumn).DataTextField.Substring(((ButtonField)oColumn).DataTextField.IndexOf("_") + 1).Insert(2, ":")));
                DateTime dtHoraSig = dtHoraGrilla.AddMinutes(Convert.ToInt32(hf_INTERVALO_TALLER.Value.ToString()));

                if ((dtHora >= dtHoraGrilla & dtHora < dtHoraSig))
                {
                    DataControlFieldCell CTR = (DataControlFieldCell)gvHorarioReserva.Rows[intFila].Controls[intCol + (dtHora == dtHoraGrilla ? 0 : 1)];

                    foreach (Control _ctr in CTR.Controls)
                    {
                        if (_ctr.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                        {
                            ((ImageButton)_ctr).ImageUrl = strIcon;
                            if (intCE > 0) ((ImageButton)_ctr).ToolTip = intCE.ToString();
                            return;
                        }
                    }
                }
            }

            intCol += 1;

        }
    }

    private void cargarComboHorarioTaller(DropDownList ddlHora, DateTime dtHoraIni, DateTime dtHoraFin, Int32 intInterv, string strInd)
    {
        ddlHora.AutoPostBack = false;
        ddlHora.Items.Clear();

        while (dtHoraIni <= dtHoraFin)
        {
            ddlHora.Items.Add("");
            ddlHora.Items[ddlHora.Items.Count - 1].Text = FormatoHora(dtHoraIni.ToString("HH:mm"));
            ddlHora.Items[ddlHora.Items.Count - 1].Value = dtHoraIni.ToString("HH:mm");

            dtHoraIni = dtHoraIni.AddMinutes(intInterv);
        }

        ddlHora.SelectedIndex = (strInd.Equals("HI") ? 0 : ddlHora.Items.Count - 1);
        ddlHora.AutoPostBack = true;
    }

    private string FormatoHora(string strHora)
    {
        string strHF = strHora;
        try
        {

            string strHoraF = Convert.ToDateTime(strHora).ToString("hh:mm");
            int strHoraF1 = Convert.ToInt32(strHora.Replace(":", ""));
            strHF = strHoraF + ((strHoraF1 < 1200) ? " A.M." : " P.M.");

        }
        catch
        {
            strHF = strHora;
        }

        return strHF;
    }
    private string GetFechaLarga(DateTime dtFecha)
    {
        return dtFecha.ToString("D", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
    }
    private DateTime GetFecha(string strFecha)
    {
        DateTime strFechaR = DateTime.MinValue;

        char strSepara = Convert.ToChar(CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);

        if (!string.IsNullOrEmpty(strFecha))
        {
            strFechaR = new DateTime(Convert.ToInt32(strFecha.Split(strSepara)[2]), Convert.ToInt16(strFecha.Split(strSepara)[1]), Convert.ToInt16(strFecha.Split(strSepara)[0]));
        }
        return strFechaR;
    }

    private DateTime SRC_FECHA_HABIL()
    {
        CitasBL oCitasBL = new CitasBL();

        oCitasBE = new CitasBE();

        Int32 _ID_TALLER = Convert.ToInt32(hf_ID_TALLER.Value.ToString());
        Int32 _INTERVALO = 0;
        Int32 _ID_SERVICIO = Convert.ToInt32(hf_ID_SERVICIO.Value.ToString());
        Int32 _ID_MODELO = Convert.ToInt32(hf_ID_MODELO.Value.ToString());

        string _TALLER = string.Empty;

        DateTime dFechaIni = Convert.ToDateTime(GetFechaMinReserva());
        DateTime dFechaFin = Convert.ToDateTime(GetFechaMaxReserva());

        DateTime dHoraIni_T = DateTime.MinValue;
        DateTime dHoraFin_T = DateTime.MinValue;

        DateTime _dHoraIni_T = DateTime.MinValue;
        DateTime _dHoraFin_T = DateTime.MinValue;

        DateTime dHoraIni_A = DateTime.MinValue;
        DateTime dHoraFin_A = DateTime.MinValue;

        //CitasBEList _lstTalleres;
        CitasBEList _lstAsesores;
        CitasBEList _lstCitas;
        CitasBEList _lstTalleresHE;

        CitasBEList lstTalleres;
        CitasBEList lstAsesores;
        CitasBEList lstCitas;
        CitasBEList lstTalleresHE;

        string strTotalHE = string.Empty;

        DateTime dtHEITaller;
        DateTime dtHEFTaller;

        DateTime dtHoraI_ddl = DateTime.MaxValue;
        DateTime dtHoraF_ddl = DateTime.MinValue;

        Int32 hay1 = 0;
        Int32 hay2 = 0;


        ddlHoraInicio2.Items.Clear();
        ddlHoraFin2.Items.Clear();
        ddlHoraInicio2.Enabled = false;
        ddlHoraFin2.Enabled = false;

        try
        {
        //Recorremos Fecha por Fecha 
        //------------------------------------------------------

        Continue_While_1:
            while (dFechaIni <= dFechaFin)
            {
                //================================

                oCitasBE.nid_Servicio = _ID_SERVICIO;
                oCitasBE.nid_modelo = _ID_MODELO;
                oCitasBE.coddpto = "0";
                oCitasBE.codprov = "0";
                oCitasBE.coddis = "0";
                oCitasBE.nid_ubica = Convert.ToInt32((ddlPuntoRed.SelectedIndex <= 0 ? "0" : ddlPuntoRed.SelectedValue.ToString()));
                oCitasBE.nid_taller = _ID_TALLER;
                oCitasBE.fe_atencion = dFechaIni;
                oCitasBE.dd_atencion = getDiaSemana(dFechaIni);
                oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

                lstTalleres = oCitasBL.ListarTalleresDisponibles_PorFecha(oCitasBE);//   1-Listado todos Talleres
                ViewState["lstTalleres"] = lstTalleres;

                if (lstTalleres.Count == 0)
                {
                    dFechaIni = dFechaIni.AddDays(+1);
                    goto Continue_While_1;
                }

                _lstTalleresHE = oCitasBL.ListarHorarioExcepcional_Talleres(oCitasBE);// 2-Listado Horario Excepcionales
                ViewState["lstTalleresHE"] = _lstTalleresHE;

                _lstAsesores = oCitasBL.ListarAsesoresDisponibles_PorFecha(oCitasBE);//  3-Listado Asesores Talleres
                ViewState["lstAsesores"] = _lstAsesores;

                _lstCitas = oCitasBL.ListarCitasAsesores(oCitasBE);//                    4-Listado CitasAsesores Talleres
                ViewState["lstCitas"] = _lstCitas;

                //===========================================

                foreach (CitasBE oTaller in lstTalleres)
                {
                    _ID_TALLER = Convert.ToInt32(oTaller.nid_taller);
                    _INTERVALO = Convert.ToInt32(oTaller.qt_intervalo_atenc);

                    _dHoraIni_T = Convert.ToDateTime(oTaller.ho_inicio_t);
                    _dHoraFin_T = Convert.ToDateTime(oTaller.ho_fin_t);


                    //=================================================================================
                    //> Validaciones
                    //=================================================================================
                    if (oTaller.qt_cantidad_t <= 0) continue;//Capacidad Atencion Taller
                    if (oParametros.SRC_Pais.Equals(1))//07092012 -> SOLO PERU
                    {
                        if (oTaller.qt_cantidad_m <= 0) continue;//Capacidad Atencion Taller y Modelo
                    }
                    if (oTaller.nid_dia_exceptuado_t == 1) continue;//Dia Exceptuado  

                    //=================================================================================
                    //> Horas Excepcional del Taller
                    //=================================================================================
                    // FILTRAR HORARIO EXCEPCIONAL
                    //-----------------------------

                    lstTalleresHE = new CitasBEList();

                    hay1 = 0;
                    hay2 = 0;
                    foreach (CitasBE oEntidad in _lstTalleresHE)
                    {
                        hay1 = 0;
                        if (oTaller.nid_taller.Equals(oEntidad.nid_taller))
                        {
                            hay1 = 1; hay2 = 1;
                            lstTalleresHE.Add(oEntidad);
                        }
                        if ((hay1 == 0) && (hay2 == 1))
                            break;
                    }

                    //-----------------------------------------------------------------------
                    strTotalHE = string.Empty;
                    foreach (CitasBE oHET in lstTalleresHE)
                    {
                        if (!string.IsNullOrEmpty(oHET.ho_rango1)) strTotalHE += oHET.ho_rango1 + "-";
                        if (!string.IsNullOrEmpty(oHET.ho_rango2)) strTotalHE += oHET.ho_rango2 + "-";
                        if (!string.IsNullOrEmpty(oHET.ho_rango3)) strTotalHE += oHET.ho_rango3 + "-";
                    }
                    if (!string.IsNullOrEmpty(strTotalHE))
                    {
                        strTotalHE = strTotalHE.Substring(0, strTotalHE.Length - 1);
                    }

                    //=================================================================================
                    //> Listado de Asesores 
                    //=================================================================================
                    // FILTRAR ASESORES - TALLER
                    //-----------------------------------

                    lstAsesores = new CitasBEList();// 2

                    hay1 = 0;
                    hay2 = 0;
                    foreach (CitasBE oEntidad in _lstAsesores)
                    {
                        hay1 = 0;
                        if (oTaller.nid_taller.Equals(oEntidad.nid_taller))
                        {
                            hay1 = 1; hay2 = 1;
                            //lstAsesores.Add(oEntidad);
                            if (oEntidad.qt_cantidad_a > 0) lstAsesores.Add(oEntidad);
                        }
                        if ((hay1 == 0) && (hay2 == 1))
                            break;
                    }

                    //--------------------------------------------------------------

                    DateTime odHIA = _dHoraIni_T;
                    DateTime odHFA = _dHoraFin_T;

                    foreach (CitasBE oAsesor in lstAsesores)//==========================>>>>
                    {
                        //=================================================================================
                        //> Listar Citas Asesores 
                        //=================================================================================
                        // FILTRAR CITAS - TALLER - ASESOR
                        //-----------------------------------

                        lstCitas = new CitasBEList();

                        hay1 = 0;
                        hay2 = 0;
                        foreach (CitasBE oEntidad in _lstCitas)
                        {
                            hay1 = 0;
                            if (oTaller.nid_taller.Equals(oEntidad.nid_taller) && oAsesor.nid_asesor.Equals(oEntidad.nid_asesor))
                            {
                                hay1 = 1; hay2 = 1;
                                lstCitas.Add(oEntidad);
                            }
                            if ((hay1 == 0) && (hay2 == 1))
                                break;
                        }

                        //-------------------------------------------------------------
                        //Recorrer cada horario del Asesor
                        foreach (string strHorario in oAsesor.horario_asesor.Split('|'))
                        {
                            dHoraIni_A = Convert.ToDateTime(strHorario.Split('-').GetValue(0).ToString());
                            dHoraFin_A = Convert.ToDateTime(strHorario.Split('-').GetValue(1).ToString());

                            if (dHoraIni_A < odHIA) dHoraIni_A = odHIA;
                            if (dHoraFin_A > odHFA) dHoraFin_A = odHFA;

                            //----------------------------------------------------
                            //> Get menor HI y mayor HF de Horarios disponibles
                            //---------------------------------------------------- 
                            if (dHoraFin_A > dtHoraF_ddl) dtHoraF_ddl = dHoraFin_A;
                            if (dHoraIni_A < dtHoraI_ddl) dtHoraI_ddl = dHoraIni_A;

                            //swHorasT = true;

                            Continue_While_2:
                            while (dHoraIni_A < dHoraFin_A)
                            {
                                foreach (CitasBE oCita in lstCitas)
                                {
                                    DateTime dtHActual = dHoraIni_A;

                                    DateTime dtHICita = Convert.ToDateTime(oCita.ho_inicio_c);
                                    DateTime dtHFCita = Convert.ToDateTime(oCita.ho_fin_c);

                                    if (dtHActual >= dtHICita & dtHActual < dtHFCita)
                                    {
                                        dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                                        goto Continue_While_2; //> Es una Cita ya reservada
                                    }
                                }

                                // > Se verifica que la hora del Asesor no sea una hora excepcional

                                if (!string.IsNullOrEmpty(strTotalHE))
                                {
                                    foreach (string _strRangoHE in strTotalHE.Split('-'))
                                    {
                                        dtHEITaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(0));// Hora Inicial
                                        dtHEFTaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(1));// Hora Final

                                        //> Si es una hora excepcionl
                                        if (dHoraIni_A >= dtHEITaller & dHoraIni_A < dtHEFTaller)
                                        {
                                            dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                                            goto Continue_While_2; //> Es Horario Excepcional
                                        }
                                    }
                                }

                                //> FECHA ENCONTADA
                                //-------------------------
                                return dFechaIni;
                            }
                        }
                    }
                }

                dFechaIni = dFechaIni.AddDays(+1);
            }
        }
        catch (Exception ex)
        {
            string strError = ex.Message;
        }

        return dFechaFin;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsDate(txtFecha.Text))
            {
                DateTime dtFecha = Convert.ToDateTime(txtFecha.Text);
                DateTime dtFechaMin = Convert.ToDateTime(GetFechaMinReserva());
                DateTime dtFechaMax = Convert.ToDateTime(GetFechaMaxReserva());

                lblSeleccion1.Text = "";
                lblSeleccion2.Text = "";

                if (DateDiff(DateInterval.Day, dtFechaMin, dtFecha) < 0)
                {
                    txtFecha.Text = Convert.ToString(hfFecha.Value);
                    SRC_MsgInformacion("La fecha mínima para una reservaciòn es el " + GetFechaMinReserva());
                }
                else if (DateDiff(DateInterval.Day, dtFecha, dtFechaMax) < 0)
                {
                    txtFecha.Text = Convert.ToString(hfFecha.Value);
                    SRC_MsgInformacion("La fecha máxima para una reservaciòn es el " + GetFechaMaxReserva());
                }
                else
                {
                    hfFecha.Value = txtFecha.Text;
                    if (ddlHoraInicio1.Items.Count > 0)
                    {
                        ddlHoraInicio1.SelectedIndex = 0;
                        ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.Count - 1;
                    }
                    ConsultarHorarioReserva(); //--> GRID 1
                }

            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DateTime dtFechaMin = Convert.ToDateTime(GetFechaMinReserva());
        DateTime dtFechaMax = Convert.ToDateTime(GetFechaMaxReserva());

        DateTime dtFechaIni;
        DateTime dtFechaFin;

        if (IsDate(txtFechaIni.Text))
        {
            dtFechaIni = Convert.ToDateTime(txtFechaIni.Text);

            lblSeleccion1.Text = "";
            lblSeleccion2.Text = "";

            if (DateDiff(DateInterval.Day, dtFechaMin, dtFechaIni) < 0)
            {
                txtFechaIni.Text = Convert.ToString(hfFechaIni.Value);
                txtFechaFin.Text = Convert.ToString(hfFechaFin.Value);
                //SRC_MsgInformacion("La fecha mínima para una reservaciòn es el " + GetFechaMinReserva());//@003
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La fecha mínima para una reservaciòn es el " + GetFechaMinReserva()), false);//@003
            }
            else if (IsDate(txtFechaFin.Text))
            {
                dtFechaFin = Convert.ToDateTime(txtFechaFin.Text);

                if (DateDiff(DateInterval.Day, dtFechaMin, dtFechaFin) < 0)
                {
                    txtFechaIni.Text = Convert.ToString(hfFechaIni.Value);
                    txtFechaFin.Text = Convert.ToString(hfFechaFin.Value);
                    //SRC_MsgInformacion("La fecha mínima para una reservaciòn es el " + GetFechaMinReserva());
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La fecha mínima para una reservaciòn es el " + GetFechaMinReserva()), false);//@003
                }
                else if (DateDiff(DateInterval.Day, dtFechaIni, dtFechaFin) < 0)
                {
                    txtFechaIni.Text = Convert.ToString(hfFechaIni.Value);
                    txtFechaFin.Text = Convert.ToString(hfFechaFin.Value);
                    //SRC_MsgInformacion("La fecha inicial no debe ser mayor que la fecha final");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La fecha inicial no debe ser mayor que la fecha final"), false);//@003
                }
                else if (DateDiff(DateInterval.Day, dtFechaFin, dtFechaMax) < 0)
                {
                    txtFechaFin.Text = Convert.ToString(hfFechaFin.Value);
                    //SRC_MsgInformacion("La fecha máxima para una reservaciòn es el " + GetFechaMaxReserva());
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La fecha máxima para una reservaciòn es el " + GetFechaMaxReserva()), false);//@003
                }
                else
                {
                    hfFechaIni.Value = txtFechaIni.Text;
                    hfFechaFin.Value = txtFechaFin.Text;
                    if (ddlHoraInicio2.Items.Count > 0)
                    {
                        ddlHoraInicio2.SelectedIndex = 0;
                        ddlHoraFin2.SelectedIndex = ddlHoraFin2.Items.Count - 1;
                    }
                    MostrarHorarioDisponible();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>fc_mostrarFiltro();fc_filtrarGrilla()</script>", false);
                }
            }
            else if (DateDiff(DateInterval.Day, dtFechaIni, dtFechaMax) < 0)
            {
                txtFechaIni.Text = Convert.ToString(hfFechaIni.Value);
                //SRC_MsgInformacion("La fecha máxima para una reservaciòn es el " + GetFechaMaxReserva());
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La fecha máxima para una reservaciòn es el " + GetFechaMaxReserva()), false);
            }
            else
            {
                hfFechaIni.Value = txtFechaIni.Text;

                if (ddlHoraInicio2.Items.Count > 0)
                {
                    ddlHoraInicio2.SelectedIndex = 0;
                    ddlHoraFin2.SelectedIndex = ddlHoraFin2.Items.Count - 1;
                }
            }
        }
    }
    protected void imbFecAnt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (IsDate(txtFecha.Text))
            {
                lblSeleccion1.Text = "";
                lblSeleccion2.Text = "";


                if (!GetFechaMinReserva().Equals(txtFecha.Text))
                {
                    txtFecha.Text = GetFecha(txtFecha.Text).AddDays(-1).ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                    hfFecha.Value = txtFecha.Text;

                    if (ddlHoraInicio1.Items.Count > 0)
                    {
                        ddlHoraInicio1.SelectedIndex = 0;
                        ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.Count - 1;
                    }

                    ConsultarHorarioReserva();  //---> GRID 1
                }
                else
                {
                    SRC_MsgInformacion("La fecha mínima para una reservaciòn es el " + txtFecha.Text);
                }
            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }
    protected void imbFecSgte_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (IsDate(txtFecha.Text))
            {
                lblSeleccion1.Text = "";
                lblSeleccion2.Text = "";

                DateTime oFechaMax = Convert.ToDateTime(GetFechaMaxReserva());
                DateTime oFechaSig = Convert.ToDateTime(txtFecha.Text);

                if (DateDiff(DateInterval.Day, oFechaSig, oFechaMax) > 0)
                {
                    txtFecha.Text = GetFecha(txtFecha.Text).AddDays(+1).ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                    hfFecha.Value = txtFecha.Text;

                    if (ddlHoraInicio1.Items.Count > 0)
                    {
                        ddlHoraInicio1.SelectedIndex = 0;
                        ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.Count - 1;
                    }

                    ConsultarHorarioReserva();  //---> GRID 1
                }
                else
                {
                    SRC_MsgInformacion("La fecha máxima para una reservaciòn es el " + txtFechaFin.Text);
                }
            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }
    protected void ddlHoraInicio1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtHoraIni = Convert.ToDateTime(ddlHoraInicio1.SelectedValue);
        DateTime dtHoraFin = Convert.ToDateTime(ddlHoraFin1.SelectedValue);

        if (dtHoraIni >= dtHoraFin)
        {
            SRC_MsgInformacion("La hora inicial debe ser menor que la hora final.");
            ddlHoraInicio1.SelectedIndex = ddlHoraInicio1.Items.IndexOf(ddlHoraInicio1.Items.FindByValue(hfHoraIni1.Value.ToString()));
        }
        else
        {
            hfHoraIni1.Value = ddlHoraInicio1.Text;

            //CAMBIAR LA GRILLA
            ActualizarRangoHorario();
        }
    }
    protected void ddlHoraFin1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtHoraIni = Convert.ToDateTime(ddlHoraInicio1.SelectedValue.ToString());
        DateTime dtHoraFin = Convert.ToDateTime(ddlHoraFin1.SelectedValue.ToString());

        if (dtHoraIni >= dtHoraFin)
        {
            SRC_MsgInformacion("La hora final debe ser mayor que la hora inicial.");
            ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.IndexOf(ddlHoraFin1.Items.FindByValue(hfHoraFin1.Value.ToString()));
        }
        else
        {
            hfHoraFin1.Value = ddlHoraFin1.Text;

            //CAMBIAR LA GRILLA
            ActualizarRangoHorario();
        }
    }

    protected void lbTextoAqui_Click(object sender, EventArgs e)
    {
        if (txtFecha.Text.Trim().Equals("")) return;

        lblSeleccion2.Text = lblSeleccion1.Text;

        if (!txtFechaIni.Text.Trim().Equals(txtFecha.Text.Trim()))
        {
            hfFechaIni.Value = txtFecha.Text;// hfFecha.Value.ToString();
            hfFechaFin.Value = txtFecha.Text; //hfFecha.Value.ToString();

            txtFechaIni.Text = hfFechaIni.Value.ToString();
            txtFechaFin.Text = hfFechaIni.Value.ToString();

            MostrarHorarioDisponible();
        }

        //------------
        //Horas
        //--------

        if (ddlHoraInicio1.Items.Count > 0)
        {
            DateTime dtHoraIni1 = Convert.ToDateTime(ddlHoraInicio1.SelectedValue);
            DateTime dtHoraFin1 = Convert.ToDateTime(ddlHoraFin1.SelectedValue);

            DateTime dtHoraIni2 = Convert.ToDateTime(ddlHoraInicio2.SelectedValue);
            DateTime dtHoraFin2 = Convert.ToDateTime(ddlHoraFin2.SelectedValue);

            DateTime dtHI1 = Convert.ToDateTime(ddlHoraInicio1.Items[0].Value);
            DateTime dtHF1 = Convert.ToDateTime(ddlHoraFin1.Items[ddlHoraFin1.Items.Count - 1].Value);

            DateTime dtHI2 = Convert.ToDateTime(ddlHoraInicio2.Items[0].Value);
            DateTime dtHF2 = Convert.ToDateTime(ddlHoraFin2.Items[ddlHoraFin2.Items.Count - 1].Value);

            //----------

            if (dtHI2 > dtHoraIni1)
            {
                ddlHoraInicio2.SelectedIndex = 0;
            }
            else
            {
                ddlHoraInicio2.SelectedIndex = ddlHoraInicio2.Items.IndexOf(ddlHoraInicio2.Items.FindByValue(ddlHoraInicio1.SelectedItem.Value));
            }
            if (dtHoraFin1 > dtHF2)
            {
                ddlHoraFin2.SelectedIndex = ddlHoraFin2.Items.Count - 1;
            }
            else
            {
                ddlHoraFin2.SelectedIndex = ddlHoraFin2.Items.IndexOf(ddlHoraFin2.Items.FindByValue(ddlHoraFin1.SelectedItem.Value));
            }

            ActualizarRangoHorarioDisponible();

        }

        PanelUno.Visible = false;
        Paneldos.Visible = true;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>fc_mostrarFiltro()</script>", false);

    }
    protected void lnkRegresar_Click(object sender, EventArgs e)
    {
        if (txtFecha.Text.Trim().Equals(""))
            return;

        lblSeleccion1.Text = lblSeleccion2.Text;

        if (!txtFechaIni.Text.Trim().Equals(txtFecha.Text.Trim()))
        {
            hfFecha.Value = Convert.ToString(hfFechaIni.Value);

            txtFecha.Text = Convert.ToString(hfFecha.Value);

            ConsultarHorarioReserva();
        }

        pnlLeyenda.Visible = (!(gvHorarioReserva.Rows.Count == 0));

        //Horas
        //----------
        if (ddlHoraInicio2.Items.Count > 0)
        {
            if (ddlHoraInicio1.Items.Count > 0)
            {

                //Horas
                //----------
                DateTime dtHoraIni1 = Convert.ToDateTime(ddlHoraInicio1.SelectedValue);
                DateTime dtHoraFin1 = Convert.ToDateTime(ddlHoraFin1.SelectedValue);
                DateTime dtHoraIni2 = Convert.ToDateTime(ddlHoraInicio2.SelectedValue);
                DateTime dtHoraFin2 = Convert.ToDateTime(ddlHoraFin2.SelectedValue);

                DateTime dtHI1 = Convert.ToDateTime(ddlHoraInicio1.Items[0].Value);
                DateTime dtHF1 = Convert.ToDateTime(ddlHoraFin1.Items[ddlHoraFin1.Items.Count - 1].Value);

                //Horas
                //----------
                if (dtHI1 > dtHoraIni2)
                {
                    ddlHoraInicio1.SelectedIndex = 0;
                }
                else
                {
                    ddlHoraInicio1.SelectedIndex = ddlHoraInicio1.Items.IndexOf(ddlHoraInicio1.Items.FindByValue(ddlHoraInicio2.SelectedItem.Value));
                }
                if (dtHoraFin2 > dtHF1)
                {
                    ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.Count - 1;
                }
                else
                {
                    ddlHoraFin1.SelectedIndex = ddlHoraFin1.Items.IndexOf(ddlHoraFin1.Items.FindByValue(ddlHoraFin2.SelectedItem.Value));
                }

                ActualizarRangoHorario();

            }
        }

        PanelUno.Visible = true;
        Paneldos.Visible = false;
    }

    protected void ImgSalir_Click(object sender, ImageClickEventArgs e)
    {
        MpeReprogramacion.Hide();
    }

    protected void gvHorarioReserva_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 intFila = Convert.ToInt32(e.CommandArgument);
        Int32 intCol = 4 + Convert.ToInt32(e.CommandName.ToString().Substring(e.CommandName.ToString().IndexOf("_") + 1, (e.CommandName.LastIndexOf("_") - e.CommandName.IndexOf("_") - 1)));

        if (gvHorarioReserva.Rows[intFila].BackColor == System.Drawing.Color.FromArgb(178, 213, 247))
            return;

        string strIMG = string.Empty;
        string strHora = string.Empty;

        CitasBE ObjBe = new CitasBE();
        CitasBL ObjBl = new CitasBL();


        //----------------------------
        ImageButton imbCelda = null;
        DataControlFieldCell CTR = ((DataControlFieldCell)gvHorarioReserva.Rows[intFila].Controls[intCol]);
        foreach (Control _ctr in CTR.Controls)
        {
            if (_ctr.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
            {
                imbCelda = (ImageButton)_ctr;
                break;
            }
        }
        //----------------------------

        DataKey oDatos = gvHorarioReserva.DataKeys[intFila];

        strIMG = imbCelda.ImageUrl;

        strHora = e.CommandName.Substring(e.CommandName.LastIndexOf("_") + 1);

        if (strIMG == imgURL_HORA_VACIA)
        {
            //-->
        }
        else if (strIMG == imgURL_HORA_RESERVADA)
        {
            //-->

        }
        else if (strIMG == imgURL_HORA_LIBRE)
        {

            ImageButton imbTemp;

            for (Int32 f = 0; f <= gvHorarioDisponible.Rows.Count - 1; f++)
            {
                DataControlFieldCell CTR1 = (DataControlFieldCell)gvHorarioDisponible.Rows[f].Controls[6];
                foreach (Control _ctr in CTR1.Controls)
                {
                    if (_ctr.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                    {
                        imbTemp = (ImageButton)_ctr;
                        if (imbTemp.ImageUrl.ToUpper() == imgURL_HORA_SEPARADA.ToUpper())
                        {
                            imbTemp.ImageUrl = imgURL_HORA_LIBRE;
                            break;
                        }
                    }
                }
            }

            //----------------

            strHora = e.CommandName.Substring(e.CommandName.LastIndexOf("_") + 1);


            for (Int32 f = 0; f <= gvHorarioReserva.Rows.Count - 1; f++)
            {
                for (Int32 c = 4; c <= gvHorarioReserva.Columns.Count - 1; c++)
                {
                    ImageButton imgB1 = getControl(f, c);
                    if (imgB1.ImageUrl == imgURL_HORA_SEPARADA)
                    {
                        imgB1.ImageUrl = imgURL_HORA_LIBRE;
                        break;
                    }
                }
            }

            imbCelda.ImageUrl = imgURL_HORA_SEPARADA;

            hf_DATOS_CITA.Text = oDatos.Values["ID_ASESOR"] + "|" + oDatos.Values["NOM_ASESOR"] + "|" + gvHorarioReserva.Rows[intFila].Cells[0].Text + "|" + txtFecha.Text + "|" + strHora.Insert(2, ":") + "|" + oDatos.Values["TELEFONO"].ToString().Replace('|', '/') + "|" + oDatos.Values["EMAIL"].ToString();
            lblSeleccion1.Text = "Selección de Reserva -  " + (oParametros.GetValor(Parametros.PARM._10).Equals("0") ? "Asesor de Servicio " + oDatos.Values["ID_ASESOR"].ToString() : oDatos.Values["NOM_ASESOR"].ToString()) + " - " + GetFechaLarga(GetFecha(txtFecha.Text)) + " - " + FormatoHora(strHora.Insert(2, ":"));
            lblSeleccion2.Text = lblSeleccion1.Text;

        }
        else
        {
            lblSeleccion1.Text = "";
            imbCelda.ImageUrl = imgURL_HORA_LIBRE;
        }
    }
    protected void gvHorarioDisponible_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 fila = Convert.ToInt32(e.CommandArgument);
        Int32 Col = 6;

        if (gvHorarioDisponible.Rows[fila].BackColor == System.Drawing.Color.FromArgb(178, 213, 247))
        {
            return;
        }

        string strIMG = string.Empty;
        string strHora = string.Empty;

        ImageButton imbCelda = null;
        ImageButton imbTemp = null;


        DataControlFieldCell CTR = (DataControlFieldCell)gvHorarioDisponible.Rows[fila].Controls[Col];
        foreach (Control _ctr in CTR.Controls)
        {
            if (_ctr.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
            {
                imbCelda = (ImageButton)_ctr;
                strIMG = imbCelda.ImageUrl.ToUpper();
                break;
            }
        }


        if (strIMG == imgURL_HORA_VACIA.ToUpper())
        {
            //-->
        }
        else if (strIMG == imgURL_HORA_RESERVADA.ToUpper())
        {
            //-->
        }
        else if (strIMG == imgURL_HORA_LIBRE.ToUpper())
        {
            //'> Quitar la seleccion de horario de Reserva

            for (Int32 f = 0; f <= gvHorarioReserva.Rows.Count - 1; f++)
            {
                for (Int32 c = 4; c <= gvHorarioReserva.Columns.Count - 1; c++)
                {
                    ImageButton imgB1 = getControl(f, c);
                    if (imgB1.ImageUrl == imgURL_HORA_SEPARADA)
                    {
                        imgB1.ImageUrl = imgURL_HORA_LIBRE;
                        break;
                    }
                }
            }

            //-->
            strHora = gvHorarioDisponible.Rows[fila].Cells[3].Text;

            for (Int32 f = 0; f <= gvHorarioDisponible.Rows.Count - 1; f++)
            {
                DataControlFieldCell CTR1 = (DataControlFieldCell)gvHorarioDisponible.Rows[f].Controls[Col];
                foreach (Control _ctr in CTR1.Controls)
                {
                    if (_ctr.GetType().ToString() == "System.Web.UI.WebControls.DataControlImageButton")
                    {
                        imbTemp = (ImageButton)_ctr;
                        if (imbTemp.ImageUrl.ToUpper() == imgURL_HORA_SEPARADA.ToUpper())
                        {
                            imbTemp.ImageUrl = imgURL_HORA_LIBRE;
                            break;
                        }
                    }
                }
            }

            imbCelda.ImageUrl = imgURL_HORA_SEPARADA;

            DataKey oDatos = gvHorarioDisponible.DataKeys[fila];

            hf_DATOS_CITA.Text = oDatos.Values["ID_ASESOR"] + "|" + oDatos.Values["NOM_ASESOR"] + "|" + gvHorarioDisponible.Rows[fila].Cells[1].Text + "|" + gvHorarioDisponible.Rows[fila].Cells[2].Text + "|" + oDatos.Values["HORA_CITA"].ToString() + "|" + oDatos.Values["TELEFONO"].ToString().Replace('|', '/') + "|" + oDatos.Values["EMAIL"].ToString();
            lblSeleccion2.Text = "Selección de Reserva -  " + (oParametros.GetValor(Parametros.PARM._10).Equals("0") ? "Asesor de Servicio " + oDatos.Values["ID_ASESOR"].ToString() : oDatos.Values["NOM_ASESOR"].ToString()) + " - " + GetFechaLarga(GetFecha(gvHorarioDisponible.Rows[fila].Cells[2].Text)) + " - " + FormatoHora(strHora);
            lblSeleccion1.Text = lblSeleccion2.Text;
        }
        else
        {
            lblSeleccion2.Text = "";
            imbCelda.ImageUrl = imgURL_HORA_LIBRE;
            hf_DATOS_CITA.Text = "";
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>fc_filtrarGrilla()</script>", false);//@003

    }

    protected void btnReprogramarCita_1_2_Click(object sender, EventArgs e)
    {
        if (txtFecha.Text.Trim().Equals("")) return;
        if (ddlHoraInicio1.Items.Count == 0) return;

        if (String.IsNullOrEmpty(lblSeleccion1.Text.Trim()) || String.IsNullOrEmpty(lblSeleccion1.Text.Trim()))
        {
            SRC_MsgInformacion(oParametros.msgSelFec);
        }
        else
        {
            Reprogramar_Cita();
        }
    }

    protected void ddlHoraInicio2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtHoraIni = Convert.ToDateTime(ddlHoraInicio2.SelectedValue);
        DateTime dtHoraFin = Convert.ToDateTime(ddlHoraFin2.SelectedValue);

        if (dtHoraIni > dtHoraFin)
        {
            //SRC_MsgInformacion("La hora inicial debe ser menor que la hora final.");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La hora inicial debe ser menor que la hora final."), false);
            ddlHoraInicio2.SelectedIndex = ddlHoraInicio2.Items.IndexOf(ddlHoraInicio2.Items.FindByValue(hfHoraIni2.Value.ToString()));
        }
        else
        {
            hfHoraIni2.Value = ddlHoraInicio2.Text;
            ActualizarRangoHorarioDisponible();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>fc_mostrarFiltro();fc_filtrarGrilla()</script>", false);
        }
    }
    protected void ddlHoraFin2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime dtHoraIni = Convert.ToDateTime(ddlHoraInicio2.SelectedValue);
        DateTime dtHoraFin = Convert.ToDateTime(ddlHoraFin2.SelectedValue);

        if (dtHoraIni > dtHoraFin)
        {
            //SRC_MsgInformacion("La hora final debe ser mayor que la hora inicial.");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script>alert('{0}');fc_mostrarFiltro();fc_filtrarGrilla();</script>", "La hora final debe ser menor que la hora final."), false);
            ddlHoraFin2.SelectedIndex = ddlHoraFin2.Items.IndexOf(ddlHoraFin2.Items.FindByValue(hfHoraFin2.Value.ToString()));
        }
        else
        {
            hfHoraFin2.Value = ddlHoraFin2.Text;

            //CAMBIAR LA GRILLA
            ActualizarRangoHorarioDisponible();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>fc_mostrarFiltro();fc_filtrarGrilla()</script>", false);
        }
    }

    protected void ddlPuntoRed_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LimpiarFiltros(6);

            string strRutaMapa1 = string.Empty;

            if (ddlPuntoRed.SelectedIndex == 0)
            {
                ddlTaller.Items.Insert(0, _TODOS);
            }
            else
            {
                oCitasBE = new CitasBE();
                oCitasBEList = new CitasBEList();

                CitasBL oCitasBL = new CitasBL();

                oCitasBE.nid_Servicio = Convert.ToInt32(hf_ID_SERVICIO.Value.ToString());
                oCitasBE.nid_modelo = Convert.ToInt32(hf_ID_MODELO.Value.ToString());
                oCitasBE.nid_ubica = Convert.ToInt32(ddlPuntoRed.SelectedValue.ToString());
                oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;


                oCitasBEList = oCitasBL.Listar_Talleres(oCitasBE);

                if (oCitasBEList.Count == 0)
                {
                    ddlTaller.Text = "No hay Talleres";
                    ddlTaller.Enabled = false;
                    btnMapaTaller.Visible = false;
                }
                else if (oCitasBEList.Count == 1)
                {
                    lblTaller.Text = oCitasBEList[0].no_taller;
                    ddlTaller.Visible = false;
                    ddlTaller.Enabled = false;
                    lblTaller.Visible = true;
                    btnMapaTaller.Visible = true;

                    imbFecSgte.Enabled = true;
                    imbFecAnt.Enabled = true;
                    imbFecha.Enabled = true;
                    imbFecha1.Enabled = true;
                    imbFecha2.Enabled = true;

                    string strMapaTaller = oCitasBEList[0].tx_mapa_taller;
                    string strNombTaller = oCitasBEList[0].no_taller;

                    if (!strMapaTaller.Trim().Equals(""))
                    {
                        string strRutaMapa = ConfigurationManager.AppSettings["RutaMapasBO"] + strMapaTaller;
                        btnMapaTaller.Attributes.Add("onclick", "javascript:foto('" + strRutaMapa + "','" + strNombTaller + "');");
                    }
                    else
                    {
                        btnMapaTaller.Attributes.Add("onclick", "javascript:alert('" + oParametros.msgNoMapa + "');");
                    }

                    //------------------------------------------------------

                    hf_ID_TALLER.Value = oCitasBEList[0].nid_taller.ToString();
                    string strFechaHabil = SRC_FECHA_HABIL().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);

                    //------------------------------------------------------------------

                    oCitasBE.nid_taller = oCitasBEList[0].nid_taller;
                    oCitasBE.dd_atencion = getDiaSemana(GetFecha(strFechaHabil));

                    oCitasBEList = new CitasBEList();
                    oCitasBEList = oCitasBL.GETListarDatosTaller(oCitasBE);

                    //------------------------------------------------------------------
                    hf_FECHA_HABIL.Value = strFechaHabil;

                    txtFecha.Text = strFechaHabil;
                    hfFecha.Value = strFechaHabil;

                    hf_DATOS_TALLER.Value = oCitasBEList[0].no_distrito + "|" + oCitasBEList[0].di_ubica + "|" + oCitasBEList[0].nu_telefono + "|" + oCitasBEList[0].no_taller;
                    hf_INTERVALO_TALLER.Value = oCitasBEList[0].qt_intervalo_atenc;

                    ConsultarHorarioReserva(); //----> GRID 1
                    //MostrarHorarioDisponible() '---> GRID 2
                }
                else
                {
                    ddlTaller.AutoPostBack = false;

                    ddlTaller.DataSource = oCitasBEList;
                    ddlTaller.DataTextField = "NO_TALLER";
                    ddlTaller.DataValueField = "NID_TALLER";
                    ddlTaller.DataBind();

                    ddlTaller.Items.Insert(0, _TODOS);
                    ddlTaller.SelectedIndex = 1;
                    ddlTaller.Enabled = true;

                    btnMapaTaller.Visible = true;

                    ddlTaller.AutoPostBack = true;
                    ddlTaller_SelectedIndexChanged(this, null);
                }
            }
        }
        catch (Exception ex)
        {
            SRC_MsgInformacion(ex.Message);
        }
    }
    protected void ddlTaller_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LimpiarFiltros(7);

            if (ddlTaller.SelectedIndex == 0)
            {
                btnMapaTaller.Attributes.Add("onclick", "javascript:alert('Seleccione un Taller.');");
            }
            else
            {
                oCitasBE = new CitasBE();
                oCitasBEList = new CitasBEList();

                CitasBL oCitasBL = new CitasBL();

                oCitasBE.nid_Servicio = Convert.ToInt32(hf_ID_SERVICIO.Value.ToString());
                oCitasBE.nid_modelo = Convert.ToInt32(hf_ID_MODELO.Value.ToString());
                oCitasBE.nid_ubica = Convert.ToInt32(ddlPuntoRed.SelectedValue.ToString());
                oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

                oCitasBEList = oCitasBL.Listar_Talleres(oCitasBE);

                hf_ID_TALLER.Value = ddlTaller.SelectedValue.ToString();
                string strFechaHabil = SRC_FECHA_HABIL().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);

                //------------------------------------------------------------------

                oCitasBE.nid_taller = Convert.ToInt32(ddlTaller.SelectedValue.ToString());
                oCitasBE.dd_atencion = getDiaSemana(GetFecha(strFechaHabil));

                oCitasBEList = new CitasBEList();
                oCitasBEList = oCitasBL.GETListarDatosTaller(oCitasBE);

                string strMapaTaller = oCitasBEList[0].tx_mapa_taller;
                string strNombTaller = oCitasBEList[0].no_taller;


                if (!(strMapaTaller.Trim().Equals("")))
                {
                    string strRutaMapa = ConfigurationManager.AppSettings["RutaMapasBO"] + strMapaTaller;
                    btnMapaTaller.Attributes.Add("onclick", "javascript:foto('" + strRutaMapa + "','" + strNombTaller + "');");
                }
                else
                {
                    btnMapaTaller.Attributes.Add("onclick", "javascript:alert('" + oParametros.msgNoMapa + "');");
                }

                hf_ID_TALLER.Value = ddlTaller.SelectedValue.ToString();
                hf_INTERVALO_TALLER.Value = oCitasBEList[0].qt_intervalo_atenc;
                hf_FECHA_HABIL.Value = strFechaHabil;
                hf_DATOS_TALLER.Value = oCitasBEList[0].no_distrito.Trim() + '|' + oCitasBEList[0].di_ubica.Trim() + '|' + oCitasBEList[0].nu_telefono.Trim();

                txtFecha.Text = strFechaHabil;
                hfFecha.Value = strFechaHabil;

                imbFecSgte.Enabled = true;
                imbFecAnt.Enabled = true;
                imbFecha.Enabled = true;
                imbFecha2.Enabled = true;
                imbFecha1.Enabled = true;

                ConsultarHorarioReserva();  //---> GRID 1
                //MostrarHorarioDisponible(); //---> GRID 2
            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }

    private void LimpiarFiltros(Int32 cantCBO)
    {
        //if (cantCBO <= 5) { ddlPuntoRed.Items.Clear(); ddlPuntoRed.Enabled = false; }
        if (cantCBO <= 5) { ddlPuntoRed.Items.Clear(); ddlPuntoRed.Items.Insert(0, (new ListItem(_SELECCIONAR, "0"))); }
        if (cantCBO <= 6)
        {
            ddlTaller.Items.Clear();
            //ddlTaller.Enabled = false;
            ddlTaller.Visible = true;
            //btnMapaTaller.Attributes.Clear();
            btnMapaTaller.Visible = false;
            lblTaller.Visible = false;
        }
        if (cantCBO <= 7)
        {
            LimpiarReservaCita();
        }
    }
    private void LimpiarReservaCita()
    {
        ddlHoraInicio1.Items.Clear();
        ddlHoraFin1.Items.Clear();
        ddlHoraInicio2.Items.Clear();
        ddlHoraFin2.Items.Clear();

        ddlHoraInicio1.Enabled = false;
        ddlHoraFin1.Enabled = false;
        ddlHoraInicio2.Enabled = false;
        ddlHoraFin2.Enabled = false;


        gvHorarioReserva.DataSource = null;
        gvHorarioReserva.DataBind();
        gvHorarioReserva.Columns.Clear();
        gvHorarioDisponible.DataSource = null;
        gvHorarioDisponible.DataBind();

        imbFecSgte.Enabled = false;
        imbFecAnt.Enabled = false;
        imbFecha.Enabled = false;
        imbFecha1.Enabled = false;
        imbFecha2.Enabled = false;

        lblFlgHorario1.Text = ConfigurationManager.AppSettings["msgNoHorario1"].ToString();
        lblFlgHorario2.Text = ConfigurationManager.AppSettings["msgNoHorario1"].ToString();

        pnlHorarioReserva.Visible = false;
        pnlHorarioDisponible.Visible = false;
        pnlLeyenda.Visible = false;
        pnlLeyenda2.Visible = false;
        lblFlgHorario1.Visible = true;
        lblFlgHorario2.Visible = true;

        lblSeleccion1.Text = "";
        lblSeleccion2.Text = "";
        txtFecha.Text = "";
        txtFechaIni.Text = "";
        txtFechaFin.Text = "";
    }

    private string GetFechaMaxReserva()
    {
        CitasBL objL = new CitasBL();
        string strFecha = String.Format("{0:d}", Convert.ToDateTime(objL.GETFechaActual()).AddDays(Convert.ToInt32(oParametros.GetValor(Parametros.PARM._06))));
        objL = null;
        return strFecha;

    }
    private string GetFechaMinReserva()
    {
        CitasBL objL = new CitasBL();
        string strFecha = String.Format("{0:d}", Convert.ToDateTime(objL.GETFechaActual()).AddDays(Convert.ToInt32(oParametros.GetValor(Parametros.PARM._05))));
        objL = null;
        return strFecha;
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
                catch //(Exception ex)
                {

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
    }//DateDiff
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
    private string returnnameday(int numberday)
    {
        string nameday = string.Empty;
        switch (numberday)
        {
            case 1: nameday = "Lunes"; break;
            case 2: nameday = "Martes"; break;
            case 3: nameday = "Mi&eacute;rcoles"; break;
            case 4: nameday = "Jueves"; break;
            case 5: nameday = "Viernes"; break;
            case 6: nameday = "S&aacute;bado"; break;
            case 7: nameday = "Domingo"; break;
        }
        return nameday;
    }
    private int getDiaSemana(DateTime dtFecha)
    {
        Int32 intDia = 0;
        switch (dtFecha.DayOfWeek)
        {
            case DayOfWeek.Monday: intDia = 1; break;
            case DayOfWeek.Tuesday: intDia = 2; break;
            case DayOfWeek.Wednesday: intDia = 3; break;
            case DayOfWeek.Thursday: intDia = 4; break;
            case DayOfWeek.Friday: intDia = 5; break;
            case DayOfWeek.Saturday: intDia = 6; break;
            case DayOfWeek.Sunday: intDia = 7; break;
        }
        return intDia;
    }

    private void ConsultarHorarioReserva()
    {
        try
        {
            Int32 intTCA = 0; // TCA -> Total Citas x Asesor
            Int32 intTCE = 0; // TCE -> Total Citas en Cola espera
            Int32 intTCT = 0; // TCT -> Total Citas x Taller

            Int32 intSW = 0;//SW 
            Int32 intCantCE = 0;

            hf_HORAS_VACIAS.Value = "";

            DateTime dHoraIni_T;//  Horario Inicial del Taller (08:00)
            DateTime dHoraFin_T;//  Horario Final del   Taller (18:00)

            DateTime _dHoraIni_T;//HI Taller Temp
            DateTime _dHoraFin_T;//HF Taller Temp

            DateTime dHoraIni_E;// Horario Excepcional Inicial del Taller (12:00)
            DateTime dHoraFin_E;// Horario Excepcional Final   del Taller (15:00)

            DateTime dHoraIni_C;// Horario Inicial Cita
            DateTime dHoraFin_C;// Horario Final   Cita

            DateTime dHoraIni_A = DateTime.MinValue;
            DateTime dHoraFin_A = DateTime.MinValue;

            Int32 _ID_TALLER = Convert.ToInt32(hf_ID_TALLER.Value.ToString());
            Int32 _INTERVALO = Convert.ToInt32(hf_INTERVALO_TALLER.Value.ToString());
            Int32 _ID_SERVICIO = Convert.ToInt32(hf_ID_SERVICIO.Value.ToString());
            Int32 _ID_MODELO = Convert.ToInt32(hf_ID_MODELO.Value.ToString());
            string _TALLER = string.Empty;

            string strTotalHE;// Acumulacion de Rangos de HE

            CitasBEList _lstCitas;
            CitasBEList lstTalleres;
            CitasBEList lstAsesores;
            CitasBEList lstCitas;
            CitasBEList lstTalleresHE;

            Int32 hay1 = 0;
            Int32 hay2 = 0;

            DataTable dtReserva = new DataTable();//Datatable para rellenar el Gridview

            var blNoDisponible = false;
            //----------------

            ddlHoraInicio1.Items.Clear();
            ddlHoraFin1.Items.Clear();
            ddlHoraInicio1.Enabled = false;
            ddlHoraFin1.Enabled = false;
            ddlHoraInicio2.Enabled = false;
            ddlHoraFin2.Enabled = false;

            pnlHorarioReserva.Visible = false;
            pnlLeyenda.Visible = false;
            lblFlgHorario1.Visible = true;
            gvHorarioReserva.DataSource = null;
            gvHorarioReserva.DataBind();

            //------------------------------------------------------------------------>>>> OPCION 1
            oCitasBE = new CitasBE();

            oCitasBE.nid_Servicio = _ID_SERVICIO;
            oCitasBE.nid_modelo = _ID_MODELO;
            oCitasBE.coddpto = "0";
            oCitasBE.codprov = "0";
            oCitasBE.coddis = "0";
            oCitasBE.nid_ubica = Convert.ToInt32((ddlPuntoRed.SelectedIndex <= 0 ? "0" : ddlPuntoRed.SelectedValue.ToString()));
            oCitasBE.nid_taller = _ID_TALLER;
            oCitasBE.fe_atencion = GetFecha(txtFecha.Text);
            oCitasBE.dd_atencion = getDiaSemana(oCitasBE.fe_atencion);
            oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

            //-------------------------------------------------
            lstTalleres = new CitasBEList();
            lstTalleresHE = new CitasBEList();
            lstAsesores = new CitasBEList();
            _lstCitas = new CitasBEList();

            CitasBL oCitasBL = new CitasBL();

            lstTalleres = oCitasBL.ListarTalleresDisponibles_PorFecha(oCitasBE);//  1-Listado el Taller
            lstTalleresHE = oCitasBL.ListarHorarioExcepcional_Talleres(oCitasBE);// 2-Listado Horario Excepcionales
            lstAsesores = oCitasBL.ListarAsesoresDisponibles_PorFecha(oCitasBE);//  3-Listado Asesores - Taller
            _lstCitas = oCitasBL.ListarCitasAsesores(oCitasBE);//                   4-Listado CitasAsesores - Taller

            //-------------------------------------------------------------------------------------

            CitasBE oTaller = new CitasBE();

            if (lstTalleres.Count > 0)
            {
                oTaller = lstTalleres[0];
                dHoraIni_T = Convert.ToDateTime(oTaller.ho_inicio_t);
                dHoraFin_T = Convert.ToDateTime(oTaller.ho_fin_t);
                _TALLER = oTaller.no_taller;
            }
            else
            {
                lblFlgHorario1.Text = oParametros.msgNoHorario2 + " " + txtFecha.Text;
                return;
            }

            //-------------------------------------------------            

            if (lstAsesores.Count == 0)
            {
                //lblFlgPanel1.Text = "No hay Asesores disponibles para el " + txtFecha.Text;
                return;
            }

            //=================================================================================
            //> Horas Excepcional del Taller
            //=================================================================================

            strTotalHE = string.Empty;
            foreach (CitasBE oHET in lstTalleresHE)
            {
                if (!string.IsNullOrEmpty(oHET.ho_rango1)) strTotalHE += oHET.ho_rango1 + "-";
                if (!string.IsNullOrEmpty(oHET.ho_rango2)) strTotalHE += oHET.ho_rango2 + "-";
                if (!string.IsNullOrEmpty(oHET.ho_rango3)) strTotalHE += oHET.ho_rango3 + "-";
            }
            if (!string.IsNullOrEmpty(strTotalHE))
            {
                strTotalHE = strTotalHE.Substring(0, strTotalHE.Length - 1);
            }


            //**********************************************************************
            //  > Creando el GridView dinamicamente
            //----------------------------------------------------------------------

            gvHorarioReserva.DataSource = null;
            gvHorarioReserva.DataBind();
            gvHorarioReserva.Columns.Clear();

            int intWIDTH_GRID = 0;

            BoundField oColumna;
            ButtonField oColumnaB;

            //-----------------------------------
            //Columna: TALLER
            //-----------------------------------
            oColumna = new BoundField();
            oColumna.HeaderText = _HEADER_TALLER;
            oColumna.DataField = _DATA_TALLER;
            oColumna.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.HeaderStyle.Width = _WIDTH_COL_TALLER;
            oColumna.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.ItemStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.ItemStyle.Width = _WIDTH_COL_TALLER;
            gvHorarioReserva.Columns.Add(oColumna);

            //-----------------------------------
            //Columna: ASESOR
            //-----------------------------------
            oColumna = new BoundField();
            oColumna.HeaderText = _HEADER_ASESOR;
            oColumna.DataField = _DATA_ASESOR;
            oColumna.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.HeaderStyle.Width = _WIDTH_COL_ASESOR;
            oColumna.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.ItemStyle.Width = _WIDTH_COL_ASESOR;
            gvHorarioReserva.Columns.Add(oColumna);

            //-----------------------------------
            //Columna: TCA
            //-----------------------------------
            oColumna = new BoundField();
            oColumna.HeaderText = "TCA";
            oColumna.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.HeaderStyle.Width = _WIDTH_COL_TCA;
            oColumna.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.ItemStyle.Width = _WIDTH_COL_TCA;
            gvHorarioReserva.Columns.Add(oColumna);

            //-----------------------------------
            //Columna: TCE
            //-----------------------------------
            oColumna = new BoundField();
            oColumna.HeaderText = "TCE";
            oColumna.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.HeaderStyle.Width = _WIDTH_COL_TCE;
            oColumna.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            oColumna.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            oColumna.ItemStyle.Width = _WIDTH_COL_TCE;
            gvHorarioReserva.Columns.Add(oColumna);

            intWIDTH_GRID = _WIDTH_COL_TALLER + _WIDTH_COL_ASESOR + _WIDTH_COL_TCE + _WIDTH_COL_TCA;

            oColumna = null;

            //--------------------------------------------------------------------------------
            // Armnado el Datatable para el Cronograma de Citas
            //----------------------------------------------------
            dtReserva = new DataTable();
            dtReserva.Columns.Add("TALLER", System.Type.GetType("System.String"));
            dtReserva.Columns.Add("ASESOR_SERVICIO", System.Type.GetType("System.String"));
            //---
            dtReserva.Columns.Add("ID_ASESOR", System.Type.GetType("System.String"));
            dtReserva.Columns.Add("ID_TALLER", System.Type.GetType("System.String"));
            dtReserva.Columns.Add("NOM_ASESOR", System.Type.GetType("System.String"));
            dtReserva.Columns.Add("TELEFONO", System.Type.GetType("System.String"));
            dtReserva.Columns.Add("EMAIL", System.Type.GetType("System.String"));

            //------

            gvHorarioReserva.DataKeyNames = new string[] { "ID_ASESOR", "ID_TALLER", "NOM_ASESOR", "TELEFONO", "EMAIL" };


            _dHoraIni_T = dHoraIni_T;
            _dHoraFin_T = dHoraFin_T;

            Int32 intCant = 0;


            while ((_dHoraIni_T < _dHoraFin_T))
            {
                string _strHT = _dHoraIni_T.ToString("HH:mm").ToUpper().Replace(".", "").Replace(":", "");

                oColumnaB = new ButtonField();
                oColumnaB.ButtonType = ButtonType.Image;
                oColumnaB.HeaderText = _dHoraIni_T.ToString("hh:mm").ToUpper().Replace(".", "");
                oColumnaB.DataTextField = String.Concat("HORA_", _strHT);
                oColumnaB.CommandName = String.Concat("HORA_", intCant.ToString(), '_', _strHT);
                oColumnaB.ImageUrl = imgURL_HORA_VACIA;
                oColumnaB.HeaderStyle.Width = _WIDTH_COL_HORAS;
                oColumnaB.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                gvHorarioReserva.Columns.Add(oColumnaB);

                dtReserva.Columns.Add(string.Concat("HORA_", _strHT), System.Type.GetType("System.String"));

                intCant = intCant + 1;
                intWIDTH_GRID = intWIDTH_GRID + _WIDTH_COL_HORAS;

                _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
            }

            //***************************************************************************************

            string strPARM_10 = oParametros.GetValor(Parametros.PARM._10).ToString();
            DataRow oRow = null;


            foreach (CitasBE oAsesor in lstAsesores)
            {
                oRow = dtReserva.NewRow();
                oRow["TALLER"] = _TALLER.Trim();
                oRow["ASESOR_SERVICIO"] = (strPARM_10.Equals("0") ? "Asesor de Servicio - " + oAsesor.nid_asesor.ToString() : oAsesor.no_asesor);
                //--
                oRow["ID_ASESOR"] = oAsesor.nid_asesor.ToString().Trim();
                oRow["ID_TALLER"] = _ID_TALLER;
                oRow["NOM_ASESOR"] = oAsesor.no_asesor.ToString().Trim();
                oRow["TELEFONO"] = oAsesor.nu_telefono_a.ToString().Trim();
                oRow["EMAIL"] = oAsesor.no_correo_a.ToString().Trim();
                //--
                dtReserva.Rows.Add(oRow);
            }

            gvHorarioReserva.DataSource = dtReserva;
            gvHorarioReserva.DataBind();
            gvHorarioReserva.Width = intWIDTH_GRID;

            //----------------------------------------------------------
            bool blNoDisponibleT = false;

            //Validaciones Fecha Exceptuada - Taller
            if (oTaller.nid_dia_exceptuado_t == 1)
            {
                blNoDisponibleT = true;
            }
            //Validaciones Capacidad Atencion - Taller
            else if (oTaller.qt_cantidad_t <= 0)
            {
                blNoDisponibleT = true;
            }
            //Validacion Capacidad Atencion - Taller y Modelo
            else if (oParametros.SRC_Pais.Equals(1))
            {
                if (oTaller.qt_cantidad_m <= 0) blNoDisponibleT = true;
            }

            //------------------------------------------------------------------------
            // Colocamos los iconos de Horario Disponible, Reservado y Excepcional
            //------------------------------------------------------------------------

            Int32 intFila = 0;
            Int32 intCol = 0;

            foreach (CitasBE oAsesor in lstAsesores)
            {
                if (blNoDisponibleT)// Por Taller
                {
                    gvHorarioReserva.Rows[intFila].BackColor = System.Drawing.Color.FromArgb(178, 213, 247);
                }
                else// Por Asesor
                {
                    blNoDisponible = false;

                    //Validacion Fecha Exceptuada - Asesor
                    if (oAsesor.nid_dia_exceptuado_a == 1)
                    {
                        blNoDisponible = true; // Continue For
                        gvHorarioReserva.Rows[intFila].BackColor = System.Drawing.Color.FromArgb(178, 213, 247);
                    }
                    if (!blNoDisponible)
                    {
                        //Validacion Capacidad Atencion - Asesor
                        if (oAsesor.qt_cantidad_a <= 0)
                        {
                            blNoDisponible = true; // Continue For
                            gvHorarioReserva.Rows[intFila].BackColor = System.Drawing.Color.FromArgb(178, 213, 247);
                        }
                    }
                }
                //-------------------------
                // SET HORARIO DEL ASESOR 
                //------------------------------------------------------ 

                intCol = 0;

                foreach (string strHorario in oAsesor.horario_asesor.Split('|'))
                {
                    _dHoraIni_T = dHoraIni_T;
                    _dHoraFin_T = dHoraFin_T;

                    intCol = 0;

                    dHoraIni_A = Convert.ToDateTime(strHorario.Split('-').GetValue(0).ToString());
                    dHoraFin_A = Convert.ToDateTime(strHorario.Split('-').GetValue(1).ToString());

                    while (_dHoraIni_T <= _dHoraFin_T)
                    {
                        if (_dHoraIni_T >= dHoraIni_A && _dHoraIni_T < dHoraFin_A)
                        {
                            setIconoGrilla(intFila, _dHoraIni_T, imgURL_HORA_LIBRE, -1);
                        }

                        intCol += 1;
                        _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
                    }
                }

                //-------------------------
                // SET CITAS 
                //------------------------------------------------------                   
                // Filtrar Citas por Asesor
                //------------------------------------------------------   

                lstCitas = new CitasBEList();

                hay1 = 0;
                hay2 = 0;
                foreach (CitasBE oEntidad in _lstCitas)
                {
                    hay1 = 0;
                    if (oAsesor.nid_asesor.Equals(oEntidad.nid_asesor))
                    {
                        hay1 = 1; hay2 = 1;
                        lstCitas.Add(oEntidad);
                    }
                    if ((hay1 == 0) && (hay2 == 1))
                        break;
                }

                //-------------------------------------------------------------------
                intTCA = 0;
                intTCE = 0;

                intSW = 0;
                intCantCE = 0;

                foreach (CitasBE oCita in lstCitas)
                {
                    dHoraIni_C = DateTime.Parse(oCita.ho_inicio_c.ToString());
                    dHoraFin_C = DateTime.Parse(oCita.ho_fin_c.ToString());

                    intCantCE = (Int32)oCita.qt_cola_espera;// Cantidad - Cola Espera

                    intSW = 0;

                    while (dHoraIni_C < dHoraFin_C)
                    {
                        _dHoraIni_T = dHoraIni_T;
                        _dHoraFin_T = dHoraFin_T;

                        intCol = 0;

                        while (_dHoraIni_T <= _dHoraFin_T)
                        {

                            if (_dHoraIni_T >= dHoraIni_C && dHoraIni_C < _dHoraFin_T)
                            {
                                //>> Si hay cita reservada (ICONO DE RESERVADO)
                                setIconoGrilla(intFila, _dHoraIni_T, imgURL_HORA_RESERVADA, intCantCE);
                                intSW = 1;
                                break;
                            }

                            intCol += 1;
                            _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
                        }

                        dHoraIni_C = dHoraIni_C.AddMinutes(_INTERVALO);
                    }

                    if (intSW == 1)
                    {
                        //Contabilizamos el TCE y TCA
                        //------------------------------
                        intTCA++;
                        intTCE += intCantCE;
                    }
                }

                intTCT += intTCA;

                gvHorarioReserva.Rows[intFila].Cells[2].Text = intTCA.ToString(); // TCA
                gvHorarioReserva.Rows[intFila].Cells[3].Text = intTCE.ToString(); // TCE

                //--------------------------
                // SET HORARIO EXCEPCIONAL  
                //------------------------------------------------

                if (!string.IsNullOrEmpty(strTotalHE))
                {
                    foreach (string _strRangoHE in strTotalHE.Split('-'))
                    {
                        dHoraIni_E = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(0));// Hora Inicial HET
                        dHoraFin_E = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(1));// Hora Final   HET

                        _dHoraIni_T = dHoraIni_T;
                        _dHoraFin_T = dHoraFin_T;

                        intCol = 0;

                        while (_dHoraIni_T <= _dHoraFin_T)
                        {
                            // Si es una hora excepcionl
                            if (_dHoraIni_T >= dHoraIni_E && _dHoraIni_T < dHoraFin_E)
                            {
                                // Icon: Horario Excepcional
                                setIconoGrilla(intFila, _dHoraIni_T, imgURL_HORA_EXCEPCIONAL, -1);
                            }

                            intCol += 1;
                            _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
                        }
                    }
                }

                intFila += 1;
            }

            Lbltct.Text = intTCT.ToString();

            cargarComboHorarioTaller(ddlHoraInicio1, dHoraIni_T, dHoraFin_T, _INTERVALO, "HI");
            cargarComboHorarioTaller(ddlHoraFin1, dHoraIni_T, dHoraFin_T, _INTERVALO, "HF");

            hfHoraIni1.Value = ddlHoraInicio1.SelectedValue.ToString();
            hfHoraFin1.Value = ddlHoraFin1.SelectedValue.ToString();


            ddlHoraInicio1.Enabled = true;
            ddlHoraFin1.Enabled = true;
            pnlLeyenda.Visible = true;
            lblFlgHorario1.Visible = false;

            //---------------------------------------------> REMOVE: HORARIO BLANCO

            Int32 inrFilas = gvHorarioReserva.Rows.Count;
            Int32 intCount = 0;


            string strHorasV = string.Empty;

            for (Int32 intC = 0; intC <= gvHorarioReserva.Columns.Count - 1; intC++)
            {
                intCount = 0;
                for (Int32 intF = 0; intF <= gvHorarioReserva.Rows.Count - 1; intF++)
                {
                    DataControlFieldCell CTR_I = (DataControlFieldCell)gvHorarioReserva.Rows[intF].Controls[intC];
                    foreach (Control _ctrI in CTR_I.Controls)
                    {
                        if (_ctrI.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                        {
                            if (((ImageButton)_ctrI).ImageUrl == imgURL_HORA_VACIA)
                                intCount += 1;
                        }
                    }
                }

                if ((gvHorarioReserva.Columns[intC].GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField")))
                {
                    if (intCount != inrFilas)
                        break;
                    strHorasV += ((ButtonField)gvHorarioReserva.Columns[intC]).DataTextField + "|";
                }
            }

            for (Int32 intC = gvHorarioReserva.Columns.Count - 1; intC >= 0; intC += -1)
            {
                intCount = 0;
                for (Int32 intF = 0; intF <= gvHorarioReserva.Rows.Count - 1; intF++)
                {
                    DataControlFieldCell CTR_F = (DataControlFieldCell)gvHorarioReserva.Rows[intF].Controls[intC];
                    foreach (Control _ctrF in CTR_F.Controls)
                    {
                        if (_ctrF.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                        {
                            if (((ImageButton)_ctrF).ImageUrl == imgURL_HORA_VACIA)
                                intCount += 1;
                        }
                    }
                }

                if ((gvHorarioReserva.Columns[intC].GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField")))
                {
                    if (intCount != inrFilas)
                        break;
                    strHorasV += ((ButtonField)gvHorarioReserva.Columns[intC]).DataTextField + '|';
                }
            }

            hf_HORAS_VACIAS.Value = strHorasV;

            if (!string.IsNullOrEmpty(strHorasV))
            {
                foreach (DataControlField oColumn in gvHorarioReserva.Columns)
                {
                    if ((oColumn.GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField")))
                    {
                        if (strHorasV.Contains(((ButtonField)oColumn).DataTextField))
                        {
                            oColumn.Visible = false;
                        }
                    }
                }

                gvHorarioReserva.Width = intWIDTH_GRID - (_WIDTH_COL_HORAS * strHorasV.Split('|').Length);
            }

            pnlHorarioReserva.Visible = true;

            //----------------------------------------------------------------
            // Deshabilitando celdas en blanco y Celda coloreadas
            //----------------------------------------------------------------

            for (int intF = 0; intF <= gvHorarioReserva.Rows.Count - 1; intF++)
            {
                for (int intC = 0; intC <= gvHorarioReserva.Columns.Count - 1; intC++)
                {
                    if ((gvHorarioReserva.Columns[intC].GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField")))
                    {

                        if (gvHorarioReserva.Rows[intF].BackColor == System.Drawing.Color.FromArgb(178, 213, 247))
                        {
                            gvHorarioReserva.Rows[intF].Cells[intC].Enabled = false;
                        }
                        else
                        {
                            DataControlFieldCell CTR_I = (DataControlFieldCell)gvHorarioReserva.Rows[intF].Controls[intC];
                            foreach (Control _ctrI in CTR_I.Controls)
                            {
                                if (_ctrI.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
                                    if (((ImageButton)_ctrI).ImageUrl == imgURL_HORA_VACIA || ((ImageButton)_ctrI).ImageUrl == imgURL_HORA_EXCEPCIONAL)
                                        gvHorarioReserva.Rows[intF].Cells[intC].Enabled = false;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }

    private void MostrarHorarioDisponible()
    {
        if (!IsDate(txtFechaFin.Text))
        {
            return;
        }

        CitasBL oCitasBL = new CitasBL();

        Int32 _ID_TALLER = Convert.ToInt32(hf_ID_TALLER.Value.ToString());
        Int32 _INTERVALO = Convert.ToInt32(hf_INTERVALO_TALLER.Value.ToString());
        Int32 _ID_SERVICIO = Convert.ToInt32(hf_ID_SERVICIO.Value.ToString());
        Int32 _ID_MODELO = Convert.ToInt32(hf_ID_MODELO.Value.ToString());

        string _TALLER = string.Empty;

        DateTime dFechaIni = Convert.ToDateTime(GetFechaMinReserva());
        DateTime dFechaFin = Convert.ToDateTime(GetFechaMaxReserva());

        DateTime dHoraIni_T = DateTime.MinValue;
        DateTime dHoraFin_T = DateTime.MinValue;

        DateTime _dHoraIni_T = DateTime.MinValue;
        DateTime _dHoraFin_T = DateTime.MinValue;

        DateTime dHoraIni_A = DateTime.MinValue;
        DateTime dHoraFin_A = DateTime.MinValue;

        //CitasBEList _lstTalleres;
        CitasBEList _lstAsesores;
        CitasBEList _lstCitas;
        CitasBEList _lstTalleresHE;

        CitasBEList lstTalleres;
        CitasBEList lstAsesores;
        CitasBEList lstCitas;
        CitasBEList lstTalleresHE;

        string strTotalHE;// Acumulacion de Rangos de HE

        string strAsesorCapac = string.Empty;

        DateTime dtHEITaller;
        DateTime dtHEFTaller;

        DateTime dtHoraI_ddl = DateTime.MaxValue;
        DateTime dtHoraF_ddl = DateTime.MinValue;

        Int32 hay1 = 0;
        Int32 hay2 = 0;

        ddlHoraInicio2.Items.Clear();
        ddlHoraFin2.Items.Clear();
        ddlHoraInicio2.Enabled = false;
        ddlHoraFin2.Enabled = false;

        bool swHorasT = false;
        DataRow oRow = null;

        try
        {
            oCitasBE = new CitasBE();

            Int32 intPRM_10 = Convert.ToInt32(oParametros.GetValor(Parametros.PARM._10));

            DataTable dtHorarioDisp = new DataTable();
            //---------------------------------
            dtHorarioDisp.Columns.Add("PUNTO_RED", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("TALLER", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("FECHA", System.Type.GetType("System.DateTime"));
            dtHorarioDisp.Columns.Add("HORA", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("ASESOR_SERVICIO", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("QUICK_SERVICE", System.Type.GetType("System.String"));
            //------
            dtHorarioDisp.Columns.Add("ID_ASESOR", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("ID_TALLER", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("NOM_ASESOR", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("HORA_CITA", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("TELEFONO", System.Type.GetType("System.String"));
            dtHorarioDisp.Columns.Add("EMAIL", System.Type.GetType("System.String"));

            //------

            gvHorarioDisponible.DataKeyNames = new String[] { "ID_ASESOR", "ID_TALLER", "HORA_CITA", "NOM_ASESOR", "TELEFONO", "EMAIL" };

        //Recorremos Fecha por Fecha 
        //------------------------------------------------------

        Continue_While_1:
            while (dFechaIni <= dFechaFin)
            {
                //================================

                oCitasBE.nid_Servicio = _ID_SERVICIO;
                oCitasBE.nid_modelo = _ID_MODELO;
                oCitasBE.coddpto = "0";
                oCitasBE.codprov = "0";
                oCitasBE.coddis = "0";
                oCitasBE.nid_ubica = Convert.ToInt32((ddlPuntoRed.SelectedIndex <= 0 ? "0" : ddlPuntoRed.SelectedValue.ToString()));
                oCitasBE.nid_taller = _ID_TALLER;
                oCitasBE.fe_atencion = dFechaIni;
                oCitasBE.dd_atencion = getDiaSemana(dFechaIni);
                oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

                //---------------------------------------------------------------------------------------------------

                lstTalleres = oCitasBL.ListarTalleresDisponibles_PorFecha(oCitasBE);//   1-Listado todos Talleres
                ViewState["lstTalleres"] = lstTalleres;

                if (lstTalleres.Count == 0)
                {
                    dFechaIni = dFechaIni.AddDays(+1);
                    goto Continue_While_1;
                }

                _lstTalleresHE = oCitasBL.ListarHorarioExcepcional_Talleres(oCitasBE);// 2-Listado Horario Excepcionales
                ViewState["lstTalleresHE"] = _lstTalleresHE;

                _lstAsesores = oCitasBL.ListarAsesoresDisponibles_PorFecha(oCitasBE);//  3-Listado Asesores Talleres
                ViewState["lstAsesores"] = _lstAsesores;

                _lstCitas = oCitasBL.ListarCitasAsesores(oCitasBE);//                    4-Listado CitasAsesores Talleres
                ViewState["lstCitas"] = _lstCitas;

                //===========================================

                foreach (CitasBE oTaller in lstTalleres)
                {
                    _ID_TALLER = Convert.ToInt32(oTaller.nid_taller);
                    _INTERVALO = Convert.ToInt32(oTaller.qt_intervalo_atenc);

                    _dHoraIni_T = Convert.ToDateTime(oTaller.ho_inicio_t);
                    _dHoraFin_T = Convert.ToDateTime(oTaller.ho_fin_t);

                    //=================================================================================
                    //> Validaciones
                    //=================================================================================
                    if (oTaller.qt_cantidad_t <= 0) continue;//Capacidad Atencion Taller
                    if (oParametros.SRC_Pais.Equals(1))//07092012 -> SOLO PERU
                    {
                        if (oTaller.qt_cantidad_m <= 0) continue;//Capacidad Atencion Taller y Modelo
                    }
                    if (oTaller.nid_dia_exceptuado_t == 1) continue;//Dia Exceptuado


                    //=================================================================================
                    //> Horas Excepcional del Taller
                    //=================================================================================
                    // FILTRAR HORARIO EXCEPCIONAL
                    //-----------------------------

                    lstTalleresHE = new CitasBEList();

                    hay1 = 0;
                    hay2 = 0;
                    foreach (CitasBE oEntidad in _lstTalleresHE)
                    {
                        hay1 = 0;
                        if (oTaller.nid_taller.Equals(oEntidad.nid_taller))
                        {
                            hay1 = 1; hay2 = 1;
                            lstTalleresHE.Add(oEntidad);
                        }
                        if ((hay1 == 0) && (hay2 == 1))
                            break;
                    }

                    //-----------------------------------------------------------------------
                    strTotalHE = string.Empty;
                    foreach (CitasBE oHET in lstTalleresHE)
                    {
                        if (!string.IsNullOrEmpty(oHET.ho_rango1)) strTotalHE += oHET.ho_rango1 + "-";
                        if (!string.IsNullOrEmpty(oHET.ho_rango2)) strTotalHE += oHET.ho_rango2 + "-";
                        if (!string.IsNullOrEmpty(oHET.ho_rango3)) strTotalHE += oHET.ho_rango3 + "-";
                    }
                    if (!string.IsNullOrEmpty(strTotalHE))
                    {
                        strTotalHE = strTotalHE.Substring(0, strTotalHE.Length - 1);
                    }

                    //=================================================================================
                    //> Listado de Asesores 
                    //=================================================================================
                    // FILTRAR ASESORES - TALLER
                    //-----------------------------------

                    lstAsesores = new CitasBEList();// 2

                    hay1 = 0;
                    hay2 = 0;
                    foreach (CitasBE oEntidad in _lstAsesores)
                    {
                        hay1 = 0;
                        if (oTaller.nid_taller.Equals(oEntidad.nid_taller))
                        {
                            hay1 = 1; hay2 = 1;
                            //lstAsesores.Add(oEntidad);

                            //Validar FechaExceptuada
                            if (oEntidad.nid_dia_exceptuado_a == 0)
                            {
                                //Validar Capacidad de Atencion
                                if (oEntidad.qt_cantidad_a > 0) lstAsesores.Add(oEntidad);
                            }
                        }
                        if ((hay1 == 0) && (hay2 == 1))
                            break;
                    }

                    //--------------------------------------------------------------

                    DateTime odHIA = _dHoraIni_T;
                    DateTime odHFA = _dHoraFin_T;

                    foreach (CitasBE oAsesor in lstAsesores)//==========================>>>>
                    {
                        //=================================================================================
                        //> Listar Citas Asesores 
                        //=================================================================================
                        // FILTRAR CITAS - TALLER - ASESOR
                        //-----------------------------------

                        lstCitas = new CitasBEList();

                        hay1 = 0;
                        hay2 = 0;
                        foreach (CitasBE oEntidad in _lstCitas)
                        {
                            hay1 = 0;
                            if (oTaller.nid_taller.Equals(oEntidad.nid_taller) && oAsesor.nid_asesor.Equals(oEntidad.nid_asesor))
                            {
                                hay1 = 1; hay2 = 1;
                                lstCitas.Add(oEntidad);
                            }
                            if ((hay1 == 0) && (hay2 == 1))
                                break;
                        }

                        //-------------------------------------------------------------
                        //Recorrer cada horario del Asesor
                        foreach (string strHorario in oAsesor.horario_asesor.Split('|'))
                        {
                            dHoraIni_A = Convert.ToDateTime(strHorario.Split('-').GetValue(0).ToString());
                            dHoraFin_A = Convert.ToDateTime(strHorario.Split('-').GetValue(1).ToString());

                            if (dHoraIni_A < odHIA) dHoraIni_A = odHIA;
                            if (dHoraFin_A > odHFA) dHoraFin_A = odHFA;

                            //----------------------------------------------------
                            //> Get menor HI y mayor HF de Horarios disponibles
                            //---------------------------------------------------- 
                            if (dHoraFin_A > dtHoraF_ddl) dtHoraF_ddl = dHoraFin_A;
                            if (dHoraIni_A < dtHoraI_ddl) dtHoraI_ddl = dHoraIni_A;

                            swHorasT = true;

                        Continue_While_2:
                            while (dHoraIni_A < dHoraFin_A)
                            {
                                string strHoraAct = dHoraIni_A.ToString("HH:mm");

                                foreach (CitasBE oCita in lstCitas)
                                {
                                    DateTime dtHActual = dHoraIni_A;

                                    DateTime dtHICita = Convert.ToDateTime(oCita.ho_inicio_c);
                                    DateTime dtHFCita = Convert.ToDateTime(oCita.ho_fin_c);

                                    if (dtHActual >= dtHICita & dtHActual < dtHFCita)
                                    {
                                        dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                                        goto Continue_While_2; //> Es una Cita ya reservada
                                    }
                                }

                                // > Se verifica que la hora del Asesor no sea una hora excepcional

                                if (!String.IsNullOrEmpty(strTotalHE))
                                {
                                    foreach (string _strRangoHE in strTotalHE.Split('-'))
                                    {
                                        dtHEITaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(0));// Hora Inicial HET
                                        dtHEFTaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(1));// Hora Final   HET

                                        //> Si es una hora excepcionl
                                        if (dHoraIni_A >= dtHEITaller & dHoraIni_A < dtHEFTaller)
                                        {
                                            dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                                            goto Continue_While_2; //> Es Horario Excepcional
                                        }
                                    }
                                }

                                //> FECHA ENCONTADA
                                //-------------------------

                                oRow = dtHorarioDisp.NewRow();

                                oRow["PUNTO_RED"] = oTaller.no_ubica;
                                oRow["TALLER"] = oTaller.no_taller;
                                oRow["FECHA"] = dFechaIni.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                                oRow["HORA"] = strHoraAct;
                                oRow["ASESOR_SERVICIO"] = (intPRM_10.Equals(0) ? "Asesor de Servicio - " + oAsesor.nid_asesor.ToString() : oAsesor.no_asesor.ToString());
                                oRow["QUICK_SERVICE"] = (hf_QUICK_SERVICE.Value).Equals("1") ? "SI" : "NO";
                                //---
                                oRow["ID_ASESOR"] = oAsesor.nid_asesor.ToString();
                                oRow["ID_TALLER"] = _ID_TALLER.ToString();
                                oRow["NOM_ASESOR"] = oAsesor.no_asesor;
                                oRow["HORA_CITA"] = strHoraAct;
                                oRow["TELEFONO"] = oAsesor.nu_telefono_a.Trim();
                                oRow["EMAIL"] = oAsesor.no_correo_a.Trim();
                                //

                                dtHorarioDisp.Rows.Add(oRow);

                                dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                            }
                        }
                    }
                }

                dFechaIni = dFechaIni.AddDays(+1);
            }


            //-------------------
            //Ordenar  GridView
            //-------------------

            DataTable dt = dtHorarioDisp.Clone();
            DataRow[] drs = dtHorarioDisp.Select("", "FECHA Asc,HORA Asc,ASESOR_SERVICIO Asc");
            foreach (DataRow dr in drs)
            {
                dt.ImportRow(dr);
            }

            dtHorarioDisp = dt;


            //> Si hay registrio se envia al Grdview
            if (dtHorarioDisp.Rows.Count > 0)
            {
                gvHorarioDisponible.DataSource = dtHorarioDisp;
                gvHorarioDisponible.DataBind();

                //------------------------------------------------------
                //ADD_2
                //-------
                // capacidad max de asesores
                //---------------------
                if (!string.IsNullOrEmpty(strAsesorCapac.Trim()))
                {
                    strAsesorCapac = strAsesorCapac.Trim().Substring(0, strAsesorCapac.Trim().Length - 1);
                    foreach (string strIndex in strAsesorCapac.Split('|'))
                    {
                        string strIdAsesor = strIndex.Replace("==", "");

                        foreach (GridViewRow _oRow in gvHorarioDisponible.Rows)
                        {
                            DataKey oDatos = gvHorarioDisponible.DataKeys[_oRow.RowIndex];

                            if (oDatos.Values["ID_ASESOR"].ToString().Equals(strIdAsesor))
                            {
                                _oRow.BackColor = System.Drawing.Color.FromArgb(178, 213, 247);
                            }
                        }
                    }
                }
                //------------------------

                lblFlgHorario2.Visible = false;
                pnlLeyenda.Visible = true;
                pnlHorarioDisponible.Visible = true;

                ddlHoraInicio2.Enabled = true;
                ddlHoraFin2.Enabled = true;
            }
            else
            {
                gvHorarioDisponible.DataSource = null;
                gvHorarioDisponible.DataBind();

                lblFlgHorario2.Visible = true;
                lblFlgHorario2.Text = "No hay Horario disponible para el rango de fecha seleccionado.";
                pnlLeyenda.Visible = false;
                pnlHorarioDisponible.Visible = false;

                ddlHoraInicio2.Enabled = false;
                ddlHoraFin2.Enabled = false;
            }

            dtHorarioDisp = null;

            if (swHorasT)
            {
                cargarComboHorarioTaller(ddlHoraInicio2, dtHoraI_ddl, dtHoraF_ddl, _INTERVALO, "HI");
                cargarComboHorarioTaller(ddlHoraFin2, dtHoraI_ddl, dtHoraF_ddl, _INTERVALO, "HF");

                hfHoraIni2.Value = ddlHoraInicio2.SelectedValue.ToString();
                hfHoraFin2.Value = ddlHoraFin2.SelectedValue.ToString();

                ddlHoraInicio2.Enabled = (gvHorarioDisponible.Rows.Count > 0) ? true : false;
                ddlHoraFin2.Enabled = (gvHorarioDisponible.Rows.Count > 0) ? true : false;

                imbFecha2.Enabled = true;
                imbFecha1.Enabled = true;
            }
            else
            {
                ddlHoraInicio2.Enabled = false;
                ddlHoraFin2.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }

    private void ActualizarRangoHorario()
    {
        DateTime dtHIRango = Convert.ToDateTime(ddlHoraInicio1.SelectedValue.ToString());
        DateTime dtHFRango = Convert.ToDateTime(ddlHoraFin1.SelectedValue.ToString());

        Int32 intCol = 0;
        Int32 intWIDTH = 0;
        string strHoraV = hf_HORAS_VACIAS.Value.ToString();

        foreach (DataControlField oColumn in gvHorarioReserva.Columns)
        {
            string strTipo1 = oColumn.GetType().ToString();

            if (oColumn.GetType().ToString().Equals("System.Web.UI.WebControls.ButtonField"))
            {
                DateTime dtHoraC = Convert.ToDateTime(((ButtonField)oColumn).DataTextField.Substring(((ButtonField)oColumn).DataTextField.IndexOf('_') + 1).Insert(2, ":"));
                gvHorarioReserva.Columns[intCol].Visible = !(dtHoraC < dtHIRango || dtHoraC >= dtHFRango);
                gvHorarioReserva.Columns[intCol].Visible = (strHoraV.Contains(((ButtonField)oColumn).DataTextField)) ? false : gvHorarioReserva.Columns[intCol].Visible;
                intWIDTH += (dtHoraC < dtHIRango || dtHoraC >= dtHFRango) ? 0 : ((strHoraV.Contains(((ButtonField)oColumn).DataTextField)) ? 0 : _WIDTH_COL_HORAS);
            }

            intCol += 1;
        }

        gvHorarioReserva.Width = _WIDTH_COL_TALLER + _WIDTH_COL_ASESOR + _WIDTH_COL_TCA + _WIDTH_COL_TCE + intWIDTH;
    }
    private void ActualizarRangoHorarioDisponible()
    {
        DateTime dtHoraI2R = Convert.ToDateTime(ddlHoraInicio2.SelectedValue.ToString());
        DateTime dtHoraF2R = Convert.ToDateTime(ddlHoraFin2.SelectedValue.ToString());


        for (Int32 i = 0; i <= gvHorarioDisponible.Rows.Count - 1; i++)
        {
            DateTime dtHora = Convert.ToDateTime(gvHorarioDisponible.Rows[i].Cells[3].Text.ToString());
            //if (dtHoraI2R <= dtHora & dtHora <= dtHoraF2R)//@004
            if (dtHoraI2R <= dtHora & dtHora < dtHoraF2R)//@004
            {
                gvHorarioDisponible.Rows[i].Visible = true;
            }
            else
            {
                gvHorarioDisponible.Rows[i].Visible = false;
            }
        }
    }
    private ImageButton getControl(Int32 fila, Int32 col)
    {

        ImageButton imbCelda = null;
        DataControlFieldCell CTR = ((DataControlFieldCell)gvHorarioReserva.Rows[fila].Controls[col]);
        foreach (Control _ctr in CTR.Controls)
        {
            if (_ctr.GetType().ToString().Equals("System.Web.UI.WebControls.DataControlImageButton"))
            {
                imbCelda = (ImageButton)_ctr;
            }
        }
        return imbCelda;
    }

    private void Reprogramar_Cita()
    {
        //VALIDACION

        //1- Validacion de Datos internamente.
        //2- Validacion de Reprogramacion de Cita.

        if (PanelUno.Visible)
        {
            //GRILLA 1
            if (lblFlgHorario1.Visible)
            {
                return;
            }
            else if (!escogioReservacion())
            {
                SRC_MsgInformacion("Seleccione una fecha para Reprogramar su Cita.");
                return;
            }
        }
        else
        {
            //GRILLA 2
            if (lblFlgHorario2.Visible)
            {
                return;
            }
            else if (!escogioReservacionHD())
            {
                return;
            }
        }

        //------------------- VERIFICAr CLIENTES EN COLA DE ESPERA ---------------------------

        CitasBE objBECita = new CitasBE();
        CitasBL objBLCita = new CitasBL();

        Int32 int_ID_CITA = Convert.ToInt32(hf_ID_CITA.Value.ToString());

        objBECita.nid_cita = int_ID_CITA;
        gv_Cola_Espera.DataSource = objBLCita.GETListarClientesEnColaEspera(objBECita);
        gv_Cola_Espera.DataBind();

        //--> REPROGRAMAMOS LA CITA

        string strDatosCita = hf_DATOS_CITA.Text;

        objBECita.nid_cita = int_ID_CITA;
        objBECita.fe_prog = GetFecha(strDatosCita.Split('|').GetValue(3).ToString());
        objBECita.ho_inicio = strDatosCita.Split('|').GetValue(4).ToString();
        objBECita.ho_fin = Convert.ToDateTime(strDatosCita.Split('|').GetValue(4).ToString()).AddMinutes(Convert.ToInt32(hf_INTERVALO_TALLER.Value)).ToString("HH:mm");
        objBECita.nid_taller = (ConfigurationManager.AppSettings["CambiarTaller"].ToString().Equals("1")) ? Convert.ToInt32(hf_ID_TALLER.Value.ToString()) : 0;
        objBECita.nid_Estado = Convert.ToInt32(((AdminCitaBE)(Session["oAdminCitaBE"])).grid_nid_estado);
        objBECita.tx_observacion = "";
        objBECita.Nid_usuario = Convert.ToInt32(strDatosCita.Split('|').GetValue(0).ToString());
        objBECita.dd_atencion = getDiaSemana(objBECita.fe_prog);
        //-------------------------------
        objBECita.co_usuario_crea = ((string.IsNullOrEmpty(Profile.UserName)) ? "" : Profile.UserName);
        objBECita.co_usuario_red = ((string.IsNullOrEmpty(Profile.UsuarioRed)) ? "" : Profile.UsuarioRed);
        objBECita.no_estacion_red = ((string.IsNullOrEmpty(Profile.Estacion)) ? "" : Profile.Estacion);

        Int32 resCita = 0;

        try
        {
            CitasBE oCitasBEPrm = new CitasBE();
            oCitasBEPrm.nid_cita = int_ID_CITA;
            CitasBEList oDatos = new CitasBL().GETListarDatosCita(oCitasBEPrm);
            //bool saveQRPNG = true;
            if (oDatos[0].no_nombreqr == null)
                objBECita.no_nombreqr = new CitasBL().GetNombreImagenQR(oDatos[0].nu_placa);
            else if (oDatos[0].no_nombreqr == "")
                objBECita.no_nombreqr = new CitasBL().GetNombreImagenQR(oDatos[0].nu_placa);
            else
            {
                objBECita.no_nombreqr = oDatos[0].no_nombreqr;
                //saveQRPNG = false;
            }
            //string resultado = new CitasBL().SaveImageQRText(oParametros.SRC_GuardaQR, objBECita.no_nombreqr, oDatos[0].nu_placa, objBECita.fe_prog, objBECita.ho_inicio, oDatos[0].no_marca, oDatos[0].no_modelo, oDatos[0].no_color_exterior, saveQRPNG);
            ViewState["img_qr"] = objBECita.no_nombreqr;

            resCita = objBLCita.Reprogramar(objBECita);

            if (resCita == 11)
            {
                MpeReprogramacion.Hide();
                mpMensajes.Show();
                return;
            }
            else if (resCita == 22)
            {
                MensajeScript("Ya se ha reservado una Cita en este mismo horario.");
                return;
            }
            else if (resCita == 33)
            {
                MensajeScript("Este vehículo ya tiene cita separada para esta fecha y hora programada.");
                return;
            }
            else if (resCita == 44)
            {
                MensajeScript("Ya se ha alcanzado el limite de atenciones por día del Taller.");
                return;
            }
            else if (resCita == 55)
            {
                MensajeScript("Ya se ha alcanzado el limite de atenciones por día del Asesor.");
                return;
            }
            else if (resCita == 66)
            {
                MensajeScript("Ya se ha alcanzado el limite de atenciones por día del Taller y Modelo");
                return;
            }
            else if (resCita == 1)
            {
                //mpPreg1.Hide();
                //mpMensajes.Show();

                MpeReprogramacion.Hide();
                EstadoBotones(false, false, true);
                lbl_mensajebox.Text = _MSG_RESPUESTA.Replace("{estado}", "Reprogramada");
                hf_ESTADO_CITA.Value = "Reprogramar";
                popup_msgbox_confirm.Show();

                objBECita.nid_cita = int_ID_CITA;

                oCitasBEList = new CitasBEList();
                oCitasBEList = objBLCita.GETListarDatosCita(objBECita);

                if (oCitasBEList.Count == 0) return;

                //>>------ REPROGRAMACION ----- >>

                //if (chk_det_recoremail.Checked) EnviarCorreo_Cliente(oCitasBEList[0], Parametros.EstadoCita.REPROGRAMADA);

                //EnviarCorreo_Asesor(oCitasBEList[0], Parametros.EstadoCita.REPROGRAMADA);

            }
            else if (resCita == 0)
            {
                //REPROGRAMADA POR OTRO USUARIO
            }
            else if (resCita > 10)
            {
            }
            else
            {              //< 0 -> ERROR DE BD
            }
        }
        catch //(Exception ex)
        { }
        finally
        {
            objBECita = null;
            objBLCita = null;
        }

        //MpeReprogramacion.Hide();
        //mpMensajes.Show();

        MpeReprogramacion.Hide();
        EstadoBotones(false, false, true);
        lbl_mensajebox.Text = _MSG_RESPUESTA.Replace("{estado}", "Reprogramada");
        popup_msgbox_confirm.Show();


        PanelUno.Visible = false;
        Paneldos.Visible = false;
    }

    private bool escogioReservacion()
    {

        for (Int32 f = 0; f <= gvHorarioReserva.Rows.Count - 1; f++)
        {
            for (Int32 c = 4; c <= gvHorarioReserva.Columns.Count - 1; c++)
            {
                ImageButton imgB1 = getControl(f, c);
                if (imgB1.ImageUrl == imgURL_HORA_SEPARADA)
                {
                    return true;
                    //break;
                }
            }
        }

        return false;

    }
    private bool escogioReservacionHD()
    {


        for (Int32 f = 0; f <= gvHorarioDisponible.Rows.Count - 1; f++)
        {
            DataControlFieldCell CTR1 = (DataControlFieldCell)(gvHorarioDisponible.Rows[f].Controls[6]);
            foreach (Control _ctr in CTR1.Controls)
            {
                if (_ctr.GetType().ToString() == "System.Web.UI.WebControls.DataControlImageButton")
                {
                    ImageButton imbCelda = (ImageButton)_ctr;
                    if (imbCelda.ImageUrl == imgURL_HORA_SEPARADA)
                    {
                        return true;
                        //break;
                    }
                }
            }

        }
        return false;
    }

    protected void btnRegresarPanelreprog_Click(object sender, ImageClickEventArgs e)
    {
        MpeReprogramacion.Hide();
    }

    protected void btnMapaTaller_Click(object sender, ImageClickEventArgs e)
    {

    }

    #endregion

    //***************************************************************************************************************/

    protected void btn_det_retornar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SRC_AdmCitas.aspx");
    }
    protected void btn_det_guardar_Click(object sender, ImageClickEventArgs e)
    {
        if (tbl_cliente.Enabled)
        {
            objEntAdminCitas = (AdminCitaBE)(Session["oAdminCitaBE"]);

            objEntCliente.no_cliente = txt_det_clinombre.Text.Trim();
            objEntCliente.no_ape_pat = txt_det_cliapellidospat.Text;
            objEntCliente.no_ape_mat = txt_det_cliapellidosmat.Text;
            objEntCliente.nu_documento = txt_det_clidnirut.Text.Trim();
            objEntCliente.nu_telefono = txt_det_clitelffijo.Text.Trim();
            objEntCliente.nu_tel_oficina = txt_det_clitelfoficina.Text.Trim();
            objEntCliente.nu_celular = (!txt_det_clitelfmovil.Text.Trim().Equals("")) ? ((oParametros.SRC_Pais.Equals(2) ? "569-" : "") + txt_det_clitelfmovil.Text.Trim()) : "";
            objEntCliente.nu_celular_alter = (!txt_det_clitelfmovil2.Text.Trim().Equals("")) ? ((oParametros.SRC_Pais.Equals(2) ? "569-" : "") + txt_det_clitelfmovil2.Text.Trim()) : "";
            objEntCliente.no_correo = txt_det_cliemail.Text.Trim();
            objEntCliente.no_correo_trabajo = txt_det_cliemail_trab.Text.Trim();
            objEntCliente.no_correo_alter = txt_det_cliemail_alter.Text.Trim();
            objEntCliente.co_usuario_crea = (Profile.UserName == null ? "" : Profile.UserName.ToString());
            objEntCliente.co_usuario_red = (Profile.UsuarioRed == null ? "" : Profile.UsuarioRed.ToString());
            objEntCliente.no_estacion_red = (Profile.Estacion == null ? "" : Profile.Estacion.ToString());

            Int32 nid_pais_telefono; Int32.TryParse(hfPaisTelefonoFijo.Value, out nid_pais_telefono);
            Int32 nid_pais_celular; Int32.TryParse(hfPaisTelefonoCelular.Value, out nid_pais_celular);

            objEntCliente.nu_anexo_telefono = txtTelefonoFijo_Anexo.Text;
            objEntCliente.nid_pais_celular = nid_pais_celular;
            objEntCliente.nid_pais_telefono = nid_pais_telefono;

            ActualizarCliente(objEntAdminCitas, objEntCliente);

        }

        if (tbl_observacion.Enabled)
        {
            AdminCitasBL objBLT = new AdminCitasBL();
            AdminCitaBE objBE = new AdminCitaBE();

            objBE.nid_cita = Convert.ToInt32(((AdminCitaBE)(Session["oAdminCitaBE"])).grid_nid_cita);
            objBE.DET_Observaciones = txt_det_observaciones.Text.Trim();

            int intResul = objBLT.UPDAdminCitaObservacion(objBE);

            Inabilitar_Sectores(false);
        }

    }

    protected void btn_det_ActCliente_Click(object sender, EventArgs e)
    {
        Inabilitar_Sectores(false);
        tbl_cliente.Enabled = !tbl_cliente.Enabled;
    }
    protected void btn_det_FormRecor_Click(object sender, EventArgs e)
    {
        Inabilitar_Sectores(false);
        tbl_recordatorio.Enabled = true;
    }

    protected void btn_Confirmar_Cita_Click(object sender, EventArgs e)
    {
        EstadoBotones(true, true, false);
        lbl_mensajebox.Text = _MSG_PREGUNTA.Replace("{estado}", "Confirmar");
        hf_ESTADO_CITA.Value = "Confirmar";
        popup_msgbox_confirm.Show();
    }
    protected void btn_Anular_Cita_Click(object sender, EventArgs e)
    {
        EstadoBotones(true, true, false);
        lbl_mensajebox.Text = _MSG_PREGUNTA.Replace("{estado}", "Anular");
        hf_ESTADO_CITA.Value = "Anular";
        popup_msgbox_confirm.Show();
    }
    protected void btn_Reprog_Cita_Click(object sender, EventArgs e)
    {
        oCitasBE = new CitasBE();
        oCitasBEList = new CitasBEList();

        CitasBL oCitasBL = new CitasBL();


        if (ConfigurationManager.AppSettings["CambiarTaller"].ToString().Equals("1"))
        {
            LimpiarFiltros(5);

            oCitasBE = new CitasBE();

            //Get nid_ubica
            oCitasBE.nid_cita = Convert.ToInt32(hf_ID_CITA.Value.ToString());
            CitasBEList lstCita = oCitasBL.GETListarDatosCita(oCitasBE);

            //Carga: Punto de Red
            //---------------------
            oCitasBE = new CitasBE();
            oCitasBEList = new CitasBEList();

            oCitasBE.nid_Servicio = Convert.ToInt32(hf_ID_SERVICIO.Value.ToString());
            oCitasBE.nid_modelo = Convert.ToInt32(hf_ID_MODELO.Value.ToString());
            oCitasBE.coddpto = "0";
            oCitasBE.codprov = "0";
            oCitasBE.coddis = "0";
            oCitasBE.Nid_usuario = Profile.Usuario.Nid_usuario;

            oCitasBEList = oCitasBL.Listar_PuntosRed(oCitasBE);

            ddlPuntoRed.Items.Clear();
            if (oCitasBEList.Count > 0)
            {
                ddlPuntoRed.DataSource = oCitasBEList;
                ddlPuntoRed.DataTextField = "no_ubica";
                ddlPuntoRed.DataValueField = "nid_ubica";
                ddlPuntoRed.DataBind();
                ddlPuntoRed.Items.Insert(0, new ListItem(_SELECCIONAR, "0"));
                //----
                ddlPuntoRed.SelectedValue = lstCita[0].nid_ubica.ToString();
                ddlPuntoRed_SelectedIndexChanged(this, null);
            }
            else
            {
                ddlPuntoRed.Items.Insert(0, new ListItem(_SELECCIONAR, "0"));
            }
        }
        else
        {
            string strFechaHabil = SRC_FECHA_HABIL().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);

            hf_FECHA_HABIL.Value = strFechaHabil;
            txtFecha.Text = strFechaHabil;
            hfFecha.Value = strFechaHabil;

            ConsultarHorarioReserva(); //---> GRID 1
            //MostrarHorarioDisponible();//---> GRID 2
        }

        //Mostrar el PopPup de Reprogramacion
        MpeReprogramacion.Show();

    }
    protected void btn_AtenderCita_Click(object sender, ImageClickEventArgs e)
    {
        CitasBE objBECita = new CitasBE();
        CitasBL objBLCita = new CitasBL();

        objBECita.nid_cita = Convert.ToInt32(((AdminCitaBE)(Session["oAdminCitaBE"])).grid_nid_cita);
        objBECita.Nid_usuario = Profile.Usuario.Nid_usuario;
        objBECita.nu_placa = txt_det_vehplaca.Text.Trim();

        objBECita.Fl_puntualidad = (chkpuntualidad.Checked) ? "1" : "0";
        objBECita.qt_km_inicial = Convert.ToInt32(txtkminicial.Text.Trim());
        objBECita.tx_glos = txtglosa.Text.Trim();
        //-----------------------------------------------
        objBECita.co_usuario_crea = Profile.UserName;
        objBECita.co_usuario_red = Profile.UsuarioRed;
        objBECita.no_estacion_red = Profile.Estacion;

        Int32 oRep = objBLCita.AtenderCita(objBECita);

        if (oRep <= 0)
        {
            SRC_MsgInformacion("El taller no permite atender una cita en estado Registrado.");
            mpAtenderCita.Hide();
        }
        else
        {
            Response.Redirect("SRC_AdmCitas.aspx");
        }
    }

    //***************************************************************************************************************/

    protected void btnEditarObserv_Click(object sender, EventArgs e)
    {
        Inabilitar_Sectores(false);
        tbl_observacion.Enabled = true;
    }
    private string getEstado(int numCita)
    {
        string strEstado = string.Empty;
        switch (numCita)
        {
            case 1: strEstado = "Registrada"; break;
            case 2: strEstado = "Reprogramada"; break;
            case 3: strEstado = "Anulada"; break;
            case 4: strEstado = "Confirmada"; break;
            case 5: strEstado = "Reasignada"; break;
            case 6: strEstado = "Atendida"; break;
            case 7: strEstado = "Cola de Espera"; break;
            default: strEstado = "Desconocido"; break;
        }
        return strEstado;
    }
    protected void btnRegresar2_Click(object sender, EventArgs e)
    {

    }

    //protected void btnQR_Click(object sender, ImageClickEventArgs e)
    //{
    //    #region HTML
    //    Parametros prm = new Parametros();
    //    string imagen = prm.SRC_AccedeQR + (new CitasBL().GetNombreImagenQRWithText(ViewState["img_qr"].ToString()));
    //    System.Text.StringBuilder strHTML = new StringBuilder();
    //    //string strBody = "<img class='imgQR' src='../images/PruebaQR.png'/>";
    //    string strBody = "<img class='imgQR' src='" + imagen + "'/>";
    //    strHTML.Append(strBody);
    //    strHTML.Replace("'", "\"");

    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script type=\"text/javascript\">" + "FC_PopupImprimirQR('" + strHTML + "');</" + "script>", false);


    //    #endregion

    //}

    private Bitmap rotateImage90(Bitmap b)
    {
        return b;
    }

    private string MySubstring(string texto)
    {

        int corte = 17;
        if (texto.Length > corte)
        {
            return texto.Substring(0, corte);
        }
        else
        {
            return texto;
        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        CitasBE oCita = (CitasBE)ViewState["oCitaImprimir"];

        System.Text.StringBuilder strHTML = new StringBuilder();
        string strBody = "";
        string strStyleTD = "";
        string strStyleTR = "";
        //-----------------------------------------------------------------
        strStyleTR = "font-size: 12px; font-family: arial;text-align: left;";
        strStyleTD = "font-weight: bold; background-color: #ebeff7;";
        //-----------------------------------------------------------------

        strBody += "<table height='28' cellspacing='0' cellpadding='6' width='570px'>";
        strBody += "<tr style='font-size: 14px; color: #012d68; font-family: Arial; background-color: #c7d7ee'>";
        strBody += "<td style='text-align: right'><b>Sistema de Reserva de Citas &nbsp;</b></td>";
        strBody += "</tr>";
        strBody += "</table>";
        //-----------------------------
        strBody += "<table cellspacing='2' border='0'><tbody>";
        strBody += "<tr><td style='font-size: 13px; color: #4264af; font-family: arial; text-align: justify'>Sr(a). <strong>" + oCita.no_cliente + "</strong></td></tr>";
        strBody += "<tr><td style='font-size: 12px; color: #4264af; font-family: arial; text-align: justify'><p>Se le adjunta el resúmen de la Reserva de su Cita:</p></td></tr>";
        strBody += "<tr><td style='font-size: 11px; color: #4264af; font-family: arial; text-align: justify'><p>&nbsp;</p></tr>";
        strBody += "</tbody>";
        strBody += "</table>";
        //-----------------------------
        strBody += "<table style='border:solid 1px #c7d7ee; color: #4264af;' cellspacing='1' cellpadding='2' width='570px'>";

        strBody += "<tr style='" + strStyleTR + "' >";
        strBody += "<td style='" + strStyleTD + "width:25%;' >Código Reserva</td>";
        strBody += "<td colspan='3'></strong>" + oCita.cod_reserva_cita + "</strong></td>";
        strBody += "</tr>";

        strBody += "<tr>";
        strBody += "<td  colspan='4'>&nbsp;</td>";
        strBody += "</tr>";

        strBody += "<tr style='" + strStyleTR + "' >";
        strBody += "<td style='" + strStyleTD + "' >" + oParametros.N_Placa + "</td>";
        strBody += "<td >" + oCita.nu_placa + "</td>";
        strBody += "<td >&nbsp;</td>";
        strBody += "<td >&nbsp;</td>";
        strBody += "</tr>";

        strBody += "<tr style='" + strStyleTR + "' >";
        strBody += "<td style='" + strStyleTD + "' >Marca</td>";
        strBody += "<td >" + oCita.no_marca + "</td>";
        strBody += "<td style='" + strStyleTD + "' >Modelo</td>";
        strBody += "<td >" + oCita.no_modelo + "</td>";
        strBody += "</tr>";

        strBody += "<tr>";
        strBody += "<td  colspan='4'>&nbsp;</td>";
        strBody += "</tr>";

        strBody += "<tr style='" + strStyleTR + "' >";
        strBody += "<td style='" + strStyleTD + "' >Cita</td>";
        strBody += "<td colspan='3'>" + oCita.fecha_atencion + "</td>";
        strBody += "</tr>";

        strBody += "<tr style='" + strStyleTR + "' >";
        strBody += "<td style='" + strStyleTD + "' >Servicio</td>";
        strBody += "<td colspan='3'>" + oCita.no_servicio + "</td>";
        strBody += "</tr>";

        strBody += "<tr style='" + strStyleTR + "' >";
        strBody += "<td style='" + strStyleTD + "' >Asesor de Servicio</td>";
        strBody += "<td colspan='3'>" + oCita.no_asesor + "</td>";
        strBody += "</tr>";

        strBody += "<tr style='" + strStyleTR + "' >";
        strBody += "<td style='" + strStyleTD + "' >Taller</td>";
        strBody += "<td >" + oCita.no_taller + "</td>";
        strBody += "<td style='" + strStyleTD + "' >Teléfono</td>";
        strBody += "<td >" + oCita.nu_telefono + "</td>";
        strBody += "</tr>";

        strBody += "<tr style='" + strStyleTR + "' >";
        strBody += "<td style='" + strStyleTD + "' >" + oParametros.N_Local + "</td>";
        strBody += "<td colspan='3'>" + oCita.no_ubica + "</td>";
        strBody += "</tr>";


        strBody += "<tr style='" + strStyleTR + "' >";
        strBody += "<td style='" + strStyleTD + "' >Dirección</td>";
        strBody += "<td colspan='3'>" + oCita.di_ubica + "</td>";
        strBody += "</tr>";

        strBody += "</table>";
        //-----------------------------
        strBody += "<p style='font-size: 11px; font-weight: bold; color: #4264af; font-family: arial; text-align: justify'>";
        strBody += "En caso desee reprogramar su cita llame a nuestro Call Center al " + oCita.nu_callcenter + "";
        strBody += "</p>";
        //-----------------------------
        strBody += "<table style='vertical-align: baseline' height='20' cellspacing='0' cellpadding='5' width='570px'>";
        strBody += "<tbody>";
        strBody += "<tr style='font-size: 11px; color: #ffffff; font-family: Arial; background-color: #000000'>  ";
        strBody += "<td style='text-align: center'><b>&copy; 2008 Grupo Gildemeister todos los derechos reservados</b></td>";
        strBody += "</tr>";
        strBody += "</table>";

        strHTML.Append(strBody);

        strHTML.Replace("'", "\"");//Formato pra visualizar

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script type=\"text/javascript\">" + "FC_PopupImprimir('" + strHTML + "');</" + "script>", false);

    }

    //***************************************************************************************************************/

    protected void EstadoBotones(bool blnSI, bool blnNO, bool blnAceptar)
    {
        btn_msgboxconfir_si.Visible = blnSI;
        btn_msgboxconfir_no.Visible = blnNO;
        btn_msgboxconfir_aceptar.Visible = blnAceptar;
    }
    protected void btn_msgboxconfir_si_Click(object sender, EventArgs e)
    {
        AdminCitaBE objECita = (AdminCitaBE)(Session["oAdminCitaBE"]);

        CitasBE objBE = new CitasBE();
        CitasBL objBL = new CitasBL();

        objBE.nid_cita = Convert.ToInt32(objECita.grid_nid_cita);
        objBE.nid_Estado = Convert.ToInt32(objECita.grid_nid_estado);
        objBE.co_usuario_crea = Profile.UserName;
        objBE.co_usuario_red = Profile.UsuarioRed;
        objBE.no_estacion_red = Profile.Estacion;

        int oRep = 0;

        strEstadoCita = hf_ESTADO_CITA.Value.ToString();

        string strEstado = objBL.VerificarEstadoCita(objBE).ToString();

        if (strEstadoCita.Equals("Confirmar"))
        {
            objBE.tex_verifica = "EC04";

            strEstado = objBL.VerificarCitasCambiaEstado(objBE);
            int Valor = (string.IsNullOrEmpty(strEstado)) ? 0 : Convert.ToInt32(strEstado);

            if (Valor == 0)
            {
                objBE.co_usuario_crea = Profile.UserName;
                objBE.co_usuario_red = Profile.UsuarioRed;
                objBE.no_estacion_red = Profile.Estacion;
                oRep = objBL.ConfirmarCita(objBE);
            }
            strEstadoCita = "Confirmada";
        }
        else if (strEstadoCita.Equals("Anular"))
        {
            if (!strEstado.Equals("EC07"))
            {
                //------------------- VERIFICAr CLIENTES EN COLA DE ESPERA ---------------------------

                hf_ID_CITA.Value = objBE.nid_cita.ToString();

                gv_Cola_Espera.DataSource = objBL.GETListarClientesEnColaEspera(objBE);
                gv_Cola_Espera.DataBind();
            }

            oRep = objBL.AnularCita(objBE);
            strEstadoCita = "Anulada";

            oCitasBEList = new CitasBEList();
            oCitasBEList = objBL.GETListarDatosCita(objBE);
            if (oCitasBEList.Count == 0) return;

            //>>------ ANULACION  ----- >>

            //if (!strEstado.Equals("EC07")) EnviarCorreo_Cliente(oCitasBEList[0], Parametros.EstadoCita.ANULADA);
            //if (oParametros.SRC_Pais.Equals(2)) EnviarCorreo_Asesor(oCitasBEList[0], Parametros.EstadoCita.ANULADA);
        }
        else if (strEstadoCita.Equals("Atender"))
        {
        }
        else
        {
            return;
        }

        //>>> RESULTADO 


        popup_msgbox_confirm.Hide();


        lbl_mensajebox.Text = _MSG_RESPUESTA;

        if (oRep == 1)
        {
            EstadoBotones(false, false, true);
            lbl_mensajebox.Text = lbl_mensajebox.Text.Replace("{estado}", strEstadoCita);
            popup_msgbox_confirm.Show();

        }
        else if (oRep == 0)
        {

            lbl_mensajebox.Text = "La Cita no existe.";
            EstadoBotones(false, false, true);
            popup_msgbox_confirm.Show();
        }
        else if (oRep > 10)
        {
            oRep = oRep - 10; // > num estado cita
            lbl_mensajebox.Text = "El Estado de la Cita ha sido cambiada a [" + getEstado(oRep) + "] por otro Usuario.";
            EstadoBotones(false, false, true);
            popup_msgbox_confirm.Show();
        }
    }
    protected void btn_msgboxconfir_no_Click(object sender, EventArgs e)
    {
        popup_msgbox_confirm.Hide();
        lbl_mensajebox.Text = _MSG_PREGUNTA;
    }
    protected void btn_msgboxconfir_aceptar_Click(object sender, EventArgs e)
    {
        popup_msgbox_confirm.Hide();

        string strEstadoCita = hf_ESTADO_CITA.Value.ToString().Trim();

        if (strEstadoCita.Equals("Reprogramar"))
        {
            if (open_contact_center)
            {
                Session["OpenModalContactCenter"] = 1;
                open_contact_center = false;
            }

            if (gv_Cola_Espera.Rows.Count > 0)
            {
                if (lblMensaje.Text.Contains("Asignada"))
                {
                    Response.Redirect("SRC_AdmCitas.aspx");
                }
                else
                {
                    mpColaEspera.Show();
                }
            }
            else
            {
                Response.Redirect("SRC_AdmCitas.aspx");
            }
        }
        else if (strEstadoCita.Equals("Anular"))
        {
            if (gv_Cola_Espera.Rows.Count > 0)
            {
                if (lblMensaje.Text.Contains("Asignada"))
                {
                    Response.Redirect("SRC_AdmCitas.aspx");
                }
                else
                {
                    mpColaEspera.Show();
                }
            }
            else
            {
                Response.Redirect("SRC_AdmCitas.aspx");
            }
        }
        else if (strEstadoCita.Equals("ColaEspera"))
        {
            Response.Redirect("SRC_AdmCitas.aspx");
        }
        else
        {
            Response.Redirect("SRC_AdmCitas.aspx");
        }
    }

    protected void btn_BuscarContacto_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            oCitasBE = new CitasBE();
            CitasBL oCitasBL = new CitasBL();

            oCitasBE.nu_documento = txt_det_clidnirut.Text.Trim();

            oCitasBEList = new CitasBEList();
            oCitasBEList = oCitasBL.GETListarDatosContactoPorDoc(oCitasBE);

            if (oCitasBEList.Count > 0)
            {
                // LimpiarContacto();
                txt_det_clinombre.Text = string.Empty;
                txt_det_cliapellidospat.Text = string.Empty;
                txt_det_cliapellidosmat.Text = string.Empty;
                txt_det_clidnirut.Text = string.Empty;
                txt_det_clitelffijo.Text = string.Empty;
                txt_det_clitelfoficina.Text = string.Empty;
                txt_det_clitelfmovil.Text = string.Empty;
                txt_det_clitelfmovil2.Text = string.Empty;
                txt_det_cliemail.Text = string.Empty;
                txt_det_cliemail_trab.Text = string.Empty;
                txt_det_cliemail_alter.Text = string.Empty;
                txtTelefonoFijo_Anexo.Text = string.Empty;
                hfPaisTelefonoCelular.Value = string.Empty;
                hfPaisTelefonoFijo.Value = string.Empty;

                oCitasBE = new CitasBE();
                oCitasBE = oCitasBEList[0];

                //-------------------
                // DATOS - CONTACTO
                //-------------------

                txt_det_clinombre.Text = string.Empty;
                txt_det_cliapellidospat.Text = string.Empty;
                txt_det_cliapellidosmat.Text = string.Empty;
                txt_det_clidnirut.Text = string.Empty;
                txt_det_clitelffijo.Text = string.Empty;
                txt_det_clitelfoficina.Text = string.Empty;
                txt_det_clitelfmovil.Text = string.Empty;
                txt_det_clitelfmovil2.Text = string.Empty;
                txt_det_cliemail.Text = string.Empty;
                txt_det_cliemail_trab.Text = string.Empty;
                txt_det_cliemail_alter.Text = string.Empty;

                //-----------------------

                txt_det_clidnirut.Text = oCitasBE.nu_documento.ToString();
                txt_det_cliapellidospat.Text = oCitasBE.no_ape_paterno;
                txt_det_cliapellidosmat.Text = oCitasBE.no_ape_materno;
                txt_det_clinombre.Text = oCitasBE.no_nombre;
                txt_det_clitelffijo.Text = oCitasBE.nu_telefono;
                txt_det_clitelfoficina.Text = oCitasBE.nu_tel_oficina;
                txt_det_clitelfmovil.Text = oCitasBE.nu_celular.Contains("-") ? oCitasBE.nu_celular.Split('-').GetValue(1).ToString() : oCitasBE.nu_celular;
                txt_det_clitelfmovil2.Text = oCitasBE.nu_celular_alter.Contains("-") ? oCitasBE.nu_celular_alter.Split('-').GetValue(1).ToString() : oCitasBE.nu_celular_alter;
                txt_det_cliemail.Text = oCitasBE.no_correo;
                txt_det_cliemail_trab.Text = oCitasBE.no_correo_trabajo;
                txt_det_cliemail_alter.Text = oCitasBE.no_correo_alter;
            }
        }
        catch (Exception ex)
        {
            SRC_MsgError(ex.Message);
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.Web.Services;
using System.Configuration;
using System.Collections;
using System.Globalization;


public partial class SRC_Operaciones_SRC_RegistroCitas : System.Web.UI.Page
{
    public static Parametros oPrm = new Parametros();
    public static string oJuridica = ConfigurationManager.AppSettings["PersonaJuridica"].ToString();
    public static string oNatural = ConfigurationManager.AppSettings["PersonaNatural"].ToString();
    public static string oDNI = ConfigurationManager.AppSettings["TIPODOCDNI"].ToString();
    public static string oRUC = ConfigurationManager.AppSettings["TIPODOCRUC"].ToString();
    public static String fl_ubigeo_all = "1";
    private static Boolean fl_format_24_horas = true;
    public static string placa_defecto_cc = "";
    public static string placa_defecto = "";
    public static int nid_contact_center = 0;
    public const string imgURL_HORA_LIBRE = "../Images/SRC/SI.PNG";
    public const string imgURL_HORA_RESERVADA = "../Images/SRC/NO.PNG";
    private const string imgURL_HORA_VACIA = "../Images/SRC/vacio.PNG";
    private const string imgURL_HORA_EXCEPCIONAL = "../Images/SRC/VACIO.PNG";
    public const string imgURL_HORA_SEPARADA = "../Images/SRC/SEPARA.PNG";
    private const string imgURL_VALORACION = "img/valoracion/estrellas-{0}.png";

    protected void Page_Load(object sender, EventArgs e)
    {
        string AccesoPagina = (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_ReservaCita_AccionVerFormulario);
        if (string.IsNullOrEmpty(AccesoPagina))
            AccesoPagina = (Master as Principal).ValidaTipoAccesoPagina(Page, "SinAcceso");

        if (!Page.IsPostBack)
        {
            placa_defecto_cc = "";
            
            if (Request.Form["placa"] != null)
            {
                placa_defecto = Request.Form["placa"].ToString();
                string contact_center_filtros = Request.Form["filtros_selected"] != null ? Request.Form["filtros_selected"].ToString() : "";
                nid_contact_center = int.Parse(Request.Form["nid_cc"].ToString());
                if (placa_defecto != "")
                {
                    placa_defecto_cc = placa_defecto.Trim();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", String.Format("<script type=\"text/javascript\">" + "FC_CargarPlacaPorDefecto('{0}','{1}');</" + "script>", placa_defecto, contact_center_filtros), false);
                }
            }

        }
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_Inicial(Int32 nid_usuario)
    {
        CitasBL oCitasBL = new CitasBL();
        List<object> oComboMarca = new List<object>();
        List<object> oComboTipoServicio = new List<object>();
        List<object> oComboTipoPersona = new List<object>();
        List<object> oComboTipoDocumento = new List<object>();
        List<object> oDias = new List<object>();
        List<object> oHorasIni = new List<object>();
        List<object> oHorasFin = new List<object>();
        object response;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        #region "- Obtiene Marcas"
        CitasBE oCitasBE = new CitasBE();
        oCitasBE.nid_usuario = nid_usuario;
        CitasBEList oMarcas = oCitasBL.GETListarMarcas(oCitasBE);
        foreach (CitasBE obj in oMarcas)
        {
            oComboMarca.Add(new
            {
                value = obj.nid_marca.ToString(),
                nombre = obj.no_marca
            });
        }
        #endregion "- Obtiene Marcas"
        #region "- Obtiene Tipo Persona"
        MaestroVehiculoBL oMaestroVehiculoBL = new MaestroVehiculoBL();
        CombosBEList oTiposPersona = oMaestroVehiculoBL.GETListarTipoPersona();
        foreach (ComboBE obj in oTiposPersona)
        {
            oComboTipoPersona.Add(new { value = obj.ID, nombre = obj.DES });
        }
        #endregion "- Obtiene Tipo Persona"
        #region "- Obtiene Tipo Documento"
        CitasBEList oTiposDocumento = oCitasBL.GETListarTipoDocumentos();
        foreach (CitasBE obj in oTiposDocumento)
        {
            oComboTipoDocumento.Add(new { value = obj.cod_tipo_documento.Trim(), nombre = obj.des_tipo_documento });
        }
        #endregion "- Obtiene Tipo Documento"
        #region "- Obtiene Datos Contacto"
        Parametros oParametros = new Parametros();
        string fl_VerDatosContacto = oParametros.GetValor(Parametros.PARM._14).ToString();
        if (fl_VerDatosContacto == "1")
        {
            CitasBEList oCitasBEList = oCitasBL.Listar_HorarioRecordatorio();
            if (oCitasBEList.Count > 0)
            {
                string dtHI = oCitasBEList[0].no_valor;
                string dtHF = oCitasBEList[1].no_valor;
                Int32 intINTERV = Convert.ToInt32(oCitasBEList[2].no_valor);
                string[] strDias = oCitasBEList[3].no_valor.ToString().Split('|');
                if (strDias.Length > 0)
                {
                    string no_dia = "";
                    for (int i = 0; i < strDias.Length; i++)
                    {
                        if (strDias[i].Trim().Equals("")) continue;
                        no_dia = getNombreDiaSemana(Convert.ToInt32(strDias[i]));
                        oDias.Add(new { value = strDias[i], nombre = no_dia });
                    }

                    dtHI = ((IsDate(dtHI)) ? dtHI : "00:00");
                    dtHF = ((IsDate(dtHF)) ? dtHF : "23:00");
                    DateTime dtHoraIni = Convert.ToDateTime(dtHI);
                    DateTime dtHoraFin = Convert.ToDateTime(dtHF);
                    while (dtHoraIni <= dtHoraFin)
                    {
                        string strHoraT = dtHoraIni.ToString("hh:mm") + (dtHoraIni.Hour >= 12 ? " PM" : " AM");
                        string strHoraV = dtHoraIni.ToString("HH:mm").ToUpper().Replace(".", "");

                        oHorasIni.Add(new { value = strHoraV, nombre = strHoraT });
                        oHorasFin.Add(new { value = strHoraV, nombre = strHoraT });

                        dtHoraIni = dtHoraIni.AddMinutes(intINTERV);
                    }
                }
            }
        }
        #endregion "- Obtiene Datos Contacto"

        response = new
        {
            oComboMarca = oComboMarca,
            oComboTipoServicio = oComboTipoServicio,
            oComboTipoPersona = oComboTipoPersona,
            oComboTipoDocumento = oComboTipoDocumento,
            PARM_14 = fl_VerDatosContacto,
            oDias = oDias,
            oHorasIni = oHorasIni,
            oHorasFin = oHorasFin,
            placa_def = placa_defecto
            ,contact_center = nid_contact_center
        };
        placa_defecto = "";
        nid_contact_center = 0;
        return serializer.Serialize(response);
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_Placas(String Value)
    {
        System.Collections.ArrayList response = new System.Collections.ArrayList();
        try
        {
            Int32 count = 10;
            CitasBL oCitasBL = new CitasBL();
            CitasBE oCitasBE = new CitasBE();
            oCitasBE.nu_placa = Value.Trim();
            CitasBEList oCitasBEList = oCitasBL.GetPlacaPatente(oCitasBE);

            Int32 i = 0;
            foreach (CitasBE obj in oCitasBEList)
            {
                object filas = new
                {
                    Value = obj.nu_placa,
                    Label = obj.nu_placa,
                    Name = obj.nid_vehiculo,
                    objDatos = new
                    {
                        nid_vehiculo = obj.nid_vehiculo,
                        nu_placa = obj.nu_placa
                    }
                };
                response.Add(filas);
                i++;
                if (i >= count)
                {
                    break;
                }
            }
        }
        catch
        {
        }
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(response);
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_Vehiculo(String[] strParametros)
    {
        Parametros oParametros = new Parametros();
        CitasBL oCitasBL = new CitasBL();
        CitasBE oVehiculoBE;
        String fl_seguir = "0";
        String msg_retorno = String.Empty;
        object strRetorno;
        String fl_aviso_recall = "0";
        String msg_aviso_recall = "";
        try
        {
            String nu_placa = strParametros[0].Trim();
            Int32 nid_usuario = Convert.ToInt32(strParametros[1]);

            oVehiculoBE = new CitasBE();
            oVehiculoBE.nu_placa = nu_placa;
            oVehiculoBE.nid_usuario = nid_usuario;
            CitasBEList oVehiculoBEList = oCitasBL.GETListarDatosVehiculoClientePorPlaca(oVehiculoBE);

            object oVehiculo = null;
            object oPropietario = null;
            object oCliente = null;
            object oContacto = null;
            object oCitaPendiente = null;
            List<object> oComboTipoServicio = null;
            if (oVehiculoBEList.Count == 0)
            {
                msg_retorno = "La " + oParametros.N_Placa + " no existe o el usuario no tiene permiso para dicho Modelo.";
            }
            else
            {
                oVehiculoBE = oVehiculoBEList[0];

                fl_seguir = "1";
                oVehiculo = new
                {
                    nid_vehiculo = oVehiculoBE.nid_vehiculo,
                    nu_placa = oVehiculoBE.nu_placa,
                    nid_marca = oVehiculoBE.nid_marca,
                    no_marca = oVehiculoBE.no_marca,
                    nid_modelo = oVehiculoBE.nid_modelo,
                    no_modelo = oVehiculoBE.no_modelo,
                    nu_vin = oVehiculoBE.Nu_vin,
                    no_propietario = ((placa_defecto_cc == nu_placa.Trim()) ? (!string.IsNullOrEmpty(oVehiculoBE.nu_documento_prop) ? (oVehiculoBE.nu_documento_prop + " - ") : "") + oVehiculoBE.propietario : ""),
                    no_cliente = ((placa_defecto_cc == nu_placa.Trim()) ? (!string.IsNullOrEmpty(oVehiculoBE.nu_documento_cli) ? (oVehiculoBE.nu_documento_cli + " - ") : "") + oVehiculoBE.cliente : "")
                    ,no_color = oVehiculoBE.no_color_exterior
                };

                if (placa_defecto_cc == nu_placa.Trim())
                {

                    oPropietario = new
                    {
                        nid_cliente = oVehiculoBE.nid_propietario,
                        co_tipo_persona = oVehiculoBE.co_tipo_persona_prop,
                        co_tipo_documento = oVehiculoBE.co_tipo_documento_prop.Trim(),
                        nu_documento = oVehiculoBE.nu_documento_prop.ToString(),
                        no_ape_paterno = oVehiculoBE.no_ape_pat_prop,
                        no_ape_materno = oVehiculoBE.no_ape_mat_prop,
                        no_nombre_razon = (oVehiculoBE.co_tipo_persona_prop.Equals(oNatural) ? oVehiculoBE.no_cliente_prop.Trim() : oVehiculoBE.no_razon_social_prop.Trim()),
                        nu_telefono = oVehiculoBE.nu_telefono_prop,
                        nu_tel_oficina = oVehiculoBE.nu_telefono2_prop,
                        nu_telmovil1 = (oVehiculoBE.nu_celular_prop.Contains("-") ? oVehiculoBE.nu_celular_prop.Split('-').GetValue(1).ToString() : oVehiculoBE.nu_celular_prop),
                        nu_telmovil2 = (oVehiculoBE.nu_celular2_prop.Contains("-") ? oVehiculoBE.nu_celular2_prop.Split('-').GetValue(1).ToString() : oVehiculoBE.nu_celular2_prop),
                        no_correo = oVehiculoBE.no_correo_prop.Trim(),
                        no_correo_trabajo = oVehiculoBE.no_correo_trab_prop.Trim(),
                        no_correo_alter = oVehiculoBE.no_correo_alter_prop.Trim()
                    };

                    oCliente = new
                    {
                        nid_cliente = oVehiculoBE.nid_cliente,
                        co_tipo_persona = oVehiculoBE.co_tipo_persona_cli,
                        co_tipo_documento = oVehiculoBE.co_tipo_documento_cli.Trim(),
                        nu_documento = oVehiculoBE.nu_documento_cli.ToString(),
                        no_ape_paterno = oVehiculoBE.no_ape_pat_cli,
                        no_ape_materno = oVehiculoBE.no_ape_mat_cli,
                        no_nombre_razon = (oVehiculoBE.co_tipo_persona_cli.Equals(oNatural) ? oVehiculoBE.no_cliente_cli.Trim() : oVehiculoBE.no_razon_social_cli.Trim()),
                        nu_telefono = oVehiculoBE.nu_telefono_cli,
                        nu_tel_oficina = oVehiculoBE.nu_telefono2_cli,
                        nu_telmovil1 = (oVehiculoBE.nu_celular_cli.Contains("-") ? oVehiculoBE.nu_celular_cli.Split('-').GetValue(1).ToString() : oVehiculoBE.nu_celular_cli),
                        nu_telmovil2 = (oVehiculoBE.nu_celular2_cli.Contains("-") ? oVehiculoBE.nu_celular2_cli.Split('-').GetValue(1).ToString() : oVehiculoBE.nu_celular2_cli),
                        no_correo = oVehiculoBE.no_correo_cli.Trim(),
                        no_correo_trabajo = oVehiculoBE.no_correo_trab_cli.Trim(),
                        no_correo_alter = oVehiculoBE.no_correo_alter_cli.Trim()
                        ,nid_pais_celular = oVehiculoBE.nid_pais_celular_cli
                        ,nid_pais_telefono = oVehiculoBE.nid_pais_telefono_cli
                        ,nu_anexo_telefono = oVehiculoBE.nu_anexo_telefono_cli
                    };

                    oContacto = new
                    {
                        nid_cliente = oVehiculoBE.nid_contacto_sr,
                        co_tipo_documento = oVehiculoBE.cod_tipo_documento.Trim(),
                        nu_documento = oVehiculoBE.nu_documento.ToString(),
                        no_ape_paterno = oVehiculoBE.no_ape_paterno,
                        no_ape_materno = oVehiculoBE.no_ape_materno,
                        no_nombre = oVehiculoBE.no_nombre,
                        nu_telefono = oVehiculoBE.nu_telefono,
                        nu_tel_oficina = oVehiculoBE.nu_tel_oficina,
                        nu_telmovil1 = oVehiculoBE.nu_celular.Contains("-") ? oVehiculoBE.nu_celular.Split('-').GetValue(1).ToString() : oVehiculoBE.nu_celular,
                        nu_telmovil2 = oVehiculoBE.nu_celular_alter.Contains("-") ? oVehiculoBE.nu_celular_alter.Split('-').GetValue(1).ToString() : oVehiculoBE.nu_celular_alter,
                        no_correo = oVehiculoBE.no_correo,
                        no_correo_trabajo = oVehiculoBE.no_correo_trabajo,
                        no_correo_alter = oVehiculoBE.no_correo_alter
                    };
                }
                else
                {
                    object objNull = null;
                    oPropietario = objNull;
                    oCliente = objNull;
                    oContacto = objNull;
                }

                                #region - Verificar Citas pendientes
                CitasBE oCitasBE = new CitasBE();
                oCitasBE.nu_placa = oVehiculoBE.nu_placa;
                CitasBEList oCitasPendientesBEList = oCitasBL.VerificarCitasPedientesPlaca(oCitasBE);
                if (oCitasPendientesBEList.Count > 0)
                {
                    string strPRM_16 = oParametros.GetValor(Parametros.PARM._16);
                    if (strPRM_16.Equals("1"))
                    {
                        CitasBE oCitaPendienteBE = oCitasPendientesBEList[0];

                        if (placa_defecto_cc == nu_placa.Trim())
                        {
                            oCitaPendiente = new
                            {
                                no_cliente = oCitaPendienteBE.no_cliente,
                                nu_tel_fijo = oCitaPendienteBE.nu_tel_fijo,
                                nu_placa = oCitaPendienteBE.nu_placa,
                                no_marca = oCitaPendienteBE.no_marca,
                                no_modelo = oCitaPendienteBE.no_modelo,
                                cod_reserva_cita = oCitaPendienteBE.cod_reserva_cita,
                                fecha_prog = oCitaPendienteBE.fe_prog.ToShortDateString(),
                                ho_inicio = oCitaPendienteBE.ho_inicio,
                                no_servicio = oCitaPendienteBE.no_servicio,
                                no_taller = oCitaPendienteBE.no_taller,
                                no_direccion = oCitaPendienteBE.no_direccion,
                                no_asesor = oCitaPendienteBE.no_asesor
                            };
                        }
                        else
                        {
                            oCitaPendiente = new
                            {
                                no_cliente = "",
                                nu_tel_fijo = "",
                                nu_placa = oCitaPendienteBE.nu_placa,
                                no_marca = oCitaPendienteBE.no_marca,
                                no_modelo = oCitaPendienteBE.no_modelo,
                                cod_reserva_cita = "",
                                fecha_prog = oCitaPendienteBE.fe_prog.ToShortDateString(),
                                ho_inicio = oCitaPendienteBE.ho_inicio,
                                no_servicio = "",
                                no_taller = "",
                                no_direccion = "",
                                no_asesor = ""
                            };
                        }
                    }
                    if (oParametros.GetValor(Parametros.PARM._08).Equals("0"))
                    {
                        if (strPRM_16.Equals("0"))
                            msg_retorno = oParametros.SRC_CodPais.Equals("1") ? "Esta " + oParametros.N_Placa + " ya contiene una cita pendiente." : oParametros.msgCitasPendPlaca;
                        fl_seguir = "0";
                    }
                }
                #endregion - Verificar Citas pendientes

                #region "- Obtiene Tipos de Servicio por Modelo"
                TipoServicioBL oMaestroTipoServicioBL = new TipoServicioBL();
                TipoServicioBE oMaestroTipoServicioBE = new TipoServicioBE();
                oMaestroTipoServicioBE.nid_usuario = nid_usuario;
                oMaestroTipoServicioBE.nid_modelo = Convert.ToInt32(oVehiculoBE.nid_modelo);
                List<TipoServicioBE> lstTipoServicios = oMaestroTipoServicioBL.GETListarTiposServicio(oMaestroTipoServicioBE);

                oComboTipoServicio = new List<object>();
                foreach (TipoServicioBE oTS in lstTipoServicios)
                {
                    oComboTipoServicio.Add(new
                    {
                        value = oTS.Id_TipoServicio.ToString(),
                        nombre = oTS.No_tipo_servicio
                    });
                }
                if (oComboTipoServicio.Count == 0)
                {
                    msg_retorno = "No se encontró ningun servicio para la marca y modelo.";
                    fl_seguir = "0";
                }
                #endregion "- Obtiene Tipos de Servicio por Modelo"
            }

            strRetorno = new
            {
                oVehiculo = oVehiculo,
                oPropietario = oPropietario,
                oCliente = oCliente,
                oContacto = oContacto,
                fl_seguir = fl_seguir,
                msg_retorno = msg_retorno,
                oCitaPendiente = oCitaPendiente,
                oComboTipoServicio = oComboTipoServicio,
                fl_aviso_recall = fl_aviso_recall,
                msg_aviso_recall = msg_aviso_recall
            };
        }
        catch (Exception ex)
        {
            object objNull = null;
            strRetorno = new
            {
                oVehiculo = objNull,
                oPropietario = objNull,
                oCliente = objNull,
                oContacto = objNull,
                fl_seguir = "-1",
                msg_retorno = "Error: " + ex.Message,
                oCitaPendiente = objNull,
                oComboTipoServicio = new List<object>(),
                fl_aviso_recall = "",
                msg_aviso_recall = ""
            };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_Modelos(String[] strParametros)
    {
        Int32 nid_usuario; Int32.TryParse(strParametros[0], out nid_usuario);
        Int32 nid_marca; Int32.TryParse(strParametros[1], out nid_marca);

        List<object> oComboModelo = new List<object>();
        object response;
        try
        {
            CitasBL oCitasBL = new CitasBL();
            CitasBE oCitasBE = new CitasBE();
            oCitasBE.nid_usuario = nid_usuario;
            oCitasBE.nid_marca = nid_marca;
            CitasBEList oModelos = oCitasBL.GETListarModelosPorMarca(oCitasBE);

            object objModelo;
            foreach (CitasBE obj in oModelos)
            {
                objModelo = new { value = obj.nid_modelo.ToString(), nombre = obj.no_modelo };
                oComboModelo.Add(objModelo);
            }
        }
        catch //(Exception ex)
        { }

        response = new
        {
            oComboModelo = oComboModelo
        };

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(response);
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_Cliente(String[] strParametros)
    {
        CitasBE oCitasBE = new CitasBE();
        CitasBL oCitasBL = new CitasBL();
        object strRetorno;

        object oCliente = null;
        try
        {
            String co_tipo_documento = strParametros[0].Trim();
            String nu_documento = strParametros[1].Trim();

            oCitasBE.nu_documento = nu_documento;
            oCitasBE.cod_tipo_documento = co_tipo_documento;
            CitasBEList oCitasBEList = oCitasBL.GETListarDatosContactoPorDoc(oCitasBE);

            if (oCitasBEList.Count > 0)
            {
                oCitasBE = new CitasBE();
                oCitasBE = oCitasBEList[0];

                oCliente = new
                {
                    nid_cliente = oCitasBE.nid_cliente,
                    co_tipo_documento = oCitasBE.cod_tipo_documento,
                    nu_documento = oCitasBE.nu_documento,
                    no_ape_paterno = oCitasBE.no_ape_paterno,
                    no_ape_materno = oCitasBE.no_ape_materno,
                    no_nombre_razon = oCitasBE.no_nombre,
                    nu_telefono = oCitasBE.nu_telefono,
                    nu_tel_oficina = oCitasBE.nu_tel_oficina,
                    nu_telmovil1 = (oCitasBE.nu_celular.Contains("-") ? oCitasBE.nu_celular.Split('-').GetValue(1).ToString() : oCitasBE.nu_celular),
                    nu_telmovil2 = (oCitasBE.nu_celular_alter.Contains("-") ? oCitasBE.nu_celular_alter.Split('-').GetValue(1).ToString() : oCitasBE.nu_celular_alter),
                    no_correo = oCitasBE.no_correo.Trim(),
                    no_correo_trabajo = oCitasBE.no_correo_trabajo.Trim(),
                    no_correo_alter = oCitasBE.no_correo_alter.Trim()
                };

            }

            strRetorno = new { oCliente = oCliente };
        }
        catch //(Exception ex)
        {
            strRetorno = new { oCliente = "" };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_Servicios(String[] strParametros)
    {
        String msg_retorno = String.Empty;
        object strRetorno;
        String fl_seguir = "1";
        List<object> oComboServicios = new List<object>();
        try
        {
            Int32 nid_tipo_servicio; Int32.TryParse(strParametros[0], out nid_tipo_servicio);
            Int32 nid_usuario; Int32.TryParse(strParametros[1], out nid_usuario);
            Int32 nid_modelo; Int32.TryParse(strParametros[2], out nid_modelo);

            ServicioBE oMaestroServicioBE = new ServicioBE();
            ServicioBL oMaestroServicioBL = new ServicioBL();
            oMaestroServicioBE.Id_TipoServicio = nid_tipo_servicio;
            oMaestroServicioBE.nid_usuario = nid_usuario;
            oMaestroServicioBE.nid_modelo = nid_modelo;
            ServicioBEList oServicioBEList = oMaestroServicioBL.GETListarServiciosPorTipo(oMaestroServicioBE);

            object objServicio;
            foreach (ServicioBE oServicio in oServicioBEList)
            {
                objServicio = new { value = oServicio.Id_Servicio.ToString(), nombre = oServicio.No_Servicio };
                oComboServicios.Add(objServicio);
            }

            strRetorno = new { fl_seguir = fl_seguir, msg_retorno = msg_retorno, oComboServicios = oComboServicios };
        }
        catch (Exception ex)
        {
            strRetorno = new
            {
                fl_seguir = "-1",
                msg_retorno = "Error: " + ex.Message,
                oComboServicios = oComboServicios
            };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }
    public static bool validarVisibilidadCampañas()
    {
        int parametro = 0;
        AppMiTaller.Intranet.BE.TipoTablaDetalleBEList oLista = new AppMiTaller.Intranet.BE.TipoTablaDetalleBEList();
        AppMiTaller.Intranet.BL.TipoTablaDetalleBL oTipoTablaDetalleBL = new AppMiTaller.Intranet.BL.TipoTablaDetalleBL();
        oLista = oTipoTablaDetalleBL.ListarTipoTablaDetalle("1", String.Empty, String.Empty, "91", String.Empty, String.Empty, String.Empty);
        if (oLista.Count > 0)
            parametro = int.Parse(oLista[0].Valor3);

        return parametro == 1;
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_UbigeoDisponible(String[] strParametros)
    {
        String msg_retorno = String.Empty;
        object strRetorno;
        String fl_seguir = "1";
        List<object> oComboDepartamento = new List<object>();
        List<object> oComboProvincia = new List<object>();
        List<object> oComboDistrito = new List<object>();
        String fl_quick_service = String.Empty, fl_dias_validos = String.Empty;
        try
        {
            Int32 nid_servicio; Int32.TryParse(strParametros[0], out nid_servicio);
            Int32 nid_marca; Int32.TryParse(strParametros[1], out nid_marca);
            Int32 nid_modelo; Int32.TryParse(strParametros[2], out nid_modelo);
            Int32 nid_usuario; Int32.TryParse(strParametros[3], out nid_usuario);
            String nu_placa = strParametros[4];

            if (nid_servicio > 0)
            {
                CitasBE oValidaKmBE = new CitasBL().Obtiene_Validacion_Km(nu_placa, nid_servicio, nid_marca);
                
                if (oValidaKmBE.tx_alternativo_01 == "-1")
                {
                    msg_retorno = oValidaKmBE.tx_alternativo_02;
                    fl_seguir = "0";
                }

                if (fl_seguir != "0")
                {
                    ServicioBL oMaestroServicioBL = new ServicioBL();
                    ServicioBE oMaestroServicioBE = new ServicioBE();
                    oMaestroServicioBE.Id_Servicio = nid_servicio;
                    ServicioBEList oMaestroServicioBEList = oMaestroServicioBL.GETListarDatosServicios(oMaestroServicioBE);
                    fl_quick_service = oMaestroServicioBEList[0].Fl_quick_service;
                    fl_dias_validos = oMaestroServicioBEList[0].no_dias_validos;

                    #region - Obtiene Ubigeo Disponible
                    CitasBL oCitasBL = new CitasBL();
                    CitasBE oCitasBE = new CitasBE();
                    oCitasBE.nid_Servicio = nid_servicio;
                    oCitasBE.nid_modelo = nid_modelo;
                    oCitasBE.nid_usuario = nid_usuario;
                    CitasBEList oUbigeoDisponible = oCitasBL.GetListar_Ubigeos_Disponibles(oCitasBE);
                    if (oUbigeoDisponible.Count > 0)
                    {
                        object objDepartamento;
                        foreach (CitasBE oUbigeo in oUbigeoDisponible)
                        {
                            objDepartamento = new { value = oUbigeo.coddpto.ToString(), nombre = oUbigeo.nomdpto };
                            oComboDepartamento.Add(objDepartamento);
                        }

                        object objProvincia;
                        List<CitasBE> oProvinciaDisponible = oUbigeoDisponible.OrderBy(prov => prov.nomprov).ToList();
                        String codprov = String.Empty;
                        foreach (CitasBE oUbigeo in oProvinciaDisponible)
                        {
                            if (fl_ubigeo_all == "1") { codprov = oUbigeo.coddpto + oUbigeo.codprov; }
                            else { codprov = oUbigeo.codprov; }
                            objProvincia = new { value = codprov, nombre = oUbigeo.nomprov, coddpto = oUbigeo.coddpto };
                            oComboProvincia.Add(objProvincia);
                        }

                        object objDistrito;
                        List<CitasBE> oDistritoDisponible = oUbigeoDisponible.OrderBy(dist => dist.nomdist).ToList();
                        String coddist = String.Empty;
                        foreach (CitasBE oUbigeo in oDistritoDisponible)
                        {
                            if (fl_ubigeo_all == "1") { coddist = oUbigeo.coddpto + oUbigeo.codprov + oUbigeo.coddis; }
                            else { coddist = oUbigeo.coddis; }
                            objDistrito = new
                            {
                                value = coddist,
                                nombre = oUbigeo.nomdist,
                                coddpto = oUbigeo.coddpto,
                                codprov = oUbigeo.codprov
                            };
                            oComboDistrito.Add(objDistrito);
                        }
                    }
                    else
                    {
                        msg_retorno = "No hay talleres disponibles para este servicio y modelo.";
                        fl_seguir = "0";
                    }
                }
                #endregion - Obtiene Ubigeo Disponible
            }

            strRetorno = new
            {
                fl_seguir = fl_seguir,
                msg_retorno = msg_retorno,
                oComboDepartamento = oComboDepartamento.Distinct(),
                oComboProvincia = oComboProvincia.Distinct(),
                oComboDistrito = oComboDistrito.Distinct()
            };
        }
        catch (Exception ex)
        {
            strRetorno = new
            {
                fl_seguir = "-1",
                msg_retorno = "Error: " + ex.Message,
                oComboDepartamento = oComboDepartamento,
                oComboProvincia = oComboProvincia,
                oComboDistrito = oComboDistrito
            };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_UbicacionDisponible(String[] strParametros)
    {
        String msg_retorno = String.Empty;
        object strRetorno;
        String fl_seguir = "1";
        List<object> oComboUbicacion = new List<object>();
        List<object> oComboTaller = new List<object>();
        String fl_quick_service = String.Empty, fl_dias_validos = String.Empty;
        try
        {
            Int32 nid_modelo; Int32.TryParse(strParametros[0], out nid_modelo);
            Int32 nid_servicio; Int32.TryParse(strParametros[1], out nid_servicio);
            String coddpto = strParametros[2] == "" ? "0" : strParametros[2];
            String codprov = strParametros[3] == "" ? "0" : strParametros[3];
            String coddist = strParametros[4] == "" ? "0" : strParametros[4];
            Int32 nid_usuario; Int32.TryParse(strParametros[5], out nid_usuario);

            #region - Obtiene Ubicacion Disponible
            CitasBE oTallerBE = new CitasBE();
            CitasBL oTallerBL = new CitasBL();
            oTallerBE.nid_Servicio = nid_servicio;
            oTallerBE.nid_modelo = nid_modelo;
            oTallerBE.coddpto = coddpto;
            oTallerBE.codprov = codprov;
            oTallerBE.coddis = coddist;
            oTallerBE.nid_usuario = nid_usuario;
            CitasBEList oUbicacionDisponible = oTallerBL.Listar_PuntosRed(oTallerBE);
            if (oUbicacionDisponible.Count > 0)
            {
                object objUbicacion;
                String co_ubicacion = String.Empty;
                foreach (CitasBE oUbicacion in oUbicacionDisponible)
                {
                    if (fl_ubigeo_all == "1") { co_ubicacion = oUbicacion.coddpto + oUbicacion.codprov + oUbicacion.coddis + oUbicacion.nid_ubica.ToString(); }
                    else { co_ubicacion = oUbicacion.nid_ubica.ToString(); }
                    objUbicacion = new
                    {
                        value = co_ubicacion,
                        nombre = oUbicacion.no_ubica,
                        coddpto = oUbicacion.coddpto,
                        codprov = oUbicacion.codprov,
                        coddist = oUbicacion.coddis
                    };
                    oComboUbicacion.Add(objUbicacion);
                }
            }
            else
            {
                msg_retorno = "No hay talleres disponibles para este servicio y modelo.";
                fl_seguir = "0";
            }
            #endregion - Obtiene Ubicacion Disponible
            if (fl_seguir != "0")
            {
                #region - Obtiene Taller Disponible
                oTallerBE = new CitasBE();
                oTallerBE.nid_Servicio = nid_servicio;
                oTallerBE.nid_modelo = nid_modelo;
                oTallerBE.coddpto = coddpto;
                oTallerBE.codprov = codprov;
                oTallerBE.coddis = coddist;
                oTallerBE.nid_ubica = 0; //para que liste todos los talleres
                oTallerBE.nid_usuario = nid_usuario;
                CitasBEList oTallerDisponible = oTallerBL.Listar_Talleres(oTallerBE);
                if (oTallerDisponible.Count > 0)
                {
                    object objTaller;
                    String co_taller = String.Empty;
                    foreach (CitasBE oTaller in oTallerDisponible)
                    {
                        if (fl_ubigeo_all == "1")
                        {
                            co_taller = oTaller.coddpto + oTaller.codprov + oTaller.coddis
                                + oTaller.nid_ubica.ToString()
                                + "$" + oTaller.nid_taller;
                        }
                        else { co_taller = oTaller.nid_taller.ToString(); }
                        objTaller = new
                        {
                            value = co_taller,
                            nombre = oTaller.no_taller,
                            coddpto = oTaller.coddpto,
                            codprov = oTaller.codprov,
                            coddist = oTaller.coddis,
                            nid_ubica = oTaller.nid_ubica,
                            tx_mapa_taller = oTaller.tx_mapa_taller,
                            fl_taxi = oTaller.fl_taxi,
                            nid_taller = oTaller.nid_taller
                        };
                        oComboTaller.Add(objTaller);
                    }
                }
                else
                {
                    msg_retorno = "No hay talleres disponibles para este servicio y modelo.";
                    fl_seguir = "0";
                }
                #endregion - Obtiene Taller Disponible
            }

            strRetorno = new
            {
                fl_seguir = fl_seguir,
                msg_retorno = msg_retorno,
                oComboUbicacion = oComboUbicacion,
                oComboTaller = oComboTaller
            };
        }
        catch (Exception ex)
        {
            strRetorno = new
            {
                fl_seguir = "-1",
                msg_retorno = "Error: " + ex.Message,
                oComboUbicacion = oComboUbicacion,
                oComboTaller = oComboTaller
            };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_HorarioDisponible(String[] strParametros)
    {
        String msg_retorno = String.Empty;
        object strRetorno;
        String fl_seguir = "1";
        List<object> oHorarioDisponible = new List<object>();
        String sfe_min_reserva = "";
        String sfe_max_reserva = GetFechaMaxReserva();
        List<object> oComboHoraInicio = new List<object>();
        List<object> oComboHoraFinal = new List<object>();
        Int32 intIntervaloT = 0;
        Parametros oParametros = new Parametros();

        String tbl_Footable = String.Empty;

        List<object> Columns_data = new List<object>();
        String Header_Tbl = String.Empty;

        String fl_mostrar_calidad = "0";
        String fl_mostrar_promociones = "0";
        #region - Define Cabecera y Model Column
        ArrayList oHorario_Cabecera = new ArrayList();
        oHorario_Cabecera.Add(oParametros.N_Local);
        oHorario_Cabecera.Add(oParametros.N_Taller);
        //////oHorario_Cabecera.Add("Calidad Servicio");
        oHorario_Cabecera.Add("Dirección");
        //////oHorario_Cabecera.Add("Promociones y Noticias");
        oHorario_Cabecera.Add("Taller Disponible");

        Dictionary<string, object> oModelCol = null;
        ArrayList oHorario_ModelCol = new ArrayList();
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "no_ubica"); oModelCol.Add("index", "no_ubica"); oModelCol.Add("width", 130); oModelCol.Add("sortable", false);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "no_taller"); oModelCol.Add("index", "no_taller"); oModelCol.Add("width", 130); oModelCol.Add("sortable", false);
        oHorario_ModelCol.Add(oModelCol);

        //////oModelCol = new Dictionary<string, object>();
        //////oModelCol.Add("name", "calidad_servicio"); oModelCol.Add("index", "calidad_servicio"); oModelCol.Add("width", 100); oModelCol.Add("align", "center");
        //////if (fl_mostrar_calidad == "0")
        //////{
        //////    oModelCol.Add("hidden", true);
        //////}
        //////oHorario_ModelCol.Add(oModelCol);

        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "di_ubica"); oModelCol.Add("index", "di_ubica"); oModelCol.Add("width", 170); oModelCol.Add("sortable", false);
        oHorario_ModelCol.Add(oModelCol);

        //////oModelCol = new Dictionary<string, object>();
        //////oModelCol.Add("name", "promociones_noticias"); oModelCol.Add("index", "promociones_noticias"); oModelCol.Add("width", 100); oModelCol.Add("align", "center"); oModelCol.Add("sortable", false);
        //////if (fl_mostrar_promociones == "0")
        //////{
        //////    oModelCol.Add("hidden", true);
        //////}
        //////oHorario_ModelCol.Add(oModelCol);

        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "fl_disponible"); oModelCol.Add("index", "fl_disponible"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        #endregion - Define Cabecera y Model Column
        try
        {
            Int32 nid_modelo; Int32.TryParse(strParametros[0], out nid_modelo);
            Int32 nid_servicio; Int32.TryParse(strParametros[1], out nid_servicio);
            String coddpto = strParametros[2] == "" ? "0" : strParametros[2];
            String codprov = strParametros[3] == "" ? "0" : strParametros[3];
            String coddist = strParametros[4] == "" ? "0" : strParametros[4];
            Int32 nid_ubica; Int32.TryParse(strParametros[5], out nid_ubica);
            Int32 nid_taller; Int32.TryParse(strParametros[6], out nid_taller);
            String sfe_reserva = strParametros[7];
            String sho_inicio_visible = strParametros[8];
            String sho_final_visible = strParametros[9];
            Int32 nid_usuario; Int32.TryParse(strParametros[10], out nid_usuario);

            sfe_min_reserva = GetFechaMinReserva(nid_usuario);
            
            #region - Obtiene Horario Disponible
            Int32 intPRM = 3;
            CitasBL oCitasBL = new CitasBL();
            CitasBE oCitasBE = new CitasBE();
            CitasBEList oCitasBEList = new CitasBEList();
            DateTime dtHITallerT = DateTime.MaxValue;
            DateTime dtHFTallerT = DateTime.MinValue;
            Int32 intPRM_13 = Convert.ToInt32(oParametros.GetValor(Parametros.PARM._13));

            //--------------------
            // 1 -> Departamento
            // 2 -> Provincia
            // 3 -> Distrito
            //--------------------
            //    1          2
            if (intPRM > intPRM_13)
            {
                CitasBEList lstTalleres_Disponibles = null;
                CitasBEList _lstTalleresHE = null;
                CitasBEList _lstAsesores_Disponibles = null;
                CitasBEList _lstCitas = null;
                #region - Obtiene Fecha mínima de reserva, Talleres disponibles, Talleres Horario Excepcional, Asesores disponibles y Citas
                oCitasBE.nid_Servicio = nid_servicio;
                oCitasBE.nid_modelo = nid_modelo;
                oCitasBE.coddpto = coddpto;
                oCitasBE.codprov = codprov;
                oCitasBE.coddis = coddist;
                oCitasBE.nid_ubica = nid_ubica;
                oCitasBE.nid_taller = nid_taller;
                oCitasBE.nid_usuario = nid_usuario;
                if ((string.IsNullOrEmpty(sfe_reserva)))
                {
                    sfe_reserva = SRC_FECHA_HABIL(nid_modelo, nid_servicio, coddpto, codprov, coddist, nid_ubica, nid_taller
                        , out lstTalleres_Disponibles, out _lstTalleresHE, out _lstAsesores_Disponibles, out _lstCitas
                        , nid_usuario).ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                }
                oCitasBE.fe_atencion = Convert.ToDateTime(sfe_reserva);
                oCitasBE.dd_atencion = getDiaSemana(oCitasBE.fe_atencion);
                if (lstTalleres_Disponibles == null)
                {
                    lstTalleres_Disponibles = oCitasBL.ListarTalleresDisponibles_PorFecha(oCitasBE);//   1-Listado todos Talleres                
                    _lstTalleresHE = oCitasBL.ListarHorarioExcepcional_Talleres(oCitasBE);// 2-Listado Horario Excepcionales
                    _lstAsesores_Disponibles = oCitasBL.ListarAsesoresDisponibles_PorFecha(oCitasBE);//  3-Listado Asesores Talleres
                    _lstCitas = oCitasBL.ListarCitasAsesores(oCitasBE);//                    4-Listado CitasAsesores Talleres
                }
                #endregion - Obtiene Fecha mínima de reserva, Talleres disponibles, Talleres Horario Excepcional, Asesores disponibles y Citas
                if (lstTalleres_Disponibles.Count == 0)
                {
                    fl_seguir = "0";
                    msg_retorno = "No hay Talleres disponibles para este día.";
                }
                else if (lstTalleres_Disponibles.Count == 1 && nid_taller > 0)
                { //Carga Asesores por Web Method
                    fl_seguir = "2";
                    goto Response;
                }
                else
                {
                    intIntervaloT = 0;
                    //Get Mayor Intervalo de Tiempo entre todos los Talleres 
                    //Get Hora_Inicio y Hora_Final entre todos los Talleres                    
                    foreach (CitasBE oTaller in lstTalleres_Disponibles)
                    {
                        if (Convert.ToDateTime(oTaller.ho_inicio_t) < dtHITallerT) dtHITallerT = Convert.ToDateTime(oTaller.ho_inicio_t);
                        if (Convert.ToDateTime(oTaller.ho_fin_t) > dtHFTallerT) dtHFTallerT = Convert.ToDateTime(oTaller.ho_fin_t);
                        if (Convert.ToInt32(oTaller.qt_intervalo_atenc) > intIntervaloT) intIntervaloT = Convert.ToInt32(oTaller.qt_intervalo_atenc);
                    }

                    if (String.IsNullOrEmpty(sho_inicio_visible))
                    {
                        oComboHoraInicio = cargarComboHorarioTaller(dtHITallerT, dtHFTallerT, intIntervaloT);
                        oComboHoraFinal = oComboHoraInicio;
                    }

                    #region "Generando CABECERA Y MODEL COLUMN de las horas"
                    Int32 qt_col_main = oHorario_Cabecera.Count;
                    DateTime _dtHITaller = dtHITallerT;
                    DateTime _dtHFTaller = dtHFTallerT;
                    string strHorasTaller = string.Empty;
                    //Agrega Columna de horas: RANGO DE HORAS                    
                    while (_dtHITaller < _dtHFTaller)
                    {
                        string strColumHora = Convert.ToDateTime(_dtHITaller).ToString("HHmm");
                        String no_columna = "HORA_" + strColumHora;

                        oHorario_Cabecera.Add(Convert.ToDateTime(_dtHITaller).ToString("HH:mm"));
                        oModelCol = new Dictionary<string, object>();
                        oModelCol.Add("name", no_columna);
                        oModelCol.Add("index", no_columna);
                        oModelCol.Add("width", 45);
                        oModelCol.Add("sortable", false);
                        oModelCol.Add("align", "center");
                        oModelCol.Add("hidden", false);
                        oHorario_ModelCol.Add(oModelCol);

                        strHorasTaller += Convert.ToDateTime(_dtHITaller).ToString("HH:mm") + "|";
                        _dtHITaller = _dtHITaller.AddMinutes(intIntervaloT);
                    }
                    if (strHorasTaller.Trim().Length > 0)
                        strHorasTaller = strHorasTaller.Substring(0, strHorasTaller.Length - 1);
                    #endregion "Generando CABECERA Y MODEL COLUMN de las horas"
                    
                    Int32 rowID = 1;
                    String imghoraVacia = String.Empty;
                    String imgHoraLibre = "<img id='{2}' idfoo='{2}' style='cursor:pointer;' title='Seleccionar' alt='' src='" + imgURL_HORA_LIBRE + "' onclick='javascript: fn_SetHoraTaller(&#39;{0}&#39;, &#39;{1}&#39;, &#39;{2}&#39;, &#39;{3}&#39;);' />";
                    String imgHoraReservada = "<img title='' alt='' src='" + imgURL_HORA_RESERVADA + "' />";
                    String imgHoraExcepcional = "<img title='' alt='' src='" + imgURL_HORA_EXCEPCIONAL + "' />";
                    String imgHoraColumna = String.Empty;
                    #region "Rellenando el grid con los Puntos de Red, Taller y Direcciones"
                    foreach (CitasBE oTaller in lstTalleres_Disponibles)
                    {
                        Dictionary<string, object> oHorario = new Dictionary<string, object>();
                        oHorario.Add("no_ubica", oTaller.no_ubica);
                        oHorario.Add("no_taller", oTaller.no_taller);
                        //////oHorario.Add("calidad_servicio", String.Format(imgValoracion, oTaller.co_valoracion));
                        oHorario.Add("di_ubica", oTaller.di_ubica);
                        //////oHorario.Add("promociones_noticias", String.Format(lnkPromocionesNoticias, oTaller.nid_taller.ToString()));
                        oHorario.Add("fl_disponible", String.Empty);
                        //Agrega Columna de horas: RANGO DE HORAS
                        _dtHITaller = dtHITallerT;
                        _dtHFTaller = dtHFTallerT;
                        while (_dtHITaller < _dtHFTaller)
                        {
                            string strColumHora = Convert.ToDateTime(_dtHITaller).ToString("HHmm");
                            String no_columna = "HORA_" + strColumHora;

                            oHorario.Add(no_columna, String.Empty);

                            _dtHITaller = _dtHITaller.AddMinutes(intIntervaloT);
                        }
                        oHorarioDisponible.Add(oHorario);
                        rowID++;
                    }
                    #endregion "Rellenando el grid con los Puntos de Red, Taller y Direcciones"
                    #region "Set Horarios de cada taller"
                    // Horarios de cada taller
                    String strHorarioT = string.Empty;
                    Int32 intNoAsesores = 0;
                    Int32 nid_taller_aux; Int32 intervalo_taller;
                    DateTime dtHITaller; DateTime dtHFTaller;
                    Dictionary<string, object> oTallerHorario;
                    Int32 intFila = 0;
                    rowID = 1;
                    foreach (CitasBE oTaller in lstTalleres_Disponibles)
                    {
                        nid_taller_aux = int.Parse(oTaller.nid_taller.ToString());
                        dtHITaller = Convert.ToDateTime(oTaller.ho_inicio_t);
                        dtHFTaller = Convert.ToDateTime(oTaller.ho_fin_t);
                        intervalo_taller = int.Parse(oTaller.qt_intervalo_atenc);

                        oTallerHorario = new Dictionary<string, object>();
                        oTallerHorario = ((Dictionary<string, object>)(oHorarioDisponible[intFila]));

                        String fl_disponible = "1";
                        //Identifica si el taller está disponible y con cupos
                        if (oTaller.nid_dia_exceptuado_t == 1) //Validaciones Fecha Exceptuada - Taller
                            fl_disponible = "0";
                        else if (oTaller.qt_cantidad_t <= 0) //Validaciones Capacidad Atencion - Taller
                            fl_disponible = "0";
                        else if (oParametros.SRC_CodPais == "1")//Validacion Capacidad Atencion - Taller y Modelo
                            if (oTaller.qt_cantidad_m <= 0)
                                fl_disponible = "0";

                        //Verifica que tenga asesores disponibles
                        List<CitasBE> lstAsesoresTaller = _lstAsesores_Disponibles.FindAll(ase => ase.nid_taller == nid_taller_aux
                            && ase.nid_dia_exceptuado_a == 0 //Validacion Fecha Exceptuada - Asesor
                            && ase.qt_cantidad_a > 0); //Validacion Capacidad Atencion - Asesor

                        if (lstAsesoresTaller.Count == 0) //No hay Asesores disponibles para este Taller
                        {
                            intNoAsesores += 1;
                            _dtHITaller = dtHITaller;
                            _dtHFTaller = dtHFTaller;
                            while (_dtHITaller <= _dtHFTaller)
                            {
                                strHorarioT += "0" + "-";
                                _dtHITaller = _dtHITaller.AddMinutes(intervalo_taller);
                            }
                            strHorarioT = strHorarioT.Substring(0, strHorarioT.Length - 1);
                            strHorarioT += "|";
                            fl_disponible = "0";
                        }

                        //Verifica si tiene Hora Excepcional del Taller
                        List<CitasBE> lstTalleresHE = _lstTalleresHE.FindAll(tllr => tllr.nid_taller == nid_taller_aux);
                        String strTotalHE = string.Empty;
                        foreach (CitasBE oHET in lstTalleresHE)
                        {
                            if (!string.IsNullOrEmpty(oHET.ho_rango1)) strTotalHE += oHET.ho_rango1 + "-";
                            if (!string.IsNullOrEmpty(oHET.ho_rango2)) strTotalHE += oHET.ho_rango2 + "-";
                            if (!string.IsNullOrEmpty(oHET.ho_rango3)) strTotalHE += oHET.ho_rango3 + "-";
                        }
                        if (!string.IsNullOrEmpty(strTotalHE))
                            strTotalHE = strTotalHE.Substring(0, strTotalHE.Length - 1);

                        //Asesores disponibles
                        String strHorarioA = String.Empty;
                        String[] strTemp;
                        foreach (CitasBE oAsesor in lstAsesoresTaller)
                        {
                            //SET HORARIO DEL ASESOR
                            _dtHITaller = dtHITaller;
                            _dtHFTaller = dtHFTaller;
                            strHorarioA = string.Empty;
                            while (_dtHITaller <= _dtHFTaller)
                            {
                                strHorarioA += "2" + "-"; //Para identificar Horario Vacio (por defecto)
                                _dtHITaller = _dtHITaller.AddMinutes(intervalo_taller);
                            }
                            //=================================================================================
                            //> Listar Citas Asesores 
                            //=================================================================================
                            // FILTRAR CITAS - TALLER - ASESOR
                            //-----------------------------------
                            DateTime ho_inicio_asesor; DateTime ho_final_asesor;
                            Int32 intCantRango = 0;
                            Int32 intCol;
                            foreach (string strRangoHA in oAsesor.horario_asesor.Split('|'))
                            {
                                _dtHITaller = dtHITaller;
                                _dtHFTaller = dtHFTaller;
                                intCol = 0;
                                ho_inicio_asesor = Convert.ToDateTime(strRangoHA.Split('-').GetValue(0).ToString());
                                ho_final_asesor = Convert.ToDateTime(strRangoHA.Split('-').GetValue(1).ToString());
                                while (_dtHITaller <= _dtHFTaller)
                                {
                                    if (_dtHITaller >= ho_inicio_asesor && _dtHITaller < ho_final_asesor)
                                    { //Si Horario del asesor está dentro del horario del taller, setea en "1"
                                        strTemp = strHorarioA.Split('-');
                                        strTemp[intCol] = "1"; //Para identificar Horario Libre
                                        strHorarioA = string.Empty;
                                        foreach (string strT in strTemp)
                                        {
                                            if (strT.Equals(""))
                                                continue;
                                            strHorarioA += strT + "-";
                                        }
                                    }
                                    intCol += 1;
                                    _dtHITaller = _dtHITaller.AddMinutes(intervalo_taller);
                                }
                                intCantRango += 1;
                            }
                            if (strHorarioA.Length > 0)
                                strHorarioA = strHorarioA.Substring(0, strHorarioA.Length - 1);

                            //Verificamos si en el horario del taller existen citas
                            List<CitasBE> lstCitasAsesor = _lstCitas.FindAll(ci => ci.nid_taller == nid_taller_aux && ci.nid_asesor == oAsesor.nid_asesor);
                            DateTime dtHICita, dtHFCita;
                            foreach (CitasBE oCita in lstCitasAsesor)
                            {
                                dtHICita = Convert.ToDateTime(oCita.ho_inicio_c);
                                dtHFCita = Convert.ToDateTime(oCita.ho_fin_c);
                                while (dtHICita < dtHFCita)
                                {
                                    _dtHITaller = dtHITaller;
                                    _dtHFTaller = dtHFTaller;
                                    intCol = 0;
                                    while (_dtHITaller <= _dtHFTaller)
                                    {
                                        if (_dtHITaller >= dtHICita && dtHICita < _dtHFTaller)
                                        { //Si Cita del asesor está dentro del horario del taller, setea en "0"
                                            //>> Si hay cita reservada (ICONO DE RESERVADO)
                                            strTemp = strHorarioA.Split('-');
                                            strTemp[intCol] = "0";
                                            strHorarioA = string.Empty;
                                            foreach (string strT in strTemp)
                                                strHorarioA += strT + "-";
                                            if (strHorarioA.Length > 0)
                                                strHorarioA = strHorarioA.Substring(0, strHorarioA.Length - 1);
                                            break;
                                        }
                                        intCol += 1;
                                        _dtHITaller = _dtHITaller.AddMinutes(intervalo_taller);
                                    }
                                    dtHICita = dtHICita.AddMinutes(intervalo_taller);
                                }
                            }
                            strHorarioT += strHorarioA + "|";
                        }

                        String[] strRangos;
                        strRangos = strHorarioT.Split('|');
                        strHorarioT = strHorarioA = string.Empty;

                        String[] strInd;
                        Int32 oCol, oColT;
                        foreach (string strRango in strRangos)
                        {
                            if (strRango.Equals(""))
                                continue;
                            strInd = strRango.Split('-');
                            oCol = 0; oColT = 0;
                            _dtHITaller = dtHITaller;
                            _dtHFTaller = dtHFTaller;
                            while (_dtHITaller < _dtHFTaller)
                            {
                                imgHoraLibre = "<img id='{2}' idfoo='{2}' style='cursor:pointer;' title='Seleccionar' alt='' src='" + imgURL_HORA_LIBRE + "' onclick='javascript: fn_SetHoraTaller(&#39;{0}&#39;, &#39;{1}&#39;, &#39;{2}&#39;, &#39;{3}&#39;);' />";
                                imgHoraColumna = string.Empty;
                                switch (strInd.GetValue(oCol).ToString())
                                {
                                    case "1": imgHoraColumna = (fl_disponible == "0") ? imgHoraReservada : imgHoraLibre; break;
                                    case "0": imgHoraColumna = imgHoraReservada; break;
                                    case "2": imgHoraColumna = imghoraVacia; break;
                                }
                                // SET HORARIO EXCEPCIONAL  ------------------------------------------------
                                DateTime dtHEITaller, dtHEFTaller;
                                if (!String.IsNullOrEmpty(strTotalHE))
                                {
                                    foreach (string _strRangoHE in strTotalHE.Split('-'))
                                    {
                                        dtHEITaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(0));
                                        dtHEFTaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(1));
                                        if (_dtHITaller >= dtHEITaller && _dtHITaller < dtHEFTaller) // Si es una hora excepcionl
                                        {
                                            imgHoraColumna = imgHoraExcepcional; // Icon: Horario Excepcional
                                            break;
                                        }
                                    }
                                }
                                oColT = 0;
                                DateTime _dtHITallerT, _dtHFTallerT;
                                _dtHITallerT = dtHITallerT;
                                _dtHFTallerT = dtHFTallerT;
                                while ((_dtHITallerT < _dtHFTallerT)) //Horas de la grilla
                                {
                                    if ((_dtHITaller >= _dtHITallerT) & (_dtHITaller < (_dtHITallerT.AddMinutes(intIntervaloT))))
                                    { //Si hora del taller está dentro de la hora de la grilla
                                        String no_columna = "HORA_" + _dtHITallerT.ToString("HHmm");

                                        if (imgHoraColumna == imgHoraLibre)
                                        {
                                            String keys = String.Empty;
                                            if (fl_ubigeo_all == "1")
                                            {
                                                keys = String.Format("{0}|{1}|{2}|{3}|{4}|{5}", oTaller.coddpto
                                                    , oTaller.coddpto + oTaller.codprov
                                                    , oTaller.coddpto + oTaller.codprov + oTaller.coddis
                                                    , oTaller.coddpto + oTaller.codprov + oTaller.coddis + oTaller.nid_ubica
                                                    , oTaller.coddpto + oTaller.codprov + oTaller.coddis + oTaller.nid_ubica + "$" + oTaller.nid_taller
                                                    , oTaller.qt_intervalo_atenc);
                                            }
                                            else
                                            {
                                                keys = String.Format("{0}|{1}|{2}|{3}|{4}|{5}", oTaller.coddpto, oTaller.codprov, oTaller.coddis, oTaller.nid_ubica, oTaller.nid_taller, oTaller.qt_intervalo_atenc);
                                            }

                                            imgHoraLibre = String.Format(imgHoraLibre, rowID.ToString(), no_columna, no_columna + "_" + rowID.ToString()
                                                , keys);
                                            imgHoraColumna = imgHoraLibre;
                                        }

                                        if (!oTallerHorario[no_columna].ToString().Contains("disponible"))
                                        { //Si en el horario ya existe un asesor disponible, se deja libre para el taller
                                            oTallerHorario[no_columna] = imgHoraColumna;
                                        }
                                    }
                                    oColT += 1;
                                    _dtHITallerT = _dtHITallerT.AddMinutes(intIntervaloT);
                                }
                                oCol += 1;
                                _dtHITaller = _dtHITaller.AddMinutes(intervalo_taller);
                            }
                        }

                        //----
                        oTallerHorario["fl_disponible"] = fl_disponible;
                        //----

                        intFila += 1;
                        rowID++;
                    }
                    #endregion "Set Horarios de cada taller"
                    if (intNoAsesores == lstTalleres_Disponibles.Count)
                    {
                        oHorario_Cabecera = null;
                        oHorario_ModelCol = null;
                        oHorarioDisponible = null;

                        fl_seguir = "0";
                        msg_retorno = oParametros.msgNoAsesores + " " + sfe_reserva;
                    }
                    else
                    {
                        #region "Quitar/Ocultar columnas en blanco"
                        //---------------------------------------------> REMOVE: HORARIO BLANCO
                        Int32 cont_sin_horario;
                        //Oculta horarios vacíos de la derecha
                        oHorario_ModelCol.Reverse();
                        foreach (Dictionary<string, object> obj_ModelCol in oHorario_ModelCol)
                        {
                            String no_columna = obj_ModelCol["name"].ToString();
                            if ((no_columna.Length == 5 && no_columna.Substring(2, 1) == ":") || no_columna.Substring(0, 5) == "HORA_") //Para las horas
                            {
                                cont_sin_horario = 0;
                                foreach (Dictionary<string, object> oHorario in oHorarioDisponible)
                                {
                                    if (oHorario[no_columna].ToString() == String.Empty)
                                        cont_sin_horario++;
                                }
                                if (cont_sin_horario == oHorarioDisponible.Count)
                                    obj_ModelCol["hidden"] = true;
                                else
                                    break;
                            }
                        }
                        oHorario_ModelCol.Reverse();
                        //Oculta horarios vacíos de la izquierda
                        foreach (Dictionary<string, object> obj_ModelCol in oHorario_ModelCol)
                        {
                            String no_columna = obj_ModelCol["name"].ToString();
                            if ((no_columna.Length == 5 && no_columna.Substring(2, 1) == ":") || no_columna.Substring(0, 5) == "HORA_") //Para las horas
                            {
                                cont_sin_horario = 0;
                                foreach (Dictionary<string, object> oHorario in oHorarioDisponible)
                                {
                                    if (oHorario[no_columna].ToString() == String.Empty)
                                        cont_sin_horario++;
                                }
                                if (cont_sin_horario == oHorarioDisponible.Count)
                                    obj_ModelCol["hidden"] = true;
                                else
                                    break;
                            }
                        }
                        #endregion "Quitar/Ocultar columnas en blanco"
                        #region "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"
                        DateTime dt_ho_inicio_visible = DateTime.MinValue, dt_ho_final_visible = DateTime.MaxValue;
                        if (!String.IsNullOrEmpty(sho_inicio_visible))
                        {
                            dt_ho_inicio_visible = Convert.ToDateTime(sho_inicio_visible);
                            dt_ho_final_visible = Convert.ToDateTime(sho_final_visible);
                        }
                        DateTime dt_hora_grilla;
                        #endregion "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"
                        #region "- Crea HTML Tabla Footable"
                        tbl_Footable = "<table id='grvUbicacion' class='footable' data-toggle-column='last' cellspacing='0' width='100%'><thead><tr>";
                        int intCol = 0;
                        Int32 intCol_Visible = 0;
                        String style_FontSize = String.Empty;

                        Int32 nu_column_max_visible_phone = 0;
                        if (fl_mostrar_calidad == "1" && fl_mostrar_promociones == "1")
                        {
                            nu_column_max_visible_phone = 5;
                        }
                        else if (fl_mostrar_calidad == "0" && fl_mostrar_promociones == "0")
                        {
                            nu_column_max_visible_phone = 9;
                        }
                        else //Si al menos se ve una de las 2 columnas (calidad o promociones)
                        {
                            nu_column_max_visible_phone = 4;
                        }

                        foreach (String cab in oHorario_Cabecera)
                        {
                            Dictionary<string, object> obj_ModelCol = (Dictionary<string, object>)oHorario_ModelCol[intCol];

                            #region "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"
                            if (!String.IsNullOrEmpty(sho_inicio_visible))
                            {
                                if (cab.Length == 5 && cab.Substring(2, 1) == ":") //Horas
                                {
                                    dt_hora_grilla = Convert.ToDateTime(cab);
                                    if (dt_hora_grilla < dt_ho_inicio_visible || dt_hora_grilla > dt_ho_final_visible)
                                    {
                                        obj_ModelCol["hidden"] = true;
                                    }
                                }
                            }
                            #endregion "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"

                            if ((!obj_ModelCol.ContainsKey("hidden")) || (Convert.ToBoolean(obj_ModelCol["hidden"]) != true))
                            {
                                //if (cab.Length == 5 && cab.Substring(2, 1) == ":")
                                if (cab.Substring(2, 1) == ":")
                                    style_FontSize = " style='font-size:12px;padding:2px;'";
                                else
                                    style_FontSize = String.Empty;

                                if (intCol_Visible == 0) { tbl_Footable += String.Format("<th{1}>{0}</th>", cab, style_FontSize); }
                                else if (intCol_Visible == 1) { tbl_Footable += String.Format("<th>{0}</th>", cab); }
                                else if (intCol_Visible < nu_column_max_visible_phone) { tbl_Footable += String.Format("<th data-hide='phone'{1}>{0}</th>", cab, style_FontSize); }
                                else { tbl_Footable += String.Format("<th data-hide='phone,tablet'{1}>{0}</th>", cab, style_FontSize); }

                                intCol_Visible++;
                            }
                            intCol = intCol + 1;
                        }
                        tbl_Footable += "</tr></thead><tbody>";
                        String color_NoDisponible = "#B2D5F7";
                        foreach (Dictionary<string, object> oHorario in oHorarioDisponible)
                        {
                            if (oHorario["fl_disponible"].ToString() == "1")
                                tbl_Footable += "<tr>";
                            else
                                tbl_Footable += String.Format("<tr style='background-color:{0}';>", color_NoDisponible);

                            intCol = 0;
                            foreach (KeyValuePair<string, object> hor in oHorario)
                            {
                                Dictionary<string, object> obj_ModelCol = (Dictionary<string, object>)oHorario_ModelCol[intCol];
                                if ((!obj_ModelCol.ContainsKey("hidden")) || (Convert.ToBoolean(obj_ModelCol["hidden"]) != true))
                                {
                                    if (hor.Key.Substring(0, 5) == "HORA_") //Para las horas
                                        tbl_Footable += String.Format("<td style='text-align:center;'>{0}</td>", hor.Value);
                                    else
                                        tbl_Footable += String.Format("<td>{0}</td>", hor.Value);
                                }
                                intCol = intCol + 1;
                            }
                            tbl_Footable += "</tr>";
                        }
                        tbl_Footable += "</tbody></table>";
                        #endregion "- Crea HTML Tabla Footable"
                    }
                }
            }
        #endregion - Obtiene Horario Disponible

        Response:
            strRetorno = new
            {
                fl_seguir = fl_seguir,
                msg_retorno = msg_retorno,
                sfe_reserva = sfe_reserva,
                sfe_min_reserva = sfe_min_reserva,
                sfe_max_reserva = sfe_max_reserva,
                oHorario_Cabecera = oHorario_Cabecera,
                oHorario_ModelCol = oHorario_ModelCol,
                oHorarioDisponible = oHorarioDisponible,
                oComboHoraInicio = oComboHoraInicio,
                oComboHoraFinal = oComboHoraFinal,
                IntervaloT = intIntervaloT,
                Header_Tbl = Header_Tbl, //Para DataTables
                Columns_data = Columns_data, //Para DataTables
                tbl_Footable = tbl_Footable //Para FooTable
            };
        }
        catch (Exception ex)
        {
            strRetorno = new
            {
                fl_seguir = "-1",
                msg_retorno = "Error: " + ex.Message,
                sfe_reserva = String.Empty,
                sfe_min_reserva = String.Empty,
                sfe_max_reserva = String.Empty,
                oHorario_Cabecera = oHorario_Cabecera,
                oHorario_ModelCol = oHorario_ModelCol,
                oHorarioDisponible = oHorarioDisponible,
                oComboHoraInicio = oComboHoraInicio,
                oComboHoraFinal = oComboHoraFinal,
                Header_Tbl = Header_Tbl,
                Columns_data = Columns_data
            };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }

    private static DateTime SRC_FECHA_HABIL(Int32 nid_modelo, Int32 nid_servicio, String coddpto, String codprov, String coddist, Int32 nid_ubica, Int32 nid_taller
        , out CitasBEList lstTalleres, out CitasBEList _lstTalleresHE, out CitasBEList _lstAsesores, out CitasBEList _lstCitas
        , Int32 nid_usuario)
    {
        lstTalleres = _lstTalleresHE = _lstAsesores = _lstCitas = null;

        Parametros oParametros = new Parametros();
        CitasBL oCitasBL = new CitasBL();
        CitasBE oCitasBE = new CitasBE();
        Int32 _ID_TALLER = nid_taller;
        Int32 _INTERVALO = 0;
        Int32 _ID_SERVICIO = nid_servicio;
        Int32 _ID_MODELO = nid_modelo;
        string _TALLER = string.Empty;
        DateTime dFechaIni = Convert.ToDateTime(GetFechaMinReserva(nid_usuario));
        DateTime dFechaFin = Convert.ToDateTime(GetFechaMaxReserva());
        DateTime dHoraIni_T, dHoraFin_T, _dHoraIni_T, _dHoraFin_T, dHoraIni_A, dHoraFin_A;
        dHoraIni_T = dHoraFin_T = _dHoraIni_T = _dHoraFin_T = dHoraIni_A = dHoraFin_A = DateTime.MinValue;
        CitasBEList lstAsesores, lstCitas, lstTalleresHE;
        string strTotalHE = string.Empty;
        DateTime dtHEITaller, dtHEFTaller;
        DateTime dtHoraI_ddl = DateTime.MaxValue;
        DateTime dtHoraF_ddl = DateTime.MinValue;
        Int32 hay1 = 0, hay2 = 0;
        try
        {
            //Recorremos Fecha por Fecha 
            while (dFechaIni <= dFechaFin)
            {
                oCitasBE.nid_Servicio = _ID_SERVICIO;
                oCitasBE.nid_modelo = _ID_MODELO;
                oCitasBE.coddpto = coddpto;
                oCitasBE.codprov = codprov;
                oCitasBE.coddis = coddist;
                oCitasBE.nid_ubica = nid_ubica;
                oCitasBE.nid_taller = _ID_TALLER;
                oCitasBE.fe_atencion = dFechaIni;
                oCitasBE.dd_atencion = getDiaSemana(dFechaIni);
                oCitasBE.nid_usuario = nid_usuario;
                lstTalleres = oCitasBL.ListarTalleresDisponibles_PorFecha(oCitasBE);//   1-Listado todos Talleres
                if (lstTalleres.Count == 0)
                {
                    dFechaIni = dFechaIni.AddDays(+1);
                    continue;
                }
                _lstTalleresHE = oCitasBL.ListarHorarioExcepcional_Talleres(oCitasBE);// 2-Listado Horario Excepcionales
                _lstAsesores = oCitasBL.ListarAsesoresDisponibles_PorFecha(oCitasBE);//  3-Listado Asesores Talleres
                _lstCitas = oCitasBL.ListarCitasAsesores(oCitasBE);//                    4-Listado CitasAsesores Talleres
                //===========================================
                foreach (CitasBE oTaller in lstTalleres)
                {
                    string _NO_TALLER_ = oTaller.no_taller;
                    string _ID_TALLER_ = oTaller.nid_taller.ToString();
                    _INTERVALO = Convert.ToInt32(oTaller.qt_intervalo_atenc);
                    _dHoraIni_T = Convert.ToDateTime(oTaller.ho_inicio_t);
                    _dHoraFin_T = Convert.ToDateTime(oTaller.ho_fin_t);
                    //=================================================================================
                    //> Validaciones
                    //=================================================================================
                    if (oTaller.qt_cantidad_t <= 0) continue;//Capacidad Atencion Taller
                    if (oParametros.SRC_CodPais == "1")
                        if (oTaller.qt_cantidad_m <= 0)
                            continue;//Capacidad Atencion Taller y Modelo
                    if (oTaller.nid_dia_exceptuado_t == 1) continue;//Dia Exceptuado
                    //=================================================================================
                    //> Horas Excepcional del Taller
                    //=================================================================================
                    // 0 - FILTRAR HORARIO EXCEPCIONAL
                    //--------------------------------
                    lstTalleresHE = new CitasBEList();
                    hay1 = 0; hay2 = 0;
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
                    // 1 - Get Horario Excepcional
                    //---------------------------------
                    strTotalHE = string.Empty;
                    foreach (CitasBE oHET in lstTalleresHE)
                    {
                        if (!string.IsNullOrEmpty(oHET.ho_rango1)) strTotalHE += oHET.ho_rango1 + "-";
                        if (!string.IsNullOrEmpty(oHET.ho_rango2)) strTotalHE += oHET.ho_rango2 + "-";
                        if (!string.IsNullOrEmpty(oHET.ho_rango3)) strTotalHE += oHET.ho_rango3 + "-";
                    }
                    strTotalHE = !string.IsNullOrEmpty(strTotalHE) ? strTotalHE.Substring(0, strTotalHE.Length - 1) : string.Empty;
                    //=================================================================================
                    //> Listado de Asesores 
                    //=================================================================================
                    // 0 - FILTRAR ASESORES - TALLER
                    //-----------------------------------
                    lstAsesores = new CitasBEList();// 2
                    hay1 = 0; hay2 = 0;
                    foreach (CitasBE oEntidad in _lstAsesores)
                    {
                        hay1 = 0;
                        if (oTaller.nid_taller.Equals(oEntidad.nid_taller))
                        {
                            hay1 = 1; hay2 = 1;
                            if (oEntidad.nid_dia_exceptuado_a == 0) //Validar FechaExceptuada
                                if (oEntidad.qt_cantidad_a > 0) //Validar Capacidad de Atencion
                                    lstAsesores.Add(oEntidad);
                        }
                        if ((hay1 == 0) && (hay2 == 1))
                            break;
                    }
                    DateTime odHIA = _dHoraIni_T, odHFA = _dHoraFin_T;
                    foreach (CitasBE oAsesor in lstAsesores)//==========================>>>> Por cada Asesor
                    {
                        //=================================================================================
                        //> Listar Citas Asesores
                        //=================================================================================
                        // 0 - FILTRAR CITAS - TALLER - ASESOR
                        //------------------------------------
                        lstCitas = new CitasBEList();
                        hay1 = 0; hay2 = 0;
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
                        //Recorrer cada horario del Asesor
                        //-------------------------------------------------------------
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
                                        dtHEITaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(0));// Hora Inicial HET
                                        dtHEFTaller = Convert.ToDateTime(_strRangoHE.Split('|').GetValue(1));// Hora Final   HET
                                        //> Si es una hora excepcional
                                        if (dHoraIni_A >= dtHEITaller & dHoraIni_A < dtHEFTaller)
                                        {
                                            dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                                            goto Continue_While_2; //> Es Horario Excepcional
                                        }
                                    }
                                }
                                //> FECHA ENCONTRADA
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

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_HorarioDisponible_Asesor(String[] strParametros)
    {
        Parametros oParametros = new Parametros();
        String msg_retorno = String.Empty;
        object strRetorno;
        String fl_seguir = "1";
        List<object> oHorarioDisponible = new List<object>();
        List<object> oComboHoraInicio = new List<object>();
        List<object> oComboHoraFinal = new List<object>();
        Int32 intTCT = 0; // TCT -> Total Citas x Taller
        String tbl_Footable = String.Empty;

        #region - Define Cabecera y Model Column
        ArrayList oHorario_Cabecera = new ArrayList();
        oHorario_Cabecera.Add(oParametros.N_Taller);
        oHorario_Cabecera.Add("Asesor de Servicio");
        oHorario_Cabecera.Add("TCA");
        oHorario_Cabecera.Add("TCE");
        oHorario_Cabecera.Add("ID Asesor");
        oHorario_Cabecera.Add("Nom. Asesor");
        oHorario_Cabecera.Add("Teléfono");
        oHorario_Cabecera.Add("Email");
        oHorario_Cabecera.Add("Asesor Disponible");

        Dictionary<string, object> oModelCol = null;
        ArrayList oHorario_ModelCol = new ArrayList();
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "no_taller"); oModelCol.Add("index", "no_taller"); oModelCol.Add("width", 130); oModelCol.Add("sortable", false);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "no_asesor_servicio"); oModelCol.Add("index", "no_asesor_servicio"); oModelCol.Add("width", 130); oModelCol.Add("sortable", false);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "qt_tca"); oModelCol.Add("index", "qt_tca"); oModelCol.Add("width", 30); oModelCol.Add("sortable", false);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "qt_tce"); oModelCol.Add("index", "qt_tce"); oModelCol.Add("width", 30); oModelCol.Add("sortable", false);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "nid_asesor"); oModelCol.Add("index", "nid_asesor"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "no_asesor"); oModelCol.Add("index", "no_asesor"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "nu_telefono"); oModelCol.Add("index", "nu_telefono"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "no_correo"); oModelCol.Add("index", "no_correo"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "fl_disponible"); oModelCol.Add("index", "fl_disponible"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        #endregion - Define Cabecera y Model Column
        try
        {
            Int32 nid_modelo; Int32.TryParse(strParametros[0], out nid_modelo);
            Int32 nid_servicio; Int32.TryParse(strParametros[1], out nid_servicio);
            String coddpto = strParametros[2] == "" ? "0" : strParametros[2];
            String codprov = strParametros[3] == "" ? "0" : strParametros[3];
            String coddist = strParametros[4] == "" ? "0" : strParametros[4];
            Int32 nid_ubica; Int32.TryParse(strParametros[5], out nid_ubica);
            Int32 nid_taller; Int32.TryParse(strParametros[6], out nid_taller);
            String sfe_atencion = strParametros[7];
            String sho_inicio_preseleccion = strParametros[8];
            Int32 qt_intervalo_atenc_tllr; Int32.TryParse(strParametros[9], out qt_intervalo_atenc_tllr);
            Int32 qt_intervalo_global_taller; Int32.TryParse(strParametros[10], out qt_intervalo_global_taller);
            String sho_inicio_visible = strParametros[11];
            String sho_final_visible = strParametros[12];
            Int32 nid_usuario; Int32.TryParse(strParametros[13], out nid_usuario);

            DateTime ho_inicio_preseleccion = DateTime.Now, ho_final_preseleccion = DateTime.Now;
            if (!String.IsNullOrEmpty(sho_inicio_preseleccion))
            {
                sho_inicio_preseleccion = sho_inicio_preseleccion.Contains(":") ? sho_inicio_preseleccion : (sho_inicio_preseleccion.Insert(2, ":"));
                ho_inicio_preseleccion = Convert.ToDateTime(String.Format("{0} {1}", DateTime.Now.ToShortDateString(), sho_inicio_preseleccion));
                ho_final_preseleccion = ho_inicio_preseleccion.AddMinutes(qt_intervalo_global_taller);
            }

            if (nid_taller == 0)
            {
                fl_seguir = "0";
                msg_retorno = "- Debe seleccionar taller.";
                goto Response;
            }

            #region - Obtiene Horario Disponible
            CitasBE oCitasBE;

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

            Int32 _ID_TALLER = nid_taller;
            Int32 _INTERVALO = qt_intervalo_atenc_tllr;
            string _TALLER = string.Empty;
            string strTotalHE;// Acumulacion de Rangos de HE
            //Int32 hay2 = 0;

            #region - Obtiene Talleres disponibles, Talleres Horario Excepcional, Asesores disponibles y Citas de Asesores
            oCitasBE = new CitasBE();
            oCitasBE.nid_Servicio = nid_servicio;
            oCitasBE.nid_modelo = nid_modelo;
            oCitasBE.coddpto = coddpto;
            oCitasBE.codprov = codprov;
            oCitasBE.coddis = coddist;
            oCitasBE.nid_ubica = nid_ubica;
            oCitasBE.nid_taller = nid_taller;
            oCitasBE.fe_atencion = Convert.ToDateTime(sfe_atencion);
            oCitasBE.dd_atencion = getDiaSemana(oCitasBE.fe_atencion);
            oCitasBE.nid_usuario = nid_usuario;
            CitasBL oCitasBL = new CitasBL();
            CitasBEList _lstCitas, lstTalleres, lstAsesores, lstTalleresHE;
            lstTalleres = oCitasBL.ListarTalleresDisponibles_PorFecha(oCitasBE);//  1-Listado el Taller

            if (lstTalleres.Count == 0)
            {
                fl_seguir = "0";
                msg_retorno = "No hay Talleres disponibles para este día.";
                goto Response;
            }

            lstTalleresHE = oCitasBL.ListarHorarioExcepcional_Talleres(oCitasBE);// 2-Listado Horario Excepcionales
            lstAsesores = oCitasBL.ListarAsesoresDisponibles_PorFecha(oCitasBE);//  3-Listado Asesores - Taller
            _lstCitas = oCitasBL.ListarCitasAsesores(oCitasBE);//                   4-Listado CitasAsesores - Taller

            if (_INTERVALO == 0) _INTERVALO = Convert.ToInt32(lstTalleres[0].qt_intervalo_atenc); //Asigna intervalo del taller

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
                fl_seguir = "0";
                msg_retorno = oParametros.msgNoHorario2 + " " + sfe_atencion;
                goto Response;
            }
            if (lstAsesores.Count == 0)
            {
                fl_seguir = "0";
                msg_retorno = oParametros.msgNoAsesores + " " + sfe_atencion;
                goto Response;
            }
            #endregion - Obtiene Talleres disponibles, Talleres Horario Excepcional, Asesores disponibles y Citas de Asesores

            //=================================================================================
            //> Verifica Horas Excepcional del Taller
            //=================================================================================
            strTotalHE = string.Empty;
            foreach (CitasBE oHET in lstTalleresHE)
            {
                if (!string.IsNullOrEmpty(oHET.ho_rango1)) strTotalHE += oHET.ho_rango1 + "-";
                if (!string.IsNullOrEmpty(oHET.ho_rango2)) strTotalHE += oHET.ho_rango2 + "-";
                if (!string.IsNullOrEmpty(oHET.ho_rango3)) strTotalHE += oHET.ho_rango3 + "-";
            }
            if (!string.IsNullOrEmpty(strTotalHE))
                strTotalHE = strTotalHE.Substring(0, strTotalHE.Length - 1);

            #region "Generando CABECERA Y MODEL COLUMN de las horas"
            _dHoraIni_T = dHoraIni_T;
            _dHoraFin_T = dHoraFin_T;

            while ((_dHoraIni_T < _dHoraFin_T))
            {
                String no_columna = _dHoraIni_T.ToString(fl_format_24_horas ? "HH:mm" : "hh:mm").ToUpper().Replace(".", "");

                //Cabecera
                oHorario_Cabecera.Add(no_columna);
                //Model Column
                oModelCol = new Dictionary<string, object>();
                oModelCol.Add("name", no_columna); oModelCol.Add("index", no_columna); oModelCol.Add("width", 45);
                oModelCol.Add("sortable", false); oModelCol.Add("align", "center"); oModelCol.Add("hidden", false);
                oHorario_ModelCol.Add(oModelCol);

                _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
            }
            #endregion "Generando CABECERA Y MODEL COLUMN de las horas"
            string strPARM_10 = oParametros.GetValor(Parametros.PARM._10).ToString();
            #region "Rellenando el grid con los Puntos de Red, Taller y Direcciones"
            foreach (CitasBE oAsesor in lstAsesores)
            {
                Dictionary<string, object> oHorario = new Dictionary<string, object>();
                oHorario.Add("no_taller", _TALLER);
                oHorario.Add("no_asesor_servicio", (strPARM_10.Equals("0") ? "Asesor de Servicio - " + oAsesor.nid_asesor.ToString() : oAsesor.no_asesor));
                oHorario.Add("qt_tca", 0);
                oHorario.Add("qt_tce", 0);
                oHorario.Add("nid_asesor", oAsesor.nid_asesor);
                oHorario.Add("no_asesor", oAsesor.no_asesor.Trim());
                oHorario.Add("nu_telefono", oAsesor.nu_telefono_a.Trim());
                oHorario.Add("no_correo", oAsesor.no_correo_a.Trim());
                oHorario.Add("fl_disponible", String.Empty);

                //Agrega Columna de horas: RANGO DE HORAS
                _dHoraIni_T = dHoraIni_T;
                _dHoraFin_T = dHoraFin_T;
                while ((_dHoraIni_T < _dHoraFin_T))
                {
                    String no_columna = _dHoraIni_T.ToString(fl_format_24_horas ? "HH:mm" : "hh:mm").ToUpper().Replace(".", "");
                    oHorario.Add(no_columna, String.Empty);

                    _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
                }
                oHorarioDisponible.Add(oHorario);
            }
            #endregion "Rellenando el grid con los Puntos de Red, Taller y Direcciones"

            bool blNoDisponibleT = false;
            if (oTaller.nid_dia_exceptuado_t == 1) //Validaciones Fecha Exceptuada - Taller
                blNoDisponibleT = true;
            else if (oTaller.qt_cantidad_t <= 0) //Validaciones Capacidad Atencion - Taller
                blNoDisponibleT = true;
            else if (oParametros.SRC_CodPais == "1") //Validacion Capacidad Atencion - Taller y Modelo
                if (oTaller.qt_cantidad_m <= 0)
                    blNoDisponibleT = true;

            //------------------------------------------------------------------------
            // Colocamos los iconos de Horario Disponible, Reservado y Excepcional
            //------------------------------------------------------------------------            
            String imghoraVacia = String.Empty;
            String imgHoraLibre = "<img id='{2}' idfoo='{2}' style='cursor:pointer;' title='Seleccionar' alt='' src='" + imgURL_HORA_LIBRE + "' onclick='javascript: fn_SetHoraAsesor(&#39;{0}&#39;, &#39;{1}&#39;, &#39;{2}&#39;, &#39;{3}&#39;);' />";
            String imgHoraReservada_Vacio = "<img title='' alt='' src='" + imgURL_HORA_RESERVADA + "' />";
            String imgHoraReservada = "<img id='{2}' idfoo='{2}' style='cursor:pointer;' title='{4}' alt='' src='" + imgURL_HORA_RESERVADA + "' onclick='javascript: fn_SetHoraAsesor_CE(&#39;{0}&#39;, &#39;{1}&#39;, &#39;{2}&#39;, &#39;{3}&#39;);' />";
            String imgHoraExcepcional = "<img title='' alt='' src='" + imgURL_HORA_EXCEPCIONAL + "' />";
            String imgHoraColumna = String.Empty;

            #region "Set Horarios de cada Asesor"
            Int32 intFila = 0, intCol = 0;
            Int32 rowID = 1;
            foreach (CitasBE oAsesor in lstAsesores)
            {
                Dictionary<string, object> oHorario = new Dictionary<string, object>();
                oHorario = ((Dictionary<string, object>)(oHorarioDisponible[intFila]));

                String fl_disponible = "1";
                //Identifica si el Asesor está disponible y con cupos
                if (blNoDisponibleT) //Por Taller
                    fl_disponible = "0";
                else//Por Asesor
                {
                    if (oAsesor.nid_dia_exceptuado_a == 1) //Validacion Fecha Exceptuada - Asesor
                    {
                        fl_disponible = "0";
                    }
                    if (fl_disponible == "1")
                    {
                        if (oAsesor.qt_cantidad_a <= 0) //Validacion Capacidad Atencion - Asesor
                        {
                            fl_disponible = "0";
                        }
                    }
                }

                //-------------------------
                // SET HORARIO DEL ASESOR 
                //-------------------------
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
                            String no_columna = _dHoraIni_T.ToString(fl_format_24_horas ? "HH:mm" : "hh:mm").ToUpper().Replace(".", "");
                            imgHoraColumna = String.Empty;
                            if (fl_disponible == "0")
                            {
                                //////imgHoraColumna = imgHoraReservada;
                                imgHoraColumna = imgHoraReservada_Vacio;
                            }
                            else
                            {
                                String lblSeleccion = "Selección de Reserva -  " + ((oParametros.GetValor(Parametros.PARM._10).ToString().Equals("0")) ? oParametros.N_Asesor + " " + oAsesor.nid_asesor.ToString() : oAsesor.no_asesor) + " - " + GetFechaLarga(Convert.ToDateTime(sfe_atencion)) + " - " + FormatoHora(no_columna);
                                String ho_inicio_a = no_columna;
                                imgHoraColumna = String.Format(imgHoraLibre, rowID.ToString(), no_columna, no_columna + "_" + rowID.ToString()
                                    , String.Format("{0}${1}${2}${3}", oAsesor.nid_asesor, ho_inicio_a, _INTERVALO, lblSeleccion));
                            }
                            if (oHorario.ContainsKey(no_columna))
                            {
                                oHorario[no_columna] = imgHoraColumna;
                            }
                        }
                        intCol += 1;
                        _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
                    }
                }
                oHorario["fl_disponible"] = fl_disponible;
                //-------------------------
                // SET CITAS 
                //------------------------------------------------------                   
                // Filtrar Citas por Asesor
                //------------------------------------------------------
                Int32 intTCA = 0; // TCA -> Total Citas x Asesor
                Int32 intTCE = 0; // TCE -> Total Citas en Cola espera
                intTCT = 0; // TCT -> Total Citas x Taller
                Int32 intSW = 0;//SW 
                Int32 intCantCE = 0;

                List<CitasBE> lstCitas = _lstCitas.FindAll(ci => ci.nid_asesor == oAsesor.nid_asesor);
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
                                String no_columna = _dHoraIni_T.ToString(fl_format_24_horas ? "HH:mm" : "hh:mm").ToUpper().Replace(".", "");
                                imgHoraColumna = String.Empty;

                                //>> Si hay cita reservada (ICONO DE RESERVADO)
                                //////imgHoraColumna = imgHoraReservada;
                                String lblSeleccion = "Selección de Reserva -  " + ((oParametros.GetValor(Parametros.PARM._10).ToString().Equals("0")) ? oParametros.N_Asesor + " " + oAsesor.nid_asesor.ToString() : oAsesor.no_asesor) + " - " + GetFechaLarga(Convert.ToDateTime(sfe_atencion)) + " - " + FormatoHora(no_columna);
                                String ho_inicio_a = no_columna;
                                imgHoraColumna = String.Format(imgHoraReservada, rowID.ToString(), no_columna, no_columna + "_" + rowID.ToString()
                                    , String.Format("{0}${1}${2}${3}", oAsesor.nid_asesor, ho_inicio_a, _INTERVALO, lblSeleccion)
                                    , (intCantCE > 0) ? intCantCE.ToString() : "Seleccionar"
                                    );

                                oHorario[no_columna] = imgHoraColumna;
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
                oHorario["qt_tca"] = intTCA.ToString(); // TCA
                oHorario["qt_tce"] = intTCE.ToString(); // TCE 

                //--------------------------
                // SET HORARIO EXCEPCIONAL  
                //--------------------------
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
                            if (_dHoraIni_T >= dHoraIni_E && _dHoraIni_T < dHoraFin_E) // Si es una hora excepcionl
                            {
                                String no_columna = _dHoraIni_T.ToString(fl_format_24_horas ? "HH:mm" : "hh:mm").ToUpper().Replace(".", "");
                                imgHoraColumna = String.Empty;
                                imgHoraColumna = imgHoraExcepcional;
                                oHorario[no_columna] = imgHoraColumna;
                            }
                            intCol += 1;
                            _dHoraIni_T = _dHoraIni_T.AddMinutes(_INTERVALO);
                        }
                    }
                }
                intFila += 1;
                rowID++;
            }
            #endregion "Set Horarios de cada Asesor"

            if (String.IsNullOrEmpty(sho_inicio_visible))
            {
                oComboHoraInicio = cargarComboHorarioTaller(dHoraIni_T, dHoraFin_T, _INTERVALO);
                oComboHoraFinal = oComboHoraInicio;
            }

            #region "Quitar/Ocultar columnas en blanco"
            //---------------------------------------------> REMOVE: HORARIO BLANCO
            Int32 cont_sin_horario;
            //Oculta horarios vacíos de la derecha
            oHorario_ModelCol.Reverse();
            foreach (Dictionary<string, object> obj_ModelCol in oHorario_ModelCol)
            {
                String no_columna = obj_ModelCol["name"].ToString();
                if (no_columna.Length == 5 && no_columna.Substring(2, 1) == ":") //Para las horas
                {
                    cont_sin_horario = 0;
                    foreach (Dictionary<string, object> oHorario in oHorarioDisponible)
                    {
                        if (oHorario[no_columna].ToString() == String.Empty)
                            cont_sin_horario++;
                    }
                    if (cont_sin_horario == oHorarioDisponible.Count)
                        obj_ModelCol["hidden"] = true;
                    else
                        break;
                }
            }
            oHorario_ModelCol.Reverse();
            //Oculta horarios vacíos de la izquierda
            foreach (Dictionary<string, object> obj_ModelCol in oHorario_ModelCol)
            {
                String no_columna = obj_ModelCol["name"].ToString();
                if (no_columna.Length == 5 && no_columna.Substring(2, 1) == ":") //Para las horas
                {
                    cont_sin_horario = 0;
                    foreach (Dictionary<string, object> oHorario in oHorarioDisponible)
                    {
                        if (oHorario[no_columna].ToString() == String.Empty)
                            cont_sin_horario++;
                    }
                    if (cont_sin_horario == oHorarioDisponible.Count)
                        obj_ModelCol["hidden"] = true;
                    else
                        break;
                }
            }
            #endregion "Quitar/Ocultar columnas en blanco"

            #region "Pre-Seleccion de Hora"
            //----------------------
            //> SET PRE-SELECCION
            //----------------------
            String color_PreSeleccion = "#B2D5F7";
            if (oParametros.SRC_CodPais == "1")
            {
                if (!(string.IsNullOrEmpty(sho_inicio_preseleccion)))
                {
                    DateTime dtRangoHI = ho_inicio_preseleccion;
                    DateTime dtRangoHF = ho_final_preseleccion;
                    Int32 intRango = 0;
                    DateTime dtHoraC;
                    foreach (Dictionary<string, object> obj_ModelCol in oHorario_ModelCol)
                    {
                        String no_columna = obj_ModelCol["name"].ToString();
                        if (no_columna.Length == 5 && no_columna.Substring(2, 1) == ":") //Para las horas
                        {
                            dtHoraC = Convert.ToDateTime(no_columna);
                            if (dtHoraC >= dtRangoHI && dtHoraC < dtRangoHF)
                            {
                                obj_ModelCol["cellattr"] = "function(rowId, val, rowObject, cm, rdata){ return 'style=\"background-color: " + color_PreSeleccion + "\"';}";
                            }
                        }
                        intRango += 1;
                    }
                }
            }
            #endregion "Pre-Seleccion de Hora"

            #region "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"
            DateTime dt_ho_inicio_visible = DateTime.MinValue, dt_ho_final_visible = DateTime.MaxValue;
            if (!String.IsNullOrEmpty(sho_inicio_visible))
            {
                dt_ho_inicio_visible = Convert.ToDateTime(sho_inicio_visible);
                dt_ho_final_visible = Convert.ToDateTime(sho_final_visible);
            }
            DateTime dt_hora_grilla;
            #endregion "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"

            #region "- Crea HTML Tabla Footable"
            tbl_Footable = "<table id='grvUbicacion' class='footable' data-toggle-column='last' cellspacing='0' width='100%'><thead><tr>";
            intCol = 0;
            Int32 intCol_Visible = 0;
            String style_FontSize = String.Empty;
            foreach (String cab in oHorario_Cabecera)
            {
                Dictionary<string, object> obj_ModelCol = (Dictionary<string, object>)oHorario_ModelCol[intCol];

                #region "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"
                if (!String.IsNullOrEmpty(sho_inicio_visible))
                {
                    if (cab.Length == 5 && cab.Substring(2, 1) == ":") //Horas
                    {
                        dt_hora_grilla = Convert.ToDateTime(cab);
                        if (dt_hora_grilla < dt_ho_inicio_visible || dt_hora_grilla > dt_ho_final_visible)
                        {
                            obj_ModelCol["hidden"] = true;
                        }
                    }
                }
                #endregion "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"

                if ((!obj_ModelCol.ContainsKey("hidden")) || (Convert.ToBoolean(obj_ModelCol["hidden"]) != true))
                {
                    if (cab.Length == 5 || cab.Substring(2, 1) == ":")
                        style_FontSize = " style='font-size:12px;padding:2px;'";
                    else
                        style_FontSize = String.Empty;

                    if (intCol_Visible == 0) { tbl_Footable += String.Format("<th{1}>{0}</th>", cab, style_FontSize); }
                    else if (intCol_Visible == 1) { tbl_Footable += String.Format("<th>{0}</th>", cab); }
                    else if (intCol_Visible < 12) { tbl_Footable += String.Format("<th data-hide='phone'{1}>{0}</th>", cab, style_FontSize); }
                    else { tbl_Footable += String.Format("<th data-hide='phone,tablet'{1}>{0}</th>", cab, style_FontSize); }

                    intCol_Visible++;
                }
                intCol = intCol + 1;
            }
            tbl_Footable += "</tr></thead><tbody>";
            String color_NoDisponible = "#B2D5F7";
            foreach (Dictionary<string, object> oHorario in oHorarioDisponible)
            {
                if (oHorario["fl_disponible"].ToString() == "1")
                    tbl_Footable += "<tr>";
                else
                    tbl_Footable += String.Format("<tr style='background-color:{0}';>", color_NoDisponible);

                intCol = 0;
                foreach (KeyValuePair<string, object> hor in oHorario)
                {
                    Dictionary<string, object> obj_ModelCol = (Dictionary<string, object>)oHorario_ModelCol[intCol];
                    if ((!obj_ModelCol.ContainsKey("hidden")) || (Convert.ToBoolean(obj_ModelCol["hidden"]) != true))
                    {
                        if (hor.Key.Length == 5 && hor.Key.Substring(2, 1) == ":") //Para las horas
                        {
                            if (obj_ModelCol.ContainsKey("cellattr")) //Pre-Seleccion
                                tbl_Footable += String.Format("<td style='text-align:center;background-color:{1};'>{0}</td>", hor.Value, color_PreSeleccion);
                            else
                                tbl_Footable += String.Format("<td style='text-align:center;'>{0}</td>", hor.Value);
                        }
                        else
                            tbl_Footable += String.Format("<td>{0}</td>", hor.Value);
                    }
                    intCol = intCol + 1;
                }
                tbl_Footable += "</tr>";
            }
            tbl_Footable += "</tbody></table>";
            #endregion "- Crea HTML Tabla Footable"

            #endregion - Obtiene Horario Disponible

            Int32 sumTCA = 0;
            foreach (Dictionary<string, object> oHorario in oHorarioDisponible)
            {
                string qt_tcca = oHorario["qt_tca"].ToString();
                sumTCA += Convert.ToInt32(qt_tcca);
            }
            intTCT = sumTCA;

        Response:
            strRetorno = new
            {
                fl_seguir = fl_seguir,
                msg_retorno = msg_retorno,
                sfe_reserva = sfe_atencion,
                oHorario_Cabecera = oHorario_Cabecera,
                oHorario_ModelCol = oHorario_ModelCol,
                oHorarioDisponible = oHorarioDisponible,
                oComboHoraInicio = oComboHoraInicio,
                oComboHoraFinal = oComboHoraFinal,
                qt_TCT = intTCT,
                tbl_Footable = tbl_Footable //Para FooTable
            };
        }
        catch (Exception ex)
        {
            strRetorno = new
            {
                fl_seguir = "-1",
                msg_retorno = "Error: " + ex.Message,
                sfe_reserva = String.Empty,
                sfe_max_reserva = String.Empty,
                oHorario_Cabecera = oHorario_Cabecera,
                oHorario_ModelCol = oHorario_ModelCol,
                oHorarioDisponible = oHorarioDisponible,
                oComboHoraInicio = oComboHoraInicio,
                oComboHoraFinal = oComboHoraFinal,
                qt_TCT = intTCT,
                tbl_Footable = tbl_Footable //Para FooTable
            };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_ValidaColaEspera(String[] strParametros)
    {
        Int32 retorno = 0;
        String msg_retorno = "";
        object response;
        try
        {
            Int32 nid_servicio; Int32.TryParse(strParametros[0].ToString(), out nid_servicio);
            Int32 nid_taller; Int32.TryParse(strParametros[1].ToString(), out nid_taller);
            Int32 nid_asesor; Int32.TryParse(strParametros[2].ToString(), out nid_asesor);
            String sfecha = strParametros[3].ToString();
            String ho_inicio = strParametros[4].ToString();
            Int32 qt_intervalo_atenc; Int32.TryParse(strParametros[5].ToString(), out qt_intervalo_atenc);

            String ho_fin = Convert.ToDateTime(ho_inicio).AddMinutes(qt_intervalo_atenc).ToString("HH:mm");

            Parametros oParametros = new Parametros();
            CitasBL oCitasBL = new CitasBL();
            CitasBE oCitasBE = new CitasBE();
            oCitasBE.nid_Servicio = nid_servicio;
            oCitasBE.nid_taller = nid_taller;
            oCitasBE.nid_usuario = nid_asesor;
            oCitasBE.ho_inicio = ho_inicio;
            oCitasBE.ho_fin = ho_fin;
            oCitasBE.fe_prog = Convert.ToDateTime(sfecha);

            Int32 intColaAct = oCitasBL.GetCantidadColaEspera(oCitasBE);
            Int32 intColaMax = Convert.ToInt32(oParametros.GetValor(Parametros.PARM._07));

            if (intColaAct >= intColaMax)
            {
                retorno = -2;
                msg_retorno = ("Se ha alcanzado el límite de clientes en cola de espera (" + intColaMax.ToString() + "), porfavor escoger otro horario.");
            }
            else
            {
                retorno = 1;
                msg_retorno = "";
            }
        }
        catch (Exception ex)
        {
            retorno = -1;
            msg_retorno = ex.Message;
        }

        response = new
        {
            retorno = retorno,
            msg_retorno = msg_retorno
        };

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(response);
    }

    #region "Próximos Turnos"
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_HorarioDisponible_ProxTurnos(String[] strParametros)
    {
        String msg_retorno = String.Empty;
        object strRetorno;
        String fl_seguir = "1";
        String tbl_Footable = String.Empty;
        List<Dictionary<string, object>> oHorarioDisponible = new List<Dictionary<string, object>>();
        List<object> oComboHoraInicio = new List<object>();
        List<object> oComboHoraFinal = new List<object>();
        Parametros oParametros = new Parametros();

        #region - Define Cabecera y Model Column
        ArrayList oHorario_Cabecera = new ArrayList();
        oHorario_Cabecera.Add(oParametros.N_Local);
        oHorario_Cabecera.Add(oParametros.N_Taller);
        oHorario_Cabecera.Add("Fecha");
        oHorario_Cabecera.Add("Hora");
        oHorario_Cabecera.Add("Asesor Servicio");
        oHorario_Cabecera.Add("Quick Service");
        oHorario_Cabecera.Add("Seleccionar");
        oHorario_Cabecera.Add("ID Asesor");
        oHorario_Cabecera.Add("ID Taller");
        oHorario_Cabecera.Add("Teléfono");
        oHorario_Cabecera.Add("Email");
        oHorario_Cabecera.Add("Taller/Asesor Disponible");

        Dictionary<string, object> oModelCol = null;
        ArrayList oHorario_ModelCol = new ArrayList();
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "no_ubica"); oModelCol.Add("index", "no_ubica"); oModelCol.Add("width", 180); oModelCol.Add("sortable", false);
        oModelCol.Add("search", false);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "no_taller"); oModelCol.Add("index", "no_taller"); oModelCol.Add("width", 150); oModelCol.Add("sortable", false); oModelCol.Add("align", "center");
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "fe_cita"); oModelCol.Add("index", "fe_cita"); oModelCol.Add("width", 90); oModelCol.Add("sortable", false); oModelCol.Add("align", "center");
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "ho_cita"); oModelCol.Add("index", "ho_cita"); oModelCol.Add("width", 70); oModelCol.Add("sortable", false); oModelCol.Add("align", "center");
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "no_asesor"); oModelCol.Add("index", "no_asesor"); oModelCol.Add("width", 200); oModelCol.Add("sortable", false);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "fl_quick_service"); oModelCol.Add("index", "fl_quick_service"); oModelCol.Add("width", 70); oModelCol.Add("sortable", false); oModelCol.Add("align", "center");
        oModelCol.Add("search", false);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "img_seleccionar"); oModelCol.Add("index", "img_seleccionar"); oModelCol.Add("width", 90); oModelCol.Add("sortable", false); oModelCol.Add("align", "center");
        oModelCol.Add("search", false);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "nid_asesor"); oModelCol.Add("index", "nid_asesor"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "nid_taller"); oModelCol.Add("index", "nid_taller"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "nu_telefono"); oModelCol.Add("index", "nu_telefono"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "no_email"); oModelCol.Add("index", "no_email"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        oModelCol = new Dictionary<string, object>();
        oModelCol.Add("name", "fl_disponible"); oModelCol.Add("index", "fl_disponible"); oModelCol.Add("hidden", true);
        oHorario_ModelCol.Add(oModelCol);
        #endregion - Define Cabecera y Model Column
        List<string> lstFiltroTaller = new List<string>(); lstFiltroTaller.Add(":");
        List<string> lstFiltroFecha = new List<string>(); lstFiltroFecha.Add(":");
        List<string> lstFiltroHora = new List<string>(); lstFiltroHora.Add(":");
        List<string> lstFiltroAsesor = new List<string>(); lstFiltroAsesor.Add(":");
        try
        {
            Int32 nid_modelo; Int32.TryParse(strParametros[0], out nid_modelo);
            Int32 nid_servicio; Int32.TryParse(strParametros[1], out nid_servicio);
            String coddpto = strParametros[2] == "" ? "0" : strParametros[2];
            String codprov = strParametros[3] == "" ? "0" : strParametros[3];
            String coddist = strParametros[4] == "" ? "0" : strParametros[4];
            Int32 nid_ubica; Int32.TryParse(strParametros[5], out nid_ubica);
            Int32 nid_taller; Int32.TryParse(strParametros[6], out nid_taller);
            String sfe_reserva_desde = strParametros[7];
            String sfe_reserva_hasta = strParametros[8];
            String sho_inicio_visible = strParametros[9];
            String sho_final_visible = strParametros[10];
            Int32 nid_usuario; Int32.TryParse(strParametros[11], out nid_usuario);

            //-------
            ServicioBE oMaestroServicioBE = new ServicioBE();
            oMaestroServicioBE.Id_Servicio = nid_servicio;
            ServicioBL oMaestroServicioBL = new ServicioBL();
            ServicioBEList oMaestroServicioBEList = oMaestroServicioBL.GETListarDatosServicios(oMaestroServicioBE);
            String fl_quick_service = (oMaestroServicioBEList[0].Fl_quick_service == "1" ? "SI" : "NO");
            //--
            Int32 _ID_TALLER = nid_taller;
            Int32 _INTERVALO = 0;
            Int32 _ID_SERVICIO = nid_servicio;
            Int32 _ID_MODELO = nid_modelo;
            string _TALLER = string.Empty;
            DateTime dFechaIni = Convert.ToDateTime(sfe_reserva_desde);
            DateTime dFechaFin = Convert.ToDateTime(sfe_reserva_hasta);

            DateTime dHoraIni_T = DateTime.MinValue;
            DateTime dHoraFin_T = DateTime.MinValue;

            DateTime _dHoraIni_T = DateTime.MinValue;
            DateTime _dHoraFin_T = DateTime.MinValue;

            DateTime dHoraIni_A = DateTime.MinValue;
            DateTime dHoraFin_A = DateTime.MinValue;

            #region "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"
            DateTime dt_ho_inicio_visible = DateTime.MinValue, dt_ho_final_visible = DateTime.MaxValue;
            if (!String.IsNullOrEmpty(sho_inicio_visible))
            {
                dt_ho_inicio_visible = Convert.ToDateTime(sho_inicio_visible);
                dt_ho_final_visible = Convert.ToDateTime(sho_final_visible);
            }
            DateTime dt_hora_grilla;
            #endregion "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"

            #region - Obtiene Horario Disponible
            CitasBL oCitasBL = new CitasBL();
            CitasBE oCitasBE = new CitasBE();
            CitasBEList oCitasBEList = new CitasBEList();

            Int32 intPRM_10 = Convert.ToInt32(oParametros.GetValor(Parametros.PARM._10));
            if (sfe_reserva_desde != "" && sfe_reserva_hasta != "")
            {
                CitasBEList lstTalleres_Disponibles = null;
                CitasBEList _lstAsesores_Disponibles;
                CitasBEList _lstCitas;
                CitasBEList _lstTalleresHE;

                CitasBEList lstAsesores;
                CitasBEList lstCitas;
                CitasBEList lstTalleresHE;

                string strTotalHE;// Acumulacion de Rangos de HE
                string strAsesorCapac = string.Empty;
                DateTime dtHEITaller;
                DateTime dtHEFTaller;

                DateTime dtHoraI_ddl = DateTime.MaxValue;
                DateTime dtHoraF_ddl = DateTime.MinValue;

                bool swHorasT = false;
                Int32 hay1 = 0; Int32 hay2 = 0;
                String imgHoraLibre = "<img id='{2}' idfoo='{2}' style='cursor:pointer;' title='Seleccionar' alt='' src='" + imgURL_HORA_LIBRE + "' onclick='javascript: fn_SetHoraAsesor_ProxTurnos(&#39;{0}&#39;, &#39;{1}&#39;, &#39;{2}&#39;, &#39;{3}&#39;);' />";
                Int32 rowID = 1;
                while (dFechaIni <= dFechaFin)
                {
                    #region - Obtiene Fecha mínima de reserva, Talleres disponibles, Talleres Horario Excepcional, Asesores disponibles y Citas
                    oCitasBE.nid_Servicio = nid_servicio;
                    oCitasBE.nid_modelo = nid_modelo;
                    oCitasBE.coddpto = coddpto;
                    oCitasBE.codprov = codprov;
                    oCitasBE.coddis = coddist;
                    oCitasBE.nid_ubica = nid_ubica;
                    oCitasBE.nid_taller = nid_taller;
                    oCitasBE.nid_usuario = nid_usuario;
                    oCitasBE.fe_atencion = dFechaIni;
                    oCitasBE.dd_atencion = getDiaSemana(oCitasBE.fe_atencion);

                    lstTalleres_Disponibles = oCitasBL.ListarTalleresDisponibles_PorFecha(oCitasBE);//   1-Listado todos Talleres                
                    _lstTalleresHE = oCitasBL.ListarHorarioExcepcional_Talleres(oCitasBE);// 2-Listado Horario Excepcionales
                    _lstAsesores_Disponibles = oCitasBL.ListarAsesoresDisponibles_PorFecha(oCitasBE);//  3-Listado Asesores Talleres
                    _lstCitas = oCitasBL.ListarCitasAsesores(oCitasBE);//                    4-Listado CitasAsesores Talleres

                    foreach (CitasBE oTaller in lstTalleres_Disponibles)
                    {
                        _ID_TALLER = Convert.ToInt32(oTaller.nid_taller);
                        _INTERVALO = Convert.ToInt32(oTaller.qt_intervalo_atenc);

                        _dHoraIni_T = Convert.ToDateTime(oTaller.ho_inicio_t);
                        _dHoraFin_T = Convert.ToDateTime(oTaller.ho_fin_t);

                        //=================================================================================
                        //> Validaciones
                        //=================================================================================
                        if (oTaller.qt_cantidad_t <= 0) continue;//Capacidad Atencion Taller
                        if (oParametros.SRC_Pais.Equals(1)) //--> SOLO PERU
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
                        hay1 = 0; hay2 = 0;
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
                        hay1 = 0; hay2 = 0;
                        foreach (CitasBE oEntidad in _lstAsesores_Disponibles)
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
                                    //--
                                    String no_columna = dHoraIni_A.ToString(fl_format_24_horas ? "HH:mm" : "hh:mm").ToUpper().Replace(".", "");
                                    String lblSeleccion = "Selección de Reserva -  " + ((oParametros.GetValor(Parametros.PARM._10).ToString().Equals("0")) ? oParametros.N_Asesor + " " + oAsesor.nid_asesor.ToString() : oAsesor.no_asesor) + " - " + GetFechaLarga(dFechaIni) + " - " + FormatoHora(no_columna);
                                    String ho_inicio_a = no_columna;
                                    String imgHoraColumna = String.Format(imgHoraLibre, rowID.ToString(), no_columna, no_columna + "_" + rowID.ToString()
                                        , String.Format("{0}${1}${2}${3}${4}${5}", oAsesor.nid_asesor, ho_inicio_a, _INTERVALO, lblSeleccion
                                        , _ID_TALLER.ToString(), dFechaIni.ToShortDateString()));

                                    String fl_disponible = "1";
                                    String fechaCita = dFechaIni.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                                    String nomAsesor = (intPRM_10.Equals(0) ? "Asesor de Servicio - " + oAsesor.nid_asesor.ToString() : oAsesor.no_asesor.ToString());

                                    #region "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"
                                    //DateTime dt_hora_grilla;
                                    if (!String.IsNullOrEmpty(sho_inicio_visible))
                                    {
                                        dt_hora_grilla = Convert.ToDateTime(strHoraAct);
                                        if (dt_hora_grilla < dt_ho_inicio_visible || dt_hora_grilla > dt_ho_final_visible)
                                        {
                                            //no muestra
                                        }
                                        else
                                        {
                                            Dictionary<string, object> oHorario = new Dictionary<string, object>();
                                            oHorario.Add("no_ubica", oTaller.no_ubica);
                                            oHorario.Add("no_taller", oTaller.no_taller);
                                            oHorario.Add("fe_cita", fechaCita);
                                            oHorario.Add("ho_cita", strHoraAct);
                                            oHorario.Add("no_asesor", nomAsesor);
                                            oHorario.Add("fl_quick_service", fl_quick_service);
                                            oHorario.Add("img_seleccionar", imgHoraColumna);
                                            oHorario.Add("nid_asesor", oAsesor.nid_asesor.ToString());
                                            oHorario.Add("nid_taller", _ID_TALLER.ToString());
                                            //oRow["NOM_ASESOR"] = oAsesor.no_asesor;
                                            oHorario.Add("nu_telefono", oAsesor.nu_telefono_a.Trim());
                                            oHorario.Add("no_email", oAsesor.no_correo_a.Trim());
                                            oHorario.Add("fl_disponible", fl_disponible);
                                            oHorarioDisponible.Add(oHorario);

                                            //Para los filtros
                                            lstFiltroTaller.Add(oTaller.no_taller + ":" + oTaller.no_taller);
                                            lstFiltroFecha.Add(fechaCita + ":" + fechaCita);
                                            lstFiltroHora.Add(strHoraAct.Replace(":", ".") + ":" + strHoraAct.Replace(":", "."));
                                            lstFiltroAsesor.Add(nomAsesor + ":" + nomAsesor);
                                            //Para los filtros

                                            rowID = rowID + 1;
                                        }
                                    }
                                    #endregion "Para ocultar las horas de acuerdo a combo de Hora Inicio y Fin"
                                    else
                                    {
                                        Dictionary<string, object> oHorario = new Dictionary<string, object>();
                                        oHorario.Add("no_ubica", oTaller.no_ubica);
                                        oHorario.Add("no_taller", oTaller.no_taller);
                                        oHorario.Add("fe_cita", fechaCita);
                                        oHorario.Add("ho_cita", strHoraAct);
                                        oHorario.Add("no_asesor", nomAsesor);
                                        oHorario.Add("fl_quick_service", fl_quick_service);
                                        oHorario.Add("img_seleccionar", imgHoraColumna);
                                        oHorario.Add("nid_asesor", oAsesor.nid_asesor.ToString());
                                        oHorario.Add("nid_taller", _ID_TALLER.ToString());
                                        //oRow["NOM_ASESOR"] = oAsesor.no_asesor;
                                        oHorario.Add("nu_telefono", oAsesor.nu_telefono_a.Trim());
                                        oHorario.Add("no_email", oAsesor.no_correo_a.Trim());
                                        oHorario.Add("fl_disponible", fl_disponible);
                                        oHorarioDisponible.Add(oHorario);

                                        //Para los filtros
                                        lstFiltroTaller.Add(oTaller.no_taller + ":" + oTaller.no_taller);
                                        lstFiltroFecha.Add(fechaCita + ":" + fechaCita);
                                        lstFiltroHora.Add(strHoraAct.Replace(":", ".") + ":" + strHoraAct.Replace(":", "."));
                                        lstFiltroAsesor.Add(nomAsesor + ":" + nomAsesor);
                                        //Para los filtros

                                        rowID = rowID + 1;
                                    }
                                    //--

                                    dHoraIni_A = dHoraIni_A.AddMinutes(_INTERVALO);
                                }
                            }
                        }

                    }

                    #endregion - Obtiene Fecha mínima de reserva, Talleres disponibles, Talleres Horario Excepcional, Asesores disponibles y Citas
                    dFechaIni = dFechaIni.AddDays(+1);
                }

                if (oHorarioDisponible.Count == 0)
                {
                    fl_seguir = "0";
                    msg_retorno = "No hay Horario disponible para el rango de fecha seleccionado.";
                    goto Response;
                }

                if (swHorasT)
                {
                    if (String.IsNullOrEmpty(sho_inicio_visible))
                    {
                        oComboHoraInicio = cargarComboHorarioTaller(dtHoraI_ddl, dtHoraF_ddl, _INTERVALO);
                        oComboHoraFinal = oComboHoraInicio;
                    }
                }

            }
            #endregion - Obtiene Horario Disponible

            //Ordena listado
            oHorarioDisponible = oHorarioDisponible.OrderBy(dict => Convert.ToDateTime(dict["fe_cita"]))
                .ThenBy(dict => Convert.ToDateTime(dict["ho_cita"]))
                .ThenBy(dict => Convert.ToString(dict["no_asesor"])).ToList();

            List<String> lstFiltroTallerDistinct = lstFiltroTaller.Distinct().ToList();
            List<String> lstFiltroFechaDistinct = lstFiltroFecha.Distinct().ToList();
            List<String> lstFiltroHoraDistinct = lstFiltroHora.Distinct().ToList();
            List<String> lstFiltroAsesorDistinct = lstFiltroAsesor.Distinct().ToList();
            String valuesFiltroTaller = String.Join(";", lstFiltroTallerDistinct.ToArray());
            String valuesFiltroFecha = String.Join(";", lstFiltroFechaDistinct.ToArray());
            String valuesFiltroHora = String.Join(";", lstFiltroHoraDistinct.ToArray());
            String valuesFiltroAsesor = String.Join(";", lstFiltroAsesorDistinct.ToArray());
            object objValues = null;
            Dictionary<string, object> obj_ModelCol_Taller = (Dictionary<string, object>)oHorario_ModelCol[1]; //no_taller
            obj_ModelCol_Taller.Add("stype", "select"); objValues = new { value = valuesFiltroTaller };
            obj_ModelCol_Taller.Add("searchoptions", objValues);

            Dictionary<string, object> obj_ModelCol_FecCita = (Dictionary<string, object>)oHorario_ModelCol[2]; //fe_cita
            obj_ModelCol_FecCita.Add("stype", "select"); objValues = new { value = valuesFiltroFecha };
            obj_ModelCol_FecCita.Add("searchoptions", objValues);

            Dictionary<string, object> obj_ModelCol_HoraCita = (Dictionary<string, object>)oHorario_ModelCol[3]; //ho_cita
            obj_ModelCol_HoraCita.Add("stype", "select"); objValues = new { value = valuesFiltroHora };
            obj_ModelCol_HoraCita.Add("searchoptions", objValues);

            Dictionary<string, object> obj_ModelCol_NomAsesor = (Dictionary<string, object>)oHorario_ModelCol[4]; //no_asesor
            obj_ModelCol_NomAsesor.Add("stype", "select"); objValues = new { value = valuesFiltroAsesor };
            obj_ModelCol_NomAsesor.Add("searchoptions", objValues);

        Response:
            strRetorno = new
            {
                fl_seguir = fl_seguir,
                msg_retorno = msg_retorno,
                oHorario_Cabecera = oHorario_Cabecera,
                oHorario_ModelCol = oHorario_ModelCol,
                oHorarioDisponible = oHorarioDisponible,
                oComboHoraInicio = oComboHoraInicio,
                oComboHoraFinal = oComboHoraFinal
            };
        }
        catch (Exception ex)
        {
            strRetorno = new
            {
                fl_seguir = "-1",
                msg_retorno = "Error: " + ex.Message,
                oHorario_Cabecera = oHorario_Cabecera,
                oHorario_ModelCol = oHorario_ModelCol,
                oHorarioDisponible = oHorarioDisponible,
                oComboHoraInicio = oComboHoraInicio,
                oComboHoraFinal = oComboHoraFinal
            };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }
    #endregion "Próximos Turnos"

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object SaveReserva(Object strParametros)
    {
        Parametros oParametros = new Parametros();
        String fl_seguir = "0";
        String msg_retorno = String.Empty;
        object strRetorno;
        object oDatosCita = null;
        try
        {
            #region "- Seteando variables Request"
            Dictionary<string, object> oRequest = new Dictionary<string, object>();
            oRequest = (Dictionary<string, object>)strParametros;
            Int32 nid_vehiculo; Int32.TryParse(oRequest["nid_vehiculo"].ToString(), out nid_vehiculo);
            String nu_placa = oRequest["nu_placa"].ToString().ToUpper();
            Int32 nid_marca; Int32.TryParse(oRequest["nid_marca"].ToString(), out nid_marca);
            String no_modelo = oRequest["no_modelo"].ToString().ToUpper();
            String no_color = oRequest["no_color"].ToString().ToUpper();
            String no_marca = oRequest["no_marca"].ToString();
            Int32 nid_modelo; Int32.TryParse(oRequest["nid_modelo"].ToString(), out nid_modelo);
            String nu_anio = oRequest["nu_anio"].ToString();
            String co_tipo_veh = oRequest["co_tipo_veh"].ToString();
            Int32 nid_servicio; Int32.TryParse(oRequest["nid_servicio"].ToString(), out nid_servicio);
            String tx_observacion = oRequest["tx_observacion"].ToString();
            Int32 nid_taller; Int32.TryParse(oRequest["nid_taller"].ToString(), out nid_taller);
            Int32 nid_asesor; Int32.TryParse(oRequest["nid_asesor"].ToString(), out nid_asesor);
            String sfe_programada = oRequest["fe_programada"].ToString();
            String ho_inicio = oRequest["ho_inicio"].ToString();
            Int32 qt_intervalo_atenc; Int32.TryParse(oRequest["qt_intervalo_atenc"].ToString(), out qt_intervalo_atenc);
            Int32 nid_contacto; Int32.TryParse(oRequest["nid_cliente"].ToString(), out nid_contacto);
            String co_tipo_documento = oRequest["co_tipo_documento"].ToString();
            String nu_documento = oRequest["nu_documento"].ToString().Trim().ToUpper();
            String no_cliente = oRequest["no_cliente"].ToString().Trim().ToUpper();
            String ape_paterno = oRequest["ape_paterno"].ToString().Trim().ToUpper();
            String ape_materno = oRequest["ape_materno"].ToString().Trim().ToUpper();
            String no_correo_personal = oRequest["no_correo_personal"].ToString().Trim();
            String no_correo_trabajo = oRequest["no_correo_trabajo"].ToString().Trim();
            String no_correo_alternativo = oRequest["no_correo_alternativo"].ToString().Trim();
            String nu_telefono = oRequest["nu_telefono"].ToString().Trim();
            String nu_telefono_oficina = oRequest["nu_telefono_oficina"].ToString().Trim();
            String nu_celular = oRequest["nu_celular"].ToString().Trim();
            String nu_celular_alter = oRequest["nu_celular_alter"].ToString().Trim();
            Int32 nid_record_cita; Int32.TryParse(oRequest["nid_record_cita"].ToString(), out nid_record_cita);
            Int32 co_tipo_record; Int32.TryParse(oRequest["co_tipo_record"].ToString(), out co_tipo_record);
            Int32 dd_record; Int32.TryParse(oRequest["dd_record"].ToString(), out dd_record);
            String ho_record_ini = (oRequest["ho_record_ini"].ToString() == "" ? "0" : oRequest["ho_record_ini"].ToString());
            String ho_record_fin = (oRequest["ho_record_fin"].ToString() == "" ? "0" : oRequest["ho_record_fin"].ToString());
            String fl_taxi = oRequest["fl_taxi"].ToString();
            String co_usuario = oRequest["co_usuario"].ToString();
            String no_usuario_red = oRequest["no_usuario_red"].ToString();
            String no_estacion_red = oRequest["no_estacion_red"].ToString();
            String strTipoReg = oRequest["tipo_registro"].ToString();
            String nid_contact_center = oRequest["nid_contact_center"].ToString();
            String no_campanias = oRequest["campanias"].ToString();
            String fl_recojounidad = oRequest["recojounidad"].ToString();
            #endregion "- Seteando variables Request"

            #region "Validaciones"
            if (nu_placa.Trim() == "") { msg_retorno = "Debe ingresar un número de " + oParametros.N_Placa; }
            else if (nid_marca <= 0) { msg_retorno = "Debe seleccionar una Marca."; }
            else if (nid_modelo <= 0) { msg_retorno = "Debe seleccionar un Modelo."; }
            else if (nid_servicio <= 0) { msg_retorno = "Debe seleciconar un servicio."; }
            else if (nid_asesor <= 0) { msg_retorno = oParametros.msgSelFec; }
            else if (ho_inicio == "") { msg_retorno = oParametros.msgSelFec; }
            if (msg_retorno != "")
            {
                fl_seguir = "0";
                goto Response;
            }
            #endregion "Validaciones

            //******************************************************************************
            //>>  REGISTRO DE CITA 
            //******************************************************************************
            #region "- Registro de cita"
            CitasBE oCitasBE = new CitasBE();
            CitasBL oCitasBL = new CitasBL();
            //===========================================
            // 1 - Datos del Contacto
            //=========================================== 
            oCitasBE.nid_contacto_sr = nid_contacto;
            oCitasBE.nid_vehiculo = nid_vehiculo;
            oCitasBE.cod_tipo_documento = co_tipo_documento;
            oCitasBE.nu_documento = nu_documento.Trim().ToUpper();
            oCitasBE.no_nombre = no_cliente.Trim().ToUpper();
            oCitasBE.no_ape_paterno = ape_paterno.Trim().ToUpper();
            oCitasBE.no_ape_materno = ape_materno.Trim().ToUpper();
            oCitasBE.nu_telefono = nu_telefono;
            oCitasBE.nu_tel_oficina = nu_telefono_oficina;
            oCitasBE.nu_celular = (!nu_celular.Trim().Equals("")) ? ((oParametros.SRC_Pais.Equals(2) ? "569-" : "") + nu_celular.Trim()) : "";
            oCitasBE.nu_celular_alter = (!nu_celular_alter.Trim().Equals("")) ? ((oParametros.SRC_Pais.Equals(2) ? "569-" : "") + nu_celular_alter.Trim()) : "";
            oCitasBE.no_correo = no_correo_personal.Trim();
            oCitasBE.no_correo_trabajo = no_correo_trabajo.Trim();
            oCitasBE.no_correo_alter = no_correo_alternativo.Trim();
            //===========================================
            // 3 - Datos de la Cita
            //=========================================== 
            oCitasBE.nid_taller = nid_taller;
            oCitasBE.nid_usuario = nid_asesor;
            oCitasBE.nid_Servicio = nid_servicio;
            oCitasBE.nu_placa = nu_placa.ToUpper();
            oCitasBE.nid_marca = nid_marca;
            oCitasBE.nid_modelo = nid_modelo;
            oCitasBE.fe_prog = Convert.ToDateTime(sfe_programada);
            oCitasBE.fl_origen = "B";
            oCitasBE.ho_inicio = ho_inicio;
            oCitasBE.ho_fin = Convert.ToDateTime(ho_inicio).AddMinutes(qt_intervalo_atenc).ToString("HH:mm");
            oCitasBE.fl_datos_pend = (nu_telefono.Trim().Length == 0) ? "1" : "0";
            oCitasBE.tx_observacion = tx_observacion.Trim();
            String strPais = (oParametros.SRC_CodPais.Equals("1") ? "PE" : "CH");
            oCitasBE.No_pais = strPais;
            oCitasBE.Tipo_reg = strTipoReg;
            oCitasBE.dd_atencion = getDiaSemana(oCitasBE.fe_prog);
            oCitasBE.fl_taxi = fl_taxi;
            oCitasBE.fl_recojounidad = fl_recojounidad;
            //-------------------------
            // AUDITORIA
            //-------------------------------------
            oCitasBE.co_usuario_crea = co_usuario;
            oCitasBE.no_usuario_red = no_usuario_red;
            oCitasBE.no_estacion_red = no_estacion_red;
            
            string hoy = DateTime.Now.ToString("dd/MM/yyyy");
            oCitasBE.co_tipo_cita = "001";
            if (!oParametros.SRC_CodPais.Equals("2") && sfe_programada.Equals(hoy))
                oCitasBE.co_tipo_cita = "002";

            //-----------------------------------------------------------------------
            //> Registrando la Cita
            //-----------------------------------------------------------------------
            string oRespBO = oCitasBL.ReservarCitaBO(oCitasBE);
            if (oRespBO.Equals("DR"))
                msg_retorno = ("El número de documento ingresado ya existe, ingrese uno diferente.");
            else if (oRespBO.Equals("C0"))
                msg_retorno = ("Esta " + oParametros.N_Placa + " ya contiene una cita pendiente.");
            else if (oRespBO.Equals("C1"))
                msg_retorno = ("Ya se ha reservado una cita con estos mismos datos.");
            else if (oRespBO.Equals("C2"))
                msg_retorno = ("Ya se ha reservado una Cita en este mismo horario.");
            else if (oRespBO.Equals("C3"))
                msg_retorno = ("Este vehículo ya tiene cita separada para esta fecha y hora programada.");
            else if (oRespBO.Equals("C4"))
                msg_retorno = ("Ya se ha alcanzado el limite de atenciones por día del Taller.");
            else if (oRespBO.Equals("C5"))
                msg_retorno = ("Ya se ha alcanzado el limite de atenciones por día del Asesor.");
            else if (oRespBO.Equals("C6"))
                msg_retorno = ("Ya se ha alcanzado el limite de atenciones por día del Taller y Modelo.");
            else if (oRespBO.Contains(strPais))
            {
                //OK -- PERU
            }
            else if (oParametros.SRC_Pais == 2)
            {
                //OK -- CHILE
            }
            else if (!oRespBO.Equals("CE"))
            {
                msg_retorno = ("No se pudo efectuar la cita, " + oRespBO);
            }
            else if (oRespBO.Equals("CE"))
            {
                msg_retorno = "La Reserva de Cita ha sido registrada como Cola de Espera.";
                fl_seguir = "2"; //reload
            }

            if (msg_retorno == "" && strTipoReg.Equals("REG"))
            {
                fl_seguir = "1";
                msg_retorno = "";

                oCitasBE = new CitasBE();
                oCitasBE.nid_cita = 0;
                oCitasBE.cod_reserva_cita = oRespBO;
                CitasBEList oCitasBEList = oCitasBL.GETListarDatosCita(oCitasBE);
                oCitasBE = new CitasBE();
                oCitasBE = oCitasBEList[0];
                
                CorreoElectronico oEmail = new CorreoElectronico(HttpContext.Current.Server.MapPath("~/"));
                //>> Llenado para la Impresion
                
                oCitasBE.fecha_atencion = GetFechaLarga(oCitasBE.fe_prog) + " - " + FormatoHora(oCitasBE.ho_inicio_c);
                oCitasBE.nu_callcenter = ConfigurationManager.AppSettings["TelefCallCenter"].ToString();
                
                string strImpresion = Plantilla_Imprimir(oCitasBE);

                //Set Datos Cita
                oDatosCita = new
                {
                    template_impresion = strImpresion,
                    //-------------------------
                    no_cliente = no_cliente.Trim().ToUpper() + " " + ape_paterno.Trim().ToUpper() + " " + ape_materno.Trim().ToUpper(),
                    nu_telefono = nu_telefono,
                    nu_celular = ((oParametros.SRC_Pais == 2) ? "(569) " : "") + nu_celular.Trim(),
                    no_correo_personal = no_correo_personal,
                    no_correo_trabajo = no_correo_trabajo,
                    no_correo_alternativo = no_correo_alternativo,
                    nu_placa = nu_placa.Trim().ToUpper(),
                    co_reserva = oRespBO,
                    fe_programada = oCitasBE.fecha_atencion,
                    no_asesor = oCitasBE.no_asesor,
                    no_taller = oCitasBE.no_taller,
                    di_ubica = oCitasBE.di_ubica,
                    nu_telf_taller = oCitasBE.nu_telefono_t,
                    nu_telf_callcenter = ConfigurationManager.AppSettings["TelefCallCenter"].ToString()
                };

                //>>------ ENVIO DE CORREOS --------- >>
                //EnviarCorreo_Cliente(oCitasBE, Parametros.EstadoCita.REGISTRADA); --pendiente
                //EnviarCorreo_Asesor(oCitasBE, Parametros.EstadoCita.REGISTRADA); --pendiente
            }
        #endregion "- Registro de cita"

        Response:
            strRetorno = new
            {
                fl_seguir = fl_seguir,
                msg_retorno = msg_retorno,
                oDatosCita = oDatosCita
            };
        }
        catch (Exception ex)
        {
            strRetorno = new
            {
                fl_seguir = "-1",
                msg_retorno = "Error: " + ex.Message,
                oDatosCita = oDatosCita
            };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }

    private static String Plantilla_Imprimir(CitasBE oCita)
    {
        Parametros oParametros = new Parametros();
        System.Text.StringBuilder strHTML = new System.Text.StringBuilder();
        String strBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title>Datos de la Cita</title><style type='text/css'>body{max-width: 610px;min-width: 320px;margin: 0 auto;font-family: Arial;color: #646464;font-size: 12px;}div{padding-top: 1px;padding-bottom: 1px;}span{padding: 3px;}</style></head>"
            + "<body style='color: #4264af;padding: 3px;'><div><div style='text-align: right; font-weight:bold; font-size: 14px; color: #012d68; font-family: Arial; background-color: #c7d7ee; padding:5px;'>Sistema de Reserva de Citas &nbsp;</div><div>"
            + "<div><p>Sr(a): <span style='font-weight: bold; text-align: justify;'>{Cliente}</span></p><p>Se le adjunta el res&uacute;men de su Cita:</p>"
                            + "<div style='border: solid 1px #c7d7ee;'>"
                                + "<div><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>Código Reserva</span><span>{CODIGO_RESERVA}</span></div>"
                                + "<div>&nbsp;</div>"
                                + "<div><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>{LABEL_PLACA}</span><span>{PLACA}</span></div>"
                                + "<div style='float:left;'><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>Marca</span><span style='display:inline-block;min-width:150px;'>{MARCA}</span></div>"
                                + "<div style='display: inline-block;'><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>Modelo</span><span>{MODELO}</span></div>"
                                + "<div style='clear: both;'>&nbsp;</div>"
                                + "<div><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>Cita</span><span>{CITA}</span></div>"
                                + "<div><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>Servicio</span><span>{SERVICIO}</span></div>"
                                + "<div><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>Asesor de Servicio</span><span>{ASESOR_SERVICIO}</span></div>"
                                + "<div style='float:left;'><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>Taller</span><span style='display:inline-block;min-width:150px;'>{TALLER}</span></div>"
                                + "<div style='display: inline-block;'><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>Teléfono</span><span>{TELEFONO_TALLER}</span></div>"
                                + "<div style='clear: both;'><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>{LABEL_LOCAL}</span><span>{LOCAL}</span></div>"
                                + "<div><span style='font-weight: bold; background-color: #ebeff7; display:inline-block;width:130px;'>Dirección</span><span>{DIRECCION}</span></div>"
                            + "</div>"
                            + "<div style='padding:5px;'><span style='font-size: 11px; font-weight: bold; color: #4264af; font-family: arial; text-align: justify; width: 100%;'>En caso desee reprogramar su cita llame a nuestro Call Center al {CallCenter}</span></div>"
                            + "<div style='text-align: center; font-size: 11px; color: #ffffff; font-family: Arial; background-color: #000000;padding:5px;'><b>&copy; 2016 Grupo Gildemeister todos los derechos reservados</b></div>"
                        + "</div>"
                    + "</div>"
                + "</div>"
            + "</body>"
            + "</html>";

        String nomCliente = oCita.no_cliente.Trim().ToUpper() + " " + oCita.no_ape_paterno.Trim().ToUpper() + " " + oCita.no_ape_materno.Trim().ToUpper();
        strBody = strBody.Replace("{Cliente}", nomCliente);
        strBody = strBody.Replace("{CODIGO_RESERVA}", oCita.cod_reserva_cita);
        strBody = strBody.Replace("{LABEL_PLACA}", oParametros.N_Placa);
        strBody = strBody.Replace("{PLACA}", oCita.nu_placa);
        strBody = strBody.Replace("{MARCA}", oCita.no_marca);
        strBody = strBody.Replace("{MODELO}", oCita.no_modelo);
        strBody = strBody.Replace("{CITA}", oCita.fecha_atencion);
        strBody = strBody.Replace("{SERVICIO}", oCita.no_servicio);
        strBody = strBody.Replace("{ASESOR_SERVICIO}", oCita.no_asesor);
        strBody = strBody.Replace("{TALLER}", oCita.no_taller);
        strBody = strBody.Replace("{TELEFONO_TALLER}", oCita.nu_telefono_t);
        strBody = strBody.Replace("{LABEL_LOCAL}", oParametros.N_Local);
        strBody = strBody.Replace("{LOCAL}", oCita.no_ubica);
        strBody = strBody.Replace("{DIRECCION}", oCita.di_ubica);
        strBody = strBody.Replace("{CallCenter}", oCita.nu_callcenter);

        strHTML.Append(strBody);
        strHTML.Replace("'", "\"");//Formato pra visualizar

        return strHTML.ToString();
    }

    #region [-------- ENVIO EMAIL ----------------------]
    private static void EnviarCorreo_Cliente(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        CorreoElectronico oCorreoElectronico = new CorreoElectronico(HttpContext.Current.Server.MapPath("~/"));

        bool flEnvio = oCorreoElectronico.EnviarMensajeCorreo(oDatos, oTipoCita, Parametros.PERSONA.CLIENTE);
        if (!flEnvio)
        {
            //SRC_MsgInformacion("Error al enviar Correo-Cliente");
        }
    }
    private static void EnviarCorreo_Asesor(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        CorreoElectronico oCorreoElectronico = new CorreoElectronico(HttpContext.Current.Server.MapPath("~/"));

        bool flEnvio = oCorreoElectronico.EnviarMensajeCorreo(oDatos, oTipoCita, Parametros.PERSONA.ASESOR);
        if (!flEnvio)
        {
            //SRC_MsgInformacion("Error al enviar Correo-Asesor"); ;
        }
    }

    #endregion [-------- ENVIO EMAIL ----------------------]

    #region "Modal - Búsqueda de Vehículos"
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_BandejaVehiculo(String[] strFiltros
        , int pPageSize, int pCurrentPage, string pSortColumn, string pSortOrder)
    {
        string oDNI = ConfigurationManager.AppSettings["TIPODOCDNI"].ToString();
        string oRUC = ConfigurationManager.AppSettings["TIPODOCRUC"].ToString();

        Int32 nid_marca; Int32.TryParse(strFiltros[1], out nid_marca);
        Int32 nid_modelo; Int32.TryParse(strFiltros[2], out nid_modelo);
        Int32 nid_usuario; Int32.TryParse(strFiltros[10], out nid_usuario);

        CitasBL oCitasBL = new CitasBL();
        CitasBE oCitasBE = new CitasBE();
        oCitasBE.nu_placa = strFiltros[0];
        oCitasBE.nid_marca = nid_marca;
        oCitasBE.nid_modelo = nid_modelo;
        oCitasBE.Nu_vin = strFiltros[3];
        oCitasBE.Tipo = Convert.ToInt32(strFiltros[4]);
        oCitasBE.cod_tipo_documento = strFiltros[5];
        oCitasBE.nu_documento = strFiltros[6].Trim();
        oCitasBE.no_nombre = (strFiltros[5].Equals(oRUC)) ? "" : strFiltros[7].Trim();
        oCitasBE.no_razon_social = (strFiltros[5].Equals(oRUC)) ? strFiltros[7].Trim() : "";
        oCitasBE.no_ape_paterno = strFiltros[8].Trim();
        oCitasBE.no_ape_materno = strFiltros[9].Trim();
        Parametros oParametro = new Parametros();
        oCitasBE.Nid_empresa = Convert.ToInt32(oParametro.SRC_CodEmpresa);
        oCitasBE.nid_usuario = nid_usuario;

        object objParametros = new
        {
            nu_placa = oCitasBE.nu_placa,
            nid_marca = oCitasBE.nid_marca,
            nid_modelo = oCitasBE.nid_modelo
            ,
            nu_vin = oCitasBE.Nu_vin,
            Tipo = oCitasBE.Tipo,
            cod_tipo_documento = oCitasBE.cod_tipo_documento
            ,
            nu_documento = oCitasBE.nu_documento,
            no_nombre = oCitasBE.no_nombre,
            no_razon_social = oCitasBE.no_razon_social
            ,
            no_ape_paterno = oCitasBE.no_ape_pat_cli,
            no_ape_materno = oCitasBE.no_ape_materno,
            nid_usuario = oCitasBE.nid_usuario
        };
        System.Web.Script.Serialization.JavaScriptSerializer ser_json = new System.Web.Script.Serialization.JavaScriptSerializer();

        CitasBEList oListaVehiculos = oCitasBL.GetBuscarPlacaVehiculo(oCitasBE);

        //--- setup calculations
        int pageIndex = pCurrentPage; //--- current page
        int pageSize = pPageSize; //--- number of rows to show per page
        int totalRecords = oListaVehiculos.Count; //--- number of total items from query
        int totalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize); //--- number of pages

        //--- filter dataset for paging and sorting
        IOrderedEnumerable<CitasBE> orderedRecords = null;
        if (pSortColumn == "nid_vehiculo") orderedRecords = oListaVehiculos.OrderBy(col => col.nid_vehiculo);
        else if (pSortColumn == "nu_placa") orderedRecords = oListaVehiculos.OrderBy(col => col.nu_placa);
        else if (pSortColumn == "no_marca") orderedRecords = oListaVehiculos.OrderBy(col => col.no_marca);
        else if (pSortColumn == "no_modelo") orderedRecords = oListaVehiculos.OrderBy(col => col.no_modelo);
        else if (pSortColumn == "nu_vin") orderedRecords = oListaVehiculos.OrderBy(col => col.Nu_vin);
        else if (pSortColumn == "no_propietario") orderedRecords = oListaVehiculos.OrderBy(col => col.propietario);

        IEnumerable<CitasBE> sortedRecords;
        if (pSortColumn == "0") sortedRecords = oListaVehiculos.ToList();
        else
        {
            sortedRecords = orderedRecords.ToList();
            if (pSortOrder == "desc") sortedRecords = sortedRecords.Reverse();
        }
        sortedRecords = sortedRecords
              .Skip((pageIndex - 1) * pageSize) //--- page the data
              .Take(pageSize);

        //Retorna formato JQGrid
        JQGridJsonResponse responseJQGrid = new JQGridJsonResponse(totalPages, pageIndex, totalRecords);
        JQGridJsonResponseRow oJQGridJsonResponseRow;
        Int32 i = 0;
        foreach (CitasBE obj in sortedRecords)
        {
            oJQGridJsonResponseRow = new JQGridJsonResponseRow();
            oJQGridJsonResponseRow.ID = (i + 1).ToString();

            String imgSeleccionar = "<img title='Seleccionar' alt='Seleccionar' src='../Images/SRC/si.png' style='width: 20px; height: 20px;' onclick='javascript: fn_SeleccionaVehiculo(&#39;{0}&#39;);' />";
            object filas = new
            {
                nid_vehiculo = obj.nid_vehiculo,
                nu_placa = obj.nu_placa,
                no_marca = obj.no_marca,
                no_modelo = obj.no_modelo,
                nu_vin = obj.Nu_vin,
                no_propietario = "No disponible",
                img = String.Format(imgSeleccionar, oJQGridJsonResponseRow.ID)
            };
            oJQGridJsonResponseRow.Row = filas;
            responseJQGrid.Items.Add(oJQGridJsonResponseRow);
            i++;
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(responseJQGrid);
    }
    #endregion
    #region "Modal - Panel de Agregar Vehículo"
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object GrabarVehiculo(String[] strParametros)
    {
        VehiculoBE oMaestroVehiculoBE = new VehiculoBE();
        MaestroVehiculoBL oMaestroVehiculoBL = new MaestroVehiculoBL();
        Parametros oParametros = new Parametros();
        object[] strRetorno;
        Int32 retorno = 0; String msg_retorno = String.Empty;
        try
        {
            String nu_placa = strParametros[0].Trim().ToUpper();
            Int32 nid_marca; Int32.TryParse(strParametros[1].ToString(), out nid_marca);
            String nu_vin = strParametros[2].Trim().ToUpper();
            Int32 nid_modelo; Int32.TryParse(strParametros[3].ToString(), out nid_modelo);
            Int32 nu_km_actual; Int32.TryParse(strParametros[4].ToString(), out nu_km_actual);
            Int32 nu_anio; Int32.TryParse(strParametros[5].ToString(), out nu_anio);
            String co_tipo = strParametros[6].ToString();
            String co_usuario = strParametros[7].ToString();
            String co_usuario_red = strParametros[8].ToString();
            String no_estacion_red = strParametros[9].ToString();

            oMaestroVehiculoBE.nu_placa = nu_placa;
            String strVIN = oMaestroVehiculoBL.GETVIN_X_PLACA(oMaestroVehiculoBE);
            if (!String.IsNullOrEmpty(strVIN))
            {
                retorno = -2;
                msg_retorno = "El número de " + oParametros.N_Placa + " ingresado ya existe, favor de ingresa otra";
            }
            else
            {
                oMaestroVehiculoBE.nid_vehiculo = 0;
                oMaestroVehiculoBE.nid_propietario = 0;
                oMaestroVehiculoBE.nid_cliente = 0;
                oMaestroVehiculoBE.nid_contacto = 0;
                oMaestroVehiculoBE.nu_placa = nu_placa;
                oMaestroVehiculoBE.nu_vin = (oParametros.SRC_VINObligatorio.Equals("1") ? nu_vin : (string.IsNullOrEmpty(nu_vin) ? nu_placa : nu_vin));
                oMaestroVehiculoBE.nid_marca = nid_marca;
                oMaestroVehiculoBE.nid_modelo = nid_modelo;
                oMaestroVehiculoBE.qt_km_actual = (!string.IsNullOrEmpty(nu_vin) ? nu_km_actual : 0);
                oMaestroVehiculoBE.fl_reg_manual = "1";
                oMaestroVehiculoBE.fl_activo = "A";
                oMaestroVehiculoBE.ind_accion = "1";
                if (oParametros.SRC_CodPais.Equals("2"))
                {
                    oMaestroVehiculoBE.nu_anio = nu_anio;
                    oMaestroVehiculoBE.co_tipo = co_tipo;
                }
                //------------------------------------------                
                oMaestroVehiculoBE.co_usuario = co_usuario;
                oMaestroVehiculoBE.co_usuario_red = co_usuario_red;
                oMaestroVehiculoBE.no_estacion_red = no_estacion_red;

                Int32 resp = oMaestroVehiculoBL.GETInserActuVehiculo(oMaestroVehiculoBE);
                if (resp == -1) //El número de VIN ya existe
                {
                    retorno = -2;
                    msg_retorno = "El número de VIN ya existe, ingrese otro.";
                }
                else if (resp > 0)
                {
                    retorno = resp;
                    msg_retorno = "La " + oParametros.N_Placa + " se registró correctamente.";
                }
                else
                {
                    retorno = -2;
                    msg_retorno = "Error al guardar.";
                }
            }

            strRetorno = new object[] { retorno, msg_retorno };
        }
        catch (Exception ex)
        {
            strRetorno = new object[] { -1, "Error: " + ex.Message };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }
    #endregion
    #region "Modal - Actualizar Propietario/Cliente"
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_TipoDocxTipoPersona(String[] strParametros)
    {
        String tipoPers = strParametros[0];

        List<object> oComboTipoDocumento = new List<object>();
        object response;
        try
        {
            ClienteBL oMaestroClienteBL = new ClienteBL();
            CombosBEList oTipoDocumentos = oMaestroClienteBL.GETListarTipoDocumento(tipoPers);
            object objTipoDoc;
            foreach (ComboBE obj in oTipoDocumentos)
            {
                objTipoDoc = new { value = obj.ID.ToString(), nombre = obj.DES };
                oComboTipoDocumento.Add(objTipoDoc);
            }
        }
        catch { }

        response = new
        {
            oComboTipoDocumento = oComboTipoDocumento
        };

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(response);
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object ActualizarPropCli(String[] strParametros)
    {
        ClienteBE entClie = new ClienteBE();
        ClienteBL oMaestroClienteBL = new ClienteBL();
        Parametros oParametros = new Parametros();
        object[] strRetorno;
        Int32 retorno = 0; String msg_retorno = String.Empty;

        object oPropietario = null;
        object oCliente = null;
        try
        {
            String co_tipo = strParametros[0].Trim();
            Int32 nid_cliente; Int32.TryParse(strParametros[1].Trim(), out nid_cliente);
            String co_tipo_persona = strParametros[2].Trim();
            String co_tipo_documento = strParametros[3].Trim();
            String nu_documento = strParametros[4].Trim();
            String no_nombre_razon = strParametros[5].Trim();
            String ape_paterno = strParametros[6].Trim();
            String ape_materno = strParametros[7].Trim();
            String telf_fijo = strParametros[8].Trim();
            String telf_oficina = strParametros[9].Trim();
            String telf_movil1 = strParametros[10].Trim();
            String telf_movil2 = strParametros[11].Trim();
            String email_personal = strParametros[12].Trim();
            String email_trabajo = strParametros[13].Trim();
            String email_alternativo = strParametros[14].Trim();
            String co_usuario = strParametros[15].ToString().Trim();
            String co_usuario_red = strParametros[16].ToString().Trim();
            String no_estacion_red = strParametros[17].ToString().Trim();
            String nu_placa = strParametros[18].ToString().Trim();
            Int32 nid_usuario; Int32.TryParse(strParametros[19].ToString(), out nid_usuario);
            Int32 nid_pais_celular; Int32.TryParse(strParametros[20].ToString(), out nid_pais_celular);
            Int32 nid_pais_telefono; Int32.TryParse(strParametros[21].ToString(), out nid_pais_telefono);
            String nu_anexo_telefono = strParametros[22].ToString();

            entClie.nid_cliente = nid_cliente;
            entClie.co_tipo_documento = co_tipo_documento;
            entClie.nu_documento = nu_documento;
            entClie.no_cliente = no_nombre_razon;
            entClie.no_ape_pat = ape_paterno;
            entClie.no_ape_mat = ape_materno;
            entClie.no_correo = email_personal;
            entClie.no_correo_trabajo = email_trabajo;
            entClie.no_correo_alter = email_alternativo;
            entClie.nu_telefono = telf_fijo;
            entClie.nu_tel_oficina = telf_oficina;
            entClie.nu_celular = (!telf_movil1.Equals("")) ? ((oParametros.SRC_Pais.Equals(2) ? "569-" : "") + telf_movil1) : "";
            entClie.nu_celular_alter = (!telf_movil2.Equals("")) ? ((oParametros.SRC_Pais.Equals(2) ? "569-" : "") + telf_movil2) : "";
            entClie.co_usuario_crea = co_usuario;
            entClie.co_usuario_red = co_usuario_red;
            entClie.no_estacion_red = no_estacion_red;
            entClie.fl_inactivo = "0";
            entClie.ind_accion = (oMaestroClienteBL.SRC_SPS_VAL_CLIENTE_X_DOC(entClie) == 0) ? "1" : "2";

            if (co_tipo == "CLI")
            {
                entClie.nid_pais_telefono = nid_pais_telefono;
                entClie.nid_pais_celular = nid_pais_celular;
                entClie.nu_anexo_telefono = nu_anexo_telefono;
            }
            
            Int32 Resp = oMaestroClienteBL.GETInserActuCliente(entClie);
            if (Resp > 0)
            {
                VehiculoBE objEnt = new VehiculoBE();
                objEnt.DET_co_tipo_cliente = co_tipo_persona;
                objEnt.DET_co_tipo_documento = co_tipo_documento;
                objEnt.DET_nu_documento = nu_documento;

                VehiculoBEList ObjLista = new VehiculoBEList();
                MaestroVehiculoBL objNeg = new MaestroVehiculoBL();
                ObjLista = objNeg.GETListarBuscarCliente(objEnt);

                Int32 nid_prop_cli_out = ObjLista[0].DET_cod_cliente;

                //--------------
                //  UPDATE - VEHICULO
                //--------------
                CitasBE oCitasBE = new CitasBE();
                CitasBL oCitasBL = new CitasBL();
                oCitasBE.nu_placa = nu_placa;
                oCitasBE.nid_usuario = nid_usuario;
                CitasBEList oCitasBEList = oCitasBL.GETListarDatosVehiculoClientePorPlaca(oCitasBE);
                CitasBE oDatos = oCitasBEList[0];

                VehiculoBE oMaestroVehiculoBE = new VehiculoBE();
                oMaestroVehiculoBE.nid_vehiculo = (int)oDatos.nid_vehiculo;
                oMaestroVehiculoBE.nid_propietario = co_tipo.Equals("PROP") ? nid_prop_cli_out : (int)oDatos.nid_propietario;
                oMaestroVehiculoBE.nid_cliente = co_tipo.Equals("CLI") ? nid_prop_cli_out : (int)oDatos.nid_cliente;
                oMaestroVehiculoBE.nid_contacto = (int)oDatos.nid_contacto_sr;
                oMaestroVehiculoBE.nu_placa = oDatos.nu_placa.Trim();
                oMaestroVehiculoBE.nu_vin = oDatos.Nu_vin;
                oMaestroVehiculoBE.nid_marca = (int)oDatos.nid_marca;
                oMaestroVehiculoBE.nid_modelo = (int)oDatos.nid_modelo;
                oMaestroVehiculoBE.qt_km_actual = (long)oDatos.qt_km_inicial;
                oMaestroVehiculoBE.fl_reg_manual = "1";
                oMaestroVehiculoBE.fl_activo = "A";
                //-----------------------------------------
                oMaestroVehiculoBE.co_usuario = co_usuario;
                oMaestroVehiculoBE.co_usuario_red = co_usuario_red;
                oMaestroVehiculoBE.no_estacion_red = no_estacion_red;
                //----------------------------------------------
                oMaestroVehiculoBE.ind_accion = "2";
                MaestroVehiculoBL oMaestroVehiculoBL = new MaestroVehiculoBL();
                Resp = oMaestroVehiculoBL.GETInserActuVehiculo(oMaestroVehiculoBE);
                if (Resp == -1) //El número de VIN ya existe
                {
                    // ya se valido al inicio
                }
                if (Resp == -2) //El número de VIN ya existe //23.08.2012
                {
                    retorno = -2;
                    msg_retorno = "El número de VIN ingresado ya existe.";
                }
                else if (Resp > 0)
                {
                    retorno = nid_prop_cli_out;
                    msg_retorno = "El registro de vehículo se actualizó con exito.";

                    oCitasBE = new CitasBE();
                    oCitasBE.nu_placa = nu_placa;
                    oCitasBE.nid_usuario = nid_usuario;
                    oCitasBEList = oCitasBL.GETListarDatosVehiculoClientePorPlaca(oCitasBE);
                    if (oCitasBEList.Count > 0)
                    {
                        CitasBE oVehiculoBE = oCitasBEList[0];
                        oPropietario = new
                        {
                            nid_cliente = oVehiculoBE.nid_propietario,
                            co_tipo_persona = oVehiculoBE.co_tipo_persona_prop,
                            co_tipo_documento = oVehiculoBE.co_tipo_documento_prop.Trim(),
                            nu_documento = oVehiculoBE.nu_documento_prop.ToString(),
                            no_ape_paterno = oVehiculoBE.no_ape_pat_prop,
                            no_ape_materno = oVehiculoBE.no_ape_mat_prop,
                            no_nombre_razon = (oVehiculoBE.co_tipo_persona_prop.Equals(oNatural) ? oVehiculoBE.no_cliente_prop.Trim() : oVehiculoBE.no_razon_social_prop.Trim()),
                            nu_telefono = oVehiculoBE.nu_telefono_prop,
                            nu_tel_oficina = oVehiculoBE.nu_telefono2_prop,
                            nu_telmovil1 = (oVehiculoBE.nu_celular_prop.Contains("-") ? oVehiculoBE.nu_celular_prop.Split('-').GetValue(1).ToString() : oVehiculoBE.nu_celular_prop),
                            nu_telmovil2 = (oVehiculoBE.nu_celular2_prop.Contains("-") ? oVehiculoBE.nu_celular2_prop.Split('-').GetValue(1).ToString() : oVehiculoBE.nu_celular2_prop),
                            no_correo = oVehiculoBE.no_correo_prop.Trim(),
                            no_correo_trabajo = oVehiculoBE.no_correo_trab_prop.Trim(),
                            no_correo_alter = oVehiculoBE.no_correo_alter_prop.Trim(),
                            //--para campo texto
                            no_propietario = (!string.IsNullOrEmpty(oVehiculoBE.nu_documento_prop) ? (oVehiculoBE.nu_documento_prop + " - ") : "") + oVehiculoBE.propietario
                        };

                        oCliente = new
                        {
                            nid_cliente = oVehiculoBE.nid_cliente,
                            co_tipo_persona = oVehiculoBE.co_tipo_persona_cli,
                            co_tipo_documento = oVehiculoBE.co_tipo_documento_cli.Trim(),
                            nu_documento = oVehiculoBE.nu_documento_cli.ToString(),
                            no_ape_paterno = oVehiculoBE.no_ape_pat_cli,
                            no_ape_materno = oVehiculoBE.no_ape_mat_cli,
                            no_nombre_razon = (oVehiculoBE.co_tipo_persona_cli.Equals(oNatural) ? oVehiculoBE.no_cliente_cli.Trim() : oVehiculoBE.no_razon_social_cli.Trim()),
                            nu_telefono = oVehiculoBE.nu_telefono_cli,
                            nu_tel_oficina = oVehiculoBE.nu_telefono2_cli,
                            nu_telmovil1 = (oVehiculoBE.nu_celular_cli.Contains("-") ? oVehiculoBE.nu_celular_cli.Split('-').GetValue(1).ToString() : oVehiculoBE.nu_celular_cli),
                            nu_telmovil2 = (oVehiculoBE.nu_celular2_cli.Contains("-") ? oVehiculoBE.nu_celular2_cli.Split('-').GetValue(1).ToString() : oVehiculoBE.nu_celular2_cli),
                            no_correo = oVehiculoBE.no_correo_cli.Trim(),
                            no_correo_trabajo = oVehiculoBE.no_correo_trab_cli.Trim(),
                            no_correo_alter = oVehiculoBE.no_correo_alter_cli.Trim(),
                            //--para campo texto
                            no_cliente = (!string.IsNullOrEmpty(oVehiculoBE.nu_documento_cli) ? (oVehiculoBE.nu_documento_cli + " - ") : "") + oVehiculoBE.cliente
                        };
                    }
                }
                else
                {
                    retorno = -2;
                    msg_retorno = "Error al guardar.";
                }
            }
            else
            {
                retorno = -2;
                msg_retorno = "Error al guardar.";
            }

            strRetorno = new object[] { retorno, msg_retorno, oPropietario, oCliente };
        }
        catch (Exception ex)
        {
            strRetorno = new object[] { -1, "Error: " + ex.Message, null, null };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_PropCliente(String[] strParametros)
    {
        VehiculoBE oMaestroVehiculoBE = new VehiculoBE();
        MaestroVehiculoBL oMaestroVehiculoBL = new MaestroVehiculoBL();
        VehiculoBEList oMaestroVehiculoBEList = new VehiculoBEList();
        object strRetorno;

        object oPropCliente = null;
        try
        {
            String co_tipo_persona = strParametros[0].Trim();
            String co_tipo_documento = strParametros[1].Trim();
            String nu_documento = strParametros[2].Trim();

            oMaestroVehiculoBE.DET_co_tipo_cliente = co_tipo_persona;
            oMaestroVehiculoBE.DET_co_tipo_documento = co_tipo_documento;
            oMaestroVehiculoBE.DET_nu_documento = nu_documento;
            oMaestroVehiculoBEList = oMaestroVehiculoBL.GETListarBuscarCliente(oMaestroVehiculoBE);

            if (oMaestroVehiculoBEList.Count > 0)
            {
                VehiculoBE oVehiculoBE = oMaestroVehiculoBEList[0];
                string cadena = oVehiculoBE.DET_NOMBRES_RZ;
                string[] nombre_rz = new string[3];
                nombre_rz = cadena.Split('*');

                String no_nombre_razon = "";
                String no_ape_paterno = "";
                String no_ape_materno = "";
                if (nombre_rz.Length == 1)
                {
                    no_nombre_razon = nombre_rz.GetValue(0).ToString();
                }
                else
                {
                    no_nombre_razon = nombre_rz.GetValue(0).ToString();
                    no_ape_paterno = nombre_rz.GetValue(1).ToString();
                    no_ape_materno = nombre_rz.GetValue(2).ToString();
                }

                oPropCliente = new
                {
                    nid_cliente = oVehiculoBE.DET_cod_cliente,
                    co_tipo_persona = oVehiculoBE.DET_co_tipo_cliente,
                    co_tipo_documento = oVehiculoBE.DET_co_tipo_documento,
                    nu_documento = oVehiculoBE.DET_nu_documento,
                    no_ape_paterno = no_ape_paterno,
                    no_ape_materno = no_ape_materno,
                    no_nombre_razon = no_nombre_razon,
                    nu_telefono = oVehiculoBE.DET_nu_telefono,
                    nu_tel_oficina = oVehiculoBE.DET_nu_telefono2,
                    nu_telmovil1 = (oVehiculoBE.DET_nu_celular.Contains("-") ? oVehiculoBE.DET_nu_celular.Split('-').GetValue(1).ToString() : oVehiculoBE.DET_nu_celular),
                    nu_telmovil2 = (oVehiculoBE.DET_nu_celular2.Contains("-") ? oVehiculoBE.DET_nu_celular2.Split('-').GetValue(1).ToString() : oVehiculoBE.DET_nu_celular2),
                    no_correo = oVehiculoBE.DET_no_correo.Trim(),
                    no_correo_trabajo = oVehiculoBE.DET_no_correo_trab.Trim(),
                    no_correo_alter = oVehiculoBE.DET_no_correo_alter.Trim()
                    ,nid_pais_celular = oVehiculoBE.nid_pais_celular
                    ,nid_pais_telefono = oVehiculoBE.nid_pais_telefono
                    ,nu_anexo_telefono = oVehiculoBE.nu_anexo_telefono
                };
            }

            strRetorno = new { oPropCliente = oPropCliente };
        }
        catch
        {
            strRetorno = new { oPropCliente = "" };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }
    #endregion
    #region "Modal - Historial de Cita"
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object Get_HistorialCita(String[] strParametros)
    {
        CitasBE oCitasBE = new CitasBE();
        CitasBL oCitasBL = new CitasBL();
        Parametros oParametros = new Parametros();

        object strRetorno;
        object oHistorialCita = null;
        try
        {
            String nu_placa = strParametros[0].Trim().ToUpper();
            Int32 nid_usuario; Int32.TryParse(strParametros[1].ToString(), out nid_usuario);

            oCitasBE.nu_placa = nu_placa;
            oCitasBE.nid_usuario = nid_usuario;
            oCitasBE = oCitasBL.Listar_HistorialCitasPorVehiculo(oCitasBE);

            String no_tipo_doc = oCitasBE.doc_cliente;
            String nombres = ((oCitasBE.co_tipo_cliente.Equals("0001")) ? "Nombres y Apelllidos" : "Razón Social");

            String no_marca = oCitasBE.no_marca;
            String no_modelo = oCitasBE.no_modelo;
            String nu_documento = oCitasBE.nu_documento;
            String no_cliente = oCitasBE.no_cliente;
            String nu_telefono = oCitasBE.nu_telefono;
            String nu_celular = oCitasBE.nu_celular;
            String no_correo = oCitasBE.no_correo;

            List<object> oLista = new List<object>();
            foreach (CitasBE obj in oCitasBE.lstcitas)
            {
                object ent = new
                {
                    Itm = obj.Itm,
                    no_ubica = obj.no_ubica,
                    no_taller = obj.no_taller,
                    no_servicio = obj.no_servicio,
                    km_ult_serv = obj.km_ult_serv,
                    no_asesor = obj.AsesorServ,
                    fe_atencion = obj.fecha_atencion,
                    ho_cita = obj.ho_prog,
                    no_estado = obj.nom_estado
                };
                oLista.Add(ent);
            }
            oHistorialCita = new
            {
                no_tipo_doc = no_tipo_doc,
                nombres = nombres,
                nu_placa = nu_placa,
                no_marca = no_marca,
                no_modelo = no_modelo,
                nu_documento = nu_documento,
                no_cliente = no_cliente,
                nu_telefono = nu_telefono,
                nu_celular = nu_celular,
                no_correo = no_correo,
                oLista = oLista
            };

            strRetorno = new { oHistorialCita = oHistorialCita };
        }
        catch
        {
            strRetorno = new { oHistorialCita = "" };
        }

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }
    #endregion
    

    public static bool IsDate(object expression)
    {
        if (expression == null)
            return false;
        DateTime testDate;
        return DateTime.TryParse(expression.ToString(), out testDate);
    }

    private static string GetFechaMinReserva(Int32 nid_usuario)
    {
        Parametros oParametros = new Parametros();
        Int32 diasMin = 0;
        if (oParametros.SRC_CodPais.Equals("2"))
            diasMin = CitasBL.VerificarAsesorCitaDiaria(nid_usuario).Equals("1") ? 0 : Convert.ToInt32(oParametros.GetValor(Parametros.PARM._05));
        else
            diasMin = ValidarEntreCita(nid_usuario) ? 0 : Convert.ToInt32(oParametros.GetValor(Parametros.PARM._05));
        CitasBL oCitasBL = new CitasBL();
        string strFecha = String.Format("{0:d}", DateTime.Now.AddDays(Convert.ToDouble(diasMin)));
        return strFecha;
    }

    private static bool ValidarEntreCita(Int32 nid_usuario)
    {
        AppMiTaller.Intranet.BE.TipoTablaDetalleBEList oLista = new AppMiTaller.Intranet.BE.TipoTablaDetalleBEList();

        AppMiTaller.Intranet.BL.TipoTablaDetalleBL oTipoTablaDetalleBL = new AppMiTaller.Intranet.BL.TipoTablaDetalleBL();

        oLista = oTipoTablaDetalleBL.ListarTipoTablaDetalle("219", String.Empty, String.Empty, "002", String.Empty, String.Empty, String.Empty);
        if (oLista.Count > 0)
        {
            AppMiTaller.Intranet.BE.TipoTablaDetalleBE objretorno = oLista[0];
            if (objretorno.Valor3 != null)
            {
                string[] datos = objretorno.Valor3.Split('|');
                foreach (string str in datos)
                {
                    if (str.Equals(nid_usuario.ToString()))
                        return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private static string GetFechaMaxReserva()
    {
        Parametros oParametros = new Parametros();
        string strFechaMin = string.Empty;
        strFechaMin = String.Format("{0:d}", DateTime.Now.AddDays(Convert.ToDouble(oParametros.GetValor(Parametros.PARM._06))));
        return strFechaMin;
    }
    private static Int32 getDiaSemana(DateTime dtFecha)
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
    private static string getNombreDiaSemana(Int32 intDia)
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
    private static List<object> cargarComboHorarioTaller(DateTime dtHoraIni, DateTime dtHoraFin, Int32 intInterv)
    {
        Parametros oParametros = new Parametros();
        List<object> oComboHora = new List<object>();
        if (intInterv.Equals(0)) return oComboHora;
        while (dtHoraIni <= dtHoraFin)
        {
            object oHora = new
            {
                value = dtHoraIni.ToString("HH:mm"),
                nombre = (oParametros.SRC_CodPais == "2" ? dtHoraIni.ToString("HH:mm") : FormatoHora(dtHoraIni.ToString("HH:mm")))
            };
            oComboHora.Add(oHora);
            dtHoraIni = dtHoraIni.AddMinutes(intInterv);
        }
        return oComboHora;
    }
    private static string GetFechaLarga(DateTime dt)
    {
        return dt.ToString("D", CultureInfo.CurrentCulture);
    }
    private static string FormatoHora(string strHora)
    {
        string strHF = strHora;
        try
        {
            string strHoraF = Convert.ToDateTime(strHora).ToString("hh:mm");
            Int32 strHoraF1 = Convert.ToInt32(strHora.Replace(":", ""));
            strHF = strHoraF + ((strHoraF1 < 1200) ? " a.m." : " p.m.");
        }
        catch (Exception)
        {
            strHF = strHora;
        }
        return strHF;
    }
    
}
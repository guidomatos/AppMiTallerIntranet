using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class CitasBE
    {
        #region "Campos"

        
        public Int32 nid_modulo { get; set; }
        public String nu_modulo { get; set; }
        public String nu_ticket { get; set; }
        public String co_estado_ate { get; set; }
        public String co_estado_ent { get; set; }
        public String no_estado_ate { get; set; }
        public String no_estado_ent { get; set; }
        public String fl_tipo_cita { get; set; }
        public String nu_patente { get; set; }
        public String fecha_ini { get; set; }
        public String fecha_fin { get; set; }
        public String fecha_cita { get; set; }
        public String hora_cita { get; set; }
        public String no_origen { get; set; }
        public String nu_ticket_ate { get; set; }
        public String fe_hora_ticket_a { get; set; }
        public String fe_hora_espera_a { get; set; }
        public String fe_hora_llamada_a { get; set; }
        public String fe_hora_atencion_a { get; set; }
        public String fe_hora_recepcion_a { get; set; }
        public String nu_ticket_ent { get; set; }
        public String fe_hora_ticket_e { get; set; }
        public String fe_hora_espera_e { get; set; }
        public String fe_hora_llamada_e { get; set; }
        public String fe_hora_atencion_e { get; set; }
        public String fe_hora_entrega_e { get; set; }
        public String no_prov { get; set; }
        public String no_dist { get; set; }
        public String fe_hora_registro { get; set; }
        public String fe_hora_cita { get; set; }
        public String fl_reprogramado { get; set; }
        public String fe_hora_cita_orig { get; set; }
        public String fl_taxi { get; set; }
        public string tx_alternativo_01 { get; set; }
        public string tx_alternativo_02 { get; set; }

        private string _num_fila;

        public string num_fila
        {
            get { return _num_fila; }
            set { _num_fila = value; }
        }

        private Nullable<Int32> _nid_empresa;

        private String _fl_puntualidad;

        private Int32 _pais;

        public Int32 Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }
        private Int32 _coddistrito;

        public Int32 Distrito
        {
            get { return _coddistrito; }
            set { _coddistrito = value; }
        }
        public String Fl_puntualidad
        {
            get { return _fl_puntualidad; }
            set { _fl_puntualidad = value; }
        }

        public Nullable<Int32> Nid_empresa
        {
            get { return _nid_empresa; }
            set { _nid_empresa = value; }
        }

        private Nullable<Int32> _qt_cola_espera;
        public Nullable<Int32> qt_cola_espera
        {
            get { return _qt_cola_espera; }
            set { _qt_cola_espera = value; }
        }


        private Nullable<Int32> _qt_capacidad;
        public Nullable<Int32> qt_capacidad
        {
            get { return _qt_capacidad; }
            set { _qt_capacidad = value; }
        }

        private Nullable<Int32> _qt_capacidad_a;
        public Nullable<Int32> qt_capacidad_a
        {
            get { return _qt_capacidad_a; }
            set { _qt_capacidad_a = value; }
        }

        private Nullable<Int32> _qt_capacidad_t;
        public Nullable<Int32> qt_capacidad_t
        {
            get { return _qt_capacidad_t; }
            set { _qt_capacidad_t = value; }
        }

        private Nullable<Int32> _qt_cantidad_t;
        public Nullable<Int32> qt_cantidad_t
        {
            get { return _qt_cantidad_t; }
            set { _qt_cantidad_t = value; }
        }

        private Nullable<Int32> _qt_cantidad_a;
        public Nullable<Int32> qt_cantidad_a
        {
            get { return _qt_cantidad_a; }
            set { _qt_cantidad_a = value; }
        }

        private Nullable<Int32> _qt_citas_t;
        public Nullable<Int32> qt_citas_t
        {
            get { return _qt_citas_t; }
            set { _qt_citas_t = value; }
        }

        private Nullable<Int32> _qt_citas_a;
        public Nullable<Int32> qt_citas_a
        {
            get { return _qt_citas_a; }
            set { _qt_citas_a = value; }
        }


        private Nullable<Int32> _qt_capacidad_m;
        public Nullable<Int32> qt_capacidad_m
        {
            get { return _qt_capacidad_m; }
            set { _qt_capacidad_m = value; }
        }

        private Nullable<Int32> _qt_cantidad_m;
        public Nullable<Int32> qt_cantidad_m
        {
            get { return _qt_cantidad_m; }
            set { _qt_cantidad_m = value; }
        }

        private Nullable<Int32> _qt_citas_m;
        public Nullable<Int32> qt_citas_m
        {
            get { return _qt_citas_m; }
            set { _qt_citas_m = value; }
        }

        private Nullable<Int32> _nid_dia_exceptuado_t;
        public Nullable<Int32> nid_dia_exceptuado_t
        {
            get { return _nid_dia_exceptuado_t; }
            set { _nid_dia_exceptuado_t = value; }
        }

        private Nullable<Int32> _nid_dia_exceptuado_a;
        public Nullable<Int32> nid_dia_exceptuado_a
        {
            get { return _nid_dia_exceptuado_a; }
            set { _nid_dia_exceptuado_a = value; }
        }



        private CitasBEList _lstcitas;
        public CitasBEList lstcitas
        {
            get { return _lstcitas; }
            set { _lstcitas = value; }
        }

        private Int32 _km_ult_serv;
        public Int32 km_ult_serv
        {
            get { return _km_ult_serv; }
            set { _km_ult_serv = value; }
        }

        private String _fec_ult_serv;
        public String fec_ult_serv
        {
            get { return _fec_ult_serv; }
            set { _fec_ult_serv = value; }
        }

        private Int32 _km_prm_ult_serv;
        public Int32 km_prm_ult_serv
        {
            get { return _km_prm_ult_serv; }
            set { _km_prm_ult_serv = value; }
        }

        private Int32 _km_prox_serv;
        public Int32 km_prox_serv
        {
            get { return _km_prox_serv; }
            set { _km_prox_serv = value; }
        }

        private String _fecproxServ;
        public String fecproxServ
        {
            get { return _fecproxServ; }
            set { _fecproxServ = value; }
        }


        private string _VCORREO;

        public string VCORREO
        {
            get { return _VCORREO; }
            set { _VCORREO = value; }
        }
        private string _AsesorServ;
        private string _nom_estado;
        private Nullable<Int32> _nid_cita;
        private Nullable<Int32> _nid_cliente;

        private Nullable<Int32> _nid_taller;
        private Nullable<Int32> _nid_contacto_sr;
        private Nullable<Int32> _nid_vehiculo;
        private string _fl_origen;
        private Nullable<Int32> _qt_km_inicial;
        private string _no_cliente;
        private string _nu_celular;
        private string _co_tipo_cliente;
        private string _doc_cliente;

        private string _fl_datos_pend;

        public string fl_datos_pend
        {
            get { return _fl_datos_pend; }
            set { _fl_datos_pend = value; }
        }
        private DateTime _fe_atencion;
        private String _fecha_atencion;

        private Nullable<Int32> _dia_prog;
        private string _tx_glos;

        private string _nu_placa;


        //Mensjaes
        private string _nid_aplicacion;

        public string nid_aplicacion
        {
            get { return _nid_aplicacion; }
            set { _nid_aplicacion = value; }
        }
        private string _numero_destino;

        public string numero_destino
        {
            get { return _numero_destino; }
            set { _numero_destino = value; }
        }
        private string _mensaje;

        public string mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }
        private string _usuario_sistema;

        public string usuario_sistema
        {
            get { return _usuario_sistema; }
            set { _usuario_sistema = value; }
        }
        private DateTime _fecha_envio;

        public DateTime fecha_envio
        {
            get { return _fecha_envio; }
            set { _fecha_envio = value; }
        }
        private string _usuario_red;

        public string usuario_red
        {
            get { return _usuario_red; }
            set { _usuario_red = value; }
        }
        private string _nombre_equipo;

        public string nombre_equipo
        {
            get { return _nombre_equipo; }
            set { _nombre_equipo = value; }
        }
        private int _estado;

        public int estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        private string _asunto;

        public string asunto
        {
            get { return _asunto; }
            set { _asunto = value; }
        }

        private DateTime _fe_exceptuada;
        public DateTime fe_exceptuada
        {
            get { return _fe_exceptuada; }
            set { _fe_exceptuada = value; }
        }


        //-----------------------------------------------

        public string nu_placa
        {
            get { return _nu_placa; }
            set { _nu_placa = value; }
        }

        private string _nom_documento_cont;

        public string nom_documento_cont
        {
            get { return _nom_documento_cont; }
            set { _nom_documento_cont = value; }
        }

        private Nullable<Int32> _nid_propietario;
        private Nullable<Int32> _nid_marca;
        private Nullable<Int32> _nid_modelo;
        private string _co_marca;
        private string _co_modelo;
        private string _nombre;
        private Nullable<Int32> _tipo;
        private string _nu_vin;
        private string _co_taller;

        private string _coddpto;
        private string _codprov;
        private string _coddis;

        private string _cod_ubi;


        private Nullable<Int32> _nid_estado;
        private Nullable<Int32> _Nid_usuario;
        private Nullable<Int32> _nid_asesor;
        private Nullable<Int32> _nid_servicio;
        private string _cod_reserva_cita;
        private DateTime _fe_prog;
        private string _ho_prog;

        private string _tx_observacion;
        //-------------------------

        private string _no_taller;
        private string _no_asesor;
        private string _ho_inicio;
        private string _ho_fin;
        private string _ho_inicio_t;
        private string _ho_fin_t;
        private string _ho_inicio_a;
        private string _ho_fin_a;

        private string _no_estado;
        private string _Itm;
        private string _nu_ot;

        private string _fl_control;

        //---------------------------------------------
        private Nullable<Int32> _nid_tipo_servicio;
        private string _no_servicio;
        private string _no_tipo_servicio;
        private Nullable<Int32> _nid_ubica;
        private string _no_ubica;
        private string _di_ubica;
        private string _tx_mapa_taller;
        private string _no_distrito;
        private string _nu_telefono;
        private string _no_modelo;
        private string _no_marca;
        private string _no_nombre;
        private string _no_ape_paterno;
        private string _no_ape_materno;
        private Nullable<Int32> _co_tipo_doc;
        private string _nu_documento;
        private string _nu_documento_prop;
        private string _propietario;
        private string _nu_documento_cli;
        private string _cliente;
        private string _no_correo;
        private string _no_correo_trabajo;
        private string _no_correo_alter;
        private string _nu_tel_fijo;
        private string _nu_tel_movil;
        private string _co_estado_cita;
        private string _no_dpto;
        private Nullable<Int32> _nu_estado;

        private string _no_estado_cita;
        private string _fl_quick_service;
        private string _no_dias_validos;
        private string _qt_intervalo_atenc;
        private Nullable<Int32> _dd_atencion;
        private string _ho_taller_ini;
        private string _ho_taller_fin;

        private Nullable<Int32> _nid_regCita;
        private Nullable<Int32> _nid_parametro;
        private string _no_valor;
        private string _tex_verifica;

        private string _nomdpto;
        private string _nomprov;
        private string _nomdist;


        public string tex_verifica
        {
            get { return _tex_verifica; }
            set { _tex_verifica = value; }
        }
        private string _cod_tipo_persona;
        private string _des_tipo_persona;
        private string _cod_tipo_documento;
        private string _des_tipo_documento;
        private string _no_razon_social;
        private string _tipo_reg;

        public string Tipo_reg
        {
            get { return _tipo_reg; }
            set { _tipo_reg = value; }
        }
        //--------------------------------

        private string _co_usuario_crea;
        private string _co_usuario_red;
        private string _no_usuario_red;
        private string _no_estacion_red;
        private string _no_pais;

        //-------------------------------

        public string No_pais
        {
            get { return _no_pais; }
            set { _no_pais = value; }
        }

        private string _ho_rango1;
        private string _ho_rango2;
        private string _ho_rango3;

        private Int32 _nid_taller_empresa;
        private string _no_banco;
        private string _nu_cuenta;
        private string _nu_callcenter;
        private string _no_correo_callcenter;
        private string _fl_activo;
        private string _no_empresa;

        private string _seccion;
        private string _tipo_servicio;
        private string _co_repuesto;
        private string _des_repuesto_servicio;
        private string _estado_ot;

        private string _an_fabricacion;
        private string _nu_motor;
        private string _no_color_exterior;

        private string _fl_entrega;
        private string _tx_url_taller;

        #endregion


        #region " Parametros "


        public string tx_url_taller
        {
            get { return _tx_url_taller; }
            set { _tx_url_taller = value; }
        }

        public string fl_entrega
        {
            get { return _fl_entrega; }
            set { _fl_entrega = value; }
        }
        public string an_fabricacion
        {
            get { return _an_fabricacion; }
            set { _an_fabricacion = value; }
        }

        public string nu_motor
        {
            get { return _nu_motor; }
            set { _nu_motor = value; }
        }

        public string no_color_exterior
        {
            get { return _no_color_exterior; }
            set { _no_color_exterior = value; }
        }
        public string seccion
        {
            get { return _seccion; }
            set { _seccion = value; }
        }
        public string tipo_servicio
        {
            get { return _tipo_servicio; }
            set { _tipo_servicio = value; }
        }
        public string co_repuesto
        {
            get { return _co_repuesto; }
            set { _co_repuesto = value; }
        }
        public string des_repuesto_servicio
        {
            get { return _des_repuesto_servicio; }
            set { _des_repuesto_servicio = value; }
        }
        public string estado_ot
        {
            get { return _estado_ot; }
            set { _estado_ot = value; }
        }

        public string no_usuario_red
        {
            get { return _no_usuario_red; }
            set { _no_usuario_red = value; }
        }
        public string co_usuario_crea
        {
            get { return _co_usuario_crea; }
            set { _co_usuario_crea = value; }
        }
        public string co_usuario_red
        {
            get { return _co_usuario_red; }
            set { _co_usuario_red = value; }
        }
        public string no_estacion_red
        {
            get { return _no_estacion_red; }
            set { _no_estacion_red = value; }
        }

        public Nullable<Int32> nid_parametro
        {
            get { return _nid_parametro; }
            set { _nid_parametro = value; }
        }
        public string no_valor
        {
            get { return _no_valor; }
            set { _no_valor = value; }
        }


        public string _co_tipo_persona_prop;
        public string co_tipo_persona_prop
        {
            get { return _co_tipo_persona_prop; }
            set { _co_tipo_persona_prop = value; }
        }
        public string _co_tipo_documento_prop;
        public string co_tipo_documento_prop
        {
            get { return _co_tipo_documento_prop; }
            set { _co_tipo_documento_prop = value; }
        }
        //public string _nu_documento_prop;
        public string nu_documento_prop
        {
            get { return _nu_documento_prop; }
            set { _nu_documento_prop = value; }
        }
        public string _no_razon_social_prop;
        public string no_razon_social_prop
        {
            get { return _no_razon_social_prop; }
            set { _no_razon_social_prop = value; }
        }
        public string _no_cliente_prop;
        public string no_cliente_prop
        {
            get { return _no_cliente_prop; }
            set { _no_cliente_prop = value; }
        }
        public string _no_ape_pat_prop;
        public string no_ape_pat_prop
        {
            get { return _no_ape_pat_prop; }
            set { _no_ape_pat_prop = value; }
        }
        public string _no_ape_mat_prop;
        public string no_ape_mat_prop
        {
            get { return _no_ape_mat_prop; }
            set { _no_ape_mat_prop = value; }
        }
        public string _nu_telefono_prop;
        public string nu_telefono_prop
        {
            get { return _nu_telefono_prop; }
            set { _nu_telefono_prop = value; }
        }
        public string _nu_telefono2_prop;
        public string nu_telefono2_prop
        {
            get { return _nu_telefono2_prop; }
            set { _nu_telefono2_prop = value; }
        }
        public string _nu_celular_prop;
        public string nu_celular_prop
        {
            get { return _nu_celular_prop; }
            set { _nu_celular_prop = value; }
        }
        public string _nu_celular2_prop;
        public string nu_celular2_prop
        {
            get { return _nu_celular2_prop; }
            set { _nu_celular2_prop = value; }
        }
        public string _no_correo_prop;
        public string no_correo_prop
        {
            get { return _no_correo_prop; }
            set { _no_correo_prop = value; }
        }
        public string _no_correo_trab_prop;
        public string no_correo_trab_prop
        {
            get { return _no_correo_trab_prop; }
            set { _no_correo_trab_prop = value; }
        }
        public string _no_correo_alter_prop;
        public string no_correo_alter_prop
        {
            get { return _no_correo_alter_prop; }
            set { _no_correo_alter_prop = value; }
        }


        public string propietario
        {
            get { return _propietario; }
            set { _propietario = value; }
        }


        public string _co_tipo_persona_cli;
        public string co_tipo_persona_cli
        {
            get { return _co_tipo_persona_cli; }
            set { _co_tipo_persona_cli = value; }
        }
        public string _co_tipo_documento_cli;
        public string co_tipo_documento_cli
        {
            get { return _co_tipo_documento_cli; }
            set { _co_tipo_documento_cli = value; }
        }
        //public string _nu_documento_cli;
        public string nu_documento_cli
        {
            get { return _nu_documento_cli; }
            set { _nu_documento_cli = value; }
        }
        public string _no_razon_social_cli;
        public string no_razon_social_cli
        {
            get { return _no_razon_social_cli; }
            set { _no_razon_social_cli = value; }
        }
        public string _no_cliente_cli;
        public string no_cliente_cli
        {
            get { return _no_cliente_cli; }
            set { _no_cliente_cli = value; }
        }
        public string _no_ape_pat_cli;
        public string no_ape_pat_cli
        {
            get { return _no_ape_pat_cli; }
            set { _no_ape_pat_cli = value; }
        }
        public string _no_ape_mat_cli;
        public string no_ape_mat_cli
        {
            get { return _no_ape_mat_cli; }
            set { _no_ape_mat_cli = value; }
        }
        public string _nu_telefono_cli;
        public string nu_telefono_cli
        {
            get { return _nu_telefono_cli; }
            set { _nu_telefono_cli = value; }
        }
        public string _nu_telefono2_cli;
        public string nu_telefono2_cli
        {
            get { return _nu_telefono2_cli; }
            set { _nu_telefono2_cli = value; }
        }
        public string _nu_celular_cli;
        public string nu_celular_cli
        {
            get { return _nu_celular_cli; }
            set { _nu_celular_cli = value; }
        }
        public string _nu_celular2_cli;
        public string nu_celular2_cli
        {
            get { return _nu_celular2_cli; }
            set { _nu_celular2_cli = value; }
        }
        public string _no_correo_cli;
        public string no_correo_cli
        {
            get { return _no_correo_cli; }
            set { _no_correo_cli = value; }
        }
        public string _no_correo_trab_cli;
        public string no_correo_trab_cli
        {
            get { return _no_correo_trab_cli; }
            set { _no_correo_trab_cli = value; }
        }
        public string _no_correo_alter_cli;
        public string no_correo_alter_cli
        {
            get { return _no_correo_alter_cli; }
            set { _no_correo_alter_cli = value; }
        }

        public string _nu_telefono_c;
        public string nu_telefono_c
        {
            get { return _nu_telefono_c; }
            set { _nu_telefono_c = value; }
        }
        public string _nu_celular_c;
        public string nu_celular_c
        {
            get { return _nu_celular_c; }
            set { _nu_celular_c = value; }
        }


        public string cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }


        public string cod_tipo_persona
        {
            get { return _cod_tipo_persona; }
            set { _cod_tipo_persona = value; }
        }
        public string des_tipo_persona
        {
            get { return _des_tipo_persona; }
            set { _des_tipo_persona = value; }
        }
        public string cod_tipo_documento
        {
            get { return _cod_tipo_documento; }
            set { _cod_tipo_documento = value; }
        }
        public string des_tipo_documento
        {
            get { return _des_tipo_documento; }
            set { _des_tipo_documento = value; }
        }

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public Nullable<Int32> Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }


        public string cod_ubi
        {
            get { return _cod_ubi; }
            set { _cod_ubi = value; }
        }

        public string Co_taller
        {
            get { return _co_taller; }
            set { _co_taller = value; }
        }


        public string coddpto
        {
            get { return _coddpto; }
            set { _coddpto = value; }
        }


        public string codprov
        {
            get { return _codprov; }
            set { _codprov = value; }
        }


        public string coddis
        {
            get { return _coddis; }
            set { _coddis = value; }
        }


        public string nomdpto
        {
            get { return _nomdpto; }
            set { _nomdpto = value; }
        }

        public string nomprov
        {
            get { return _nomprov; }
            set { _nomprov = value; }
        }

        public string nomdist
        {
            get { return _nomdist; }
            set { _nomdist = value; }
        }



        public string co_modelo
        {
            get { return _co_modelo; }
            set { _co_modelo = value; }
        }

        public string Nu_vin
        {
            get { return _nu_vin; }
            set { _nu_vin = value; }
        }

        public string co_marca
        {
            get { return _co_marca; }
            set { _co_marca = value; }
        }
        public Nullable<Int32> nid_cita
        {
            get { return _nid_cita; }
            set { _nid_cita = value; }
        }


        public Nullable<Int32> nid_regCita
        {
            get { return _nid_regCita; }
            set { _nid_regCita = value; }
        }

        public Nullable<Int32> dia_prog
        {
            get { return _dia_prog; }
            set { _dia_prog = value; }
        }

        public Nullable<Int32> nid_cliente
        {
            get { return _nid_cliente; }
            set { _nid_cliente = value; }
        }

        public Nullable<Int32> nid_propietario
        {
            get { return _nid_propietario; }
            set { _nid_propietario = value; }
        }

        public Nullable<Int32> nid_taller
        {
            get { return _nid_taller; }
            set { _nid_taller = value; }
        }

        public Nullable<Int32> nid_Estado
        {
            get { return _nid_estado; }
            set { _nid_estado = value; }
        }

        public Nullable<Int32> Nid_usuario
        {
            get { return _Nid_usuario; }
            set { _Nid_usuario = value; }
        }

        public Nullable<Int32> nid_asesor
        {
            get { return _nid_asesor; }
            set { _nid_asesor = value; }
        }

        public Nullable<Int32> nid_Servicio
        {
            get { return _nid_servicio; }
            set { _nid_servicio = value; }
        }


        public string cod_reserva_cita
        {
            get { return _cod_reserva_cita; }
            set { _cod_reserva_cita = value; }
        }

        public DateTime fe_prog
        {
            get { return _fe_prog; }
            set { _fe_prog = value; }
        }

        public string ho_prog
        {
            get { return _ho_prog; }
            set { _ho_prog = value; }
        }

        public string tx_observacion
        {
            get { return _tx_observacion; }
            set { _tx_observacion = value; }
        }


        //--------------------------------------

        public string no_taller
        {
            get { return _no_taller; }
            set { _no_taller = value; }
        }

        private string _no_direccion;
        public string no_direccion
        {
            get { return _no_direccion; }
            set { _no_direccion = value; }
        }



        public string no_asesor
        {
            get { return _no_asesor; }
            set { _no_asesor = value; }
        }

        public string ho_inicio
        {
            get { return _ho_inicio; }
            set { _ho_inicio = value; }
        }

        public string ho_fin
        {
            get { return _ho_fin; }
            set { _ho_fin = value; }
        }

        public string ho_inicio_t
        {
            get { return _ho_inicio_t; }
            set { _ho_inicio_t = value; }
        }

        public string ho_fin_t
        {
            get { return _ho_fin_t; }
            set { _ho_fin_t = value; }
        }

        public string ho_inicio_a
        {
            get { return _ho_inicio_a; }
            set { _ho_inicio_a = value; }
        }

        public string ho_fin_a
        {
            get { return _ho_fin_a; }
            set { _ho_fin_a = value; }
        }

        private string _ho_inicio_c;
        public string ho_inicio_c
        {
            get { return _ho_inicio_c; }
            set { _ho_inicio_c = value; }
        }

        private string _ho_fin_c;
        public string ho_fin_c
        {
            get { return _ho_fin_c; }
            set { _ho_fin_c = value; }
        }

        private string _horario_asesor;
        public string horario_asesor
        {
            get { return _horario_asesor; }
            set { _horario_asesor = value; }
        }

        public string no_estado
        {
            get { return _no_estado; }
            set { _no_estado = value; }
        }

        public string fl_control
        {
            get { return _fl_control; }
            set { _fl_control = value; }
        }

        //---------------------
        public Nullable<Int32> nid_contacto_sr
        {
            get { return _nid_contacto_sr; }
            set { _nid_contacto_sr = value; }
        }

        public Nullable<Int32> nid_vehiculo
        {
            get { return _nid_vehiculo; }
            set { _nid_vehiculo = value; }
        }

        public string fl_origen
        {
            get { return _fl_origen; }
            set { _fl_origen = value; }
        }

        public Nullable<Int32> qt_km_inicial
        {
            get { return _qt_km_inicial; }
            set { _qt_km_inicial = value; }
        }

        public DateTime fe_atencion
        {
            get { return _fe_atencion; }
            set { _fe_atencion = value; }
        }

        public String fecha_atencion
        {
            get { return _fecha_atencion; }
            set { _fecha_atencion = value; }
        }

        public string Itm
        {
            get { return _Itm; }
            set { _Itm = value; }
        }

        public string nu_ot
        {
            get { return _nu_ot; }
            set { _nu_ot = value; }
        }

        public string tx_glos
        {
            get { return _tx_glos; }
            set { _tx_glos = value; }
        }


        //
        public Nullable<Int32> nid_modelo
        {
            get { return _nid_modelo; }
            set { _nid_modelo = value; }
        }

        public Nullable<Int32> nid_marca
        {
            get { return _nid_marca; }
            set { _nid_marca = value; }
        }



        public string qt_intervalo_atenc
        {
            get { return _qt_intervalo_atenc; }
            set { _qt_intervalo_atenc = value; }
        }
        public Nullable<Int32> dd_atencion
        {
            get { return _dd_atencion; }
            set { _dd_atencion = value; }
        }
        public string ho_taller_ini
        {
            get { return _ho_taller_ini; }
            set { _ho_taller_ini = value; }
        }
        public string ho_taller_fin
        {
            get { return _ho_taller_fin; }
            set { _ho_taller_fin = value; }
        }

        //-----------------
        public Nullable<Int32> nid_tipo_servicio
        {
            get { return _nid_tipo_servicio; }
            set { _nid_tipo_servicio = value; }
        }
        public Nullable<Int32> nu_estado
        {
            get { return _nu_estado; }
            set { _nu_estado = value; }
        }

        public string no_servicio
        {
            get { return _no_servicio; }
            set { _no_servicio = value; }
        }

        public string no_tipo_servicio
        {
            get { return _no_tipo_servicio; }
            set { _no_tipo_servicio = value; }
        }

        public Nullable<Int32> nid_ubica
        {
            get { return _nid_ubica; }
            set { _nid_ubica = value; }
        }

        public string tx_mapa_taller
        {
            get { return _tx_mapa_taller; }
            set { _tx_mapa_taller = value; }
        }

        public string no_ubica
        {
            get { return _no_ubica; }
            set { _no_ubica = value; }
        }

        public string di_ubica
        {
            get { return _di_ubica; }
            set { _di_ubica = value; }
        }

        public string no_distrito
        {
            get { return _no_distrito; }
            set { _no_distrito = value; }
        }

        public string nu_telefono
        {
            get { return _nu_telefono; }
            set { _nu_telefono = value; }
        }

        private string _nu_telefono_t;
        public string nu_telefono_t
        {
            get { return _nu_telefono_t; }
            set { _nu_telefono_t = value; }
        }

        private string _nu_telefono_a;
        public string nu_telefono_a
        {
            get { return _nu_telefono_a; }
            set { _nu_telefono_a = value; }
        }

        private string _no_correo_asesor;
        public string no_correo_asesor
        {
            get { return _no_correo_asesor; }
            set { _no_correo_asesor = value; }
        }

        public string no_modelo
        {
            get { return _no_modelo; }
            set { _no_modelo = value; }
        }

        public string no_marca
        {
            get { return _no_marca; }
            set { _no_marca = value; }
        }

        public string no_nombre
        {
            get { return _no_nombre; }
            set { _no_nombre = value; }
        }

        public string no_razon_social
        {
            get { return _no_razon_social; }
            set { _no_razon_social = value; }
        }


        public string no_ape_paterno
        {
            get { return _no_ape_paterno; }
            set { _no_ape_paterno = value; }
        }

        public string no_ape_materno
        {
            get { return _no_ape_materno; }
            set { _no_ape_materno = value; }
        }

        public Nullable<Int32> co_tipo_doc
        {
            get { return _co_tipo_doc; }
            set { _co_tipo_doc = value; }
        }

        public string nu_documento
        {
            get { return _nu_documento; }
            set { _nu_documento = value; }
        }

        public string no_cliente
        {
            get { return _no_cliente; }
            set { _no_cliente = value; }
        }

        public string nu_celular
        {
            get { return _nu_celular; }
            set { _nu_celular = value; }
        }
        public string co_tipo_cliente
        {
            get { return _co_tipo_cliente; }
            set { _co_tipo_cliente = value; }
        }
        public string doc_cliente
        {
            get { return _doc_cliente; }
            set { _doc_cliente = value; }
        }

        private string _no_correo_a;
        public string no_correo_a
        {
            get { return _no_correo_a; }
            set { _no_correo_a = value; }
        }

        public string no_correo
        {
            get { return _no_correo; }
            set { _no_correo = value; }
        }

        public string no_correo_trabajo
        {
            get { return _no_correo_trabajo; }
            set { _no_correo_trabajo = value; }
        }

        public string no_correo_alter
        {
            get { return _no_correo_alter; }
            set { _no_correo_alter = value; }
        }

        public string nu_tel_fijo
        {
            get { return _nu_tel_fijo; }
            set { _nu_tel_fijo = value; }
        }

        public string nu_tel_movil
        {
            get { return _nu_tel_movil; }
            set { _nu_tel_movil = value; }
        }

        private string _nu_celular_alter;
        public string nu_celular_alter
        {
            get { return _nu_celular_alter; }
            set { _nu_celular_alter = value; }
        }

        private string _nu_tel_oficina;
        public string nu_tel_oficina
        {
            get { return _nu_tel_oficina; }
            set { _nu_tel_oficina = value; }
        }

        /*
        public Nullable<Int32> nid_record_cita
        {
            get { return _nid_record_cita; }
            set { _nid_record_cita = value; }
        }

        public Nullable<Int32> co_tipo_record
        {
            get { return _co_tipo_record; }
            set { _co_tipo_record = value; }
        }

        public Nullable<Int32> dd_record
        {
            get { return _dd_record; }
            set { _dd_record = value; }
        }

        public string ho_record_ini
        {
            get { return _ho_record_ini; }
            set { _ho_record_ini = value; }
        }

        public string ho_record_fin
        {
            get { return _ho_record_fin; }
            set { _ho_record_fin = value; }
        }

        public string estado_record
        {
            get { return _estado_record; }
            set { _estado_record = value; }
        }
        */
        public string co_estado_cita
        {
            get { return _co_estado_cita; }
            set { _co_estado_cita = value; }
        }

        public string no_dpto
        {
            get { return _no_dpto; }
            set { _no_dpto = value; }
        }


        public string no_estado_cita
        {
            get { return _no_estado_cita; }
            set { _no_estado_cita = value; }
        }

        private Nullable<Int32> _co_intervalo_atenc;
        public Nullable<Int32> co_intervalo_atenc
        {
            get { return _co_intervalo_atenc; }
            set { _co_intervalo_atenc = value; }
        }

        public string fl_quick_service
        {
            get { return _fl_quick_service; }
            set { _fl_quick_service = value; }
        }


        public string no_dias_validos
        {
            get { return _no_dias_validos; }
            set { _no_dias_validos = value; }
        }

        public string AsesorServ
        {
            get { return _AsesorServ; }
            set { _AsesorServ = value; }
        }


        public string nom_estado
        {
            get { return _nom_estado; }
            set { _nom_estado = value; }
        }

        public string ho_rango1
        {
            get { return _ho_rango1; }
            set { _ho_rango1 = value; }
        }
        public string ho_rango2
        {
            get { return _ho_rango2; }
            set { _ho_rango2 = value; }
        }
        public string ho_rango3
        {
            get { return _ho_rango3; }
            set { _ho_rango3 = value; }
        }

        public Int32 nid_taller_empresa
        {
            get { return _nid_taller_empresa; }
            set { _nid_taller_empresa = value; }
        }

        public string nu_cuenta
        {
            get { return _nu_cuenta; }
            set { _nu_cuenta = value; }
        }

        public string nu_callcenter
        {
            get { return _nu_callcenter; }
            set { _nu_callcenter = value; }
        }

        public string no_banco
        {
            get { return _no_banco; }
            set { _no_banco = value; }
        }

        public string no_correo_callcenter
        {
            get { return _no_correo_callcenter; }
            set { _no_correo_callcenter = value; }
        }

        public string fl_activo
        {
            get { return _fl_activo; }
            set { _fl_activo = value; }
        }

        public string no_empresa
        {
            get { return _no_empresa; }
            set { _no_empresa = value; }
        }

        private string _fl_nota;
        public string fl_nota
        {
            get { return _fl_nota; }
            set { _fl_nota = value; }
        }



        #endregion

        public string no_nombreqr { get; set; } 
        public string co_tipo_cita { get; set; } 
        public string fl_recojounidad { get; set; } 

        
        public int nid_pais_celular { get; set; }
        public int nid_pais_telefono { get; set; }
        public string nu_anexo_telefono { get; set; }
        public int nid_pais_celular_cli { get; set; }
        public int nid_pais_telefono_cli { get; set; }
        public string nu_anexo_telefono_cli { get; set; }
        

    }

    [Serializable]
    public class CitasBEList : List<CitasBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            CitasBEComparer dc = new CitasBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class CitasBEComparer : IComparer<CitasBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public CitasBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(CitasBE x, CitasBE y)
        {
            /*if (!(x.GetType().ToString() == y.GetType().ToString()))
            {
                throw new ArgumentException("Objects must be of the same type");
            }*/

            PropertyInfo propertyX = x.GetType().GetProperty(_prop);
            PropertyInfo propertyY = y.GetType().GetProperty(_prop);

            object px = propertyX.GetValue(x, null);
            object py = propertyY.GetValue(y, null);

            if (px == null && py == null)
            {
                return 0;
            }
            else if (px != null && py == null)
            {
                if (_dir == direccionOrden.Ascending)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else if (px == null && py != null)
            {
                if (_dir == direccionOrden.Ascending)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else if (px.GetType().GetInterface("IComparable") != null)
            {
                if (_dir == direccionOrden.Ascending)
                {
                    return ((IComparable)px).CompareTo(py);
                }
                else
                {
                    return ((IComparable)py).CompareTo(px);
                }
            }
            else
            {
                return 0;
            }
        }
    }
}

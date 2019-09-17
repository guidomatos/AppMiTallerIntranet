using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class AdminCitaBE
    {
        #region "FALTAN"
        private int _nid_cita;

        public int nid_cita
        {
            get { return _nid_cita; }
            set { _nid_cita = value; }
        }

        private string _fl_activo;

        public string fl_activo
        {
            get { return _fl_activo; }
            set { _fl_activo = value; }
        }

        private int _bo_resultado;

        public int bo_resultado
        {
            get { return _bo_resultado; }
            set { _bo_resultado = value; }
        }

        private int _nid_usuario;

        public int nid_usuario
        {
            get { return _nid_usuario; }
            set { _nid_usuario = value; }
        }
	
        #endregion

        #region "FILTRO PANTALLA 1"
        private string _cod_reserva_cita;
        private string _coddpto;
        private string _codprov;
        private string _coddist;
        private int _nid_ubica;
        private int _nid_taller;
        private string _AsesorServicio;
        private string _Estadoreserva;
        private string _IndPendiente;
        private string _fecreg1;
        private string _fecreg2;
        private string _feccita1;
        private string _feccita2;
        private string _horacita1;
        private string _horacita2;
        private string _nu_placa;
        private int _nid_marca;
        private int _nid_modelo;
        private string _co_tipo_documento;
        private string _nu_documento;
        private string _no_cliente;
        private string _no_apellidos;

        
        private Int32 _paginacion;

        public Int32 paginacion
        {
            get { return _paginacion; }
            set { _paginacion = value; }
        }

        public string no_apellidos
        {
            get { return _no_apellidos; }
            set { _no_apellidos = value; }
        }


        public string no_cliente
        {
            get { return _no_cliente; }
            set { _no_cliente = value; }
        }


        public string nu_documento
        {
            get { return _nu_documento; }
            set { _nu_documento = value; }
        }


        public string co_tipo_documento
        {
            get { return _co_tipo_documento; }
            set { _co_tipo_documento = value; }
        }


        public int nid_modelo
        {
            get { return _nid_modelo; }
            set { _nid_modelo = value; }
        }

        public int nid_marca
        {
            get { return _nid_marca; }
            set { _nid_marca = value; }
        }


        public string nu_placa
        {
            get { return _nu_placa; }
            set { _nu_placa = value; }
        }


        public string horacita2
        {
            get { return _horacita2; }
            set { _horacita2 = value; }
        }


        public string horacita1
        {
            get { return _horacita1; }
            set { _horacita1 = value; }
        }


        public string feccita2
        {
            get { return _feccita2; }
            set { _feccita2 = value; }
        }



        public string feccita1
        {
            get { return _feccita1; }
            set { _feccita1 = value; }
        }


        public string fecreg2
        {
            get { return _fecreg2; }
            set { _fecreg2 = value; }
        }


        public string fecreg1
        {
            get { return _fecreg1; }
            set { _fecreg1 = value; }
        }


        public string IndPendiente
        {
            get { return _IndPendiente; }
            set { _IndPendiente = value; }
        }


        public string Estadoreserva
        {
            get { return _Estadoreserva; }
            set { _Estadoreserva = value; }
        }


        public string AsesorServicio
        {
            get { return _AsesorServicio; }
            set { _AsesorServicio = value; }
        }


        public int nid_taller
        {
            get { return _nid_taller; }
            set { _nid_taller = value; }
        }


        public int nid_ubica
        {
            get { return _nid_ubica; }
            set { _nid_ubica = value; }
        }


        public string coddist
        {
            get { return _coddist; }
            set { _coddist = value; }
        }


        public string codprov
        {
            get { return _codprov; }
            set { _codprov = value; }
        }


        public string coddpto
        {
            get { return _coddpto; }
            set { _coddpto = value; }
        }


        public string cod_reserva_cita
        {
            get { return _cod_reserva_cita; }
            set { _cod_reserva_cita = value; }
        }
        #endregion

        #region "GRID PANTALLA1"
        private string _grid_nid_cita;
        private string _grid_cod_reserva_cita;
        private string _grid_FE_HORA_REG;
        private string _grid_FECHA_CITA;
        private string _grid_HORA_CITA;
        private string _grid_ESTADO_CITA;
        private string _grid_Departamento;
        private string _grid_Provincia;
        private string _grid_Distrito;
        private string _grid_Punto_RED;
        private string _grid_Taller;
        private string _grid_AsesorServicio;
        private string _grid_PlacaPatente;
        private string _grid_NumDocumento;
        private string _grid_NomCliente;
        private string _grid_ApeCliente;
        private string _grid_IndPendiente;
        private string _grid_nid_estado;
        private string _grid_TelefonoCliente;
        private string _grid_EmailCliente;
        private string _grid_nid_tallerCita;
        private string _grid_nid_servicioCita;
        private string _grid_no_dias_validos;        
        private string _grid_IntervaloTaller;
        private string _grid_Id_Asesor;
        private string _grid_HORA_CITA_FIN;
        private string _grid_nid_modelo;
        private string _grid_co_usuario_crea;
        private string _grid_co_usuario_modi;

        public string grid_nid_modelo
        {
            get { return _grid_nid_modelo; }
            set { _grid_nid_modelo = value; }
        }
	

        public string grid_HORA_CITA_FIN
        {
            get { return _grid_HORA_CITA_FIN; }
            set { _grid_HORA_CITA_FIN = value; }
        }
	

        public string grid_Id_Asesor
        {
            get { return _grid_Id_Asesor; }
            set { _grid_Id_Asesor = value; }
        }

        public string grid_IntervaloTaller
        {
            get { return _grid_IntervaloTaller; }
            set { _grid_IntervaloTaller = value; }
        }

        public string grid_no_dias_validos
        {
            get { return _grid_no_dias_validos; }
            set { _grid_no_dias_validos = value; }
        }

        public string grid_nid_servicioCita
        {
            get { return _grid_nid_servicioCita; }
            set { _grid_nid_servicioCita = value; }
        }

        public string grid_nid_tallerCita
        {
            get { return _grid_nid_tallerCita; }
            set { _grid_nid_tallerCita = value; }
        }

        public string grid_TelefonoCliente
        {
            get { return _grid_TelefonoCliente; }
            set { _grid_TelefonoCliente = value; }
        }

        public string grid_EmailCliente
        {
            get { return _grid_EmailCliente; }
            set { _grid_EmailCliente = value; }
        }

        public string grid_nid_estado
        {
            get { return _grid_nid_estado; }
            set { _grid_nid_estado = value; }
        }

        public string grid_IndPendiente
        {
            get { return _grid_IndPendiente; }
            set { _grid_IndPendiente = value; }
        }


        public string grid_ApeCliente
        {
            get { return _grid_ApeCliente; }
            set { _grid_ApeCliente = value; }
        }


        public string grid_NomCliente
        {
            get { return _grid_NomCliente; }
            set { _grid_NomCliente = value; }
        }

        public string grid_NumDocumento
        {
            get { return _grid_NumDocumento; }
            set { _grid_NumDocumento = value; }
        }

        public string grid_PlacaPatente
        {
            get { return _grid_PlacaPatente; }
            set { _grid_PlacaPatente = value; }
        }


        public string grid_AsesorServicio
        {
            get { return _grid_AsesorServicio; }
            set { _grid_AsesorServicio = value; }
        }


        public string grid_Taller
        {
            get { return _grid_Taller; }
            set { _grid_Taller = value; }
        }


        public string grid_Punto_RED
        {
            get { return _grid_Punto_RED; }
            set { _grid_Punto_RED = value; }
        }


        public string grid_Distrito
        {
            get { return _grid_Distrito; }
            set { _grid_Distrito = value; }
        }


        public string grid_Provincia
        {
            get { return _grid_Provincia; }
            set { _grid_Provincia = value; }
        }


        public string grid_Departamento
        {
            get { return _grid_Departamento; }
            set { _grid_Departamento = value; }
        }


        public string grid_ESTADO_CITA
        {
            get { return _grid_ESTADO_CITA; }
            set { _grid_ESTADO_CITA = value; }
        }


        public string grid_HORA_CITA
        {
            get { return _grid_HORA_CITA; }
            set { _grid_HORA_CITA = value; }
        }


        public string grid_FECHA_CITA
        {
            get { return _grid_FECHA_CITA; }
            set { _grid_FECHA_CITA = value; }
        }


        public string grid_FE_HORA_REG
        {
            get { return _grid_FE_HORA_REG; }
            set { _grid_FE_HORA_REG = value; }
        }


        public string grid_cod_reserva_cita
        {
            get { return _grid_cod_reserva_cita; }
            set { _grid_cod_reserva_cita = value; }
        }


        public string grid_nid_cita
        {
            get { return _grid_nid_cita; }
            set { _grid_nid_cita = value; }
        }


        public string grid_co_usuario_crea
        {
            get { return _grid_co_usuario_crea; }
            set { _grid_co_usuario_crea = value; }
        }

        public string grid_co_usuario_modi
        {
            get { return _grid_co_usuario_modi; }
            set { _grid_co_usuario_modi = value; }
        }

        private string _grid_no_servicio;
        public string grid_no_servicio
        {
            get { return _grid_no_servicio; }
            set { _grid_no_servicio = value; }
        }

        private string _grid_no_marca;
        public string grid_no_marca
        {
            get { return _grid_no_marca; }
            set { _grid_no_marca = value; }
        }
        private string _grid_no_modelo;
        public string grid_no_modelo
        {
            get { return _grid_no_modelo; }
            set { _grid_no_modelo = value; }
        }
        private string _grid_nu_vin;
        public string grid_nu_vin
        {
            get { return _grid_nu_vin; }
            set { _grid_nu_vin = value; }
        }

        #endregion

        #region "VER DETALLE"
        private string _DET_Fecha;
        private string _DET_Hora;
        private string _DET_AsesorServicio;
        private string _DET_Ubicacion;
        private string _DET_Nombre;
        private string _DET_ApePat;
        private string _DET_ApeMat;
        private string _DET_DNI;
        private string _DET_TelfFijo;
        private string _DET_TelfOficina;
        private string _DET_TelfMovil;
        private string _DET_TelfMovil2;
        private string _DET_Email;
        private string _DET_Email_Trab;
        private string _DET_Email_Alter;
        private string _DET_Placa;
        private string _DET_Marca;
        private string _DET_Modelo;
        private string _DET_Observaciones;
        private string _DET_CodigoReserva;
        private string _DET_Servicio;
        private string _DET_TipoServicio;
        private string _DET_Estado;

        public string DET_Estado
        {
            get { return _DET_Estado; }
            set { _DET_Estado = value; }
        }


        public string DET_Observaciones
        {
            get { return _DET_Observaciones; }
            set { _DET_Observaciones = value; }
        }

        public string DET_Modelo
        {
            get { return _DET_Modelo; }
            set { _DET_Modelo = value; }
        }

        public string DET_Marca
        {
            get { return _DET_Marca; }
            set { _DET_Marca = value; }
        }

        public string DET_Placa
        {
            get { return _DET_Placa; }
            set { _DET_Placa = value; }
        }

        public string DET_Email
        {
            get { return _DET_Email; }
            set { _DET_Email = value; }
        }

        public string DET_Email_Trab
        {
            get { return _DET_Email_Trab; }
            set { _DET_Email_Trab = value; }
        }

        public string DET_Email_Alter
        {
            get { return _DET_Email_Alter; }
            set { _DET_Email_Alter = value; }
        }


        public string DET_TelfMovil
        {
            get { return _DET_TelfMovil; }
            set { _DET_TelfMovil = value; }
        }

        public string DET_TelfMovil2
        {
            get { return _DET_TelfMovil2; }
            set { _DET_TelfMovil2 = value; }
        }

        public string DET_TelfFijo
        {
            get { return _DET_TelfFijo; }
            set { _DET_TelfFijo = value; }
        }

        public string DET_TelfOficina
        {
            get { return _DET_TelfOficina; }
            set { _DET_TelfOficina = value; }
        }

        public string DET_DNI
        {
            get { return _DET_DNI; }
            set { _DET_DNI = value; }
        }

        public string DET_ApePat
        {
            get { return _DET_ApePat; }
            set { _DET_ApePat = value; }
        }

        public string DET_ApeMat
        {
            get { return _DET_ApeMat; }
            set { _DET_ApeMat = value; }
        }

        public string DET_Nombre
        {
            get { return _DET_Nombre; }
            set { _DET_Nombre = value; }
        }

        public string DET_Ubicacion
        {
            get { return _DET_Ubicacion; }
            set { _DET_Ubicacion = value; }
        }
        
        public string DET_CodigoReserva
        {
            get { return _DET_CodigoReserva; }
            set { _DET_CodigoReserva = value; }
        }
        
        public string DET_Servicio
        {
            get { return _DET_Servicio; }
            set { _DET_Servicio = value; }
        }

        public string DET_TipoServicio
        {
            get { return _DET_TipoServicio; }
            set { _DET_TipoServicio = value; }
        }        
                
        public string DET_AsesorServicio
        {
            get { return _DET_AsesorServicio; }
            set { _DET_AsesorServicio = value; }
        }

        public string DET_Hora
        {
            get { return _DET_Hora; }
            set { _DET_Hora = value; }
        }

        public string DET_Fecha
        {
            get { return _DET_Fecha; }
            set { _DET_Fecha = value; }
        }
        #endregion

        #region "EDITAR RECORDATORIO"
        private int _CTRECOR_co_tipo_record;
        private int _CTRECOR_dd_record;
        private string _CTRECOR_ho_record_ini;
        private string _CTRECOR_ho_record_fin;
        private string _CTRECOR_co_usuario_modi;
        private string _CTRECOR_co_usuario_red;
        private string _CTRECOR_no_estacion_red;

        public int CTRECOR_co_tipo_record
        {
            get { return _CTRECOR_co_tipo_record; }
            set { _CTRECOR_co_tipo_record = value; }
        }

        public int CTRECOR_dd_record
        {
            get { return _CTRECOR_dd_record; }
            set { _CTRECOR_dd_record = value; }
        }

        public string CTRECOR_ho_record_ini
        {
            get { return _CTRECOR_ho_record_ini; }
            set { _CTRECOR_ho_record_ini = value; }
        }

        public string CTRECOR_ho_record_fin
        {
            get { return _CTRECOR_ho_record_fin; }
            set { _CTRECOR_ho_record_fin = value; }
        }

        public string CTRECOR_co_usuario_modi
        {
            get { return _CTRECOR_co_usuario_modi; }
            set { _CTRECOR_co_usuario_modi = value; }
        }

        public string CTRECOR_co_usuario_red
        {
            get { return _CTRECOR_co_usuario_red; }
            set { _CTRECOR_co_usuario_red = value; }
        }

        public string CTRECOR_no_estacion_red
        {
            get { return _CTRECOR_no_estacion_red; }
            set { _CTRECOR_no_estacion_red = value; }
        }
        #endregion

        #region "VER VEHICULO PROPIETARIO"
        private string _DV_Placa;
        private int _DV_Marca;
        private int _DV_Modelo;
        private int _DV_NidVin;
        private string _DV_NroVin;
        private string _DP_Paterno;
        private string _DP_Materno;
        private string _DP_Nombre;
        private string _DP_TipoDoc;
        private string _DP_NroDoc;
        private string _DP_NroTel;
        private string _DP_NroCel;
        private string _DP_Email;
        private string _DC_Paterno;
        private string _DC_Materno;
        private string _DC_Nombre;
        private string _DC_TipoDoc;
        private string _DC_NroDoc;
        private string _DC_NroTel;
        private string _DC_NroCel;
        private string _DC_Email;

        public string DV_Placa
        {
            get { return _DV_Placa; }
            set { _DV_Placa = value; }
        }

        public int DV_Marca
        {
            get { return _DV_Marca; }
            set { _DV_Marca = value; }
        }

        public int DV_Modelo
        {
            get { return _DV_Modelo; }
            set { _DV_Modelo = value; }
        }

        public int DV_NidVin
        {
            get { return _DV_NidVin; }
            set { _DV_NidVin = value; }
        }

        public string DV_NroVin
        {
            get { return _DV_NroVin; }
            set { _DV_NroVin = value; }
        }

        public string DP_Paterno
        {
            get { return _DP_Paterno; }
            set { _DP_Paterno = value; }
        }

        public string DP_Materno
        {
            get { return _DP_Materno; }
            set { _DP_Materno = value; }
        }

        public string DP_Nombre
        {
            get { return _DP_Nombre; }
            set { _DP_Nombre = value; }
        }

        public string DP_TipoDoc
        {
            get { return _DP_TipoDoc; }
            set { _DP_TipoDoc = value; }
        }

        public string DP_NroDoc
        {
            get { return _DP_NroDoc; }
            set { _DP_NroDoc = value; }
        }

        public string DP_NroTel
        {
            get { return _DP_NroTel; }
            set { _DP_NroTel = value; }
        }

        public string DP_NroCel
        {
            get { return _DP_NroCel; }
            set { _DP_NroCel = value; }
        }

        public string DP_Email
        {
            get { return _DP_Email; }
            set { _DP_Email = value; }
        }

        public string DC_Paterno
        {
            get { return _DC_Paterno; }
            set { _DC_Paterno = value; }
        }

        public string DC_Materno
        {
            get { return _DC_Materno; }
            set { _DC_Materno = value; }
        }

        public string DC_Nombre
        {
            get { return _DC_Nombre; }
            set { _DC_Nombre = value; }
        }

        public string DC_TipoDoc
        {
            get { return _DC_TipoDoc; }
            set { _DC_TipoDoc = value; }
        }

        public string DC_NroDoc
        {
            get { return _DC_NroDoc; }
            set { _DC_NroDoc = value; }
        }

        public string DC_NroTel
        {
            get { return _DC_NroTel; }
            set { _DC_NroTel = value; }
        }

        public string DC_NroCel
        {
            get { return _DC_NroCel; }
            set { _DC_NroCel = value; }
        }

        public string DC_Email
        {
            get { return _DC_Email; }
            set { _DC_Email = value; }
        }
        #endregion

        #region "VARIABLES CITA COLA ESPERA"
        private string _COESP_vi_cod_reserva;

        public string COESP_vi_cod_reserva
        {
            get { return _COESP_vi_cod_reserva; }
            set { _COESP_vi_cod_reserva = value; }
        }

        private int _COESP_vi_nid_usuario;

        public int COESP_vi_nid_usuario
        {
            get { return _COESP_vi_nid_usuario; }
            set { _COESP_vi_nid_usuario = value; }
        }

        private string _COESP_vi_ho_inicio;

        public string COESP_vi_ho_inicio
        {
            get { return _COESP_vi_ho_inicio; }
            set { _COESP_vi_ho_inicio = value; }
        }

        private string _COESP_vi_ho_fin;

        public string COESP_vi_ho_fin
        {
            get { return _COESP_vi_ho_fin; }
            set { _COESP_vi_ho_fin = value; }
        }

        private DateTime _COESP_vi_fe_programada;

        public DateTime COESP_vi_fe_programada
        {
            get { return _COESP_vi_fe_programada; }
            set { _COESP_vi_fe_programada = value; }
        }

        private int _COESP_vo_resultado;

        public int COESP_vo_resultado
        {
            get { return _COESP_vo_resultado; }
            set { _COESP_vo_resultado = value; }
        }
	
        #endregion

        public Int32 fl_identidad_validada { get; set; } 

        public string no_nombreqr { get; set; } 
        
        public int nid_pais_celular { get; set; }
        public int nid_pais_telefono { get; set; }
        public string nu_anexo_telefono { get; set; }
        
    }

    [Serializable]
    public class AdminCitaBEList : List<AdminCitaBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            AdminCitaBEComparer dc = new AdminCitaBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class AdminCitaBEComparer : IComparer<AdminCitaBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public AdminCitaBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(AdminCitaBE x, AdminCitaBE y)
        {

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

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class UsuarioBE
    {
        #region "Metodos"
        public String GetMD5(String input)
        {
            String retorno = String.Empty;
            UsuarioBE obj = new UsuarioBE();
            retorno = obj.ComputeHash(input);
            //MD5HashAlgorithm oCrip = new MD5HashAlgorithm();
            //retorno = oCrip.ComputeHash(input);
            obj = null;
            return retorno;
        }

        public String LoginGetMD5(String input)
        {
            String retorno = String.Empty;

            MD5HashAlgorithm oCrip = new MD5HashAlgorithm();
            retorno = oCrip.ComputeHash(input);
            oCrip = null;

            return retorno;
        }


        public Byte[] ConvertStringToByteArray(String s)
        {
            return (new UnicodeEncoding()).GetBytes(s);
        }


        public string ComputeHash(string DataToHash)
        {
            Byte[] valueToHash = ConvertStringToByteArray(DataToHash);
            byte[] hashvalue = (new MD5CryptoServiceProvider()).ComputeHash(valueToHash);
            return BitConverter.ToString(hashvalue);
        }


        #endregion

        public Int32 qt_capacidad { get; set; }
        public string fl_control { get; set; }
        public Int32 nu_modulo { get; set; }

        private String _Co_Perfil_Login;

        public String Co_Perfil_Login
        {
            get { return _Co_Perfil_Login; }
            set { _Co_Perfil_Login = value; }
        }

        private Int32 _Nid_usuario_Login;

        public Int32 Nid_usuario_Login
        {
            get { return _Nid_usuario_Login; }
            set { _Nid_usuario_Login = value; }
        }

        private String _VPASSMD5;

        public String VPASSMD5
        {
            get { return _VPASSMD5; }
            set { _VPASSMD5 = value; }
        }

        private String _op;

        public String Op
        {
            get { return _op; }
            set { _op = value; }
        }

        private String _co_usuario_cambio;

        public String Co_usuario_cambio
        {
            get { return _co_usuario_cambio; }
            set { _co_usuario_cambio = value; }
        }

        private String _CCOAPL;

        public String CCOAPL
        {
            get { return _CCOAPL; }
            set { _CCOAPL = value; }
        }

        private String _no_usuario;

        public String No_usuario
        {
            get { return _no_usuario; }
            set { _no_usuario = value; }
        }

        private Int32 _nid_perfil;

        public Int32 Nid_perfil
        {
            get { return _nid_perfil; }
            set { _nid_perfil = value; }
        }

        private String _fe_inicio_acceso1;

        public String Fe_inicio_acceso1
        {
            get { return _fe_inicio_acceso1; }
            set { _fe_inicio_acceso1 = value; }
        }
        private String _fe_fin_acceso1;

        public String Fe_fin_acceso1
        {
            get { return _fe_fin_acceso1; }
            set { _fe_fin_acceso1 = value; }
        }

        private DateTime _fe_exceptuada;

        public DateTime Fe_exceptuada
        {
            get { return _fe_exceptuada; }
            set { _fe_exceptuada = value; }
        }


        private Int32 _dd_atencion;

        public Int32 Dd_atencion
        {
            get { return _dd_atencion; }
            set { _dd_atencion = value; }
        }

        private String _ho_inicio;

        public String Ho_inicio
        {
            get { return _ho_inicio; }
            set { _ho_inicio = value; }
        }

        private String _ho_fin;

        public String Ho_fin
        {
            get { return _ho_fin; }
            set { _ho_fin = value; }
        }
        private Int32 _qt_capacidad_fo;
        public Int32 qt_capacidad_fo
        {
            get { return _qt_capacidad_fo; }
            set { _qt_capacidad_fo = value; }
        }

        private Int32 _qt_capacidad_bo;
        public Int32 qt_capacidad_bo
        {
            get { return _qt_capacidad_bo; }
            set { _qt_capacidad_bo = value; }
        }

        private String _fl_tipo;
        public String Fl_tipo
        {
            get { return _fl_tipo; }
            set { _fl_tipo = value; }
        }

        private String _co_usuario_red;

        public String Co_usuario_red
        {
            get { return _co_usuario_red; }
            set { _co_usuario_red = value; }
        }

        private String _VMSGBLQ;

        public String VMSGBLQ
        {
            get { return _VMSGBLQ; }
            set { _VMSGBLQ = value; }
        }

        private String _CESTBLQ;

        public String CESTBLQ
        {
            get { return _CESTBLQ; }
            set { _CESTBLQ = value; }
        }

        private Int32 _nid_tipo_servicio;

        public Int32 Nid_tipo_servicio
        {
            get { return _nid_tipo_servicio; }
            set { _nid_tipo_servicio = value; }
        }

        private String _no_tipo_servicio;

        public String No_tipo_servicio
        {
            get { return _no_tipo_servicio; }
            set { _no_tipo_servicio = value; }
        }

        private Int32 _nid_servicio;

        public Int32 Nid_servicio
        {
            get { return _nid_servicio; }
            set { _nid_servicio = value; }
        }

        private String _no_servicio;

        public String No_servicio
        {
            get { return _no_servicio; }
            set { _no_servicio = value; }
        }

        private String _no_valor1;

        public String No_valor1
        {
            get { return _no_valor1; }
            set { _no_valor1 = value; }
        }

        private String _strID;

        public String StrID
        {
            get { return _strID; }
            set { _strID = value; }
        }

        private Int32 _intID;

        public Int32 IntID
        {
            get { return _intID; }
            set { _intID = value; }
        }

        private String _DES;

        public String DES
        {
            get { return _DES; }
            set { _DES = value; }
        }

        private Int32 _Nid_usuario;

        public Int32 Nid_usuario
        {
            get { return _Nid_usuario; }
            set { _Nid_usuario = value; }
        }
        private String _no_ape_paterno;

        public String No_ape_paterno
        {
            get { return _no_ape_paterno; }
            set { _no_ape_paterno = value; }
        }
        private String _no_ape_materno;

        public String No_ape_materno
        {
            get { return _no_ape_materno; }
            set { _no_ape_materno = value; }
        }
        private String _VNOMUSR;

        public String VNOMUSR
        {
            get { return _VNOMUSR; }
            set { _VNOMUSR = value; }
        }
        private String _CUSR_ID;

        public String CUSR_ID
        {
            get { return _CUSR_ID; }
            set { _CUSR_ID = value; }
        }
        private Int32 _nid_ubica;

        public Int32 Nid_ubica
        {
            get { return _nid_ubica; }
            set { _nid_ubica = value; }
        }
        
        private String _nu_tipo_documento;

        public String Nu_tipo_documento
        {
            get { return _nu_tipo_documento; }
            set { _nu_tipo_documento = value; }
        }
        private String _fl_activo;

        public String Fl_activo
        {
            get { return _fl_activo; }
            set { _fl_activo = value; }
        }
        private String _coddpto;

        public String Coddpto
        {
            get { return _coddpto; }
            set { _coddpto = value; }
        }

        private String _dpto;

        public String Dpto
        {
            get { return _dpto; }
            set { _dpto = value; }
        }

        private String _codprov;

        public String Codprov
        {
            get { return _codprov; }
            set { _codprov = value; }
        }

        private String _prov;

        public String Prov
        {
            get { return _prov; }
            set { _prov = value; }
        }

        private String _coddist;

        public String Coddist
        {
            get { return _coddist; }
            set { _coddist = value; }
        }

        private String _dist;

        public String Dist
        {
            get { return _dist; }
            set { _dist = value; }
        }

        private String ubigeo;

        public String Ubigeo
        {
            get { return ubigeo; }
            set { ubigeo = value; }
        }

        private Int32 _nid_taller;

        public Int32 nid_taller
        {
            get { return _nid_taller; }
            set { _nid_taller = value; }
        }

        private String _no_taller;

        public String No_taller
        {
            get { return _no_taller; }
            set { _no_taller = value; }
        }

        private String _cod_perfil;

        public String Cod_perfil
        {
            get { return _cod_perfil; }
            set { _cod_perfil = value; }
        }

        private String perfil;

        public String Perfil
        {
            get { return perfil; }
            set { perfil = value; }
        }

        private String _tip_ubica;

        public String Tip_ubica
        {
            get { return _tip_ubica; }
            set { _tip_ubica = value; }
        }

        private String _no_tip_ubica;

        public String No_tip_ubica
        {
            get { return _no_tip_ubica; }
            set { _no_tip_ubica = value; }
        }

        private String _no_ubica;

        public String No_ubica
        {
            get { return _no_ubica; }
            set { _no_ubica = value; }
        }

        private Int32 _nid_marca;

        public Int32 Nid_marca
        {
            get { return _nid_marca; }
            set { _nid_marca = value; }
        }

        private String _no_marca;

        public String No_marca
        {
            get { return _no_marca; }
            set { _no_marca = value; }
        }

        private Int32 _nid_empresa;

        public Int32 Nid_empresa
        {
            get { return _nid_empresa; }
            set { _nid_empresa = value; }
        }

        private String _no_empresa;

        public String No_empresa
        {
            get { return _no_empresa; }
            set { _no_empresa = value; }
        }

        private Int32 _nid_negocio_linea;

        public Int32 Nid_negocio_linea
        {
            get { return _nid_negocio_linea; }
            set { _nid_negocio_linea = value; }
        }

        private String _linea_comercial;

        public String Linea_comercial
        {
            get { return _linea_comercial; }
            set { _linea_comercial = value; }
        }

        private Int32 _nid_modelo;

        public Int32 Nid_modelo
        {
            get { return _nid_modelo; }
            set { _nid_modelo = value; }
        }

        private String _no_modelo;

        public String No_modelo
        {
            get { return _no_modelo; }
            set { _no_modelo = value; }
        }

        private String _VUSR_PASS;

        public String VUSR_PASS
        {
            get { return _VUSR_PASS; }
            set { _VUSR_PASS = value; }
        }

        private String _VTELEFONO;

        public String VTELEFONO
        {
            get { return _VTELEFONO; }
            set { _VTELEFONO = value; }
        }

        private Int32 _nid_cod_tipo_usuario;

        public Int32 Nid_cod_tipo_usuario
        {
            get { return _nid_cod_tipo_usuario; }
            set { _nid_cod_tipo_usuario = value; }
        }

        private DateTime _fe_inicio_acceso;

        public DateTime Fe_inicio_acceso
        {
            get { return _fe_inicio_acceso; }
            set { _fe_inicio_acceso = value; }
        }

        private DateTime _fe_fin_acceso;

        public DateTime Fe_fin_acceso
        {
            get { return _fe_fin_acceso; }
            set { _fe_fin_acceso = value; }
        }

        private String _hr_inicio_acceso;

        public String Hr_inicio_acceso
        {
            get { return _hr_inicio_acceso; }
            set { _hr_inicio_acceso = value; }
        }

        private String _hr_fin_acceso;

        public String Hr_fin_acceso
        {
            get { return _hr_fin_acceso; }
            set { _hr_fin_acceso = value; }
        }


        private String _VCORREO;

        public String VCORREO
        {
            get { return _VCORREO; }
            set { _VCORREO = value; }
        }

        private String _fl_inactivo;

        public String Fl_inactivo
        {
            get { return _fl_inactivo; }
            set { _fl_inactivo = value; }
        }

        private String _co_usuario_crea;

        public String Co_usuario_crea
        {
            get { return _co_usuario_crea; }
            set { _co_usuario_crea = value; }
        }

        private String _no_estacion_red;

        public String No_estacion_red
        {
            get { return _no_estacion_red; }
            set { _no_estacion_red = value; }
        }

        private String _no_usuario_red;

        public String No_usuario_red
        {
            get { return _no_usuario_red; }
            set { _no_usuario_red = value; }
        }

        public string va_correo {get; set; }
        public string nro_telf { get; set; }


        #region "Atributos"
        private String _NO_APE_PATERNO;
        private String _NO_APE_MATERNO;
        private String _VUSR_PASS_MD5;
        private Int32 _NID_UBICA;
        private String _VUBICACION;
        private Int32 _NID_TIPO;
        private String _CUSR_TIPO;
        private String _VUSR_TIPO;
        private Int32 _NID_PERFIL;
        private String _VUSR_PERFIL;
        private Int32 _NID_ROL;
        private DateTime _FE_INICIO_ACCESO;
        private String _SFE_INICIO_ACCESO;
        private DateTime _FE_FIN_ACCESO;
        private String _SFE_FIN_ACCESO;
        private String _HR_INICIO_ACCESO;
        private String _HR_FIN_ACCESO;
        private String _NU_TIPO_DOCUMENTO;
        private Int32 _CO_TIPO_VENDEDOR;
        private String _FL_INACTIVO;
        private String _SFL_INACTIVO;
        private String _CO_USUARIO_CREA;
        private String _CO_USUARIO_CAMBIO;
        private String _NO_USUARIO_RED;
        private String _NO_ESTACION_RED;
        private DateTime _FE_INICIO_ACCESO_PERFIL;
        private String _SFE_INICIO_ACCESO_PERFIL;
        private DateTime _FE_FIN_ACCESO_PERFIL;
        private String _SFE_FIN_ACCESO_PERFIL;
        private String _HR_INICIO_ACCESO_PERFIL;
        private String _HR_FIN_ACCESO_PERFIL;
        private String _fl_reset;
        private String _passwordDesEnc;
        private String _co_perfil_usuario;
        private String _VNOMUSR_CUSR_ID;

        public String no_ubica_corto { get; set; }
        public int int_dias_caducidad { get; set; }
        #endregion


        public String VNOMUSR_CUSR_ID
        {
            get { return _VNOMUSR_CUSR_ID; }
            set { _VNOMUSR_CUSR_ID = value; }
        }
        public String co_perfil_usuario
        {
            get { return this._co_perfil_usuario; }
            set { this._co_perfil_usuario = value; }
        }
        public String passwordDesEnc
        {
            get { return this._passwordDesEnc; }
            set { this._passwordDesEnc = value; }
        }
        public String NO_APE_PATERNO
        {
            get { return this._NO_APE_PATERNO; }
            set { this._NO_APE_PATERNO = value; }
        }
        public String NO_APE_MATERNO
        {
            get { return this._NO_APE_MATERNO; }
            set { this._NO_APE_MATERNO = value; }
        }
        public String VUSR_PASS_MD5
        {
            get { return this._VUSR_PASS_MD5; }
            set { this._VUSR_PASS_MD5 = value; }
        }
        
        public Int32 NID_UBICA
        {
            get { return this._NID_UBICA; }
            set { this._NID_UBICA = value; }
        }
        public String VUBICACION
        {
            get { return this._VUBICACION; }
            set { this._VUBICACION = value; }
        }
        public String CUSR_TIPO
        {
            get { return this._CUSR_TIPO; }
            set { this._CUSR_TIPO = value; }
        }
        public String VUSR_TIPO
        {
            get { return this._VUSR_TIPO; }
            set { this._VUSR_TIPO = value; }
        }
        public Int32 NID_PERFIL
        {
            get { return this._NID_PERFIL; }
            set { this._NID_PERFIL = value; }
        }
        public String VUSR_PERFIL
        {
            get { return this._VUSR_PERFIL; }
            set { this._VUSR_PERFIL = value; }
        }
        public Int32 NID_ROL
        {
            set { this._NID_ROL = value; }
            get { return this._NID_ROL; }
        }
        
        public DateTime FE_INICIO_ACCESO
        {
            get { return this._FE_INICIO_ACCESO; }
            set { this._FE_INICIO_ACCESO = value; }
        }
        public String SFE_INICIO_ACCESO
        {
            get { return this._SFE_INICIO_ACCESO; }
            set { this._SFE_INICIO_ACCESO = value; }
        }
        public DateTime FE_FIN_ACCESO
        {
            get { return this._FE_FIN_ACCESO; }
            set { this._FE_FIN_ACCESO = value; }
        }
        public String SFE_FIN_ACCESO
        {
            get { return this._SFE_FIN_ACCESO; }
            set { this._SFE_FIN_ACCESO = value; }
        }
        public String HR_INICIO_ACCESO
        {
            get { return this._HR_INICIO_ACCESO; }
            set { this._HR_INICIO_ACCESO = value; }
        }
        public String HR_FIN_ACCESO
        {
            get { return this._HR_FIN_ACCESO; }
            set { this._HR_FIN_ACCESO = value; }
        }
        public String NU_TIPO_DOCUMENTO
        {
            get { return this._NU_TIPO_DOCUMENTO; }
            set { this._NU_TIPO_DOCUMENTO = value; }
        }
        public Int32 CO_TIPO_VENDEDOR
        {
            get { return this._CO_TIPO_VENDEDOR; }
            set { this._CO_TIPO_VENDEDOR = value; }
        }
        public String FL_INACTIVO
        {
            get { return this._FL_INACTIVO; }
            set { this._FL_INACTIVO = value; }
        }
        public String SFL_INACTIVO
        {
            get { return this._SFL_INACTIVO; }
            set { this._SFL_INACTIVO = value; }
        }
        
        public String CO_USUARIO_CREA
        {
            get { return this._CO_USUARIO_CREA; }
            set { this._CO_USUARIO_CREA = value; }
        }
        
        public String CO_USUARIO_CAMBIO
        {
            get { return this._CO_USUARIO_CAMBIO; }
            set { this._CO_USUARIO_CAMBIO = value; }
        }
        public String NO_USUARIO_RED
        {
            get { return this._NO_USUARIO_RED; }
            set { this._NO_USUARIO_RED = value; }
        }
        public String NO_ESTACION_RED
        {
            get { return this._NO_ESTACION_RED; }
            set { this._NO_ESTACION_RED = value; }
        }
        public Int32 NID_TIPO
        {
            set { this._NID_TIPO = value; }
            get { return this._NID_TIPO; }
        }
        public DateTime FE_INICIO_ACCESO_PERFIL
        {
            get { return this._FE_INICIO_ACCESO_PERFIL; }
            set { this._FE_INICIO_ACCESO_PERFIL = value; }
        }
        public String SFE_INICIO_ACCESO_PERFIL
        {
            get { return this._SFE_INICIO_ACCESO_PERFIL; }
            set { this._SFE_INICIO_ACCESO_PERFIL = value; }
        }
        public DateTime FE_FIN_ACCESO_PERFIL
        {
            get { return this._FE_FIN_ACCESO_PERFIL; }
            set { this._FE_FIN_ACCESO_PERFIL = value; }
        }
        public String SFE_FIN_ACCESO_PERFIL
        {
            get { return this._SFE_FIN_ACCESO_PERFIL; }
            set { this._SFE_FIN_ACCESO_PERFIL = value; }
        }
        public String HR_INICIO_ACCESO_PERFIL
        {
            get { return this._HR_INICIO_ACCESO_PERFIL; }
            set { this._HR_INICIO_ACCESO_PERFIL = value; }
        }
        public String HR_FIN_ACCESO_PERFIL
        {
            get { return this._HR_FIN_ACCESO_PERFIL; }
            set { this._HR_FIN_ACCESO_PERFIL = value; }
        }
        public String fl_reset
        {
            get { return this._fl_reset; }
            set { this._fl_reset = value; }
        }
    }
    [Serializable]
    public class UsuarioBEList : List<UsuarioBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            UsuarioBEComparer dc = new UsuarioBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class UsuarioBEComparer : IComparer<UsuarioBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public UsuarioBEComparer(String propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(UsuarioBE x, UsuarioBE y)
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
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class ParametrosBackOfficeBE
    {
        #region "Parametros"
        private int _nid_parametro;
        private string _co_parametro;
        private string _no_parametro;
        private string _no_tipo_valor;
        private string _no_valor1;
        private int _qt_valor2;
        private string _fl_valor3;
        private string _co_usuario;
        private string _fe_accion;
        private string _co_usuario_red;
        private string _no_estacion_red;
        private string _fl_activo;
        private string _ind_accion;
        private string _valor;
        private string _valor_texto;
        #endregion

        #region "Eventos Parametros"
        public string valor_texto
        {
            get { return _valor_texto; }
            set { _valor_texto = value; }
        }
        public string valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public string ind_accion
        {
            get { return _ind_accion; }
            set { _ind_accion = value; }
        }


        public string fl_activo
        {
            get { return _fl_activo; }
            set { _fl_activo = value; }
        }


        public string no_estacion_red
        {
            get { return _no_estacion_red; }
            set { _no_estacion_red = value; }
        }


        public string co_usuario_red
        {
            get { return _co_usuario_red; }
            set { _co_usuario_red = value; }
        }


        public string fe_accion
        {
            get { return _fe_accion; }
            set { _fe_accion = value; }
        }


        public string co_usuario
        {
            get { return _co_usuario; }
            set { _co_usuario = value; }
        }


        public string fl_valor3
        {
            get { return _fl_valor3; }
            set { _fl_valor3 = value; }
        }

        public int qt_valor2
        {
            get { return _qt_valor2; }
            set { _qt_valor2 = value; }
        }


        public string no_valor1
        {
            get { return _no_valor1; }
            set { _no_valor1 = value; }
        }


        public string no_tipo_valor
        {
            get { return _no_tipo_valor; }
            set { _no_tipo_valor = value; }
        }


        public string no_parametro
        {
            get { return _no_parametro; }
            set { _no_parametro = value; }
        }


        public string co_parametro
        {
            get { return _co_parametro; }
            set { _co_parametro = value; }
        }

        public int nid_parametro
        {
            get { return _nid_parametro; }
            set { _nid_parametro = value; }
        }
        #endregion

        #region Horario Defecto
        private string _HoraInicio;

        public string HoraInicio
        {
            get { return _HoraInicio; }
            set { _HoraInicio = value; }
        }

        private string _HoraFinal;

        public string HoraFinal
        {
            get { return _HoraFinal; }
            set { _HoraFinal = value; }
        }

        private string _IntervaloTime;

        public string IntervaloTime
        {
            get { return _IntervaloTime; }
            set { _IntervaloTime = value; }
        }

        #endregion

        #region Horas SRC
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _DES;

        public string DES
        {
            get { return _DES; }
            set { _DES = value; }
        }
        #endregion

        #region CONFIGURACION_TALLER-EMPRESA
        private Int32 _nid_taller_empresa;
        public Int32 nid_taller_empresa
        {
            get { return _nid_taller_empresa; }
            set { _nid_taller_empresa = value; }
        }

        private Int32 _nid_taller;
        public Int32 nid_taller
        {
            get { return _nid_taller; }
            set { _nid_taller = value; }
        }

        private Int32 _nid_empresa;
        public Int32 nid_empresa
        {
            get { return _nid_empresa; }
            set { _nid_empresa = value; }
        }

        private string _no_taller;
        public string no_taller
        {
            get { return _no_taller; }
            set { _no_taller = value; }
        }

        private string _no_banco;
        public string no_banco
        {
            get { return _no_banco; }
            set { _no_banco = value; }
        }

        private string _nu_cuenta;
        public string nu_cuenta
        {
            get { return _nu_cuenta; }
            set { _nu_cuenta = value; }
        }

        private string _no_correo_callcenter;
        public string no_correo_callcenter
        {
            get { return _no_correo_callcenter; }
            set { _no_correo_callcenter = value; }
        }

        private string _nu_callcenter;
        public string nu_callcenter
        {
            get { return _nu_callcenter; }
            set { _nu_callcenter = value; }
        }

        private String _cod_dist;
        public String cod_dist
        {
            get { return _cod_dist; }
            set { _cod_dist = value; }
        }

        private String _cod_dpto;
        public String cod_dpto
        {
            get { return _cod_dpto; }
            set { _cod_dpto = value; }
        }

        private String _cod_prov;
        public String cod_prov
        {
            get { return _cod_prov; }
            set { _cod_prov = value; }
        }

        private DateTime _fe_crea;
        public DateTime fe_crea
        {
            get { return _fe_crea; }
            set { _fe_crea = value; }
        }

        private String _co_usuario_crea;
        public String co_usuario_crea
        {
            get { return _co_usuario_crea; }
            set { _co_usuario_crea = value; }
        }

        private DateTime _fe_modi;
        public DateTime fe_modi
        {
            get { return _fe_modi; }
            set { _fe_modi = value; }
        }

        private String _co_usuario_modi;
        public String co_usuario_modi
        {
            get { return _co_usuario_modi; }
            set { _co_usuario_modi = value; }
        }

        //private String _co_usuario_red;
        //public String co_usuario_red
        //{
        //    get { return _co_usuario_red; }
        //    set { _co_usuario_red = value; }
        //}

        //private String _no_estacion_red;
        //public String no_estacion_red
        //{
        //    get { return _no_estacion_red; }
        //    set { _no_estacion_red = value; }
        //}

        //private String _fl_activo;
        //public String fl_activo
        //{
        //    get { return _fl_activo; }
        //    set { _fl_activo = value; }
        //}

        private String _no_empresa;
        public String no_empresa
        {
            get { return _no_empresa; }
            set { _no_empresa = value; }
        }

        #endregion

    }
    [Serializable]
    public class ParametrosBackOffieBEList : List<ParametrosBackOfficeBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            ParametrosBackOffieComparer dc = new ParametrosBackOffieComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class ParametrosBackOffieComparer : IComparer<ParametrosBackOfficeBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public ParametrosBackOffieComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(ParametrosBackOfficeBE x, ParametrosBackOfficeBE y)
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

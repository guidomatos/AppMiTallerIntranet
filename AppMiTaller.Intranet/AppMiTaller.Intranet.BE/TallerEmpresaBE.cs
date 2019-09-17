using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class TallerEmpresaBE
    {
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

        private String _co_usuario_red;
        public String co_usuario_red
        {
            get { return _co_usuario_red; }
            set { _co_usuario_red = value; }
        }

        private String _no_estacion_red;
        public String no_estacion_red
        {
            get { return _no_estacion_red; }
            set { _no_estacion_red = value; }
        }

        private String _fl_activo;
        public String fl_activo
        {
            get { return _fl_activo; }
            set { _fl_activo = value; }
        }

        private String _no_empresa;
        public String no_empresa
        {
            get { return _no_empresa; }
            set { _no_empresa = value; }
        }
    }

    [Serializable]
    public class TallerEmpresaBEList : List<TallerEmpresaBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            TallerEmpresaBEComparer dc = new TallerEmpresaBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }


    class TallerEmpresaBEComparer : IComparer<TallerEmpresaBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public TallerEmpresaBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(TallerEmpresaBE x, TallerEmpresaBE y)
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

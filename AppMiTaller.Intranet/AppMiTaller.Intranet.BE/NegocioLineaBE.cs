using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class NegocioLineaBE:BaseBE
    {
        #region "Atributos"
        private Int32 _nid_negocio_linea;
        private String _co_familia;
        private String _co_negocio;
        private Int32 _nid_cod_tipo_linea;
        private String _fl_inactivo;
        private DateTime _fe_crea;
        private String _co_usuario_crea;
        private DateTime _fe_cambio;
        private String _co_usuario_cambio;        
        private String _no_usuario_red;
        private String _no_estacion;
        //**************************
        private String _nom_linea;

        //************agregado por eddy******************//
        private String nom_negocio;
        private String cod_linea;
        private String nom_tipo_linea;
        private String dsc_estado;
        private String cod_estado;
        private Int32 id_tipo_linea;
        private String _cod_familia;
        private String _nom_familia;
        #endregion

        #region "Propiedades"
         #region "Propiedades de eddy"
            public String nom_familia
            {
                get { return _nom_familia; }
                set { _nom_familia = value; }
            }
            public String cod_familia
            {
                get { return _cod_familia; }
                set { _cod_familia = value; }
            }
            public Int32 Id_tipo_linea
            {
                get { return id_tipo_linea; }
                set { id_tipo_linea = value; }
            }
            public String Cod_estado
            {
                get { return cod_estado; }
                set { cod_estado = value; }
            }
            public String Dsc_estado
            {
                get { return dsc_estado; }
                set { dsc_estado = value; }
            }
            public String Nom_tipo_linea
            {
                get { return nom_tipo_linea; }
                set { nom_tipo_linea = value; }
            }
           
            public String Cod_linea
            {
                get { return cod_linea; }
                set { cod_linea = value; }
            }
            public String Nom_negocio
            {
                get { return nom_negocio; }
                set { nom_negocio = value; }
            }
        #endregion
        public Int32 nid_negocio_linea
        {
            get { return _nid_negocio_linea; }
            set { _nid_negocio_linea = value; }
        }
        public String co_familia
        {
            get { return _co_familia; }
            set { _co_familia = value; }
        }
        public String co_negocio
        {
            get { return _co_negocio; }
            set { _co_negocio = value; }
        }
        public Int32 nid_cod_tipo_linea
        {
            get { return _nid_cod_tipo_linea; }
            set { _nid_cod_tipo_linea = value; }
        }        
        public String fl_inactivo
        {
            get { return _fl_inactivo; }
            set { _fl_inactivo = value; }
        }
        public DateTime fe_crea
        {
            get { return _fe_crea; }
            set { _fe_crea = value; }
        }
        public String co_usuario_crea
        {
            get { return _co_usuario_crea; }
            set { _co_usuario_crea = value; }
        }
        public DateTime fe_cambio
        {
            get { return _fe_cambio; }
            set { _fe_cambio = value; }
        }
        public String co_usuario_cambio
        {
            get { return _co_usuario_cambio; }
            set { _co_usuario_cambio = value; }
        }
        public String no_usuario_red
        {
            get { return _no_usuario_red; }
            set { _no_usuario_red = value; }
        }
        public String no_estacion
        {
            get { return _no_estacion; }
            set { _no_estacion = value; }
        }

        public String Nom_linea
        {
            get { return _nom_linea; }
            set { _nom_linea = value; }
        }
        #endregion

        #region "Metodos"
        public NegocioLineaBE()
        {
        }
        #endregion
    }

    [Serializable]
    public class NegocioLineaBEList : List<NegocioLineaBE>
    {
        public void ordenar(string propertyName, direccionOrden Direction)
        {
            NegocioLineaComparer dc = new NegocioLineaComparer(propertyName, Direction);
            this.Sort(dc);
        } 
    }

    class NegocioLineaComparer : IComparer<NegocioLineaBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public NegocioLineaComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(NegocioLineaBE x, NegocioLineaBE y)
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

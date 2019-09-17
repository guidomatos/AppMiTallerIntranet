using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class PaisBE
    {
        #region "Atributos"
        private Int32 _nid_pais;
        private String _no_pais;
        private String _no_pais_ingles;
        private String _fl_inactivo;
        private DateTime _fe_crea;
        private String _sfe_crea;
        private String _co_usuario_crea;
        private DateTime _fe_cambio;
        private String _sfe_cambio;                
        private String _co_usuario_cambio;
        private String _no_estacion;
        private String _no_usuario_red;
        private String _estado;
        private String _co_auto_ultra;

        private String _no_pais_Anterior;
        private String _fl_inactivo_Anterior;
        private String _co_usuario_cambio_Anterior;
        private String _no_estacion_Anterior;
        private String _no_usuario_red_Anterior;
        private String _no_pais_ingles_Anterior;
        private String _sfe_cambio_anterior;
        private String _co_auto_ultra_Anterior;

        #endregion

        #region "Propiedades"
        public String co_auto_ultra_Anterior
        {
            get { return _co_auto_ultra_Anterior; }
            set { _co_auto_ultra_Anterior = value; }
        }
        public String sfe_cambio
        {
            get { return _sfe_cambio; }
            set { _sfe_cambio = value; }
        }
        public String sfe_crea
        {
            get { return _sfe_crea; }
            set { _sfe_crea = value; }
        }
        public String sfe_cambio_anterior
        {
            get { return _sfe_cambio_anterior; }
            set { _sfe_cambio_anterior = value; }
        }
        public Int32 nid_pais
        {
            get { return _nid_pais; }
            set { _nid_pais = value; }
        }
        public String no_pais
        {
            get { return _no_pais; }
            set { _no_pais = value; }
        }
        public String no_pais_ingles
        {
            get { return _no_pais_ingles; }
            set { _no_pais_ingles = value; }
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
        public String no_estacion
        {
            get { return _no_estacion; }
            set { _no_estacion = value; }
        }
        public String no_usuario_red
        {
            get { return _no_usuario_red; }
            set { _no_usuario_red = value; }
        }
        public String estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        public String co_auto_ultra
        {
            get { return _co_auto_ultra; }
            set { _co_auto_ultra = value; }
        }

        public String no_pais_Anterior
        {
            get { return _no_pais_Anterior; }
            set { _no_pais_Anterior = value; }
        }
        public String fl_inactivo_Anterior
        {
            get { return _fl_inactivo_Anterior; }
            set { _fl_inactivo_Anterior = value; }
        }
        public String co_usuario_cambio_Anterior
        {
            get { return _co_usuario_cambio_Anterior; }
            set { _co_usuario_cambio_Anterior = value; }
        }
        public String no_estacion_Anterior
        {
            get { return _no_estacion_Anterior; }
            set { _no_estacion_Anterior = value; }
        }
        public String no_usuario_red_Anterior
        {
            get { return _no_usuario_red_Anterior; }
            set { _no_usuario_red_Anterior = value; }
        }
        public String no_pais_ingles_Anterior
        {
            get { return _no_pais_ingles_Anterior; }
            set { _no_pais_ingles_Anterior = value; }
        }
        #endregion

        #region "Metodos"
        public PaisBE()
        {
        }
        #endregion
    }

    [Serializable]
    public class PaisBEList : List<PaisBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            PaisComparer dc = new PaisComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class PaisComparer : IComparer<PaisBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public PaisComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(PaisBE x, PaisBE y)
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

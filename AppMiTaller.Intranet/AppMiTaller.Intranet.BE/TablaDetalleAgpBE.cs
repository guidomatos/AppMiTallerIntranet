using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class TablaDetalleAgpBE
    {
        #region "Atributos"
        private Int32 _nid_tabla_gen_det_agp;
        private Int32 _nid_tabla_gen_agp;
        private String _no_valor1;
        private String _no_valor2;
        private String _no_valor3;
        private String _no_valor4;
        private String _no_valor5;
        private String _fl_inactivo;
        #endregion

        #region "Propiedades"
        public String fl_inactivo
        {
            get { return _fl_inactivo; }
            set { _fl_inactivo = value; }
        }

        public String no_valor5
        {
            get { return _no_valor5; }
            set { _no_valor5 = value; }
        }

        public String no_valor4
        {
            get { return _no_valor4; }
            set { _no_valor4 = value; }
        }

        public String no_valor3
        {
            get { return _no_valor3; }
            set { _no_valor3 = value; }
        }

        public String no_valor2
        {
            get { return _no_valor2; }
            set { _no_valor2 = value; }
        }

        public String no_valor1
        {
            get { return _no_valor1; }
            set { _no_valor1 = value; }
        }

        public Int32 nid_tabla_gen_agp
        {
            get { return _nid_tabla_gen_agp; }
            set { _nid_tabla_gen_agp = value; }
        }
        
        public Int32 nid_tabla_gen_det_agp
        {
            get { return _nid_tabla_gen_det_agp; }
            set { _nid_tabla_gen_det_agp = value; }
        }
        #endregion
    }

    [Serializable]
    public class TablaDetalleAgpBEList : List<TablaDetalleAgpBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            TablaDetalleAgpComparer dc = new TablaDetalleAgpComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class TablaDetalleAgpComparer : IComparer<TablaDetalleAgpBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public TablaDetalleAgpComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(TablaDetalleAgpBE x, TablaDetalleAgpBE y)
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

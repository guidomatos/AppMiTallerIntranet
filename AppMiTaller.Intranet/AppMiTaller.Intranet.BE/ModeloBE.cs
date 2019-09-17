using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class ModeloBE
    {
        public Int32 nid_modelo { get; set; }
        public String co_modelo { get; set; }
        public string co_marca { get; set; }
        public Int32 nid_marca { get; set; }
        public String no_modelo { get; set; }
        public String co_negocio { get; set; }
        public Int32 nid_linea_comercial { get; set; }
        public Int32 nid_linea_importacion { get; set; }
        public String fl_inactivo { get; set; }
        public String co_usuario_creacion { get; set; }
        public DateTime fe_cambio { get; set; }
        public String co_usuario_cambio { get; set; }
        public String no_estacion { get; set; }
        public String no_usuario_red { get; set; }
        public String estado { get; set; }
        public String no_marca { get; set; }
        public String no_linea_importacion { get; set; }
        public String no_linea_comercial { get; set; }
        public String sfe_cambio { get; set; }
        public string co_familia { get; set; }
        public string no_familia { get; set; }
        public string no_negocio { get; set; }
        

        public ModeloBE()
        {
        }
    }

    [Serializable]
    public class ModeloBEList : List<ModeloBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            ModeloComparer dc = new ModeloComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class ModeloComparer : IComparer<ModeloBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public ModeloComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(ModeloBE x, ModeloBE y)
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

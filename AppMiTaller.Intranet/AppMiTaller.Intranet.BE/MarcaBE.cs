using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class MarcaBE
    {
        
        public Int32 nid_marca { get; set; }
        public String nu_ruc { get; set; }
        public String co_marca { get; set; }
        public Int32 nid_importador { get; set; }
        public Int32 nid_empresa { get; set; }
        public String no_marca { get; set; }
        public String logo { get; set; }
        public Int32 tamanio_imagen { get; set; }
        public Byte[] imagen_logo { get; set; }
        public String fl_inactivo { get; set; }
        public String co_usuario_creacion { get; set; }
        public DateTime fe_cambio { get; set; }
        public String co_usuario_cambio { get; set; }
        public String no_estacion { get; set; }
        public String no_usuario_red { get; set; }
        public String estado { get; set; }
        public String sfe_cambio { get; set; }
        
    }
    [Serializable]
    public class MarcaBEList : List<MarcaBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            MarcaComparer dc = new MarcaComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class MarcaComparer : IComparer<MarcaBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public MarcaComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(MarcaBE x, MarcaBE y)
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

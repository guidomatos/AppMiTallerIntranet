using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class TallerServiciosBE
    {
        public int nid_taller { get; set; }
        public int nid_servicio { get; set; }
        public string co_servicio { get; set; }
        public string no_servicio { get; set; }
        public int nid_tipo_servicio { get; set; }
        public string co_tipo_servicio { get; set; }
        public string no_tipo_servicio { get; set; }
        public string co_usuario { get; set; }
        public string co_usuario_red { get; set; }
        public string no_estacion_red { get; set; }
    }

    [Serializable]
    public class TallerServiciosBEList : List<TallerServiciosBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            TallerServiciosBEComparer dc = new TallerServiciosBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class TallerServiciosBEComparer : IComparer<TallerServiciosBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public TallerServiciosBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(TallerServiciosBE x, TallerServiciosBE y)
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

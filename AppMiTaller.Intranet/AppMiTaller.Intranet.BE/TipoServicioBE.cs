using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class TipoServicioBE
    {
        public int nid_usuario { get; set; }
        public int Id_TipoServicio { get; set; }
        public String Co_tipo_servicio { get; set; }
        public String No_tipo_servicio { get; set; }
        public String Co_usuario_crea { get; set; }
        public String Co_usuario_modi { get; set; }
        public String Co_usuario_red { get; set; }
        public String No_estacion_red { get; set; }
        public String Fl_activo { get; set; }
        public String fl_visible { get; set; }
        public String fl_validar_km { get; set; }
        public int nid_modelo { get; set; }
    }

    [Serializable]
    public class TipoServicioBEList : List<TipoServicioBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            TipoServicioBEComparer dc = new TipoServicioBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class TipoServicioBEComparer : IComparer<TipoServicioBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public TipoServicioBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(TipoServicioBE x, TipoServicioBE y)
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

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class TallerHorariosBE
    {
        public int nid_horario { get; set; }
        public int nid_propietario { get; set; }
        public int dd_atencion { get; set; }
        public int qt_capacidad_fo { get; set; }
        public int qt_capacidad_bo { get; set; }
        public int qt_capacidad { get; set; }
        public string ho_inicio { get; set; }
        public string ho_fin { get; set; }
        public string fl_tipo { get; set; }
        public string fl_control { get; set; }
        public string fl_activo { get; set; }
        public string co_usuario { get; set; }
        public string co_usuario_red { get; set; }
        public string no_estacion_red { get; set; }
    }

    [Serializable]
    public class TallerHorariosBEList : List<TallerHorariosBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            TallerHorariosBEComparer dc = new TallerHorariosBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class TallerHorariosBEComparer : IComparer<TallerHorariosBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public TallerHorariosBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(TallerHorariosBE x, TallerHorariosBE y)
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

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class ComboBE
    {
        public int nid_padre { get; set; }
        public Int32 IntID { get; set; }
        public string DES { get; set; }
        public string ID { get; set; }
        public int nid_hijo { get; set; }
        public string no_nombre { get; set; }
    }

    [Serializable]
    public class CombosBEList : List<ComboBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            CombosBEComparer dc = new CombosBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class CombosBEComparer : IComparer<ComboBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public CombosBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(ComboBE x, ComboBE y)
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

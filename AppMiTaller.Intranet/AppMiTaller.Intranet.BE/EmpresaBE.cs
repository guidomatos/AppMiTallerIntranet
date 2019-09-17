using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class EmpresaBE
    {
        public String prefijo_corto { get; set; }
        public Int32 nid_empresa { get; set; }
        public String no_empresa { get; set; }

        public EmpresaBE()
        {

        }
    }
    [Serializable]
    public class EmpresaBEList : List<EmpresaBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            EmpresaComparer dc = new EmpresaComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class EmpresaComparer : IComparer<EmpresaBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public EmpresaComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(EmpresaBE x, EmpresaBE y)
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
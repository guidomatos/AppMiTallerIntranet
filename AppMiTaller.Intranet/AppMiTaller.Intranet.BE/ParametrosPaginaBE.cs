using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class ParametrosPaginaBE
    {
        #region "Atributos"        
        private String _co_pagina;
        private Int32 _nu_tab;
        private List<String> _filtros;
        #endregion

        #region "Propiedades"        
        public List<String> filtros
        {
            get { return _filtros; }
            set { _filtros = value; }
        }
        public Int32 nu_tab
        {
            get { return _nu_tab; }
            set { _nu_tab = value; }
        }
        public String co_pagina
        {
            get { return _co_pagina; }
            set { _co_pagina = value; }
        }
        #endregion

        #region "Metodos"
        public ParametrosPaginaBE()
        {
            if(this.filtros==null)
                this.filtros = new List<string>();
        }
        #endregion
    }
    [Serializable]
    public class ParametrosPaginaBEList : List<ParametrosPaginaBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            ParametrosPaginaComparer dc = new ParametrosPaginaComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class ParametrosPaginaComparer : IComparer<ParametrosPaginaBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public ParametrosPaginaComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(ParametrosPaginaBE x, ParametrosPaginaBE y)
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

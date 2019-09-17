using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class ErrorImportacionBE
    {
        #region "Atributos"
        private Int32 _Indice;
        private Int16 _Fila;
        private Int16 _Columna;
        private String _codError;
        private String _msjError;
        #endregion

        #region "Propiedades"
        public Int32 Indice
        {
            get { return _Indice; }
            set { this._Indice = value; }
        }
        public Int16 Fila
        {
            get { return _Fila; }
            set { this._Fila = value; }
        }
        public Int16 Columna
        {
            get { return _Columna; }
            set { this._Columna = value; }
        }
        public String codError
        {
            get { return _codError; }
            set { this._codError = value; }
        }
        public String msjError
        {
            get { return _msjError; }
            set { this._msjError = value; }
        }
        #endregion

        #region "Metodos"

        public ErrorImportacionBE()
        {
            this.Indice = -1;
        }

        public ErrorImportacionBE(Int16 _Fila, Int16 _Columna, String _codError, String _msjError)
        {
            this._Fila = (Int16)(_Fila + 1);
            this._Columna = (Int16)(_Columna + 1);
            this._codError = _codError;
            this._msjError = _msjError;
            this.SetIndice();
        }

        public void SetIndice()
        {
            this.Indice = (Int32)(this.Fila * 100 + this.Columna);
        }

        #endregion
    }

    [Serializable]
    public class ErrorImportacionBEList : List<ErrorImportacionBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            ErrorImportacionComparer dc = new ErrorImportacionComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class ErrorImportacionComparer : IComparer<ErrorImportacionBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public ErrorImportacionComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(ErrorImportacionBE x, ErrorImportacionBE y)
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

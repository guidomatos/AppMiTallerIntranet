using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class TipoTablaDetalleBE : BaseBE
    {
        #region Atributos
        private Int32 id_tabla_detalle;
        private Int32 id_tabla;
        private String valor1;
        private String valor2;
        private String valor3;
        private String valor4;
        private String valor5;
        private String cod_estado;
        private String dsc_estado;
        private String fl_inactivo;
        private String sfl_inactivo;

        private DateTime _fe_crea;
        private String _co_usuario_crea;
        private DateTime _fe_cambio;
        private String _co_usuario_cambio;
        private String _no_estacion;
        private String _no_usuario_red;
        //RNapa - 09/03/2009 - Inicio
        private Int32 _mes;
        private Int32 _anio;
        private String _fec_ayer;
                
        //RNapa - 09/03/2009 - Fin
        #endregion
        #region propiedades tipo tabla detalle

        public String fec_ayer
        {
            get { return _fec_ayer; }
            set { _fec_ayer = value; }
        }
        public String Dsc_estado
        {
            get { return dsc_estado; }
            set { dsc_estado = value; }
        }

        public String Cod_estado
        {
            get { return cod_estado; }
            set { cod_estado = value; }
        }
        public String Valor5
        {
            get { return valor5; }
            set { valor5 = value; }
        }
        public String Valor4
        {
            get { return valor4; }
            set { valor4 = value; }
        }
        public String Valor3
        {
            get { return valor3; }
            set { valor3 = value; }
        }
        public String Valor2
        {
            get { return valor2; }
            set { valor2 = value; }
        }

        public String Valor1
        {
            get { return valor1; }
            set { valor1 = value; }
        }
        public Int32 Id_tabla
        {
            get { return id_tabla; }
            set { id_tabla = value; }
        }
        public Int32 Id_tabla_detalle
        {
            get { return id_tabla_detalle; }
            set { id_tabla_detalle = value; }
        }
        public String Fl_inactivo
        {
            get { return fl_inactivo; }
            set { fl_inactivo = value; }
        }
        public String Sfl_inactivo
        {
            get { return sfl_inactivo; }
            set { sfl_inactivo = value; }
        }
        public String Co_usuario_cambio
        {
            get { return _co_usuario_cambio; }
            set { _co_usuario_cambio = value; }
        }
        public String No_estacion
        {
            get { return _no_estacion; }
            set { _no_estacion = value; }
        }
        public String No_usuario_red
        {
            get { return _no_usuario_red; }
            set { _no_usuario_red = value; }
        }
        public DateTime Fe_crea
        {
            get { return _fe_crea; }
            set { _fe_crea = value; }
        }
        public String Co_usuario_crea
        {
            get { return _co_usuario_crea; }
            set { _co_usuario_crea = value; }
        }
        public DateTime Fe_cambio
        {
            get { return _fe_cambio; }
            set { _fe_cambio = value; }
        }
        //RNapa - 09/03/2009 - Inicio        
        public Int32 mes
        {
            get { return _mes; }
            set { _mes = value; }
        }

        public Int32 anio
        {
            get { return _anio; }
            set { _anio = value; }
        }        
        //RNapa - 09/03/2009 - Fin
        #endregion
    }

    [Serializable]
    public class TipoTablaDetalleBEList : List<TipoTablaDetalleBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            TipoTablaDetalleComparer dc = new TipoTablaDetalleComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class TipoTablaDetalleComparer : IComparer<TipoTablaDetalleBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public TipoTablaDetalleComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(TipoTablaDetalleBE x, TipoTablaDetalleBE y)
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

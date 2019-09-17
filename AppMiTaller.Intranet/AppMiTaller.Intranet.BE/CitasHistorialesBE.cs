using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{

    #region "HistorialCitas"

        [Serializable]
        public class HistorialCitasxVehiculoBE
        {
            private String _nu_placa;
            public String nu_placa {
                get { return _nu_placa; }
                set { _nu_placa = value; }
            }

            private String _no_marca;
            public String no_marca {
                get { return _no_marca; }
                set { _no_marca = value; }
            }

            private String _no_modelo;
            public String no_modelo {
                get { return _no_modelo; }
                set { _no_modelo = value; }
            }

            private String _nu_documento;
            public String nu_documento {
                get { return _nu_documento; }
                set { _nu_documento = value; }
            }

            private String _no_cliente;
            public String no_cliente {
                get { return _no_cliente; }
                set { _no_cliente = value; }
            }

            private String _nu_telefono;
            public String nu_telefono {
                get { return _nu_telefono; }
                set { _nu_telefono = value; }
            }

            private String _nu_celular;
            public String nu_celular {
                get { return _nu_celular; }
                set { _nu_celular = value; }
            }

            private String _no_correo;
            public String no_correo {
                get { return _no_correo; }
                set { _no_correo = value; }
            }

 private String _co_tipo_cliente;
            public String co_tipo_cliente
            {
                get { return _co_tipo_cliente; }
                set { _co_tipo_cliente = value; }
            }

            private String _doc_cliente;
            public String doc_cliente
            {
                get { return _doc_cliente; }
                set { _doc_cliente = value; }
            }

            private CitasxVehiculoBEList _lstcitas;
            public CitasxVehiculoBEList lstcitas
            {
                get { return _lstcitas; }
                set { _lstcitas = value; }
            }
           



            public HistorialCitasxVehiculoBE()
            {
                _lstcitas = new CitasxVehiculoBEList();
            }

        }
        [Serializable]
        public class HistorialCitasxVehiculoBEList : List<HistorialCitasxVehiculoBE>
        {
            public void Ordenar(string propertyName, direccionOrden Direction)
            {
                HistorialCitasxVehiculoBEComparer dc = new HistorialCitasxVehiculoBEComparer(propertyName, Direction);
                this.Sort(dc);
            }
        }
        class HistorialCitasxVehiculoBEComparer : IComparer<HistorialCitasxVehiculoBE>
        {
            string _prop = "";
            direccionOrden _dir;

            public HistorialCitasxVehiculoBEComparer(string propertyName, direccionOrden Direction)
            {
                _prop = propertyName;
                _dir = Direction;
            }

            public int Compare(HistorialCitasxVehiculoBE x, HistorialCitasxVehiculoBE y)
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


        [Serializable]
        public class CitasxVehiculoBE
        {

            private Int32 _Itm;
            public Int32 Itm
            {
                get { return _Itm; }
                set { _Itm = value; }
            }

            private Int32 _nid_cita;
            public Int32 nid_cita
            {
                get { return _nid_cita; }
                set { _nid_cita = value; }
            }

            private Int32 _co_estado_cita;
            public Int32 co_estado_cita
            {
                get { return _co_estado_cita; }
                set { _co_estado_cita = value; }
            }

            private String _no_dpto;
            public String no_dpto
            {
                get { return _no_dpto; }
                set { _no_dpto = value; }
            }

            private String _no_ubica;
            public String no_ubica
            {
                get { return _no_ubica; }
                set { _no_ubica = value; }
            }

            private String _no_taller;
            public String no_taller
            {
                get { return _no_taller; }
                set { _no_taller = value; }
            }

            private String _nom_estado;
            public String nom_estado
            {
                get { return _nom_estado; }
                set { _nom_estado = value; }
            }

            private String _AsesorServ;
            public String AsesorServ
            {
                get { return _AsesorServ; }
                set { _AsesorServ = value; }
            }

            private String _Fecha;
            public String Fecha
            {
                get { return _Fecha; }
                set { _Fecha = value; }
            }

            private String _hora;
            public String hora
            {
                get { return _hora; }
                set { _hora = value; }
            }

            private String _no_tipo_servicio;
            public String no_tipo_servicio
            {
                get { return _no_tipo_servicio; }
                set { _no_tipo_servicio = value; }
            }

            private String _no_servicio;
            public String no_servicio
            {
                get { return _no_servicio; }
                set { _no_servicio = value; }
            }

            private String _fecultser;
            public String fecultser
            {
                get { return _fecultser; }
                set { _fecultser = value; }
            }

            private Int32 _ultkm;
            public Int32 ultkm
            {
                get { return _ultkm; }
                set { _ultkm = value; }
            }
        }
        [Serializable]
        public class CitasxVehiculoBEList : List<CitasxVehiculoBE>
            {
                public void Ordenar(string propertyName, direccionOrden Direction)
                {
                    CitasxVehiculoBEComparer dc = new CitasxVehiculoBEComparer(propertyName, Direction);
                    this.Sort(dc);
                }
            }
        class CitasxVehiculoBEComparer : IComparer<CitasxVehiculoBE>
            {
                string _prop = "";
                direccionOrden _dir;

                public CitasxVehiculoBEComparer(string propertyName, direccionOrden Direction)
                {
                    _prop = propertyName;
                    _dir = Direction;
                }

                public int Compare(CitasxVehiculoBE x, CitasxVehiculoBE y)
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

    #endregion

    #region "HistorialServicios"

        [Serializable]
        public class HistorialServiciosxVehiculoBE
        {
            private String _nu_placa;
            public String nu_placa {
                get { return _nu_placa; }
                set { _nu_placa = value; }
            }

            private String _no_marca;
            public String no_marca {
                get { return _no_marca; }
                set { _no_marca = value; }
            }

            private String _no_modelo;
            public String no_modelo {
                get { return _no_modelo; }
                set { _no_modelo = value; }
            }

            private String _nu_documento;
            public String nu_documento {
                get { return _nu_documento; }
                set { _nu_documento = value; }
            }

            private String _no_cliente;
            public String no_cliente {
                get { return _no_cliente; }
                set { _no_cliente = value; }
            }

            private String _nu_telefono;
            public String nu_telefono {
                get { return _nu_telefono; }
                set { _nu_telefono = value; }
            }

            private String _nu_celular;
            public String nu_celular {
                get { return _nu_celular; }
                set { _nu_celular = value; }
            }

            private String _no_correo;
            public String no_correo {
                get { return _no_correo; }
                set { _no_correo = value; }
            }

            private String _co_tipo_cliente;
            public String co_tipo_cliente
            {
                get { return _co_tipo_cliente; }
                set { _co_tipo_cliente = value; }
            }

            private String _doc_cliente;
            public String doc_cliente
            {
                get { return _doc_cliente; }
                set { _doc_cliente = value; }
            }

            private ServiciosxVehiculoBEList _lstservicios;
            public ServiciosxVehiculoBEList lstservicios
            {
                get { return _lstservicios; }
                set { _lstservicios = value; }
            }

 
       

            public HistorialServiciosxVehiculoBE()
            {
                _lstservicios = new ServiciosxVehiculoBEList();
            }
        }
        [Serializable]
        public class HistorialServiciosxVehiculoBEList : List<HistorialServiciosxVehiculoBE>
        {
            public void Ordenar(string propertyName, direccionOrden Direction)
            {
                HistorialServiciosxVehiculoBEComparer dc = new HistorialServiciosxVehiculoBEComparer(propertyName, Direction);
                this.Sort(dc);
            }
        }
        class HistorialServiciosxVehiculoBEComparer : IComparer<HistorialServiciosxVehiculoBE>
        {
            string _prop = "";
            direccionOrden _dir;

            public HistorialServiciosxVehiculoBEComparer(string propertyName, direccionOrden Direction)
            {
                _prop = propertyName;
                _dir = Direction;
            }

            public int Compare(HistorialServiciosxVehiculoBE x, HistorialServiciosxVehiculoBE y)
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


        [Serializable]
        public class ServiciosxVehiculoBE
        {
            private Int32 _Itm;
            public Int32 Itm
            {
                get { return _Itm; }
                set { _Itm = value; }
            }

            private Int32 _co_estado_cita;
            public Int32 co_estado_cita
            {
                get { return _co_estado_cita; }
                set { _co_estado_cita = value; }
            }

            private String _no_dpto;
            public String no_dpto
            {
                get { return _no_dpto; }
                set { _no_dpto = value; }
            }

            private String _no_ubica;
            public String no_ubica
            {
                get { return _no_ubica; }
                set { _no_ubica = value; }
            }

            private String _no_taller;
            public String no_taller
            {
                get { return _no_taller; }
                set { _no_taller = value; }
            }

            private String _fe_atencion;
            public String fe_atencion
            {
                get { return _fe_atencion; }
                set { _fe_atencion = value; }
            }

            private String _hora;
            public string hora
            {
                get { return _hora; }
                set { _hora = value; }
            }

            private String _no_servicio;
            public String no_servicio
            {
                get { return _no_servicio; }
                set { _no_servicio = value; }
            }

            private String _tx_glosa_atencion;
            public String tx_glosa_atencion
            {
                get { return _tx_glosa_atencion; }
                set { _tx_glosa_atencion = value; }
            }

            private String _AsesorServ;
            public String AsesorServ
            {
                get { return _AsesorServ; }
                set { _AsesorServ = value; }
            }
        }
        [Serializable]
        public class ServiciosxVehiculoBEList : List<ServiciosxVehiculoBE>
            {
                public void Ordenar(string propertyName, direccionOrden Direction)
                {
                    ServiciosxVehiculoBEComparer dc = new ServiciosxVehiculoBEComparer(propertyName, Direction);
                    this.Sort(dc);
                }
            }
        class ServiciosxVehiculoBEComparer : IComparer<ServiciosxVehiculoBE>
            {
                string _prop = "";
                direccionOrden _dir;

                public ServiciosxVehiculoBEComparer(string propertyName, direccionOrden Direction)
                {
                    _prop = propertyName;
                    _dir = Direction;
                }

                public int Compare(ServiciosxVehiculoBE x, ServiciosxVehiculoBE y)
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

    #endregion

    #region "Calculadora"
    [Serializable]
    public class CalculadoraBE
    {
        private String _nu_placa;
        public String nu_placa
        {
            get { return _nu_placa; }
            set { _nu_placa = value; }
        }

        private String _no_marca;
        public String no_marca
        {
            get { return _no_marca; }
            set { _no_marca = value; }
        }

        private String _no_modelo;
        public String no_modelo
        {
            get { return _no_modelo; }
            set { _no_modelo = value; }
        }

        private String _ot;
        public String ot
        {
            get { return _ot; }
            set { _ot = value; }
        }

        private Int32 _km_ult_serv;
        public Int32 km_ult_serv
        {
            get { return _km_ult_serv; }
            set { _km_ult_serv = value; }
        }

        private String _fec_ult_serv;
        public String fec_ult_serv
        {
            get { return _fec_ult_serv; }
            set { _fec_ult_serv = value; }
        }

        private Int32 _km_prm_ult_serv;
        public Int32 km_prm_ult_serv
        {
            get { return _km_prm_ult_serv; }
            set { _km_prm_ult_serv = value; }
        }

        private Int32 _km_prox_serv;
        public Int32 km_prox_serv
        {
            get { return _km_prox_serv; }
            set { _km_prox_serv = value; }
        }

        private String _fecproxServ;
        public String fecproxServ
        {
            get { return _fecproxServ; }
            set { _fecproxServ = value; }
        }
    }

    [Serializable]
    public class CalculadoraBEList : List<CalculadoraBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            CalculadoraBEComparer dc = new CalculadoraBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class CalculadoraBEComparer : IComparer<CalculadoraBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public CalculadoraBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(CalculadoraBE x, CalculadoraBE y)
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
    #endregion

}

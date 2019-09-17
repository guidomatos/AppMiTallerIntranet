using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class CitasRepGenBE
    {
        private String _cod_reserva;
        public String cod_reserva
        {
            get { return _cod_reserva; }
            set { _cod_reserva = value; }
        }


        private String _coddpto;
        public String coddpto {
            get { return _coddpto; }
            set { _coddpto = value; }
        }

        private String _nomdpto;
        public String nomdpto {
            get { return _nomdpto; }
            set { _nomdpto = value; }
        }

        private String _codprov;
        public String codprov {
            get { return _codprov; }
            set { _codprov = value; }
        }

        private String _nomprov;
        public String nomprov {
            get { return _nomprov; }
            set { _nomprov = value; }
        }

        private String _coddist;
        public String coddist {
            get { return _coddist; }
            set { _coddist = value; }
        }

        private String _nomdist;
        public String nomdist {
            get { return _nomdist; }
            set { _nomdist = value; }
        }

        private Int32 _nid_ubica;
        public Int32 nid_ubica {
            get { return _nid_ubica; }
            set { _nid_ubica = value; }
        }

        private String _puntored;
        public String puntored {
            get { return _puntored; }
            set { _puntored = value; }
        }

        private Int32 _nid_taller;
        public Int32 nid_taller {
            get { return _nid_taller; }
            set { _nid_taller = value; }
        }

        private String _no_taller;
        public String no_taller {
            get { return _no_taller; }
            set { _no_taller = value; }
        }

        private Int32 _nid_usuario;
        public Int32 nid_usuario {
            get { return _nid_usuario; }
            set { _nid_usuario = value; }
        }

        private String _AsesorServ;
        public String AsesorServ {
            get { return _AsesorServ; }
            set { _AsesorServ = value; }
        }

        private String _fe_crea;
        public String fe_crea {
            get { return _fe_crea; }
            set { _fe_crea = value; }
        }

        private String _fe_cita;
        public String fe_cita {
            get { return _fe_cita; }
            set { _fe_cita = value; }
        }

        private Boolean _Reprog;
        public Boolean Reprog {
            get { return _Reprog; }
            set { _Reprog = value; }
        }

        private String _fe_Orig;
        public String fe_Orig {
            get { return _fe_Orig; }
            set { _fe_Orig = value; }
        }

        private String _no_cliente;
        public String no_cliente {
            get { return _no_cliente; }
            set { _no_cliente = value; }
        }

        private String _nu_documento;
        public String nu_documento
        {
            get { return _nu_documento; }
            set { _nu_documento = value; }
        }

        private String _nu_telefono;
        public String nu_telefono {
            get { return _nu_telefono; }
            set { _nu_telefono = value; }
        }

        private String _nu_placa;
        public String nu_placa {
            get { return _nu_placa; }
            set { _nu_placa = value; }
        }

        private Int32 _nid_marca;
        public Int32 nid_marca {
            get { return _nid_marca; }
            set { _nid_marca = value; }
        }

        private String _no_marca;
        public String no_marca {
            get { return _no_marca; }
            set { _no_marca = value; }
        }

        private Int32 _nid_modelo;
        public Int32 nid_modelo {
            get { return _nid_modelo; }
            set { _nid_modelo = value; }
        }

        private String _no_modelo;
        public String no_modelo {
            get { return _no_modelo; }
            set { _no_modelo = value; }
        }

        private Int32 _nid_tipo_servicio;
        public Int32 nid_tipo_servicio 
        {
            get { return _nid_tipo_servicio; }
            set { _nid_tipo_servicio = value; }
        }

        private String _no_tipo_servicio;
        public String no_tipo_servicio {
            get { return _no_tipo_servicio; }
            set { _no_tipo_servicio = value; }
        }

        private Int32 _nid_servicio;
        public Int32 nid_servicio {
            get { return _nid_servicio; }
            set { _nid_servicio = value; }
        }

        private String _no_servicio;
        public String no_servicio {
            get { return _no_servicio; }
            set { _no_servicio = value; }
        }

        private String _tx_observacion;
        public String tx_observacion {
            get { return _tx_observacion; }
            set { _tx_observacion = value; }
        }

        private String _Origen;
        public String Origen {
            get { return _Origen; }
            set { _Origen = value; }
        }

        private String _co_usuario_crea;
        public String co_usuario_crea {
            get { return _co_usuario_crea; }
            set { _co_usuario_crea = value; }
        }

		/*GMATOS - 22/06/2011*/
        private String _co_estado_cita;
        public String co_estado_cita {
            get { return _co_estado_cita; }
            set { _co_estado_cita = value; }
        }
		/*GMATOS - 22/06/2011*/

        private String _nom_estado;
        public String nom_estado {
            get { return _nom_estado; }
            set { _nom_estado = value; }
        }

        private String _fregde;
        public String fregde {
            get { return _fregde; }
            set { _fregde = value; }
        }

        private String _fregal;
        public String fregal {
            get { return _fregal; }
            set { _fregal = value; }
        }

        private String _fcitde;
        public String fcitde {
            get { return _fcitde; }
            set { _fcitde = value; }
        }

        private String _fcital;
        public String fcital {
            get { return _fcital; }
            set { _fcital = value; }
        }

        private string _est;
        public string est
        {
            get { return _est; }
            set { _est = value; }
        }
        private string _no_nombreqr;
        public string no_nombreqr
        {
            get { return _no_nombreqr; }
            set { _no_nombreqr = value; }
        }
    }

    [Serializable]
    public class CitasRepGenBEList : List<CitasRepGenBE>
    {

        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            CitasRepGenBEComparer dc = new CitasRepGenBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class CitasRepGenBEComparer : IComparer<CitasRepGenBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public CitasRepGenBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(CitasRepGenBE x, CitasRepGenBE y)
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
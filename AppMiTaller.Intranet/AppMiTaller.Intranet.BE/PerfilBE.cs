using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class PerfilBE
    {
        #region "Atributos"
        private Int32 _NID_PERFIL;
        private String _CCOAPL;
        private String _VDEPRF;
        private DateTime _DFEINIVIG;
        private String _SFEINIVIG;
        private DateTime _DFEFINVIG;
        private String _SFEFINVIG;
        private String _CHORINI;
        private String _CHORFIN;

        private DateTime _DFE_CREA;
        private String _SFE_CREA;
        private String _CO_USUARIO_CREA;
        private DateTime _DFE_CAMBIO;
        private String _SFE_CAMBIO;
        private String _CO_USUARIO_CAMBIO;

        private String _FL_INACTIVO;
        private String _VFL_INACTIVO;

        private String _NO_USUARIO_RED;
        private String _NO_ESTACION_RED;

        private Int32 _NRO_USUARIOS;
        private OpcionSeguridadBEList _OPCIONES;

        private String _fl_obligatorio;
        private String _co_perfil_usuario;

        //DAC - 21/12/2010 - Inicio
        private String _fl_concesionario;
        //DAC - 21/12/2010 - Fin

        #endregion

        #region "Propiedades"

        //DAC - 21/12/2010 - Inicio
        public String fl_concesionario
        {
            set { this._fl_concesionario = value; }
            get { return this._fl_concesionario; }
        }
        //DAC - 21/12/2010 - Fin

        public String co_perfil_usuario
        {
            set { this._co_perfil_usuario = value; }
            get { return this._co_perfil_usuario; }
        }
        public String fl_obligatorio
        {
            set { this._fl_obligatorio = value; }
            get { return this._fl_obligatorio; }
        }
        public Int32 NID_PERFIL
        {
            set { this._NID_PERFIL = value; }
            get { return this._NID_PERFIL; }
        }

        public String CCOAPL
        {
            set { this._CCOAPL = value; }
            get { return this._CCOAPL; }
        }
        public String VDEPRF
        {
            set { this._VDEPRF = value; }
            get { return this._VDEPRF; }
        }
        public DateTime DFEINIVIG
        {
            set { this._DFEINIVIG = value; }
            get { return this._DFEINIVIG; }
        }
        public String SFEINIVIG
        {
            set { this._SFEINIVIG = value; }
            get { return this._SFEINIVIG; }
        }
        public DateTime DFEFINVIG
        {
            set { this._DFEFINVIG = value; }
            get { return this._DFEFINVIG; }
        }
        public String SFEFINVIG
        {
            set { this._SFEFINVIG = value; }
            get { return this._SFEFINVIG; }
        }
        public String CHORINI
        {
            set { this._CHORINI = value; }
            get { return this._CHORINI; }
        }
        public String CHORFIN
        {
            set { this._CHORFIN = value; }
            get { return this._CHORFIN; }
        }

        public DateTime DFE_CREA
        {
            set { this._DFE_CREA = value; }
            get { return this._DFE_CREA; }
        }
        public String SFE_CREA
        {
            set { this._SFE_CREA = value; }
            get { return this._SFE_CREA; }
        }
        public String CO_USUARIO_CREA
        {
            set { this._CO_USUARIO_CREA = value; }
            get { return this._CO_USUARIO_CREA; }
        }
        public DateTime DFE_CAMBIO
        {
            set { this._DFE_CAMBIO = value; }
            get { return this._DFE_CAMBIO; }
        }
        public String SFE_CAMBIO
        {
            set { this._SFE_CAMBIO = value; }
            get { return this._SFE_CAMBIO; }
        }
        public String CO_USUARIO_CAMBIO
        {
            set { this._CO_USUARIO_CAMBIO = value; }
            get { return this._CO_USUARIO_CAMBIO; }
        }

        public String FL_INACTIVO
        {
            set { this._FL_INACTIVO = value; }
            get { return this._FL_INACTIVO; }
        }
        public String VFL_INACTIVO
        {
            set { this._VFL_INACTIVO = value; }
            get { return this._VFL_INACTIVO; }
        }

        public String NO_USUARIO_RED
        {
            set { this._NO_USUARIO_RED = value; }
            get { return this._NO_USUARIO_RED; }
        }
        public String NO_ESTACION_RED
        {
            set { this._NO_ESTACION_RED = value; }
            get { return this._NO_ESTACION_RED; }
        }

        public Int32 NRO_USUARIOS
        {
            set { this._NRO_USUARIOS = value; }
            get { return this._NRO_USUARIOS; }
        }
        public OpcionSeguridadBEList OPCIONES
        {
            set { this._OPCIONES = value; }
            get { return this._OPCIONES; }
        }

        #endregion

    }

    [Serializable]
    public class PerfilBEList : List<PerfilBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            PerfilComparer dc = new PerfilComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class PerfilComparer : IComparer<PerfilBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public PerfilComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(PerfilBE x, PerfilBE y)
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

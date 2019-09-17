using System;
using System.Reflection;
using System.Collections.Generic;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class OpcionSeguridadBE
    {
        #region "Atributos"
        private Int32 _NID_OPCION;
        private String _CCOAPL;
        private String _CCOOPM;
        private String _CSTRUCT;
        private String _VDEMEN;
        private String _VNOMEN;
        private String _CWINDOW;
        private String _CPUBLIC;
        private String _NO_URL_WEB;
        private String _NO_IMG_ON;
        private String _NO_IMG_OFF;
        private DateTime _FE_CREA;
        private String _CO_USUARIO;
        private String _NO_USUARIO;
        private String _NO_ESTACION_RED;

        private String _fl_ind_visible;
        private String _fl_ind_ver_hijos;
        private String _fl_icono;

        /*Auxiliares*/
        private Int32 _NIVEL;
        private String _IND_REL;

        //DAC - 01/04/2011 - Inicio
        private Int32 _nid_perfil;
        private String _cad_lista_opciones;
        private String _cad_lista_tipoacceso;
        private Int32 _nro_registro;
        //DAC - 01/04/2011 - Fin

        //I @001
        private Int32 _nid_usr_opm;
        private Int32 _nid_opcion_perfil;
        //F @001

        #endregion

        #region "Propiedades"
        //I @001
        public Int32 nid_opcion_perfil
        {
            get { return this._nid_opcion_perfil; }
            set { this._nid_opcion_perfil = value; }
        }
        public Int32 nid_usr_opm
        {
            get { return this._nid_usr_opm; }
            set { this._nid_usr_opm = value; }
        }
        //F @002
        public Int32 NID_OPCION
        {
            get { return this._NID_OPCION; }
            set { this._NID_OPCION = value; }
        }

        public String CCOAPL
        {
            get { return this._CCOAPL; }
            set { this._CCOAPL = value; }
        }

        public String CCOOPM
        {
            get { return this._CCOOPM; }
            set { this._CCOOPM = value; }
        }

        public String CSTRUCT
        {
            get { return this._CSTRUCT; }
            set { this._CSTRUCT = value; }
        }

        public String VDEMEN
        {
            get { return this._VDEMEN; }
            set { this._VDEMEN = value; }
        }

        public String VNOMEN
        {
            get { return this._VNOMEN; }
            set { this._VNOMEN = value; }
        }

        public String CWINDOW
        {
            get { return this._CWINDOW; }
            set { this._CWINDOW = value; }
        }

        public String CPUBLIC
        {
            get { return this._CPUBLIC; }
            set { this._CPUBLIC = value; }
        }

        public String NO_URL_WEB
        {
            get { return this._NO_URL_WEB; }
            set { this._NO_URL_WEB = value; }
        }

        public String NO_IMG_ON
        {
            get { return this._NO_IMG_ON; }
            set { this._NO_IMG_ON = value; }
        }

        public String NO_IMG_OFF
        {
            get { return this._NO_IMG_OFF; }
            set { this._NO_IMG_OFF = value; }
        }

        public DateTime FE_CREA
        {
            get { return this._FE_CREA; }
            set { this._FE_CREA = value; }
        }

        public Int32 NIVEL
        {
            get { return this._NIVEL; }
            set { this._NIVEL = value; }
        }

        public String IND_REL
        {
            get { return this._IND_REL; }
            set { this._IND_REL = value; }
        }

        public String CO_USUARIO
        {
            get { return this._CO_USUARIO; }
            set { this._CO_USUARIO = value; }
        }
        public String NO_USUARIO
        {
            get { return this._NO_USUARIO; }
            set { this._NO_USUARIO = value; }
        }
        public String NO_ESTACION_RED
        {
            get { return this._NO_ESTACION_RED; }
            set { this._NO_ESTACION_RED = value; }
        }

        public String fl_ind_visible
        {
            get { return this._fl_ind_visible; }
            set { this._fl_ind_visible = value; }
        }
        public String fl_ind_ver_hijos
        {
            get { return this._fl_ind_ver_hijos; }
            set { this._fl_ind_ver_hijos = value; }
        }
        public String fl_icono
        {
            get { return this._fl_icono; }
            set { this._fl_icono = value; }
        }

        //DAC - 01/04/2011 - Inicio
        public Int32 nid_perfil
        {
            get { return this._nid_perfil; }
            set { this._nid_perfil = value; }
        }

        public String cad_lista_opciones
        {
            get { return this._cad_lista_opciones; }
            set { this._cad_lista_opciones = value; }
        }

        public String cad_lista_tipoacceso
        {
            get { return this._cad_lista_tipoacceso; }
            set { this._cad_lista_tipoacceso = value; }
        }

        public Int32 nro_registro
        {
            get { return this._nro_registro; }
            set { this._nro_registro = value; }
        }
        //DAC - 01/04/2011 - Fin

        #endregion
    }

    [Serializable]
    public class OpcionSeguridadBEList : List<OpcionSeguridadBE>
    {
        public Boolean DebeVerOpcion(String estructura)
        {
            Boolean debeVerOpcion = false;
            int indice = 0;
            while (indice < this.Count && !debeVerOpcion)
            {
                if (this[indice].CSTRUCT.IndexOf(estructura) == 0 &&
                    !this[indice].IND_REL.Trim().Equals(String.Empty))
                {
                    debeVerOpcion = true;
                }
                indice++;
            }
            return debeVerOpcion;
        }

        public OpcionSeguridadBEList Ordenar()
        {
            //Sacamos el mínimo nivel del aplicativo.
            int minimoNivel = -1;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].CSTRUCT.Length < minimoNivel || minimoNivel == -1)
                {
                    minimoNivel = this[i].CSTRUCT.Length;
                }
            }

            //Sacamos todos lo elementos del mínimo nivel.
            OpcionSeguridadBEList oListaResultado = new OpcionSeguridadBEList();
            OpcionSeguridadBEList oListaPrimerNivel = new OpcionSeguridadBEList();
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].CSTRUCT.Length == minimoNivel)
                    oListaPrimerNivel.Add(this[i]);
            }

            //Se llama al metodo recursivo encargado de ordenar la lista.
            OrdenaRecursivo(ref oListaResultado, oListaPrimerNivel, this);
            return oListaResultado;
        }

        private void OrdenaRecursivo(ref OpcionSeguridadBEList oListaResultado, OpcionSeguridadBEList oListaNivelAnt, OpcionSeguridadBEList oListaActual)
        {
            OpcionSeguridadBEList oListaSiguienteNivel = null;
            oListaNivelAnt.Ordenar("VDEMEN", direccionOrden.Ascending);
            for (int i = 0; i < oListaNivelAnt.Count; i++)
            {
                oListaResultado.Add(oListaNivelAnt[i]);
                oListaSiguienteNivel = new OpcionSeguridadBEList();
                for (int j = 0; j < oListaActual.Count; j++)
                {
                    if (oListaActual[j].CSTRUCT.Length == oListaNivelAnt[i].CSTRUCT.Length + 2 &&
                        oListaActual[j].CSTRUCT.IndexOf(oListaNivelAnt[i].CSTRUCT) == 0)
                        oListaSiguienteNivel.Add(oListaActual[j]);
                }

                if (oListaSiguienteNivel.Count > 0)
                {
                    OrdenaRecursivo(ref oListaResultado, oListaSiguienteNivel, oListaActual);
                }
            }
        }

        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            OpcionSeguridadComparer dc = new OpcionSeguridadComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class OpcionSeguridadComparer : IComparer<OpcionSeguridadBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public OpcionSeguridadComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(OpcionSeguridadBE x, OpcionSeguridadBE y)
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

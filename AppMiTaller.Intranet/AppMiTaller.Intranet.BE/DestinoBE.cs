using System;
using System.Collections.Generic;
using System.Reflection;


namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class DestinoBE : BaseBE
    {

        private Int32 id_ubicacion;
        private String tipo_ubicacion;
        private String direccion;
        private String nom_ubicacion;
        private String nom_corto_ubicacion;
        private String nro_ruc;
        private String nom_dpto;
        private String cod_dpto;
        private String nom_provincia;
        private String cod_provincia;
        private String cod_distrito;
        private String nom_distrito;
        private DateTime fec_creacion;
        private String cod_usu_crea;
        private String dsc_estado;
        private String cod_estado;
        private DateTime fec_modi;
        private String cod_usu_modi;
        private String nom_estacion;
        private String nom_usuario_red;
        private String _sfe_modi;
        private String _nombredireccion;
        
        public String sfe_modi
        {
            get { return _sfe_modi; }
            set { _sfe_modi = value; }
        }
        
        public String Nom_usuario_red
        {
            get { return nom_usuario_red; }
            set { nom_usuario_red = value; }
        }
        public String Nom_estacion
        {
            get { return nom_estacion; }
            set { nom_estacion = value; }
        }
        public String Cod_usu_modi
        {
            get { return cod_usu_modi; }
            set { cod_usu_modi = value; }
        }

        public DateTime Fec_modi
        {
            get { return fec_modi; }
            set { fec_modi = value; }
        }
        public String Cod_estado
        {
            get { return cod_estado; }
            set { cod_estado = value; }
        }
        public String Dsc_estado
        {
            get { return dsc_estado; }
            set { dsc_estado = value; }
        }
        public String Cod_usu_crea
        {
            get { return cod_usu_crea; }
            set { cod_usu_crea = value; }
        }
        public DateTime Fec_creacion
        {
            get { return fec_creacion; }
            set { fec_creacion = value; }
        }
        
        public String Nom_distrito
        {
            get { return nom_distrito; }
            set { nom_distrito = value; }
        }
        public String Cod_distrito
        {
            get { return cod_distrito; }
            set { cod_distrito = value; }
        }
        public String Cod_provincia
        {
            get { return cod_provincia; }
            set { cod_provincia = value; }
        }
        public String Nom_provincia
        {
            get { return nom_provincia; }
            set { nom_provincia = value; }
        }
        public String Cod_dpto
        {
            get { return cod_dpto; }
            set { cod_dpto = value; }
        }
        public String Nom_dpto
        {
            get { return nom_dpto; }
            set { nom_dpto = value; }
        }
        public String Nro_ruc
        {
            get { return nro_ruc; }
            set { nro_ruc = value; }
        }
        public String Nom_corto_ubicacion
        {
            get { return nom_corto_ubicacion; }
            set { nom_corto_ubicacion = value; }
        }
        public String Nom_ubicacion
        {
            get { return nom_ubicacion; }
            set { nom_ubicacion = value; }
        }
        public String Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        public String Tipo_ubicacion
        {
            get { return tipo_ubicacion; }
            set { tipo_ubicacion = value; }
        }
        public Int32 Id_ubicacion
        {
            get { return id_ubicacion; }
            set { id_ubicacion = value; }
        }
        

        public String nombredireccion
        {
            get { return _nombredireccion; }
            set { _nombredireccion = value; }
        }
        
    }
    [Serializable]
    public class DestinoBEList : List<DestinoBE> {
        public void ordenar(string propertyName, direccionOrden Direction)
        {
            DestinoComparer dc = new DestinoComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class DestinoComparer : IComparer<DestinoBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public DestinoComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(DestinoBE x, DestinoBE y)
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
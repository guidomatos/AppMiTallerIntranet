using System;
using System.Collections.Generic;
using System.Reflection;


namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class NombreComercialBE : BaseBE
    {
        #region "Atributos"

        private Int32 id_nombre_comercial;
        private String nom_comercial;
        private String nom_product_name;
        private Int32 id_marca;
        private String nom_marca;
        private Int32 id_modelo;
        private String nom_modelo;
        private DateTime fec_creacion;
        private String cod_usu_crea;
        private String dsc_estado;
        private String cod_estado;
        private DateTime fec_modi;
        private String cod_usu_modi;
        private String nom_estacion;
        private String nom_usuario_red;
        private Int32 cod_Creacion;
        private String _Idtarifario;
        private String _Codcanalventa;
        private String _Canalventa;
        private String _Marca;
        private String _Modelo;
        private String _Nombrecomercial;
        private String _Preciolista;
        private String _Preciocierre;
        private String _Preciocierrejv;
        private String _Preciocierregv;
        private String _Preciocierregg;
        private String _Preciodealer;
        private String _CodModelo;
        private String _cadena_total;
        private String _CodNombreComercial;
        private String _CodMarca;
        private String _an_modelo;
        private String _an_fabricacion;

        //@001 - DAC - Inicio
        private String _PreciolistaSoles;
        private String _PreciocierreSoles;
        private String _PreciocierrejvSoles;
        private String _PreciocierregvSoles;
        private String _PreciocierreggSoles;
        private String _PreciodealerSoles;
        //@001 - DAC - Fin

        //@002 I
        private int _nid_marcaSeminueva;
        //@002 F

        
        private string _GrupoComercial { get; set; }
        

        //@004 I
        public String no_marca_gerencial { get; set; }
        //@004 F

        #endregion

        #region "Propiedades"

        public Int32 Id_modelo
        {
            get { return id_modelo; }
            set { id_modelo = value; }
        }
        public Int32 Id_marca
        {
            get { return id_marca; }
            set { id_marca = value; }
        }
        public Int32 Cod_Creacion
        {
            get { return cod_Creacion; }
            set { cod_Creacion = value; }
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
        public String Nom_modelo
        {
            get { return nom_modelo; }
            set { nom_modelo = value; }
        }
        public String Nom_marca
        {
            get { return nom_marca; }
            set { nom_marca = value; }
        }
        public String Nom_product_name
        {
            get { return nom_product_name; }
            set { nom_product_name = value; }
        }
        public String Nom_comercial
        {
            get { return nom_comercial; }
            set { nom_comercial = value; }
        }
        public Int32 Id_nombre_comercial
        {
            get { return id_nombre_comercial; }
            set { id_nombre_comercial = value; }
        }
        public String Idtarifario
        {
            get { return _Idtarifario; }
            set { _Idtarifario = value; }
        }
        public String Codcanalventa
        {
            get { return _Codcanalventa; }
            set { _Codcanalventa = value; }
        }
        public String Canalventa
        {
            get { return _Canalventa; }
            set { _Canalventa = value; }
        }
        public String Marca
        {
            get { return _Marca; }
            set { _Marca = value; }
        }
        public String Modelo
        {
            get { return _Modelo; }
            set { _Modelo = value; }
        }
        public String Nombrecomercial
        {
            get { return _Nombrecomercial; }
            set { _Nombrecomercial = value; }
        }
        public String Preciolista
        {
            get { return _Preciolista; }
            set { _Preciolista = value; }
        }
        public String Preciocierre
        {
            get { return _Preciocierre; }
            set { _Preciocierre = value; }
        }
        public String Preciocierrejv
        {
            get { return _Preciocierrejv; }
            set { _Preciocierrejv = value; }
        }
        public String Preciocierregv
        {
            get { return _Preciocierregv; }
            set { _Preciocierregv = value; }
        }
        public String Preciocierregg
        {
            get { return _Preciocierregg; }
            set { _Preciocierregg = value; }
        }
        public String Preciodealer
        {
            get { return _Preciodealer; }
            set { _Preciodealer = value; }
        }
        public String CodModelo
        {
            get { return _CodModelo; }
            set { _CodModelo = value; }
        }
        public String CodNombreComercial
        {
            get { return _CodNombreComercial; }
            set { _CodNombreComercial = value; }
        }
        public String cadena_total
        {
            get { return _cadena_total; }
            set { _cadena_total = value; }
        }
        public String CodMarca
        {
            get { return _CodMarca; }
            set { _CodMarca = value; }
        }
        public String an_modelo
        {
            get { return _an_modelo; }
            set { _an_modelo = value; }
        }
        public String an_fabricacion
        {
            get { return _an_fabricacion; }
            set { _an_fabricacion = value; }
        }

        //@001 - DAC - Inicio
        public String PreciolistaSoles
        {
            get { return _PreciolistaSoles; }
            set { _PreciolistaSoles = value; }
        }
        public String PreciocierreSoles
        {
            get { return _PreciocierreSoles; }
            set { _PreciocierreSoles = value; }
        }
        public String PreciocierrejvSoles
        {
            get { return _PreciocierrejvSoles; }
            set { _PreciocierrejvSoles = value; }
        }
        public String PreciocierregvSoles
        {
            get { return _PreciocierregvSoles; }
            set { _PreciocierregvSoles = value; }
        }
        public String PreciocierreggSoles
        {
            get { return _PreciocierreggSoles; }
            set { _PreciocierreggSoles = value; }
        }
        public String PreciodealerSoles
        {
            get { return _PreciodealerSoles; }
            set { _PreciodealerSoles = value; }
        }
        //@001 - DAC - Fin

        //@002 I
        public int nid_marcaSeminueva
        {
            get { return _nid_marcaSeminueva; }
            set { _nid_marcaSeminueva = value; }
        }
        //@002 F

        
        public String GrupoComercial
        {
            get { return _GrupoComercial; }
            set { _GrupoComercial = value; }
        }
        


        #endregion

        #region "Metodos"

        public Boolean Validar()
        {
            Boolean Res = true;
            //   if (_Error != String.Empty) Res = false;
            return Res;
        }

        #endregion
    }

    [Serializable]
    public class Nombres_ComercialesBEList : List<NombreComercialBE>
    {
        public void ordenar(string propertyName, direccionOrden Direction)
        {
            Nombres_ComercialesComparer dc = new Nombres_ComercialesComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }

    class Nombres_ComercialesComparer : IComparer<NombreComercialBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public Nombres_ComercialesComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(NombreComercialBE x, NombreComercialBE y)
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

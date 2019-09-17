using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class ClienteBE
    {

        public int nid_cliente_direccion { get; set; }
        public int nid_pais { get; set; }
        public string nu_fax { get; set; }
        public string coddpto { get; set; }
        public string codprov { get; set; }
        public string coddist { get; set; }
        public string no_dpto { get; set; }
        public string no_prov { get; set; }
        public string no_dist { get; set; }
        public string no_ubigeo { get; set; }
        public string no_direccion { get; set; }
        public string co_usuario { get; set; }
        public string no_usuario_red { get; set; }

        public Int32 fl_identidad_validada { get; set; }

        private int _nid_cliente;

	    public int nid_cliente
	    {
		    get { return _nid_cliente;}
		    set { _nid_cliente = value;}
	    }
	
        private string _no_cliente;

	    public string no_cliente
	    {
		    get { return _no_cliente;}
		    set { _no_cliente = value;}
	    }
	
        private string _no_ape_pat;

	    public string no_ape_pat
	    {
		    get { return _no_ape_pat;}
		    set { _no_ape_pat = value;}
	    }
	
        private string _no_ape_mat;

	    public string no_ape_mat
	    {
		    get { return _no_ape_mat;}
		    set { _no_ape_mat = value;}
	    }
	
        private string _co_tipo_documento;

	    public string co_tipo_documento
	    {
		    get { return _co_tipo_documento;}
		    set { _co_tipo_documento = value;}
	    }
	
        private string _nu_documento;

	    public string nu_documento
	    {
		    get { return _nu_documento;}
		    set { _nu_documento = value;}
	    }
	
        private string _no_correo;

	    public string no_correo
	    {
		    get { return _no_correo;}
		    set { _no_correo = value;}
	    }

        private string _no_correo_trabajo;

        public string no_correo_trabajo
        {
            get { return _no_correo_trabajo; }
            set { _no_correo_trabajo = value; }
        }

        private string _no_correo_alter;

        public string no_correo_alter
        {
            get { return _no_correo_alter; }
            set { _no_correo_alter = value; }
        }
	
        private string _nu_telefono;

	    public string nu_telefono
	    {
		    get { return _nu_telefono;}
		    set { _nu_telefono = value;}
	    }

        private string _nu_tel_oficina;

        public string nu_tel_oficina
        {
            get { return _nu_tel_oficina; }
            set { _nu_tel_oficina = value; }
        }
	
	
        private string _nu_celular;

	    public string nu_celular
	    {
		    get { return _nu_celular;}
		    set { _nu_celular = value;}
	    }

        private string _nu_celular_alter;

        public string nu_celular_alter
        {
            get { return _nu_celular_alter; }
            set { _nu_celular_alter = value; }
        }

        private string _fl_inactivo;

        public string fl_inactivo
        {
            get { return _fl_inactivo; }
            set { _fl_inactivo = value; }
        }

        private string _Documento;

        public string Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }

        private string _co_tipo_cliente;

        public string co_tipo_cliente
        {
            get { return _co_tipo_cliente; }
            set { _co_tipo_cliente = value; }
        }
	

        private string _Estado;

        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        private string _ind_accion;

        public string ind_accion
        {
            get { return _ind_accion; }
            set { _ind_accion = value; }
        }

        private string _co_usuario_crea;

        public string co_usuario_crea
        {
            get { return _co_usuario_crea; }
            set { _co_usuario_crea = value; }
        }

        private string _co_usuario_red;

        public string co_usuario_red
        {
            get { return _co_usuario_red; }
            set { _co_usuario_red = value; }
        }

        private string _no_estacion_red;

        public string no_estacion_red
        {
            get { return _no_estacion_red; }
            set { _no_estacion_red = value; }
        }

        public int nid_pais_celular { get; set; }
        public int nid_pais_telefono { get; set; }
        public string nu_anexo_telefono { get; set; }
    }

    [Serializable]
    public class ClienteBEList : List<ClienteBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            ClienteBEComparer dc = new ClienteBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class ClienteBEComparer : IComparer<ClienteBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public ClienteBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(ClienteBE x, ClienteBE y)
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

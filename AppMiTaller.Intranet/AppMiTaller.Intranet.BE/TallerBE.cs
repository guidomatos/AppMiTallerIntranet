using System;
using System.Collections.Generic;
using System.Reflection;


namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class TallerBE
    {
        public String Co_perfil_usuario { get; set; }
        public Int32 Nid_usuario { get; set; }
        public String Fl_tipo { get; set; }
        public String ID { get; set; }
        public String DES { get; set; }
        public String co_valoracion { get; set; }
        public String fl_taxi_BO { get; set; }
        public String fl_taxi_FO { get; set; }
        public String Op { get; set; }
        public int nid_taller { get; set; }
        public String no_dias { get; set; }
        public string co_taller { get; set; }
        public string no_taller { get; set; }
        public string coddpto { get; set; }
        public string nomdpto { get; set; }
        public string codprov { get; set; }
        public string nomprov { get; set; }
        public string coddist { get; set; }
        public string nomdist { get; set; }
        public int nid_ubica { get; set; }
        public string no_ubica { get; set; }
        public string fl_activo { get; set; }
        public string nu_telefono { get; set; }
        public string tx_mapa_taller { get; set; }
        public string tx_url_taller { get; set; }
        public string no_direccion { get; set; }
        public int co_intervalo_atenc { get; set; }
        public string co_usuario { get; set; }
        public string co_usuario_red { get; set; }
        public string no_estacion_red { get; set; }
        public Int32 Nid_marca { get; set; }
        public String No_marca { get; set; }
        public Int32 Nid_modelo { get; set; }
        public String No_modelo { get; set; }
        public Int32 Nid_tserv { get; set; }
        public String No_tserv { get; set; }
        public Int32 Nid_serv { get; set; }
        public String No_serv { get; set; }
        public String No_valor1 { get; set; }
        public Int32 Cod_intervalo { get; set; }
        public String Num_intervalo { get; set; }
        public Int32 Dd_atencion { get; set; }
        public Int32 qt_capacidad { get; set; }
        public String HoraInicio { get; set; }
        public String Ubigeo { get; set; }
        public String HoraFin { get; set; }
        public Int32 Nid_dia_exceptuado { get; set; }
        public DateTime Fe_exceptuada { get; set; }
        public String Fe_exceptuada1 { get; set; }
        public String Di_ubica { get; set; }
        public String Nu_telefono_ubica { get; set; }
        public String Co_usuario_modi { get; set; }
        public List<TallerHorariosBE> lstTallerHorarios { get; set; }
        public List<TallerDiasExceptuadosBE> lstTallerDiasExceptuados { get; set; }
        public List<TallerServiciosBE> lstTallerServicios { get; set; }
        public List<TallerModelosBE> lstTallerModelos { get; set; }

        public string descripcion { get; set; }
        public TallerBE() { 
            this.lstTallerHorarios = new List<TallerHorariosBE>();
            this.lstTallerDiasExceptuados = new List<TallerDiasExceptuadosBE>();
            this.lstTallerServicios = new List<TallerServiciosBE>();
            this.lstTallerModelos = new List<TallerModelosBE>();
        }

    }

    [Serializable]
    public class TallerBEList : List<TallerBE>
    {
        
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            TallerBEComparer dc = new TallerBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class TallerBEComparer : IComparer<TallerBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public TallerBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(TallerBE x, TallerBE y)
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

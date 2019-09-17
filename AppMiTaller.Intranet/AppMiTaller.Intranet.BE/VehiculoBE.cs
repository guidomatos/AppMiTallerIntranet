using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{   

    [Serializable]
    public class VehiculoBE
    {
        public int nid_vehiculo { get; set; }
        public int nid_propietario { get; set; }
        public int nid_cliente { get; set; }
        public int nid_contacto { get; set; }
        public string nu_placa { get; set; }
        public string nu_vin { get; set; }
        public int nid_marca { get; set; }
        public int nid_modelo { get; set; }
        public Int64 qt_km_actual { get; set; }
        public string fl_reg_manual { get; set; }
        public string co_usuario { get; set; }
        public string co_usuario_red { get; set; }
        public string no_estacion_red { get; set; }
        public string fl_activo { get; set; }
        public string ind_accion { get; set; }
        public string no_marca { get; set; }
        public string no_modelo { get; set; }
        public string pro_co_tipo_cliente { get; set; }
        public string pro_co_tipo_documento { get; set; }
        public string pro_nu_documento { get; set; }
        public string clie_co_tipo_cliente { get; set; }
        public string clie_co_tipo_documento { get; set; }
        public string clie_nu_documento { get; set; }
        public string cont_co_tipo_cliente { get; set; }
        public string cont_co_tipo_documento { get; set; }
        public string cont_nu_documento { get; set; }
        public int DET_cod_cliente { get; set; }
        public string DET_co_tipo_cliente { get; set; }
        public string DET_co_tipo_documento { get; set; }
        public string DET_nu_documento { get; set; }
        public string DET_no_cliente { get; set; }
        public string DET_NOMBRES_RZ { get; set; }
        public string DET_no_ape_mat { get; set; }
        public string DET_no_ape_pat { get; set; }
        public string DET_nu_telefono { get; set; }
        public string DET_nu_telefono2 { get; set; }
        public string DET_nu_celular { get; set; }
        public string DET_nu_celular2 { get; set; }
        public string DET_no_correo { get; set; }
        public string DET_no_correo_trab { get; set; }
        public string DET_no_correo_alter { get; set; }
        public Int32 nu_anio { get; set; }
        public string co_tipo { get; set; }
        public string no_tipo { get; set; }
        public Int32 fl_identidad_validada { get; set; }
        public string per_pro { get; set; }
        public string tipodoc_pro { get; set; }
        public string nrodoc_pro { get; set; }
        public string per_clie { get; set; }
        public string tipodoc_clie { get; set; }
        public string nrodoc_clie { get; set; }
        public string per_cont { get; set; }
        public string tipodoc_cont { get; set; }
        public string nrodoc_cont { get; set; }
        public int nid_pais_celular { get; set; }
        public int nid_pais_telefono { get; set; }
        public string nu_anexo_telefono { get; set; }
    }

    [Serializable]
    public class VehiculoBEList : List<VehiculoBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            VehiculoBEComparer dc = new VehiculoBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class VehiculoBEComparer : IComparer<VehiculoBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public VehiculoBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(VehiculoBE x, VehiculoBE y)
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

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class TallerHorariosExcepcionalBE
    {
        #region "VARIABLES DE BUSQUEDA"
        public int VI_nid_propietario { get; set; }
        public string VI_no_descripcion { get; set; }
        public string VI_fe_inicio { get; set; }
        public string VI_fe_fin { get; set; }
        public string VI_fl_activo { get; set; }
        #endregion

        #region "VARIABLES DE SELECT BUSQUEDA"
        public string grid_cod_hor_excep { get; set; }
        public string grid_codTllr_hor_excep { get; set; }
        public string grid_des_hor_excep { get; set; }
        public string grid_fecini_hor_excep { get; set; }
        public string grid_fecfin_hor_excep { get; set; }
        public string grid_idestado_hor_excep { get; set; }
        public string grid_estado_hor_excep { get; set; }
        #endregion

        #region "VARIABLES HORARIO DE ATENCION X TALLER"
        public int VI_dd_atencion { get; set; }
        public string TllrHorIniAten { get; set; }
        public string TllrHorfinAten { get; set; }
        public string TllrIntervaloAten { get; set; }
        #endregion

        #region "VARIABLE CABE HOR. EXCEPCIONAL"
        public int VI_nid_horario_HECabe { get; set; }
        public int VI_nid_propietario_HECabe { get; set; }
        public string VI_no_descripcion_HECabe { get; set; }
        public string VI_fe_inicio_HECabe { get; set; }
        public string VI_fe_fin_HECabe { get; set; }
        public string VI_fl_tipo_HECabe { get; set; }
        public string VI_co_usuario_crea_HECabe { get; set; }
        public string VI_co_usuario_modi_HECabe { get; set; }
        public string VI_co_usuario_red_HECabe { get; set; }
        public string VI_no_estacion_red_HECabe { get; set; }
        public string VI_fl_activo_HECabe { get; set; }
        #endregion

        #region "VARIABLE DETA HOR. EXCEPCIONAL"
        public int VI_nid_horario_HEDeta { get; set; }
        public int VI_dd_atencion_HEDeta { get; set; }
        public string VI_ho_rango1_HEDeta { get; set; }
        public string VI_ho_rango2_HEDeta { get; set; }
        public string VI_ho_rango3_HEDeta { get; set; }
        public string VI_co_usuario_crea_HEDeta { get; set; }
        public string VI_co_usuario_modi_HEDeta { get; set; }
        public string VI_co_usuario_red_HEDeta { get; set; }
        public string VI_no_estacion_red_HEDeta { get; set; }
        public string VI_fl_activo_HEDeta { get; set; }
        #endregion

        #region "VARIABLES DE SELECT DETA HOR. EXCEPCIONAL"
        public int DetIdDia { get; set; }
        public string DetHo_rango1 { get; set; }
        public string DetHo_rango2 { get; set; }
        public string DetHo_rango3 { get; set; }
        #endregion
    }

    [Serializable]
    public class TallerHorariosExcepcionalBEList : List<TallerHorariosExcepcionalBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direction)
        {
            TallerHorariosExcepcionalBEComparer dc = new TallerHorariosExcepcionalBEComparer(propertyName, Direction);
            this.Sort(dc);
        }
    }
    class TallerHorariosExcepcionalBEComparer : IComparer<TallerHorariosExcepcionalBE>
    {
        string _prop = "";
        direccionOrden _dir;

        public TallerHorariosExcepcionalBEComparer(string propertyName, direccionOrden Direction)
        {
            _prop = propertyName;
            _dir = Direction;
        }

        public int Compare(TallerHorariosExcepcionalBE x, TallerHorariosExcepcionalBE y)
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

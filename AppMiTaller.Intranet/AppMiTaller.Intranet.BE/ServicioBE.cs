using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class ServicioBE
    {
        public int Id_Servicio { get; set; }
        public String Co_Servicio { get; set; }
        public String No_Servicio { get; set; }
        public int Id_TipoServicio { get; set; }
        public String No_tipo_servicio { get; set; }
        public int Qt_tiempo_prom { get; set; }
        public String Fl_activo { get; set; }
        public int nid_modelo { get; set; }
        public int nid_tabla { get; set; }
        public string XmlServicios { get; set; }
        public String Fl_quick_service { get; set; }
        public String no_dias_validos { get; set; }
        public int nid_usuario { get; set; }
        public long nu_item { get; set; }
        public int nid_tipo_servicio { get; set; }
        public int nu_registro_por_pagina { get; set; }
        public string no_tipo_servicio { get; set; }
        public int nid_servicio { get; set; }
        public int nid_marca { get; set; }
        public string no_servicio { get; set; }
        public string no_marca { get; set; }
        public string no_modelo { get; set; }
        public int nid_mae_servicio_especifico_modelo { get; set; }
        public string fl_inactivo { get; set; }
        public string co_usuario_crea { get; set; }
        public string co_usuario_cambio { get; set; }
        public string no_estacion_red { get; set; }
        public string no_usuario_red { get; set; }
    }
    [Serializable]
    public class ServicioBEList : List<ServicioBE>
    {
        public void Ordenar(string propertyName, direccionOrden Direccion)
        {
            ServicioBEComparer dc = new ServicioBEComparer(propertyName, Direccion);
            this.Sort(dc);
        }
    }
    class ServicioBEComparer : IComparer<ServicioBE>
    {
        string _prop="";
        direccionOrden _dir;

        public ServicioBEComparer(string propertyName, direccionOrden Direccion)
        {
            _prop = propertyName;
            _dir = Direccion;
        }
        public int Compare(ServicioBE x, ServicioBE y)
        {


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

using System;
using System.Linq;
using System.Xml.Linq;
using AppMiTaller.Intranet.BL;
using AppMiTaller.Intranet.BE;
using System.Collections.Generic;
using System.Web.Services;

public partial class SRC_Mantenimiento_SRC_ServicioModelo_Bandeja : PageBase
{
    #region Atributos
    private static readonly ComboBL oComboBl = new ComboBL();
    private static readonly ServicioBL oServicioBl = new ServicioBL();
    #endregion Atributos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Inicializar();
        }

    }

    #region Metodos
    private void Inicializar()
    {

    }

    #endregion Metodos


    #region WebMethod
    [WebMethod]
    public static System.Collections.ArrayList ObtenerMarcas()
    {
        System.Collections.ArrayList arrayMarca = new System.Collections.ArrayList();
        var oMarcaList = oComboBl.GetAllComboBe(0, TipoTabla.MARCA.GetHashCode());
        var objMarca = oMarcaList.Select(item => new
        {
            nombre = item.no_nombre,
            codigo = item.nid_hijo,
        });
        arrayMarca.AddRange(objMarca.ToArray());
        return arrayMarca;
    }

    [WebMethod]
    public static System.Collections.ArrayList ObtenerModelos(string snidMarca)
    {

        int nidMarca = 0;
        int.TryParse(snidMarca, out nidMarca);
        System.Collections.ArrayList arrayModelo = new System.Collections.ArrayList();
        var oModeloList = oComboBl.GetAllComboBe(nidMarca, TipoTabla.MODELO.GetHashCode());
        var objModelo = oModeloList.Select(item => new
        {
            nombre = item.no_nombre,
            codigo = item.nid_hijo,
        });
        arrayModelo.AddRange(objModelo.ToArray());
        return arrayModelo;
    }

    [WebMethod]
    public static System.Collections.ArrayList ObtenerBandeja(string[] parametros)
    {
        int nidMarca = 0;
        int nidModelo = 0;
        int paginaActual = 0;
        int.TryParse(parametros[0], out nidMarca);
        int.TryParse(parametros[1], out nidModelo);
        int.TryParse(parametros[2], out paginaActual);
        var orderby = parametros[3];
        var orderbydirection = parametros[4];
        var resultado = 0;
        System.Collections.ArrayList arrayBandeja = new System.Collections.ArrayList();
        int cantidadregistros = 0;
        try
        {
            var oBandejaList = oServicioBl.GetAllServicioModelo(nidMarca, nidModelo, paginaActual, ref cantidadregistros, orderby, orderbydirection).ToList();
            decimal cdPagina = Convert.ToDecimal(cantidadregistros) / 10;
            int cPagina = Convert.ToInt32(Math.Ceiling(cdPagina));
            if (oBandejaList.Count > 0)
            {

                var BandejaHtml = Convert.ToString(ServicioBandejaHtml(cantidadregistros, paginaActual, orderby, orderbydirection, cPagina, oBandejaList));
                resultado = 1;
                arrayBandeja.Add(resultado);
                arrayBandeja.Add(BandejaHtml);
            }
            else
            {
                resultado = 0;
                var BandejaHtml = Convert.ToString(ServicioBandejaHtml(cantidadregistros, paginaActual, orderby, orderbydirection, cPagina, oBandejaList));

                arrayBandeja.Add(resultado);
                arrayBandeja.Add(BandejaHtml);
            }
        }
        catch //(Exception ex)
        {
            resultado = -1;
            arrayBandeja.Add(resultado);
        }


        return arrayBandeja;
    }

    [WebMethod]
    public static System.Collections.ArrayList ObtenerDisponibles(string snidMarca, string snidModelo, string orderby, string orderbydirection)
    {
        var resultado = 1;
        int nidMarca = 0;
        int nidModelo = 0;
        int.TryParse(snidMarca, out nidMarca);
        int.TryParse(snidModelo, out nidModelo);
        string ServicioBandejaHtml = string.Empty;
        string ServicioBandejaHeaderHtml = string.Empty;
        string textDisponibles = string.Empty;
        int cantidadRegistros = 0;
        System.Collections.ArrayList arrayBandejaDisponibles = new System.Collections.ArrayList();
        try
        {
            var oBandejaList = oServicioBl.GetAllServicioBe(nidMarca, nidModelo, orderby, orderbydirection).ToList();


            cantidadRegistros = oBandejaList.Count();
            textDisponibles = string.Format("Servicios no asignados: {0}", cantidadRegistros);
            if (cantidadRegistros > 0)
            {
                ServicioBandejaHtml = Convert.ToString(AsignacionBandejaHtml(oBandejaList, "Disponibles", orderby, orderbydirection));
                ServicioBandejaHeaderHtml = Convert.ToString(AsignacionBandejaHeaderHtml("Disponibles", orderby, orderbydirection));
            }
            else
            {
                resultado = 0;
                ServicioBandejaHtml = Convert.ToString(AsignacionBandejaHtml(oBandejaList, "Disponibles", orderby, orderbydirection));
                ServicioBandejaHeaderHtml = Convert.ToString(AsignacionBandejaHeaderHtml("Disponibles", orderby, orderbydirection));

            }

        }
        catch //(Exception ex)
        {
            resultado = -1;
        }

        arrayBandejaDisponibles.Add(resultado);
        arrayBandejaDisponibles.Add(ServicioBandejaHeaderHtml);
        arrayBandejaDisponibles.Add(ServicioBandejaHtml);
        arrayBandejaDisponibles.Add(textDisponibles);
        return arrayBandejaDisponibles;
    }

    [WebMethod]
    public static System.Collections.ArrayList ObtenerAsignados(string snidMarca, string snidModelo, string orderby, string orderbydirection)
    {
        var resultado = 1;
        int nidMarca = 0;
        int nidModelo = 0;
        int.TryParse(snidMarca, out nidMarca);
        int.TryParse(snidModelo, out nidModelo);
        string ServicioBandejaHtml = string.Empty;
        string ServicioBandejaHeaderHtml = string.Empty;
        string textAsignados = string.Empty;
        int cantidadRegistros = 0;
        System.Collections.ArrayList arrayBandejaDisponibles = new System.Collections.ArrayList();
        try
        {

            var oBandejaList = oServicioBl.GetAllServicioAsignadoBe(nidMarca, nidModelo, orderby, orderbydirection).ToList();
            cantidadRegistros = oBandejaList.Count();
            textAsignados = string.Format("Servicios asignados: {0}", cantidadRegistros);
            if (cantidadRegistros > 0)
            {

                ServicioBandejaHtml = Convert.ToString(AsignacionBandejaHtml(oBandejaList, "Asignados", orderby, orderbydirection));
                ServicioBandejaHeaderHtml = Convert.ToString(AsignacionBandejaHeaderHtml("Asignados", orderby, orderbydirection));
            }
            else
            {
                resultado = 0;
                ServicioBandejaHtml = Convert.ToString(AsignacionBandejaHtml(new List<ServicioBE>(), "Asignados", orderby, orderbydirection));
                ServicioBandejaHeaderHtml = Convert.ToString(AsignacionBandejaHeaderHtml("Asignados", orderby, orderbydirection));

            }
        }
        catch //(Exception ex)
        {
            resultado = -1;
        }

        arrayBandejaDisponibles.Add(resultado);
        arrayBandejaDisponibles.Add(ServicioBandejaHeaderHtml);
        arrayBandejaDisponibles.Add(ServicioBandejaHtml);
        arrayBandejaDisponibles.Add(textAsignados);
        return arrayBandejaDisponibles;
    }

    [WebMethod]
    public static System.Collections.ArrayList AsignarServicios(string[] datos, string[] seleccion)
    {
        System.Collections.ArrayList arrayServicios = new System.Collections.ArrayList();

        var resultado = 0;
        int nidMarca = 0;
        int.TryParse(datos[0], out nidMarca);
        int nidModelo = 0;
        string fl_inactivo = datos[2];
        int.TryParse(datos[1], out nidModelo);
        try
        {
            if (nidMarca != 0 || nidModelo != 0)
            {
                var XmlServicios = string.Empty;
                if (seleccion.Length > 0)
                {

                    XmlServicios = new XElement("ROOT", seleccion.Select(i => new XElement("doc",
                                   new XAttribute("nid_servicio", i),
                                   new XAttribute("nid_modelo", nidModelo)))).ToString();
                    oServicioBl.AddServicioModelo(new ServicioBE { nid_marca = nidMarca, nid_modelo = nidModelo, XmlServicios = XmlServicios, co_usuario_crea = "", no_usuario_red = "", no_estacion_red = "", fl_inactivo = fl_inactivo });

                    resultado = 1;
                    arrayServicios.Add(resultado);

                }
                else
                {
                    resultado = 0;
                    arrayServicios.Add(resultado);
                }

            }
            else
            {
                resultado = 0;
                arrayServicios.Add(resultado);
            }
        }
        catch (Exception)
        {
            resultado = -1;
            arrayServicios.Add(resultado);
        }


        return arrayServicios;
    }

    [WebMethod]
    public static System.Collections.ArrayList EliminarServiciosModelo(string[] parametros)
    {


        System.Collections.ArrayList arrayBandeja = new System.Collections.ArrayList();
        var resultado = 0;
        try
        {
            if (parametros.Length > 0)
            {

                var seleccionXML = new XElement("ROOT", parametros.Select(i => new XElement("doc",
                               new XAttribute("nid_modelo", i)))).ToString();
                oServicioBl.UpdateServicioModelo(new ServicioBE { XmlServicios = seleccionXML, co_usuario_cambio = "", no_usuario_red = "", no_estacion_red = "" });
                resultado = 1;
                arrayBandeja.Add(resultado);
            }
            else
            {
                resultado = 0;
                arrayBandeja.Add(resultado);
            }
        }
        catch (Exception)
        {
            resultado = -1;
            arrayBandeja.Add(resultado);
        }

        return arrayBandeja;

    }

    private static XElement AsignacionBandejaHtml(List<ServicioBE> oListServicioBE, string tabla, string orderby, string orderbydirection)
    {
        var AsignacionBandejaHtml = new XElement("table",
        new XAttribute("style", "border-color:White;border-width:0px;width:380px;border-collapse:collapse;"),
        new XAttribute("border", "0"),
        new XAttribute("rules", "all"),
        new XAttribute("cellspacing", "0"),
        new XAttribute("cellpadding", "0"),
        new XAttribute("id", string.Format("gv{0}", tabla)),
        //new XElement("thead",
        //new XElement("tr",
        //new XAttribute("class", "CabeceraGrilla"),
        //    //Inicio Definir Cabecera
        //    //--------
        //new XElement("th", new XAttribute("align", "center"), new XElement("input", new XAttribute("type", "checkbox"), tabla == "Disponibles" ? new XAttribute("onclick", "CheckAllD(this)") : new XAttribute("onclick", "CheckAllA(this)"), new XAttribute("id", "ckHeader")),
        //    //--------
        //new XAttribute("style", "width:1%;"),
        //new XAttribute("scope", "col")),
        //    //--------------------------------
        //new XElement("th", "Item",
        //new XAttribute("style", "width:12%;"),
        //new XAttribute("scope", "col"),
        //  new XAttribute("onClick", 
        //    tabla == "Asignados" ? 
        //    string.Format("fc_cargar_asignados(null, null, 'nu_item', '{0}')", orderbydirection == "D" ? "A" : "D") : 
        //    string.Format("fc_cargar_disponibles(null, null, 'nu_item', '{0}')", orderbydirection == "D" ? "A" : "D"))
        //),

        //    //--------------------------------
        //new XElement("th", "Tipo Servicio",
        //new XAttribute("style", "width:43%;"),
        //new XAttribute("scope", "col"),
        //   new XAttribute("onClick", 
        //     tabla == "Asignados" ? 
        //     string.Format("fc_cargar_asignados(null, null, 'no_tipo_servicio', '{0}')",orderbydirection == "D" ? "A" : "D") : 
        //     string.Format("fc_cargar_disponibles(null, null, 'no_tipo_servicio', '{0}')", orderbydirection == "D" ? "A" : "D"))),


        //    //--------------------------------
        //new XElement("th", "Servicio",
        //new XAttribute("style", "width:44%;"),
        //new XAttribute("scope", "col"),
        //     new XAttribute("onClick", 
        //     tabla == "Asignados" ? string.Format("fc_cargar_asignados(null, null, 'no_servicio', '{0}')", orderbydirection == "D" ? "A" : "D") :
        //     string.Format("fc_cargar_disponibles(null, null, 'no_servicio', '{0}')", orderbydirection == "D" ? "A" : "D"))))), 
        new XElement("tbody",
        //--------------------------------


        //Fin Definir Cabecera
        oListServicioBE.Select(i =>
                   new XElement("tr",
                   new XAttribute("class", "textogrilla"),
                   new XAttribute("style", " cursor: hand;"),
                   new XAttribute("id", string.Format("{0}", i.nid_servicio)),
                   new XAttribute("bgcolor", i.nid_tabla % 2 == 0 ? "#E3E7F2" : "#FFFFFF"),
                   new XAttribute("onClick", string.Format("selTR(this,'gv{0}');", tabla)),
                   //Inicio Definir Filas

                   new XElement("td", new XAttribute("align", "center"), new XAttribute("style", "width:1%;"), new XElement("input", new XAttribute("type", "checkbox"), new XAttribute("id", "myCheck"), new XAttribute("value", string.Format("{0}", i.nid_servicio))),
                   new XElement("td", new XAttribute("align", "center"), new XAttribute("style", "width:12%;"), i.nu_item),
                   new XElement("td", new XAttribute("align", "left"), new XAttribute("style", "width:43%;"), i.no_tipo_servicio == null ? string.Empty : i.no_tipo_servicio),
                   new XElement("td", new XAttribute("align", "center"), new XAttribute("style", "width:44%;"), i.no_servicio == null ? string.Empty : i.no_servicio)))),
                         oListServicioBE.Count == 0 ? new XElement("tr",
                   new XAttribute("class", "textogrilla"),
                   new XElement("td",
                   new XAttribute("colspan", "4"),
                   new XAttribute("align", "center"), "No existen resultados para esta consulta.")) : null)

                   //new XElement("tfoot",
                   //new XElement("tr", new XAttribute("class", "Footer"),
                   //new XElement("td", new XAttribute("colspan", "5"))))
                   );

        return AsignacionBandejaHtml;
    }

    private static XElement AsignacionBandejaHeaderHtml(string tabla, string orderby, string orderbydirection)
    {
        var AsignacionBandejaHtml = new XElement("table",
        new XAttribute("style", "border-color:White;border-width:0px;width:380px;border-collapse:collapse;"),
        new XAttribute("border", "0"),
        new XAttribute("rules", "all"),
        new XAttribute("cellspacing", "0"),
        new XAttribute("cellpadding", "0"),
        new XAttribute("id", string.Format("gv{0}", tabla)),
            new XElement("thead",
            new XElement("tr",
            new XAttribute("class", "CabeceraGrilla"),
            //Inicio Definir Cabecera
            //--------
            new XElement("th", new XAttribute("align", "center"), new XElement("input", new XAttribute("type", "checkbox"), tabla == "Disponibles" ? new XAttribute("onclick", "CheckAllD(this)") : new XAttribute("onclick", "CheckAllA(this)"), new XAttribute("id", "ckHeader")),
            //--------
            new XAttribute("style", "width:1%;"),
            new XAttribute("scope", "col")),
            //--------------------------------
            new XElement("th", "Item",
            new XAttribute("style", "width:12%;"),
            new XAttribute("scope", "col"),
              new XAttribute("onClick",
                tabla == "Asignados" ?
                string.Format("fc_cargar_asignados(null, null, 'nu_item', '{0}')", orderbydirection == "D" ? "A" : "D") :
                string.Format("fc_cargar_disponibles(null, null, 'nu_item', '{0}')", orderbydirection == "D" ? "A" : "D"))
            ),

            //--------------------------------
            new XElement("th", "Tipo Servicio",
            new XAttribute("style", "width:43%;"),
            new XAttribute("scope", "col"),
               new XAttribute("onClick",
                 tabla == "Asignados" ?
                 string.Format("fc_cargar_asignados(null, null, 'no_tipo_servicio', '{0}')", orderbydirection == "D" ? "A" : "D") :
                 string.Format("fc_cargar_disponibles(null, null, 'no_tipo_servicio', '{0}')", orderbydirection == "D" ? "A" : "D"))),


            //--------------------------------
            new XElement("th", "Servicio",
            new XAttribute("style", "width:44%;"),
            new XAttribute("scope", "col"),
                 new XAttribute("onClick",
                 tabla == "Asignados" ? string.Format("fc_cargar_asignados(null, null, 'no_servicio', '{0}')", orderbydirection == "D" ? "A" : "D") :
                 string.Format("fc_cargar_disponibles(null, null, 'no_servicio', '{0}')", orderbydirection == "D" ? "A" : "D")))))


                   );

        return AsignacionBandejaHtml;
    }

    private static XElement ServicioBandejaHtml(int totalOK, int page, string orderby, string orderbydirection, int cantidadPaginas, List<ServicioBE> oListServicioBE)
    {
        var ServicioBandejaHtml = new XElement("table",
        new XAttribute("style", "border-color:White;border-width:0px;width:950px;border-collapse:collapse;"),
        new XAttribute("border", "0"),
        new XAttribute("rules", "all"),
        new XAttribute("cellspacing", "0"),
        new XAttribute("cellpadding", "0"),
        new XAttribute("id", "gvServicios"),
        new XElement("thead",
        new XElement("tr",
        new XAttribute("class", "CabeceraGrilla"),
        //Inicio Definir Cabecera
        //--------
        new XElement("th", new XAttribute("align", "center"), new XElement("input", new XAttribute("type", "checkbox"), new XAttribute("onclick", "CheckAllB(this)"), new XAttribute("id", "ckHeader")),
        //--------
        new XAttribute("style", "width:1%;"),
        new XAttribute("scope", "col")),
        //--------------------------------
        new XElement("th", "Item",
        new XAttribute("style", "width:10%;"),
        new XAttribute("onClick", string.Format("fc_Buscar('{0}','nu_item','{1}')", page, orderbydirection == "D" ? "A" : "D")),
        new XAttribute("scope", "col")),
        //--------------------------------
        new XElement("th", "Marca",
        new XAttribute("style", "width:45%;"),
        new XAttribute("onClick", string.Format("fc_Buscar('{0}','no_marca','{1}')", page, orderbydirection == "D" ? "A" : "D")),
        new XAttribute("scope", "col")),
        //--------------------------------
        new XElement("th", "Modelo",
        new XAttribute("style", "width:44%;"),
        new XAttribute("onClick", string.Format("fc_Buscar('{0}','no_modelo','{1}')", page, orderbydirection == "D" ? "A" : "D")),
        new XAttribute("scope", "col")))), new XElement("tbody", new XAttribute("id", "tbgvServicios"),
        //--------------------------------
        //Fin Definir Cabecera
        oListServicioBE.Select(i =>
                   new XElement("tr",
                   new XAttribute("class", "textogrilla"),
                   new XAttribute("style", " cursor: hand;"),
                   new XAttribute("id", string.Format("{0}", i.nid_servicio)),
                   new XAttribute("bgcolor", i.nid_tabla % 2 == 0 ? "#E3E7F2" : "#FFFFFF"),
                   new XAttribute("onClick", string.Format("selTR(this,'{0}');", "gvServicios")),
                   new XAttribute("ondblClick", string.Format("selTRPP('{0}','{1}');", i.nid_marca, i.nid_modelo)),
                   //Inicio Definir Filas
                   new XElement("td", new XAttribute("align", "center"), new XElement("input", new XAttribute("type", "checkbox"), new XAttribute("id", "myCheck"), new XAttribute("value", i.nid_modelo)),
                   new XElement("td", new XAttribute("align", "center"), i.nu_item),
                   new XElement("td", new XAttribute("align", "left"), i.no_marca == null ? string.Empty : i.no_marca),
                   new XElement("td", new XAttribute("align", "left"), i.no_modelo == null ? string.Empty : i.no_modelo)))),
                         oListServicioBE.Count == 0 ? new XElement("tr",
                   new XAttribute("class", "textogrilla"),
                   new XElement("td",
                   new XAttribute("colspan", "5"),
                   new XAttribute("align", "center"), "No existen resultados para esta consulta.")) : null,
                   new XElement("tr", new XAttribute("class", "Footer"),
                   new XElement("td", new XAttribute("colspan", "4"))),
                   cantidadPaginas > 1 ? new XElement("tr", new XAttribute("class", "Paginacion"),
                   new XElement("td", new XAttribute("colspan", "4"),
                   new XAttribute("style", "color:#555a6d;padding-top:4px"),
                   new XElement("span", string.Format("Total Reg. {0}", totalOK), new XAttribute("align", "left"), new XAttribute("style", "padding-right:350px")),
                   new XElement("span",
                       new XElement("a", new XAttribute("style", "color:#555a6d;"),
                       new XAttribute("href", string.Format("javascript:fc_Buscar('{0}');", 1)),
                       new XElement("img", new XAttribute("src", "../Images/iconos/izquierdas.gif")))),
                       new XElement("span", new XElement("a", new XAttribute("style", "color:#555a6d;"),
                       new XAttribute("href", string.Format("javascript:fc_Buscar('{0}');", page > 1 ? page - 1 : 1)),
                       new XElement("img", new XAttribute("src", "../Images/iconos/izquierda.gif")))),
                       new XElement("span", "Pagina", new XAttribute("style", "padding-left:5px;padding-right:5px")),
                       new XElement("span", new XElement("input", new XAttribute("type", "text"), new XAttribute("value", page), new XAttribute("style", "width:30px;height:10px;color: #555a6d;font-size: 11px;text-align: center;"))),
                       new XElement("span", string.Format("de {0}", cantidadPaginas), new XAttribute("style", "padding-left:5px;padding-right:5px")),
                       new XElement("span",
                       new XElement("a", new XAttribute("style", "color:#555a6d;"),
                       new XAttribute("href", string.Format("javascript:fc_Buscar('{0}');", page < cantidadPaginas ? page + 1 : cantidadPaginas)),
                       new XElement("img", new XAttribute("src", "../Images/iconos/derecha.gif")))),
                       new XElement("span", new XElement("a", new XAttribute("style", "color:#555a6d;"),
                       new XAttribute("href", string.Format("javascript:fc_Buscar('{0}');", cantidadPaginas)),
                       new XElement("img", new XAttribute("src", "../Images/iconos/derechas.gif"))))
                   )) : null));

        return ServicioBandejaHtml;
    }



    #endregion WebMethod
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System.Xml.Linq;
using System.Linq;
using System.Web.Services;
using System.Collections.Generic;

public partial class SRC_Mantenimiento_SRC_Maestro_Detalle_TServicio : System.Web.UI.Page
{
    #region " Variables "
    TipoServicioBL objTServBL = new TipoServicioBL();
    TipoServicioBE objTServBE = new TipoServicioBE();
    private String op = "", co_tipo = "", no_tipo = "", fl_activo = "", id = "";
    private String fl_visible = "", fl_valida_km = "";
    private static readonly TipoServicioMarcaBL oMaestroTipoServicioMarcaBl = new TipoServicioMarcaBL();
    private static readonly ComboBL oComboBl = new ComboBL();
    private static readonly TipoServicioBL oMaestroTipoServicioBl = new TipoServicioBL();

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["op"] != null) op = Session["op"].ToString().Trim();
        if (!Page.IsPostBack)
        {
            CargaInicial();
        }
    }

    void CargarEntidad()
    {
        TipoServicioBEList objBEList = new TipoServicioBEList();
        objBEList = (TipoServicioBEList)(Session["TipoServicioBEList"]);

        for (int i = 0; i < objBEList.Count; i++)
        {
            if (objBEList[i].Id_TipoServicio.ToString().Trim().Equals(Session["txh_id_TipoServicio"].ToString().Trim()))
            {
                id = objBEList[i].Id_TipoServicio.ToString().Trim();
                co_tipo = objBEList[i].Co_tipo_servicio.ToString().Trim();
                no_tipo = objBEList[i].No_tipo_servicio.ToString().Trim();
                fl_activo = objBEList[i].Fl_activo.ToString().Trim();
                fl_visible = objBEList[i].fl_visible;
                fl_valida_km = objBEList[i].fl_validar_km;
                txth_nid_tipo_servicio.Value = id;
            }
        }
    }
    void CargaInicial()
    {
        btnEditar.Visible = false;

        if (Request.QueryString["id_TipoServicio"] != null)
        {

            Session["txh_id_TipoServicio"] = Request.QueryString["id_TipoServicio"];
            btnEditar.Visible = true;
            btnGrabar.Visible = false;
        }

        txtCodigo.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");
        txtNom.Attributes.Add("onkeypress", "return SoloLetrasNumeros(event)");

        cboEstado.Items.Insert(0, new ListItem());
        cboEstado.Items[0].Text = "Activo";
        cboEstado.Items[0].Value = "A";
        cboEstado.Items.Insert(1, new ListItem());
        cboEstado.Items[1].Text = "Inactivo";
        cboEstado.Items[1].Value = "I";
        if (op == "1")
        {
            txtCodigo.Enabled = false;
            CargarEntidad();
            p_items.Enabled = true;
            txtCodigo.Text = co_tipo;
            txtNom.Text = no_tipo;
            if (fl_activo == "Activo")
                cboEstado.SelectedIndex = 0;
            else
                cboEstado.SelectedIndex = 1;
            //I @001
            if (fl_visible == "0")
                chkObservacion.Checked = false;
            else
                chkObservacion.Checked = true;

            if (fl_valida_km == "0")
                chkKm.Checked = false;
            else
                chkKm.Checked = true;
            //F @001
        }
        else if (op == "2")
        {
            CargarEntidad();
            p_items.Enabled = false;
            btnGrabar.Visible = false;
            btnEditar.Visible = true;
            txtCodigo.Text = co_tipo;
            txtNom.Text = no_tipo;
            if (fl_activo == "Activo")
                cboEstado.SelectedIndex = 0;
            else
                cboEstado.SelectedIndex = 1;
            //I @001
            if (fl_visible == "0")
                chkObservacion.Checked = false;
            else
                chkObservacion.Checked = true;

            if (fl_valida_km == "0")
                chkKm.Checked = false;
            else
                chkKm.Checked = true;
            //F @001
        }
        else
        {
            p_items.Enabled = true;
            txtCodigo.Text = "";
            txtNom.Text = "";
        }

    }
    void Limpiar()
    {
        txtCodigo.Text = "";
        txtNom.Text = "";
        cboEstado.SelectedIndex = 0;
    }
    protected void _mesg(string sms)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "<script>alert('" + sms + "')</script>", false);
    }

    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        if (op == "0")
        {
            objTServBE.Co_tipo_servicio = txtCodigo.Text.Trim();
            objTServBE.No_tipo_servicio = txtNom.Text.Trim();
            objTServBE.Co_usuario_crea = Profile.UserName;
            objTServBE.Co_usuario_red = Profile.UsuarioRed;
            objTServBE.No_estacion_red = Profile.Estacion;
            objTServBE.Fl_activo = cboEstado.SelectedValue.ToString();
            
            if (chkObservacion.Checked == true)
                objTServBE.fl_visible = "1";
            else
                objTServBE.fl_visible = "0";

            if (chkKm.Checked == true)
                objTServBE.fl_validar_km = "1";
            else
                objTServBE.fl_validar_km = "0";

            Int32 res;
            res = objTServBL.InsertTServicio(objTServBE);
            if (res == 0)
            {
                Parametros.SRC_Mensaje_Redireccionar(this, "El registro se guardo con exito.", "SRC_Maestro_TServicio.aspx");
            }
            else
            {
                if (res == 9)
                {
                    _mesg("Codigo ya existe.");
                }
            }
        }
        else if (op == "1" || op == "2")
        {
            CargarEntidad();
            objTServBE.Id_TipoServicio = Int32.Parse(id);
            objTServBE.Co_tipo_servicio = txtCodigo.Text.Trim();
            objTServBE.No_tipo_servicio = txtNom.Text.Trim();
            objTServBE.Co_usuario_modi = Profile.UserName;
            objTServBE.Co_usuario_red = Profile.UsuarioRed;
            objTServBE.No_estacion_red = Profile.Estacion;
            objTServBE.Fl_activo = cboEstado.SelectedValue.ToString();
            
            if (chkObservacion.Checked)
                objTServBE.fl_visible = "1";
            else
                objTServBE.fl_visible = "0";

            if (chkKm.Checked == true)
                objTServBE.fl_validar_km = "1";
            else
                objTServBE.fl_validar_km = "0";

            Int32 res;
            res = objTServBL.ActualizarTServicio(objTServBE);
            if (res == 0)
            {
                Parametros.SRC_Mensaje_Redireccionar(this, "El registro se actualizó con exito.", "SRC_Maestro_TServicio.aspx");
            }
            else
            {
                if (res == 9)
                {
                    _mesg("Codigo ya existe.");
                }
            }
        }
    }
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Limpiar();
        Response.Redirect("SRC_Maestro_TServicio.aspx");
    }
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        btnEditar.Visible = false;
        btnGrabar.Visible = true;
        txtNom.Enabled = true;
        txtCodigo.Enabled = false;
        cboEstado.Enabled = true;
        p_items.Enabled = true;
        btnGrabar.Enabled = true;

    }
    
    #region Texto Informativo
    [WebMethod]
    public static System.Collections.ArrayList ObtenerMarcas(string snidUsuario)
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
    public static System.Collections.ArrayList ObtenerTipoServicios(string snidUsuario)
    {

        int nidUsuario = 0;
        int.TryParse(snidUsuario, out nidUsuario);
        System.Collections.ArrayList arrayTipoServicio = new System.Collections.ArrayList();
        var oTipoServicioList = oMaestroTipoServicioBl.BusqTServicioList(new TipoServicioBE { Co_tipo_servicio = string.Empty, No_tipo_servicio = string.Empty, Fl_activo = string.Empty });
        var objTipoServicio = oTipoServicioList.Select(item => new
        {
            nombre = item.No_tipo_servicio,
            codigo = item.Id_TipoServicio,
        });
        arrayTipoServicio.AddRange(objTipoServicio.ToArray());
        return arrayTipoServicio;
    }

    [WebMethod]
    public static System.Collections.ArrayList ObtenerBandeja(string[] parametros)
    {
        int nidTipoServicio = 0;
        int.TryParse(parametros[0], out nidTipoServicio);
        var orderby = parametros[1];
        var orderbydirection = parametros[2];
        var resultado = 0;
        System.Collections.ArrayList arrayBandeja = new System.Collections.ArrayList();

        try
        {
            var oBandejaList = oMaestroTipoServicioMarcaBl.GetAllMaestroTipoServicioMarca(nidTipoServicio, orderby, orderbydirection).ToList();


            var BandejaHtml = Convert.ToString(TipoServicioBandejaHtml(oBandejaList));
            var HeaderHtml = Convert.ToString(TipoServicioBandejaHeaderHtml(orderby, orderbydirection));
            resultado = 1;
            arrayBandeja.Add(resultado);
            arrayBandeja.Add(HeaderHtml);
            arrayBandeja.Add(BandejaHtml);

        }
        catch //(Exception ex)
        {
            resultado = -1;
            arrayBandeja.Add(resultado);
        }


        return arrayBandeja;
    }

    [WebMethod]
    public static System.Collections.ArrayList AgregarTextoInformativo(string[] parametros)
    {
        System.Collections.ArrayList arrayTextoInformativo = new System.Collections.ArrayList();
        var resultado = 1;
        int nid_tipo_servicio = 0;
        int.TryParse(parametros[0], out nid_tipo_servicio);
        int nid_marca = 0;
        int.TryParse(parametros[1], out nid_marca);
        int nid_tipo_servicio_marca = 0;
        int.TryParse(parametros[7], out nid_tipo_servicio_marca);
        var fl_visible = parametros[2];
        var texto_informativo = parametros[3];
        var oTipoServicioMarcaBe =
            new TipoServicioMarcaBE
            {
                nid_tipo_servicio_marca = nid_tipo_servicio_marca,
                nid_tipo_servicio = nid_tipo_servicio,
                nid_marca = nid_marca,
                fl_visible = fl_visible == "True" ? "1" : "0",
                tx_informativo = texto_informativo,
                co_usuario_crea = parametros[4],
                co_usuario_red = parametros[5],
                no_estacion_red = parametros[6]
            };
        try
        {
            var oMaestroTipoServicioMarcaBe = oMaestroTipoServicioMarcaBl.GetOneMaestroTipoServicioByMarca(nid_tipo_servicio, nid_marca);

            if (nid_tipo_servicio_marca == 0)
            {
                if (oMaestroTipoServicioMarcaBe == null)
                    oMaestroTipoServicioMarcaBl.AddMaestroTipoServicioMarca(oTipoServicioMarcaBe);
                else
                    resultado = 0;
            }
            else
            {
                oMaestroTipoServicioMarcaBl.UpdateMaestroTipoServicioMarca(oTipoServicioMarcaBe);
               
            }
        }
        catch //(Exception ex)
        {

            resultado = -1;
        }
        arrayTextoInformativo.Add(resultado);
        return arrayTextoInformativo;
    }

    [WebMethod]
    public static System.Collections.ArrayList EliminarTipoServicioMarca(string[] seleccion, string[] parametros)
    {


        System.Collections.ArrayList arrayBandeja = new System.Collections.ArrayList();
        var resultado = 0;
        try
        {
            if (seleccion.Length > 0)
            {

                var seleccionXML = new XElement("ROOT", seleccion.Select(i => new XElement("doc",
                               new XAttribute("nid_tipo_servicio_marca", i)))).ToString();
                oMaestroTipoServicioMarcaBl.UpdateMaestroTipoServicioByMarca(new TipoServicioMarcaBE { TipoServicioXml = seleccionXML, co_usuario_modi = parametros[0], co_usuario_red = parametros[1], no_estacion_red = parametros[2] });
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

    private static string ObtenerGrillaBandeja()
    {
        var AsignacionBandejaHtml = new XElement("table",
        new XAttribute("style", "border-color:White;border-width:0px;width:950px;border-collapse:collapse;"),
        new XAttribute("border", "0"),
        new XAttribute("rules", "all"),
        new XAttribute("cellspacing", "0"),
        new XAttribute("cellpadding", "0"),
        new XAttribute("id", "gvServicios}"),
        new XElement("thead",
        new XElement("tr",
        new XAttribute("class", "CabeceraGrilla"),
            //Inicio Definir Cabecera
            //--------
        new XElement("th", new XAttribute("align", "center"), new XElement("input", new XAttribute("type", "checkbox"), new XAttribute("disabled", "disabled"), new XAttribute("id", "ckHeader")),
            //--------
        new XAttribute("style", "width:1%;"),
        new XAttribute("scope", "col")),
            //--------------------------------
        new XElement("th", "Item",
        new XAttribute("style", "width:12%;"),
        new XAttribute("scope", "col")),
            //--------------------------------
        new XElement("th", "Marca",
        new XAttribute("style", "width:43%;"),
        new XAttribute("scope", "col")),
            //--------------------------------
        new XElement("th", "Modelo",
        new XAttribute("style", "width:44%;"),
        new XAttribute("scope", "col")))), new XElement("tbody",
            //--------------------------------

       //Fin Definir Cabecera
        new XElement("tr", new XElement("td", new XAttribute("align", "center"), new XAttribute("colspan", "4"), " No existen resultados para esta consulta.")),
         new XElement("tr", new XAttribute("class", "Footer"),
                       new XElement("td", new XAttribute("colspan", "4")))));

        return AsignacionBandejaHtml.ToString();
    }

    private static XElement TipoServicioBandejaHtml(List<TipoServicioMarcaBE> oListServicioBE)
    {
        var ServicioBandejaHtml = new XElement("table",
        new XAttribute("style", "border-color:White;border-width:0px;width:850px;border-collapse:collapse;"),
        new XAttribute("border", "0"),
        new XAttribute("rules", "all"),
        new XAttribute("cellspacing", "0"),
        new XAttribute("cellpadding", "0"),
        new XAttribute("id", "gvServicios"),
            //new XElement("thead",
            //new XElement("tr",
            //new XAttribute("class", "CabeceraGrilla"),
            //    //Inicio Definir Cabecera
            //    //--------
            //new XElement("th", new XAttribute("align", "center"), new XElement("input", new XAttribute("type", "checkbox"), new XAttribute("onclick", "CheckAllB(this)"), new XAttribute("id", "ckHeader")),
            //    //--------
            //new XAttribute("style", "width:5%;"),
            //new XAttribute("scope", "col")),
            //    //--------------------------------
            //new XElement("th", "Item",
            //new XAttribute("style", "width:10%;"),
            //new XAttribute("onClick", string.Format("fc_Buscar('nu_item','{0}')", orderbydirection == "D" ? "A" : "D")),
            //new XAttribute("scope", "col")),
            //    //--------------------------------
            //new XElement("th", "Marca",
            //new XAttribute("style", "width:55%;"),
            //new XAttribute("onClick", string.Format("fc_Buscar('no_marca','{0}')", orderbydirection == "D" ? "A" : "D")),
            //new XAttribute("scope", "col")),
            //    //--------------------------------
            // new XElement("th", "Visible?",
            //new XAttribute("style", "width:15%;"),
            //new XAttribute("onClick", string.Format("fc_Buscar('fl_visible','{0}')", orderbydirection == "D" ? "A" : "D")),
            //new XAttribute("scope", "col")),
            //    //--------------------------------
            //new XElement("th", "Ver Detalle",
            //new XAttribute("style", "width:15%;"),
            //new XAttribute("scope", "col")))), 

        new XElement("tbody",
            //--------------------------------
            //Fin Definir Cabecera

        oListServicioBE.Select(i =>
          new XElement("tr",
                   new XAttribute("class", "textogrilla"),
                   new XAttribute("style", " cursor: hand;"),
                   new XAttribute("id", string.Format("{0}", i.nid_tipo_servicio_marca)),
                   new XAttribute("bgcolor", i.nid_tabla % 2 == 0 ? "#E3E7F2" : "#FFFFFF"),
                   new XAttribute("onClick", string.Format("selTR(this,'{0}');", "gvServicios")),
                   new XAttribute("ondblClick", string.Format("selTRPP('{0}');", i.nid_tipo_servicio_marca)),
              //Inicio Definir Filas
                   new XElement("td", new XAttribute("align", "center"), new XAttribute("style", "width:5%;"), new XElement("input", new XAttribute("type", "checkbox"), new XAttribute("id", "myCheck"), new XAttribute("value", i.nid_tipo_servicio_marca)),
                  new XElement("td", new XAttribute("align", "center"), new XAttribute("style", "width:10%;"), i.nu_item),
                  new XElement("td", new XAttribute("align", "left"), new XAttribute("style", "width:55%;"), i.no_marca == null ? string.Empty : i.no_marca),
                  new XElement("td", new XAttribute("align", "center"), new XAttribute("style", "width:15%;"), i.fl_visible == "1" ? "Si" : "No"),
                   new XElement("td", new XAttribute("align", "center"), new XAttribute("style", "width:15%;"), new XElement("a", new XAttribute("style", "color:#555a6d;"),
                       new XAttribute("href", string.Format("javascript:fc_ver_detalle('{0}');", i.nid_tipo_servicio_marca)),
                       new XElement("img", new XAttribute("src", "../Images/iconos/b-buscar.gif"), new XAttribute("width", "23px"))))))),
                    oListServicioBE.Count == 0 ? new XElement("tr",
                   new XAttribute("class", "textogrilla"),
                   new XElement("td",
                   new XAttribute("colspan", "5"),
                   new XAttribute("align", "center"), "No existen resultados para esta consulta.")) : null)
            //,
            //new XElement("tfoot",
            //new XElement("tr", new XAttribute("class", "Footer"),
            //new XElement("td", new XAttribute("colspan", "5"))))
                 );

        return ServicioBandejaHtml;
    }

    private static XElement TipoServicioBandejaHeaderHtml(string orderby, string orderbydirection)
    {
        var ServicioBandejaHeaderHtml = new XElement("table",
        new XAttribute("style", "border-color:White;border-width:0px;width:850px;border-collapse:collapse;"),
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
            new XAttribute("style", "width:5%;"),
            new XAttribute("scope", "col")),
            //--------------------------------
            new XElement("th", "Item",
            new XAttribute("style", "width:10%;"),
            new XAttribute("onClick", string.Format("fc_Buscar('nu_item','{0}')", orderbydirection == "D" ? "A" : "D")),
            new XAttribute("scope", "col")),
            //--------------------------------
            new XElement("th", "Marca",
            new XAttribute("style", "width:55%;"),
            new XAttribute("onClick", string.Format("fc_Buscar('no_marca','{0}')", orderbydirection == "D" ? "A" : "D")),
            new XAttribute("scope", "col")),
            //--------------------------------
             new XElement("th", "Visible?",
            new XAttribute("style", "width:15%;"),
            new XAttribute("onClick", string.Format("fc_Buscar('fl_visible','{0}')", orderbydirection == "D" ? "A" : "D")),
            new XAttribute("scope", "col")),
            //--------------------------------
            new XElement("th", "Ver Detalle",
            new XAttribute("style", "width:15%;"),
            new XAttribute("scope", "col")))));

        return ServicioBandejaHeaderHtml;
    }

    [WebMethod]
    public static System.Collections.ArrayList ObtenerTextoInformativo(string[] parametros)
    {
        System.Collections.ArrayList arrayTextoInformativo = new System.Collections.ArrayList();
        int nid_tipo_servicio_marca = 0;
        int.TryParse(parametros[0], out nid_tipo_servicio_marca);
        var resultado = 0;
        TipoServicioMarcaBE oMaestroTipoServicioMarcaBe = null;
        try
        {
            oMaestroTipoServicioMarcaBe = oMaestroTipoServicioMarcaBl.GetOneMaestroTipoServicioMarca(nid_tipo_servicio_marca);
            if (oMaestroTipoServicioMarcaBe != null)
            {
                resultado = 1;
                arrayTextoInformativo.Add(resultado);
                arrayTextoInformativo.Add(oMaestroTipoServicioMarcaBe.nid_tipo_servicio);
                arrayTextoInformativo.Add(oMaestroTipoServicioMarcaBe.nid_marca);
                arrayTextoInformativo.Add(oMaestroTipoServicioMarcaBe.fl_visible);
                arrayTextoInformativo.Add(oMaestroTipoServicioMarcaBe.tx_informativo);
            }
            else
            {
                arrayTextoInformativo.Add(resultado);
            }
        }
        catch (Exception)
        {

            resultado = -1;
            arrayTextoInformativo.Add(resultado);
        }

        return arrayTextoInformativo;
    }

    [WebMethod]
    public static System.Collections.ArrayList EliminarServiciosModelo(string[] parametros)
    {


        System.Collections.ArrayList arrayBandeja = new System.Collections.ArrayList();
        //var resultado = 0;
        //try
        //{
        //    if (parametros.Length > 0)
        //    {

        //        var seleccionXML = new XElement("ROOT", parametros.Select(i => new XElement("doc",
        //                       new XAttribute("nid_modelo", i)))).ToString();
        //        oServicioBl.UpdateServicioModelo(new ServicioBE { XmlServicios = seleccionXML, co_usuario_cambio = "", no_usuario_red = "", no_estacion_red = "" });
        //        resultado = 1;
        //        arrayBandeja.Add(resultado);
        //    }
        //    else
        //    {
        //        resultado = 0;
        //        arrayBandeja.Add(resultado);
        //    }
        //}
        //catch (Exception)
        //{
        //    resultado = -1;
        //    arrayBandeja.Add(resultado);
        //}

        return arrayBandeja;

    }

    #endregion Texto Informativo
}

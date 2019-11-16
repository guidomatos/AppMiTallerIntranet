using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;

public partial class SRC_Reportes_Citas_Atendidas_Por_Asesor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string AccesoPagina = (Master as Principal).ValidaAccesoOpcion(ConstanteBE.SRC_ReservaCita_AccionVerFormulario);
        //if (string.IsNullOrEmpty(AccesoPagina))
        //    AccesoPagina = (Master as Principal).ValidaTipoAccesoPagina(Page, "SinAcceso");

        if (!Page.IsPostBack)
        {


        }
    }

    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [WebMethod]
    public static object GetDataReporte(Int32 nid_usuario)
    {
        ReporteBL objBL = new ReporteBL();
        List<object> listaDataReporte = new List<object>();

        CitasBEList objResponseBL = objBL.ListarCitasAtendidasPorAsesor(nid_usuario);
        foreach (CitasBE obj in objResponseBL)
        {
            listaDataReporte.Add(new
            {
                no_asesor = obj.no_asesor,
                qt_cita = obj.qt_citas_a
            });
        }

        object response = new
        {
            listaDataReporte = listaDataReporte
        };

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(response);
    }

}
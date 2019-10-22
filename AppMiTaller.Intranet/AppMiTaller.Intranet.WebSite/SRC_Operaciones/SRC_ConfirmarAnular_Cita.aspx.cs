using System;
using System.Web.UI;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SRC_Operaciones_SRC_ConfirmarAnular_Cita : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            try
            {

                Int32 nid_cita = Convert.ToInt32(Request.QueryString["cita"].ToString());
                String opcion = Request.QueryString["op"].ToString();

                CitasBE ent = new CitasBE();
                CitasBL neg = new CitasBL();

                Response.Write("opcion seleccionada : " + opcion + " cita seleccionada: " + nid_cita.ToString());

                ent.nid_cita = nid_cita;
                ent.co_estado_cita = opcion;
                ent.co_usuario_crea = "";
                ent.co_usuario_red = "";
                ent.no_estacion_red = "";
                ent.fl_activo = "A";

                neg.ConfirmarCitaPorCorreo(ent);

                Response.Write("Cita Actualizada");


            }
            catch
            {

            }
        }
    }
}
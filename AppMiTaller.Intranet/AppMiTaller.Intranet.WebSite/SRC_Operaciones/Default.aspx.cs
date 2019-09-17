using System;

public partial class SRC_Operaciones_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ComunCode.SetFormatImageButton(btnBuscar, "excel", "fc_OpenCargaExcel()");
        ComunCode.SetFormatImageButton(btnClose, "cerrar", "fc_OpenCargaExcel()");

    }
    protected void btnOpen_Click(object sender, EventArgs e)
    {

    }
}

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;

public partial class SGS_UserControl_ComboEstado : System.Web.UI.UserControl
{
    public override String SkinID
    {
        get { return this.cboEstado.SkinID; }
        set { this.cboEstado.SkinID = value; }
    }

    public String SelectedValue
    {
        get { return this.cboEstado.SelectedValue; }
        set { this.cboEstado.SelectedValue = value; }
    }

    public String SelectedText
    {
        get { return this.cboEstado.SelectedItem.Text; }
    }

    public Boolean Enabled
    {
        set { if (this.cboEstado != null) this.cboEstado.Enabled = value; }
    }

    private String condicion;

    public String Condicion
    {
        get { return condicion; }
        set { condicion = value; }
    }

    public Unit Width
    {
        set { ViewState["_Width"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (ViewState["_Width"] != null) this.cboEstado.Width = (Unit)ViewState["_Width"];
            else this.cboEstado.Width = Unit.Pixel(100);
        }
    }

    public void cargarCombo(String condicion)
    {
        String objeto;
        if (!condicion.Equals(String.Empty))
        {
            if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_TODOS))
            {
                objeto = ConstanteBE.OBJECTO_TODOS;
            }
            else if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE))
            {
                objeto = ConstanteBE.OBJECTO_SELECCIONE;
            }
            else objeto = ConstanteBE.OBJECTO_TODOS;

            this.cboEstado.Items.Insert(0, new ListItem(objeto, String.Empty));
        }
        this.cboEstado.SelectedValue = String.Empty;
    }

}
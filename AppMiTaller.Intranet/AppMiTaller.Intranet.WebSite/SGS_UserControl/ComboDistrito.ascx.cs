using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BL;
using AppMiTaller.Intranet.BE;

public partial class SGS_UserControl_ComboDistrito : System.Web.UI.UserControl
{
    public String CssClass
    {
        get { return this.cboDistrito.CssClass; }
        set { this.cboDistrito.CssClass = value; }
    }

    public Unit Width
    {
        set { ViewState["_Width"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (ViewState["_Width"] != null) this.cboDistrito.Width = (Unit)ViewState["_Width"];

            if (condicion != null && !condicion.Equals(String.Empty))
            {
                String objeto = ConstanteBE.OBJECTO_TODOS;
                if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE)) { objeto = ConstanteBE.OBJECTO_SELECCIONE; }
                if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_DIST)) { objeto = ConstanteBE.OBJETO_DISTRITO; }
                this.cboDistrito.Items.Insert(0, new ListItem(objeto, String.Empty));
            }
        }
    }
    private String condicion;
    public String Condicion
    {
        get { return condicion; }
        set { condicion = value; }
    }

    private String _id_provincia;
    public String id_provincia
    {
        get { return _id_provincia; }
        set { _id_provincia = value; }
    }

    private String _id_departamento;
    public String id_departamento
    {
        get { return _id_departamento; }
        set { _id_departamento = value; }
    }

    public String SelectedValue
    {
        get { return this.cboDistrito.SelectedValue; }
        set { this.cboDistrito.SelectedValue = value; }
    }

    public Boolean Enabled
    {
        set
        {
            if (this.cboDistrito != null)
            {
                this.cboDistrito.Enabled = value;
            }
        }
    }

    public String SelectedText
    {
        get { return this.cboDistrito.SelectedItem.Text; }
    }

    public ListItemCollection Items
    {
        get { return this.cboDistrito.Items; }
    }

    public void cargarCombo(String opcion)
    {
        cboDistrito.Items.Clear();
        UbigeoBL oUbigeoBL = new UbigeoBL();

        this.cboDistrito.SelectedValue = null;

        cboDistrito.DataSource = oUbigeoBL.GetListaDistrito(_id_departamento, _id_provincia);
        cboDistrito.DataValueField = "coddist";
        cboDistrito.DataTextField = "nombre";
        cboDistrito.DataBind();

        String objeto = ConstanteBE.OBJECTO_TODOS;
        if (!opcion.Equals(String.Empty))
        {
            if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE)) { objeto = ConstanteBE.OBJECTO_SELECCIONE; }
            if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_DIST)) { objeto = ConstanteBE.OBJETO_DISTRITO; }
        }
        this.cboDistrito.Items.Insert(0, new ListItem(objeto, String.Empty));
    }
    public String FindItem(String valor)
    {
        String retorno = String.Empty;
        if (cboDistrito.Items.FindByValue(valor) != null)
            retorno = valor;

        return retorno;
    }

    public void CargarComboOpcion(String condicion)
    {
        try
        {
            this.cboDistrito.Items.Clear();
            String objeto = String.Empty;
            if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_TODOS))
            {
                objeto = ConstanteBE.OBJECTO_TODOS;
            }
            else if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE))
            {
                objeto = ConstanteBE.OBJECTO_SELECCIONE;
            }
            else objeto = ConstanteBE.OBJECTO_TODOS;

            this.cboDistrito.Items.Insert(0, new ListItem(objeto, String.Empty));
        }
        catch { }
    }
}
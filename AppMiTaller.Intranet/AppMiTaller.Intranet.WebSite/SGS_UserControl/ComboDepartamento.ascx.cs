using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BL;
using AppMiTaller.Intranet.BE;

public partial class SGS_UserControl_ComboDepartamento : System.Web.UI.UserControl
{
    //Delegados 
    public delegate void SelectedIndexChangedDelegate(object sender, EventArgs e);
    public event SelectedIndexChangedDelegate SelectedIndexChanged;
    public String CssClass
    {
        get { return this.cboDepartamento.CssClass; }
        set { this.cboDepartamento.CssClass = value; }
    }
    public Unit Width
    {
        set { ViewState["_Width"] = value; }

    }
    //Propiedades
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (ViewState["_Width"] != null) this.cboDepartamento.Width = (Unit)ViewState["_Width"];
            //cargarCombo();
        }
    }

    private String condicion;
    public String Condicion
    {
        get { return condicion; }
        set { condicion = value; }
    }

    public String SelectedValue
    {
        get { return this.cboDepartamento.SelectedValue; }
        set { this.cboDepartamento.SelectedValue = value; }
    }

    public String SelectedText
    {
        get { return this.cboDepartamento.SelectedItem.Text; }
    }

    public ListItemCollection Items
    {
        get { return this.cboDepartamento.Items; }
    }
    public Boolean Enabled
    {
        set
        {
            if (this.cboDepartamento != null)
            {
                this.cboDepartamento.Enabled = value;
            }
        }
    }

    public bool AutoPostBack
    {
        set { this.cboDepartamento.AutoPostBack = value; }
        get { return this.cboDepartamento.AutoPostBack; }
    }

    public void cargarCombo(String opcion)
    {
        UbigeoBL oUbigeoBL = new UbigeoBL();

        cboDepartamento.DataSource = oUbigeoBL.GetListaDepartamento();
        cboDepartamento.DataValueField = "coddpto";
        cboDepartamento.DataTextField = "nombre";
        cboDepartamento.DataBind();

        String objeto = ConstanteBE.OBJECTO_TODOS;
        if (!opcion.Equals(String.Empty))
        {
            if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE)) { objeto = ConstanteBE.OBJECTO_SELECCIONE; }
            if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_DEPA)) { objeto = ConstanteBE.OBJETO_DEPARTAMENTO; }
        }

        this.cboDepartamento.Items.Insert(0, new ListItem(objeto, String.Empty));
        this.cboDepartamento.SelectedValue = String.Empty;
    }
    public String FindItem(String valor)
    {
        String retorno = String.Empty;
        if (cboDepartamento.Items.FindByValue(valor) != null)
            retorno = valor;

        return retorno;
    }

    protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedIndexChanged(sender, e);
    }
    public void CargarComboOpcion(String condicion)
    {
        try
        {
            this.cboDepartamento.Items.Clear();
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

            this.cboDepartamento.Items.Insert(0, new ListItem(objeto, String.Empty));
        }
        catch { }
    }
}
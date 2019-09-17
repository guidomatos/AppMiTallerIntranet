using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BL;
using AppMiTaller.Intranet.BE;

public partial class SGS_UserControl_ComboProvincia : System.Web.UI.UserControl
{
    //Delegados 
    public delegate void SelectedIndexChangedDelegate(object sender, EventArgs e);
    public event SelectedIndexChangedDelegate SelectedIndexChanged;
    public String CssClass
    {
        get { return this.cboProvincia.CssClass; }
        set { this.cboProvincia.CssClass = value; }
    }
    public Unit Width
    {
        set { ViewState["_Width"] = value; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (ViewState["_Width"] != null) this.cboProvincia.Width = (Unit)ViewState["_Width"];
            if (condicion != null && !condicion.Equals(String.Empty))
            {
                String objeto = ConstanteBE.OBJECTO_TODOS;
                if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE)) { objeto = ConstanteBE.OBJECTO_SELECCIONE; }
                if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_PROV)) { objeto = ConstanteBE.OBJETO_PROVINCIA; }

                this.cboProvincia.Items.Insert(0, new ListItem(objeto, String.Empty));
            }
        }
    }
    private String condicion;
    public String Condicion
    {
        get { return condicion; }
        set { condicion = value; }
    }

    private String _id_Departamento;
    public String id_Departamento
    {
        get { return _id_Departamento; }
        set { _id_Departamento = value; }
    }

    public String SelectedValue
    {
        get { return this.cboProvincia.SelectedValue; }
        set { this.cboProvincia.SelectedValue = value; }
    }

    public String SelectedText
    {
        get { return this.cboProvincia.SelectedItem.Text; }
    }

    public ListItemCollection Items
    {
        get { return this.cboProvincia.Items; }
    }

    public Boolean Enabled
    {
        set
        {
            if (this.cboProvincia != null)
            {
                this.cboProvincia.Enabled = value;
            }
        }
    }

    public bool AutoPostBack
    {
        set { this.cboProvincia.AutoPostBack = value; }
        get { return this.cboProvincia.AutoPostBack; }
    }

    public void cargarCombo(String opcion)
    {
        this.cboProvincia.Items.Clear();
        UbigeoBL oUbigeoBL = new UbigeoBL();

        this.cboProvincia.SelectedValue = null;
        this.cboProvincia.DataSource = oUbigeoBL.GetListaProvincia(_id_Departamento);
        this.cboProvincia.DataValueField = "codprov";
        this.cboProvincia.DataTextField = "nombre";
        this.cboProvincia.DataBind();

        String objeto = ConstanteBE.OBJECTO_TODOS;
        if (!opcion.Equals(String.Empty))
        {
            if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE)) { objeto = ConstanteBE.OBJECTO_SELECCIONE; }
            if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_PROV)) { objeto = ConstanteBE.OBJETO_PROVINCIA; }
        }
        this.cboProvincia.Items.Insert(0, new ListItem(objeto, String.Empty));
    }
    protected void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedIndexChanged(sender, e);
    }

    public String FindItem(String valor)
    {
        String retorno = String.Empty;
        if (cboProvincia.Items.FindByValue(valor) != null)
            retorno = valor;

        return retorno;
    }

    public void CargarComboOpcion(String condicion)
    {
        try
        {
            this.cboProvincia.Items.Clear();
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

            this.cboProvincia.Items.Insert(0, new ListItem(objeto, String.Empty));
        }
        catch { }
    }
}
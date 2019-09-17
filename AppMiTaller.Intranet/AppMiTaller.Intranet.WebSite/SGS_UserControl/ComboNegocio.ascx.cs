using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SGS_UserControl_ComboNegocio : System.Web.UI.UserControl
{

    //Delegados 
    public delegate void SelectedIndexChangedDelegate(object sender, EventArgs e);
    public event SelectedIndexChangedDelegate SelectedIndexChanged;

    public bool Enabled
    {
        get { return this.cboNegocio.Enabled; }
        set { this.cboNegocio.Enabled = value; }
    }

    public String CssClass
    {
        get { return this.cboNegocio.CssClass; }
        set { this.cboNegocio.CssClass = value; }
    }

    public String SelectedValue
    {
        get { return this.cboNegocio.SelectedValue; }
        set { this.cboNegocio.SelectedValue = value; }
    }

    public String SelectedText
    {
        get { return this.cboNegocio.SelectedItem.Text; }
    }

    private String condicion;
    public String Condicion
    {
        get { return condicion; }
        set { condicion = value; }
    }

    public bool AutoPostBack
    {
        set { this.cboNegocio.AutoPostBack = value; }
        get { return this.cboNegocio.AutoPostBack; }
    }

    public Boolean PorUsuario
    {
        set { ViewState["PorUsuario"] = value; }
        get { return ViewState["PorUsuario"] == null ? false : (Boolean)ViewState["PorUsuario"]; }
    }

    public Unit Width
    {
        set { ViewState["_Width"] = value; }

    }

    public ListItemCollection Items
    {
        get { return this.cboNegocio.Items; }
    }

    public Int32 SelectedIndex
    {
        set { this.cboNegocio.SelectedIndex = value; }
        get { return this.cboNegocio.SelectedIndex; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CargarCombo();            
            if (ViewState["_Width"] != null) this.cboNegocio.Width = (Unit)ViewState["_Width"];
        }
    }

    public void cargarCombo(String opcion)
    {
        NegocioBL oNegocioBL = new NegocioBL();
        cboNegocio.DataSource = oNegocioBL.ListarNegocio();
        cboNegocio.DataValueField = "cod_negocio";
        cboNegocio.DataTextField = "nom_negocio";
        cboNegocio.DataBind();

        if (!opcion.Equals(String.Empty))
        {
            if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_TODOS))
                this.cboNegocio.Items.Insert(0, new ListItem(ConstanteBE.OBJECTO_TODOS, String.Empty));
            else
                if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE))
                this.cboNegocio.Items.Insert(0, new ListItem(ConstanteBE.OBJECTO_SELECCIONE, String.Empty));
        }
        this.cboNegocio.SelectedValue = String.Empty;
    }

    protected void cboNegocio_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedIndexChanged(sender, e);
    }

    public String FindItem(String valor)
    {
        String retorno = String.Empty;
        if (this.cboNegocio.Items.FindByValue(valor) != null)
        {
            retorno = valor;
        }
        return retorno;
    }
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SGS_UserControl_ComboEmpresa : System.Web.UI.UserControl
{
    public delegate void ErrorDelegate(object sender, Exception ex);
    public event ErrorDelegate ErrorEvent;

    public delegate void SelectedIndexChangedDelegate(object sender, EventArgs e);
    public event SelectedIndexChangedDelegate SelectedIndexChangedEvent;

    public string fl_empresa_usuario { get; set; }

    public ListItemCollection Items
    {
        get { return this.cboEmpresa.Items; }
    }
    public Unit Width
    {
        set { ViewState["_Width"] = value; }
    }
    public String CssClass
    {
        get { return this.cboEmpresa.CssClass; }
        set { this.cboEmpresa.CssClass = value; }
    }
    public String SelectedValue
    {
        get { return this.cboEmpresa.SelectedValue; }
        set { this.cboEmpresa.SelectedValue = value; }
    }

    public String SelectedText
    {
        get { return this.cboEmpresa.SelectedItem.Text; }
    }

    public Boolean Enabled
    {
        set
        {
            if (this.cboEmpresa != null)
            {
                this.cboEmpresa.Enabled = value;
            }
            else
            {
                ViewState["_Enabled"] = value;
            }
        }
    }

    public Boolean AutoPostBack
    {
        set { ViewState["_AutoPostBack"] = value; }
    }

    public Int32 SelectedIndex
    {
        get { return this.cboEmpresa.SelectedIndex; }
        set { this.cboEmpresa.SelectedIndex = value; }
    }

    public Boolean SoloMisEmpresas
    {
        get { return ViewState["SoloMisEmpresas"] == null ? false : (Boolean)ViewState["SoloMisEmpresas"]; }
        set { ViewState["SoloMisEmpresas"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (ViewState["_Width"] != null) this.cboEmpresa.Width = (Unit)ViewState["_Width"];
            if (ViewState["_Enabled"] != null) this.cboEmpresa.Enabled = (Boolean)ViewState["_Enabled"];
        }

        if (ViewState["_AutoPostBack"] != null && (Boolean)ViewState["_AutoPostBack"])
        {
            this.cboEmpresa.SelectedIndexChanged += new EventHandler(cboEmpresa_SelectedIndexChanged);
            this.cboEmpresa.AutoPostBack = true;
        }
        else
        {
            this.cboEmpresa.AutoPostBack = false;
        }
    }

    public void CargarCombo(String opcion)
    {
        EmpresaBL oEmpresaBL = new EmpresaBL();

        EmpresaBEList lista = new EmpresaBEList();
        lista = oEmpresaBL.GetListaEmpresa();

        cboEmpresa.DataSource = lista;
        cboEmpresa.DataValueField = "nid_empresa";
        cboEmpresa.DataTextField = "no_empresa";
        cboEmpresa.DataBind();

        if (!opcion.Equals(String.Empty))
        {
            if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_TODOS))
                this.cboEmpresa.Items.Insert(0, new ListItem(ConstanteBE.OBJECTO_TODOS, String.Empty));
            else
                if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE))
                this.cboEmpresa.Items.Insert(0, new ListItem(ConstanteBE.OBJECTO_SELECCIONE, String.Empty));
        }
        this.cboEmpresa.SelectedValue = String.Empty;
    }
    public String ValidationGroup
    {
        get { return this.rfvCboEmpresa.ValidationGroup; }
        set { this.rfvCboEmpresa.ValidationGroup = value; }
    }
    public bool EnabledValidacion
    {
        get { return this.rfvCboEmpresa.Enabled; }
        set { this.rfvCboEmpresa.Enabled = value; }
    }
    protected void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.SelectedIndexChangedEvent(sender, e);
        }
        catch (Exception ex)
        {
            this.ErrorEvent(this, ex);
        }
    }
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SGS_UserControl_ComboLinea : System.Web.UI.UserControl
{
    public delegate void SelectedIndexChangedDelegate(object sender, EventArgs e);
    public event SelectedIndexChangedDelegate SelectedIndexChanged;

    public String CssClass
    {
        get { return this.cboLinea.CssClass; }
        set { this.cboLinea.CssClass = value; }
    }
    public Unit Width
    {
        set { ViewState["_Width"] = value; }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (ViewState["_Width"] != null) this.cboLinea.Width = (Unit)ViewState["_Width"];
            //si no se asigna ningun valor a la condicion 
            //el valor por defecto sera --Todos--
            String objeto = ConstanteBE.OBJECTO_TODOS;
            if (condicion != null)
                if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE))
                    objeto = ConstanteBE.OBJECTO_SELECCIONE;

            this.cboLinea.Items.Insert(0, new ListItem(objeto, String.Empty));
        }
    }

    private String condicion;
    public String Condicion
    {
        get { return condicion; }
        set { condicion = value; }
    }

    private String _cod_negocio;
    public String cod_negocio
    {
        get { return _cod_negocio; }
        set { _cod_negocio = value; }
    }

    public String SelectedValue
    {
        get { return this.cboLinea.SelectedValue; }
        set { this.cboLinea.SelectedValue = value; }
    }

    public String SelectedText
    {
        get { return this.cboLinea.SelectedItem.Text; }
    }

    public bool AutoPostBack
    {
        set { this.cboLinea.AutoPostBack = value; }
        get { return this.cboLinea.AutoPostBack; }
    }

    public void cargarCombo(String opcion)
    {
        NegocioLineaBL oNegocioLineaBL = new NegocioLineaBL();

        cboLinea.DataSource = oNegocioLineaBL.GetListaLineaImportacion(_cod_negocio);
        cboLinea.DataValueField = "nid_negocio_linea";
        cboLinea.DataTextField = "nom_linea";
        cboLinea.DataBind();

        String objeto = ConstanteBE.OBJECTO_TODOS;
        if (!opcion.Equals(String.Empty))
            if (opcion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE))
                objeto = ConstanteBE.OBJECTO_SELECCIONE;

        this.cboLinea.Items.Insert(0, new ListItem(objeto, String.Empty));
        //this.cboLinea.SelectedValue = String.Empty;
    }
    //para asignar el grupo al que pertenece la validacion
    public String ValidationGroup
    {
        get { return this.rfvCboLinea.ValidationGroup; }
        set { this.rfvCboLinea.ValidationGroup = value; }
    }
    //desabilitar la validacion del cboEstado en los casos de busqueda 
    public bool EnabledValidacion
    {
        get { return this.rfvCboLinea.Enabled; }
        set { this.rfvCboLinea.Enabled = value; }
    }

    protected void cboLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedIndexChanged(sender, e);
    }
}
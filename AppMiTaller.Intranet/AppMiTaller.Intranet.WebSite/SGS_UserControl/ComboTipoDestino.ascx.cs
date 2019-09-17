using System;
using System.Web.UI.WebControls;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

public partial class SGS_UserControl_ComboTipoDestino : System.Web.UI.UserControl
{
    //Delegados 
    public delegate void SelectedIndexChangedDelegate(object sender, EventArgs e);
    public event SelectedIndexChangedDelegate SelectedIndexChanged;

    public bool AutoPostBack
    {
        set { this.cboTipoDestino.AutoPostBack = value; }
        get { return this.cboTipoDestino.AutoPostBack; }
    }

    public String CssClass
    {
        get { return this.cboTipoDestino.CssClass; }
        set { this.cboTipoDestino.CssClass = value; }
    }

    public String SelectedValue
    {
        get { return this.cboTipoDestino.SelectedValue; }
        set { this.cboTipoDestino.SelectedValue = value; }
    }

    public String SelectedText
    {
        get { return this.cboTipoDestino.SelectedItem.Text; }
    }

    private String condicion;

    public String Condicion
    {
        get { return condicion; }
        set { condicion = value; }
    }

    public String OnChange
    {
        set { this.cboTipoDestino.Attributes["onChange"] = value; }
    }
    public Unit Width
    {
        set { ViewState["_Width"] = value; }
    }

    public Boolean Enabled
    {
        set { if (this.cboTipoDestino != null) this.cboTipoDestino.Enabled = value; }
    }

    public String canalVin;
    public String CanalVin
    {
        get { return canalVin; }
        set { canalVin = value; }
    }

    public Int32 nidVin;
    public Int32 NidVin
    {
        get { return nidVin; }
        set { nidVin = value; }
    }
    
    public String cTipoLlamada;
    public String CTipoLLamada
    {
        get { return cTipoLlamada; }
        set { cTipoLlamada = value; }
    }
    
    public ListItemCollection Items
    {
        get { return this.cboTipoDestino.Items; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void CargarCombo()
    {
        if (cTipoLlamada == null || cTipoLlamada.Trim().Equals(string.Empty))
        {
            cTipoLlamada = "";
        }
        
        if (ViewState["_Width"] != null) this.cboTipoDestino.Width = (Unit)ViewState["_Width"];
        
        TipoDestinoBL oTipoDestinoBL = new TipoDestinoBL();
        TipoDestinoBEList oList = null;
        this.cboTipoDestino.Items.Clear();
        String tipUbica = String.Empty;

        if (this.CanalVin == null || this.CanalVin.Trim().Equals(String.Empty))
        {
            if (cTipoLlamada.Trim().Equals("13"))
            {
                oList = oTipoDestinoBL.ListarDestinoUsuario(tipUbica, Profile.Usuario.NID_USUARIO);
            }
            else
            {
                oList = oTipoDestinoBL.ListarDestino(tipUbica);
            }
        }
        else
        {
            if (cTipoLlamada.Trim().Equals("13"))
            {
                oList = oTipoDestinoBL.ListarDestinoUsuario(tipUbica, Profile.Usuario.NID_USUARIO);
            }
            else
            {
                oList = oTipoDestinoBL.ListarDestinoCanalVin(tipUbica, this.NidVin);
            }
        }

        this.cboTipoDestino.SelectedValue = null;
        cboTipoDestino.DataSource = oList;
        cboTipoDestino.DataValueField = "Cod_tipo_ubicacion";
        cboTipoDestino.DataTextField = "Nom_tipo_ubicacion";
        cboTipoDestino.DataBind();

        String objeto = "";
        if (String.IsNullOrEmpty(condicion)) condicion = "";
        if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_TODOS))
        {
            objeto = ConstanteBE.OBJECTO_TODOS;
        }
        else if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE))
        {
            objeto = ConstanteBE.OBJECTO_SELECCIONE;
        }
        else objeto = ConstanteBE.OBJECTO_TODOS;

        this.cboTipoDestino.Items.Insert(0, new ListItem(objeto, String.Empty));
    }
    public void CargarCombo(String tipoUbicacion)
    {
        TipoDestinoBL oTipoDestinoBL = new TipoDestinoBL();
        TipoDestinoBEList oList = null;
        this.cboTipoDestino.Items.Clear();

        oList = oTipoDestinoBL.ListarDestino(tipoUbicacion);
        if (oList != null)
            for (int i = 0; i < oList.Count; i++)
                this.cboTipoDestino.Items.Add(new ListItem(oList[i].Nom_tipo_ubicacion, oList[i].Cod_tipo_ubicacion.ToString()));

        String objeto = "";
        if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_TODOS)) objeto = ConstanteBE.OBJECTO_TODOS;
        else if (condicion.Equals(ConstanteBE.OBJECTO_TIPO_SELECCIONE)) objeto = ConstanteBE.OBJECTO_SELECCIONE;
        else objeto = ConstanteBE.OBJECTO_TODOS;
        this.cboTipoDestino.Items.Insert(0, new ListItem(objeto, String.Empty));
    }

    protected void cboTipoDestino_SelectedIndexChanged(object sender, EventArgs e)
    {
        //String sel = this.cboMarca.SelectedValue;
        SelectedIndexChanged(sender, e);
    }
    public String FindItem(String valor)
    {
        String retorno = String.Empty;
        if (cboTipoDestino.Items.FindByValue(valor) != null)
            retorno = valor;

        return retorno;
    }
}

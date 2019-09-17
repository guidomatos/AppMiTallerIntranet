using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SGS_UserControl_TextBoxFecha : System.Web.UI.UserControl
{
    public String Text
    {
        set { this.txtFecha.Text = value; }
        get { return this.txtFecha.Text; }
    }

    public String CssClass
    {
        set { this.txtFecha.CssClass = value; }
        get { return this.txtFecha.CssClass; }
    }

    public Boolean ReadOnly
    {
        set
        {
            if (this.txtFecha != null)
            {
                this.txtFecha.ReadOnly = value;
            }
            else
            {
                ViewState["_ReadOnly"] = value;
            }
        }
    }

    public void SetAtributtes(String evento, String funcion)
    {
        this.txtFecha.Attributes.Add(evento, funcion);
    }

    public Boolean Enabled
    {
        set
        {
            if (this.txtFecha != null)
            {
                this.txtFecha.Enabled = value;
                this.btnFecha.Visible = value;
            }
            else
            {
                ViewState["_Enabled"] = value;
            }
        }
    }

    public Unit Width
    {
        set { ViewState["_Width"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (ViewState["_Width"] != null) this.txtFecha.Width = (Unit)ViewState["_Width"];

            this.btnFecha.Style["cursor"] = "pointer";

            if (ViewState["_Enabled"] != null)
            {
                this.txtFecha.Enabled = (Boolean)ViewState["_Enabled"];
                this.btnFecha.Visible = (Boolean)ViewState["_Enabled"];
            }

            this.ceFecha.DefaultDate = DateTime.Now.ToShortDateString();
        }
    }
}
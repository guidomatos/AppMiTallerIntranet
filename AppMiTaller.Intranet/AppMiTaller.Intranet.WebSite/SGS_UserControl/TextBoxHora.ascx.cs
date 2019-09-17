using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SGS_UserControl_TextBoxHora : System.Web.UI.UserControl
{
    public String CssClass
    {
        get { return this.txtHora.CssClass; }
        set { this.txtHora.CssClass = value; }
    }
    public String Text
    {
        set { this.txtHora.Text = value; }
        get { return this.txtHora.Text.Trim(); }
    }
    public Unit Width
    {
        set { ViewState["_Width"] = value; }

    }
    public Boolean Enabled
    {
        set
        {
            if (this.txtHora != null)
            {
                this.txtHora.Enabled = value;
            }
            else
            {
                ViewState["_Enabled"] = value;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (ViewState["_Width"] != null) this.txtHora.Width = (Unit)ViewState["_Width"];
            if (ViewState["_Enabled"] != null)
            {
                this.txtHora.Enabled = (Boolean)ViewState["_Enabled"];
            }
        }
    }
}
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public class ComunCode
{
    /// <summary>
    /// Formatea la cadena para mostrarlo en un alert, quitandole los caracteres especiales que puedan evitar mostrarse
    /// </summary>
    /// <param name="pCadena">Cadena Origen</param>
    /// <returns></returns>
    public static string String_MsgToAlert(string pCadena)
    {
        pCadena = pCadena.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("'", "").Replace("\\", "/");
        pCadena = pCadena.Replace(Convert.ToChar(13), '-').Replace(Convert.ToChar(10), '-');
        return pCadena;
    }
    public static void Alert(UpdatePanel UP, string pKey, string pMensaje)
    {
        ScriptManager.RegisterClientScriptBlock(UP, UP.GetType(), pKey, String.Format("alert('{0}');", pMensaje), true);
    }
    public static void Script(UpdatePanel UP, string pKey, string pScript){
        ScriptManager.RegisterClientScriptBlock(UP, UP.GetType(), pKey, pScript, true);
    }
    public static void OnClick(UpdatePanel UP, string pKey, string ControlClientID)
    {
        ScriptManager.RegisterClientScriptBlock(UP, UP.GetType(), pKey, String.Format("document.getElementById('{0}').click();", ControlClientID), true);
    }
    public static void SetFormatImageButton(ImageButton ibtn, String img, String pOnClientClick)
    {
        ibtn.ImageUrl = string.Format("~/Images/iconos/b-{0}.gif", img);
        ibtn.Attributes.Add("onmouseover", string.Format("javascript:this.src='../Images/iconos/b-{0}2.gif'", img));
        ibtn.Attributes.Add("onmouseout", string.Format("javascript:this.src='../Images/iconos/b-{0}.gif'", img));
        if (!string.IsNullOrEmpty(pOnClientClick))
        {
            ibtn.OnClientClick = string.Format("JavaScript:return {0};", pOnClientClick);
        }
    }
    public static void PressKey(TextBox TxtOnPress, string CtrlPressID)
    {
        TxtOnPress.Attributes.Add("onkeydown", string.Format("fc_PressKey(13,'{0}');", CtrlPressID));
    }
    public static void PressKey(DropDownList ddlOnPress, string CtrlPressID)
    {
        ddlOnPress.Attributes.Add("onkeydown", string.Format("fc_PressKey(13,'{0}');", CtrlPressID));
    }
    public static bool Goto(Page Form, bool pCondicion, string pMensaje, UpdatePanel up)
    {
        if (pCondicion)
        {
            string PageGoTo = "../Inicio/Default.aspx";
            if (up == null)
                Form.ClientScript.RegisterStartupScript(Form.GetType(), "__BeforeLoad__", string.Format("alert('{0}');location.href='{1}';", pMensaje, PageGoTo), true);
            else
                Script(up, "__BeforeLoad__", string.Format("alert('{0}');location.href='{1}';", pMensaje, PageGoTo));
        }
        return pCondicion;
    }

    public static int ValidarHorario()
    {
        int intMensaje = 0;
        DateTime Hoy = DateTime.Now;

        if (Hoy <= Convert.ToDateTime("01/09/2011 12:00:00"))
        {
            intMensaje = 1;
        }
        else if (Hoy <= Convert.ToDateTime("02/09/2011 06:00:00"))
        {
            intMensaje = 2;
        }
        return intMensaje;
    }
}

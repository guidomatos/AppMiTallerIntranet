using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;
using System.IO;

public static class Utility
{
    public static String GenerarRutaCarpeta()
    {
        return String.Concat(DateTime.Now.Year, DateTime.Now.ToString("MM"));
    }
    public static String CrearCarpetaFileServer(String path)
    {
        String newPath = String.Empty;
        newPath = String.Concat(path, Utility.GenerarRutaCarpeta());
        if (!Directory.Exists(newPath))
        {
            Directory.CreateDirectory(newPath);
        }
        return String.Concat(newPath, @"\");
    }
    private static void BloquearControles(Control control, bool flag)
    {
        foreach (Control subControl in control.Controls)
        {
            switch (subControl.GetType().Name)
            {
                case "TextBox":
                    ((TextBox)subControl).Enabled = !flag;
                    ((TextBox)subControl).CssClass = flag ? "Disabled" : "Enabled";
                    break;
                case "DropDownList":
                    ((DropDownList)subControl).Enabled = !flag;
                    ((DropDownList)subControl).CssClass = flag ? "Disabled" : "Enabled";
                    break;
                case "RadioButtonList":
                    ((RadioButtonList)subControl).Enabled = !flag;
                    ((RadioButtonList)subControl).CssClass = flag ? "Disabled" : "Enabled";
                    break;
                default:
                    BloquearControles(subControl, flag);
                    break;
            }
        }
    }
    public static String OcultarControlesJS(Control control, bool flag, ref StringBuilder function)
    {
        foreach (Control subControl in control.Controls)
        {
            if (subControl.GetType().Name.Equals("Button"))
                function.AppendLine(string.Format("document.getElementById('{0}').style.display = {1};", subControl.ClientID, flag ? "inline" : "none"));
            else OcultarControlesJS(subControl, flag, ref function);
        }
        return function.ToString();
    }
    private static void LimpiarControlesJS(Control control, ref StringBuilder function, List<String> lstControlType)
    {
        foreach (Control subControl in control.Controls)
        {
            if (lstControlType.IndexOf(subControl.GetType().Name) > -1)
            {
                function.AppendLine(string.Format("document.getElementById('{0}').value = '';", subControl.ClientID));
            }
            else LimpiarControlesJS(subControl, ref function, lstControlType);
        }
    }
    private static void BloquearControlesJS(Control control, bool flag, ref StringBuilder function, List<String> lstControlType)
    {
        foreach (Control subControl in control.Controls)
        {
            if (lstControlType.IndexOf(subControl.GetType().Name) > -1)
            {
                function.AppendLine(string.Format("document.getElementById('{0}').disabled = {1};", subControl.ClientID, flag.ToString().ToLower()));
                function.AppendLine(string.Format("document.getElementById('{0}').className = 'Disabled';", subControl.ClientID));
            }
            else
                BloquearControlesJS(subControl, flag, ref function, lstControlType);
        }
    }
    public static String ObtenerCarpetaFileServer(String file)
    {
        String noCarpeta = String.Empty, strCarpeta = String.Empty;

        noCarpeta = Path.GetFileNameWithoutExtension(file);

        if (!noCarpeta.Trim().Equals(String.Empty))
        {
            strCarpeta = noCarpeta.Trim().Substring(noCarpeta.Length - 14, 14);

            strCarpeta = strCarpeta.Trim().Substring(0, 6);
        }

        return strCarpeta;
    }
}
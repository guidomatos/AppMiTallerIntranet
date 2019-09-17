using System;

public class Global
{
    public Global()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    #region "Manejo de errores no controlados por aplicativo"
    public static System.Exception LastError;

    public static string getMensajeError()
    {

        while (LastError.InnerException != null)
        {
            LastError = LastError.InnerException;

        }
        return LastError.Message;

    }
    #endregion

    public static string ObtenerXml(String CodOpciones, String IndOpciones)
    {
        System.Text.StringBuilder SB = new System.Text.StringBuilder();
        SB.Append("<?xml version=\"1.0\"?><ROOT>");

        String[] arrCodOpciones = CodOpciones.Trim().Split('|');
        String[] arrIndOpciones = IndOpciones.Trim().Split('|');

        for (int tk = 0; tk < arrCodOpciones.Length - 1; tk++)
        {
            if (!arrIndOpciones[tk].Trim().Equals(String.Empty))
            {
                String xml = "<doc ";
                xml += "nid_opcion=" + "\"" + arrCodOpciones[tk] + "\" " + " fl_tipo_acceso=" + "\"" + arrIndOpciones[tk] + "\" ";
                xml = xml + "/>";
                SB.Append(xml);
            }
        }
        SB.Append("</ROOT>");
        return SB.ToString();
    }

    public static string ObtenerXmlPerfil(String CodOpciones, String IndOpciones, String CadNidOpcionPerfil)
    {
        System.Text.StringBuilder SB = new System.Text.StringBuilder();
        SB.Append("<?xml version=\"1.0\"?><ROOT>");

        String[] arrCodOpciones = CodOpciones.Trim().Split('|');
        String[] arrIndOpciones = IndOpciones.Trim().Split('|');
        String[] arrCadNidOpcionPerfil = CadNidOpcionPerfil.Trim().Split('|');

        for (int tk = 0; tk < arrCodOpciones.Length - 1; tk++)
        {
            if (!arrIndOpciones[tk].Trim().Equals(String.Empty))
            {
                String xml = "<doc ";
                xml += "nid_opcion=" + "\"" + arrCodOpciones[tk] + "\" " + " fl_tipo_acceso=" + "\"" + arrIndOpciones[tk] + "\" " + " nid_opcion_perfil=" + "\"" + arrCadNidOpcionPerfil[tk] + "\" ";
                xml = xml + "/>";
                SB.Append(xml);
            }
        }
        SB.Append("</ROOT>");
        return SB.ToString();
    }
}

using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;

[WebService(Namespace = "http://localhost/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class wsCombos : System.Web.Services.WebService
{

    public wsCombos()
    {

    }
   
    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    public object Get_PaisTelefono(String[] strParametros)
    {
        // Variables retorno Serializadas
        object strRetorno;
        ArrayList oCombo;
        try
        {

            PaisBEList oPaisBEList = null;
            PaisBL oPaisBL = new PaisBL();

            Int32 UsuarioID = 0;
            Int32.TryParse(strParametros[0], out UsuarioID);
            oPaisBEList = oPaisBL.GetListaPaisTelefono();

            oCombo = new ArrayList();


            foreach (PaisBE oEntidad in oPaisBEList)
            {
                object objPais;
                objPais = new { value = oEntidad.nid_pais.ToString(), nombre = oEntidad.no_pais };
                oCombo.Add(objPais);
            }


            strRetorno = new
            {
                oComboPaisTelefono = oCombo,
                msg_retorno = String.Empty
            };

        }
        catch (Exception e)
        {
            strRetorno = new
            {
                oComboPaisTelefono = String.Empty,
                msg_retorno = "Error: " + e.Message
            };
        }
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(strRetorno);
    }
    
}

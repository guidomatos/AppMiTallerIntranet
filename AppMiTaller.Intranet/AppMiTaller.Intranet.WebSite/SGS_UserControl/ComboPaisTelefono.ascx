<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ComboPaisTelefono.ascx.cs" Inherits="SGS_UserControl_ComboPaisTelefono" %>
<%@ Import Namespace="AppMiTaller.Intranet.BE" %>
<select name="select" id="cboPaisTelefono" class="form-control"></select>

<script type="text/javascript">


    function fc_UserControlComboPaisTelefono(IndObligatorio, disabled, jsonAsync, comboId) {

        if (document.getElementById('cboPaisTelefono'))
            document.getElementById('cboPaisTelefono').id = comboId;
        else
            return;

        var parametros = new Array();
        parametros[0] = "<%=this.Profile.Usuario.NID_USUARIO %>";

        var Obligatorio = "";

        if (IndObligatorio == "T")
            Obligatorio = "<%=ConstanteBE.OBJECTO_TODOS %>";
        else
            Obligatorio = "<%=ConstanteBE.OBJECTO_SELECCIONE %>";

        var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
        var strUrlServicio = "../SGS_UserControl/wsCombos.asmx/Get_PaisTelefono"
        this.fc_getJsonAjax(strParametros, strUrlServicio,
            function (objResponse) {

                if (objResponse.msg_retorno == "") {
                    this.fc_FillCombo(comboId, objResponse.oComboPaisTelefono, Obligatorio);
                    if (disabled == "1") {
                        $("#" + comboId).attr("disabled", true);
                    }
                }
                else { fc_Alert(objResponse.msg_retorno); }
            }, jsonAsync);

    }
</script>
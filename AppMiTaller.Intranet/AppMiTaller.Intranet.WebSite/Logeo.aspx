<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logeo.aspx.cs" Inherits="Logeo" Theme="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SRC</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE10; IE=EmulateIE11" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" href="Images/Minvest.ico" />
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" href="bootstrap/css/JqGrid/jquery-ui.min.css" />
    <link rel="stylesheet" type="text/css" href="bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="bootstrap/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="bootstrap/css/fontello.min.css" />

    <!-- JS -->
    <script type="text/javascript" src="bootstrap/js/jqGrid-4.6.0/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="bootstrap/js/jqGrid-4.6.0/jquery-1.11.1-ui.min.js"></script>
    <script type="text/javascript" src="bootstrap/js/jqGrid-4.6.0/Funciones.min.js" ></script>
    <script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="bootstrap/js/mensaje.js"></script>
    <%--<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />--%>
    <meta name="viewport" content="width=device-width" />
    <style type="text/css">
        html
        {
            height: 100%;
            box-sizing: border-box;
        }
        *, *:before, *:after
        {
            box-sizing: inherit;
        }
        body
        {
            position: relative;
            margin: 0;
            padding-bottom: 6rem;
            min-height: 100%;
            color: #003C79;
            background-image: url(images/flash/fondo.jpg);
            background-repeat: repeat-x;
            font-family: Arial;
            font-size: 14px;
        }
        @font-face
        {
            font-family: font-DeckerB;
            src: url(images/DeckerB.ttf);
        }
        .header {
			padding-top: 30px;
			padding-bottom: 30px;
			text-align: center;
		}
        .footer
        {
            position: absolute;
            right: 0;
            bottom: 0;
            left: 0;
            padding: 10px;
            background: #000;
            color: #FFF;
            font-size: 10px;
            text-align: center;
        }
        .contenedor-login {
			background: url(images/fondo-login2.png)no-repeat;
			min-height: 620px;
			position: absolute;
			background-size: cover;
			width: 100%;
			height: 100%;
			padding: 4% 20px;
		}
        a
        {
            text-decoration: underline;
            font-weight: bold;
            color: #003C79;
			text-shadow:0-1px 2px #97c8f3;
        }
        a:hover
        {
            color: #425b92;
        }
        .title
        {
            font-family: font-DeckerB;
            font-size: 40px;
            font-weight: bolder;
            vertical-align: middle;
            padding-left: 10px;
			text-shadow: 0px 2px 3px #ffffff;
        }
        .login {
			max-width: 400px;
			margin: 0 auto;
		}

        label
        {
            font-weight: bold;
        }
        input[type="text"], input[type="password"]
        {
            font-weight: normal;
            width: 100%;
            height: 34px;
            padding: 6px 12px;
            box-sizing: border-box;
            font-size: 12px !important;
            line-height: 1.42857143;
            color: #555;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        }
        textarea
        {
            font-size: 12px !important;
        }
        input[type=submit]
        {
            font-size: 14px;
        }
        .form-horizontal
        {
            padding-bottom: 7px;
        }
        .ui-dialog
        {
            font-size: 14px;
            min-width: 300px;
            width: inherit !important;
            max-width: 400px;
        }
        .modal-title
        {
            font-size: 14px;
        }
        .modal-body
        {
            padding: 25px 50px;
            font-size: 14px;
        }
        .oblig
        {
            position: absolute;
            top: 0;
            right: 0;
        }
        .form-horizontal .has-feedback .form-control-feedback
        {
            right: 0px;
        }
        .btn-default
        {
            border-bottom-width: 2px;
        }
        @media only screen and (orientation: landscape)
        {
            body
            {
                padding-bottom: 0;
            }
        }
        @media only screen and (max-width : 600px)
        {
            body
            {
                padding-bottom: 0;
            }
            .header
            {
                padding-left: 0;
            }
            .title
            {
                font-size: 20px;
                font-weight: bold;
            }
        }
        @media only screen and (max-width : 360px)
        {
            body
            {
                font-size: 14px;
            }
            .title
            {
                font-size: 16px;
                font-weight: bold;
            }
            input[type=submit]
            {
                font-size: 14px;
            }
            .modal-body
            {
                padding: 15px;
            }
        }
        
        @media only screen and (min-width: 500px)
        {
            .modal-md
            {
                max-width: 470px;
                margin: 30px auto;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="txhPolitica" runat="server" />
    <div>
        
        <div class="contenedor-login">
			<p class="header">
				<img src="Images/logo_SGA.png" alt="Logo" style="display:none">
				<label class="title">
							Sistema de Reserva de Citas</label>
			</p>
            <div class="login">
                <div style="text-align: right;">
                    <label style="font-size:16px;">
                        Ingreso al Sistema</label>
                        <img alt="" src="Images/Flash/bandera.png" style="height:15px;margin-bottom: 5px;display:none">
					</div>
                <div>
                    <div style="background: url('Images/Flash/puntos.gif') repeat-x; padding-bottom:1px;">
                    </div>
                        
                    <div style="padding: 10px;">
                        <asp:Login ID="pnlLogeo" runat="server" DisplayRememberMe="False" LoginButtonText="Ingresar"
                            UserNameLabelText="Usuario" PasswordLabelText="Contraseña" TitleText="" FailureText=""
                            DestinationPageUrl="Inicio/Default.aspx" OnAuthenticate="Logeo_Authenticate"
                            Width="100%">
                            <LayoutTemplate>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <div class="has-feedback">
                                                <asp:TextBox ID="UserName" runat="server" PlaceHolder="Usuario" MaxLength="20"></asp:TextBox>
                                                <i class="glyphicon glyphicon-user form-control-feedback" style="color:#555B6C;"></i>
                                            </div>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="oblig"
                                                ValidationGroup="Logeo">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <div class="has-feedback">
                                                <asp:TextBox ID="Password" runat="server" PlaceHolder="Contraseña" TextMode="Password"></asp:TextBox>
                                                <i class="glyphicon glyphicon-lock form-control-feedback" style="color:#555B6C;"></i>
                                            </div>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" CssClass="oblig"
                                                ValidationGroup="Logeo">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-xs-12">
                                    <asp:Button ID="LoginButton" CommandName="Login" runat="server" Text="Ingresar" ValidationGroup="Logeo"
                                        CssClass="btn btn-primary" style="width:100%;" />
                                        </div>
                                        </div>
                                </div>
                                <div style="background: url('Images/Flash/puntos.gif') repeat-x; height: 10px;">
                                </div>
                                <div>
                                    <div class="form-horizontal">
                                    <div class="form-group" style="font-size:13px">
                                    <div class="col-xs-6" style="text-align:left;">
                                        <a id="lnkCambiarPwd" href="#">Cambiar contraseña</a>
                                        </div>
                                        <div class="col-xs-6" style="text-align:right";>
                                        <a id="lnkRecuperarPwd" href="#">Recuperar contraseña</a>
                                        </div>
                                        
                                    <asp:LinkButton ID="lbRecuperarContrasena" runat="server" OnClick="lbRecuperarContrasena_Click"
                                        Style="display: none;">Recuperar Contraseña</asp:LinkButton>
                                    </div>
                                    </div>
                                        
                                    <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                                        <div class="alert alert-warning" style="text-align:justify;font-size:13px;">
                                            <p class="fa fa-warning fa fa-2x">
                                            &nbsp</p>
                                            <asp:Label ID="FailureText" runat="server"></asp:Label>
                                        </div>
                                    </asp:Panel>
       
                                </div>
                            </LayoutTemplate>
                        </asp:Login>
                    </div>
                </div>
            </div>
            <div class="footer">
                ©<%=DateTime.Now.Year.ToString() %>
                Todos los derechos reservados | Sistema de Reserva de Citas v1.0
            </div>
        </div>
        <div class="modal" id="modal_CambiarPwd" style="cursor: move" data-backdrop='static'
            data-keyboard='false' tabindex='-1'>
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" style="float:left; padding-top: 5px;">
                            CAMBIO CONTRASEÑA</h4>
                            <div style="float: right;">                            
                                <button id="btnLimpiar_CambioPwd" type="button" class="btn btn-default" title="Limpiar">
                                    <span class="glyphicon glyphicon-erase" style="color:#1f497d;"></span> Limpiar
                                </button>
                                <button type="button" class="btn btn-default" onclick="fn_Aceptar_CambioPwd();" title="Aceptar">
                                    <span class="glyphicon glyphicon-ok" style="color:#10ab10;"></span> Aceptar
                                </button>
                                <button id="btnClose_CambioPwd" type="button" class="btn btn-default" title="Cerrar">
                                    <span class="glyphicon glyphicon-remove" style="color:#ff0000;"></span> Cerrar
                                </button>                               
                            <asp:ImageButton ID="btnAceptarCambioContrasenha" runat="server" ToolTip="Aceptar"
                                Style="display: none;" ImageUrl="Images/iconos/b-aceptar.gif"
                                OnClick="btnAceptarCambioContrasenha_Click" onmouseover="javascript:this.src='Images/iconos/b-aceptar2.gif'"
                                onmouseout="javascript:this.src='Images/iconos/b-aceptar.gif'" />
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="has-feedback">
                                                <asp:TextBox ID="txtUsuario_CambioPwd" runat="server" placeholder="Usuario" CssClass="form-control"></asp:TextBox>
                                                <i class="icon icon-user form-control-feedback" style="color:#A4A4A4; margin-top:0.5em;"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="has-feedback">
                                                <asp:TextBox ID="txtPwd_CambioPwd" TextMode="Password" runat="server" placeholder="Contraseña Actual" CssClass="form-control" />
                                                <i class="icon icon-lock form-control-feedback" style="color:#A4A4A4; margin-top:0.5em;"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="has-feedback">
                                                <asp:TextBox ID="txtNewPwd_CambioPwd" runat="server" TextMode="password" placeholder="Nueva Contraseña" CssClass="form-control" />
                                                <i class="icon icon-lock-filled form-control-feedback" style="color:#0070C0; margin-top:0.5em;"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="has-feedback">
                                                <asp:TextBox ID="txtConfirmPwd_CambioPwd" runat="server" TextMode="Password" placeholder="Confirmar Nueva Contraseña" class="form-control" />
                                                <i class="icon icon-lock-filled form-control-feedback" style="color:#0070C0; margin-top:0.5em;"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#modal_CambiarPwd").draggable({
            handle: ".modal-header"
        });
        $("#modal_UsuaBlock").draggable({
            handle: ".modal-header"
        });
        $("#lnkCambiarPwd").click(function() {
            document.getElementById("<%=txtUsuario_CambioPwd.ClientID%>").value = "";
            document.getElementById("<%=txtPwd_CambioPwd.ClientID%>").value = "";
            document.getElementById("<%=txtNewPwd_CambioPwd.ClientID%>").value = "";
            document.getElementById("<%=txtConfirmPwd_CambioPwd.ClientID%>").value = "";
            $("#modal_CambiarPwd").modal("show");
        });

        $("#lnkRecuperarPwd").click(function() {
            if (document.getElementById("pnlLogeo_UserName").value != "") {
                fc_Confirm("Esta acción reinicializará su contraseña y le enviará un correo con instrucciones para poder ingresar nuevamente al sistema. ¿Desea continuar?", function(res) {
                    if (res == true) {
                        document.getElementById("pnlLogeo_lbRecuperarContrasena").click();
                    }
                });
            } else {
                fc_Alert("Debe ingresar un usuario!");
            }
        });

        $("#btnLimpiar_CambioPwd").click(function() {
            document.getElementById("<%=txtUsuario_CambioPwd.ClientID%>").value = "";
            document.getElementById("<%=txtPwd_CambioPwd.ClientID%>").value = "";
            document.getElementById("<%=txtNewPwd_CambioPwd.ClientID%>").value = "";
            document.getElementById("<%=txtConfirmPwd_CambioPwd.ClientID%>").value = "";
        });
        function fn_Aceptar_CambioPwd() {
            var msjError = "";
            var usuario = fc_Trim(document.getElementById("<%=txtUsuario_CambioPwd.ClientID%>").value);
            var pwd = fc_Trim(document.getElementById("<%=txtPwd_CambioPwd.ClientID%>").value);
            var new_pwd = fc_Trim(document.getElementById("<%=txtNewPwd_CambioPwd.ClientID%>").value);
            var confirm_pwd = fc_Trim(document.getElementById("<%=txtConfirmPwd_CambioPwd.ClientID%>").value);
            if (usuario == "") {
                msjError += "Debe ingresar el usuario.\n";
            }
            if (pwd == "") {
                msjError += "Debe ingresar su contraseña actual.\n";
            }
            if (new_pwd == "") {
                msjError += "Debe ingresar su contraseña nueva.\n";
            }
            if (new_pwd != "") {
                if (new_pwd.length < 6) {
                    msjError += "- La contraseña nueva debe de ser de mínimo 6 carácteres.\n";
                }
            }

            if (confirm_pwd == "") {
                msjError += "Debe ingresar la confirmación de la contraseña.\n";
            }
            if (confirm_pwd != "") {
                if (confirm_pwd.length < 6) {
                    msjError += "- La confirmación de la contraseña debe de ser de mínimo 6 carácteres.\n";
                }
            }

            if (new_pwd != "" && confirm_pwd != "" && new_pwd != confirm_pwd) {
                msjError += "La confirmación de la contraseña no es válida.\n";
            }

            if (msjError != "") {
                fc_Alert(msjError);
            }
            else {
                fc_Confirm(mstrSeguroGrabar, function(res) {
                    if (res == true) {
                        document.getElementById("<%=btnAceptarCambioContrasenha.ClientID %>").click();
                    }
                });
            }
        }
        $("#btnClose_CambioPwd").click(function() {
            $("#modal_CambiarPwd").modal("hide");
        });
        $("#btnClose_UsuaBlock").click(function() {
            $("#modal_UsuaBlock").modal("hide");
        });
    </script>

</body>
</html>

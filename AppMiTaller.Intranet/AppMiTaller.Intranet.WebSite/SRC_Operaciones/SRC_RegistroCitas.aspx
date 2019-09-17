<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="SRC_RegistroCitas.aspx.cs" Inherits="SRC_Operaciones_SRC_RegistroCitas" Async="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Src="~/SGS_UserControl/ComboPaisTelefono.ascx" TagName="ComboPaisTelefono" TagPrefix="uc1" %>
<%@ Import Namespace="AppMiTaller.Intranet.BE" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/SRC_Validacion.js"></script>
    <style type="text/css">
        .ui-search-input {
            color: #000;
        }

        .ui-search-oper {
            display: none;
        }

        .ui-search-clear {
            display: none;
        }

        @media (max-width: 567px) {
            .container-fluid {
                padding-right: 0px;
                padding-left: 0px;
            }
        }

        @media (min-width: 768px) {
            .col_11_3_porcent {
                width: 11.3%;
            }

            .col_88_7_porcent {
                width: 88.7%;
            }

            .sm_border_right {
                border-right: solid 2px #d4dfff;
            }
        }

        .divselhora {
            background-color: #005cab;
            color: #FFF;
            min-height: 30px;
            margin-top: 10px;
            margin-bottom: 10px;
            padding: 5px;
            vertical-align: middle;
            width: 100%;
            text-align: center;
            font-weight: bold;
            font-size: 12px;
        }

        input[type="radio"], input[type="radio"] + label {
            cursor: pointer;
        }

        tr[role="rowheader"] th div {
            word-wrap: break-word;
            white-space: normal !important;
            height: auto !important;
        }

        tr[role="row"] td {
            word-wrap: break-word;
            white-space: normal !important;
        }

        .footable > thead > tr > th {
            background: #005cab;
            color: #FFF;
            text-align: center;
        }

        .footable > tbody > tr > td, .footable > thead > tr > th {
            padding: 0;
            border: solid 1px #FFF;
            border-bottom-color: #005cab;
            padding: 2px;
        }

        .footable > tbody > tr:nth-child(even) {
            background: #E3E7F2;
        }
    </style>
    <div class="container-fluid">
        <%-- INICIO CABECERA DIV--%>
        <div class="panel panel-default">
            <div class="panel-heading clearfix">
                <div class="pull-left">
                    <i class="glyphicon-more_windows"></i><span class="SpanCabecera">&nbsp;&nbsp;RESERVAR
                        CITA</span>
                </div>
                <div class="pull-right">
                </div>
            </div>
            <div class="clearfix">
            </div>
            <div class="panel-body">
                <div class="box">
                    <div>
                        <div class="TituloLineaDiv">
                            <a href="#" class="content-slideUp" style="color: #002C67; text-decoration: none"><i
                                class="icon-angle-down icondown"></i>&nbsp;&nbsp;Detalle del Vehículo</a>
                        </div>
                    </div>
                    <div class="box-content">
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Placa</label>
                                        <div class="col-sm-8">
                                            <input type="text" name="txtPlaca" id="txtPlaca" placeholder="Buscar Placa..." class="form-control"
                                                maxlength="6" style="width: 75%; display: inline; text-transform: uppercase;"
                                                onkeypress="return SoloPlaca(event);"
                                                onkeyup="autocomplete_itemSelectedVehiculo = false;" />
                                            <img id="btnBuscarPlaca" src="../Images/iconos/lupa.gif" style="cursor: pointer;"
                                                title="Buscar Vehículo" onclick="fn_Open_BuscarVehiculo();">
                                            <img id="btnAgregarVehiculo" src="../Images/iconos/agregarvalor-chiquito.gif" style="cursor: pointer;"
                                                title="Agregar Vehículo" onclick="fn_Open_AgregarVehiculo();">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Marca</label>
                                        <div class="col-sm-8">
                                            <input type="text" name="txtMarca" id="txtMarca" class="form-control" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Modelo</label>
                                        <div class="col-sm-8">
                                            <input type="text" name="txtModelo" id="txtModelo" class="form-control" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            VIN</label>
                                        <div class="col-sm-8">
                                            <input type="text" name="txtVIN" id="txtVIN" class="form-control" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-8">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-2">
                                            Propietario</label>
                                        <div class="col-sm-10">
                                            <input type="text" name="txtPropietario" id="txtPropietario" class="form-control"
                                                disabled="disabled" style="width: 80%; display: inline;" />
                                            <img src="../Images/iconos/edit_hoja.ico" style="cursor: pointer;" title="Actualizar Propietario"
                                                onclick="fn_Open_ActualizarPropCli('1');">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-2">
                                            Cliente</label>
                                        <div class="col-sm-10">
                                            <input type="text" name="txtCliente" id="txtCliente" class="form-control" disabled="disabled"
                                                style="width: 80%; display: inline;" />
                                            <img src="../Images/iconos/edit_hoja.ico" style="cursor: pointer;" title="Actualizar Cliente"
                                                onclick="fn_Open_ActualizarPropCli('2');">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <button onclick="fn_Open_HistorialCita();" type="button" class="btn btn-default">Historial de Citas</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box">
                    <div>
                        <div class="TituloLineaDiv">
                            <a href="#" class="content-slideUp" style="color: #002C67; text-decoration: none"><i
                                class="icon-angle-down icondown"></i>&nbsp;&nbsp;Detalle del Contacto</a>
                        </div>
                    </div>
                    <div class="box-content">
                        <div class="row">
                            <div class="col-sm-10 sm_border_right">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Tipo Documento</label>
                                                <div class="col-sm-8">
                                                    <select id="cboTipoDocumento" class="form-control">
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Num. Documento</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtNroDocumento" id="txtNroDocumento" placeholder="Buscar Documento..."
                                                        class="form-control" maxlength="20" style="width: 80%; display: inline;" />
                                                    <img id="btnBuscarDocumento" src="../Images/iconos/lupa.gif" style="cursor: pointer;"
                                                        onclick="fn_GetCliente();" title="Buscar por número de documento">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Nombres(s)</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtNombres" id="txtNombres" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Apellido Paterno</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtApePaterno" id="txtApePaterno" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Apellido Materno</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtApeMaterno" id="txtApeMaterno" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Teléfono Fijo</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtTelefonoFijo" id="txtTelefonoFijo" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Teléfono Oficina</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtTelefonoOficina" id="txtTelefonoOficina" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Teléfono Móvil 1</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtTelefonoMovil1" id="txtTelefonoMovil1" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Teléfono Móvil 2</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtTelefonoMovil2" id="txtTelefonoMovil2" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Email Personal</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtEmailPersonal" id="txtEmailPersonal" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Email Trabajo</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtEmailTrabajo" id="txtEmailTrabajo" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">
                                                    Email Alternativo</label>
                                                <div class="col-sm-8">
                                                    <input type="text" name="txtEmailAlternativo" id="txtEmailAlternativo" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-2 col_11_3_porcent">
                                                    Observaciones</label>
                                                <div class="col-sm-10 col_88_7_porcent">
                                                    <textarea id="txtObservaciones" class="form-control"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="div_NotaIdentidadValidada" style="display: none;">
                                    <div class="col-sm-12" style="color: Red; font-weight: bold;">
                                        Nota: Algunos datos de la persona/empresa no pueden modificarse debido a que su
                                        identidad ha sido verificada con una entidad reguladora.
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <a href="#" style="color: #002C67; text-decoration: none; font-weight: bold;">&nbsp;&nbsp;Recordatorio</a>
                                <div class="row">
                                    <div class="col-sm-12">
                                        Por
                                        <input id="chkEmailCli" type="checkbox" disabled="disabled"
                                            checked="checked" />
                                        <label for="chkEmailCli">
                                            E-mail</label>
                                        <input id="chkSMSCli" type="checkbox" checked="checked" />
                                        <label for="chkSMSCli">
                                            SMS</label>
                                    </div>
                                </div>
                                <div id="divDatosRecord">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            Día
                                            <select id="cboDiaContacto" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            Horario
                                            <select id="cboHIContacto" class="form-control">
                                            </select>
                                            <select id="cboHFContacto" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box">
                    <div>
                        <div class="TituloLineaDiv">
                            <a href="#" class="content-slideUp" style="color: #002C67; text-decoration: none"><i
                                class="icon-angle-down icondown"></i>&nbsp;&nbsp;Detalle del Servicio</a>
                        </div>
                    </div>
                    <div class="box-content">
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Tipo Servicio</label>
                                        <div class="col-sm-8">
                                            <select id="cboTipoServicio" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Servicio Específico</label>
                                        <div class="col-sm-8">
                                            <select id="cboServicioEspecifico" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Departamento</label>
                                        <div class="col-sm-8">
                                            <select id="cboDepartamento" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Provincia</label>
                                        <div class="col-sm-8">
                                            <select id="cboProvincia" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Distrito</label>
                                        <div class="col-sm-8">
                                            <select id="cboDistrito" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Local</label>
                                        <div class="col-sm-8">
                                            <select id="cboUbicacion" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Taller</label>
                                        <div class="col-sm-8">
                                            <select id="cboTaller" class="form-control">
                                            </select>
                                            <button type="button" class="btn btn-default" id="btnMapaTallerPR">
                                                Ver Mapa de ubicación</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4" id="divValeTaxi">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Vale Taxi</label>
                                        <div class="col-sm-8">
                                            <input id="chkValeTaxi" type="checkbox" />
                                            <label for="chkValeTaxi">
                                                SI</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Recojo de Unidad</label>
                                        <div class="col-sm-8">
                                            <input id="chkRecojoUnidad" type="checkbox" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="dvCampaniasCitas">
                            <div class="TituloLineaDiv">
                                <a href="#" class="content-slideUp" style="color: #002C67; text-decoration: none">&nbsp;&nbsp;Detalle de Campañas</a>
                            </div>
                            <div id="divResultados" style="padding-right: 10px;">
                                <table id="grvBandeja">
                                </table>
                                <div id="grvBandeja_Pie">
                                </div>
                            </div>
                        </div>
                        <div id="divselhora" class="divselhora">
                        </div>
                        <div class="row" id="divSinProximosTurnos">
                            <div class="col-sm-4">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Fecha Inicio</label>
                                        <div class="col-sm-8">
                                            <input id="txtFecInicio" type="text" class="form-control" style="display: inline-block; width: 70%;"
                                                disabled="disabled" onchange="fn_ChangedFecha();" />
                                            <img alt="" id="previousDay_FecInicio" src="../Images/SRC/btn_atras.jpg" style="cursor: pointer;" />
                                            <img alt="" id="nextDay_FecInicio" src="../Images/SRC/btn_adelante.jpg" style="cursor: pointer;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Hora Inicio</label>
                                        <div class="col-sm-8">
                                            <select id="cboHoraInicioReserva" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Hora Final</label>
                                        <div class="col-sm-8">
                                            <select id="cboHoraFinalReserva" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <a href="#" onclick="return fn_ProximosTurnos();">Próximos Turnos >></a>
                            </div>
                        </div>
                        <div class="row" id="divProximosTurnos" style="display: none;">
                            <div class="col-sm-3">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Fecha Inicio</label>
                                        <div class="col-sm-8">
                                            <input id="txtFecInicio_PT" type="text" class="form-control" style="display: inline-block"
                                                disabled="disabled" onchange="fn_ChangedFecha_PT();" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Fecha Final</label>
                                        <div class="col-sm-8">
                                            <input id="txtFecFinal_PT" type="text" class="form-control" style="display: inline-block"
                                                disabled="disabled" onchange="fn_ChangedFecha_PT();" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Hora Inicio</label>
                                        <div class="col-sm-8">
                                            <select id="cboHoraInicioReserva_PT" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">
                                            Hora Final</label>
                                        <div class="col-sm-8">
                                            <select id="cboHoraFinalReserva_PT" class="form-control">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <a href="#" onclick="return fn_SinProximosTurnos()"><< Regresar</a>
                            </div>
                        </div>
                        <div class="row">
                            <div id="divGrvUbicacion" style="width: 100%; overflow: auto;">
                            </div>
                            <div style="width: 100%;">
                                <table id="grvReserva">
                                </table>
                                <div id="grvReserva_Pie">
                                </div>
                            </div>
                        </div>
                        <div class="row" id="div_Leyendas_Pie" style="padding-top: 15px;">
                            <div class="col-sm-2 col-xs-2">
                                <span style="font-weight: bold;">TCT: </span>
                                <label id="lblTCT">0</label>
                            </div>
                            <div class="col-sm-5 col-xs-6">
                                <img src="../Images/SRC/SI.png" alt="" />
                                Horario disponible para reservar <%= oPrm.SRC_Pais.Equals (1)? "cita":"hora" %>.
                            </div>
                            <div class="col-sm-5 col-xs-4">
                                <img src="../Images/SRC/NO.png" alt="" />
                                Horario reservado.
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <button id="btnRegistrar" type="button" class="btn btn-default" onclick="fn_RegistrarCita();">
                                    Registrar Cita</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="modal_BusquedaVehiculo" style="cursor: move" data-backdrop='static'
        data-keyboard='false' tabindex='-1'>
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="float: right;">
                        <a href="#">
                            <img id="btnBuscar_BusVeh" src="../Images/iconos/b-buscar.gif" alt="" title="Buscar"
                                onclick="fn_BuscarVehiculos();" style="cursor: pointer;" /></a> <a href="#">
                                    <img src="../Images/iconos/b-cerrar.gif" alt="" title="Cerrar" onclick="fn_Close_BuscarVehiculo();"
                                        style="cursor: pointer;" /></a>
                    </div>
                    <h4 class="modal-title">BÚSQUEDA DE VEHÍCULOS</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12 TituloLineaDiv">
                            Vehículo
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Placa</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtPlaca_BusVeh" class="form-control" maxlength="6" style="text-transform: uppercase;" />
                                        <input type="text" id="hf_ColorExterno" style="display: none" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Marca</label>
                                    <div class="col-sm-8">
                                        <select id="cboMarca_BusVeh" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Modelo</label>
                                    <div class="col-sm-8">
                                        <select id="cboModelo_BusVeh" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        VIN</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtVIN_BusVeh" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 TituloLineaDiv">
                            Buscar Por
                            <input type="radio" name="rblTipo_BusVeh" id="rbPropietario" value="1" checked="checked" />
                            <label for="rbPropietario">
                                Propietario</label>
                            <input type="radio" name="rblTipo_BusVeh" id="rbCliente" value="2" />
                            <label for="rbCliente">
                                Cliente</label>
                            <input type="radio" name="rblTipo_BusVeh" id="rbContacto" value="3" />
                            <label for="rbContacto">
                                Contacto</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Tipo Doc.</label>
                                    <div class="col-sm-8">
                                        <select id="cboTipoDocumento_BusVeh" class="form-control" onchange="javascript:return Fc_ValidarTipoDoc(this);">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Número Doc.</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtNroDocumento_BusVeh" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4" id="lblTipoPers_BusVeh">
                                        Nombres</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtNombres_BusVeh" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Ape. Paterno</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtApePaterno_BusVeh" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Ape. Materno</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtApeMaterno_BusVeh" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <table id="grvVehiculo_BusVeh">
                        </table>
                        <div id="grvVehiculo_BusVeh_Pie">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="modal_AgregarVehiculo" style="cursor: move" data-backdrop='static'
        data-keyboard='false' tabindex='-1'>
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="float: right;">
                        <a href="#">
                            <img src="../Images/iconos/b-guardar.gif" alt="" title="Grabar" onclick="fn_GrabarVehiculo();"
                                style="cursor: pointer;" /></a> <a href="#">
                                    <img src="../Images/iconos/b-cerrar.gif" alt="" title="Cerrar" onclick="fn_Close_AgregarVehiculo();"
                                        style="cursor: pointer;" /></a>
                    </div>
                    <h4 class="modal-title">REGISTRAR VEHÍCULO</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Placa</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtPlaca_RV" class="form-control" maxlength="6" style="text-transform: uppercase;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Marca</label>
                                    <div class="col-sm-8">
                                        <select id="cboMarca_RV" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        VIN</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtVIN_RV" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Modelo</label>
                                    <div class="col-sm-8">
                                        <select id="cboModelo_RV" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Km. Actual</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtKMActual_RV" class="form-control" maxlength="7" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="modal_ActualizarPropCli" style="cursor: move" data-backdrop='static'
        data-keyboard='false' tabindex='-1'>
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="float: right;">
                        <a href="#">
                            <img src="../Images/iconos/b-guardar.gif" alt="" title="Grabar" onclick="fn_Grabar_ActualizarPropCli();"
                                style="cursor: pointer;" /></a> <a href="#">
                                    <img src="../Images/iconos/b-cerrar.gif" alt="" title="Cerrar" onclick="fn_Close_ActualizarPropCli();"
                                        style="cursor: pointer;" /></a>
                    </div>
                    <h4 class="modal-title">ACTUALIZAR <span id="lblTipoActualizacion"></span>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Tipo Persona</label>
                                    <div class="col-sm-8">
                                        <select id="cboTipoPersona_PropCli" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Tipo Documento</label>
                                    <div class="col-sm-8">
                                        <select id="cboTipoDocumento_PropCli" class="form-control">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Nro. Documento</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txtNroDocumento_PropCli" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4" id="lblNombreRazon_PropCli">
                                        Nombres(s)</label>
                                    <div class="col-sm-8">
                                        <input id="txtNombresRazon_PropCli" type="text" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4" id="div_ApePaterno_PropCli">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Apellido Paterno</label>
                                    <div class="col-sm-8">
                                        <input id="txtApePaterno_PropCli" type="text" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4" id="div_ApeMaterno_PropCli">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Apellido Materno</label>
                                    <div class="col-sm-8">
                                        <input id="txtApeMaterno_PropCli" type="text" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Teléfono Fijo</label>
                                    <div id="dvPais_Fijo" class="col-sm-4" style="padding-right: 3px">
                                        <uc1:ComboPaisTelefono runat="server" ID="cboPaisTelefonoFijo_PropCli" />
                                    </div>
                                    <div id="dvTelefono_Fijo" class="col-sm-4">
                                        <input id="txtTelefonoFijo_PropCli" type="text" class="form-control" placeholder="999999999" maxlength="9" onkeypress="return SoloNumeros(event);" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4" id="dvAnexo_Fijo">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-4">
                                        <input type="text" id="txtTelefonoFijo_Anexo_PropCli" class="form-control"
                                            maxlength="10" placeholder="Anexo" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Teléfono Oficina</label>
                                    <div class="col-sm-8">
                                        <input id="txtTelefonoOficina_PropCli" type="text" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Teléfono Móvil 1</label>
                                    <div id="dvPais_Celular" class="col-sm-4" style="padding-right: 3px">
                                        <uc1:ComboPaisTelefono runat="server" ID="cboPaisTelefonoCel_PropCli" />
                                    </div>
                                    <div id="dvTelefono_Celular" class="col-sm-4">
                                        <input id="txtTelefonoMovil1_PropCli" type="text" placeholder="999999999" class="form-control" maxlength="9" onkeypress="return SoloNumeros(event);" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Teléfono Móvil 2</label>
                                    <div class="col-sm-8">
                                        <input id="txtTelefonoMovil2_PropCli" type="text" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Email Personal</label>
                                    <div class="col-sm-8">
                                        <input id="txtEmailPersonal_PropCli" type="text" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Email Trabajo</label>
                                    <div class="col-sm-8">
                                        <input id="txtEmailTrabajo_PropCli" type="text" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">
                                        Email Alternativo</label>
                                    <div class="col-sm-8">
                                        <input id="txtEmailAlternativo_PropCli" type="text" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="modal_HistorialServicio" style="cursor: move" data-backdrop='static'
        data-keyboard='false' tabindex='-1'>
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="float: right;">
                        <a href="#">
                            <img src="../Images/iconos/b-cerrar.gif" alt="" title="Cerrar" onclick="fn_Close_HistorialServicio();"
                                style="cursor: pointer;" /></a>
                    </div>
                    <h4 class="modal-title">HISTORIAL DE SERVICIOS</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Placa</label>
                                    <label id="lblPlaca_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label id="lblNomTipoDocumento_HS" class="control-label col-xs-4" style="font-weight: bold;">
                                    </label>
                                    <label id="lblNroDocumento_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        VIN</label>
                                    <label id="lblVIN_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label id="lblNombres_HS" class="control-label col-xs-4" style="font-weight: bold;">
                                    </label>
                                    <label id="lblNombre_RazonSocial_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Marca</label>
                                    <label id="lblMarca_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Teléfono</label>
                                    <label id="lblTelefono_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Modelo</label>
                                    <label id="lblModelo_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Celular</label>
                                    <label id="lblCelular_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Año</label>
                                    <label id="lblAnio_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Color</label>
                                    <label id="lblColor_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Email</label>
                                    <label id="lblEmail_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Num. Motor</label>
                                    <label id="lblNroMotor_HS" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <table id="grvHistorialServicio_HS">
                        </table>
                        <div id="grvHistorialServicio_HS_Pie">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="modal_RepuestosOT" style="cursor: move" data-backdrop='static'
        data-keyboard='false' tabindex='-1'>
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="float: right;">
                        <a href="#">
                            <img src="../Images/iconos/b-cerrar.gif" alt="" title="Cerrar" onclick="fn_Close_RepuestosOT();"
                                style="cursor: pointer;" /></a>
                    </div>
                    <h4 class="modal-title">REPUESTOS OT</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <table id="grvRepuestos_OT">
                        </table>
                        <div id="grvRepuestos_OT_Pie">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="modal_HistorialCita" style="cursor: move" data-backdrop='static'
        data-keyboard='false' tabindex='-1'>
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="float: right;">
                        <a href="#">
                            <img src="../Images/iconos/b-cerrar.gif" alt="" title="Cerrar" onclick="fn_Close_HistorialCita();"
                                style="cursor: pointer;" /></a>
                    </div>
                    <h4 class="modal-title">HISTORIAL DE CITAS</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Placa</label>
                                    <label id="lblPlaca_HC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label id="lblNomTipoDocumento_HC" class="control-label col-xs-4" style="font-weight: bold;">
                                    </label>
                                    <label id="lblNroDocumento_HC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        VIN</label>
                                    <label id="lblVIN_HC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label id="lblNombres_HC" class="control-label col-xs-4" style="font-weight: bold;">
                                    </label>
                                    <label id="lblNombre_RazonSocial_HC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Marca</label>
                                    <label id="lblMarca_HC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Teléfono</label>
                                    <label id="lblTelefono_HC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Modelo</label>
                                    <label id="lblModelo_HC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Celular</label>
                                    <label id="lblCelular_HC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Email</label>
                                    <label id="lblEmail_HC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <table id="grvHistorialCita_HC">
                        </table>
                        <div id="grvHistorialCita_HC_Pie">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="modal_Calculadora" style="cursor: move" data-backdrop='static'
        data-keyboard='false' tabindex='-1'>
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="float: right;">
                        <a href="#">
                            <img src="../Images/iconos/b-cerrar.gif" alt="" title="Cerrar" onclick="fn_Close_Calculadora();"
                                style="cursor: pointer;" /></a>
                    </div>
                    <h4 class="modal-title">CALCULADORA</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-12 TituloLineaDiv">
                                    Vehículo
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-4" style="font-weight: bold;">
                                                Placa</label>
                                            <label id="lblPlaca_CALCU" class="control-label col-xs-8">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-4" style="font-weight: bold;">
                                                VIN</label>
                                            <label id="lblVIN_CALCU" class="control-label col-xs-8">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-4" style="font-weight: bold;">
                                                Marca</label>
                                            <label id="lblMarca_CALCU" class="control-label col-xs-8">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-4" style="font-weight: bold;">
                                                Modelo</label>
                                            <label id="lblModelo_CALCU" class="control-label col-xs-8">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-12 TituloLineaDiv">
                                    Último Servicio
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-4" style="font-weight: bold;">
                                                OT</label>
                                            <label id="lblOT_CALCU" class="control-label col-xs-8">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-4" style="font-weight: bold;">
                                                KM</label>
                                            <label id="lblKmUltServicio_CALCU" class="control-label col-xs-8">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-4" style="font-weight: bold;">
                                                Fecha</label>
                                            <label id="lblFecUltServicio_CALCU" class="control-label col-xs-8">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-4" style="font-weight: bold;">
                                                Km Promedio</label>
                                            <label id="lblKmPromedio_CALCU" class="control-label col-xs-8">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="row">
                                <div class="col-sm-12 TituloLineaDiv">
                                    Próximo Servicio
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-4" style="font-weight: bold;">
                                                Fecha</label>
                                            <label id="lblFecProxServicio_CALCU" class="control-label col-xs-8">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="control-label col-xs-4" style="font-weight: bold;">
                                                Kilometraje</label>
                                            <label id="lblKmProxServicio_CALCU" class="control-label col-xs-8">
                                            </label>
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
    <div class="modal" id="modal_CitaPendiente" style="cursor: move" data-backdrop='static'
        data-keyboard='false' tabindex='-1'>
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="float: right;">
                        <a href="#">
                            <img src="../Images/iconos/b-cerrar.gif" alt="" title="Cerrar" onclick="fn_Close_CitaPendiente();"
                                style="cursor: pointer;" /></a>
                    </div>
                    <h4 class="modal-title">DATOS DE LA CITA</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12 TituloLineaDiv">
                            Esta
                            <%=oPrm.N_Placa %>
                            tiene una cita pendiente con los siguientes datos:
                        </div>
                    </div>
                    <div class="row" id="divCliente_CP">
                        <div class="col-xs-4">
                            <b>Cliente</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblNomCliente_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row" id="divTelefono_CP">
                        <div class="col-xs-4">
                            <b>Teléfono</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblTelfCli_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row" id="divPlaca_CP">
                        <div class="col-xs-4">
                            <b>
                                <%=oPrm.N_Placa %></b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblPlaca_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row" id="divMarca_CP">
                        <div class="col-xs-4">
                            <b>Marca</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblMarca_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row" id="divModelo_CP">
                        <div class="col-xs-4">
                            <b>Modelo</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblModelo_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row" id="divCodReserva_CP">
                        <div class="col-xs-4">
                            <b>Código de Reserva</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblCodReserva_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4">
                            <b>Fecha Cita</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblFechaCita_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row" id="divHoraCita_CP">
                        <div class="col-xs-4">
                            <b>Hora Cita</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblHoraCita_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row" id="divServicio_CP">
                        <div class="col-xs-4">
                            <b>Servicio</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblServicio_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4">
                            <b>Taller</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblTaller_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row" id="divDireccion_CP">
                        <div class="col-xs-4">
                            <b>Dirección</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblDireccion_CP">
                            </label>
                        </div>
                    </div>
                    <div class="row" id="divAsesor_CP">
                        <div class="col-xs-4">
                            <b>Asesor de Servicio</b>
                        </div>
                        <div class="col-xs-8">
                            <label id="lblAesor_CP">
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="modal_ResumenCita" style="cursor: move" data-backdrop='static'
        data-keyboard='false' tabindex='-1'>
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <div style="float: right;">
                        <a href="#">
                            <img id="btnImprimir" src="../Images/iconos/b-imprimir2.gif" alt="" title="Imprimir"
                                style="cursor: pointer;" /></a> <a href="#">
                                    <img src="../Images/iconos/b-cerrar.gif" alt="" title="Cerrar" onclick="fn_Close_ResumenCita();"
                                        style="cursor: pointer;" /></a>
                    </div>
                    <h4 class="modal-title">RESUMEN DE LA CITA</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12 TituloLineaDiv">
                            Cliente
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label id="lblCliente_RC" class="control-label col-sm-12" style="font-weight: bold;">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Teléfono Fijo</label>
                                    <label id="lblTelefono_RC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Teléfono Móvil</label>
                                    <label id="lblCelular_RC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div_EmailPersonal_RC">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2 col-xs-4" style="font-weight: bold;">
                                        Email Personal</label>
                                    <label id="lblEmailPersonal_RC" class="control-label col-sm-10 col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div_EmailTrabajo_RC">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2 col-xs-4" style="font-weight: bold;">
                                        Email Trabajo</label>
                                    <label id="lblEmailTrabajo_RC" class="control-label col-sm-10 col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div_EmailAlternativo_RC">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2 col-xs-4" style="font-weight: bold;">
                                        Email Alternativo</label>
                                    <label id="lblEmailAlternativo_RC" class="control-label col-sm-10 col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 TituloLineaDiv">
                            Vehículo
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        <%=oPrm.N_Placa %></label>
                                    <label id="lblPlaca_RC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Marca</label>
                                    <label id="lblMarca_RC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Modelo</label>
                                    <label id="lblModelo_RC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 TituloLineaDiv">
                            Cita
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2 col-xs-4" style="font-weight: bold;">
                                        Código de Reserva</label>
                                    <label id="lblCodCita_RC" class="control-label col-sm-10 col-xs-8" style="font-weight: bold; text-decoration: underline;">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2 col-xs-4" style="font-weight: bold;">
                                        Fecha</label>
                                    <label id="lblFecha_RC" class="control-label col-sm-10 col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2 col-xs-4" style="font-weight: bold;">
                                        Servicio</label>
                                    <label id="lblServicio_RC" class="control-label col-sm-10 col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2 col-xs-4" style="font-weight: bold;">
                                        Aservicio Servicio</label>
                                    <label id="lblAsesorServicio_RC" class="control-label col-sm-10 col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        <%=oPrm.N_Taller %></label>
                                    <label id="lblTaller_RC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-xs-4" style="font-weight: bold;">
                                        Teléfono</label>
                                    <label id="lblTelefonoTaller_RC" class="control-label col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2 col-xs-4" style="font-weight: bold;">
                                        Dirección</label>
                                    <label id="lblDireccionTaller_RC" class="control-label col-sm-10 col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 TituloLineaDiv">
                            Call Center
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-2 col-xs-4" style="font-weight: bold;">
                                        Teléfono</label>
                                    <label id="lblTelefonoCallCenter_RC" class="control-label col-sm-10 col-xs-8">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var no_pagina = window.location.pathname;
        var nid_usuario = "<%=this.Profile.Usuario.NID_USUARIO %>";
        var co_usuario = "<%=this.Profile.UserName.ToString() %>";
        var co_usuario_red = "<%=this.Profile.UsuarioRed.ToString() %>";
        var no_estacion_red = "<%=this.Profile.Estacion.ToString() %>";
        var oCita = {
            nid_cita: 0, co_reserva: "", nu_estado: "", nid_vehiculo: 0, nu_placa: "", nu_vin: "", nid_marca: 0, no_marca: "", nid_modelo: 0, nu_anio: 0, co_tipo_veh: "", co_modeloservicio_ax: "", fl_campania_veh: "0"
            , nid_servicio: 0, tx_observacion: "", nid_taller: 0, nid_asesor: 0, fe_programada: "", ho_inicio: "", qt_intervalo_atenc: ""
            , nid_cliente: 0, co_tipo_documento: "", nu_documento: "", no_cliente: "", ape_paterno: "", ape_materno: "", no_correo_personal: ""
            , no_correo_trabajo: "", no_correo_alternativo: "", nu_telefono: "", nu_telefono_oficina: "", nu_celular: "", nu_celular_alter: ""
            , nid_record_cita: 0, co_tipo_record: "", dd_record: 0, ho_record_ini: "", ho_record_fin: ""
            , qt_intervalo_taller: 0
            , no_color: ""
            , campanias: ""
            , recojounidad: "0"
        };
        var placa_defecto = false;
        var filtros_selected_contact = "";
        var id_contactcenter = 0;
        $("#dvCampaniasCitas").hide();
        var listaCampanias = new Array();
        var loadCampanias = 0;
        var oPropietario = null;
        var oCliente = null;

        var autocomplete_OpenVehiculo = false;
        var autocomplete_itemSelectedVehiculo = false;

        var SRC_CodPais = "<%=oPrm.SRC_CodPais %>";
        var text_Todos = "<%=ConstanteBE.OBJECTO_TODOS %>";
        var text_Todos = "<%=ConstanteBE.OBJECTO_TODOS %>";
        var text_Seleccione = "<%=ConstanteBE.OBJECTO_SELECCIONE %>";
        var oJuridica = '<%=ConfigurationManager.AppSettings["PersonaJuridica"].ToString() %>';
        var oNatural = '<%=ConfigurationManager.AppSettings["PersonaNatural"].ToString() %>';
        var oDNI = '<%=ConfigurationManager.AppSettings["TIPODOCDNI"].ToString() %>';
        var oRUC = '<%=ConfigurationManager.AppSettings["TIPODOCRUC"].ToString() %>';

        var oComboProvincia = [];
        var oComboDistrito = [];
        var oComboTaller = [];
        var fl_ubigeo_all = "1";
        var PARM_13 = "<%=oPrm.GetValor(Parametros.PARM._13)%>";
        var strDivSinHorario = "<div id='divSinHorario' style='background-color: #FFF; font-weight: bold; padding-top: 25px; padding-bottom:25px; text-align: center; font-size: 18px;'></div>";

        var imgURL_Hora_Separada = "<%=imgURL_HORA_SEPARADA %>";
        var imgURL_Hora_Libre = "<%=imgURL_HORA_LIBRE %>";
        var imgURL_Hora_Reservada = "<%=imgURL_HORA_RESERVADA %>";

        $(document).ready(function () {
            fn_CargaInicial();
        });

        fc_UserControlComboPaisTelefono("S", "0", false, 'cboPaisTelefonoFijo_PropCli');
        $('#cboPaisTelefonoFijo_PropCli').val("162");
        fc_UserControlComboPaisTelefono("S", "0", false, 'cboPaisTelefonoCel_PropCli');
        $('#cboPaisTelefonoCel_PropCli').val("162");

        function fn_CargaInicial() {
            $("#txtPlaca").keydown(function (event) {
                if (event.keyCode == 13) {
                    var value_placa = $("#txtPlaca").val();
                    $("#txtPlaca").blur();
                    if (oCita.nu_placa.toUpperCase() == value_placa.toUpperCase()) {
                        fn_GetVehiculo(value_placa);
                    }
                }
            });
            /*#region - Bloqueando controles*/
            fn_BloquerDatosContacto(true);
            /*#endregion - Bloqueando controles*/
            fc_FormatNumeros("txtKMActual_RV");

            /*#region - Format controles Detalle Contacto*/
            fc_FormatNumeros("txtNroDocumento");
            $("#txtNombres").attr("onkeypress", "return SoloLetrasEspacio(event)");
            $("#txtApePaterno").attr("onkeypress", "return SoloLetrasEspacio(event)");
            $("#txtApeMaterno").attr("onkeypress", "return SoloLetrasEspacio(event)");
            fc_FormatNumeros("txtTelefonoMovil1");
            fc_FormatNumeros("txtTelefonoMovil2");
            /*#endregion - Format controles Detalle Contacto*/

            /*#region - Format controles Modal Busqueda Vehiculo*/
            $("#txtPlaca_BusVeh").attr("onkeypress", "return SoloPlaca(event)");
            $("#txtVIN_BusVeh").attr("onkeypress", "return SoloLetrasNumeros(event)");
            $("#txtApePaterno_BusVeh").attr("onkeypress", "return SoloLetrasEspacio(event)");
            $("#txtApeMaterno_BusVeh").attr("onkeypress", "return SoloLetrasEspacio(event)");
            $("#txtNombres_BusVeh").attr("onkeypress", "return SoloLetrasEspacio(event)");
            /*#endregion - Format controles Modal Busqueda Vehiculo*/

            /*#region - Format controles Modal Registrar Vehiculo*/
            $("#txtPlaca_RV").attr("onkeypress", "return SoloPlaca(event)");
            /*#endregion - Format controles Modal Registrar Vehiculo*/

            /*#region - Format controles Modal Actualizar Propietario/Cliente*/
            fc_FormatNumeros("txtTelefonoMovil1_PropCli");
            fc_FormatNumeros("txtTelefonoMovil2_PropCli");
            /*#endregion - Format controles Modal Actualizar Propietario/Cliente*/

            //#region - Carga controles
            var strParametros = "{ nid_usuario: " + nid_usuario + "}";
            var strUrlServicio = no_pagina + "/Get_Inicial";
            this.fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                this.fc_FillCombo("cboMarca_RV", objResponse.oComboMarca, this.text_Seleccione);
                this.fc_FillCombo("cboModelo_RV", [], this.text_Seleccione);
                this.fc_FillCombo("cboMarca_BusVeh", objResponse.oComboMarca, this.text_Seleccione);
                this.fc_FillCombo("cboModelo_BusVeh", [], this.text_Seleccione);
                this.fc_FillCombo("cboTipoPersona_PropCli", objResponse.oComboTipoPersona, this.text_Seleccione);
                this.fc_FillCombo("cboTipoDocumento_BusVeh", objResponse.oComboTipoDocumento, this.text_Seleccione);
                this.fc_FillCombo("cboTipoDocumento", objResponse.oComboTipoDocumento, this.text_Seleccione);
                $("#cboTipoDocumento option[value='" + oRUC + "']").remove();
                this.fc_FillCombo("cboDiaContacto", objResponse.oDias, "---");
                this.fc_FillCombo("cboHIContacto", objResponse.oHorasIni, "---");
                this.fc_FillCombo("cboHFContacto", objResponse.oHorasFin, "---");
                if (objResponse.PARM_14 == "1") $("#divDatosRecord").show();
                else $("#divDatosRecord").hide();

                this.fc_FillCombo("cboTipoServicio", objResponse.oComboTipoServicio, this.text_Seleccione);
                $("#cboTipoServicio").trigger("change");

                if (objResponse.placa_def != "") {
                    fc_BuscarVehiculoDefecto(objResponse.placa_def);
                }

                id_contactcenter = objResponse.contact_center;
            });
            //#endregion - Carga controles

            //#region - Modal Busqueda Vehiculos - Controles
            $("#txtPlaca_BusVeh").keydown(function (event) { fc_PressKey(13, "btnBuscar_BusVeh"); });
            $("#cboMarca_BusVeh").keydown(function (event) { fc_PressKey(13, "btnBuscar_BusVeh"); });
            $("#cboModelo_BusVeh").keydown(function (event) { fc_PressKey(13, "btnBuscar_BusVeh"); });
            $("#txtVIN_BusVeh").keydown(function (event) { fc_PressKey(13, "btnBuscar_BusVeh"); });
            $("#cboTipoDocumento_BusVeh").keydown(function (event) { fc_PressKey(13, "btnBuscar_BusVeh"); });
            $("#txtNroDocumento_BusVeh").keydown(function (event) { fc_PressKey(13, "btnBuscar_BusVeh"); });
            $("#txtNombres_BusVeh").keydown(function (event) { fc_PressKey(13, "btnBuscar_BusVeh"); });
            $("#txtApePaterno_BusVeh").keydown(function (event) { fc_PressKey(13, "btnBuscar_BusVeh"); });
            $("#txtApeMaterno_BusVeh").keydown(function (event) { fc_PressKey(13, "btnBuscar_BusVeh"); });
            //#endregion - Modal Busqueda Vehiculos - Controles
        }
        $("#txtPlaca").blur(function () {
            var value_placa = $(this).val();
            if (oCita.nu_placa.toUpperCase() != value_placa.toUpperCase()) {
                fn_GetVehiculo(value_placa);
            }
        });
        $("#txtPlaca").autocomplete({
            source: function (request, response) {
                $("#txtPlaca").val(request.term);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: no_pagina + "/Get_Placas",
                    dataType: "json",
                    data: "{'Value':'" + request.term + "'}",
                    success: function (data) {
                        response($.map(JSON.parse(data.d), function (item) {
                            return {
                                label: item.Label,
                                value: item.Value
                            }
                        }));
                    },
                    complete: function () { },
                    error: function (request, textStatus, error) {
                        if (request.status == 401) {
                            alert('Acceso no Autorizado: ' + error);
                            location.reload();
                        }
                        else {
                            var indexIni = request.responseText.indexOf('<title>') + 7
                            var indexFin = request.responseText.indexOf('</title>') - indexIni;
                            var err = (request.responseText.substr(indexIni, indexFin));
                            if (err == '') {
                                try {
                                    err = jQuery.parseJSON(request.responseText).Message;
                                } catch (ex) { }
                            }
                            alert("Error: (" + request.status + "): " + err);
                        }
                    }
                });
            },
            minLength: 3,
            select: function (event, ui) {
                autocomplete_itemSelectedVehiculo = false;
                fn_GetVehiculo(ui.item.value);
            },
            open: function () {
                autocomplete_OpenVehiculo = true;
            },
            close: function () {
                autocomplete_OpenVehiculo = false;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Error: " + textStatus + " - " + errorThrown);
            }
        });

        function fn_GetVehiculo(value_placa) {
            if (!autocomplete_itemSelectedVehiculo) {
                oCita.nid_vehiculo = 0; //Se reinicia
                oCita.nu_placa = ""; //Se reinicia
                var nu_placa = fc_Trim(value_placa);
                var msg_retorno = "";
                if (nu_placa == "") { msg_retorno = "- Debe ingresar número de <%=oPrm.N_Placa %>"; }

                if (msg_retorno != "") {
                    fc_Alert(msg_retorno);
                }
                else {
                    fn_LimpiarDatosVehiculo();
                    fn_LimpiarDatosContacto();
                    fn_BloquerDatosContacto(true);
                    this.fc_FillCombo("cboTipoServicio", [], text_Seleccione);
                    fn_LimpiarDetalleServicio();

                    var parametros = new Array();
                    parametros[0] = nu_placa;
                    parametros[1] = nid_usuario;
                    var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                    var strUrlServicio = no_pagina + "/Get_Vehiculo";
                    this.fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                        //this.fn_Limpiar_Paso1();
                        if (objResponse.msg_retorno != "") { fc_Alert(objResponse.msg_retorno); }

                        if (objResponse.oVehiculo != null) {
                            $("#txtMarca").val(objResponse.oVehiculo.no_marca);
                            $("#txtModelo").val(objResponse.oVehiculo.no_modelo);
                            $("#txtVIN").val(objResponse.oVehiculo.nu_vin);
                            $("#txtPropietario").val(objResponse.oVehiculo.no_propietario);
                            $("#txtCliente").val(objResponse.oVehiculo.no_cliente);
                            $("#hf_ColorExterno").val(objResponse.oVehiculo.no_color);
                            //Set datos de reserva
                            oCita.nid_vehiculo = objResponse.oVehiculo.nid_vehiculo;
                            oCita.nu_placa = objResponse.oVehiculo.nu_placa;
                            oCita.nu_vin = objResponse.oVehiculo.nu_vin;
                            oCita.nid_marca = objResponse.oVehiculo.nid_marca;
                            oCita.no_marca = objResponse.oVehiculo.no_marca;
                            oCita.nid_modelo = objResponse.oVehiculo.nid_modelo;
                            oCita.no_modelo = objResponse.oVehiculo.no_modelo;
                        }
                        this.oPropietario = objResponse.oPropietario;
                        this.oCliente = objResponse.oCliente;
                        if (objResponse.fl_seguir == "1") {
                            fn_BloquerDatosContacto(false);
                        }

                        //Set datos de reserva
                        oCita.nid_cliente = (oCliente == null ? 0 : objResponse.oContacto.nid_cliente);

                        if (objResponse.oContacto != null) {
                            $("#cboTipoDocumento").val(objResponse.oContacto.co_tipo_documento);
                            $("#txtNroDocumento").val(objResponse.oContacto.nu_documento);
                            $("#txtNombres").val(objResponse.oContacto.no_nombre);
                            $("#txtApePaterno").val(objResponse.oContacto.no_ape_paterno);
                            $("#txtApeMaterno").val(objResponse.oContacto.no_ape_materno);
                            $("#txtTelefonoFijo").val(objResponse.oContacto.nu_telefono);
                            $("#txtTelefonoOficina").val(objResponse.oContacto.nu_tel_oficina);
                            $("#txtTelefonoMovil1").val(objResponse.oContacto.nu_telmovil1);
                            $("#txtTelefonoMovil2").val(objResponse.oContacto.nu_telmovil2);
                            $("#txtEmailPersonal").val(objResponse.oContacto.no_correo);
                            $("#txtEmailTrabajo").val(objResponse.oContacto.no_correo_trabajo);
                            $("#txtEmailAlternativo").val(objResponse.oContacto.no_correo_alter);
                            var fl_identidad_validada = (objResponse.oContacto.fl_identidad_validada == "1" ? true : false);
                            $("#txtNombres").prop("disabled", fl_identidad_validada);
                            $("#txtApePaterno").prop("disabled", fl_identidad_validada);
                            $("#txtApeMaterno").prop("disabled", fl_identidad_validada);
                            if (fl_identidad_validada) $("#div_NotaIdentidadValidada").show();
                            else $("#div_NotaIdentidadValidada").hide();
                        }

                        if (objResponse.oRecordatorio != null) {
                            $("#chkSMSCli").prop("checked", objResponse.oRecordatorio.fl_record_sms);
                            if (fn_ExistDisplayControl("divDatosRecord")) {
                                $("#cboDiaContacto").val(objResponse.oRecordatorio.dd_record);
                                $("#cboHIContacto").val(objResponse.oRecordatorio.ho_record_ini);
                                $("#cboHFContacto").val(objResponse.oRecordatorio.ho_record_fin);
                            }
                        }
                        if (objResponse.oCitaPendiente != null) {
                            if (SRC_CodPais == "2") {
                                $("#divCliente_CP").show();
                                $("#divTelefono_CP").show();
                                $("#divPlaca_CP").show();
                                $("#divMarca_CP").show();
                                $("#divModelo_CP").show();
                                $("#divCodReserva_CP").show();
                                $("#divHoraCita_CP").show();
                                $("#divServicio_CP").show();
                                $("#divDireccion_CP").show();
                                $("#divAsesor_CP").show();
                            }
                            else {
                                $("#divCliente_CP").hide();
                                $("#divTelefono_CP").hide();
                                $("#divPlaca_CP").hide();
                                $("#divMarca_CP").hide();
                                $("#divModelo_CP").hide();
                                $("#divCodReserva_CP").hide();
                                $("#divHoraCita_CP").hide();
                                $("#divServicio_CP").hide();
                                $("#divDireccion_CP").hide();
                                $("#divAsesor_CP").hide();
                            }

                            fn_Open_CitaPendiente();
                            $("#lblNomCliente_CP").text(objResponse.oCitaPendiente.no_cliente);
                            $("#lblTelfCli_CP").text(objResponse.oCitaPendiente.nu_tel_fijo);
                            $("#lblPlaca_CP").text(objResponse.oCitaPendiente.nu_placa);
                            $("#lblMarca_CP").text(objResponse.oCitaPendiente.no_marca);
                            $("#lblModelo_CP").text(objResponse.oCitaPendiente.no_modelo);
                            $("#lblCodReserva_CP").text(objResponse.oCitaPendiente.cod_reserva_cita);
                            $("#lblFechaCita_CP").text(objResponse.oCitaPendiente.fecha_prog);
                            $("#lblHoraCita_CP").text(objResponse.oCitaPendiente.ho_inicio);
                            $("#lblServicio_CP").text(objResponse.oCitaPendiente.no_servicio);
                            $("#lblTaller_CP").text(objResponse.oCitaPendiente.no_taller);
                            $("#lblDireccion_CP").text(objResponse.oCitaPendiente.no_direccion);
                            $("#lblAesor_CP").text(objResponse.oCitaPendiente.no_asesor);
                        }

                        if (objResponse.fl_seguir == "1") {
                            if (objResponse.oVehiculo != null) {
                                this.fc_FillCombo("cboTipoServicio", objResponse.oComboTipoServicio, text_Seleccione);
                            }
                        }
                    });
                }
            }
        }

        function fn_LimpiarDatosVehiculo() {
            $("#txtMarca").val("");
            $("#txtModelo").val("");
            $("#txtVIN").val("");
            $("#txtPropietario").val("");
            $("#txtCliente").val("");
        }
        function fn_LimpiarDatosContacto() {
            //Datos de Contacto
            $("#cboTipoDocumento").val("");
            $("#txtNroDocumento").val("");
            $("#txtNombres").val("");
            $("#txtApePaterno").val("");
            $("#txtApeMaterno").val("");
            $("#txtTelefonoFijo").val("");
            $("#txtTelefonoOficina").val("");
            $("#txtTelefonoMovil1").val("");
            $("#txtTelefonoMovil2").val("");
            $("#txtEmailPersonal").val("");
            $("#txtEmailTrabajo").val("");
            $("#txtEmailAlternativo").val("");
            $("#txtObservaciones").val("");
            //Recordatorio
            //////$("#chkSMSCli").val("");
            $("#cboDiaContacto").val("");
            $("#cboHIContacto").val("");
            $("#cboHFContacto").val("");
        }
        function fn_BloquerDatosContacto(fl_bloqueo) {
            //Datos Contacto
            if (fl_bloqueo) { $("#btnBuscarDocumento").hide(); }
            else { $("#btnBuscarDocumento").show(); }
            $("#cboTipoDocumento").prop("disabled", fl_bloqueo);
            $("#txtNroDocumento").prop("disabled", fl_bloqueo);
            $("#txtNombres").prop("disabled", fl_bloqueo);
            $("#txtApePaterno").prop("disabled", fl_bloqueo);
            $("#txtApeMaterno").prop("disabled", fl_bloqueo);
            $("#txtTelefonoFijo").prop("disabled", fl_bloqueo);
            $("#txtTelefonoOficina").prop("disabled", fl_bloqueo);
            $("#txtTelefonoMovil1").prop("disabled", fl_bloqueo);
            $("#txtTelefonoMovil2").prop("disabled", fl_bloqueo);
            $("#txtEmailPersonal").prop("disabled", fl_bloqueo);
            $("#txtEmailTrabajo").prop("disabled", fl_bloqueo);
            $("#txtEmailAlternativo").prop("disabled", fl_bloqueo);
            $("#txtObservaciones").prop("disabled", fl_bloqueo);
            //Recordatorio
            $("#chkSMSCli").prop("disabled", fl_bloqueo);
            $("#cboDiaContacto").prop("disabled", fl_bloqueo);
            $("#cboHIContacto").prop("disabled", fl_bloqueo);
            $("#cboHFContacto").prop("disabled", fl_bloqueo);
        }

        $("#cboTipoDocumento").change(function () {
            var co_tipo_documento = $(this).val();
            fn_LimpiarDatosContacto();
            $("#cboTipoDocumento").val(co_tipo_documento);
        });
        $("#txtNroDocumento").blur(function () {
            fn_GetCliente();
        });
        function fn_GetCliente() {
            var co_tipo_documento = $("#cboTipoDocumento").val();
            var nu_documento = fc_Trim($("#txtNroDocumento").val());
            fn_LimpiarDatosContacto();
            $("#cboTipoDocumento").val(co_tipo_documento);
            $("#txtNroDocumento").val(nu_documento);

            if (fc_Trim(document.getElementById("txtPlaca").value) == "" || oCita.nid_vehiculo <= 0) {
                fc_Alert("Debe de ingresar un número de <%=oPrm.N_Placa %>");
            }
            else if (co_tipo_documento == "") {
                fc_Alert("Debe seleccionar tipo documento.");
            }
            else if (nu_documento == "") {
                fc_Alert("Debe ingresar número de documento.");
            }
            else {
                var parametros = new Array();
                parametros[0] = co_tipo_documento;
                parametros[1] = nu_documento;
                var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                var strUrlServicio = no_pagina + "/Get_Cliente";
                fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                    var oCliente = objResponse.oCliente;
                    if (oCliente == null || oCliente == "") {
                        oCliente == null;
                        fc_Alert("El número de documento no existe.");
                    }
                    //Set datos de reserva
                    oCita.nid_cliente = (oCliente == null ? 0 : oCliente.nid_cliente)

                    $("#txtNroDocumento").val(oCliente == null ? nu_documento : oCliente.nu_documento);
                    $("#txtNombres").val(oCliente == null ? "" : oCliente.no_nombre_razon);
                    $("#txtApePaterno").val(oCliente == null ? "" : oCliente.no_ape_paterno);
                    $("#txtApeMaterno").val(oCliente == null ? "" : oCliente.no_ape_materno);
                    $("#txtTelefonoFijo").val(oCliente == null ? "" : oCliente.nu_telefono);
                    $("#txtTelefonoOficina").val(oCliente == null ? "" : oCliente.nu_tel_oficina);
                    $("#txtTelefonoMovil1").val(oCliente == null ? "" : oCliente.nu_telmovil1);
                    $("#txtTelefonoMovil2").val(oCliente == null ? "" : oCliente.nu_telmovil2);
                    $("#txtEmailPersonal").val(oCliente == null ? "" : oCliente.no_correo);
                    $("#txtEmailTrabajo").val(oCliente == null ? "" : oCliente.no_correo_trabajo);
                    $("#txtEmailAlternativo").val(oCliente == null ? "" : oCliente.no_correo_alter);
                    var fl_identidad_validada = (oCliente == null ? false : (oCliente.fl_identidad_validada == "1" ? true : false));
                    $("#txtNombres").prop("disabled", fl_identidad_validada);
                    $("#txtApePaterno").prop("disabled", fl_identidad_validada);
                    $("#txtApeMaterno").prop("disabled", fl_identidad_validada);
                    if (fl_identidad_validada) $("#div_NotaIdentidadValidada").show();
                    else $("#div_NotaIdentidadValidada").hide();

                    if (objResponse.oRecordatorio != null && objResponse.oRecordatorio != "") {
                        $("#chkSMSCli").prop("checked", objResponse.oRecordatorio.fl_record_sms);
                        $("#cboDiaContacto").val(objResponse.oRecordatorio.dd_record);
                        $("#cboHIContacto").val(objResponse.oRecordatorio.ho_record_ini);
                        $("#cboHFContacto").val(objResponse.oRecordatorio.ho_record_fin);
                    }
                });
            }
        }

        function fn_LimpiarDetalleServicio() {
            $("#cboTipoServicio").val("").trigger("change");
        }
        $("#cboTipoServicio").change(function () {
            var nid_tipo_servicio = $(this).val();
            if (nid_tipo_servicio > 0) {
                var parametros = new Array();
                parametros[0] = nid_tipo_servicio;
                parametros[1] = nid_usuario;
                parametros[2] = oCita.nid_modelo;
                var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                var strUrlServicio = no_pagina + "/Get_Servicios";
                fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                    if (objResponse.msg_retorno != "") { fc_Alert(objResponse.msg_retorno); }
                    fc_FillCombo("cboServicioEspecifico", objResponse.oComboServicios, text_Seleccione);

                    if (objResponse.oComboServicios.length == 1) {
                        $("#cboServicioEspecifico").val(objResponse.oComboServicios[0].value);
                    }
                    $("#cboServicioEspecifico").trigger("change");
                });
            }
            else {
                fc_FillCombo("cboServicioEspecifico", [], text_Seleccione);
                $("#cboServicioEspecifico").trigger("change");
            }
        });

        $("#cboServicioEspecifico").change(function () {
            var nid_servicio = $(this).val();
            //Set datos de reserva
            oCita.nid_servicio = nid_servicio;

            oComboProvincia = [];
            oComboDistrito = [];
            if (nid_servicio > 0) {
                var parametros = new Array();
                parametros[0] = nid_servicio;
                parametros[1] = oCita.nid_marca;
                parametros[2] = oCita.nid_modelo;
                parametros[3] = nid_usuario;
                parametros[4] = oCita.nu_placa;
                parametros[5] = id_contactcenter;
                parametros[6] = loadCampanias;
                var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                var strUrlServicio = no_pagina + "/Get_UbigeoDisponible";
                fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                    if (objResponse.msg_retorno != "") { fc_Alert(objResponse.msg_retorno); }
                    var text_default = text_Todos;
                    if (PARM_13 == "1") text_default = text_Seleccione;
                    if (objResponse.fl_seguir == "1") {

                        this.fc_FillCombo("cboDepartamento", objResponse.oComboDepartamento, text_default);
                        if (objResponse.oComboDepartamento.length == 1) {
                            $("#cboDepartamento").val(objResponse.oComboDepartamento[0].value);
                        }
                        oComboProvincia = objResponse.oComboProvincia;
                        oComboDistrito = objResponse.oComboDistrito;
                        $("#cboDepartamento").trigger("change");
                    }
                    else {
                        oComboProvincia = [];
                        oComboDistrito = [];
                        this.fc_FillCombo("cboDepartamento", [], text_default);
                        $("#cboDepartamento").trigger("change");
                    }

                });
            }
            else {
                fc_FillCombo("cboDepartamento", [], text_Seleccione);
                $("#cboDepartamento").trigger("change");
            }
        });
        function fn_GetValue_Prov() {
            var codprov_value = $("#cboProvincia").val();
            var codprov = codprov_value.length > 2 ? codprov_value.substr(2, 2) : codprov_value;
            return codprov;
        }
        function fn_GetValue_Dist() {
            var coddist_value = $("#cboDistrito").val();
            var coddist = coddist_value.length > 2 ? coddist_value.substr(4, 2) : coddist_value;
            return coddist;
        }
        function fn_GetValue_Ubicacion() {
            var nid_ubica_value = $("#cboUbicacion").val();
            var nid_ubica = nid_ubica_value.length > 6 ? nid_ubica_value.substr(6) : nid_ubica_value;
            return nid_ubica;
        }
        function fn_GetValue_Taller() {
            var nid_taller_value = $("#cboTaller").val();
            var arr_ubi_taller_aux = nid_taller_value.split("$");
            var nid_taller;
            if (arr_ubi_taller_aux.length > 1) { nid_taller = arr_ubi_taller_aux[1]; }
            else { nid_taller = nid_taller_value; }
            return nid_taller;
        }
        var PRM_SELECTED;
        $("#cboDepartamento").change(function (event, co_prov_sel, co_dist_sel, nid_ubica_sel, nid_taller_sel, fl_ver_horario) {
            var coddpto = $(this).val();
            if (coddpto != "") {
                var objProvincias = $.grep(oComboProvincia, function (e) { return e.coddpto == coddpto; });
                var text_default = text_Todos;
                if (PARM_13 == "2") text_default = text_Seleccione;
                fc_FillCombo("cboProvincia", objProvincias, text_default);
                if (co_prov_sel != undefined && co_prov_sel != "") { $("#cboProvincia").val(co_prov_sel); }
                if (objProvincias.length == 1) {
                    $("#cboProvincia").val(objProvincias[0].value);
                }
                $("#cboProvincia").trigger("change", [co_dist_sel, nid_ubica_sel, nid_taller_sel, fl_ver_horario]);
            }
            else if (fl_ubigeo_all == "1") {
                fc_FillCombo("cboProvincia", oComboProvincia, text_Todos);
                if (co_prov_sel != undefined && co_prov_sel != "") { $("#cboProvincia").val(co_prov_sel); }
                $("#cboProvincia").trigger("change", [co_dist_sel, nid_ubica_sel, nid_taller_sel, fl_ver_horario]);
            }
            else {
                fc_FillCombo("cboProvincia", [], text_Seleccione);
                $("#cboProvincia").trigger("change");
            }
        });
        $("#cboProvincia").change(function (event, co_dist_sel, nid_ubica_sel, nid_taller_sel, fl_ver_horario) {
            var codprov_value = $(this).val();
            var codprov = codprov_value.length > 2 ? codprov_value.substr(2, 2) : codprov_value;
            var coddpto = $("#cboDepartamento").val();
            if (coddpto == "" && codprov_value.length > 2) {
                $("#cboDepartamento").val(codprov_value.substr(0, 2));
                $("#cboDepartamento").trigger("change", [codprov_value, undefined, undefined, undefined, undefined]);
                return;
            }
            else {
                if (coddpto != "") {
                    var objDistritos;
                    if (codprov != "") objDistritos = $.grep(oComboDistrito, function (e) { return (e.coddpto == coddpto && e.codprov == codprov); });
                    else objDistritos = $.grep(oComboDistrito, function (e) { return (e.coddpto == coddpto); });

                    var text_default = text_Todos;
                    if (PARM_13 == "3") text_default = text_Seleccione;
                    fc_FillCombo("cboDistrito", objDistritos, text_default);
                    if (co_dist_sel != undefined && co_dist_sel != "") { $("#cboDistrito").val(co_dist_sel); }
                    if (objDistritos.length == 1) {
                        $("#cboDistrito").val(objDistritos[0].value);
                    }
                    $("#cboDistrito").trigger("change", [nid_ubica_sel, nid_taller_sel, fl_ver_horario]);
                }
                else if (fl_ubigeo_all == "1") {
                    fc_FillCombo("cboDistrito", oComboDistrito, text_Todos);
                    if (co_dist_sel != undefined && co_dist_sel != "") { $("#cboDistrito").val(co_dist_sel); }
                    $("#cboDistrito").trigger("change", [nid_ubica_sel, nid_taller_sel, fl_ver_horario]);
                }
                else {
                    fc_FillCombo("cboDistrito", [], text_Seleccione);
                    $("#cboDistrito").trigger("change");
                }
            }
        });
        $("#cboDistrito").change(function (event, nid_ubica_sel, nid_taller_sel, fl_ver_horario) {
            var coddist_value = $(this).val();
            var coddist = coddist_value.length > 2 ? coddist_value.substr(4, 2) : coddist_value;

            var coddpto = $("#cboDepartamento").val();
            var codprov_value = $("#cboProvincia").val();
            var codprov = codprov_value.length > 2 ? codprov_value.substr(2, 2) : codprov_value;

            if ((coddpto == "" || codprov_value == "") && coddist_value.length > 2) {
                $("#cboDepartamento").val(coddist_value.substr(0, 2));
                if (codprov_value == "") { codprov_value = coddist_value.substr(0, 4); }
                $("#cboDepartamento").trigger("change", [codprov_value, coddist_value, undefined, undefined, undefined]);
                return;
            }
            else {
                var qt_dptos = $("#cboDepartamento option").length;
                var fl_exist_dpto = "1";
                if (qt_dptos == 1 && coddpto == "") fl_exist_dpto = "0";
                if (oCita.nid_servicio > 0 && fl_exist_dpto == "1" && (coddist != "" || fl_ubigeo_all == "1")) {
                    var parametros = new Array();
                    parametros[0] = oCita.nid_modelo;
                    parametros[1] = oCita.nid_servicio;
                    parametros[2] = coddpto;
                    parametros[3] = codprov;
                    parametros[4] = coddist;
                    parametros[5] = nid_usuario;
                    var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                    var strUrlServicio = no_pagina + "/Get_UbicacionDisponible";
                    fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                        if (objResponse.msg_retorno != "") { fc_Alert(objResponse.msg_retorno); }

                        var text_default = text_Todos;
                        if (PARM_13 == "4") text_default = text_Seleccione;
                        this.fc_FillCombo("cboUbicacion", objResponse.oComboUbicacion, text_default);
                        if (nid_ubica_sel != undefined && nid_ubica_sel != "") { $("#cboUbicacion").val(nid_ubica_sel); }
                        if (objResponse.oComboUbicacion.length == 1) {
                            $("#cboUbicacion").val(objResponse.oComboUbicacion[0].value);
                        }

                        this.oComboTaller = objResponse.oComboTaller;
                        this.fc_FillCombo("cboTaller", this.oComboTaller, text_default);

                        $("#cboUbicacion").trigger("change", [nid_taller_sel, fl_ver_horario]);
                    });
                }
                else {
                    fc_FillCombo("cboUbicacion", [], text_Seleccione);
                    oComboTaller = [];
                    $("#cboUbicacion").trigger("change");
                }
            }
        });
        var IntervaloT;
        $("#cboUbicacion").change(function (event, nid_taller_sel, fl_ver_horario) {
            var nid_ubica_value = $(this).val();
            var nid_ubica = nid_ubica_value.length > 6 ? nid_ubica_value.substr(6) : nid_ubica_value;

            var coddpto = $("#cboDepartamento").val();
            var codprov_value = $("#cboProvincia").val();
            var codprov = codprov_value.length > 2 ? codprov_value.substr(2, 2) : codprov_value;
            var coddist_value = $("#cboDistrito").val();
            var coddist = coddist_value.length > 2 ? coddist_value.substr(4, 2) : coddist_value;

            if ((coddpto == "" || codprov_value == "" || coddist_value == "") && nid_ubica_value.length > 6) {
                $("#cboDepartamento").val(nid_ubica_value.substr(0, 2));
                if (codprov_value == "") { codprov_value = nid_ubica_value.substr(0, 4); }
                if (coddist_value == "") { coddist_value = nid_ubica_value.substr(0, 6); }
                $("#cboDepartamento").trigger("change", [codprov_value, coddist_value, nid_ubica_value, undefined, undefined]);
                return;
            }
            else {
                $("#btnMapaTallerPR").hide();
                $("#cboTaller").prop("disabled", false);
                if (coddpto != "") {
                    var objTalleres;
                    if (nid_ubica != "") objTalleres = $.grep(oComboTaller, function (e) { return (e.nid_ubica == nid_ubica); });
                    else if (coddist != "") objTalleres = $.grep(oComboTaller, function (e) { return (e.coddpto == coddpto && e.codprov == codprov && e.coddist == coddist); });
                    else if (codprov != "") objTalleres = $.grep(oComboTaller, function (e) { return (e.coddpto == coddpto && e.codprov == codprov); });
                    else objTalleres = $.grep(oComboTaller, function (e) { return (e.coddpto == coddpto); });

                    var text_default = text_Todos;
                    if (PARM_13 == "5") text_default = text_default;
                    fc_FillCombo("cboTaller", objTalleres, text_default);
                    if (nid_taller_sel != undefined && nid_taller_sel != "") { $("#cboTaller").val(nid_taller_sel); }
                    if (objTalleres.length == 1) {
                        $("#cboTaller").val(objTalleres[0].value);

                        $("#btnMapaTallerPR").show();
                        $("#cboTaller").prop("disabled", true);

                        var strMapaTaller = objTalleres[0].tx_mapa_taller;
                        if (strMapaTaller != "") {
                            var strRutaMapa = '<%=ConfigurationManager.AppSettings["RutaMapasBO"].ToString()  %>' + strMapaTaller;
                            $("#btnMapaTallerPR").attr("onclick", "javascript:foto('" + strRutaMapa + "','" + objTalleres[0].nombre + "');");
                        }
                        else {
                            $("#btnMapaTallerPR").attr("onclick", "javascript:alert('<%=oPrm.msgNoMapa %>');");
                        }
                    }
                    $("#cboTaller").trigger("change", [fl_ver_horario]);
                }
                else if (fl_ubigeo_all == "1") {
                    fc_FillCombo("cboTaller", oComboTaller, text_Todos);
                    if (nid_taller_sel != undefined && nid_taller_sel != "") { $("#cboTaller").val(nid_taller_sel); }
                    $("#cboTaller").trigger("change", [fl_ver_horario]);
                }
                else {
                    fc_FillCombo("cboTaller", [], text_Seleccione);
                    $("#cboTaller").trigger("change");
                }
            }
        });
        $("#cboTaller").change(function (event, fl_ver_horario) {
            if (fl_ver_horario != "0") {
                //Set datos de reserva - Taller
                oCita.nid_taller = 0;
                oCita.fe_programada = "";
                oCita.qt_intervalo_taller = 0;
                //Set datos de reserva - Asesor
                oCita.nid_asesor = 0;
                oCita.ho_inicio = "";
                oCita.qt_intervalo_atenc = 0;

                $("#txtFecInicio").val("");
                $("#txtFecInicio").datepicker("destroy");
            }

            var nid_taller_value = $(this).val();
            var arr_ubi_taller_aux = nid_taller_value.split("$");
            var nid_taller;
            if (arr_ubi_taller_aux.length > 1) { nid_taller = arr_ubi_taller_aux[1]; }
            else { nid_taller = nid_taller_value; }

            //Set datos de reserva
            oCita.nid_taller = nid_taller;

            //--
            $("#chkValeTaxi").prop("checked", false);
            if (nid_taller > 0) {
                var objTalleres = $.grep(oComboTaller, function (e) { return (e.nid_taller == this.oCita.nid_taller); });
                if (objTalleres[0].fl_taxi == "1") { $("#divValeTaxi").show(); }
                else { $("#divValeTaxi").hide(); }
            }
            else {
                $("#divValeTaxi").hide();
            }
            //--

            var coddpto = $("#cboDepartamento").val();
            var codprov_value = $("#cboProvincia").val();
            var codprov = codprov_value.length > 2 ? codprov_value.substr(2, 2) : codprov_value;
            var coddist_value = $("#cboDistrito").val();
            var coddist = coddist_value.length > 2 ? coddist_value.substr(4, 2) : coddist_value;
            var nid_ubica_value = $("#cboUbicacion").val();
            var nid_ubica = nid_ubica_value.length > 6 ? nid_ubica_value.substr(6) : nid_ubica_value;

            if ((coddpto == "" || codprov_value == "" || coddist_value == "" || nid_ubica_value == "") && arr_ubi_taller_aux.length > 1) {
                $("#cboDepartamento").val(arr_ubi_taller_aux[0].substr(0, 2));
                if (codprov_value == "") { codprov_value = arr_ubi_taller_aux[0].substr(0, 4); }
                if (coddist_value == "") { coddist_value = arr_ubi_taller_aux[0].substr(0, 6); }
                if (nid_ubica_value == "") { nid_ubica_value = arr_ubi_taller_aux[0]; }

                $("#cboDepartamento").trigger("change", [codprov_value, coddist_value, nid_ubica_value, nid_taller_value, fl_ver_horario]);
                return;
            }
            else {
                fn_GetHorarioDisponibleTaller(fl_ver_horario);
            }
        });
        var id_img_select = "";
        var src_img_select_ant = "";
        function fn_SetHoraTaller(RowID, no_columna, id_img, keys) {
            if (id_img != id_img_select) {
                $("img[idfoo='" + id_img + "']").attr("src", imgURL_Hora_Separada);
                if (id_img_select != "") {
                    $("img[idfoo='" + id_img_select + "']").attr("src", imgURL_Hora_Libre);
                }
                id_img_select = id_img;

                var coddpto = keys.split("|")[0];
                var codprov_value = keys.split("|")[1];
                var coddist_value = keys.split("|")[2];
                var nid_ubica_value = keys.split("|")[3];
                var nid_taller_value = keys.split("|")[4];
                var qt_intervalo_taller = keys.split("|")[5];

                var hora_ini_preseleccion = no_columna.split("_")[1];

                var codprov = codprov_value.length > 2 ? codprov_value.substr(2, 2) : codprov_value;
                var coddist = coddist_value.length > 2 ? coddist_value.substr(4, 2) : coddist_value;
                var arr_ubi_taller_aux = nid_taller_value.split("$");
                var nid_ubica;
                var nid_taller;
                if (arr_ubi_taller_aux.length > 1) {
                    nid_ubica = arr_ubi_taller_aux[0].substr(6);
                    nid_taller = arr_ubi_taller_aux[1];
                }
                else {
                    nid_ubica = nid_ubica_value;
                    nid_taller = nid_taller_value;
                }

                //Set datos de reserva
                this.oCita.nid_taller = nid_taller;
                this.oCita.fe_programada = $("#txtFecInicio").val();
                this.oCita.qt_intervalo_taller = qt_intervalo_taller;

                $("#cboTaller").val(nid_taller_value);
                var fl_ver_horario = "0";
                $("#cboTaller").trigger("change", [fl_ver_horario]);

                fn_GetHorarioDisponibleAsesor(hora_ini_preseleccion);
            }
        }
        function fn_SetHoraAsesor_CE(RowID, no_columna, id_img, keys) {
            if (id_img != id_img_select) {
                var nid_asesor = keys.split("$")[0];
                var ho_inicio = keys.split("$")[1];
                var qt_intervalo_atenc = keys.split("$")[2];

                var parametros = new Array();
                parametros[0] = oCita.nid_servicio;
                parametros[1] = oCita.nid_taller;
                parametros[2] = nid_asesor;
                parametros[3] = oCita.fe_programada;
                parametros[4] = ho_inicio;
                parametros[5] = qt_intervalo_atenc;

                var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                var strUrlServicio = no_pagina + "/Get_ValidaColaEspera";
                this.fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                    if (objResponse.msg_retorno != "") {
                        fc_Alert(objResponse.msg_retorno);
                    }
                    if (objResponse.retorno > 0) {
                        fn_SetHoraAsesor(RowID, no_columna, id_img, keys);
                    }
                });
            }
        }
        function fn_SetHoraAsesor(RowID, no_columna, id_img, keys) {
            if (id_img != id_img_select) {
                var src_img = $("img[idfoo='" + id_img + "']").attr("src");
                if (src_img == imgURL_Hora_Reservada) {
                    this.oCita.tipo_registro = "CE";
                }
                else {
                    this.oCita.tipo_registro = "REG";
                }

                $("img[idfoo='" + id_img + "']").attr("src", imgURL_Hora_Separada);
                if (id_img_select != "") {
                    $("img[idfoo='" + id_img_select + "']").attr("src", src_img_select_ant);
                }
                id_img_select = id_img;
                src_img_select_ant = src_img;

                var nid_asesor = keys.split("$")[0];
                var ho_inicio = keys.split("$")[1];
                var qt_intervalo_atenc = keys.split("$")[2];
                var strSeleccion = keys.split("$")[3];

                $("#divselhora").text(strSeleccion);
                //Set datos de reserva
                this.oCita.nid_asesor = nid_asesor;
                this.oCita.ho_inicio = no_columna;
                this.oCita.qt_intervalo_atenc = qt_intervalo_atenc;
            }
        }
        function fn_ChangedFecha() {
            this.oCita.fe_programada = $("#txtFecInicio").val();
            if (this.oCita.nid_taller > 0) fn_GetHorarioDisponibleAsesor(undefined);
            else fn_GetHorarioDisponibleTaller(undefined);
        }
        $("#previousDay_FecInicio").click(function () {
            var date_ant = $("#txtFecInicio").val();
            if (date_ant != "") {
                $("#txtFecInicio").datepicker("setDate", "c-1d");
                var date_aux = $("#txtFecInicio").val();
                if (date_ant == date_aux) {
                    fc_Alert("La fecha mínima de reserva es el " + date_ant);
                } else {
                    fn_ChangedFecha();
                }
            }
        });
        $("#nextDay_FecInicio").click(function () {
            var date_ant = $("#txtFecInicio").val();
            if (date_ant != "") {
                $("#txtFecInicio").datepicker("setDate", "c+1d");
                var date_aux = $("#txtFecInicio").val();
                if (date_ant == date_aux) {
                    fc_Alert("La fecha máxima de reserva es el " + date_ant);
                } else {
                    fn_ChangedFecha();
                }
            }
        });
        function fn_GetHorarioDisponibleTaller(fl_ver_horario) {
            //Set datos de reserva
            $("#divselhora").empty();
            oCita.nid_asesor = 0;
            oCita.ho_inicio = "";
            oCita.qt_intervalo_atenc = "";

            var coddpto = $("#cboDepartamento").val();
            var codprov = fn_GetValue_Prov();
            var coddist = fn_GetValue_Dist();
            var nid_ubica = fn_GetValue_Ubicacion();
            var nid_taller = fn_GetValue_Taller();

            if (this.oCita.nid_servicio <= 0) {
                $("#txtFecInicio").val("");
                $("#txtFecInicio").datepicker("destroy");
            }
            if (fl_cambio_x_horario != true) {
                fc_FillCombo("cboHoraInicioReserva", [], text_Seleccione);
                fc_FillCombo("cboHoraFinalReserva", [], text_Seleccione);
            }
            fl_cambio_x_horario = false;

            $("#divGrvUbicacion").empty();
            $("#div_Leyendas_Pie").hide();
            $("#lblTCT").text("0");

            if (nid_taller != "") PRM_SELECTED = "5";
            else if (nid_ubica != 0) PRM_SELECTED = "4";
            else if (coddist != "") PRM_SELECTED = "3";
            else if (codprov != "") PRM_SELECTED = "2";
            else if (coddpto != "") PRM_SELECTED = "1";
            else PRM_SELECTED = "";

            if (fl_ver_horario == "1" || fl_ver_horario == undefined) {
                if (oCita.nid_servicio > 0 && coddpto != "" && (PARM_13 <= PRM_SELECTED || fl_ubigeo_all == "1")) {
                    var parametros = new Array();
                    parametros[0] = oCita.nid_modelo;
                    parametros[1] = oCita.nid_servicio;
                    parametros[2] = coddpto;
                    parametros[3] = codprov;
                    parametros[4] = coddist;
                    parametros[5] = nid_ubica;
                    parametros[6] = nid_taller;
                    parametros[7] = $("#txtFecInicio").val();
                    //parametros[8] = nid_usuario;
                    parametros[8] = $("#cboHoraInicioReserva").val();
                    parametros[9] = $("#cboHoraFinalReserva").val();
                    parametros[10] = nid_usuario;

                    var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                    var strUrlServicio = no_pagina + "/Get_HorarioDisponible";

                    $("#divProgress_aux").show();
                    fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                        $("#divProgress_aux").hide();

                        if (objResponse.msg_retorno != "") { fc_Alert(objResponse.msg_retorno); }

                        if ($("#txtFecInicio").val() == "") {
                            $("#txtFecInicio").datepicker("destroy");
                            var DatePicker_Opciones = DatePicker_Opciones_Default;
                            DatePicker_Opciones.minDate = objResponse.sfe_min_reserva;
                            DatePicker_Opciones.maxDate = objResponse.sfe_max_reserva;
                            DatePicker_Opciones.fl_enabled_textbox = false;
                            this.fc_FormatFecha("txtFecInicio", DatePicker_Opciones, "", "");
                            $("#txtFecInicio").datepicker("setDate", objResponse.sfe_reserva);
                        }

                        id_img_select = "";
                        src_img_select_ant = "";
                        if (objResponse.fl_seguir == "1") {
                            if (objResponse.oComboHoraInicio.length > 0) {
                                this.fc_FillCombo("cboHoraInicioReserva", objResponse.oComboHoraInicio, text_Seleccione);
                                this.fc_FillCombo("cboHoraFinalReserva", objResponse.oComboHoraInicio, text_Seleccione);
                                $("#cboHoraInicioReserva option[value='']").remove();
                                $("#cboHoraFinalReserva option[value='']").remove();
                                $("#cboHoraFinalReserva").prop("selectedIndex", $("#cboHoraFinalReserva option").length - 1);
                            }

                            oHorario_Cabecera = objResponse.oHorario_Cabecera;
                            oHorario_ModelCol = objResponse.oHorario_ModelCol;
                            oHorarioDisponible = objResponse.oHorarioDisponible;
                            IntervaloT = objResponse.IntervaloT;

                            $("#divGrvUbicacion").empty();
                            $("#divGrvUbicacion").append(objResponse.tbl_Footable);

                            $("#div_Leyendas_Pie").show();
                            $("#lblTCT").text("0");
                        }
                        else if (objResponse.fl_seguir == "2") {
                            this.oCita.fe_programada = $("#txtFecInicio").val();
                            this.fn_GetHorarioDisponibleAsesor(undefined);
                        }
                        else if (objResponse.fl_seguir == "0") {
                            $("#divGrvUbicacion").empty();
                            $("#divGrvUbicacion").append(strDivSinHorario);
                            $("#divSinHorario").text(objResponse.msg_retorno);
                            $("#div_Leyendas_Pie").hide();
                            $("#lblTCT").text("0");
                        }
                    });
                }
            }
        }
        function fn_GetHorarioDisponibleAsesor(hora_ini_preseleccion) {
            //Set datos de reserva
            $("#divselhora").empty();
            oCita.nid_asesor = 0;
            oCita.ho_inicio = "";
            oCita.qt_intervalo_atenc = "";

            if (this.oCita.nid_servicio <= 0) {
                $("#txtFecInicio").val("");
                $("#txtFecInicio").datepicker("destroy");
            }

            if (fl_cambio_x_horario != true) {
                fc_FillCombo("cboHoraInicioReserva", [], text_Seleccione);
                fc_FillCombo("cboHoraFinalReserva", [], text_Seleccione);
            }
            fl_cambio_x_horario = false;

            $("#divGrvUbicacion").empty();
            $("#div_Leyendas_Pie").hide();
            $("#lblTCT").text("0");

            //Carga Horario de Asesores
            var parametros = new Array();
            parametros[0] = oCita.nid_modelo;
            parametros[1] = oCita.nid_servicio;
            parametros[2] = $("#cboDepartamento").val();
            parametros[3] = fn_GetValue_Prov();
            parametros[4] = fn_GetValue_Dist();
            parametros[5] = fn_GetValue_Ubicacion();
            parametros[6] = this.oCita.nid_taller;
            parametros[7] = this.oCita.fe_programada;
            parametros[8] = (hora_ini_preseleccion == undefined ? "" : hora_ini_preseleccion);
            parametros[9] = this.oCita.qt_intervalo_taller;
            parametros[10] = IntervaloT;
            parametros[11] = $("#cboHoraInicioReserva").val();
            parametros[12] = $("#cboHoraFinalReserva").val();
            parametros[13] = nid_usuario;
            var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
            var strUrlServicio = no_pagina + "/Get_HorarioDisponible_Asesor";
            fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                if (objResponse.msg_retorno != "") { fc_Alert(objResponse.msg_retorno); }

                id_img_select = "";
                src_img_select_ant = "";
                if (objResponse.fl_seguir == "1") {
                    $("#txtFecInicio").val(objResponse.sfe_reserva);

                    if (objResponse.oComboHoraInicio.length > 0) {
                        this.fc_FillCombo("cboHoraInicioReserva", objResponse.oComboHoraInicio, text_Seleccione);
                        this.fc_FillCombo("cboHoraFinalReserva", objResponse.oComboHoraInicio, text_Seleccione);
                        $("#cboHoraInicioReserva option[value='']").remove();
                        $("#cboHoraFinalReserva option[value='']").remove();
                        $("#cboHoraFinalReserva").prop("selectedIndex", $("#cboHoraFinalReserva option").length - 1);
                    }

                    $("#lblTCT").text(objResponse.qt_TCT);

                    $("#divGrvUbicacion").empty();
                    $("#divGrvUbicacion").append(objResponse.tbl_Footable);
                    $("#div_Leyendas_Pie").show();

                }
                else if (objResponse.fl_seguir == "0") {
                    $("#divGrvUbicacion").empty();
                    $("#divGrvUbicacion").append(strDivSinHorario);
                    $("#divSinHorario").text(objResponse.msg_retorno);
                    $("#div_Leyendas_Pie").hide();
                    $("#lblTCT").text("0");
                }
            });
        }
        var fl_cambio_x_horario = false;
        $("#cboHoraFinalReserva").change(function () {
            var ho_final = $("#cboHoraInicioReserva").val();
            var ho_inicio = $(this).val();

            if ($("#cboHoraInicioReserva").prop("selectedIndex") >= $("#cboHoraFinalReserva").prop("selectedIndex")) {
                fc_Alert("La hora final debe ser mayor que la hora inicial.");
                return;
            }

            fl_cambio_x_horario = true;

            if (oCita.nid_taller > 0) fn_GetHorarioDisponibleAsesor(undefined);
            else fn_GetHorarioDisponibleTaller(undefined);
        });
        $("#cboHoraInicioReserva").change(function () {
            var ho_inicio = $(this).val();
            var ho_final = $("#cboHoraFinalReserva").val();

            if ($("#cboHoraInicioReserva").prop("selectedIndex") >= $("#cboHoraFinalReserva").prop("selectedIndex")) {
                fc_Alert("La hora inicial debe ser menor que la hora final.");
                return;
            }

            fl_cambio_x_horario = true;

            if (oCita.nid_taller > 0) fn_GetHorarioDisponibleAsesor(undefined);
            else fn_GetHorarioDisponibleTaller(undefined);
        });

        function fn_RegistrarCita() {
            this.oCita.co_tipo_documento = $("#cboTipoDocumento").val();
            this.oCita.nu_documento = fc_Trim($("#txtNroDocumento").val());
            this.oCita.no_cliente = fc_Trim($("#txtNombres").val());
            this.oCita.ape_paterno = fc_Trim($("#txtApePaterno").val());
            this.oCita.ape_materno = fc_Trim($("#txtApeMaterno").val());
            this.oCita.no_correo_personal = fc_Trim($("#txtEmailPersonal").val());
            this.oCita.no_correo_trabajo = fc_Trim($("#txtEmailTrabajo").val());
            this.oCita.no_correo_alternativo = fc_Trim($("#txtEmailAlternativo").val());
            this.oCita.nu_telefono = $("#txtTelefonoFijo").val();
            this.oCita.nu_telefono_oficina = $("#txtTelefonoOficina").val();
            this.oCita.nu_celular = $("#txtTelefonoMovil1").val();
            this.oCita.nu_celular_alter = $("#txtTelefonoMovil2").val();
            this.oCita.co_tipo_record = ($("#chkSMSCli").prop("checked") == true ? "3" : "1");
            this.oCita.dd_record = $("#cboDiaContacto").val();
            this.oCita.ho_record_ini = $("#cboHIContacto").val();
            this.oCita.ho_record_fin = $("#cboHFContacto").val();
            this.oCita.fl_taxi = ($("#chkValeTaxi").prop("checked") == true ? "1" : "0");
            this.oCita.co_usuario = co_usuario;
            this.oCita.no_usuario_red = co_usuario_red;
            this.oCita.no_estacion_red = no_estacion_red;
            this.oCita.no_color = $("#hf_ColorExterno").val();
            this.oCita.nid_contact_center = id_contactcenter;
            this.oCita.recojounidad = ($("#chkRecojoUnidad").prop("checked") == true ? "1" : "0");
            if (this.fn_ValidarDatosReservaCliente()) {
                $("#btnRegistrar").hide();

                var strParametros = "{'strParametros':" + JSON.stringify(this.oCita) + "}";
                var strUrlServicio = no_pagina + "/SaveReserva";
                this.fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                    if (objResponse.fl_seguir == "1") {
                        //muestra modal resumen
                        fn_Open_ResumenCita();

                        $("#lblCliente_RC").text(objResponse.oDatosCita.no_cliente);
                        $("#lblTelefono_RC").text(objResponse.oDatosCita.nu_telefono);
                        $("#lblCelular_RC").text(objResponse.oDatosCita.nu_celular);
                        $("#lblEmailPersonal_RC").text(objResponse.oDatosCita.no_correo_personal);
                        $("#lblEmailTrabajo_RC").text(objResponse.oDatosCita.no_correo_trabajo);
                        $("#lblEmailAlternativo_RC").text(objResponse.oDatosCita.no_correo_alternativo);
                        if (objResponse.oDatosCita.no_correo_personal == "") {
                            $("#div_EmailPersonal_RC").hide();
                        }
                        if (objResponse.oDatosCita.no_correo_trabajo == "") {
                            $("#div_EmailTrabajo_RC").hide();
                        }
                        if (objResponse.oDatosCita.no_correo_alternativo == "") {
                            $("#div_EmailAlternativo_RC").hide();
                        }
                        //--
                        $("#lblPlaca_RC").text(objResponse.oDatosCita.nu_placa);
                        $("#lblMarca_RC").text($("#txtMarca").val());
                        $("#lblModelo_RC").text($("#txtModelo").val());
                        $("#lblCodCita_RC").text(objResponse.oDatosCita.co_reserva);
                        $("#lblFecha_RC").text(objResponse.oDatosCita.fe_programada);
                        $("#lblServicio_RC").text($("#cboServicioEspecifico option:selected").text());
                        $("#lblAsesorServicio_RC").text(objResponse.oDatosCita.no_asesor);
                        $("#lblTaller_RC").text(objResponse.oDatosCita.no_taller);
                        $("#lblTelefonoTaller_RC").text(objResponse.oDatosCita.nu_telf_taller);
                        $("#lblDireccionTaller_RC").text(objResponse.oDatosCita.di_ubica);
                        $("#lblTelefonoCallCenter_RC").text(objResponse.oDatosCita.nu_telf_callcenter);

                        $("#btnImprimir").attr("onclick", "javascript:fn_PopupDetalleCita('" + objResponse.oDatosCita.template_impresion + "');")
                    }

                    if (objResponse.msg_retorno != "") { fc_Alert(objResponse.msg_retorno); }
                    if (objResponse.fl_seguir == "2") {
                        //reload
                        location.reload();
                    }
                });
            }
        }
        function fn_ValidarDatosReservaCliente() {
            var Pais = '<%=ConfigurationManager.AppSettings["CodPais"].ToString() %>';

            this.oCita.tx_observacion = $("#txtObservaciones").val();

            //Valida datos vehículo y servicio
            if (this.oCita.nu_placa == "") {
                fc_Alert("Debe ingresar un número de <%=oPrm.N_Placa %>"); return false;
            }
            else if (this.oCita.nid_marca <= 0) {
                fc_Alert("Debe ingresar un número de <%=oPrm.N_Placa %> y buscar"); return false;
            }
            else if (this.oCita.nid_modelo <= 0) {
                fc_Alert("Debe ingresar un número de <%=oPrm.N_Placa %> y buscar"); return false;
            }
            else if ($("#cboTipoServicio").val() == "") {
                fc_Alert("Debe seleccionar un Tipo de Servicio."); return false;
            }
            else if (this.oCita.nid_servicio <= 0) {
                fc_Alert("Debe seleccionar un Servicio."); return false;
            }
            else if (this.oCita.nid_asesor <= 0) {
                fc_Alert("Debe seleccionar un Horario de Cita."); return false;
            }
            else if (this.oCita.ho_inicio == "") {
                fc_Alert("Debe seleccionar un Horario de Cita."); return false;
            }

            //Valida Contacto
            if (oCita.co_tipo_documento == "") {
                fc_Alert("Debe seleccionar tipo de documento."); return false;
            }
            else if (oCita.nu_documento == "") {
                fc_Alert("Debe ingresar un número de documento."); return false;
            }

            if (oCita.co_tipo_documento == "01") { //[ DNI - RUT ]
                if (Pais == "1") {
                    if (oCita.nu_documento.length != 8) {
                        fc_Alert("Ingrese un DNI correcto."); return false;
                    }
                }
                else {
                    if (oCita.nu_documento.length < 7) {
                        fc_Alert("Ingrese un RUT correcto."); return false;
                    }
                }
            }
            else if (oCita.co_tipo_documento == "02") { //PASAPORTE
                if (oCita.nu_documento.length < 3) {
                    fc_Alert("Ingrese los dígitos del número de Pasaporte."); return false;
                }
            }
            else if (oCita.co_tipo_documento == "04") { //CE
                if (oCita.nu_documento.length <= 0) {
                    fc_Alert("Ingrese los dígitos del número de CE."); return false;
                }
            }

            if (fc_Trim(this.oCita.no_cliente) == "") {
                fc_Alert("Ingrese el nombre del contacto."); return false;
            }
            if (fc_Trim(this.oCita.ape_paterno) == "") {
                fc_Alert("Ingrese el apellido paterno del contacto."); return false;
            }
            if (fc_Trim(this.oCita.ape_materno) == "") {
                fc_Alert("Ingrese el apellido materno del contacto."); return false;
            }
            if (fc_Trim(oCita.nu_telefono) == "") {
                fc_Alert("Ingrese el teléfono fijo del contacto."); return false;
            }

            var chkSMSCli = document.getElementById("chkSMSCli");
            if (chkSMSCli.checked) {
                if (fc_Trim(oCita.nu_celular) == "") {
                    fc_Alert("Ingrese el teléfono móvil del contacto."); return false;
                }
            }

            //Emails
            if (fc_Trim(this.oCita.no_correo_personal) != "") {
                if (!fc_ValidarEmail(this.oCita.no_correo_personal)) { fc_Alert("Ingrese una dirección de e-mail válido."); return false; }
            }
            if (fc_Trim(this.oCita.no_correo_trabajo) != "") {
                if (!fc_ValidarEmail(this.oCita.no_correo_trabajo)) { fc_Alert("Ingrese una dirección de e-mail válido."); return false; }
            }
            if (fc_Trim(this.oCita.no_correo_alternativo) != "") {
                if (!fc_ValidarEmail(this.oCita.no_correo_alternativo)) { fc_Alert("Ingrese una dirección de e-mail válido."); return false; }
            }

            if (fc_Trim(this.oCita.no_correo_personal) == "" && fc_Trim(this.oCita.no_correo_trabajo) == "" && fc_Trim(this.oCita.no_correo_alternativo) == "") {
                fc_Alert("Debe ingresar al menos uno de los 3 e-mails."); return false;
            }

            //verificamos si esta oculto o no el panel de datos de recordatorio
            if (this.fn_ExistDisplayControl("divDatosRecord")) {
                if (this.oCita.dd_record == "" && this.oCita.ho_record_ini == "" && this.oCita.ho_record_fin == "") {
                    //Sin seleccion alguna. OK
                }
                else {
                    if (this.oCita.dd_record == "") {
                        fc_Alert("Debe seleccionar el día de Recordatorio."); return false;
                    }
                    if (this.oCita.ho_record_ini == "") {
                        fc_Alert("Seleccione la hora de inicio de recordatorio."); return false;
                    }
                    if (this.oCita.ho_record_fin == "") {
                        fc_Alert("Seleccione la hora final de recordatorio."); return false;
                    }
                    if ($("#cboHIContacto").prop("selectedIndex") >= $("#cboHFContacto").prop("selectedIndex")) {
                        fc_Alert("La hora de inicio no puede ser mayor que la hora final."); return false;
                    }
                }
            }
            return true;
        }


        /*#region - Funciones del Modal - Cita Pendiente*/
        $("#modal_CitaPendiente").draggable({
            handle: ".modal-header"
        });
        function fn_Open_CitaPendiente() {
            $("#modal_CitaPendiente").modal("show");
        }
        function fn_Close_CitaPendiente() {
            $("#modal_CitaPendiente").modal("hide");
        }
        /*#endregion - Funciones del Modal - Cita Pendiente*/

        /*#region - Funciones del Modal - Busqueda de Vehiculos*/
        //#region - Variables Grilla Vehiculo
        var idGrilla_Vehiculo = "#grvVehiculo_BusVeh";
        var idPieGrilla_Vehiculo = "#grvVehiculo_BusVeh_Pie";
        var strCabecera_Vehiculo = ['#', '', 'Placa', 'Marca', 'Modelo', 'VIN', 'Propietario'];
        var ModelCol_Vehiculo = [
            { name: 'nid_vehiculo', index: 'nid_vehiculo', hidden: true },
            { name: 'img', index: 'img', width: 30, align: 'center' },
            { name: 'nu_placa', index: 'nu_placa', width: 50, align: 'center' },
            { name: 'no_marca', index: 'no_marca', width: 70, align: 'center' },
            { name: 'no_modelo', index: 'no_modelo', width: 70, align: 'center' },
            { name: 'nu_vin', index: 'nu_vin', width: 120, align: 'center' },
            { name: 'no_propietario', index: 'no_propietario', width: 250 }
        ];
        //#endregion - Variables Grilla Vehiculo
        $("#modal_BusquedaVehiculo").draggable({
            handle: ".modal-header"
        });
        function fn_Open_BuscarVehiculo() {
            $("#modal_BusquedaVehiculo").modal("show");

            $("#txtPlaca_BusVeh").val("");
            $("#cboMarca_BusVeh").val("").trigger("change");
            $("#txtVIN_BusVeh").val("");
            $("#cboTipoDocumento_BusVeh").val("").trigger("change");
            $("#txtNroDocumento_BusVeh").val("");
            $("#txtNombres_BusVeh").val("");
            $("#txtApePaterno_BusVeh").val("");
            $("#txtApeMaterno_BusVeh").val("");

            var objResponse = [];
            JQGrid_Util.AutoWidthResponsive(idGrilla_Vehiculo);
            JQGrid_Util.GetTabla_Local(idGrilla_Vehiculo, idPieGrilla_Vehiculo, strCabecera_Vehiculo, ModelCol_Vehiculo, JQGrid_Opciones_Default
                , objResponse, function () { }, function () { }, function () { });
        }
        function fn_Close_BuscarVehiculo() {
            $("#modal_BusquedaVehiculo").modal("hide");
        }
        $("#cboMarca_BusVeh").change(function () {
            var nid_marca = $(this).val();
            if (nid_marca > 0) {
                var parametros = new Array();
                parametros[0] = nid_usuario;
                parametros[1] = nid_marca;
                var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                var strUrlServicio = no_pagina + "/Get_Modelos";
                fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                    fc_FillCombo("cboModelo_BusVeh", objResponse.oComboModelo, text_Seleccione);
                });
            }
            else {
                fc_FillCombo("cboModelo_BusVeh", [], text_Seleccione);
            }
        });

        function Fc_ValidarTipoDoc(obj) {
            var Pais = '<%=ConfigurationManager.AppSettings["CodPais"].ToString() %>';
            var DNI = '<%=ConfigurationManager.AppSettings["TIPODOCDNI"].ToString() %>';
            var RUC = '<%=ConfigurationManager.AppSettings["TIPODOCRUC"].ToString() %>';

            document.getElementById("lblTipoPers_BusVeh").innerHTML = ((obj.value == RUC) ? "Razón Social" : "Nombres");
            document.getElementById("txtApePaterno_BusVeh").disabled = (obj.value == RUC);
            document.getElementById("txtApeMaterno_BusVeh").disabled = (obj.value == RUC);
            document.getElementById("txtNroDocumento_BusVeh").disabled = (obj.selectedIndex == 0);
            document.getElementById("txtNroDocumento_BusVeh").value = "";

            if (Pais == '1') {
                document.getElementById("txtNroDocumento_BusVeh").maxLength = ((obj.value == DNI) ? 8 : ((obj.value == RUC) ? 11 : 20));
                document.getElementById("txtNroDocumento_BusVeh").setAttribute("onkeypress", ((obj.value == RUC) || (obj.value == DNI)) ? "return fc_SoloNumeros(event);" : "return SoloLetrasNumeros(event);");
            }
            else {
                document.getElementById("txtNroDocumento_BusVeh").maxLength = ((obj.value == DNI) ? 9 : ((obj.value == RUC) ? 11 : 20));
                document.getElementById("txtNroDocumento_BusVeh").setAttribute("onkeypress", ((obj.value == RUC) ? "return fc_SoloNumeros(event);" : "return SoloLetrasNumeros(event);"));
            }

            if (obj.value == RUC) {
                document.getElementById("txtApePaterno_BusVeh").value = "";
                document.getElementById("txtApeMaterno_BusVeh").value = "";
            }
        }

        function fn_BuscarVehiculos() {
            var arr_parametros = new Array();
            arr_parametros[0] = $("#txtPlaca_BusVeh").val();
            arr_parametros[1] = $("#cboMarca_BusVeh").val();
            arr_parametros[2] = $("#cboModelo_BusVeh").val();
            arr_parametros[3] = $("#txtVIN_BusVeh").val();
            arr_parametros[4] = $("input[name='rblTipo_BusVeh']:checked").val();
            arr_parametros[5] = $("#cboTipoDocumento_BusVeh").val();
            arr_parametros[6] = $("#txtNroDocumento_BusVeh").val();
            arr_parametros[7] = $("#txtNombres_BusVeh").val();
            arr_parametros[8] = $("#txtApePaterno_BusVeh").val();
            arr_parametros[9] = $("#txtApeMaterno_BusVeh").val();
            arr_parametros[10] = nid_usuario;

            var strUrlServicio = no_pagina + "/Get_BandejaVehiculo";
            JQGrid_Util.AutoWidthResponsive(idGrilla_Vehiculo);
            JQGrid_Util.GetTabla_Ajax(arr_parametros, strUrlServicio, idGrilla_Vehiculo, idPieGrilla_Vehiculo
                , strCabecera_Vehiculo, ModelCol_Vehiculo, JQGrid_Opciones_Default, function () { }, function () { }, function () { });

        }
        function fn_SeleccionaVehiculo(rowID) {
            var rowData = $(idGrilla_Vehiculo).getRowData(rowID);
            fn_Close_BuscarVehiculo();
            $("#txtPlaca").val(rowData["nu_placa"]);
            fn_GetVehiculo($("#txtPlaca").val());
        }
        /*#endregion - Funciones del Modal - Busqueda de Vehiculos*/

        /*#region - Funciones del Modal - Agregar Vehículo*/
        $("#modal_AgregarVehiculo").draggable({
            handle: ".modal-header"
        });
        function fn_Open_AgregarVehiculo() {
            $("#modal_AgregarVehiculo").modal("show");

            $("#txtPlaca_RV").val("");
            $("#cboMarca_RV").val("").trigger("change");
            $("#txtVIN_RV").val("");
            $("#txtKMActual_RV").val("0");
        }
        function fn_Close_AgregarVehiculo() {
            $("#modal_AgregarVehiculo").modal("hide");
        }
        $("#cboMarca_RV").change(function () {
            var nid_marca = $(this).val();
            if (nid_marca > 0) {
                var parametros = new Array();
                parametros[0] = nid_usuario;
                parametros[1] = nid_marca;
                var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                var strUrlServicio = no_pagina + "/Get_Modelos";
                fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                    fc_FillCombo("cboModelo_RV", objResponse.oComboModelo, text_Seleccione);
                });
            }
            else {
                fc_FillCombo("cboModelo_RV", [], text_Seleccione);
            }
        });
        function fn_GrabarVehiculo() {
            var nu_placa = fc_Trim($("#txtPlaca_RV").val());
            var nid_marca = $("#cboMarca_RV").val();
            var nu_vin = fc_Trim($("#txtVIN_RV").val());
            var nid_modelo = $("#cboModelo_RV").val();
            var nu_km_actual = fc_Trim($("#txtKMActual_RV").val());
            var nu_anio = 0; //se usa para chile
            var co_tipo = ""; //se usa para chile
            var mstrError = "";

            if (nu_placa == "")
                mstrError += mstrDebeIngresar + "la <%=oPrm.N_Placa %>. \n"
            else if (nu_placa != "" && !nu_placa.match(RE_ALAFANUMERICO))
                mstrError += mstrElCampo + " " + <%=oPrm.N_Placa %> + " " + mstrReAlfanumerico + ". \n"

            if (nu_km_actual == "")
                mstrError += mstrDebeIngresar + "el Kilometraje actual" + ". \n";

            if (nid_marca == "")
                mstrError += mstrDebeSeleccionar + "la Marca" + ". \n";

            if (nid_modelo == "")
                mstrError += mstrDebeSeleccionar + "el Modelo" + ". \n";

            if (mstrError != "") {
                fc_Alert(mstrError)
            }
            else {
                fc_Confirm(mstrSeguroGrabar, function (res) {
                    if (res == true) {
                        var parametros = new Array();
                        parametros[0] = nu_placa;
                        parametros[1] = nid_marca;
                        parametros[2] = nu_vin;
                        parametros[3] = nid_modelo;
                        parametros[4] = nu_km_actual;
                        parametros[5] = nu_anio;
                        parametros[6] = co_tipo;
                        parametros[7] = co_usuario;
                        parametros[8] = co_usuario_red;
                        parametros[9] = no_estacion_red;
                        var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                        var strUrlServicio = no_pagina + "/GrabarVehiculo";
                        fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                            var retorno = objResponse[0];
                            var msg_retorno = objResponse[1];
                            if (retorno > 0) {
                                fn_Close_AgregarVehiculo();
                                $("#txtPlaca").val(nu_placa);
                                fn_GetVehiculo(nu_placa);
                            }
                            fc_Alert(msg_retorno);
                        });
                    }
                });
            }
        }
        /*#endregion - Funciones del Modal - Agregar Vehículo*/

        /*#region - Funciones del Modal - Actualizar Propietario/Cliente*/
        var oPropCli = null;
        $("#modal_ActualizarPropCli").draggable({
            handle: ".modal-header"
        });
        function fn_Open_ActualizarPropCli(tipo) {
            if (fc_Trim(document.getElementById("txtPlaca").value) == "" || oCita.nid_vehiculo <= 0) {
                fc_Alert("Debe de ingresar un número de <%=oPrm.N_Placa %>");
            }
            else {
                $("#modal_ActualizarPropCli").modal("show");
                $("#lblTipoActualizacion").text((tipo == "1" ? "PROPIETARIO" : "CLIENTE"));

                oPropCli = (tipo == "1" ? oPropietario : oCliente);

                if (tipo == "2") {
                    $("#dvAnexo_Fijo").show();
                    $("#dvPais_Fijo").show();
                    $("#dvPais_Celular").show();
                    $("#dvTelefono_Fijo").removeClass("col-sm-8").addClass("col-sm-4");
                    $("#dvTelefono_Celular").removeClass("col-sm-8").addClass("col-sm-4");
                } else {
                    $("#dvAnexo_Fijo").hide();
                    $("#dvPais_Fijo").hide();
                    $("#dvPais_Celular").hide();
                    $("#dvTelefono_Fijo").removeClass("col-sm-4").addClass("col-sm-8");
                    $("#dvTelefono_Celular").removeClass("col-sm-4").addClass("col-sm-8");
                }

                //Limpiar campos y setea valores
                if (oPropCli != null) {
                    $("#cboTipoPersona_PropCli").val(oPropCli.co_tipo_persona).trigger("change", [oPropCli]);
                }
                else {
                    $("#cboTipoPersona_PropCli").val("").trigger("change");
                }
            }
        }
        function fn_Close_ActualizarPropCli() {
            $("#modal_ActualizarPropCli").modal("hide");
        }
        $("#cboTipoPersona_PropCli").change(function (event, oPropCli) {
            var co_tipo_per = $(this).val();
            if (co_tipo_per != "") {
                var parametros = new Array();
                parametros[0] = co_tipo_per;
                var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                var strUrlServicio = no_pagina + "/Get_TipoDocxTipoPersona";
                fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                    fc_FillCombo("cboTipoDocumento_PropCli", objResponse.oComboTipoDocumento, text_Seleccione);
                    $("#cboTipoDocumento_PropCli").val(co_tipo_per == oJuridica ? oRUC : (co_tipo_per == oNatural ? oDNI : "0"));
                    $("#cboTipoDocumento_PropCli").trigger("change", [oPropCli]);
                });
            }
            else {
                fc_FillCombo("cboTipoDocumento_PropCli", [], text_Seleccione);
                $("#cboTipoDocumento_PropCli").trigger("change");
            }

            $("#lblNombreRazon_PropCli").text(co_tipo_per == oNatural ? "Nombres(s)" : "Razón Social");
            if (co_tipo_per == oNatural) {
                $("#div_ApePaterno_PropCli").show();
                $("#div_ApeMaterno_PropCli").show();
            }
            else {
                $("#div_ApePaterno_PropCli").hide();
                $("#div_ApeMaterno_PropCli").hide();
            }
        });
        $("#cboTipoDocumento_PropCli").change(function (event, oPropCli) {
            var co_tipo_doc = $(this).val();
            if (oPropCli != null && oPropCli != undefined) {
                $("#CboTipoDocumento_PropCli").val(oPropCli.co_tipo_documento);
                $("#txtNroDocumento_PropCli").val(oPropCli.nu_documento);
                $("#txtNombresRazon_PropCli").val(oPropCli.no_nombre_razon);
                $("#txtApePaterno_PropCli").val(oPropCli.no_ape_paterno);
                $("#txtApeMaterno_PropCli").val(oPropCli.no_ape_materno);
                $("#txtTelefonoFijo_PropCli").val(oPropCli.nu_telefono);
                $("#txtTelefonoOficina_PropCli").val(oPropCli.nu_tel_oficina);
                $("#txtTelefonoMovil1_PropCli").val(oPropCli.nu_telmovil1);
                $("#txtTelefonoMovil2_PropCli").val(oPropCli.nu_telmovil2);
                $("#txtEmailPersonal_PropCli").val(oPropCli.no_correo);
                $("#txtEmailTrabajo_PropCli").val(oPropCli.no_correo_trabajo);
                $("#txtEmailAlternativo_PropCli").val(oPropCli.no_correo_alter);
                $("#txtTelefonoFijo_Anexo_PropCli").val(oPropCli.nu_anexo_telefono);
                $("#cboPaisTelefonoCel_PropCli").val(oPropCli.nid_pais_celular);
                $("#cboPaisTelefonoFijo_PropCli").val(oPropCli.nid_pais_telefono);
            }
            else {
                $("#txtNroDocumento_PropCli").val("");
                $("#txtNombresRazon_PropCli").val("");
                $("#txtApePaterno_PropCli").val("");
                $("#txtApeMaterno_PropCli").val("");
                $("#txtTelefonoFijo_PropCli").val("");
                $("#txtTelefonoOficina_PropCli").val("");
                $("#txtTelefonoMovil1_PropCli").val("");
                $("#txtTelefonoMovil2_PropCli").val("");
                $("#txtEmailPersonal_PropCli").val("");
                $("#txtEmailTrabajo_PropCli").val("");
                $("#txtEmailAlternativo_PropCli").val("");
                $("#txtTelefonoFijo_Anexo_PropCli").val("");
                $("#cboPaisTelefonoCel_PropCli").val("");
                $("#cboPaisTelefonoFijo_PropCli").val("");
            }

            if (co_tipo_doc != "") {
                if (SRC_CodPais == "1") {
                    if (co_tipo_doc == oDNI) {
                        $("#txtNroDocumento_PropCli").attr("maxlength", 8);
                        $("#txtNroDocumento_PropCli").attr("onkeypress", "return fc_SoloNumeros(event)");
                    }
                    else if (co_tipo_doc == oRUC) {
                        $("#txtNroDocumento_PropCli").attr("maxlength", 11);
                        $("#txtNroDocumento_PropCli").attr("onkeypress", "return fc_SoloNumeros(event)");
                    }
                    else {
                        $("#txtNroDocumento_PropCli").attr("maxlength", 20);
                        $("#txtNroDocumento_PropCli").attr("onkeypress", "return SoloLetrasNumeros(event)");
                    }
                }
                else {
                    if (co_tipo_doc = oDNI) {
                        $("#txtNroDocumento_PropCli").attr("maxlength", 9);
                        $("#txtNroDocumento_PropCli").attr("onkeypress", "return SoloLetrasNumeros(event)");
                    }
                    else if (co_tipo_doc == oRUC) {
                        $("#txtNroDocumento_PropCli").attr("maxlength", 11);
                        $("#txtNroDocumento_PropCli").attr("onkeypress", "return fc_SoloNumeros(event)");
                    }
                    else {
                        $("#txtNroDocumento_PropCli").attr("maxlength", 20);
                        $("#txtNroDocumento_PropCli").attr("onkeypress", "return SoloLetrasNumeros(event)");
                    }
                }
            }
        });
        $("#txtNroDocumento_PropCli").blur(function () {
            var nu_documento = fc_Trim($(this).val());
            if ((nu_documento != "" && (oPropCli != null && nu_documento != oPropCli.nu_documento))
                || (nu_documento != "" && oPropCli == null)) {
                var co_tipo_persona = fc_Trim($("#cboTipoPersona_PropCli").val());
                var co_tipo_documento = fc_Trim($("#cboTipoDocumento_PropCli").val());
                if (co_tipo_persona != "" && co_tipo_persona != "") {
                    var parametros = new Array();
                    parametros[0] = co_tipo_persona;
                    parametros[1] = co_tipo_documento;
                    parametros[2] = nu_documento;
                    var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                    var strUrlServicio = no_pagina + "/Get_PropCliente";
                    fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                        oPropCli = objResponse.oPropCliente;

                        if (oPropCli == null || oPropCli == "") {
                            oPropCli == null;
                        }
                        $("#txtNroDocumento_PropCli").val(oPropCli == null ? nu_documento : oPropCli.nu_documento);
                        $("#txtNombresRazon_PropCli").val(oPropCli == null ? "" : oPropCli.no_nombre_razon);
                        $("#txtApePaterno_PropCli").val(oPropCli == null ? "" : oPropCli.no_ape_paterno);
                        $("#txtApeMaterno_PropCli").val(oPropCli == null ? "" : oPropCli.no_ape_materno);
                        $("#txtTelefonoFijo_PropCli").val(oPropCli == null ? "" : oPropCli.nu_telefono);
                        $("#txtTelefonoOficina_PropCli").val(oPropCli == null ? "" : oPropCli.nu_tel_oficina);
                        $("#txtTelefonoMovil1_PropCli").val(oPropCli == null ? "" : oPropCli.nu_telmovil1);
                        $("#txtTelefonoMovil2_PropCli").val(oPropCli == null ? "" : oPropCli.nu_telmovil2);
                        $("#txtEmailPersonal_PropCli").val(oPropCli == null ? "" : oPropCli.no_correo);
                        $("#txtEmailTrabajo_PropCli").val(oPropCli == null ? "" : oPropCli.no_correo_trabajo);
                        $("#txtEmailAlternativo_PropCli").val(oPropCli == null ? "" : oPropCli.no_correo_alter);
                        $("#txtTelefonoFijo_Anexo_PropCli").val(oPropCli == null ? "" : oPropCli.nu_anexo_telefono == undefined ? "" : oPropCli.nu_anexo_telefono);
                        $("#cboPaisTelefonoCel_PropCli").val(oPropCli == null ? "" : oPropCli.nid_pais_celular == undefined ? "" : oPropCli.nid_pais_celular);
                        $("#cboPaisTelefonoFijo_PropCli").val(oPropCli == null ? "" : oPropCli.nid_pais_telefono == undefined ? "" : oPropCli.nid_pais_telefono);
                    });
                }
            }
        });
        function fn_Grabar_ActualizarPropCli() {
            var co_tipo = "";
            var noTipo = $("#lblTipoActualizacion").text();
            if (noTipo == "PROPIETARIO") {
                co_tipo = "PROP";
            }
            else if (noTipo == "CLIENTE") {
                co_tipo = "CLI";
            }
            else { return; }

            var nid_propcli = (oPropCli == null ? 0 : oPropCli.nid_cliente);
            var co_tipo_persona = fc_Trim($("#cboTipoPersona_PropCli").val());
            var co_tipo_documento = fc_Trim($("#cboTipoDocumento_PropCli").val());
            var nu_documento = fc_Trim($("#txtNroDocumento_PropCli").val());
            var no_nombre_razon = fc_Trim($("#txtNombresRazon_PropCli").val());
            var ape_paterno = fc_Trim($("#txtApePaterno_PropCli").val());
            var ape_materno = fc_Trim($("#txtApeMaterno_PropCli").val());
            var telf_fijo = $("#txtTelefonoFijo_PropCli").val();
            var telf_oficina = $("#txtTelefonoOficina_PropCli").val();
            var telf_movil1 = $("#txtTelefonoMovil1_PropCli").val();
            var telf_movil2 = $("#txtTelefonoMovil2_PropCli").val();
            var email_personal = fc_Trim($("#txtEmailPersonal_PropCli").val());
            var email_trabajo = fc_Trim($("#txtEmailTrabajo_PropCli").val());
            var email_alternativo = fc_Trim($("#txtEmailAlternativo_PropCli").val());
            var anexo_telefono = $("#txtTelefonoFijo_Anexo_PropCli").val();
            var pais_celular = $("#cboPaisTelefonoCel_PropCli").val();
            var pais_fijo = $("#cboPaisTelefonoFijo_PropCli").val();

            var msError = "";

            if (co_tipo_persona == "") {
                msError += "- Seleccione el tipo de persona.\n";
            }
            else if (co_tipo_documento == "") {
                msError += "- Seleccione el tipo de Documento.\n";
            }
            else {
                if (nu_documento == "")
                    msError += "- Ingresar el número de documento.\n";
                else {
                    if (co_tipo_persona == oNatural) {
                        if (co_tipo_documento == oDNI) {
                            if (nu_documento.length < 8) {
                                msError += "- El DNI debe tener 8 dígitos.\n";
                            }
                        }
                    }
                }
                if (co_tipo_persona == oNatural) {
                    if (no_nombre_razon == "")
                        msError += '- Ingresar el Nombre.\n';
                    if (ape_paterno == "")
                        msError += '- Ingresar el Apellido Paterno.\n';
                    if (ape_materno == "")
                        msError += "- Ingresar el Apellido Materno.\n";
                }
                else {
                    if (no_nombre_razon == "")
                        msError += "- Ingresar la Razón Social.\n";
                }
            }

            if (co_tipo == "CLI") {
                if (pais_celular == "" && telf_movil1 != "") { msError += "Si ingresa un número celular debe indicar el pais. \n"; }
                else if (valida_celular(document.getElementById("txtTelefonoMovil1_PropCli"), (pais_celular == "162")) != true) { msError += SRC_Telefono_Movil_Invalido + " \n"; } 
                if (pais_fijo == "" && telf_fijo != "") { msError += "Si ingresa un número de teléfono debe indicar el pais. \n"; }
                else if (valida_telefono(document.getElementById("txtTelefonoFijo_PropCli")) != true) { msError += SRC_Telefono_Fijo_Invalido + "\n"; } 
                if (telf_fijo == "" && telf_movil1 == "") { msError += "Debe ingresar un teléfono fijo o un teléfono celular. \n"; }
            }

            if (valida_email_blacklist(document.getElementById("txtEmailPersonal_PropCli")) != true) { msError += SRC_Email_Invalido + "\n"; }

            if (msError != "") {
                fc_Alert(msError)
            }
            else {
                fc_Confirm(mstrSeguroGrabar, function (res) {
                    if (res == true) {
                        var parametros = new Array();
                        parametros[0] = co_tipo;
                        parametros[1] = nid_propcli;
                        parametros[2] = co_tipo_persona;
                        parametros[3] = co_tipo_documento;
                        parametros[4] = nu_documento;
                        parametros[5] = no_nombre_razon;
                        parametros[6] = ape_paterno;
                        parametros[7] = ape_materno;
                        parametros[8] = telf_fijo;
                        parametros[9] = telf_oficina;
                        parametros[10] = telf_movil1;
                        parametros[11] = telf_movil2;
                        parametros[12] = email_personal;
                        parametros[13] = email_trabajo;
                        parametros[14] = email_alternativo;
                        parametros[15] = co_usuario;
                        parametros[16] = co_usuario_red;
                        parametros[17] = no_estacion_red;
                        parametros[18] = oCita.nu_placa;
                        parametros[19] = nid_usuario;
                        parametros[20] = pais_celular;
                        parametros[21] = pais_fijo;
                        parametros[22] = anexo_telefono;

                        var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                        var strUrlServicio = no_pagina + "/ActualizarPropCli";
                        fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                            var retorno = objResponse[0];
                            var msg_retorno = objResponse[1];
                            if (retorno > 0) {
                                oPropietario = objResponse[2];
                                oCliente = objResponse[3];

                                $("#txtPropietario").val(oPropietario.no_propietario);
                                $("#txtCliente").val(oCliente.no_cliente);
                            }
                            fc_Alert(msg_retorno);
                            fn_Close_ActualizarPropCli();
                        });
                    }
                });
            }
        }
        /*#endregion - Funciones del Modal - Actualizar Propietario/Cliente*/
        /*#region - Funciones del Modal - Historial de Citas*/
        //#region - Variables Grilla Historial Cita
        var idGrilla_HistorialCita = "#grvHistorialCita_HC";
        var idPieGrilla_HistorialCita = "#grvHistorialCita_HC_Pie";
        var strCabecera_HistorialCita = ['#', 'Local', 'Taller', 'Servicio', 'Ultimo Km.', 'Asesor de Servicio', 'Fecha', 'Hora', 'Estado'];
        var ModelCol_HistorialCita = [
            { name: 'Itm', index: 'Itm', width: 30, align: 'center' },
            { name: 'no_ubica', index: 'no_ubica', width: 200 },
            { name: 'no_taller', index: 'no_taller', width: 100, align: 'center' },
            { name: 'no_servicio', index: 'no_servicio', width: 100, align: 'center' },
            { name: 'km_ult_serv', index: 'km_ult_serv', width: 50, align: 'center' },
            { name: 'no_asesor', index: 'no_asesor' },
            { name: 'fe_atencion', index: 'fe_atencion', width: 80, align: 'center' },
            { name: 'ho_cita', index: 'ho_cita', width: 50, align: 'center' },
            { name: 'no_estado', index: 'no_estado', width: 80, align: 'center' }
        ];
        //#endregion - Variables Grilla Historial Cita
        $("#modal_HistorialCita").draggable({
            handle: ".modal-header"
        });
        function fn_Open_HistorialCita() {
            if (fc_Trim(document.getElementById("txtPlaca").value) == "" || oCita.nid_vehiculo <= 0) {
                fc_Alert("Debe de ingresar un número de <%=oPrm.N_Placa %>");
            }
            else {
                var parametros = new Array();
                parametros[0] = fc_Trim($("#txtPlaca").val());
                parametros[1] = nid_usuario;
                var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
                var strUrlServicio = no_pagina + "/Get_HistorialCita";
                fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                    $("#modal_HistorialCita").modal("show");

                    $("#lblPlaca_HC").text(objResponse.oHistorialCita.nu_placa);
                    $("#lblNomTipoDocumento_HC").text(objResponse.oHistorialCita.no_tipo_doc);
                    $("#lblNroDocumento_HC").text(objResponse.oHistorialCita.nu_documento);
                    $("#lblVIN_HC").text($("#txtVIN").val());
                    $("#lblNombres_HC").text(objResponse.oHistorialCita.nombres);
                    $("#lblNombre_RazonSocial_HC").text(objResponse.oHistorialCita.no_cliente);
                    $("#lblMarca_HC").text(objResponse.oHistorialCita.no_marca);
                    $("#lblTelefono_HC").text(objResponse.oHistorialCita.nu_telefono);
                    $("#lblModelo_HC").text(objResponse.oHistorialCita.no_modelo);
                    $("#lblCelular_HC").text(objResponse.oHistorialCita.nu_celular);
                    $("#lblEmail_HC").text(objResponse.oHistorialCita.no_correo);

                    var JQGrid_Opciones_Default_1 = JQGrid_Opciones_Default;
                    JQGrid_Opciones_Default_1.shrinkToFit = false;
                    JQGrid_Opciones_Default_1.height = "auto";
                    JQGrid_Opciones_Default_1.fl_paginar_scroll = false;
                    JQGrid_Util.AutoWidthResponsive(idGrilla_HistorialCita);
                    JQGrid_Util.GetTabla_Local(idGrilla_HistorialCita, idPieGrilla_HistorialCita, strCabecera_HistorialCita, ModelCol_HistorialCita, JQGrid_Opciones_Default_1
                        , objResponse.oHistorialCita.oLista, function () { }, function () { }, function () { });
                });
            }
        }
        function fn_Close_HistorialCita() {
            $("#modal_HistorialCita").modal("hide");
        }
        /*#endregion - Funciones del Modal - Historial de Citas*/

        /*#region - Funciones del Modal - ResumenCita*/
        $("#modal_ResumenCita").draggable({
            handle: ".modal-header"
        });
        function fn_Open_ResumenCita() {
            $("#modal_ResumenCita").modal("show");
        }
        function fn_Close_ResumenCita() {

            if (placa_defecto) {
                var f = document.createElement("form");
                f.setAttribute("method", "post");
                f.setAttribute("action", no_pagina);
                var hiddenfield = document.createElement("input");
                hiddenfield.setAttribute("type", "hidden");
                hiddenfield.setAttribute("name", "loadPopUpContactCenter");
                hiddenfield.setAttribute("value", 1);

                var hiddenfield2 = document.createElement("input");
                hiddenfield2.setAttribute("type", "hidden");
                hiddenfield2.setAttribute("name", "filtros_selected");
                hiddenfield2.setAttribute("value", filtros_selected_contact);

                f.appendChild(hiddenfield);
                f.appendChild(hiddenfield2);
                document.body.appendChild(f);
                f.submit();
                document.body.removeChild(f);
            } else {
                location.href = no_pagina;
            }
        }
        var printWindow;
        function fn_PopupDetalleCita(strHTML) {
            printWindow = window.open("", "mywindow", "location=0,status=0,scrollbars=0,resizable=0,width=600px,height=430px");
            var strContent = "<html><body>";
            strContent = strContent + "<title>Impresión de Resúmen de Cita</title>";
            strContent = strContent + "<script type='text/javascript' language='Javascript'>"
            strContent = strContent + "function imprimir() {window.print();}" //Print
            strContent = strContent + "</" + "script>"
            strContent = strContent + "</head><body>";
            strContent = strContent + strHTML;
            strContent = strContent + "</body>";
            strContent = strContent + "<script language='javascript' type='text/javascript'> imprimir(); </" + "script>";
            strContent = strContent + "</html>";
            //printWindow.moveTo((screen.width-this.width +12)/2,(screen.height-this.height + 28)/2);   
            printWindow.document.write(strContent);
            printWindow.document.close();
            printWindow.focus();
        }
        /*#endregion - Funciones del Modal - ResumenCita*/

        function SoloLetrasNumeros(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser  

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key >= 48 && key <= 57) { }
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú
            else if (key == 225 || key == 223 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key == 8 || key == 9 || key == 46 || key == 37 || key == 39) { }
            else if (key == 209 || key == 241) { } // ñ Ñ
            else {
                return false;
            }
        }
        function SoloLetrasEspacio(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser                  


            if (key >= 65 && key <= 90) { }
            else if (key == 32 || key == 8 || key == 9 || key == 46 || key == 37 || key == 39) { }
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú
            else if (key == 225 || key == 223 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key >= 97 && key <= 122) { }
            else if (key == 209 || key == 241) { } // ñ Ñ
            else {
                return false;
            }
        }
        function SoloPlaca(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser   

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key >= 48 && key <= 57) { }
            //else if (key==45){} //sin guión
            else {
                return false; //anula la entrada de texto. 
            }
        }

        //--------------------------------
        var ventana;
        var cont = 0;
        var titulopordefecto = "Defecto";
        function foto(mapa, titulo) {
            if (cont == 1) { ventana.close(); ventana = null }
            if (titulo == null) titulo = titulopordefecto;
            ventana = window.open('', 'ventana', 'toolbar=no,status=no,location=no,directories=0,menubar=no,scrollbars=no,resizable=0,width=50%,height=50%');
            ventana.document.write('<html><head><title>' + titulo + '</title></head><body style="overflow:hidden" marginwidth="0" marginheight="0" topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" scroll="no" onUnload="opener.cont=0"><img src="' + mapa + '" onLoad="opener.redimensionar(this.width, this.height)">');
            ventana.document.close();
            cont++;
        }
        function redimensionar(ancho, alto) {
            ventana.resizeTo(ancho + 12, alto + 28)
            ventana.moveTo((screen.width - ancho) / 2, (screen.height - alto) / 2)
        }
        //--------------------------------

        function fn_ExistDisplayControl(Id_control) {
            if (Id_control.indexOf("#") >= 0) Id_control = Id_control.replace("#", "");

            if (document.getElementById(Id_control) != null && $("#" + Id_control).is(':visible')) {
                return true;
            }
            else return false;
        }

        //Proximos Turnos
        function fn_ProximosTurnos() {
            //this.oCita.fe_programada = $("#txtFecInicio").val();
            var fe_programada = fc_Trim($("#txtFecInicio").val());
            if (fe_programada != "") {
                $("#divSinProximosTurnos").hide();
                $("#divProximosTurnos").fadeIn();

                $("#txtFecInicio_PT").val(fe_programada);
                $("#txtFecFinal_PT").val(fe_programada);

                var DatePicker_Opciones = DatePicker_Opciones_Default;
                this.fc_FormatFecha("txtFecInicio_PT", DatePicker_Opciones, "", "");
                this.fc_FormatFecha("txtFecFinal_PT", DatePicker_Opciones, "", "");

                fn_GetHorario_ProxTurnos();
            }
            return false;
        }
        function fn_ChangedFecha_PT() {
            fn_GetHorario_ProxTurnos();
        }
        var fl_cambio_x_horario_PT = false;
        $("#cboHoraFinalReserva_PT").change(function () {
            var ho_final = $("#cboHoraInicioReserva_PT").val();
            var ho_inicio = $(this).val();

            if ($("#cboHoraInicioReserva_PT").prop("selectedIndex") >= $("#cboHoraFinalReserva_PT").prop("selectedIndex")) {
                fc_Alert("La hora final debe ser mayor que la hora inicial.");
                return;
            }

            fl_cambio_x_horario_PT = true;
            fn_GetHorario_ProxTurnos();
        });
        $("#cboHoraInicioReserva_PT").change(function () {
            var ho_inicio = $(this).val();
            var ho_final = $("#cboHoraFinalReserva_PT").val();

            if ($("#cboHoraInicioReserva_PT").prop("selectedIndex") >= $("#cboHoraFinalReserva_PT").prop("selectedIndex")) {
                fc_Alert("La hora inicial debe ser menor que la hora final.");
                return;
            }

            fl_cambio_x_horario_PT = true;
            fn_GetHorario_ProxTurnos();
        });
        function fn_SinProximosTurnos() {
            $("#divProximosTurnos").hide();
            $("#divSinProximosTurnos").fadeIn();

            $("#txtFecInicio").val($("#txtFecInicio_PT").val())
            fn_ChangedFecha();
            return false;
        }
        function fn_GetHorario_ProxTurnos() {
            //Set datos de reserva
            $("#divselhora").empty();
            oCita.nid_asesor = 0;
            oCita.ho_inicio = "";
            oCita.qt_intervalo_atenc = "";

            if (fl_cambio_x_horario_PT != true) {
                fc_FillCombo("cboHoraInicioReserva_PT", [], text_Seleccione);
                fc_FillCombo("cboHoraFinalReserva_PT", [], text_Seleccione);
            }
            fl_cambio_x_horario_PT = false;

            $("#divGrvUbicacion").empty();
            $("#div_Leyendas_Pie").hide();

            //Carga Horario de Próximos Turnos
            var parametros = new Array();
            parametros[0] = oCita.nid_modelo;
            parametros[1] = oCita.nid_servicio;
            parametros[2] = $("#cboDepartamento").val();
            parametros[3] = fn_GetValue_Prov();
            parametros[4] = fn_GetValue_Dist();
            parametros[5] = fn_GetValue_Ubicacion();
            parametros[6] = this.oCita.nid_taller;
            parametros[7] = $("#txtFecInicio_PT").val();
            parametros[8] = $("#txtFecFinal_PT").val();
            parametros[9] = $("#cboHoraInicioReserva_PT").val();
            parametros[10] = $("#cboHoraFinalReserva_PT").val();
            parametros[11] = nid_usuario;
            var strParametros = "{'strParametros':" + JSON.stringify(parametros) + "}";
            var strUrlServicio = no_pagina + "/Get_HorarioDisponible_ProxTurnos";
            fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {
                if (objResponse.msg_retorno != "") { fc_Alert(objResponse.msg_retorno); }

                id_img_select = "";
                src_img_select_ant = "";
                if (objResponse.fl_seguir == "1") {
                    if (objResponse.oComboHoraInicio.length > 0) {
                        this.fc_FillCombo("cboHoraInicioReserva_PT", objResponse.oComboHoraInicio, text_Seleccione);
                        this.fc_FillCombo("cboHoraFinalReserva_PT", objResponse.oComboHoraInicio, text_Seleccione);
                        $("#cboHoraInicioReserva_PT option[value='']").remove();
                        $("#cboHoraFinalReserva_PT option[value='']").remove();
                        $("#cboHoraFinalReserva_PT").prop("selectedIndex", $("#cboHoraFinalReserva_PT option").length - 1);
                    }

                    $("#divGrvUbicacion").empty();
                    $("#divGrvUbicacion").append('<table id="grvProxTurnos"></table><div id="grvProxTurnos_Pie"></div>');
                    $("#div_Leyendas_Pie").show();

                    var idGrilla_ProxTurnos = "#grvProxTurnos";
                    var idPieGrilla_ProxTurnos = "#grvProxTurnos_Pie";

                    var JQGrid_Opciones_Default_1 = JQGrid_Opciones_Default;
                    JQGrid_Opciones_Default_1.shrinkToFit = false;
                    JQGrid_Opciones_Default_1.height = 200;
                    JQGrid_Opciones_Default_1.fl_paginar_scroll = true;
                    JQGrid_Util.AutoWidthResponsive(idGrilla_ProxTurnos);
                    JQGrid_Util.GetTabla_Local(idGrilla_ProxTurnos, idPieGrilla_ProxTurnos
                        , objResponse.oHorario_Cabecera, objResponse.oHorario_ModelCol, JQGrid_Opciones_Default_1
                        , objResponse.oHorarioDisponible, function () { }, function () { }, function () { });
                    jQuery("#grvProxTurnos").jqGrid('filterToolbar',
                        {
                            searchOperators: true
                            , afterSearch: function () {
                                $("#divselhora").empty();
                                oCita.nid_asesor = 0;
                                oCita.ho_inicio = "";
                                oCita.qt_intervalo_atenc = "";
                                //--
                                id_img_select = "";
                                src_img_select_ant = "";
                            }
                        }
                    );
                    $("#gs_ho_cita > option").each(function () {
                        this.text = this.text.replace(".", ":");
                        this.value = this.value.replace(".", ":");
                    });

                }
                else if (objResponse.fl_seguir == "0") {
                    $("#divGrvUbicacion").empty();
                    $("#divGrvUbicacion").append(strDivSinHorario);
                    $("#divSinHorario").text(objResponse.msg_retorno);
                    $("#div_Leyendas_Pie").hide();
                }
            });
        }
        function fn_SetHoraAsesor_ProxTurnos(RowID, no_columna, id_img, keys) {
            if (id_img != id_img_select) {
                var src_img = $("img[idfoo='" + id_img + "']").attr("src");
                if (src_img == imgURL_Hora_Reservada) {
                    this.oCita.tipo_registro = "CE";
                }
                else {
                    this.oCita.tipo_registro = "REG";
                }

                $("img[idfoo='" + id_img + "']").attr("src", imgURL_Hora_Separada);
                if (id_img_select != "") {
                    $("img[idfoo='" + id_img_select + "']").attr("src", src_img_select_ant);
                }
                id_img_select = id_img;
                src_img_select_ant = src_img;

                var nid_asesor = keys.split("$")[0];
                var ho_inicio = keys.split("$")[1];
                var qt_intervalo_atenc = keys.split("$")[2];
                var strSeleccion = keys.split("$")[3];
                var nid_taller = keys.split("$")[4];
                var sfe_programada = keys.split("$")[5];

                $("#divselhora").text(strSeleccion);
                //Set datos de reserva
                this.oCita.nid_asesor = nid_asesor;
                this.oCita.ho_inicio = no_columna;
                this.oCita.qt_intervalo_atenc = qt_intervalo_atenc;
                this.oCita.nid_taller = nid_taller;
                this.oCita.fe_programada = sfe_programada;
            }
        }

        function FC_CargarPlacaPorDefecto(placa, filtros_selected) {
            placa_defecto = true;
            filtros_selected_contact = filtros_selected;
        }

        function fc_BuscarVehiculoDefecto(placa) {
            $("#txtPlaca").val(placa);
            setTimeout(fc_BuscarVehiculoDef, 500);
        }
        function fc_BuscarVehiculoDef() {
            fn_GetVehiculo($("#txtPlaca").val());
        }

        function fn_EditCellGrid(e) {
            jQuery("#grvBandeja").editCell(0, 0, false);
        }

    </script>

</asp:Content>

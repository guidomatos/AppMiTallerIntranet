<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="Citas_Atendidas_Por_Asesor.aspx.cs" Inherits="SRC_Reportes_Citas_Atendidas_Por_Asesor" Async="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Import Namespace="AppMiTaller.Intranet.BE" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading clearfix">
                <div class="pull-left">
                    <i class="glyphicon-more_windows"></i><span class="SpanCabecera">&nbsp;&nbsp;Reporte de Citas Atendidas por Asesor</span>
                </div>
                <div class="pull-right">
                </div>
            </div>
            <div class="clearfix">
            </div>
            <div class="panel-body">
                <div class="box">
                    <div class="box-content">
                        <div class="row">
                            <div id="chartdiv">

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Styles -->
    <style>
    #chartdiv {
      width: 90%;
      max-height: 500px;
      height: 450px;
    }
    </style>
    
    <!-- Resources -->
    <script type="text/javascript" src="../js/amcharts/core.js"></script>
    <script type="text/javascript" src="../js/amcharts/charts.js"></script>
    <script type="text/javascript" src="../js/amcharts/animated.js"></script>

    <script type="text/javascript">

        var no_pagina = window.location.pathname;

        fc_GetDataReporte();

        function fc_GetDataReporte() {

            var strParametros = "{ }";
            var strUrlServicio = no_pagina + "/GetDataReporte";
            this.fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {

                arrayDataReporte = objResponse.listaDataReporte;

                fc_VerReporte(arrayDataReporte);

            });
        }

        function fc_VerReporte(arrayDataReporte) {

            am4core.useTheme(am4themes_animated);

            var chart = am4core.create("chartdiv", am4charts.PieChart);

            chart.data = arrayDataReporte;

            var series = chart.series.push(new am4charts.PieSeries());
            series.dataFields.value = "qt_cita";
            series.dataFields.category = "no_asesor";

            // this creates initial animation
            series.hiddenState.properties.opacity = 1;
            series.hiddenState.properties.endAngle = -90;
            series.hiddenState.properties.startAngle = -90;

            chart.legend = new am4charts.Legend();
        }

    </script>

</asp:Content>
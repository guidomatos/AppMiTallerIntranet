<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="Citas_Atendidas_Por_Marca.aspx.cs" Inherits="SRC_Reportes_Citas_Atendidas_Por_Marca" Async="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading clearfix">
                <div class="pull-left">
                    <i class="glyphicon-more_windows"></i><span class="SpanCabecera">&nbsp;&nbsp;Reporte de Citas Atendidas por Marca</span>
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
        var nid_usuario = "<%=this.Profile.Usuario.Nid_usuario %>";

        fc_GetDataReporte();

        function fc_GetDataReporte() {

            var strParametros = "{ nid_usuario: " + nid_usuario + "}";
            var strUrlServicio = no_pagina + "/GetDataReporte";
            this.fc_getJsonAjax(strParametros, strUrlServicio, function (objResponse) {

                arrayDataReporte = objResponse.listaDataReporte;

                fc_VerReporte(arrayDataReporte);
                
            });
        }

        function fc_VerReporte(arrayDataReporte) {

            am4core.useTheme(am4themes_animated);

            var chart = am4core.create("chartdiv", am4charts.XYChart);

            chart.data = arrayDataReporte;
            chart.padding(40, 40, 40, 40);

            var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
            categoryAxis.renderer.grid.template.location = 0;
            //categoryAxis.dataFields.category = "country";
            categoryAxis.dataFields.category = "no_marca";
            categoryAxis.renderer.minGridDistance = 60;

            var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

            var series = chart.series.push(new am4charts.ColumnSeries());
            //series.dataFields.categoryX = "country";
            //series.dataFields.valueY = "visits";
            series.dataFields.categoryX = "no_marca";
            series.dataFields.valueY = "qt_cita";
            series.tooltipText = "{valueY.value}"
            series.columns.template.strokeOpacity = 0;

            chart.cursor = new am4charts.XYCursor();

            // as by default columns of the same series are of the same color, we add adapter which takes colors from chart.colors color set
            series.columns.template.adapter.add("fill", function (fill, target) {
                return chart.colors.getIndex(target.dataItem.index);
            });

        }

    </script>

</asp:Content>

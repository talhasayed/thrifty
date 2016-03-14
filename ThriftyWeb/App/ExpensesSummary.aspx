<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExpensesSummary.aspx.cs" Inherits="ThriftyWeb.App.ExpensesSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h1>Graphs</h1>


    <br/>
    <br/>

    <h3>Monthly Expense Summary</h3>
    <div style="display: none">
        <asp:Chart ID="Chart1" runat="server" Height="343px" Width="889px" EnableViewState="True" OnCustomize="Chart1_OnCustomize">
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart></div>


    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <div style="width: 1000px; height: 400px; text-align: left">
    <div id="chart_div" style="height: 400px; width: 1500px; float: left; position: relative; left: -150px;"></div>
        <div style="clear: both"></div>
        </div>
    <script>
        google.charts.load('current', { packages: ['corechart', 'bar'] });
        google.charts.setOnLoadCallback(drawBasic);

        function drawBasic() {


            var formatterMoney = new google.visualization.NumberFormat({ suffix: ' KD', decimalSymbol: '.', groupingSymbol: ' ' });
            var formatterDate = new google.visualization.DateFormat({ pattern: 'dd-MMM' });

            var data = new google.visualization.DataTable();

            data.addColumn('string', 'order date'); //used to be date field here
            data.addColumn('number', 'Amount');

            <%= getJson() %>

            var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            formatterMoney.format(data, 1);
            chart.draw(data, { width: '1000px', height: '400px', legend: 'none'});
        }


    </script>

</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ThriftyWeb.App.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        .ui-datepicker { font-size: 12px; }
    </style>


    <h1>Dashboard</h1>


    <table style="width: 100%">
        <tr>
            <td colspan="2">
                Select period:
                <br/>
                <br/>
                Start Date:
                &nbsp;
                &nbsp;
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="datepicker_from" ClientIDMode="Static"></asp:TextBox>
                &nbsp;
                &nbsp;
                &nbsp;

                End Date:
                &nbsp;
                &nbsp;
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="datepicker_from" ClientIDMode="Static"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <h3>Expenses</h3>
                <asp:GridView ID="gvExpenses" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField HeaderText="Account" DataField="AccountName"/>
                        <asp:BoundField HeaderText="Amount" DataField="AbsDebits"/>
                    </Columns>
                </asp:GridView>
            </td>
            <td>
                <h3>Incomes</h3>

            </td>
        </tr>
    </table>


    <script>

        $(function() {
            $("#txtStartDate").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 3,
                dateFormat: 'dd/mm/yy',
                onClose: function(selectedDate) {
                    $("#txtEndDate").datepicker("option", "minDate", selectedDate);
                }
            });


            $("#txtEndDate").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 3,
                dateFormat: 'dd/mm/yy',
                onClose: function(selectedDate) {
                    $("#txtStartDate").datepicker("option", "maxDate", selectedDate);
                }
            });
        });


    </script>

</asp:Content>
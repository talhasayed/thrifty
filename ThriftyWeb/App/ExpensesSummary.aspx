<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExpensesSummary.aspx.cs" Inherits="ThriftyWeb.App.ExpensesSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h1>Graphs</h1>


    <br/>
    <br/>
    
    <h3>Monthly Expense Summary</h3>

    <asp:Chart ID="Chart1" runat="server" Height="343px" Width="889px" EnableViewState="True" OnCustomize="Chart1_OnCustomize">
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>


</asp:Content>
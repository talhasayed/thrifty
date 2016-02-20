<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExpensesSummary.aspx.cs" Inherits="ThriftyWeb.App.ExpensesSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h1>Graphs</h1>


    <br/>
    <br/>

    <asp:Chart ID="Chart1" runat="server" Height="343px" Width="889px" EnableViewState="True">
        <Series>
            <asp:Series ChartType="Area" Name="Series1" YValuesPerPoint="3">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>


</asp:Content>
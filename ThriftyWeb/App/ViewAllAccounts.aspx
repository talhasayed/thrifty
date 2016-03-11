<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAllAccounts.aspx.cs" Inherits="ThriftyWeb.App.ViewAllAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div style="height: 150px;">&nbsp;</div>


    <div>
        <style>
            .account-table {
                width: 800px;
                table-layout: fixed;
            }
            .account-table th {
                font-weight: bold;
                background-color: #dbe2f8;
            }
        </style>
        
        
        <asp:Literal ID="ltAccounts" runat="server" Mode="PassThrough"></asp:Literal>
        
        
        <asp:PlaceHolder ID="phAccounts" runat="server"></asp:PlaceHolder>
        
        
        
        
        
        
        
        
        
        

    </div>


</asp:Content>
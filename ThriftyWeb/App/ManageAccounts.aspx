<%@ Page Title="Manage Accounts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageAccounts.aspx.cs" Inherits="ThriftyWeb.App.ManageAccounts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <style>
        .accounts-input-table td:first-child {
            font-weight: bold;
        }
    </style>
    
    
    
    
    <h3>Manage Accounts</h3>


    <span style="font-size: 15px;"><asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label></span>
    

    <table class="table table-bordered accounts-input-table" style="width: 400px; margin-top: 20px;">
        <tr>
            <td style="width: 100px;">
                Account Name:
            </td>
            <td>
                <asp:TextBox ID="txtAccountName" runat="server" CssClass="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Account Category
            </td>
            <td>
                <asp:DropDownList ID="ddlAccountCategory" runat="server" Width="150px"></asp:DropDownList>
                
                <script>
                    $(function() {
                        $('#<%= ddlAccountCategory.ClientID%>').selectmenu();
                    });
                </script>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" OnClick="btnSave_OnClick" Text="Save" CssClass="btn btn-default btn-primary" />
            </td>
        </tr>
    </table>
    
    

</asp:Content>

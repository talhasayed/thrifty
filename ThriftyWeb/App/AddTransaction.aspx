<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTransaction.aspx.cs" Inherits="ThriftyWeb.App.AddTransaction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        
        <h2> Add Transaction</h2>
        
        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="False"></asp:Label>

        <table>
            <tr>
                <td>Transaction Description</td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Width="250px"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Debit Account</td>
                <td>
                    <asp:TextBox ID="txtDebitAccount" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Credit Account</td>
                <td>
                    <asp:TextBox ID="txtCreditAccount" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Amount</td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
        </table>

        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_OnClick"/>

        
        
        
        
        


    </div>
</form>
</body>
</html>
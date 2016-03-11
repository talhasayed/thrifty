<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="ThriftyWeb.App.Manage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
        
        <asp:TextBox ID="txtAccountName" runat="server"></asp:TextBox>
        <asp:DropDownList ID="ddlAccountCategory" runat="server"></asp:DropDownList>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="btnClearData" runat="server" Text="Clear Data" OnClick="btnClearData_OnClick" />
        

        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="btnAddSampleData" runat="server" Text="Add Sample Data" OnClick="btnAddSampleData_Click" />


        <br/>
        <br/>
        <br/>
        <br/>
        <asp:Button ID="btnProcess" runat="server" Text="Custom Process" OnClick="btnProcess_OnClick" />
        

    </div>
    </form>
</body>
</html>

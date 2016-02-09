<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAccounts.aspx.cs" Inherits="ThriftyWeb.App.ViewAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:DropDownList ID="ddlAccounts" runat="server" OnSelectedIndexChanged="ddlAccounts_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>


    <asp:GridView ID="gvAccountData" runat="server" ItemType="ThriftyWeb.Models.TransactionLeg" AutoGenerateColumns="False" OnRowDataBound="gvAccountData_OnDataBound" ShowFooter="True">
        <Columns>
            <asp:BoundField DataField="TransactionLegType.Type" HeaderText="Transaction Type"/>
            <asp:TemplateField HeaderText="Debit">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Visible='<%# ((string) Eval("TransactionLegType.Type") == "DEBIT") %>' Text='<%# Eval("Amount") %>'/>
                </ItemTemplate>
                <FooterTemplate>
                    <b>
                        <asp:Label ID="lblTotalDebits" runat="server" Text="Label"></asp:Label>
                    </b>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Credit">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Visible='<%# ((string) Eval("TransactionLegType.Type") == "CREDIT") %>' Text='<%# Eval("Amount") %>'/>
                </ItemTemplate>
                <FooterTemplate>
                    <b>
                        <asp:Label ID="lblTotalCredits" runat="server" Text="Label"></asp:Label>
                    </b>
                </FooterTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</asp:Content>
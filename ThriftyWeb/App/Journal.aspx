<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Journal.aspx.cs" Inherits="ThriftyWeb.App.Journal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        thead th {
            font-weight: bold;
            font-size: 16px;
            text-align: center;
        }

        .debitCol { width: 100px; }

        table { font-size: 16px; }
    </style>


    <div style="width: 800px; margin: 30px;">

        <h1 style="text-align: center">JOURNAL</h1>


        <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:BoundField DataField="TransactionDate" DataFormatString="{0: MMM dd, yyyy}" HeaderText="DATE" ItemStyle-Width="150px"/>
                <asp:TemplateField HeaderText="PARTICULARS">
                    <ItemTemplate>
                        <div style="float: right;">
                            Dr.
                        </div>
                        <asp:Literal runat="server" Text=' <%# Eval("DebitAccount") %>'></asp:Literal>


                        <br/>

                        &nbsp;
                        &nbsp;
                        &nbsp;
                        &nbsp;

                        To


                        <asp:Literal runat="server" Text=' <%# Eval("CreditAccount") %>'></asp:Literal>

                        <br/>

                        (<asp:Literal runat="server" Text=' <%# Eval("TransactionDescription") %>'></asp:Literal>)


                    </ItemTemplate>


                </asp:TemplateField>
                <asp:TemplateField HeaderText="DEBIT">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CREDIT">
                    <ItemTemplate>
                        <br/>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>


</asp:Content>
<%@ Page Title="Balance Sheet" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BalanceSheet.aspx.cs" Inherits="ThriftyWeb.App.BalanceSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        .account-table th {
            font-weight: bold;
            background-color: #dbe2f8;
            font-size: 16px;
        }

        .highlighted {
            font-weight: bold;
        }

        .account-table td:nth-child(2) {
            text-align: right;
        }
    </style>

    <h1>Balance Sheet</h1>

    <table style="margin-top: 10px; width: 1170px; table-layout: fixed">
        <tr>
            <td style="width: 49%">
                <h2>Assets</h2>

                <asp:GridView ID="gvAssets" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered account-table">
                    <Columns>
                        <asp:BoundField HeaderText="Account" DataField="AccountName" />
                        <asp:BoundField HeaderText="Amount" DataField="AbsDebits">
                            <HeaderStyle Width="120px"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>

            </td>
            <td style="width: 2%;">&nbsp;
            </td>
            <td style="width: 49%">
                <h2>Liabilities</h2>


                <asp:GridView ID="gvLiabilities" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered account-table">
                    <Columns>
                        <asp:BoundField HeaderText="Account" DataField="AccountName" />
                        <asp:BoundField HeaderText="Amount" DataField="AbsCredits">
                            <HeaderStyle Width="120px"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>

            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr />

                <style>
                    .balance-summary-table {
                        
                    }

                     .balance-summary-table td:first-child {
                         font-size: 18px;
                         /*font-weight: bold;*/
                     }

                    .balance-summary-table td:nth-child(2) {
                        font-size: 18px;
                        text-align: right;
                        /*font-weight: bold;*/
                    }

                    .balance-summary-table tr:last-child td {
                        border-top: 2px solid;
                        border-bottom: 2px solid;
                        padding: 10px;
                    }
                </style>

                <table class="table table-bordered balance-summary-table" style="width: 300px">
                    <tr>
                        <td>Balance:&nbsp;</td>
                        <td>
                            <asp:Label ID="lblBalance" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Current Expenses:&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCurrentExpenses" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Acutal Balance:&nbsp;</td>
                        <td>
                            <asp:Label ID="lblActualBalance" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                </table>





            </td>
        </tr>
    </table>





</asp:Content>

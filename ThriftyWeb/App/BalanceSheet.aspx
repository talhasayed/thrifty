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
    </table>





</asp:Content>

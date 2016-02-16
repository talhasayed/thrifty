<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ThriftyWeb.App.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        .ui-datepicker { font-size: 12px; }
    </style>


    <h1>Dashboard</h1>


    <table style="width: 100%">
        <tr>
            <td colspan="2" class="form-inline">
                Select period:
                <br/>
                <br/>

                <div class="col-md-8">

                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="datepicker_from form-control input-sm" ClientIDMode="Static" placeholder="Start Date"></asp:TextBox>
                    to
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="datepicker_from form-control input-sm" ClientIDMode="Static" placeholder="End Date"></asp:TextBox>

                </div>



                <div class="col-md-4">
                     <asp:LinkButton ID="CurrentMonth" CssClass="btn btn-primary btn-sm" runat="server" OnCommand="CurrentMonth_OnClick" CommandName="ShowCurrentMonth">
                    <span class="glyphicon glyphicon-cog"></span></asp:LinkButton>
                </div>


               
            </td>
        </tr>
        <tr>
            <td colspan="2">

                <div class="col-md-6">
                    <h3>Expenses</h3>
                    <asp:GridView ID="gvExpenses" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" ShowFooter="true">
                        <Columns>
                            <asp:BoundField HeaderText="Account" DataField="AccountName"/>
                            <asp:TemplateField HeaderText="Amount">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AbsDebits") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("AbsDebits") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalDebits" runat="server" ></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-md-6">
                    <h3>Incomes</h3>
                </div>


            </td>
        </tr>
    </table>


    <script>

        $(function() {
            $("#txtStartDate").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 3,
                dateFormat: 'dd/mm/yy',
                onClose: function(selectedDate) {
                    $("#txtEndDate").datepicker("option", "minDate", selectedDate);
                }
            });


            $("#txtEndDate").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 3,
                dateFormat: 'dd/mm/yy',
                onClose: function(selectedDate) {
                    $("#txtStartDate").datepicker("option", "maxDate", selectedDate);
                }
            });
        });


    </script>

</asp:Content>
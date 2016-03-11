<%@ Page Title="Journal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Journal.aspx.cs" Inherits="ThriftyWeb.App.Journal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        thead th {
            font-weight: bold;
            font-size: 16px;
            text-align: center;
        }

        .debitCol {
            width: 100px;
        }

        table {
            font-size: 16px;
        }
    </style>


    <div style="width: 800px; margin: 30px;">

        <h1 style="text-align: center">JOURNAL</h1>


        <br />

        <div class="clearfix">
            <div class="col-md-6 form-group">

                <div class="col-sm-5">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="datepicker_from form-control input-sm " ClientIDMode="Static" placeholder="Start Date"></asp:TextBox>
                </div>
                <div class="col-sm-1" style="text-align: center; line-height: 30px;">
                    <label class="control-label">to</label>
                </div>

                <div class="col-sm-5">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="datepicker_from form-control input-sm col-sm-2" ClientIDMode="Static" placeholder="End Date"></asp:TextBox>
                </div>
            </div>


            <div class="col col-md-6 form-group col-md-pull-1">
                <asp:LinkButton ID="lnkRefresh" CssClass="btn btn-primary btn-sm" runat="server" OnCommand="CommandsHandler" CommandName="Refresh" Text="Refresh">
                    <span class="glyphicon glyphicon-refresh"></span>
                </asp:LinkButton>

                <asp:LinkButton ID="lnkCurrentMonth" CssClass="btn btn-primary btn-sm" runat="server" OnCommand="CommandsHandler" CommandName="ShowCurrentMonth" Text="Current Month"></asp:LinkButton>

                &nbsp;
                <asp:CheckBox ID="chkShowOnlyExpenses" runat="server" AutoPostBack="True" Text="Show only expenses" OnCheckedChanged="chkShowOnlyExpenses_OnCheckedChanged" />

            </div>
        </div>

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


        <% if (chkShowOnlyExpenses.Checked)
           { %>
        <div style="font-size: 16px;">
            <span>Total Expense for current selection:</span>
            <span style="font-weight: bold">
                <asp:Label ID="lblExpenseForDuration" runat="server" EnableViewState="False"></asp:Label>
                KWD</span>
        </div>
        <% } %>
        <br />

        <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" AllowPaging="True" OnPageIndexChanging="gvTransactions_OnPageIndexChanging">
            <Columns>
                <asp:BoundField DataField="TransactionDate" DataFormatString="{0: MMM dd, yyyy}" HeaderText="DATE" ItemStyle-Width="150px" />
                <asp:TemplateField HeaderText="PARTICULARS">
                    <ItemTemplate>
                        <asp:Literal runat="server" Text=' <%# Eval("DebitAccount") %>'></asp:Literal>

                        Dr.
                        
                        To


                        <asp:Literal runat="server" Text=' <%# Eval("CreditAccount") %>'></asp:Literal>

                        <br />

                        (<asp:Literal runat="server" Text=' <%# Eval("TransactionDescription") %>'></asp:Literal>)


                    </ItemTemplate>


                </asp:TemplateField>
                <asp:TemplateField HeaderText="AMOUNT">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>


</asp:Content>

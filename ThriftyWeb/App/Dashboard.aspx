<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ThriftyWeb.App.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



<%-- To persist the selected tab --%>
<asp:HiddenField ID="hdnSelectedTransactionTab" runat="server" Value="tabExpenses" ClientIDMode="Static"/>

<script>


    $(function() {
        $("#tabExpenses").click(function() {
            $("#hdnSelectedTransactionTab").val("tabExpenses");
        });

        $("#tabOtherTransaction").click(function() {
            $("#hdnSelectedTransactionTab").val("tabOtherTransaction");
        });


        var activeTab = "#" + $("#hdnSelectedTransactionTab").val();

        $(activeTab).tab("show");

    });


</script>

<style>
    .ui-datepicker { font-size: 12px; }


    table thead th { font-weight: bold; }

    table tfoot { font-weight: bold; }
</style>


<h1>Dashboard</h1>


<table style="width: 100%">
    <tr>
        <td colspan="2" class="form-inline">
            Select period:
            <br/>
            <br/>

            <div class="col-md-8 form-group">

                <div class="col-sm-3">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="datepicker_from form-control input-sm " ClientIDMode="Static" placeholder="Start Date"></asp:TextBox>
                </div>
                <div class="col-sm-1" style="text-align: center; line-height: 30px;">
                    <label class="control-label">to</label>
                </div>

                <div class="col-sm-3">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="datepicker_from form-control input-sm col-sm-2" ClientIDMode="Static" placeholder="End Date"></asp:TextBox>
                </div>
            </div>


            <div class="col col-md-4 form-group pull-left col-md-pull-3">
                <div class="col-sm-10">
                    <asp:LinkButton ID="lnkRefresh" CssClass="btn btn-primary btn-sm" runat="server" OnCommand="CommandsHandler" CommandName="Refresh" Text="Refresh">
                        <span class="glyphicon glyphicon-refresh"></span>
                    </asp:LinkButton>

                    <asp:LinkButton ID="lnkCurrentMonth" CssClass="btn btn-primary btn-sm" runat="server" OnCommand="CommandsHandler" CommandName="ShowCurrentMonth" Text="Current Month"></asp:LinkButton>
                </div>
            </div>


        </td>
    </tr>
    <tr>
        <td colspan="2">

            <div class="col-md-6">
                <h3>Expenses Summary</h3>
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
                                <asp:Label ID="lblTotalDebits" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


                <br/>

                <asp:DropDownList ID="ddlChartType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChartType_OnSelectedIndexChanged">
                    <Items>
                        <asp:ListItem Text="Pie"></asp:ListItem>
                        <asp:ListItem Text="Doughnut"></asp:ListItem>
                        <asp:ListItem Text="Column"></asp:ListItem>
                    </Items>
                </asp:DropDownList>

                <br/>

                <asp:Chart ID="Chart1" runat="server" EnableViewState="True">
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>


            </div>

            <div class="col-md-6">
                <h3>Add Transaction</h3>

                <ul class="nav nav-tabs">
                    <li class="active">
                        <a id="tabExpenses" data-toggle="tab" href="#tab1">Expenses Transaction</a>
                    </li>
                    <li>
                        <a id="tabOtherTransaction" data-toggle="tab" href="#tab2">Other Transaction</a>
                    </li>
                </ul>


                <div class="tab-content">
                    <div id="tab1" class="tab-pane active panel">
                        <h3>HOME</h3>
                        <p>Some content.</p>
                    </div>
                    <div id="tab2" class="tab-pane ">

                        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="False"></asp:Label>


                        <div class="form-horizontal" role="form">
                            <div class="form-group">

                                <div class="col col-sm-9">
                                    <%--<label class="control-label" for="txtDescription">Transaction Description:</label>--%>
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control input-sm" placeholder="Transaction Description" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col col-sm-3">
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control input-sm" placeholder="Amount" ClientIDMode="Static"></asp:TextBox>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-lg-5">
                                    <asp:TextBox ID="txtDebitAccount" runat="server" CssClass="form-control input-sm" placeholder="Dr. Account"></asp:TextBox>

                                </div>
                                <div class="col-lg-1">
                                    to
                                </div>
                                <div class="col-lg-5">
                                    <asp:TextBox ID="txtCreditAccount" runat="server" CssClass="form-control input-sm" placeholder="Cr. Account"></asp:TextBox>
                                </div>
                                <div class="col-lg-1" style="position: relative; left: -15px;">
                                    <asp:LinkButton ID="lnkSubmitOtherTrasaction" CssClass="btn btn-primary btn-sm" runat="server" OnCommand="btnSubmit_OnClick" CommandName="SubmitOtherTransaction">
                                        <span class="fa fa-floppy-o" style="font-size: 15px;"></span>
                                    </asp:LinkButton>

                                </div>
                            </div>
                        </div>


                    </div>
                </div>

                <style>
                    .ui-autocomplete {
                        max-height: 100px;
                        overflow-y: auto;
                        /* prevent horizontal scrollbar */
                        overflow-x: hidden;
                    }
                </style>

                <div class="ui-widget">
                    <label>Your preferred programming language: </label>
                    <select id="combobox">
                        <option value="">Select one...</option>
                        <option value="ActionScript">ActionScript</option>
                        <option value="AppleScript">AppleScript</option>
                        <option value="Asp">Asp</option>
                        <option value="BASIC">BASIC</option>
                        <option value="C">C</option>
                        <option value="C++">C++</option>
                        <option value="Clojure">Clojure</option>
                        <option value="COBOL">COBOL</option>
                        <option value="ColdFusion">ColdFusion</option>
                        <option value="Erlang">Erlang</option>
                        <option value="Fortran">Fortran</option>
                        <option value="Groovy">Groovy</option>
                        <option value="Haskell">Haskell</option>
                        <option value="Java">Java</option>
                        <option value="JavaScript">JavaScript</option>
                        <option value="Lisp">Lisp</option>
                        <option value="Perl">Perl</option>
                        <option value="PHP">PHP</option>
                        <option value="Python">Python</option>
                        <option value="Ruby">Ruby</option>
                        <option value="Scala">Scala</option>
                        <option value="Scheme">Scheme</option>
                    </select>
                </div>

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


<style>
    .custom-combobox {
        position: relative;
        display: inline-block;
    }

    .custom-combobox-toggle {
        position: absolute;
        top: 0;
        bottom: 0;
        margin-left: -1px;
        padding: 0;
    }

    .custom-combobox-input {
        margin: 0;
        padding: 5px 10px;
    }
</style>
<script>
    (function($) {
        $.widget("custom.combobox", {
            _create: function() {
                this.wrapper = $("<span>")
                    .addClass("custom-combobox")
                    .insertAfter(this.element);

                this.element.hide();
                this._createAutocomplete();
                this._createShowAllButton();
            },

            _createAutocomplete: function() {
                var selected = this.element.children(":selected"),
                    value = selected.val() ? selected.text() : "";

                this.input = $("<input>")
                    .appendTo(this.wrapper)
                    .val(value)
                    .attr("title", "")
                    .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
                    .autocomplete({
                        delay: 0,
                        minLength: 0,
                        source: $.proxy(this, "_source")
                    })
                    .tooltip({
                        tooltipClass: "ui-state-highlight"
                    });

                this._on(this.input, {
                    autocompleteselect: function(event, ui) {
                        ui.item.option.selected = true;
                        this._trigger("select", event, {
                            item: ui.item.option
                        });
                    },

                    autocompletechange: "_removeIfInvalid"
                });
            },

            _createShowAllButton: function() {
                var input = this.input,
                    wasOpen = false;

                $("<a>")
                    .attr("tabIndex", -1)
                    .attr("title", "Show All Items")
                    .tooltip()
                    .appendTo(this.wrapper)
                    .button({
                        icons: {
                            primary: "ui-icon-triangle-1-s"
                        },
                        text: false
                    })
                    .removeClass("ui-corner-all")
                    .addClass("custom-combobox-toggle ui-corner-right")
                    .mousedown(function() {
                        wasOpen = input.autocomplete("widget").is(":visible");
                    })
                    .click(function() {
                        input.focus();

                        // Close if already visible
                        if (wasOpen) {
                            return;
                        }

                        // Pass empty string as value to search for, displaying all results
                        input.autocomplete("search", "");
                    });
            },

            _source: function(request, response) {
                var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                response(this.element.children("option").map(function() {
                    var text = $(this).text();
                    if (this.value && (!request.term || matcher.test(text)))
                        return {
                            label: text,
                            value: text,
                            option: this
                        };
                }));
            },

            _removeIfInvalid: function(event, ui) {

                // Selected an item, nothing to do
                if (ui.item) {
                    return;
                }

                // Search for a match (case-insensitive)
                var value = this.input.val(),
                    valueLowerCase = value.toLowerCase(),
                    valid = false;
                this.element.children("option").each(function() {
                    if ($(this).text().toLowerCase() === valueLowerCase) {
                        this.selected = valid = true;
                        return false;
                    }
                });

                // Found a match, nothing to do
                if (valid) {
                    return;
                }

                // Remove invalid value
                this.input
                    .val("")
                    .attr("title", value + " didn't match any item")
                    .tooltip("open");
                this.element.val("");
                this._delay(function() {
                    this.input.tooltip("close").attr("title", "");
                }, 2500);
                this.input.autocomplete("instance").term = "";
            },

            _destroy: function() {
                this.wrapper.remove();
                this.element.show();
            }
        });
    })(jQuery);

    $(function() {
        $("#combobox").combobox();

    });
</script>

<script>
    $(function() {
        $("#combobox").combobox();

    });
</script>


</asp:Content>
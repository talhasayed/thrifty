using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using ThriftyWeb.Models;

namespace ThriftyWeb.App
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadInformation();

                LoadDDL();
            }
        }

        private void LoadDDL()
        {
            using (var ctx = new ApplicationDbContext())
            {
                // For the general transaction
                var accounts = ctx.Accounts.Select(x => x.AccountName).ToList();

                ddlDebitAccount.DataSource = accounts;
                ddlDebitAccount.DataBind();
                ddlDebitAccount.Items.Insert(0, new ListItem("Dr. Account", "-1"));

                ddlCreditAccount.DataSource = accounts;
                ddlCreditAccount.DataBind();
                ddlCreditAccount.Items.Insert(0, new ListItem("Cr. Account", "-1"));


                // For the expenses transaction

                var expAccounts =
                    ctx.Accounts.Where(
                        x => x.AccountCategory == AccountCategory.Nominal && x.AccountName.Contains("exp"))
                        .Select(x => x.AccountName)
                        .ToList();

                ddlDebitAccountExpenses.DataSource = expAccounts;
                ddlDebitAccountExpenses.DataBind();
                ddlDebitAccountExpenses.Items.Insert(0, new ListItem("Expense Account", "-1"));


                ddlCreditAccountExpenses.DataSource =
                    ctx.Accounts.Where(x => x.AccountCategory == AccountCategory.Real)
                        .Select(x => x.AccountName)
                        .ToList();
                ddlCreditAccountExpenses.DataBind();
                ddlCreditAccountExpenses.Items.Insert(0, new ListItem("Credit Account", "-1"));
            }
        }


        protected void PageCommands()
        {
        }

        protected void CommandsHandler(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "ShowCurrentMonth")
            {
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                txtStartDate.Text = firstDayOfMonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtEndDate.Text = lastDayOfMonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);


                LoadInformation();
            }
            else if (e.CommandName == "Refresh")
            {
                LoadInformation();
            }
        }

        protected void ddlChartType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var content = (SeriesChartType) Enum.Parse(typeof (SeriesChartType), ddlChartType.SelectedValue);


            Chart1.Series[0].ChartType = content;

            LoadInformation();
        }

        private void LoadInformation()
        {
            if (!Page.IsPostBack)
            {
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                txtStartDate.Text = firstDayOfMonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtEndDate.Text = lastDayOfMonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }


            using (var ctx = new ApplicationDbContext())
            {
                var minDate = DateTime.MinValue;
                var maxDate = DateTime.MaxValue;

                DateTime tempDateTime;

                if (DateTime.TryParseExact(txtStartDate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None,
                    out tempDateTime))
                {
                    minDate = tempDateTime;
                }

                if (DateTime.TryParseExact(txtEndDate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None,
                    out tempDateTime))
                {
                    maxDate = tempDateTime;
                }


                // To make the endDate as the last minute of the day so that x < endDate comparision can work correctly.
                if (maxDate.Date != DateTime.MaxValue.Date)
                {
                    maxDate = maxDate.AddDays(1).AddMilliseconds(-1);
                }


                var data = ctx.Accounts.Where(acc => acc.AccountCategory == AccountCategory.Nominal).Select(x => new
                {
                    x.AccountName,
                    TotalCredits = x.TransactionLegs
                        .Where(leg => leg.TransactionLegType == TransactionLegType.Credit)
                        .Where(leg => leg.Timestamp >= minDate && leg.Timestamp < maxDate)
                        .Sum(y => (decimal?) y.Amount) ?? 0,
                    TotalDebits = x.TransactionLegs
                        .Where(leg => leg.TransactionLegType == TransactionLegType.Debit)
                        .Where(leg => leg.Timestamp >= minDate && leg.Timestamp < maxDate)
                        .Sum(y => (decimal?) y.Amount) ?? 0,
                    x.AccountCategory
                }).ToList().OrderBy(x => x.AccountName);


                var finalData = data.Select(x => new
                {
                    x.AccountName,
                    x.AccountCategory,
                    AbsCredits = x.TotalCredits - x.TotalDebits,
                    AbsDebits = x.TotalDebits - x.TotalCredits,
                    x.TotalCredits,
                    x.TotalDebits
                }).OrderByDescending(y => y.AbsDebits).ToList();

                var totalAmount = finalData.Sum(x => x.TotalDebits - x.TotalCredits);


                gvExpenses.DataSource = finalData.ToList();
                gvExpenses.DataBind();


                var chartType = (SeriesChartType) Enum.Parse(typeof (SeriesChartType), ddlChartType.SelectedValue);

                Chart1.Series.Clear();
                Chart1.Series.Add(new Series()
                {
                    ChartType = chartType
                });

                // for the JavaScript Google Chart

                var expensesChart = new List<object>();

                expensesChart.Add(new object[] {"Account", "Amount"});

                foreach (var account in finalData)
                {
                    if (account.AbsDebits > 0)
                    {
                        Chart1.Series[0].Points.AddXY(account.AccountName, account.AbsDebits);

                        expensesChart.Add(new object[] { account.AccountName, account.AbsDebits });

                    }
                }

                Session["_Dashboard_ExpensesChart"] = expensesChart;


                var lblTotalDebits = gvExpenses.FooterRow.FindControl("lblTotalDebits") as Label;

                if (lblTotalDebits != null)
                {
                    lblTotalDebits.Text = totalAmount.ToString(CultureInfo.InvariantCulture);
                }

                if ((gvExpenses.ShowHeader == true && gvExpenses.Rows.Count > 0)
                    || (gvExpenses.ShowHeaderWhenEmpty == true))
                {
                    //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                    gvExpenses.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gvExpenses.ShowFooter == true && gvExpenses.Rows.Count > 0)
                {
                    //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                    gvExpenses.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
        }

        protected void btnSubmit_OnClick(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "SubmitExpensesTransaction")
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var amount = decimal.Parse(txtAmountExpenses.Text.Trim());
                    var debitAccount = ctx.Accounts.Single(x => x.AccountName == ddlDebitAccountExpenses.SelectedValue);
                    var creditAccount = ctx.Accounts.Single(x => x.AccountName == ddlCreditAccountExpenses.SelectedValue);

                    var transaction = new Transaction()
                    {
                        Id = Guid.NewGuid(),
                        Description = txtDescriptionExpenses.Text.Trim()
                    };

                    var tranLegDebit = new TransactionLeg()
                    {
                        Id = Guid.NewGuid(),
                        Transaction = transaction,
                        Account = debitAccount,
                        TransactionLegType = TransactionLegType.Debit,
                        Amount = amount
                    };

                    var tranLegCredit = new TransactionLeg()
                    {
                        Id = Guid.NewGuid(),
                        Transaction = transaction,
                        Account = creditAccount,
                        TransactionLegType = TransactionLegType.Credit,
                        Amount = amount
                    };

                    try
                    {
                        ctx.TransactionLegs.Add(tranLegDebit);
                        ctx.TransactionLegs.Add(tranLegCredit);
                        ctx.Transactions.Add(transaction);

                        ctx.SaveChanges();

                        lblMessageExpenses.Text = "Transaction added successfully";
                        ClearExpenses();

                        LoadInformation();
                    }
                    catch (Exception ex)
                    {
                        lblMessageExpenses.Text = ex.Message;
                    }
                }
            }
            else if (e.CommandName == "SubmitOtherTransaction")
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var amount = decimal.Parse(txtAmount.Text.Trim());
                    var debitAccount = ctx.Accounts.Single(x => x.AccountName == ddlDebitAccount.SelectedValue);
                    var creditAccount = ctx.Accounts.Single(x => x.AccountName == ddlCreditAccount.SelectedValue);

                    var transaction = new Transaction()
                    {
                        Id = Guid.NewGuid(),
                        Description = txtDescription.Text.Trim()
                    };

                    var tranLegDebit = new TransactionLeg()
                    {
                        Id = Guid.NewGuid(),
                        Transaction = transaction,
                        Account = debitAccount,
                        TransactionLegType = TransactionLegType.Debit,
                        Amount = amount
                    };

                    var tranLegCredit = new TransactionLeg()
                    {
                        Id = Guid.NewGuid(),
                        Transaction = transaction,
                        Account = creditAccount,
                        TransactionLegType = TransactionLegType.Credit,
                        Amount = amount
                    };

                    try
                    {
                        ctx.TransactionLegs.Add(tranLegDebit);
                        ctx.TransactionLegs.Add(tranLegCredit);
                        ctx.Transactions.Add(transaction);

                        ctx.SaveChanges();

                        lblMessage.Text = "Transaction added successfully";
                        Clear();

                        LoadInformation();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }
                }
            }
        }

        private void Clear()
        {
            txtDescription.Text = "";
            txtAmount.Text = "";
            ddlCreditAccount.SelectedIndex = 0;
            ddlDebitAccount.SelectedIndex = 0;
        }

        private void ClearExpenses()
        {
            txtDescriptionExpenses.Text = "";
            txtAmountExpenses.Text = "";
            ddlCreditAccountExpenses.SelectedIndex = 0;
            ddlDebitAccountExpenses.SelectedIndex = 0;
        }


        public string getJson()
        {
            var publicationTable = new List<object>
            {
                new object[] {"Task", "Hours per day"},
                new object[] {"Work", 11},
                new object[] {"Eat", 2}
            };
            //return (new JavaScriptSerializer()).Serialize(publicationTable);

            return (new JavaScriptSerializer()).Serialize(Session["_Dashboard_ExpensesChart"]);
        }
    }
}
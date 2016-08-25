using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using ThriftyWeb.Models;
using ListExtensions = WebGrease.Css.Extensions.ListExtensions;

namespace ThriftyWeb.App
{
    public partial class Journal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadInformation();
            }
        }


        private void LoadInformation()
        {
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


                var transactions = ctx.Transactions.Where(x => x.Timestamp >= minDate && x.Timestamp < maxDate).ToList();

                if (chkShowOnlyExpenses.Checked)
                {
                    var transactionsExpenses = transactions.Where(
                        x =>
                             x.TransactionLegs.Single(y => y.TransactionLegType == TransactionLegType.Debit)
                                 .Account.AccountCategory == AccountCategory.Nominal).ToList();


                    var reversals = transactions.Where(
                        x =>
                             x.TransactionLegs.Single(y => y.TransactionLegType == TransactionLegType.Credit)
                                 .Account.AccountCategory == AccountCategory.Nominal).ToList();

                    reversals.ForEach(tran => ListExtensions.ForEach(tran.TransactionLegs, leg => leg.Amount *= -1));


                    transactions = transactionsExpenses.Union(reversals).OrderBy(x => x.Timestamp).ToList();


                }


                var finalData2 = transactions.Select(x => new
                {
                    TransactionDate = x.Timestamp,
                    DebitAccount =
                        x.TransactionLegs.FirstOrDefault(y => y.TransactionLegType == TransactionLegType.Debit)
                            .Account.AccountName,
                    CreditAccount =
                        x.TransactionLegs.FirstOrDefault(y => y.TransactionLegType == TransactionLegType.Credit)
                            .Account.AccountName,
                    Amount = x.TransactionLegs.FirstOrDefault().Amount,
                    TransactionDescription = x.Description
                }).OrderBy(y => y.TransactionDate).ToList();


                gvTransactions.DataSource = finalData2;
                gvTransactions.DataBind();


                lblExpenseForDuration.Text = finalData2.Sum(x => x.Amount).ToString();


                if ((gvTransactions.ShowHeader == true && gvTransactions.Rows.Count > 0)
                    || (gvTransactions.ShowHeaderWhenEmpty == true))
                {
                    //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                    gvTransactions.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (gvTransactions.ShowFooter == true && gvTransactions.Rows.Count > 0)
                {
                    //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                    gvTransactions.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
        }

        protected void chkShowOnlyExpenses_OnCheckedChanged(object sender, EventArgs e)
        {
            gvTransactions.PageIndex = 0;
            LoadInformation();
        }

        protected void gvTransactions_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransactions.PageIndex = e.NewPageIndex;
            LoadInformation();
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
    }
}
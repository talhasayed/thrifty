using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using ThriftyWeb.Models;
using WebGrease.Css.Extensions;

namespace ThriftyWeb.App
{
    public partial class ExpensesSummary : System.Web.UI.Page
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
                #region commented

                //var minDate = DateTime.MinValue;
                //var maxDate = DateTime.MaxValue;

                //DateTime tempDateTime;

                //if (DateTime.TryParseExact(txtStartDate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None,
                //    out tempDateTime))
                //{
                //    minDate = tempDateTime;
                //}

                //if (DateTime.TryParseExact(txtEndDate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None,
                //    out tempDateTime))
                //{
                //    maxDate = tempDateTime;
                //}


                //// To make the endDate as the last minute of the day so that x < endDate comparision can work correctly.
                //if (maxDate.Date != DateTime.MaxValue.Date)
                //{
                //    maxDate = maxDate.AddDays(1).AddMilliseconds(-1);
                //}

                #endregion

                var transactions = ctx.Transactions.ToList();


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


                var finalData2 = transactions.Select(x => new
                {
                    TransactionDate = x.Timestamp,
                    DebitAccount =
                        x.TransactionLegs.Single(y => y.TransactionLegType == TransactionLegType.Debit)
                            .Account.AccountName,
                    CreditAccount =
                        x.TransactionLegs.Single(y => y.TransactionLegType == TransactionLegType.Credit)
                            .Account.AccountName,
                    Amount = x.TransactionLegs.First().Amount,
                    TransactionDescription = x.Description
                })
                    .GroupBy(g => g.TransactionDate.Date)
                    .Select(q => new {Date = q.Key.Date, Amount = q.Sum(tr => tr.Amount)})
                    .OrderBy(item => item.Date);


                //var chartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), ddlChartType.SelectedValue);

                Chart1.Series.Clear();
                Chart1.Series.Add(new Series()
                {
                    ChartType = SeriesChartType.Column,
                    BorderWidth = 3
                });

                foreach (var item in finalData2)
                {
                    Chart1.Series[0].Points.AddXY(item.Date, item.Amount);
                }
            }
        }
    }
}
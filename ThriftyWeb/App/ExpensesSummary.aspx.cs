using System;
using System.Collections.Generic;
using System.Drawing;
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


                var dayExpenses = (from dynamic item in finalData2 select new DayExpense(item.Date, item.Amount)).ToList();

                Session["_ExpensesSummary_ExpensesChart"] = dayExpenses;

                //var chartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), ddlChartType.SelectedValue);

                Chart1.Series.Clear();
                Chart1.Series.Add(new Series()
                {
                    ChartType = SeriesChartType.Column,
                });

                Chart1.Series[0]["PixelPointWidth"] = "15";
                Chart1.Series[0].XValueType = ChartValueType.Date;

                Chart1.ChartAreas[0].AxisX.Interval = 1;
                Chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;

                Chart1.ChartAreas[0].AxisX.MajorGrid = new Grid() {Enabled = false};

                Chart1.Series[0].XValueType = ChartValueType.DateTime;
                var minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddSeconds(-1);
                var maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1); // or DateTime.Now;
                Chart1.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();
                Chart1.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();


                Chart1.ChartAreas[0].AxisY.Interval = 10;
                Chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;


                foreach (var item in finalData2)
                {
                    Chart1.Series[0].Points.AddXY(item.Date, item.Amount);
                }
            }
        }

        protected void Chart1_OnCustomize(object sender, EventArgs e)
        {
            foreach (CustomLabel cl in Chart1.ChartAreas[0].AxisX.CustomLabels)
            {
                DateTime dt;

                if (DateTime.TryParseExact(cl.Text, "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dt))
                {
                    if (dt.DayOfWeek == DayOfWeek.Friday || dt.DayOfWeek == DayOfWeek.Saturday)
                    {
                        cl.ForeColor = Color.Brown;
                    }
                }
            }
        }


        public string getJson()
        {

            var dayExpenses = (List<DayExpense>)Session["_ExpensesSummary_ExpensesChart"];


            string str = "";

            var minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1); // or DateTime.Now;

            foreach (DateTime day in EachDay(minDate, maxDate))
            {
                decimal amount = 0;

                if (dayExpenses.Any(x => x.Date == day))
                {
                    amount = dayExpenses.Single(x => x.Date == day).Amount;
                }


                str += $"data.addRow([formatterDate.formatValue(new Date('{day.Date.ToString("yyyy-MM-dd")}')), {amount.ToString("N3")}]);\n";


            }


                return str;
        }


        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }


    }



    public struct DayExpense
    {
        public DayExpense(DateTime date, decimal amount)
        {
            Date = date;
            Amount = amount;
        }

        public DateTime Date;
        public decimal Amount;
    }


}
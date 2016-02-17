using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
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
                //ddlChartType.DataSource = Enum.GetValues(typeof (SeriesChartType));
                //ddlChartType.DataBind();
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


                // To make the endDate as the last minute of the day do that x < endDate comparision can work correctly.
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
                }).ToList();

                var totalAmount = finalData.Sum(x => x.TotalDebits);


                gvExpenses.DataSource = finalData.ToList();
                gvExpenses.DataBind();


                foreach (var account in finalData)
                {
                    if (account.AbsDebits > 0)
                    {
                        Chart1.Series[0].Points.AddXY(account.AccountName, account.AbsDebits);
                    }
                }


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


        protected void PageCommands()
        {
        }

        protected void CurrentMonth_OnClick(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "ShowCurrentMonth")
            {
            }
        }

        protected void ddlChartType_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            var content = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), ddlChartType.SelectedValue);


            Chart1.Series[0].ChartType = content;
        }

        private void LoadInformation()
        {

            

        }


    }
}
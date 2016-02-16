using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThriftyWeb.Models;

namespace ThriftyWeb.App
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var data = ctx.Accounts.Where(acc => acc.AccountCategory == AccountCategory.Nominal).Select(x => new
                {
                    x.AccountName,
                    TotalCredits =
                        x.TransactionLegs.Where(leg => leg.TransactionLegType == TransactionLegType.Credit)
                            .Sum(y => (decimal?) y.Amount) ?? 0,
                    TotalDebits =
                        x.TransactionLegs.Where(leg => leg.TransactionLegType == TransactionLegType.Debit)
                            .Sum(y => (decimal?) y.Amount) ?? 0,
                    x.AccountCategory
                }).ToList().OrderBy(x => x.AccountName);


                var finalData = data.Select(x => new
                {
                    x.AccountName,
                    x.AccountCategory,
                    AbsCredits =
                        (x.TotalCredits - x.TotalDebits) > 0
                            ? (x.TotalCredits - x.TotalDebits).ToString(CultureInfo.InvariantCulture)
                            : null,
                    AbsDebits =
                        (x.TotalDebits - x.TotalCredits) > 0
                            ? (x.TotalDebits - x.TotalCredits).ToString(CultureInfo.InvariantCulture)
                            : null,
                    x.TotalCredits,
                    x.TotalDebits
                });

                var totalAmount = finalData.Sum(x => x.TotalDebits);


                gvExpenses.DataSource = finalData.ToList();
                gvExpenses.DataBind();

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
    }
}
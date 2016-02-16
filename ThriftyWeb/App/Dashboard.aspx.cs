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
                                .Sum(y => (decimal?)y.Amount) ?? 0,
                    TotalDebits =
                            x.TransactionLegs.Where(leg => leg.TransactionLegType == TransactionLegType.Debit)
                                .Sum(y => (decimal?)y.Amount) ?? 0,
                    x.AccountCategory
                }).ToList().OrderBy(x => x.AccountName);



                var finalData = data.Select(x => new
                {
                    x.AccountName,
                    x.AccountCategory,
                    AbsCredits =
                        (x.TotalCredits - x.TotalDebits) > 0 ? (x.TotalCredits - x.TotalDebits).ToString(CultureInfo.InvariantCulture) : null,
                    AbsDebits =
                        (x.TotalDebits - x.TotalCredits) > 0 ? (x.TotalDebits - x.TotalCredits).ToString(CultureInfo.InvariantCulture) : null,
                    x.TotalCredits,
                    x.TotalDebits
                });


                gvExpenses.DataSource = finalData.ToList();
                gvExpenses.DataBind();
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
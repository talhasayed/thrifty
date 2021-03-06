﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThriftyWeb.Models;

namespace ThriftyWeb.App
{
    public partial class ViewAccountsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var data = ctx.Accounts.Select(x => new
                    {
                        x.AccountName,
                        TotalCredits =
                            x.TransactionLegs.Where(leg => leg.TransactionLegType == TransactionLegType.Credit)
                                .Sum(y => (decimal?)y.Amount) ?? 0,
                        TotalDebits =
                            x.TransactionLegs.Where(leg => leg.TransactionLegType == TransactionLegType.Debit)
                                .Sum(y => (decimal?)y.Amount) ?? 0,
                        x.AccountCategory
                    }).ToList().OrderBy(x => x.AccountCategory);



                    var finalData = data.Select(x => new
                    {
                        x.AccountName,
                        x.AccountCategory,
                        AbsCredits =
                            (x.TotalCredits - x.TotalDebits) > 0 ? (x.TotalCredits - x.TotalDebits).ToString() : null,
                        AbsDebits =
                            (x.TotalDebits - x.TotalCredits) > 0 ? (x.TotalDebits - x.TotalCredits).ToString() : null,
                        x.TotalCredits,
                        x.TotalDebits
                    });


                    gvListAccounts.DataSource = finalData;
                    gvListAccounts.DataBind();


                }
            }
        }
    }
}
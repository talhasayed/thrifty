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
                var transactions = ctx.Transactions.OrderBy(x => x.Timestamp).ToList();


                var finalData2 = transactions.Select(x => new
                {
                    TransactionDate = x.Timestamp,
                    DebitAccount = x.TransactionLegs.Single(y=> y.TransactionLegType == TransactionLegType.Debit).Account.AccountName,
                    CreditAccount = x.TransactionLegs.Single(y => y.TransactionLegType == TransactionLegType.Credit).Account.AccountName,
                    Amount = x.TransactionLegs.First().Amount,
                    TransactionDescription = x.Description
                });


                gvTransactions.DataSource = finalData2;
                gvTransactions.DataBind();


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
    }
}
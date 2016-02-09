using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThriftyWeb.Migrations;
using ThriftyWeb.Models;

namespace ThriftyWeb.App
{
    public partial class ViewAllAccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            using (var ctx = new ApplicationDbContext())
            {
                foreach (var account in ctx.Accounts)
                {
                    RenderAccount(account);
                }
            }
        }


        private void RenderAccount(Models.Account account)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var transactions = ctx.TransactionLegs.Where(x => x.Account.Id == account.Id).ToList();
                if (transactions.Count != 0)
                {
                    var gv = new GridView
                    {
                        DataSource = transactions,
                        AutoGenerateColumns = false,
                        EmptyDataText = "There are no transactions in this account"
                    };


                    gv.Columns.Add(new BoundField() {HeaderText = "Type", DataField = "TransactionLegType"});
                    gv.Columns.Add(new BoundField() {HeaderText = "Amount", DataField = "Amount"});
                    gv.Columns.Add(new BoundField()
                    {
                        HeaderText = "TimeStamp",
                        DataField = "Timestamp",
                        DataFormatString = "{0:dd'/'MM'/'yyyy}"
                    });

                    gv.DataBind();

                    phAccounts.Controls.Add(new Literal() {Text = account.AccountName});
                    phAccounts.Controls.Add(gv);
                    phAccounts.Controls.Add(new Literal()
                    {
                        Text = "<div style=\"padding: 10px; border-bottom: 2px solid #444\">&nbsp;</div>",
                        Mode = LiteralMode.PassThrough
                    });
                }
            }
        }
    }
}
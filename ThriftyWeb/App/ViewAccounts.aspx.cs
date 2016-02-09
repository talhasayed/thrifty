using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThriftyWeb.Models;

namespace ThriftyWeb.App
{
    public partial class ViewAccounts : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadAccounts();
            }
        }

        private void LoadAccounts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var accounts = ctx.Accounts.ToList();

                ddlAccounts.DataSource = accounts;
                ddlAccounts.DataTextField = "AccountName";
                ddlAccounts.DataValueField = "Id";
                ddlAccounts.DataBind();
            }
        }

        protected void ddlAccounts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var accountId = Guid.Parse(ddlAccounts.SelectedValue);
                var transactions =
                    ctx.TransactionLegs.Where(x => x.Account.Id == accountId)
                        .Include(x => x.TransactionLegType)
                        .ToList();

                gvAccountData.DataSource = transactions;
                gvAccountData.DataBind();
            }
        }

        protected void gvAccountData_OnDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var accountId = Guid.Parse(ddlAccounts.SelectedValue);
                using (var ctx = new ApplicationDbContext())
                {
                    var totalCredits = ctx.TransactionLegs.Where(x => x.Account.Id == accountId && x.TransactionLegType == TransactionLegType.Credit).Sum(x => (decimal?)x.Amount) ?? 0;
                    var totalDebits = ctx.TransactionLegs.Where(x => x.Account.Id == accountId && x.TransactionLegType == TransactionLegType.Debit).Sum(x => (decimal?)x.Amount) ?? 0;


                    Label lblTotalDebits = (Label)e.Row.FindControl("lblTotalDebits");
                    Label lblTotalCredits = (Label)e.Row.FindControl("lblTotalCredits");
                    lblTotalDebits.Text = totalDebits.ToString();
                    lblTotalCredits.Text = totalCredits.ToString();


                }



            }
        }
    }
}
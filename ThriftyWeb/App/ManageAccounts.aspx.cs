using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThriftyWeb.Models;

namespace ThriftyWeb.App
{
    public partial class ManageAccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                var accountCategories = Enum.GetValues(typeof(AccountCategory)).Cast<AccountCategory>()
                .Select(ac => new { Value = (int)ac, Text = ac.ToString() })
                .ToList();
                ddlAccountCategory.DataSource = accountCategories;
                ddlAccountCategory.DataTextField = "Text";
                ddlAccountCategory.DataValueField = "Value";
                ddlAccountCategory.DataBind();


                ddlAccountCategory.Items.RemoveAt(0);


            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var account = new Models.Account()
                {
                    Id = Guid.NewGuid(),
                    AccountName = txtAccountName.Text.Trim(),
                    AccountCategory = (AccountCategory)Convert.ToInt32(ddlAccountCategory.SelectedValue)
                };

                ctx.Accounts.Add(account);
                ctx.SaveChanges();

                lblMessage.Text = $"Account {account.AccountName} created successfully";


            }
        }
    }
}
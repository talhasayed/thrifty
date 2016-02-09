using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThriftyWeb.Models;

namespace ThriftyWeb.App
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var account = new Models.Account()
                {
                    Id = Guid.NewGuid(),
                    AccountName = "testac"
                };
                ctx.Accounts.Add(account);

                ctx.SaveChanges();
            }
        }

        protected void btnAddSampleData_Click(object sender, EventArgs e)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Accounts.AddRange(new[]
                {
                    new Models.Account {AccountName = "Cash"},
                    new Models.Account {AccountName = "BankAccount"},
                    new Models.Account {AccountName = "CreditCard"},
                    new Models.Account {AccountName = "PetrolCard"},

                    new Models.Account {AccountName = "Riyas", AccountCategory = AccountCategory.Personal},
                    new Models.Account {AccountName = "Asheeb", AccountCategory = AccountCategory.Personal},
                    new Models.Account {AccountName = "Abbu", AccountCategory = AccountCategory.Personal},

                    new Models.Account {AccountName = "OfficeExp", AccountCategory = AccountCategory.Nominal},
                    new Models.Account {AccountName = "FriendsExp", AccountCategory = AccountCategory.Nominal},
                    new Models.Account {AccountName = "MiscExp", AccountCategory = AccountCategory.Nominal},
                    new Models.Account {AccountName = "FamilyExp", AccountCategory = AccountCategory.Nominal},
                });

                ctx.SaveChanges();

            }
        }
    }
}
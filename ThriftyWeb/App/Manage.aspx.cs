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
                //ctx.Accounts.Add(new Models.Account()
                //{
                //    Id = Guid.NewGuid(),
                //    AccountName = "Talha",
                //    AccountCategory = AccountCategory.Personal,
                //});


                //ctx.SaveChanges();


                //lblMessage.Text = "Account talha created successfully.";
                //var account = new Models.Account()
                //{
                //    Id = Guid.NewGuid(),
                //    AccountName = "testac"
                //};
                //ctx.Accounts.Add(account);

                //ctx.SaveChanges();
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

        protected void btnClearData_OnClick(object sender, EventArgs e)
        {
            using (var ctx = new ApplicationDbContext())
            {

                ctx.TransactionLegs.RemoveRange(ctx.TransactionLegs.ToList());
                ctx.Transactions.RemoveRange(ctx.Transactions.ToList());

                ctx.SaveChanges();

            }

        }

        protected void btnProcess_OnClick(object sender, EventArgs e)
        {
            //using (var ctx = new ApplicationDbContext())
            //{
            //    ctx.Accounts.Add(new Models.Account()
            //    {
            //        Id = Guid.NewGuid(),
            //        AccountName = "Jalil",
            //        AccountCategory = AccountCategory.Personal,
            //    });
            //    ctx.Accounts.Add(new Models.Account()
            //    {
            //        Id = Guid.NewGuid(),
            //        AccountName = "Tuffy",
            //        AccountCategory = AccountCategory.Personal,
            //    });


            //    ctx.SaveChanges();

            //    lblMessage.Text = "accoutn jalil and tuffy created successfully";

            //}
        }
        }
}
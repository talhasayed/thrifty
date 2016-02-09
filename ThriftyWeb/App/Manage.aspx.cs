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
                ctx.TransactionLegs.RemoveRange(ctx.TransactionLegs);
                ctx.Transactions.RemoveRange(ctx.Transactions);
                ctx.SaveChanges();
            }
        }
    }
}
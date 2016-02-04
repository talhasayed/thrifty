using ThriftyWeb.Models;
using WebGrease.Css.Extensions;

namespace ThriftyWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ThriftyWeb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ThriftyWeb.Models.ApplicationDbContext";
        }

        protected override void Seed(ThriftyWeb.Models.ApplicationDbContext context)
        {
           context.AccountCategories.AddOrUpdate(p => p.Id,
                new AccountCategory() { Id = 1, CategoryName = "REAL" }
                , new AccountCategory() { Id = 2, CategoryName = "PERSONAL" }
                , new AccountCategory() { Id = 3, CategoryName = "NOMINAL" });
        }
    }
}
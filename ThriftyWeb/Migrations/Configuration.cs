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
            context.AccountCategories.AddOrUpdate(p => p.CategoryName,
                new AccountCategory() {Id = 1, CategoryName = "REAL"}
                , new AccountCategory() {Id = 2, CategoryName = "PERSONAL"}
                , new AccountCategory() {Id = 3, CategoryName = "NOMINAL"});


            context.TransactionLegType.AddOrUpdate(p => p.Type,
                new TransactionLegType() {Id = 1, Type = "CREDIT"}
                , new TransactionLegType() {Id = 2, Type = "DEBIT"});

            context.SaveChanges();

            var realAccount = context.AccountCategories.Single(y => y.CategoryName == "REAL");
            var personalAccount = context.AccountCategories.Single(y => y.CategoryName == "PERSONAL");

            context.Accounts.AddOrUpdate(x => x.AccountName,
                new Models.Account() { Id = Guid.NewGuid(), AccountName = "CASH", AccountCategory = realAccount },
                new Models.Account() { Id = Guid.NewGuid(), AccountName = "BANK", AccountCategory = realAccount },
                new Models.Account() { Id = Guid.NewGuid(), AccountName = "CREDITCARD", AccountCategory = realAccount },
                new Models.Account() { Id = Guid.NewGuid(), AccountName = "RIYAS", AccountCategory = personalAccount },
                new Models.Account() { Id = Guid.NewGuid(), AccountName = "ASHEEB", AccountCategory = personalAccount },
                new Models.Account() { Id = Guid.NewGuid(), AccountName = "AMMI", AccountCategory = personalAccount },
                new Models.Account() { Id = Guid.NewGuid(), AccountName = "ABBU", AccountCategory = personalAccount },
                new Models.Account() { Id = Guid.NewGuid(), AccountName = "MAMMU", AccountCategory = personalAccount },
                new Models.Account() { Id = Guid.NewGuid(), AccountName = "BAJI", AccountCategory = personalAccount }
                );
        }
    }
}
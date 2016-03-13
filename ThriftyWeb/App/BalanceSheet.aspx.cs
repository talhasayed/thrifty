using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThriftyWeb.Models;

namespace ThriftyWeb.App
{
    public partial class BalanceSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //***************** ASSETS **********************

                var data = ctx.Accounts.Where(acc => acc.AccountCategory != AccountCategory.Nominal).Select(x => new
                {
                    x.AccountName,
                    TotalCredits = x.TransactionLegs
                        .Where(leg => leg.TransactionLegType == TransactionLegType.Credit)
                        .Sum(y => (decimal?)y.Amount) ?? 0,
                    TotalDebits = x.TransactionLegs
                        .Where(leg => leg.TransactionLegType == TransactionLegType.Debit)
                        .Sum(y => (decimal?)y.Amount) ?? 0,
                    x.AccountCategory
                }).ToList().OrderBy(x => x.AccountName);



                var data2 = data.Select(x => new
                {
                    x.AccountName,
                    x.AccountCategory,
                    AbsCredits = x.TotalCredits - x.TotalDebits,
                    AbsDebits = x.TotalDebits - x.TotalCredits,
                    IsDebitAccount = x.TotalDebits > x.TotalCredits
                }).ToList();


                var finalData = data2.Where(x => x.IsDebitAccount);

                gvAssets.DataSource = finalData;
                gvAssets.DataBind();



                //***************** LIABILITIES **********************




                var dataLiabilities = ctx.Accounts.Where(acc => acc.AccountCategory != AccountCategory.Nominal).Select(x => new
                {
                    x.AccountName,
                    TotalCredits = x.TransactionLegs
                        .Where(leg => leg.TransactionLegType == TransactionLegType.Credit)
                        .Sum(y => (decimal?)y.Amount) ?? 0,
                    TotalDebits = x.TransactionLegs
                        .Where(leg => leg.TransactionLegType == TransactionLegType.Debit)
                        .Sum(y => (decimal?)y.Amount) ?? 0,
                    x.AccountCategory
                }).ToList().OrderBy(x => x.AccountName.Contains("Talha")? "": x.AccountName);



                var dataLiabilities2 = dataLiabilities.Select(x => new
                {
                    x.AccountName,
                    x.AccountCategory,
                    AbsCredits = x.TotalCredits - x.TotalDebits,
                    AbsDebits = x.TotalDebits - x.TotalCredits,
                    IsCreditAccount = x.TotalCredits > x.TotalDebits
                }).ToList();


                var finalDataLiabilities = dataLiabilities2.Where(x => x.IsCreditAccount);

                gvLiabilities.DataSource = finalDataLiabilities;
                gvLiabilities.DataBind();

                gvLiabilities.Rows[0].CssClass = "highlighted";


                //***************** ADJUSTED EXPENSES **********************

                // Total Expenses of Current period
                var expensesData = ctx.Accounts.Where(acc => acc.AccountCategory == AccountCategory.Nominal).Select(x => new
                {
                    x.AccountName,
                    TotalCredits = x.TransactionLegs
                        .Where(leg => leg.TransactionLegType == TransactionLegType.Credit)
                        .Sum(y => (decimal?)y.Amount) ?? 0,
                    TotalDebits = x.TransactionLegs
                        .Where(leg => leg.TransactionLegType == TransactionLegType.Debit)
                        .Sum(y => (decimal?)y.Amount) ?? 0,
                    x.AccountCategory
                }).ToList().OrderBy(x => x.AccountName);


                var expensesFinalData = expensesData.Select(x => new
                {
                    x.AccountName,
                    x.AccountCategory,
                    AbsCredits = x.TotalCredits - x.TotalDebits,
                    AbsDebits = x.TotalDebits - x.TotalCredits,
                    x.TotalCredits,
                    x.TotalDebits
                }).ToList();

                var totalexpenses = expensesFinalData.Sum(x => x.TotalDebits - x.TotalCredits);

                lblCurrentExpenses.Text = totalexpenses.ToString("N3");


                // Current balance on Talha account
                var talhaData =
                    ctx.Accounts.Where(
                        acc => acc.AccountCategory != AccountCategory.Nominal && acc.AccountName.Contains("Talha"))
                        .Select(x => new
                        {
                            x.AccountName,
                            TotalCredits = x.TransactionLegs
                                .Where(leg => leg.TransactionLegType == TransactionLegType.Credit)
                                .Sum(y => (decimal?) y.Amount) ?? 0,
                            TotalDebits = x.TransactionLegs
                                .Where(leg => leg.TransactionLegType == TransactionLegType.Debit)
                                .Sum(y => (decimal?) y.Amount) ?? 0,
                            x.AccountCategory
                        });



                var talhaData2 = talhaData.Select(x => new
                {
                    x.AccountName,
                    x.AccountCategory,
                    AbsCredits = x.TotalCredits - x.TotalDebits,
                    AbsDebits = x.TotalDebits - x.TotalCredits,
                    IsCreditAccount = x.TotalCredits > x.TotalDebits
                }).Single();


                var balance = talhaData2.AbsCredits;

                lblBalance.Text = balance.ToString("N3");


                lblActualBalance.Text = (balance - totalexpenses).ToString("N3");



            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThriftyWeb.Models;

namespace ThriftyWeb.App
{
    public partial class AddTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {


            using (var ctx = new ApplicationDbContext())
            {

                var amount = decimal.Parse(txtAmount.Text.Trim());
                var debitAccount = ctx.Accounts.Single(x => x.AccountName == txtDebitAccount.Text.Trim());
                var creditAccount = ctx.Accounts.Single(x => x.AccountName == txtDebitAccount.Text.Trim());
                var debitType = ctx.TransactionLegType.Single(x => x.Type == "DEBIT");
                var creditType = ctx.TransactionLegType.Single(x => x.Type == "CREDIT");

                var transaction = new Transaction()
                {
                    Id = Guid.NewGuid(),
                    Description = txtDescription.Text.Trim()
                };

                var tranLegDebit = new TransactionLeg()
                {
                    Id = Guid.NewGuid(),
                    Transaction = transaction,
                    Account = debitAccount,
                    TransactionLegType = debitType,
                    Amount = amount
                };

                var tranLegCredit = new TransactionLeg()
                {
                    Id = Guid.NewGuid(),
                    Transaction = transaction,
                    Account = creditAccount,
                    TransactionLegType = creditType,
                    Amount = amount
                };

                try
                {
                    ctx.TransactionLegs.Add(tranLegDebit);
                    ctx.TransactionLegs.Add(tranLegCredit);
                    ctx.Transactions.Add(transaction);

                    ctx.SaveChanges();

                    lblMessage.Text = "Transaction added successfully";
                    Clear();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }

            }

        }

        private void Clear()
        {
            txtDescription.Text = "";
            txtAmount.Text = "";
            txtCreditAccount.Text = "";
            txtDebitAccount.Text = "";
        }
    }
}
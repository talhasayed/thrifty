using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ThriftyWeb.Models
{
    public enum AccountCategory
    {
        NotSet = 0,
        Real,
        Personal,
        Nominal
    }

    public enum TransactionLegType
    {
        NotSet = 0,
        Credit,
        Debit
    }


    public class Account
    {
        public Account()
        {
            Id = Guid.NewGuid();
            AccountCategory = AccountCategory.Real;
        }

        public Guid Id { get; set; }
        public string AccountName { get; set; }

        [DefaultValue(AccountCategory.Real)]
        [Range(1, 3), Display(Name = "Account Category")]
        public AccountCategory AccountCategory { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; } 
    }

    public class Transaction
    {
        public Transaction()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual ICollection<TransactionLeg> TransactionLegs { get; set; }
    }

    public class TransactionLeg
    {
        public TransactionLeg()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }

        public Guid Id { get; set; }

        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual Account Account { get; set; }

        [DefaultValue(TransactionLegType.Credit)]
        [Range(1, 2), Display(Name = "Transaction Leg Type")]
        public TransactionLegType TransactionLegType { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketGames.Domain.Model;

namespace TicketGames.API.Models.Account
{
    public class Account
    {
        public float Balance { get; set; }

        public Account()
        {
            this.Balance = 0;
        }

        public Account Balances(List<Transaction> transactions)
        {
            var account = new Account();

            var credit = transactions.Where(t => t.TransactionTypeId == 1).Sum(s => s.Value) + transactions.Where(t => t.TransactionTypeId == 3).Sum(s => s.Value);
            var debit = transactions.Where(t => t.TransactionTypeId == 2).Sum(s => s.Value) + transactions.Where(t => t.TransactionTypeId == 4).Sum(s => s.Value);

            account.Balance = credit - debit;

            return account;
        }
    }
}
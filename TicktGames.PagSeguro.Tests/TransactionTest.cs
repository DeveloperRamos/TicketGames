using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketGames.PagSeguro;

namespace TicktGames.PagSeguro.Tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void BilletCheckout()
        {
            Transaction transaction = new Transaction();

            transaction.BilletCheckout(new TicketGames.PagSeguro.Model.Billet());
        }
    }
}

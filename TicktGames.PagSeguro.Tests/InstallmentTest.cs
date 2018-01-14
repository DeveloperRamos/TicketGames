using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketGames.PagSeguro;

namespace TicktGames.PagSeguro.Tests
{
    [TestClass]
    public class InstallmentTest
    {
        [TestMethod]
        public void GetInstallments()
        {
            Installment installment = new Installment();


            //var result = installment.GetInstallmentsPagSeguro(100, "visa", 5);

            Assert.IsTrue(true);
        }
    }
}

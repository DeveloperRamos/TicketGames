using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketGames.PagSeguro;

namespace TicktGames.PagSeguro.Tests
{
    [TestClass]
    public class SessionTest
    {
        [TestMethod]
        public void CreateSession()
        {
            Session session = new Session();            

            Assert.IsTrue(!string.IsNullOrEmpty(session.Id));

        }
    }
}

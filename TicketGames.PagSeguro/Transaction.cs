using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.PagSeguro.Model;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Direct;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;


namespace TicketGames.PagSeguro
{
    public class Transaction
    {
        public void BilletCheckout(Billet billet)
        {
            bool isSandbox = true;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // Instantiate a new checkout
            BoletoCheckout checkout = billet.MappingDebitCheckout();

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);
                Uol.PagSeguro.Domain.Transaction result = TransactionService.CreateCheckout(credentials, checkout);

            }
            catch (PagSeguroServiceException exception)
            {
                //Gravar log do erro
            }
        }

        public void CreditCheckout(Credit credit)
        {
            bool isSandbox = true;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);
        }

        public void DebitCheckout(Debit debit)
        {
            bool isSandbox = true;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);
        }

    }
}

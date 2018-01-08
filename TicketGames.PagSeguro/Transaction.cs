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
        private string configuration = ".../.../Configuration/PagSeguroConfig.xml";

        public void BilletCheckout(Billet billet)
        {
            PagSeguroConfiguration.UrlXmlConfiguration = this.configuration;
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
            PagSeguroConfiguration.UrlXmlConfiguration = this.configuration;
            bool isSandbox = true;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // Instantiate a new checkout
            CreditCardCheckout checkout = credit.MappingCreditCheckout();

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);
                Uol.PagSeguro.Domain.Transaction result = TransactionService.CreateCheckout(credentials, checkout);
            }
            catch (PagSeguroServiceException exception)
            {

                foreach (ServiceError element in exception.Errors)
                {
                    var erro = element;
                }
            }

        }

        public void DebitCheckout(Debit debit)
        {
            PagSeguroConfiguration.UrlXmlConfiguration = this.configuration;
            bool isSandbox = true;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);
        }

    }
}

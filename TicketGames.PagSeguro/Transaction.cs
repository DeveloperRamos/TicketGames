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
        private string configuration = "http://hml.ticketgames.com.br/Configuration/PagSeguroConfig.xml";
        private readonly PagSeguroResult pagSeguroResult;


        public Transaction()
        {
            pagSeguroResult = new PagSeguroResult();

        }


        public PagSeguroResult BilletCheckout(Billet billet)
        {
            PagSeguroConfiguration.UrlXmlConfiguration = this.configuration;
            bool isSandbox = true;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // Instantiate a new checkout
            BoletoCheckout checkout = billet.MappingBilletCheckout();

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);
                Uol.PagSeguro.Domain.Transaction transaction = TransactionService.CreateCheckout(credentials, checkout);

                if (!string.IsNullOrEmpty(transaction.Code))
                {
                    pagSeguroResult.Success = true;
                    pagSeguroResult.Code = transaction.Code;
                    pagSeguroResult.FeeAmount = transaction.FeeAmount;
                    pagSeguroResult.NetAmount = transaction.NetAmount;
                    pagSeguroResult.Reference = transaction.Reference;
                    pagSeguroResult.TransactionStatus = transaction.TransactionStatus.ToString();
                    pagSeguroResult.PaymentLink = transaction.PaymentLink;
                }

                return pagSeguroResult;

            }
            catch (PagSeguroServiceException exception)
            {
                return new PagSeguroResult() { Success = false };
                //Gravar log do erro
            }
        }

        public PagSeguroResult CreditCheckout(Credit credit)
        {
            PagSeguroConfiguration.UrlXmlConfiguration = this.configuration;
            bool isSandbox = true;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // Instantiate a new checkout
            CreditCardCheckout checkout = credit.MappingCreditCheckout();

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);
                Uol.PagSeguro.Domain.Transaction transaction = TransactionService.CreateCheckout(credentials, checkout);

                if (!string.IsNullOrEmpty(transaction.Code))
                {
                    pagSeguroResult.Success = true;
                    pagSeguroResult.Code = transaction.Code;
                    pagSeguroResult.FeeAmount = transaction.FeeAmount;
                    pagSeguroResult.NetAmount = transaction.NetAmount;
                    pagSeguroResult.Reference = transaction.Reference;
                    pagSeguroResult.TransactionStatus = transaction.TransactionStatus.ToString();
                }

                return pagSeguroResult;
            }
            catch (PagSeguroServiceException exception)
            {

                return new PagSeguroResult() { Success = false };

                //foreach (ServiceError element in exception.Errors)
                //{
                //    throw new System.ArgumentException(element.Message, "original");
                //}


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

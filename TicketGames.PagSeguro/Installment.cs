using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Installment;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace TicketGames.PagSeguro
{
    public class Installment
    {
        private string configuration = "http://hml.ticketgames.com.br/Configuration/PagSeguroConfig.xml";
        public Installments GetInstallmentsPagSeguro(Decimal amount, string creditCardBrand, int maxInstallmentNoInterest)
        {
            PagSeguroConfiguration.UrlXmlConfiguration = this.configuration;

            bool isSandbox = true;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            try
            {

                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);                

                Installments result = InstallmentService.GetInstallments(credentials, amount, creditCardBrand, maxInstallmentNoInterest);

                return result;

            }
            catch (PagSeguroServiceException exception)
            {

                return new Installments();
                //foreach (ServiceError element in exception.Errors)
                //{

                //}
            }
            catch (Exception ex)
            {

                return new Installments();
            }
        }
    }
}

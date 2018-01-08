using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace TicketGames.PagSeguro
{
    public class Session
    {
        public string Id;
        private string configuration = "http://hml.ticketgames.com.br/Configuration/PagSeguroConfig.xml";


        public Session()
        {
            this.Id = CreateSession();
        }

        private string CreateSession()
        {
            PagSeguroConfiguration.UrlXmlConfiguration = this.configuration;

            bool isSandbox = true;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

                Uol.PagSeguro.Domain.Direct.Session result = SessionService.CreateSession(credentials);

                return result.id;
            }
            catch (PagSeguroServiceException exception)
            {
                //Gravar Log do erro
                return string.Empty;
            }
        }
    }
}

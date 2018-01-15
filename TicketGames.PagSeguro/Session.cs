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
        private string configuration = string.Empty;
        private bool isSandbox;
        private const string pagSeguro = "pagSeguro.";

        public Session()
        {
            this.Id = string.Empty;
        }

        public Session(Dictionary<string, string> settings)
        {
            bool.TryParse(settings.Where(b => b.Key.ToUpper() == (pagSeguro + "isSandbox").ToUpper()).Select(s => s.Value).FirstOrDefault(), out this.isSandbox);

            this.configuration = settings.Where(b => b.Key.ToUpper() == (pagSeguro + "urlXmlConfiguration").ToUpper()).Select(s => s.Value).FirstOrDefault();

            this.configuration = string.Format(this.configuration, isSandbox ? "Homologation" : "Production");

            this.Id = CreateSession();
        }

        private string CreateSession()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.configuration))
                {
                    PagSeguroConfiguration.UrlXmlConfiguration = this.configuration;

                    //bool isSandbox = true;
                    EnvironmentConfiguration.ChangeEnvironment(this.isSandbox);

                    AccountCredentials credentials = PagSeguroConfiguration.Credentials(this.isSandbox);

                    Uol.PagSeguro.Domain.Direct.Session result = SessionService.CreateSession(credentials);

                    return result.id;
                }

                return string.Empty;
            }
            catch (PagSeguroServiceException exception)
            {
                //Gravar Log do erro
                return string.Empty;
            }
        }
    }
}

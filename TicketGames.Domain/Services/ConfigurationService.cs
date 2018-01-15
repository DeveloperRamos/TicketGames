using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;

namespace TicketGames.Domain.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationService(IConfigurationRepository configurationRepository)
        {
            this._configurationRepository = configurationRepository;
        }

        public List<Configuration> GetSettings()
        {
            return this._configurationRepository.GetSettings();
        }

        public Configuration Get(string key)
        {
            return this._configurationRepository.GetConfigurationByKey(key);
        }
    }
}

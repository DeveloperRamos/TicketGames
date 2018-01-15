using System.Collections.Generic;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Repositories
{
    public interface IConfigurationRepository
    {
        List<Configuration> GetSettings();
        Configuration GetConfigurationByKey(string key);
    }
}

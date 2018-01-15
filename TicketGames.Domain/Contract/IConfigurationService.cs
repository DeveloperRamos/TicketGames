using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Contract
{
    public interface IConfigurationService
    {
        List<Configuration> GetSettings();
        Configuration Get(string key);

    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Infrastructure.Context;

namespace TicketGames.Infrastructure.Configuration
{
    public class TicketGamesInitialization : CreateDatabaseIfNotExists<TicketGamesContext>
    {
    }
}

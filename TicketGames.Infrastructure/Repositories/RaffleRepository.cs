using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.Infrastructure.Context;

namespace TicketGames.Infrastructure.Repositories
{
    public class RaffleRepository : IRaffleRepository
    {
        private readonly TicketGamesContext _context;
        public RaffleRepository()
        {
            this._context = new TicketGamesContext();
        }
        public Raffle GetRaffleByProductId(long productId)
        {
            throw new NotImplementedException();
        }
    }
}

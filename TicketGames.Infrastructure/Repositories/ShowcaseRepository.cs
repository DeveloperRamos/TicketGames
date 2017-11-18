using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.Infrastructure.Context;
using System.Data.Entity;

namespace TicketGames.Infrastructure.Repositories
{
    public class ShowcaseRepository : IShowcaseRepository
    {
        private readonly TicketGamesContext _context;
        public ShowcaseRepository()
        {
            this._context = new TicketGamesContext();
        }

        public Showcase GetShowcaseByTypeId(int showcaseTypeId)
        {
            Showcase showcase = this._context.Showcases
                                            .Include(s => s.ShowcaseProducts)
                                            .Where(s => s.ShowcaseTypeId == showcaseTypeId)
                                            .FirstOrDefault();

            return showcase;
        }
    }
}

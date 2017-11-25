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
            List<ShowcaseProduct> showcaseProducts = this._context.ShowcaseProducts
                .Include(s => s.Showcase)
                .Include(s => s.Product.Images)
                .Include(s => s.Product.Category)
                .Include(s => s.Product.Raffles)
                .Where(s => s.Showcase.ShowcaseTypeId == showcaseTypeId && s.Showcase.Active == true && s.Active == true && s.Product.Active == true)
                .ToList();

            return showcaseProducts.Select(s => s.Showcase).FirstOrDefault();
        }
    }
}

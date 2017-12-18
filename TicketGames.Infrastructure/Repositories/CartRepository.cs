using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.Infrastructure.Context;

namespace TicketGames.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly TicketGamesContext _context;
        private string connection = ConfigurationManager.ConnectionStrings["TicketGamesContext"].ConnectionString;
        public CartRepository()
        {
            this._context = new TicketGamesContext();
        }

        public Cart Create(Cart cart)
        {           
            cart.CartStatusId = 2;

            var result = this._context.Set<Cart>().Add(cart);

            this._context.SaveChanges();

            return result;
        }

        public Cart Update(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}

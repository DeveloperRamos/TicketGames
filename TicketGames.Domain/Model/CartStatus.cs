using System.Collections.Generic;

namespace TicketGames.Domain.Model
{
    public class CartStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class OrderStatus
    {
        public OrderStatus()
        {
            this.Orders = new List<Order>();
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}

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
            this.OrderHistory = new List<OrderHistory>();
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string External { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<OrderHistory> OrderHistory { get; set; }
    }
}

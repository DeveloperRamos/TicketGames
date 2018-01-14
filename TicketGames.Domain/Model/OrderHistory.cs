using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class OrderHistory
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime InsertDate { get; set; }
        public Order Order { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

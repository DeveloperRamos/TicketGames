using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class CartItem
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public long RaffleId { get; set; }
        public int Quantity { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
        public virtual Raffle Raffle { get; set; }
    }
}

using System;

namespace TicketGames.Domain.Model
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public long RaffleId { get; set; }
        public float Value { get; set; }
        public int Quantity { get; set; }
        public DateTime InsertDate { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual Raffle Raffle { get; set; }
    }
}

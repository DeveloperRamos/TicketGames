using System;
using System.Collections.Generic;

namespace TicketGames.Domain.Model
{
    public class Raffle
    {
        public Raffle()
        {
            this.CartItems = new List<CartItem>();
            this.OrderItems = new List<OrderItem>();
        }

        public long Id { get; set; }
        public long ProductId { get; set; }
        public int RaffleStatusId { get; set; }
        public Nullable<long> Winner { get; set; }
        public string Name { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public DateTime ExpectedDate { get; set; }
        public Nullable<DateTime> DrawDate { get; set; }
        public int Extend { get; set; }
        public int ParticipantsNumber { get; set; }
        public Nullable<int> DrawNumber { get; set; }
        public virtual RaffleStatus RaffleStatus { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}

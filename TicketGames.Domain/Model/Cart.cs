using System;
using System.Collections.Generic;

namespace TicketGames.Domain.Model
{
    public class Cart
    {
        public Cart()
        {
            this.CartItems = new List<CartItem>();
        }
        public long Id { get; set; }
        public long ParticipantId { get; set; }
        public int CartStatusId { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public virtual Participant Participant { get; set; }
        public virtual CartStatus CartStatus { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}

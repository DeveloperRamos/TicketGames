using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class Order
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();            
            this.OrderHistory = new List<OrderHistory>();
        }
        public long Id { get; set; }
        public long ParticipantId { get; set; }
        public int OrderStatusId { get; set; }
        public string PaymentType { get; set; }
        public float Value { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public float Money { get; set; }
        public float Point { get; set; }
        public virtual Participant Participant { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        //public virtual Credit Credit { get; set; }
        //public virtual Billet Billet { get; set; }
        public ICollection<OrderHistory> OrderHistory { get; set; }

        [NotMapped]
        public long CartId { get; set; }


    }
}

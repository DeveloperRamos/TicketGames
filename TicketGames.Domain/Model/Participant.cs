using System;
using System.Collections.Generic;

namespace TicketGames.Domain.Model
{
    public class Participant
    {
        public Participant()
        {
            this.Sessions = new List<Session>();
            this.Carts = new List<Cart>();
            this.OrderDeliveryAddress = new List<OrderDeliveryAddress>();
        }
        public long Id { get; set; }
        public int ParticipantStatusId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public virtual ParticipantStatus ParticipantStatus { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDeliveryAddress> OrderDeliveryAddress { get; set; }

    }
}

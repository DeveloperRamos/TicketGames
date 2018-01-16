using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class Billet
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string SenderHash { get; set; }
        public string Session { get; set; }
        public float Value { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime InsertDate { get; set; }
        public float FeeAmount { get; set; }
        public float NetAmount { get; set; }
        public string Code { get; set; }
        public string PaymentLink { get; set; }
        public virtual Order Order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Order
{
    public class OrderDetails
    {
        public long OrderId { get; set; }
        public float SubTotal { get; set; }
        public float TotalPoints { get; set; }
        public float TotalMoney { get; set; }
        public PaymentType PaymentType { get; set; }
        public Billet Billet { get; set; }
        public Credit Credit { get; set; }

        public void MappingDetailsByBillet(Domain.Model.Order order, Domain.Model.Billet billet)
        {
            this.OrderId = order.Id;
            this.SubTotal = order.Value;
            this.TotalMoney = order.Money;
            this.TotalPoints = order.Point;

            this.Billet = new Billet()
            {
                LinkPayment = billet.PaymentLink
            };

        }
        public void MappingDetailsByCredit()
        {

        }
    }

    public class Billet
    {
        public string LinkPayment { get; set; }
    }

    public class Credit
    {
        public string Brand { get; set; }
        public int Parcel { get; set; }
        public float Value { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Order
{
    public class Order
    {
        public PaymentType PaymentType { get; set; }
        public Card Card { get; set; }


        //public OrderDeliveryAddress MappingDomain()
        //{
        //    var deliveryAddress = new OrderDeliveryAddress();

        //    deliveryAddress.Name = this.Name;
        //    deliveryAddress.Street = this.Street;
        //    deliveryAddress.Number = this.Number;
        //    deliveryAddress.Complement = this.Complement;
        //    deliveryAddress.District = this.District;
        //    deliveryAddress.City = this.City;
        //    deliveryAddress.State = this.State;
        //    deliveryAddress.ZipCode = this.ZipCode;
        //    deliveryAddress.Reference = this.Reference;
        //    deliveryAddress.Email = this.Email;
        //    deliveryAddress.HomePhone = this.HomePhone;
        //    deliveryAddress.CellPhone = this.CellPhone;

        //    return deliveryAddress;
        //}

    }
    public class Card
    {
        public bool Owner { get; set; }
        public string Brand { get; set; }
        public string Number { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        public string SenderHash { get; set; }
        public string CreditCardToken { get; set; }
        public Parcel Parcel { get; set; }
        public BillingAddress BillingAddress { get; set; }
        public CreditCardHolder CreditCardHolder { get; set; }


    }
    public class Parcel
    {
        public int Quantity { get; set; }
        public float Value { get; set; }

    }

    public class BillingAddress
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
    public class CreditCardHolder
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
    }
}
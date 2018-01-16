using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketGames.Domain.Model;

namespace TicketGames.API.Models.Order
{
    public class Order
    {
        public PaymentType PaymentType { get; set; }
        public string SenderHash { get; set; }
        public Card Card { get; set; }

        public TicketGames.PagSeguro.Model.Billet MappingBillet(Domain.Model.Participant participant, Domain.Model.OrderDeliveryAddress orderDeliveryAddress, TicketGames.Domain.Model.Billet billet)
        {
            TicketGames.PagSeguro.Model.Billet billetPagSeguro = new PagSeguro.Model.Billet();

            billetPagSeguro.Price = billet.Value;

            billetPagSeguro.ShippingAddress = new TicketGames.PagSeguro.Model.ShippingAddress();

            billetPagSeguro.ShippingAddress.Street = orderDeliveryAddress.Street;
            billetPagSeguro.ShippingAddress.Number = orderDeliveryAddress.Number;
            billetPagSeguro.ShippingAddress.Complement = orderDeliveryAddress.Complement;
            billetPagSeguro.ShippingAddress.District = orderDeliveryAddress.District;
            billetPagSeguro.ShippingAddress.ZipCode = orderDeliveryAddress.ZipCode;
            billetPagSeguro.ShippingAddress.City = orderDeliveryAddress.City;
            billetPagSeguro.ShippingAddress.State = orderDeliveryAddress.State;

            billetPagSeguro.Buyer = new TicketGames.PagSeguro.Model.Buyer();

            billetPagSeguro.Session = billet.Session;
            billetPagSeguro.SenderHash = billet.SenderHash;
            billetPagSeguro.Buyer.Name = participant.Name;
            billetPagSeguro.Buyer.CPF = participant.CPF;
            billetPagSeguro.Buyer.DDD = "11";
            billetPagSeguro.Buyer.Phone = "980203026";
            billetPagSeguro.Buyer.Mail = participant.Email;

            return billetPagSeguro;
        }

        public TicketGames.PagSeguro.Model.Credit MappingCredit(Domain.Model.Participant participant, Domain.Model.Cart cart, Domain.Model.OrderDeliveryAddress orderDeliveryAddress, TicketGames.Domain.Model.Credit credit)
        {
            TicketGames.PagSeguro.Model.Credit creditPagSeguro = new TicketGames.PagSeguro.Model.Credit();

            // Add items
            foreach (var item in cart.CartItems)
            {
                creditPagSeguro.Items.Add(new TicketGames.PagSeguro.Model.Item()
                {
                    ProductId = item.ProductId.ToString(),
                    Product = item.Product.Name,
                    Quantity = item.Quantity,
                    Value = (45 / cart.CartItems.Count)
                });
            }

            creditPagSeguro.OrderId = 33873647;


            creditPagSeguro.ShippingAddress = new TicketGames.PagSeguro.Model.ShippingAddress();

            creditPagSeguro.ShippingAddress.Street = orderDeliveryAddress.Street;
            creditPagSeguro.ShippingAddress.Number = orderDeliveryAddress.Number;
            creditPagSeguro.ShippingAddress.Complement = orderDeliveryAddress.Complement;
            creditPagSeguro.ShippingAddress.District = orderDeliveryAddress.District;
            creditPagSeguro.ShippingAddress.ZipCode = orderDeliveryAddress.ZipCode;
            creditPagSeguro.ShippingAddress.City = orderDeliveryAddress.City;
            creditPagSeguro.ShippingAddress.State = orderDeliveryAddress.State;

            if (credit.Owner)
            {
                creditPagSeguro.BillingAddress = new TicketGames.PagSeguro.Model.BillingAddress();

                creditPagSeguro.BillingAddress.Street = participant.Street;
                creditPagSeguro.BillingAddress.Number = participant.Number;
                creditPagSeguro.BillingAddress.Complement = participant.Complement;
                creditPagSeguro.BillingAddress.ZipCode = participant.ZipCode;
                creditPagSeguro.BillingAddress.District = participant.District;
                creditPagSeguro.BillingAddress.City = participant.City;
                creditPagSeguro.BillingAddress.State = participant.State;
            }


            creditPagSeguro.CreditCardHolder = new TicketGames.PagSeguro.Model.CreditCardHolder();

            creditPagSeguro.CreditCardHolder.Name = participant.Name;
            creditPagSeguro.CreditCardHolder.CPF = participant.CPF;
            creditPagSeguro.CreditCardHolder.BirthDate = Convert.ToDateTime("21/05/1989");
            creditPagSeguro.CreditCardHolder.DDD = "11";
            creditPagSeguro.CreditCardHolder.Phone = "980203026";

            creditPagSeguro.Buyer = new TicketGames.PagSeguro.Model.Buyer();

            creditPagSeguro.SenderHash = credit.SenderHash;
            creditPagSeguro.Buyer.Name = participant.Name;
            creditPagSeguro.Buyer.CPF = participant.CPF;
            creditPagSeguro.Buyer.DDD = "11";
            creditPagSeguro.Buyer.Phone = "980203026";
            creditPagSeguro.Buyer.Mail = participant.Email;

            creditPagSeguro.CreditCardToken = credit.CreditCardToken;

            creditPagSeguro.Parcel = new TicketGames.PagSeguro.Model.Parcel();

            creditPagSeguro.Parcel.Quantity = credit.Parcel;
            creditPagSeguro.Parcel.Value = credit.Value;
            creditPagSeguro.Brand = credit.Brand;
            creditPagSeguro.Owner = credit.Owner;
            creditPagSeguro.Session = credit.Session;

            return creditPagSeguro;

        }

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
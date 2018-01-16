using System;
using System.Collections.Generic;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Direct;

namespace TicketGames.PagSeguro.Model
{
    public class Credit
    {
        public long OrderId { get; set; }
        public bool Owner { get; set; }
        public string Brand { get; set; }
        public string Number { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        public string SenderHash { get; set; }
        public string CreditCardToken { get; set; }
        public string Session { get; set; }
        public Buyer Buyer { get; set; }
        public Parcel Parcel { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public BillingAddress BillingAddress { get; set; }
        public CreditCardHolder CreditCardHolder { get; set; }
        public List<Item> Items { get; set; }

        public Credit()
        {
            this.Items = new List<Item>();
        }

        public CreditCardCheckout MappingCreditCheckout()
        {
            // Instantiate a new checkout
            CreditCardCheckout checkout = new CreditCardCheckout();

            // Sets the payment mode
            checkout.PaymentMode = PaymentMode.DEFAULT;
            checkout.PaymentMethod = "creditCard";

            // Sets the receiver e-mail should will get paid
            //checkout.ReceiverEmail = "backoffice@lojamodelo.com.br";
            checkout.ReceiverEmail = "marcio.correia@terra.com.br";

            // Sets the currency
            checkout.Currency = Currency.Brl;

            // Add items
            //foreach (var item in this.Items)
            //{
            //    checkout.Items.Add(new Uol.PagSeguro.Domain.Item(item.ProductId, item.Product, item.Quantity, Convert.ToDecimal(item.Value)));
            //    checkout.Items.Add(new Item("0002", "Notebook Rosa", 2, 150.99m));
            //}

            //foreach (var item in this.Items)
            //{
            //    checkout.Items.Add(new Uol.PagSeguro.Domain.Item(item.ProductId, item.Product, item.Quantity, Convert.ToDecimal(item.Value)));                
            //}

            checkout.Items.Add(new Uol.PagSeguro.Domain.Item("2018", "Ticket Games", 1, Convert.ToDecimal(this.Parcel.Value * this.Parcel.Quantity)));

            // Sets a reference code for this checkout, it is useful to identify this payment in future notifications.
            checkout.Reference = this.OrderId.ToString();

            // Sets shipping information.
            checkout.Shipping = new Shipping();
            checkout.Shipping.ShippingType = ShippingType.Sedex;
            checkout.Shipping.Cost = 0.00m;
            checkout.Shipping.Address = new Address(
                "BRA",
                this.ShippingAddress.State,
                this.ShippingAddress.City,
                this.ShippingAddress.District,
                this.ShippingAddress.ZipCode,
                this.ShippingAddress.Street,
                this.ShippingAddress.Number,
                string.Empty
            );

            // Sets shipping information.
            checkout.Billing = new Billing();
            checkout.Billing.Address = new Address(
                "BRA",
                this.BillingAddress.State,
                this.BillingAddress.City,
                this.BillingAddress.District,
                this.BillingAddress.ZipCode,
                this.BillingAddress.Street,
                this.BillingAddress.Number,
                string.Empty
            );

            // Sets credit card holder information.
            checkout.Holder = new Holder(
                this.CreditCardHolder.Name,
                new Phone(this.CreditCardHolder.DDD, this.CreditCardHolder.Phone),
                new HolderDocument(Documents.GetDocumentByType("CPF"), this.CreditCardHolder.CPF),
                this.CreditCardHolder.BirthDate.ToString("dd/MM/yyyy") //"01/10/1980"
            );

            // Sets your customer information.
            // If you using SANDBOX you must use an email @sandbox.pagseguro.com.br
            checkout.Sender = new Sender(
                this.Buyer.Name,
                this.Buyer.Mail,
                new Phone(this.Buyer.DDD, this.Buyer.Phone)
            );

            checkout.Sender.Hash = this.SenderHash;
            SenderDocument senderCPF = new SenderDocument(Documents.GetDocumentByType("CPF"), this.Buyer.CPF);
            checkout.Sender.Documents.Add(senderCPF);

            // Sets a credit card token.
            checkout.Token = this.CreditCardToken;

            //Sets the installments information
            checkout.Installment = new Uol.PagSeguro.Domain.Direct.Installment(this.Parcel.Quantity, Convert.ToDecimal(this.Parcel.Value), 12);

            // Sets the notification url
            checkout.NotificationURL = "https://ticketgames.com.br";

            return checkout;
        }
    }
}

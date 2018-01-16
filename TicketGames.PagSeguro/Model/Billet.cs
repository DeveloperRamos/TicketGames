using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Direct;

namespace TicketGames.PagSeguro.Model
{
    public class Billet
    {
        public long OrderId { get; set; }
        public string SenderHash { get; set; }
        public float Price { get; set; }
        public string Session { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public Buyer Buyer { get; set; }

        public BoletoCheckout MappingBilletCheckout()
        {
            // Instantiate a new checkout
            BoletoCheckout checkout = new BoletoCheckout();

            // Sets the payment mode
            checkout.PaymentMode = PaymentMode.DEFAULT;
            checkout.PaymentMethod = "boleto";

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

            checkout.Items.Add(new Uol.PagSeguro.Domain.Item("2018", "Ticket Games", 1, Convert.ToDecimal(this.Price)));

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

            // Sets the notification url
            checkout.NotificationURL = "https://ticketgames.com.br";

            return checkout;
        }
        public BoletoCheckout MappingDebitCheckout(int id)
        {
            // Instantiate a new checkout
            BoletoCheckout checkout = new BoletoCheckout();

            // Sets the payment mode
            checkout.PaymentMode = PaymentMode.DEFAULT;


            // Sets the receiver e-mail should will get paid
            checkout.ReceiverEmail = "marcio.correia@terra.com.br"; //Email do participante que irá receber o boleto

            // Sets the currency
            checkout.Currency = Currency.Brl;

            // Extra amount to be added to the transaction total
            //checkout.ExtraAmount = 1.00m;

            // Add items
            checkout.Items.Add(new Uol.PagSeguro.Domain.Item("0001", "Resident Evil 6", 1, 10.00m));
            checkout.Items.Add(new Uol.PagSeguro.Domain.Item("0002", "Megaman Collection", 2, 20.00m));

            // Sets the notification url
            checkout.NotificationURL = "http://www.lojamodelo.com.br";

            // Sets a reference code for this checkout, it is useful to identify this payment in future notifications.
            checkout.Reference = "REF1234"; //Vai o ID do pedido TicketGames

            // Sets your customer information.
            // If you using SANDBOX you must use an email @sandbox.pagseguro.com.br
            checkout.Sender = new Sender(
                "Joao Comprador", //Nome do participante
                "comprador@uol.com.br", // Email do Participante
                new Phone("11", "56273440") // Telefone do Participante
            );

            SenderDocument senderCPF = new SenderDocument(Documents.GetDocumentByType("CPF"), "37482628843"); // CPF do participante
            checkout.Sender.Documents.Add(senderCPF);

            checkout.Sender.Hash = "b2806d600653cbb2b195f317ca9a1a58738187a02c05bf7f2280e2076262e73b"; //Hash gerado no PagSeguroDirectPayment.getSenderHash();

            // Sets shipping information
            checkout.Shipping = new Shipping();
            checkout.Shipping.ShippingType = ShippingType.NotSpecified;
            checkout.Shipping.Cost = 0.00m;
            checkout.Shipping.Address = new Address(
                "BRA",
                "SP",
                "Sao Paulo",
                "Jardim Paulistano",
                "01452002",
                "Av. Brig. Faria Lima",
                "1384",
                "5o andar"
            ); //Endereço de entrega do participante

            return checkout;
        }
    }
}

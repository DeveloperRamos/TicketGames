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
        public BoletoCheckout MappingDebitCheckout()
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
            checkout.ExtraAmount = 1.00m;

            // Add items
            checkout.Items.Add(new Item("0001", "Resident Evil 6", 1, 10.00m));
            checkout.Items.Add(new Item("0002", "Megaman Collection", 2, 20.00m));

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

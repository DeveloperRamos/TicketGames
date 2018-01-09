using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.PagSeguro.Model;

namespace TicketGames.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
        public bool Redemption(Participant participant, Cart cart, OrderDeliveryAddress orderDeliveryAddress, TicketGames.Domain.Model.Credit credit)
        {
            TicketGames.PagSeguro.Model.Credit creditPagSeguro = new TicketGames.PagSeguro.Model.Credit();


            // Add items
            foreach (var item in cart.CartItems)
            {
                creditPagSeguro.Items.Add(new Item()
                {
                    ProductId = item.ProductId.ToString(),
                    Product = item.Product.Name,
                    Quantity = item.Quantity,
                    Value = (45 / cart.CartItems.Count)
                });
            }

            creditPagSeguro.OrderId = 33873647;


            creditPagSeguro.ShippingAddress = new ShippingAddress();

            creditPagSeguro.ShippingAddress.Street = orderDeliveryAddress.Street;
            creditPagSeguro.ShippingAddress.Number = orderDeliveryAddress.Number;
            creditPagSeguro.ShippingAddress.Complement = orderDeliveryAddress.Complement;
            creditPagSeguro.ShippingAddress.District = orderDeliveryAddress.District;
            creditPagSeguro.ShippingAddress.ZipCode = orderDeliveryAddress.ZipCode;
            creditPagSeguro.ShippingAddress.City = orderDeliveryAddress.City;
            creditPagSeguro.ShippingAddress.State = orderDeliveryAddress.State;

            if (credit.Owner)
            {
                creditPagSeguro.BillingAddress = new BillingAddress();

                creditPagSeguro.BillingAddress.Street = participant.Street;
                creditPagSeguro.BillingAddress.Number = participant.Number;
                creditPagSeguro.BillingAddress.Complement = participant.Complement;
                creditPagSeguro.BillingAddress.ZipCode = participant.ZipCode;
                creditPagSeguro.BillingAddress.District = participant.District;
                creditPagSeguro.BillingAddress.City = participant.City;
                creditPagSeguro.BillingAddress.State = participant.State;
            }


            creditPagSeguro.CreditCardHolder = new CreditCardHolder();

            creditPagSeguro.CreditCardHolder.Name = participant.Name;
            creditPagSeguro.CreditCardHolder.CPF = participant.CPF;
            creditPagSeguro.CreditCardHolder.BirthDate = Convert.ToDateTime("21/05/1989");
            creditPagSeguro.CreditCardHolder.DDD = "11";
            creditPagSeguro.CreditCardHolder.Phone = "980203026";

            creditPagSeguro.Buyer = new Buyer();

            creditPagSeguro.SenderHash = credit.SenderHash;
            creditPagSeguro.Buyer.Name = participant.Name;
            creditPagSeguro.Buyer.CPF = participant.CPF;
            creditPagSeguro.Buyer.DDD = "11";
            creditPagSeguro.Buyer.Phone = "980203026";
            creditPagSeguro.Buyer.Mail = "c00710247826950020006@sandbox.pagseguro.com.br";//participant.Email;

            creditPagSeguro.CreditCardToken = credit.CreditCardToken;

            creditPagSeguro.Parcel = new Parcel();

            creditPagSeguro.Parcel.Quantity = credit.Parcel;
            creditPagSeguro.Parcel.Value = credit.Value;


            PagSeguro.Transaction trans = new PagSeguro.Transaction();


            trans.CreditCheckout(creditPagSeguro);

            return true;
        }
    }
}

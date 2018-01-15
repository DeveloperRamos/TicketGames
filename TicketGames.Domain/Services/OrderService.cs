using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.PagSeguro;
using TicketGames.PagSeguro.Model;

namespace TicketGames.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ITransactionRepository transactionRepository, ICartRepository cartRepository)
        {
            this._orderRepository = orderRepository;
            this._transactionRepository = transactionRepository;
            this._cartRepository = cartRepository;            
        }

        public List<PagSeguro.Model.Installment> Installments(decimal amount, string creditCardBrand, int maxInstallmentNoInterest)
        {
            var installment = new PagSeguro.Model.Installment();

            List<PagSeguro.Model.Installment> installments = installment.GetInstallments(amount, creditCardBrand, maxInstallmentNoInterest);

            return installments;

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

        public long Redemption(PagSeguro.Model.Credit credit, Domain.Model.Transaction transaction, Domain.Model.Order order)
        {
            PagSeguro.Transaction trans = new PagSeguro.Transaction();
            var resultTransaction = new Domain.Model.Transaction();

            var orderCreate = this._orderRepository.Create(order);

            if (orderCreate.Id > 0)
            {
                credit.OrderId = orderCreate.Id;

                if (transaction.Value > 0)
                {
                    transaction.OrderId = orderCreate.Id;

                    transaction.Description = string.Format(transaction.Description, orderCreate.Id);

                    resultTransaction = this._transactionRepository.CreateTransaction(transaction);
                    //Order Debited
                    this._orderRepository.CreateOrderHistory(new OrderHistory() { OrderId = orderCreate.Id, OrderStatusId = 9 });
                }

                var pagSeguro = trans.CreditCheckout(credit);

                if (pagSeguro.Success)
                {
                    Domain.Model.Credit creditCreate = new Model.Credit();

                    creditCreate.OrderId = orderCreate.Id;
                    creditCreate.Owner = credit.Owner;
                    creditCreate.Brand = credit.Brand;
                    creditCreate.SenderHash = credit.SenderHash;
                    creditCreate.CreditCardToken = credit.CreditCardToken;
                    creditCreate.Session = credit.Session;
                    creditCreate.Parcel = credit.Parcel.Quantity;
                    creditCreate.Value = credit.Parcel.Value;
                    creditCreate.SubTotal = order.Money;
                    creditCreate.Holder = credit.CreditCardHolder.Name;
                    creditCreate.CPF = credit.CreditCardHolder.CPF;
                    creditCreate.DateNasc = credit.CreditCardHolder.BirthDate;
                    creditCreate.Contact = credit.CreditCardHolder.DDD + credit.CreditCardHolder.Phone;

                    creditCreate.Street = credit.BillingAddress.Street;
                    creditCreate.Number = credit.BillingAddress.Number;
                    creditCreate.Complement = credit.BillingAddress.Complement;
                    creditCreate.ZipCode = credit.BillingAddress.ZipCode;
                    creditCreate.District = credit.BillingAddress.District;
                    creditCreate.City = credit.BillingAddress.City;
                    creditCreate.State = credit.BillingAddress.State;

                    creditCreate.FeeAmount = Convert.ToSingle(pagSeguro.FeeAmount);
                    creditCreate.NetAmount = Convert.ToSingle(pagSeguro.NetAmount);
                    creditCreate.Code = pagSeguro.Code;

                    this._orderRepository.CreateCreditByOrderId(creditCreate);


                    int type = Enum.Parse(typeof(Domain.Model.Enum.OrderStatus), pagSeguro.TransactionStatus).GetHashCode();

                    //Order Debited
                    this._orderRepository.CreateOrderHistory(new OrderHistory() { OrderId = orderCreate.Id, OrderStatusId = type });

                    this._orderRepository.UpdateOrderDeliveryAddressByCartId(order.CartId, orderCreate.Id);

                    this._cartRepository.UpdateStatusByCartId(order.CartId, 1);
                }

                return orderCreate.Id;
            }

            return 0;
        }
    }
}

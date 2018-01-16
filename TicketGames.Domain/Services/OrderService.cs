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

        public long Redemption(PagSeguro.Model.Billet billet, Model.Transaction transaction, Order order)
        {
            PagSeguro.Transaction trans = new PagSeguro.Transaction();
            var resultTransaction = new Domain.Model.Transaction();

            var orderCreate = this._orderRepository.Create(order);

            if (orderCreate.Id > 0)
            {
                billet.OrderId = orderCreate.Id;

                if (transaction.Value > 0)
                {
                    transaction.OrderId = orderCreate.Id;

                    transaction.Description = string.Format(transaction.Description, orderCreate.Id);

                    resultTransaction = this._transactionRepository.CreateTransaction(transaction);
                    //Order Debited
                    this._orderRepository.CreateOrderHistory(new OrderHistory() { OrderId = orderCreate.Id, OrderStatusId = 9 });
                }

                var pagSeguro = trans.BilletCheckout(billet);

                if (pagSeguro.Success)
                {
                    Domain.Model.Billet billetCreate = new Model.Billet();

                    billetCreate.OrderId = orderCreate.Id;
                    billetCreate.SenderHash = billet.SenderHash;

                    billetCreate.Session = billet.Session;

                    billetCreate.Value = order.Money;
                    billetCreate.Name = billet.Buyer.Name;
                    billetCreate.CPF = billet.Buyer.CPF;
                    billetCreate.Email = billet.Buyer.Mail;


                    billetCreate.Street = billet.ShippingAddress.Street;
                    billetCreate.Number = billet.ShippingAddress.Number;
                    billetCreate.Complement = billet.ShippingAddress.Complement;
                    billetCreate.ZipCode = billet.ShippingAddress.ZipCode;
                    billetCreate.District = billet.ShippingAddress.District;
                    billetCreate.City = billet.ShippingAddress.City;
                    billetCreate.State = billet.ShippingAddress.State;

                    billetCreate.FeeAmount = Convert.ToSingle(pagSeguro.FeeAmount);
                    billetCreate.NetAmount = Convert.ToSingle(pagSeguro.NetAmount);
                    billetCreate.Code = pagSeguro.Code;
                    billetCreate.PaymentLink = pagSeguro.PaymentLink;

                    this._orderRepository.CreateBilletByOrderId(billetCreate);

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

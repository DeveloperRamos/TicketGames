using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using TicketGames.API.Models.Account;
using TicketGames.API.Models.Order;
using TicketGames.API.Models.Participant;
using TicketGames.CrossCutting.Cache;
using TicketGames.CrossCutting.Cache.Redis;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Services;
using TicketGames.Infrastructure.Repositories;

namespace TicketGames.API.Controllers
{
    [RoutePrefix("v1/order")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class OrderController : ApiController
    {
        private readonly IParticipantService _participantService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly ITransactionService _transactionService;
        private long participantId = 0;


        public OrderController(IParticipantService participantService, ICartService cartService, IOrderService orderService, ITransactionService transactionService)
        {
            this._participantService = participantService;
            this._cartService = cartService;
            this._orderService = orderService;
            this._transactionService = transactionService;
        }
        public OrderController()
            : this(new ParticipantService(new ParticipantRepository()), new CartService(new CartRepository()), new OrderService(new OrderRepository(), new TransactionRepository(), new CartRepository()), new TransactionService(new TransactionRepository()))
        {
            CacheManager.SetProvider(new CacheProvider());
        }
        [Authorize]
        [HttpPost, Route()]
        public IHttpActionResult Redemption(Order order)
        {
            try
            {
                long orderId = 0;

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out participantId);

                if (this.participantId > 0)
                {
                    #region Get Participant

                    Participant participant = null;

                    var participantKey = string.Concat("Participant:Id:", this.participantId.ToString(), ":Register");

                    participant = CacheManager.GetObject<Participant>(participantKey);

                    if (participant == null)
                    {
                        var result = this._participantService.GetParticipant(this.participantId);

                        participant = new Participant(result);
                    }


                    var domainParticipant = participant.MappingDomain();


                    #endregion

                    #region Get Cart

                    Domain.Model.Cart domainCart = null;

                    var cartKey = string.Concat("Participant:Id:", this.participantId.ToString(), ":Cart");

                    domainCart = CacheManager.GetObject<Domain.Model.Cart>(cartKey);

                    if (domainCart == null)
                    {
                        domainCart = this._cartService.Get(this.participantId);
                    }

                    var deliveryAddressResult = this._cartService.Get(this.participantId, domainCart.Id);

                    var carts = new List<TicketGames.API.Models.Order.Cart>();

                    carts = new TicketGames.API.Models.Order.Cart().CreateCart(domainCart);

                    #endregion

                    #region Get Balance

                    Account account = new Account();

                    var transactions = this._transactionService.GetTransactions(participantId);

                    if (transactions.Count > 0)
                    {
                        account = new Account().Balances(transactions);
                    }

                    #endregion

                    #region Get Session

                    string session = null;

                    var key = string.Concat("Participant:Id:", participantId.ToString(), ":Session");

                    session = CacheManager.GetObject<string>(key);

                    #endregion

                    float total = 0;

                    carts.ForEach(c =>
                    total += c.Price * c.Quantity
                    );

                    var transaction = new Domain.Model.Transaction();
                    var orderDomain = new Domain.Model.Order();

                    orderDomain.ParticipantId = this.participantId;

                    //Status de pedido criado
                    orderDomain.OrderStatusId = 4;

                    orderDomain.OrderHistory.Add(new Domain.Model.OrderHistory() { OrderStatusId = 4 });

                    orderDomain.Value = total;

                    switch (order.PaymentType)
                    {
                        case PaymentType.Point:
                            {
                                break;
                            }
                        case PaymentType.Billet:
                            {
                                break;
                            }
                        case PaymentType.Credit:
                            {
                                Decimal money = Convert.ToDecimal(total - account.Balance);

                                orderDomain.Money = Convert.ToSingle(money);

                                orderDomain.PaymentType = "Credit";

                                foreach (var cart in carts)
                                {
                                    orderDomain.OrderItems.Add(new Domain.Model.OrderItem()
                                    {
                                        ProductId = cart.ProductId,
                                        RaffleId = cart.RaffleId,
                                        Value = cart.Price,
                                        Quantity = cart.Quantity
                                    });

                                    orderDomain.CartId = cart.CartId;
                                }

                                if (account.Balance > 0)
                                {
                                    transaction.ParticipantId = this.participantId;
                                    transaction.TransactionTypeId = 2;
                                    transaction.Description = "Fechamento do pedido {0}";
                                    transaction.Value = account.Balance;

                                    orderDomain.Point = account.Balance;
                                    orderDomain.PaymentType = "Point + Credit";
                                }

                                var credit = new Domain.Model.Credit();

                                credit.Owner = order.Card.Owner;
                                credit.SenderHash = order.Card.SenderHash;
                                credit.Brand = order.Card.Brand;
                                credit.CreditCardToken = order.Card.CreditCardToken;
                                credit.Session = session;

                                credit.Parcel = order.Card.Parcel.Quantity;
                                credit.Value = order.Card.Parcel.Value;

                                var paymentCredit = order.MappingCredit(domainParticipant, domainCart, deliveryAddressResult, credit);

                                orderId = this._orderService.Redemption(paymentCredit, transaction, orderDomain);

                                if (orderId > 0)
                                {
                                    CacheManager.KeyDelete(cartKey);
                                }


                                break;
                            }
                        default: break;
                    }



                    var pedido = order;
                }

                return Ok(orderId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet, Route("installment/{brand}")]
        public IHttpActionResult GetInstallments(string brand)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out participantId);

                if (this.participantId > 0)
                {
                    #region Get Balance

                    Account account = new Account();

                    var result = this._transactionService.GetTransactions(participantId);

                    if (result.Count > 0)
                    {
                        account = new Account().Balances(result);
                    }

                    #endregion

                    #region Get Cart

                    Domain.Model.Cart domainCart = null;

                    var key = string.Concat("Participant:Id:", this.participantId.ToString(), ":Cart");

                    domainCart = CacheManager.GetObject<Domain.Model.Cart>(key);

                    if (domainCart == null)
                    {
                        domainCart = this._cartService.Get(this.participantId);
                    }

                    var carts = new List<TicketGames.API.Models.Order.Cart>();

                    carts = new TicketGames.API.Models.Order.Cart().CreateCart(domainCart);

                    #endregion

                    float total = 0;

                    carts.ForEach(c =>
                    total += c.Price * c.Quantity
                    );

                    Decimal money = Convert.ToDecimal(total - account.Balance);

                    var installments = this._orderService.Installments(money, brand, 12);

                    return Ok(installments);
                }
                else
                {
                    return BadRequest("Precisa está logado!");
                }


            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}

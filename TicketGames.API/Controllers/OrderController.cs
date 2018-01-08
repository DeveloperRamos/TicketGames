using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
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
        private long participantId = 0;


        public OrderController(IParticipantService participantService, ICartService cartService, IOrderService orderService)
        {
            this._participantService = participantService;
            this._cartService = cartService;
            this._orderService = orderService;
        }
        public OrderController()
            : this(new ParticipantService(new ParticipantRepository()), new CartService(new CartRepository()), new OrderService(new OrderRepository()))
        {
            CacheManager.SetProvider(new CacheProvider());
        }
        [Authorize]
        [HttpPost, Route()]
        public IHttpActionResult Redemption(Order order)
        {
            try
            {
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

                    #endregion


                    var credit = new Domain.Model.Credit();

                    credit.Owner = order.Card.Owner;
                    credit.SenderHash = order.Card.SenderHash;


                    credit.CreditCardToken = order.Card.CreditCardToken;

                    credit.Quantity = order.Card.Parcel.Quantity;
                    credit.Value = order.Card.Parcel.Value;

                    this._orderService.Redemption(domainParticipant, domainCart, deliveryAddressResult, credit);


                    var pedido = order;
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

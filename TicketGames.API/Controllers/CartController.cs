using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TicketGames.API.Models.Order;
using TicketGames.CrossCutting.Cache;
using TicketGames.CrossCutting.Cache.Redis;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Model;
using TicketGames.Domain.Services;
using TicketGames.Infrastructure.Repositories;

namespace TicketGames.API.Controllers
{
    [RoutePrefix("v1/cart")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class CartController : ApiController
    {
        private readonly ICartService _cartService;
        private readonly IRaffleService _raffleService;
        private long participantId = 0;

        public CartController(ICartService cartService, IRaffleService raffleService)
        {
            this._cartService = cartService;
            this._raffleService = raffleService;
        }
        public CartController()
            : this(new CartService(new CartRepository()), new RaffleService(new RaffleRepository()))
        {
            CacheManager.SetProvider(new CacheProvider());

        }

        [Authorize]
        [HttpGet, Route()]
        public IHttpActionResult Get()
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out this.participantId);

                if (participantId > 0)
                {

                    Domain.Model.Cart domainCart = null;

                    var key = string.Concat("Participant:Id:", this.participantId.ToString(), ":Cart");

                    domainCart = CacheManager.GetObject<Domain.Model.Cart>(key);

                    var cart = new List<TicketGames.API.Models.Order.Cart>();

                    if (domainCart != null)
                    {
                        cart = new TicketGames.API.Models.Order.Cart().CreateCart(domainCart);
                    }
                    else
                    {
                        domainCart = this._cartService.Get(this.participantId);

                        cart = new TicketGames.API.Models.Order.Cart().CreateCart(domainCart);

                        CacheManager.StoreObject(key, domainCart, LifetimeProfile.Long);
                    }

                    return Ok(cart);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [Authorize]
        [HttpPost, Route("{addCart}")]
        public IHttpActionResult Post([FromBody]TicketGames.API.Models.Order.Cart cart, bool addCart)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out this.participantId);

                Domain.Model.Cart domainCart = null;

                var key = string.Concat("Participant:Id:", this.participantId.ToString(), ":Cart");

                domainCart = CacheManager.GetObject<Domain.Model.Cart>(key);

                if (!addCart)
                {
                    if (domainCart != null)
                    {
                        domainCart.ParticipantId = this.participantId;

                        var cartItem = domainCart.CartItems.Where(x => x.ProductId == cart.ProductId);

                        if (cartItem.Count() > 0)
                        {
                            var item = cartItem.First();

                            if (cart.Quantity >= item.Quantity)
                            {
                                item.Quantity = cart.Quantity == item.Quantity ? item.Quantity + 1 : cart.Quantity + item.Quantity;
                            }
                            else if (cart.Quantity >= 0 && cart.Quantity < item.Quantity)
                            {
                                item.Quantity++;
                            }
                            else
                            {
                                item.Quantity++;
                            }
                        }
                        else
                        {
                            Domain.Model.CartItem item = new Domain.Model.CartItem();

                            var raffle = this._raffleService.GetRaffle(cart.ProductId);
                            item.ProductId = cart.ProductId;
                            item.Product = raffle.Product;

                            raffle.Product = null;

                            item.RaffleId = raffle.Id;
                            item.Raffle = raffle;
                            item.Quantity = cart.Quantity <= 0 ? 1 : cart.Quantity;

                            domainCart.CartItems.Add(item);
                        }

                        CacheManager.StoreObject(key, domainCart, LifetimeProfile.Long);
                    }
                    else
                    {
                        domainCart = new Domain.Model.Cart();

                        var raffle = this._raffleService.GetRaffle(cart.ProductId);

                        domainCart.ParticipantId = this.participantId;
                        domainCart.CartStatusId = 2;
                        Domain.Model.CartItem cartItem = new Domain.Model.CartItem();

                        cartItem.ProductId = cart.ProductId;
                        cartItem.Product = raffle.Product;

                        raffle.Product = null;

                        cartItem.RaffleId = raffle.Id;
                        cartItem.Raffle = raffle;
                        cartItem.Quantity = cart.Quantity;

                        domainCart.CartItems.Add(cartItem);

                        CacheManager.StoreObject(key, domainCart, LifetimeProfile.Long);
                    }
                }
                else
                {
                    var result = this._cartService.Add(domainCart);

                    foreach (var item in result.CartItems)
                    {
                        item.Cart = null;
                    }

                    CacheManager.StoreObject(key, result, LifetimeProfile.Long);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPut, Route()]
        public IHttpActionResult Put()
        {
            return Ok();
        }

        [HttpDelete, Route("{cartId}")]
        public IHttpActionResult Delete(long cartId)
        {
            return Ok(cartId);
        }


        [Authorize]
        [HttpPost, Route("Address")]
        public IHttpActionResult CreateOrderDeliveryAddress([FromBody]DeliveryAddress deliveryAddress)
        {
            try
            {
                OrderDeliveryAddress orderDeliveryAddress = null;

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out this.participantId);

                var validation = deliveryAddress.ValidationAddress();

                if (!string.IsNullOrEmpty(validation))
                {
                    return BadRequest(validation);
                }

                var cart = this._cartService.Get(this.participantId);

                var deliveryAddressResult = this._cartService.Get(this.participantId, cart.Id);

                if (deliveryAddressResult.Id > 0)
                {

                    orderDeliveryAddress = deliveryAddress.MappingDomain();
                    orderDeliveryAddress.Id = deliveryAddressResult.Id;
                    orderDeliveryAddress.ParticipantId = this.participantId;
                    orderDeliveryAddress.CartId = cart.Id;

                    orderDeliveryAddress = this._cartService.Add(orderDeliveryAddress);
                }
                else
                {
                    orderDeliveryAddress = deliveryAddress.MappingDomain();
                    orderDeliveryAddress.ParticipantId = this.participantId;
                    orderDeliveryAddress.CartId = cart.Id;

                    orderDeliveryAddress = this._cartService.Add(orderDeliveryAddress);
                }

                return Ok("Endereço cadastrado!");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}

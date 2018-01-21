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
        private readonly IParticipantService _participantService;
        private long participantId = 0;

        public CartController(ICartService cartService, IRaffleService raffleService, IParticipantService participantService)
        {
            this._cartService = cartService;
            this._raffleService = raffleService;
            this._participantService = participantService;
        }
        public CartController()
            : this(new CartService(new CartRepository()), new RaffleService(new RaffleRepository()), new ParticipantService(new ParticipantRepository()))
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

                        if (domainCart.Id > 0)
                        {
                            cart = new TicketGames.API.Models.Order.Cart().CreateCart(domainCart);

                            CacheManager.StoreObject(key, domainCart, LifetimeProfile.Long);
                        }
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
                long cartId = 0;

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
                    domainCart = this._cartService.Add(domainCart);

                    domainCart = this._cartService.Get(this.participantId);

                    foreach (var item in domainCart.CartItems)
                    {
                        item.Cart = null;
                        item.Product.CartItems = null;
                    }

                    cartId = domainCart.Id;

                    CacheManager.StoreObject(key, domainCart, LifetimeProfile.Long);
                }

                return Ok(cartId);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPut, Route()]
        public IHttpActionResult Put([FromBody]TicketGames.API.Models.Order.Cart cart)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out this.participantId);

                Domain.Model.Cart domainCart = null;

                var key = string.Concat("Participant:Id:", this.participantId.ToString(), ":Cart");

                domainCart = CacheManager.GetObject<Domain.Model.Cart>(key);

                if (domainCart != null)
                {
                    var cartItem = domainCart.CartItems.Where(i => i.ProductId == cart.ProductId).First();

                    if (cart.Quantity > 0)
                    {
                        cartItem.Quantity = cart.Quantity;

                        CacheManager.StoreObject(key, domainCart, LifetimeProfile.Long);
                    }
                }

                return Ok("Carrinho atualizado!");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete, Route("{productId}")]
        public IHttpActionResult Delete(long productId)
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out this.participantId);


                Domain.Model.Cart domainCart = null;

                var key = string.Concat("Participant:Id:", this.participantId.ToString(), ":Cart");

                domainCart = CacheManager.GetObject<Domain.Model.Cart>(key);

                if (domainCart != null)
                {
                    if (domainCart.CartItems.Count == 1)
                    {
                        CacheManager.KeyDelete(key);
                    }
                    else
                    {
                        domainCart.CartItems.Remove(domainCart.CartItems.Where(i => i.ProductId == productId).First());

                        CacheManager.StoreObject(key, domainCart, LifetimeProfile.Long);
                    }
                }

                var result = this._cartService.Delete(this.participantId, productId);

                return Ok("Item removido!");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [Authorize]
        [HttpPost, Route("Address")]
        public IHttpActionResult CreateOrderDeliveryAddress([FromBody]DeliveryAddress deliveryAddress)
        {
            try
            {
                OrderDeliveryAddress orderDeliveryAddress = null;

                long cartId = 0;

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


                cartId = cart.Id;

                return Ok(cartId);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet, Route("Address/{cartId}")]
        public IHttpActionResult CreateOrderDeliveryAddress(long cartId)
        {
            try
            {
                OrderDeliveryAddress orderDeliveryAddress = null;

                DeliveryAddress deliveryAddress = null;

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out this.participantId);

                var deliveryAddressResult = this._cartService.Get(this.participantId, cartId);

                if (deliveryAddressResult != null && deliveryAddressResult.Id > 0)
                {
                    deliveryAddress = new DeliveryAddress(deliveryAddressResult);
                }
                else
                {
                    TicketGames.API.Models.Participant.Participant participant = null;

                    var key = string.Concat("Participant:Id:", participantId.ToString(), ":Register");

                    participant = CacheManager.GetObject<TicketGames.API.Models.Participant.Participant>(key);

                    if (participant == null)
                    {
                        var result = this._participantService.GetParticipant(participantId);

                        participant = new TicketGames.API.Models.Participant.Participant(result);
                    }

                    deliveryAddress = new DeliveryAddress(participant);
                }

                return Ok(deliveryAddress);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}

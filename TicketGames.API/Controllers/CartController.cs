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
                long participantId;

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out participantId);

                if (participantId > 0)
                {

                    return Ok();
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

        [HttpPost, Route("{addCart}")]
        public IHttpActionResult Post([FromBody]Cart cart, bool addCart)
        {
            try
            {
                long participantId;

                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                long.TryParse(principal.Claims.Where(c => c.Type == "participant_Id").Single().Value, out participantId);

                Domain.Model.Cart domainCart = null;

                var key = string.Concat("Participant:Id:", participantId.ToString(), ":Cart");

                domainCart = CacheManager.GetObject<Domain.Model.Cart>(key);

                if (!addCart)
                {
                    if (domainCart != null)
                    {
                        domainCart.ParticipantId = participantId;

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

                            item.ProductId = cart.ProductId;
                            item.RaffleId = this._raffleService.GetRaffle(cart.ProductId).Id;
                            item.Quantity = cart.Quantity <= 0 ? 1 : cart.Quantity;

                            domainCart.CartItems.Add(item);
                        }

                        CacheManager.StoreObject(key, domainCart, LifetimeProfile.Long);
                    }
                    else
                    {
                        domainCart = new Domain.Model.Cart();

                        domainCart.ParticipantId = participantId;
                        domainCart.CartStatusId = 2;
                        Domain.Model.CartItem cartItem = new Domain.Model.CartItem();

                        cartItem.ProductId = cart.ProductId;
                        cartItem.RaffleId = this._raffleService.GetRaffle(cart.ProductId).Id;
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

    }
}

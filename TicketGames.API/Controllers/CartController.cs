using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TicketGames.API.Models.Order;

namespace TicketGames.API.Controllers
{
    [RoutePrefix("v1/cart")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class CartController : ApiController
    {

        [HttpGet, Route()]
        public IHttpActionResult Get()
        {

            List<Cart> carts = new List<Cart>();
            Cart cartProduct1 = new Cart();
            Cart cartProduct2 = new Cart();
            Cart cartProduct3 = new Cart();

            cartProduct1.Id = 1;
            cartProduct1.ProductId = 150;
            cartProduct1.Product = "GTA V";
            cartProduct1.Qtd = 1;
            cartProduct1.Price = 10.00M;

            cartProduct2.Id = 2;
            cartProduct2.ProductId = 310;
            cartProduct2.Product = "Mario Kart 8 Deluxe";
            cartProduct2.Qtd = 1;
            cartProduct2.Price = 20.00M;

            cartProduct3.Id = 3;
            cartProduct3.ProductId = 263;
            cartProduct3.Product = "Forza Horizon 3";
            cartProduct3.Qtd = 1;
            cartProduct3.Price = 10.00M;

            carts.Add(cartProduct1);
            carts.Add(cartProduct2);
            carts.Add(cartProduct3);

            return Ok(carts);
        }

        public IHttpActionResult Post()
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

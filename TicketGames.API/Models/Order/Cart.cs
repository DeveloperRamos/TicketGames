using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketGames.CrossCutting.Raffle;

namespace TicketGames.API.Models.Order
{
    public class Cart
    {
        public long ProductId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public Domain.Model.Cart MappingDomain()
        {
            return new Domain.Model.Cart();
        }

        public List<Cart> CreateCart(Domain.Model.Cart cart)
        {
            List<Cart> carts = new List<Cart>();

            foreach (var item in cart.CartItems)
            {
                item.Product.Raffles = new List<Domain.Model.Raffle>();
                item.Product.Raffles.Add(item.Raffle);

                carts.Add(new Cart()
                {
                    ProductId = item.ProductId,
                    Product = item.Product.Name,
                    Quantity = item.Quantity,
                    Price = new Raffle().value(item.Product)
                });
            }

            return carts;
        }
    }
}
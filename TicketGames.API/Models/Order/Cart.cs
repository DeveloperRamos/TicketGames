using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Order
{
    public class Cart
    {   
        public long ProductId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Domain.Model.Cart MappingDomain()
        {
            return new Domain.Model.Cart();
        }
    }
}
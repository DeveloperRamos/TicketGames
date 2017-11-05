using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Order
{
    public class Cart
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public int Qtd { get; set; }
        public decimal Price { get; set; }
    }
}
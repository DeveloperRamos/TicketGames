using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.PagSeguro.Model
{
    public class Item
    {

        public string ProductId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public float Value { get; set; }
    }
}

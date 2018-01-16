using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.PagSeguro.Model
{
    public class CreditCardHolder
    {

        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public string DDD { get; set; }
        public string Phone { get; set; }
    }
}

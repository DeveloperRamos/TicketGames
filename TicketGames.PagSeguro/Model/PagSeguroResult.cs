using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uol.PagSeguro.Enums;

namespace TicketGames.PagSeguro.Model
{
    public class PagSeguroResult
    {
        public string Code { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string Reference { get; set; }
        public string TransactionStatus { get; set; }
        public string PaymentLink { get; set; }
        public bool Success { get; set; }

    }
}

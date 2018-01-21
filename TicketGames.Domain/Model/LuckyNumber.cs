using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class LuckyNumber
    {
        public long Id { get; set; }
        public long RaffleId { get; set; }
        public long CartId { get; set; }
        public long OrderId { get; set; }
        public int Number { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public Raffle Raffle { get; set; }
    }
}

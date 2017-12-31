using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class Transaction
    {
        public long Id { get; set; }
        public long ParticipantId { get; set; }
        public int TransactionTypeId { get; set; }
        public float Value { get; set; }
        public string Description { get; set; }
        public long RaffleId { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public virtual Participant Participant { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}

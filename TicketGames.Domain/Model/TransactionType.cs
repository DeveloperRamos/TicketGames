using System.Collections.Generic;

namespace TicketGames.Domain.Model
{
    public class TransactionType
    {
        public TransactionType()
        {
            this.Transactions = new List<Transaction>();
        }
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}

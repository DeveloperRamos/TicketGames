using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class Session
    {
        public long Id { get; set; }
        public string Session_ { get; set; }
        public long ParticipantId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public bool Activated { get; set; }

        public virtual Participant Participant { get; set; }
    }
}

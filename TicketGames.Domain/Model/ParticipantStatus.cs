using System.Collections.Generic;

namespace TicketGames.Domain.Model
{
    public class ParticipantStatus
    {
        public ParticipantStatus()
        {
            this.Participants = new List<Participant>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
    }
}

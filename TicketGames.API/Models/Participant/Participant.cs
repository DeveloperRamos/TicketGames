using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Participant
{
    public class Participant
    {
        public long ParticipantId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }

        public Participant()
        {

        }
        
    }
}
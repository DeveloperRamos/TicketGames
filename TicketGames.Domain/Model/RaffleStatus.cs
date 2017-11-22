﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class RaffleStatus
    {
        public RaffleStatus()
        {
            this.Raffles = new List<Raffle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Raffle> Raffles { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Contract
{
    public interface IRaffleService
    {
        Task<Raffle> GetRaffleAsync(long productId);
        Raffle GetRaffle(long productId);
    }
}

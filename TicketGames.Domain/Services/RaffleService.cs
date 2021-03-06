﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;

namespace TicketGames.Domain.Services
{
    public class RaffleService : IRaffleService
    {
        private readonly IRaffleRepository _raffleRepository;

        public RaffleService(IRaffleRepository raffleRepository)
        {
            this._raffleRepository = raffleRepository;
        }

        public async Task<Raffle> GetRaffleAsync(long productId)
        {
            return await this._raffleRepository.GetRaffleAsyncByProductId(productId);
        }

        Raffle IRaffleService.GetRaffle(long productId)
        {
            return this._raffleRepository.GetRaffleByProductId(productId);
        }
    }
}

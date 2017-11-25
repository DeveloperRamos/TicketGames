using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Catalog
{
    public class Raffle
    {
        public DateTime? RaffleDate { get; set; }
        public int SalesMade { get; set; }
        public int MissingtoSell { get; set; }

        public Raffle()
        {

        }

        public Raffle(Domain.Model.Raffle Raffle)
        {
            this.SalesMade = 100;
            this.MissingtoSell = 0;
            this.RaffleDate = (RaffleStatus)Raffle.RaffleStatusId == RaffleStatus.Prolonged ? Raffle.DrawDate : Raffle.ExpectedDate;
        }

        public Raffle(List<Domain.Model.Raffle> raffles)
        {
            if (raffles != null && raffles.Count > 0)
            {
                this.SalesMade = 100;
                this.MissingtoSell = 0;
                this.RaffleDate = (RaffleStatus)raffles[0].RaffleStatusId == RaffleStatus.Prolonged ? raffles[0].DrawDate : raffles[0].ExpectedDate;
            }
        }
    }
}
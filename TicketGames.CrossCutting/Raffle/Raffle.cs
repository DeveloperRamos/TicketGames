using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.CrossCutting.Raffle
{
    public class Raffle
    {
        public float value(Product product)
        {
            float value = product.Value;
            int qtd = product.Raffles.Select(s => s.ParticipantsNumber).FirstOrDefault();

            float result = (float)Math.Ceiling(value / qtd);

            return result;
        }
    }
}

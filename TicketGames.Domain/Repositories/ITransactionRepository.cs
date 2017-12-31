using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Repositories
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactionsByParticipantId(long participantId);
    }
}

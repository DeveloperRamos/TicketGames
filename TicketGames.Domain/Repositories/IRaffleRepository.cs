using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Repositories
{
    public interface IRaffleRepository
    {
        Task<Raffle> GetRaffleAsyncByProductId(long productId);
        Raffle GetRaffleByProductId(long productId);
    }
}

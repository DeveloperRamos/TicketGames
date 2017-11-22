using TicketGames.Domain.Model;

namespace TicketGames.Domain.Repositories
{
    public interface IRaffleRepository
    {
        Raffle GetRaffleByProductId(long productId);
    }
}

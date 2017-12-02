using System.Collections.Generic;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Contract
{
    public interface IShowcaseService
    {
        Showcase GetShowcase(int typeId);

        List<Product> GetProducts(int showcaseTypeId);
    }
}

using System.Collections.Generic;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Contract
{
    public interface ICatalogService
    {
        List<Category> GetCategories();
        Product GetProduct(long productId);
    }
}

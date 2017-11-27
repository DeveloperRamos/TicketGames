using System.Collections.Generic;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Repositories
{
    public interface ICatalogRepository
    {
        List<Category> GetCategories();
        Product GetProductById(long id);
        List<Product> GetProducts(int categoryId);
        List<Product> GetProducts(string name);
        List<Product> GetRecentProductsByCategory(long categoryId);
    }
}

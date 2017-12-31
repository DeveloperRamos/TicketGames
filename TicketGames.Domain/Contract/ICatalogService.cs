using System.Collections.Generic;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Contract
{
    public interface ICatalogService
    {
        List<Category> GetCategories();
        Product GetProduct(long productId);
        List<Product> GetProducts(int categoryId);
        List<Product> GetProducts(string name);
        List<Product> GetRecentProducts(long categoryId);
        List<Product> GetProducts(int categoryId, string name);
        bool CreateOrUpdateImage(Product product);
    }
}

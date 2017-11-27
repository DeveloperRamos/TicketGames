using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;

namespace TicketGames.Domain.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogsRepository)
        {
            this._catalogRepository = catalogsRepository;
        }

        public List<Category> GetCategories()
        {
            return this._catalogRepository.GetCategories();
        }

        public Product GetProduct(long productId)
        {
            return this._catalogRepository.GetProductById(productId);
        }

        public List<Product> GetProducts(int categoryId)
        {
            return this._catalogRepository.GetProducts(categoryId);
        }

        public List<Product> GetProducts(string name)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetRecentProducts(long categoryId)
        {
            return this._catalogRepository.GetRecentProductsByCategory(categoryId);
        }
    }
}

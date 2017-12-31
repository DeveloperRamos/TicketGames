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

        public bool CreateOrUpdateImage(Product product)
        {
            try
            {
                if (product.Images.Count > 0)
                {
                    var prod = this._catalogRepository.GetProductById(product.Id);

                    if (prod != null)
                    {

                        foreach (var image in product.Images)
                        {
                            var result = this._catalogRepository.UpdateImage(image.ImageTypeId, product.Id, image.Url);

                            if (result.Id > 0)
                                continue;
                        }

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
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

        public List<Product> GetProducts(int categoryId, string name)
        {
            return this._catalogRepository.GetProducts(categoryId, name);
        }

        public List<Product> GetRecentProducts(long categoryId)
        {
            return this._catalogRepository.GetRecentProductsByCategory(categoryId);
        }
    }
}

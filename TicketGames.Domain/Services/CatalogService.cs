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
    }
}

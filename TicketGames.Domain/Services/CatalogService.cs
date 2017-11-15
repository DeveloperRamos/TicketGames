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
        private readonly ICatalogRepository _catalogsRepository;

        public CatalogService(ICatalogRepository catalogsRepository)
        {
            this._catalogsRepository = catalogsRepository;
        }

        public List<Category> GetCategories()
        {
            return this._catalogsRepository.GetCategories();
        }
    }
}

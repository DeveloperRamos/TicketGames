using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TicketGames.API.Models.Catalog;
using TicketGames.CrossCutting.Cache;
using TicketGames.CrossCutting.Cache.Redis;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Services;
using TicketGames.Infrastructure.Repositories;

namespace TicketGames.API.Controllers
{
    [RoutePrefix("v1/categories")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class CategoryController : ApiController
    {
        private readonly ICatalogService _catalogService;
        public CategoryController(ICatalogService catalogService)
        {
            this._catalogService = catalogService;
        }
        public CategoryController()
            : this(new CatalogService(new CatalogRepository()))
        {
            CacheManager.SetProvider(new CacheProvider());
        }

        [HttpGet, Route()]
        public IHttpActionResult Get()
        {
            Category category = new Category();


            var key = "categories";
            IList<Category> categories_ = null;
            categories_ = CacheManager.GetObject<List<Category>>(key);

            if (categories_ == null)
            {
                var result = this._catalogService.GetCategories();

                categories_ = category.MappingCategories(result);

                if (categories_ != null && categories_.Count > 0)
                    CacheManager.StoreObject(key, categories_, LifetimeProfile.Longest);
            }

            return Ok(categories_);
        }
    }
}

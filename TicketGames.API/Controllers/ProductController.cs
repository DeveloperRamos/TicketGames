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
    [RoutePrefix("v1/product")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ProductController : ApiController
    {
        private readonly ICatalogService _catalogService;
        public ProductController(ICatalogService catalogService)
        {
            this._catalogService = catalogService;
        }
        public ProductController()
            : this(new CatalogService(new CatalogRepository()))
        {
            CacheManager.SetProvider(new CacheProvider());
        }

        [HttpGet, Route("{id}")]
        public IHttpActionResult Get(long id)
        {

            if (id <= 0)
            {
                return BadRequest("Id de produto invalido!");
            }

            Product product = null;

            var key = string.Concat("Catalog:Products:", id.ToString());

            product = CacheManager.GetObject<Product>(key);

            if (product == null)
            {
                var result = this._catalogService.GetProduct(id);

                product = new Product(result);

                if (product != null)
                    CacheManager.StoreObject(key, product, LifetimeProfile.Moderate);
            }

            return Ok(product);
        }

        [HttpPost, Route("search")]
        public IHttpActionResult Searchs(Search search)
        {
            IList<Product> products = null;

            if (search.CategoryId > 0)
            {
                var key = string.Concat("Catalog:Products:Category:", search.CategoryId.ToString());

                products = CacheManager.GetObject<List<Product>>(key);

                if (products == null)
                {
                    var result = this._catalogService.GetProducts(search.CategoryId);

                    products = new Product().MappingProducts(result);

                    if (products != null && products.Count > 0)
                        CacheManager.StoreObject(key, products, LifetimeProfile.Longest);
                }
            }
            else
            {
                if (search.CategoryId > 0 && search.DepartmentId == 0 && string.IsNullOrEmpty(search.Word))
                {
                    var result = this._catalogService.GetProducts(search.CategoryId);

                    products = new Product().MappingProducts(result);
                }
            }

            return Ok(products);
        }
    }
}

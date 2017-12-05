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
    [RoutePrefix("v1/showcase")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ShowcaseController : ApiController
    {
        private readonly IShowcaseService _showcaseService;
        public ShowcaseController(IShowcaseService showcaseService)
        {
            this._showcaseService = showcaseService;
        }
        public ShowcaseController()
            : this(new ShowcaseService(new ShowcaseRepository()))
        {
            CacheManager.SetProvider(new CacheProvider());
        }

        [Authorize]
        [HttpGet, Route("{type}")]
        public IHttpActionResult Get(ShowcaseType type)
        {
            IList<Product> products = null;

            var key = string.Concat("Catalog:Showcase:", type.ToString());

            products = CacheManager.GetObject<List<Product>>(key);

            if (products == null)
            {
                var result = this._showcaseService.GetProducts((int)type);

                if (result.Count > 0)
                {
                    products = new Product().MappingProducts(result);
                    CacheManager.StoreObject(key, products, LifetimeProfile.Longest);
                }
                else
                {
                    return BadRequest("Vitrine não encontrada!");
                }
            }

            return Ok(products);


        }
    }
}

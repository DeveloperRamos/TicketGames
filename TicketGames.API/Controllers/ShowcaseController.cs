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

        //[Authorize]
        [HttpGet, Route("{type}")]
        public IHttpActionResult Get(ShowcaseType type)
        {
            Showcase showcase = null;

            var key = string.Concat("Catalog:Showcase:", type.ToString());

            showcase = CacheManager.GetObject<Showcase>(key);

            if (showcase == null)
            {
                var result = this._showcaseService.GetShowcase((int)type);

                showcase = new Showcase(result);

                if (showcase != null)
                    CacheManager.StoreObject(key, showcase, LifetimeProfile.Longest);
            }

            return Ok(showcase);
        }
    }
}

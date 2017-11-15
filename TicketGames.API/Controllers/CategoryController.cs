using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TicketGames.API.Models.Catalog;
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

        }

        [HttpGet, Route()]
        public IHttpActionResult Get()
        {
            Category category = new Category();

            var result = this._catalogService.GetCategories();

            List<Category> categories = category.GetCategories();

            return Ok(categories);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TicketGames.API.Models.Catalog;

namespace TicketGames.API.Controllers
{
    [RoutePrefix("v1/categories")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class CategoryController : ApiController
    {
        [HttpGet, Route()]
        public IHttpActionResult Get()
        {
            Category category = new Category();

            List<Category> categories = category.GetCategories();

            return Ok(categories);
        }
    }
}

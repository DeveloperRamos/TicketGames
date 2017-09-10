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
    [RoutePrefix("v1/showcase")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ShowcaseController : ApiController
    {
        //[Authorize]
        [HttpGet, Route()]
        public IHttpActionResult Get()
        {
            List<Showcase> showcases = new List<Showcase>();

            #region Buscando vitrine banner

            Showcase showcase = new Showcase();
            showcase.Id = 1;
            showcase.Name = "Vitrine do banner";
            showcase.ShowcaseType = ShowcaseType.Banner;
            showcase.GetShowcase();

            showcases.Add(showcase);

            #endregion

            #region Buscando vitrine de categorias

            //Showcase showcaseCategory = new Models.Showcase();
            //showcaseCategory.Id = 2;
            //showcaseCategory.Name = "Vitrine das categorias";
            //showcaseCategory.ShowcaseType = ShowcaseType.Category;
            //showcaseCategory.GetShowcase();

            //showcases.Add(showcaseCategory);

            #endregion

            #region Buscando vitrine dos consoles

            //Showcase showcaseConsole = new Models.Showcase();
            //showcaseConsole.Id = 3;
            //showcaseConsole.Name = "Vitrine dos consoles";
            //showcaseConsole.ShowcaseType = ShowcaseType.Console;
            //showcaseConsole.GetShowcase();

            //home.Showcases.Add(showcaseConsole);

            #endregion

            #region Buscando vitrine de produtos

            //Showcase showcaseProducts = new Models.Showcase();
            //showcaseProducts.Id = 4;
            //showcaseProducts.Name = "Vitrine de produtos";
            //showcaseProducts.ShowcaseType = ShowcaseType.Product;
            //showcaseProducts.GetShowcase();

            //home.Showcases.Add(showcaseProducts);

            #endregion

            return Ok(showcases);
        }
    }
}

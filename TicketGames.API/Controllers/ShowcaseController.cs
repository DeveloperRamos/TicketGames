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
        [HttpGet, Route("{type}")]
        public IHttpActionResult Get(ShowcaseType type)
        {           
            Showcase showcase = new Showcase();

            switch (type)
            {
                case ShowcaseType.Banner:
                    {

                        #region Buscando vitrine banner


                        showcase.Id = 1;
                        showcase.Name = "Vitrine do banner";
                        showcase.ShowcaseType = ShowcaseType.Banner;
                        showcase.GetShowcase();
                        
                        #endregion

                        break;
                    }
                case ShowcaseType.Recent:
                    {
                        #region Buscando vitrine de categorias

                        showcase.Id = 2;
                        showcase.Name = "Vitrine dos produtos recentes";
                        showcase.ShowcaseType = ShowcaseType.Recent;
                        showcase.GetShowcase();                        

                        #endregion

                        break;
                    }
                case ShowcaseType.Popular:
                    {
                        #region Buscando vitrine de produtos
                        
                        showcase.Id = 3;
                        showcase.Name = "Vitrine dos mais vendidos";
                        showcase.ShowcaseType = ShowcaseType.Popular;
                        showcase.GetShowcase();                        

                        #endregion
                        break;
                    }
                case ShowcaseType.Console:
                    {
                        #region Buscando vitrine dos consoles
                        
                        showcase.Id = 4;
                        showcase.Name = "Vitrine dos consoles";
                        showcase.ShowcaseType = ShowcaseType.Console;
                        showcase.GetShowcase();
                       
                        #endregion
                        break;
                    }
            }

            return Ok(showcase);
        }
    }
}

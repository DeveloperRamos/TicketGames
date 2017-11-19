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

        }

        [HttpGet, Route("{id}")]
        public IHttpActionResult Get(long id)
        {

            if (id <= 0)
            {
                return BadRequest("Id de produto invalido!");
            }


            Product product = null;

            var result = this._catalogService.GetProduct(id);

            product = new Product(result);            

            return Ok(product);
        }

        [HttpPost, Route("search")]
        public IHttpActionResult Searchs([FromBody] Search search)
        {
            List<Product> products = new List<Product>();


            List<Image> images = new List<Image>();

            images.Add(new Image() { Id = 1, ImageType = ImageType.Cover, URL = "1.png" });
            images.Add(new Image() { Id = 2, ImageType = ImageType.Detail, URL = "1-D-1.png" });
            images.Add(new Image() { Id = 3, ImageType = ImageType.Detail, URL = "1-D-2.png" });
            images.Add(new Image() { Id = 4, ImageType = ImageType.Detail, URL = "1-D-3.png" });
            images.Add(new Image() { Id = 5, ImageType = ImageType.Detail, URL = "1-D-4.png" });
            images.Add(new Image() { Id = 6, ImageType = ImageType.Detail, URL = "1-D-5.png" });
            images.Add(new Image() { Id = 7, ImageType = ImageType.Banner, URL = "1-B.png" });

            Product productone = new Product()
            {
                Id = 1,
                Name = "Watch Dogs 2",
                Category = new Category() { Id = 1, Name = "Playstation 4" },
                Department = new Department() { Id = 1, Name = "Jogos" },
                ShortDescription = "Watch Dogs 2 é um jogo eletrônico desenvolvido pela Ubisoft Montreal que sucede o popular Watch Dogs, de 2014.",
                Value = 10.00f,
                Images = images,
                SalesMade = 98,
                MissingtoSell = 2
            };

            products.Add(productone);
            products.Add(productone);
            products.Add(productone);
            products.Add(productone);
            products.Add(productone);
            products.Add(productone);
            products.Add(productone);
            products.Add(productone);

            return Ok(products);
        }
    }
}

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
    [RoutePrefix("v1/product")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ProductController : ApiController
    {

        [HttpGet, Route("{id}")]
        public IHttpActionResult Get(long id)
        {
            
            if (id <= 0)
            {
                return BadRequest("Id de produto invalido!");
            }

            List<Image> images = new List<Image>();

            string img = string.Concat(1200.ToString(), "-D-");

            images.Add(new Image() { Id = 1, ImageType = ImageType.Cover, URL = string.Concat(1200.ToString(),".png") });
            images.Add(new Image() { Id = 2, ImageType = ImageType.Detail, URL = string.Concat(img, "1.jpg") });
            images.Add(new Image() { Id = 3, ImageType = ImageType.Detail, URL = string.Concat(img, "2.jpg") });
            images.Add(new Image() { Id = 4, ImageType = ImageType.Detail, URL = string.Concat(img, "3.jpg") });            
            images.Add(new Image() { Id = 7, ImageType = ImageType.Banner, URL = string.Concat(1200.ToString(), "-B.png") });

            Product product = new Product()
            {
                Id = 1200,
                Name = "The Evil Within 2",
                Category = new Category() { Id = 1, Name = "Playstation 4" },
                Department = new Department() { Id = 1, Name = "Jogos" },
                ShortDescription = "The Evil Within 2 leva a aclamada franquia a um novo nível com sua mistura única de tensão psicológica e horror de sobrevivência.",
                Description = "Do mestre Shinji Mikami e da talentosa equipe da Tango Gameworks, The Evil Within 2 leva a aclamada franquia a um novo nível com sua mistura única de tensão psicológica e horror de sobrevivência. <br/>" + 
                "Você é o detetive Sebastian Castellanos, e está no seu pior momento. Mas quando tem a chance de salvar a sua filha, você deve entrar em um mundo cheio de pesadelos e descobrir as origens sombrias de uma cidade outrora idílica para resgatá-la.Ameaças aterrorizantes emergem de cada canto conforme o mundo se retorce e se deforma ao seu redor. Você irá enfrentar a adversidade face a face com armas e armadilhas, ou passar sorrateiramente pelas sombras para sobreviver?<br/><br/>"
                +"<b> História </b>: O detetive Sebastian Castellanos perdeu tudo, inclusive sua filha, Lily. Para salvá-la, ele é forçado a se aliar à Mobius, a sombria organização responsável pela destruição da antiga vida de Sebastian. Ele deve descer a uma das suas terríveis criações, o perturbador mundo de Union.Ameaças assustadoras surgem de cada canto, e ele deve confiar na sua perspicácia para sobreviver."
                +"Para sua última chance de redenção, a única forma de sair é entrar.<br/><br/>"
                +"<b> História de Redenção</b>: Sebastian deve mergulhar no pesadelo para ter sua vida e sua família de volta.  <br/><br/>"
                +"<b> Descubra Lugares Assustadores</b>: Explore o mais longe que ousar em um mundo no qual nada é exatamente o que parece, mas planeje-se com sabedoria. <br/><br/>"
                +"<b> Decida Como Sobreviver</b>: Ataque das sombras com a besta, fuja como se não houvesse amanhã ou avance de arma em punho usando a pouca munição que tem. <br/><br/>"
                +"<b> Enfrente Inimigos Perturbadores</b>: Sobreviva a confrontos com inimigos sádicos e conheça personagens que poderão guiar(ou enganar) você no seu caminho rumo à redenção.<br/><br/>"
                +"<b> Terror e Suspense Viscerais </b>: Entre em um mundo apavorante e cheio de tensão, no qual aberrações perturbadoras espreitam por toda parte."
                +"<br/><br/><b> Imagens meramente ilustrativas.</b><br/><b> Todas as informações divulgadas são de responsabilidade do Fabricante / Fornecedor.</b><br/><br/>",
                Value = 10.00m,
                Images = images,
                SalesMade = 100,
                MissingtoSell = 0,
                RaffleDate = DateTime.Now.AddDays(10)
            };

            return Ok(product);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Model;
using TicketGames.Domain.Services;
using TicketGames.Infrastructure.Repositories;

namespace TicketGames.Images
{
    public class Program
    {
        static void Main(string[] args)
        {
            Task T = new Task(ApiCall);
            T.Start();
            Console.WriteLine("Json data........");
            Console.ReadLine();
        }
        static async void ApiCall()
        {
            ICatalogService _catalogService = new CatalogService(new CatalogRepository());

            var apiImageShack = "https://api.imageshack.com/";
            var route = "v2/user/marcio.correia/images?offset=0&limit=1000";

            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(string.Concat(apiImageShack, route));

                response.EnsureSuccessStatusCode();

                RootObject images = null;

                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseBody.Substring(0, 50) + "........");

                    images = JsonConvert.DeserializeObject<RootObject>(responseBody);
                }

                List<Product> products = new List<Product>();
                Product product = null;

                foreach (var image in images.result.images)
                {
                    var list = image.original_filename.Split('.');
                    long id;
                    long.TryParse(list[0], out id);

                    if (id > 0)
                    {
                        product = new Product();
                        product.Id = id;

                        product.Images.Add(new Image() { ImageTypeId = 2, Url = "https://" + image.direct_link });

                        var b = images.result.images.Where(i => i.original_filename.Contains(id.ToString() + "-B")).Select(x => x.direct_link).FirstOrDefault();
                        var gp1 = images.result.images.Where(i => i.original_filename.Contains(id.ToString() + "-GP-1")).Select(x => x.direct_link).FirstOrDefault();
                        var gp2 = images.result.images.Where(i => i.original_filename.Contains(id.ToString() + "-GP-2")).Select(x => x.direct_link).FirstOrDefault();
                        var gp3 = images.result.images.Where(i => i.original_filename.Contains(id.ToString() + "-GP-3")).Select(x => x.direct_link).FirstOrDefault();

                        if (!string.IsNullOrEmpty(b)) product.Images.Add(new Image() { ImageTypeId = 1, Url = "https://" + b });
                        //if (!string.IsNullOrEmpty(gp1)) product.Images.Add(new Image() { ImageTypeId = 3, Url = "https://" + gp1 });
                        //if (!string.IsNullOrEmpty(gp2)) product.Images.Add(new Image() { ImageTypeId = 3, Url = "https://" + gp2 });
                        //if (!string.IsNullOrEmpty(gp3)) product.Images.Add(new Image() { ImageTypeId = 3, Url = "https://" + gp3 });


                        if(_catalogService.CreateOrUpdateImage(product))
                        {
                            Console.WriteLine("Imagens do productId:{0}", product.Id.ToString(), " Atualizado!");
                        }

                        //products.Add(product);
                    }
                }






                Console.WriteLine(products.Count.ToString());

                Console.ReadKey();
            }
        }
    }
}

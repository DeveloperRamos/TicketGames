using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Catalog
{
    public class Showcase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ShowcaseType ShowcaseType { get; set; }        
        public List<Product> Products { get; set; }

        public Showcase()
        {
            this.Products = new List<Product>();
        }

        public Showcase(Domain.Model.Showcase showcase)
        {
            this.Id = showcase.Id;
            this.Name = showcase.Name;
            this.ShowcaseType = (ShowcaseType)showcase.ShowcaseTypeId;
            this.Products = new List<Product>();

            foreach (Domain.Model.ShowcaseProduct showcaseProduct in showcase.ShowcaseProducts)
            {
                showcaseProduct.Product.Order = showcaseProduct.Order;
                Product product = new Product(showcaseProduct.Product);

                this.Products.Add(product);
            }
        }        
    }
}
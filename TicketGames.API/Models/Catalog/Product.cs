using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Catalog
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Department Department { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public List<Image> Images { get; set; }
        public int? Order { get; set; }
        public Raffle Raffle { get; set; }

        public Product()
        {
            this.Id = 0;
            this.Name = this.ShortDescription = string.Empty;
            this.Order = 0;
            this.Category = new Category();
            this.Department = new Department();
            this.Images = new List<Image>();
        }

        public Product(Domain.Model.Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.ShortDescription = product.DescriptionShort;
            this.Description = product.Description;
            this.Value = product.Value;
            this.Order = product.Order;
            this.Images = new Image().MappingImages(product.Images.ToList());
            this.Category = new Category(product.Category);
            this.Department = new Department();
            this.Raffle = new Raffle(product.Raffles.ToList());
        }

        public List<Product> MappingProducts(List<Domain.Model.Product> products)
        {
            List<Product> _products = new List<Product>();

            foreach (Domain.Model.Product product in products)
            {
                var _product = new Product(product);

                _products.Add(_product);
            }

            return _products;
        }
    }
}
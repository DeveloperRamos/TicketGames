using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.Infrastructure.Context;

namespace TicketGames.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly TicketGamesContext _context;
        private string connection = ConfigurationManager.ConnectionStrings["TicketGamesContext"].ConnectionString;
        public CatalogRepository()
        {
            this._context = new TicketGamesContext();
        }

        public Image CreateImage(Product product)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategories()
        {
            using (var connect = new MySqlConnection(connection))
            {
                string queryCategory = @"Select * From Tb_Category C " +
                                "Inner Join Tb_Department D On(D.CategoryId = C.Id) " +
                                "Where C.Active = 1 And D.Active = 1 Order By C.Order Asc;";

                var categoryDictionary = new Dictionary<long, Category>();

                var results = connect.Query<Category, Department, Category>(queryCategory,
                  (category, department) =>
                  {
                      Category categoryEntity;

                      if (!categoryDictionary.TryGetValue(category.Id, out categoryEntity))
                      {
                          categoryEntity = category;
                          categoryEntity.Departaments = new List<Department>();
                          categoryDictionary.Add(category.Id, categoryEntity);
                      }

                      categoryEntity.Departaments.Add(department);

                      return categoryEntity;

                  }).Distinct().ToList();

                return results;
            }
        }

        public Product GetProductById(long id)
        {
            using (var connect = new MySqlConnection(connection))
            {
                string query = @"Select * From Tb_Product P " +
                                "Inner Join Tb_Category C On(P.CategoryId = C.Id) " +
                                "Inner Join Tb_Department D On(P.DepartmentId = D.Id) " +
                                "Inner Join Tb_Image I On(P.Id = I.ProductId) " +
                                "Inner Join Tb_Raffle R On(P.Id = R.ProductId) " +
                                "Where P.Active = 1 And C.Active = 1 And D.Active = 1 And I.Active = 1 And P.Id = @id";

                //connect.Open();

                var productDictionary = new Dictionary<long, Product>();

                var result = connect.Query<Product, Category, Department, Image, Raffle, Product>(query,
                  (product, category, department, images, raffles) =>
                {
                    Product productEntity;

                    if (!productDictionary.TryGetValue(product.Id, out productEntity))
                    {
                        productEntity = product;
                        productEntity.Images = new List<Image>();
                        productEntity.Raffles = new List<Raffle>();
                        productDictionary.Add(product.Id, productEntity);
                        productEntity.Category = category;
                        productEntity.Department = department;
                    }

                    productEntity.Images.Add(images);
                    productEntity.Raffles.Add(raffles);

                    return productEntity;

                }, new { Id = id }).Distinct().FirstOrDefault();

                //connect.Close();

                return result;
            }
        }

        public List<Product> GetProducts(int categoryId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                string query = @"Select * From Tb_Product P " +
                                "Inner Join Tb_Category C On(P.CategoryId = C.Id) " +
                                "Inner Join Tb_Department D On(P.DepartmentId = D.Id) " +
                                "Inner Join Tb_Image I On(P.Id = I.ProductId) " +
                                "Inner Join Tb_Raffle R On(P.Id = R.ProductId) " +
                                "Where P.Active = 1 And C.Active = 1 And D.Active = 1 And I.Active = 1 And P.CategoryId = @categoryId";

                //connect.Open();

                var productDictionary = new Dictionary<long, Product>();

                var results = connect.Query<Product, Category, Department, Image, Raffle, Product>(query,
                  (product, category, department, images, raffles) =>
                  {
                      Product productEntity;

                      if (!productDictionary.TryGetValue(product.Id, out productEntity))
                      {
                          productEntity = product;
                          productEntity.Images = new List<Image>();
                          productEntity.Raffles = new List<Raffle>();
                          productDictionary.Add(product.Id, productEntity);
                          productEntity.Category = category;
                          productEntity.Department = department;
                      }

                      productEntity.Images.Add(images);
                      productEntity.Raffles.Add(raffles);

                      return productEntity;

                  }, new { CategoryId = categoryId }).Distinct().ToList();

                //connect.Close();

                return results;
            }
        }

        public List<Product> GetProducts(string name)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts(int categoryId, string name)
        {
            var productName = name.Replace(" ", "%");

            using (var connect = new MySqlConnection(connection))
            {
                string query = @"Select * From Tb_Product P " +
                                "Inner Join Tb_Category C On(P.CategoryId = C.Id) " +
                                "Inner Join Tb_Department D On(P.DepartmentId = D.Id) " +
                                "Inner Join Tb_Image I On(P.Id = I.ProductId) " +
                                "Inner Join Tb_Raffle R On(P.Id = R.ProductId) " +
                                "Where P.Active = 1 And C.Active = 1 And D.Active = 1 And I.Active = 1 And P.CategoryId = @categoryId And P.Name Like CONCAT('%@name%');";

                //connect.Open();

                var productDictionary = new Dictionary<long, Product>();

                var results = connect.Query<Product, Category, Department, Image, Raffle, Product>(query,
                  (product, category, department, images, raffles) =>
                  {
                      Product productEntity;

                      if (!productDictionary.TryGetValue(product.Id, out productEntity))
                      {
                          productEntity = product;
                          productEntity.Images = new List<Image>();
                          productEntity.Raffles = new List<Raffle>();
                          productDictionary.Add(product.Id, productEntity);
                          productEntity.Category = category;
                          productEntity.Department = department;
                      }

                      productEntity.Images.Add(images);
                      productEntity.Raffles.Add(raffles);

                      return productEntity;

                  }, new { CategoryId = categoryId, name = productName }).Distinct().ToList();

                //connect.Close();

                return results;
            }
        }

        public List<Product> GetRecentProductsByCategory(long categoryId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                string query = @"Select * From Tb_Product P " +
                                "Inner Join Tb_Category C On(P.CategoryId = C.Id) " +
                                "Inner Join Tb_Department D On(P.DepartmentId = D.Id) " +
                                "Inner Join Tb_Image I On(P.Id = I.ProductId) " +
                                "Inner Join Tb_Raffle R On(P.Id = R.ProductId) " +
                                "Where P.Active = 1 And C.Active = 1 And D.Active = 1 And I.Active = 1 And P.CategoryId = @categoryId";

                //connect.Open();

                var productDictionary = new Dictionary<long, Product>();

                var results = connect.Query<Product, Category, Department, Image, Raffle, Product>(query,
                  (product, category, department, images, raffles) =>
                  {
                      Product productEntity;

                      if (!productDictionary.TryGetValue(product.Id, out productEntity))
                      {
                          productEntity = product;
                          productEntity.Images = new List<Image>();
                          productEntity.Raffles = new List<Raffle>();
                          productDictionary.Add(product.Id, productEntity);
                          productEntity.Category = category;
                          productEntity.Department = department;
                      }

                      productEntity.Images.Add(images);
                      productEntity.Raffles.Add(raffles);

                      return productEntity;

                  }, new { CategoryId = categoryId })
                  .Distinct().OrderByDescending(p => p.Id).Take(4).ToList();

                //connect.Close();

                return results;
            }
        }

        public Image UpdateImage(int typeImageId, long productId, string newUrl)
        {
            using (var connect = new MySqlConnection(connection))
            {
                //connect.Open();

                string queryImage = @"Select * From Tb_Image Where ImageTypeId = @imageTypeId And ProductId = @productId And Active = 1;";

                var imageModified = connect.Query<Image>(queryImage, new { imageTypeId = typeImageId, productId = productId }).FirstOrDefault();

                //connect.Close();

                if (imageModified.Id > 0)
                {
                    imageModified.Url = newUrl;

                    this._context.Entry(imageModified).State = EntityState.Modified;

                    this._context.SaveChanges();
                }

                return imageModified != null ? imageModified : new Image();
            }
        }
    }
}

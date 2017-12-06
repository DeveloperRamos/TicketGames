using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.Infrastructure.Context;
using System.Data.Entity;
using MySql.Data.MySqlClient;
using System.Configuration;
using Dapper;

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

        public List<Category> GetCategories()
        {

            List<Category> categories = this._context.Categories.Include(c => c.Departaments).ToList();

            return categories;
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

                connect.Open();

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

                connect.Close();

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

                connect.Open();

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

                connect.Close();

                return results;
            }
        }

        public List<Product> GetProducts(string name)
        {
            throw new NotImplementedException();
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

                connect.Open();

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

                connect.Close();

                return results;
            }
        }
    }
}

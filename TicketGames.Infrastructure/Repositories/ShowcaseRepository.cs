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
using Dapper;
using System.Configuration;

namespace TicketGames.Infrastructure.Repositories
{
    public class ShowcaseRepository : IShowcaseRepository
    {
        private readonly TicketGamesContext _context;
        private string connection = ConfigurationManager.ConnectionStrings["TicketGamesContext"].ConnectionString;
        public ShowcaseRepository()
        {
            this._context = new TicketGamesContext();
        }

        public List<Product> GetProductsByShowcaseTypeId(int showcaseTypeId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                string query = @"Select * From Tb_Showcase S " +
                                "Inner Join Tb_ShowcaseProduct SP On(S.Id = SP.ShowcaseId) " +
                                "Inner Join Tb_Product P On(SP.ProductId = P.Id) " +
                                "Inner Join Tb_Category C On(P.CategoryId = C.Id) " +
                                "Inner Join Tb_Department D On(P.DepartmentId = D.Id) " +
                                "Inner Join Tb_Image I On(P.Id = I.ProductId) " +
                                "Inner Join Tb_Raffle R On(P.Id = R.ProductId) " +
                                "Where S.Active = 1 And SP.Active = 1 And P.Active = 1 And C.Active = 1 And D.Active = 1 And I.Active = 1 And S.ShowcaseTypeId = @showcaseTypeId;";

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

                  }, new { ShowcaseTypeId = showcaseTypeId }).Distinct().ToList();

                //connect.Close();

                return results;
            }
        }

        public Showcase GetShowcaseByTypeId(int showcaseTypeId)
        {
            List<ShowcaseProduct> showcaseProducts = this._context.ShowcaseProducts
                .Include(s => s.Showcase)
                .Include(s => s.Product.Images)
                .Include(s => s.Product.Category)
                .Include(s => s.Product.Raffles)
                .Where(s => s.Showcase.ShowcaseTypeId == showcaseTypeId && s.Showcase.Active == true && s.Active == true && s.Product.Active == true)
                .ToList();

            return showcaseProducts.Select(s => s.Showcase).FirstOrDefault();
        }
    }
}

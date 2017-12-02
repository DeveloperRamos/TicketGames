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
                connect.Open();

                var product = connect.Query<Product>("Select * From Tb_Product Where Id = @id;", new { Id = id }).Single();

                if (product.Id > 0)
                {
                    product.Category = connect.Query<Category>("Select * From Tb_Category Where Id = @categoryId;", new { categoryId = product.CategoryId }).Single();

                    product.Department = connect.Query<Department>("Select * From Tb_Department Where Id = @departmentId;", new { departmentId = product.DepartmentId }).Single();

                    product.Images = connect.Query<Image>("Select * From Tb_Image Where ProductId = @productId;", new { productId = product.Id }).ToList();

                    product.Raffles = connect.Query<Raffle>("Select * From Tb_Raffle Where RaffleStatusId In(4,3) And ProductId = @productId;", new { productId = product.Id }).ToList();
                }

                connect.Close();

                return product;
            }
        }

        public List<Product> GetProducts(int categoryId)
        {
            List<Product> products = this._context.Products
                                .Include(p => p.Images)
                                .Include(p => p.Category)
                                .Include(p => p.Raffles)
                                .Where(p => p.CategoryId == categoryId).ToList();

            return products;
        }

        public List<Product> GetProducts(string name)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetRecentProductsByCategory(long categoryId)
        {
            List<Product> products = this._context.Products
                                   .Include(p => p.Images)
                                   .Include(p => p.Category)
                                   .Include(p => p.Raffles)
                                   .Where(p => p.CategoryId == categoryId)
                                   .Take(4).OrderByDescending(p => p.Id).ToList();

            return products;

        }


        //        List<Catalog> catalogs = new List<Catalog>();
        //        List<Category> categorys = new List<Category>();
        //        List<Product> products = new List<Product>();
        //        Discount discount = new Discount();

        //        var campaignCompany = this._context.CampaignCompanys
        //                                    .Include(c => c.Campaign)
        //                                        .Where(x => x.Id == contractId).FirstOrDefault();

        //        var catalogsCampaign = this._context.CatalogCampaigns
        //                                                .Include(c => c.Catalog)
        //                                                    .Where(c => c.CampaignId == campaignCompany.CampaignId).ToList();

        //            if (catalogsCampaign.Count <= 0)
        //            {
        //                var catalogsPartner = this._context.CatalogPartners
        //                                                        .Include(c => c.Catalog)
        //                                                            .Where(c => c.PartnerId == campaignCompany.Campaign.PartnerId).ToList();

        //                foreach (var catalogPartner in catalogsPartner)
        //                {
        //                    catalogPartner.Catalog.DiscountId = catalogPartner.DiscountId;
        //                    catalogs.Add(catalogPartner.Catalog);
        //                }
        //}
    }
}

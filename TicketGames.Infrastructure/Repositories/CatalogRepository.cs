using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.Infrastructure.Context;
using System.Data.Entity;

namespace TicketGames.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly TicketGamesContext _context;
        public CatalogRepository()
        {
            this._context = new TicketGamesContext();
        }

        public List<Category> GetCategories()
        {

            List<Category> categories = this._context.Categories.Include(c => c.Departaments).ToList();

            return categories;
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

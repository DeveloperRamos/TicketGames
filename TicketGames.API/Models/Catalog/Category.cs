using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketGames.Domain.Model;

namespace TicketGames.API.Models.Catalog
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<Department> Departments { get; set; }

        public Category()
        {
            this.Departments = new List<Department>();
        }
        public List<Category> MappingCategories(List<Domain.Model.Category> categories)
        {
            List<Category> lstCategories = new List<Category>();

            foreach (Domain.Model.Category category in categories)
            {
                lstCategories.Add(new Category()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Order = category.Ordem,
                    Departments = new List<Department>()
                });
            }

            return lstCategories;
        }
    }
}
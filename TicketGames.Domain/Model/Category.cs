using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class Category
    {

        public Category()
        {
            //this.Departaments = new List<Department>();
            //this.Products = new List<Product>();
            //this.CatalogCategories = new List<CatalogCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public bool Active { get; set; }
        //public IList<Department> Departaments { get; set; }
        //public ICollection<Product> Products { get; set; }
        //public ICollection<CatalogCategory> CatalogCategories { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class Product
    {
        public Product()
        {
            this.Images = new List<Image>();
            this.ShowcaseProducts = new List<ShowcaseProduct>();
        }

        public long Id { get; set; }
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionShort { get; set; }
        public float Value { get; set; }
        public Nullable<int> Order { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public bool Active { get; set; }

        public virtual Category Category { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ShowcaseProduct> ShowcaseProducts { get; set; }

    }
}

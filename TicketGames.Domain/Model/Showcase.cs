using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class Showcase
    {
        public Showcase()
        {
            this.ShowcaseProducts = new List<ShowcaseProduct>();

        }
        public int Id { get; set; }
        public int ShowcaseTypeId { get; set; }
        public string Name { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public bool Active { get; set; }
        public virtual ShowcaseType ShowcaseType { get; set; }
        public virtual ICollection<ShowcaseProduct> ShowcaseProducts { get; set; }
    }
}

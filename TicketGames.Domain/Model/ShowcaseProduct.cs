using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class ShowcaseProduct
    {
        public long Id { get; set; }
        public int ShowcaseId { get; set; }
        public long ProductId { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public virtual Showcase Showcase { get; set; }
        public virtual Product Product { get; set; }
    }
}

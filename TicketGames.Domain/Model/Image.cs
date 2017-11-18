using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class Image
    {
        public long Id { get; set; }
        public int ImageTypeId { get; set; }
        public long ProductId { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public Nullable<int> Order { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public virtual ImageType ImageType { get; set; }
        public virtual Product Product { get; set; }
    }
}

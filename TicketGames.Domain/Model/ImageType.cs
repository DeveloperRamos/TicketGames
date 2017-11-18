using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class ImageType
    {
        public ImageType()
        {
            this.Images = new List<Image>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}

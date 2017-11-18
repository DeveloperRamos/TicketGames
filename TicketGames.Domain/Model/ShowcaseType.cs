using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class ShowcaseType
    {
        public ShowcaseType()
        {
            this.Showcases = new List<Showcase>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Showcase> Showcases { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Repositories
{
    public interface IShowcaseRepository
    {
        Showcase GetShowcaseByTypeId(int showcaseTypeId);
        List<Product> GetProductsByShowcaseTypeId(int showcaseTypeId);
    }
}

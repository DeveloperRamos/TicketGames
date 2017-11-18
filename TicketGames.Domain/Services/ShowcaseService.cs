using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;

namespace TicketGames.Domain.Services
{
    public class ShowcaseService : IShowcaseService
    {
        private readonly IShowcaseRepository _showcaseRepository;

        public ShowcaseService(IShowcaseRepository showcaseRepository)
        {
            this._showcaseRepository = showcaseRepository;
        }
        public Showcase GetShowcase(int typeId)
        {
            Showcase showcase = this._showcaseRepository.GetShowcaseByTypeId(typeId);

            return showcase;
        }
    }
}

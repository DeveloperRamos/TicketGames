using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.Infrastructure.Context;

namespace TicketGames.Infrastructure.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly TicketGamesContext _context;
        public ParticipantRepository()
        {
            this._context = new TicketGamesContext();
        }
        public Participant Create(Participant participant)
        {
            throw new NotImplementedException();
        }

        public Participant GetParticipantById(long id)
        {
            var participant = this._context.Participants.Find(id);

            return participant;
        }

        public Participant GetParticipantByLoginAndCPF(string login, string cpf)
        {
            Participant participant = this._context.Participants.Where(p => p.Login == login && p.CPF == cpf).FirstOrDefault();

            return participant;            
        }

        public Participant Update(Participant participant)
        {
            throw new NotImplementedException();
        }
    }
}

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
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;

        public ParticipantService(IParticipantRepository participantRepository)
        {
            this._participantRepository = participantRepository;
        }
        public bool CreateOrUpdate(Participant participant)
        {
            try
            {
                Participant part = new Participant();

                if (participant.Id > 0)
                {
                    part = this._participantRepository.Update(participant);
                }
                else
                {
                    part = this._participantRepository.Create(participant);

                    return part.Id > 0;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Participant GetParticipant(string login, string cpf)
        {
            throw new NotImplementedException();
        }

        public Participant GetParticipant(long id)
        {
            throw new NotImplementedException();
        }
    }
}

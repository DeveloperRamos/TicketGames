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

        public Participant Authenticate(string login, string password, string salt)
        {
            return this._participantRepository.Authenticate(login, password, salt);
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
            return this._participantRepository.GetParticipantByLoginAndCPF(login, cpf);
        }

        public Participant GetParticipant(long id)
        {
            throw new NotImplementedException();
        }

        public Participant GetParticipant(string session)
        {
            var _session = this._participantRepository.GetSessionBySession(session);

            return this._participantRepository.GetParticipantById(_session.ParticipantId);
        }

        public Session GetSession(string session)
        {
            return this._participantRepository.GetSessionBySession(session);
        }

        public bool ValidateSession(string session, long participantId)
        {
            var result = this._participantRepository.GetSessionBySession(session);

            if (result != null)
                return result.ParticipantId == participantId;

            return false;

        }
    }
}

﻿using TicketGames.Domain.Model;

namespace TicketGames.Domain.Contract
{
    public interface IParticipantService
    {
        bool CreateOrUpdate(Participant participant);
        Participant GetParticipant(string login, string cpf);
        Participant GetParticipant(long id);
        bool ValidateSession(string session, long participantId);
        Participant GetParticipant(string session);
        Session GetSession(string session);
    }
}

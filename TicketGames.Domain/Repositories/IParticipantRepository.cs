﻿using TicketGames.Domain.Model;

namespace TicketGames.Domain.Repositories
{
    public interface IParticipantRepository
    {
        Participant Create(Participant participant);
        Participant Update(Participant participant);
        Participant GetParticipantByLoginAndCPF(string login, string cpf);
        Participant GetParticipantById(long id);
    }
}
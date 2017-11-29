using TicketGames.Domain.Model;

namespace TicketGames.Domain.Contract
{
    public interface IParticipantService
    {
        bool CreateOrUpdate(Participant participant);
        Participant GetParticipant(string login, string cpf);
        Participant GetParticipant(long id);
    }
}

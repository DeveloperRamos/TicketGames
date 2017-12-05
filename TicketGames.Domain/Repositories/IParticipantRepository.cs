using TicketGames.Domain.Model;

namespace TicketGames.Domain.Repositories
{
    public interface IParticipantRepository
    {
        Participant Create(Participant participant);
        Participant Update(Participant participant);
        Participant GetParticipantByLoginAndCPF(string login, string cpf);
        Participant GetParticipantById(long id);
        Session GetSessionBySession(string session);
        Participant Authenticate(string login, string password, string salt);
    }
}

using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private string connection = ConfigurationManager.ConnectionStrings["TicketGamesContext"].ConnectionString;
        public ParticipantRepository()
        {
            this._context = new TicketGamesContext();
        }
        public Participant Create(Participant participant)
        {

            Session session = new Session();
            session.ExpirationDate = DateTime.Now.AddDays(3);
            session.Session_ = Guid.NewGuid().ToString();

            participant.Sessions.Add(session);

            var result = this._context.Set<Participant>().Add(participant);

            this._context.SaveChanges();

            return result;
        }

        public Participant GetParticipantById(long id)
        {
            var participant = this._context.Participants.Find(id);

            return participant;
        }

        public Participant GetParticipantByLoginAndCPF(string login, string cpf)
        {
            using (var connect = new MySqlConnection(connection))
            {
                Participant participant = new Participant();

                string query = @"Select * From Tb_Participant Where CPF = @cpf And Login = @login;";

                connect.Open();

                participant = connect.Query<Participant>(query, new { cpf = cpf, login = login }).FirstOrDefault();

                connect.Close();

                return participant;
            }
        }

        public Session GetSessionBySession(string session)
        {
            using (var connect = new MySqlConnection(connection))
            {
                Session _session = new Session();

                string query = @"Select * From Tb_Session Where Session = @session And Expiration < @date And Activated = 0;";

                connect.Open();

                _session = connect.Query<Session>(query, new { Session = session, Expiration = DateTime.Now }).FirstOrDefault();

                connect.Close();

                return _session;
            }
        }

        public Participant Update(Participant participant)
        {
            throw new NotImplementedException();
        }
    }
}

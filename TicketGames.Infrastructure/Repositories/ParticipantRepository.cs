using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
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

        public Participant Authenticate(string login, string password, string salt)
        {
            using (var connect = new MySqlConnection(connection))
            {
                Participant participant = new Participant();

                string query = @"Select * From Tb_Participant Where Login = @login And Password = @password And Salt = @salt And ParticipantStatusId = 1;";

                connect.Open();

                participant = connect.Query<Participant>(query, new { login = login, password = password, salt = salt }).FirstOrDefault();

                connect.Close();

                return participant;
            }
        }

        public Participant Create(Participant participant)
        {

            Session session = new Session();
            session.ExpirationDate = DateTime.Now.AddDays(3);
            session.SessionKey = Guid.NewGuid().ToString();

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

                string query = @"Select * From Tb_Session Where SessionKey = @session And ExpirationDate > @date And Activated = 0;";

                connect.Open();

                _session = connect.Query<Session>(query, new { session = session, date = DateTime.Now }).FirstOrDefault();

                connect.Close();

                return _session;
            }
        }

        public Participant Update(Participant participant)
        {
            var participantModified = new Participant();
            var sessionModified = new Session();

            using (var connect = new MySqlConnection(connection))
            {
                connect.Open();

                if (participant.Sessions.Count > 0)
                {
                    string querySession = @"Select * From Tb_Session Where SessionKey = @session And ExpirationDate > @date And Activated = 0;";

                    foreach (var session in participant.Sessions)
                    {
                        sessionModified = connect.Query<Session>(querySession, new { session = session.SessionKey, date = DateTime.Now }).FirstOrDefault();
                    }
                }

                string queryParticipant = @"Select * From Tb_Participant Where Id = @participantId;";

                participantModified = connect.Query<Participant>(queryParticipant, new { participantId = participant.Id }).FirstOrDefault();

                connect.Close();
            }

            if (sessionModified.Id > 0)
            {
                sessionModified.Activated = true;

                this._context.Entry(sessionModified).State = EntityState.Modified;
            }

            participantModified.ParticipantStatusId = participant.ParticipantStatusId;
            participantModified.Name = participant.Name;
            participantModified.Gender = participant.Gender;

            if (!string.IsNullOrEmpty(participant.Password) && !string.IsNullOrEmpty(participant.Salt))
            {
                participantModified.Password = participant.Password;
                participantModified.Salt = participant.Salt;
            }

            if (participant.BirthDate.HasValue)
            {
                participantModified.BirthDate = (DateTime)participant.BirthDate;
            }

            participantModified.RG = participant.RG;
            participantModified.Email = participant.Email;
            participantModified.HomePhone = participant.HomePhone;
            participantModified.CellPhone = participant.CellPhone;
            participantModified.Street = participant.Street;
            participantModified.Number = participant.Number;
            participantModified.Complement = participant.Complement;
            participantModified.District = participant.District;
            participantModified.City = participant.City;
            participantModified.State = participant.State;
            participantModified.ZipCode = participant.ZipCode;

            this._context.Entry(participantModified).State = EntityState.Modified;

            this._context.SaveChanges();

            return participant;
        }
    }
}

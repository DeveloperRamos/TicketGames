using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TicketGames.CrossCutting.Cryptography;

namespace TicketGames.API.Models.Participant
{
    public class Participant
    {
        public long Id { get; set; }
        public int ParticipantStatusId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Confirmpass { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Session { get; set; }

        public Participant()
        {

        }
        public Participant(Domain.Model.Participant participant)
        {
            this.Id = participant.Id;
            this.Name = participant.Name;
            this.Gender = participant.Gender;
            this.BirthDate = participant.BirthDate;
            this.CPF = participant.CPF;
            this.RG = participant.RG;
            this.Email = participant.Email;
            this.HomePhone = participant.HomePhone;
            this.CellPhone = participant.CellPhone;
            this.Street = participant.Street;
            this.Number = participant.Number;
            this.Complement = participant.Complement;
            this.District = participant.District;
            this.City = participant.City;
            this.State = participant.State;
            this.ZipCode = participant.ZipCode;
        }

        public Domain.Model.Participant MappingDomain()
        {
            Hash baseCrypt = new Hash();
            var salt = string.Empty;
            var password = string.Empty;

            if (!string.IsNullOrEmpty(this.Password))
            {
                salt = Guid.NewGuid().ToString();
                password = baseCrypt.GetHash(this.Password, salt, CypherType.SHA512);
            }

            Domain.Model.Participant participant = new Domain.Model.Participant()
            {
                Id = this.Id,
                ParticipantStatusId = (this.Id > 0) ? (int)ParticipantStatus.Active : (int)ParticipantStatus.Pending,
                Login = (this.Id > 0) ? this.Login : this.CPF,
                Password = !string.IsNullOrEmpty(password) ? password : string.Empty,
                Salt = !string.IsNullOrEmpty(salt) ? salt : string.Empty,
                Name = this.Name,
                Gender = this.Gender,
                BirthDate = this.BirthDate,
                CPF = this.CPF,
                RG = this.RG,
                Email = this.Email,
                HomePhone = this.HomePhone,
                CellPhone = this.CellPhone,
                Street = this.Street,
                Number = this.Number,
                Complement = this.Complement,
                District = this.District,
                City = this.City,
                State = this.State,
                ZipCode = this.ZipCode
            };

            return participant;
        }        
    }
}
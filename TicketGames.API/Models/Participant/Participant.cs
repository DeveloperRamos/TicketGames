using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TicketGames.API.Models.Participant
{
    public class Participant
    {
        public long Id { get; set; }
        public int ParticipantStatusId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
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
        public string Session { get; set; }

        public Participant()
        {

        }

        public Domain.Model.Participant MappingDomain()
        {
            Domain.Model.Participant participant = new Domain.Model.Participant()
            {
                Id = this.Id,
                ParticipantStatusId = (this.Id > 0) ? (int)ParticipantStatus.Active : (int)ParticipantStatus.Pending,
                Login = (this.Id > 0) ? this.Login : this.CPF,
                Password = !string.IsNullOrEmpty(this.Password) ? GeraMD5Hash(this.Password) : string.Empty,
                Salt = !string.IsNullOrEmpty(this.Password) ? GeraMD5Hash(this.Password + "@senha@") : string.Empty,
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
                State = this.State                
            };

            return participant;
        }
        private string GeraMD5Hash(string texto)
        {
            //cria instância da classe MD5CryptoServiceProvider

            MD5CryptoServiceProvider MD5provider = new MD5CryptoServiceProvider();

            //gera o hash do texto

            byte[] valorHash = MD5provider.ComputeHash(Encoding.Default.GetBytes(texto));

            StringBuilder str = new StringBuilder();

            //retorna o hash

            for (int contador = 0; contador < valorHash.Length; contador++)

            {

                str.Append(valorHash[contador].ToString("x2"));

            }

            return str.ToString();

        }
    }
}
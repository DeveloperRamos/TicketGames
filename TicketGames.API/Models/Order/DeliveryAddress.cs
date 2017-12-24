using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketGames.Domain.Model;

namespace TicketGames.API.Models.Order
{
    public class DeliveryAddress
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Reference { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }

        public string ValidationAddress()
        {
            if (string.IsNullOrEmpty(this.Street))
                return "Preciso informar o logradouro";

            if (string.IsNullOrEmpty(this.Number))
                return "Preciso informar o número";

            if (string.IsNullOrEmpty(this.Complement))
                return "Preciso informar o complemento";

            if (string.IsNullOrEmpty(this.District))
                return "Preciso informar o bairro";

            if (string.IsNullOrEmpty(this.City))
                return "Preciso informar a cidade";

            if (string.IsNullOrEmpty(this.State))
                return "Preciso informar o estado";

            if (string.IsNullOrEmpty(this.ZipCode))
                return "Preciso informar o CEP";

            if (string.IsNullOrEmpty(this.Email))
                return "Preciso informar o email";

            if (string.IsNullOrEmpty(this.CellPhone) || string.IsNullOrEmpty(this.HomePhone))
                return "Preciso informar um dos telefone para contato";

            return string.Empty;
        }

        public OrderDeliveryAddress MappingDomain()
        {
            var deliveryAddress = new OrderDeliveryAddress();

            deliveryAddress.Street = this.Street;
            deliveryAddress.Number = this.Number;
            deliveryAddress.Complement = this.Complement;
            deliveryAddress.District = this.District;
            deliveryAddress.City = this.City;
            deliveryAddress.State = this.State;
            deliveryAddress.ZipCode = this.ZipCode;
            deliveryAddress.Reference = this.Reference;
            deliveryAddress.Email = this.Email;
            deliveryAddress.HomePhone = this.HomePhone;
            deliveryAddress.CellPhone = this.CellPhone;

            return deliveryAddress;
        }
    }
}
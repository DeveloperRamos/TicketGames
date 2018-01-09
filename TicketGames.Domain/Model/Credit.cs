using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class Credit
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public bool Owner { get; set; }
        public string Brand { get; set; }
        public string CreditCardToken { get; set; }
        public string Session { get; set; }
        public string SenderHash { get; set; }
        public int Parcel { get; set; }
        public float Value { get; set; }
        public float SubTotal { get; set; }
        public string Holder { get; set; }
        public string CPF { get; set; }
        public DateTime DateNasc { get; set; }
        public string Contact { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime InsertDate { get; set; }
        public virtual Order Order { get; set; }






        #region PagSeguro Credit        
        public string NumberCard { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }


        //public Buyer Buyer { get; set; }
        //public Parcel Parcel { get; set; }
        //public ShippingAddress ShippingAddress { get; set; }
        //public BillingAddress BillingAddress { get; set; }
        // public CreditCardHolder CreditCardHolder { get; set; }

        #endregion


    }
}

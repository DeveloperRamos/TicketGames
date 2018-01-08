using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Domain.Model
{
    public class Credit
    {

        public int Quantity { get; set; }
        public float Value { get; set; }
        public float SubTotal { get; set; }





        #region PagSeguro Credit

        public long OrderId { get; set; }
        public bool Owner { get; set; }
        public string Brand { get; set; }
        public string Number { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        public string SenderHash { get; set; }
        public string CreditCardToken { get; set; }
        //public Buyer Buyer { get; set; }
        //public Parcel Parcel { get; set; }
        //public ShippingAddress ShippingAddress { get; set; }
        //public BillingAddress BillingAddress { get; set; }
        // public CreditCardHolder CreditCardHolder { get; set; }

        #endregion


    }
}

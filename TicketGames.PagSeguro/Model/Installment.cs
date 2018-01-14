using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.PagSeguro.Model
{
    public class Installment
    {
        public int Quantity { get; private set; }
        public decimal Value { get; private set; }

        public List<Installment> GetInstallments(Decimal amount, string creditCardBrand, int maxInstallmentNoInterest)
        {
            TicketGames.PagSeguro.Installment installment = new PagSeguro.Installment();
            List<Installment> installments = new List<Installment>();

            var result = installment.GetInstallmentsPagSeguro(amount, creditCardBrand, maxInstallmentNoInterest);

            foreach (var install in result.Get())
            {
                installments.Add(new Installment()
                {
                    Quantity = install.quantity,
                    Value = install.amount
                });
            }

            return installments;
        }
    }
}

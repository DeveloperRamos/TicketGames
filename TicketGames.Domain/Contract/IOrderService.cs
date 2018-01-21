using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;
using TicketGames.PagSeguro;

namespace TicketGames.Domain.Contract
{
    public interface IOrderService
    {
        long Redemption(TicketGames.PagSeguro.Model.Credit credit, Domain.Model.Transaction transaction, Domain.Model.Order order);
        long Redemption(TicketGames.PagSeguro.Model.Billet billet, Domain.Model.Transaction transaction, Domain.Model.Order order);

        List<PagSeguro.Model.Installment> Installments(Decimal amount, string creditCardBrand, int maxInstallmentNoInterest);
        Order GetOrder(long orderId);

        Billet GetBillet(long participantId, long orderId);
        Credit GetCredit(long participantId, long orderId);
    }
}

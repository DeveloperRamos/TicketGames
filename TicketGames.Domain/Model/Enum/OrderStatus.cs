
namespace TicketGames.Domain.Model.Enum
{
    public enum OrderStatus
    {
        Paid = 1,
        WaitingPayment = 2,
        InAnalysis = 3,
        Initiated = 4,
        Available = 5,
        InDispute = 6,
        Refunded = 7,
        Cancelled = 8
    }
}

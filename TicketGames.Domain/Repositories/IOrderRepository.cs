using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Repositories
{
    public interface IOrderRepository
    {
        Order Create(Order order);
        bool UpdateStatusByOrderId(long orderId, int orderStatusId);

        Credit CreateCreditByOrderId(Credit credit);

        bool CreateOrderHistory(OrderHistory history);

        bool UpdateOrderDeliveryAddressByCartId(long cartId, long orderId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Contract
{
    public interface ICartService
    {
        Cart Add(Cart cart);
        Cart Get(long participantId);
        Cart Delete(long participantId, long productId);
        OrderDeliveryAddress Add(OrderDeliveryAddress orderDeliveryAddress);
        OrderDeliveryAddress Get(long participantId, long cartId);
    }
}

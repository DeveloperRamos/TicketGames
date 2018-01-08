using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Contract
{
    public interface IOrderService
    {
        bool Redemption(Participant participant, Cart cart, OrderDeliveryAddress orderDeliveryAddress, Credit credit);
    }
}

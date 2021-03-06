﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Domain.Repositories
{
    public interface ICartRepository
    {
        Cart Create(Cart cart);
        Cart Update(Cart cart);
        Cart GetCartByParticipantId(long participantId);
        Cart DeleteCartItemByPartIdAndProdId(long participantId, long productId);
        OrderDeliveryAddress Create(OrderDeliveryAddress orderDeliveryAddress);
        OrderDeliveryAddress GetDeliveryAddressByPartIdAndCartId(long participantId, long cartId);
        bool UpdateStatusByCartId(long cartId, int cartStatusId);
    }
}

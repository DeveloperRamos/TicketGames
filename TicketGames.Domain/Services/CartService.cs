using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Contract;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;

namespace TicketGames.Domain.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        public Cart Add(Cart cart)
        {
            return this._cartRepository.Create(cart);
        }

        public OrderDeliveryAddress Add(OrderDeliveryAddress orderDeliveryAddress)
        {
            return this._cartRepository.Create(orderDeliveryAddress);
        }

        public Cart Delete(long participantId, long productId)
        {
            return this._cartRepository.DeleteCartItemByPartIdAndProdId(participantId, productId);
        }

        public Cart Get(long participantId)
        {
            return this._cartRepository.GetCartByParticipantId(participantId);
        }

        public OrderDeliveryAddress Get(long participantId, long cartId)
        {
            return this._cartRepository.GetDeliveryAddressByPartIdAndCartId(participantId, cartId);
        }
    }
}

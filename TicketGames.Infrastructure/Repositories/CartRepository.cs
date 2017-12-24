using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.Infrastructure.Context;

namespace TicketGames.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly TicketGamesContext _context;
        private string connection = ConfigurationManager.ConnectionStrings["TicketGamesContext"].ConnectionString;
        public CartRepository()
        {
            this._context = new TicketGamesContext();
        }

        public Cart Create(Cart cart)
        {
            cart.CartStatusId = 2;

            var result = this._context.Set<Cart>().Add(cart);

            this._context.SaveChanges();

            return result;
        }

        public OrderDeliveryAddress Create(OrderDeliveryAddress orderDeliveryAddress)
        {
            OrderDeliveryAddress deliveryAddressModified = null;

            if (orderDeliveryAddress.Id > 0)
            {


                using (var connect = new MySqlConnection(connection))
                {
                    connect.Open();

                    string query = @"Select A.* From Tb_OrderDeliveryAddress A Inner Join Tb_Cart C On(A.CartId = C.Id) Where A.Id = @orderDeliveryAddressId And C.CartStatusId = 2;";

                    deliveryAddressModified = connect.Query<OrderDeliveryAddress>(query, new { orderDeliveryAddressId = orderDeliveryAddress.Id }).FirstOrDefault();

                    connect.Close();
                }

                deliveryAddressModified.Street = orderDeliveryAddress.Street;
                deliveryAddressModified.Number = orderDeliveryAddress.Number;
                deliveryAddressModified.Complement = orderDeliveryAddress.Complement;
                deliveryAddressModified.District = orderDeliveryAddress.District;
                deliveryAddressModified.City = orderDeliveryAddress.City;
                deliveryAddressModified.State = orderDeliveryAddress.State;
                deliveryAddressModified.Reference = orderDeliveryAddress.Reference;
                deliveryAddressModified.ZipCode = orderDeliveryAddress.ZipCode;
                deliveryAddressModified.Email = orderDeliveryAddress.Email;
                deliveryAddressModified.CellPhone = orderDeliveryAddress.CellPhone;
                deliveryAddressModified.HomePhone = orderDeliveryAddress.HomePhone;

                this._context.Entry(deliveryAddressModified).State = EntityState.Modified;
            }
            else
            {
                deliveryAddressModified = this._context.Set<OrderDeliveryAddress>().Add(orderDeliveryAddress);
            }

            this._context.SaveChanges();

            return deliveryAddressModified;

        }

        public Cart GetCartByParticipantId(long participantId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                //Cart cart = new Cart();

                string query = @"Select * From Tb_Cart C " +
                                "Inner Join Tb_CartItem I On(C.Id = I.CartId) " +
                                "Inner Join Tb_Product P On(P.Id = I.ProductId) " +
                                "Inner Join Tb_Raffle R On(R.Id = I.RaffleId) " +
                                "Where ParticipantId = @participantId And CartStatusId = 2 And R.RaffleStatusId In(3,4);";

                connect.Open();

                //cart = connect.Query<Cart>(query, new { participantId = participantId }).FirstOrDefault();


                var cartDictionary = new Dictionary<long, Cart>();

                var result = connect.Query<Cart, CartItem, Product, Raffle, Cart>(query,
                  (cart, cartItem, product, raffle) =>
                  {
                      Cart cartEntity;

                      if (!cartDictionary.TryGetValue(cart.Id, out cartEntity))
                      {
                          cartEntity = cart;
                          cartEntity.CartItems = new List<CartItem>();
                          cartDictionary.Add(cart.Id, cartEntity);
                      }

                      cartItem.Product = product;
                      cartItem.Raffle = raffle;
                      cartEntity.CartItems.Add(cartItem);

                      return cartEntity;

                  }, new { participantId = participantId }).Distinct().FirstOrDefault();

                connect.Close();

                return result ?? new Cart();
            }
        }

        public OrderDeliveryAddress GetDeliveryAddressByPartIdAndCartId(long participantId, long cartId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                connect.Open();

                string query = @"Select A.* From Tb_OrderDeliveryAddress A Inner Join Tb_Cart C On(A.CartId = C.Id) Where A.ParticipantId = @participantId And C.CartStatusId = 2;";

                var deliveryAddress = connect.Query<OrderDeliveryAddress>(query, new { participantId = participantId }).FirstOrDefault();

                connect.Close();

                return deliveryAddress ?? new OrderDeliveryAddress();
            }
        }

        public Cart Update(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}

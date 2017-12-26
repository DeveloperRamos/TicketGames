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

        public Cart Create(Cart _cart)
        {
            Cart result = null;
            bool SaveChanges = false;

            if (_cart.Id > 0)
            {
                var items = _cart.CartItems.Where(i => i.Id == 0).ToList();

                if (items.Count > 0)
                {
                    foreach (var cartItemAdd in items)
                    {
                        cartItemAdd.CartId = _cart.Id;
                        cartItemAdd.Product = null;
                        cartItemAdd.Raffle = null;
                    }

                    this._context.Set<CartItem>().AddRange(items);

                    SaveChanges = true;
                }

                using (var connect = new MySqlConnection(connection))
                {
                    string query = @"Select * From Tb_Cart C " +
                                    "Inner Join Tb_CartItem I On(C.Id = I.CartId) " +
                                    "Inner Join Tb_Product P On(P.Id = I.ProductId) " +
                                    "Inner Join Tb_Raffle R On(R.Id = I.RaffleId) " +
                                    "Where ParticipantId = @participantId And CartStatusId = 2 And R.RaffleStatusId In(3,4);";

                    connect.Open();

                    var cartDictionary = new Dictionary<long, Cart>();

                    result = connect.Query<Cart, CartItem, Product, Raffle, Cart>(query,
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

                      }, new { participantId = _cart.ParticipantId }).Distinct().FirstOrDefault();

                    connect.Close();

                    foreach (var cartItemModified in result.CartItems)
                    {
                        try
                        {
                            var cartItem = _cart.CartItems.Where(i => i.ProductId == cartItemModified.ProductId).First();

                            if (cartItemModified.Quantity != cartItem.Quantity)
                            {
                                cartItemModified.Quantity = cartItem.Quantity;

                                cartItemModified.Product = null;
                                cartItemModified.Raffle = null;

                                this._context.Entry(cartItemModified).State = EntityState.Modified;

                                SaveChanges = true;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            else
            {

                foreach (var cartItemAdd in _cart.CartItems)
                {
                    cartItemAdd.Product = null;
                    cartItemAdd.Raffle = null;
                }

                _cart.CartStatusId = 2;

                result = this._context.Set<Cart>().Add(_cart);

                SaveChanges = true;
            }

            if (SaveChanges)
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

                deliveryAddressModified.Name = orderDeliveryAddress.Name;
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

        public Cart DeleteCartItemByPartIdAndProdId(long participantId, long productId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                string query = @"Select * From Tb_Cart C " +
                                "Inner Join Tb_CartItem I On(C.Id = I.CartId) " +
                                "Inner Join Tb_Product P On(P.Id = I.ProductId) " +
                                "Inner Join Tb_Raffle R On(R.Id = I.RaffleId) " +
                                "Where ParticipantId = @participantId And CartStatusId = 2 And R.RaffleStatusId In(3,4);";

                connect.Open();

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


                if (result != null)
                {
                    var cartItemId = result.CartItems.Where(p => p.ProductId == productId).Select(s => s.Id).First();

                    connect.Query(@"Delete From Tb_CartItem Where Id = @cartItemId;", new { cartItemId = cartItemId });

                    if (result.CartItems.Count == 1)
                    {
                        connect.Query(@"Delete From Tb_OrderDeliveryAddress Where CartId = @cartId And ParticipantId = @participantId;", new { cartId = result.Id, participantId = participantId });

                        connect.Query(@"Delete From Tb_Cart Where Id = @cartId And ParticipantId = @participantId;", new { cartId = result.Id, participantId = participantId });
                    }
                }

                connect.Close();

                return result ?? new Cart();
            }
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

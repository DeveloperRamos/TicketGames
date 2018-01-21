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
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketGamesContext _context;
        private string connection = ConfigurationManager.ConnectionStrings["TicketGamesContext"].ConnectionString;
        public OrderRepository()
        {
            this._context = new TicketGamesContext();
        }

        public Order Create(Order order)
        {
            var result = this._context.Set<Order>().Add(order);

            this._context.SaveChanges();

            return result;
        }

        public Billet CreateBilletByOrderId(Billet billet)
        {
            var result = this._context.Set<Billet>().Add(billet);

            this._context.SaveChanges();

            return result;
        }

        public Credit CreateCreditByOrderId(Credit credit)
        {
            var result = this._context.Set<Credit>().Add(credit);

            this._context.SaveChanges();

            return result;
        }

        public bool CreateOrderHistory(OrderHistory history)
        {
            var result = this._context.Set<OrderHistory>().Add(history);

            return this._context.SaveChanges() > 0;
        }

        public Billet GetBilletByOrderId(long participantId, long orderId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                //connect.Open();

                string queryBillet = @"Select B.* From Tb_Billet B Inner Join Tb_Order O On(B.OrderId = O.Id) Where O.ParticipantId = @participantId And B.OrderId = @orderId;";

                var resultBillet = connect.Query<Billet>(queryBillet, new { orderId = orderId }).FirstOrDefault();

                //connect.Close();

                return resultBillet;
            }
        }

        public Credit GetCreditByOrderId(long participantId, long orderId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                //connect.Open();

                string queryCredit = @"Select C.* From Tb_Credit C Inner Join Tb_Order O On(C.OrderId = O.Id) Where O.ParticipantId = @participantId And C.OrderId = @orderId;";

                var resultCredit = connect.Query<Credit>(queryCredit, new { orderId = orderId }).FirstOrDefault();

                //connect.Close();

                return resultCredit;
            }
        }

        public bool UpdateOrderDeliveryAddressByCartId(long cartId, long orderId)
        {
            var orderDeliveryAddressModified = new OrderDeliveryAddress();

            using (var connect = new MySqlConnection(connection))
            {
                //connect.Open();

                string queryOrderDeliveryAddress = @"Select * From Tb_OrderDeliveryAddress Where CartId = @cartId;";

                orderDeliveryAddressModified = connect.Query<OrderDeliveryAddress>(queryOrderDeliveryAddress, new { cartId = cartId }).FirstOrDefault();

                //connect.Close();
            }

            orderDeliveryAddressModified.OrderId = orderId;

            this._context.Entry(orderDeliveryAddressModified).State = EntityState.Modified;

            return this._context.SaveChanges() > 0;
        }

        public bool UpdateStatusByOrderId(long orderId, int orderStatusId)
        {
            var orderModified = new Order();

            using (var connect = new MySqlConnection(connection))
            {
                //connect.Open();

                string queryOrder = @"Select * From Tb_Order Where Id = @orderId;";

                orderModified = connect.Query<Order>(queryOrder, new { orderId = orderId }).FirstOrDefault();

                //connect.Close();
            }

            orderModified.OrderStatusId = orderStatusId;

            this._context.Entry(orderModified).State = EntityState.Modified;

            return this._context.SaveChanges() > 0;
        }
    }
}

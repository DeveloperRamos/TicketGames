using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;
using TicketGames.Domain.Repositories;
using TicketGames.Infrastructure.Context;

namespace TicketGames.Infrastructure.Repositories
{
    public class RaffleRepository : IRaffleRepository
    {
        private readonly TicketGamesContext _context;
        private string connection = ConfigurationManager.ConnectionStrings["TicketGamesContext"].ConnectionString;
        public RaffleRepository()
        {
            this._context = new TicketGamesContext();
        }
        public Raffle GetRaffleByProductId(long productId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                connect.Open();

                var raffle = connect.Query<Raffle>("Select * From Tb_Raffle Where ProductId = @prodId", new { prodId = productId }).Single();               

                connect.Close();

                return raffle;
            }
        }
    }
}

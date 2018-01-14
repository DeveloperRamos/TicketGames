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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TicketGamesContext _context;
        private string connection = ConfigurationManager.ConnectionStrings["TicketGamesContext"].ConnectionString;
        public TransactionRepository()
        {
            this._context = new TicketGamesContext();
        }

        public Transaction CreateTransaction(Transaction transaction)
        {
            var result = this._context.Set<Transaction>().Add(transaction);

            this._context.SaveChanges();

            return result;
        }

        public List<Transaction> GetTransactionsByParticipantId(long participantId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                connect.Open();

                string query = @"Select * From Tb_Transaction Where ParticipantId = @participantId;";

                var transactions = connect.Query<Transaction>(query, new { participantId = participantId }).ToList();

                connect.Close();


                return transactions ?? new List<Transaction>();
            }
        }
    }
}

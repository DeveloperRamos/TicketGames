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

        public async Task<Raffle> GetRaffleAsyncByProductId(long productId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                //connect.Open();                

                var raffleDictionary = new Dictionary<long, Raffle>();

                var result = connect.Query<Raffle, Product, LuckyNumber, Raffle>("Select * From Tb_Raffle R " +
                    "Inner Join Tb_Product P On(P.Id = R.ProductId) " +
                    "Inner Join Tb_LuckyNumber N On(R.Id = N.RaffleId) " +
                    "Where R.ProductId = @productId And R.RaffleStatusId In(3,4);",
                 (raffle, product, luckyNumber) =>
                 {
                     Raffle raffleEntity;

                     if (!raffleDictionary.TryGetValue(raffle.Id, out raffleEntity))
                     {
                         raffleEntity = raffle;
                         raffleEntity.LuckyNumbers = new List<LuckyNumber>();
                         raffleDictionary.Add(raffle.Id, raffleEntity);
                         raffleEntity.Product = product;
                     }

                     raffleEntity.LuckyNumbers.Add(luckyNumber);
                     return raffleEntity;

                 }, new { productId = productId }).Distinct().FirstOrDefault();

                //connect.Close();

                return result;
            }
        }


        Raffle IRaffleRepository.GetRaffleByProductId(long productId)
        {
            using (var connect = new MySqlConnection(connection))
            {
                //connect.Open();                

                var raffleDictionary = new Dictionary<long, Raffle>();

                var result = connect.Query<Raffle, Product, LuckyNumber, Raffle>("Select * From Tb_Raffle R " +
                    "Inner Join Tb_Product P On(P.Id = R.ProductId) " +
                    "Inner Join Tb_LuckyNumber N On(R.Id = N.RaffleId) " +
                    "Where R.ProductId = @productId And R.RaffleStatusId In(3,4);",
                 (raffle, product, luckyNumber) =>
                 {
                     Raffle raffleEntity;

                     if (!raffleDictionary.TryGetValue(raffle.Id, out raffleEntity))
                     {
                         raffleEntity = raffle;
                         raffleEntity.LuckyNumbers = new List<LuckyNumber>();
                         raffleDictionary.Add(raffle.Id, raffleEntity);
                         raffleEntity.Product = product;
                     }

                     raffleEntity.LuckyNumbers.Add(luckyNumber);
                     return raffleEntity;

                 }, new { productId = productId }).Distinct().FirstOrDefault();

                //connect.Close();

                return result;
            }
        }
    }
}

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
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly TicketGamesContext _context;
        private string connection = ConfigurationManager.ConnectionStrings["TicketGamesContext"].ConnectionString;
        public ConfigurationRepository()
        {
            this._context = new TicketGamesContext();
        }

        public Domain.Model.Configuration GetConfigurationByKey(string key)
        {
            using (var connect = new MySqlConnection(connection))
            {
                //connect.Open();

                string queryConfiguration = @"Select * From Tb_Configuration Where UPPER(key) = UPPER(@key) And Active = 1;";

                var configuration = connect.Query<Domain.Model.Configuration>(queryConfiguration, new { key = key }).FirstOrDefault();

                //connect.Close();

                return configuration;
            }
        }

        public List<Domain.Model.Configuration> GetSettings()
        {
            using (var connect = new MySqlConnection(connection))
            {
                //connect.Open();

                string querySettings = @"Select * From Tb_Configuration Where Active = 1;";

                var settings = connect.Query<Domain.Model.Configuration>(querySettings).ToList();

                //connect.Close();

                return settings;
            }
        }
    }
}

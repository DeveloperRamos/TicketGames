using System;

namespace TicketGames.Domain.Model
{
    public class Configuration
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime InsertDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public bool Active { get; set; }
    }
}

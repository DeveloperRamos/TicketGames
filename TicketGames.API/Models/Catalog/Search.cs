using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Catalog
{
    public class Search
    {
        public long CategoryId { get; set; }
        public long DepartmentId { get; set; }
        public string Word { get; set; }
    }
}
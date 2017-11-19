using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Catalog
{
    public class Search
    {
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string Word { get; set; }

        public Search()
        {
            this.CategoryId = 0;
            this.DepartmentId = 0;
            this.Word = string.Empty;
        }
    }
}
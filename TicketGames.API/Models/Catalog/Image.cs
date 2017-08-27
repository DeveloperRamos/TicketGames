using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketGames.API.Models.Catalog
{
    public class Image
    {
        public long Id { get; set; }
        public ImageType ImageType { get; set; }
        public string URL { get; set; }
    }
}
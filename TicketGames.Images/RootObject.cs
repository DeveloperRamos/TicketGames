using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.Images
{
    class RootObject
    {
        public bool success { get; set; }
        public int process_time { get; set; }
        public Result result { get; set; }

        public class Album
        {
            public string id { get; set; }
            public string title { get; set; }
            public bool @public { get; set; }
        }

        public class Avatar
        {
            public string id { get; set; }
            public string filename { get; set; }
            public int server { get; set; }
            public bool cropped { get; set; }
            public int x_pos { get; set; }
            public int y_pos { get; set; }
            public int x_length { get; set; }
            public int y_length { get; set; }
        }

        public class Owner
        {
            public string username { get; set; }
            public Avatar avatar { get; set; }
            public string membership { get; set; }
            public bool featured_photographer { get; set; }
        }

        public class Image
        {
            public string id { get; set; }
            public int server { get; set; }
            public int bucket { get; set; }
            public string filename { get; set; }
            public string direct_link { get; set; }
            public string original_filename { get; set; }
            public string title { get; set; }
            public Album album { get; set; }
            public int creation_date { get; set; }
            public bool @public { get; set; }
            public bool hidden { get; set; }
            public int filesize { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int likes { get; set; }
            public bool liked { get; set; }
            public bool is_owner { get; set; }
            public Owner owner { get; set; }
            public bool adult_content { get; set; }
        }

        public class Result
        {
            public string base_url { get; set; }
            public int limit { get; set; }
            public int offset { get; set; }
            public int total { get; set; }
            public List<Image> images { get; set; }
        }
    }
}

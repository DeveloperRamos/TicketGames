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
        public Image()
        {

        }
        public Image(Domain.Model.Image image)
        {
            this.Id = image.Id;
            this.ImageType = (ImageType)image.ImageTypeId;
            this.URL = image.Url;
        }

        public List<Image> MappingImages(List<Domain.Model.Image> images)
        {
            List<Image> _images = new List<Image>();

            foreach (Domain.Model.Image image in images)
            {
                var Image = new Image(image);

                _images.Add(Image);
            }

            return _images;
        }
    }
}
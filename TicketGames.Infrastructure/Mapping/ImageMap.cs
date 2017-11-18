using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(550);

            // Table & Column Mappings
            this.ToTable("Tb_Image");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageTypeId).HasColumnName("ImageTypeId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Order).HasColumnName("Order");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships            
            this.HasRequired(t => t.ImageType)
                .WithMany(t => t.Images)
                .HasForeignKey(d => d.ImageTypeId);

        }
    }
}

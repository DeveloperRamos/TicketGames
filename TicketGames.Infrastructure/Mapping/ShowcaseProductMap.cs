using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class ShowcaseProductMap : EntityTypeConfiguration<ShowcaseProduct>
    {
        public ShowcaseProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_Product");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ShowcaseId).HasColumnName("ShowcaseId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.Order).HasColumnName("Order");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships            
            this.HasRequired(t => t.Showcase)
                .WithMany(t => t.ShowcaseProducts)
                .HasForeignKey(d => d.ShowcaseId);

            this.HasRequired(t => t.Product)
                .WithMany(t => t.ShowcaseProducts)
                .HasForeignKey(d => d.ProductId);
        }
    }
}

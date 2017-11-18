using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class ShowcaseMap : EntityTypeConfiguration<Showcase>
    {
        public ShowcaseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_Showcase");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ShowcaseTypeId).HasColumnName("ShowcaseTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.Active).HasColumnName("Active");

            // Relationships            
            this.HasRequired(t => t.ShowcaseType)
                .WithMany(t => t.Showcases)
                .HasForeignKey(d => d.ShowcaseTypeId);


        }
    }
}

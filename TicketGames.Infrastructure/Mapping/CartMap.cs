using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class CartMap : EntityTypeConfiguration<Cart>
    {
        public CartMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_Cart");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ParticipantId).HasColumnName("ParticipantId");
            this.Property(t => t.CartStatusId).HasColumnName("CartStatusId");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            this.HasRequired(t => t.Participant)
                .WithMany(t => t.Carts)
                .HasForeignKey(d => d.ParticipantId);

            this.HasRequired(t => t.CartStatus)
                .WithMany(t => t.Carts)
                .HasForeignKey(d => d.CartStatusId);

        }
    }
}

using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class CartItemMap : EntityTypeConfiguration<CartItem>
    {
        public CartItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_CartItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CartId).HasColumnName("CartId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.RaffleId).HasColumnName("RaffleId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships
            this.HasRequired(t => t.Cart)
                .WithMany(t => t.CartItems)
                .HasForeignKey(d => d.CartId);

            this.HasRequired(t => t.Product)
                .WithMany(t => t.CartItems)
                .HasForeignKey(d => d.ProductId);

            this.HasRequired(t => t.Raffle)
                .WithMany(t => t.CartItems)
                .HasForeignKey(d => d.RaffleId);

        }
    }
}

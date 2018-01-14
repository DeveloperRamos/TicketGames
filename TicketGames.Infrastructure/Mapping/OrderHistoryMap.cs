using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class OrderHistoryMap : EntityTypeConfiguration<OrderHistory>
    {
        public OrderHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_OrderHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderId).HasColumnName("OrderId");
            this.Property(t => t.OrderStatusId).HasColumnName("OrderStatusId");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");

            // Relationships            
            this.HasRequired(t => t.Order)
                .WithMany(t => t.OrderHistory)
                .HasForeignKey(d => d.OrderId);

            this.HasRequired(t => t.OrderStatus)
                .WithMany(t => t.OrderHistory)
                .HasForeignKey(d => d.OrderStatusId);

        }
    }
}

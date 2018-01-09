using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class OrderStatusMap : EntityTypeConfiguration<OrderStatus>
    {
        public OrderStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_OrderStatus");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}

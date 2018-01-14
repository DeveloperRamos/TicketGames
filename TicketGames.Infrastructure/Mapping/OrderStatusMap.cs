using System.Data.Entity.ModelConfiguration;

namespace TicketGames.Infrastructure.Mapping
{
    public class OrderStatusMap : EntityTypeConfiguration<Domain.Model.OrderStatus>
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
            this.Property(t => t.External).HasColumnName("External");
        }
    }
}

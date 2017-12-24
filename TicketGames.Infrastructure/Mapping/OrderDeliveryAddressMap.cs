using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class OrderDeliveryAddressMap : EntityTypeConfiguration<OrderDeliveryAddress>
    {
        public OrderDeliveryAddressMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_OrderDeliveryAddress");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CartId).HasColumnName("CartId");
            this.Property(t => t.ParticipantId).HasColumnName("ParticipantId");
            this.Property(t => t.OrderId).HasColumnName("OrderId");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.Complement).HasColumnName("Complement");
            this.Property(t => t.District).HasColumnName("District");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.ZipCode).HasColumnName("ZipCode");
            this.Property(t => t.Reference).HasColumnName("Reference");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.HomePhone).HasColumnName("HomePhone");
            this.Property(t => t.CellPhone).HasColumnName("CellPhone");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships            
            this.HasRequired(t => t.Cart)
                .WithMany(t => t.OrderDeliveryAddress)
                .HasForeignKey(d => d.CartId);

            this.HasRequired(t => t.Participant)
                .WithMany(t => t.OrderDeliveryAddress)
                .HasForeignKey(d => d.ParticipantId);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_Order");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ParticipantId).HasColumnName("ParticipantId");
            this.Property(t => t.OrderStatusId).HasColumnName("OrderStatusId");
            this.Property(t => t.PaymentType).HasColumnName("PaymentType");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.Money).HasColumnName("Money");
            this.Property(t => t.Point).HasColumnName("Point");

            // Relationships            
            this.HasRequired(t => t.Participant)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.ParticipantId);

            this.HasRequired(t => t.OrderStatus)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.OrderStatusId);
        }
    }
}

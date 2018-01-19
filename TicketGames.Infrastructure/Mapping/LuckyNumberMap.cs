using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class LuckyNumberMap : EntityTypeConfiguration<LuckyNumber>
    {
        public LuckyNumberMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_LuckyNumber");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RaffleId).HasColumnName("RaffleId");
            this.Property(t => t.CartId).HasColumnName("CartId");
            this.Property(t => t.OrderId).HasColumnName("OrderId");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
        }
    }
}

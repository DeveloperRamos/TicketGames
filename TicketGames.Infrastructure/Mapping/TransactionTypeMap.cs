using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class TransactionTypeMap : EntityTypeConfiguration<TransactionType>
    {
        public TransactionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_TransactionType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Symbol).HasColumnName("Symbol");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}

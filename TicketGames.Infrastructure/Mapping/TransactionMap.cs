using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class TransactionMap : EntityTypeConfiguration<Transaction>
    {
        public TransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_Transaction");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ParticipantId).HasColumnName("ParticipantId");
            this.Property(t => t.TransactionTypeId).HasColumnName("TransactionTypeId");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.RaffleId).HasColumnName("RaffleId");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships            
            this.HasRequired(t => t.Participant)
                .WithMany(t => t.Transactions)
                .HasForeignKey(d => d.ParticipantId);

            this.HasRequired(t => t.TransactionType)
                .WithMany(t => t.Transactions)
                .HasForeignKey(d => d.TransactionTypeId);
        }
    }
}

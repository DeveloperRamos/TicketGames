using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class RaffleMap : EntityTypeConfiguration<Raffle>
    {
        public RaffleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_Raffle");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.RaffleStatusId).HasColumnName("RaffleStatusId");
            this.Property(t => t.Winner).HasColumnName("Winner");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.ExpectedDate).HasColumnName("ExpectedDate");
            this.Property(t => t.DrawDate).HasColumnName("DrawDate");
            this.Property(t => t.Extend).HasColumnName("Extend");
            this.Property(t => t.ParticipantsNumber).HasColumnName("ParticipantsNumber");
            this.Property(t => t.DrawNumber).HasColumnName("DrawNumber");

            // Relationships            
            this.HasRequired(t => t.RaffleStatus)
                .WithMany(t => t.Raffles)
                .HasForeignKey(d => d.RaffleStatusId);

            this.HasRequired(t => t.Product)
                .WithMany(t => t.Raffles)
                .HasForeignKey(d => d.ProductId);
        }
    }
}

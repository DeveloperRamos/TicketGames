using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class SessionMap : EntityTypeConfiguration<Session>
    {
        public SessionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_Session");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Session_).HasColumnName("Session");
            this.Property(t => t.ParticipantId).HasColumnName("ParticipantId");
            this.Property(t => t.ExpirationDate).HasColumnName("ExpirationDate");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.Activated).HasColumnName("Activated");

            // Relationships            
            this.HasRequired(t => t.Participant)
                .WithMany(t => t.Sessions)
                .HasForeignKey(d => d.ParticipantId);

        }
    }
}

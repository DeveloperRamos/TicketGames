using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    class ParticipantMap : EntityTypeConfiguration<Participant>
    {
        public ParticipantMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_Participant");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ParticipantStatusId).HasColumnName("ParticipantStatusId");
            this.Property(t => t.Login).HasColumnName("Login");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Salt).HasColumnName("Salt");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.CPF).HasColumnName("CPF");
            this.Property(t => t.RG).HasColumnName("RG");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.HomePhone).HasColumnName("HomePhone");
            this.Property(t => t.CellPhone).HasColumnName("CellPhone");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.Complement).HasColumnName("Complement");
            this.Property(t => t.District).HasColumnName("District");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.ZipCode).HasColumnName("ZipCode");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");

            // Relationships            
            this.HasRequired(t => t.ParticipantStatus)
                .WithMany(t => t.Participants)
                .HasForeignKey(d => d.ParticipantStatusId);

        }
    }
}

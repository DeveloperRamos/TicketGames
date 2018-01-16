using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    public class BilletMap : EntityTypeConfiguration<Billet>
    {
        public BilletMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_Billet");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderId).HasColumnName("OrderId");
            this.Property(t => t.SenderHash).HasColumnName("SenderHash");
            this.Property(t => t.Session).HasColumnName("Session");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CPF).HasColumnName("CPF");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.Complement).HasColumnName("Complement");
            this.Property(t => t.ZipCode).HasColumnName("ZipCode");
            this.Property(t => t.District).HasColumnName("District");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.FeeAmount).HasColumnName("FeeAmount");
            this.Property(t => t.NetAmount).HasColumnName("NetAmount");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.PaymentLink).HasColumnName("PaymentLink");

        }
    }
}

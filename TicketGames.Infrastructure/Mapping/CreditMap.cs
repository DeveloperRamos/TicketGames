using System.Data.Entity.ModelConfiguration;

namespace TicketGames.Infrastructure.Mapping
{
    public class CreditMap : EntityTypeConfiguration<Domain.Model.Credit>
    {
        public CreditMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_Credit");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OrderId).HasColumnName("OrderId");
            this.Property(t => t.Owner).HasColumnName("Owner");
            this.Property(t => t.Brand).HasColumnName("Brand");
            this.Property(t => t.SenderHash).HasColumnName("SenderHash");
            this.Property(t => t.CreditCardToken).HasColumnName("CreditCardToken");
            this.Property(t => t.Session).HasColumnName("Session");
            this.Property(t => t.Parcel).HasColumnName("Parcel");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.SubTotal).HasColumnName("SubTotal");
            this.Property(t => t.Holder).HasColumnName("Holder");
            this.Property(t => t.CPF).HasColumnName("CPF");
            this.Property(t => t.DateNasc).HasColumnName("DateNasc");
            this.Property(t => t.Contact).HasColumnName("Contact");
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


        }
    }
}

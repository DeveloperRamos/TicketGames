using System.Data.Entity.ModelConfiguration;

namespace TicketGames.Infrastructure.Mapping
{
    class ConfigurationMap : EntityTypeConfiguration<Domain.Model.Configuration>
    {
        public ConfigurationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("Tb_Configuration");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.Active).HasColumnName("Active");

        }
    }
}

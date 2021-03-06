﻿using System.Data.Entity.ModelConfiguration;
using TicketGames.Domain.Model;

namespace TicketGames.Infrastructure.Mapping
{
    class ShowcaseTypeMap : EntityTypeConfiguration<ShowcaseType>
    {
        public ShowcaseTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Tb_ShowcaseType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}

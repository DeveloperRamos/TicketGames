﻿using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using TicketGames.Domain.Model;
using TicketGames.Infrastructure.Mapping;

namespace TicketGames.Infrastructure.Context
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class TicketGamesContext : DbContext
    {
        static TicketGamesContext()
        {

        }

        public TicketGamesContext() : base("Name=TicketGamesContext")
        {
            //Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImageType> ImageTypes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ShowcaseType> ShowcaseTypes { get; set; }
        public DbSet<Showcase> Showcases { get; set; }
        public DbSet<ShowcaseProduct> ShowcaseProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ImageTypeMap());
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new ShowcaseTypeMap());
            modelBuilder.Configurations.Add(new ShowcaseMap());
            modelBuilder.Configurations.Add(new ShowcaseProductMap());
        }

        public override int SaveChanges()
        {
            foreach (var entity in ChangeTracker.Entries().Where(entity => entity.Entity.GetType().GetProperty("InsertDate") != null))
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Property("InsertDate").CurrentValue = DateTime.Now;
                }

                if (entity.State == EntityState.Modified)
                {
                    entity.Property("InsertDate").IsModified = false;
                    entity.Property("UpdateDate").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}

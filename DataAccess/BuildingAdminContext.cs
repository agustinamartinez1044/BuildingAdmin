﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain;

namespace DataAccess
{
    public class BuildingAdminContext : DbContext
    {
        public BuildingAdminContext() { }
        
        public BuildingAdminContext(DbContextOptions options) : base(options){ }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString(@"BuildingAdmin");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}

﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(BuildingAdminContext))]
    partial class BuildingAdminContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<short>("Bathrooms")
                        .HasColumnType("smallint");

                    b.Property<int?>("BuildingId")
                        .HasColumnType("int");

                    b.Property<int>("DoorNumber")
                        .HasColumnType("int");

                    b.Property<short>("Floor")
                        .HasColumnType("smallint");

                    b.Property<bool>("HasTerrace")
                        .HasColumnType("bit");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<short>("Rooms")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Apartment");
                });

            modelBuilder.Entity("Domain.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ConstructionCompanyId")
                        .HasColumnType("int");

                    b.Property<float>("Expenses")
                        .HasColumnType("real");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConstructionCompanyId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.ConstructionCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ConstructionCompanies");
                });

            modelBuilder.Entity("Domain.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("Domain.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("Domain.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ApartmentId")
                        .HasColumnType("int");

                    b.Property<int?>("AssignedToId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AttentionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BuildingId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("BuildingId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedById");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Administrator", b =>
                {
                    b.HasBaseType("Domain.User");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("Domain.MaintenanceOperator", b =>
                {
                    b.HasBaseType("Domain.User");

                    b.Property<int?>("buildingId")
                        .HasColumnType("int");

                    b.HasIndex("buildingId");

                    b.HasDiscriminator().HasValue("MaintenanceOperator");
                });

            modelBuilder.Entity("Domain.Manager", b =>
                {
                    b.HasBaseType("Domain.User");

                    b.HasDiscriminator().HasValue("Manager");
                });

            modelBuilder.Entity("Domain.Apartment", b =>
                {
                    b.HasOne("Domain.Building", null)
                        .WithMany("Apartments")
                        .HasForeignKey("BuildingId");

                    b.HasOne("Domain.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Building", b =>
                {
                    b.HasOne("Domain.ConstructionCompany", "ConstructionCompany")
                        .WithMany()
                        .HasForeignKey("ConstructionCompanyId");

                    b.HasOne("Domain.Manager", null)
                        .WithMany("Buildings")
                        .HasForeignKey("ManagerId");

                    b.Navigation("ConstructionCompany");
                });

            modelBuilder.Entity("Domain.Ticket", b =>
                {
                    b.HasOne("Domain.Apartment", "Apartment")
                        .WithMany()
                        .HasForeignKey("ApartmentId");

                    b.HasOne("Domain.User", "AssignedTo")
                        .WithMany()
                        .HasForeignKey("AssignedToId");

                    b.HasOne("Domain.Building", null)
                        .WithMany("Tickets")
                        .HasForeignKey("BuildingId");

                    b.HasOne("Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("Apartment");

                    b.Navigation("AssignedTo");

                    b.Navigation("Category");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Domain.MaintenanceOperator", b =>
                {
                    b.HasOne("Domain.Building", "building")
                        .WithMany()
                        .HasForeignKey("buildingId");

                    b.Navigation("building");
                });

            modelBuilder.Entity("Domain.Building", b =>
                {
                    b.Navigation("Apartments");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Domain.Manager", b =>
                {
                    b.Navigation("Buildings");
                });
#pragma warning restore 612, 618
        }
    }
}

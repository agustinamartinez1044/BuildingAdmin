﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Owner_OwnerId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Administrators_AdministratorId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_AdministratorId",
                table: "Invitations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "AdministratorId",
                table: "Invitations");

            migrationBuilder.RenameTable(
                name: "Apartments",
                newName: "Apartment");

            migrationBuilder.RenameTable(
                name: "Administrators",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartment",
                newName: "IX_Apartment_OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "Apartment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expenses = table.Column<float>(type: "real", nullable: false),
                    ConstructionCompanyId = table.Column<int>(type: "int", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Building_ConstructionCompany_ConstructionCompanyId",
                        column: x => x.ConstructionCompanyId,
                        principalTable: "ConstructionCompany",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Building_User_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedToId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AttentionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApartmentId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    BuildingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_User_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_BuildingId",
                table: "Apartment",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_ConstructionCompanyId",
                table: "Building",
                column: "ConstructionCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_ManagerId",
                table: "Building",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ApartmentId",
                table: "Ticket",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AssignedToId",
                table: "Ticket",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_BuildingId",
                table: "Ticket",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CategoryId",
                table: "Ticket",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CreatedById",
                table: "Ticket",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_Building_BuildingId",
                table: "Apartment",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_Owner_OwnerId",
                table: "Apartment",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_Building_BuildingId",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_Owner_OwnerId",
                table: "Apartment");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ConstructionCompany");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment");

            migrationBuilder.DropIndex(
                name: "IX_Apartment_BuildingId",
                table: "Apartment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "Apartment");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "User");

            migrationBuilder.RenameTable(
                name: "Apartment",
                newName: "Apartments");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Administrators");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_OwnerId",
                table: "Apartments",
                newName: "IX_Apartments_OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "AdministratorId",
                table: "Invitations",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_AdministratorId",
                table: "Invitations",
                column: "AdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Owner_OwnerId",
                table: "Apartments",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Administrators_AdministratorId",
                table: "Invitations",
                column: "AdministratorId",
                principalTable: "Administrators",
                principalColumn: "Id");
        }
    }
}

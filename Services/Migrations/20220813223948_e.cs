using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    public partial class e : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_IdentityUser_CreatedById",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Fines_IdentityUser_CreatedById",
                table: "Fines");

            migrationBuilder.DropForeignKey(
                name: "FK_HGSs_IdentityUser_CreatedById",
                table: "HGSs");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_IdentityUser_CreatedById",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_IdentityUser_CreatedById",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_IdentityUser_CreatedById",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_IdentityUser_CreatedById",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_CreatedById",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Rents_CreatedById",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_CreatedById",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Items_CreatedById",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_HGSs_CreatedById",
                table: "HGSs");

            migrationBuilder.DropIndex(
                name: "IX_Fines_CreatedById",
                table: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_Bills_CreatedById",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "HGSs");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Bills");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Vehicles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Rents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Maintenances",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Items",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "HGSs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Fines",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Bills",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HGSs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Bills");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Vehicles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Rents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Maintenances",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "HGSs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Fines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Bills",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CreatedById",
                table: "Vehicles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_CreatedById",
                table: "Rents",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_CreatedById",
                table: "Maintenances",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedById",
                table: "Items",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HGSs_CreatedById",
                table: "HGSs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_CreatedById",
                table: "Fines",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CreatedById",
                table: "Bills",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_IdentityUser_CreatedById",
                table: "Bills",
                column: "CreatedById",
                principalTable: "IdentityUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_IdentityUser_CreatedById",
                table: "Fines",
                column: "CreatedById",
                principalTable: "IdentityUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HGSs_IdentityUser_CreatedById",
                table: "HGSs",
                column: "CreatedById",
                principalTable: "IdentityUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_IdentityUser_CreatedById",
                table: "Items",
                column: "CreatedById",
                principalTable: "IdentityUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_IdentityUser_CreatedById",
                table: "Maintenances",
                column: "CreatedById",
                principalTable: "IdentityUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_IdentityUser_CreatedById",
                table: "Rents",
                column: "CreatedById",
                principalTable: "IdentityUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_IdentityUser_CreatedById",
                table: "Vehicles",
                column: "CreatedById",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}

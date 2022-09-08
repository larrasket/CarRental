using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Services.Migrations
{
    public partial class maintenance_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Maintenances_MaintenanceId",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "HGSs");

            migrationBuilder.RenameColumn(
                name: "MaintenanceId",
                table: "Bills",
                newName: "CycleMaintenanceId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_MaintenanceId",
                table: "Bills",
                newName: "IX_Bills_CycleMaintenanceId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Bills",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Maintenances_CycleMaintenanceId",
                table: "Bills",
                column: "CycleMaintenanceId",
                principalTable: "Maintenances",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Maintenances_CycleMaintenanceId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "CycleMaintenanceId",
                table: "Bills",
                newName: "MaintenanceId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_CycleMaintenanceId",
                table: "Bills",
                newName: "IX_Bills_MaintenanceId");

            migrationBuilder.CreateTable(
                name: "HGSs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BillId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HGSs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HGSs_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HGSs_BillId",
                table: "HGSs",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Maintenances_MaintenanceId",
                table: "Bills",
                column: "MaintenanceId",
                principalTable: "Maintenances",
                principalColumn: "Id");
        }
    }
}

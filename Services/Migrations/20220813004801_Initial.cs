using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Services.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DaysToRenew = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenances_IdentityUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_IdentityUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    MaintenanceId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_IdentityUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_Maintenances_MaintenanceId",
                        column: x => x.MaintenanceId,
                        principalTable: "Maintenances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Types = table.Column<string>(type: "text", nullable: false),
                    BillId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fines_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fines_IdentityUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HGSs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Location = table.Column<string>(type: "text", nullable: true),
                    BillId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HGSs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HGSs_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HGSs_IdentityUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BillId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_IdentityUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RentStart = table.Column<DateOnly>(type: "date", nullable: false),
                    RentEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    VehicleId = table.Column<long>(type: "bigint", nullable: false),
                    ContractId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rents_Bills_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_IdentityUser_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rents_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CreatedById",
                table: "Bills",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_MaintenanceId",
                table: "Bills",
                column: "MaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_BillId",
                table: "Fines",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_CreatedById",
                table: "Fines",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HGSs_BillId",
                table: "HGSs",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_HGSs_CreatedById",
                table: "HGSs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BillId",
                table: "Items",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedById",
                table: "Items",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_CreatedById",
                table: "Maintenances",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_ContractId",
                table: "Rents",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_CreatedById",
                table: "Rents",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_VehicleId",
                table: "Rents",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CreatedById",
                table: "Vehicles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Number",
                table: "Vehicles",
                column: "Number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropTable(
                name: "HGSs");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}

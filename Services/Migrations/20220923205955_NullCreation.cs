using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    public partial class NullCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Creators_CreationId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Fines_Creators_CreationId",
                table: "Fines");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Creators_CreationId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Creators_CreationId",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Creators_CreationId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<long>(
                name: "CreationId",
                table: "Vehicles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreationId",
                table: "Rents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreationId",
                table: "Maintenances",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreationId",
                table: "Fines",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreationId",
                table: "Bills",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Creators_CreationId",
                table: "Bills",
                column: "CreationId",
                principalTable: "Creators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_Creators_CreationId",
                table: "Fines",
                column: "CreationId",
                principalTable: "Creators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Creators_CreationId",
                table: "Maintenances",
                column: "CreationId",
                principalTable: "Creators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Creators_CreationId",
                table: "Rents",
                column: "CreationId",
                principalTable: "Creators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Creators_CreationId",
                table: "Vehicles",
                column: "CreationId",
                principalTable: "Creators",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Creators_CreationId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Fines_Creators_CreationId",
                table: "Fines");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Creators_CreationId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Creators_CreationId",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Creators_CreationId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<long>(
                name: "CreationId",
                table: "Vehicles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreationId",
                table: "Rents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreationId",
                table: "Maintenances",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreationId",
                table: "Fines",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreationId",
                table: "Bills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Creators_CreationId",
                table: "Bills",
                column: "CreationId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_Creators_CreationId",
                table: "Fines",
                column: "CreationId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Creators_CreationId",
                table: "Maintenances",
                column: "CreationId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Creators_CreationId",
                table: "Rents",
                column: "CreationId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Creators_CreationId",
                table: "Vehicles",
                column: "CreationId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    public partial class CreationFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Creators_CreatorId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Fines_Creators_CreatorId",
                table: "Fines");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Creators_CreatorId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Creators_CreatorId",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Creators_CreatorId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Vehicles",
                newName: "CreationId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_CreatorId",
                table: "Vehicles",
                newName: "IX_Vehicles_CreationId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Rents",
                newName: "CreationId");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_CreatorId",
                table: "Rents",
                newName: "IX_Rents_CreationId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Maintenances",
                newName: "CreationId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenances_CreatorId",
                table: "Maintenances",
                newName: "IX_Maintenances_CreationId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Fines",
                newName: "CreationId");

            migrationBuilder.RenameIndex(
                name: "IX_Fines_CreatorId",
                table: "Fines",
                newName: "IX_Fines_CreationId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Bills",
                newName: "CreationId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_CreatorId",
                table: "Bills",
                newName: "IX_Bills_CreationId");

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

            migrationBuilder.RenameColumn(
                name: "CreationId",
                table: "Vehicles",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_CreationId",
                table: "Vehicles",
                newName: "IX_Vehicles_CreatorId");

            migrationBuilder.RenameColumn(
                name: "CreationId",
                table: "Rents",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_CreationId",
                table: "Rents",
                newName: "IX_Rents_CreatorId");

            migrationBuilder.RenameColumn(
                name: "CreationId",
                table: "Maintenances",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenances_CreationId",
                table: "Maintenances",
                newName: "IX_Maintenances_CreatorId");

            migrationBuilder.RenameColumn(
                name: "CreationId",
                table: "Fines",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Fines_CreationId",
                table: "Fines",
                newName: "IX_Fines_CreatorId");

            migrationBuilder.RenameColumn(
                name: "CreationId",
                table: "Bills",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_CreationId",
                table: "Bills",
                newName: "IX_Bills_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Creators_CreatorId",
                table: "Bills",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_Creators_CreatorId",
                table: "Fines",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Creators_CreatorId",
                table: "Maintenances",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Creators_CreatorId",
                table: "Rents",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Creators_CreatorId",
                table: "Vehicles",
                column: "CreatorId",
                principalTable: "Creators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

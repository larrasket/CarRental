using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    public partial class BillId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Bills_ContractId",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Rents",
                newName: "BillId");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_ContractId",
                table: "Rents",
                newName: "IX_Rents_BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Bills_BillId",
                table: "Rents",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Bills_BillId",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "BillId",
                table: "Rents",
                newName: "ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_BillId",
                table: "Rents",
                newName: "IX_Rents_ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Bills_ContractId",
                table: "Rents",
                column: "ContractId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

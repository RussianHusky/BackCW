using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackCW.Migrations
{
    /// <inheritdoc />
    public partial class addedPartnerID1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartnerID",
                table: "SecretSantas",
                newName: "PartnerIDId");

            migrationBuilder.CreateIndex(
                name: "IX_SecretSantas_PartnerIDId",
                table: "SecretSantas",
                column: "PartnerIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecretSantas_SecretSantas_PartnerIDId",
                table: "SecretSantas",
                column: "PartnerIDId",
                principalTable: "SecretSantas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecretSantas_SecretSantas_PartnerIDId",
                table: "SecretSantas");

            migrationBuilder.DropIndex(
                name: "IX_SecretSantas_PartnerIDId",
                table: "SecretSantas");

            migrationBuilder.RenameColumn(
                name: "PartnerIDId",
                table: "SecretSantas",
                newName: "PartnerID");
        }
    }
}

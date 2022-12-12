using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackCW.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecretSantas_SecretSantas_PartnerIDId",
                table: "SecretSantas");

            migrationBuilder.RenameColumn(
                name: "PartnerIDId",
                table: "SecretSantas",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_SecretSantas_PartnerIDId",
                table: "SecretSantas",
                newName: "IX_SecretSantas_PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecretSantas_SecretSantas_PartnerId",
                table: "SecretSantas",
                column: "PartnerId",
                principalTable: "SecretSantas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecretSantas_SecretSantas_PartnerId",
                table: "SecretSantas");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "SecretSantas",
                newName: "PartnerIDId");

            migrationBuilder.RenameIndex(
                name: "IX_SecretSantas_PartnerId",
                table: "SecretSantas",
                newName: "IX_SecretSantas_PartnerIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecretSantas_SecretSantas_PartnerIDId",
                table: "SecretSantas",
                column: "PartnerIDId",
                principalTable: "SecretSantas",
                principalColumn: "Id");
        }
    }
}

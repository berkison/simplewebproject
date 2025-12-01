using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simple.Migrations
{
    /// <inheritdoc />
    public partial class IdentityEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_kitaplar",
                table: "kitaplar");

            migrationBuilder.RenameTable(
                name: "kitaplar",
                newName: "Kitaplar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kitaplar",
                table: "Kitaplar",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Kitaplar",
                table: "Kitaplar");

            migrationBuilder.RenameTable(
                name: "Kitaplar",
                newName: "kitaplar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_kitaplar",
                table: "kitaplar",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.Id);
                });
        }
    }
}

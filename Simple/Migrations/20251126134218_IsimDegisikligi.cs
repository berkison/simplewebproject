using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simple.Migrations
{
    /// <inheritdoc />
    public partial class IsimDegisikligi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adi",
                table: "kitaplar",
                newName: "Ad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "kitaplar",
                newName: "Adi");
        }
    }
}

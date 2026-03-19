using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maal.Migrations
{
    /// <inheritdoc />
    public partial class AddAllowRoundDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowRoundDeletion",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowRoundDeletion",
                table: "Games");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboration2_A.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsBorrowed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBoorowed",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBoorowed",
                table: "Books");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskProjectBackend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "groupsessions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "groupsessions");
        }
    }
}

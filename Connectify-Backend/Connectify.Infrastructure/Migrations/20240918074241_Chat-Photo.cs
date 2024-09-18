using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChatPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Chats",
                type: "VARCHAR(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Chats");
        }
    }
}

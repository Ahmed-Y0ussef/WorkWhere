using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Place_PlaceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PlaceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlaceId",
                table: "Users",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Place_PlaceId",
                table: "Users",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "Id");
        }
    }
}

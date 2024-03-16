using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class t2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Room_PlaceId",
                table: "Room",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Place_PlaceId",
                table: "Room",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Place_PlaceId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_PlaceId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Room");
        }
    }
}

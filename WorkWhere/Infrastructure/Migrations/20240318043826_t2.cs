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
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UtilityName",
                table: "RoomUtilities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "UtilityName",
                table: "PlaceUtilities");

            migrationBuilder.RenameColumn(
                name: "Review",
                table: "RoomReview",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Review",
                table: "PlaceReview",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Review",
                table: "CourseReview",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RoomUtilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PlaceUtilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RoomUtilities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PlaceUtilities");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RoomReview",
                newName: "Review");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PlaceReview",
                newName: "Review");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "CourseReview",
                newName: "Review");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UtilityName",
                table: "RoomUtilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UtilityName",
                table: "PlaceUtilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

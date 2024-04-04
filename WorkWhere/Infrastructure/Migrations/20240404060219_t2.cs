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
            migrationBuilder.DropForeignKey(
                name: "FK_RoomBooking_RoomTimeSlot_TimeSlotId",
                table: "RoomBooking");

            migrationBuilder.DropTable(
                name: "RoomTimeSlot");

            migrationBuilder.DropIndex(
                name: "IX_RoomBooking_TimeSlotId",
                table: "RoomBooking");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RoomBooking");

            migrationBuilder.RenameColumn(
                name: "TimeSlotId",
                table: "RoomBooking",
                newName: "StartTime");

            migrationBuilder.AddColumn<int>(
                name: "EndTime",
                table: "RoomBooking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "RoomBooking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RoomBooking_PlaceId",
                table: "RoomBooking",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBooking_Place_PlaceId",
                table: "RoomBooking",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomBooking_Place_PlaceId",
                table: "RoomBooking");

            migrationBuilder.DropIndex(
                name: "IX_RoomBooking_PlaceId",
                table: "RoomBooking");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "RoomBooking");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "RoomBooking");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "RoomBooking",
                newName: "TimeSlotId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RoomBooking",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoomTimeSlot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    TimeEnd = table.Column<long>(type: "bigint", nullable: false),
                    TimeStrart = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTimeSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTimeSlot_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomBooking_TimeSlotId",
                table: "RoomBooking",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTimeSlot_RoomId",
                table: "RoomTimeSlot",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBooking_RoomTimeSlot_TimeSlotId",
                table: "RoomBooking",
                column: "TimeSlotId",
                principalTable: "RoomTimeSlot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

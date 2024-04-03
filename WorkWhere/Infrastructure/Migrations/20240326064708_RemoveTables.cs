using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");
            migrationBuilder.DropTable(
                name: "AspNetUserLogins");
            migrationBuilder.DropTable(
                name: "AspNetUserTokens");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

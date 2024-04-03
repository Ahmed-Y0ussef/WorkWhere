using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Guid adminRoleId = Guid.NewGuid();
            Guid guestRoleId = Guid.NewGuid();
            migrationBuilder.Sql($"INSERT INTO Role (Id, Name, NormalizedName) VALUES ('{adminRoleId}', 'Admin', 'ADMIN')");
            migrationBuilder.Sql($"INSERT INTO Role (Id, Name, NormalizedName) VALUES ('{guestRoleId}', 'Guest', 'GUEST')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Name IN ('Admin', 'Guest')");
        }
    }
}

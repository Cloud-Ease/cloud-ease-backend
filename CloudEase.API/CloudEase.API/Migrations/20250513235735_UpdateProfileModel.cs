using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudEase.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProfileModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Profiles",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Profiles",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Profiles",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Profiles",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Profiles",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Profiles",
                newName: "CreateAt");
        }
    }
}

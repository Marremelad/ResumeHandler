using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeHandler.Migrations
{
    /// <inheritdoc />
    public partial class Init20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ContactInformation_ContactInformationFk",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ContactInformation");

            migrationBuilder.DropIndex(
                name: "IX_Users_ContactInformationFk",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInformationFk",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(254)",
                maxLength: 254,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ContactInformationFk",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContactInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactInformationFk",
                table: "Users",
                column: "ContactInformationFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ContactInformation_ContactInformationFk",
                table: "Users",
                column: "ContactInformationFk",
                principalTable: "ContactInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

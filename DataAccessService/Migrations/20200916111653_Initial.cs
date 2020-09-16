using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Guard.Against.Null(migrationBuilder, nameof(migrationBuilder));

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            Guard.Against.Null(migrationBuilder, nameof(migrationBuilder));

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Name", "DisplayName", "Url" },
                values: new object[] { "account-list-get", "Get Account List", "http://localhost:44000/cb.accounts" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}

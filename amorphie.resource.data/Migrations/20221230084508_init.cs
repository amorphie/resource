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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Enabled = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime?>(type: "date", nullable: true),
                    UpdatedDate = table.Column<DateTime?>(type: "date", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] {"Id", "Name", "DisplayName", "Url", "Description", "Enabled", "CreatedDate", "UpdatedDate", "CreatedUser","UpdatedUser" },
                values: new object[] {Guid.NewGuid(), "account-list-get", "Get Account List", "http://localhost:44000/cb.accounts", "Get Account List Resource", 1, DateTime.Now, (DateTime?)null, "User1", (string)null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceLanguage",
                table: "ResourceLanguage");

            migrationBuilder.RenameTable(
                name: "ResourceLanguage",
                newName: "ResourceLanguages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceLanguages",
                table: "ResourceLanguages",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceLanguages",
                table: "ResourceLanguages");

            migrationBuilder.RenameTable(
                name: "ResourceLanguages",
                newName: "ResourceLanguage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceLanguage",
                table: "ResourceLanguage",
                column: "Id");
        }
    }
}

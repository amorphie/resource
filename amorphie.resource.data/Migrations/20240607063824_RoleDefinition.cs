using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class RoleDefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleDefinitionId",
                table: "Translations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DefinitionId",
                table: "Roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RoleDefinition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tags = table.Column<string[]>(type: "text[]", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDefinition", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translations_RoleDefinitionId",
                table: "Translations",
                column: "RoleDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DefinitionId",
                table: "Roles",
                column: "DefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_RoleDefinition_DefinitionId",
                table: "Roles",
                column: "DefinitionId",
                principalTable: "RoleDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_RoleDefinition_RoleDefinitionId",
                table: "Translations",
                column: "RoleDefinitionId",
                principalTable: "RoleDefinition",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_RoleDefinition_DefinitionId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Translations_RoleDefinition_RoleDefinitionId",
                table: "Translations");

            migrationBuilder.DropTable(
                name: "RoleDefinition");

            migrationBuilder.DropIndex(
                name: "IX_Translations_RoleDefinitionId",
                table: "Translations");

            migrationBuilder.DropIndex(
                name: "IX_Roles_DefinitionId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleDefinitionId",
                table: "Translations");

            migrationBuilder.DropColumn(
                name: "DefinitionId",
                table: "Roles");
        }
    }
}

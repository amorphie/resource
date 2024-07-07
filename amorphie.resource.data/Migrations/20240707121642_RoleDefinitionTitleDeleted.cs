using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class RoleDefinitionTitleDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translations_RoleDefinitions_RoleDefinitionId",
                table: "Translations");

            migrationBuilder.DropIndex(
                name: "IX_Translations_RoleDefinitionId",
                table: "Translations");

            migrationBuilder.DropColumn(
                name: "RoleDefinitionId",
                table: "Translations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleDefinitionId",
                table: "Translations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translations_RoleDefinitionId",
                table: "Translations",
                column: "RoleDefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_RoleDefinitions_RoleDefinitionId",
                table: "Translations",
                column: "RoleDefinitionId",
                principalTable: "RoleDefinitions",
                principalColumn: "Id");
        }
    }
}

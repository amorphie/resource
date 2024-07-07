using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class RoleDefinitionFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_RoleDefinition_DefinitionId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Translations_RoleDefinition_RoleDefinitionId",
                table: "Translations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleDefinition",
                table: "RoleDefinition");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "RoleDefinition");

            migrationBuilder.RenameTable(
                name: "RoleDefinition",
                newName: "RoleDefinitions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleDefinitions",
                table: "RoleDefinitions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_RoleDefinitions_DefinitionId",
                table: "Roles",
                column: "DefinitionId",
                principalTable: "RoleDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_RoleDefinitions_RoleDefinitionId",
                table: "Translations",
                column: "RoleDefinitionId",
                principalTable: "RoleDefinitions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_RoleDefinitions_DefinitionId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Translations_RoleDefinitions_RoleDefinitionId",
                table: "Translations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleDefinitions",
                table: "RoleDefinitions");

            migrationBuilder.RenameTable(
                name: "RoleDefinitions",
                newName: "RoleDefinition");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "RoleDefinition",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleDefinition",
                table: "RoleDefinition",
                column: "Id");

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
    }
}

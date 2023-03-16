using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class v18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceRateLimits_Resources_ResourceId",
                table: "ResourceRateLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceRoles_Resources_ResourceId",
                table: "ResourceRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceRoles_Roles_RoleId",
                table: "ResourceRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleGroupRoles_RoleGroups_RoleGroupId",
                table: "RoleGroupRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleGroupRoles_Roles_RoleId",
                table: "RoleGroupRoles");

            migrationBuilder.DropIndex(
                name: "IX_RoleGroupRoles_RoleGroupId",
                table: "RoleGroupRoles");

            migrationBuilder.DropIndex(
                name: "IX_RoleGroupRoles_RoleId",
                table: "RoleGroupRoles");

            migrationBuilder.DropIndex(
                name: "IX_ResourceRoles_ResourceId",
                table: "ResourceRoles");

            migrationBuilder.DropIndex(
                name: "IX_ResourceRoles_RoleId",
                table: "ResourceRoles");

            migrationBuilder.DropIndex(
                name: "IX_ResourceRateLimits_ResourceId",
                table: "ResourceRateLimits");

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("534e04f3-fc7a-47e7-8a11-ecd1989f8ea5"));

            migrationBuilder.DeleteData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: new Guid("070866ee-e1b2-464c-968c-3e352dbfaee5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ddda4d0f-f967-43dc-bfc9-a9b4e75c6038"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("354f5bef-19d0-439b-8436-bfb10f66414c"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("5a70d7f3-ab00-4d76-9e14-ece9a40948b9"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("635df6eb-6022-4cb7-aeab-fa5eee53a2a4"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("7b876b46-dc06-45e5-a81f-61769b85f250"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("d0f40c85-d2f0-419e-bdad-2a018b3e4ddd"));

            migrationBuilder.AlterColumn<int>(
                name: "Limit",
                table: "ResourceRateLimits",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Privileges",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags", "Type", "Url" },
                values: new object[] { new Guid("ec88a6d6-b243-40ab-a598-e350143ebfe3"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1185), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1196), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), "A", new[] { "tag1", "tag2" }, (byte)0, "urlsample" });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags" },
                values: new object[] { new Guid("9c72b6a7-5475-41d8-8e9b-d73c8bd1dcdc"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1242), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1243), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), "A", new[] { "tag1", "tag2" } });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags" },
                values: new object[] { new Guid("9c60439c-5cd0-40db-aa43-07da7dbf97d9"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1230), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1231), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), "A", new[] { "tag1", "tag2" } });

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Label", "Language", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "ResourceId_Description", "ResourceId_DisplayName", "RoleGroupId_Title", "RoleId_Title" },
                values: new object[,]
                {
                    { new Guid("1d064d61-b8ba-42f3-8b5a-fbde508a9b59"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1270), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), "Açıklama", "tr", new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1271), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("ec88a6d6-b243-40ab-a598-e350143ebfe3"), null, null, null },
                    { new Guid("4758b395-453f-455d-8d2e-d94cf2a6c85f"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1280), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), "Rol Grup Başlık", "tr", new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1280), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), null, null, new Guid("9c72b6a7-5475-41d8-8e9b-d73c8bd1dcdc"), null },
                    { new Guid("58cc4d41-0a33-498c-8a50-4d4c824ed03b"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1273), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), "Description", "en", new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1274), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("ec88a6d6-b243-40ab-a598-e350143ebfe3"), null, null, null },
                    { new Guid("9ad2f5e1-0629-414a-8e47-3c4e9d5701d1"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1277), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), "Rol Başlık", "tr", new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1278), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), null, null, null, new Guid("9c60439c-5cd0-40db-aa43-07da7dbf97d9") },
                    { new Guid("fef2ff34-6832-42ec-b1e6-f6405b04b1cd"), new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1275), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), "Başlık", "tr", new DateTime(2023, 3, 16, 15, 0, 45, 413, DateTimeKind.Local).AddTicks(1275), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), new Guid("bcdad1dd-edd0-4604-8f82-6d9b5a95f57e"), null, new Guid("ec88a6d6-b243-40ab-a598-e350143ebfe3"), null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("1d064d61-b8ba-42f3-8b5a-fbde508a9b59"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("4758b395-453f-455d-8d2e-d94cf2a6c85f"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("58cc4d41-0a33-498c-8a50-4d4c824ed03b"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("9ad2f5e1-0629-414a-8e47-3c4e9d5701d1"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("fef2ff34-6832-42ec-b1e6-f6405b04b1cd"));

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("ec88a6d6-b243-40ab-a598-e350143ebfe3"));

            migrationBuilder.DeleteData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: new Guid("9c72b6a7-5475-41d8-8e9b-d73c8bd1dcdc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9c60439c-5cd0-40db-aa43-07da7dbf97d9"));

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Privileges");

            migrationBuilder.AlterColumn<int>(
                name: "Limit",
                table: "ResourceRateLimits",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags", "Type", "Url" },
                values: new object[] { new Guid("534e04f3-fc7a-47e7-8a11-ecd1989f8ea5"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9434), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9451), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "A", new[] { "tag1", "tag2" }, (byte)0, "urlsample" });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags" },
                values: new object[] { new Guid("070866ee-e1b2-464c-968c-3e352dbfaee5"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9544), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9545), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "A", new[] { "tag1", "tag2" } });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags" },
                values: new object[] { new Guid("ddda4d0f-f967-43dc-bfc9-a9b4e75c6038"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9510), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9511), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "A", new[] { "tag1", "tag2" } });

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Label", "Language", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "ResourceId_Description", "ResourceId_DisplayName", "RoleGroupId_Title", "RoleId_Title" },
                values: new object[,]
                {
                    { new Guid("354f5bef-19d0-439b-8436-bfb10f66414c"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9606), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "Rol Grup Başlık", "tr", new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9606), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), null, null, null, null },
                    { new Guid("5a70d7f3-ab00-4d76-9e14-ece9a40948b9"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9578), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "Description", "en", new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9579), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), null, null, null, null },
                    { new Guid("635df6eb-6022-4cb7-aeab-fa5eee53a2a4"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9601), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "Rol Başlık", "tr", new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9602), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), null, null, null, null },
                    { new Guid("7b876b46-dc06-45e5-a81f-61769b85f250"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9596), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "Başlık", "tr", new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9597), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), null, null, null, null },
                    { new Guid("d0f40c85-d2f0-419e-bdad-2a018b3e4ddd"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9573), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "Açıklama", "tr", new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9573), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), null, null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroupRoles_RoleGroupId",
                table: "RoleGroupRoles",
                column: "RoleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroupRoles_RoleId",
                table: "RoleGroupRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRoles_ResourceId",
                table: "ResourceRoles",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRoles_RoleId",
                table: "ResourceRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRateLimits_ResourceId",
                table: "ResourceRateLimits",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceRateLimits_Resources_ResourceId",
                table: "ResourceRateLimits",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceRoles_Resources_ResourceId",
                table: "ResourceRoles",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceRoles_Roles_RoleId",
                table: "ResourceRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleGroupRoles_RoleGroups_RoleGroupId",
                table: "RoleGroupRoles",
                column: "RoleGroupId",
                principalTable: "RoleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleGroupRoles_Roles_RoleId",
                table: "RoleGroupRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

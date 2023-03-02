using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class v17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Privileges_Resources_ResourceId",
                table: "Privileges");

            migrationBuilder.DropIndex(
                name: "IX_Privileges_ResourceId",
                table: "Privileges");

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: new Guid("874001e3-b769-497b-b047-33b1b256c659"));

            migrationBuilder.DeleteData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: new Guid("81cf2e59-964a-4af8-83c0-454f0b642d76"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("93e8b671-641f-493f-bf54-4a67a0b64d2e"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("0a20e26d-c612-42b8-8618-76615112e609"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("133e4993-fdf6-4066-9236-25db1bbb6ee4"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("464c0dd7-8aad-4e4a-b9e8-7b6856ab7efb"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("5a0e94c8-33b5-4361-891b-3c78ab60c6f9"));

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: new Guid("689cc733-306a-4891-9ed6-11791f2f10a9"));

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Privileges");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "Translations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Translations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Translations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Translations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "Roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Roles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "RoleGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RoleGroups",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "RoleGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoleGroups",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "RoleGroupRoles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RoleGroupRoles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "RoleGroupRoles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoleGroupRoles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "Resources",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Resources",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Resources",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resources",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "ResourceRoles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ResourceRoles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "ResourceRoles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ResourceRoles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "ResourceRateLimits",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ResourceRateLimits",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "ResourceRateLimits",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ResourceRateLimits",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Ttl",
                table: "Privileges",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "Privileges",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Privileges",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Privileges",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Privileges",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
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
                    { new Guid("354f5bef-19d0-439b-8436-bfb10f66414c"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9606), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "Rol Grup Başlık", "tr", new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9606), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), null, null, new Guid("070866ee-e1b2-464c-968c-3e352dbfaee5"), null },
                    { new Guid("5a70d7f3-ab00-4d76-9e14-ece9a40948b9"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9578), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "Description", "en", new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9579), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("534e04f3-fc7a-47e7-8a11-ecd1989f8ea5"), null, null, null },
                    { new Guid("635df6eb-6022-4cb7-aeab-fa5eee53a2a4"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9601), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "Rol Başlık", "tr", new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9602), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), null, null, null, new Guid("ddda4d0f-f967-43dc-bfc9-a9b4e75c6038") },
                    { new Guid("7b876b46-dc06-45e5-a81f-61769b85f250"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9596), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "Başlık", "tr", new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9597), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), null, new Guid("534e04f3-fc7a-47e7-8a11-ecd1989f8ea5"), null, null },
                    { new Guid("d0f40c85-d2f0-419e-bdad-2a018b3e4ddd"), new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9573), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), "Açıklama", "tr", new DateTime(2023, 3, 2, 14, 4, 14, 936, DateTimeKind.Local).AddTicks(9573), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("277fc767-8ec3-456f-8a95-632cbaf7d8c4"), new Guid("534e04f3-fc7a-47e7-8a11-ecd1989f8ea5"), null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "Translations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Translations",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Translations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Translations",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "Roles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Roles",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Roles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "RoleGroups",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RoleGroups",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "RoleGroups",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoleGroups",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "RoleGroupRoles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "RoleGroupRoles",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "RoleGroupRoles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RoleGroupRoles",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "Resources",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Resources",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Resources",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resources",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "ResourceRoles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ResourceRoles",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "ResourceRoles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ResourceRoles",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "ResourceRateLimits",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "ResourceRateLimits",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "ResourceRateLimits",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ResourceRateLimits",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Ttl",
                table: "Privileges",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedBy",
                table: "Privileges",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Privileges",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Privileges",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Privileges",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Privileges",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags", "Type", "Url" },
                values: new object[] { new Guid("874001e3-b769-497b-b047-33b1b256c659"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7684), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7699), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "A", new[] { "tag1", "tag2" }, (byte)0, "urlsample" });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags" },
                values: new object[] { new Guid("81cf2e59-964a-4af8-83c0-454f0b642d76"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7781), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7782), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "A", new[] { "tag1", "tag2" } });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags" },
                values: new object[] { new Guid("93e8b671-641f-493f-bf54-4a67a0b64d2e"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7756), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7757), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "A", new[] { "tag1", "tag2" } });

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Label", "Language", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "ResourceId_Description", "ResourceId_DisplayName", "RoleGroupId_Title", "RoleId_Title" },
                values: new object[,]
                {
                    { new Guid("0a20e26d-c612-42b8-8618-76615112e609"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7843), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "Rol Grup Başlık", "tr", new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7844), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), null, null, null, null },
                    { new Guid("133e4993-fdf6-4066-9236-25db1bbb6ee4"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7809), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "Açıklama", "tr", new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7811), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), null, null, null, null },
                    { new Guid("464c0dd7-8aad-4e4a-b9e8-7b6856ab7efb"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7829), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "Başlık", "tr", new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7829), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), null, null, null, null },
                    { new Guid("5a0e94c8-33b5-4361-891b-3c78ab60c6f9"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7825), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "Description", "en", new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7826), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), null, null, null, null },
                    { new Guid("689cc733-306a-4891-9ed6-11791f2f10a9"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7838), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "Rol Başlık", "tr", new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7839), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), null, null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Privileges_ResourceId",
                table: "Privileges",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Privileges_Resources_ResourceId",
                table: "Privileges",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

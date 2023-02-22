using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class v16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string[]>(type: "text[]", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tags = table.Column<string[]>(type: "text[]", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tags = table.Column<string[]>(type: "text[]", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Ttl = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Privileges_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceRateLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Scope = table.Column<string>(type: "text", nullable: true),
                    Condition = table.Column<string>(type: "text", nullable: true),
                    Cron = table.Column<string>(type: "text", nullable: true),
                    Limit = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceRateLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceRateLimits_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceRoles_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleGroupRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroupRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleGroupRoles_RoleGroups_RoleGroupId",
                        column: x => x.RoleGroupId,
                        principalTable: "RoleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleGroupRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    ResourceIdDescription = table.Column<Guid>(name: "ResourceId_Description", type: "uuid", nullable: true),
                    ResourceIdDisplayName = table.Column<Guid>(name: "ResourceId_DisplayName", type: "uuid", nullable: true),
                    RoleGroupIdTitle = table.Column<Guid>(name: "RoleGroupId_Title", type: "uuid", nullable: true),
                    RoleIdTitle = table.Column<Guid>(name: "RoleId_Title", type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_Resources_ResourceId_Description",
                        column: x => x.ResourceIdDescription,
                        principalTable: "Resources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Translations_Resources_ResourceId_DisplayName",
                        column: x => x.ResourceIdDisplayName,
                        principalTable: "Resources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Translations_RoleGroups_RoleGroupId_Title",
                        column: x => x.RoleGroupIdTitle,
                        principalTable: "RoleGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Translations_Roles_RoleId_Title",
                        column: x => x.RoleIdTitle,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

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
                    { new Guid("0a20e26d-c612-42b8-8618-76615112e609"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7843), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "Rol Grup Başlık", "tr", new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7844), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), null, null, new Guid("81cf2e59-964a-4af8-83c0-454f0b642d76"), null },
                    { new Guid("133e4993-fdf6-4066-9236-25db1bbb6ee4"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7809), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "Açıklama", "tr", new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7811), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("874001e3-b769-497b-b047-33b1b256c659"), null, null, null },
                    { new Guid("464c0dd7-8aad-4e4a-b9e8-7b6856ab7efb"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7829), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "Başlık", "tr", new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7829), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), null, new Guid("874001e3-b769-497b-b047-33b1b256c659"), null, null },
                    { new Guid("5a0e94c8-33b5-4361-891b-3c78ab60c6f9"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7825), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "Description", "en", new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7826), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("874001e3-b769-497b-b047-33b1b256c659"), null, null, null },
                    { new Guid("689cc733-306a-4891-9ed6-11791f2f10a9"), new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7838), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), "Rol Başlık", "tr", new DateTime(2023, 2, 22, 10, 15, 0, 930, DateTimeKind.Local).AddTicks(7839), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), new Guid("97e40e79-ad08-4b5e-896e-cb3e9d6a111b"), null, null, null, new Guid("93e8b671-641f-493f-bf54-4a67a0b64d2e") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Privileges_ResourceId",
                table: "Privileges",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRateLimits_ResourceId",
                table: "ResourceRateLimits",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRoles_ResourceId",
                table: "ResourceRoles",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRoles_RoleId",
                table: "ResourceRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroupRoles_RoleGroupId",
                table: "RoleGroupRoles",
                column: "RoleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroupRoles_RoleId",
                table: "RoleGroupRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_ResourceId_Description",
                table: "Translations",
                column: "ResourceId_Description");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_ResourceId_DisplayName",
                table: "Translations",
                column: "ResourceId_DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_RoleGroupId_Title",
                table: "Translations",
                column: "RoleGroupId_Title");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_RoleId_Title",
                table: "Translations",
                column: "RoleId_Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Privileges");

            migrationBuilder.DropTable(
                name: "ResourceRateLimits");

            migrationBuilder.DropTable(
                name: "ResourceRoles");

            migrationBuilder.DropTable(
                name: "RoleGroupRoles");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "RoleGroups");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

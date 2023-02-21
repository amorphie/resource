using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class v15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
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
                values: new object[] { new Guid("83135033-d8bf-4729-99e7-4734ad197f5d"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1352), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1369), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), "A", new[] { "tag1", "tag2" }, "CONNECT", "urlsample" });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags" },
                values: new object[] { new Guid("4462ca2c-981c-401d-8bcf-e228b05ddb96"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1428), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1430), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), "A", new[] { "tag1", "tag2" } });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags" },
                values: new object[] { new Guid("d62d4d38-ecdf-4b4e-b17c-0882412340bb"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1411), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1413), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), "A", new[] { "tag1", "tag2" } });

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Label", "Language", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "ResourceId_Description", "ResourceId_DisplayName", "RoleGroupId_Title", "RoleId_Title" },
                values: new object[,]
                {
                    { new Guid("3bdfe9ce-c4b8-4a60-a526-a8d3de22b8db"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1454), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), "Başlık", "tr", new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1454), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), null, new Guid("83135033-d8bf-4729-99e7-4734ad197f5d"), null, null },
                    { new Guid("6edc0d4e-c4b0-476e-a493-1df98dfefb68"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1461), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), "Rol Başlık", "tr", new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1461), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), null, null, null, new Guid("d62d4d38-ecdf-4b4e-b17c-0882412340bb") },
                    { new Guid("a000f232-0bad-4740-a789-e67a5995c91f"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1446), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), "Açıklama", "tr", new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1447), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("83135033-d8bf-4729-99e7-4734ad197f5d"), null, null, null },
                    { new Guid("c11d2b3a-79d1-4245-82f4-7ead28650baa"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1451), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), "Description", "en", new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1451), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("83135033-d8bf-4729-99e7-4734ad197f5d"), null, null, null },
                    { new Guid("fcb45d0c-fd75-45ba-bf5d-0ba577a63994"), new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1465), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), "Rol Grup Başlık", "tr", new DateTime(2023, 2, 21, 10, 20, 7, 224, DateTimeKind.Local).AddTicks(1465), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), new Guid("1b354a8e-c956-46cc-80fa-032914b179e0"), null, null, new Guid("4462ca2c-981c-401d-8bcf-e228b05ddb96"), null }
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

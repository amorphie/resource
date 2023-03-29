using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class v20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Ttl = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.Id);
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
                    Limit = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceRateLimits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string[]>(type: "text[]", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleGroupRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroupRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tags = table.Column<string[]>(type: "text[]", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
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
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string[]>(type: "text[]", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scopes", x => x.Id);
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
                    ScopeIdTitle = table.Column<Guid>(name: "ScopeId_Title", type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByBehalfOf = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Translations_Scopes_ScopeId_Title",
                        column: x => x.ScopeIdTitle,
                        principalTable: "Scopes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags", "Type", "Url" },
                values: new object[] { new Guid("336fe77c-e00e-49dc-b6c4-930111c495e5"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3398), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3419), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), "A", new[] { "tag1", "tag2" }, (byte)0, "urlsample" });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags" },
                values: new object[] { new Guid("40b3c202-f27e-4bcf-a26c-4b415497a8a8"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3499), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3500), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), "A", new[] { "tag1", "tag2" } });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "Status", "Tags" },
                values: new object[] { new Guid("e78c287d-f372-46ed-b782-9e52a4874696"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3476), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3478), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), "A", new[] { "tag1", "tag2" } });

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedByBehalfOf", "Label", "Language", "ModifiedAt", "ModifiedBy", "ModifiedByBehalfOf", "ResourceId_Description", "ResourceId_DisplayName", "RoleGroupId_Title", "RoleId_Title", "ScopeId_Title" },
                values: new object[,]
                {
                    { new Guid("1431d2d3-a115-4aa6-8c3f-0ebaa49c06d1"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3522), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), "Açıklama", "tr", new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3524), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("336fe77c-e00e-49dc-b6c4-930111c495e5"), null, null, null, null },
                    { new Guid("2913d8e4-f6de-41fe-8324-c0e6ba361ab3"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3537), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), "Rol Başlık", "tr", new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3537), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), null, null, null, new Guid("e78c287d-f372-46ed-b782-9e52a4874696"), null },
                    { new Guid("a0d1aa3e-47e6-42b3-a7c3-eb1d021c3070"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3528), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), "Description", "en", new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3529), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("336fe77c-e00e-49dc-b6c4-930111c495e5"), null, null, null, null },
                    { new Guid("e56c523f-bf66-4142-a4a4-1119cae6c7b5"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3541), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), "Rol Grup Başlık", "tr", new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3542), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), null, null, new Guid("40b3c202-f27e-4bcf-a26c-4b415497a8a8"), null, null },
                    { new Guid("ef358264-0f50-4fbd-b889-d51eed99f85d"), new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3532), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), "Başlık", "tr", new DateTime(2023, 3, 29, 14, 32, 21, 246, DateTimeKind.Local).AddTicks(3533), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), new Guid("9fd8c652-73f9-49cd-a7e4-119166963de4"), null, new Guid("336fe77c-e00e-49dc-b6c4-930111c495e5"), null, null, null }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Translations_ScopeId_Title",
                table: "Translations",
                column: "ScopeId_Title");
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

            migrationBuilder.DropTable(
                name: "Scopes");
        }
    }
}

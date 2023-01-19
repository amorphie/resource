using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Enabled = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Enabled = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true)
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
                    Name = table.Column<string>(type: "text", nullable: true),
                    Enabled = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true)
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
                    Name = table.Column<string>(type: "text", nullable: true),
                    Enabled = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true)
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
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Privileges",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "Enabled", "Name", "UpdatedDate", "UpdatedUser" },
                values: new object[] { new Guid("80ae9a58-ede7-4087-9af4-51718cc46378"), new DateTime(2023, 1, 17, 14, 38, 3, 502, DateTimeKind.Local).AddTicks(376), "User1", 1, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "Description", "DisplayName", "Enabled", "Name", "Type", "UpdatedDate", "UpdatedUser", "Url" },
                values: new object[] { new Guid("c9976603-f118-4bc0-bdc9-e7999ee1e1bd"), new DateTime(2023, 1, 17, 14, 38, 3, 502, DateTimeKind.Local).AddTicks(298), "User1", "Get Account List Resource", "Get Account List", 1, "account-list-get", "Get", null, null, "http://localhost:44000/cb.accounts" });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "Enabled", "Name", "UpdatedDate", "UpdatedUser" },
                values: new object[] { new Guid("52506503-d73a-4d0a-b091-12ec6e7fa3c4"), new DateTime(2023, 1, 17, 14, 38, 3, 502, DateTimeKind.Local).AddTicks(345), "User1", 1, "Bireysel", null, null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "Enabled", "Name", "UpdatedDate", "UpdatedUser" },
                values: new object[] { new Guid("80ae9a58-ede7-4087-9af4-51718cc46378"), new DateTime(2023, 1, 17, 14, 38, 3, 502, DateTimeKind.Local).AddTicks(336), "User1", 1, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "ResourceRoles",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "ResourceId", "RoleId" },
                values: new object[] { new Guid("2b3f5958-8d17-4df3-b282-73fab2abc72e"), new DateTime(2023, 1, 17, 14, 38, 3, 502, DateTimeKind.Local).AddTicks(366), "User1", new Guid("c9976603-f118-4bc0-bdc9-e7999ee1e1bd"), new Guid("80ae9a58-ede7-4087-9af4-51718cc46378") });

            migrationBuilder.InsertData(
                table: "RoleGroupRoles",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "RoleGroupId", "RoleId" },
                values: new object[] { new Guid("416aa449-8098-4ea3-a10d-12a5898ea9a2"), new DateTime(2023, 1, 17, 14, 38, 3, 502, DateTimeKind.Local).AddTicks(355), "User1", new Guid("52506503-d73a-4d0a-b091-12ec6e7fa3c4"), new Guid("80ae9a58-ede7-4087-9af4-51718cc46378") });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Privileges");

            migrationBuilder.DropTable(
                name: "ResourceRoles");

            migrationBuilder.DropTable(
                name: "RoleGroupRoles");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "RoleGroups");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

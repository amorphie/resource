using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
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
                name: "ResourceRateLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Period = table.Column<int>(type: "integer", nullable: false),
                    Limit = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true),
                    UpdatedUser = table.Column<string>(type: "text", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_ResourceRateLimits_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
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

            migrationBuilder.CreateTable(
                name: "RolePrivileges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrivilegeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePrivileges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePrivileges_Privileges_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privileges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePrivileges_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Privileges",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "Enabled", "Name", "UpdatedDate", "UpdatedUser" },
                values: new object[] { new Guid("074d8d66-9c5f-42f6-a4d1-ce60d36381ed"), new DateTime(2023, 1, 23, 10, 17, 12, 731, DateTimeKind.Local).AddTicks(7911), "User1", 1, "Write", null, null });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "Description", "DisplayName", "Enabled", "Name", "Type", "UpdatedDate", "UpdatedUser", "Url" },
                values: new object[] { new Guid("f252985e-9029-4538-8f53-5c350505e7a5"), new DateTime(2023, 1, 23, 10, 17, 12, 731, DateTimeKind.Local).AddTicks(7825), "User1", "Get Account List Resource", "Get Account List", 1, "account-list-get", "Get", null, null, "http://localhost:44000/cb.accounts" });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "Enabled", "Name", "UpdatedDate", "UpdatedUser" },
                values: new object[] { new Guid("c3c47d0b-9f80-4724-b302-93b502d06f28"), new DateTime(2023, 1, 23, 10, 17, 12, 731, DateTimeKind.Local).AddTicks(7875), "User1", 1, "Bireysel", null, null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "Enabled", "Name", "UpdatedDate", "UpdatedUser" },
                values: new object[] { new Guid("6c2c3c84-3392-4989-bb24-6bc9fbf4f1ca"), new DateTime(2023, 1, 23, 10, 17, 12, 731, DateTimeKind.Local).AddTicks(7864), "User1", 1, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "ResourceRateLimits",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "Enabled", "Limit", "Period", "ResourceId", "RoleId", "UpdatedDate", "UpdatedUser" },
                values: new object[] { new Guid("1e39aa20-1380-4165-88e3-b9bfde752594"), new DateTime(2023, 1, 23, 10, 17, 12, 731, DateTimeKind.Local).AddTicks(7933), "User1", 1, 10, 60, new Guid("f252985e-9029-4538-8f53-5c350505e7a5"), new Guid("6c2c3c84-3392-4989-bb24-6bc9fbf4f1ca"), null, null });

            migrationBuilder.InsertData(
                table: "ResourceRoles",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "ResourceId", "RoleId" },
                values: new object[] { new Guid("c50fb87c-d72d-4e05-9302-d1fede323df9"), new DateTime(2023, 1, 23, 10, 17, 12, 731, DateTimeKind.Local).AddTicks(7899), "User1", new Guid("f252985e-9029-4538-8f53-5c350505e7a5"), new Guid("6c2c3c84-3392-4989-bb24-6bc9fbf4f1ca") });

            migrationBuilder.InsertData(
                table: "RoleGroupRoles",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "RoleGroupId", "RoleId" },
                values: new object[] { new Guid("2d036728-9936-4892-a406-d5af12a2e596"), new DateTime(2023, 1, 23, 10, 17, 12, 731, DateTimeKind.Local).AddTicks(7885), "User1", new Guid("c3c47d0b-9f80-4724-b302-93b502d06f28"), new Guid("6c2c3c84-3392-4989-bb24-6bc9fbf4f1ca") });

            migrationBuilder.InsertData(
                table: "RolePrivileges",
                columns: new[] { "Id", "CreatedDate", "CreatedUser", "PrivilegeId", "RoleId" },
                values: new object[] { new Guid("0b17e292-bf13-403c-8142-9889ad5fcc4e"), new DateTime(2023, 1, 23, 10, 17, 12, 731, DateTimeKind.Local).AddTicks(7922), "User1", new Guid("074d8d66-9c5f-42f6-a4d1-ce60d36381ed"), new Guid("6c2c3c84-3392-4989-bb24-6bc9fbf4f1ca") });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRateLimits_ResourceId",
                table: "ResourceRateLimits",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceRateLimits_RoleId",
                table: "ResourceRateLimits",
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
                name: "IX_RoleGroupRoles_RoleGroupId",
                table: "RoleGroupRoles",
                column: "RoleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroupRoles_RoleId",
                table: "RoleGroupRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePrivileges_PrivilegeId",
                table: "RolePrivileges",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePrivileges_RoleId",
                table: "RolePrivileges",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceRateLimits");

            migrationBuilder.DropTable(
                name: "ResourceRoles");

            migrationBuilder.DropTable(
                name: "RoleGroupRoles");

            migrationBuilder.DropTable(
                name: "RolePrivileges");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "RoleGroups");

            migrationBuilder.DropTable(
                name: "Privileges");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

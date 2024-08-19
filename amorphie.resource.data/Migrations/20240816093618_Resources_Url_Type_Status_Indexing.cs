using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amorphie.resource.data.Migrations
{
    /// <inheritdoc />
    public partial class Resources_Url_Type_Status_Indexing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS pg_trgm;");
            
            migrationBuilder.CreateIndex(
                name: "IX_Resources_Status_Type",
                table: "Resources",
                columns: new[] { "Status", "Type" });

            migrationBuilder.Sql(@"
                CREATE INDEX IX_Resources_Url 
                ON public.""Resources"" 
                USING gin (
                    lower(""Url"") gin_trgm_ops
                );
            ");
            
            // migrationBuilder.CreateIndex(
            //     name: "IX_Resources_Url",
            //     table: "Resources",
            //     column: "Url")
            //     .Annotation("Npgsql:IndexMethod", "gin")
            //     .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP EXTENSION IF EXISTS pg_trgm;");
            
            migrationBuilder.DropIndex(
                name: "IX_Resources_Status_Type",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_Url",
                table: "Resources");
        }
    }
}

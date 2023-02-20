using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//namespace amorphie.tag.data;

class ResourceDbContextFactory : IDesignTimeDbContextFactory<ResourceDBContext>
{
    public ResourceDBContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ResourceDBContext>();

        var connStr = "Host=localhost:5432;Database=resources;Username=postgres;Password=postgres";
        builder.UseNpgsql(connStr);
        return new ResourceDBContext(builder.Options);
    }
}

public class ResourceDBContext : DbContext
{
    public DbSet<Resource>? Resources { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<RoleGroup>? RoleGroups { get; set; }
    public DbSet<RoleGroupRole>? RoleGroupRoles { get; set; }
    public DbSet<ResourceRole>? ResourceRoles { get; set; }
    public DbSet<Privilege>? Privileges { get; set; }
    public DbSet<ResourceRateLimit>? ResourceRateLimits { get; set; }   

    public DbSet<Translation>? Translations { get; set; }  
    public ResourceDBContext(DbContextOptions options) : base(options) { AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Resource>()
        // .HasKey(r => r.Id);

    //     modelBuilder.Entity<Role>()
    //     .HasKey(r => r.Id);

    //     modelBuilder.Entity<RoleGroup>()
    //     .HasKey(r => r.Id);

    //     modelBuilder.Entity<RoleGroupRole>()
    //    .HasKey(r => r.Id);

    //     modelBuilder.Entity<ResourceRole>()
    //    .HasKey(r => r.Id);

    //     modelBuilder.Entity<Privilege>()
    
    //     .HasKey(r => r.Id);

    //     modelBuilder.Entity<ResourceRateLimit>()
    //     .HasKey(r => r.Id);

    //     modelBuilder.Entity<Translation>()
    //    .HasKey(r => r.Id);

        var ResourceId = Guid.NewGuid();
        var RoleId = Guid.NewGuid();
        var RoleGroupId = Guid.NewGuid();
        var PrivilegeId = Guid.NewGuid();
        var UserId = Guid.NewGuid();

    //     var tags = new string[] { "tag1", "tag2" };

    //       modelBuilder.Entity<Resource>().HasData(
    //         new
    //         {
    //             Id = ResourceId,
    //             RowId = ResourceId,
    //             Type = HttpMethodType.CONNECT,
    //             Url = "urlsample",
    //             Tags = tags,
    //             Status = "A",
    //             CreatedAt = DateTime.Now,
    //             ModifiedAt = DateTime.Now,
    //             CreatedBy = UserId,
    //             ModifiedBy = UserId,
    //             CreatedByBehalfOf = UserId,
    //             ModifiedByBehalfOf = UserId
    //         });

    //         modelBuilder.Entity<Role>().HasData(
    //         new
    //         {
    //             Id = RoleId,
    //             Status = "A",
    //             CreatedAt = DateTime.Now,
    //             ModifiedAt = DateTime.Now,
    //             CreatedBy = UserId,
    //             ModifiedBy = UserId,
    //             CreatedByBehalfOf = UserId,
    //             ModifiedByBehalfOf = UserId
    //         });

    //    modelBuilder.Entity<Translation>().HasData(
    //     new
    //     {
    //         Id = Guid.NewGuid(),
    //         TableName = "Resources",
    //         RowId = ResourceId,
    //         FieldName = "DisplayName",
    //         Text = "Hesaplar",
    //         LanguageCode = "tr",
    //         Order = 1,
    //         Status = "A",
    //         CreatedAt = DateTime.Now,
    //         ModifiedAt = DateTime.Now,
    //         CreatedBy = UserId,
    //         ModifiedBy = UserId,
    //         CreatedByBehalfOf = UserId,
    //         ModifiedByBehalfOf = UserId
    //     },
    //       new
    //     {
    //         Id = Guid.NewGuid(),
    //         TableName = "Resources",
    //         RowId = ResourceId,
    //         FieldName = "Description",
    //         Text = "Açıklama",
    //         LanguageCode = "tr",
    //         Order = 1,
    //         Status = "A",
    //         CreatedAt = DateTime.Now,
    //         ModifiedAt = DateTime.Now,
    //         CreatedBy = UserId,
    //         ModifiedBy = UserId,
    //         CreatedByBehalfOf = UserId,
    //         ModifiedByBehalfOf = UserId
    //     },
    //     new
    //     {
    //         Id = Guid.NewGuid(),
    //         TableName = "Roles",
    //         RowId = RoleId,
    //         FieldName = "RoleName",
    //         Text = "Admin",
    //         LanguageCode = "tr",
    //         Order = 1,
    //         Status = "A",
    //         CreatedAt = DateTime.Now,
    //         ModifiedAt = DateTime.Now,
    //         CreatedBy = UserId,
    //         ModifiedBy = UserId,
    //         CreatedByBehalfOf = UserId,
    //         ModifiedByBehalfOf = UserId
    //     }
    //     );        
    }
}
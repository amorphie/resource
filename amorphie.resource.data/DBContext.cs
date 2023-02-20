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
        modelBuilder.Entity<Resource>()
       .HasKey(r => r.Id);

        modelBuilder.Entity<Role>()
        .HasKey(r => r.Id);

        modelBuilder.Entity<RoleGroup>()
        .HasKey(r => r.Id);

        modelBuilder.Entity<RoleGroupRole>()
       .HasKey(r => r.Id);

        modelBuilder.Entity<ResourceRole>()
       .HasKey(r => r.Id);

        modelBuilder.Entity<Privilege>()
        .HasKey(r => r.Id);

        modelBuilder.Entity<ResourceRateLimit>()
        .HasKey(r => r.Id);

        modelBuilder.Entity<Translation>()
       .HasKey(r => r.Id);

        // Translation Relations
        modelBuilder.Entity<Translation>().Property<Guid?>("ResourceId_DisplayName");
        modelBuilder.Entity<Translation>().Property<Guid?>("ResourceId_Description");

        modelBuilder.Entity<Resource>()
           .HasMany<Translation>(t => t.DisplayNames)
           .WithOne()
           .HasForeignKey("ResourceId_DisplayName");

        modelBuilder.Entity<Resource>()
        .HasMany<Translation>(t => t.Descriptions)
        .WithOne()
        .HasForeignKey("ResourceId_Description");

        var ResourceId = Guid.NewGuid();
        var RoleId = Guid.NewGuid();
        var RoleGroupId = Guid.NewGuid();
        var PrivilegeId = Guid.NewGuid();
        var UserId = Guid.NewGuid();

        var tags = new string[] { "tag1", "tag2" };

        var translation1 = new Translation();
        translation1.Id = Guid.NewGuid();
        translation1.Language = "tr";
        translation1.Label = "açıklama";
        var translation2 = new Translation();
        translation2.Id = Guid.NewGuid();
        translation2.Language = "en";
        translation2.Label = "description";
        var translations = new List<Translation>(new Translation[] { translation1, translation2 });

        modelBuilder.Entity<Resource>().HasData(
          new
          {
              Id = ResourceId,
              RowId = ResourceId,
              Type = HttpMethodType.CONNECT,
              Url = "urlsample",
              Tags = tags,
              Status = "A",
              CreatedAt = DateTime.Now,
              ModifiedAt = DateTime.Now,
              CreatedBy = UserId,
              ModifiedBy = UserId,
              CreatedByBehalfOf = UserId,
              ModifiedByBehalfOf = UserId,
          });

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

        modelBuilder.Entity<Translation>().HasData(
         new
         {
             Id = Guid.NewGuid(),
             Language = "tr",
             Label = "Açıklama",
             Status = "A",
             CreatedAt = DateTime.Now,
             ModifiedAt = DateTime.Now,
             CreatedBy = UserId,
             ModifiedBy = UserId,
             CreatedByBehalfOf = UserId,
             ModifiedByBehalfOf = UserId,
             ResourceId_Description = ResourceId
         },
           new
           {
               Id = Guid.NewGuid(),
               Language = "en",
               Label = "Description",
               Status = "A",
               CreatedAt = DateTime.Now,
               ModifiedAt = DateTime.Now,
               CreatedBy = UserId,
               ModifiedBy = UserId,
               CreatedByBehalfOf = UserId,
               ModifiedByBehalfOf = UserId,
               ResourceId_Description = ResourceId
           },
            new
            {
               Id = Guid.NewGuid(),
               Language = "tr",
               Label = "Başlık",
               Status = "A",
               CreatedAt = DateTime.Now,
               ModifiedAt = DateTime.Now,
               CreatedBy = UserId,
               ModifiedBy = UserId,
               CreatedByBehalfOf = UserId,
               ModifiedByBehalfOf = UserId,
               ResourceId_DisplayName = ResourceId
            }
         );
    }
}
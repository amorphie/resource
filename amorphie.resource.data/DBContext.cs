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
    public DbSet<ResourceLanguage>? ResourceLanguages { get; set; }
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

        modelBuilder.Entity<ResourceLanguage>()
       .HasKey(r => r.Id);

        //  modelBuilder.Entity<TagInRole>()
        // .HasKey(r => r.Id);

        // modelBuilder.Entity<TagInRole>().OwnsOne(p => p.Role);

        var ResourceId = Guid.NewGuid();
        var RoleId = Guid.NewGuid();
        var RoleGroupId = Guid.NewGuid();
        var PrivilegeId = Guid.NewGuid();

        // var langList = new List<MultiLanguageText>();
        // var langText1 = new MultiLanguageText("TR", "baslik");
        // var langText2 = new MultiLanguageText("EN", "title");

        // langList.Add(langText1);
        // langList.Add(langText2);

        var tagInRoles = new List<TagInRole>();
        var tagInRole1 = new TagInRole();
        var tagInRole2 = new TagInRole();

        tagInRole1.Id = Guid.NewGuid();
        tagInRole1.Title = "tag1";

        tagInRole2.Id = Guid.NewGuid();
        tagInRole2.Title = "tag2";

        tagInRoles.Add(tagInRole1);
        tagInRoles.Add(tagInRole2);

        // var langArray = langList.ToArray();

        var tags = new string[] { "tag1", "tag2" };

        //     modelBuilder.Entity<Resource>().HasData(
        //         new
        //         {
        //             Id = ResourceId,
        //             // DisplayName = langArray,
        //             Type = HttpMethodType.GET,
        //             Url = "http://localhost:44000/cb.accounts",
        //             // Description = langArray,
        //             Tags = tags,
        //             Status = "A",
        //             CreatedAt = DateTime.Now,
        //             ModifiedAt = (DateTime?)null,
        //             CreatedBy = Guid.NewGuid(),
        //             ModifiedBy = (Guid?)null,
        //             CreatedByBehalfOf = (Guid?)null,
        //             ModifiedByBehalfOf = (Guid?)null
        //         });

        //     modelBuilder.Entity<Role>().HasData(
        //     new Role
        //     {
        //         Id = RoleId,
        //         // TagInRoles = tagInRoles,
        //         Status = "A",
        //         CreatedAt = DateTime.Now,
        //         ModifiedAt = (DateTime?)null,
        //         CreatedBy = Guid.NewGuid(),
        //         ModifiedBy = (Guid?)null,
        //         CreatedByBehalfOf = (Guid?)null,
        //         ModifiedByBehalfOf = (Guid?)null
        //     });

        //     modelBuilder.Entity<TagInRole>().HasData(
        //    new TagInRole
        //    {
        //        Id = Guid.NewGuid(),
        //        RoleId = RoleId,
        //        Title = "Title"
        //    });

        //     modelBuilder.Entity<RoleGroup>().HasData(
        //     new
        //     {
        //         Id = RoleGroupId,
        //         // Title = langArray,
        //         Tags = tags,
        //         Status = "A",
        //         CreatedAt = DateTime.Now,
        //         ModifiedAt = (DateTime?)null,
        //         CreatedBy = Guid.NewGuid(),
        //         ModifiedBy = (Guid?)null,
        //         CreatedByBehalfOf = (Guid?)null,
        //         ModifiedByBehalfOf = (Guid?)null
        //     });

        // modelBuilder.Entity<RoleGroupRole>().HasData(
        // new
        // {
        //     Id = Guid.NewGuid(),
        //     RoleGroupId = RoleGroupId,
        //     RoleId = RoleId,
        //     Status = "A",
        //     CreatedAt = DateTime.Now,
        //     ModifiedAt = (DateTime?)null,
        //     CreatedBy = Guid.NewGuid(),
        //     ModifiedBy = (Guid?)null,
        //     CreatedByBehalfOf = (Guid?)null,
        //     ModifiedByBehalfOf = (Guid?)null
        // });

        // modelBuilder.Entity<ResourceRole>().HasData(
        // new
        // {
        //     Id = Guid.NewGuid(),
        //     ResourceId = ResourceId,
        //     RoleId = RoleId,
        //     Status = "A",
        //     CreatedAt = DateTime.Now,
        //     ModifiedAt = (DateTime?)null,
        //     CreatedBy = Guid.NewGuid(),
        //     ModifiedBy = (Guid?)null,
        //     CreatedByBehalfOf = (Guid?)null,
        //     ModifiedByBehalfOf = (Guid?)null
        // });

        // modelBuilder.Entity<Privilege>().HasData(
        // new
        // {
        //     Id = PrivilegeId,
        //     ResourceId = ResourceId,
        //     Url = "Url",
        //     Ttl = 1,
        //     Status = "A",
        //     CreatedAt = DateTime.Now,
        //     ModifiedAt = (DateTime?)null,
        //     CreatedBy = Guid.NewGuid(),
        //     ModifiedBy = (Guid?)null,
        //     CreatedByBehalfOf = (Guid?)null,
        //     ModifiedByBehalfOf = (Guid?)null
        // });

        // modelBuilder.Entity<ResourceRateLimit>().HasData(
        // new
        // {
        //     Id = Guid.NewGuid(),
        //     ResourceId = ResourceId,
        //     Scope = "Scope",
        //     Condition = "Condition",
        //     Cron = "Cron",
        //     Limit = 10,
        //     Status = "A",
        //     CreatedAt = DateTime.Now,
        //     ModifiedAt = (DateTime?)null,
        //     CreatedBy = Guid.NewGuid(),
        //     ModifiedBy = (Guid?)null,
        //     CreatedByBehalfOf = (Guid?)null,
        //     ModifiedByBehalfOf = (Guid?)null
        // });
    }
}
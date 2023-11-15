using amorphie.core.Base;
using amorphie.core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public DbSet<Scope>? Scopes { get; set; }
    public DbSet<ResourcePrivilege>? ResourcePrivileges { get; set; }
    public DbSet<ResponseTransformation>? ResponseTransformations { get; set; }
    public DbSet<ResponseTransformationMessage>? ResponseTransformationMessages { get; set; }
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

        modelBuilder.Entity<Scope>()
        .HasKey(r => r.Id);

        modelBuilder.Entity<Translation>()
       .HasKey(r => r.Id);

        modelBuilder.Entity<ResourcePrivilege>()
        .HasKey(r => r.Id);

        modelBuilder.Entity<ResponseTransformation>()
       .HasKey(r => r.Id);

        modelBuilder.Entity<ResponseTransformationMessage>()
      .HasKey(r => r.Id);

        // Translation Relations
        modelBuilder.Entity<Translation>().Property<Guid?>("ResourceId_DisplayName");
        modelBuilder.Entity<Translation>().Property<Guid?>("ResourceId_Description");

        modelBuilder.Entity<Translation>().Property<Guid?>("RoleId_Title");

        modelBuilder.Entity<Translation>().Property<Guid?>("RoleGroupId_Title");

        modelBuilder.Entity<Translation>().Property<Guid?>("ScopeId_Title");

        modelBuilder.Entity<Resource>()
           .HasMany<Translation>(t => t.DisplayNames)
           .WithOne()
           .HasForeignKey("ResourceId_DisplayName");

        modelBuilder.Entity<Resource>()
        .HasMany<Translation>(t => t.Descriptions)
        .WithOne()
        .HasForeignKey("ResourceId_Description");

        modelBuilder.Entity<Role>()
        .HasMany<Translation>(t => t.Titles)
        .WithOne()
        .HasForeignKey("RoleId_Title");

        modelBuilder.Entity<RoleGroup>()
        .HasMany<Translation>(t => t.Titles)
        .WithOne()
        .HasForeignKey("RoleGroupId_Title");

        modelBuilder.Entity<Scope>()
        .HasMany<Translation>(t => t.Titles)
        .WithOne()
        .HasForeignKey("ScopeId_Title");
    }
}
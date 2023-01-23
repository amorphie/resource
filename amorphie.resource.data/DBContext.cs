﻿using Microsoft.EntityFrameworkCore;
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
    public DbSet<RolePrivilege>? RolePrivileges { get; set; }
     public DbSet<ResourceRateLimit>? ResourceRateLimits { get; set; }
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

        modelBuilder.Entity<RolePrivilege>()
        .HasKey(r => r.Id);

        modelBuilder.Entity<ResourceRateLimit>()
        .HasKey(r => r.Id);

        var ResourceId = Guid.NewGuid();
        var RoleId = Guid.NewGuid();
        var RoleGroupId = Guid.NewGuid();
        var PrivilegeId = Guid.NewGuid();

        modelBuilder.Entity<Resource>().HasData(
            new
            {
                Id = ResourceId,
                Name = "account-list-get",
                DisplayName = "Get Account List",
                Type = "Get",
                Url = "http://localhost:44000/cb.accounts",
                Description = "Get Account List Resource",
                Enabled = 1,
                CreatedDate = DateTime.Now,
                UpdatedDate = (DateTime?)null,
                CreatedUser = "User1",
                UpdatedUser = (string?)null
            });

        modelBuilder.Entity<Role>().HasData(
        new
        {
            Id = RoleId,
            Name = "Admin",
            Enabled = 1,
            CreatedDate = DateTime.Now,
            UpdatedDate = (DateTime?)null,
            CreatedUser = "User1",
            UpdatedUser = (string?)null
        });

        modelBuilder.Entity<RoleGroup>().HasData(
        new
        {
            Id = RoleGroupId,
            Name = "Bireysel",
            Enabled = 1,
            CreatedDate = DateTime.Now,
            UpdatedDate = (DateTime?)null,
            CreatedUser = "User1",
            UpdatedUser = (string?)null
        });

        modelBuilder.Entity<RoleGroupRole>().HasData(
        new
        {
            Id = Guid.NewGuid(),
            RoleGroupId = RoleGroupId,
            RoleId = RoleId,
            CreatedDate = DateTime.Now,
            CreatedUser = "User1"
        });

        modelBuilder.Entity<ResourceRole>().HasData(
        new
        {
            Id = Guid.NewGuid(),
            ResourceId = ResourceId,
            RoleId = RoleId,
            CreatedDate = DateTime.Now,
            CreatedUser = "User1"
        });

        modelBuilder.Entity<Privilege>().HasData(
        new
        {
            Id = PrivilegeId,
            Name = "Write",
            Enabled = 1,
            CreatedDate = DateTime.Now,
            UpdatedDate = (DateTime?)null,
            CreatedUser = "User1",
            UpdatedUser = (string?)null
        });

        modelBuilder.Entity<RolePrivilege>().HasData(
        new
        {
            Id = Guid.NewGuid(),
            RoleId = RoleId,
            PrivilegeId = PrivilegeId,
            CreatedDate = DateTime.Now,
            CreatedUser = "User1"
        });

        modelBuilder.Entity<ResourceRateLimit>().HasData(
        new
        {
            Id = Guid.NewGuid(),
            ResourceId = ResourceId,
            RoleId = RoleId,
            Period = 60,
            Limit = 10,
            Enabled = 1,
            CreatedDate = DateTime.Now,
            CreatedUser = "User1",
            UpdatedDate = (DateTime?)null,
            UpdatedUser = (string?)null
        });
    }
}
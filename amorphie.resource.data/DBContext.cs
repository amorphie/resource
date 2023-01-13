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
    public ResourceDBContext(DbContextOptions options) : base(options) { AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>()
            .HasKey(r => r.Id);

         modelBuilder.Entity<Role>()
            .HasKey(r => r.Id);    

        modelBuilder.Entity<Resource>().HasData(
            new
            {
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
                Name = "Admin",
                Enabled = 1,
                CreatedDate = DateTime.Now,
                UpdatedDate = (DateTime?)null,
                CreatedUser = "User1",
                UpdatedUser = (string?)null
            });
    }

}

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
    public ResourceDBContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>()
            .HasKey(r => r.Name);
       
        modelBuilder.Entity<Resource>().HasData(new { Name = "account-list-get", DisplayName="Get Account List", Url = "http://localhost:44000/cb.accounts" });
}

}

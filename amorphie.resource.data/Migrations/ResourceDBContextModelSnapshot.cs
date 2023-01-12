﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace amorphie.resource.data.Migrations
{
    [DbContext(typeof(ResourceDBContext))]
    partial class ResourceDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Resource", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<byte>("Enabled")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("date");

                    b.Property<string>("CreatedUser")
                        .HasColumnType("text");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Resources");

                    b.HasData(
                        new
                        {
                            Id = "aa",
                            Name = "account-list-get",
                            DisplayName = "Get Account List",
                            Url = "http://localhost:44000/cb.accounts",
                            Description = "Get Account List Resource",
                            Enabled = 1,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = (DateTime?)null,
                            CreatedUser = "User1",
                            UpdatedUser = (string)null
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

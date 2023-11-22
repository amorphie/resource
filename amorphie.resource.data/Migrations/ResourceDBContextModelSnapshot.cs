﻿// <auto-generated />
using System;
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
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Privilege", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Privileges");
                });

            modelBuilder.Entity("Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string[]>("Tags")
                        .HasColumnType("text[]");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("ResourceGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string[]>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("ResourceGroups");
                });

            modelBuilder.Entity("ResourceGroupResource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResourceGroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ResourceGroupResources");
                });

            modelBuilder.Entity("ResourceGroupRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResourceGroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ResourceGroupRoles");
                });

            modelBuilder.Entity("ResourcePrivilege", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<int?>("Priority")
                        .HasColumnType("integer");

                    b.Property<Guid>("PrivilegeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int?>("Ttl")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PrivilegeId");

                    b.ToTable("ResourcePrivileges");
                });

            modelBuilder.Entity("ResourceRateLimit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Condition")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Cron")
                        .HasColumnType("text");

                    b.Property<int?>("Limit")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Scope")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ResourceRateLimits");
                });

            modelBuilder.Entity("ResponseTransformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string[]>("Audience")
                        .HasColumnType("text[]");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayMode")
                        .HasColumnType("text");

                    b.Property<string>("Filter")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("ResponseCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ResponseTransformations");
                });

            modelBuilder.Entity("ResponseTransformationMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResponseTransformationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Subtitle")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ResponseTransformationId");

                    b.ToTable("ResponseTransformationMessages");
                });

            modelBuilder.Entity("Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string[]>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("RoleGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string[]>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("RoleGroups");
                });

            modelBuilder.Entity("RoleGroupRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleGroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RoleGroupRoles");
                });

            modelBuilder.Entity("Scope", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Reference")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleGroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string[]>("Tags")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("Scopes");
                });

            modelBuilder.Entity("amorphie.core.Base.Translation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModifiedByBehalfOf")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ResourceGroupId_Title")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ResourceId_Description")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ResourceId_DisplayName")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RoleGroupId_Title")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RoleId_Title")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ScopeId_Title")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ResourceGroupId_Title");

                    b.HasIndex("ResourceId_Description");

                    b.HasIndex("ResourceId_DisplayName");

                    b.HasIndex("RoleGroupId_Title");

                    b.HasIndex("RoleId_Title");

                    b.HasIndex("ScopeId_Title");

                    b.ToTable("Translations");
                });

            modelBuilder.Entity("ResourcePrivilege", b =>
                {
                    b.HasOne("Privilege", "Privilege")
                        .WithMany()
                        .HasForeignKey("PrivilegeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Privilege");
                });

            modelBuilder.Entity("ResponseTransformationMessage", b =>
                {
                    b.HasOne("ResponseTransformation", null)
                        .WithMany("ResponseTransformationMessages")
                        .HasForeignKey("ResponseTransformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("amorphie.core.Base.Translation", b =>
                {
                    b.HasOne("ResourceGroup", null)
                        .WithMany("Titles")
                        .HasForeignKey("ResourceGroupId_Title");

                    b.HasOne("Resource", null)
                        .WithMany("Descriptions")
                        .HasForeignKey("ResourceId_Description");

                    b.HasOne("Resource", null)
                        .WithMany("DisplayNames")
                        .HasForeignKey("ResourceId_DisplayName");

                    b.HasOne("RoleGroup", null)
                        .WithMany("Titles")
                        .HasForeignKey("RoleGroupId_Title");

                    b.HasOne("Role", null)
                        .WithMany("Titles")
                        .HasForeignKey("RoleId_Title");

                    b.HasOne("Scope", null)
                        .WithMany("Titles")
                        .HasForeignKey("ScopeId_Title");
                });

            modelBuilder.Entity("Resource", b =>
                {
                    b.Navigation("Descriptions");

                    b.Navigation("DisplayNames");
                });

            modelBuilder.Entity("ResourceGroup", b =>
                {
                    b.Navigation("Titles");
                });

            modelBuilder.Entity("ResponseTransformation", b =>
                {
                    b.Navigation("ResponseTransformationMessages");
                });

            modelBuilder.Entity("Role", b =>
                {
                    b.Navigation("Titles");
                });

            modelBuilder.Entity("RoleGroup", b =>
                {
                    b.Navigation("Titles");
                });

            modelBuilder.Entity("Scope", b =>
                {
                    b.Navigation("Titles");
                });
#pragma warning restore 612, 618
        }
    }
}

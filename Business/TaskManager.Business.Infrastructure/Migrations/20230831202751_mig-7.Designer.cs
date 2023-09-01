﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskManager.Business.Infrastructure.Context;

#nullable disable

namespace TaskManager.Business.Infrastructure.Migrations
{
    [DbContext(typeof(BusinessDbContext))]
    [Migration("20230831202751_mig-7")]
    partial class mig7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskManager.Business.Domain.Entities.Column", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CreatedByUser")
                        .HasColumnType("uuid")
                        .HasColumnName("createdbyuser");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createddate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer")
                        .HasColumnName("projectid");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean")
                        .HasColumnName("status");

                    b.Property<Guid>("UpdatedByUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updatedbyuser");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateddate");

                    b.HasKey("Id")
                        .HasName("pk_columns");

                    b.ToTable("columns", (string)null);
                });

            modelBuilder.Entity("TaskManager.Business.Domain.Entities.Comments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<Guid>("CreatedByUser")
                        .HasColumnType("uuid")
                        .HasColumnName("createdbyuser");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createddate");

                    b.Property<bool>("Rewrite")
                        .HasColumnType("boolean")
                        .HasColumnName("rewrite");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean")
                        .HasColumnName("status");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer")
                        .HasColumnName("taskid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateddate");

                    b.HasKey("Id")
                        .HasName("pk_comments");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("TaskManager.Business.Domain.Entities.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CreatedByUser")
                        .HasColumnType("uuid")
                        .HasColumnName("createdbyuser");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createddate");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("discriminator");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.Property<string>("Storage")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("storage");

                    b.Property<Guid>("UpdatedByUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updatedbyuser");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateddate");

                    b.HasKey("Id")
                        .HasName("pk_files");

                    b.ToTable("files", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("File");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TaskManager.Business.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CreatedByUser")
                        .HasColumnType("uuid")
                        .HasColumnName("createdbyuser");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createddate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean")
                        .HasColumnName("status");

                    b.Property<Guid>("UpdatedByUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updatedbyuser");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateddate");

                    b.HasKey("Id")
                        .HasName("pk_projects");

                    b.ToTable("projects", (string)null);
                });

            modelBuilder.Entity("TaskManager.Business.Domain.Entities.ProjectUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer")
                        .HasColumnName("projectid");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean")
                        .HasColumnName("status");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("Id")
                        .HasName("pk_projectusers");

                    b.ToTable("projectusers", (string)null);
                });

            modelBuilder.Entity("TaskManager.Business.Domain.Entities.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AssigneeId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("assigneeid");

                    b.Property<int>("ColumnId")
                        .HasColumnType("integer")
                        .HasColumnName("columnid");

                    b.Property<Guid>("CreatedByUser")
                        .HasColumnType("uuid")
                        .HasColumnName("createdbyuser");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createddate");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("enddate");

                    b.Property<int?>("Label")
                        .HasColumnType("integer")
                        .HasColumnName("label");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Priority")
                        .HasColumnType("integer")
                        .HasColumnName("priority");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer")
                        .HasColumnName("projectid");

                    b.Property<string>("ReporterId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("reporterid");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean")
                        .HasColumnName("status");

                    b.Property<Guid>("UpdatedByUser")
                        .HasColumnType("uuid")
                        .HasColumnName("updatedbyuser");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateddate");

                    b.Property<DateTime>("UserUpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("userupdateddate");

                    b.HasKey("Id")
                        .HasName("pk_tasks");

                    b.ToTable("tasks", (string)null);
                });

            modelBuilder.Entity("TaskManager.Business.Domain.Entities.ProjectFile", b =>
                {
                    b.HasBaseType("TaskManager.Business.Domain.Entities.File");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer")
                        .HasColumnName("projectid");

                    b.HasDiscriminator().HasValue("ProjectFile");
                });

            modelBuilder.Entity("TaskManager.Business.Domain.Entities.TaskFile", b =>
                {
                    b.HasBaseType("TaskManager.Business.Domain.Entities.File");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer")
                        .HasColumnName("taskid");

                    b.HasDiscriminator().HasValue("TaskFile");
                });

            modelBuilder.Entity("TaskManager.Business.Domain.Entities.UserFile", b =>
                {
                    b.HasBaseType("TaskManager.Business.Domain.Entities.File");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasDiscriminator().HasValue("UserFile");
                });
#pragma warning restore 612, 618
        }
    }
}

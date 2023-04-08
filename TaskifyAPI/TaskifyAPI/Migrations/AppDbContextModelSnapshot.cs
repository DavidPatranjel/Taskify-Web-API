﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskifyAPI.Data;

#nullable disable

namespace TaskifyAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskifyAPI.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TaskifyAPI.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TaskifyAPI.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskifyAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TaskifyAPI.Models.UserProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id", "UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProjects");
                });

            modelBuilder.Entity("TaskifyAPI.Models.Comment", b =>
                {
                    b.HasOne("TaskifyAPI.Models.Task", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId");

                    b.HasOne("TaskifyAPI.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskifyAPI.Models.Project", b =>
                {
                    b.HasOne("TaskifyAPI.Models.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskifyAPI.Models.Task", b =>
                {
                    b.HasOne("TaskifyAPI.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId");

                    b.HasOne("TaskifyAPI.Models.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskifyAPI.Models.UserProject", b =>
                {
                    b.HasOne("TaskifyAPI.Models.Project", "Project")
                        .WithMany("UserProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskifyAPI.Models.User", "User")
                        .WithMany("UserProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskifyAPI.Models.Project", b =>
                {
                    b.Navigation("Tasks");

                    b.Navigation("UserProjects");
                });

            modelBuilder.Entity("TaskifyAPI.Models.Task", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("TaskifyAPI.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Projects");

                    b.Navigation("Tasks");

                    b.Navigation("UserProjects");
                });
#pragma warning restore 612, 618
        }
    }
}

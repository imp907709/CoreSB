﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mvccoresb.Infrastructure.EF;

namespace mvccoresb.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20190612234003_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("mvccoresb.Domain.TestModels.BlogEF", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Rating");

                    b.Property<string>("Url");

                    b.HasKey("BlogId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.BlogImage", b =>
                {
                    b.Property<int>("BlogImageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId");

                    b.Property<string>("Caption");

                    b.Property<byte[]>("Image");

                    b.HasKey("BlogImageId");

                    b.HasIndex("BlogId")
                        .IsUnique();

                    b.ToTable("BlogImage");
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.InstructorEF", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("QualityGrade");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.PersonEF", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PersonID");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsConcurrencyToken()
                        .IsRequired();

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PersonEF");
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.PostEF", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("AuthorId");

                    b.Property<int>("BlogId");

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("PostId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.PostTagEF", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<string>("TagId");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTagEF");
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.ServiceType", b =>
                {
                    b.Property<Guid>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ServiceID");

                    b.HasKey("ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceType");
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.TagEF", b =>
                {
                    b.Property<string>("TagId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("TagId");

                    b.ToTable("TagEF");
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.StudentEF", b =>
                {
                    b.HasBaseType("mvccoresb.Domain.TestModels.PersonEF");

                    b.Property<DateTime>("EnrollmentDate");

                    b.HasDiscriminator().HasValue("StudentEF");
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.BlogImage", b =>
                {
                    b.HasOne("mvccoresb.Domain.TestModels.BlogEF", "Blog")
                        .WithOne("BlogImage")
                        .HasForeignKey("mvccoresb.Domain.TestModels.BlogImage", "BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.PostEF", b =>
                {
                    b.HasOne("mvccoresb.Domain.TestModels.PersonEF", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("mvccoresb.Domain.TestModels.BlogEF", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("mvccoresb.Domain.TestModels.PostTagEF", b =>
                {
                    b.HasOne("mvccoresb.Domain.TestModels.PostEF", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("mvccoresb.Domain.TestModels.TagEF", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
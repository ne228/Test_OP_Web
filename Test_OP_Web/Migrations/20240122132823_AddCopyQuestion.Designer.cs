﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test_OP_Web.Data.Options;

#nullable disable

namespace Test_OP_Web.Migrations
{
    [DbContext(typeof(OptionContext))]
    [Migration("20240122132823_AddCopyQuestion")]
    partial class AddCopyQuestion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.Anwser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Right")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<int?>("questionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("questionId");

                    b.ToTable("Anwser");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.CopyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NoVariant")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumQ")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumVar")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuestionString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CopyQuestion");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumVar")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NoVariant")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumQ")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumVar")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OptionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuestionString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OptionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("NumVar")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeFinsih")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserAxeId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Сompleted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserAxeId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.SessionAnwser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Enter")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Right")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SessionQuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SessionQuestionId");

                    b.ToTable("SessionAnwsers");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.SessionQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Blocked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Correctly")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Right")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SessionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SessionId");

                    b.ToTable("SessionQuestions");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Confirm")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserAxeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserAxeId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Test_OP_Web.Data.UserAxe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Test_OP_Web.Services.PersonStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("AveragePercent")
                        .HasColumnType("REAL");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("NumVar")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("PersonStat");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Test_OP_Web.Data.UserAxe", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Test_OP_Web.Data.UserAxe", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test_OP_Web.Data.UserAxe", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Test_OP_Web.Data.UserAxe", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.Anwser", b =>
                {
                    b.HasOne("Test_OP_Web.Data.Options.Question", "question")
                        .WithMany("Anwsers")
                        .HasForeignKey("questionId");

                    b.Navigation("question");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.Question", b =>
                {
                    b.HasOne("Test_OP_Web.Data.Options.Option", null)
                        .WithMany("Questions")
                        .HasForeignKey("OptionId");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.Session", b =>
                {
                    b.HasOne("Test_OP_Web.Data.UserAxe", "UserAxe")
                        .WithMany("Sessions")
                        .HasForeignKey("UserAxeId");

                    b.Navigation("UserAxe");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.SessionAnwser", b =>
                {
                    b.HasOne("Test_OP_Web.Data.Options.CopyQuestion", "SessionQuestion")
                        .WithMany("Anwsers")
                        .HasForeignKey("SessionQuestionId");

                    b.Navigation("SessionQuestion");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.SessionQuestion", b =>
                {
                    b.HasOne("Test_OP_Web.Data.Options.CopyQuestion", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.HasOne("Test_OP_Web.Data.Options.Session", null)
                        .WithMany("SessionQuestions")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Report", b =>
                {
                    b.HasOne("Test_OP_Web.Data.Options.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test_OP_Web.Data.UserAxe", "UserAxe")
                        .WithMany()
                        .HasForeignKey("UserAxeId");

                    b.Navigation("Question");

                    b.Navigation("UserAxe");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.CopyQuestion", b =>
                {
                    b.Navigation("Anwsers");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.Option", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.Question", b =>
                {
                    b.Navigation("Anwsers");
                });

            modelBuilder.Entity("Test_OP_Web.Data.Options.Session", b =>
                {
                    b.Navigation("SessionQuestions");
                });

            modelBuilder.Entity("Test_OP_Web.Data.UserAxe", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}

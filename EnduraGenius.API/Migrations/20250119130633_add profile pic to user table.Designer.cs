﻿// <auto-generated />
using System;
using EnduraGenius.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnduraGenius.API.Migrations
{
    [DbContext(typeof(EnduraGeniusDBContext))]
    [Migration("20250119130633_add profile pic to user table")]
    partial class addprofilepictousertable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.Inbody", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("BFP")
                        .HasColumnType("real");

                    b.Property<float>("BMI")
                        .HasColumnType("real");

                    b.Property<float>("BMR")
                        .HasColumnType("real");

                    b.Property<int>("CaloricNeed")
                        .HasColumnType("int");

                    b.Property<int>("DailyProtenNeedInGrams")
                        .HasColumnType("int");

                    b.Property<float>("FFM")
                        .HasColumnType("real");

                    b.Property<float>("IdealBodyWeight")
                        .HasColumnType("real");

                    b.Property<float>("LBM")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TBW")
                        .HasColumnType("real");

                    b.Property<float>("WaterIntake")
                        .HasColumnType("real");

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("Inbodies");
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.Muscle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Muscles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("df475ba8-6be7-42b6-871f-6a29cd4c91d8"),
                            Description = "Shoulder muscle composed of three heads: anterior, lateral, and posterior.",
                            Name = "Deltoid (shoulder-Top)"
                        },
                        new
                        {
                            Id = new Guid("d729e9f8-3384-4873-83bc-74acdccacabe"),
                            Description = "The front portion of the deltoid muscle.",
                            Name = "Anterior Deltoid (shoulder-Front)"
                        },
                        new
                        {
                            Id = new Guid("c4ac8999-40a4-40c8-9518-42be7e2d0744"),
                            Description = "The back portion of the deltoid muscle.",
                            Name = "Posterior Deltoid (shoulder-Back)"
                        },
                        new
                        {
                            Id = new Guid("8909c9d4-2f45-4a2f-90c8-d6f475820a2f"),
                            Description = "Upper part of the pectoralis major muscle.",
                            Name = "Clavicular Head (Upper Chest)"
                        },
                        new
                        {
                            Id = new Guid("d8738439-5832-4764-8290-20ebe48d50dc"),
                            Description = "Lower part of the pectoralis major muscle.",
                            Name = "Sternal Head (Lower Chest)"
                        },
                        new
                        {
                            Id = new Guid("a2528d27-0dfe-48c8-9a18-7887dd9743ef"),
                            Description = "Large muscles on the sides of the back.",
                            Name = "Latissimus Dorsi (Lats)"
                        },
                        new
                        {
                            Id = new Guid("405069d0-6e66-4d50-883d-e5b29a403a84"),
                            Description = "Muscle extending from the neck to the mid-back.",
                            Name = "Trapezius (Middle Back)"
                        },
                        new
                        {
                            Id = new Guid("b61c068e-8e8b-4756-a05c-482d0ec70f9e"),
                            Description = "Muscles running along the spine, supporting posture.",
                            Name = "Erector Spinae (Lower Back)"
                        },
                        new
                        {
                            Id = new Guid("b391d0e8-442e-414a-85cc-9445b3bd11be"),
                            Description = "Four muscles located on the front of the thigh.",
                            Name = "Quadriceps (Front of Leg)"
                        },
                        new
                        {
                            Id = new Guid("2c6891f3-98d9-4bfe-bf90-a7e5b6b1caf8"),
                            Description = "Three muscles located on the back of the thigh.",
                            Name = "Hamstrings (Back of Leg)"
                        },
                        new
                        {
                            Id = new Guid("619fe532-8226-4d84-9a61-b64bcdef8084"),
                            Description = "Largest muscle of the gluteal group, forming the buttocks.",
                            Name = "Gluteus Maximus (Butt)"
                        },
                        new
                        {
                            Id = new Guid("e56d075d-e1db-49d0-be1c-a925b80e87a6"),
                            Description = "The larger calf muscle, forming the bulge of the calf.",
                            Name = "Gastrocnemius (Calves)"
                        },
                        new
                        {
                            Id = new Guid("cb5afad8-c1ae-44e5-ac7b-aa992f5c80a7"),
                            Description = "Muscle underneath the gastrocnemius, part of the calf.",
                            Name = "Soleus (Calves)"
                        },
                        new
                        {
                            Id = new Guid("7be040bd-2759-4b13-a049-8cde648dfd75"),
                            Description = "Muscles of the forearm responsible for wrist and finger movements.",
                            Name = "Forearms"
                        },
                        new
                        {
                            Id = new Guid("6ed76f26-52f2-4350-b0b3-103e370f0835"),
                            Description = "Front upper arm muscle responsible for elbow flexion.",
                            Name = "Biceps"
                        },
                        new
                        {
                            Id = new Guid("f19a5492-81ea-4ebc-8b03-1198e8440a58"),
                            Description = "Back upper arm muscle responsible for elbow extension.",
                            Name = "Triceps"
                        });
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.Plan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descreption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlanCreatedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlanCreatedBy");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.PlanWorkout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DayNumber")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reps")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("PlanWorkouts");
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.PlansUsers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PlanOrder")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.HasIndex("UserId");

                    b.ToTable("PlansUsers");
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMale")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TallInCm")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<float>("WeightInKg")
                        .HasColumnType("real");

                    b.Property<bool>("isPublic")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.UserWorkout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("LastWeight")
                        .HasColumnType("real");

                    b.Property<float>("MaxWeight")
                        .HasColumnType("real");

                    b.Property<int>("TimesPerformed")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("UserWorkouts");
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.Workout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCertified")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MainMuscleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SecondaryMuscleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WorkoutCreatedBy")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MainMuscleId");

                    b.HasIndex("SecondaryMuscleId");

                    b.HasIndex("WorkoutCreatedBy");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f02a0229-1fa7-4c12-8985-2436ff1b495b",
                            ConcurrencyStamp = "f02a0229-1fa7-4c12-8985-2436ff1b495b",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "de6594c7-64c1-4d22-bdfc-4de7eda3628c",
                            ConcurrencyStamp = "de6594c7-64c1-4d22-bdfc-4de7eda3628c",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.Inbody", b =>
                {
                    b.HasOne("EnduraGenius.API.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.Plan", b =>
                {
                    b.HasOne("EnduraGenius.API.Models.Domain.User", "planCreator")
                        .WithMany()
                        .HasForeignKey("PlanCreatedBy")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("planCreator");
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.PlanWorkout", b =>
                {
                    b.HasOne("EnduraGenius.API.Models.Domain.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnduraGenius.API.Models.Domain.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.PlansUsers", b =>
                {
                    b.HasOne("EnduraGenius.API.Models.Domain.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnduraGenius.API.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.UserWorkout", b =>
                {
                    b.HasOne("EnduraGenius.API.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnduraGenius.API.Models.Domain.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("EnduraGenius.API.Models.Domain.Workout", b =>
                {
                    b.HasOne("EnduraGenius.API.Models.Domain.Muscle", "MainMuscle")
                        .WithMany()
                        .HasForeignKey("MainMuscleId");

                    b.HasOne("EnduraGenius.API.Models.Domain.Muscle", "SecondaryMuscle")
                        .WithMany()
                        .HasForeignKey("SecondaryMuscleId");

                    b.HasOne("EnduraGenius.API.Models.Domain.User", "WorkoutCreator")
                        .WithMany()
                        .HasForeignKey("WorkoutCreatedBy")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("MainMuscle");

                    b.Navigation("SecondaryMuscle");

                    b.Navigation("WorkoutCreator");
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
                    b.HasOne("EnduraGenius.API.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EnduraGenius.API.Models.Domain.User", null)
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

                    b.HasOne("EnduraGenius.API.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EnduraGenius.API.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

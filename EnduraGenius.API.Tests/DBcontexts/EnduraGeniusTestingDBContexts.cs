using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using FluentAssertions.Equivalency;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Tests.DBcontexts
{
    public class EnduraGeniusTestingDBContexts
    {
        //muscle data
        //plans data
        //roles data
        //users data
        //userroles data
        //workouts data
        //inbody data
        //plansusers data
        //workoutplans data
        //workoutusers data
        public async Task<EnduraGeniusDBContext> GetDBContextWithData()
        {
            var options = new DbContextOptionsBuilder<EnduraGeniusDBContext>()
                .UseInMemoryDatabase(databaseName: "TestingEnduraGeniusDB")
                .Options;
            var dbContext = new EnduraGeniusDBContext(options);
            dbContext.Database.EnsureCreated();
            if (await dbContext.Muscles.CountAsync() <= 0)
            {
                dbContext.Muscles.AddRange(new List<Muscle>() {
                new Muscle { Id = Guid.Parse("df475ba8-6be7-42b6-871f-6a29cd4c91d8"), Name = "Deltoid (shoulder-Top)", Description = "Shoulder muscle composed of three heads: anterior, lateral, and posterior." },
                new Muscle { Id = Guid.Parse("d729e9f8-3384-4873-83bc-74acdccacabe"), Name = "Anterior Deltoid (shoulder-Front)", Description = "The front portion of the deltoid muscle." },
                new Muscle { Id = Guid.Parse("c4ac8999-40a4-40c8-9518-42be7e2d0744"), Name = "Posterior Deltoid (shoulder-Back)", Description = "The back portion of the deltoid muscle." },
                new Muscle { Id = Guid.Parse("8909c9d4-2f45-4a2f-90c8-d6f475820a2f"), Name = "Clavicular Head (Upper Chest)", Description = "Upper part of the pectoralis major muscle." },
                new Muscle { Id = Guid.Parse("d8738439-5832-4764-8290-20ebe48d50dc"), Name = "Sternal Head (Lower Chest)", Description = "Lower part of the pectoralis major muscle." },
                new Muscle { Id = Guid.Parse("a2528d27-0dfe-48c8-9a18-7887dd9743ef"), Name = "Latissimus Dorsi (Lats)", Description = "Large muscles on the sides of the back." },
                new Muscle { Id = Guid.Parse("405069d0-6e66-4d50-883d-e5b29a403a84"), Name = "Trapezius (Middle Back)", Description = "Muscle extending from the neck to the mid-back." },
                new Muscle { Id = Guid.Parse("b61c068e-8e8b-4756-a05c-482d0ec70f9e"), Name = "Erector Spinae (Lower Back)", Description = "Muscles running along the spine, supporting posture." },
                new Muscle { Id = Guid.Parse("b391d0e8-442e-414a-85cc-9445b3bd11be"), Name = "Quadriceps (Front of Leg)", Description = "Four muscles located on the front of the thigh." },
                new Muscle { Id = Guid.Parse("2c6891f3-98d9-4bfe-bf90-a7e5b6b1caf8"), Name = "Hamstrings (Back of Leg)", Description = "Three muscles located on the back of the thigh." },
                new Muscle { Id = Guid.Parse("619fe532-8226-4d84-9a61-b64bcdef8084"), Name = "Gluteus Maximus (Butt)", Description = "Largest muscle of the gluteal group, forming the buttocks." },
                new Muscle { Id = Guid.Parse("e56d075d-e1db-49d0-be1c-a925b80e87a6"), Name = "Gastrocnemius (Calves)", Description = "The larger calf muscle, forming the bulge of the calf." },
                new Muscle { Id = Guid.Parse("cb5afad8-c1ae-44e5-ac7b-aa992f5c80a7"), Name = "Soleus (Calves)", Description = "Muscle underneath the gastrocnemius, part of the calf." },
                new Muscle { Id = Guid.Parse("7be040bd-2759-4b13-a049-8cde648dfd75"), Name = "Forearms", Description = "Muscles of the forearm responsible for wrist and finger movements." },
                new Muscle { Id = Guid.Parse("6ed76f26-52f2-4350-b0b3-103e370f0835"), Name = "Biceps", Description = "Front upper arm muscle responsible for elbow flexion." },
                new Muscle { Id = Guid.Parse("f19a5492-81ea-4ebc-8b03-1198e8440a58"), Name = "Triceps", Description = "Back upper arm muscle responsible for elbow extension." }
                });
                await dbContext.SaveChangesAsync();
            }
            if (await dbContext.Plans.CountAsync() <= 0)
            {
                dbContext.Plans.AddRange(new List<Plan>()
                    {
                        new Plan { Id = Guid.Parse("75A057A2-4238-44DE-E48F-08DD2F6B81D1"), CreatedAt = DateTime.Parse("2025-01-08 00:34:54.2288903"), Descreption = "tested pardo", IsPublic = false, Name = "tested", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-09 06:05:54.1939540"), planCreator = null },
                        new Plan { Id = Guid.Parse("16BFEB86-3B7D-4AF7-8FE4-08DD2F6BDA0D"), CreatedAt = DateTime.Parse("2025-01-08 00:37:21.8171157"), Descreption = "first pro split workout", IsPublic = false, Name = "prosplit 1", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-08 00:37:21.8171446"), planCreator = null },
                        new Plan { Id = Guid.Parse("73CEB7CE-F469-4386-FA63-08DD2F6CAE37"), CreatedAt = DateTime.Parse("2025-01-08 00:43:17.7645455"), Descreption = "first pro split workout", IsPublic = false, Name = "prosplit 1", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-08 00:43:17.7645752"), planCreator = null },
                        new Plan { Id = Guid.Parse("481D2142-F78B-4584-B76F-08DD2F6DE0DA"), CreatedAt = DateTime.Parse("2025-01-08 00:51:52.2318353"), Descreption = "first pro split workout", IsPublic = false, Name = "prosplit 1", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-08 00:51:52.2318660"), planCreator = null },
                        new Plan { Id = Guid.Parse("681EAF94-BEC4-49A7-B770-08DD2F6DE0DA"), CreatedAt = DateTime.Parse("2025-01-08 00:52:35.5421252"), Descreption = "first pro split workout", IsPublic = false, Name = "prosplit 1", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-08 00:52:35.5421309"), planCreator = null },
                        new Plan { Id = Guid.Parse("60686D8F-3DC6-4A5B-F59C-08DD2F6EC83D"), CreatedAt = DateTime.Parse("2025-01-08 00:58:20.4175949"), Descreption = "first pro split workout", IsPublic = false, Name = "prosplit 1", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-08 00:58:20.4176229"), planCreator = null },
                        new Plan { Id = Guid.Parse("10EB6A3F-5575-4D70-FCFD-08DD2F6EF9C3"), CreatedAt = DateTime.Parse("2025-01-08 00:59:43.5023156"), Descreption = "first pro split workout", IsPublic = false, Name = "prosplit 1", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-08 00:59:43.5023440"), planCreator = null },
                        new Plan { Id = Guid.Parse("850D665D-3A78-483F-09D6-08DD2F700044"), CreatedAt = DateTime.Parse("2025-01-08 01:07:03.9123240"), Descreption = "first pro split workout", IsPublic = false, Name = "prosplit 1", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-08 01:07:03.9123542"), planCreator = null },
                        new Plan { Id = Guid.Parse("B63FDB83-B5C1-4D3E-9E20-08DD2F703002"), CreatedAt = DateTime.Parse("2025-01-08 01:08:24.0126040"), Descreption = "first pro split workout", IsPublic = false, Name = "prosplit 1", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-08 01:08:24.0126352"), planCreator = null },
                        new Plan { Id = Guid.Parse("96929CC6-219E-4B71-3EDD-08DD2F948D47"), CreatedAt = DateTime.Parse("2025-01-08 05:28:42.4001807"), Descreption = "first pro split workout", IsPublic = false, Name = "prosplit 2", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-08 05:28:42.4002151"), planCreator = null },
                        new Plan { Id = Guid.Parse("A5613396-A2F6-4307-E0DD-08DD2FB252C7"), CreatedAt = DateTime.Parse("2025-01-08 09:01:49.5789928"), Descreption = "first pro split workout", IsPublic = true, Name = "prosplit 9", PlanCreatedBy = "a4059c44-8a45-4200-bfa8-bd618696d3ea", UpdatedAt = DateTime.Parse("2025-01-08 09:01:49.5790279"), planCreator = null },
                        new Plan { Id = Guid.Parse("7D8E432C-4308-4F03-C029-08DD31E3B99C"), CreatedAt = DateTime.Parse("2025-01-11 04:00:29.7675908"), Descreption = "string", IsPublic = false, Name = "test", PlanCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde", UpdatedAt = DateTime.Parse("2025-01-11 04:00:29.7676207"), planCreator = null },
                        new Plan { Id = Guid.Parse("AD068EA7-A8EA-43BF-C02A-08DD31E3B99C"), CreatedAt = DateTime.Parse("2025-01-11 04:07:52.7442578"), Descreption = "string", IsPublic = false, Name = "testing edit", PlanCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde", UpdatedAt = DateTime.Parse("2025-01-12 08:20:12.9610587"), planCreator = null },
                    });
                await dbContext.SaveChangesAsync();
            };
            if(await dbContext.Roles.CountAsync() <= 0)
            {
                var AdminRole = new IdentityRole {Id = "f02a0229-1fa7-4c12-8985-2436ff1b495b" , ConcurrencyStamp = "f02a0229-1fa7-4c12-8985-2436ff1b495b" , Name = "Admin" ,NormalizedName = "Admin".ToUpper()};
                var UserRole = new IdentityRole { Id = "de6594c7-64c1-4d22-bdfc-4de7eda3628c", ConcurrencyStamp = "de6594c7-64c1-4d22-bdfc-4de7eda3628c", Name = "User", NormalizedName = "User".ToUpper() };
                dbContext.Roles.AddRange(new List<IdentityRole> { AdminRole, UserRole });
            }
            if (await dbContext.Users.CountAsync() <= 0)
            {
                var user1 = new User
                {
                    Id = "29b0975c-b32f-4842-988a-e038f0470fde",
                    Points = 13,
                    WeightInKg = 300,
                    Age = 23,
                    TallInCm = 180,
                    UserName = "Adel",
                    NormalizedUserName = "ADEL",
                    Email = "adel@example.com",
                    NormalizedEmail = "adel@example.com".ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEFmnP3oAYzQXbJzbg3E0QO244dITFVVhATdbAPnhxsKOE/RIXzEDrhh2PfRnwlV9FA==",
                    SecurityStamp = "ZKOWONABQMSDTEH24DA7UKL7BHHEVE35",
                    ConcurrencyStamp = "8ede4032-e421-48fe-819a-8d56827652fe",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    IsMale = true,
                    isPublic = true,
                };
                var user2 = new User
                {
                    Id = "8dda839a-bbd1-46bc-884b-9abeade45f11",
                    Points = 17,
                    WeightInKg = 300,
                    Age = 20,
                    TallInCm = 170,
                    UserName = "omar",
                    NormalizedUserName = "omar".ToUpper(),
                    Email = "omar@example.com",
                    NormalizedEmail = "omar@example.com".ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEBn1KKJM/U2l7IHDg/nzn5tjEkUbfUO5SS1HP10nqQDhgn9Gcen6sDXlaXS4Da7vfQ==",
                    SecurityStamp = "LABT4FCISSWEPNBRR3HZUWUFFSLL6Y7W",
                    ConcurrencyStamp = "3f3d25d0-9aac-4636-b82e-44a48a9e4807",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    IsMale = true,
                    isPublic = true,
                };
                var user3 = new User
                {
                    Id = "a4059c44-8a45-4200-bfa8-bd618696d3ea",
                    Points = 20,
                    WeightInKg = 95,
                    Age = 22,
                    TallInCm = 182,
                    UserName = "Mohamed_Alage",
                    NormalizedUserName = "Mohamed_Alage".ToUpper(),
                    Email = "user@example.com",
                    NormalizedEmail = "user@example.com".ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEIgdao0VzAmeQQeTYftwgImGIkDrBwGL3bXCkbOzxNxOWQ8Oa70arQWsg/p07k9EyQ==",
                    SecurityStamp = "4YYRMNKFKUEDU6JWAFQ3UQ24WP3C44D6",
                    ConcurrencyStamp = "fe1282a9-ddfe-48df-ad2f-d213ad625eb1",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    IsMale = true,
                    isPublic = true,
                };
                dbContext.Users.AddRange(new List<User> { user1, user2, user3 });
                await dbContext.SaveChangesAsync();
            }
            if( await dbContext.UserRoles.CountAsync() <= 0)
            {
                var userRole1 = new IdentityUserRole<string> { UserId = "29b0975c-b32f-4842-988a-e038f0470fde", RoleId = "de6594c7-64c1-4d22-bdfc-4de7eda3628c" };
                var userRole2 = new IdentityUserRole<string> { UserId = "8dda839a-bbd1-46bc-884b-9abeade45f11", RoleId = "de6594c7-64c1-4d22-bdfc-4de7eda3628c" };
                var userRole3 = new IdentityUserRole<string> { UserId = "a4059c44-8a45-4200-bfa8-bd618696d3ea", RoleId = "de6594c7-64c1-4d22-bdfc-4de7eda3628c" };
                dbContext.UserRoles.AddRange(new List<IdentityUserRole<string>> { userRole1, userRole2, userRole3 });
                await dbContext.SaveChangesAsync();
            }

            if (await dbContext.Workouts.CountAsync() <= 0)
            {
                dbContext.Workouts.AddRange(new List<Workout>() {
    new Workout {
        Id = Guid.Parse("F641BAD9-BBC2-4642-FBBE-08DD3151CB88"),
        Name = "Overhead Dumbbell Press",
        MainMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Press dumbbells overhead while seated or standing to target all three deltoid heads.",
        Link = "https://www.example.com/overhead-dumbbell-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:35:53.3139610"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:35:53.3139965")
    },
    new Workout {
        Id = Guid.Parse("AEE71966-6DF2-4806-FBBF-08DD3151CB88"),
        Name = "Arnold Press",
        MainMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Rotate dumbbells while pressing overhead to engage all parts of the deltoid.",
        Link = "https://www.example.com/arnold-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:36:06.4985274"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:36:06.4985347")
    },
    new Workout {
        Id = Guid.Parse("4A9CA6D1-C95A-4D52-FBC0-08DD3151CB88"),
        Name = "Lateral Raises",
        MainMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Lift dumbbells to the side to isolate the lateral deltoid.",
        Link = "https://www.example.com/lateral-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:36:12.1715043"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:36:12.1715168")
    },
    new Workout {
        Id = Guid.Parse("D0C398B8-EEB3-426D-FBC1-08DD3151CB88"),
        Name = "Front Raises",
        MainMuscleId = Guid.Parse("D729E9F8-3384-4873-83BC-74ACDCCACABE"),
        SecondaryMuscleId = Guid.Parse("8909C9D4-2F45-4A2F-90C8-D6F475820A2F"),
        Description = "Lift dumbbells forward to target the anterior deltoid.",
        Link = "https://www.example.com/front-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:36:16.8247937"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:36:16.8248011")
    },
    new Workout {
        Id = Guid.Parse("384ABF7A-25B0-46B2-FBC2-08DD3151CB88"),
        Name = "Reverse Fly",
        MainMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Bend forward and lift dumbbells outward to target the posterior deltoid.",
        Link = "https://www.example.com/reverse-fly",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:36:20.6321133"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:36:20.6321211")
    },
    new Workout {
        Id = Guid.Parse("B2B0CFD1-0257-4C23-FBC3-08DD3151CB88"),
        Name = "Push Press",
        MainMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Use your legs and arms to explosively press a barbell overhead.",
        Link = "https://www.example.com/push-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:36:25.3064456"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:36:25.3064546")
    },
    new Workout {
        Id = Guid.Parse("C9539E49-D6C3-488E-FBC4-08DD3151CB88"),
        Name = "Cable Lateral Raises",
        MainMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Perform lateral raises using a cable machine for constant tension.",
        Link = "https://www.example.com/cable-lateral-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:36:30.0001847"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:36:30.0001953")
    },
    new Workout {
        Id = Guid.Parse("FDD60EE0-3C7F-4E98-FBC5-08DD3151CB88"),
        Name = "Upright Row",
        MainMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Pull a barbell or dumbbells upward close to your body to target the deltoid and traps.",
        Link = "https://www.example.com/upright-row",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:36:34.3728698"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:36:34.3728741")
    },
    new Workout {
        Id = Guid.Parse("8D37133B-C45A-48ED-FBC6-08DD3151CB88"),
        Name = "Dumbbell Shrugs",
        MainMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        SecondaryMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        Description = "Lift your shoulders while holding dumbbells to activate the traps and delts.",
        Link = "https://www.example.com/dumbbell-shrugs",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:36:39.0959626"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:36:39.0959699")
    },
    new Workout {
        Id = Guid.Parse("DCA4875C-703E-474E-FBC8-08DD3151CB88"),
        Name = "Reverse Pec Deck Machine",
        MainMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Sit on a pec deck machine, face forward, and grip the handles. Push the handles back, focusing on squeezing your shoulder blades together to target the posterior deltoids.",
        Link = "https://www.example.com/reverse-pec-deck",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:39:47.0709970"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:39:47.0710013")
    },
    new Workout {
        Id = Guid.Parse("1E0F91A7-D800-4451-FBC9-08DD3151CB88"),
        Name = "Bent-Over Dumbbell Lateral Raises",
        MainMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Stand with your knees slightly bent, hinge at the waist, and hold a dumbbell in each hand. Raise your arms to the side, keeping a slight bend in your elbows, to target the posterior deltoids.",
        Link = "https://www.example.com/bent-over-dumbbell-lateral-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:39:53.5474222"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:39:53.5474284")
    },
    new Workout {
        Id = Guid.Parse("51B97618-2006-4AA2-FBCB-08DD3151CB88"),
        Name = "Rear Delt Fly (Machine)",
        MainMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Sit on a rear delt fly machine, set the pads to shoulder height, and push the handles outward in a reverse fly motion to target the posterior deltoids.",
        Link = "https://www.example.com/rear-delt-fly-machine",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:40:02.1633907"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:40:02.1633969")
    },
    new Workout {
        Id = Guid.Parse("49B1ABFC-9F1C-4CF7-FBCC-08DD3151CB88"),
        Name = "Single-Arm Dumbbell Row",
        MainMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        SecondaryMuscleId = Guid.Parse("A2528D27-0DFE-48C8-9A18-7887DD9743EF"),
        Description = "Place one knee and hand on a bench for support, hold a dumbbell in the opposite hand, and pull the weight towards your hip, squeezing the posterior deltoid.",
        Link = "https://www.example.com/single-arm-dumbbell-row",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:40:06.6568810"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:40:06.6568901")
    },
    new Workout {
        Id = Guid.Parse("4F32A9A3-8DA7-4354-FBCD-08DD3151CB88"),
        Name = "Reverse Cable Fly",
        MainMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Set the cables at shoulder height on a cable machine, hold the handles with both hands, and pull the cables outward in a reverse fly motion to target the posterior deltoid.",
        Link = "https://www.example.com/reverse-cable-fly",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:40:11.3531908"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:40:11.3532415")
    },
    new Workout {
        Id = Guid.Parse("76EF880D-D45F-4A48-FBCE-08DD3151CB88"),
        Name = "Standing Dumbbell Reverse Fly",
        MainMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Stand with feet shoulder-width apart, hinge slightly at the waist, and hold a dumbbell in each hand. Raise your arms outward to the side while keeping a slight bend in your elbows.",
        Link = "https://www.example.com/standing-dumbbell-reverse-fly",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:40:19.5709707"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:40:19.5709732")
    },
    new Workout {
        Id = Guid.Parse("DA673C8D-9576-4A1F-FBCF-08DD3151CB88"),
        Name = "Cable Rear Delt Pulldown",
        MainMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Set a cable machine to a high position, grasp the rope attachment with both hands, and pull it down and back while squeezing the posterior deltoids.",
        Link = "https://www.example.com/cable-rear-delt-pulldown",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:40:23.9473958"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:40:23.9473975")
    },
    new Workout {
        Id = Guid.Parse("4928B010-5D4B-40D4-FBD0-08DD3151CB88"),
        Name = "Barbell Rear Delt Row",
        MainMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Grip the barbell with both hands, bend your knees slightly, and hinge at the waist. Pull the barbell towards your upper chest, focusing on the contraction in the posterior deltoids.",
        Link = "https://www.example.com/barbell-rear-delt-row",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:40:28.4963667"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:40:28.4963691")
    },
    new Workout {
        Id = Guid.Parse("07E9777E-F273-409D-FBD1-08DD3151CB88"),
        Name = "Incline Rear Delt Fly",
        MainMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Lie face down on an incline bench and hold a dumbbell in each hand. Perform a reverse fly movement to target the posterior deltoid muscles.",
        Link = "https://www.example.com/incline-rear-delt-fly",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:40:32.7470895"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:40:32.7470918")
    },
    new Workout {
        Id = Guid.Parse("7656FB4B-7382-4A5A-FBD2-08DD3151CB88"),
        Name = "Incline Barbell Bench Press",
        MainMuscleId = Guid.Parse("8909C9D4-2F45-4A2F-90C8-D6F475820A2F"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Set an incline bench to a 30-45 degree angle. Perform a bench press using a barbell, focusing on driving the weight up while squeezing the upper chest.",
        Link = "https://www.example.com/incline-barbell-bench-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:41:34.1101917"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:41:34.1101948")
    },
    new Workout {
        Id = Guid.Parse("43E9ED19-CC59-4270-FBD3-08DD3151CB88"),
        Name = "Incline Dumbbell Bench Press",
        MainMuscleId = Guid.Parse("8909C9D4-2F45-4A2F-90C8-D6F475820A2F"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Set an incline bench and hold a dumbbell in each hand. Lower the dumbbells to your chest and press them up, focusing on contracting the upper chest at the top.",
        Link = "https://www.example.com/incline-dumbbell-bench-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:41:45.1421426"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:41:45.1421454")
    },
    new Workout {
        Id = Guid.Parse("13B108AE-30BF-4825-FBD4-08DD3151CB88"),
        Name = "Incline Cable Chest Fly",
        MainMuscleId = Guid.Parse("8909C9D4-2F45-4A2F-90C8-D6F475820A2F"),
        SecondaryMuscleId = Guid.Parse("D729E9F8-3384-4873-83BC-74ACDCCACABE"),
        Description = "Set an incline bench in front of two low cable pulleys. Grab the cable handles and perform a chest fly, focusing on squeezing your upper chest at the top of the movement.",
        Link = "https://www.example.com/incline-cable-chest-fly",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:41:49.7022907"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:41:49.7022924")
    },
    new Workout {
        Id = Guid.Parse("CADEC81E-A3DD-4D43-FBD5-08DD3151CB88"),
        Name = "Reverse Grip Bench Press",
        MainMuscleId = Guid.Parse("8909C9D4-2F45-4A2F-90C8-D6F475820A2F"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Using a reverse grip (palms facing you), perform a bench press. This variation places more emphasis on the upper chest compared to a regular grip.",
        Link = "https://www.example.com/reverse-grip-bench-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:41:55.0403311"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:41:55.0403334")
    },
    new Workout {
        Id = Guid.Parse("52C5A44D-5EC6-4131-FBD6-08DD3151CB88"),
        Name = "Incline Dumbbell Chest Fly",
        MainMuscleId = Guid.Parse("8909C9D4-2F45-4A2F-90C8-D6F475820A2F"),
        SecondaryMuscleId = Guid.Parse("D729E9F8-3384-4873-83BC-74ACDCCACABE"),
        Description = "Set an incline bench and hold a dumbbell in each hand. With a slight bend in your elbows, open your arms wide and bring the dumbbells together in front of your chest.",
        Link = "https://www.example.com/incline-dumbbell-chest-fly",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:42:00.7230091"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:42:00.7230126")
    },
    new Workout {
        Id = Guid.Parse("2D202F5B-02AC-4F1C-FBD8-08DD3151CB88"),
        Name = "Push-Ups (Feet Elevated)",
        MainMuscleId = Guid.Parse("8909C9D4-2F45-4A2F-90C8-D6F475820A2F"),
        SecondaryMuscleId = Guid.Parse("D729E9F8-3384-4873-83BC-74ACDCCACABE"),
        Description = "Place your feet on an elevated surface (like a bench or box) while performing push-ups. This shifts the emphasis to the upper chest.",
        Link = "https://www.example.com/push-ups-feet-elevated",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:42:09.4469359"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:42:09.4469398")
    },
    new Workout {
        Id = Guid.Parse("0A4355B1-1652-4EDD-FBD9-08DD3151CB88"),
        Name = "Landmine Chest Press",
        MainMuscleId = Guid.Parse("8909C9D4-2F45-4A2F-90C8-D6F475820A2F"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Place a barbell in a landmine attachment, and hold the barbell with both hands. Press the barbell forward, focusing on the upper chest contraction at the top.",
        Link = "https://www.example.com/landmine-chest-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:42:14.0779390"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:42:14.0779416")
    },
    new Workout {
        Id = Guid.Parse("0416E8E9-3B9B-476B-FBDA-08DD3151CB88"),
        Name = "Machine Chest Press (Incline)",
        MainMuscleId = Guid.Parse("8909C9D4-2F45-4A2F-90C8-D6F475820A2F"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Using an incline chest press machine, press the handles forward and together while focusing on the contraction in the upper chest.",
        Link = "https://www.example.com/machine-chest-press-incline",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:42:19.9901459"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:42:19.9901487")
    },
    new Workout {
        Id = Guid.Parse("D16D02ED-279C-4537-FBDB-08DD3151CB88"),
        Name = "Standing Cable Chest Fly (High to Low)",
        MainMuscleId = Guid.Parse("8909C9D4-2F45-4A2F-90C8-D6F475820A2F"),
        SecondaryMuscleId = Guid.Parse("D729E9F8-3384-4873-83BC-74ACDCCACABE"),
        Description = "Set the cables to a high position, grab the handles, and pull the cables down and together in a fly motion, focusing on the upper chest contraction.",
        Link = "https://www.example.com/standing-cable-chest-fly-high-to-low",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:42:25.1076302"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:42:25.1076331")
    },
    new Workout {
        Id = Guid.Parse("A95A64EB-2A55-418E-FBDC-08DD3151CB88"),
        Name = "Decline Barbell Bench Press",
        MainMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Set a bench to a decline angle and use a barbell to perform a bench press, lowering the bar to your lower chest and pressing it up while keeping your focus on the lower chest.",
        Link = "https://www.example.com/decline-barbell-bench-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:43:47.5484274"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:43:47.5484296")
    },
    new Workout {
        Id = Guid.Parse("8CF941DC-0508-4B39-FBDD-08DD3151CB88"),
        Name = "Decline Dumbbell Bench Press",
        MainMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Perform a decline bench press with dumbbells, focusing on pressing the weights up while contracting the lower chest.",
        Link = "https://www.example.com/decline-dumbbell-bench-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:43:51.8551053"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:43:51.8551076")
    },
    new Workout {
        Id = Guid.Parse("9D387A2C-C574-4D7F-FBDE-08DD3151CB88"),
        Name = "Decline Cable Chest Fly",
        MainMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        SecondaryMuscleId = Guid.Parse("D729E9F8-3384-4873-83BC-74ACDCCACABE"),
        Description = "Set the cables to a high position and perform a chest fly, bringing the cables downward to target the lower chest.",
        Link = "https://www.example.com/decline-cable-chest-fly",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:43:56.6557689"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:43:56.6557713")
    },
    new Workout {
        Id = Guid.Parse("03661744-F19B-4134-FBDF-08DD3151CB88"),
        Name = "Chest Dips (Leaning Forward)",
        MainMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Perform dips on parallel bars while leaning forward to place more emphasis on the lower chest, making sure to keep the elbows flared.",
        Link = "https://www.example.com/chest-dips-leaning-forward",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:44:01.9806347"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:44:01.9806364")
    },
    new Workout {
        Id = Guid.Parse("8B7FA470-9A8E-4584-FBE0-08DD3151CB88"),
        Name = "Machine Chest Press (Decline)",
        MainMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Using a decline chest press machine, press the handles forward and together, focusing on the contraction in the lower chest.",
        Link = "https://www.example.com/machine-chest-press-decline",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:44:06.5152716"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:44:06.5152733")
    },
    new Workout {
        Id = Guid.Parse("837B69C5-A8E2-4A39-FBE1-08DD3151CB88"),
        Name = "Decline Dumbbell Chest Fly",
        MainMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        SecondaryMuscleId = Guid.Parse("D729E9F8-3384-4873-83BC-74ACDCCACABE"),
        Description = "On a decline bench, hold a dumbbell in each hand, and with a slight bend in your elbows, open your arms wide and bring them back together to target the lower chest.",
        Link = "https://www.example.com/decline-dumbbell-chest-fly",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:44:12.1130672"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:44:12.1130685")
    },
    new Workout {
        Id = Guid.Parse("364365E5-3821-47E9-FBE2-08DD3151CB88"),
        Name = "Barbell Pullover",
        MainMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        SecondaryMuscleId = Guid.Parse("A2528D27-0DFE-48C8-9A18-7887DD9743EF"),
        Description = "Lying on a bench with a barbell in your hands, lower the barbell behind your head and pull it back over your chest, emphasizing the lower chest as you bring it forward.",
        Link = "https://www.example.com/barbell-pullover",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:44:17.6405121"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:44:17.6405138")
    },
    new Workout {
        Id = Guid.Parse("5741216F-FA31-4D3D-FBE3-08DD3151CB88"),
        Name = "Incline Push-Ups (Feet Elevated)",
        MainMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Perform push-ups with your feet elevated on a platform to target the lower chest more effectively while maintaining a focus on form.",
        Link = "https://www.example.com/incline-push-ups-feet-elevated",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:44:22.1161945"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:44:22.1161958")
    },
    new Workout {
        Id = Guid.Parse("4CB9A713-C6EB-405D-FBE4-08DD3151CB88"),
        Name = "Decline Push-Ups",
        MainMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Place your feet on an elevated surface to perform push-ups, which will shift the emphasis to the lower chest and allow for better engagement of the sternal head.",
        Link = "https://www.example.com/decline-push-ups",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:44:26.2857221"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:44:26.2857234")
    },
    new Workout {
        Id = Guid.Parse("02678A66-023E-4FED-FBE5-08DD3151CB88"),
        Name = "Landmine Chest Press (Decline)",
        MainMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Set a barbell in a landmine attachment, hold the bar with both hands, and perform a chest press with a downward angle to target the lower chest.",
        Link = "https://www.example.com/landmine-chest-press-decline",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:44:30.3046095"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:44:30.3046106")
    },
    new Workout {
        Id = Guid.Parse("23E32C3B-D09F-4656-FBE6-08DD3151CB88"),
        Name = "Pull-Ups",
        MainMuscleId = Guid.Parse("A2528D27-0DFE-48C8-9A18-7887DD9743EF"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Hang from a pull-up bar with your palms facing away from you and pull your chin above the bar, focusing on squeezing your lats.",
        Link = "https://www.example.com/pull-ups",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:45:04.6294675"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:45:04.6294693")
    },
    new Workout {
        Id = Guid.Parse("479093A7-E4EE-45DF-FBE7-08DD3151CB88"),
        Name = "Lat Pulldown",
        MainMuscleId = Guid.Parse("A2528D27-0DFE-48C8-9A18-7887DD9743EF"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Sit at a lat pulldown machine and pull the bar down toward your chest while keeping your back slightly arched to engage the lats.",
        Link = "https://www.example.com/lat-pulldown",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:45:09.3760829"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:45:09.3760851")
    },
    new Workout {
        Id = Guid.Parse("F9F331D6-AE0F-474F-FBE8-08DD3151CB88"),
        Name = "Chin-Ups",
        MainMuscleId = Guid.Parse("A2528D27-0DFE-48C8-9A18-7887DD9743EF"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Perform chin-ups with your palms facing you to emphasize the engagement of your lats and biceps while pulling your chin above the bar.",
        Link = "https://www.example.com/chin-ups",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:45:26.7311521"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:45:26.7311534")
    },
    new Workout {
        Id = Guid.Parse("1F1D4111-C018-499F-FBE9-08DD3151CB88"),
        Name = "Wide-Grip Lat Pulldown",
        MainMuscleId = Guid.Parse("A2528D27-0DFE-48C8-9A18-7887DD9743EF"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Sit at a lat pulldown machine with a wide grip and pull the bar down towards your chest, focusing on engaging your lats throughout the movement.",
        Link = "https://www.example.com/wide-grip-lat-pulldown",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:45:39.9188191"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:45:39.9188207")
    },
    new Workout {
        Id = Guid.Parse("17949109-D244-43B4-FBEB-08DD3151CB88"),
        Name = "Barbell Shrugs",
        MainMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Hold a barbell in front of your thighs with an overhand grip, shrug your shoulders up toward your ears, and hold the contraction for a moment before lowering.",
        Link = "https://www.example.com/barbell-shrugs",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:46:33.9104635"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:46:33.9104648")
    },
    new Workout {
        Id = Guid.Parse("A41523D3-0CEA-418A-FBED-08DD3151CB88"),
        Name = "Face Pulls",
        MainMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        SecondaryMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        Description = "Using a rope attachment on a cable machine, pull the rope towards your face while keeping your elbows high and squeezing the traps at the peak of the movement.",
        Link = "https://www.example.com/face-pulls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:51:53.8088397"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:51:53.8088414")
    },
    new Workout {
        Id = Guid.Parse("9A2A8A9E-E858-45BE-FBEE-08DD3151CB88"),
        Name = "Upright Rows",
        MainMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        SecondaryMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        Description = "Hold a barbell or dumbbells in front of your thighs, then pull them upwards towards your chin while keeping your elbows higher than your hands.",
        Link = "https://www.example.com/upright-rows",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:53:40.2607404"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:53:40.2607419")
    },
    new Workout {
        Id = Guid.Parse("BE2723A8-668C-4DB3-FBEF-08DD3151CB88"),
        Name = "Cable Reverse Fly",
        MainMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        SecondaryMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        Description = "Using a cable machine, set the pulleys at shoulder height, pull the cables outward in a reverse fly motion, and squeeze the shoulder blades together at the peak.",
        Link = "https://www.example.com/cable-reverse-fly",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:53:46.2997223"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:53:46.2997245")
    },
    new Workout {
        Id = Guid.Parse("1E3263BA-BDC6-4CC8-FBF0-08DD3151CB88"),
        Name = "Dumbbell Reverse Fly",
        MainMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        SecondaryMuscleId = Guid.Parse("C4AC8999-40A4-40C8-9518-42BE7E2D0744"),
        Description = "Bend at the hips and hold a dumbbell in each hand, then extend your arms out to the sides in a reverse fly motion, targeting the middle traps.",
        Link = "https://www.example.com/dumbbell-reverse-fly",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:53:52.6337534"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:53:52.6337553")
    },
    new Workout {
        Id = Guid.Parse("5D7B9D42-B443-4F7C-FBF1-08DD3151CB88"),
        Name = "Pendlay Rows",
        MainMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        SecondaryMuscleId = Guid.Parse("A2528D27-0DFE-48C8-9A18-7887DD9743EF"),
        Description = "Perform a barbell row with a strict form by keeping the barbell at the ground after each rep and pulling it explosively toward your lower chest, engaging the traps.",
        Link = "https://www.example.com/pendlay-rows",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:53:57.8292874"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:53:57.8292886")
    },
    new Workout {
        Id = Guid.Parse("76DF5638-D5E0-4486-FBF2-08DD3151CB88"),
        Name = "Wide-Grip Barbell Rows",
        MainMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        SecondaryMuscleId = Guid.Parse("A2528D27-0DFE-48C8-9A18-7887DD9743EF"),
        Description = "Using a wider grip than usual, row the barbell towards your torso while focusing on engaging the traps and upper back muscles.",
        Link = "https://www.example.com/wide-grip-barbell-rows",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:54:02.8713974"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:54:02.8713985")
    },
    new Workout {
        Id = Guid.Parse("DC489DFF-3E14-4D22-FBF3-08DD3151CB88"),
        Name = "Inverted Rows",
        MainMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        SecondaryMuscleId = Guid.Parse("A2528D27-0DFE-48C8-9A18-7887DD9743EF"),
        Description = "Position yourself under a barbell, hold it with an overhand grip, and pull your chest toward the bar while keeping your body straight, engaging your traps and upper back.",
        Link = "https://www.example.com/inverted-rows",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:54:13.5170911"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:54:13.5170927")
    },
    new Workout {
        Id = Guid.Parse("DA6A252F-9231-487C-FBF6-08DD3151CB88"),
        Name = "Hyperextensions",
        MainMuscleId = Guid.Parse("B61C068E-8E8B-4756-A05C-482D0EC70F9E"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Lie face down on a hyperextension bench, hook your feet under the pads, and raise your upper body to a horizontal position, squeezing your lower back muscles at the top.",
        Link = "https://www.example.com/hyperextensions",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:55:35.9321856"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:55:35.9321880")
    },
    new Workout {
        Id = Guid.Parse("AA86C018-8086-4521-FBF7-08DD3151CB88"),
        Name = "Good Mornings",
        MainMuscleId = Guid.Parse("B61C068E-8E8B-4756-A05C-482D0EC70F9E"),
        SecondaryMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        Description = "Place a barbell on your shoulders and bend forward at the hips, keeping your back straight and chest up, then return to the standing position.",
        Link = "https://www.example.com/good-mornings",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:55:39.7095861"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:55:39.7095872")
    },
    new Workout {
        Id = Guid.Parse("62CE56F2-7954-494C-FBF9-08DD3151CB88"),
        Name = "Back Extensions",
        MainMuscleId = Guid.Parse("B61C068E-8E8B-4756-A05C-482D0EC70F9E"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "On a back extension machine, extend your torso upward until it is aligned with your legs, then lower your torso back to the starting position.",
        Link = "https://www.example.com/back-extensions",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:55:48.7970935"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:55:48.7970950")
    },
    new Workout {
        Id = Guid.Parse("4AA15AC5-CF84-446F-FBFA-08DD3151CB88"),
        Name = "Bird Dogs",
        MainMuscleId = Guid.Parse("B61C068E-8E8B-4756-A05C-482D0EC70F9E"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "From a hands-and-knees position, extend one arm and the opposite leg simultaneously, hold for a second, and then return to the starting position, focusing on engaging your lower back.",
        Link = "https://www.example.com/bird-dogs",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:55:52.8170541"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:55:52.8170557")
    },
    new Workout {
        Id = Guid.Parse("B1B978F9-93F4-46DE-FBFB-08DD3151CB88"),
        Name = "Squats",
        MainMuscleId = Guid.Parse("B61C068E-8E8B-4756-A05C-482D0EC70F9E"),
        SecondaryMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        Description = "With a barbell on your back, squat down while keeping your back straight, knees behind your toes, and then return to standing position by pushing through your heels.",
        Link = "https://www.example.com/squats",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:55:56.9191851"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:55:56.9191868")
    },
    new Workout {
        Id = Guid.Parse("03C2EAF6-04D3-442A-FBFC-08DD3151CB88"),
        Name = "Trap Bar Deadlifts",
        MainMuscleId = Guid.Parse("B61C068E-8E8B-4756-A05C-482D0EC70F9E"),
        SecondaryMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        Description = "Stand inside a trap bar, squat down to grip the handles, and lift the bar by standing up while keeping your back straight.",
        Link = "https://www.example.com/trap-bar-deadlifts",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:56:02.2748039"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:56:02.2748050")
    },
    new Workout {
        Id = Guid.Parse("0F976F37-14B9-4F88-FBFD-08DD3151CB88"),
        Name = "Single-Leg Romanian Deadlifts",
        MainMuscleId = Guid.Parse("B61C068E-8E8B-4756-A05C-482D0EC70F9E"),
        SecondaryMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        Description = "Balance on one leg, hold a dumbbell or kettlebell in one hand, and hinge forward at the hips while keeping your back straight, then return to the standing position.",
        Link = "https://www.example.com/single-leg-romanian-deadlifts",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:56:06.8489463"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:56:06.8489655")
    },
    new Workout {
        Id = Guid.Parse("96D7D586-79F2-4246-FBFE-08DD3151CB88"),
        Name = "Barbell Squats",
        MainMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Stand with your feet shoulder-width apart, place a barbell on your upper back, and squat down while keeping your back straight and knees behind your toes, then return to standing.",
        Link = "https://www.example.com/barbell-squats",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:56:41.4346414"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:56:41.4346427")
    },
    new Workout {
        Id = Guid.Parse("74E490CF-C1BD-4573-FBFF-08DD3151CB88"),
        Name = "Leg Press",
        MainMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Sit on a leg press machine, place your feet on the platform shoulder-width apart, and push the weight upward while keeping your knees at a 90-degree angle.",
        Link = "https://www.example.com/leg-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:56:46.4799722"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:56:46.4799736")
    },
    new Workout {
        Id = Guid.Parse("2DB73FE2-5F2D-4909-FC02-08DD3151CB88"),
        Name = "Bulgarian Split Squats",
        MainMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Place one foot behind you on an elevated surface, lower your body into a lunge position, then push through your front heel to return to the starting position.",
        Link = "https://www.example.com/bulgarian-split-squats",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:57:04.9805628"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:57:04.9805638")
    },
    new Workout {
        Id = Guid.Parse("A70E2BAF-64D5-4710-FC04-08DD3151CB88"),
        Name = "Hack Squats",
        MainMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Position yourself on a hack squat machine, squat down while keeping your back pressed against the pad, then push through your heels to return to standing.",
        Link = "https://www.example.com/hack-squats",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:57:17.0174984"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:57:17.0175000")
    },
    new Workout {
        Id = Guid.Parse("3857CA7C-FE4E-4340-FC05-08DD3151CB88"),
        Name = "Walking Lunges",
        MainMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Take a step forward into a lunge, then bring your back leg forward into the next lunge without stopping, continuing to alternate legs as you walk.",
        Link = "https://www.example.com/walking-lunges",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:57:26.5993395"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:57:26.5993415")
    },
    new Workout {
        Id = Guid.Parse("B5EF2E70-F51B-4A59-FC06-08DD3151CB88"),
        Name = "Front Squats",
        MainMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Position a barbell on the front of your shoulders, squat down while keeping your back straight and chest up, and then return to the standing position.",
        Link = "https://www.example.com/front-squats",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:57:32.6867831"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:57:32.6867844")
    },
    new Workout {
        Id = Guid.Parse("24FCC173-E492-4C2E-FC07-08DD3151CB88"),
        Name = "Romanian Deadlifts",
        MainMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "With a slight bend in your knees, lower the barbell or dumbbells down to your shins while keeping your back straight, then return to standing position.",
        Link = "https://www.example.com/romanian-deadlifts",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:58:10.1329669"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:58:10.1329686")
    },
    new Workout {
        Id = Guid.Parse("4856A997-1F81-4B12-FC08-08DD3151CB88"),
        Name = "Deadlifts",
        MainMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Stand with your feet shoulder-width apart, grip the barbell in front of you, and lift it by extending your hips and knees while keeping your back straight.",
        Link = "https://www.example.com/deadlifts",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:58:21.2886295"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:58:21.2886317")
    },
    new Workout {
        Id = Guid.Parse("1CA07AFA-61D7-491A-FC0A-08DD3151CB88"),
        Name = "Hip Thrusts",
        MainMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Sit on the floor with your upper back against a bench, place a barbell across your hips, and thrust your hips upward while keeping your feet flat on the floor.",
        Link = "https://www.example.com/hip-thrusts",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:58:33.2757898"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:58:33.2757925")
    },
    new Workout {
        Id = Guid.Parse("1C4E2F86-5EB7-450E-FC0B-08DD3151CB88"),
        Name = "Single-Leg Deadlifts",
        MainMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Stand on one leg, hold a dumbbell or kettlebell in one hand, and hinge forward at the hips while keeping your back straight, then return to standing position.",
        Link = "https://www.example.com/single-leg-deadlifts",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:58:37.6874044"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:58:37.6874061")
    },
    new Workout {
        Id = Guid.Parse("4E3DE179-39F2-4933-FC0C-08DD3151CB88"),
        Name = "Glute-Ham Raises",
        MainMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        SecondaryMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        Description = "Position yourself on a glute-ham raise machine, lower your torso towards the floor, then push through your hamstrings and glutes to raise your body back up.",
        Link = "https://www.example.com/glute-ham-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:58:43.0770533"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:58:43.0770551")
    },
    new Workout {
        Id = Guid.Parse("8E1E49B4-85A9-4F55-FC10-08DD3151CB88"),
        Name = "Barbell Hip Thrusts",
        MainMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        SecondaryMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        Description = "Sit on the floor with your upper back against a bench, place a barbell across your hips, and thrust your hips upward while keeping your feet flat on the floor.",
        Link = "https://www.example.com/barbell-hip-thrusts",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:59:19.7522732"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:59:19.7522747")
    },
    new Workout {
        Id = Guid.Parse("24B974C0-6DE3-49CA-FC14-08DD3151CB88"),
        Name = "Lunges",
        MainMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        SecondaryMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        Description = "Take a large step forward and lower your body until both knees are bent at 90-degree angles, then push through your front heel to return to standing.",
        Link = "https://www.example.com/lunges",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:59:40.7896513"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:59:40.7896529")
    },
    new Workout {
        Id = Guid.Parse("DDB23929-5FB1-416C-FC15-08DD3151CB88"),
        Name = "Kettlebell Swings",
        MainMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        SecondaryMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        Description = "Swing a kettlebell between your legs and then explosively raise it to chest height or above, engaging your glutes and hamstrings to initiate the movement.",
        Link = "https://www.example.com/kettlebell-swings",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:59:45.6426721"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:59:45.6426732")
    },
    new Workout {
        Id = Guid.Parse("489F51DB-5BB3-4233-FC16-08DD3151CB88"),
        Name = "Glute Bridge",
        MainMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        SecondaryMuscleId = Guid.Parse("2C6891F3-98D9-4BFE-BF90-A7E5B6B1CAF8"),
        Description = "Lie on your back with your knees bent and feet flat on the floor, then lift your hips toward the ceiling by squeezing your glutes.",
        Link = "https://www.example.com/glute-bridge",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:59:49.0892744"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:59:49.0892757")
    },
    new Workout {
        Id = Guid.Parse("E00F0CA7-A92D-4DB7-FC17-08DD3151CB88"),
        Name = "Step-Ups",
        MainMuscleId = Guid.Parse("619FE532-8226-4D84-9A61-B64BCDEF8084"),
        SecondaryMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        Description = "Step onto a bench or elevated platform with one leg, then bring the other leg up to meet it, and step back down, alternating legs.",
        Link = "https://www.example.com/step-ups",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 10:59:52.9303992"),
        UpdatedAt = DateTime.Parse("2025-01-10 10:59:52.9304003")
    },
    new Workout {
        Id = Guid.Parse("A3910812-E136-4DC6-FC18-08DD3151CB88"),
        Name = "Standing Calf Raises",
        MainMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        SecondaryMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        Description = "Stand tall with your feet shoulder-width apart, then raise your heels to stand on the balls of your feet, and lower back down.",
        Link = "https://www.example.com/standing-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:01:23.2575499"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:01:23.2575510")
    },
    new Workout {
        Id = Guid.Parse("818FFCCF-B57E-46A1-FC19-08DD3151CB88"),
        Name = "Seated Calf Raises",
        MainMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        SecondaryMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        Description = "Sit on a calf raise machine with your feet on the platform and knees bent, then raise your heels as high as possible and lower back down.",
        Link = "https://www.example.com/seated-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:01:29.3512382"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:01:29.3512396")
    },
    new Workout {
        Id = Guid.Parse("124C69B7-7477-49E5-FC1A-08DD3151CB88"),
        Name = "Calf Raises on Leg Press",
        MainMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        SecondaryMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        Description = "Position your feet at the bottom of a leg press machine, then extend your legs and push through the balls of your feet to raise your heels, before returning.",
        Link = "https://www.example.com/calf-raises-on-leg-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:01:33.6312090"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:01:33.6312102")
    },
    new Workout {
        Id = Guid.Parse("4ADAD905-F087-4E46-FC1B-08DD3151CB88"),
        Name = "Donkey Calf Raises",
        MainMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        SecondaryMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        Description = "Bend forward at the waist while keeping your feet on the platform and your knees slightly bent, then raise your heels as high as possible.",
        Link = "https://www.example.com/donkey-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:01:37.9880166"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:01:37.9880178")
    },
    new Workout {
        Id = Guid.Parse("167F44C3-4483-45AB-FC1C-08DD3151CB88"),
        Name = "Jump Rope",
        MainMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        SecondaryMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        Description = "Jump on the balls of your feet while rotating the rope, engaging your calves with each jump.",
        Link = "https://www.example.com/jump-rope",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:01:42.0896313"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:01:42.0896328")
    },
    new Workout {
        Id = Guid.Parse("F97FCFDF-3134-498B-FC1D-08DD3151CB88"),
        Name = "Box Jumps",
        MainMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        SecondaryMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        Description = "Jump explosively onto a box or elevated platform, landing softly on the balls of your feet, then step down.",
        Link = "https://www.example.com/box-jumps",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:01:45.6287081"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:01:45.6287094")
    },
    new Workout {
        Id = Guid.Parse("2A7E0C10-BD7C-4783-FC1F-08DD3151CB88"),
        Name = "Hill Sprints",
        MainMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        SecondaryMuscleId = Guid.Parse("B391D0E8-442E-414A-85CC-9445B3BD11BE"),
        Description = "Sprint up a hill or incline, engaging your calves as you push off each step, then walk back down to rest.",
        Link = "https://www.example.com/hill-sprints",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:01:54.1853136"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:01:54.1853151")
    },
    new Workout {
        Id = Guid.Parse("DC92C7B9-2692-41D7-FC20-08DD3151CB88"),
        Name = "Calf Raises on Smith Machine",
        MainMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        SecondaryMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        Description = "Place your shoulders under the bar of a Smith machine, then raise your heels while keeping your knees slightly bent, before lowering back down.",
        Link = "https://www.example.com/calf-raises-on-smith-machine",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:01:59.9101569"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:01:59.9101585")
    },
    new Workout {
        Id = Guid.Parse("3561CD3C-B174-4261-FC23-08DD3151CB88"),
        Name = "Standing Calf Raises with Bent Knees",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Stand tall with your knees slightly bent, then raise your heels to stand on the balls of your feet, and lower back down.",
        Link = "https://www.example.com/standing-calf-raises-bent-knees",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:02:40.3643229"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:02:40.3643240")
    },
    new Workout {
        Id = Guid.Parse("DC4E0FF1-740D-4D3F-FC24-08DD3151CB88"),
        Name = "Smith Machine Seated Calf Raises",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Sit on a bench with a Smith machine bar resting on your thighs, place your feet flat on the ground, then raise your heels as high as possible.",
        Link = "https://www.example.com/smith-machine-seated-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:02:45.2472096"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:02:45.2472113")
    },
    new Workout {
        Id = Guid.Parse("597637F5-AEE6-4F66-FC25-08DD3151CB88"),
        Name = "Toe Presses on Leg Press Machine",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Place your toes on the bottom of a leg press machine, extend your legs, and push through your toes to raise the weight, focusing on the calves.",
        Link = "https://www.example.com/toe-presses-on-leg-press-machine",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:02:51.7871117"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:02:51.7871132")
    },
    new Workout {
        Id = Guid.Parse("45E975C4-5DFE-4346-FC26-08DD3151CB88"),
        Name = "Lying Calf Raises",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Lie on your stomach with your feet flat on the floor, then place a weight on your ankles and raise your heels while squeezing your calves.",
        Link = "https://www.example.com/lying-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:02:56.0787921"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:02:56.0787941")
    },
    new Workout {
        Id = Guid.Parse("675404B5-975A-4D38-FC27-08DD3151CB88"),
        Name = "Resistance Band Calf Raises",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Place a resistance band under your feet and hold the handles at your sides, then raise your heels while keeping your knees slightly bent.",
        Link = "https://www.example.com/resistance-band-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:03:01.3893369"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:03:01.3893387")
    },
    new Workout {
        Id = Guid.Parse("E53A5A66-881B-4041-FC28-08DD3151CB88"),
        Name = "Bodyweight Seated Calf Raises",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Sit on a bench with your feet flat on the floor, then raise your heels as high as possible using only your bodyweight.",
        Link = "https://www.example.com/bodyweight-seated-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:03:05.2069618"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:03:05.2069629")
    },
    new Workout {
        Id = Guid.Parse("AA7A3A83-10D6-4B30-FC29-08DD3151CB88"),
        Name = "Calf Raise Machine",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Sit or stand on a calf raise machine, then press your toes against the platform and raise your heels while focusing on your calves.",
        Link = "https://www.example.com/calf-raise-machine",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:03:08.8927800"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:03:08.8927821")
    },
    new Workout {
        Id = Guid.Parse("EC662201-52EB-498B-FC2A-08DD3151CB88"),
        Name = "Donkey Calf Raises with Bent Knees",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Bend at the waist while keeping your knees bent, then raise your heels as high as possible while engaging your calves.",
        Link = "https://www.example.com/donkey-calf-raises-bent-knees",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:03:13.2859447"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:03:13.2859463")
    },
    new Workout {
        Id = Guid.Parse("590FE0AC-2744-4A32-FC2B-08DD3151CB88"),
        Name = "Seated Calf Raises with Dumbbells",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Sit on a bench with a dumbbell resting on each knee, then raise your heels while keeping your knees bent, focusing on the calves.",
        Link = "https://www.example.com/seated-calf-raises-with-dumbbells",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:03:17.7612585"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:03:17.7612597")
    },
    new Workout {
        Id = Guid.Parse("57F7C994-D60D-4919-FC2C-08DD3151CB88"),
        Name = "Standing Calf Raises with Dumbbells",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Stand upright holding dumbbells in your hands, raise your heels to stand on the balls of your feet, and lower back down.",
        Link = "https://www.example.com/standing-calf-raises-with-dumbbells",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:04:01.5266890"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:04:01.5266900")
    },
    new Workout {
        Id = Guid.Parse("1F245609-0AD8-4854-FC2D-08DD3151CB88"),
        Name = "Seated Calf Raise Machine (Double Leg)",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Sit on the calf raise machine with both legs positioned under the pads, then raise your heels as high as you can.",
        Link = "https://www.example.com/seated-calf-raise-machine-double-leg",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:04:05.4954218"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:04:05.4954234")
    },
    new Workout {
        Id = Guid.Parse("CF5613FB-FA28-4E49-FC2E-08DD3151CB88"),
        Name = "Seated Single-Leg Calf Raises",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Sit on a bench, place one foot on the floor and the other on a raised surface, then raise your heel with a focus on one calf at a time.",
        Link = "https://www.example.com/seated-single-leg-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:04:10.2291140"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:04:10.2291153")
    },
    new Workout {
        Id = Guid.Parse("4262E2BD-BD91-4B57-FC2F-08DD3151CB88"),
        Name = "Standing Calf Raise on Smith Machine",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Position your shoulders under the bar on a Smith machine, then raise your heels while keeping your knees slightly bent.",
        Link = "https://www.example.com/standing-calf-raise-on-smith-machine",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:04:14.7280096"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:04:14.7280112")
    },
    new Workout {
        Id = Guid.Parse("D29876BA-C81B-4323-FC30-08DD3151CB88"),
        Name = "Barbell Calf Raises",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Stand with a barbell on your shoulders, then raise your heels to perform a calf raise, engaging your Soleus and Gastrocnemius.",
        Link = "https://www.example.com/barbell-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:04:18.0565899"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:04:18.0565910")
    },
    new Workout {
        Id = Guid.Parse("A4910F87-715E-4458-FC31-08DD3151CB88"),
        Name = "Step-Calf Raises",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Stand with the balls of your feet on a step, let your heels drop below the step, and then raise your heels as high as possible.",
        Link = "https://www.example.com/step-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:04:21.4034465"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:04:21.4034479")
    },
    new Workout {
        Id = Guid.Parse("885B9BA4-C8A9-4309-FC32-08DD3151CB88"),
        Name = "Walking Calf Raises",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Walk on the balls of your feet, engaging your calves as you take each step, then switch to the other leg.",
        Link = "https://www.example.com/walking-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:04:26.0594737"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:04:26.0594753")
    },
    new Workout {
        Id = Guid.Parse("33C1C8A9-EF6B-479E-FC33-08DD3151CB88"),
        Name = "Donkey Calf Raises with Dumbbell",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Bend forward at the waist while holding a dumbbell between your feet, raise your heels, and squeeze your calves at the top of the movement.",
        Link = "https://www.example.com/donkey-calf-raises-with-dumbbell",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:04:30.6849157"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:04:30.6849171")
    },
    new Workout {
        Id = Guid.Parse("E5D8298E-E4D6-46C6-FC35-08DD3151CB88"),
        Name = "Resistance Band Seated Calf Raises",
        MainMuscleId = Guid.Parse("CB5AFAD8-C1AE-44E5-AC7B-AA992F5C80A7"),
        SecondaryMuscleId = Guid.Parse("E56D075D-E1DB-49D0-BE1C-A925B80E87A6"),
        Description = "Sit down, loop a resistance band around the balls of your feet, then push against the band to raise your heels as high as possible.",
        Link = "https://www.example.com/resistance-band-seated-calf-raises",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:04:38.9809079"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:04:38.9809094")
    },
    new Workout {
        Id = Guid.Parse("30899F72-9EEB-4C3C-FC36-08DD3151CB88"),
        Name = "Wrist Curls with Dumbbells",
        MainMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Sit on a bench with your forearms resting on your thighs, hold a dumbbell in each hand, and curl your wrists upwards while keeping your arms stationary.",
        Link = "https://www.example.com/wrist-curls-with-dumbbells",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:05:11.6138046"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:05:11.6138056")
    },
    new Workout {
        Id = Guid.Parse("98B906A2-7253-4E02-FC37-08DD3151CB88"),
        Name = "Reverse Wrist Curls",
        MainMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Sit with your forearms on your thighs, hold a dumbbell with an overhand grip, and curl your wrists upwards to target the forearms.",
        Link = "https://www.example.com/reverse-wrist-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:05:18.1770728"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:05:18.1770742")
    },
    new Workout {
        Id = Guid.Parse("426B195C-A3B3-425E-FC39-08DD3151CB88"),
        Name = "Farmer's Walk",
        MainMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        SecondaryMuscleId = Guid.Parse("405069D0-6E66-4D50-883D-E5B29A403A84"),
        Description = "Pick up a heavy dumbbell or kettlebell in each hand and walk for a set distance or time while keeping your grip tight.",
        Link = "https://www.example.com/farmers-walk",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:05:26.8999958"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:05:26.8999976")
    },
    new Workout {
        Id = Guid.Parse("BA24730E-42A6-43C9-FC3A-08DD3151CB88"),
        Name = "Wrist Roller",
        MainMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Using a wrist roller machine, roll a weight up and down a rope or bar by rotating your wrists.",
        Link = "https://www.example.com/wrist-roller",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:05:31.6653068"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:05:31.6653082")
    },
    new Workout {
        Id = Guid.Parse("3D02FB33-C5F1-43F6-FC3C-08DD3151CB88"),
        Name = "Plate Pinches",
        MainMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        SecondaryMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        Description = "Pinch two weight plates together with your fingers and hold for as long as you can to increase forearm strength.",
        Link = "https://www.example.com/plate-pinches",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:05:35.3315995"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:05:35.3316007")
    },
    new Workout {
        Id = Guid.Parse("831288BF-A444-4190-FC3D-08DD3151CB88"),
        Name = "Reverse Hammer Curls",
        MainMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Hold dumbbells with a neutral grip and curl them upward with your elbows stationary, engaging your forearms throughout the movement.",
        Link = "https://www.example.com/reverse-hammer-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:05:39.0584101"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:05:39.0584114")
    },
    new Workout {
        Id = Guid.Parse("C92B84E2-47CE-443D-FC3E-08DD3151CB88"),
        Name = "Finger Curls with Barbell",
        MainMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Hold a barbell with both hands, allowing it to roll down to your fingers, then curl it back up using only your forearms.",
        Link = "https://www.example.com/finger-curls-with-barbell",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:05:43.9018479"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:05:43.9018496")
    },
    new Workout {
        Id = Guid.Parse("886B6E19-C91D-4295-FC3F-08DD3151CB88"),
        Name = "Towel Pull-Ups",
        MainMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Wrap a towel over a pull-up bar and perform pull-ups while gripping the towel, forcing the forearms to work harder.",
        Link = "https://www.example.com/towel-pull-ups",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:05:48.5893822"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:05:48.5893835")
    },
    new Workout {
        Id = Guid.Parse("B1227BD7-CBB7-46A7-FC40-08DD3151CB88"),
        Name = "Cable Wrist Curls",
        MainMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        SecondaryMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        Description = "Attach a rope to a low pulley, sit with your forearms on your thighs, and curl the rope with your wrists to target your forearms.",
        Link = "https://www.example.com/cable-wrist-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:05:55.2287853"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:05:55.2287868")
    },
    new Workout {
        Id = Guid.Parse("59E99C0C-9CBB-4DF4-FC41-08DD3151CB88"),
        Name = "Barbell Bicep Curls",
        MainMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        SecondaryMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        Description = "Hold a barbell with an underhand grip, keeping your elbows close to your torso, and curl the barbell up towards your shoulders.",
        Link = "https://www.example.com/barbell-bicep-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:06:31.9695023"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:06:31.9695037")
    },
    new Workout {
        Id = Guid.Parse("FFD7CED9-DB99-45AA-FC42-08DD3151CB88"),
        Name = "Dumbbell Bicep Curls",
        MainMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        SecondaryMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        Description = "Hold a dumbbell in each hand with your palms facing forward, curl the dumbbells up to shoulder height while keeping your elbows stationary.",
        Link = "https://www.example.com/dumbbell-bicep-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:06:36.9917077"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:06:36.9917095")
    },
    new Workout {
        Id = Guid.Parse("881F6038-4DB0-4ADC-FC43-08DD3151CB88"),
        Name = "Hammer Curls",
        MainMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        SecondaryMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        Description = "Hold dumbbells with a neutral grip (palms facing each other), and curl the dumbbells up towards your shoulders, focusing on the brachialis and biceps.",
        Link = "https://www.example.com/hammer-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:06:42.8081804"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:06:42.8081822")
    },
    new Workout {
        Id = Guid.Parse("7A2A0AE0-02E5-4736-FC44-08DD3151CB88"),
        Name = "Concentration Curls",
        MainMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        SecondaryMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        Description = "Sit on a bench, lean forward, and curl a dumbbell with one arm at a time, isolating the biceps to maximize the contraction.",
        Link = "https://www.example.com/concentration-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:06:47.0914118"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:06:47.0914134")
    },
    new Workout {
        Id = Guid.Parse("111DA14D-71B3-436A-FC45-08DD3151CB88"),
        Name = "Preacher Curls",
        MainMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        SecondaryMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        Description = "Sit on a preacher bench and curl a barbell or dumbbells, focusing on a slow and controlled movement to maximize bicep engagement.",
        Link = "https://www.example.com/preacher-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:06:51.5667230"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:06:51.5667244")
    },
    new Workout {
        Id = Guid.Parse("25D7DE37-C93C-456E-FC47-08DD3151CB88"),
        Name = "Cable Bicep Curls",
        MainMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        SecondaryMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        Description = "Using a cable machine, attach a straight bar or rope and curl it towards your shoulders, keeping your elbows close to your body.",
        Link = "https://www.example.com/cable-bicep-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:06:54.9658655"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:06:54.9658667")
    },
    new Workout {
        Id = Guid.Parse("D8EC0342-3581-4B5A-FC48-08DD3151CB88"),
        Name = "Incline Dumbbell Curls",
        MainMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        SecondaryMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        Description = "Lie back on an incline bench and curl dumbbells towards your shoulders, focusing on the stretch and contraction of the biceps.",
        Link = "https://www.example.com/incline-dumbbell-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:06:59.1813365"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:06:59.1813379")
    },
    new Workout {
        Id = Guid.Parse("D41CD417-20B0-486E-FC49-08DD3151CB88"),
        Name = "Zottman Curls",
        MainMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        SecondaryMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        Description = "Hold a pair of dumbbells with a regular grip, curl them up, then rotate your palms to a reverse grip on the way down, targeting both the biceps and forearms.",
        Link = "https://www.example.com/zottman-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:07:03.5874013"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:07:03.5874032")
    },
    new Workout {
        Id = Guid.Parse("E3963A75-2DE8-4714-FC4B-08DD3151CB88"),
        Name = "Spider Curls",
        MainMuscleId = Guid.Parse("6ED76F26-52F2-4350-B0B3-103E370F0835"),
        SecondaryMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        Description = "Using an incline bench, place your chest against the bench while curling dumbbells up towards your face, isolating the biceps.",
        Link = "https://www.example.com/spider-curls",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:07:14.7844124"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:07:14.7844145")
    },
    new Workout {
        Id = Guid.Parse("81318E83-6F40-4787-FC4D-08DD3151CB88"),
        Name = "Triceps Dips",
        MainMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        SecondaryMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        Description = "Using parallel bars or a dip machine, lower your body by bending your elbows and then push back up to target the triceps.",
        Link = "https://www.example.com/triceps-dips",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:11:54.6329041"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:11:54.6329054")
    },
    new Workout {
        Id = Guid.Parse("2FAE1B2D-493F-46F2-FC4E-08DD3151CB88"),
        Name = "Close-Grip Bench Press",
        MainMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        SecondaryMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        Description = "On a flat bench, use a narrower grip to press a barbell, keeping your elbows close to your body, to focus on the triceps.",
        Link = "https://www.example.com/close-grip-bench-press",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:12:02.0294051"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:12:02.0294072")
    },
    new Workout {
        Id = Guid.Parse("BA31367E-A020-4A4F-FC4F-08DD3151CB88"),
        Name = "Overhead Triceps Extension",
        MainMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        SecondaryMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        Description = "Hold a dumbbell with both hands behind your head, then extend your arms to lift the weight, focusing on the triceps.",
        Link = "https://www.example.com/overhead-triceps-extension",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:12:08.0123583"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:12:08.0123595")
    },
    new Workout {
        Id = Guid.Parse("EEE08197-BD41-45F9-FC50-08DD3151CB88"),
        Name = "Triceps Pushdowns",
        MainMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        SecondaryMuscleId = Guid.Parse("7BE040BD-2759-4B13-A049-8CDE648DFD75"),
        Description = "Using a cable machine, attach a bar or rope to the high pulley and push the bar down to your thighs, extending your arms fully.",
        Link = "https://www.example.com/triceps-pushdowns",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:12:16.5820569"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:12:16.5820581")
    },
    new Workout {
        Id = Guid.Parse("DA21757C-A159-4C59-FC51-08DD3151CB88"),
        Name = "Skull Crushers (Lying Triceps Extensions)",
        MainMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        SecondaryMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        Description = "Lie on a flat bench, hold a barbell or dumbbells, and lower them towards your forehead, then extend your arms to engage the triceps.",
        Link = "https://www.example.com/skull-crushers",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:12:21.5689682"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:12:21.5689699")
    },
    new Workout {
        Id = Guid.Parse("F6AB5415-C8B3-492A-FC52-08DD3151CB88"),
        Name = "Diamond Push-Ups",
        MainMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        SecondaryMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        Description = "Perform a push-up with your hands close together beneath your chest, forming a diamond shape with your thumbs and index fingers to target the triceps.",
        Link = "https://www.example.com/diamond-push-ups",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:12:26.7687348"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:12:26.7687362")
    },
    new Workout {
        Id = Guid.Parse("1F1566C8-6C04-4EFA-FC53-08DD3151CB88"),
        Name = "Triceps Kickbacks",
        MainMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        SecondaryMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        Description = "Holding a dumbbell, bend over at the waist and extend your arm behind you, straightening your elbow to target the triceps.",
        Link = "https://www.example.com/triceps-kickbacks",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:12:32.6335951"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:12:32.6335965")
    },
    new Workout {
        Id = Guid.Parse("5F365DB9-AF42-4043-FC54-08DD3151CB88"),
        Name = "Dumbbell Triceps Extensions",
        MainMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        SecondaryMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        Description = "Sit or stand with a dumbbell in both hands above your head, lower it behind your head by bending your elbows, then extend your arms.",
        Link = "https://www.example.com/dumbbell-triceps-extensions",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:12:38.3427311"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:12:38.3427326")
    },
    new Workout {
        Id = Guid.Parse("B03C3DBF-EB4C-4269-FC55-08DD3151CB88"),
        Name = "Bench Dips",
        MainMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        SecondaryMuscleId = Guid.Parse("D8738439-5832-4764-8290-20EBE48D50DC"),
        Description = "Place your hands on a bench behind you, extend your legs out, and lower your body by bending your elbows, then press back up.",
        Link = "https://www.example.com/bench-dips",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:12:43.2674263"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:12:43.2674275")
    },
    new Workout {
        Id = Guid.Parse("6BB7769A-5F6F-4211-FC57-08DD3151CB88"),
        Name = "Cable Overhead Triceps Extension",
        MainMuscleId = Guid.Parse("F19A5492-81EA-4EBC-8B03-1198E8440A58"),
        SecondaryMuscleId = Guid.Parse("DF475BA8-6BE7-42B6-871F-6A29CD4C91D8"),
        Description = "Using a cable machine, attach a rope to the low pulley, and extend your arms overhead, focusing on the triceps as you bring the rope forward.",
        Link = "https://www.example.com/cable-overhead-triceps-extension",
        WorkoutCreatedBy = "29b0975c-b32f-4842-988a-e038f0470fde",
        IsCertified = true,
        CreatedAt = DateTime.Parse("2025-01-10 11:12:48.2168161"),
        UpdatedAt = DateTime.Parse("2025-01-10 11:12:48.2168177")
    }
                });
                dbContext.SaveChanges();
            }
            if ( await dbContext.Inbodies.CountAsync() <= 0)
            {
                dbContext.Inbodies.AddRange(new List<Inbody>(){
                    new Inbody {
                        Id = Guid.Parse("6F98D438-438E-4675-3498-08DD3036A4F8"),
                        userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea",
                        Name = "inbody",
                        age =  22,
                        weight = (float) 95,
                        BMI = (float) 28.68011,
                        BMR = (float) 1816.5,
                        FFM = (float) 62.62768,
                        LBM = (float) 62.62768,
                        BFP = (float) 34.07613,
                        TBW = (float) 45.7182,
                        CaloricNeed =  2815,
                        WaterIntake = (float) 3.135,
                        IdealBodyWeight = (float) 72.30315,
                        DailyProtenNeedInGrams = 133,
                    },
                    new Inbody {
                        Id = Guid.Parse("6613C126-BD75-4E04-5C53-08DD30376AD2"),
                        userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea",
                        Name = "string",
                        age =  22,
                        weight = (float) 98,
                        BMI = (float) 28.02482,
                        BMR = (float) 1877.75,
                        FFM = (float) 65.37601,
                        LBM = (float) 65.37601,
                        BFP = (float) 33.28979,
                        TBW = (float) 47.72449,
                        CaloricNeed = 2910,
                        WaterIntake = (float) 3.234,
                        IdealBodyWeight = (float) 76.83071,
                        DailyProtenNeedInGrams = 137,
                    }
                });
                await dbContext.SaveChangesAsync();
            }
            if(await dbContext.PlansUsers.CountAsync() <= 0)
            {
                dbContext.PlansUsers.AddRange(new List<PlansUsers> {
                    new PlansUsers {
                        Id = Guid.Parse("70A73454-B1A8-48CC-E09B-08DD2FB0EF5D" ),
                        UserId = "a4059c44-8a45-4200-bfa8-bd618696d3ea",
                        PlanId = Guid.Parse("82AE1F10-CC89-4958-5C41-08DD2FB0EF3C"),
                        PlanOrder = 3},
                    new PlansUsers {
                        Id = Guid.Parse("57BBFCC9-0CFB-48A6-AB52-08DD2FB252E9" ),
                        UserId = "a4059c44-8a45-4200-bfa8-bd618696d3ea",
                        PlanId = Guid.Parse("A5613396-A2F6-4307-E0DD-08DD2FB252C7"),
                        PlanOrder = 2},
                    new PlansUsers {
                        Id = Guid.Parse("29891FD5-7752-4D62-DBAA-08DD31E3B9BD" ),
                        UserId = "29b0975c-b32f-4842-988a-e038f0470fde",
                        PlanId = Guid.Parse("7D8E432C-4308-4F03-C029-08DD31E3B99C"),
                        PlanOrder = 2},
                    new PlansUsers {
                        Id = Guid.Parse("0781D4BB-7371-4AD0-DBAB-08DD31E3B9BD" ),
                        UserId = "29b0975c-b32f-4842-988a-e038f0470fde",
                        PlanId = Guid.Parse("AD068EA7-A8EA-43BF-C02A-08DD31E3B99C"),
                        PlanOrder = 3},
                });
                await dbContext.SaveChangesAsync();
            }
            if (await dbContext.PlanWorkouts.CountAsync() == 0) {
                dbContext.PlanWorkouts.AddRange(
                    new List<PlanWorkout> { 
                        new PlanWorkout {
                            Id = Guid.Parse("A46BD22D-952A-4CBD-DF2B-08DD31E3B9B3"),
                            PlanId = Guid.Parse("7D8E432C-4308-4F03-C029-08DD31E3B99C"),
                            WorkoutId = Guid.Parse("F641BAD9-BBC2-4642-FBBE-08DD3151CB88"),
                            Reps = "string",
                            DayNumber = 1,
                            Order = 1 },
                        new PlanWorkout {
                            Id = Guid.Parse("73EC71A9-9E27-4B60-228A-08DD32D0F913"),
                            PlanId = Guid.Parse("AD068EA7-A8EA-43BF-C02A-08DD31E3B99C"),
                            WorkoutId = Guid.Parse("AEE71966-6DF2-4806-FBBF-08DD3151CB88"),
                            Reps = "string",
                            DayNumber = 0,
                            Order = 0
                        },
                        new PlanWorkout {
                            Id = Guid.Parse("AAE23501-D77F-4925-228B-08DD32D0F913"),
                            PlanId = Guid.Parse("AD068EA7-A8EA-43BF-C02A-08DD31E3B99C"),
                            WorkoutId = Guid.Parse("F641BAD9-BBC2-4642-FBBE-08DD3151CB88"),
                            Reps = "string",
                            DayNumber = 0,
                            Order = 0
                        }
                    });
                await dbContext.SaveChangesAsync();
            }
            if(await dbContext.UserWorkouts.CountAsync() <= 0){
            var userWorkouts = new List<UserWorkout> {
            new UserWorkout{
                Id = Guid.Parse("EB848B01-784A-4C76-AFC1-08DD31E3B9B9"),
                UserId = "29b0975c-b32f-4842-988a-e038f0470fde",
                WorkoutId = Guid.Parse("F641BAD9-BBC2-4642-FBBE-08DD3151CB88"),
                MaxWeight = 30,
                LastWeight = 20,
                TimesPerformed = 5},
            new UserWorkout{
                Id = Guid.Parse("994F2C31-9C80-42EE-AFC2-08DD31E3B9B9"),
                UserId = "29b0975c-b32f-4842-988a-e038f0470fde",
                WorkoutId = Guid.Parse("AEE71966-6DF2-4806-FBBF-08DD3151CB88"),
                MaxWeight = 30,
                LastWeight = 20,
                TimesPerformed = 7},
            };
                dbContext.UserWorkouts.AddRange(userWorkouts);
            }
            return dbContext;
        }
    }
}

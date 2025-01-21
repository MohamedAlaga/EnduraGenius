using EnduraGenius.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnduraGenius.API.Data
{
    public class EnduraGeniusDBContext : IdentityDbContext<User>
    {
        public EnduraGeniusDBContext(DbContextOptions<EnduraGeniusDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Muscle> Muscles { get; set; } 
        public DbSet<PlansUsers> PlansUsers { get; set; }
        public DbSet<PlanWorkout> PlanWorkouts { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<UserWorkout> UserWorkouts { get; set; }
        public DbSet<Inbody> Inbodies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminId = "f02a0229-1fa7-4c12-8985-2436ff1b495b";
            var userId = "de6594c7-64c1-4d22-bdfc-4de7eda3628c";
            var adminuser = new User {
                Id = "48149538-3d20-4314-9f75-33a92d9c2b24",
                Points = 0,
                WeightInKg = 95 ,
                Age = 22,TallInCm = 182 ,
                UserName = "Alaga_Admin",
                NormalizedUserName = "Alaga_Admin".ToUpper(),
                Email = "admin@EnduraGenius.com",
                NormalizedEmail = "ADMIN@ENDURAGENIUS.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEB8mSwn38vSJ7/KNpsmlbKq6WJrDHdhP0KFAIf+XSJ4OxMtfq7eqdLrv3IxTrqbSQg==",
                SecurityStamp = "FJAS5D5VKCP3AVUILWGTTBJWK2TLWS36",
                ConcurrencyStamp = "f02a0229-1fa7-4c12-8985-2436ff1b495b",
                LockoutEnabled = true,
                IsMale = true,
                isPublic = true,
            };
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminId,
                    ConcurrencyStamp = adminId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = userId,
                    ConcurrencyStamp = userId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
            };
            List<Muscle> muscles = new List<Muscle>() {
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
            new Muscle { Id = Guid.Parse("f19a5492-81ea-4ebc-8b03-1198e8440a58"), Name = "Triceps", Description = "Back upper arm muscle responsible for elbow extension." }};
            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminId,
                    UserId = adminuser.Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = userId,
                    UserId = adminuser.Id
                }
            };
            builder.Entity<Plan>()
                .HasOne(p => p.planCreator)
                .WithMany()
                .HasForeignKey(p => p.PlanCreatedBy)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Workout>()
                .HasOne(w => w.WorkoutCreator)
                .WithMany()
                .HasForeignKey(w => w.WorkoutCreatedBy)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<IdentityRole>().HasData(roles);
            builder.Entity<User>().HasData(adminuser);
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
            builder.Entity<Muscle>().HasData(muscles);
        }
    }
}

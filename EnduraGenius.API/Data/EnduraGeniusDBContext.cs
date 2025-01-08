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
            builder.Entity<Plan>()
                .HasOne(p => p.planCreator)
                .WithMany()
                .HasForeignKey(p => p.PlanCreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Workout>()
                .HasOne(w => w.WorkoutCreator)
                .WithMany()
                .HasForeignKey(w => w.WorkoutCreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Workout>()
                .HasOne(w => w.MainMuscle)
                .WithMany()
                .HasForeignKey(w => w.MainMuscleId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Workout>()
                .HasOne(w => w.SecondaryMuscle)
                .WithMany()
                .HasForeignKey(w => w.SecondaryMuscleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

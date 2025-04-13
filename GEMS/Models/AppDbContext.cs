using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GEMS.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<UserExercise> UserExercises { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 1,
                    Name = "Squats",
                    Description = "Stand with feet shoulder-width apart. Lower your hips until thighs are parallel to the ground, then push back up. Strengthens legs and glutes.",
                    isWeighted = false
                },
                new Exercise
                {
                    Id = 2,
                    Name = "Jumping Jacks",
                    Description = "Start standing, then jump while spreading legs and raising arms overhead. Improves cardiovascular endurance and coordination.",
                    isWeighted = false
                },
                new Exercise
                {
                    Id = 3,
                    Name = "Pull Ups",
                    Description = "Grip a bar wider than shoulder-width. Pull your body up until your chin clears the bar, then lower slowly. Targets back, arms, and shoulders.",
                    isWeighted = false
                },
                new Exercise
                {
                    Id = 4,
                    Name = "Push Ups",
                    Description = "Place hands slightly wider than shoulders. Lower your body until chest nearly touches the ground, then push up. Works chest, arms, and core.",
                    isWeighted = false
                },
                new Exercise
                {
                    Id = 5,
                    Name = "Planks",
                    Description = "Hold a push-up position with elbows bent 90°, keeping your body straight. Engages core, shoulders, and back muscles.",
                    isWeighted = false
                },
                new Exercise
                {
                    Id = 6,
                    Name = "Bicep Curls",
                    Description = "Hold weights (dumbbells) at your sides, palms forward. Curl weights toward shoulders, then lower slowly. Isolates bicep muscles.",
                    isWeighted = true
                },
                new Exercise
                {
                    Id = 7,
                    Name = "Front Raises",
                    Description = "Hold weights in front of thighs. Raise arms straight to shoulder height, then lower slowly. Strengthens shoulder (deltoid) muscles.",
                    isWeighted = true
                },
                new Exercise
                {
                    Id = 8,
                    Name = "Leg Raises",
                    Description = "Lie flat, lift legs straight up to 90°, then lower without touching the ground. Focuses on lower abdominal strength.",
                    isWeighted = false
                }
            );
        }


    }

}

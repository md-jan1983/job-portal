using Microsoft.EntityFrameworkCore;

namespace Jobportal.Web.API.Models
{    
    public class Job
    {
        public int Id { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<JobSkill> JobSkills { get; set; }
    }

    public class JobSkill
    {
        public int JobId { get; set; }
        public int SkillId { get; set; }
    }

    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Employer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string Contact { get; set; }
    }

    // Models/Application.cs
    public class Application
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public string CandidateId { get; set; } // FK to User.Id
        public string ResumeUrl { get; set; }
        public string Status { get; set; } = "Applied";
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
    public class User
    {
        public string Id { get; set; } // Assuming string to match Identity User Id type
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // "Candidate" or "Employer"
        public DateTime CreatedAt { get; set; } // "Candidate" or "Employer"
    }

    // Data/AppDbContext.cs
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobSkill>().HasKey(js => new { js.JobId, js.SkillId });
            // further configuration...
            //modelBuilder.Entity<JobSkill>().HasForeignKey<Skill>(js => js.);
        }
    }

}

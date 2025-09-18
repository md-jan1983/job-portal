using Jobportal.Web.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jobportal.Web.API.JobsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public JobsController(AppDbContext db) { _db = db; }
        public JobsController() { }
        public async Task<IActionResult> GetJob()
        {
            var job = new JobData
            {
                Id = 1,
                Title = "Senior Software Engineer",
                Company = "Tech Solutions Inc.",
                Location = "Remote",
                Type = "Full-time",
                Salary = "$140k-150k",
                PostedDate = "2 days ago",                
                Responsibilities = new List<string>{
        "Developing user-facing applications using React.js",
        "Building reusable components and front-end libraries"
    },
                Requirements = new List<string>
    {
        "4+ years of professional experience with React.js",
        "Strong proficiency in JavaScript, HTML, and CSS"
    },
                HiringManager = new HiringManager
                {
                    Name = "Jane Doe",
                    Title = "HR Manager",
                    Email = "jane.doe@techsolutions.com"
                }
            };
            if (job == null) return NotFound();
            return Ok(job);
        }
    }
    public class JobData
    {
        // Properties mapping to JSON
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Salary { get; set; }
        public string PostedDate { get; set; }
        public List<string> Description { get; set; }
        public List<string> Responsibilities { get; set; }
        public List<string> Requirements { get; set; }
        public List<string> Benefits { get; set; }
        public List<string> AboutTheCompany { get; set; }
        public HiringManager HiringManager { get; set; }
        public List<JobSummary> RelatedJobs { get; set; }

        // ----- Example Methods -----

        // Print basic job details
        public void PrintSummary()
        {
            Console.WriteLine($"[{Id}] {Title} at {Company} ({Location})");
            Console.WriteLine($"Type: {Type}, Salary: {Salary}, Posted: {PostedDate}");
        }

        // Check if the job matches a keyword
        public bool MatchesKeyword(string keyword)
        {
            keyword = keyword?.ToLower();
            return Title.ToLower().Contains(keyword) ||
                   Company.ToLower().Contains(keyword) ||
                   (Description?.Any(d => d.ToLower().Contains(keyword)) ?? false) ||
                   (Responsibilities?.Any(r => r.ToLower().Contains(keyword)) ?? false) ||
                   (Requirements?.Any(r => r.ToLower().Contains(keyword)) ?? false);
        }

        // Get formatted responsibilities
        public string GetResponsibilitiesAsText()
        {
            return Responsibilities != null ? string.Join("\n- ", Responsibilities) : string.Empty;
        }

        // Check if job meets experience criteria
        public bool RequiresYearsOfExperience(int years)
        {
            return Requirements != null &&
                   Requirements.Any(r => r.Contains($"{years}+ years") || r.Contains($"{years} years"));
        }

        // Add a related job
        public void AddRelatedJob(JobSummary relatedJob)
        {
            RelatedJobs ??= new List<JobSummary>();
            RelatedJobs.Add(relatedJob);
        }
    }
    public class HiringManager
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }

        public void PrintContact()
        {
            Console.WriteLine($"Hiring Manager: {Name}, {Title} ({Email})");
        }
    }
    public class JobSummary
    {
        // A lighter version of JobData for related jobs
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Salary { get; set; }
        public string PostedDate { get; set; }
        public List<string> Description { get; set; }
        public List<string> Requirements { get; set; }

    }
}

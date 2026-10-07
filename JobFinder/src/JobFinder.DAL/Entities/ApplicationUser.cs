using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? CompanyName { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
        public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
    }
}

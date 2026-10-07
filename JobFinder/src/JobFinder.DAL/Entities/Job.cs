using JobFinder.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.DAL.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public decimal Salary { get; set; }
        public int YearsOfExperience { get; set; }
        public JobStatus Status { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public String CreatedBy { get; set; }
        public String UpdatedBy { get; set; }
        public String DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        

        public ApplicationUser Owner { get; set; }

        public ICollection<JobApplication> Applications { get; set; }
            = new List<JobApplication>();
    }
}

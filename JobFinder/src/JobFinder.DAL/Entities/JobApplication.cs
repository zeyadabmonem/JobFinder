using JobFinder.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.DAL.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }



        public ApplicationStatus Status { get; set; }

        public DateTime AppliedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; } 
        public string SeekerId { get; set; } 

        public ApplicationUser Seeker { get; set; } 
    }
}

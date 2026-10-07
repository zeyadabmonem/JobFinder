using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.DAL.Entities.Enums
{
    public enum UserRole
    {
        Seeker,
        CompanyAdmin
    }
    public enum JobStatus
    {
        Open,
        Closed
    }
    public enum ApplicationStatus
    {
        Pending,
        Reviewed,
        Interview,
        Accepted,
        Rejected
    }
}

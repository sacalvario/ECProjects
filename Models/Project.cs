using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int IdProject { get; set; }
        public int IdGeneratedby { get; set; }
        public int IdCustomer { get; set; }
        public int IdManager { get; set; }
        public string QuoteNumber { get; set; }
        public int ProjectComplexity { get; set; }
        public int IdStatus { get; set; }
        public int TotalAssembliesInProject { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CustomerNeedby { get; set; }
        public int TotalEstimatedDuration { get; set; }
        public int SuccesRateEstimate { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual Employee IdGeneratedbyNavigation { get; set; }
        public virtual Employee IdManagerNavigation { get; set; }
        public virtual Status IdStatusNavigation { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}

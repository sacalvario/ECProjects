using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.ProjectsModel
{
    public partial class Project
    {
        public Project()
        {
            ProjectEmployees = new HashSet<ProjectEmployee>();
            ProjectParts = new HashSet<ProjectPart>();
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int IdProject { get; set; }
        public int IdGeneratedby { get; set; }
        public int IdCustomer { get; set; }
        public int IdLocation { get; set; }
        public int IdIndustry { get; set; }
        public string FolderLink { get; set; }
        public int IdDocument { get; set; }
        public int IdManager { get; set; }
        public string QuoteNumber { get; set; }
        public int IdAttach { get; set; }
        public int IdStatus { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalEstimatedDuration { get; set; }
        public int SuccesRateEstimate { get; set; }

        public virtual Attachment IdAttachNavigation { get; set; }
        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual Document IdDocumentNavigation { get; set; }
        public virtual Employee IdGeneratedbyNavigation { get; set; }
        public virtual Industry IdIndustryNavigation { get; set; }
        public virtual Location IdLocationNavigation { get; set; }
        public virtual Employee IdManagerNavigation { get; set; }
        public virtual Status IdStatusNavigation { get; set; }
        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public virtual ICollection<ProjectPart> ProjectParts { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}

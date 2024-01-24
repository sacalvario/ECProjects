using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Status
    {
        public Status()
        {
            ProjectTasks = new HashSet<ProjectTask>();
            Projects = new HashSet<Project>();
        }

        public int IdStatus { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Task
    {
        public Task()
        {
            ProjectTaskIdTaskNavigations = new HashSet<ProjectTask>();
            ProjectTaskPredecessorNavigations = new HashSet<ProjectTask>();
        }

        public int IdTask { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProjectTask> ProjectTaskIdTaskNavigations { get; set; }
        public virtual ICollection<ProjectTask> ProjectTaskPredecessorNavigations { get; set; }
    }
}

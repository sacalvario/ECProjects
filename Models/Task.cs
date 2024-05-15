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
        }

        public int IdTask { get; set; }
        public string Name { get; set; }
        public float? Predecessor { get; set; }
        public float? Number { get; set; }

        public virtual ICollection<ProjectTask> ProjectTaskIdTaskNavigations { get; set; }
    }
}

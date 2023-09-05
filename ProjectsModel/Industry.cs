using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.ProjectsModel
{
    public partial class Industry
    {
        public Industry()
        {
            Projects = new HashSet<Project>();
        }

        public int IdIndustry { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.ProjectsModel
{
    public partial class Location
    {
        public Location()
        {
            Projects = new HashSet<Project>();
        }

        public int IdLocation { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}

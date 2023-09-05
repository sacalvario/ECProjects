using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.ProjectsModel
{
    public partial class Group
    {
        public Group()
        {
            Tasks = new HashSet<Task>();
        }

        public int IdGroup { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.ProjectsModel
{
    public partial class Attachment
    {
        public Attachment()
        {
            Projects = new HashSet<Project>();
        }

        public int IdAttachment { get; set; }
        public byte[] File { get; set; }
        public string FileLink { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}

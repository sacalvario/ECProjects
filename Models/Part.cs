using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Models
{
    public partial class Part
    {
        public Part()
        {
            ProjectParts = new HashSet<ProjectPart>();
        }

        public int IdPart { get; set; }
        public string PartNumber { get; set; }
        public string Revision { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        // Si quieres agregar esto más adelante
        // public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<ProjectPart> ProjectParts { get; set; } = new HashSet<ProjectPart>();
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.ProjectsModel
{
    public partial class Part
    {
        public Part()
        {
            ProjectParts = new HashSet<ProjectPart>();
        }

        public int IdPart { get; set; }
        public string NoPart { get; set; }
        public int IdCustomer { get; set; }
        public string Revision { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual ICollection<ProjectPart> ProjectParts { get; set; }
    }
}

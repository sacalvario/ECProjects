using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.ProjectsModel
{
    public partial class Customer
    {
        public Customer()
        {
            Parts = new HashSet<Part>();
            Projects = new HashSet<Project>();
        }

        public int IdCustomer { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}

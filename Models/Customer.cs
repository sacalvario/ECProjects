using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Projects = new HashSet<Project>();
            Numberparts = new HashSet<Part>();
        }

        public int IdCustomer { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Part> Numberparts { get; set; }
    }
}

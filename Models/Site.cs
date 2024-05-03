using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Site
    {
        public Site()
        {
            Employees = new HashSet<Employee>();
        }

        public int IdSite { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}

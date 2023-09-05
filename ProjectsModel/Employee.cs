using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.ProjectsModel
{
    public partial class Employee
    {
        public Employee()
        {
            ProjectEmployees = new HashSet<ProjectEmployee>();
            ProjectIdGeneratedbyNavigations = new HashSet<Project>();
            ProjectIdManagerNavigations = new HashSet<Project>();
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int IdDepartament { get; set; }

        public virtual Department IdDepartamentNavigation { get; set; }
        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public virtual ICollection<Project> ProjectIdGeneratedbyNavigations { get; set; }
        public virtual ICollection<Project> ProjectIdManagerNavigations { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}

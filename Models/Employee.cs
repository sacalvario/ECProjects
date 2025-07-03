using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Employee : ViewModelBase
    {
        public Employee()
        {
            ProjectIdGeneratedbyNavigations = new HashSet<Project>();
            ProjectIdManagerNavigations = new HashSet<Project>();
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int IdDepartament { get; set; }
        public sbyte Active { get; set; }
        public int IdSite { get; set; }
        
        public string Name => FirstName + " " + LastName;
        private string _ActiveText;
        public string ActiveText
        {
            get
            {
                if (Active == 1)
                {
                    _ActiveText = "Active";
                }
                else if (Active == 0)
                {
                    _ActiveText = "Inactive";
                }
                return _ActiveText;
            }
            set
            {
                if (_ActiveText != value)
                {
                    _ActiveText = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _IsActive;
        public bool IsActive
        {
            get
            {
                if (Convert.ToBoolean(Active))
                {
                    _IsActive = true;
                }
                else
                {
                    _IsActive = false;
                }
                return _IsActive;
            }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    Active = Convert.ToSByte(_IsActive);
                    RaisePropertyChanged();

                    
                    if (Active == 1)
                    {
                        ActiveText = "Active";
                    }
                    else if (Active == 0)
                    {
                        ActiveText = "Inactive";
                    }
                }
            }
        }

        public virtual Department IdDepartamentNavigation { get; set; }
        public virtual Site IdSiteNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Project> ProjectIdGeneratedbyNavigations { get; set; }
        public virtual ICollection<Project> ProjectIdManagerNavigations { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Project : ViewModelBase
    {
        public Project()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int IdProject { get; set; }
        public int IdGeneratedby { get; set; }
        public int IdCustomer { get; set; }
        public int IdManager { get; set; }
        public string QuoteNumber { get; set; }
        public int ProjectComplexity { get; set; }
        public int IdStatus { get; set; }

        private int _TotalAssembliesInProject;
        public int TotalAssembliesInProject
        {
            get => _TotalAssembliesInProject;
            set
            {
                if (_TotalAssembliesInProject != value)
                {
                    _TotalAssembliesInProject = value;
                    RaisePropertyChanged("TotalAssembliesInProject");
                }
            }
        }
        public DateTime CreationDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CustomerNeedby { get; set; }
        public int TotalEstimatedDuration { get; set; }
        public int SuccesRateEstimate { get; set; }

        public int Year => CreationDate.Year;
        public int Month => CreationDate.Month;
        public string MonthName => CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(CreationDate.Month);
        public int Day => CreationDate.Day;
        public string LongDate => CreationDate.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
        public string LongNeedByDate => CustomerNeedby.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));

        private string _ProjectComplexity;
        public string ProjectComplexityString
        {
            get
            {
                if (ProjectComplexity == 1)
                {
                    _ProjectComplexity = "LOW";
                }
                else if (ProjectComplexity == 2)
                {
                    _ProjectComplexity = "MEDIUM";
                }
                else if (ProjectComplexity == 3)
                {
                    _ProjectComplexity = "HIGH";
                }
                return _ProjectComplexity;
            }
        }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual Employee IdGeneratedbyNavigation { get; set; }
        public virtual Employee IdManagerNavigation { get; set; }
        public virtual Status IdStatusNavigation { get; set; }

        private ICollection<ProjectTask> _ProjectTasks;
        public virtual ICollection<ProjectTask> ProjectTasks
        {
            get => _ProjectTasks;
            set
            {
                if (_ProjectTasks != value)
                {
                    _ProjectTasks = value;
                    RaisePropertyChanged("ProjectTasks");
                }
            }
        }
    }
}

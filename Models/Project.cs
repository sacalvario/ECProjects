using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Project : ViewModelBase
    {
        public Project()
        {
            ProjectTasks = new HashSet<ProjectTask>();
            ProjectParts = new HashSet<ProjectPart>();
        }

        public int IdProject { get; set; }
        public int IdGeneratedby { get; set; }
        public int IdCustomer { get; set; }
        public int IdManager { get; set; }
        public string QuoteNumber { get; set; }
        public string Comments { get; set; }
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

        // Questionnaire fields (Part I)
        public sbyte NewCustomer { get; set; }
        public int AssemblyQuantity { get; set; }
        public sbyte CustomerDrawingAvailable { get; set; }
        public int NewRawMaterialQty { get; set; }
        public sbyte NewTooling { get; set; }
        public sbyte TestingBoard { get; set; }
        public sbyte RoutingBoard { get; set; }
        public sbyte NewMachine { get; set; }
        public sbyte NewMold { get; set; }
        public int CrimpApplication { get; set; }
        public sbyte IsAutomotive { get; set; }
        public int QuestionnairePoints { get; set; }

        public string DisplayProjectId => $"N{IdProject}";

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

        public virtual ICollection<CustomProjectTask> CustomProjectTasks { get; set; } = new List<CustomProjectTask>();


        public virtual ICollection<ProjectPart> ProjectParts { get; set; } = new HashSet<ProjectPart>();
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}

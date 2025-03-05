using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable

namespace ProjectManager.Models
{
    public partial class ProjectTask : ViewModelBase
    {
        public int IdProject { get; set; }
        public int IdTask { get; set; }
        private int _Duration;
        public int Duration
        {
            get => _Duration;
            set
            {
                if (_Duration != value)
                {
                    _Duration = value;
                    RaisePropertyChanged("Duration");
                }
            }
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //private int _IdEmployee;
        public int IdEmployee { get; set; }
        //{
        //    get => _IdEmployee;
        //    set
        //    {
        //        if (_IdEmployee != value)
        //        {
        //            _IdEmployee = value;
        //            RaisePropertyChanged("IdEmployee");
        //        }
        //    }
        //}
        public int IdStatus { get; set; }
        public DateTime? CompletationDate { get; set; }
        public DateTime? ReadyToBuildDate { get; set; }
        public string? Comments { get; set; }

        public string LongStartDate => StartDate.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
        public string LongEndDate => EndDate.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));

        public string CompletationDateString => CompletationDate.HasValue ? CompletationDate?.ToString("D", CultureInfo.CreateSpecificCulture("en-US")) : "";

        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual Project IdProjectNavigation { get; set; }
        public virtual Status IdStatusNavigation { get; set; }
        public virtual Task IdTaskNavigation { get; set; }

        public virtual ICollection<Employee> EmployeeList { get; set; }
    }
}

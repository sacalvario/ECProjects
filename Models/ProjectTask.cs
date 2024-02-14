using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable

namespace ProjectManager.Models
{
    public partial class ProjectTask
    {
        public int IdProject { get; set; }
        public int IdTask { get; set; }
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IdEmployee { get; set; }
        public int? Predecessor { get; set; }
        public int IdStatus { get; set; }

        public string LongStartDate => StartDate.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
        public string LongEndDate => EndDate.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));

        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual Project IdProjectNavigation { get; set; }
        public virtual Status IdStatusNavigation { get; set; }
        public virtual Task IdTaskNavigation { get; set; }
        public virtual Task PredecessorNavigation { get; set; }
    }
}

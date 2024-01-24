using System;
using System.Collections.Generic;

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

        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual Project IdProjectNavigation { get; set; }
        public virtual Status IdStatusNavigation { get; set; }
        public virtual Task IdTaskNavigation { get; set; }
        public virtual Task PredecessorNavigation { get; set; }
    }
}

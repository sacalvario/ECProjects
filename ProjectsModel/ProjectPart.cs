using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectManager.ProjectsModel
{
    public partial class ProjectPart
    {
        public int IdProject { get; set; }
        public int IdPart { get; set; }

        public virtual Part IdPartNavigation { get; set; }
        public virtual Project IdProjectNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Models
{
    public partial class ProjectPart
    {
        public int IdProject { get; set; }
        public int IdPart { get; set; }

        public virtual Project Project { get; set; }
        public virtual Part Part { get; set; }
    }
}

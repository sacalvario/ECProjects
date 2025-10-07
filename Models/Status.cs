using System;
using System.Collections.Generic;
using System.Windows.Media;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Status
    {
        public Status()
        {
            ProjectTasks = new HashSet<ProjectTask>();
            Projects = new HashSet<Project>();
        }

        public int IdStatus { get; set; }
        public string Name { get; set; }

        private SolidColorBrush _StatusColor;
        public SolidColorBrush StatusColor
        {
            get
            {
                if (IdStatus == 1)
                {
                    _StatusColor = new SolidColorBrush(Colors.LimeGreen);
                }
                else if (IdStatus == 2)
                {
                    _StatusColor = new SolidColorBrush(Colors.DarkOrange);
                }
                else if (IdStatus == 3)
                {
                    _StatusColor = new SolidColorBrush(Colors.OrangeRed);
                }
                else if (IdStatus == 4)
                {
                    _StatusColor = new SolidColorBrush(Color.FromRgb(100, 184, 0));
                }
                else if (IdStatus == 5)
                {
                    _StatusColor = new SolidColorBrush(Colors.Red);
                }
                return _StatusColor;
            }
        }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<CustomProjectTask> CustomProjectTasks { get; set; } = new List<CustomProjectTask>();

    }
}

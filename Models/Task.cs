using System;
using System.Collections.Generic;
using System.Windows.Media;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Task
    {
        public Task()
        {
            ProjectTaskId = new HashSet<ProjectTask>();
        }

        public int IdTask { get; set; }
        public string Name { get; set; }
        public int? PredecessorTaskId { get; set; }
        public float? Number { get; set; }

        public virtual ICollection<Task> DependentTasks { get; set; } = new HashSet<Task>();

        private SolidColorBrush _NumberColor;
        public SolidColorBrush NumberColor
        {
            get
            {
                if (Number == 1)
                {
                    _NumberColor = new SolidColorBrush(Color.FromRgb(10, 134, 205));
                }
                else if (IdTask == 2)
                {
                    _NumberColor = new SolidColorBrush(Color.FromRgb(12, 31, 153));
                }
                else if (Number == 2)
                {
                    _NumberColor = new SolidColorBrush(Color.FromRgb(137, 13, 164));
                }
                else if (Number == 3)
                {
                    _NumberColor = new SolidColorBrush(Color.FromRgb(253, 73, 245));
                }
                else if (Number == 4)
                {
                    _NumberColor = new SolidColorBrush(Color.FromRgb(251, 0, 0));
                }
                else if (Number == 5)
                {
                    _NumberColor = new SolidColorBrush(Color.FromRgb(193, 148, 93));
                }
                return _NumberColor;
            }
        }

        public virtual Task PredecessorTask { get; set; }
        public virtual ICollection<ProjectTask> ProjectTaskId { get; set; }
    }
}

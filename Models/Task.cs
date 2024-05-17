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
            ProjectTaskIdTaskNavigations = new HashSet<ProjectTask>();
        }

        public int IdTask { get; set; }
        public string Name { get; set; }
        public float? Predecessor { get; set; }
        public float? Number { get; set; }

        private SolidColorBrush _NumberColor;
        public SolidColorBrush NumberColor
        {
            get
            {
                if (Number == 1)
                {
                    _NumberColor = new SolidColorBrush(Colors.LimeGreen);
                }
                else if (Number == 1.2)
                {
                    _NumberColor = new SolidColorBrush(Colors.DarkOrange);
                }
                else if (Number == 2)
                {
                    _NumberColor = new SolidColorBrush(Colors.OrangeRed);
                }
                else if (Number == 3)
                {
                    _NumberColor = new SolidColorBrush(Color.FromRgb(100, 184, 0));
                }
                else if (Number == 4)
                {
                    _NumberColor = new SolidColorBrush(Colors.Red);
                }
                else if (Number == 5)
                {
                    _NumberColor = new SolidColorBrush(Colors.Red);
                }
                return _NumberColor;
            }
        }

        public virtual ICollection<ProjectTask> ProjectTaskIdTaskNavigations { get; set; }
    }
}

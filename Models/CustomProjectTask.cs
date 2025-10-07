using GalaSoft.MvvmLight;
using System;
using System.Globalization;

namespace ProjectManager.Models
{
    public partial class CustomProjectTask : ViewModelBase
    {
        public int IdCustomTask { get; set; }
        public int ProjectId { get; set; }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (Set(ref _duration, value))
                    DurationChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler DurationChanged;

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    RaisePropertyChanged(nameof(StartDate));
                    RaisePropertyChanged(nameof(LongStartDate));
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    RaisePropertyChanged(nameof(EndDate));
                    RaisePropertyChanged(nameof(LongEndDate));
                }
            }
        }

        public int? IdEmployee { get; set; }
        public int StatusId { get; set; } = 1;
        public string CustomDescription { get; set; }

        public string LongStartDate => StartDate.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
        public string LongEndDate => EndDate.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));

        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual Project Project { get; set; }
        public virtual Status Status { get; set; }
    }
}

using GalaSoft.MvvmLight;
using ProjectManager.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Data;

namespace ProjectManager.ViewModels
{
    public class AdvancedFormatViewModel : ViewModelBase, INavigationAware
    {
        public AdvancedFormatViewModel()
        {
            Options = new ObservableCollection<Option>();
            Groups = new ObservableCollection<Group>()
            {
                 new Group()
                    {
                        GroupId = 1,
                        GroupName = "CSR Review",
                        StartDate = DateTime.Now,
                        FinishDate = DateTime.Now
                    },
                 new Group()
                    {
                        GroupId = 2,
                        GroupName = "Estimating Review",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now
                    },
                 new Group()
                    {
                        GroupId = 3,
                        GroupName = "Engineering Review",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now
                    },
                  new Group()
                    {
                        GroupId = 4,
                        GroupName = "Engineering Review",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now
                    },
                   new Group()
                    {
                        GroupId = 5,
                        GroupName = "QA Review",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now
                    },
                    new Group()
                    {
                        GroupId = 6,
                        GroupName = "Purchasing / Materials Review",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now
                    },
                     new Group()
                    {
                        GroupId = 7,
                        GroupName = "Planning / Production Review",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now
                    },
                      new Group()
                    {
                        GroupId = 8,
                        GroupName = "CSR / Sales Closure",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now
                    },
            };

            Activities = new ObservableCollection<Activity>()
            {
                    new Activity()
                    {
                        ActivityId = 1,
                        ActivityName = "Order is received",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[0]
                    },
                    new Activity()
                    {
                        ActivityId = 2,
                        ActivityName = "Kick New Part Request",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[0]
                    },
                     new Activity()
                    {
                        ActivityId = 3,
                        ActivityName = "Review Set-up information",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[1]
                    },
                       new Activity()
                    {
                        ActivityId = 4,
                        ActivityName = "Provide Set-up information",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[1]
                    },
                         new Activity()
                    {
                        ActivityId = 5,
                        ActivityName = "Tooling Validation",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[2]
                    },
                           new Activity()
                    {
                        ActivityId = 6,
                        ActivityName = "NPI review of BOM",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[2]
                    },
                             new Activity()
                    {
                        ActivityId = 7,
                        ActivityName = "Define QA criteria for EG(s)",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[3]
                    },
                               new Activity()
                    {
                        ActivityId = 8,
                        ActivityName = "Define QA inspection process",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[3]
                    },
                     new Activity()
                    {
                        ActivityId = 9,
                        ActivityName = "Place POs",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[4]
                    },
                      new Activity()
                    {
                        ActivityId = 10,
                        ActivityName = "Material confirmation",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[4]
                    },
                       new Activity()
                    {
                        ActivityId = 11,
                        ActivityName = "Order confirmation",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[5]
                    },
                        new Activity()
                    {
                        ActivityId = 12,
                        ActivityName = "Production floor layout",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[5]
                    },
                         new Activity()
                    {
                        ActivityId = 13,
                        ActivityName = "FA Order shippeed to customer",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[6]
                    },
                          new Activity()
                    {
                        ActivityId = 14,
                        ActivityName = "Customer approval",
                        StartDate = DateTime.Now,
                         FinishDate = DateTime.Now,
                        Group = Groups[6]
                    },
        };


            CvsActivities = new CollectionViewSource();

            CvsActivities.Source = Activities;
            CvsActivities.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

        }

        public partial class Option
        {
            public int OptionId { get; set; }
            public string OptionName { get; set; }

            public Option()
            {

            }
        }


        public partial class Group
        {
            public int GroupId { get; set; }
            public string GroupName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime FinishDate { get; set; }
            public List<Group> Groups;

            public Group()
            {
              
            }
        }


        public partial class Activity
        {
            public int ActivityId { get; set; }
            public string ActivityName { get; set; }
            //public int ActivityDuration { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime FinishDate { get; set; }
            public Option Responsable { get; set; }
            public Group Group { get; set; }

            public List<Activity> Activities;

            public Activity()
            {
                
            }
        }
        private ObservableCollection<Option> _Options;
        public ObservableCollection<Option> Options
        {
            get => _Options;
            set
            {
                if (_Options != value)
                {
                    _Options = value;
                    RaisePropertyChanged("Options");
                }
            }
        }

        private ObservableCollection<Group> _Groups;
        public ObservableCollection<Group> Groups
        {
            get => _Groups;
            set
            {
                if (_Groups != value)
                {
                    _Groups = value;
                    RaisePropertyChanged("Groups");
                }
            }
        }

        private ObservableCollection<Activity> _Activities;
        public ObservableCollection<Activity> Activities
        {
            get => _Activities;
            set
            {
                if (_Activities != value)
                {
                    _Activities = value;
                    RaisePropertyChanged("Activities");
                }
            }
        }

        private CollectionViewSource _CvsActivities;
        public CollectionViewSource CvsActivities
        {
            get => _CvsActivities;
            set
            {
                if (_CvsActivities != value)
                {
                    _CvsActivities = value;
                    RaisePropertyChanged("CvsActivities");
                }
            }
        }

        public void OnNavigatedTo(object parameter)
        {
        }

        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }
    }
}

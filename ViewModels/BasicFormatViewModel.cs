using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ProjectManager.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class BasicFormatViewModel : ViewModelBase, INavigationAware
    {
        //private readonly INavigationService _navigationService;

        public BasicFormatViewModel()
        {
            //_navigationService = navigationService;

            Options = new ObservableCollection<Option>();
            Activities = new ObservableCollection<Activity>()
            {
                    new Activity()
                    {
                        ActivityId = 1,
                        ActivityName = "New Assembly set-up",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                    new Activity()
                    {
                        ActivityId = 2,
                        ActivityName = "Tooling/Bom validation",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                     new Activity()
                    {
                        ActivityId = 3,
                        ActivityName = "Order is entered",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                       new Activity()
                    {
                        ActivityId = 4,
                        ActivityName = "Material confirmation",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                         new Activity()
                    {
                        ActivityId = 5,
                        ActivityName = "Tooling Validation",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                           new Activity()
                    {
                        ActivityId = 6,
                        ActivityName = "Engineering documentation",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                             new Activity()
                    {
                        ActivityId = 7,
                        ActivityName = "QA validation",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                               new Activity()
                    {
                        ActivityId = 8,
                        ActivityName = "Ready to build",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                     new Activity()
                    {
                        ActivityId = 9,
                        ActivityName = "Order is confirmed",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                      new Activity()
                    {
                        ActivityId = 10,
                        ActivityName = "Material confirmation",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                       new Activity()
                    {
                        ActivityId = 11,
                        ActivityName = "Order confirmation",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                        new Activity()
                    {
                        ActivityId = 12,
                        ActivityName = "Production floor layout",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                         new Activity()
                    {
                        ActivityId = 13,
                        ActivityName = "FA Order shippeed to customer",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
                          new Activity()
                    {
                        ActivityId = 14,
                        ActivityName = "Customer approval",
                        StartDate = DateTime.Now,
                        ActivityDuration = 1,
                         FinishDate = DateTime.Now.AddDays(1)
                    },
        };
        }

        public partial class Option
        {
            public int OptionId { get; set; }
            public string OptionName { get; set; }

            public Option()
            {
                
            }
        }

        public partial class Activity
        {
            public int ActivityId { get; set; }
            public string ActivityName { get; set; }
            public int ActivityDuration { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime FinishDate { get; set; }
            public Option Responsable { get; set; }

            public Activity()
            {

            }
        }

        private ICommand _AddCommand;
        public ICommand AddCommand => _AddCommand ??= new RelayCommand(AddControl);

        private ICommand _AddActivity;
        public ICommand AddActivityCommand => _AddActivity ??= new RelayCommand(AddActivity);

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

        private void AddControl()
        {
            Options.Add(new Option());
        }

        private void AddActivity()
        {
            Activities.Add(new Activity());
        }


        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(object parameter)
        {
            //throw new NotImplementedException();
        }
    }
}

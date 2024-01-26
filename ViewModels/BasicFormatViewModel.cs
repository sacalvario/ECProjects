using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class BasicFormatViewModel : ViewModelBase, INavigationAware
    {
        private readonly Contracts.Services.INavigationService _navigationService;
        private readonly IProjectsDataService _projectsDataService;

        private Project _Project;
        public Project Project
        {
            get => _Project;
            set
            {
                if (_Project != value)
                {
                    _Project = value;
                    RaisePropertyChanged("Project");
                }
            }
        }

        public BasicFormatViewModel(Contracts.Services.INavigationService navigationService, IProjectsDataService projectsDataService)
        {
            _navigationService = navigationService;
            _projectsDataService = projectsDataService;

            Task6DurationDays = 10;
            Project = new Project();
           
            GetTasks();
            CreateTasks();
        //    Options = new ObservableCollection<Option>();
        //    Activities = new ObservableCollection<Activity>()
        //    {
        //            new Activity()
        //            {
        //                ActivityId = 1,
        //                ActivityName = "New Assembly set-up",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //            new Activity()
        //            {
        //                ActivityId = 2,
        //                ActivityName = "Tooling/Bom validation",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //             new Activity()
        //            {
        //                ActivityId = 3,
        //                ActivityName = "Order is entered",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //               new Activity()
        //            {
        //                ActivityId = 4,
        //                ActivityName = "Material confirmation",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //                 new Activity()
        //            {
        //                ActivityId = 5,
        //                ActivityName = "Tooling Validation",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //                   new Activity()
        //            {
        //                ActivityId = 6,
        //                ActivityName = "Engineering documentation",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //                     new Activity()
        //            {
        //                ActivityId = 7,
        //                ActivityName = "QA validation",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //                       new Activity()
        //            {
        //                ActivityId = 8,
        //                ActivityName = "Ready to build",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //             new Activity()
        //            {
        //                ActivityId = 9,
        //                ActivityName = "Order is confirmed",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //              new Activity()
        //            {
        //                ActivityId = 10,
        //                ActivityName = "Material confirmation",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //               new Activity()
        //            {
        //                ActivityId = 11,
        //                ActivityName = "Order confirmation",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //                new Activity()
        //            {
        //                ActivityId = 12,
        //                ActivityName = "Production floor layout",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //                 new Activity()
        //            {
        //                ActivityId = 13,
        //                ActivityName = "FA Order shippeed to customer",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //                  new Activity()
        //            {
        //                ActivityId = 14,
        //                ActivityName = "Customer approval",
        //                StartDate = DateTime.Now,
        //                ActivityDuration = 1,
        //                 FinishDate = DateTime.Now.AddDays(1)
        //            },
        //};
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

        private ObservableCollection<Task> _Tasks;
        public ObservableCollection<Task> Tasks
        {
            get => _Tasks;
            set
            {
                if (_Tasks != value)
                {
                    _Tasks = value;
                    RaisePropertyChanged("Tasks");
                }
            }
        }

        private int _TypeProject;
        public int TypeProject
        {
            get => _TypeProject;
            set
            {
                if (_TypeProject != value)
                {
                    _TypeProject = value;
                    RaisePropertyChanged("TypeProject");
                }
            }
        }

        private int _Task6DurationDays;
        public int Task6DurationDays
        {
            get => _Task6DurationDays;
            set
            {
                if (_Task6DurationDays != value)
                {
                    _Task6DurationDays = value;
                    RaisePropertyChanged("Task6DurationDays");
                }
            }
        }

        private void CreateTasks()
        {
            Project.ProjectTasks = new ObservableCollection<ProjectTask>()
            {
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[0],
                       Duration = 3,
                       StartDate = DateTime.Now.AddDays(5),
                       EndDate = DateTime.Now.AddDays(8)
                    },
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[1],
                       Duration = 3,
                       StartDate = DateTime.Now.AddDays(8),
                       EndDate = DateTime.Now.AddDays(11),
                       Predecessor = 1
                    },
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[2],
                       Duration = 1,
                       StartDate = DateTime.Now.AddDays(11),
                       EndDate = DateTime.Now.AddDays(12),
                       Predecessor = 2
                    },
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[3],
                       Duration = 7,
                       StartDate = DateTime.Now.AddDays(12),
                       EndDate = DateTime.Now.AddDays(19),
                       Predecessor = 3
                    },
                  new ProjectTask
                    {
                       IdTaskNavigation = Tasks[4],
                       Duration = 5,
                       StartDate = DateTime.Now.AddDays(19),
                       EndDate = DateTime.Now.AddDays(24),
                       Predecessor = 3
                    },
                    new ProjectTask
                    {
                       IdTaskNavigation = Tasks[5],
                       Duration =  Task6DurationDays,
                       StartDate = DateTime.Now.AddDays(24),
                       EndDate = DateTime.Now.AddDays(Task6DurationDays),
                       Predecessor = 3
                    },
                     new ProjectTask
                    {
                       IdTaskNavigation = Tasks[6],
                       Duration =  5,
                       StartDate = DateTime.Now.AddDays(Task6DurationDays),
                       EndDate = DateTime.Now.AddDays(29),
                       Predecessor = 3
                    },
                      new ProjectTask
                    {
                       IdTaskNavigation = Tasks[7],
                       Duration =  2,
                       StartDate = DateTime.Now.AddDays(29),
                       EndDate = DateTime.Now.AddDays(31),
                       Predecessor = 4
                    }
            };
        }

        private async void GetTasks()
        {
            Tasks = new ObservableCollection<Task>();

            var data = await _projectsDataService.GetTasksAsync();

            foreach (var item in data)
            {
                Tasks.Add(item);
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

           if (parameter is int points)
            {
                if (points <= 3)
                {
                    TypeProject = 1;
                    Task6DurationDays = 10;
                }
                else if (points > 3 && points < 8)
                {
                    TypeProject = 2;
                    Task6DurationDays = 15;
                }
                else
                {
                    TypeProject = 3;
                    Task6DurationDays = 20;
                }
            }
        }
    }
}

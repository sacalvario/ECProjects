﻿using GalaSoft.MvvmLight;
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
        private readonly IWindowManagerService _windowManagerService;

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

        public BasicFormatViewModel(Contracts.Services.INavigationService navigationService, IProjectsDataService projectsDataService, IWindowManagerService windowManagerService)
        {
            _navigationService = navigationService;
            _projectsDataService = projectsDataService;
            _windowManagerService = windowManagerService;

            Project = new Project() 
            {
                CustomerNeedby = DateTime.Now
            };

            GetCustomers();
            GetEmployees();
            GetTasks();
            CreateTasks();
       
        }


        private ICommand _AddCommand;
        public ICommand AddCommand => _AddCommand ??= new RelayCommand(AddControl);

        private ICommand _AddProject;
        public ICommand AddProjectCommand => _AddProject ??= new RelayCommand(AddProject);


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

        private ObservableCollection<Customer> _Customers;
        public ObservableCollection<Customer> Customers
        {
            get => _Customers;
            set
            {
                if (_Customers != value)
                {
                    _Customers = value;
                    RaisePropertyChanged("Customers");
                }
            }
        }
        
        private ObservableCollection<Employee> _Employees;
        public ObservableCollection<Employee> Employees
        {
            get => _Employees;
            set
            {
                if (_Employees != value)
                {
                    _Employees = value;
                    RaisePropertyChanged("Employees");
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
        
        private int _Points;
        public int Points
        {
            get => _Points;
            set
            {
                if (_Points != value)
                {
                    _Points = value;
                    RaisePropertyChanged("Points");
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

        private DateTime WorkDays(int days)
        {
            DateTime date = new DateTime();
            date = DateTime.Now;

            for (int i = 1; i <= days; i++)
            {
                if (DateTime.Now.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                {
                    days++;
                }
                else if (DateTime.Now.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    days++;
                }
            }

            return date.AddDays(days);

        }

        private void CreateTasks()
        {
            Project.ProjectTasks = new ObservableCollection<ProjectTask>()
            {
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[0],
                       Duration = 3,
                       StartDate = WorkDays(3),
                       EndDate = WorkDays(6),
                       IdStatus = 5
                    },
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[1],
                       Duration = 3,
                       StartDate = WorkDays(6),
                       EndDate = WorkDays(9),
                       Predecessor = 1,
                       IdStatus = 5
                    },
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[2],
                       Duration = 1,
                       StartDate = DateTime.Now.AddDays(7),
                       EndDate = DateTime.Now.AddDays(8),
                       Predecessor = 2,
                       IdStatus = 5
                    },
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[3],
                       Duration = 7,
                       StartDate = DateTime.Now.AddDays(12),
                       EndDate = DateTime.Now.AddDays(19),
                       Predecessor = 3,
                       IdStatus = 5
                    },
                  new ProjectTask
                    {
                       IdTaskNavigation = Tasks[4],
                       Duration = 5,
                       StartDate = DateTime.Now.AddDays(19),
                       EndDate = DateTime.Now.AddDays(24),
                       Predecessor = 3,
                       IdStatus = 5
                    },
                    new ProjectTask
                    {
                       IdTaskNavigation = Tasks[5],
                       Duration =  Task6DurationDays,
                       StartDate = DateTime.Now.AddDays(24),
                       EndDate = DateTime.Now.AddDays(Task6DurationDays),
                       Predecessor = 3,
                       IdStatus = 5
                    },
                     new ProjectTask
                    {
                       IdTaskNavigation = Tasks[6],
                       Duration =  5,
                       StartDate = DateTime.Now.AddDays(Task6DurationDays),
                       EndDate = DateTime.Now.AddDays(29),
                       Predecessor = 3,
                       IdStatus = 5
                    },
                      new ProjectTask
                    {
                       IdTaskNavigation = Tasks[7],
                       Duration =  2,
                       StartDate = DateTime.Now.AddDays(29),
                       EndDate = DateTime.Now.AddDays(31),
                       Predecessor = 4,
                       IdStatus = 5
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
        
        private async void GetCustomers()
        {
            Customers = new ObservableCollection<Customer>();

            var data = await _projectsDataService.GetCustomersAsync();

            foreach (var item in data)
            {
                Customers.Add(item);
            }
        }
        
        private async void GetEmployees()
        {
            Employees = new ObservableCollection<Employee>();

            var data = await _projectsDataService.GetEmployeesAsync();

            foreach (var item in data)
            {
                item.IdDepartamentNavigation = await _projectsDataService.GetDepartmentAsync(item.IdDepartament);
                Employees.Add(item);
            }
        }

        private void AddControl()
        {
        }

        private void AddProject()
        {
            Project.IdGeneratedby = UserRecord.Employee_ID;
            Project.ProjectComplexity = TypeProject;
            Project.IdStatus = 2;
            Project.CreationDate = DateTime.Now;
            Project.EndDate = DateTime.Now;
            Project.TotalEstimatedDuration = 1;
            Project.SuccesRateEstimate = 1;

            try
            {
                if (_projectsDataService.SaveProject(Project))
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ApplyMessageViewModel).FullName, Project.IdProject);
                }

            }
            catch (Exception ex)
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al registrar - " + ex.ToString());
            }


        }


        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(object parameter)
        {
           if (parameter is int points)
            {
                Points = new int();
                Points = points;
                

                if (Points <= 3)
                {
                    TypeProject = 1;
                    Task6DurationDays = 10;
                }
                else if (Points > 3 && Points < 8)
                {
                    TypeProject = 2;
                    Task6DurationDays = 15;
                }
                else
                {
                    TypeProject = 3;
                    Task6DurationDays = 20;
                }
                
                _ = _windowManagerService.OpenInDialog(typeof(ApplyMessageViewModel).FullName, Points);
            }

        }
    }
}

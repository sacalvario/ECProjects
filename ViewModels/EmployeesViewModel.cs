using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ProjectManager.Contracts.Services;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace ProjectManager.ViewModels
{
    public class EmployeesViewModel : ViewModelBase
    {
        public readonly IProjectsDataService _projectsDataService;
        public readonly IWindowManagerService _windowManagerService;
        private readonly INavigationService _navigationService;

        private ICommand _navigateToTasksListCommand;
        public ICommand NavigateToTasksListCommand => _navigateToTasksListCommand ??= new RelayCommand<Employee>(NavigateToTasksList);

        public EmployeesViewModel(IProjectsDataService projectsDataService, IWindowManagerService windowManagerService, INavigationService navigationService)
        {
            _projectsDataService = projectsDataService;
            _windowManagerService = windowManagerService;
            _navigationService = navigationService;

            Employees = new ObservableCollection<Employee>();
            GetEmployees();

            CvsEmployees = new CollectionViewSource
            {
                Source = Employees
            };

            CvsEmployees.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            CvsEmployees.Filter += ApplyFilter;

            if (UserRecord.Employee_ID == 92 || UserRecord.Employee_ID == 212)
            {
                AdminEmployeeBtnsVisibility = Visibility.Visible;
            }

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CvsEmployees.View.Refresh();

        }

        private ICommand _OpenEmployeeManageWindowCommand;
        public ICommand OpenEmployeeManageWindowCommand
        {
            get
            {
                if (_OpenEmployeeManageWindowCommand == null)
                {
                    _OpenEmployeeManageWindowCommand = new RelayCommand<Employee>(OpenEmployeeManageWindow);
                }
                return _OpenEmployeeManageWindowCommand;
            }
        }

        private Visibility _AdminEmployeeBtnsVisibility = Visibility.Collapsed;
        public Visibility AdminEmployeeBtnsVisibility
        {
            get => _AdminEmployeeBtnsVisibility;
            set
            {
                if (_AdminEmployeeBtnsVisibility != value)
                {
                    _AdminEmployeeBtnsVisibility = value;
                    RaisePropertyChanged("AdminEmployeeBtnsVisibility");
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

        private async void GetEmployees()
        {
            var data = await _projectsDataService.GetEmployeesAsync();

            foreach (var item in data)
            {
                item.IdDepartamentNavigation = await _projectsDataService.GetDepartmentAsync(item.IdDepartament);
                Employees.Add(item);
            }
        }

        private void NavigateToTasksList(Employee employee)
        {
            if (employee != null)
            {
                _navigationService.NavigateTo(typeof(TasksViewModel).FullName, employee);
            }
        }

        internal static CollectionViewSource CvsEmployees { get; set; }
        public static ICollectionView EmployeeCollection => CvsEmployees.View;

        private string filter;
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnFilterChanged();
            }

        }
        private void OnFilterChanged()
        {
            CvsEmployees.View.Refresh();
        }

        private void ApplyFilter(object sender, FilterEventArgs e)
        {
            Employee er = (Employee)e.Item;

            e.Accepted = string.IsNullOrWhiteSpace(Filter) || Filter.Length == 0 || er.Name.ToLower().Contains(Filter.ToLower());
        }

        private void OpenEmployeeManageWindow(Employee Employee)
        {
            Messenger.Default.Send(new NotificationMessage<Employee>(Employee, "ShowManageEmployeeWindow"));
        }

    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

namespace ProjectManager.ViewModels
{
    public class EmployeesViewModel : ViewModelBase
    {
        public readonly IProjectsDataService _projectsDataService;
        public readonly IWindowManagerService _windowManagerService;

        public EmployeesViewModel(IProjectsDataService projectsDataService, IWindowManagerService windowManagerService)
        {

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
    }
}

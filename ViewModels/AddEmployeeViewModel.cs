using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectManager.Contracts.Services;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class AddEmployeeViewModel : ViewModelBase
    {

        private readonly IProjectsDataService _ecnDataService;
        private readonly IWindowManagerService _windowManagerService;

        private ObservableCollection<Department> _Departments;
        public ObservableCollection<Department> Departments
        {
            get => _Departments;
            set
            {
                if (_Departments != value)
                {
                    _Departments = value;
                    RaisePropertyChanged("Departments");
                }
            }
        }

        private Employee _Employee;
        public Employee Employee
        {
            get => _Employee;
            set
            {
                if (_Employee != value)
                {
                    _Employee = value;
                    RaisePropertyChanged("Employee");
                }
            }
        }

        private Visibility _UpdateEmployeeVisibility;
        public Visibility UpdateEmployeeVisibility
        {
            get => _UpdateEmployeeVisibility;
            set
            {
                if (_UpdateEmployeeVisibility != value)
                {
                    _UpdateEmployeeVisibility = value;
                    RaisePropertyChanged("UpdateEmployeeVisibility");
                }
            }
        }

        private Visibility _AddEmployeeVisibility;
        public Visibility AddEmployeeVisibility
        {
            get => _AddEmployeeVisibility;
            set
            {
                if (_AddEmployeeVisibility != value)
                {
                    _AddEmployeeVisibility = value;
                    RaisePropertyChanged("AddEmployeeVisibility");
                }
            }
        }

        private ICommand _UpgradeEmployeeCommand;
        public ICommand UpgradeEmployeeCommand
        {
            get
            {
                if (_UpgradeEmployeeCommand == null)
                {
                    _UpgradeEmployeeCommand = new RelayCommand(UpgradeEmployee);
                }
                return _UpgradeEmployeeCommand;
            }
        }

        private ICommand _AddEmployeeCommand;
        public ICommand AddEmployeeCommand
        {
            get
            {
                if (_AddEmployeeCommand == null)
                {
                    _AddEmployeeCommand = new RelayCommand(AddEmployee);
                }
                return _AddEmployeeCommand;
            }
        }

        private bool _IsEdition;
        public bool IsEdition
        {
            get => _IsEdition;
            set
            {
                if (_IsEdition != value)
                {
                    _IsEdition = value;
                    RaisePropertyChanged("IsEdition");
                }
            }
        }

        public AddEmployeeViewModel(Employee employee, IProjectsDataService ecnDataService, IWindowManagerService windowManagerService)
        {
            _ecnDataService = ecnDataService;
            _windowManagerService = windowManagerService;
            //_employeesPageVM = SimpleIoc.Default.GetInstance<EmployeesPageViewModel>(Guid.NewGuid().ToString());
            Employee = employee;

            if (Employee == null)
            {
                UpdateEmployeeVisibility = Visibility.Collapsed;
                AddEmployeeVisibility = Visibility.Visible;
                Employee = new Employee();
            }
            else
            {
                UpdateEmployeeVisibility = Visibility.Visible;
                AddEmployeeVisibility = Visibility.Collapsed;
                IsEdition = true;
            }

            GetDepartments();
        }

        private async void GetDepartments()
        {
            Departments = new ObservableCollection<Department>();

            var data = await _ecnDataService.GetDepartmentsAsync();
            foreach (var item in data)
            {
                Departments.Add(item);
            }
        }

        private void ResetData()
        {
            Employee = new Employee();
        }

        private void UpgradeEmployee()
        {
            if (Employee.FirstName != null && Employee.LastName != null && Employee.Email != null)
            {
                try
                {
                    if (_ecnDataService.UpgradeEmployee(Employee))
                    {
                        _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se actualizo la información del empleado correctamente.");
                    }
                }
                catch (Exception ex)
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al editar empleado - " + ex.ToString());
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Llena todo los campos.");
            }
        }

        private void AddEmployee()
        {
            if (Employee.IdEmployee != 0 && Employee.FirstName != null && Employee.LastName != null && Employee.Email != null && Employee.IdDepartamentNavigation != null)
            {
                try
                {
                    if (_ecnDataService.AddEmployee(Employee))
                    {
                        _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Se añadio el empleado correctamente.");
                        //_employeesPageVM.RefreshList();
                        //_employeesPageVM.Cleanup();
                        ViewModelLocator.UnregisterEmployeesPageViewModel();
                        ResetData();
                    }

                }
                catch (Exception ex)
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al añadir empleado - " + ex.ToString());
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Llena todo los campos.");
            }
        }
    }
}


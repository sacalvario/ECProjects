using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectManager.Contracts.Services;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class AddCustomerViewModel : ViewModelBase
    {
        private readonly IProjectsDataService _projectsDataService;
        private readonly IWindowManagerService _windowManagerService;

        public AddCustomerViewModel(IProjectsDataService numberPartsDataService, IWindowManagerService windowManagerService)
        {
            _projectsDataService = numberPartsDataService;
            _windowManagerService = windowManagerService;
            Customer = new Customer();
        }

        private Customer _Customer;
        public Customer Customer
        {
            get => _Customer;
            set
            {
                if (_Customer != value)
                {
                    _Customer = value;
                    RaisePropertyChanged("Customer");
                }
            }
        }

        private ICommand _AddCustomerCommand;
        public ICommand AddCustomerCommand
        {
            get
            {
                if (_AddCustomerCommand == null)
                {
                    _AddCustomerCommand = new RelayCommand(AddCustomer);
                }
                return _AddCustomerCommand;
            }
        }

        private void AddCustomer()
        {
            if (Customer.Name != null)
            {
                try
                {
                    if (_projectsDataService.AddCustomer(Customer))
                    {
                        _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "The customer was added correctly.");
                        ResetData();
                    }

                }
                catch (Exception ex)
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error when adding customer - " + ex.ToString());
                }
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "The field is required to be filled out.");
            }
        }

        private void ResetData()
        {
            Customer = new Customer();
        }
    }
}

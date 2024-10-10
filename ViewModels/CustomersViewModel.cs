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
using System.Windows.Data;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class CustomersViewModel : ViewModelBase
    {
        public readonly IProjectsDataService _projectsDataService;
        public readonly IWindowManagerService _windowManagerService;
        private readonly INavigationService _navigationService;

        public CustomersViewModel(IProjectsDataService projectsDataService, IWindowManagerService windowManagerService, INavigationService navigationService)
        {
            _projectsDataService = projectsDataService;
            _windowManagerService = windowManagerService; 
            _navigationService = navigationService;

            Customers = new ObservableCollection<Customer>();
            GetEmployees();

            CvsCustomers = new CollectionViewSource
            {
                Source = Customers
            };

            CvsCustomers.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            CvsCustomers.Filter += ApplyFilter;
        }

        private ICommand _OpenCustomerManageWindowCommand;
        public ICommand OpenCustomerManageWindowCommand
        {
            get
            {
                if (_OpenCustomerManageWindowCommand == null)
                {
                    _OpenCustomerManageWindowCommand = new RelayCommand(OpenCustomerManagerWindow);
                }
                return _OpenCustomerManageWindowCommand;
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

        private async void GetEmployees()
        {
            var data = await _projectsDataService.GetCustomersAsync();

            foreach (var item in data)
            {
                Customers.Add(item);
            }
        }

        internal static CollectionViewSource CvsCustomers { get; set; }
        public static ICollectionView CustomersCollection => CvsCustomers.View;

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
            CvsCustomers.View.Refresh();
        }

        private void ApplyFilter(object sender, FilterEventArgs e)
        {
            Customer er = (Customer)e.Item;

            e.Accepted = string.IsNullOrWhiteSpace(Filter) || Filter.Length == 0 || er.Name.ToLower().Contains(Filter.ToLower());
        }

        private void OpenCustomerManagerWindow()
        {
            Messenger.Default.Send(new NotificationMessage("ShowManageCustomerWindow"));
        }
    }
}

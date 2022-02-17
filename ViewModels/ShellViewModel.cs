﻿using System;
using System.Windows.Input;

using ECN.Contracts.Services;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using ModernWpf.Controls;

namespace ECN.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private NavigationViewItem _selectedMenuItem;
        private RelayCommand _goBackCommand;
        private ICommand _menuItemInvokedCommand;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;

        public string Name => UserRecord.Employee.EmployeeFirstName + " " + UserRecord.Employee.EmployeeLastName;
        public string Department => UserRecord.Employee.Department.DepartmentName;

        public NavigationViewItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { Set(ref _selectedMenuItem, value); }
        }

        public RelayCommand GoBackCommand => _goBackCommand ??= new RelayCommand(OnGoBack, CanGoBack);

        public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ??= new RelayCommand(OnMenuItemInvoked);

        public ICommand LoadedCommand => _loadedCommand ??= new RelayCommand(OnLoaded);

        public ICommand UnloadedCommand => _unloadedCommand ??= new RelayCommand(OnUnloaded);

        public ShellViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //SelectedMenuItem = new NavigationViewItem();
        }

        private void OnLoaded()
        {
            _navigationService.Navigated += OnNavigated;
        }

        private void OnUnloaded()
        {
            _navigationService.Navigated -= OnNavigated;
        }

        private bool CanGoBack()
            => _navigationService.CanGoBack;

        private void OnGoBack()
            => _navigationService.GoBack();

        private void OnMenuItemInvoked()
            => NavigateTo(SelectedMenuItem.SetTargetPageType());

        private void NavigateTo(Type targetViewModel)
        {
            if (targetViewModel != null)
            {
                _navigationService.NavigateTo(targetViewModel.FullName);
            }
        }


        private void OnNavigated(object sender, string viewModelName)
        {
            //    var item = MenuItems
            //                .OfType<NavigationViewItem>()
            //                .FirstOrDefault(i => viewModelName == i.SetTargetPageType().FullName);
            //    if (item != null)
            //    {
            //        SelectedMenuItem = item;
            //    }

            //    GoBackCommand.RaiseCanExecuteChanged();
        }
    }
}

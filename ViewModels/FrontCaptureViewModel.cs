using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.Views;
using ProjectManager.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ProjectManager.Contracts.ViewModels;

namespace ProjectManager.ViewModels
{
    public class FrontCaptureViewModel : ViewModelBase, INavigationAware
    {

        private readonly IWindowManagerService _windowManagerService;
        private readonly INavigationService _navigationService;

        private ICommand _navigateBasicFormatCommand;
        public ICommand NavigateToBasicCommand => _navigateBasicFormatCommand ??= new RelayCommand(NavigateToBasicFormat);


        private bool _MultiOn = false;
        public bool MultiOn
        {
            get => _MultiOn;
            set
            {
                if (_MultiOn != value)
                {
                    _MultiOn = value;
                    RaisePropertyChanged("MultiOn");

                    if (_MultiOn)
                    {
                        MultiEnsambleVisibility = Visibility.Visible;
                        SingleEnsambleVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        SingleEnsambleVisibility = Visibility.Visible;
                        MultiEnsambleVisibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private bool _MoreThanTen;
        public bool MoreThanTen
        {
            get => _MoreThanTen;
            set
            {
                if (_MoreThanTen != value)
                {
                    _MoreThanTen = value;
                    RaisePropertyChanged("MoreThanTen");

                    
                }
            }
        }

        private bool _SingleMoreThanTen;
        public bool SingleMoreThanTen
        {
            get => _SingleMoreThanTen;
            set
            {
                if (_SingleMoreThanTen != value)
                {
                    _SingleMoreThanTen = value;
                    RaisePropertyChanged("SingleMoreThanTen");
                }
            }
        }

        private bool _VariationExisting = false;
        public bool VariationExisting
        {
            get => _VariationExisting;
            set
            {
                if (_VariationExisting != value)
                {
                    _VariationExisting = value;
                    RaisePropertyChanged("VariationExisting");

                    if (_VariationExisting)
                    {
                        ItemsVisibility = Visibility.Visible;
                    }
                    else
                    {
                        ItemsVisibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private bool _NewCustomer = false;
        public bool NewCustomer
        {
            get => _NewCustomer;
            set
            {
                if (_NewCustomer != value)
                {
                    _NewCustomer = value;
                    RaisePropertyChanged("NewCustomer");

                    if (!_NewCustomer)
                    {
                        VariationExistingVisibility = Visibility.Visible;
                        ItemsVisibility = Visibility.Collapsed;

                    }
                    else
                    {
                        VariationExistingVisibility = Visibility.Collapsed;
                        ItemsVisibility = Visibility.Visible;
                    }
                }
            }
        }

        private Visibility _MultiEnsambleVisibility = Visibility.Collapsed;
        public Visibility MultiEnsambleVisibility
        {
            get => _MultiEnsambleVisibility;
            set
            {
                if (_MultiEnsambleVisibility != value)
                {
                    _MultiEnsambleVisibility = value;
                    RaisePropertyChanged("MultiEnsambleVisibility");
                }
            }
        }

        private Visibility _SingleEnsambleVisibility = Visibility.Collapsed;
        public Visibility SingleEnsambleVisibility
        {
            get => _SingleEnsambleVisibility;
            set
            {
                if (_SingleEnsambleVisibility != value)
                {
                    _SingleEnsambleVisibility = value;
                    RaisePropertyChanged("SingleEnsambleVisibility");
                }
            }
        }

        private Visibility _ItemsVisibility = Visibility.Collapsed;
        public Visibility ItemsVisibility
        {
            get => _ItemsVisibility;
            set
            {
                if (_ItemsVisibility != value)
                {
                    _ItemsVisibility = value;
                    RaisePropertyChanged("ItemsVisibility");
                }
            }
        }

        private Visibility _VariationExistingVisibility = Visibility.Collapsed;
        public Visibility VariationExistingVisibility
        {
            get => _VariationExistingVisibility;
            set
            {
                if (_VariationExistingVisibility != value)
                {
                    _VariationExistingVisibility = value;
                    RaisePropertyChanged("VariationExistingVisibility");
                }
            }
        }

     
        public FrontCaptureViewModel(IWindowManagerService windowManagerService, INavigationService navigationService)
        {
            _windowManagerService = windowManagerService;
            _navigationService = navigationService;

        }

        private void NavigateToBasicFormat()
        {
             _navigationService.NavigateTo(typeof(BasicFormatViewModel).FullName);
        }

        public void OnNavigatedTo(object parameter)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }
    }
}

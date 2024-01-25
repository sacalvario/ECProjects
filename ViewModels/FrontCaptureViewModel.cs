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

        private bool _NewCustomer = false;
        public bool NewCustomer
        {
            get => _NewCustomer;
            set
            {
                if (_NewCustomer != value)
                {
                    if (_NewCustomer)
                    {
                        Points++;
                    }
                    else if (Points > 0)
                    {
                        Points--;
                    }

                    _NewCustomer = value;
                    RaisePropertyChanged("NewCustomer");
                }
            }
        }

        private int _AssemblyQuantity;
        public int AssemblyQuantity
        {
            get => _AssemblyQuantity;
            set
            {
                if (_AssemblyQuantity != value)
                {
                    if (_AssemblyQuantity > 9 && _AssemblyQuantity < 20)
                    {
                        Points++;
                    }
                    else if (_AssemblyQuantity > 20)
                    {
                        Points += 2;
                    }
                    _AssemblyQuantity = value;
                    RaisePropertyChanged("AssemblyQuantity");
                }
            }
        }

        private bool _CustomerDrawingAvailable;
        public bool CustomerDrawingAvailable
        {
            get => _CustomerDrawingAvailable;
            set
            {
                if (_CustomerDrawingAvailable != value)
                {
                    _CustomerDrawingAvailable = value;
                    RaisePropertyChanged("CustomerDrawingAvailable");
                }
            }
        }

        private string _NewRawMaterialQty;
        public string NewRawMaterialQty
        {
            get => _NewRawMaterialQty;
            set
            {
                if (_NewRawMaterialQty != value)
                {
                    _NewRawMaterialQty = value;
                    RaisePropertyChanged("NewRawMaterialQty");
                }
            }
        }

        private bool _NewTooling;
        public bool NewTooling
        {
            get => _NewTooling;
            set
            {
                if (_NewTooling != value)
                {
                    _NewTooling = value;
                    RaisePropertyChanged("NewTooling");
                }
            }
        }

        private bool _TestingBoard;
        public bool TestingBoard
        {
            get => _TestingBoard;
            set
            {
                if (_TestingBoard != value)
                {
                    _TestingBoard = value;
                    RaisePropertyChanged("TestingBoard");
                }
            }
        }

        private bool _RoutingBoard;
        public bool RoutingBoard
        {
            get => _RoutingBoard;
            set
            {
                if (_RoutingBoard != value)
                {
                    _RoutingBoard = value;
                    RaisePropertyChanged("RoutingBoard");
                }
            }
        }

        private bool _NewMachine;
        public bool NewMachine
        {
            get => _NewMachine;
            set
            {
                if (_NewMachine != value)
                {
                    _NewMachine = value;
                    RaisePropertyChanged("NewMachine");
                }
            }
        }

        private bool _NewMold;
        public bool NewMold
        {
            get => _NewMold;
            set
            {
                if (_NewMold != value)
                {
                    _NewMold = value;
                    RaisePropertyChanged("NewMold");
                }
            }
        }

        private string _CrimpApplication;
        public string CrimpApplication
        {
            get => _CrimpApplication;
            set
            {
                if (_CrimpApplication != value)
                {
                    _CrimpApplication = value;
                    RaisePropertyChanged("CrimpApplication");
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

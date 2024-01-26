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
                    else if (_AssemblyQuantity >= 20)
                    {
                        Points += 2;
                    }
                    else if (_AssemblyQuantity <= 9 )
                    {
                        if (Points > 0)
                        {
                            Points--;
                        }
                    }
                    _AssemblyQuantity = value;
                    RaisePropertyChanged("AssemblyQuantity");
                }
            }
        }

        private bool _CustomerDrawingAvailable = false;
        public bool CustomerDrawingAvailable
        {
            get => _CustomerDrawingAvailable;
            set
            {
                if (_CustomerDrawingAvailable != value)
                {
                    if (_CustomerDrawingAvailable)
                    {
                        if (Points > 0)
                        {
                            Points--;
                        }
                    }
                    else
                    {
                        Points++;
                    }
                    _CustomerDrawingAvailable = value;
                    RaisePropertyChanged("CustomerDrawingAvailable");
                }
            }
        }

        private int _NewRawMaterialQty;
        public int NewRawMaterialQty
        {
            get => _NewRawMaterialQty;
            set
            {
                if (_NewRawMaterialQty != value)
                {
                    if (_NewRawMaterialQty > 15 && _NewRawMaterialQty < 41)
                    {
                        Points++;
                    }
                    else if (_NewRawMaterialQty > 40)
                    {
                        Points += 2;
                    }
                    else if (_NewRawMaterialQty <= 15)
                    {
                        if (Points > 0)
                        {
                            Points--;
                        }
                    }

                        _NewRawMaterialQty = value;
                    RaisePropertyChanged("NewRawMaterialQty");
                }
            }
        }

        private bool _NewTooling = false;
        public bool NewTooling
        {
            get => _NewTooling;
            set
            {
                if (_NewTooling != value)
                {

                    if (_NewTooling)
                    {
                        Points++;
                    }
                    else if (Points > 0)
                    {
                        Points--;
                    }

                    _NewTooling = value;
                    RaisePropertyChanged("NewTooling");
                }
            }
        }

        private bool _TestingBoard = false;
        public bool TestingBoard
        {
            get => _TestingBoard;
            set
            {
                if (_TestingBoard != value)
                {

                    if (_TestingBoard)
                    {
                        Points++;
                    }
                    else if (Points > 0)
                    {
                        Points--;
                    }

                    _TestingBoard = value;
                    RaisePropertyChanged("TestingBoard");
                }
            }
        }

        private bool _RoutingBoard = false;
        public bool RoutingBoard
        {
            get => _RoutingBoard;
            set
            {
                if (_RoutingBoard != value)
                {
                    if (_RoutingBoard)
                    {
                        Points++;
                    }
                    else if (Points > 0)
                    {
                        Points--;
                    }
                    _RoutingBoard = value;
                    RaisePropertyChanged("RoutingBoard");
                }
            }
        }

        private bool _NewMachine = false;
        public bool NewMachine
        {
            get => _NewMachine;
            set
            {
                if (_NewMachine != value)
                {

                    if (_NewMachine)
                    {
                        Points++;
                    }
                    else if (Points > 0)
                    {
                        Points--;
                    }

                    _NewMachine = value;
                    RaisePropertyChanged("NewMachine");
                }
            }
        }

        private bool _NewMold = false;
        public bool NewMold
        {
            get => _NewMold;
            set
            {
                if (_NewMold != value)
                {
                    if (_NewMold)
                    {
                        Points++;
                    }
                    else if (Points > 0)
                    {
                        Points--;
                    }

                    _NewMold = value;
                    RaisePropertyChanged("NewMold");
                }
            }
        }

        private int _CrimpApplication;
        public int CrimpApplication
        {
            get => _CrimpApplication;
            set
            {
                if (_CrimpApplication != value)
                {
                    if (_CrimpApplication > 2 && _CrimpApplication < 8)
                    {
                        Points++;
                    }
                    else if (_CrimpApplication > 7)
                    {
                        Points += 2;
                    }
                    else if (_CrimpApplication < 3)
                    {
                        if (Points > 0)
                        {
                            Points--;
                        }
                    }
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

            Points = new int();

        }

        private void NavigateToBasicFormat()
        {
             _navigationService.NavigateTo(typeof(BasicFormatViewModel).FullName, Points+10);
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

using ProjectManager.Contracts.Services;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System.Windows.Input;
using ProjectManager.Contracts.ViewModels;

namespace ProjectManager.ViewModels
{
    public class FrontCaptureViewModel : ViewModelBase, INavigationAware
    {

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

        private bool _NewCustomer;
        public bool NewCustomer
        {
            get => _NewCustomer;
            set
            {
                if (_NewCustomer != value)
                {
                    _NewCustomer = value;
                    RaisePropertyChanged("NewCustomer");

                    if (_NewCustomer)
                    {
                        _Points++;
                    }
                    else
                    { 
                        if (Points > 0)
                        {
                            _Points--;
                        }
                    }
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
                    _AssemblyQuantity = value;
                    RaisePropertyChanged("AssemblyQuantity");

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

                        _NewRawMaterialQty = value;
                    RaisePropertyChanged("NewRawMaterialQty");

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

                    if (_NewTooling)
                    {
                        Points++;
                    }
                    else
                    {
                        if (Points > 0)
                        {
                            Points--;
                        }
                    }
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

                    if (_TestingBoard)
                    {
                        Points++;
                    }
                    else
                    {
                        if (Points > 0)
                        {
                            Points--;
                        }
                    }
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

                    if (_RoutingBoard)
                    {
                        Points++;
                    }
                    else
                    {
                        if (Points > 0)
                        {
                            Points--;
                        }
                    }
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

                    if (_NewMachine)
                    {
                        Points++;
                    }
                    else
                    {
                        Points--;
                    }
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

                    if (_NewMold)
                    {
                        Points++;
                    }
                    else
                    {
                        Points--;
                    }
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
                    _CrimpApplication = value;
                    RaisePropertyChanged("CrimpApplication");

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
                }
            }
        }

     
        public FrontCaptureViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

         
        }

        private void NavigateToBasicFormat()
        {
             _navigationService.NavigateTo(typeof(BasicFormatViewModel).FullName, Points);
        }

        public void OnNavigatedTo(object parameter)
        {
            Points = new int();
            Points = 0;

            NewCustomer = new bool();
            AssemblyQuantity = new int();
            CustomerDrawingAvailable = new bool();
            NewRawMaterialQty = new int();
            NewTooling = new bool();
            TestingBoard = new bool();
            RoutingBoard = new bool();
            NewMachine = new bool();
            NewMold = new bool();
            CrimpApplication = new int();
        }

        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }
    }
}

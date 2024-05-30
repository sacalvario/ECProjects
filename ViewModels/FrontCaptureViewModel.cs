using ProjectManager.Contracts.Services;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System.Windows.Input;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;

namespace ProjectManager.ViewModels
{
    public class FrontCaptureViewModel : ViewModelBase, INavigationAware
    {

        private readonly INavigationService _navigationService;
        private readonly IWindowManagerService _windowManagerService;

        private ICommand _navigateBasicFormatCommand;
        public ICommand NavigateToBasicCommand => _navigateBasicFormatCommand ??= new RelayCommand(NavigateToBasicFormat);

        private int _QuestionsAnswered;
        public int QuestionsAnswered
        {
            get => _QuestionsAnswered;
            set
            {
                if (_QuestionsAnswered != value)
                {
                    _QuestionsAnswered = value;
                    RaisePropertyChanged("QuestionsAnswered");
                }
            }
        }

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

                    QuestionsAnswered++;

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
                    QuestionsAnswered++;

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
                    QuestionsAnswered++;

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
                    QuestionsAnswered++;

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
                    QuestionsAnswered++;

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
                    QuestionsAnswered++;

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
                    QuestionsAnswered++;

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

        private bool _IsAutomotive;
        public bool IsAutomotive
        {
            get => _IsAutomotive;
            set
            {
                if (_IsAutomotive != value)
                {
                    _IsAutomotive = value;
                    QuestionsAnswered++;
                    RaisePropertyChanged("IsAutomotive");
                }
            }
        }



        public FrontCaptureViewModel(INavigationService navigationService, IWindowManagerService windowManagerService)
        {
            _navigationService = navigationService;
            _windowManagerService = windowManagerService;
        }

        private void NavigateToBasicFormat()
        {
            if (QuestionsAnswered == 8)
            {
                ProjectData data = new ProjectData();

                if (Points <= 3)
                {
                    data.TypeProject = 1;
                    data.TaskDurationDays = 10;
                }
                else if (Points > 3 && Points < 8)
                {
                    data.TypeProject = 2;
                    data.TaskDurationDays = 15;
                }
                else if (Points > 8)
                {
                    data.TypeProject = 3;
                    data.TaskDurationDays = 20;
                }

                data.TotalAssemblies = AssemblyQuantity;
                data.IsAutomotive = (bool)IsAutomotive;

                _navigationService.NavigateTo(typeof(BasicFormatViewModel).FullName, data);
            }
            else
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "You have not completed the questions");
            }
        }

        public void OnNavigatedTo(object parameter)
        {
            Points = new int();
            Points = 0; 
            
            QuestionsAnswered = new int();
            QuestionsAnswered = 0;

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
            IsAutomotive = new bool();

        }

        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }
    }
}

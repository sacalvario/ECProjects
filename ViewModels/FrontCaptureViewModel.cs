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
        private readonly IMailService _mailService;

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
                QuestionsAnswered++;

                if (_NewCustomer != value)
                {
                    _NewCustomer = value;
                    RaisePropertyChanged("NewCustomer");


                    if (_NewCustomer)
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
                QuestionsAnswered++;

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

                QuestionsAnswered++;
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

                QuestionsAnswered++;
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

                QuestionsAnswered++;
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

                QuestionsAnswered++;

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

                QuestionsAnswered++;

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

        private bool _IsAutomotive;
        public bool IsAutomotive
        {
            get => _IsAutomotive;
            set
            {
                QuestionsAnswered++;

                if (_IsAutomotive != value)
                {
                    _IsAutomotive = value;
                    RaisePropertyChanged("IsAutomotive");
                }
            }
        }



        public FrontCaptureViewModel(INavigationService navigationService, IWindowManagerService windowManagerService, IMailService mailService)
        {
            _navigationService = navigationService;
            _windowManagerService = windowManagerService;
            _mailService = mailService;
        }

        private void NavigateToBasicFormat()
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
            data.IsAutomotive = IsAutomotive;
            data.Points = Points;

            _navigationService.NavigateTo(typeof(BasicFormatViewModel).FullName, data);

        }

        public void OnNavigatedTo(object parameter)
        {
            Points = new int();
            Points = 0; 
            
            QuestionsAnswered = new int();

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

            QuestionsAnswered = 0;

        }

        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }
    }
}

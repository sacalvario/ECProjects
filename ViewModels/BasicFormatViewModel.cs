using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class BasicFormatViewModel : ViewModelBase, INavigationAware
    {
        private readonly Contracts.Services.INavigationService _navigationService;
        private readonly IProjectsDataService _projectsDataService;
        private readonly IWindowManagerService _windowManagerService;
        private readonly IMailService _mailService;

        public RelayCommand GoToNextTabItemCommand { get; set; }
        public RelayCommand GoToLastTabItemCommand { get; set; }

        private Project _Project;
        public Project Project
        {
            get => _Project;
            set
            {
                if (_Project != value)
                {
                    _Project = value;
                    RaisePropertyChanged("Project");
                }
            }
        }

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
                    else if (_AssemblyQuantity <= 9)
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

        public BasicFormatViewModel(Contracts.Services.INavigationService navigationService, IProjectsDataService projectsDataService, IWindowManagerService windowManagerService, IMailService mailservice)
        {
            _navigationService = navigationService;
            _projectsDataService = projectsDataService;
            _windowManagerService = windowManagerService;
            _mailService = mailservice;

            Data = new ProjectData();

            Project = new Project()
            {
                CustomerNeedby = DateTime.Now
            };

            Project.ProjectTasks = new ObservableCollection<ProjectTask>();

            GetEmployees();
            GetCustomers();
            GetTasks();

            Cont = 0;

            GoToNextTabItemCommand = new RelayCommand(GoToNexTabItem);
            GoToLastTabItemCommand = new RelayCommand(GoToLastTabItem);
        }


        private ICommand _AddCommand;
        public ICommand AddCommand => _AddCommand ??= new RelayCommand(AddControl);

        private ICommand _AddProject;
        public ICommand AddProjectCommand => _AddProject ??= new RelayCommand(AddProject);

        private ICommand _GetEmployeeCommand;
        public ICommand GetEmployeeCommand => _GetEmployeeCommand ??= new RelayCommand<int>(GetEmployee);

        private ICommand _GetProjectLevelCommand;
        public ICommand GetProjectLevelCommand => _GetProjectLevelCommand ??= new RelayCommand(GetProjectLevel);

        private ObservableCollection<Task> _Tasks;
        public ObservableCollection<Task> Tasks
        {
            get => _Tasks;
            set
            {
                if (_Tasks != value)
                {
                    _Tasks = value;
                    RaisePropertyChanged("Tasks");
                }
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

        private List <int> _EmployeeValue = new List<int>();
        public List<int> EmployeeValue
        {
            get => _EmployeeValue;
            set
            {
                if (_EmployeeValue != value)
                {
                    _EmployeeValue = value;
                    RaisePropertyChanged("EmployeeValue");
                }
            }
        }

        private int _Cont = new int();
        public int Cont
        {
            get => _Cont;
            set
            {
                if (_Cont != value)
                {
                    _Cont = value;
                    RaisePropertyChanged("Cont");
                }
            }
        }

        private ObservableCollection<Employee> _Employees;
        public ObservableCollection<Employee> Employees
        {
            get => _Employees;
            set
            {
                if (_Employees != value)
                {
                    _Employees = value;
                    RaisePropertyChanged("Employees");
                }
            }
        }

        private ObservableCollection<Employee> _Managers;
        public ObservableCollection<Employee> Managers
        {
            get => _Managers;
            set
            {
                if (_Managers != value)
                {
                    _Managers = value;
                    RaisePropertyChanged("Managers");
                }
            }
        }

        private ObservableCollection<ProjectTask> _TaskList;
        public ObservableCollection<ProjectTask> TaskList
        {
            get => _TaskList;
            set
            {
                if (_TaskList != value)
                {
                    _TaskList = value;
                    RaisePropertyChanged("TaskList");
                }
            }
        }

        private int _SelectedTabItem;
        public int SelectedTabItem
        {
            get => _SelectedTabItem;
            set
            {
                if (_SelectedTabItem != value)
                {
                    _SelectedTabItem = value;
                    RaisePropertyChanged("SelectedTabItem");
                }
            }
        }

        private ProjectData _Data;
        public ProjectData Data
        {
            get => _Data;
            set
            {
                if (_Data != value)
                {
                    _Data = value;
                    RaisePropertyChanged("Data");
                }
            }
        }

        private string _ProjectComplexity;
        public string ProjectComplexityString
        {
            get
            {
                if (Data.TypeProject == 1)
                {
                    _ProjectComplexity = "LOW";
                }
                else if (Data.TypeProject == 2)
                {
                    _ProjectComplexity = "MEDIUM";
                }
                else if (Data.TypeProject == 3)
                {
                    _ProjectComplexity = "HIGH";
                }
                return _ProjectComplexity;
            }
        }

        private DateTime WorkDays(int days)
        { 
            DateTime date = new DateTime();
            date = DateTime.Now;

            for (int i = 1; i <= days; i++)
            {
                if (date.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                {
                    days++;
                }
                else if (date.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    days++;
                }
            }

            return date.AddDays(days);

        }

        private void GoToNexTabItem()
        {
            SelectedTabItem++;
        }
        private void GoToLastTabItem()
        {
            SelectedTabItem--;
        }

        private void CreateTasks()
        {
            TaskList = new ObservableCollection<ProjectTask>()
            {
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[0],
                       Duration = 1,
                       StartDate = DateTime.Now,
                       EndDate = WorkDays(1),
                       IdStatus = 2
                    },
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[1],
                       Duration = 2,
                       StartDate = WorkDays(1),
                       EndDate = WorkDays(3),
                       IdStatus = 3
                    },
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[2],
                       Duration = 3,
                       StartDate = WorkDays(3),
                       EndDate = WorkDays(6),
                       IdStatus = 3
                    },
                new ProjectTask
                    {
                       IdTaskNavigation = Tasks[3],
                       Duration = 1,
                       StartDate = WorkDays(6),
                       EndDate = WorkDays(7),
                       IdStatus = 3
                    },
                  new ProjectTask
                    {
                       IdTaskNavigation = Tasks[4],
                       Duration = 7,
                       StartDate = WorkDays(7),
                       EndDate = WorkDays(14),
                       IdStatus = 3
                    },
                    new ProjectTask
                    {
                       IdTaskNavigation = Tasks[5],
                       Duration =  5,
                       StartDate = WorkDays(7),
                       EndDate = WorkDays(12),
                       IdStatus = 3
                    },
                     new ProjectTask
                    {
                       IdTaskNavigation = Tasks[6],
                       Duration =  Data.TaskDurationDays,
                       StartDate = WorkDays(7),
                       EndDate = WorkDays(7 + Data.TaskDurationDays),
                       IdStatus = 3
                    },
                      new ProjectTask
                    {
                       IdTaskNavigation = Tasks[7],
                       Duration =  5,
                       StartDate = WorkDays(7),
                       EndDate = WorkDays(12),
                       IdStatus = 3
                    },
                      new ProjectTask
                    {
                       IdTaskNavigation = Tasks[8],
                       Duration =  2,
                       StartDate = WorkDays(7 + Data.TaskDurationDays),
                       EndDate = WorkDays(9 + Data.TaskDurationDays),
                       IdStatus = 3
                    }
            };

            foreach (ProjectTask task in TaskList)
            {
                task.EmployeeList = Employees;
            }

            Project.ProjectTasks = TaskList;
        }

        private void GetEmployee(int emp)
        {
            _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al registrar - " + emp);
            EmployeeValue.Add(emp);
        }

        private async void GetTasks()
        {
            Tasks = new ObservableCollection<Task>();

            var data = await _projectsDataService.GetTasksAsync();

            foreach (var item in data)
            {
                Tasks.Add(item);
            }
        }
        
        private async void GetCustomers()
        {
            Customers = new ObservableCollection<Customer>();

            var data = await _projectsDataService.GetCustomersAsync();

            foreach (var item in data)
            {
                Customers.Add(item);
            }
        }

        private async void GetEmployees()
        {
            Employees = new ObservableCollection<Employee>();

            var data = await _projectsDataService.GetEmployeesAsync();

            foreach (var item in data)
            {
                item.IdDepartamentNavigation = await _projectsDataService.GetDepartmentAsync(item.IdDepartament);
                item.IdSiteNavigation = await _projectsDataService.GetSiteAsync(item.IdSite);

                if (item.IsActive)
                {
                    Employees.Add(item);
                }

                
            }
            Employees =  new ObservableCollection<Employee>(Employees.OrderBy(i => i.Name));
        }

        private async void GetManagersAndEnginers()
        {
            Managers = new ObservableCollection<Employee>();

            var data = await _projectsDataService.GetEmployeesAsync();

            foreach (var item in data)
            {
                if (item.IdDepartament == 1 || item.IdDepartament == 2)
                {
                    item.IdDepartamentNavigation = await _projectsDataService.GetDepartmentAsync(item.IdDepartament);
                    item.IdSiteNavigation = await _projectsDataService.GetSiteAsync(item.IdSite);
                    Managers.Add(item);
                }
            }
        }

        private async void GetManagers()
        {
            Managers = new ObservableCollection<Employee>();

            var data = await _projectsDataService.GetEmployeesAsync();

            foreach (var item in data)
            {
                if (item.IdDepartament == 1)
                {
                    item.IdDepartamentNavigation = await _projectsDataService.GetDepartmentAsync(item.IdDepartament);
                    item.IdSiteNavigation = await _projectsDataService.GetSiteAsync(item.IdSite);
                    Managers.Add(item);
                }
            }
        }

        private void AddControl()
        {
        }

        private async void AddProject()
        {
            Project.IdGeneratedby = UserRecord.Employee_ID;
            Project.ProjectComplexity = Data.TypeProject;
            Project.IdStatus = 2;
            Project.CreationDate = DateTime.Now;
            Project.EndDate = DateTime.Now;
            Project.TotalEstimatedDuration = 1;
            Project.SuccesRateEstimate = 1;



            Project.ProjectTasks = TaskList;
           
            if (Project.Comments == null)
            {
                Project.Comments = "N/A";
            }

            try
            {
                if (_projectsDataService.SaveProject(Project))
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ApplyMessageViewModel).FullName, Project.IdProject);

                    ProjectTask task = _projectsDataService.GetOnlyActiveTask(Project.IdProject);
                    task.IdEmployeeNavigation = await _projectsDataService.GetEmployeeAsync(task.IdEmployee);

                    _mailService.SendNewTaskEmail(task.IdEmployeeNavigation.Email, Project.IdGeneratedbyNavigation.Email, Project.IdProject, task.IdEmployeeNavigation.Name, UserRecord.Employee.Name, task.LongStartDate, Project.IdCustomerNavigation.Name);


                    _navigationService.NavigateTo(typeof(ProjectDetailsViewModel).FullName, Project);
                    SelectedTabItem = 0;
                    ResetData();
                }

            }
            catch (Exception ex)
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al registrar - " + ex.ToString());
            }

        }

        private void ResetData()
        {
            Project = new Project
            {
                CustomerNeedby = DateTime.Now
            };

            GetCustomers();
            GetEmployees();
            GetTasks();
            CreateTasks();
        }

        private void GetProjectLevel()
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

            Data = data;

            Project.TotalAssembliesInProject = data.TotalAssemblies;

            if (Data.IsAutomotive)
            {
                GetManagers();
            }
            else
            {
                GetManagersAndEnginers();
            }

            CreateTasks();

            GoToNexTabItem();
            _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Total points: " + Data.Points + " Project complexity: " + ProjectComplexityString);

            ResetPoints();
        }

        public void ResetPoints()
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

        public void OnNavigatedTo(object parameter)
        {
            
        }
    }
}


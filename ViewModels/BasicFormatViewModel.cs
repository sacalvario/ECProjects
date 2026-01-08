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
            set { if (_Project != value) { _Project = value; RaisePropertyChanged("Project"); } }
        }

        private int _QuestionsAnswered;
        public int QuestionsAnswered
        {
            get => _QuestionsAnswered;
            set { if (_QuestionsAnswered != value) { _QuestionsAnswered = value; RaisePropertyChanged("QuestionsAnswered"); } }
        }

        private int _Points;
        public int Points
        {
            get => _Points;
            set { if (_Points != value) { _Points = value; RaisePropertyChanged("Points"); } }
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
                    if (_NewCustomer) Points++; else if (Points > 0) Points--;
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
                    // Ajuste de puntos por rango
                    if (_AssemblyQuantity > 9 && _AssemblyQuantity < 20)
                    {
                        Points++;
                    }
                    else if (_AssemblyQuantity >= 20)
                    {
                        Points += 2;
                    }
                    else if (_AssemblyQuantity <= 9 && Points > 0)
                    {
                        Points--;
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
                        if (Points > 0) Points--; // Tener dibujo reduce complejidad
                    }
                    else
                    {
                        Points++; // No tener dibujo aumenta complejidad
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
                        Points++;
                    else if (_NewRawMaterialQty > 40)
                        Points += 2;
                    else if (_NewRawMaterialQty <= 15 && Points > 0)
                        Points--;
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
                    if (_NewTooling) Points++; else if (Points > 0) Points--;
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
                    if (_TestingBoard) Points++; else if (Points > 0) Points--;
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
                    if (_RoutingBoard) Points++; else if (Points > 0) Points--;
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
                    if (_NewMachine) Points++; else if (Points > 0) Points--;
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
                    if (_NewMold) Points++; else if (Points > 0) Points--;
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
                        Points++;
                    else if (_CrimpApplication >= 8)
                        Points += 2;
                    else if (_CrimpApplication <= 2 && Points > 0)
                        Points--;
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

        // Contador adicional preservado del código original si se necesita externamente
        private int _Cont;
        public int Cont
        {
            get => _Cont;
            set { if (_Cont != value) { _Cont = value; RaisePropertyChanged("Cont"); } }
        }

        public BasicFormatViewModel(Contracts.Services.INavigationService navigationService, IProjectsDataService projectsDataService, IWindowManagerService windowManagerService, IMailService mailservice)
        {
            _navigationService = navigationService;
            _projectsDataService = projectsDataService;
            _windowManagerService = windowManagerService;
            _mailService = mailservice;

            Data = new ProjectData();
            Project = new Project { CustomerNeedby = DateTime.Now };
            Project.ProjectTasks = new ObservableCollection<ProjectTask>();
            CustomTasks = new ObservableCollection<CustomProjectTask>();

            GetEmployees();
            GetCustomers();
            GetTasks();

            Cont = 0;

            if (NewParts == null) NewParts = new ObservableCollection<Part>();
            if (NewParts.Count == 0)
            {
                NewParts.Add(new Part { PartNumber = string.Empty, Revision = string.Empty, CustomerId = Project.IdCustomer });
            }

            GoToNextTabItemCommand = new RelayCommand(GoToNexTabItem);
            GoToLastTabItemCommand = new RelayCommand(GoToLastTabItem);
            RemoveCustomTaskCommand = new RelayCommand<CustomProjectTask>(RemoveCustomTask);
        }

        private ICommand _AddProject;
        public ICommand AddProjectCommand => _AddProject ??= new RelayCommand(AddProject);

        private ICommand _GetProjectLevelCommand;
        public ICommand GetProjectLevelCommand => _GetProjectLevelCommand ??= new RelayCommand(EvaluateProjectComplexity);

        private ICommand _AddPartCommand;
        public ICommand AddPartCommand => _AddPartCommand ??= new RelayCommand(AddPart);

        private ICommand _DeletePartCommand;
        public ICommand DeletePartCommand => _DeletePartCommand ??= new RelayCommand(DeletePart);

        private ICommand _AddCustomTaskCommand;
        public ICommand AddCustomTaskCommand => _AddCustomTaskCommand ??= new RelayCommand(AddCustomTask);

        public ICommand RemoveCustomTaskCommand { get; }

        private ObservableCollection<CustomProjectTask> _CustomTasks = new ObservableCollection<CustomProjectTask>();
        public ObservableCollection<CustomProjectTask> CustomTasks
        {
            get => _CustomTasks;
            set { _CustomTasks = value; RaisePropertyChanged(nameof(CustomTasks)); }
        }

        private void AddCustomTask()
        {
            var task = new CustomProjectTask
            {
                CustomDescription = "New custom activity",
                Duration = 1,
                StartDate = DateTime.Now,
                EndDate = WorkDaysFromDate(DateTime.Now, 1),
                IdEmployeeNavigation = null
            };
            task.DurationChanged += (s, e) =>
            {
                if (s is CustomProjectTask changed)
                {
                    changed.EndDate = WorkDaysFromDate(changed.StartDate, changed.Duration);
                }
            };
            CustomTasks.Add(task);
        }

        private void RemoveCustomTask(CustomProjectTask task)
        {
            if (task != null && CustomTasks.Contains(task)) CustomTasks.Remove(task);
        }

        private ObservableCollection<Task> _Tasks;
        public ObservableCollection<Task> Tasks { get => _Tasks; set { _Tasks = value; RaisePropertyChanged("Tasks"); } }

        private ObservableCollection<Customer> _Customers;
        public ObservableCollection<Customer> Customers { get => _Customers; set { _Customers = value; RaisePropertyChanged("Customers"); } }

        private ObservableCollection<Part> _NewParts;
        public ObservableCollection<Part> NewParts { get => _NewParts; set { _NewParts = value; RaisePropertyChanged("NewParts"); } }

        private ObservableCollection<Employee> _Employees;
        public ObservableCollection<Employee> Employees { get => _Employees; set { _Employees = value; RaisePropertyChanged("Employees"); } }

        private ObservableCollection<Employee> _Managers;
        public ObservableCollection<Employee> Managers { get => _Managers; set { _Managers = value; RaisePropertyChanged("Managers"); } }

        private ObservableCollection<ProjectTask> _TaskList;
        public ObservableCollection<ProjectTask> TaskList { get => _TaskList; set { _TaskList = value; RaisePropertyChanged("TaskList"); } }

        private int _SelectedTabItem;
        public int SelectedTabItem { get => _SelectedTabItem; set { if (_SelectedTabItem != value) { _SelectedTabItem = value; RaisePropertyChanged("SelectedTabItem"); } } }

        private ProjectData _Data;
        public ProjectData Data { get => _Data; set { _Data = value; RaisePropertyChanged("Data"); } }

        private string _ProjectComplexity;
        public string ProjectComplexityString
        {
            get
            {
                if (Data.TypeProject == 1) _ProjectComplexity = "LOW";
                else if (Data.TypeProject == 2) _ProjectComplexity = "MEDIUM";
                else if (Data.TypeProject == 3) _ProjectComplexity = "HIGH";
                return _ProjectComplexity;
            }
        }

        private void AddPart()
        {
            if (Project.IdCustomer == 0)
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Selecciona un cliente antes de agregar partes.");
                return;
            }
            NewParts.Add(new Part { PartNumber = string.Empty, Revision = string.Empty, CustomerId = Project.IdCustomer });
        }

        private DateTime WorkDays(int days)
        {
            DateTime date = DateTime.Now;
            for (int i = 1; i <= days; i++)
            {
                if (date.AddDays(i).DayOfWeek == DayOfWeek.Saturday || date.AddDays(i).DayOfWeek == DayOfWeek.Sunday) days++;
            }
            return date.AddDays(days);
        }

        private void GoToNexTabItem() => SelectedTabItem++;
        private void GoToLastTabItem() => SelectedTabItem--;

        private void CreateTasks()
        {
            if (Tasks == null || Tasks.Count < 9) return; // Seguridad

            TaskList = new ObservableCollection<ProjectTask>
            {
                new ProjectTask { IdTaskNavigation = Tasks[0], Duration = 1, StartDate = DateTime.Now, EndDate = WorkDays(1), IdStatus = 2 },
                new ProjectTask { IdTaskNavigation = Tasks[1], Duration = 2, StartDate = WorkDays(1), EndDate = WorkDays(3), IdStatus = 3 },
                new ProjectTask { IdTaskNavigation = Tasks[2], Duration = 3, StartDate = WorkDays(3), EndDate = WorkDays(6), IdStatus = 3 },
                new ProjectTask { IdTaskNavigation = Tasks[3], Duration = 1, StartDate = WorkDays(6), EndDate = WorkDays(7), IdStatus = 3 },
                new ProjectTask { IdTaskNavigation = Tasks[4], Duration = 7, StartDate = WorkDays(7), EndDate = WorkDays(14), IdStatus = 3 },
                new ProjectTask { IdTaskNavigation = Tasks[5], Duration = 5, StartDate = WorkDays(7), EndDate = WorkDays(12), IdStatus = 3 },
                new ProjectTask { IdTaskNavigation = Tasks[6], Duration = Data.TaskDurationDays, StartDate = WorkDays(7), EndDate = WorkDays(7 + Data.TaskDurationDays), IdStatus = 3 },
                new ProjectTask { IdTaskNavigation = Tasks[7], Duration = 5, StartDate = WorkDays(7), EndDate = WorkDays(12), IdStatus = 3 },
                new ProjectTask { IdTaskNavigation = Tasks[8], Duration = 2, StartDate = WorkDays(7 + Data.TaskDurationDays), EndDate = WorkDays(9 + Data.TaskDurationDays), IdStatus = 3 }
            };

            foreach (var task in TaskList)
            {
                task.EmployeeList = Employees;
                task.DurationChanged += (s, e) =>
                {
                    if (s is ProjectTask changed) UpdateTaskDuration(changed);
                };
            }
            Project.ProjectTasks = TaskList;
        }

        private void UpdateTaskDuration(ProjectTask modifiedTask)
        {
            if (modifiedTask == null) return;
            modifiedTask.EndDate = WorkDaysFromDate(modifiedTask.StartDate, modifiedTask.Duration);
            var dependents = TaskList.Where(t => t.IdTaskNavigation.PredecessorTaskId == modifiedTask.IdTaskNavigation.IdTask).ToList();
            foreach (var dependent in dependents)
            {
                if (dependent.StartDate <= modifiedTask.EndDate)
                {
                    dependent.StartDate = modifiedTask.EndDate;
                    dependent.EndDate = WorkDaysFromDate(dependent.StartDate, dependent.Duration);
                    UpdateTaskDuration(dependent);
                }
            }
        }

        private DateTime WorkDaysFromDate(DateTime start, int days)
        {
            DateTime date = start;
            int added = 0;
            while (added < days)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday) added++;
            }
            return date;
        }

        private async void GetTasks()
        {
            Tasks = new ObservableCollection<Task>();
            var data = await _projectsDataService.GetTasksAsync();
            foreach (var item in data) Tasks.Add(item);
        }

        private async void GetCustomers()
        {
            Customers = new ObservableCollection<Customer>();
            var data = await _projectsDataService.GetCustomersAsync();
            foreach (var item in data) Customers.Add(item);
        }

        private async void GetEmployees()
        {
            Employees = new ObservableCollection<Employee>();
            var data = await _projectsDataService.GetEmployeesAsync();
            foreach (var item in data)
            {
                item.IdDepartamentNavigation = await _projectsDataService.GetDepartmentAsync(item.IdDepartament);
                item.IdSiteNavigation = await _projectsDataService.GetSiteAsync(item.IdSite);
                if (item.IsActive) Employees.Add(item);
            }
            Employees = new ObservableCollection<Employee>(Employees.OrderBy(i => i.Name));
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
            await EnsureFixedManagersAsync();
            Managers = new ObservableCollection<Employee>(Managers.OrderBy(i => i.Name));
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
            await EnsureFixedManagersAsync();
            Managers = new ObservableCollection<Employee>(Managers.OrderBy(i => i.Name));
        }

        private async System.Threading.Tasks.Task EnsureFixedManagersAsync()
        {
            // Always include Alejandra Lizares (92) and Daniel Sandoval (217)
            var fixedIds = new[] { 92, 217 };
            foreach (var id in fixedIds)
            {
                if (!Managers.Any(m => m.IdEmployee == id))
                {
                    var emp = await _projectsDataService.GetEmployeeAsync(id);
                    if (emp != null)
                    {
                        emp.IdDepartamentNavigation = await _projectsDataService.GetDepartmentAsync(emp.IdDepartament);
                        emp.IdSiteNavigation = await _projectsDataService.GetSiteAsync(emp.IdSite);
                        Managers.Add(emp);
                    }
                }
            }
        }

        private async void AddProject()
        {
            // Copia respuestas del cuestionario al modelo antes de guardar
            ApplyQuestionnaireToProject();

            Project.IdGeneratedby = UserRecord.Employee_ID;
            Project.ProjectComplexity = Data.TypeProject;
            Project.IdStatus = 2;
            Project.CreationDate = DateTime.Now;
            Project.EndDate = DateTime.Now;
            Project.TotalEstimatedDuration = Data.TaskDurationDays;
            Project.SuccesRateEstimate = 1;
            Project.QuestionnairePoints = Points;

            Project.ProjectTasks = TaskList;
            foreach (var custom in CustomTasks) custom.ProjectId = Project.IdProject;
            Project.CustomProjectTasks = CustomTasks;
            if (string.IsNullOrWhiteSpace(Project.Comments)) Project.Comments = "N/A";

            try
            {
                if (_projectsDataService.SaveProject(Project))
                {
                    foreach (var part in NewParts)
                    {
                        if (part.CustomerId == 0) part.CustomerId = Project.IdCustomer;
                        int newPartId = await _projectsDataService.SavePartAsync(part);
                        var projectPart = new ProjectPart { IdProject = Project.IdProject, IdPart = newPartId };
                        _ = await _projectsDataService.SaveProjectPartAsync(projectPart);
                    }

                    _ = _windowManagerService.OpenInDialog(typeof(ApplyMessageViewModel).FullName, Project.IdProject);

                    var task = _projectsDataService.GetOnlyActiveTask(Project.IdProject);
                    task.IdEmployeeNavigation = await _projectsDataService.GetEmployeeAsync(task.IdEmployee);
                    _mailService.SendNewTaskEmail(task.IdEmployeeNavigation.Email, Project.IdGeneratedbyNavigation.Email, Project.IdProject, task.IdEmployeeNavigation.Name, UserRecord.Employee.Name, task.LongStartDate, Project.IdCustomerNavigation.Name);

                    // Send initial NPR created email to activity 3 responsible and Project Manager
                    var activity3 = TaskList?.FirstOrDefault(t => t.IdTaskNavigation?.IdTask == 3);
                    if (activity3 != null && activity3.IdEmployee != 0)
                    {
                        var activity3Emp = await _projectsDataService.GetEmployeeAsync(activity3.IdEmployee);
                        var managerEmp = await _projectsDataService.GetEmployeeAsync(Project.IdManager);
                        var customer = (await _projectsDataService.GetCustomerAsync(Project.IdCustomer))?.Name ?? "";
                        if (activity3Emp != null)
                        {
                            var to = activity3Emp.Email;
                            var cc = managerEmp?.Email;
                            _mailService.SendNewNprCreatedEmail(to, cc, Project.IdProject, customer);
                        }
                    }

                    _navigationService.NavigateTo(typeof(ProjectDetailsViewModel).FullName, Project);
                    SelectedTabItem = 0;
                    ResetAllForNewProject();
                }
            }
            catch (Exception ex)
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error al registrar - " + ex);
            }
        }

        private void ApplyQuestionnaireToProject()
        {
            Project.NewCustomer = NewCustomer;
            Project.AssemblyQuantity = AssemblyQuantity;
            Project.CustomerDrawingAvailable = CustomerDrawingAvailable;
            Project.NewRawMaterialQty = NewRawMaterialQty;
            Project.NewTooling = NewTooling;
            Project.TestingBoard = TestingBoard;
            Project.RoutingBoard = RoutingBoard;
            Project.NewMachine = NewMachine;
            Project.NewMold = NewMold;
            Project.CrimpApplication = CrimpApplication;
            Project.IsAutomotive = IsAutomotive;
            Project.TotalAssembliesInProject = AssemblyQuantity;
        }

        private void EvaluateProjectComplexity()
        {
            // Determinación de complejidad con reglas dadas
            var totalPoints = Points;
            int typeProject;
            int durationDays;

            if (NewCustomer || IsAutomotive)
            {
                typeProject = 3;
                durationDays = 20;
            }
            else if (totalPoints <= 3)
            {
                typeProject = 1;
                durationDays = 10;
            }
            else if (totalPoints < 8) // >3 y <8
            {
                typeProject = 2;
                durationDays = 15;
            }
            else // >=8
            {
                typeProject = 3;
                durationDays = 20;
            }

            Data = new ProjectData
            {
                TypeProject = typeProject,
                TaskDurationDays = durationDays,
                TotalAssemblies = AssemblyQuantity,
                IsAutomotive = IsAutomotive,
                Points = totalPoints
            };

            // Guardar también en Project para persistencia
            Project.ProjectComplexity = typeProject;
            Project.TotalAssembliesInProject = AssemblyQuantity;
            Project.QuestionnairePoints = totalPoints;
            Project.IsAutomotive = IsAutomotive;
            Project.NewCustomer = NewCustomer;

            // Asignación de managers según automotriz
            if (IsAutomotive) GetManagers(); else GetManagersAndEnginers();

            CreateTasks();
            GoToNexTabItem();
            _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "Total points: " + Data.Points + " Project complexity: " + ProjectComplexityString);

            ResetQuestionnaireValues();
        }

        private void ResetQuestionnaireValues()
        {
            Points = 0; QuestionsAnswered = 0; NewCustomer = false; AssemblyQuantity = 0; CustomerDrawingAvailable = false; NewRawMaterialQty = 0; NewTooling = false; TestingBoard = false; RoutingBoard = false; NewMachine = false; NewMold = false; CrimpApplication = 0; IsAutomotive = false; Cont = 0;
        }

        private void ResetAllForNewProject()
        {
            Project = new Project { CustomerNeedby = DateTime.Now };
            Data = new ProjectData();
            ResetQuestionnaireValues();
            NewParts = new ObservableCollection<Part> { new Part { PartNumber = string.Empty, Revision = string.Empty, CustomerId = Project.IdCustomer } };
            CustomTasks = new ObservableCollection<CustomProjectTask>();
            GetCustomers(); GetEmployees(); GetTasks();
            TaskList = new ObservableCollection<ProjectTask>();
        }

        private void DeletePart()
        {
            if (NewParts.Count > 1) NewParts.RemoveAt(NewParts.Count - 1);
        }

        public void OnNavigatedFrom() { }
        public void OnNavigatedTo(object parameter) { }
    }
}


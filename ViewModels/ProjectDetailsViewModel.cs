using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class ProjectDetailsViewModel : ViewModelBase, INavigationAware
    {
        private readonly IProjectsDataService _projectsDataService;
        private readonly IWindowManagerService _windowManagerService;
        private readonly IMailService _mailService;
        private readonly INavigationService _navigationService;

        public ProjectDetailsViewModel(IProjectsDataService projectsDataService, IWindowManagerService windowManagerService,IMailService mailService, INavigationService navigationService)
        {
            _projectsDataService = projectsDataService;
            _windowManagerService = windowManagerService;
            _mailService = mailService;
            _navigationService = navigationService;

            SelectedTabItem = 0;
            Activities = new ObservableCollection<ProjectTask>();
            ProjectParts = new ObservableCollection<ProjectPart>();
        }

        #region Commands
        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= new RelayCommand(GoBack);

        private ICommand _completeTaskCommand;
        public ICommand CompleteTaskCommand => _completeTaskCommand ??= new RelayCommand(async () => await CompleteTaskAsync());

        private ICommand _cancelProjectCommand;
        public ICommand CancelProjectCommand => _cancelProjectCommand ??= new RelayCommand(CancelProject);
        #endregion

        #region Properties
        private int _selectedTabItem;
        public int SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                if (_selectedTabItem != value)
                {
                    _selectedTabItem = value;
                    RaisePropertyChanged(nameof(SelectedTabItem));
                }
            }
        }

        private Visibility _cancelProjectBtnVisibility = Visibility.Collapsed;
        public Visibility CancelProjectBtnVisibility
        {
            get => _cancelProjectBtnVisibility;
            set
            {
                if (_cancelProjectBtnVisibility != value)
                {
                    _cancelProjectBtnVisibility = value;
                    RaisePropertyChanged(nameof(CancelProjectBtnVisibility));
                }
            }
        }

        private Visibility _completeTaskBtnVisibility = Visibility.Collapsed;
        public Visibility CompleteTaskBtnVisibility
        {
            get => _completeTaskBtnVisibility;
            set
            {
                if (_completeTaskBtnVisibility != value)
                {
                    _completeTaskBtnVisibility = value;
                    RaisePropertyChanged(nameof(CompleteTaskBtnVisibility));
                }
            }
        }

        private Visibility _projectGeneratorVisibility = Visibility.Collapsed;
        public Visibility ProjectGeneratorVisibility
        {
            get => _projectGeneratorVisibility;
            set
            {
                if (_projectGeneratorVisibility != value)
                {
                    _projectGeneratorVisibility = value;
                    RaisePropertyChanged(nameof(ProjectGeneratorVisibility));
                }
            }
        }

        private Project _project;
        public Project Project
        {
            get => _project;
            set
            {
                if (_project != value)
                {
                    _project = value;
                    RaisePropertyChanged(nameof(Project));
                }
            }
        }

        private ProjectTask _activeTask;
        public ProjectTask ActiveTask
        {
            get => _activeTask;
            set
            {
                if (_activeTask != value)
                {
                    _activeTask = value;
                    RaisePropertyChanged(nameof(ActiveTask));
                }
            }
        }

        private ObservableCollection<ProjectTask> _activities;
        public ObservableCollection<ProjectTask> Activities
        {
            get => _activities;
            set
            {
                if (_activities != value)
                {
                    _activities = value;
                    RaisePropertyChanged(nameof(Activities));
                }
            }
        }

        private ObservableCollection<ProjectPart> _projectParts;
        public ObservableCollection<ProjectPart> ProjectParts
        {
            get => _projectParts;
            set
            {
                if (_projectParts != value)
                {
                    _projectParts = value;
                    RaisePropertyChanged(nameof(ProjectParts));
                }
            }
        }
        #endregion

        #region Methods

        public async void OnNavigatedTo(object parameter)
        {
            if (parameter is Project project)
            {
                await InitializeAsync(project);
            }
        }

        private async System.Threading.Tasks.Task InitializeAsync(Project project)
        {
            Project = project;

            CancelProjectBtnVisibility = Visibility.Collapsed;
            ProjectGeneratorVisibility = Visibility.Collapsed;
            CompleteTaskBtnVisibility = Visibility.Collapsed;

            if (Project.IdGeneratedby == UserRecord.Employee_ID)
            {
                ProjectGeneratorVisibility = Visibility.Visible;

                if (Project.IdStatusNavigation?.IdStatus == 2)
                    CancelProjectBtnVisibility = Visibility.Visible;
            }

            Activities.Clear();
            await LoadActivitiesAsync();

            await LoadPartsAsync();

            ActiveTask = _projectsDataService.GetActiveTask(Project.IdProject, UserRecord.Employee_ID);

            if (ActiveTask != null && ActiveTask.IdEmployee == UserRecord.Employee_ID)
            {
                CompleteTaskBtnVisibility = Visibility.Visible;
            }
        }

        private async System.Threading.Tasks.Task LoadActivitiesAsync()
        {
            var activities = await _projectsDataService.GetActivitiesAsync(Project.IdProject);
            Activities.Clear();

            foreach (var item in activities)
            {
                item.IdEmployeeNavigation = await _projectsDataService.GetEmployeeAsync(item.IdEmployee);
                item.IdEmployeeNavigation.IdDepartamentNavigation = await _projectsDataService.GetDepartmentAsync(item.IdEmployeeNavigation.IdDepartament);
                item.IdStatusNavigation = await _projectsDataService.GetStatusAsync(item.IdStatus);
                item.IdTaskNavigation = await _projectsDataService.GetTaskAsync(item.IdTask);
                Activities.Add(item);
            }
        }

        private async System.Threading.Tasks.Task LoadPartsAsync()
        {
            var parts = await _projectsDataService.GetProjectPartsWithPartInfoAsync(Project.IdProject);
            ProjectParts = new ObservableCollection<ProjectPart>(parts);
        }

        private async System.Threading.Tasks.Task CompleteTaskAsync()
        {
            if (ActiveTask == null) return;


            // 2. Verificar si se completó tarde o temprano
            if (DateTime.Now > ActiveTask.EndDate)
                ActiveTask.IdStatus = 1; // Completada
            else
                ActiveTask.IdStatus = 4;

            ActiveTask.EndDate = DateTime.Now;

            // 3. Guardar cambios
            await _projectsDataService.UpdateTaskAsync(ActiveTask);

            // 4. Buscar tareas dependientes
            var dependentTasks = Project.ProjectTasks
                .Where(t => t.IdTaskNavigation.PredecessorTaskId == ActiveTask.IdTaskNavigation.IdTask)
                .ToList();


            foreach (var nextTask in dependentTasks)
            {
                // 5. Activar la siguiente tarea (cambiar su estado y fechas)
                nextTask.IdStatus = 2; // Activa
                nextTask.StartDate = ActiveTask.EndDate;
                nextTask.EndDate = WorkDaysFromDate(nextTask.StartDate, nextTask.Duration);

                await _projectsDataService.UpdateTaskAsync(nextTask);

                // 6. Enviar correo de notificación (de prueba, va a ti)
                //await _mailService.SendEmailAsync(
                //    to: "scalvario@ecmfg.com",
                //    subject: $"Nueva tarea activada: {nextTask.IdTaskNavigation.TaskName}",
                //    body: $"Se activó la tarea: {nextTask.IdTaskNavigation.TaskName}\n\n" +
                //          $"Inicio: {nextTask.StartDate:dd/MM/yyyy}\n" +
                //          $"Fin: {nextTask.EndDate:dd/MM/yyyy}\n" +
                //          $"Empleado asignado: {nextTask.Employee?.FirstName ?? "N/A"}"
                //);
            }

            // 7. Si la tarea completada es la número 4, activar simultáneamente las 4 siguientes
            if (ActiveTask.IdTaskNavigation.IdTask == 4)
            {
                var nextSimultaneousTasks = Project.ProjectTasks
                    .Where(t => new[] { 5, 6, 7, 8 }.Contains(t.IdTaskNavigation.IdTask))
                    .ToList();

                foreach (var task in nextSimultaneousTasks)
                {
                    task.IdStatus = 2;
                    task.StartDate = ActiveTask.EndDate;
                    task.EndDate = WorkDaysFromDate(task.StartDate, task.Duration);

                    await _projectsDataService.UpdateTaskAsync(task);

                    //await _emailService.SendEmailAsync(
                    //    to: "scalvario@ecmfg.com",
                    //    subject: $"Tarea simultánea activada: {task.IdTaskNavigation.TaskName}",
                    //    body: $"Se activó la tarea: {task.IdTaskNavigation.TaskName}\n\n" +
                    //          $"Inicio: {task.StartDate:dd/MM/yyyy}\n" +
                    //          $"Fin: {task.EndDate:dd/MM/yyyy}\n" +
                    //          $"Empleado asignado: {task.Employee?.FirstName ?? "N/A"}"
                    //);
                }
            }

            // 8. Notificar a la vista que se completó
            RaisePropertyChanged(nameof(ActiveTask));
            _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "The task has been completed");
            await RefreshActivitiesAsync();
        }

        private DateTime WorkDaysFromDate(DateTime start, int workDays)
        {
            DateTime date = start;
            int added = 0;

            while (added < workDays)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    added++;
            }

            return date;
        }

        private async System.Threading.Tasks.Task RefreshActivitiesAsync()
        {
            Activities.Clear();
            await LoadActivitiesAsync();
        }

        private async System.Threading.Tasks.Task NotifyNextTasksEmailsAsync()
        {
            if (ActiveTask == null) return;

            if (ActiveTask.IdTask < 4)
            {
                await NotifyEmailForTaskAsync(ActiveTask.IdTask + 1);
            }
            else if (ActiveTask.IdTask == 4)
            {
                for (int i = 5; i < 9; i++)
                {
                    await NotifyEmailForTaskAsync(i);
                }
            }
            else if (ActiveTask.IdTask == 5)
            {
                await NotifyEmailForTaskAsync(9);
            }
        }

        private async System.Threading.Tasks.Task NotifyEmailForTaskAsync(int taskId)
        {
            var nextTask = _projectsDataService.GetNextTask(Project.IdProject, taskId);
            nextTask.IdEmployeeNavigation = await _projectsDataService.GetEmployeeAsync(nextTask.IdEmployee);

            _mailService.SendNewTaskEmail(
                nextTask.IdEmployeeNavigation.Email,
                Project.IdGeneratedbyNavigation.Email,
                Project.IdProject,
                nextTask.IdEmployeeNavigation.Name,
                Project.IdGeneratedbyNavigation.Name,
                nextTask.LongEndDate,
                Project.IdCustomerNavigation.Name);
        }

        public void OnNavigatedFrom()
        {
            if (CancelProjectBtnVisibility == Visibility.Visible)
                CancelProjectBtnVisibility = Visibility.Collapsed;
        }

        private void CancelProject()
        {
            try
            {
                if (_projectsDataService.CancelProject(Project))
                {
                    _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "The project has been cancelled successfully");
                    _navigationService.GoBack();
                    CancelProjectBtnVisibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "ERROR - " + ex.ToString());
            }
        }

        private void GoBack() => _navigationService.GoBack();

        #endregion
    }
}


using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
        public readonly IProjectsDataService _projectsDataService;
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
        public ICommand CompleteTaskCommand => _completeTaskCommand ??= new RelayCommand(async () => await OnCompleteTaskAsync());
        //public ICommand CompleteTaskCommand => _completeTaskCommand ??= new RelayCommand(OnCompleteTaskAsync());

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

        private ICommand _ExportPDFCommand;
        public ICommand ExportPDFCommand
        {
            get
            {
                if (_ExportPDFCommand == null)
                {
                    _ExportPDFCommand = new RelayCommand(Export);
                }
                return _ExportPDFCommand;
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

        private void Export()
        {
            Messenger.Default.Send(new NotificationMessage<Project>(Project, "ShowReport"));
        }

        private async System.Threading.Tasks.Task OnCompleteTaskAsync()
        {
            if (ActiveTask == null)
                return;

            try
            {
                await _projectsDataService.CompleteTaskAsync(ActiveTask);

                // Opcional: refrescar tareas del proyecto
                await RefreshActivitiesAsync();

                // Notificar éxito
                _ = _windowManagerService.OpenInDialog(typeof(ApplyMessageViewModel).FullName, "Se completo la tarea correctamente");

                // Si deseas cerrar la vista o navegar, hazlo aquí
                //_navigationService.GoBack(); // si usas navegación

            }
            catch (Exception ex)
            {
                _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "Error, No se pudo completar la tarea: " + ex.ToString());
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


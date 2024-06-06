using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class ProjectDetailsViewModel : ViewModelBase, INavigationAware
    {
        public IProjectsDataService _projectsDataService;
        private readonly IWindowManagerService _windowManagerService;
        private readonly IMailService _mailService;
        private readonly INavigationService _navigationService;

        public ProjectDetailsViewModel(IProjectsDataService projectsDataService, IWindowManagerService windowManagerService, IMailService mailService, INavigationService navigationService)
        {
            _projectsDataService = projectsDataService;
            _windowManagerService = windowManagerService;
            _mailService = mailService;
            _navigationService = navigationService;

            SelectedTabItem = 0;

        }

        private ICommand _GoToBackCommand;
        public ICommand GoToBackCommand
        {
            get
            {
                if (_GoToBackCommand == null)
                {
                    _GoToBackCommand = new RelayCommand(GoBack);
                }
                return _GoToBackCommand;
            }
        }

        private ICommand _CompleteTaskCommand;
        public ICommand CompleteTaskCommand
        {
            get
            {
                if (_CompleteTaskCommand == null)
                {
                    _CompleteTaskCommand = new RelayCommand(CompleteTask);
                }
                return _CompleteTaskCommand;
            }
        }

        private int _selectedTabItem;
        public int SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                if (_selectedTabItem != value)
                {
                    _selectedTabItem = value;
                    RaisePropertyChanged("SelectedTabItem");

                    if (SelectedTabItem == 1 && ActiveTask.IdEmployee == UserRecord.Employee_ID)
                    {
                        CompleteTaskBtnVisibility = Visibility.Visible;
                    }
                    else
                    {
                        CompleteTaskBtnVisibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private Visibility _CancelProjectBtnVisibility = Visibility.Collapsed;
        public Visibility CancelProjectBtnVisibility
        {
            get => _CancelProjectBtnVisibility;
            set
            {
                if (_CancelProjectBtnVisibility != value)
                {
                    _CancelProjectBtnVisibility = value;
                    RaisePropertyChanged("CancelProjectBtnVisibility");
                }
            }
        }

        private Visibility _CompleteTaskBtnVisibility = Visibility.Collapsed;
        public Visibility CompleteTaskBtnVisibility
        {
            get => _CompleteTaskBtnVisibility;
            set
            {
                if (_CompleteTaskBtnVisibility != value)
                {
                    _CompleteTaskBtnVisibility = value;
                    RaisePropertyChanged("CompleteTaskBtnVisibility");
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
                    RaisePropertyChanged("Project");
                }
            }
        }

        private ProjectTask _ActiveTask;
        public ProjectTask ActiveTask
        {
            get => _ActiveTask;
            set
            {
                if (_ActiveTask != value)
                {
                    _ActiveTask = value;
                    RaisePropertyChanged("ActiveTask");
                }
            }
        }

        private ObservableCollection<ProjectTask> _Activities;
        public ObservableCollection<ProjectTask> Activities
        {
            get => _Activities;
            set
            {
                if (_Activities != value)
                {
                    _Activities = value;
                    RaisePropertyChanged("Activities");
                }
            }
        }

        private async void GetActivities()
        {
            var activities = await _projectsDataService.GetActivitiesAsync(Project.IdProject);

            foreach (var item in activities)
            {
                item.IdEmployeeNavigation = await _projectsDataService.GetEmployeeAsync(item.IdEmployee);
                item.IdEmployeeNavigation.IdDepartamentNavigation = await _projectsDataService.GetDepartmentAsync(item.IdEmployeeNavigation.IdDepartament);
                item.IdStatusNavigation = await _projectsDataService.GetStatusAsync(item.IdStatus);
                item.IdTaskNavigation = await _projectsDataService.GetTaskAsync(item.IdTask);
                Activities.Add(item);
            }
        }

        private void CompleteTask()
        {
            _projectsDataService.CompleteTask(ActiveTask);
        }
        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Project project)
            {
                Project = new Project();
                Project = project;
            }

            if (Project.IdGeneratedby == UserRecord.Employee_ID)
            {
                CancelProjectBtnVisibility = Visibility.Visible;
            }

            Activities = new ObservableCollection<ProjectTask>();
            GetActivities();

            ActiveTask = _projectsDataService.GetActiveTask(Project.IdProject);
        }

        private void GoBack()
        {
            _navigationService.GoBack();
        }
    }
}

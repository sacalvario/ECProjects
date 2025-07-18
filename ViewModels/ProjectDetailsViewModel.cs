﻿using GalaSoft.MvvmLight;
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

        private ICommand _CancelTaskCommand;
        public ICommand CancelProjectCommand
        {
            get
            {
                if (_CancelTaskCommand == null)
                {
                    _CancelTaskCommand = new RelayCommand(CancelProject);
                }
                return _CancelTaskCommand;
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

                    //if (SelectedTabItem == 1 && ActiveTask.IdEmployee == UserRecord.Employee_ID)
                    //{
                    //    CompleteTaskBtnVisibility = Visibility.Visible;
                    //}
                    //else
                    //{
                    //    CompleteTaskBtnVisibility = Visibility.Collapsed;
                    //}
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

        private Visibility _ProjectGeneratorVisibility = Visibility.Collapsed;
        public Visibility ProjectGeneratorVisibility
        {
            get => _ProjectGeneratorVisibility;
            set
            {
                if (_ProjectGeneratorVisibility != value)
                {
                    _ProjectGeneratorVisibility = value;
                    RaisePropertyChanged("ProjectGeneratorVisibility");
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

        private ObservableCollection<ProjectPart> _ProjectParts;
        public ObservableCollection<ProjectPart> ProjectParts
        {
            get => _ProjectParts;
            set
            {
                _ProjectParts = value;
                RaisePropertyChanged(nameof(ProjectParts));
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

        private async void GetParts()
        {
            var parts = await _projectsDataService.GetProjectPartsWithPartInfoAsync(Project.IdProject);
            ProjectParts = new ObservableCollection<ProjectPart>(parts);
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

        private async void CompleteTask()
        {
            try
            {
                if (_projectsDataService.CompleteTask(ActiveTask))
                {                   
                    
                    Activities = new ObservableCollection<ProjectTask>();
                    GetActivities();

                    if (ActiveTask.IdTask < 4)
                    {
                        ProjectTask nextask = _projectsDataService.GetNextTask(Project.IdProject, ActiveTask.IdTask + 1);
                        nextask.IdEmployeeNavigation = await _projectsDataService.GetEmployeeAsync(nextask.IdEmployee);

                        _mailService.SendNewTaskEmail(nextask.IdEmployeeNavigation.Email, Project.IdGeneratedbyNavigation.Email, Project.IdProject, nextask.IdEmployeeNavigation.Name, Project.IdGeneratedbyNavigation.Name, nextask.LongEndDate, Project.IdCustomerNavigation.Name);
                    }

                    else if (ActiveTask.IdTask == 4)
                    {
                        for (int i = 5; i < 9; i++)
                        {
                            ProjectTask nextask = _projectsDataService.GetNextTask(Project.IdProject, i);
                            nextask.IdEmployeeNavigation = await _projectsDataService.GetEmployeeAsync(nextask.IdEmployee);
                            _mailService.SendNewTaskEmail(nextask.IdEmployeeNavigation.Email, Project.IdGeneratedbyNavigation.Email, Project.IdProject, nextask.IdEmployeeNavigation.Name, Project.IdGeneratedbyNavigation.Name, nextask.LongEndDate, Project.IdCustomerNavigation.Name);
                        }
                    }

                    else if (ActiveTask.IdTask == 5)
                    {
                        ProjectTask nextask = _projectsDataService.GetNextTask(Project.IdProject, 9);
                        nextask.IdEmployeeNavigation = await _projectsDataService.GetEmployeeAsync(nextask.IdEmployee);
                        _mailService.SendNewTaskEmail(nextask.IdEmployeeNavigation.Email, Project.IdGeneratedbyNavigation.Email, Project.IdProject, nextask.IdEmployeeNavigation.Name, Project.IdGeneratedbyNavigation.Name, nextask.LongEndDate, Project.IdCustomerNavigation.Name);

                    }

                    _ = _windowManagerService.OpenInDialog(typeof(EcnSignedViewModel).FullName, "The task has been completed");

                    CompleteTaskBtnVisibility = Visibility.Hidden;
                }

            }
            catch (Exception ex)
            {
                 _ = _windowManagerService.OpenInDialog(typeof(ErrorViewModel).FullName, "ERROR - " + ex.ToString());
            }
        }
        public void OnNavigatedFrom()
        {
            if (CancelProjectBtnVisibility == Visibility.Visible)
            {
                CancelProjectBtnVisibility = Visibility.Collapsed;
            }
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
            else
            {
                ProjectGeneratorVisibility = Visibility.Visible;

            }

            Activities = new ObservableCollection<ProjectTask>();
            GetActivities();

            GetParts();

            ActiveTask = _projectsDataService.GetActiveTask(Project.IdProject, UserRecord.Employee_ID);
                
            if (ActiveTask != null)
            {
                if (ActiveTask.IdEmployee == UserRecord.Employee_ID)
                {
                    CompleteTaskBtnVisibility = Visibility.Visible;
                }
            }


            if (Project.IdStatusNavigation.IdStatus == 2 && Project.IdGeneratedbyNavigation.IdEmployee == UserRecord.Employee_ID)
            {
                CancelProjectBtnVisibility = Visibility.Visible;
            }

        }
        
        private void GoBack()
        {
            _navigationService.GoBack();

        }
    }
}
 
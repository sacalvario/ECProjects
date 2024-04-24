using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class TasksViewModel : ViewModelBase, INavigationAware
    {
        private readonly IProjectsDataService _projectsDataService;
        private readonly INavigationService _navigationService;

        private ICommand _navigateToProjectCommand;
        public ICommand NavigateToProjectCommand => _navigateToProjectCommand ??= new RelayCommand<Project>(NavigateToProject);

        public TasksViewModel(IProjectsDataService projectsDataService, INavigationService navigationService)
        {
            _projectsDataService = projectsDataService;
            _navigationService = navigationService;
        }

        private ObservableCollection<ProjectTask> _Checklist;
        public ObservableCollection<ProjectTask> Checklist
        {
            get => _Checklist;
            set
            {
                if (_Checklist != value)
                {
                    _Checklist = value;
                    RaisePropertyChanged("Checklist");
                }
            }
        }

        private async void GetChecklist(int employee)
        {
            var data = await _projectsDataService.GetTasksAsync(employee);

            foreach (var item in data)
            {
                item.IdProjectNavigation = await _projectsDataService.GetProjectAsync(item.IdProject);
                item.IdStatusNavigation = await _projectsDataService.GetStatusAsync(item.IdStatus);
                item.IdEmployeeNavigation = await _projectsDataService.GetEmployeeAsync(item.IdEmployee);

                Checklist.Add(item);
            }
        }


        private void NavigateToProject(Project project)
        {
            _navigationService.NavigateTo(typeof(ProjectDetailsViewModel).FullName, project);
        }

        public void OnNavigatedTo(object parameter)
        {
            Checklist = new ObservableCollection<ProjectTask>();

            if (parameter is Employee employee)
            {
                GetChecklist(employee.IdEmployee);
            }
            else
            {
                GetChecklist(UserRecord.Employee_ID);
            }

        }

        public void OnNavigatedFrom()
        {
            throw new NotImplementedException();
        }
    }
}

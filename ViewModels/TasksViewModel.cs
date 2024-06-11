using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
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

            CvsChecklist = new CollectionViewSource();

            CvsChecklist.GroupDescriptions.Add(new PropertyGroupDescription("IdProjectNavigation"));
            CvsChecklist.SortDescriptions.Add(new SortDescription("IdProjectNavigation.Year", ListSortDirection.Descending));
            CvsChecklist.SortDescriptions.Add(new SortDescription("IdProjectNavigation.Month", ListSortDirection.Descending));
            CvsChecklist.SortDescriptions.Add(new SortDescription("IdProjectNavigation.IdProject", ListSortDirection.Descending));
        }

        private CollectionViewSource _CvsChecklist;
        public CollectionViewSource CvsChecklist
        {
            get => _CvsChecklist;
            set
            {
                if (_CvsChecklist != value)
                {
                    _CvsChecklist = value;
                    RaisePropertyChanged("CvsChecklist");
                }
            }
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
            Checklist = new ObservableCollection<ProjectTask>();
            var data = await _projectsDataService.GetTasksAsync(employee);

            foreach (var item in data)
            {
                item.IdProjectNavigation = await _projectsDataService.GetProjectAsync(item.IdProject);
                item.IdProjectNavigation.IdStatusNavigation = await _projectsDataService.GetStatusAsync(item.IdStatus);
                item.IdProjectNavigation.IdManagerNavigation = await _projectsDataService.GetEmployeeAsync(item.IdProjectNavigation.IdManager);
                item.IdProjectNavigation.IdGeneratedbyNavigation = await _projectsDataService.GetEmployeeAsync(item.IdProjectNavigation.IdGeneratedby);
                item.IdProjectNavigation.IdCustomerNavigation = await _projectsDataService.GetCustomerAsync(item.IdProjectNavigation.IdCustomer);
                item.IdStatusNavigation = await _projectsDataService.GetStatusAsync(item.IdStatus);
                item.IdEmployeeNavigation = await _projectsDataService.GetEmployeeAsync(item.IdEmployee);
                item.IdTaskNavigation = await _projectsDataService.GetTaskAsync(item.IdTask);


                Checklist.Add(item);
            }
        }


        private void NavigateToProject(Project project)
        {
            _navigationService.NavigateTo(typeof(ProjectDetailsViewModel).FullName, project);
        }

        public void OnNavigatedTo(object parameter)
        {
            CvsChecklist.Source = null;

            if (parameter is Employee employee)
            {
                GetChecklist(employee.IdEmployee);
            }
            else
            {
                GetChecklist(UserRecord.Employee_ID);
            }


            CvsChecklist.Source = Checklist;

        }

        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }
    }
}

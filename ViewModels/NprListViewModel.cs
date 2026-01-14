using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;

namespace ProjectManager.ViewModels
{
    public class NprListViewModel : ViewModelBase, INavigationAware
    {
        private readonly IProjectsDataService _projectsDataService;
        private readonly INavigationService _navigationService;

        // Simple in-memory cache shared across instances
        private static List<Project> _projectsCache;

        public NprListViewModel(IProjectsDataService projectsDataService, INavigationService navigationService)
        {
            _projectsDataService = projectsDataService;
            _navigationService = navigationService;
            Projects = new ObservableCollection<Project>();
        }

        private ObservableCollection<Project> _projects;
        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set { _projects = value; RaisePropertyChanged(nameof(Projects)); }
        }

        private ICommand _navigateToDetailCommand;
        public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<Project>(NavigateToDetail);

        public async void OnNavigatedTo(object parameter)
        {
            // allow caller to force a refresh by passing a bool true as parameter
            bool force = false;
            if (parameter is bool b) force = b;
            await LoadAsync(force);
        }

        public void OnNavigatedFrom() { }

        private async System.Threading.Tasks.Task LoadAsync(bool forceRefresh = false)
        {
            // If cache exists and refresh not requested, use it directly
            if (_projectsCache != null && !forceRefresh)
            {
                Projects = new ObservableCollection<Project>(_projectsCache);
                return;
            }

            Projects.Clear();
            var all = await _projectsDataService.GetAllProjectsAsync();

            var tempList = new List<Project>();
            foreach (var p in all)
            {
                // Ensure related info for bindings
                if (p.IdStatusNavigation == null)
                {
                    p.IdStatusNavigation = await _projectsDataService.GetStatusAsync(p.IdStatus);
                }
                if (p.IdManagerNavigation != null && p.IdManagerNavigation.IdDepartamentNavigation == null)
                {
                    p.IdManagerNavigation.IdDepartamentNavigation = await _projectsDataService.GetDepartmentAsync(p.IdManagerNavigation.IdDepartament);
                }
                Projects.Add(p);
                tempList.Add(p);
            }

            // update cache
            _projectsCache = tempList;
        }

        private void NavigateToDetail(Project project)
        {
            if (project != null)
            {
                _navigationService.NavigateTo(typeof(ProjectDetailsViewModel).FullName, project);
            }
        }
    }
}

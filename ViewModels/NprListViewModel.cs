using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class NprListViewModel : ViewModelBase, INavigationAware
    {
        private readonly IProjectsDataService _projectsDataService;
        private readonly INavigationService _navigationService;

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
            await LoadAsync();
        }

        public void OnNavigatedFrom() { }

        private async System.Threading.Tasks.Task LoadAsync()
        {
            Projects.Clear();
            var all = await _projectsDataService.GetAllProjectsAsync();
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
            }
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

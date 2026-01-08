using GalaSoft.MvvmLight;
using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProjectManager.ViewModels
{
    public class NprListViewModel : ViewModelBase, INavigationAware
    {
        private readonly IProjectsDataService _projectsDataService;

        public NprListViewModel(IProjectsDataService projectsDataService)
        {
            _projectsDataService = projectsDataService;
            Projects = new ObservableCollection<Project>();
        }

        private ObservableCollection<Project> _projects;
        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set { _projects = value; RaisePropertyChanged(nameof(Projects)); }
        }

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
                Projects.Add(p);
            }
        }
    }
}

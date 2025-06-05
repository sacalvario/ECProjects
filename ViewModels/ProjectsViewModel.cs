using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectManager.Contracts.Services;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class ProjectsViewModel : ViewModelBase
    {
        private readonly IProjectsDataService _projectsDataService;
        private readonly INavigationService _navigationService;
        private readonly IOpenFileService _openFileService;
        private readonly IWindowManagerService _windowManagerService;

        private ICollectionView collView;

        private ICommand _navigateToDetailCommand;
        public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<Project>(NavigateToDetail);
        public ProjectsViewModel(IProjectsDataService projectsDataService, INavigationService navigationService, IOpenFileService openFileService, IWindowManagerService windowManagerService)
        {
            _projectsDataService = projectsDataService;
            _navigationService = navigationService;
            _openFileService = openFileService;
            _windowManagerService = windowManagerService;

            FilteredList = new ObservableCollection<Project>();
        }

        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;

                collView.Filter = e =>
                {
                    var item = (Project)e;
                    return item != null && (item.IdProject.ToString().Contains(value) || string.IsNullOrWhiteSpace(value) || value.Length == 0);
                };

                collView.Refresh();

                FilteredList = new ObservableCollection<Project>(collView.OfType<Project>().ToList());

                RaisePropertyChanged("Filter");
            }
        }

        private Project _SelectedItem;
        public Project SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }

        private ObservableCollection<Project> _Records;
        public ObservableCollection<Project> Records
        {
            get => _Records;
            set
            {
                if (_Records != value)
                {
                    _Records = value;
                    RaisePropertyChanged("Records");
                }
            }
        }

        private ObservableCollection<Project> _FilteredList;
        public ObservableCollection<Project> FilteredList
        {
            get => _FilteredList;
            set
            {
                if (_FilteredList != value)
                {
                    _FilteredList = value;
                    RaisePropertyChanged("FilteredList");
                }
            }
        }

        private async void GetRecords()
        {
            //var data = await _projectsDataService.GetEcnRecordsAsync();

            //foreach (var item in data)
            //{
            //    item.ChangeType = await _ecnDataService.GetChangeTypeAsync(item.ChangeTypeId);
            //    item.DocumentType = await _ecnDataService.GetDocumentTypeAsync(item.DocumentTypeId);
            //    item.Status = await _ecnDataService.GetStatusAsync(item.StatusId);
            //    item.Employee = await _ecnDataService.GetEmployeeAsync(item.EmployeeId);

            //    item.ChangeTypeName = item.ChangeType.ChangeTypeName;
            //    item.DocumentTypeName = item.DocumentType.DocumentTypeName;
            //    item.EmployeeName = item.Employee.Name;
            //    item.StatusName = item.Status.StatusName;

            //    if (Convert.ToBoolean(item.IsEco))
            //    {
            //        item.EcnEco = await _ecnDataService.GetEcnEcoAsync(item.Id);
            //    }

            //    Records.Add(item);
            //}
        }

        public void OnNavigatedTo(object parameter)
        {
            Records = new ObservableCollection<Project>();
            GetRecords();

            FilteredList = new ObservableCollection<Project>(Records);
            collView = CollectionViewSource.GetDefaultView(FilteredList);

            SelectedItem = null;

            //DispatcherTimer timer = new DispatcherTimer
            //{
            //    Interval = TimeSpan.FromSeconds(15)
            //};
            //timer.Tick += new EventHandler(Timer_Tick);
            //timer.Start();
        }
        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    Records = new ObservableCollection<Ecn>();
        //    GetRecords();

        //    FilteredList = new ObservableCollection<Ecn>(Records);
        //    collView = CollectionViewSource.GetDefaultView(FilteredList);

        //    SelectedItem = null;
        //}

        public void OnNavigatedFrom()
        {

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

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
using System.Windows.Data;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class HistoryViewModel : ViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IProjectsDataService _projectsDataService;

        private ICommand _navigateToDetailCommand;
        public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<Project>(NavigateToDetail);

        private int _HistoryCount;
        public int HistoryCount
        {
            get => _HistoryCount;
            set
            {
                if (_HistoryCount != value)
                {
                    _HistoryCount = value;
                    RaisePropertyChanged("HistoryCount");
                }
            }
        }
        public HistoryViewModel(INavigationService navigationService, IProjectsDataService projectsDataService)
        {
            _navigationService = navigationService;
            _projectsDataService = projectsDataService;

            CvsHistory = new CollectionViewSource();

            CvsHistory.GroupDescriptions.Add(new PropertyGroupDescription("Year"));
            CvsHistory.GroupDescriptions.Add(new PropertyGroupDescription("MonthName"));
            CvsHistory.SortDescriptions.Add(new SortDescription("Year", ListSortDirection.Descending));
            CvsHistory.SortDescriptions.Add(new SortDescription("Month", ListSortDirection.Descending));
            CvsHistory.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));

            CvsHistory.Filter += ApplyFilter;
        }

        private CollectionViewSource _CvsHistory;
        public CollectionViewSource CvsHistory
        {
            get => _CvsHistory;
            set
            {
                if (_CvsHistory != value)
                {
                    _CvsHistory = value;
                    RaisePropertyChanged("CvsHistory");
                }
            }
        }

        private ObservableCollection<Project> _History;
        public ObservableCollection<Project> History
        {
            get => _History;
            set
            {
                if (_History != value)
                {
                    _History = value;
                    RaisePropertyChanged("History");
                }
            }
        }

        private async void GetHistory()
        {
            History = new ObservableCollection<Project>();
            var data = await _projectsDataService.GetHistoryAsync();

            foreach (var item in data)
            {
                item.IdStatusNavigation = await _projectsDataService.GetStatusAsync(item.IdStatus);
                //item.Employee = await _historyDataService.GetEmployeeAsync(item.EmployeeId);

                History.Add(item);

                HistoryCount = History.Count;

                //if (Convert.ToBoolean(item.IsEco))
                //{
                //    EcoHardStopsCount = History.Count(data => data.EcnEco.EcoType.EcoTypeId == 1);
                //    EcoRollingChangesCount = History.Count(data => data.EcnEco.EcoType.EcoTypeId == 2);
                //}

            }
        }

        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                OnFilterChanged();
            }
        }

        private void OnFilterChanged()
        {
            CvsHistory.View.Refresh();
        }

        private void ApplyFilter(object sender, FilterEventArgs e)
        {
            Project project = (Project)e.Item;

            e.Accepted = string.IsNullOrWhiteSpace(Filter) || Filter.Length == 0 || project.IdProject.ToString().Contains(Filter);
        }

        private void NavigateToDetail(Project project)
        {
            if (project != null)
            {
                _navigationService.NavigateTo(typeof(ProjectDetailsViewModel).FullName, project);
            }
        }

        public void OnNavigatedTo(object parameter)
        {
            CvsHistory.Source = null;

            GetHistory();
            CvsHistory.Source = History;
        }

        public void OnNavigatedFrom()
        {
        }
    }
}

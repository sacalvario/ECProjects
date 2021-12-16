﻿using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class HistoryViewModel : ViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IEcnDataService _historyDataService;
        private ICommand _navigateToDetailCommand;

        public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<Ecn>(NavigateToDetail);

        public HistoryViewModel(INavigationService navigationService, IEcnDataService historyDataService)
        {
            _navigationService = navigationService;
            _historyDataService = historyDataService;

            History = new ObservableCollection<Ecn>();
            GetHistory();

            CvsHistory = new CollectionViewSource
            {
                Source = History
            };

            CvsHistory.GroupDescriptions.Add(new PropertyGroupDescription("Year"));
            CvsHistory.GroupDescriptions.Add(new PropertyGroupDescription("MonthName"));
            CvsHistory.SortDescriptions.Add(new SortDescription("Year", ListSortDirection.Descending));
            CvsHistory.SortDescriptions.Add(new SortDescription("Month", ListSortDirection.Descending));
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

        private ObservableCollection<Ecn> _History;
        public ObservableCollection<Ecn> History
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
            var data = await _historyDataService.GetHistoryAsync();

            foreach (var item in data)
            {
                item.ChangeType = await _historyDataService.GetChangeTypeAsync(item.ChangeTypeId);
                item.DocumentType = await _historyDataService.GetDocumentTypeAsync(item.DocumentTypeId);
                item.Status = await _historyDataService.GetStatusAsync(item.StatusId);
                item.Employee = await _historyDataService.GetEmployeeAsync(item.EmployeeId);

                if (Convert.ToBoolean(item.IsEco))
                {
                    item.EcnEco = await _historyDataService.GetEcnEcoAsync(item.Id);
                }

                History.Add(item);
            }
        }

        private void NavigateToDetail(Ecn ecn)
        {
            _navigationService.NavigateTo(typeof(HistoryDetailsViewModel).FullName, ecn);
        }

        public void OnNavigatedTo(object parameter)
        {
        }

        public void OnNavigatedFrom()
        {
        }


    }
}
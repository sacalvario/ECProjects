﻿using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class SearchViewModel : ViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IEcnDataService _historyDataService;
        private readonly INumberPartsDataService _numberPartsDataService;

        public SearchViewModel(INavigationService navigationService, IEcnDataService ecnDataService, INumberPartsDataService numberPartsDataService)
        {
            _navigationService = navigationService;
            _historyDataService = ecnDataService;
            _numberPartsDataService = numberPartsDataService;

            CvsNumberPartHistory = new CollectionViewSource();

            CvsNumberPartHistory.GroupDescriptions.Add(new PropertyGroupDescription("Ecn.Year"));
            CvsNumberPartHistory.GroupDescriptions.Add(new PropertyGroupDescription("Ecn.MonthName"));
            CvsNumberPartHistory.SortDescriptions.Add(new SortDescription("Ecn.Year", ListSortDirection.Descending));
            CvsNumberPartHistory.SortDescriptions.Add(new SortDescription("Ecn.Month", ListSortDirection.Descending));
            CvsNumberPartHistory.SortDescriptions.Add(new SortDescription("Ecn.Id", ListSortDirection.Descending));

            CvsNumberPartHistory.Filter += ApplyFilter;

        }

        private ObservableCollection<EcnNumberpart> _NumberPartHistory;
        public ObservableCollection<EcnNumberpart> NumberPartHistory
        {
            get => _NumberPartHistory;
            set
            {
                if (_NumberPartHistory != value)
                {
                    _NumberPartHistory = value;
                    RaisePropertyChanged("NumberPartHistory");
                }
            }
        }

        private CollectionViewSource _CvsNumberPartHistory;
        public CollectionViewSource CvsNumberPartHistory
        {
            get => _CvsNumberPartHistory;
            set
            {
                if (_CvsNumberPartHistory != value)
                {
                    _CvsNumberPartHistory = value;
                    RaisePropertyChanged("CvsNumberPartHistory");
                }
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
            CvsNumberPartHistory.View.Refresh();
        }

        private void ApplyFilter(object sender, FilterEventArgs e)
        {
            EcnNumberpart ecn = (EcnNumberpart)e.Item;

            e.Accepted = string.IsNullOrWhiteSpace(Filter) || Filter.Length == 0 || ecn.Product.NumberPartId.ToLower().Contains(Filter.ToLower());
        }

        private ICommand _navigateToDetailCommand;
        public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<Ecn>(NavigateToDetail);

        private void NavigateToDetail(Ecn ecn)
        {
            if (ecn != null)
            {
                _navigationService.NavigateTo(typeof(HistoryDetailsViewModel).FullName, ecn);
            }
        }

        private async void GetNumberPartHistory()
        {
            NumberPartHistory = new ObservableCollection<EcnNumberpart>();
            var data = await _numberPartsDataService.GetHistoryAsync();

            foreach (var item in data)
            {
                item.Product = await _numberPartsDataService.GetNumberPartAsync(item.ProductId);
                item.Product.Customer = await _numberPartsDataService.GetCustomerAsync(item.Product.CustomerId);
                item.Ecn = await _historyDataService.GetEcnAsync(item.EcnId);
                item.Ecn.Employee = await _historyDataService.GetEmployeeAsync(item.Ecn.EmployeeId);
                item.Ecn.Status = await _historyDataService.GetStatusAsync(item.Ecn.StatusId);
                item.Ecn.ChangeType = await _historyDataService.GetChangeTypeAsync(item.Ecn.ChangeTypeId);
                item.Ecn.DocumentType = await _historyDataService.GetDocumentTypeAsync(item.Ecn.DocumentTypeId);

                if (Convert.ToBoolean(item.Ecn.IsEco))
                {
                    item.Ecn.EcnEco = await _historyDataService.GetEcnEcoAsync(item.EcnId);
                }

                NumberPartHistory.Add(item);
            }
        }

        public void OnNavigatedFrom()
        {
        }

        public void OnNavigatedTo(object parameter)
        {
            GetNumberPartHistory();
            CvsNumberPartHistory.Source = NumberPartHistory;
        }

    }
}

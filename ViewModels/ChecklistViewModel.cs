﻿using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.ViewModels;
using ProjectManager.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace ProjectManager.ViewModels
{
    public class ChecklistViewModel : ViewModelBase, INavigationAware
    {
        private readonly IEcnDataService _ecnDataService;
        private readonly INavigationService _navigationService;
        private ICommand _navigateToCheckCommand;

        public ICommand NavigateToCheckCommand => _navigateToCheckCommand ??= new RelayCommand<Ecn>(NavigateToCheck);

        public ChecklistViewModel(IEcnDataService ecnDataService, INavigationService navigationService)
        {
            _ecnDataService = ecnDataService;
            _navigationService = navigationService;

        }

        private ObservableCollection<Ecn> _Checklist;
        public ObservableCollection<Ecn> Checklist
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

        private int _ChecklistCount;
        public int ChecklistCount
        {
            get => _ChecklistCount;
            set
            {
                if (_ChecklistCount != value)
                {
                    _ChecklistCount = value;
                    RaisePropertyChanged("ChecklistCount");
                }
            }
        }


        private async void GetChecklist()
        {
            var data = await _ecnDataService.GetChecklistAsync();

            foreach (var item in data)
            {
                item.ChangeType = await _ecnDataService.GetChangeTypeAsync(item.ChangeTypeId);
                item.DocumentType = await _ecnDataService.GetDocumentTypeAsync(item.DocumentTypeId);
                item.Status = await _ecnDataService.GetStatusAsync(item.StatusId);
                item.Employee = await _ecnDataService.GetEmployeeAsync(item.EmployeeId);
                item.CurrentSignature = await _ecnDataService.GetCurrentSignatureAsync(item.Id);
                item.SignatureCount = _ecnDataService.GetSignatureCount(item.Id);

                if (Convert.ToBoolean(item.IsEco))
                {
                    item.EcnEco = await _ecnDataService.GetEcnEcoAsync(item.Id);
                }

                Checklist.Add(item);
            }
        }

        private void NavigateToCheck(Ecn ecn)
        {
            _navigationService.NavigateTo(typeof(HistoryDetailsViewModel).FullName, ecn);
        }

        public void OnNavigatedTo(object parameter)
        {
            Checklist = new ObservableCollection<Ecn>();
            GetChecklist();

            ChecklistCount = Checklist.Count;

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(15)
            };
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Checklist = new ObservableCollection<Ecn>();
            GetChecklist();

            ChecklistCount = Checklist.Count;
        }

        public void OnNavigatedFrom()
        {
        }
    }
}

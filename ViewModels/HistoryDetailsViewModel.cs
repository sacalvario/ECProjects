﻿using ECN.Contracts.Services;
using ECN.Contracts.ViewModels;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ECN.ViewModels
{
    public class HistoryDetailsViewModel : ViewModelBase, INavigationAware
    {
        private IEcnDataService _ecnDataService;
        private INumberPartsDataService _numberPartsDataService;
        private IOpenFileService _openFileService;
        private Ecn _ecn;

        public Ecn Ecn
        {
            get => _ecn;
            set
            {
                if (_ecn != value)
                {
                    _ecn = value;
                    RaisePropertyChanged("Ecn");
                }
            }
        }

        private Visibility _EcnRegisterTypeVisibility = Visibility.Collapsed;
        public Visibility EcnRegisterTypeVisibility
        {
            get => _EcnRegisterTypeVisibility;
            set
            {
                if (_EcnRegisterTypeVisibility != value)
                {
                    _EcnRegisterTypeVisibility = value;
                    RaisePropertyChanged("EcnRegisterTypeVisibility");
                }
            }
        }

        private Visibility _EcnIntExtTypeVisibility = Visibility.Visible;
        public Visibility EcnIntExtTypeVisibility
        {
            get => _EcnIntExtTypeVisibility;
            set
            {
                if (_EcnIntExtTypeVisibility != value)
                {
                    _EcnIntExtTypeVisibility = value;
                    RaisePropertyChanged("EcnIntExtTypeVisibility");
                }
            }
        }

        private Visibility _EcnHistoryTypeVisibility = Visibility.Visible;
        public Visibility EcnHistoryTypeVisibility
        {
            get => _EcnHistoryTypeVisibility;
            set
            {
                if (_EcnHistoryTypeVisibility != value)
                {
                    _EcnHistoryTypeVisibility = value;
                    RaisePropertyChanged("EcnHistoryTypeVisibility");
                }
            }
        }

        private Visibility _EcnSignTypeVisibility = Visibility.Collapsed;
        public Visibility EcnSignTypeVisibility
        {
            get => _EcnSignTypeVisibility;
            set
            {
                if (_EcnSignTypeVisibility != value)
                {
                    _EcnSignTypeVisibility = value;
                    RaisePropertyChanged("EcnSignTypeVisibility");
                }
            }
        }

        private Visibility _EcnNumberPartsVisibility = Visibility.Visible;
        public Visibility EcnNumberPartsVisibility
        {
            get => _EcnNumberPartsVisibility;
            set
            {
                if (_EcnNumberPartsVisibility != value)
                {
                    _EcnNumberPartsVisibility = value;
                    RaisePropertyChanged("EcnNumberPartsVisibility");
                }
            }
        }

        private ObservableCollection<Numberpart> _NumberParts;
        public ObservableCollection<Numberpart> NumberParts
        {
            get => _NumberParts;
            set
            {
                if (_NumberParts != value)
                {
                    _NumberParts = value;
                    RaisePropertyChanged("NumberParts");
                }
            }
        }

        private ObservableCollection<Attachment> _Attachments;
        public ObservableCollection<Attachment> Attachments
        {
            get => _Attachments;
            set
            {
                if (_Attachments != value)
                {
                    _Attachments = value;
                    RaisePropertyChanged("Attachments");
                }
            }
        }

        private ObservableCollection<EcnRevision> _Revisions;
        public ObservableCollection<EcnRevision> Revisions
        {
            get => _Revisions;
            set
            {
                if (_Revisions != value)
                {
                    _Revisions = value;
                    RaisePropertyChanged("Revisions");
                }
            }
        }

        private ObservableCollection<EcnDocumenttype> _Documents;
        public ObservableCollection<EcnDocumenttype> Documents
        {
            get => _Documents;
            set
            {
                if (_Documents != value)
                {
                    _Documents = value;
                    RaisePropertyChanged("Documents");
                } 
            }
        }

        private Attachment _SelectedAttachment;
        public Attachment SelectedAttachment
        {
            get => _SelectedAttachment;
            set
            {
                if (_SelectedAttachment != value)
                {
                    _SelectedAttachment = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ICommand _DownloadAttachmentCommand;
        public ICommand DownloadAttachmentCommand
        {
            get
            {
                if (_DownloadAttachmentCommand == null)
                {
                    _DownloadAttachmentCommand = new RelayCommand<Attachment>(DownloadAttachment);
                }
                return _DownloadAttachmentCommand;
            }
        }


        public HistoryDetailsViewModel(IEcnDataService ecnDataService, INumberPartsDataService numberPartsDataService, IOpenFileService openFileService)
        {
            Ecn = new Ecn();
            _ecnDataService = ecnDataService;
            _numberPartsDataService = numberPartsDataService;
            _openFileService = openFileService;
        }

        public void OnNavigatedFrom()
        {

        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is Ecn ecn)
            {
                Ecn = ecn;
            }

            if (Ecn.Employee != UserRecord.Employee)
            {
                EcnSignTypeVisibility = Visibility.Visible;
                EcnHistoryTypeVisibility = Visibility.Collapsed;
            }


            NumberParts = new ObservableCollection<Numberpart>();
            Attachments = new ObservableCollection<Attachment>();
            Revisions = new ObservableCollection<EcnRevision>();
            Documents = new ObservableCollection<EcnDocumenttype>();
            GetNumberParts();
            GetAttachments();
            GetRevisions();
            GetDocuments();

            if (Ecn.ChangeType.ChangeTypeId == 3)
            {
                EcnRegisterTypeVisibility = Visibility.Visible;
                EcnIntExtTypeVisibility = Visibility.Collapsed;
            }
            else
            {
                if (EcnIntExtTypeVisibility == Visibility.Collapsed)
                {
                    EcnIntExtTypeVisibility = Visibility.Visible;
                    EcnRegisterTypeVisibility = Visibility.Collapsed;
                }
            }

            if (NumberParts.Count == 0)
            {
                EcnNumberPartsVisibility = Visibility.Collapsed;
            }
        }

        private async void GetNumberParts()
        {
            Ecn.EcnNumberparts = await _numberPartsDataService.GetNumberPartsEcnsAsync(Ecn.Id);

            foreach(var item in Ecn.EcnNumberparts)
            {
                var np = await _numberPartsDataService.GetNumberPartAsync(item.ProductId);
                np.NumberPartTypeNavigation = await _numberPartsDataService.GetNumberpartTypeAsync(np.NumberPartType);
                np.Customer = await _numberPartsDataService.GetCustomerAsync(np.CustomerId);
                NumberParts.Add(np);
            }

        }

        private async void GetAttachments()
        {
            Ecn.EcnAttachments = await _ecnDataService.GetAttachmentsAsync(Ecn.Id);

            foreach (var item in Ecn.EcnAttachments)
            {
                var attached = await _ecnDataService.GetAttachmentAsync(item.AttachmentId);
                attached.Extension = Path.GetExtension(attached.AttachmentFilename);
                Attachments.Add(attached);
            }

        }

        private async void GetRevisions()
        {
            Ecn.EcnRevisions = await _ecnDataService.GetRevisionsAsync(Ecn.Id);

            foreach (var item in Ecn.EcnRevisions)
            {
                item.Employee = await _ecnDataService.GetEmployeeAsync(item.EmployeeId);
                item.Employee.Department = await _ecnDataService.GetDepartmentAsync(item.Employee.DepartmentId);
                item.Status = await _ecnDataService.GetStatusAsync(item.StatusId);
                Revisions.Add(item);
            }
        }

        private async void GetDocuments()
        {
            Ecn.EcnDocumenttypes = await _ecnDataService.GetDocumentsAsync(Ecn.Id);

            foreach (var item in Ecn.EcnDocumenttypes)
            {
                item.DocumentType = await _ecnDataService.GetDocumentTypeAsync(item.DocumentTypeId);
                Documents.Add(item);
            }

        }

        private void DownloadAttachment(Attachment attachment)
        {
            if (_openFileService.SaveFileDialog(attachment.AttachmentFilename))
            {
                File.WriteAllBytes(_openFileService.Path, attachment.AttachmentFile);

                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo(_openFileService.Path)
                    {
                        UseShellExecute = true
                    }
                };
                _ = process.Start();

            }
        }
    }
}

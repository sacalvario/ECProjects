﻿using ECN.Contracts.Services;
using ECN.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace ECN.ViewModels
{
    public class EcnViewModel : ViewModelBase
    {

        private readonly IEcnDataService _ecnDataService;
        public Ecn ECN { get; set; }
        public RelayCommand SaveECNCommand { get; set; }
        public RelayCommand OpenFileDialogCommand { get; set; }
        public RelayCommand OpenNumberPartsDialogCommand { get; set; }
        public RelayCommand RemoveNumberPartCommand { get; set; }
        public RelayCommand OpenSignatureFlowDialogCommand { get; set; }
        public RelayCommand RemoveAttachedCommand { get; set; }

        private Visibility _BtnAddNumberPartVisibility;
        public Visibility BtnAddNumberPartVisibility
        {
            get => _BtnAddNumberPartVisibility;
            set
            {
                if (_BtnAddNumberPartVisibility != value)
                {
                    _BtnAddNumberPartVisibility = value;
                    RaisePropertyChanged("BtnAddNumberPartVisibility");
                }
            }
        }
        private Visibility _BtnRemoveNumberPartVisibility;
        public Visibility BtnRemoveNumberPartVisibility
        {
            get => _BtnRemoveNumberPartVisibility;
            set
            {
                if (_BtnRemoveNumberPartVisibility != value)
                {
                    _BtnRemoveNumberPartVisibility = value;
                    RaisePropertyChanged("BtnRemoveNumberPartVisibility");
                }
            }
        }
        private Visibility _TxtEcoVisibility;
        public Visibility TxtEcoVisibility
        {
            get => _TxtEcoVisibility;
            set
            {
                if (_TxtEcoVisibility != value)
                {
                    _TxtEcoVisibility = value;
                    RaisePropertyChanged("TxtEcoVisibility");
                }
            }
        }

        private Visibility _CbEcoTypeVisibility;
        public Visibility CbEcoTypeVisibility
        {
            get => _CbEcoTypeVisibility;
            set
            {
                if (_CbEcoTypeVisibility != value)
                {
                    _CbEcoTypeVisibility = value;
                    RaisePropertyChanged("CbEcoTypeVisibility");
                }
            }
        }

        private Visibility _BtnRemoveAtachedVisibility;
        public Visibility BtnRemoveAttachedVisibility
        {
            get => _BtnRemoveAtachedVisibility;
            set
            {
                if (_BtnRemoveAtachedVisibility != value)
                {
                    _BtnRemoveAtachedVisibility = value;
                    RaisePropertyChanged("BtnRemoveAttachedVisibility");
                }
            }
        }

        private bool _IsEco;
        public bool IsEco
        {
            get => _IsEco;
            set
            {
                if (_IsEco != value)
                {
                    _IsEco = value;
                    RaisePropertyChanged("IsEco");
                    TxtEcoVisibility = IsEco ? Visibility.Visible : Visibility.Hidden;
                    CbEcoTypeVisibility = IsEco ? Visibility.Visible : Visibility.Hidden;
                    ECN.EndDate = IsEco ? DateTime.Now.AddDays(45) : DateTime.Now.AddDays(30);
                }
            }
        }

        public EcnViewModel(IEcnDataService ecnDataService)
        {
            _ecnDataService = ecnDataService;
            ECN = new Ecn();

            NumberParts = new ObservableCollection<Numberpart>();
            Attacheds = new ObservableCollection<Attachment>();
            SelectedForSign = new ObservableCollection<Employee>();
            SelectedForView = new ObservableCollection<Employee>();
            SetData();

            GetChangeTypes();
            GetDocumentTypes();
            GetEcoTypes();

            SaveECNCommand = new RelayCommand(SaveECN);
            OpenFileDialogCommand = new RelayCommand(OpenFileDialog);
            OpenNumberPartsDialogCommand = new RelayCommand(OpenNumberPartsDialog);
            RemoveNumberPartCommand = new RelayCommand(RemoveNumberPart);
            OpenSignatureFlowDialogCommand = new RelayCommand(OpenSignatureFlowDialog);
            RemoveAttachedCommand = new RelayCommand(RemoveAttached);

            BtnAddNumberPartVisibility = Visibility.Visible;
            BtnRemoveNumberPartVisibility = Visibility.Hidden;
            TxtEcoVisibility = Visibility.Hidden;
            CbEcoTypeVisibility = Visibility.Hidden;
            BtnRemoveAttachedVisibility = Visibility.Collapsed;
        }

        private void SaveECN()
        {
            if (ECN != null)
            {
                ECN.EmployeeId = 212;
                ECN.StatusId = 1;
                ECN.ChangeType = SelectedChangeType;
                ECN.DocumentType = SelectedDocumentType;

                if (IsEco)
                {
                    ECN.IsEco = Convert.ToSByte(IsEco);
                    EcnEco EcnEco = new EcnEco
                    {
                        IdEcnNavigation = ECN,
                        IdEco = Eco,
                        EcoType = SelectedEcoType
                    };
                    ECN.EcnEco = EcnEco;
                }


                if (NumberParts.Count > 0)
                {
                    foreach (Numberpart np in NumberParts)
                    {
                        ECN.EcnNumberparts.Add(new EcnNumberpart()
                        {
                            Ecn = ECN,
                            Product = np
                        });
                    }
                }

                if (Attacheds.Count > 0)
                {
                    foreach (Attachment ar in Attacheds)
                    {
                        ECN.EcnAttachments.Add(new EcnAttachment()
                        {
                            Attachment = ar,
                            Ecn = ECN
                        });
                    }
                }


                if (SelectedForSign.Count > 0)
                {
                    foreach (Employee er in SelectedForSign)
                    {
                        EcnRevision revision = new EcnRevision
                        {
                            Ecn = ECN,
                            Employee = er,
                            RevisionSequence = SelectedForSign.IndexOf(er) + 1,
                            StatusId = SetStatus(er),
                            Notes = ""
                        };
                        ECN.EcnRevisions.Add(revision);
                    }
                }

                try
                {
                    _ecnDataService.SaveEcn(ECN);
                    _ = MessageBox.Show("Record saving sucefully.");
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {

                    ResetData();
                }
            }
        }


        private int SetStatus(Employee er)
        {
            return SelectedForSign.IndexOf(er) == 0 ? 5 : 1;
        }

        private void OpenFileDialog()
        {
            //OpenFileDialog file = new OpenFileDialog();

            //if (file.ShowDialog() == true)
            //{
            //    Attacheds.Add(new AttachedRecord(Path.GetExtension(file.FileName))
            //    {
            //        Path = file.FileName,
            //        Name = file.SafeFileName
            //    });
            //}
        }

        private void OpenNumberPartsDialog()
        {
            Messenger.Default.Send(new NotificationMessage("ShowNumberParts"));
        }

        private void RemoveNumberPart()
        {
            if (SelectedNumberPart != null)
            {
                _ = NumberParts.Remove(SelectedNumberPart);
            }
        }

        private void RemoveAttached()
        {
            if (SelectedAttached != null)
            {
                _ = Attacheds.Remove(SelectedAttached);
            }
        }

        private void OpenSignatureFlowDialog()
        {
            Messenger.Default.Send(new NotificationMessage("ShowSignatureFlow"));
        }

        private void ResetData()
        {
            ECN.ChangeDescription = string.Empty;
            ECN = new Ecn();
        }

        private void SetData()
        {
            ECN.StartDate = DateTime.Now;
            ECN.EndDate = DateTime.Now.AddDays(30);
            ECN.DocumentUpgradeDate = DateTime.Now;
            IsEco = false;
        }

        private static ObservableCollection<Numberpart> _NumberParts;
        public static ObservableCollection<Numberpart> NumberParts
        {
            get => _NumberParts;
            set
            {
                if (_NumberParts != value)
                {
                    _NumberParts = value;
                    //RaisePropertyChanged("NumberParts");
                }
            }
        }

        private Numberpart _SelectedNumberPart;
        public Numberpart SelectedNumberPart
        {
            get => _SelectedNumberPart;
            set
            {
                if (_SelectedNumberPart != value)
                {
                    _SelectedNumberPart = value;
                    RaisePropertyChanged("SelectedNumberPart");

                    BtnRemoveNumberPartVisibility = _SelectedNumberPart != null ? Visibility.Visible : Visibility.Hidden;
                }
            }
        }

        private ObservableCollection<Changetype> _ChangeTypes;
        public ObservableCollection<Changetype> ChangeTypes
        {
            get => _ChangeTypes;
            set
            {
                if (_ChangeTypes != value)
                {
                    _ChangeTypes = value;
                    RaisePropertyChanged("ChangeTypes");
                }
            }
        }

        private ObservableCollection<Documenttype> _DocumentTypes;
        public ObservableCollection<Documenttype> DocumentTypes
        {
            get => _DocumentTypes;
            set
            {
                if (_DocumentTypes != value)
                {
                    _DocumentTypes = value;
                    RaisePropertyChanged("DocumentTypes");
                }
            }
        }

        private ObservableCollection<EcoType> _EcoTypes;
        public ObservableCollection<EcoType> EcoTypes
        {
            get => _EcoTypes;
            set
            {
                if (_EcoTypes != value)
                {
                    _EcoTypes = value;
                    RaisePropertyChanged("EcoTypes");
                }
            }
        }

        private EcoType _SelectedEcoType;
        public EcoType SelectedEcoType
        {
            get => _SelectedEcoType;
            set
            {
                if (_SelectedEcoType != value)
                {
                    _SelectedEcoType = value;
                    RaisePropertyChanged("SelectedEcoType");
                }
            }
        }

        private string _Eco;
        public string Eco
        {
            get => _Eco;
            set
            {
                if (_Eco != value)
                {
                    _Eco = value;
                    RaisePropertyChanged("Eco");
                }
            }
        }

        private ObservableCollection<Attachment> _Attacheds;
        public ObservableCollection<Attachment> Attacheds
        {
            get => _Attacheds;
            set
            {
                if (_Attacheds != value)
                {
                    _Attacheds = value;
                    RaisePropertyChanged("Attacheds");
                }
            }
        }

        private Attachment _SelectedAttached;
        public Attachment SelectedAttached
        {
            get => _SelectedAttached;
            set
            {
                if (_SelectedAttached != value)
                {
                    _SelectedAttached = value;
                    RaisePropertyChanged("SelectedAttached");
                    BtnRemoveAttachedVisibility = _SelectedAttached != null ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private Changetype _SelectedChangeType;
        public Changetype SelectedChangeType
        {
            get => _SelectedChangeType;
            set
            {
                if (_SelectedChangeType != value)
                {
                    _SelectedChangeType = value;
                    RaisePropertyChanged("SelectedChangeType");
                }
            }
        }

        private Documenttype _SelectedDocumentType;
        public Documenttype SelectedDocumentType
        {
            get => _SelectedDocumentType;
            set
            {
                if (_SelectedDocumentType != value)
                {
                    _SelectedDocumentType = value;
                    RaisePropertyChanged("SelectedDocumentType");
                }
            }
        }

        private ObservableCollection<Employee> _SelectedForView;
        public ObservableCollection<Employee> SelectedForView
        {
            get => _SelectedForView;
            set
            {
                if (_SelectedForView != value)
                {
                    _SelectedForView = value;
                    RaisePropertyChanged("SelectedForView");
                }
            }
        }

        private ObservableCollection<Employee> _SelectedForSign;
        public ObservableCollection<Employee> SelectedForSign
        {
            get => _SelectedForSign;
            set
            {
                if (_SelectedForSign != value)
                {
                    _SelectedForSign = value;
                    RaisePropertyChanged("SelectedForSign");
                }
            }
        }

        public async void GetChangeTypes()
        {
            ChangeTypes = new ObservableCollection<Changetype>();

            var data = await _ecnDataService.GetChangeTypesAsync();
            foreach(var item in data)
            {
                ChangeTypes.Add(item);
            }
        }

        public async void GetDocumentTypes()
        {
            DocumentTypes = new ObservableCollection<Documenttype>();

            var data = await _ecnDataService.GetDocumentTypesAsync();
            foreach (var item in data)
            {
                DocumentTypes.Add(item);
            }
        }

        public async void GetEcoTypes()
        {
            EcoTypes = new ObservableCollection<EcoType>();

            var data = await _ecnDataService.GetEcoTypesAsync();
            foreach (var item in data)
            {
                EcoTypes.Add(item);
            }

        }

    }
}
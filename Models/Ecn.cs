﻿using GalaSoft.MvvmLight;

using System;
using System.Collections.Generic;
using System.Windows;

#nullable disable

namespace ProjectManager.Models
{
    public partial class Ecn : ViewModelBase
    {
        public Ecn()
        {
            EcnAttachments = new HashSet<EcnAttachment>();
            EcnDocumenttypes = new HashSet<EcnDocumenttype>();
            EcnNumberparts = new HashSet<EcnNumberpart>();
            EcnRevisions = new HashSet<EcnRevision>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }

        private DateTime _EndDate = DateTime.Now.AddDays(30);
        public DateTime EndDate
        {
            get => _EndDate;
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    RaisePropertyChanged("EndDate");
                    RaisePropertyChanged("LongEndDate");
                }
            }
        }
        public int ChangeTypeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentName { get; set; }
        public string DocumentLvl { get; set; }
        public string DrawingLvl { get; set; }

        private string _OldDrawingLvl;
        public string OldDrawingLvl
        {
            get => _OldDrawingLvl;
            set
            {
                if (_OldDrawingLvl != value)
                {
                    _OldDrawingLvl = value;
                    RaisePropertyChanged("OldDrawingLvl");
                }
            }
        }
        public string OldDocumentLvl { get; set; }
        public int EmployeeId { get; set; }
        public sbyte IsEco { get; set; }

        private string _ChangeDescription;
        public string ChangeDescription
        {
            get => _ChangeDescription;
            set
            {
                if (_ChangeDescription != value)
                {
                    _ChangeDescription = value;
                    RaisePropertyChanged("ChangeDescription");
                }
            }
        }
        private string _ChangeJustification;
        public string ChangeJustification
        {
            get => _ChangeJustification;
            set
            {
                if (_ChangeJustification != value)
                {
                    _ChangeJustification = value;
                    RaisePropertyChanged("ChangeJustification");
                }
            }
        }
        private string _ManufacturingAffectations;
        public string ManufacturingAffectations
        {
            get => _ManufacturingAffectations;
            set
            {
                if (_ManufacturingAffectations != value)
                {
                    _ManufacturingAffectations = value;
                    RaisePropertyChanged("ManufacturingAffectations");
                }
            }
        }

        public string Notes { get; set; }

        private int _StatusId;
        public int StatusId
        {
            get => _StatusId;
            set
            {
                if (_StatusId != value)
                {
                    _StatusId = value;
                    RaisePropertyChanged("StatusId");
                }
            }
        }
        public int Year => StartDate.Year;
        public int Month => StartDate.Month;
        public string MonthName => System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(StartDate.Month);
        public int Day => StartDate.Day;
        public string ShortDate => StartDate.ToShortDateString();
        public string CutDate => StartDate.ToString("dd/MM");
        public string LongDate => StartDate.ToLongDateString();
        public string LongEndDate
        {
            get => EndDate.ToLongDateString();
            set { }
        }

        private Visibility _IsEcoVisibility = Visibility.Collapsed;
        public Visibility IsEcoVisibility
        {
            get
            {
                if (Convert.ToBoolean(IsEco))
                {
                    _IsEcoVisibility = Visibility.Visible;
                }
                else
                {
                    _IsEcoVisibility = Visibility.Collapsed;
                }

                return _IsEcoVisibility;
            }
        }


        public bool Is_Eco => Convert.ToBoolean(IsEco);
        public string IsEcoToString => Convert.ToBoolean(IsEco) ? "Sí" : "No";
        public int SignatureCount { get; set; }
        public string EmployeeName { get; set; }
        public string StatusName { get; set; }
        public string ChangeTypeName { get; set; }
        public string DocumentTypeName { get; set; }


        public virtual Changetype ChangeType { get; set; }
        public virtual Documenttype DocumentType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
        public virtual EcnEco EcnEco { get; set; } 
        public virtual EcnRevision CurrentSignature { get; set; }
        public virtual ICollection<EcnAttachment> EcnAttachments { get; set; }
        public virtual ICollection<EcnDocumenttype> EcnDocumenttypes { get; set; }
        public virtual ICollection<EcnNumberpart> EcnNumberparts { get; set; }
        public virtual ICollection<EcnRevision> EcnRevisions { get; set; }
    }
}

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectManager.Models;
using ProjectManager.Contracts.Services;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows.Media;
using SimpleWPFReporting;

namespace ProjectManager.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private IProjectsDataService _projectsDataService;
        private Project _Project;
        public Project Project
        {
            get => _Project;
            set
            {
                if (_Project != value)
                {
                    _Project = value;
                    RaisePropertyChanged("Project");
                }
            }
        }

        private ICommand _ExportPDFCommand;
        public ICommand ExportPDFCommand
        {
            get
            {
                if (_ExportPDFCommand == null)
                {
                    _ExportPDFCommand = new RelayCommand<Visual>(ExportECN);
                }
                return _ExportPDFCommand;
            }
        }

        public ReportViewModel(Project project, IProjectsDataService projectsDataService)
        {
            _projectsDataService = projectsDataService;
            Project = _projectsDataService.GetProjectWithInfoAsync(project.IdProject);
            Project = project;
        }

        private void ExportECN(Visual visual)
        {
            Report.ExportVisualAsPdf(visual);
        }
    }
}

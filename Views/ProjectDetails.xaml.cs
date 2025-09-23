using GalaSoft.MvvmLight.Messaging;
using ProjectManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectManager.Views
{
    /// <summary>
    /// Lógica de interacción para Project.xaml
    /// </summary>
    public partial class ProjectDetails : Page
    {
        public ProjectDetails()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<Models.Project>>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage<Models.Project> obj)
        {
            if (obj.Notification == "ShowReport")
            {
                var report = new Report
                {
                    DataContext = new ReportViewModel(obj.Content, ((ProjectDetailsViewModel)DataContext)._projectsDataService)
                };
                _ = report.ShowDialog();
            }
        }

    }
}

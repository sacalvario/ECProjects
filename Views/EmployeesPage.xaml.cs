﻿
using ProjectManager.ViewModels;

using GalaSoft.MvvmLight.Messaging;

using System.Windows.Controls;
using ProjectManager.Models;

namespace ProjectManager.Views
{
    /// <summary>
    /// Lógica de interacción para EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<Employee>>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage<Employee> obj)
        {
            if (obj.Notification == "ShowManageEmployeeWindow")
            {
                var addemploye = new AddEmployee
                {
                    DataContext = new AddEmployeeViewModel(obj.Content, ((EmployeesPageViewModel)DataContext)._ecnDataService, ((EmployeesPageViewModel)DataContext)._windowManagerService)
                };
                _ = addemploye.ShowDialog();
            }
        }

    }
}

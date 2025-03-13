using GalaSoft.MvvmLight.Messaging;
using ProjectManager.Models;
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
    /// Lógica de interacción para Emlpoyees.xaml
    /// </summary>
    public partial class Employees : Page
    {
        public Employees()
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
                    DataContext = new AddEmployeeViewModel(obj.Content, ((EmployeesViewModel)DataContext)._projectsDataService, ((EmployeesViewModel)DataContext)._windowManagerService)
                };
                _ = addemploye.ShowDialog();
            }
        }
    }
}

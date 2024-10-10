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
    /// Lógica de interacción para Customers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        public Customers()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived2);
        }

        private void NotificationMessageReceived2(NotificationMessage obj)
        {
            if (obj.Notification == "ShowManageCustomerWindow")
            {
                var addcustomer = new AddCustomer
                {
                    DataContext = new AddCustomerViewModel(((CustomersViewModel)DataContext)._projectsDataService, ((CustomersViewModel)DataContext)._windowManagerService)
                };
                _ = addcustomer.ShowDialog();
            }
        }
    }
}

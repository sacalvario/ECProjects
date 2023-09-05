using System.Windows.Controls;

using ProjectManager.Contracts.Views;
using GalaSoft.MvvmLight.Messaging;

namespace ProjectManager.Views
{
    public partial class ShellWindow : IShellWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "CloseWindow")
            {
                CloseWindow();
            }

        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();
    }
}

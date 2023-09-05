
using ProjectManager.Contracts.Views;

using System.Windows.Controls;

namespace ProjectManager.Views
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class ShellLogin : ILoginWindow
    {

        public ShellLogin()
        {
            InitializeComponent();
        }


        public void CloseWindow() => Close();

        public void ShowWindow() => Show();

        public Frame GetNavigationFrame() => loginFrame;
    }
}

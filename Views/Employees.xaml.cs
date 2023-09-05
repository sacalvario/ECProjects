
using ProjectManager.Contracts.Views;

namespace ProjectManager.Views
{
    /// <summary>
    /// Lógica de interacción para Employees.xaml
    /// </summary>
    public partial class Employees : IEmployeesWindow
    {
        public Employees()
        {
            InitializeComponent();
        }

        public void CloseWindow() => Close();


        public void ShowWindow() => Show();
    }
}

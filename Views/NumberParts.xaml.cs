
using ProjectManager.Contracts.Views;


namespace ProjectManager.Views
{
    /// <summary>
    /// Lógica de interacción para NumberParts.xaml
    /// </summary>
    public partial class NumberParts : INumberPartsWindow
    {
        public NumberParts()
        {
            InitializeComponent();
        }

        public void CloseWindow() => Close();

        public void ShowWindow() => Show();

    }
}

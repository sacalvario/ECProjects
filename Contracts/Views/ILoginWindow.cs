
using System.Windows.Controls;

namespace ProjectManager.Contracts.Views
{
    public interface ILoginWindow
    {
        void ShowWindow();

        void CloseWindow();

        Frame GetNavigationFrame();
    }
}

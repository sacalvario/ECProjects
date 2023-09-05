using System.Windows;

namespace ProjectManager.Contracts.Services
{
    public interface IWindowManagerService
    {
        Window MainWindow { get; }

        void OpenInNewWindow(string pageKey, object parameter = null);

        bool? OpenInDialog(string pageKey, object parameter);

        Window GetWindow(string pageKey);
    }
}

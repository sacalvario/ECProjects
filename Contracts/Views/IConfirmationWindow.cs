
namespace ProjectManager.Contracts.Views
{
    public interface IConfirmationWindow
    {
        void ShowWindow();
        void CloseWindow();
        bool Result();
    }
}

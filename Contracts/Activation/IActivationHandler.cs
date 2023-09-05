using System.Threading.Tasks;

namespace ProjectManager.Contracts.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle();

        Task HandleAsync();
    }
}

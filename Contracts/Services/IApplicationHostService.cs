using System.Threading.Tasks;

namespace ProjectManager.Contracts.Services
{
    public interface IApplicationHostService
    {
        Task StartAsync();

        Task StopAsync();
    }
}

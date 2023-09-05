using ProjectManager.Models;

namespace ProjectManager.Contracts.Services
{
    public interface ILoginDataService
    {
        User Login(string username, string password);
        bool IsNotRegistered(string employeeid);
        bool Exist(string employeeid);
        bool UsernameExist(string username);
        bool SaveUser(User user);
    }
}

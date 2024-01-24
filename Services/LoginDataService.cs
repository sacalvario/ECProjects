
using ProjectManager.Contracts.Services;
using ProjectManager.Models;

using System.Linq;

namespace ProjectManager.Services
{
    public class LoginDataService : ILoginDataService
    {
        private readonly projectsContext context = null;
        public LoginDataService()
        {
            context = new projectsContext();
        }

        public bool Exist(string employeeid)
        {
            if (employeeid.All(char.IsDigit))
            {
                int employeeno = int.Parse(employeeid);

                Employee employee = context.Employees.Find(employeeno);
                return employee != null;
            }
            return false;
        }

        public bool IsNotRegistered(string employeeid)
        {
            if (employeeid.All(char.IsDigit))
            {
                int employeeno = int.Parse(employeeid);

                User user = context.Users.Find(employeeno);

                return user == null;
            }
            return false;
        }

        public User Login(string username, string password)
        {
            if (username.All(char.IsDigit))
            {
                int employeenumber = int.Parse(username);
                return context.Users.FirstOrDefault(i => i.EmployeeId == employeenumber && i.Password == password);
            }
            else
            {
                return context.Users.FirstOrDefault(i => i.Username == username && i.Password == password);
            }
        }

        public bool SaveUser(User user)
        {
            if (user != null)
            {
                context.Users.Add(user);

                var result = context.SaveChanges();
                return result > 0;
            }
            return false;
        }

        public bool UsernameExist(string username)
        {
            User user = context.Users.Find(username);

            return user != null;
        }

    }
}

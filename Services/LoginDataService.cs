using ProjectManager.Contracts.Services;
using ProjectManager.Models;

using System.Linq;
using System.Collections.Concurrent;

namespace ProjectManager.Services
{
    public class LoginDataService : ILoginDataService
    {
        private readonly projectsContext context = null;

        // Simple cache for recent successful logins: key = "username:password"
        // stores minimal info to reconstruct a lightweight User without EF tracking
        private class UserCacheEntry
        {
            public int EmployeeId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private static readonly ConcurrentDictionary<string, UserCacheEntry> _loginCache = new ConcurrentDictionary<string, UserCacheEntry>();

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
            // simple cache key
            var key = username + ":" + password;

            if (_loginCache.TryGetValue(key, out var cached))
            {
                // return a lightweight detached User object
                return new User
                {
                    Username = cached.Username,
                    EmployeeId = cached.EmployeeId
                };
            }

            // Ensure the query is executed on DB side by using IQueryable/EF methods
            User found = null;
            if (username.All(char.IsDigit))
            {
                int employeenumber = int.Parse(username);
                found = context.Users.Where(i => i.EmployeeId == employeenumber && i.Password == password).FirstOrDefault();
            }
            else
            {
                found = context.Users.Where(i => i.Username == username && i.Password == password).FirstOrDefault();
            }

            if (found != null)
            {
                // cache minimal info
                var entry = new UserCacheEntry { EmployeeId = found.EmployeeId, Username = found.Username, Password = found.Password };
                _loginCache.TryAdd(key, entry);

                // return a detached copy to avoid EF tracking lifetimes
                return new User
                {
                    Username = found.Username,
                    EmployeeId = found.EmployeeId
                };
            }

            return null;
        }

        public bool SaveUser(User user)
        {
            if (user != null)
            {
                context.Users.Add(user);

                var result = context.SaveChanges();

                if (result > 0)
                {
                    // Update cache for newly created user
                    var key = user.Username + ":" + user.Password;
                    var entry = new UserCacheEntry { EmployeeId = user.EmployeeId, Username = user.Username, Password = user.Password };
                    _loginCache[key] = entry;
                }

                return result > 0;
            }
            return false;
        }

        public bool UsernameExist(string username)
        {
            // Try fast path with Find if username is key, otherwise query DB
            User user = context.Users.Find(username);

            if (user != null) return true;

            // fallback to DB query
            return context.Users.Where(u => u.Username == username).Any();
        }

    }
}

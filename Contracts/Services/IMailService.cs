using ProjectManager.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.Contracts.Services
{
    public interface IMailService
    {
        System.Threading.Tasks.Task SendAssignedManagerEmailAsync(string manageremail, string managername, string generatorname, int id, string customer);
        System.Threading.Tasks.Task SendNewTaskEmailAsync(string email, string generatoremail, int id, string resposiblename, string generatorname, string targetdate, string customer);
        System.Threading.Tasks.Task SendNewNprCreatedEmailAsync(string toEmail, string ccEmail, int projectId, string customer);
    }
}

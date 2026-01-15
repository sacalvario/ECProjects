using ProjectManager.Models;

using System.Collections.Generic;

namespace ProjectManager.Contracts.Services
{
    public interface IMailService
    {
        void SendAssignedManagerEmail(string manageremail, string managername, string generatorname, int id, string customer);
        void SendNewTaskEmail(string email, string generatoremail, int id, string resposiblename, string generatorname, string targetdate, string customer);
        void SendNewNprCreatedEmail(string toEmail, string ccEmail, int projectId, string customer);
    }
}

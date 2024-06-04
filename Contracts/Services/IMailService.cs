
using ProjectManager.Models;

using System.Collections.Generic;

namespace ProjectManager.Contracts.Services
{
    public interface IMailService
    {
        void SendAssignedManagerEmail(string manageremail, string managername, string generatorname, int id);
        void SendNewTaskEmail(string email, string generatoremail, int id, string resposiblename, string generatorname, string targetdate);
        void SendRefuseECNToGeneratorEmail(string email, int id, string refusedname, string generatorname, List<string> emails);
        void SendCloseECN(string email, int id, string generatorname);
        //void SendCancelECN(string email, Ecn ecn, string generatorname);
        void SendApprovedECN(int id, string generatorname, string generatoremail);
        void SendCloseECO(int id, string generatorname, string generatoremail);
    }
}

﻿
using ProjectManager.Models;

using System.Collections.Generic;

namespace ProjectManager.Contracts.Services
{
    public interface IMailService
    {
        void SendEmail(List<string> email, int id);
        void SendSignEmail(string email, string generatoremail, int id, string signedname, string generatorname);
        void SendRefuseECNToGeneratorEmail(string email, int id, string refusedname, string generatorname, List<string> emails);
        void SendCloseECN(string email, int id, string generatorname);
        void SendCancelECN(string email, Ecn ecn, string generatorname);
        void SendApprovedECN(int id, string generatorname, string generatoremail);
        void SendCloseECO(int id, string generatorname, string generatoremail);
    }
}

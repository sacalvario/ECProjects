﻿using ECN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECN.Contracts.Services
{
    public interface IEcnDataService
    {
        Task<IEnumerable<Ecn>> GetHistoryAsync();
        Task<Changetype> GetChangeTypeAsync(int id);
        Task<Documenttype> GetDocumentTypeAsync(int id);
        Task<Status> GetStatusAsync(int id);
        Task<EcnEco> GetEcnEcoAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int id);
        Task<IEnumerable<Changetype>> GetChangeTypesAsync();
        Task<IEnumerable<Documenttype>> GetDocumentTypesAsync();
        Task<IEnumerable<EcoType>> GetEcoTypesAsync();
        Task<Department> GetDepartmentAsync(int id);
        void SaveEcn(Ecn ecn);
    }
}

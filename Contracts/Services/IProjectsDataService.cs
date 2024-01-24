using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Contracts.Services
{
    public interface IProjectsDataService
    {
        Task<Employee> GetEmployeeAsync(int id);
    }
}

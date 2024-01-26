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
        Task<Department> GetDepartmentAsync(int id);
        Task<IEnumerable<Models.Task>> GetTasksAsync();
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        bool SaveProject(Project project);
    }
}

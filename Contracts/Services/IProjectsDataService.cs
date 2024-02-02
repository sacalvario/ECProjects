using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Contracts.Services
{
    public interface IProjectsDataService
    {
        Task<IEnumerable<Project>> GetHistoryAsync();
        Task<Employee> GetEmployeeAsync(int id);
        Task<Department> GetDepartmentAsync(int id);
        Task<Customer> GetCustomerAsync(int id);
        Task<Models.Task> GetTaskAsync(int id);
        Task<IEnumerable<Models.Task>> GetTasksAsync();
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<ICollection<ProjectTask>> GetActivitiesAsync(int project);
        bool SaveProject(Project project);
        Task<Status> GetStatusAsync(int id);
    }
}

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
        Task<Site> GetSiteAsync(int id);
        Task<Customer> GetCustomerAsync(int id);
        Task<Models.Task> GetTaskAsync(int id);
        Task<Project> GetProjectAsync(int id);
        Task<IEnumerable<Project>> GetHistoryAsync();
        Task<IEnumerable<Models.Task>> GetTasksAsync();
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<ICollection<ProjectTask>> GetActivitiesAsync(int project);
        Task<ICollection<ProjectTask>> GetTasksAsync(int employee);
        bool SaveProject(Project project);
        bool CompleteTask(ProjectTask task);
        bool AddCustomer(Customer customer);
        ProjectTask GetActiveTask(int project, int employee);
        ProjectTask GetOnlyActiveTask(int project);
        ProjectTask GetNextTask(int project, int task);
        Task<Status> GetStatusAsync(int id);
    }
}


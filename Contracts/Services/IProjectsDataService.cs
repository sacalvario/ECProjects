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
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        bool SaveProject(Project project);
        bool AddCustomer(Customer customer);
        bool CancelProject(Project project);
        bool AddEmployee(Employee employee);
        bool UpgradeEmployee(Employee employee);
        ProjectTask GetActiveTask(int project, int employee);
        ProjectTask GetOnlyActiveTask(int project);
        ProjectTask GetNextTask(int project, int task);
        Task<Status> GetStatusAsync(int id);
        Task<int> SavePartAsync(Part part);
        Task<bool> SaveProjectPartAsync(ProjectPart projectPart);
        Task<IEnumerable<ProjectPart>> GetProjectPartsWithPartInfoAsync(int projectId);
        System.Threading.Tasks.Task CompleteTaskAsync(ProjectTask activeTask);
        System.Threading.Tasks.Task UpdateTaskAsync(ProjectTask task);
    }
}


using ProjectManager.Contracts.Services;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Services
{
    public class ProjectsDataService : IProjectsDataService
    {
        private readonly projectsContext context = null;

        public ProjectsDataService()
        {
            context = new projectsContext();
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetEmployee(id);
        }

        public async Task<IEnumerable<Models.Task>> GetTasksAsync()
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetTasks();

        }

        private IEnumerable<Models.Task> GetTasks()
        {
            return context.Tasks.ToList().OrderBy(data => data.IdTask);
        }

        private Employee GetEmployee(int id)
        {
            Employee employee = context.Employees.Find(id);
            employee.IdDepartamentNavigation = context.Departments.Find(employee.IdDepartament);
            return employee;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetCustomers();
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return context.Customers.ToList().OrderBy(data => data.Name);
        }   

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetEmployees();
        }

        private IEnumerable<Employee> GetEmployees()
        {
            return context.Employees.ToList().Where(i => i.IdEmployee != UserRecord.Employee_ID);
        }

        public async Task<Department> GetDepartmentAsync(int id)
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetDepartment(id);
        }

        private Department GetDepartment(int id)
        {
            return context.Departments.Find(id);
        }

        public bool SaveProject(Project project)
        {
            if (project != null)
            {
                context.Projects.Add(project);

                var result = context.SaveChanges();
                return result > 0;
            }
            return false;
        }
    }
}

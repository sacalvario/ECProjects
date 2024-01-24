using ProjectManager.Contracts.Services;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
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

        private Employee GetEmployee(int id)
        {
            Employee employee = context.Employees.Find(id);
            employee.IdDepartamentNavigation = context.Departments.Find(employee.IdDepartament);
            return employee;
        }
    }
}

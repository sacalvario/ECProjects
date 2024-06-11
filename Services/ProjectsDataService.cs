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
        DateTime LastDate;

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

        public async Task<IEnumerable<Project>> GetHistoryAsync()
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetHistory();
        }

        private IEnumerable<Project> GetHistory()
        {
            using projectsContext context = new projectsContext();
            return context.Projects.Where(data => data.IdGeneratedby == UserRecord.Employee_ID).ToList();
        }

        public async Task<Status> GetStatusAsync(int id)
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetStatus(id);
        }

        private Status GetStatus(int id)
        {
            return context.Status.Find(id);
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetCustomer(id);
        }

        private Customer GetCustomer(int id)
        {
            return context.Customers.Find(id);
        }

        public async Task<ICollection<ProjectTask>> GetActivitiesAsync(int project)
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetActivities(project);
        }

        private ICollection<ProjectTask> GetActivities(int project)
        {
            using projectsContext context = new projectsContext();
            return context.ProjectTasks.Where(i => i.IdProject == project).ToList();
        }

        public async Task<Models.Task> GetTaskAsync(int id)
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetTask(id);
        }

        private Models.Task GetTask(int id)
        {
            return context.Tasks.Find(id);
        }

        public bool CompleteTask(ProjectTask task)
        {
            if (DateTime.Now > task.EndDate)
            {
                task.IdStatus = 1;
            }
            else
            {
                task.IdStatus = 4;
            }

            task.EndDate = DateTime.Now;

            List<ProjectTask> projectTasks = context.ProjectTasks.Where(i => i.IdProject == task.IdProject).ToList();

            foreach (ProjectTask obj in projectTasks)
            {
                if (obj.IdTask > task.IdTask)
                {

                    if (task.IdTask + 1 == obj.IdTask)
                    {
                        obj.IdStatus = 2;
                        obj.StartDate = DateTime.Now;
                        obj.EndDate = WorkDays(obj.Duration, obj.StartDate);

                        if (obj.IdTask < 5 || obj.IdTask > 7)
                        {
                            LastDate = obj.EndDate;
                        }
                    }
                    else
                    {
                        obj.StartDate = LastDate;
                        obj.EndDate = WorkDays(obj.Duration, obj.StartDate);

                        if (obj.IdTask < 5 || obj.IdTask > 7)
                        {
                            LastDate = obj.EndDate;
                        }
                    }
                }


            }
            var result = context.SaveChanges();
            return result > 0;
        }

        private DateTime WorkDays(int days, DateTime date)
        {

            for (int i = 1; i <= days; i++)
            {
                if (date.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                {
                    days++;
                }
                else if (date.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    days++;
                }
            }

            return date.AddDays(days);

        }

        public async Task<ICollection<ProjectTask>> GetTasksAsync(int employee)
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetTasks(employee); 
        }

        private ICollection<ProjectTask> GetTasks(int employee)
        {
            using projectsContext context = new projectsContext();
            return context.ProjectTasks.Where(i =>  i.IdEmployee == employee).ToList();
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetProject(id);
        }

        private Project GetProject(int id)
        {
            return context.Projects.Find(id);
        }

        public async Task<Site> GetSiteAsync(int id)
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetSite(id);
        }

        private Site GetSite(int id)
        {
            return context.Sites.Find(id);
        }

        public ProjectTask GetActiveTask(int project)
        {
            return context.ProjectTasks.First(i => i.IdProject == project && i.IdStatus == 2);
        }
    }
}

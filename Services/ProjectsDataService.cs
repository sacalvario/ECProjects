using Microsoft.EntityFrameworkCore;
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
        DateTime WorsDate;

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
            return context.Employees.ToList();
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

        public async Task<ICollection<ProjectTask>> GetTasksAsync(int employee)
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetTasks(employee);
        }

        private ICollection<ProjectTask> GetTasks(int employee)
        {
            using projectsContext context = new projectsContext();
            return context.ProjectTasks.Where(i => i.IdEmployee == employee && i.IdStatus == 2).ToList();
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

        public ProjectTask GetActiveTask(int project, int employee)
        {
            return context.ProjectTasks.FirstOrDefault(i => i.IdProject == project && i.IdStatus == 2 && i.IdEmployee == employee);
        }

        public bool AddCustomer(Customer customer)
        {
            if (customer != null)
            {
                context.Customers.Add(customer);

                var result = context.SaveChanges();
                return result > 0;
            }
            return false;
        }

        public ProjectTask GetOnlyActiveTask(int project)
        {
            return context.ProjectTasks.First(i => i.IdProject == project && i.IdStatus == 2);
        }

        public ProjectTask GetNextTask(int project, int task)
        {
            return context.ProjectTasks.First(i => i.IdProject == project && i.IdTask == task);
        }

        public bool CancelProject(Project project)
        {
            context.Projects.Update(project);
            project.IdStatus = 5;

            List<ProjectTask> projectTasks = context.ProjectTasks.Where(i => i.IdProject == project.IdProject && i.IdStatus == 2).ToList();
            foreach (var projectTask in projectTasks)
            {
                projectTask.IdStatus = 3;
            }

            var result = context.SaveChanges();
            return result > 0;

        }

        public bool AddEmployee(Employee employee)
        {

            if (employee != null)
            {
                context.Employees.Add(employee);

                var result = context.SaveChanges();
                return result > 0;
            }
            return false;
        }

        public bool UpgradeEmployee(Employee employee)
        {
            //employee.Active = Convert.ToSByte(employee.IsActive);
            _ = context.Update(employee);

            var result = context.SaveChanges();
            return result > 0;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            await System.Threading.Tasks.Task.CompletedTask;
            return GetDepartmens();
        }

        private IEnumerable<Department> GetDepartmens()
        {
            return context.Departments.ToList();
        }

        public async Task<int> SavePartAsync(Part part)
        {
            context.Parts.Add(part);
            await context.SaveChangesAsync();
            return part.IdPart;
        }

        public async Task<bool> SaveProjectPartAsync(ProjectPart projectPart)
        {
            context.ProjectParts.Add(projectPart);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProjectPart>> GetProjectPartsWithPartInfoAsync(int projectId)
        {
            return await context.ProjectParts
           .Where(pp => pp.IdProject == projectId)
           .Include(pp => pp.Part)
           .ThenInclude(p => p.Customer)
           .ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(ProjectTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));


            var existingTask = await context.ProjectTasks
                .FirstOrDefaultAsync(t => t.IdTask == task.IdTask);

            if (existingTask == null)
                throw new InvalidOperationException($"No se encontró la tarea con ID {task.IdTask}");

            // Actualizamos las propiedades necesarias
            existingTask.StartDate = task.StartDate;
            existingTask.EndDate = task.EndDate;
            existingTask.IdStatus = task.IdStatus;

            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task CompleteTaskAsync(ProjectTask activeTask)
        {
            if (activeTask == null)
                return;

            if (DateTime.Now > activeTask.EndDate)
                activeTask.IdStatus = 1; // Completada
            else
                activeTask.IdStatus = 4;

            activeTask.EndDate = DateTime.Now;

            await UpdateTaskAsync(activeTask);
            // Recalcular fechas en cascada
            await UpdateDependentTasksRecursive(activeTask);

            // Abrir tareas simultáneas si aplica (por ejemplo, 5–8 cuando se completa la 4)
            await OpenSimultaneousTasksIfNeeded(activeTask);

            var allTasks = await context.ProjectTasks
                        .Include(t => t.IdTaskNavigation)
                        .Where(t => t.IdProject == activeTask.IdProject)
                        .ToListAsync();

            foreach (var task in allTasks.Where(t =>
                t.IdStatus == 3 && // Pendiente
                t.IdTaskNavigation.PredecessorTaskId == activeTask.IdTaskNavigation.IdTask))
            {
                // Si la predecesora es la que acabamos de completar
                task.IdStatus = 2; // En proceso
                await UpdateTaskAsync(task);
            }

            var allTasksCompleted = await context.ProjectTasks
                                    .Where(t => t.IdProject == activeTask.IdProject)
                                    .AllAsync(t => t.IdStatus == 1 || t.IdStatus == 4); // 1 y 4: tareas finalizadas

            if (allTasksCompleted)
            {
                var project = await context.Projects.FirstOrDefaultAsync(p => p.IdProject == activeTask.IdProject);
                if (project != null)
                {
                    project.IdStatus = 4; // Proyecto completado
                    await context.SaveChangesAsync();
                }
            }

            // Notifica al encargado de la(s) siguiente(s) tarea(s)
            //var nextTasks = await context.ProjectTasks
            //        .Include(t => t.IdTaskNavigation)
            //        .Include(t => t.IdEmployeeNavigation)
            //        .Where(t => t.IdTaskNavigation.PredecessorTaskId == activeTask.IdTaskNavigation.IdTask && t.IdProject == activeTask.IdProject)
            //        .ToListAsync();

            //foreach (var nextTask in nextTasks)
            //{
            //    // Aquí puedes cambiar esto en producción
            //    await _emailService.SendEmailAsync(
            //        "scalvario@ecmfg.com", // Reemplazar con nextTask.Employee.Email después de pruebas
            //        "Nueva tarea disponible",
            //        $"Ya puedes iniciar tu tarea \"{nextTask.IdTaskNavigation.Name}\" del proyecto."
            //    );
            //}

        }

        private async System.Threading.Tasks.Task UpdateDependentTasksRecursive(ProjectTask modifiedTask)
        {
            var dependents = await context.ProjectTasks
                .Include(t => t.IdTaskNavigation)
                .Where(t => t.IdTaskNavigation.PredecessorTaskId == modifiedTask.IdTaskNavigation.IdTask && t.IdProject == modifiedTask.IdProject)
                .ToListAsync();

            foreach (var dependent in dependents)
            {
                    dependent.StartDate = modifiedTask.EndDate;
                    dependent.EndDate = CalculateEndDate(dependent.StartDate, dependent.Duration);

                    await UpdateTaskAsync(dependent);

                    // Recursividad
                    await UpdateDependentTasksRecursive(dependent);
            }
        }

        private DateTime CalculateEndDate(DateTime startDate, int duration)
        {
            DateTime date = startDate;
            int added = 0;
            while (added < duration)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    added++;
            }
            return date;
        }

        private async System.Threading.Tasks.Task OpenSimultaneousTasksIfNeeded(ProjectTask completedTask)
        {
            // Ejemplo estático: si se completó la tarea 4, abrir tareas 5, 6, 7, 8
            if (completedTask.IdTaskNavigation.IdTask == 4)
            {
                var simultaneousTaskIds = new List<int> { 5, 6, 7, 8 };

                var tasksToOpen = await context.ProjectTasks
                    .Include(t => t.IdTaskNavigation)
                    .Where(t => simultaneousTaskIds.Contains(t.IdTaskNavigation.IdTask))
                    .ToListAsync();

                foreach (var task in tasksToOpen)
                {
                    task.StartDate = DateTime.Now;
                    task.EndDate = CalculateEndDate(task.StartDate, task.Duration);
                    task.IdStatus = 2; // Abierta
                    await UpdateTaskAsync(task);
                }
            }
        }

        public Project GetProjectWithInfoAsync(int id)
        {
            using (var context = new projectsContext())
            {
                return context.Projects
                    .Include(e => e.ProjectParts)
                        .ThenInclude(ea => ea.Part)
                            .ThenInclude(np => np.Customer)
                    .Include(e => e.ProjectTasks)
                        .ThenInclude(enp => enp.IdTaskNavigation)
                    .Include(e => e.ProjectTasks)
                        .ThenInclude(pt => pt.IdEmployeeNavigation)
                            .ThenInclude(emp => emp.IdDepartamentNavigation)
                    .Include(e => e.ProjectTasks)
                        .ThenInclude(pt => pt.IdStatusNavigation)
                    .Include(e => e.IdManagerNavigation)
                        .ThenInclude(c => c.IdDepartamentNavigation)
                    .Include(e => e.IdGeneratedbyNavigation)
                        .ThenInclude(c => c.IdDepartamentNavigation)
                     .Include(e => e.IdStatusNavigation)
                     .Include(e => e.IdCustomerNavigation)
                    .FirstOrDefault(e => e.IdProject == id);
            }
        }

        public async Task<ICollection<CustomProjectTask>> GetCustomActivitiesAsync(int project)
        {
            var customActivities = await context.CustomProjectTasks      // Incluye la tarea predecesora
                                .Include(c => c.Status)         // Incluye el estado
                                .Include(c => c.IdEmployeeNavigation)       // Incluye el empleado responsable
                                    .ThenInclude(e => e.IdDepartamentNavigation) // Incluye el departamento
                                .Where(c => c.ProjectId == project)        // Opcional: ordena por número de tarea
                                .ToListAsync();

            return customActivities;
        }
    }

}



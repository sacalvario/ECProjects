using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProjectManager.Models
{
    public partial class projectsContext : DbContext
    {
        public projectsContext()
        {
        }

        public projectsContext(DbContextOptions<projectsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<ProjectPart> ProjectParts{ get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=192.168.36.4;database=projects;user id=usermysql;password=user", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCustomer)
                    .HasName("PRIMARY");

                entity.ToTable("customers");

                entity.HasIndex(e => e.IdCustomer, "ID_Customer_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(e => e.IdPart)
                    .HasName("PRIMARY");

                entity.ToTable("parts");

                entity.HasIndex(e => e.IdPart, "NO_Part_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PartNumber, "ID_Part_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CustomerId, "customer_id_idx");

                entity.Property(e => e.IdPart).HasColumnName("NO_Part");

                entity.Property(e => e.CustomerId).HasColumnName("ID_Customer");

                entity.Property(e => e.PartNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ID_Part");

                entity.Property(e => e.Revision)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Revision");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Numberparts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_id");
            });

            modelBuilder.Entity<ProjectPart>(entity =>
            {
                entity.HasKey(e => new { e.IdPart, e.IdProject })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("project_parts");

                entity.HasIndex(e => e.IdProject, "project_id_idx");

                entity.HasIndex(e => e.IdPart, "part_id_idx");

                entity.Property(e => e.IdProject).HasColumnName("ID_Project");

                entity.Property(e => e.IdPart).HasColumnName("ID_Part");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectParts)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("project_id");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.ProjectParts)
                    .HasForeignKey(d => d.IdPart)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("part_id");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.IdDepartment)
                    .HasName("PRIMARY");

                entity.ToTable("departments");

                entity.HasIndex(e => e.IdDepartment, "ID_Department_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdDepartment).HasColumnName("ID_Department");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("PRIMARY");

                entity.ToTable("employees");

                entity.HasIndex(e => e.IdEmployee, "ID_Employee_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdDepartament, "fkdepartment_idx");

                entity.HasIndex(e => e.IdSite, "fksite_idx");

                entity.Property(e => e.IdEmployee)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Employee");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("First_Name");

                entity.Property(e => e.IdDepartament).HasColumnName("ID_Departament");

                entity.Property(e => e.IdSite).HasColumnName("ID_Site");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("Last_Name");

                entity.HasOne(d => d.IdDepartamentNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdDepartament)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkdepartment");

                entity.HasOne(d => d.IdSiteNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdSite)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fksite");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdProject)
                    .HasName("PRIMARY");

                entity.ToTable("projects");

                entity.HasIndex(e => e.IdProject, "ID_Project_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdCustomer, "fk_customer_idx");

                entity.HasIndex(e => e.IdManager, "fk_manager_idx");

                entity.HasIndex(e => e.IdStatus, "fk_status_idx");

                entity.HasIndex(e => e.IdGeneratedby, "fkgeneratedby_idx");

                entity.Property(e => e.IdProject).HasColumnName("ID_Project");

                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.CustomerNeedby).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");

                entity.Property(e => e.IdGeneratedby).HasColumnName("ID_Generatedby");

                entity.Property(e => e.IdManager).HasColumnName("ID_Manager");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.QuoteNumber)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customer");

                entity.HasOne(d => d.IdGeneratedbyNavigation)
                    .WithMany(p => p.ProjectIdGeneratedbyNavigations)
                    .HasForeignKey(d => d.IdGeneratedby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_generatedby");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.ProjectIdManagerNavigations)
                    .HasForeignKey(d => d.IdManager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_manager");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_status");
            });

            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.HasKey(e => new { e.IdProject, e.IdTask })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("project_tasks");

                entity.HasIndex(e => e.IdEmployee, "fk-employee_idx");

                entity.HasIndex(e => e.IdStatus, "fk-status_idx");

                entity.HasIndex(e => e.IdTask, "fk-task_idx");

                entity.Property(e => e.IdProject).HasColumnName("ID_Project");

                entity.Property(e => e.IdTask).HasColumnName("ID_Task");

                entity.Property(e => e.CompletationDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.ReadyToBuildDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Comments)
                     .HasMaxLength(500);

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-employee");

                entity.HasOne(d => d.IdProjectNavigation)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-project");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-status");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.ProjectTaskId)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-task");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasKey(e => e.IdSite)
                    .HasName("PRIMARY");

                entity.ToTable("sites");

                entity.HasIndex(e => e.IdSite, "ID_Site_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdSite).HasColumnName("ID_Site");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PRIMARY");

                entity.ToTable("status");

                entity.HasIndex(e => e.IdStatus, "ID_Status_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.IdTask)
                    .HasName("PRIMARY");

                entity.ToTable("tasks");

                entity.HasIndex(e => e.IdTask, "ID_Task_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdTask).HasColumnName("ID_Task");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(90);

                entity.Property(e => e.Number).HasColumnType("float");

                modelBuilder.Entity<Task>()
                .HasOne(t => t.PredecessorTask)
                .WithMany(t => t.DependentTasks)
                .HasForeignKey(t => t.PredecessorTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.HasIndex(e => e.EmployeeId, "Employee_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "Username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Employee_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_fk_id");
            });

            OnModelCreatingPartial(modelBuilder);
            _ = modelBuilder.Entity<Employee>().Ignore(t => t.Name);
            _ = modelBuilder.Entity<Employee>().Ignore(t => t.IsActive);
            _ = modelBuilder.Entity<ProjectTask>().Ignore(t => t.EmployeeList);
            _ = modelBuilder.Entity<Employee>().Ignore(t => t.ActiveText);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

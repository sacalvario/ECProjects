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
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=projects;userid=root;password=user", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));
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

                entity.Property(e => e.Active).HasColumnName("Active");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("Last_Name");

                entity.HasOne(d => d.IdDepartamentNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdDepartament)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkdepartment");
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

                entity.HasIndex(e => e.Predecessor, "fk-predecessor_idx");

                entity.HasIndex(e => e.IdStatus, "fk-status_idx");

                entity.HasIndex(e => e.IdTask, "fk-task_idx");

                entity.Property(e => e.IdProject).HasColumnName("ID_Project");

                entity.Property(e => e.IdTask).HasColumnName("ID_Task");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.CompletationDate).HasColumnType("date");

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
                    .WithMany(p => p.ProjectTaskIdTaskNavigations)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-task");

                entity.HasOne(d => d.PredecessorNavigation)
                    .WithMany(p => p.ProjectTaskPredecessorNavigations)
                    .HasForeignKey(d => d.Predecessor)
                    .HasConstraintName("fk-predecessor");
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
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

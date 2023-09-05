using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProjectManager.ProjectsModel
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

        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        public virtual DbSet<ProjectPart> ProjectParts { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=192.168.36.4;database=projects;user=usermysql;password=user", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasKey(e => e.IdAttachment)
                    .HasName("PRIMARY");

                entity.ToTable("attachments");

                entity.HasIndex(e => e.IdAttachment, "ID_Attachment_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdAttachment).HasColumnName("ID_Attachment");

                entity.Property(e => e.File).IsRequired();

                entity.Property(e => e.FileLink)
                    .IsRequired()
                    .HasMaxLength(500);
            });

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

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.IdDocument)
                    .HasName("PRIMARY");

                entity.ToTable("documents");

                entity.HasIndex(e => e.IdDocument, "ID_Document_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdDocument).HasColumnName("ID_Document");

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

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.IdGroup)
                    .HasName("PRIMARY");

                entity.ToTable("groups");

                entity.HasIndex(e => e.IdGroup, "ID_Group_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdGroup).HasColumnName("ID_Group");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Industry>(entity =>
            {
                entity.HasKey(e => e.IdIndustry)
                    .HasName("PRIMARY");

                entity.ToTable("industries");

                entity.HasIndex(e => e.IdIndustry, "ID_Industry_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdIndustry).HasColumnName("ID_Industry");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.IdLocation)
                    .HasName("PRIMARY");

                entity.ToTable("locations");

                entity.HasIndex(e => e.IdLocation, "ID_Location_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdLocation).HasColumnName("ID_Location");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(e => e.IdPart)
                    .HasName("PRIMARY");

                entity.ToTable("parts");

                entity.HasIndex(e => e.IdPart, "ID_Part_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.NoPart, "No_Part_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdCustomer, "fkcustomer_idx");

                entity.Property(e => e.IdPart).HasColumnName("ID_Part");

                entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");

                entity.Property(e => e.NoPart)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("No_Part");

                entity.Property(e => e.Revision)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Parts)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkcustomer");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdProject)
                    .HasName("PRIMARY");

                entity.ToTable("projects");

                entity.HasIndex(e => e.IdProject, "ID_Project_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdAttach, "fk_attach_idx");

                entity.HasIndex(e => e.IdCustomer, "fk_customer_idx");

                entity.HasIndex(e => e.IdDocument, "fk_document_idx");

                entity.HasIndex(e => e.IdIndustry, "fk_industry_idx");

                entity.HasIndex(e => e.IdLocation, "fk_location_idx");

                entity.HasIndex(e => e.IdManager, "fk_manager_idx");

                entity.HasIndex(e => e.IdStatus, "fk_status_idx");

                entity.HasIndex(e => e.IdGeneratedby, "fkgeneratedby_idx");

                entity.Property(e => e.IdProject).HasColumnName("ID_Project");

                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.FolderLink)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.IdAttach).HasColumnName("ID_Attach");

                entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");

                entity.Property(e => e.IdDocument).HasColumnName("ID_Document");

                entity.Property(e => e.IdGeneratedby).HasColumnName("ID_Generatedby");

                entity.Property(e => e.IdIndustry).HasColumnName("ID_Industry");

                entity.Property(e => e.IdLocation).HasColumnName("ID_Location");

                entity.Property(e => e.IdManager).HasColumnName("ID_Manager");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.QuoteNumber)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.IdAttachNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdAttach)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_attach");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customer");

                entity.HasOne(d => d.IdDocumentNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_document");

                entity.HasOne(d => d.IdGeneratedbyNavigation)
                    .WithMany(p => p.ProjectIdGeneratedbyNavigations)
                    .HasForeignKey(d => d.IdGeneratedby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_generatedby");

                entity.HasOne(d => d.IdIndustryNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdIndustry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_industry");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.IdLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_location");

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

            modelBuilder.Entity<ProjectEmployee>(entity =>
            {
                entity.HasKey(e => new { e.IdProject, e.IdEmployee })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("project_employees");

                entity.HasIndex(e => e.IdEmployee, "employee_fk_idx");

                entity.Property(e => e.IdProject).HasColumnName("ID_Project");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.ProjectEmployees)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_fk");

                entity.HasOne(d => d.IdProjectNavigation)
                    .WithMany(p => p.ProjectEmployees)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("project_fk");
            });

            modelBuilder.Entity<ProjectPart>(entity =>
            {
                entity.HasKey(e => new { e.IdProject, e.IdPart })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("project_parts");

                entity.HasIndex(e => e.IdPart, "part-fk_idx");

                entity.Property(e => e.IdProject).HasColumnName("ID_Project");

                entity.Property(e => e.IdPart).HasColumnName("ID_Part");

                entity.HasOne(d => d.IdPartNavigation)
                    .WithMany(p => p.ProjectParts)
                    .HasForeignKey(d => d.IdPart)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("part-fk");

                entity.HasOne(d => d.IdProjectNavigation)
                    .WithMany(p => p.ProjectParts)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("project-fk");
            });

            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.HasKey(e => new { e.IdProject, e.IdTask })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("project_tasks");

                entity.HasIndex(e => e.IdEmployee, "fk-employee_idx");

                entity.HasIndex(e => e.IdTask, "fk-task_idx");

                entity.Property(e => e.IdProject).HasColumnName("ID_Project");

                entity.Property(e => e.IdTask).HasColumnName("ID_Task");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.Property(e => e.StartDate).HasColumnType("date");

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

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-task");
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

                entity.HasIndex(e => e.IdGroup, "fkgroup_idx");

                entity.Property(e => e.IdTask).HasColumnName("ID_Task");

                entity.Property(e => e.IdGroup).HasColumnName("ID_Group");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(90);

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkgroup");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

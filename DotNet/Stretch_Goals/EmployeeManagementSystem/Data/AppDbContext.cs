using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure JSON fields for SQL Server
            modelBuilder.Entity<Employee>()
                .Property(e => e.SkillsJSON)
                .HasColumnType("nvarchar(MAX)");

            modelBuilder.Entity<Employee>()
                .Property(e => e.AddressJSON)
                .HasColumnType("nvarchar(MAX)");

            modelBuilder.Entity<Project>()
                .Property(p => p.TeamMembersJSON)
                .HasColumnType("nvarchar(MAX)");

            modelBuilder.Entity<Salary>()
                .Property(s => s.AllowancesJSON)
                .HasColumnType("nvarchar(MAX)");

            modelBuilder.Entity<Salary>()
                .Property(s => s.DeductionsJSON)
                .HasColumnType("nvarchar(MAX)");

            // Configure relationships

            // Department -> Employee (One-to-Many)
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department -> Project (One-to-Many)
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Projects)
                .WithOne(p => p.Department)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee -> Salary (One-to-Many)
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Salaries)
                .WithOne(s => s.Employee)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Department Manager (Self-referencing)
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
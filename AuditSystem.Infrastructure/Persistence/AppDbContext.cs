using Audit_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /*------------------------------------------------------------------*/
        #region Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        #endregion
        /*------------------------------------------------------------------*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Seeding IDs Variables
            var adminId = new Guid("00000000-0000-0000-0000-000000000001");
            var user1Id = new Guid("A1B2C3D4-E5F6-4A7B-8C9D-0E1F2A3B4C5D");
            var user2Id = new Guid("B2C3D4E5-F6A7-5B8C-9D0E-1F2A3B4C5D6E");

            var course1Id = Guid.Parse("C1C1C1C1-1111-1111-1111-C1C1C1C1C1C1");
            var course2Id = Guid.Parse("C2C2C2C2-2222-2222-2222-C2C2C2C2C2C2");
            var course3Id = Guid.Parse("C3C3C3C3-3333-3333-3333-C3C3C3C3C3C3");

            var enrollment1Id = Guid.Parse("E1E1E1E1-1111-1111-1111-E1E1E1E1E1E1");
            var enrollment2Id = Guid.Parse("E2E2E2E2-2222-2222-2222-E2E2E2E2E2E2");
            var enrollment3Id = Guid.Parse("E3E3E3E3-3333-3333-3333-E3E3E3E3E3E3");
            var enrollment4Id = Guid.Parse("E4E4E4E4-4444-4444-4444-E4E4E4E4E4E4");

            var log1Id = Guid.Parse("F1F1F1F1-1111-1111-1111-F1F1F1F1F1F1");
            var log2Id = Guid.Parse("F2F2F2F2-2222-2222-2222-F2F2F2F2F2F2");
            var log3Id = Guid.Parse("F3F3F3F3-3333-3333-3333-F3F3F3F3F3F3");
            var log4Id = Guid.Parse("F4F4F4F4-4444-4444-4444-F4F4F4F4F4F4");
            var log5Id = Guid.Parse("F5F5F5F5-5555-5555-5555-F5F5F5F5F5F5");
            var log6Id = Guid.Parse("F6F6F6F6-6666-6666-6666-F6F6F6F6F6F6");
            var log7Id = Guid.Parse("F7F7F7F7-7777-7777-7777-F7F7F7F7F7F7");
            var log8Id = Guid.Parse("F8F8F8F8-8888-8888-8888-F8F8F8F8F8F8");
            var log9Id = Guid.Parse("F9F9F9F9-9999-9999-9999-F9F9F9F9F9F9");
            var log10Id = Guid.Parse("FAFAFAFA-AAAA-AAAA-AAAA-FAFAFAFAFAFA");
            #endregion

            #region User Configration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasData(
                    new User { Id = adminId, Name = "Admin", Email = "admin@gmail.com", PasswordHash = "Admin@123" },
                    new User { Id = user1Id, Name = "Ali", Email = "ali@gmail.com", PasswordHash = "Ali@123" },
                    new User { Id = user2Id, Name = "Sara", Email = "sara@gmail.com", PasswordHash = "Sara@123" }
                    );
                #endregion

                #region Course Configration
                modelBuilder.Entity<Course>(entity =>
                {
                    entity.HasKey(c => c.Id);
                    entity.Property(c => c.Price).HasColumnType("decimal(18,2)");

                    entity.HasData(
                        new Course { Id = course1Id, Title = "C# Basics", Description = "Learn the fundamentals of C# programming.", Author= "Suzanne Collin", Price= 200m },
                        new Course { Id = course2Id, Title = "ASP.NET Core", Description = "Build web applications using ASP.NET Core.", Author = "Andrew Lock", Price = 200m },
                        new Course { Id = course3Id, Title = "Entity Framework Core", Description = "Master data access with Entity Framework Core.", Author = "Julia Lerman", Price = 300m }
                        );
                });
                #endregion

                #region Enrollment Configration
                modelBuilder.Entity<Enrollment>(entity =>
                {
                    entity.HasKey(e => e.Id);

                    entity.HasOne(e => e.User)
                          .WithMany(u => u.Enrollments)
                          .HasForeignKey(e => e.UserId)
                          .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(e => e.Course)
                          .WithMany(c => c.Enrollments)
                          .HasForeignKey(e => e.CourseId)
                          .OnDelete(DeleteBehavior.Restrict);

                    entity.HasData(
                        new Enrollment { Id = enrollment1Id, UserId = user1Id, CourseId = course1Id, Timestamp = new DateTime(2026, 1, 1) },
                        new Enrollment { Id = enrollment2Id, UserId = user1Id, CourseId = course2Id, Timestamp = new DateTime(2026, 1, 1) },
                        new Enrollment { Id = enrollment3Id, UserId = user2Id, CourseId = course2Id, Timestamp = new DateTime(2026, 1, 1) },
                        new Enrollment { Id = enrollment4Id, UserId = user2Id, CourseId = course3Id, Timestamp = new DateTime(2026, 1, 1) }
                       );
                });
                #endregion

                #region AuditLog Configration
                modelBuilder.Entity<AuditLog>(entity =>
                {
                    entity.HasKey(a => a.Id);

                    entity.HasData(
                        new AuditLog { Id = log1Id, UserId = adminId, Action = "INSERT", EntityName = "User", EntityId = adminId.ToString(), Timestamp = new DateTime(2026, 1, 1), Metadata = "Initial System Admin Created." },
                        new AuditLog { Id = log2Id, UserId = adminId, Action = "INSERT", EntityName = "User", EntityId = user1Id.ToString(), Timestamp = new DateTime(2026, 1, 1), Metadata = "Admin created user: Ali" },
                        new AuditLog { Id = log3Id, UserId = adminId, Action = "INSERT", EntityName = "User", EntityId = user2Id.ToString(), Timestamp = new DateTime(2026, 1, 1), Metadata = "Admin created user: Sara" },

                        new AuditLog { Id = log4Id, UserId = adminId, Action = "INSERT", EntityName = "Course", EntityId = course1Id.ToString(), Timestamp = new DateTime(2026, 1, 1), Metadata = "Created Course: C# Basics" },
                        new AuditLog { Id = log5Id, UserId = adminId, Action = "INSERT", EntityName = "Course", EntityId = course2Id.ToString(), Timestamp = new DateTime(2026, 1, 1), Metadata = "Created Course: ASP.NET Core" },
                        new AuditLog { Id = log6Id, UserId = adminId, Action = "INSERT", EntityName = "Course", EntityId = course3Id.ToString(), Timestamp = new DateTime(2026, 1, 1), Metadata = "Created Course: Entity Framework Core" },

                        new AuditLog { Id = log7Id, UserId = user1Id, Action = "ENROLL", EntityName = "Enrollment", EntityId = enrollment1Id.ToString(), Timestamp = new DateTime(2026, 1, 1), Metadata = "Ali enrolled in C# Basics" },
                        new AuditLog { Id = log8Id, UserId = user1Id, Action = "ENROLL", EntityName = "Enrollment", EntityId = enrollment2Id.ToString(), Timestamp = new DateTime(2026, 1, 1), Metadata = "Ali enrolled in ASP.NET Core" },
                        new AuditLog { Id = log9Id, UserId = user2Id, Action = "ENROLL", EntityName = "Enrollment", EntityId = enrollment3Id.ToString(), Timestamp = new DateTime(2026, 1, 1), Metadata = "Sara enrolled in ASP.NET Core" },
                        new AuditLog { Id = log10Id, UserId = user2Id, Action = "ENROLL", EntityName = "Enrollment", EntityId = enrollment4Id.ToString(), Timestamp = new DateTime(2026, 1, 1), Metadata = "Sara enrolled in Entity Framework Core" }
                        );
                });
                #endregion


            });
        }
    }
}

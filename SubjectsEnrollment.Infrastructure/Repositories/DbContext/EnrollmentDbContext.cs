using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubjectsEnrollment.Domain.Models;

namespace SubjectsEnrollment.Infrastructure.Repositories.DbContext
{
    public class EnrollmentDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=..\\SubjectsEnrollment.Infrastructure\\DB\\Enrollment.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Enrollment>(entity => {
                entity.HasOne(s => s.Student).WithMany(e => e.Enrollments).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(s => s.Subject).WithMany(e => e.Enrollments).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

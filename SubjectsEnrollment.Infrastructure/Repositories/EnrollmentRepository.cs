
using Microsoft.EntityFrameworkCore;
using SubjectsEnrollment.Domain.Models;
using SubjectsEnrollment.Infrastructure.Repositories.DbContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsEnrollment.Infrastructure.Repositories
{
    public interface IEnrollmentRepository
    {
        Task<Subject> AddSubjectAsync(string subjectName);
        Task<Subject> GetSubjectByNameAsync(string subjectName);
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();

        Task<Student> AddStudentAsync(string studentName);
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<Student> GetStudentByNameAsync(string studentName);
        Task<IEnumerable<Student>> GetAllStudentsAsync();

        Task<Enrollment> AddEnrollmentAsync(Student student, Subject subject, string status);


    }

    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly EnrollmentDbContext _dbContext;

        public EnrollmentRepository(EnrollmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync() => await _dbContext.Subjects.ToListAsync();
        public async Task<Subject> GetSubjectByNameAsync(string subjectName) => await _dbContext.Subjects.Where(s=>s.SubjectName == subjectName).FirstOrDefaultAsync();
        public async Task<Subject> AddSubjectAsync(string subjectName)
        {
            var subject = new Subject
            {
                SubjectName = subjectName
            };

            await _dbContext.Subjects.AddAsync(subject);
            await _dbContext.SaveChangesAsync();

            return subject;
        }
        public async Task<Student> AddStudentAsync(string studentName)
        {
            var student = new Student
            {
                StudentName = studentName
            };

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            return student;
        }

        //TODO: Create a shared code for expression
        public async Task<IEnumerable<Student>> GetAllStudentsAsync() => await _dbContext.Students.Include(s => s.Enrollments).ToListAsync();
        public async Task<Student> GetStudentByIdAsync(int studentId) => await _dbContext.Students.Include(s=>s.Enrollments).Where(s => s.StudentId == studentId).FirstOrDefaultAsync();
        public async Task<Student> GetStudentByNameAsync(string studentName) => await _dbContext.Students.Where(s => s.StudentName == studentName).FirstOrDefaultAsync();


        public async Task<Enrollment> AddEnrollmentAsync(Student student, Subject subject, string status)
        {
            var erollment = new Enrollment { Student = student, Subject = subject, Status = status };

            await _dbContext.Enrollments.AddAsync(erollment);
            await _dbContext.SaveChangesAsync();

            return erollment;
        }
    }
}

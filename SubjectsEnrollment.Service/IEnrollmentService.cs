using System.Collections.Generic;
using System.Threading.Tasks;
using SubjectsEnrollment.Domain.Models;

namespace SubjectsEnrollment.Service
{
    public interface IEnrollmentService
    {
        Task<Subject> CreateSubjectAsync(string subjectName);
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();

        Task<Student> CreateStudentAsync(string studentName);
        Task<Student> GetStudentAsync(int studentId);
        Task<IEnumerable<Student>> GetAllStudentsAsync();


        Task<ServiceResult<Enrollment>> EnrollStudentAsync(int studentId, string subjectName);

        Task<Student> UpdateEnrollmentStatusAsync(int studentId, string status);



    }
}

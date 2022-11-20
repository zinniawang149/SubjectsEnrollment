using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubjectsEnrollment.Domain.Commons;
using SubjectsEnrollment.Domain.Models;
using SubjectsEnrollment.Infrastructure.Repositories;

namespace SubjectsEnrollment.Service
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        //TODO: Update the return object to ServiceResult<T> 
        public async Task<Subject> CreateSubjectAsync(string subjectName)
        {
            var subject = await _enrollmentRepository.GetSubjectByNameAsync(subjectName);
            if (subject != null) { return subject; }
            return await _enrollmentRepository.AddSubjectAsync(subjectName);
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _enrollmentRepository.GetAllSubjectsAsync();
        }

        public async Task<Student> CreateStudentAsync(string studentName)
        {
            // Same name student allowed 
            return await _enrollmentRepository.AddStudentAsync(studentName);
        }
        public async Task<Student> GetStudentAsync(int studentId)
        {
            return await _enrollmentRepository.GetStudentByIdAsync(studentId);
        }
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _enrollmentRepository.GetAllStudentsAsync();
        }
        public async Task<ServiceResult<Enrollment>> EnrollStudentAsync(int studentId, string subjectName)
        {
            var student = await _enrollmentRepository.GetStudentByIdAsync(studentId);
            var subject = await _enrollmentRepository.GetSubjectByNameAsync(subjectName);

            if (subject == null || student == null)
            {
                return new ServiceResult<Enrollment>
                {
                    Status = ServiceResultStatus.Failure,
                    Error = new Error
                    {
                        Code ="ERROR:001",
                        Message = "Student or Subject doesn't exist"
                    }
                };
            }
            var incompleteSubjectAmount = student.Enrollments?.Where(e => e.Status.Equals(Constants.Incompleted)).Count();
            if (incompleteSubjectAmount >= 5) //TODO: make this number configurable if necessary
            {
                return new ServiceResult<Enrollment>
                {
                    Status = ServiceResultStatus.Failure,
                    Error = new Error
                    {
                        Code = "ERROR:002",
                        Message = $"Too many incompleted enrollments. Incomplete Subject Amount: {incompleteSubjectAmount}"
                    }
                };
            }

            var enrollment = await _enrollmentRepository.AddEnrollmentAsync(student, subject, Constants.Incompleted);
            
            // TODO: The application layer should handle the trasaction part, ex. rollback. Since it's a demo project, I will skip the application layer.
            SendNotification(enrollment);
            return new ServiceResult<Enrollment>
            {
                Status = ServiceResultStatus.Success,
                Result = enrollment
            };

        }
        public Task<Student> UpdateEnrollmentStatusAsync(int studentId, string status)
        {
            throw new NotImplementedException();
        }

        private void SendNotification(Enrollment enrollment) { 
            //Do nothing
            //On cloud it should be an event trigger, we can put it in another queue and let another job to pick it up
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubjectsEnrollment.Domain.Models;
using SubjectsEnrollment.Requests;
using SubjectsEnrollment.Service;

namespace SubjectsEnrollment.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class EnrollmentController : ControllerBase
    {
        private readonly ILogger<EnrollmentController> _logger;
        private readonly IEnrollmentService _enrollmentService;
        //TODO: DTO/autoMapper required 
        public EnrollmentController(ILogger<EnrollmentController> logger, IEnrollmentService enrollmentService)
        {
            _logger = logger;
            _enrollmentService = enrollmentService;
        }

        [HttpPost("subjects", Name = "CreateSubject")]
        [ProducesResponseType(typeof(Subject), 201)]
        //To PUT if we need to update the subjects
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectRequest request)
        {
            var subject = await _enrollmentService.CreateSubjectAsync(request.SubjectName.ToLower());
            return Ok(subject);
        }
        //TODO: Pagination
        [HttpGet("subjects")]
        [ProducesResponseType(typeof(IEnumerable<Subject>), 200)]
        public async Task<IActionResult> GetSubjects() {
            var subjects = await _enrollmentService.GetAllSubjectsAsync();
            return Ok(subjects);
        }

        [HttpPost("students", Name = "CreateStudent")]
        [ProducesResponseType(typeof(Student), 201)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest request)
        {
            //TODO: Data cleanup
            var student = await _enrollmentService.CreateStudentAsync(request.StudentName.ToLower()); //TODO: To CamelCase
            return Ok(student);
        }


        [HttpGet("students/{studentId:int}")]
        [ProducesResponseType(typeof(Student), 200)]
        public async Task<IActionResult> GetStudent(int studentId)
        {
            var student = await _enrollmentService.GetStudentAsync(studentId);
            return Ok(student);
        }

        [HttpGet("students")]
        [ProducesResponseType(typeof(IEnumerable<Student>), 200)]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _enrollmentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpPost("students/{studentId:int}/enrollments", Name = "CreateEnrollment")]
        [ProducesResponseType(typeof(Enrollment), 201)]
        public async Task<IActionResult> CreateEnrollment(int studentId, [FromBody] CreateEnrollmentRequest request)
        {
            //TODO: Data cleanup
            var enrollmentResult = await _enrollmentService.EnrollStudentAsync(studentId, request.SubjectName.ToLower());
            if (!enrollmentResult.IsSuccess) {
                return BadRequest(enrollmentResult.Error);
            }
            var newEnrollment = new Enrollment
            {
                EnrollmentId = enrollmentResult.Result.EnrollmentId,
                Status = enrollmentResult.Result.Status
            };
            //Check result status
            return Ok(newEnrollment);
        }
    }
}

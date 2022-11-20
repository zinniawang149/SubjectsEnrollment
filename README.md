# SubjectsEnrollment

This is a simulator for the SubjectsEnrollment service.
In this simulator, you can add/read a subject, add/read a student and enroll the student to a subject.
Before you runs the project, please make sure you have [.Net core 3.1 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) installed.

## API Reference

#### Create a subject
```
  POST /api/v1/subjects
```
Body sample
```
{
    "subjectName":"english"
}
```
#### Get subjects
```
  GET /api/v1/subjects
```
#### Create a student
```
  POST /api/v1/students
```
Body sample
```
{
    "studentName":"zinnia"
}
```
#### Get a student
```
  GET /api/v1/students/${studentId}
```

#### Get students
```
  GET /api/v1/students
```
#### Enroll a student to a subject

```
  POST /api/v1/students/${studentId}/enrollments
```
Body sample
```
{
    "subjectName":"science"
}
```

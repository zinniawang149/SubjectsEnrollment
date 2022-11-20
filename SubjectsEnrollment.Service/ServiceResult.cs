using System.Net;

namespace SubjectsEnrollment.Service
{
    public enum ServiceResultStatus
    {
        Success = 0,
        Failure = 1
    }

    public class ServiceResult<T>
    {
        public ServiceResultStatus Status { get; set; }
        public bool IsSuccess => Status == ServiceResultStatus.Success && Error == null;
        public Error Error { get; set; }
        public T Result { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

}

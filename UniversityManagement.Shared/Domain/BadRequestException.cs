namespace UniversityManagement.Shared.Domain
{
    public abstract class BadRequestException(string message)
        : ApplicationException("Bad Request", message)
    {
    }
}

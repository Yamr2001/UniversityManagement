namespace UniversityManagement.Shared.Domain
{
    public abstract class NotFoundException(string Name, object Key)
        : ApplicationException("غير موجوده", "البيانات غير موجوده")
    {
    }
}

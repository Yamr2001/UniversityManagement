namespace UniversityManagement.Application.HangFireJobs
{
    public interface IJobService
    {
        string EnqueueEmailJob(string email, string name);
    }
}

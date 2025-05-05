using Hangfire;

namespace UniversityManagement.Application.HangFireJobs
{
    public class JobService : IJobService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public JobService(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }
        public string EnqueueEmailJob(string email, string name)
        {
            return _backgroundJobClient.Enqueue<EmailJob>(x => x.SendWelcomeEmail(email, name));
        }
    }
}

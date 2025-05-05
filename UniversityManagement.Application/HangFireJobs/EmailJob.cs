using Hangfire;

namespace UniversityManagement.Application.HangFireJobs
{
    public class EmailJob 
    {
        private readonly IEmailService _emailService;

        public EmailJob(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task SendWelcomeEmail(string email, string name)
        {
            await _emailService.SendWelcomeEmail(email, name);
        }
    }
}

namespace UniversityManagement.Application.HangFireJobs
{
    public class EmailService : IEmailService
    {
        public Task ScheduleBulkEmailAsync(IEnumerable<string> to, string subject, string body, DateTime scheduleTime)
        {
            throw new NotImplementedException();
        }

        public Task ScheduleEmailAsync(string to, string subject, string body, DateTime scheduleTime)
        {
            throw new NotImplementedException();
        }

        public Task SendBulkEmailAsync(IEnumerable<string> to, string subject, string body)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailAsync(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }

        public Task SendWelcomeEmail(string Email, string User)
        {
            Console.WriteLine($"this is the Email Content {Email} to This is {User}");
            return Task.CompletedTask;
        }
    }
}

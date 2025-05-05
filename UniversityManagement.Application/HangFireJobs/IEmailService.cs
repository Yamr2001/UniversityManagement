namespace UniversityManagement.Application.HangFireJobs
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(string Email, string User);
        Task SendEmailAsync(string to, string subject, string body);
        Task SendBulkEmailAsync(IEnumerable<string> to, string subject, string body);
        Task ScheduleEmailAsync(string to, string subject, string body, DateTime scheduleTime);
        Task ScheduleBulkEmailAsync(IEnumerable<string> to, string subject, string body, DateTime scheduleTime);
    }
}

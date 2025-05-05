//using Hangfire;

//namespace UniversityManagement.Infrastructure.HangFire
//{
//    public static class RecurringJobService
//    {
//        public static void AddRecurringJobs()
//        {
//            RecurringJob.AddOrUpdate<EmailJob>(
//                "database-cleanup",
//                x => x.CleanupOldData(),
//                Cron.Daily);
//        }
//    }
//}
//using Hangfire;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using Abp.Admin.HttpApi.Host.Jobs;

//namespace Abp.Admin.HttpApi.Host.Extensions
//{
//    public static class RecurringJobsExtensions
//    {
//        public static void CreateRecurringJob(this IServiceProvider service)
//        {
//            var job = service.GetService<TestJob>();
//            RecurringJob.AddOrUpdate("测试Job", () => job.ExecuteAsync(), CronTypeHelper.Minute(1));
//        }
//    }
//}

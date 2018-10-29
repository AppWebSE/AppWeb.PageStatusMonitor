using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MonitorPageStatus.Actions;
using MonitorPageStatus.Configurations;
using MonitorPageStatus.Interfaces;
using MonitorPageStatus.Models;
using MonitorPageStatus.Services;

namespace MonitorPageStatus.ExampleConsoleApp
{
    public class Program
    {
        public IMonitorService MonitorService;
        public IEmailService EmailService;

        public Program()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();

            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            EmailService = new EmailService(appSettings.EmailConfiguration);

            // Actions to be run after each check result
            Action<MonitorResultItem> onCheckCompleteAction = (monitorResultItem) =>
            {
                // Console write status
                ConsoleActions.WriteCheckStatus(monitorResultItem);
                // Email if check fails
                EmailActions.SendEmailOnFail(monitorResultItem, EmailService);
            };

            var monitorConfiguration = new MonitorConfiguration(monitorItems: appSettings.MonitorItems, 
                                                                onCheckCompleteAction: onCheckCompleteAction, 
                                                                maxDegreeOfParallelism: appSettings.MaxDegreeOfParallelism);
            MonitorService = new MonitorService(monitorConfiguration);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            
            Program program = new Program();
            var runResult = program.MonitorService
                                    .RunChecks(); // Runs the check
                                    // Optional extentions:
                                    //.FilterOnlySuccessful() // filter so we only get successful checks
                                    //.FilterOnlyFailed() // filter so we only get failed checks
                                    //.FilterLongExecutionTime(500) // filter so we just get checks with long excution time 
                                    //.Then(ConsoleActions.WriteSuccessfulSummary) // console write summary of successful checks
                                    //.Then(ConsoleActions.WriteFailedSummary) // console write summary of failed checks

            Console.WriteLine();
            Console.WriteLine("Done, press any key to close");

            Console.ReadKey();
        }
    }
}
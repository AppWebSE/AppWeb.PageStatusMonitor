using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using AppWeb.PageStatusMonitor.Actions;
using AppWeb.PageStatusMonitor.Configurations;
using AppWeb.PageStatusMonitor.Interfaces;
using AppWeb.PageStatusMonitor.Models;
using AppWeb.PageStatusMonitor.Services;

namespace AppWeb.PageStatusMonitor.ExampleConsoleApp
{
    public class Program
    {
		public IMonitorService MonitorService;
        public IEmailService EmailService;
        public MonitorConfiguration MonitorConfiguration;

        public Program()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();

            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            // Actions to be run after each check result
            Action<MonitorResultItem> onCheckCompleteAction = (monitorResultItem) =>
            {
                // Console write status
                ConsoleActions.WriteCheckStatus(monitorResultItem);
                // Email if check fails
                EmailActions.SendEmailOnFail(monitorResultItem, EmailService);
            };

            MonitorService = new MonitorService();
            EmailService = new EmailService(appSettings.EmailConfiguration);
			MonitorConfiguration = new MonitorConfiguration(appSettings.MonitorItems, onCheckCompleteAction);
		}

        static void Main(string[] args)
        {
            Console.WriteLine("Started");

            Program program = new Program();
            
            var runResult = program.MonitorService
                                    .RunChecks(program.MonitorConfiguration); // Runs the check
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
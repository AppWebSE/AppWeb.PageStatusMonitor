# AppWeb.PageStatusMonitor
.Net Standard package and application for monitoring the status of your home page. 
Sends warnings and status updates through email/sms/telegram or your prefferred way when something is wrong.

Project url: https://appweb.se/en/packages/page-status-monitor

## Installation
The package can be installed through nuget https://www.nuget.org/packages/AppWeb.PageStatusMonitor/
```nuget
Install-Package AppWeb.PageStatusMonitor
```

## Example configuration
```json
{
  "AppSettings": {
    "MaxDegreeOfParallelism": 3,
    "MonitorItems": [
      {
        "CheckUri": "https://www.appweb.se",
        "CheckType": "HttpGet"
      },
      {
        "CheckIPAddress": "127.0.0.1",
        "CheckType": "Ping"
      },
      {
        "CheckUri": "https://www.shouldNotExist1337orWhat.se",
        "CheckType": "HttpGet"
      }
    ],
    "EmailConfiguration": {
      "FromEmail": "<your-email>",
      "FromName": "<your-name>",
      "ToEmail": "<to-email>",
      "ToName": "<to-name>",
      "SmtpHost": "<smtp-host>",
      "SmtpUsername": "<smtp-username>",
      "SmtpPassword": "<smtp-password>",
      "UseSSL": true
    }
  }
}
```
 
## Example usage code
The following is from the ExampleConsoleApp provided in the solution
```csharp
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MonitorPageStatus.Actions;
using MonitorPageStatus.Configurations;
using MonitorPageStatus.Enums;
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

            var appConfiguration = configuration.GetSection("AppSettings").Get<AppSettings>();

            EmailService = new EmailService(appConfiguration.EmailConfiguration);

            // Actions to be run after each check result
            Action<MonitorResultItem> onCheckCompleteAction = (monitorResultItem) =>
            {
                // Console write status
                ConsoleActions.WriteCheckStatus(monitorResultItem);
                // Email if check fails
                EmailActions.SendEmailOnFail(monitorResultItem, EmailService);
            };

            var monitorConfiguration = new MonitorConfiguration(monitorItems: appConfiguration.MonitorItems, 
                                                                onCheckCompleteAction: onCheckCompleteAction, 
                                                                maxDegreeOfParallelism: 3);
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
```
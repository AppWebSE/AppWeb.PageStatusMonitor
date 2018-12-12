# AppWeb.PageStatusMonitor
.Net Standard package and application for monitoring the status of your home page. 
Sends warnings and status updates through email/sms/telegram or your prefferred way when something is wrong.

Project url: https://appweb.se/en/packages/page-status-monitor

## Installation
The package can be installed through nuget https://www.nuget.org/packages/AppWeb.PageStatusMonitor/
```nuget
Install-Package AppWeb.PageStatusMonitor
```

## Most simple example usage of monitor service
```csharp
using AppWeb.PageStatusMonitor.Interfaces;
using AppWeb.PageStatusMonitor.Models;
using AppWeb.PageStatusMonitor.Services;
...

// Create monitor service
IMonitorService monitorService = new MonitorService();

// Run single check
var checkResult = monitorService.Check(new MonitorItem(new Uri("https://appweb.se")));

// Add your action on the result
if(checkResult.Successful)
{
	var responseTime = checkResult.Milliseconds;
	...
}
else{
	...
}

...
```
The code above is the most simple way of using this package and running simple checks.

## Different supported check types
Monitor items can be configured to chech either a URI och a IPAdress and checking if either a Get request or a Ping is successful. More advanced use cases will be supported in the future.

```csharp
    using AppWeb.PageStatusMonitor.Enums;
    using AppWeb.PageStatusMonitor.Models;
    ...

    // URI, when no CheckType is provided it will perform HttpGet by default
    var item1 = new MonitorItem(new Uri("https://appweb.se"));
    // URI to be checked with ping 
    var item2 = new MonitorItem(new Uri("https://appweb.se"), CheckType.Ping);


    // IPAddress to explicity be checked with Get-request
    var item3 = new MonitorItem(IPAddress.Parse("127.0.0.1"), CheckType.HttpGet);
    // IPAdddress to be checked with ping 
    var item4 = new MonitorItem(IPAddress.Parse("127.0.0.1"), CheckType.Ping);

    ...
```

## Example configuration for more advanced usage
```json
{
	"AppSettings": {
		"EmailConfiguration": {
			"FromEmail": "<your-email>",
			"FromName": "<your-name>",
			"ToEmail": "<to-email>",
			"ToName": "<to-name>",
			"SmtpHost": "<smtp-host>",
			"SmtpUsername": "<smtp-username>",
			"SmtpPassword": "<smtp-password>",
			"UseSSL": true
		},
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
				"CheckType": "HttpGet",
				"CheckUri": "https://www.shouldNotExist1337orWhat.se"
			}
		]
	}
}
```
 
## Advanced example usage code
The following is from the ExampleConsoleApp provided in the solution
```csharp
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

            MonitorService = new MonitorService(new HttpService());
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
```
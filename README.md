# Monitor-Page-Status
C# console application for monitoring the status of your home page. Sends warnings and status updates through email/sms/telegram or your prefferred way when something is wrong.

## Example usage code
The following is from the ExampleConsoleApp provided in the solution
```cs
using System;
using System.Linq;
using System.Net;
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
        public MonitorConfiguration MonitorConfiguration;

        public Program()
        {
            MonitorConfiguration = new MonitorConfiguration();
            MonitorConfiguration.MonitorItems.Add(new MonitorItem(new Uri("https://www.amattias.se"), CheckType.HttpGet));
            MonitorConfiguration.MonitorItems.Add(new MonitorItem(IPAddress.Parse("184.168.221.51"), CheckType.HttpGet));
            MonitorConfiguration.MonitorItems.Add(new MonitorItem(new Uri("https://tinkr.cloud"), CheckType.Ping));
            MonitorConfiguration.MonitorItems.Add(new MonitorItem(IPAddress.Parse("184.168.221.51"), CheckType.Ping));
            MonitorConfiguration.MonitorItems.Add(new MonitorItem(new Uri("https://www.shouldNotExist1337orWhat.se"), CheckType.HttpGet));
            MonitorConfiguration.SendEmailWhenDown = false;
            
            MonitorService = new MonitorService();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            
            Program program = new Program();
            var runResult = program.MonitorService
                                    .RunChecks(program.MonitorConfiguration) // Runs the check
                                    //.OnlySuccessful() // filter so we only get successful checks
                                    //.OnlyFailed() // filter so we only get failed checks
                                    //.LongExecutionTime(500) // filter so we just get checks with long excution time 
                                    .Then(ConsoleActions.WriteSuccessful) // console log successful checks
                                    .Then(ConsoleActions.WriteFailed); //console log failed checks

            Console.WriteLine();
            Console.WriteLine("Done, press any key to close");

            Console.ReadKey();
        }
    }
}
```
using System;
using System.Collections.Generic;
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

        public Program()
        {
            List<MonitorItem> monitorItems = new List<MonitorItem>() {
                new MonitorItem(new Uri("https://www.amattias.se"), CheckType.HttpGet),
                new MonitorItem(IPAddress.Parse("184.168.221.51"), CheckType.HttpGet),
                new MonitorItem(new Uri("https://tinkr.cloud"), CheckType.Ping),
                new MonitorItem(IPAddress.Parse("184.168.221.51"), CheckType.Ping),
                new MonitorItem(new Uri("https://www.shouldNotExist1337orWhat.se"), CheckType.HttpGet)
            };

            var monitorConfiguration = new MonitorConfiguration(monitorItems: monitorItems, 
                                                                onCheckCompleteAction: ConsoleActions.WriteCheckStatus, 
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
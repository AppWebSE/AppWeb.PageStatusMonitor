using MonitorPageStatus.Configurations;
using MonitorPageStatus.Interfaces;
using MonitorPageStatus.Models;
using MonitorPageStatus.Services;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MonitorPageStatus.ExampleConsoleApp
{
   

    public class Program
    {
        public IMonitorService MonitorService;
        public Program()
        {
            MonitorConfiguration monitorConfiguration = new MonitorConfiguration();
            monitorConfiguration.MonitorUris.Add(new MonitorUri(new Uri("https://www.amattias.se")));
            monitorConfiguration.MonitorUris.Add(new MonitorUri(new Uri("https://www.shouldNotExist1337orWhat.se")));
            monitorConfiguration.SendEmailWhenDown = false;

            MonitorService = new MonitorService(monitorConfiguration);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Started");

            Program program = new Program();
            List<MonitorResult> monitorResults = program.MonitorService.Monitor();
            foreach(var monitorResult in monitorResults)
            {
                Console.WriteLine($"{monitorResult.Uri} - {monitorResult.Success} ({monitorResult.Milliseconds}ms)");
            }
            
            Console.WriteLine("Done");
            Thread.Sleep(5000);
        }
    }
}

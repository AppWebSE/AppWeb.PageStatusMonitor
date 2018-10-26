using System;
using System.Collections.Generic;
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
            MonitorConfiguration monitorConfiguration = new MonitorConfiguration();
            monitorConfiguration.MonitorUris.Add(new MonitorUri(new Uri("https://www.amattias.se"), MonitorTypeEnum.HttpGet));
            monitorConfiguration.MonitorUris.Add(new MonitorUri(new Uri("https://tinkr.cloud"), MonitorTypeEnum.Ping));
            monitorConfiguration.MonitorUris.Add(new MonitorUri(new Uri("https://www.shouldNotExist1337orWhat.se"), MonitorTypeEnum.HttpGet));
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
            
            Console.WriteLine("Done, press any key to close");

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
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
            monitorConfiguration.MonitorItems.Add(new MonitorItem(new Uri("https://www.amattias.se"), MonitorTypeEnum.HttpGet));
            monitorConfiguration.MonitorItems.Add(new MonitorItem(IPAddress.Parse("184.168.221.51"), MonitorTypeEnum.HttpGet));
            monitorConfiguration.MonitorItems.Add(new MonitorItem(new Uri("https://tinkr.cloud"), MonitorTypeEnum.Ping));
            monitorConfiguration.MonitorItems.Add(new MonitorItem(IPAddress.Parse("184.168.221.51"), MonitorTypeEnum.Ping));
            monitorConfiguration.MonitorItems.Add(new MonitorItem(new Uri("https://www.shouldNotExist1337orWhat.se"), MonitorTypeEnum.HttpGet));
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
                Console.WriteLine($"{monitorResult.MonitorItem.ToString()} - {monitorResult.Success} ({monitorResult.Milliseconds}ms)");
            }
            
            Console.WriteLine("Done, press any key to close");

            Console.ReadKey();
        }
    }
}

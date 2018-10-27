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
                                    .RunChecks(program.MonitorConfiguration)
                                    .Then(ConsoleActions.WriteSuccessful)
                                    .Then(ConsoleActions.WriteFailed);

            Console.WriteLine();
            Console.WriteLine("Done, press any key to close");

            Console.ReadKey();
        }
    }
}
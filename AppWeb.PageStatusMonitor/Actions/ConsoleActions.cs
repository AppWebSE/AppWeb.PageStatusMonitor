using System;
using System.Linq;
using AppWeb.PageStatusMonitor.Models;

namespace AppWeb.PageStatusMonitor.Actions
{
    public static class ConsoleActions
    {
        public static Action<MonitorResultItem> WriteCheckStatus = (monitorResultItem) =>
        {
            if (monitorResultItem.Successful)
            {
                Console.WriteLine($"Successful check: {monitorResultItem.MonitorItem.ToString()} - {monitorResultItem.Successful} ({monitorResultItem.Milliseconds}ms)");
            }
            else
            {
                Console.WriteLine($"Failed check: {monitorResultItem.MonitorItem.ToString()} - {monitorResultItem.Successful} ({monitorResultItem.Milliseconds}ms)");
            }
        };

        public static Action<MonitorResult> WriteSuccessfulSummary = (monitorResult) => {
            Console.WriteLine();
            Console.WriteLine("Summary successful checks:");
            foreach (var result in monitorResult.Results.Where(x => x.Successful))
            {
                Console.WriteLine($"{result.MonitorItem.ToString()} - {result.Successful} ({result.Milliseconds}ms)");
            }
        };

        public static Action<MonitorResult> WriteFailedSummary = (monitorResult) => {
            Console.WriteLine();
            Console.WriteLine("Summary not successful checks:");
            foreach (var result in monitorResult.Results.Where(x => !x.Successful))
            {
                Console.WriteLine($"{result.MonitorItem.ToString()} - {result.Successful} ({result.Milliseconds}ms)");
            }
        };
    }
}

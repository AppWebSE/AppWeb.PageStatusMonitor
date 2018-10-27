using System;
using System.Linq;
using MonitorPageStatus.Models;

namespace MonitorPageStatus.Actions
{
    public static class ConsoleActions
    {
        public static Action<MonitorResult> WriteSuccessful = (monitorResult) => {
            Console.WriteLine();
            Console.WriteLine("Successful checks:");
            foreach (var result in monitorResult.Results.Where(x => x.Successful))
            {
                Console.WriteLine($"{result.MonitorItem.ToString()} - ({result.Milliseconds}ms)");
            }
        };

        public static Action<MonitorResult> WriteFailed = (monitorResult) => {
            Console.WriteLine();
            Console.WriteLine("Not successful checks:");
            foreach (var result in monitorResult.Results.Where(x => !x.Successful))
            {
                Console.WriteLine($"{result.MonitorItem.ToString()} - ({result.Milliseconds}ms)");
            }
        };
    }
}

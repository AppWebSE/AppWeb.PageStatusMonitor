using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppWeb.PageStatusMonitor.Models
{
    public class MonitorResult
    {
        public List<MonitorResultItem> Results { get; set; }

        public MonitorResult()
        {
            Results = new List<MonitorResultItem>();
        }

        public MonitorResult Then(Action<MonitorResult> action)
        {
            if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			// Run action
			action(this);
            
            return this;
        }
        
        public MonitorResult FilterOnlySuccessful()
        {
            Results = Results.Where(x => x.Successful).ToList();

            return this;
        }

        public MonitorResult FilterOnlyFailed()
        {
            Results = Results.Where(x => !x.Successful).ToList();

            return this;
        }

        public MonitorResult FilterLongExecutionTime(int largerThan)
        {
            Results = Results.Where(x => x.Milliseconds > largerThan).ToList();

            return this;
        }
    }
}

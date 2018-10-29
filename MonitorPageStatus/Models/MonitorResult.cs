using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorPageStatus.Models
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
                throw new ArgumentNullException(nameof(action));

            // Run action
            action(this);
            
            return this;
        }
        
        public MonitorResult FilterOnlySuccessful()
        {
            this.Results = this.Results.Where(x => x.Successful).ToList();

            return this;
        }

        public MonitorResult FilterOnlyFailed()
        {
            this.Results = this.Results.Where(x => !x.Successful).ToList();

            return this;
        }

        public MonitorResult FilterLongExecutionTime(int largerThan)
        {
            this.Results = this.Results.Where(x => x.Milliseconds > largerThan).ToList();

            return this;
        }
    }
}

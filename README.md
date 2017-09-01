# Monitor-Page-Status
C# console application for monitoring the status of your home page. Sends warnings and status updates through email/sms/telegram or your prefferred way when something is wrong.

**Example usage code**
```cs
public void YourFunction()
{
    HttpConfiguration httpConfiguration = new HttpConfiguration();
    EmailConfiguration emailConfiguration = new EmailConfiguration();
    MonitorConfiguration monitorConfiguration = new MonitorConfiguration();

    // todo: set your configurations

    MonitorService monitorService = 
	new MonitorService(monitorConfiguration, emailConfiguration, httpConfiguration)

    MonitorResult result = monitorService.Monitor();
   
    // todo: check your result, take action if you want/need to

    monitorService.Dispose();
}
```
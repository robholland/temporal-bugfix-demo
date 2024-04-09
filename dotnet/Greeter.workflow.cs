namespace Demo;

using Microsoft.Extensions.Logging;
using Temporalio.Workflows;

[Workflow]
public class Greeter
{
    [WorkflowRun]
    public async Task<string> RunAsync(string name)
    {
        var greeting = await Workflow.ExecuteActivityAsync(
            () => DemoActivities.PickGreeting(name),
            new()
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(1),
                RetryPolicy = new()
                {
                    MaximumInterval = TimeSpan.FromSeconds(5),
                }
            });

        var sms = Workflow.ExecuteActivityAsync(
            () => DemoActivities.SendSMS(greeting, name),
            new()
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(1),
                RetryPolicy = new()
                {
                    MaximumInterval = TimeSpan.FromSeconds(5),
                }
            });

        var email = Workflow.ExecuteActivityAsync(
            () => DemoActivities.SendEmail(greeting, name),
            new()
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(1),
                RetryPolicy = new()
                {
                    MaximumInterval = TimeSpan.FromSeconds(5),
                }
            });

        await Task.WhenAll(sms, email);

        return "";
    }
}
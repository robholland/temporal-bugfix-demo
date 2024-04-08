namespace Demo;

using Microsoft.Extensions.Logging;
using Temporalio.Workflows;

[Workflow]
public class Greeter
{
    [WorkflowRun]
    public static async Task<string> RunAsync(string name)
    {
        var greeting = await Workflow.ExecuteActivityAsync(
            () => DemoActivities.PickGreeting(name),
            new()
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(1),
            });

        await Workflow.ExecuteActivityAsync(
            () => DemoActivities.SendSMS(greeting, name),
            new()
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(1),
            });

        var msgId = await Workflow.ExecuteActivityAsync(
            () => DemoActivities.SendEmail(greeting, name),
            new()
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(1),
            });

        return msgId;
    }
}
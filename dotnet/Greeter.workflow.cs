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
            (MyActivities act) => act.PickGreeting(name),
            new()
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(1),
            });

        await Workflow.ExecuteActivityAsync(
            (MyActivities act) => act.SendSMS(greeting, name),
            new()
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(1),
            });

        var msgId = await Workflow.ExecuteActivityAsync(
            (MyActivities act) => act.SendEmail(greeting, name),
            new()
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(1),
            });

        return msgId;
    }
}
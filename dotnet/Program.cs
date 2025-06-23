using Microsoft.Extensions.Logging;
using Temporalio.Client;
using Temporalio.Worker;
using Demo;

var client = await TemporalClient.ConnectAsync(new("localhost:7233")
{
    LoggerFactory = LoggerFactory.Create(builder =>
        builder.
            AddSimpleConsole(options => options.TimestampFormat = "[HH:mm:ss] ").
            SetMinimumLevel(LogLevel.Information)),
});

async Task RunWorkerAsync()
{
    using var tokenSource = new CancellationTokenSource();
    Console.CancelKeyPress += (_, eventArgs) =>
    {
        tokenSource.Cancel();
        eventArgs.Cancel = true;
    };

    Console.WriteLine("Running worker");
    using var worker = new TemporalWorker(
        client,
        new TemporalWorkerOptions(taskQueue: "bugfix-demo").
            AddActivity(DemoActivities.PickGreeting).
            AddActivity(DemoActivities.SendSMS).
            AddActivity(DemoActivities.SendEmail).
            AddWorkflow<Greeter>());
    try
    {
        await worker.ExecuteAsync(tokenSource.Token);
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Worker cancelled");
    }
}

async Task ExecuteWorkflowAsync()
{
    await client.ExecuteWorkflowAsync(
        (Greeter wf) => wf.RunAsync("Alice"),
        new(id: "bugfix-demo-alice", taskQueue: "bugfix-demo"));

    await client.ExecuteWorkflowAsync(
        (Greeter wf) => wf.RunAsync("Bob"),
        new(id: "bugfix-demo-bob", taskQueue: "bugfix-demo"));
}

switch (args.ElementAtOrDefault(0))
{
    case "worker":
        await RunWorkerAsync();
        break;
    case "start-workflows":
        await ExecuteWorkflowAsync();
        break;
    default:
        throw new ArgumentException("Must pass 'worker' or 'start-workflows' as the single argument");
}

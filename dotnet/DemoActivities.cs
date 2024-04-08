namespace Demo;

using Temporalio.Activities;

public class DemoActivities
{
    [Activity]
    public static string PickGreeting(string name) =>
        Console.WriteLine($"--> pickGreeting(name: {name})");

        switch (name) {
        case "Alice":
            return "Awesome";
        case "Bob":
            return "Brilliant";
        default:
            return "Infamous";
        }

    [Activity]
    public static Task<void> SendSMS(string greeting, string name) =>
        Console.WriteLine($"--> SendSMS(greeting: {greeting}, name: {name})");
        
        var hasBug = true;
        
        if (hasBug && name == "Bob") {
            throw new InvalidOperationException("oops, can't send to Bob right now");
        }

        Console.WriteLine($"*** SMS: Hey {greeting} {name}!");

    [Activity]
    public static Task<string> SendEmail(string greeting, string name) =>
        Console.WriteLine($"--> SendEmail(greeting: {greeting}, name: {name})");
        Console.WriteLine($"*** Email: Hey {greeting} {name}!");
        var msgId = "123456789-123456789"

        return msgId

}
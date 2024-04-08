namespace Demo;

using Temporalio.Activities;

public static class DemoActivities
{
    [Activity]
    public static string PickGreeting(string name) {
        Console.WriteLine($"--> pickGreeting(name: {name})");

        return name switch 
        {
            "Alice" => "Awesome",
            "Bob" => "Brilliant",
            _ => "Infamous",
        };
    }

    [Activity]
    public static void SendSMS(string greeting, string name) {
        Console.WriteLine($"--> SendSMS(greeting: {greeting}, name: {name})");
        
        var hasBug = false;
        
        if (hasBug && name == "Bob") {
            throw new InvalidOperationException("oops, can't send to Bob right now");
        }

        Console.WriteLine($"*** SMS: Hey {greeting} {name}!");
    }

    [Activity]
    public static string SendEmail(string greeting, string name) {
        Console.WriteLine($"--> SendEmail(greeting: {greeting}, name: {name})");
        Console.WriteLine($"*** Email: Hey {greeting} {name}!");
        
        var msgId = "123456789-123456789";

        return msgId;
    }
}
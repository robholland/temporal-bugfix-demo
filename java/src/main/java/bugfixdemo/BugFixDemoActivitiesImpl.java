package bugfixdemo;

public class BugFixDemoActivitiesImpl implements BugFixDemoActivities {
    @Override
    public String pickGreeting(String name) {
        System.out.printf("--> pickGreeting(name: %s)\n", name);
        
        switch(name) {
        case "Alice":
            return "Awesome";
        case "Bob":
            return "Brilliant";
        default:
            return "Infamous";
        }    
    }

    @Override
    public void sendSMS(String greeting, String name) {
        System.out.printf("--> sendSMS(greeting: %s, name: %s)\n", greeting, name);

        boolean has_bug = true;

        if (has_bug && name.equals("Bob")) {
            throw new UnsupportedOperationException("oops, can't send to Bob right now");
        }

        System.out.printf("*** SMS: Hey %s %s!\n", greeting, name);
    }

    @Override
    public void sendEmail(String greeting, String name) {
        System.out.printf("--> sendEmail(greeting: %s, name: %s)\n", greeting, name);
        System.out.printf("*** Email: Hey %s %s!\n", greeting, name);
    }
}

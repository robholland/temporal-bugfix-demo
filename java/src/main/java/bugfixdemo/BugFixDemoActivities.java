package bugfixdemo;

import io.temporal.activity.ActivityInterface;
import io.temporal.activity.ActivityMethod;

@ActivityInterface
public interface BugFixDemoActivities {
    @ActivityMethod()
    String pickGreeting(String name);

    @ActivityMethod()
    void sendSMS(String greeting, String name);

    @ActivityMethod()
    void sendEmail(String greeting, String name);
}
package bugfixdemo;

import io.temporal.activity.ActivityOptions;
import io.temporal.common.RetryOptions;
import io.temporal.workflow.Workflow;
import io.temporal.workflow.Async;
import io.temporal.workflow.Promise;

import java.time.Duration;

public class GreeterImpl implements GreeterWorkflow {
    private final BugFixDemoActivities activities =
            Workflow.newActivityStub(
                    BugFixDemoActivities.class,
                    ActivityOptions.newBuilder()
                            .setStartToCloseTimeout(Duration.ofSeconds(1))
                            .setRetryOptions(RetryOptions.newBuilder()
                                    .setInitialInterval(Duration.ofSeconds(1))
                                    .setBackoffCoefficient(1.0)
                                    .setMaximumInterval(Duration.ofSeconds(5))
                                    .build())
                            .build());

    @Override
    public String greeter(String name) {
        String greeting = activities.pickGreeting(name);
        
        Promise<Void> email = Async.function(activities::sendEmail, greeting, name);
        Promise<Void> sms = Async.function(activities::sendSMS, greeting, name);

        email.get();
        sms.get();

        return "";
    }
}

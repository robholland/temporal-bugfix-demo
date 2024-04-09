package bugfixdemo;

import io.temporal.api.common.v1.WorkflowExecution;
import io.temporal.client.WorkflowClient;
import io.temporal.client.WorkflowOptions;
import io.temporal.serviceclient.WorkflowServiceStubs;
import io.temporal.worker.Worker;
import io.temporal.worker.WorkerFactory;

public class Starter {
    public static void main(String[] args) {
        WorkflowServiceStubs service = WorkflowServiceStubs.newLocalServiceStubs();
        WorkflowClient client = WorkflowClient.newInstance(service);

        WorkflowOptions workflowOptions =
                WorkflowOptions.newBuilder().setWorkflowId("bugfix-demo-alice").setTaskQueue("bugfix-demo").build();
        GreeterWorkflow workflow = client.newWorkflowStub(GreeterWorkflow.class, workflowOptions);

        WorkflowClient.start(workflow::greeter, "Alice");

        workflowOptions =
                WorkflowOptions.newBuilder().setWorkflowId("bugfix-demo-bob").setTaskQueue("bugfix-demo").build();
        workflow = client.newWorkflowStub(GreeterWorkflow.class, workflowOptions);

        WorkflowClient.start(workflow::greeter, "Bob");

        System.exit(0);
    }
}
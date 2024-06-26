package bugfixdemo;

import io.temporal.client.WorkflowClient;
import io.temporal.serviceclient.WorkflowServiceStubs;
import io.temporal.worker.WorkerFactory;

import io.temporal.client.WorkflowClient;
import io.temporal.serviceclient.WorkflowServiceStubs;
import io.temporal.worker.WorkerFactory;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Worker {
    public static final WorkflowServiceStubs service = WorkflowServiceStubs.newLocalServiceStubs();
    public static final WorkflowClient client = WorkflowClient.newInstance(service);
    public static final WorkerFactory factory = WorkerFactory.newInstance(client);

    private static final Logger logger = LoggerFactory.getLogger(Worker.class);

    public static void main(String[] args) {
        io.temporal.worker.Worker worker = factory.newWorker("bugfix-demo");
        worker.registerWorkflowImplementationTypes(GreeterImpl.class);
        worker.registerActivitiesImplementations(new BugFixDemoActivitiesImpl());

        logger.debug("Starting worker");
        factory.start();
    }
}

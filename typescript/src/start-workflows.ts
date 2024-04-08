import { Connection, Client } from '@temporalio/client';
import { greeter } from './workflows';

async function run() {
  const connection = await Connection.connect();

  const client = new Client({
    connection,
  });

  let handle = await client.workflow.start(greeter, {
    taskQueue: 'bugfix-demo',
    args: ["Alice"],
    workflowId: 'bugfix-demo-alice',
  });
  console.log(`Started workflow ${handle.workflowId}.`);

  handle = await client.workflow.start(greeter, {
    taskQueue: 'bugfix-demo',
    args: ["Bob"],
    workflowId: 'bugfix-demo-bob',
  });
  console.log(`Started workflow ${handle.workflowId}.`);
}

run().catch((err) => {
  console.error(err);
  process.exit(1);
});

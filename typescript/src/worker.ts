import { DefaultLogger, NativeConnection, Worker, Runtime } from '@temporalio/worker';
import * as activities from './activities';

async function run() {
  Runtime.install({});

  const connection = await NativeConnection.connect({});

  const worker = await Worker.create({
    connection,
    taskQueue: 'bugfix-demo',
    workflowsPath: require.resolve('./workflows'),
    stickyQueueScheduleToStartTimeout: '1s',
    activities,
  });

  await worker.run();
}

run().catch((err) => {
  console.error(err);
  process.exit(1);
});

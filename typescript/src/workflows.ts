import * as wf from '@temporalio/workflow';
import type * as activities from './activities';

const { pickGreeting, sendSMS, sendEmail } = wf.proxyActivities<typeof activities>({
  startToCloseTimeout: '1s',
  retry: {
      initialInterval: '1s',
      maximumInterval: '5s',
  },
});

export async function greeter(name: string): Promise<string> {
  wf.log.info(`Workflow started`)

  const greeting = await pickGreeting(name);

  await Promise.all([
    sendSMS(greeting, name),
    sendEmail(greeting, name),
  ])

  wf.log.info(`Workflow completed`)

  return "Done!"
}

from datetime import timedelta
import asyncio

from temporalio import workflow
from activities import *
from temporalio.common import RetryPolicy

@workflow.defn
class Greeter:
    @workflow.run
    async def run(self, name: str) -> None:
        workflow.logger.info("Workflow started")

        greeting = await workflow.execute_activity(
            pickGreeting,
            PickGreetingInput(name),
            start_to_close_timeout=timedelta(seconds=1),
            retry_policy=RetryPolicy(maximum_interval=timedelta(seconds=5)),
        )

        sms = workflow.execute_activity(
            sendSMS,
            SendSMSInput(greeting, name),
            start_to_close_timeout=timedelta(seconds=1),
            retry_policy=RetryPolicy(maximum_interval=timedelta(seconds=5)),
        )

        email = workflow.execute_activity(
            sendEmail,
            SendEmailInput(greeting, name),
            start_to_close_timeout=timedelta(seconds=1),
            retry_policy=RetryPolicy(maximum_interval=timedelta(seconds=5)),
        )

        await asyncio.gather(sms, email)

        workflow.logger.info("Workflow completed")

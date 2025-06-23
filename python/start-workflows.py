#!/usr/bin/env python3

import asyncio
import logging
from temporalio.client import Client

from workflow import *

async def main() -> None:
    logging.basicConfig(level=logging.INFO)

    client = await Client.connect("localhost:7233")

    await client.start_workflow(
        Greeter.run,
        "Alice",
        id="bugfix-demo-alice",
        task_queue="bugfix-demo",
    )

    await client.start_workflow(
        Greeter.run,
        "Bob",
        id="bugfix-demo-bob",
        task_queue="bugfix-demo",
    )

if __name__ == "__main__":
    asyncio.run(main())

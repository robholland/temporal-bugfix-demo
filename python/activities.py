from temporalio import activity
from dataclasses import dataclass

@dataclass
class PickGreetingInput:
    name: str

@dataclass
class SendSMSInput:
    greeting: str
    name: str

@dataclass
class SendEmailInput:
    greeting: str
    name: str

@activity.defn
async def pickGreeting(input: PickGreetingInput) -> str:
    print("--> pickGreeting(name: %s)" % input.name)

    match input.name:
        case "Alice":
            return "Awesome"
        case "Bob":
            return "Brilliant"
        case _:
            return "Infamous"

@activity.defn
async def sendSMS(input: SendSMSInput):
    print("--> sendSMS(greeting: %s, name: %s)" % (input.greeting, input.name))

    has_bug = True
    if has_bug and input.name == "Bob":
        raise Exception("oops, can't send to Bob right now")

    print("*** SMS: Hey %s %s!" % (input.greeting, input.name))


@activity.defn
async def sendEmail(input: SendEmailInput) -> str:
    print("--> sendEmail(greeting: %s, name: %s)" % (input.greeting, input.name))
    print("*** Email: Hey %s %s!" % (input.greeting, input.name))

    msg_id = "123456789-123456789"

    return msg_id
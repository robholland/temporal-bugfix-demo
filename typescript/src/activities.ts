export async function pickGreeting(name: string): Promise<string> {
  console.log(`--> pickGreeting(name: ${name})`)

  switch(name) {
    case "Alice":
      return "Awesome"
    case "Bob":
      return "Brilliant"
    default:
      return "Infamous"
  }
}

export async function sendSMS(greeting: string, name: string): Promise<void> {
  console.log(`--> sendSMS(greeting: ${greeting}, name: ${name})`)

  const hasBug = true

  if (hasBug && name == "Bob") {
    throw new Error("oops, can't send to Bob right now")
  }

  console.log(`*** SMS: Hey ${greeting} ${name}!`)
}

export async function sendEmail(greeting: string, name: string): Promise<string> {
  console.log(`--> sendEmail(greeting: ${greeting}, name: ${name})`)
  console.log(`*** Email: Hey ${greeting} ${name}!`)

  const msgId = "123456789-123456789"

  return msgId
}

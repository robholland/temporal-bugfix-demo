package main

import (
	"log"

	"demo"

	"go.temporal.io/sdk/client"
	"go.temporal.io/sdk/worker"
)

var act *demo.Activities

func main() {
	c, err := client.Dial(client.Options{})
	if err != nil {
		log.Fatalln("Unable to create client", err)
	}
	defer c.Close()

	w := worker.New(c, "bugfix-demo", worker.Options{})

	w.RegisterWorkflow(demo.Greeter)
	w.RegisterActivity(act)

	err = w.Run(worker.InterruptCh())
	if err != nil {
		log.Fatalln("Unable to start worker", err)
	}
}

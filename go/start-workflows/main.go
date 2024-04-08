package main

import (
	"context"
	"log"

	"demo"

	"go.temporal.io/sdk/client"
)

func main() {
	c, err := client.Dial(client.Options{})
	if err != nil {
		log.Fatalln("Unable to create client", err)
	}
	defer c.Close()

	we, err := c.ExecuteWorkflow(context.Background(),
		client.StartWorkflowOptions{
			TaskQueue: "bugfix-demo",
			ID:        "bugfix-demo-alice",
		},
		demo.Greeter,
		"Alice",
	)

	if err != nil {
		log.Fatalln("Unable to execute workflow", err)
	}

	log.Println("Started workflow", "WorkflowID", we.GetID())

	we, err = c.ExecuteWorkflow(context.Background(),
		client.StartWorkflowOptions{
			TaskQueue: "bugfix-demo",
			ID:        "bugfix-demo-bob",
		},
		demo.Greeter,
		"Bob",
	)

	if err != nil {
		log.Fatalln("Unable to execute workflow", err)
	}

	log.Println("Started workflow", "WorkflowID", we.GetID())
}

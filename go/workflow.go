package demo

import (
	"time"

	"go.temporal.io/sdk/temporal"
	"go.temporal.io/sdk/workflow"
)

func Greeter(ctx workflow.Context, name string) (string, error) {
	ao := workflow.ActivityOptions{
		StartToCloseTimeout: 1 * time.Second,
		RetryPolicy: &temporal.RetryPolicy{
			InitialInterval: 1 * time.Second,
			MaximumInterval: 5 * time.Second,
		},
	}
	ctx = workflow.WithActivityOptions(ctx, ao)

	logger := workflow.GetLogger(ctx)

	logger.Info("Workflow started")

	var greeting string

	err := workflow.ExecuteActivity(ctx,
		act.PickGreeting,
		name,
	).Get(ctx, &greeting)
	if err != nil {
		return "", err
	}

	err = workflow.ExecuteActivity(ctx,
		act.SendSMS,
		greeting,
		name,
	).Get(ctx, nil)
	if err != nil {
		return "", err
	}

	var msgId string

	err = workflow.ExecuteActivity(ctx,
		act.SendEmail,
		greeting,
		name,
	).Get(ctx, &msgId)
	if err != nil {
		return "", err
	}

	logger.Info("Workflow completed.")

	return msgId, nil
}

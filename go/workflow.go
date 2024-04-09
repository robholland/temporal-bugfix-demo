package demo

import (
	"time"

	"go.temporal.io/sdk/temporal"
	"go.temporal.io/sdk/workflow"
)

func Greeter(ctx workflow.Context, name string) error {
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

	pick := workflow.ExecuteActivity(ctx,
		act.PickGreeting,
		name,
	)
	err := pick.Get(ctx, &greeting)
	if err != nil {
		return err
	}

	sms := workflow.ExecuteActivity(ctx,
		act.SendSMS,
		greeting,
		name,
	)

	email := workflow.ExecuteActivity(ctx,
		act.SendEmail,
		greeting,
		name,
	)

	err = sms.Get(ctx, nil)
	if err != nil {
		return err
	}

	err = email.Get(ctx, nil)
	if err != nil {
		return err
	}

	logger.Info("Workflow completed.")

	return nil
}

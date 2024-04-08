package demo

import (
	"errors"
	"fmt"
)

type Activities struct{}

var act *Activities

func (a *Activities) PickGreeting(name string) (string, error) {
	fmt.Printf("--> pickGreeting(name: %s)\n", name)
	switch name {
	case "Alice":
		return "Awesome", nil
	case "Bob":
		return "Brilliant", nil
	default:
		return "Infamous", nil
	}
}

func (a *Activities) SendSMS(greeting string, name string) error {
	fmt.Printf("--> sendSMS(greeting: %s, name: %s)\n", greeting, name)

	hasBug := false

	if hasBug && name == "Bob" {
		return errors.New("oops, can't send to Bob right now")
	}

	fmt.Printf("*** SMS: Hey %s %s!\n", greeting, name)

	return nil
}

func (a *Activities) SendEmail(greeting string, name string) (string, error) {
	fmt.Printf("--> sendEmail(greeting: %s, name: %s)\n", greeting, name)

	fmt.Printf("*** Email: Hey %s %s!\n", greeting, name)

	msgId := "123456789-123456789"
	return msgId, nil
}

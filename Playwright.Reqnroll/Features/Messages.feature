@helper @ignore
Feature: Popup and toast messages
  Purpose: Provide reusable steps for verifying message visibility

  Scenario Outline: Verify that a specific message is displayed
    Then I should see the "<message>" message

    Examples:
      | message              |
      | Successfully Saved   |
      | Successfully Deleted |
      | Successfully Updated |
      | No Records Found     |

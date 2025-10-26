@helper @ignore
Feature: Navigation

  Scenario: Navigate to Admin section
    Given I am logged in as admin
    When I navigate to the "Admin" section

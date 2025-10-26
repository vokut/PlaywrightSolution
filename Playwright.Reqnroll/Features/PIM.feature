Feature: PIM employee management
  As an HR administrator
  I want to create, update, and delete employees
  So that personnel records remain accurate

  Background:
    Given I am logged in as admin
    When I navigate to the "PIM" section

  Scenario: Create a new employee
    When I open the PIM "Add Employee" section
    And I add a new employee

  Scenario: Edit an employee's contact details
    When I create a temporary employee
    And I open the PIM "Employee List" section
    And I search for that employee by ID
    And I edit the employee's contact details
    Then I should see the "Successfully Updated" message

  Scenario: Delete an employee
    When I create a temporary employee
    And I open the PIM "Employee List" section
    And I search for that employee by ID
    And I delete that employee
    Then I should see the "Successfully Deleted" message
    Then I should see the "No Records Found" message

  Scenario: Cancel employee deletion
    When I create a temporary employee
    And I open the PIM "Employee List" section
    And I search for that employee by ID
    And I start deleting the employee but cancel the confirmation
    Then I should still find that employee in the list

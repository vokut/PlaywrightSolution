Feature: Admin management
  As an admin user
  I want to manage job titles and locations
  So that organizational data stays up to date

  Background:
    Given I am logged in as admin
    When I navigate to the "Admin" section

  Scenario: Create a new job title
    When I open the "Job" -> "Job Titles" section
    And I add a new job title
    Then I should see the "Successfully Saved" message

  Scenario: Create a new location
    When I open the "Organization" -> "Locations" section
    And I add a new location
    Then I should see the "Successfully Saved" message

  Scenario: Delete a location
    When I open the "Organization" -> "Locations" section
    And I create a temporary location
    And I delete that location
    Then I should see the "Successfully Deleted" message
    Then I should see the "No Records Found" message

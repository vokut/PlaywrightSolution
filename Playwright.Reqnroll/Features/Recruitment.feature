Feature: Recruitment management
  As an HR administrator
  I want to manage candidates
  So that I can track recruitment activities efficiently

  Background:
    Given I am logged in as admin
    When I navigate to the "Recruitment" section

  Scenario: Create a new candidate
    When I open the Recruitment "Candidates" section
    And I add a new candidate

  Scenario: Search for a candidate by keywords
    When I create two temporary candidates
    When I navigate to the "Recruitment" section
    And I search for candidates by an invalid keyword
    Then I should see the "No Records Found" message
    When I search for candidates by the first candidate's keyword
    Then I should see exactly one matching record

  Scenario: Delete a candidate
    When I create a temporary candidate
    When I navigate to the "Recruitment" section
    And I search for that candidate by keyword
    And I delete that candidate
    Then I should see the "Successfully Deleted" message
    Then I should see the "No Records Found" message


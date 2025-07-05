@API
Feature: Get Users

    Scenario: Successfully get paginated list for all users
        When I request the user paginated list
        
        And the response should include the following users:
          | id | email                      |
          | 7  | michael.lawson@reqres.in   |
          | 8  | lindsay.ferguson@reqres.in |
          
    Scenario: Attempt to get user using invalid id
        When I request get user using invalid id
        
        Then User receives not found response
        

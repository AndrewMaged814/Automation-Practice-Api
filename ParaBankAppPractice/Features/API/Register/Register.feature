@API
Feature: User Registration

    Scenario: Successfully register a user via API
        When I send a registration request with the following data:
          | email              | password |
          | eve.holt@reqres.in | pistol   |
        Then User receives success response
        Then I fetch this user
        Then User details should include:
          | email              |
          | eve.holt@reqres.in |

    Scenario: Unsuccessfull register a user via API
        When I send attempt registration request with the following data:
          | email              |
          | eve.holt@reqres.in |
        Then User receives an error
          | error            |
          | Missing password |
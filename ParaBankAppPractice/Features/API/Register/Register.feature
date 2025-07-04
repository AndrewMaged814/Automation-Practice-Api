@API
Feature: User Registration

    Scenario: Successfully register a new user
        Given I am on the ParaBank registration page
        When I fill in the registration form with valid details:
          | First Name | Last Name | Address        | City     | State | Zip Code | Phone      | SSN      | Username | Password | Confirm  |
          | John       | Doe       | 123 Elm Street | New York | NY    | 10001    | 1234567890 | 999-99-9 | johndemo | Pass123! | Pass123! |
        And I submit the registration form
        Then I should be redirected to the account services page
        And I should see a welcome message containing "Welcome"
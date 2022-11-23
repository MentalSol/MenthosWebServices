Feature: StudentsServiceTests
As a Developer
I want to add new Student through API
In order to make it available for applications.

    Background:
        Given the Endpoint https://localhost:7125/api/v1/student/sign-up is available
        
    @student-adding
    Scenario: Add Student
        When a Post Request is sent
          | Name     | Lastname | Username  | Codigo     | email                  | telephone | PasswordHash |
          | Leonardo | Aquino   | leonardoA | u20201b949 | leoac_2002@hotmail.com | 931350631 | leonardo     |
        Then A Response is received with Status 200
        And a Student Resource is included in Response Body
          | Id | Name     | Lastname | Username  | Codigo     | email                  | telephone | PasswordHash |
          | 1  | Leonardo | Aquino   | leonardoA | u20201b949 | leoac_2002@hotmail.com | 931350631 | leonardo     |
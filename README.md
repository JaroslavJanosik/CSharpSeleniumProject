Simple Automation Framework based on C#, NUnit and Selenium WebDriver.

### How to use this framework?
- clone the repository to your workspace
- open the project (SendEmailProject.sln) in the Microsoft Visual Studio Community 2022
- set a BrowserType in the *appsetting.json* file
- build the solution
- run the tests via Visual Studio Test Explorer

### Test case
```
Application: Seznam.cz Email
Test environment url: https://email.seznam.cz/

Scenario: Send an email with an attachment

	Given User goes to the application's Login page
	When User enters a username and password
	And User clicks on the Sign in button
	Then User is logged in
	And the Home page is loading successfully

	Given User is on the Home page
	When User clicks a Compose button in the Navigation panel
	Then the Compose email dialog is open
	When User enters a recipient, subject, email body 
	And User adds an attachment
	And User clicks on a Send button
	Then the email is sent successfully
	And the Compose email dialog is closed
	And the Notification message is displayd
	When User clicks on the Sent button in the Navigation panel
	Then a list of sent emails is displayd in the Content section
	
	Given User checks the last email in the list
	When User clicks on Users button
	Then the Action menu is open
	When User clicks on the Logout action
	Then User is logged out successfully
  
	Given User is on the application's Login page
  ```

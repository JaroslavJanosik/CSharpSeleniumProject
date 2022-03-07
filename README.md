## Assignment
Below are described the events in three blocks, which are arranged from the least complex to the most complex. Your task is to choose at least one block, write a test case for it (in English) and automate it in any selected automation tool.

#### Block level 1:
- Login to email (yours)
- Logout

#### Block level 2:
- Email Login (yours)
- Creating an email message for recipients from contacts *(tip: if you don't have it, save your email to your contacts so that you don't remember another email during the tests)*
- Sending an email
- Logout

#### Block level 3:
- Email Login (yours)
- Creating an email message for recipients from contacts *(tip: if you don't have it, save your email to your contacts so that you don't remember another email during the tests)*
- Inserting an attachment
- Sending an email
- Logout
***********************************************************************
## Solution
### Automation Framework 

Simple Automation Framwork based on C#, NUnit and Selenium WebDriver.

#### Tools
- Microsoft Visual Studio Community 2019 (16.11.5)
#### Dependencies 
#### Frameworks
- Microsoft.NETCore.App
- Microsoft.WindowsDesktop.App
#### Packages
- Microsoft.NET.Test.Sdk (17.1.0)
- Microsoft.Extensions.Configuration (6.0.0)
- Microsoft.Extensions.Configuration.Abstractions (6.0.0)
- Microsoft.Extensions.Configuration.Binder (6.0.0)
- Microsoft.Extensions.Configuration.Json (6.0.0)
- Microsoft.Extensions.DependencyInjection.Abstractions (6.0.0)
- NUnit (3.13.2)
- NUnit3TestAdapter (4.1.0)
- DotNetSeleniumExtras.PageObjects (3.11.0)
- DotNetSeleniumExtras.PageObjects.Core (4.0.1)
- DotNetSeleniumExtras.WaitHelpers (3.11.0) 
- Selenium.Support (4.1.0)
- Selenium.WebDriver (4.1.0)

### How to use this framework?
- clone the repository to your workspace
- open the project (IndraAvitech.sln) in the Microsoft Visual Studio Community 2019
- download a chromedriver or geckodriver driver compatible with your currently installed browser
- set a driver path in the *appsetting.json* file
- build the solution
- run the tests via Visual Studio Test Explorer

### Test case
```
Application: Seznam.cz Email
Test environment url: https://login.szn.cz/

Scenario: Send an email with an attachment

	Given Jaroslav goes to the application's Login page
	When Jaroslav enters a username and password
	And Jaroslav clicks on the Sign in button
	Then Jaroslav is logged in
	And the Home page is loading successfully

	Given Jaroslav is on the Home page
	When Jaroslav clicks a Compose button in the Navigation panel
	Then the Compose email dialog is open
	When Jaroslav enters a recipient, subject, email body 
	And Jaroslav adds an attachment
	And Jaroslav clicks on a Send button
	Then the email is sent successfully
	And the Compose email dialog is closed
	And the Notification message is displayd
	When Jaroslav clicks on the Sent button in the Navigation panel
	Then a list of sent emails is displayd in the Content section
	
	Given Jaroslav checks the last email in the list
	When Jaroslav clicks on Users button
	Then the Action menu is open
	When Jaroslav clicks on the Logout action
	Then Jaroslav is logged out successfully
  
	Given Jaroslav is on the application's Login page
  ```
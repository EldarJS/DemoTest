# DemoProject (Automated Web Test with C# & Playwright)

This project demonstrates an end-to-end automated UI test built with C#, NUnit, and Microsoft Playwright.

## About
This project is an automated test suite written in **C#** with **Playwright** and **NUnit**.  
It was created as a test assignment and automates the purchase flow on the (https://www.saucedemo.com/) application:
- Login with valid credentials,
- Navigate to the **Sauce Labs Bolt T-Shirt**,
- Add the item to the shopping cart,
- Proceed to checkout,
- Fill in customer details,
- Complete the order,
- Take a screenshot of the confirmation page;
## Tech Stack
- [.NET 8](https://dotnet.microsoft.com/),
- [Playwright for .NET](https://playwright.dev/dotnet/docs/intro),
- [NUnit](https://nunit.org/),
- GitHub Actions for CI/CD;
## Installation & Run (locally)
1. Clone the repo:
- git clone;
- cd;
2. Install dependencies:
- dotnet restore;
3. Install Playwright browsers:
- dotnet build;
- dotnet add package Microsoft.Playwright;
- dotnet add package Microsoft.Playwright.NUnit;
- dotnet tool install --global Microsoft.Playwright.CLI;
- playwright install;
5. Run tests:
- dotnet test;
## Credentials
Tests use environment variables for DemoProject login:
- SAUCE_USERNAME;
- SAUCE_PASSWORD;
## FYI
- In GitHub Actions, they run in headless mode;
- If you want to see the test execution, change Headless => from **true** to **false**;
- In GitHub Actions, credentials are securely stored as repository secrets;

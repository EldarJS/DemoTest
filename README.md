# DemoProject (Automated Web Test with C# & Playwright)

This project demonstrates an end-to-end automated UI test built with C#, NUnit, and Microsoft Playwright.

## The test suite covers:

- **LoginTest**: Login to SauceDemo with configurable credentials (via environment variables or default demo user).
- **PurchaseTShirtTest**: Purchase flow for the product "Sauce Labs Bolt T-Shirt": 
1. Navigate to the product
2. Add to cart
3. Proceed to checkout
4. Fill in customer details
5. Complete the order
## Technologies used
1. .NET 8

2. NUnit (unit test framework)

3. Microsoft Playwright (browser automation)

Visual artifacts: screenshots taken at key steps are saved to the TestResults folder.
